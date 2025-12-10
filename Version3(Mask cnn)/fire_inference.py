import cv2
import torch
import torchvision.transforms as T
from torch.serialization import add_safe_globals
from torchvision.models.detection.faster_rcnn import FasterRCNN
from PIL import Image
import io
import numpy as np
import random
import json
import base64

LABEL_NAMES = {
    1: "fire",
    2: "fireplace",
    3: "smoke"
}

class FireDetector:
    def __init__(self, model_path, device=None):
        add_safe_globals([FasterRCNN])
        self.device = device or torch.device("cuda" if torch.cuda.is_available() else "cpu")
        self.model = torch.load(model_path, map_location=self.device, weights_only=False)
        self.model.to(self.device)
        self.model.eval()
        self.class_colors = {}

        self.transform = T.Compose([
            T.ToPILImage(),
            T.Resize((416, 416)),
            T.ToTensor()
        ])

    def _get_color_for_label(self, label_id):
        # deterministic pseudo-random color by label
        lid = int(label_id)
        if lid == 1:
            return (255, 0, 0)       # red
        elif lid == 2:
            return (0, 0, 255)       # blue
        elif lid == 3:
            return (255, 255, 0) 

    def process_frame_np(self, frame_bytes, confidence):
        """
        Accepts bytes of a compressed image (BMP/JPG) and annotates it.
        """
        nparr = np.frombuffer(frame_bytes, np.uint8)
        frame = cv2.imdecode(nparr, cv2.IMREAD_COLOR)  # shape (H, W, 3)

        if frame is None:
            raise ValueError("Failed to decode frame bytes")

        h, w = frame.shape[:2]
        frame_rgb = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)
        img_tensor = self.transform(frame_rgb).unsqueeze(0).to(self.device)
        
        detection_happened = False

        with torch.no_grad():
            preds = self.model(img_tensor)

        boxes = preds[0]['boxes'].cpu().numpy()
        scores = preds[0]['scores'].cpu().numpy()
        labels = preds[0]['labels'].cpu().numpy()

        scale_x = w / 416
        scale_y = h / 416

        fireplace_boxes = []

        # First, collect all class 2 boxes
        for box, score, label in zip(boxes, scores, labels):
            if score > confidence and label == 2:
                fireplace_boxes.append(box)

        detection_happened = False

        for box, score, label in zip(boxes, scores, labels):
            if score < confidence:
                continue

            color = self._get_color_for_label(int(label))
            x1, y1, x2, y2 = box
            x1, x2 = int(x1 * scale_x), int(x2 * scale_x)
            y1, y2 = int(y1 * scale_y), int(y2 * scale_y)
            
            # Draw the box and label
            label_text = LABEL_NAMES.get(label, str(label))
            cv2.rectangle(frame_rgb, (x1, y1), (x2, y2), color, 2)
            cv2.putText(frame_rgb, f"{label_text} {score:.2f}", (x1, y1-10),
                        cv2.FONT_HERSHEY_SIMPLEX, 0.5, color, 2)

            # Detection logic
            if label in [1, 3]:
                inside_fireplace = False
                for f_box in fireplace_boxes:
                    fx1, fy1, fx2, fy2 = f_box
                    fx1, fx2 = int(fx1 * scale_x), int(fx2 * scale_x)
                    fy1, fy2 = int(fy1 * scale_y), int(fy2 * scale_y)
                    # Check if current box is fully inside fireplace
                    if x1 >= fx1 and y1 >= fy1 and x2 <= fx2 and y2 <= fy2:
                        inside_fireplace = True
                        break
                if not inside_fireplace:
                    detection_happened = True

        pil_img = Image.fromarray(frame_rgb)
        buf = io.BytesIO()
        pil_img.save(buf, format='BMP')
        buf.seek(0)
        img_bytes = buf.getvalue()

        # Encode image to base64 for JSON-safe transfer
        encoded = base64.b64encode(img_bytes).decode("utf-8")

        result = {
            "Detected": detection_happened,
            "Image": encoded
        }

        return json.dumps(result)
