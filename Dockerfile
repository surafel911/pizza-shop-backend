# https://hub.docker.com/_/microsoft-dotnet-core
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY PizzaShopWebService/*.csproj ./PizzaShopWebService/
RUN dotnet restore -r linux-musl-x64

# copy everything else and build app
COPY PizzaShopWebService/. ./PizzaShopWebService/
WORKDIR /source/PizzaShopWebService
RUN dotnet build -o /app -r linux-musl-x64

# final stage/image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine
WORKDIR /app
COPY --from=build /app ./
EXPOSE 5432
EXPOSE 5001
ENTRYPOINT ["dotnet", "PizzaShopWebService.dll"]
