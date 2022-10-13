cd "$(dirname "$0")"
cd ../K8s/Prometheus
kubectl apply -f prometheus.yaml
kubectl apply -f ServiceMonitors
helm repo update
helm install -n monitoring prometheus-http prometheus-community/prometheus-adapter -f values.yaml
read -p "Press any key to exit..." x