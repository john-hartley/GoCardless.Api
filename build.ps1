[CmdletBinding()]
Param(
	[Parameter(ValueFromPipelineByPropertyName)]
	[switch]$isPreview
)

$build = "dotnet build .\GoCardless.Api.sln --configuration release --no-restore"

if ($isPreview)
{
	$build += " --version-suffix ""preview"""
}

Write-Host "Building with: $build" -ForegroundColor Yellow -BackgroundColor Black
Invoke-Expression $build
