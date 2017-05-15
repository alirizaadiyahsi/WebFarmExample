docker rm $(docker ps -aq)
docker-compose up -d ali_redis
sleep 3
docker-compose up -d ali_webapi
sleep 2
docker-compose scale ali_webapi=3
sleep 2
docker-compose up -d load_balancer