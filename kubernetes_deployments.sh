cd "$(dirname "$0")"
cd ./K8s
kubectl apply -f mosquitto_configmap.yaml
kubectl apply -f mosquitto_deployment.yaml
kubectl apply -f analytics_deployment.yaml
read -p "Press any key to exit..." x