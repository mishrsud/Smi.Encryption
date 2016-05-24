@echo off

REM Get piping hot cake
powershell .\getcake.ps1

powershell .\build.ps1 -Configuration release

REM echo Copying packages to local feed
REM xcopy /Y /D ..\NuGetPackage\*.nupkg C:\NuGet\LocalFeed\ 
pause