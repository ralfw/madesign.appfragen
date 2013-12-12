rem SET PATH=%PATH%;C:\Windows\Microsoft.Net\Framework64\v2.0.50727\
SET srcFolder=..
del ..\bin\*.* /f /q
buildOnAppVeyor.bat
