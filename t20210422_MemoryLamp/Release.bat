C:\Factory\Tools\RDMD.exe /RC out

COPY /B MemoryLamp\MemoryLamp\bin\Release\MemoryLamp.exe out\MemoryLamp.exe

C:\Factory\Tools\xcp.exe doc out

C:\Factory\SubTools\zip.exe /O out MemoryLamp
