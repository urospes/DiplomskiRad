cd "$(dirname "$0")"
cd ./K8s/ConfigMaps
kubectl apply -f mosquitto_configmap.yaml
cd ../Deployments
kubectl apply -f mosquitto_deployment.yaml
kubectl apply -f analytics_deployment.yaml
read -p "Press any key to exit..." x