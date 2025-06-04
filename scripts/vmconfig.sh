#!/bin/bash

wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb 
sudo dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb

sudo apt-get update
sudo apt-get install -y apt-transport-https
sudo apt-get update
sudo apt-get install -y dotnet-sdk-8.0

cd /home/azureuser
git clone https://github.com/AndrewHrechyn/mslearn-load-balance-web-traffic-with-application-gateway.git vehicleapp

export DOTNET_CLI_HOME=/home/azureuser
cd /home/azureuser/vehicleapp/vehicles
dotnet build
dotnet publish -o published -c Release
sudo mkdir -p /opt/vehicleapp
sudo cp -r published/* /opt/vehicleapp

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
