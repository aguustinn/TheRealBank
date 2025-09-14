@ECHO OFF

ECHO ---------------------------------------------------ECHO Criacao de migrations (MainContext)
ECHO ----------------------------------------------------

SET /p NAME="Informe o nome da migration: "
IF "%NAME%"=="" GOTO end

SET UI_PROJECT=../TheRealBank.UI
SET REPO_PROJECT=.
SET FOLDER=Migrations

ECHO ---
ECHO Gerando migration: %NAME% (MainContext)
ECHO ---

dotnet ef migrations add %NAME% --project %REPO_PROJECT% --startup-project %UI_PROJECT% --context MainContext --output-dir %FOLDER%
IF %ERRORLEVEL% NEQ 0 GOTO error

ECHO ---
ECHO Migration criada com sucesso em %FOLDER%.
ECHO Para aplicar:
ECHO dotnet ef database update --project %REPO_PROJECT% --startup-project %UI_PROJECT% --context MainContext
ECHO ---
GOTO end

:error
ECHO ---
ECHO Script concluido com erro
ECHO ---
PAUSE

:end