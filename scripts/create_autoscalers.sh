minikube addons enable metrics-server
cd "$(dirname "$0")"
cd ../K8s/HPAs
kubectl create -f datastore_hpa.yaml
kubectl create -f mosquitto_hpa.yaml
read x