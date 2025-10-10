# Set Working Directory
Split-Path $MyInvocation.MyCommand.Path | Push-Location
[Environment]::CurrentDirectory = $PWD

Remove-Item "$env:RELOADEDIIMODS/fftivc.config.zodioverwriter/*" -Force -Recurse
dotnet publish "./fftivc.config.zodioverwriter.csproj" -c Release -o "$env:RELOADEDIIMODS/fftivc.config.zodioverwriter" /p:OutputPath="./bin/Release" /p:ReloadedILLink="true"

# Restore Working Directory
Pop-Location