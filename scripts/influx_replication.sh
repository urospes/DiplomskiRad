#creating remote connection to another influxdb instance, subcommand is used to get the org-id of the remote repository
kubectl exec influxdb-0 -- bash -c "influx remote create --name cloud-remote1 --remote-url http://influxdb-1.influxdb:8086  \
--remote-api-token token --remote-org-id $(kubectl exec influxdb-1 -- bash -c "influx org list | awk 'NR==2{print \$1}'")"

#using the created remote to create replication stream
kubectl exec influxdb-0 -- bash -c "influx replication create --name cloud-replica1 \
--local-bucket-id $(kubectl exec influxdb-0 -- bash -c "influx bucket list | awk '/car_data/ {print \$1}'") \
--remote-bucket-id $(kubectl exec influxdb-1 -- bash -c "influx bucket list | awk '/car_data/ {print \$1}'")  \
--remote-id $(kubectl exec influxdb-0 -- bash -c "influx remote list | awk '/cloud-remote1/ {print \$1}'")"

read -p "Press any key to exit..." x