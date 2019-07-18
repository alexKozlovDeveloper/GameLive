net stop MyFirstService.Demo3

mkdir "bin\Installing"

COPY "bin\Debug\SecondWindowsService.exe" "bin\Installing\SecondWindowsService.exe"
COPY "bin\Debug\SecondWindowsService.exe.config" "bin\Installing\SecondWindowsService.exe.config"
COPY "bin\Debug\SecondWindowsService.pdb" "bin\Installing\SecondWindowsService.pdb"
COPY "bin\Debug\GameLive.Core.dll" "bin\Installing\GameLive.Core.dll"
COPY "bin\Debug\GameLive.Core.pdb" "bin\Installing\GameLive.Core.pdb"
COPY "bin\Debug\Newtonsoft.Json.dll" "bin\Installing\Newtonsoft.Json.dll"

C:\Windows\Microsoft.NET\Framework64\v4.0.30319\InstallUtil.exe -u "bin\Installing\SecondWindowsService.exe"
C:\Windows\Microsoft.NET\Framework64\v4.0.30319\InstallUtil.exe "bin\Installing\SecondWindowsService.exe"

net start MyFirstService.Demo3

::sc delete serviceName