$account = $env:APPVEYOR_ACCOUNT_NAME
$project = $env:APPVEYOR_PROJECT_SLUG
$projectUrl = "https://ci.appveyor.com/api/projects/$account/$project/settings/build-number"

$headers = @{
  "Authorization" = "Bearer $AppVeyorAccessToken"
  "Content-type" = "application/json"
  "Accept" = "application/json"
}

$mask = [int]::MaxValue
$date = $(Get-Date).TofileTime()
$buildNumber = $date -band $mask

Write-Host "Changing build number to: $buildNumber" -ForegroundColor Yellow -BackgroundColor Black

$build = @{
	nextBuildNumber = $buildNumber
}

$body = $build | ConvertTo-Json

Invoke-RestMethod -Method PUT $projectUrl -Headers $headers -Body $body