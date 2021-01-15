rem @(#)SCADE2(W:SKD08212CO2:SAT.DyP.Util.Security.Interop.SgiHelpersTest:CopySgiCripto2.cmd:0:22/Mayo/2008[SAT.DyP.Util.Security.Interop.SgiHelpersTest:1.0:22/Mayo/2008])
rem
@echo off
if .%1.==.. goto displayUsage
if .%2.==.. goto displayUsage
if .%2.==.Release. goto copyReleaseBuild

:copyDebugBuild
pushd %1
XCopy ..\..\References\SgiCripto2\SgiCripto2msd.dll .\bin\Debug /f /v /y
XCopy ..\..\References\SgiSelloDigital\SgiSelloDigital_msdbg.dll .\bin\Debug /f /v /y
popd
goto :finish

:copyReleaseBuild
pushd %1
XCopy ..\..\References\SgiCripto2\SgiCripto2.dll .\bin\Release /f /v /y
XCopy ..\..\References\SgiSelloDigital\SgiSelloDigital.dll .\bin\Release /f /v /y
popd
goto :finish

:displayUsage
echo Usage: CopySgiCripto2.cmd [Project-Dir] [Build-Type]
echo        Build-Type can be 'Debug' or 'Release'.

:finish
echo.
