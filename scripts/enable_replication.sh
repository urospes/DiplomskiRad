for (( i=1 ; i<$1 ; i++ ));
do
  remotehost="influxdb-$i"
  remotename="cloud-remote$i"
  replicaname="cloud-replica$i"

  #creating remote connection to another influxdb instance, subcommand is used to get the org-id of the remote repository
  kubectl exec influxdb-0 -- bash -c "influx remote create --name $remotename --remote-url http://$remotehost.influxdb:8086  \
  --remote-api-token token --remote-org-id $(kubectl exec $remotehost -- bash -c "influx org list | awk 'NR==2{print \$1}'")"

  #using the created remote to create replication stream
  kubectl exec influxdb-0 -- bash -c "influx replication create --name $replicaname \
  --local-bucket-id $(kubectl exec influxdb-0 -- bash -c "influx bucket list | awk '/car_data/ {print \$1}'") \
  --remote-bucket-id $(kubectl exec $remotehost -- bash -c "influx bucket list | awk '/car_data/ {print \$1}'")  \
  --remote-id $(kubectl exec influxdb-0 -- bash -c "influx remote list | awk '/$remotename/ {print \$1}'")"
done

read -p "Press any key to exit..." x