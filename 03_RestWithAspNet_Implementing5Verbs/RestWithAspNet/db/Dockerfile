FROM mysql:5.7.22
EXPOSE 3306
COPY ./RestWithAspNet/db/migrations/ /home/database/
COPY ./RestWithAspNet/db/datasets/ /home/database/
COPY ./RestWithAspNet/ci/init_database.sh/ /docker-entrypoint-initdb.d/init_database.sh