msbuild.exe %srcFolder%\source\jsonserialization\jsonserialization.sln

msbuild.exe %srcFolder%\source\af.contracts\af.contracts.sln

msbuild.exe %srcFolder%\source\af.modul.auswerten\af.modul.auswerten.sln
msbuild.exe %srcFolder%\source\af.modul.befragen\af.module.befragen.sln
msbuild.exe %srcFolder%\source\af.ui\af.ui.sln

msbuild.exe %srcFolder%\source\demo\demo.sln


nunit-console.exe /result:%NUnitResults%\myresults.xml %srcFolder%\source\jsonserialization.tests\bin\Debug\jsonserialization.tests.dll