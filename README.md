# Online Demo (APIs)

[https://vc.keenneed.com/swagger/index.html](https://vc.keenneed.com/swagger/index.html)

## Online Demo (Pages)

[https://vcf.keenneed.com](https://vcf.keenneed.com)

## Run the project

```shell

    # local env

    # Depends on Mysql and Redis
    # You may need to modify connection string base on your own env.

    "ConnectionStrings": {
        "connection": "server=localhost;port=23306;user=root;password=*******;database=DbVerification"
    },
    "RedisCacheSettings": {
        "Enabled": true,
        "connectionString": "localhost:26379"
    }

    # update database
    dotnet ef database update

    # update ef tools
    dotnet tool update --global dotnet-ef


    cd Verification.Api

    dotnet run

    # To check  https://localhost:3234/swagger/index.html

```

## local docker env or production docker env

```shell

    # make sure the database (database=DbVerification) is existed.

    cd Verification.Api

    docker compose up -d --build

    or

    docker-compose up -d --build (old version)
    # To check  http://localhost:3234/swagger/index.html

    # note: the protocol (http | https) is different from local.
```

## Unit test the project

```shell

    cd Verification.UnitTests

    dotnet test

```

## Notes

The project can run in VsCode smoothly.

There may be a problem that the smart hint does not work on Rider IDE. The reason is that the project contains 2 solutions. Rider cannot find the correct csproj. You can switch the project to "backup" branch. There is no unit test content in this branch.
