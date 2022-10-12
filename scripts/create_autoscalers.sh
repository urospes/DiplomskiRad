minikube addons enable metrics-server
cd "$(dirname "$0")"
cd ../K8s/HPAs
kubectl create -f datastore_hpa.yaml
kubectl create -f mosquitto_hpa.yaml
kubectl apply -f cars_hpa.yaml
kubectl apply -f defects_hpa.yaml
kubectl apply -f gateway_hpa.yaml
read -p "Press any key to exit..." x