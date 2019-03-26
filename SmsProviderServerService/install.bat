SET mypath=%~dp0\
@echo off
cls
echo y | reg delete HKLM\Software\Microsoft\Windows\CurrentVersion\Run /v SmsProviderServerService
reg add HKLM\Software\Microsoft\Windows\CurrentVersion\Run /v SmsProviderServerService /d %mypath%\SmsProviderServerService.exe
pause