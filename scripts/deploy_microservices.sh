cd "$(dirname "$0")"
cd ../K8s/Storage
kubectl apply -f influxdb_storageclass.yaml 
kubectl apply -f mongodb_data_storageclass.yaml
kubectl apply -f mongodb_cars_storageclass.yaml
cd ../ConfigMaps
kubectl apply -f mosquitto_configmap.yaml
kubectl apply -f influxdb_configmap.yaml
cd ../Secrets
kubectl apply -f influxdb_secret.yaml
cd ../Deployments
kubectl apply -f mosquitto_deployment.yaml
kubectl apply -f influxdb_statefulset.yaml
helm install mongodb-cars --values mongodb_data_values.yaml bitnami/mongodb
helm install mongodb-data --values mongodb_cars_values.yaml bitnami/mongodb
kubectl apply -f kafka_bridge_deployment.yaml
kubectl apply -f datastore_deployment.yaml
kubectl apply -f grafana_deployment.yaml
kubectl apply -f analytics_deployment.yaml
kubectl apply -f cars_deployment.yaml
kubectl apply -f defects_deployment.yaml
cd ../Ingress
kubectl apply -f ingress_rules.yaml
read -p "Press any key to exit..." x