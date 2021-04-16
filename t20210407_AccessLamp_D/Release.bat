C:\Factory\Tools\RDMD.exe /RC out

COPY /B AccessLamp\AccessLamp\bin\Release\AccessLamp.exe out\AccessLamp_D.exe

C:\Factory\Tools\xcp.exe doc out

C:\Factory\SubTools\zip.exe /O out AccessLamp_D
