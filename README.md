# KubernetesDockerDemo
This code basically exposes an aspnet core api to get and post books list from the underlying mysql database.
If you run the commands from the same folder apply below Commands to get application running on the loadbalancer external IP url

kubectl apply -f .\mysql-statefulset.yaml

kubectl apply -f .\servicedeployment.yaml
