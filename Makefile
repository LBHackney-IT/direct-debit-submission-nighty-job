.PHONY: setup
setup:
	docker-compose build

.PHONY: build
build:
	docker-compose build direct-debit-submission-nighty-job

.PHONY: serve
serve:
	docker-compose build direct-debit-submission-nighty-job && docker-compose up direct-debit-submission-nighty-job

.PHONY: shell
shell:
	docker-compose run direct-debit-submission-nighty-job bash

.PHONY: test
test:
	docker-compose up dynamodb-database & docker-compose build direct-debit-submission-nighty-job-test && docker-compose up direct-debit-submission-nighty-job-test

.PHONY: lint
lint:
	-dotnet tool install -g dotnet-format
	dotnet tool update -g dotnet-format
	dotnet format

.PHONY: restart-db
restart-db:
	docker stop $$(docker ps -q --filter ancestor=dynamodb-database -a)
	-docker rm $$(docker ps -q --filter ancestor=dynamodb-database -a)
	docker rmi dynamodb-database
	docker-compose up -d dynamodb-database
