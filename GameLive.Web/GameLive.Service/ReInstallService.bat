mkdir "bin\Installing"

COPY "bin\Debug\GameLive.Service.exe" "bin\Installing\GameLive.Service.exe"
COPY "bin\Debug\GameLive.Service.exe.config" "bin\Installing\GameLive.Service.exe.config"
COPY "bin\Debug\GameLive.Service.pdb" "bin\Installing\GameLive.Service.pdb"

net stop GameService

C:\Windows\Microsoft.NET\Framework64\v4.0.30319\InstallUtil.exe -u "bin\Installing\GameLive.Service.exe"
C:\Windows\Microsoft.NET\Framework64\v4.0.30319\InstallUtil.exe "bin\Installing\GameLive.Service.exe"

net start GameService