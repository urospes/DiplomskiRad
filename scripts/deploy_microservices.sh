cd "$(dirname "$0")"
cd ../K8s/ConfigMaps
kubectl apply -f mosquitto_configmap.yaml
cd ../Deployments
kubectl apply -f mosquitto_deployment.yaml
kubectl apply -f kafka_bridge_deployment.yaml
kubectl apply -f datastore_deployment.yaml
cd ../Ingress
kubectl apply -f ingress_rules.yaml
read -p "Press any key to exit..." x