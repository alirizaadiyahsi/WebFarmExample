# COMMON PATHS

$dockerFolder = (Get-Item -Path "./" -Verbose).FullName
$dOutputFolder = Join-Path $dockerFolder "outputs"
$slnFolder = Join-Path $dockerFolder "../"
$webapiFolder = Join-Path $slnFolder "WebFarmExample"

## CLEAR ######################################################################

Remove-Item $dOutputFolder -Force -Recurse
New-Item -Path $dOutputFolder -ItemType Directory

## RESTORE NUGET PACKAGES #####################################################

Set-Location $slnFolder
dotnet restore

## PUBLISH WEB API PROJECT ###################################################

Set-Location $webapiFolder
dotnet publish --output (Join-Path $dOutputFolder "webapi")

## CREATE DOCKER IMAGES #######################################################

# Webapi
Set-Location (Join-Path $dOutputFolder "webapi")

docker rmi ali/webapi -f
docker build -t ali/webapi .

## FINALIZE ###################################################################

Set-Location $dockerFolder