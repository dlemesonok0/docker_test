FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build

WORKDIR /src

COPY *.csproj ./
RUN dotnet restore

COPY . .

RUN dotnet publish -c Release -o /app/out


FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final

LABEL MAINTAINER="Дмитрий Лемешонок"
ENV APP_VERSION="1.0.0"

WORKDIR /app

RUN apk add --no-cache curl

COPY --from=build /app/out .

RUN adduser -D -g '' appuser && chown -R appuser:appuser /app

USER appuser

ENTRYPOINT ["dotnet", "TodoApi.dll"]
