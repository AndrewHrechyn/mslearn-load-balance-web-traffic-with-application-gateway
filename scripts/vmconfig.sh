#!/bin/bash

# Встановлення .NET SDK 8.0
wget https://packages.microsoft.com/config/ubuntu/18.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb

sudo apt-get update
sudo apt-get install -y apt-transport-https
sudo apt-get update
sudo apt-get install -y dotnet-sdk-8.0

# Клонування актуального репозиторію
cd /home/azureuser
git clone https://github.com/AndrewHrechyn/mslearn-load-balance-web-traffic-with-application-gateway.git vehicleapp

# Збірка та публікація веб-додатку
export DOTNET_CLI_HOME=/home/azureuser
cd /home/azureuser/vehicleapp/vehicles
dotnet build
dotnet publish -o published -c Release
sudo mkdir -p /opt/vehicleapp
sudo cp -r published/* /opt/vehicleapp

# Налаштування systemd сервісу
cat << EOD | sudo tee /etc/systemd/system/vehicleapp.service
[Unit]
Description=Vehicle Web App

[Service]
WorkingDirectory=/opt/vehicleapp
ExecStart=/usr/bin/dotnet /opt/vehicleapp/vehicles.dll --urls "http://0.0.0.0:80"
Restart=always
RestartSec=10
User=azureuser

[Install]
WantedBy=multi-user.target
EOD

sudo systemctl daemon-reload
sudo systemctl enable vehicleapp.service
sudo systemctl start vehicleapp.service
sudo systemctl status vehicleapp.service
