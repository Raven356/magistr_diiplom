import torchvision
import torch
import torch.nn as nn
from torchvision.models.detection.faster_rcnn import FasterRCNN
from torchvision.models.detection.backbone_utils import resnet_fpn_backbone
from torchvision.models.detection.faster_rcnn import FastRCNNPredictor

def get_model_old(num_classes):
    # Load detection model (no pretrained weights)
    model = torchvision.models.detection.fasterrcnn_resnet50_fpn(weights=None)

    # Replace head
    in_features = model.roi_heads.box_predictor.cls_score.in_features
    model.roi_heads.box_predictor = torchvision.models.detection.faster_rcnn.FastRCNNPredictor(in_features, num_classes)

    return model

import torchvision
import torch
import torch.nn as nn
from torchvision.models.detection import FasterRCNN
from torchvision.models.detection.backbone_utils import resnet_fpn_backbone
from torchvision.models.detection.faster_rcnn import FastRCNNPredictor


class EnhancedFastRCNNPredictor(nn.Module):
    def __init__(self, in_channels, num_classes):
        super().__init__()

        self.fc1 = nn.Linear(in_channels, 1024)
        self.bn1 = nn.BatchNorm1d(1024)
        self.drop1 = nn.Dropout(0.5)

        self.fc2 = nn.Linear(1024, 512)
        self.bn2 = nn.BatchNorm1d(512)
        self.drop2 = nn.Dropout(0.3)

        self.cls_score = nn.Linear(512, num_classes)
        self.bbox_pred = nn.Linear(512, num_classes * 4)

        for layer in [self.fc1, self.fc2, self.cls_score, self.bbox_pred]:
            nn.init.kaiming_normal_(layer.weight, mode='fan_out', nonlinearity='relu')
            nn.init.constant_(layer.bias, 0)

    def forward(self, x):
        x = self.drop1(torch.relu(self.bn1(self.fc1(x))))
        x = self.drop2(torch.relu(self.bn2(self.fc2(x))))
        scores = self.cls_score(x)
        bbox_deltas = self.bbox_pred(x)
        return scores, bbox_deltas


def get_model(num_classes, pretrained=True, train_backbone=False):
    backbone = resnet_fpn_backbone(
        backbone_name='resnet50',
        weights='DEFAULT' if pretrained else None
    )

    if not train_backbone:
        for param in backbone.body.parameters():
            param.requires_grad = False

    model = FasterRCNN(backbone, num_classes=num_classes)

    # Replace the default head with the enhanced one
    in_features = model.roi_heads.box_predictor.cls_score.in_features
    model.roi_heads.box_predictor = EnhancedFastRCNNPredictor(in_features, num_classes)

    return model
