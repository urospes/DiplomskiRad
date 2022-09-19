cd "$(dirname "$0")"
cd ../K8s/Storage
kubectl apply -f influxdb_storageclass.yaml 
cd ../ConfigMaps
kubectl apply -f mosquitto_configmap.yaml
kubectl apply -f influxdb_configmap.yaml
cd ../Secrets
kubectl apply -f influxdb_secret.yaml
cd ../Deployments
kubectl apply -f mosquitto_deployment.yaml
kubectl apply -f influxdb_statefulset.yaml
kubectl apply -f kafka_bridge_deployment.yaml
kubectl apply -f datastore_deployment.yaml
kubectl apply -f grafana_deployment.yaml
cd ../Ingress
kubectl apply -f ingress_rules.yaml
read -p "Press any key to exit..." x