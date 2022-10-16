kubectl create namespace kafka
cd "$(dirname "$0")"
cd ../K8s/Kafka
#kubectl create -f 'https://strimzi.io/install/latest?namespace=kafka' -n kafka
#kubectl apply -f https://strimzi.io/examples/latest/kafka/kafka-persistent-single.yaml -n kafka
kubectl create -f kafka-config.yaml -n kafka
kubectl apply -f kafka-cluster.yaml -n kafka
kubectl wait kafka/my-cluster --for=condition=Ready --timeout=300s -n kafka
read -p "Press any key to exit..." x