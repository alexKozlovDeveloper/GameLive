net stop MyFirstService.Demo3

mkdir "bin\Installing"

COPY "bin\Debug\GameLive.WindowsService.exe" "bin\Installing\GameLive.WindowsService.exe"
COPY "bin\Debug\GameLive.WindowsService.exe.config" "bin\Installing\GameLive.WindowsService.exe.config"
COPY "bin\Debug\GameLive.WindowsService.pdb" "bin\Installing\GameLive.WindowsService.pdb"
COPY "bin\Debug\GameLive.Core.dll" "bin\Installing\GameLive.Core.dll"
COPY "bin\Debug\Arena.Core.dll" "bin\Installing\Arena.Core.dll"
COPY "bin\Debug\Arena.WcfService.dll" "bin\Installing\Arena.WcfService.dll"
COPY "bin\Debug\GameLive.Core.pdb" "bin\Installing\GameLive.Core.pdb"
COPY "bin\Debug\Newtonsoft.Json.dll" "bin\Installing\Newtonsoft.Json.dll"

C:\Windows\Microsoft.NET\Framework64\v4.0.30319\InstallUtil.exe -u "bin\Installing\GameLive.WindowsService.exe"
C:\Windows\Microsoft.NET\Framework64\v4.0.30319\InstallUtil.exe "bin\Installing\GameLive.WindowsService.exe"

net start MyFirstService.Demo3

::sc delete serviceName