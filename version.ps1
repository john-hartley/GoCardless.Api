[CmdletBinding()]
Param(
	[Parameter(Mandatory = $true, Position = 0)]
	$relativePath
)

$fullPath = [System.IO.Path]::Combine($pwd, $relativePath)

$version = [System.Reflection.AssemblyName]::GetAssemblyName($fullPath).Version
$productVersion = [System.Diagnostics.FileVersionInfo]::GetVersionInfo($fullPath).ProductVersion

Write-Host Assembly version: $version -ForegroundColor Yellow -BackgroundColor Black
Write-Host Product version: $productVersion -ForegroundColor Yellow -BackgroundColor Black
