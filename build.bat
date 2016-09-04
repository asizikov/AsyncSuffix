@echo off
cls
"tools\nuget\NuGet.exe" "Install" "FAKE" "-OutputDirectory" "packages" "-ExcludeVersion"
"tools\nuget\NuGet.exe" restore ./src/MrEric.sln
"packages\FAKE\tools\Fake.exe" build.fsx