@ECHO OFF

ECHO ----------------------------------------------------
ECHO Script para criacao de migrations do Entity Framework
ECHO -----
ECHO Pre-requisitos:
ECHO   1. Runtime do DotNet compativel com o do projeto
ECHO   2. Ferramentas de linha de comando do EF
ECHO      - dotnet tool install --global dotnet-ef
ECHO      (Para atualizar:)
ECHO      - dotnet tool update --global dotnet-ef
ECHO ----------------------------------------------------

SET /p NAME="Informe o nome da migration: "

IF [%NAME%] == [] GOTO end

SET UI_PROJECT=../TheRealBank.UI
SET FOLDER=Migrations

ECHO ---
ECHO PostgreSql:
ECHO ---
dotnet ef migrations add %NAME% --startup-project %UI_PROJECT% --context PostgreSqlContext --output-dir %FOLDER%/PostgreSql
IF %ERRORLEVEL% NEQ 0 GOTO error

ECHO ---
ECHO SqlServer:
ECHO ---
dotnet ef migrations add %NAME% --startup-project %UI_PROJECT% --context SqlServerContext --output-dir %FOLDER%/SqlServer
IF %ERRORLEVEL% NEQ 0 GOTO error

ECHO ---
ECHO MySql:
ECHO ---
dotnet ef migrations add %NAME% --startup-project %UI_PROJECT% --context MySqlContext --output-dir %FOLDER%/MySql
IF %ERRORLEVEL% NEQ 0 GOTO error

ECHO ---
ECHO Sqlite:
ECHO ---
dotnet ef migrations add %NAME% --startup-project %UI_PROJECT% --context SqliteContext --output-dir %FOLDER%/Sqlite
IF %ERRORLEVEL% NEQ 0 GOTO error

ECHO ---
ECHO Script concluido com sucesso!
ECHO ---

GOTO end

:error
ECHO ---
ECHO Script concluido com erro
ECHO ---
PAUSE

:end