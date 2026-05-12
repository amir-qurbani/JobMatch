# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Kopiera projektfilen
COPY *.csproj ./
RUN dotnet restore 

# Kopiera all kod och bygg
COPY . ./
RUN dotnet publish -c Release -o /out

#2. Kör appen
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

COPY --from=build /out .

ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT [ "dotnet", "JobMatch.API.dll" ]

