from dataset import COCODataset
import torchvision.transforms as T
import os
from torch.serialization import add_safe_globals
from torchvision.models.detection.faster_rcnn import FasterRCNN
import torch
import cv2

transform = T.Compose([T.ToTensor()])

add_safe_globals([FasterRCNN])
device = torch.device("cuda" if torch.cuda.is_available() else "cpu")
model = torch.load("fire_detector_full.pth", map_location=device, weights_only=False)
model.eval()

train_dataset = COCODataset(
    img_dir="D:\\python\\fire_diplom\\fire-detection-8\\test",
    ann_file="D:\\python\\fire_diplom\\fire-detection-8\\test\\_annotations.coco.json",
    transforms=transform
)

output_dir = "D:\\python\\fire_diplom\\annotated_test"
os.makedirs(output_dir, exist_ok=True)

for idx, (img, target) in enumerate(train_dataset):
    model.eval()
    with torch.no_grad():
        pred = model([img.to(device)])

    # Convert tensor to numpy for OpenCV
    frame = img.permute(1, 2, 0).cpu().numpy()  # C,H,W -> H,W,C
    frame = (frame * 255).astype("uint8")       # scale to 0-255
    frame = cv2.cvtColor(frame, cv2.COLOR_RGB2BGR)

    boxes = pred[0]["boxes"].cpu().numpy()
    scores = pred[0]["scores"].cpu().numpy()
    labels = pred[0]["labels"].cpu().numpy()

    for box, score, label in zip(boxes, scores, labels):
        if score > 0.1:
            x1, y1, x2, y2 = map(int, box)
            cv2.rectangle(frame, (x1, y1), (x2, y2), (0, 0, 255), 2)
            cv2.putText(frame, f"{label} {score:.2f}", (x1, y1-10),
                        cv2.FONT_HERSHEY_SIMPLEX, 0.5, (0,0,255), 2)

    # Save annotated image
    output_path = os.path.join(output_dir, f"annotated_{idx:04d}.jpg")
    cv2.imwrite(output_path, frame)

    if idx % 50 == 0:
        print(f"Saved {idx} images")