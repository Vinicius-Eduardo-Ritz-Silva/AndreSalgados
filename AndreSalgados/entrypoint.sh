#!/bin/bash
set -e

echo "Aguardando SQL Server iniciar..."
for i in {1..30}; do
  if nc -z db 1433; then
    echo "SQL Server pronto"
    break
  fi
  sleep 1
done

echo "Executando migra��es..."
dotnet ef database update --project ../Infrastructure/Infrastructure.csproj --startup-project . --verbose || echo "Migra��es falharam, continuando..."

echo "Iniciando aplica��o..."
exec dotnet "AndreSalgados.dll"