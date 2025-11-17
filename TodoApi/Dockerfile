FROM mcr.microsoft.com/dotnet/sdk:9.0

WORKDIR /app

COPY . .

RUN dotnet publish -c Release -o out

ENTRYPOINT ["dotnet", "/app/out/TodoApi.dll"]