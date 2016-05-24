$TOOLS_DIR = Join-Path $PSScriptRoot "tools"
$NUGET_EXE = Join-Path $TOOLS_DIR "nuget.exe"
$NUGET_URL = "http://dist.nuget.org/win-x86-commandline/latest/nuget.exe"

# Create tools directory
if (!(Test-Path $NUGET_EXE)) {
    Write-Host -Message "Creating tools directory"
    New-Item $TOOLS_DIR -ItemType Directory
}

# Try download NuGet.exe if not exists
if (!(Test-Path $NUGET_EXE)) {
    Write-Host -Message "Downloading NuGet.exe..."
    try {
        (New-Object System.Net.WebClient).DownloadFile($NUGET_URL, $NUGET_EXE)
    } catch {
        Throw "Could not download NuGet.exe."
    }
}

Write-Host -Message "Downloading Cake build bootstrapper"
Invoke-WebRequest http://cakebuild.net/download/bootstrapper/windows -OutFile build.ps1