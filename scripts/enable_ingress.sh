cd "$(dirname "$0")"
minikube addons enable ingress
kubectl patch configmap tcp-services -n ingress-nginx --patch "{\"data\":{\"1883\":\"default/mosquitto-service:1883\"}}"
kubectl get configmap tcp-services -n ingress-nginx -o yaml
cd ../K8s/Ingress
kubectl patch deployment ingress-nginx-controller --patch "$(cat ingress-nginx-controller-patch.yaml)" -n ingress-nginx
read -p "Press any key to exit..." x