# FuckBaiduNetdisk
防止百度网盘篡改注册表导致SVN/GIT图标显示不正常

Windows

百度网盘
资源管理
图标覆盖

BaiduNetdist
Explorer
ShellIconOverlayIdentifiers

\HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\ShellIconOverlayIdentifiers

delete prefix key ".WorkspaceExt"
