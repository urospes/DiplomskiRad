cd "$(dirname "$0")"
cd ../K8s/Prometheus
kubectl apply --server-side -f manifests/setup
kubectl apply -f manifests/
read -p "Press any key to exit..." x