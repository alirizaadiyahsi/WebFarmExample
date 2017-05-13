docker rm $(docker ps -aq)
docker-compose up -d ali_redis
sleep 3
docker-compose up -d ali_webapi