import sys
import torch
from torch.utils.data import DataLoader
import torchvision.transforms as T
from tqdm import tqdm
from dataset import COCODataset
from model import get_model, get_model_old

# ========================
# Dataset and Dataloader
# ========================
transform = T.Compose([T.ToTensor()])

train_dataset = COCODataset(
    img_dir="D:\\python\\fire_diplom\\fire-detection-10\\train",
    ann_file="D:\\python\\fire_diplom\\fire-detection-10\\train\\_annotations.coco.json",
    transforms=transform
)

val_dataset = COCODataset(
    img_dir="D:\\python\\fire_diplom\\fire-detection-10\\valid",
    ann_file="D:\\python\\fire_diplom\\fire-detection-10\\valid\\_annotations.coco.json",
    transforms=transform
)

all_labels = []

for _, target in train_dataset:
    # target["labels"] — це тензор із мітками об’єктів на зображенні
    all_labels.extend(target["labels"].tolist())

all_labels = torch.tensor(all_labels)

train_loader = DataLoader(train_dataset, batch_size=4, shuffle=True, collate_fn=lambda x: tuple(zip(*x)))
val_loader = DataLoader(val_dataset, batch_size=2, shuffle=False, collate_fn=lambda x: tuple(zip(*x)))

# ========================
# Model Setup
# ========================
device = torch.device("cuda" if torch.cuda.is_available() else "cpu")
model = get_model_old(num_classes=all_labels.max().item() + 1)  # fire, smoke, background
model.to(device)

# Optimizer
params = [p for p in model.parameters() if p.requires_grad]
optimizer = torch.optim.SGD(params, lr=0.005, momentum=0.9, weight_decay=0.0005)

# ========================
# Validation Function
# ========================
def evaluate(model, data_loader, device):
    model.eval()
    total_images = 0
    total_detections = 0
    total_confidence = 0.0
    count_conf = 0

    with torch.no_grad():
        for images, targets in tqdm(data_loader, desc="Validation", file=sys.stdout, leave=False):
            images = list(img.to(device) for img in images)
            outputs = model(images)

            for target, output in zip(targets, outputs):
                total_images += 1
                scores = output['scores'].cpu().numpy()

                detections = (scores > 0.5)
                total_detections += detections.sum()
                total_confidence += scores[detections].sum()
                count_conf += detections.sum()

    avg_dets_per_img = total_detections / max(total_images, 1)
    avg_conf = total_confidence / max(count_conf, 1)

    print(f"\nValidation Results:")
    print(f"  - Total Images: {total_images}")
    print(f"  - Total Detections: {total_detections}")
    print(f"  - Avg Detections per Image: {avg_dets_per_img:.2f}")
    print(f"  - Avg Confidence: {avg_conf:.3f}")

# ========================
# Training Loop
# ========================
num_epochs = 20

for epoch in range(num_epochs):
    model.train()
    total_loss = 0.0
    total_images = 0

    # Training progress bar -> stderr
    with tqdm(train_loader, desc=f"Epoch {epoch} [Training]", leave=True, file=sys.stderr) as pbar:
        for images, targets in pbar:
            images = list(img.to(device) for img in images)
            targets = [{k: v.to(device) for k, v in t.items()} for t in targets]

            # Skip batch if all images have no boxes
            if all(t["boxes"].shape[0] == 0 for t in targets):
                continue

            loss_dict = model(images, targets)
            losses = sum(loss for loss in loss_dict.values())

            optimizer.zero_grad()
            losses.backward()
            optimizer.step()

            batch_loss = losses.item()
            total_loss += batch_loss
            total_images += len(images)

            avg_loss = total_loss / max(total_images, 1)
            pbar.set_postfix({"avg_loss_per_img": f"{avg_loss:.4f}"})

    # Cleanly finish training bar before validation
    sys.stderr.flush()

    avg_epoch_loss = total_loss / max(total_images, 1)
    evaluate(model, val_loader, device)
    print(f"Epoch {epoch} completed. Avg Training Loss per Image: {avg_epoch_loss:.4f}\n")

# ========================
# Save Model
# ========================
torch.save(model, "fire_detector_smoke_and_fireplace_added_model.pth")
print("✅ Model saved as 'fire_detector_smoke_and_fireplace_added_model.pth'")
