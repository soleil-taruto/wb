C:\Factory\Tools\RDMD.exe /RC out

COPY /B Claes20200001\Claes20200001\bin\Release\Claes20200001.exe out\MapConverter-CUI.exe
COPY /B Silvia20200001\Silvia20200001\bin\Release\Silvia20200001.exe out\MapConverter.exe

C:\Factory\Tools\xcp.exe doc out

C:\Factory\SubTools\zip.exe /O out MapConverter
