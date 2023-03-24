@echo off
cls
call C:\Windows\Microsoft.net\framework64\v4.0.30319\csc.exe lpo-spoofer.cs
pause
cls
call C:\Windows\Microsoft.net\framework64\v4.0.30319\csc.exe client.cs
echo Compiled
pause