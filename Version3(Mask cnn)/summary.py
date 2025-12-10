from torchinfo import summary
import torch
from model import get_model

model = get_model(num_classes=2)
device = torch.device("cuda" if torch.cuda.is_available() else "cpu")
model.to(device)

summary(model, input_size=[(1, 3, 224, 224)], device=device)