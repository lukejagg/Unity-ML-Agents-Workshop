# Unity-ML-Agents-Workshop
 Workshop project to showcase Unity ML Agents package

# Installation
1. Change current directory:
cd (project folder dir)

2. Create virtual python environment
py -m venv venv

3. Activate virtual environment
venv\Scripts\activate

4. Install packages
py -m pip install --upgrade pip
pip install torch
pip install mlagents

# Usage
## Enable mlagents
mlagents-learn \[--force] \[--run-id=ID]

## Visualization
tensorboard --logdir (results)
