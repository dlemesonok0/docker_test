FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY *.csproj ./
RUN dotnet restore

COPY . .

RUN dotnet publish -c Release -o /app/out


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

LABEL MAINTAINER="Дмитрий Лемешонок"
ENV APP_VERSION="1.0.0"

WORKDIR /app

RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*

COPY --from=build /app/out .

RUN useradd -m appuser && chown -R appuser:appuser /app

USER appuser

ENTRYPOINT ["dotnet", "TodoApi.dll"]