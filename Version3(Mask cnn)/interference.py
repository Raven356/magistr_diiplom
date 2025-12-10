import cv2
import torch
import torchvision.transforms as T
from torch.serialization import add_safe_globals
from torchvision.models.detection.faster_rcnn import FasterRCNN
import random

# Allow loading the full model
add_safe_globals([FasterRCNN])
device = torch.device("cuda" if torch.cuda.is_available() else "cpu")

# Load model
model = torch.load("fire_detector_smoke_and_fireplace_added_model.pth", map_location=device, weights_only=False)
model.to(device)
model.eval()

# Video capture
cap = cv2.VideoCapture(r"D:\python\fire_diplom\Version2(RCNN)\test_videos\test2.mp4")

# Transform: resize to 416x416 and convert to tensor
transform = T.Compose([
    T.ToPILImage(),
    T.Resize((416, 416)),
    T.ToTensor()
])

CLASS_COLORS = {}

def get_color_for_label(label_id):
    """Return a consistent color (BGR) for each label."""
    if label_id not in CLASS_COLORS:
        # Generate a random bright color
        CLASS_COLORS[label_id] = (
            random.randint(0, 255),
            random.randint(0, 255),
            random.randint(0, 255)
        )
    return CLASS_COLORS[label_id]

while cap.isOpened():
    ret, frame = cap.read()
    if not ret:
        break

    original_h, original_w = frame.shape[:2]

    # Convert BGR to RGB
    frame_rgb = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)

    # Apply transform and send to device
    img_tensor = transform(frame_rgb).unsqueeze(0).to(device)

    # Inference
    with torch.no_grad():
        preds = model(img_tensor)

    # Extract predictions
    boxes = preds[0]['boxes'].cpu().numpy()
    scores = preds[0]['scores'].cpu().numpy()
    labels = preds[0]['labels'].cpu().numpy()

    # Rescale boxes to original frame size
    scale_x = original_w / 416
    scale_y = original_h / 416

    for box, score, label in zip(boxes, scores, labels):
        if score > 0.3:
            color = get_color_for_label(int(label))
            x1, y1, x2, y2 = box
            x1, x2 = int(x1 * scale_x), int(x2 * scale_x)
            y1, y2 = int(y1 * scale_y), int(y2 * scale_y)
            cv2.rectangle(frame, (x1, y1), (x2, y2), color, 2)
            cv2.putText(frame, f"{label} {score:.2f}", (x1, y1-10),
                        cv2.FONT_HERSHEY_SIMPLEX, 0.5, color, 2)

    # Display
    cv2.imshow("Fire Detection", frame)
    if cv2.waitKey(1) & 0xFF == ord('q'):
        break

cap.release()
cv2.destroyAllWindows()
