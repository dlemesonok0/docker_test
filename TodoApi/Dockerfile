FROM mcr.microsoft.com/dotnet/sdk:8.0

LABEL MAINTAINER="Дмитрий Лемешонок"

ENV APP_VERSION="1.0.0"

WORKDIR /app

COPY . .

RUN dotnet publish -c Release -o out

ENTRYPOINT ["dotnet", "/app/out/TodoApi.dll"]