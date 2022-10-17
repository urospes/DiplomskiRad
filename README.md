# DiplomskiRad
Diplomski rad na temu "Orkestracija mikroservisa u sistemu za nadgledanje i analizu parametara u automobilima"

## Pokretanje

1) Najpre je potrebno pokrenuti kubernetes klaster. Za potrebe testiranja korišćen je lokalni minikube klaster i kubectl alat

    **minikube start --driver=hyperv --memory=8192 --cpus=4**

2) U folderu DiplomskiRad/scripts nalaze se potrebne skripte za startovanje svih neophodnih servisa

    **enable_ingress** - Za konfiguraciju ingress-controllera
    
    **deploy_kafka.sh** - Kofiguracija kafka klastera sa dva čvora
    
    **setup_ prometheus** i **deploy_prometheus** - Konfiguracija Prometheusa za nagleddanje parametara čitavog klastera, kao i prikupljanje metrika za potrebe skaliranja kontejnera
    
    **deploy_microservices.sh** - Isporučivanje svih deployment-a, servisa, statefulset-ova...
    
    **create_autoscalers.sh** - Kreiranje automatskih skalera za servise
    
    _uoliko je pokrenuto više replika influxdb baze, potrebno je izvršiti i skriptu **influx_replication.sh <broj_replika>**_

3) Komandom minikube ip moguće je dobiti IP adresu klastera, kako bi se u _hosts_ fajl operativnog sistema dodala sledeća preslikavanja

    -minikube ip- grafana.cartracker.com
    
    -minikube ip- grafana.cartracker.com
    
    -minikube ip- grafana.cartracker.com

4) Nakon što su svi pod-ovi uspešno pokrenuti, što je moguće proveriti komandom kubectl get pods --all-namespaces moguće je pristupiti API-ju sistema na adresi
    **http://api.cartracker.com:80** . Dostupni endpoint-i su GET /cars, /cars/{carId}, POST /cars

5) Pomoćna aplikacija se nalazi u folderu SensorDataGenApp i pokreće se komandom **dotnet run _<lista identifikatora>_**
 
6) Za testiranje skaliranja usled povećanja opterećenja moguće je pokrenuti pomoćnu skriptu **startup.bat** u istom folderu

7) Testiranje skaliranja API servisa je moguće isprobati slanjem velikog broja zahteva korišćenjem skripte **requests.sh**
    
