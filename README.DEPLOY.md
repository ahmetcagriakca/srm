git checkout new-master

docker-compose -f docker-compose.prod.yml -f docker-compose.yml -p srm-prod up  -d  --build

git checkout dev

docker-compose -f docker-compose.test.yml -f docker-compose.yml -p srm-test up  -d  --build

git checkout new-master

docker-compose -f docker-compose.hasbahce.yml -f docker-compose.yml -p srm-hasbahce up  -d  --build