echo Creating NuGet package
set solnDir=%1
set projFilename=%2
set configName=%3

if not exist %solnDir%\NugetPackage (
	echo creating NugetPackage directory
	mkdir %solnDir%\NugetPackage
)

echo Building package with %configName% build configuration

%solnDir%\build\tools\nuget.exe pack %projFilename% -Verbosity detailed -outputdirectory %solnDir%NugetPackage\ -Properties Configuration=%configName%