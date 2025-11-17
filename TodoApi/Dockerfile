FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

COPY *.csproj ./
RUN dotnet restore

COPY . .

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS final

LABEL MAINTAINER="Дмитрий Лемешонок"

ENV APP_VERSION="1.0.0"

WORKDIR /app

COPY --from=build /app/out .

RUN useradd -m appuser && chown -R appuser:appuser /app

USER appuser

ENTRYPOINT ["dotnet", "/app/out/TodoApi.dll"]