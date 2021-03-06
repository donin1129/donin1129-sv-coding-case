function VerifyExitCode
{
    if ($LastExitCode -ne 0)
    {
        throw "Exit code is not 0"
    }
}

try
{
	$files = "-f db.yml"
    $migrationFile = "db-migration.Dockerfile"
    $dockerDir = Get-Location

    Write-Host "Using: $(Invoke-Expression -Command "docker compose version")" -ForegroundColor Green

	#Stop & Remove container, network and volumes
    Invoke-Expression -Command "docker compose $files down -v"
    VerifyExitCode
    Write-Host "Done! docker compose $files down -v" -ForegroundColor Green

    Invoke-Expression -Command "docker compose $files pull"
    VerifyExitCode
    Write-Host "Done! docker compose $files pull" -ForegroundColor Green

    Invoke-Expression -Command "docker compose $files up -d --build"
    VerifyExitCode
    Write-Host "Done! docker compose $files up" -ForegroundColor Green

    Set-Location -Path "../../.."

    Write-Host "Start db migration" -ForegroundColor Green
    dotnet ef database update --project src\Infrastructure --startup-project src\WebUI
    VerifyExitCode

    Set-Location -Path $dockerDir

    Invoke-Expression -Command "docker compose $files down"

    Write-Host "All configured" -ForegroundColor Green
    exit 0
}
catch
{
    Set-Location -Path $dockerDir
    Write-Host "An error occurred:" -ForegroundColor Red
    Write-Host $_ -ForegroundColor Red

    exit 1
}