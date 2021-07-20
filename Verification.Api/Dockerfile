FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env

ARG BUILDCONFIG=RELEASE
ARG VERSION=1.0.0

COPY Verification.Api.csproj /build/

RUN dotnet restore ./build/Verification.Api.csproj

COPY . ./build/
WORKDIR /build/
RUN dotnet publish ./Verification.Api.csproj -c $BUILDCONFIG -o dist /p:Version=$VERSION

FROM mcr.microsoft.com/dotnet/aspnet:5.0

ENV TZ=Pacific/Auckland
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone

WORKDIR /app
COPY --from=build-env /build/dist .

ENTRYPOINT ["dotnet", "Verification.Api.dll"]