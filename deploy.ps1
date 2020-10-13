Write-Host "Pull request: $env:APPVEYOR_PULL_REQUEST_IS_PULL_REQUEST"
Write-Host "Branch: $env:APPVEYOR_PULL_REQUEST_HEAD_REPO_BRANCH"

Get-ChildItem Env:*

if ($env:APPVEYOR_PULL_REQUEST_IS_PULL_REQUEST)
{        
	Write-Host "Skipping deploy."
	Exit-AppveyorBuild 
}