## Run the project

```shell

    # local env
    # Depends on Mysql service running on port 13306
    # Depends on Redis service running on port 16379

    cd Verification.Api

    dotnet run

    # To check  http://localhost:5001/swagger/index.html


    # local docker env or production docker env)

    cd Verification.Api

    docker compose up -d --build

    # To check  http://localhost:5000/swagger/index.html
    # note: the port is different from local.

```

## Unit test the project

```shell

    cd Verification.UnitTests

    dotnet test

```

## Notes

It can be used in Vscode smoothly.

There may be a problem that the smart hint does not work on Rider IDE. The reason is that the project contains 2 solutions. Rider cannot find the correct csproj. You can switch the project to "backup" branch. There is no unit test content in this branch.
