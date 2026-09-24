# frame-agriculture-api

Core delivery C# ASP.NET backend template.

* [Install MongoDB](#install-mongodb)
* [Inspect MongoDB](#inspect-mongodb)
* [Testing](#testing)
* [Running](#running)
* [Dependabot](#dependabot)


### Docker Compose

A Docker Compose template is in [compose.yml](compose.yml).

A local environment with:

- Localstack for AWS services (S3, SQS)
- Redis
- MongoDB
- This service.
- A commented out frontend example.

```bash
docker compose up --build -d
```

A more extensive setup is available in [github.com/DEFRA/cdp-local-environment](https://github.com/DEFRA/cdp-local-environment)

### MongoDB

#### MongoDB via Docker

See above.

```
docker compose up -d mongodb
```

#### MongoDB locally

Alternatively install MongoDB locally:

- Install [MongoDB](https://www.mongodb.com/docs/manual/tutorial/#installation) on your local machine
- Start MongoDB:
```bash
sudo mongod --dbpath ~/mongodb-cdp
```

#### MongoDB in CDP environments

In CDP environments a MongoDB instance is already set up
and the credentials exposed as enviromment variables.


### Inspect MongoDB

To inspect the Database and Collections locally:
```bash
mongosh
```

You can use the CDP Terminal to access the environments' MongoDB.

### Testing

Run the tests with:

Tests run by running a full `WebApplication` backed by [Ephemeral MongoDB](https://github.com/asimmon/ephemeral-mongo).
Tests do not use mocking of any sort and read and write from the in-memory database.

```bash
dotnet test
````

### Running

Run CDP-Deployments application:
```bash
dotnet run --project FrameAgricultureApi --launch-profile FrameAgricultureApi
```

In Development, open [Scalar](http://localhost:8085/scalar) to browse and try the API.
The generated OpenAPI document is available at [openapi/v1.json](http://localhost:8085/openapi/v1.json).

### Rate limiting

All requests share a fixed-window limit of 100 requests per 60 seconds per application
instance, including the development documentation endpoints. `/health` is exempt.
Excess requests are rejected immediately with HTTP `429`, a problem-details response,
and a `Retry-After` header indicating how many seconds to wait.

Configure positive values for `RateLimiting:PermitLimit` and `RateLimiting:WindowSeconds`
in `appsettings.json`, or override them with the environment variables
`RateLimiting__PermitLimit` and `RateLimiting__WindowSeconds`. Restart the application
after changing these values. Counters are held in memory and are not shared across replicas.

### SonarCloud

Example SonarCloud configuration are available in the GitHub Action workflows.

### Dependabot

We have added an example dependabot configuration file to the repository. You can enable it by renaming
the [.github/example.dependabot.yml](.github/example.dependabot.yml) to `.github/dependabot.yml`


### About the licence

The Open Government Licence (OGL) was developed by the Controller of Her Majesty's Stationery Office (HMSO) to enable
information providers in the public sector to license the use and re-use of their information under a common open
licence.

It is designed to encourage use and re-use of information freely and flexibly, with only a few conditions.
