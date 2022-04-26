@ECHO OFF
REM ---------------------------START----------------------------
MODE 80, 25
TITLE Central AME Script & COLOR 70
SET "ver=v0.9"
REM Allows for more flexibility with these two variables
FOR /F "delims=" %%d in ('echo "%~dp0"') DO SET "dirPath=%%d"
SET "dirPath=%dirPath:~1,-1%"
FOR /F "delims=" %%e in ('echo "%~f0"') DO SET "scriptPath=%%e"
SET "scriptPath=%scriptPath:~1,-1%"
FOR /F %%A IN ('"prompt $H &echo on &for %%B in (1) do rem"') DO SET BS=%%A
PUSHD "%dirPath%"
CALL :AUX-GETUSERNAME
IF /I "%~1"=="LangSet" GOTO DISPLANG-USERCHECK
IF /I "%~1"=="kbLangSet" GOTO KBLANG-PRESET
IF /I "%~1"=="updateFinished" GOTO AUX-UPDATEFINISHED

:PRE-ADMINCHECK1

NET SESSION > NUL 2>&1
IF %ERRORLEVEL% GTR 0 GOTO PRE-ADMINCHECK2
CALL :AUX-ELEVATIONCHECK
IF /I "%currentUsername%"=="RestartRequired" (
	ECHO Running this script after a username change may cause serious damage^! & ECHO.
	CHOICE /C YN /N /M "Run anyways? (Y/N): "
		IF %ERRORLEVEL%==1 GOTO HOME-MAINMENU
		IF %ERRORLEVEL%==2 EXIT /B 0 )
GOTO HOME-MAINMENU

:PRE-ADMINCHECK2

POWERSHELL -NoP -C "Start-Process '%scriptPath%' -Verb RunAs" > NUL 2>&1
IF %ERRORLEVEL% GTR 0 CHOICE /C YN /N /M "Elevation canceled, run with limited functionality? (Y/N): "
	IF %ERRORLEVEL%==1 SET lim=rem & GOTO HOME-MAINMENU
	IF %ERRORLEVEL%==2 EXIT /B 0
EXIT /B 0
REM -------------------------START-END--------------------------


					REM ------------
					REM MENU SECTION
					REM ------------


REM ----------------------------MENU----------------------------
:HOME-MAINMENU

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                           ^| Central AME Script %ver% ^| & ECHO. & ECHO.
%lim%ECHO                  [1] Change Username or Password
%lim%ECHO                  [2] Change Lockscreen Image
%lim%ECHO                  [3] Change Profile Image
ECHO                  [4] Change Language Settings
%lim%ECHO                  [5] Elevate User to Administrator
%lim%ECHO                  [6] Login w^/o Typing Username
ECHO.
%lim%ECHO                  [E] Extra
ECHO                  [X] Exit & ECHO. & ECHO            __________________________________________________________ & ECHO.
	CHOICE /C 123456EX8 /N /M "%BS%           Choose a menu option: "
		%lim%IF %ERRORLEVEL%==1 GOTO USERPASS-MENU
		%lim%IF %ERRORLEVEL%==2 GOTO LOCKSCREEN-GRABIMAGE
		%lim%IF %ERRORLEVEL%==3 GOTO PFP-GRABIMAGE 
		IF %ERRORLEVEL%==4 GOTO HOME-LANGUAGE
		%lim%IF %ERRORLEVEL%==5 GOTO ELEVATE-MENU
		%lim%IF %ERRORLEVEL%==6 GOTO NOUSERNAME-MENU
		%lim%IF %ERRORLEVEL%==7 GOTO HOME-EXTRA
		IF %ERRORLEVEL%==8 EXIT /B 0
	GOTO HOME-MAINMENU

:HOME-EXTRA

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                           ^| Central AME Script %ver% ^| & ECHO. & ECHO.
%lim%ECHO                  [1] Enable Hibernation
%lim%ECHO                  [2] Enable Windows Script Host (Legacy)
%lim%ECHO                  [3] Enable NCSI Active Probing (Legacy)
%lim%ECHO                  [4] Create New User (Beta)
%lim%ECHO.
ECHO                  [0] Return to Menu & ECHO                  [X] Exit & ECHO. & ECHO            __________________________________________________________ & ECHO.
	CHOICE /C 12340X /N /M "%BS%           Choose a menu option: "
		%lim%IF %ERRORLEVEL%==1 GOTO HIBERNATE-MENU
		%lim%IF %ERRORLEVEL%==2 GOTO WSH-MENU
		%lim%IF %ERRORLEVEL%==3 GOTO NCSI-MENU
		%lim%IF %ERRORLEVEL%==4 GOTO NEWUSER-MENU
		IF %ERRORLEVEL%==5 GOTO HOME-MAINMENU
		IF %ERRORLEVEL%==6 EXIT /B 0
	GOTO HOME-EXTRA

:HOME-LANGUAGE

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                           ^| Central AME Script %ver% ^| & ECHO. & ECHO. 
%lim%ECHO                  [1] Change Display Language
ECHO                  [2] Add Keyboard Language
%lim%ECHO                  [3] Install Language Pack
%lim%ECHO                  [4] Uninstall Language Pack
ECHO. & ECHO                  [0] Return to Menu & ECHO                  [X] Exit & ECHO. & ECHO            __________________________________________________________ & ECHO.
CHOICE /C 12340X /N /M "%BS%           Choose a menu option: "
	%lim%IF %ERRORLEVEL%==1 SET "lpStatus=" & GOTO DISPLANG-MENUP1
	IF %ERRORLEVEL%==2 GOTO KBLANG-LANGS
	%lim%IF %ERRORLEVEL%==3 SET "lpStatus=added"L & GOTO DISPLANG-MENUP1
	%lim%IF %ERRORLEVEL%==4 SET "lpStatus=removed" & GOTO DISPLANG-MENUP1
	IF %ERRORLEVEL%==5 GOTO HOME-MAINMENU
	IF %ERRORLEVEL%==6 EXIT /B 0
GOTO HOME-LANGUAGE
REM --------------------------MENU-END--------------------------


                    REM -----------------
                    REM Primary Functions
                    REM -----------------


REM --------------------------USERPASS--------------------------
:USERPASS-MENU	

SETLOCAL
CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                           ^| Central AME Script %ver% ^| & ECHO. & ECHO. & ECHO                  [1] Change Username & ECHO                  [2] Change Password & ECHO                  [3] Change Administrator Password & ECHO. & ECHO                  [0] Return to Menu & ECHO                  [X] Exit & ECHO. & ECHO            __________________________________________________________ & ECHO.
CHOICE /C 1230X /N /M "%BS%           Choose a menu option: "
	IF %ERRORLEVEL%==1 GOTO USERPASS-USERNAME
	IF %ERRORLEVEL%==2 GOTO USERPASS-PASSWORD
	IF %ERRORLEVEL%==3 GOTO USERPASS-ADMINPASSWORD
	IF %ERRORLEVEL%==4 ENDLOCAL & GOTO HOME-MAINMENU
	IF %ERRORLEVEL%==5 EXIT /B 0

:USERPASS-USERNAME

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                           ^| Central AME Script %ver% ^| & ECHO.
SET "newUsername="
SET /P "newUsername=%BS%           Enter the new Username, or enter 'Cancel' to quit: "
	IF /I "%newUsername%"=="Cancel" ENDLOCAL & GOTO USERPASS-MENU
	IF "%newUsername%"=="" (
	ECHO. & ECHO. & ECHO                              Input cannot be blank. & ECHO            __________________________________________________________ & ECHO.
	PAUSE > NUL|SET /P =%BS%           Press any key to return to the Menu: 
	ENDLOCAL & GOTO USERPASS-MENU )
TIMEOUT /T 1 /NOBREAK > NUL
FOR /F "usebackq tokens=3" %%A IN (`WMIC useraccount where "name='%currentUsername%'" rename '%newUsername%'`) DO SET "wmicOutput=%%A" > NUL 2>&1
	IF "%wmicOutput%"=="0;" ENDLOCAL & SET "currentUsername=%newUsername%" & ECHO. & ECHO                          Username Changed Successfully & ECHO                            A restart is recommended.
	REM This should only happen if the user changes their username AND closes/re-opens the .cmd before restarting.
	IF "%wmicOutput%"=="Available." ENDLOCAL & ECHO. & ECHO              You must restart before changing your username again.
	IF "%wmicOutput%"=="9;" ENDLOCAL & ECHO. & ECHO                                  Invalid input. & SET "loc=USERPASS-MENU"
ECHO            __________________________________________________________ & ECHO.
PAUSE > NUL|SET /P =%BS%           Press any key to return to the Menu: 
GOTO HOME-MAINMENU

:USERPASS-PASSWORD

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                           ^| Username/Password Changer ^| & ECHO.
SET "newPassword="
SET /P "newPassword=%BS%           Enter the new Password, or enter 'Cancel' to quit: "
	IF /I "%newPassword%"=="Cancel" ENDLOCAL & GOTO USERPASS-MENU
	IF "%newPassword%"=="" (
	ECHO. & ECHO                              Input cannot be blank. :& ECHO            __________________________________________________________ & ECHO.
	PAUSE > NUL|SET /P =%BS%           Press any key to return to the Menu: 
	ENDLOCAL & GOTO USERPASS-MENU )
TIMEOUT /T 1 /NOBREAK > NUL
NET user "%currentUsername%" "%newPassword%" > NUL 2>&1
	IF %ERRORLEVEL% LEQ 0 ECHO. & ECHO. & ECHO                          Password Changed Successfully
	REM This should only happen if the user changes their username AND closes/re-opens the .cmd before restarting.
	IF %ERRORLEVEL% GTR 0 ECHO. & ECHO. & ECHO. & ECHO             You must restart after changing your username. & ECHO.
ECHO            __________________________________________________________ & ECHO.
PAUSE > NUL|SET /P =%BS%           Press any key to return to the Menu: 
ENDLOCAL & GOTO HOME-MAINMENU

:USERPASS-ADMINPASSWORD

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                           ^| Username/Password Changer ^| & ECHO.
SET "newPassword="
SET /P "newPassword=%BS%           Enter the new Password, or enter 'Cancel' to quit: "
	IF /I "%newPassword%"=="Cancel" GOTO USERPASS-MENU
	IF "%newPassword%"=="" (
	ECHO. & ECHO                              Input cannot be blank. :& ECHO            __________________________________________________________ & ECHO.
	PAUSE > NUL|SET /P =%BS%           Press any key to return to the Menu: 
	GOTO USERPASS-MENU )
TIMEOUT /T 1 /NOBREAK > NUL
NET user "Administrator" "%newPassword%" > NUL 2>&1
	IF %ERRORLEVEL% LEQ 0 ECHO. & ECHO. & ECHO                       Admin Password Changed Successfully
	REM This should only happen if the user changes their username AND closes/re-opens the .cmd before restarting.
	IF %ERRORLEVEL% GTR 0 ECHO. & ECHO. & ECHO. & ECHO                                  Action failed. & ECHO.
ECHO            __________________________________________________________ & ECHO.
PAUSE > NUL|SET /P =%BS%           Press any key to return to the Menu: 
ENDLOCAL & GOTO HOME-MAINMENU
REM ------------------------USERPASS-END------------------------



REM -------------------------LOCKSCREEN-------------------------
:LOCKSCREEN-GRABIMAGE

SETLOCAL
REM Original Author & Co-Author: Logan Darklock, lucid
CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                           ^| Central AME Script %ver% ^| & ECHO. & ECHO                                Select your image & ECHO.

DIR /B "%SYSTEMDRIVE%\Users" | FINDSTR /x "%possibleUserDir%" > NUL 2>&1
	IF %ERRORLEVEL% LEQ 0 SET "UserPath=\%possibleUserDir%"

FOR /F "usebackq delims=" %%I in (`POWERSHELL -NoP -C "[System.Reflection.Assembly]::LoadWithPartialName('System.windows.forms')|Out-Null;$OFD = New-Object System.Windows.Forms.OpenFileDialog;$OFD.Multiselect = $False;$OFD.Filter = 'Image Files (*.jpg; *.jpeg; *.png; *.bmp; *.jfif)| *.jpg; *.jpeg; *.png; *.bmp; *.jfif';$OFD.InitialDirectory = '%SYSTEMDRIVE%\Users%UserPath%';$OFD.ShowDialog()|out-null;$OFD.FileNames"`) DO SET "IMAGEPATH=%%~I"
	IF "%IMAGEPATH%" =="" (
		ECHO. & ECHO                            You must select an image   & ECHO            __________________________________________________________ & ECHO.
		PAUSE > NUL|SET /P =%BS%           Press any key to return to the Menu: 
		ENDLOCAL & GOTO HOME-MAINMENU )

CHOICE /C YN /N /M "%BS%           Remove lockscreen blur? (Y/N): "
	IF %ERRORLEVEL%==1 REG ADD "HKLM\SOFTWARE\Policies\Microsoft\Windows\System" /v DisableAcrylicBackgroundOnLogon /t REG_DWORD /d 1 /f > NUL
	IF %ERRORLEVEL%==2 REG DELETE "HKLM\SOFTWARE\Policies\Microsoft\Windows" /v DisableAcrylicBackgroundOnLogon /f > NUL 2>&1

:LOCKSCREEN-DEPLOY

TIMEOUT /T 1 /NOBREAK > NUL
REM Copy wallpaper to the right spot
TAKEOWN /F "%WINDIR%\Web\Screen\img100.jpg">NUL 2>&1 & TAKEOWN /F "%WINDIR%\Te\Web\Screen\img103.png">NUL 2>&1 & TAKEOWN /F "%WINDIR%\Web\Wallpaper\Windows\img0.jpg" > NUL 2>&1
ICACLS "%WINDIR%\Web\Screen\img100.jpg" /reSET>NUL & ICACLS "%WINDIR%\Web\Screen\img103.png" /reSET>NUL & ICACLS "%WINDIR%\Web\Wallpaper\Windows\img0.jpg" /reSET > NUL
COPY "%IMAGEPATH%" "%WINDIR%\Web\Screen\img100.jpg" /y>NUL & COPY "%IMAGEPATH%" "%WINDIR%\Web\Screen\img103.png" /y>NUL & COPY "%IMAGEPATH%" "%WINDIR%\Web\Wallpaper\Windows\img0.jpg" /y > NUL
REM Clear cache
TAKEOWN /R /D Y /F "%PROGRAMDATA%\Microsoft\Windows\SystemData" > NUL
ICACLS "%PROGRAMDATA%\Microsoft\Windows\SystemData" /reSET /t > NUL
FOR /D %%x in ("%PROGRAMDATA%\Microsoft\Windows\SystemData\*") do (
FOR /D %%y in ("%%x\ReadOnly\LockScreen_*") do rd /s /q "%%y" )

ECHO. & ECHO. & ECHO                         Wallpaper changed successfully & ECHO            __________________________________________________________ & ECHO.
PAUSE > NUL|SET /P =%BS%           Press any key to return to the Menu: 
ENDLOCAL & GOTO HOME-MAINMENU
REM -----------------------LOCKSCREEN-END-----------------------



REM ----------------------------PFP-----------------------------
:PFP-GRABIMAGE

SETLOCAL
REM Original Author & Co-Author: Logan Darklock, lucid
CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                           ^| Central AME Script %ver% ^| & ECHO. & ECHO                                Select your image & ECHO.

REM Used for default starting directory for file selection window
DIR /B "%SYSTEMDRIVE%\Users" | FINDSTR /x "%possibleUserDir%" > NUL 2>&1
	IF %ERRORLEVEL% LEQ 0 SET "UserPath=\%possibleUserDir%"

FOR /F "usebackq delims=" %%I in (`POWERSHELL -NoP -C "[System.Reflection.Assembly]::LoadWithPartialName('System.windows.forms')|Out-Null;$OFD = New-Object System.Windows.Forms.OpenFileDialog;$OFD.Multiselect = $False;$OFD.Filter = 'Image Files (*.jpg; *.jpeg; *.png; *.bmp; *.jfif)| *.jpg; *.jpeg; *.png; *.bmp; *.jfif';$OFD.InitialDirectory = '%SYSTEMDRIVE%\Users%UserPath%';$OFD.ShowDialog()|out-null;$OFD.FileNames"`) DO SET "IMAGE=%%~I"
	IF "%IMAGE%"=="" (
		ECHO. & ECHO                            You must select an image   & ECHO            __________________________________________________________ & ECHO.
		PAUSE > NUL|SET /P =%BS%           Press any key to return to the Menu: 
		ENDLOCAL & GOTO HOME-MAINMENU )

:PFP-DEPLOY

ECHO                             Setting profile image...
FOR /F "usebackq delims=" %%F IN (`WMIC useraccount where "name="%currentUsername%"" get sid ^| FINDSTR "S-"`) DO SET PFPSID=%%F
	SET "PFPSID=%PFPSID:~0,-3%"

REM On recent Windows 10 versions, resolutions called for are:
REM 32x32, 40x40, 48x48, 64x64, 96x96, 192x192, 208x208, 240x240, 424x424,
REM 448x448, 1080x1080
SET "FOLDER=%PUBLIC%\AccountPictures\%PFPSID%"

MKDIR "%FOLDER%" > NUL 2>&1
TAKEOWN /r /d Y /f "%FOLDER%" > NUL
ICACLS "%FOLDER%" /reset /t > NUL
DEL /Q /F "%FOLDER%\*" > NUL

POWERSHELL -NoP -C "Add-Type -AssemblyName System.Drawing; $img = [System.Drawing.Image]::FromFile((Get-Item """"%IMAGE%"""")); $a = New-Object System.Drawing.Bitmap(32, 32); $graph = [System.Drawing.Graphics]::FromImage($a); $graph.DrawImage($img, 0, 0, 32, 32); $a.Save("""%FOLDER%\32x32.png"""); $b = New-Object System.Drawing.Bitmap(40, 40); $graph = [System.Drawing.Graphics]::FromImage($b); $graph.DrawImage($img, 0, 0, 40, 40); $b.Save("""%FOLDER%\40x40.png"""); $c = New-Object System.Drawing.Bitmap(48, 48); $graph = [System.Drawing.Graphics]::FromImage($c); $graph.DrawImage($img, 0, 0, 48, 48); $c.Save("""%FOLDER%\48x48.png"""); $d = New-Object System.Drawing.Bitmap(64, 64); $graph = [System.Drawing.Graphics]::FromImage($d); $graph.DrawImage($img, 0, 0, 64, 64); $d.Save("""%FOLDER%\64x64.png"""); $e = New-Object System.Drawing.Bitmap(96, 96); $graph = [System.Drawing.Graphics]::FromImage($e); $graph.DrawImage($img, 0, 0, 96, 96); $e.Save("""%FOLDER%\96x96.png"""); $f = New-Object System.Drawing.Bitmap(192, 192); $graph = [System.Drawing.Graphics]::FromImage($f); $graph.DrawImage($img, 0, 0, 192, 192); $f.Save("""%FOLDER%\192x192.png"""); $g = New-Object System.Drawing.Bitmap(208, 208); $graph = [System.Drawing.Graphics]::FromImage($g); $graph.DrawImage($img, 0, 0, 208, 208); $g.Save("""%FOLDER%\208x208.png"""); $h = New-Object System.Drawing.Bitmap(240, 240); $graph = [System.Drawing.Graphics]::FromImage($h); $graph.DrawImage($img, 0, 0, 240, 240); $h.Save("""%FOLDER%\240x240.png"""); $i = New-Object System.Drawing.Bitmap(424, 424); $graph = [System.Drawing.Graphics]::FromImage($i); $graph.DrawImage($img, 0, 0, 424, 424); $i.Save("""%FOLDER%\424x424.png"""); $j = New-Object System.Drawing.Bitmap(448, 448); $graph = [System.Drawing.Graphics]::FromImage($j); $graph.DrawImage($img, 0, 0, 448, 448); $j.Save("""%FOLDER%\448x448.png"""); $k = New-Object System.Drawing.Bitmap(1080, 1080); $graph = [System.Drawing.Graphics]::FromImage($k); $graph.DrawImage($img, 0, 0, 1080, 1080); $k.Save("""%FOLDER%\1080x1080.png""")"

SET "KEY=HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\AccountPicture\Users\%PFPSID%"

REM Runs the reg delete command as SYSTEM
SCHTASKS /create /tn "AME PFPREG" /tr "CMD /C 'REG DELETE '%KEY%' /f'" /sc MONTHLY /f /rl HIGHEST /ru "SYSTEM" > NUL
SCHTASKS /run /tn "AME PFPREG" > NUL
SCHTASKS /delete /tn "AME PFPREG" /f > NUL

REG ADD "%KEY%" /f > NUL
REG ADD "%KEY%" /v Image32 /t REG_SZ /d "%FOLDER%\32x32.png" /f > NUL
REG ADD "%KEY%" /v Image40 /t REG_SZ /d "%FOLDER%\40x40.png" /f > NUL
REG ADD "%KEY%" /v Image48 /t REG_SZ /d "%FOLDER%\48x48.png" /f > NUL
REG ADD "%KEY%" /v Image64 /t REG_SZ /d "%FOLDER%\64x64.png" /f > NUL
REG ADD "%KEY%" /v Image96 /t REG_SZ /d "%FOLDER%\96x96.png" /f > NUL
REG ADD "%KEY%" /v Image192 /t REG_SZ /d "%FOLDER%\192x192.png" /f > NUL
REG ADD "%KEY%" /v Image208 /t REG_SZ /d "%FOLDER%\208x208.png" /f > NUL
REG ADD "%KEY%" /v Image240 /t REG_SZ /d "%FOLDER%\240x240.png" /f > NUL
REG ADD "%KEY%" /v Image424 /t REG_SZ /d "%FOLDER%\424x424.png" /f > NUL
REG ADD "%KEY%" /v Image448 /t REG_SZ /d "%FOLDER%\448x448.png" /f > NUL
REG ADD "%KEY%" /v Image1080 /t REG_SZ /d "%FOLDER%\1080x1080.png" /f > NUL
REG ADD "HKU\%PFPSID%\SOFTWARE\OpenShell\StartMenu\Settings" /v UserPicturePath /t REG_SZ /d "%FOLDER%\448x448.png" /f > NUL

GPUPDATE /force > NUL

ECHO. & ECHO. & ECHO                        Profile image changed successfully & ECHO            __________________________________________________________ & ECHO.
PAUSE > NUL|SET /P =%BS%           Press any key to return to the Menu: 
ENDLOCAL & GOTO HOME-MAINMENU
REM --------------------------PFP-END---------------------------



REM -------------------------ELEVATION--------------------------
:ELEVATE-MENU	

SETLOCAL
CLS & ECHO            %currentUsername%: %userStatus% & ECHO            __________________________________________________________ & ECHO. & ECHO                           ^| Central AME Script %ver% ^| & ECHO. & ECHO. & ECHO                  [1] Elevate your user & ECHO                  [2] De-elevate your user & ECHO. & ECHO                  [0] Return to Menu & ECHO                  [X] Exit & ECHO. & ECHO            __________________________________________________________ & ECHO.
CHOICE /C 120X /N /M "%BS%           Choose a menu option: "
	IF %ERRORLEVEL%==1 SET "elevMenu=1" & GOTO ELEVATE-ELEVATE
	IF %ERRORLEVEL%==2 SET "elevMenu=2" & GOTO ELEVATE-REVOKE
	IF %ERRORLEVEL%==3 ENDLOCAL & GOTO HOME-MAINMENU
	IF %ERRORLEVEL%==4 EXIT /B 0

:ELEVATE-ELEVATE

SET "cenStr=%currentUsername% is already an Administrator." & CALL :AUX-CENTERTEXT
IF "%userStatus%"=="Elevated" (
	CLS & ECHO            %currentUsername%: %userStatus% & ECHO            __________________________________________________________ & ECHO. & ECHO                           ^| Central AME Script %ver% ^| & ECHO. & ECHO. & ECHO. & ECHO%cenOut% & ECHO            __________________________________________________________& ECHO.	
	PAUSE > NUL|SET /P =%BS%           Press any key to return to the Menu: 
	ENDLOCAL & GOTO ELEVATE-MENU )
SET "cenStr=Elevating %currentUsername% to Administrator..." & CALL :AUX-CENTERTEXT
CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                           ^| Central AME Script %ver% ^| & ECHO. & ECHO%cenOut%
TIMEOUT /T 2 /NOBREAK > NUL
NET localgroup administrators "%currentUsername%" /add > NUL 2>&1
	IF %ERRORLEVEL% GTR 0 ECHO. & SET "elevFail=true"
	IF %ERRORLEVEL% LEQ 0 SET "userStatus=Elevated"
GOTO ELEVATE-FINISH

:ELEVATE-REVOKE

SET "cenStr=%currentUsername% is not an Administator." & CALL :AUX-CENTERTEXT
IF "%userStatus%"=="Not Elevated" (
	CLS & ECHO           %currentUsername%: %userStatus% & ECHO            __________________________________________________________ & ECHO. & ECHO                           ^| Central AME Script %ver% ^| & ECHO. & ECHO. & ECHO%cenOut% & ECHO            __________________________________________________________& ECHO.
	PAUSE > NUL|SET /P =%BS%           Press any key to return to the Menu: 
	ENDLOCAL & GOTO ELEVATE-MENU )
SET "cenStr=Revoking admin rights for %currentUsername%..." & CALL :AUX-CENTERTEXT
CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                           ^| Central AME Script %ver% ^| & ECHO. & ECHO%cenOut%
TIMEOUT /T 2 /NOBREAK > NUL 2>&1
NET localgroup administrators "%currentUsername%" /delete > NUL 2>&1
	IF %ERRORLEVEL% GTR 0 ECHO. & SET "elevFail=true"
	IF %ERRORLEVEL% LEQ 0 SET "userStatus=Not Elevated"
GOTO ELEVATE-FINISH

:ELEVATE-FINISH

IF "%elevFail%"=="true" (
	ECHO. & ECHO.& ECHO                      Action failed. A restart may fix this. & ECHO            __________________________________________________________ & ECHO.
	PAUSE > NUL|SET /P =%BS%           Press any key to return to the Menu: 
	ENDLOCAL & GOTO HOME-MAINMENU )
IF "%elevMenu%"=="1" SET "cenStr=%currentUsername% is now an Administrator"
IF "%elevMenu%"=="2" SET "cenStr=Admin rights have been revoked for %currentUsername%"
CALL :AUX-CENTERTEXT
ECHO. & ECHO. & ECHO%cenOut% & ECHO                       A restart is needed to take effect. & ECHO            __________________________________________________________ & ECHO.
CHOICE /C YN /N /M "%BS%           Would you like to restart now? (Y/N): "
	IF %ERRORLEVEL%==1 SHUTDOWN -R -T 0 & EXIT 0
	IF %ERRORLEVEL%==2 ENDLOCAL & GOTO HOME-MAINMENU
REM -----------------------ELEVATION-END------------------------



REM --------------------------DISPLANG--------------------------
:DISPLANG-MENUP1

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                           ^| Central AME Script %ver% ^| & ECHO. & ECHO.
ECHO                  [1] Arabic (ar-SA) & ECHO                  [2] Bulgarian (bg-BG) & ECHO                  [3] Chineese [Simplified] (zh-CN) & ECHO                  [4] Chineese [Traditional] (zh-TW) & ECHO                  [5] Croatian (hr-HR) & ECHO                  [6] Czech (cs-CZ) & ECHO                  [7] Danish (da-DK) & ECHO.
ECHO                  [N] Next Page & ECHO                  [0] Return to Menu & ECHO                  [X] Exit & ECHO                                     Page 1/6 & ECHO            __________________________________________________________ & ECHO.
CHOICE /C 1234567N0X /N /M "%BS%           Choose a menu option: "
	IF %ERRORLEVEL%==1 SET "langSel=ar-SA" & SET "dispDl=2.5" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==2 SET "langSel=bg-BG" & SET "dispDl=2.5" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==3 SET "langSel=zh-CN" & SET "dispDl=3.2" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==4 SET "langSel=zh-TW" & SET "dispDl=3.2" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==5 SET "langSel=hr-HR" & SET "dispDl=2.9" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==6 SET "langSel=cs-CZ" & SET "dispDl=2.5" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==7 SET "langSel=da-DK" & SET "dispDl=2.5" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==8 GOTO DISPLANG-MENUP2
	IF %ERRORLEVEL%==9 GOTO HOME-MAINMENU
	IF %ERRORLEVEL%==10 EXIT /B 0

:DISPLANG-MENUP2

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                           ^| Central AME Script %ver% ^| & ECHO. & ECHO.
ECHO                  [1] Dutch (nl-NL) & ECHO                  [2] English [US] (en-US) & ECHO                  [3] English [UK] (en-GB) & ECHO                  [4] Estonian (et-EE) & ECHO                  [5] Finnish (fi-FI) & ECHO                  [6] French [Canada] (fr-CA) & ECHO                  [7] French [France] (fr-FR) & ECHO.
ECHO                  [N] Next Page & ECHO                  [P] Previous Page & ECHO                  [0] Return to Menu & ECHO                  [X] Exit & ECHO                                     Page 2/6 & ECHO            __________________________________________________________ & ECHO.
CHOICE /C 1234567NP0X /N /M "%BS%           Choose a menu option: "
	IF %ERRORLEVEL%==1 SET "langSel=nl-NL" & SET "dispDl=2.9" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==2 SET "langSel=en-US" & SET "dispDl=2.5" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==3 SET "langSel=en-GB" & SET "dispDl=2.5" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==4 SET "langSel=et-EE" & SET "dispDl=2.5" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==5 SET "langSel=fi-FI" & SET "dispDl=2.5" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==6 SET "langSel=fr-CA" & SET "dispDl=2.5" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==7 SET "langSel=fr-FR" & SET "dispDl=2.9" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==8 GOTO DISPLANG-MENUP3
	IF %ERRORLEVEL%==9 GOTO DISPLANG-MENUP1
	IF %ERRORLEVEL%==10 GOTO HOME-MAINMENU
	IF %ERRORLEVEL%==11 EXIT /B 0

:DISPLANG-MENUP3

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                           ^| Central AME Script %ver% ^| & ECHO. & ECHO.
ECHO                  [1] German (de-DE) & ECHO                  [2] Greek (el-GR) & ECHO                  [3] Hebrew (he-IL) & ECHO                  [4] Hungarian (hu-HU) & ECHO                  [5] Italian (it-IT) & ECHO                  [6] Japanese (ja-JP) & ECHO                  [7] Korean (ko-KR) & ECHO.
ECHO                  [N] Next Page & ECHO                  [P] Previous Page & ECHO                  [0] Return to Menu & ECHO                  [X] Exit & ECHO                                     Page 3/6 & ECHO            __________________________________________________________ & ECHO.
CHOICE /C 1234567NP0X /N /M "%BS%           Choose a menu option: "
	IF %ERRORLEVEL%==1 SET "langSel=de-DE" & SET "dispDl=2.5" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==2 SET "langSel=el-GR" & SET "dispDl=2.5" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==3 SET "langSel=he-IL" & SET "dispDl=2.9" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==4 SET "langSel=hu-HU" & SET "dispDl=2.9" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==5 SET "langSel=it-IT" & SET "dispDl=2.9" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==6 SET "langSel=ja-JP" & SET "dispDl=2.9" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==7 SET "langSel=ko-KR" & SET "dispDl=2.9" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==8 GOTO DISPLANG-MENUP4
	IF %ERRORLEVEL%==9 GOTO DISPLANG-MENUP2
	IF %ERRORLEVEL%==10 GOTO HOME-MAINMENU
	IF %ERRORLEVEL%==11 EXIT /B 0

:DISPLANG-MENUP4

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                           ^| Central AME Script %ver% ^| & ECHO. & ECHO.
ECHO                  [1] Latvian (lv-LV) & ECHO                  [2] Lithuanian (lt-LT) & ECHO                  [3] Norwegian (nb-NO) & ECHO                  [4] Polish (pl-PL) & ECHO                  [5] Portugeese [Brazil] (pt-BR) & ECHO                  [6] Portugeese [Portugal] (pt-PT) & ECHO                  [7] Romanian (ro-RO) & ECHO.
ECHO                  [N] Next Page & ECHO                  [P] Previous Page & ECHO                  [0] Return to Menu & ECHO                  [X] Exit & ECHO                                     Page 4/6 & ECHO            __________________________________________________________ & ECHO.
CHOICE /C 1234567NP0X /N /M "%BS%           Choose a menu option: "
	IF %ERRORLEVEL%==1 SET "langSel=lv-LV" & SET "dispDl=2.9" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==2 SET "langSel=lt-LT" & SET "dispDl=2.9" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==3 SET "langSel=nb-NO" & SET "dispDl=2.9" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==4 SET "langSel=pl-PL" & SET "dispDl=3.2" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==5 SET "langSel=pt-BR" & SET "dispDl=3.2" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==6 SET "langSel=pt-PT" & SET "dispDl=3.2" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==7 SET "langSel=ro-RO" & SET "dispDl=3.2" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==8 GOTO DISPLANG-MENUP5
	IF %ERRORLEVEL%==9 GOTO DISPLANG-MENUP3
	IF %ERRORLEVEL%==10 GOTO HOME-MAINMENU
	IF %ERRORLEVEL%==11 EXIT /B 0

:DISPLANG-MENUP5

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                           ^| Central AME Script %ver% ^| & ECHO. & ECHO.
ECHO                  [1] Russian (ru-RU) & ECHO                  [2] Serbian (sr-Latn-RS) & ECHO                  [3] Slovak (sk-SK) & ECHO                  [4] Slovenian (sl-SI) & ECHO                  [5] Spanish [Mexico] (es-MX) & ECHO                  [6] Spanish [Spain] (es-ES) & ECHO                  [7] Swedish (sv-SE) & ECHO.
ECHO                  [N] Next Page & ECHO                  [P] Previous Page & ECHO                  [0] Return to Menu & ECHO                  [X] Exit & ECHO                                     Page 5/6 & ECHO            __________________________________________________________ & ECHO.
CHOICE /C 1234567NP0X /N /M "%BS%           Choose a menu option: "
	IF %ERRORLEVEL%==1 SET "langSel=ru-RU" & SET "dispDl=3.2" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==2 SET "langSel=sr-Latn-RS" & SET "dispDl=3.2" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==3 SET "langSel=sk-SK" & SET "dispDl=3.2" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==4 SET "langSel=sl-SI" & SET "dispDl=3.2" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==5 SET "langSel=es-MX" & SET "dispDl=2.5" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==6 SET "langSel=es-ES" & SET "dispDl=2.5" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==7 SET "langSel=sv-SE" & SET "dispDl=3.2" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==8 GOTO DISPLANG-MENUP6
	IF %ERRORLEVEL%==9 GOTO DISPLANG-MENUP4
	IF %ERRORLEVEL%==10 GOTO HOME-MAINMENU
	IF %ERRORLEVEL%==11 EXIT /B 0

:DISPLANG-MENUP6

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                           ^| Central AME Script %ver% ^| & ECHO. & ECHO.
ECHO                  [1] Thai (th-TH) & ECHO                  [2] Turkish (tr-TR) & ECHO                  [3] Ukrainian (uk-UA) & ECHO.
ECHO                  [P] Previous Page & ECHO                  [0] Return to Menu & ECHO                  [X] Exit & ECHO                                     Page 6/6 & ECHO            __________________________________________________________ & ECHO.
CHOICE /C 123P0X /N /M "%BS%           Choose a menu option: "
	IF %ERRORLEVEL%==1 SET "langSel=th-TH" & SET "dispDl=3.2" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==2 SET "langSel=tr-TR" & SET "dispDl=3.2" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==3 SET "langSel=uk-UA" & SET "dispDl=3.2" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==4 GOTO DISPLANG-MENUP5
	IF %ERRORLEVEL%==5 GOTO HOME-MAINMENU
	IF %ERRORLEVEL%==6 EXIT /B 0

:DISPLANG-DOWNLOAD

SETLOCAL
REM Check if language pack is already installed
CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                           ^| Central AME Script %ver% ^| & ECHO.
IF "%lpStatus%"=="removed" GOTO DISPLANG-LPREMOVE

WHERE 7z.exe>NUL 2>&1 && SET "dispSkip0=rem "
WHERE choco.exe>NUL 2>&1 && SET "dispChoco=true"

DISM /Online /Get-Intl /English | FIND "Installed language(s): %langSel%" > NUL 2>&1
	IF %ERRORLEVEL% LEQ 0 GOTO DISPLANG-USERCHECK

FOR /F tokens^=2^ delims^=^" %%A IN ('TASKLIST /FI "IMAGENAME eq lpksetup.exe" /NH /FO csv') DO SET lpkStatus=%%A
	IF "%lpkStatus%"=="," (
		ECHO. & ECHO. & ECHO                  All instances of lpksetup.exe must be closed. & ECHO            __________________________________________________________ & ECHO. & ENDLOCAL & PAUSE > NUL|SET /P =%BS%           Press any key to return to the Menu: & GOTO HOME-MAINMENU )

ECHO                  A ~%dispDl%GB Language Packs ISO must be downloaded & ECHO.
CHOICE /C YN /N /M "%BS%           Continue? (Y/N): "
	IF %ERRORLEVEL%==2 ENDLOCAL & GOTO HOME-MAINMENU

PING -n 1 archlinux.org -w 20000 > NUL 2>&1
	IF %ERRORLEVEL% GTR 0 (
		ECHO. & ECHO. & ECHO                       An internet connection is required. & ECHO            __________________________________________________________ & ECHO. & ENDLOCAL & PAUSE > NUL|SET /P =%BS%           Press any key to return to the Menu: & GOTO HOME-MAINMENU )

FOR /F "tokens=2 delims==" %%A IN ('WMIC logicaldisk where "DeviceID='%~d0'" get FreeSpace /format:value') DO SET freeSpace=%%A
	SET "freeSpace=%freeSpace:~0,-10%"
	IF "%freeSpace%"=="" SET "freeSpace=1"
	IF %freeSpace% LSS 5 (
		ECHO. & ECHO. & ECHO                           Not enough free disk space. & ECHO            __________________________________________________________ & ECHO. & ENDLOCAL & PAUSE > NUL|SET /P =%BS%           Press any key to return to the Menu: & GOTO HOME-MAINMENU )

IF EXIST "%dirPath%LangPacks.ISO" DEL /Q "%dirPath%LangPacks.ISO"
IF EXIST "%dirPath%LangPacks" RMDIR /Q /S "%dirPath%LangPacks"

REM If 7zip must be installed, there will not be enough space to display everything in 25 lines (script height) without this line
%dispSkip0%CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                           ^| Central AME Script %ver% ^|
ECHO. & ECHO                                Download Progress
IF "%dispDl%"=="2.5" CURL --range 0-2480000000 -L --progress-bar "https://software-download.microsoft.com/download/pr/19041.1.191206-1406.vb_release_CLIENTLANGPACKDVD_OEM_MULTI.iso" --output "%dirPath%LangPacks.ISO"
IF "%dispDl%"=="2.9" CURL --range 0-2900000000 -L --progress-bar "https://software-download.microsoft.com/download/pr/19041.1.191206-1406.vb_release_CLIENTLANGPACKDVD_OEM_MULTI.iso" --output "%dirPath%LangPacks.ISO"
IF "%dispDl%"=="3.2" CURL --range 0-3230000000 -L --progress-bar "https://software-download.microsoft.com/download/pr/19041.1.191206-1406.vb_release_CLIENTLANGPACKDVD_OEM_MULTI.iso" --output "%dirPath%LangPacks.ISO"
FOR %%A IN ("%dirPath%LangPacks.ISO") DO SET "langISOSize=%%~zA"
	IF "%langISOSize"=="" SET "langISOSize=1"
	REM Detects size of ISO file, this essentially allows for a simple error detection.
	IF %langISOSize% LSS 700000 (
		DEL /Q "%dirPath%LangPacks.ISO" > NUL
		ENDLOCAL & GOTO AUX-DOWNLOADFAILED )

:DISPLANG-INSTALL

%dispSkip0%IF NOT "%dispChoco%"=="true" (
%dispSkip0%    ECHO. & ECHO. & ECHO                       7zip or choclatey must be installed. & ECHO            __________________________________________________________ & ECHO. & ENDLOCAL & PAUSE > NUL|SET /P =%BS%           Press any key to return to the Menu: & GOTO HOME-MAINMENU )
%dispSkip0%ECHO. & ECHO                                Installing 7zip... & choco install -y --force --allow-empty-checksums "7zip" > NUL
7z e -y -o"%dirPath%LangPacks" "%dirPath%LangPacks.ISO" x64\langpacks\*.cab > NUL 2>&1
ECHO. & ECHO                 Installing language pack. This might take awhile & ECHO.
FOR /F "tokens=2" %%A IN ('DATE /T') DO SET "dateAfter=%%A"
SET "timeAfter=%TIME:~0,-3%"
LPKSETUP /i %langSel% /p "%dirPath%LangPacks\Microsoft-Windows-Client-Language-Pack_x64_%langSel%.cab" /r > NUL
:lpkInstLogLoop
	TIMEOUT /T 1 /NOBREAK > NUL
	POWERSHELL -NoP -C "$timeAfter = Get-Date -Date '%dateAfter% %timeAfter%'; Get-WinEvent -FilterHashtable @{Logname='Microsoft-Windows-LanguagePackSetup/Operational';StartTime=$timeAfter}" | FINDSTR "2014 2007" > NUL
		IF %ERRORLEVEL% LSS 1 (
			%dispSkip0%ECHO                                 Removing 7zip... & choco uninstall 7zip -y --force-dependencies --allow-empty-checksums>NUL & ECHO.
			DEL /Q "%dirPath%LangPacks.ISO">NUL & RMDIR /Q /S "%dirPath%LangPacks" & ECHO. & ECHO. & ECHO                                  Action failed. & ECHO            __________________________________________________________ & ECHO. & ENDLOCAL & PAUSE > NUL|SET /P =%BS%           Press any key to return to the Menu: & GOTO HOME-MAINMENU
		)
	POWERSHELL -NoP -C "$timeAfter = Get-Date -Date '%dateAfter% %timeAfter%'; Get-WinEvent -FilterHashtable @{Logname='Microsoft-Windows-LanguagePackSetup/Operational';StartTime=$timeAfter}" | FINDSTR "2000" > NUL
		IF %ERRORLEVEL% LSS 1 (
			%dispSkip0%ECHO                                 Removing 7zip... & choco uninstall 7zip -y --force-dependencies --allow-empty-checksums>NUL & ECHO.
			IF "%lpStatus%"=="added" DEL /Q "%dirPath%LangPacks.ISO">NUL & RMDIR /Q /S "%dirPath%LangPacks" & GOTO DISPLANG-LPCOMPLETE 
			DEL /Q "%dirPath%LangPacks.ISO">NUL & RMDIR /Q /S "%dirPath%LangPacks" & GOTO DISPLANG-USERCHECK
		)
	GOTO :lpkInstLogLoop

:DISPLANG-USERCHECK

IF /I "%~1"=="LangSet" WAITFOR /SI Golden>NUL 2>&1 & SET "langSel=%~2" & SET "makeKBDef=%~3" & GOTO DISPLANG-SETLANG
CHOICE /C YN /N /M "%BS%           Make default keyboard language? (Y/N): "
	IF %ERRORLEVEL%==2 SET "makeKBDef=false"
REM The FOR command creates a new process ID, and thus cannot be used here
POWERSHELL -NoP -C "(Get-WmiObject Win32_Process -Filter ProcessId=$PID).ParentProcessId" > %temp%\CentralAMEProcessID.txt
	FOR /F "delims=" %%A IN (%temp%\CentralAMEProcessID.txt) DO SET "scriptPID=%%A" & DEL "%temp%\CentralAMEProcessID.txt"
REM Detects if the script is being run under the currently logged in user. If not it runs a second instance as the current user
FOR /F tokens^=2^ delims^=^" %%A IN ('TASKLIST /FI "PID eq %scriptPID%" /FI "USERNAME eq %currentUsername%" /NH /FO csv') DO SET processRunOut=%%A
	IF NOT "%processRunOut%"=="," (
		REM If %lim% = rem that means the script is NOT being run as administrator. There's a small chance someone attempts to run it as another non-admin user,
		REM this would be problematic as schtasks requires admin privilages.
		IF "%lim%"=="rem " ECHO. & ECHO. & ECHO                      Script must be run as the current user & ECHO                         or with administrator privilages. & ECHO            __________________________________________________________ & ECHO. & ENDLOCAL & PAUSE > NUL|SET /P =%BS%           Press any key to return to the Menu: & GOTO HOME-MAINMENU
		SETLOCAL ENABLEDELAYEDEXPANSION
		SCHTASKS /CREATE /tn SetDispLang /tr "CMD /C 'START /min '' '%scriptPath%' LangSet %langSel% %makeKBDef%'" /sc ONSTART /ru "%currentUsername%" /it /f > NUL
		REM RUNAS will work, however it requires the user to enter a password, and won't accept a blank one. This is a lot more simple and reliable
		SCHTASKS /RUN /tn SetDispLang > NUL
			WAITFOR Golden /T 10 > NUL 2>&1
				IF !ERRORLEVEL! LSS 1 SCHTASKS /DELETE /tn SetDispLang /f>NUL & GOTO DISPLANG-COMPLETE
			SCHTASKS /DELETE /tn SetDispLang /f > NUL
			ENDLOCAL & ENDLOCAL & ECHO. & ECHO. & ECHO                                  Action failed. & ECHO            __________________________________________________________ & ECHO. & PAUSE > NUL|SET /P =%BS%           Press any key to return to the Menu: & GOTO HOME-MAINMENU )

:DISPLANG-SETLANG

ECHO. & ECHO                               Setting language...
FOR /F "delims= " %%A IN ('POWERSHELL -NoP -C "Get-WinDefaultInputMethodOverride"') DO SET "possibleLangDef=%%A"
	IF NOT "%possibleLangDef%"=="" SET "currentLangDef=%possibleLangDef%" & SET "dispSkip1=rem "
%dispSkip1%FOR /F "delims=" %%A IN ('POWERSHELL -NoP -C "(Get-WinUserLanguageList)[0].InputMethodTips"') DO SET "currentLangDef=%%A"
SETLOCAL ENABLEDELAYEDEXPANSION
REM Accounts for zero input methods. Very unlikely scenario
POWERSHELL -NoP -c "(Get-WinUserLanguageList).InputMethodTips" | FINDSTR ":" > NUL 2>&1
	IF %ERRORLEVEL% GTR 0 SET "dispSkip2=rem "
%dispSkip2%FOR /F "delims=" %%A IN ('POWERSHELL -NoP -C "(Get-WinUserLanguageList).InputMethodTips"') DO SET "oldInputMethods=!oldInputMethods!;$LangList[0].InputMethodTips.Add('%%A')"
POWERSHELL -NoP -C "Set-WinSystemLocale %langSel%; $LangList = New-WinUserLanguageList %langSel%%oldInputMethods%; Set-WinUserLanguageList $LangList -Force"
REM Clears override
IF "%makeKBDef%"=="" POWERSHELL -NoP -C "Set-WinDefaultInputMethodOverride"
IF "%makeKBDef%"=="false" POWERSHELL -NoP -C "Set-WinDefaultInputMethodOverride "%currentLangDef%""
ENDLOCAL
IF /I "%~1"=="LangSet" EXIT 0

:DISPLANG-COMPLETE

ECHO. & ECHO. & ECHO                        Display language changed to %langSel% & ECHO                       A restart is needed to take effect. & ECHO            __________________________________________________________ & ECHO.
CHOICE /C YN /N /M "%BS%           Would you like to restart now? (Y/N): "
	IF %ERRORLEVEL%==1 SHUTDOWN -R -T 0 & EXIT 0
	IF %ERRORLEVEL%==2 ENDLOCAL & GOTO HOME-MAINMENU

:DISPLANG-LPCOMPLETE

%dispSkip0%ECHO. & ECHO                                 Removing 7zip... & choco uninstall 7zip.install -y --force > NUL
%dispSkip0%choco uninstall 7zip -y --force > NUL

ECHO. & ECHO. & ECHO                      LanguagePack %langSel% %lpStatus% successfully & ECHO                            A restart is recommended. & ECHO            __________________________________________________________ & ECHO.
PAUSE > NUL|SET /P =%BS%           Press any key to return to the Menu: 
ENDLOCAL & GOTO HOME-MAINMENU

:DISPLANG-LPREMOVE

SET "dispSkip0=rem "
CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                           ^| Central AME Script %ver% ^| & ECHO.
FOR /F tokens^=2^ delims^=^" %%A IN ('TASKLIST /FI "IMAGENAME eq lpksetup.exe" /NH /FO csv') DO SET "lpkStatus=%%A"
	IF "%lpkStatus%"=="," ECHO. & ECHO. & ECHO                  All instances of lpksetup.exe must be closed. & ECHO            __________________________________________________________ & ECHO. & ENDLOCAL & PAUSE > NUL|SET /P =%BS%           Press any key to return to the Menu: & GOTO HOME-MAINMENU )
FOR /F "tokens=2" %%A IN ('DATE /T') DO SET "dateAfter=%%A"
SET "timeAfter=%TIME:~0,-3%"
ECHO                        Uninstalling %langSel% LanguagePack...
LPKSETUP /u %langSel% /r
:lpkRemLogLoop
	TIMEOUT /T 1 /NOBREAK > NUL
	POWERSHELL -command "$timeAfter = Get-Date -Date '%dateAfter% %timeAfter%'; Get-WinEvent -FilterHashtable @{Logname='Microsoft-Windows-LanguagePackSetup/Operational';StartTime=$timeAfter}" | FINDSTR "2014 2008" > NUL
		IF %ERRORLEVEL% LSS 1 ECHO. & ECHO. & ECHO                                  Action failed. & ECHO            __________________________________________________________ & ECHO. & ENDLOCAL & PAUSE > NUL|SET /P =%BS%           Press any key to return to the Menu: & GOTO HOME-MAINMENU
	POWERSHELL -command "$timeAfter = Get-Date -Date '%dateAfter% %timeAfter%'; Get-WinEvent -FilterHashtable @{Logname='Microsoft-Windows-LanguagePackSetup/Operational';StartTime=$timeAfter}" | FINDSTR "2001" > NUL
		IF %ERRORLEVEL% LSS 1 GOTO DISPLANG-LPCOMPLETE
	GOTO lpkRemLogLoop
REM ------------------------DISPLANG-END------------------------



REM ---------------------------KBLANG---------------------------
:KBLANG-LANGS

SETLOCAL ENABLEDELAYEDEXPANSION
FOR /F "tokens=1 delims=:" %%A IN ('FINDSTR /B /N /C:"REM DB-Languages" "%~f0"') DO SET /A dbStartLine=%%A
SET /A count=0
SET "kbSub=false"
FOR /F "usebackq tokens=1, 2, 4, 6, 8, 10, 12, 14 skip=%dbStartLine% delims=|" %%A IN ("%~f0") DO (
	IF "%%A"=="REM DB-Corresponding Commands" GOTO KBLANG-CCOMMS
	IF "%%B"=="" GOTO KBLANG-CCOMMS
	IF NOT "%%B"=="spacer" SET /A count=!count!+1 & SET "lang!count!=%%B"
	IF "%%C"=="" GOTO KBLANG-CCOMMS
	IF NOT "%%C"=="spacer" SET /A "count=!count!+1" & SET "lang!count!=%%C"
	IF "%%D"=="" GOTO KBLANG-CCOMMS
	IF NOT "%%D"=="spacer" SET /A "count=!count!+1" & SET "lang!count!=%%D"
	IF "%%E"=="" GOTO KBLANG-CCOMMS
	IF NOT "%%E"=="spacer" SET /A "count=!count!+1" & SET "lang!count!=%%E"
	IF "%%F"=="" GOTO KBLANG-CCOMMS
	IF NOT "%%F"=="spacer" SET /A "count=!count!+1" & SET "lang!count!=%%F"
	REM Overflow protecton, allows for more than 5 items per DB line
	IF NOT "%%G"=="" SET /A "count=!count!+1" & SET "lang!count!=%%G"
	IF NOT "%%H"=="" SET /A "count=!count!+1" & SET "lang!count!=%%H"
)

:KBLANG-CCOMMS

FOR /F "tokens=1 delims=:" %%A IN ('FINDSTR /B /N /C:"REM DB-Corresponding Commands" "%~f0"') DO SET /A dbStartLine=%%A
SET /A kbCCommCount=0
FOR /F "usebackq tokens=1, 2, 4, 6, 8, 10, 12, 14 skip=%dbStartLine% delims=|" %%A IN ("%~f0") DO (
	IF "%%A"=="REM DB-Submenu Input Methods" GOTO KBLANG-PREMMS
	IF "%%B"=="" GOTO KBLANG-PREMMS
	IF NOT "%%B"=="spacer" SET /A kbCCommCount=!kbCCommCount!+1 & SET "kbComm!kbCCommCount!=SET kbLangSel=%%B"
	IF "%%C"=="" GOTO KBLANG-PREMMS
	IF NOT "%%C"=="spacer" SET /A "kbCCommCount=!kbCCommCount!+1" & SET "kbComm!kbCCommCount!=SET kbLangSel=%%C"
	IF "%%D"=="" GOTO KBLANG-PREMMS
	IF NOT "%%D"=="spacer" SET /A "kbCCommCount=!kbCCommCount!+1" & SET "kbComm!kbCCommCount!=SET kbLangSel=%%D"
	IF "%%E"=="" GOTO KBLANG-PREMMS
	IF NOT "%%E"=="spacer" SET /A "kbCCommCount=!kbCCommCount!+1" & SET "kbComm!kbCCommCount!=SET kbLangSel=%%E"
	IF "%%F"=="" GOTO KBLANG-PREMMS
	IF NOT "%%F"=="spacer" SET /A "kbCCommCount=!kbCCommCount!+1" & SET "kbComm!kbCCommCount!=SET kbLangSel=%%F"
	REM Overflow protecton, allows for more than 5 items per DB line
	IF NOT "%%G"=="" SET /A "count=!count!+1" & SET "lang!count!=SET kbLangSel=%%G" 
	IF NOT "%%H"=="" SET /A "count=!count!+1" & SET "lang!count!=SET kbLangSel=%%H"
)

:KBLANG-SUBLANGS

SETLOCAL ENABLEDELAYEDEXPANSION
FOR /F "tokens=1 delims=:" %%A IN ('FINDSTR /B /N /C:"REM DB-Submenu Input Methods" "%~f0"') DO SET /A dbStartLine=%%A
SET /A count=0
SET "kbSub=true"
FOR /F "usebackq tokens=1, 2, 4, 6, 8, 10, 12, 14, 16, 18 skip=%dbStartLine% delims=|" %%A IN ("%~f0") DO (
	IF "%%A"=="REM DB-SubCorresponding Input Methods" GOTO KBLANG-CCOMMS
	IF "%%B"=="%kbLangSel%" (
		IF "%%C"=="" GOTO KBLANG-SUBCOMMS
		SET /A count=!count!+1 & SET "lang!count!=%%C"
		IF "%%D"=="" GOTO KBLANG-SUBCCOMMS
		SET /A "count=!count!+1" & SET "lang!count!=%%D"
		IF "%%E"=="" GOTO KBLANG-SUBCCOMMS
		SET /A "count=!count!+1" & SET "lang!count!=%%E"
		IF "%%F"=="" GOTO KBLANG-SUBCCOMMS
		SET /A "count=!count!+1" & SET "lang!count!=%%F"
		IF "%%G"=="" GOTO KBLANG-SUBCCOMMS
		SET /A "count=!count!+1" & SET "lang!count!=%%G"
		IF "%%H"=="" GOTO KBLANG-SUBCCOMMS
		SET /A "count=!count!+1" & SET "lang!count!=%%H"
		IF "%%I"=="" GOTO KBLANG-SUBCCOMMS
		SET /A "count=!count!+1" & SET "lang!count!=%%I"
		IF "%%J"=="" GOTO KBLANG-SUBCCOMMS
		SET /A "count=!count!+1" & SET "lang!count!=%%J"
	)
)

:KBLANG-SUBCCOMMS

FOR /F "tokens=1 delims=:" %%A IN ('FINDSTR /B /N /C:"REM DB-SubCorresponding Input Methods" "%~f0"') DO SET /A dbStartLine=%%A
SET /A skbCCommCount=0
FOR /F "usebackq tokens=1, 2, 4, 6, 8, 10, 12, 14, 16, 18 skip=%dbStartLine% delims=|" %%A IN ("%~f0") DO (
	IF "%%A"=="REM Marker" GOTO KBLANG-PREMMS
	IF "%%B"=="%kbLangSel%" (
		IF "%%C"=="" GOTO KBLANG-PREMMS
		SET /A skbCCommCount=!skbCCommCount!+1 & SET "kbComm!skbCCommCount!=SET kbLangSel=%%C& GOTO KBLANG-PRESET"
		IF "%%D"=="" GOTO KBLANG-PREMMS
		SET /A "skbCCommCount=!skbCCommCount!+1" & SET "kbComm!skbCCommCount!=SET kbLangSel=%%D& GOTO KBLANG-PRESET"
		IF "%%E"=="" GOTO KBLANG-PREMMS
		SET /A "skbCCommCount=!skbCCommCount!+1" & SET "kbComm!skbCCommCount!=SET kbLangSel=%%E& GOTO KBLANG-PRESET"
		IF "%%F"=="" GOTO KBLANG-PREMMS
		SET /A "skbCCommCount=!skbCCommCount!+1" & SET "kbComm!skbCCommCount!=SET kbLangSel=%%F& GOTO KBLANG-PRESET"
		IF "%%G"=="" GOTO KBLANG-PREMMS
		SET /A "skbCCommCount=!skbCCommCount!+1" & SET "kbComm!skbCCommCount!=SET kbLangSel=%%G& GOTO KBLANG-PRESET"
		IF "%%H"=="" GOTO KBLANG-PREMMS
		SET /A "skbCCommCount=!skbCCommCount!+1" & SET "kbComm!skbCCommCount!=SET kbLangSel=%%H& GOTO KBLANG-PRESET"
		IF "%%I"=="" GOTO KBLANG-PREMMS
		SET /A "skbCCommCount=!skbCCommCount!+1" & SET "kbComm!skbCCommCount!=SET kbLangSel=%%I& GOTO KBLANG-PRESET"
		IF "%%J"=="" GOTO KBLANG-PREMMS
		SET /A "skbCCommCount=!skbCCommCount!+1" & SET "kbComm!skbCCommCount!=SET kbLangSel=%%J& GOTO KBLANG-PRESET"
	)
)

:KBLANG-PREMMS

SET kbLangCount=0
SET /A "kbLangPages=%count%/9"
SET /A "kbLangRemainder=%count%-(%kbLangPages%*9)""
IF "%kbLangRemainder%" GTR "0" SET /A kbLangPages=%kbLangPages%+1
SET kbLangPageLoc=1

:KBLANG-MMS
REM Modular menu system

SET "lELs=9"
IF "%kbSub%"=="true" (SET "kb0Opt=Go Back") ELSE (SET "kb0Opt=Return to Menu")
SET "kbDisablePrev=" & SET "lC10=N" & SET "kbDisableNext=" & SET "lC11=P"
IF "%kbLangPageLoc%" EQU "1" SET "kbDisablePrev=rem " & SET "lC11="
SET "lC1=1" & SET "lC2=2" & SET "lC3=3" & SET "lC4=4" & SET "lC5=5" & SET "lC6=6" & SET "lC7=7" & SET "lC8=8" & SET "lC9=9"
SET "kbLangSkip1=" & SET "kbLangSkip2=" & SET "kbLangSkip3=" & SET "kbLangSkip4=" & SET "kbLangSkip5=" & SET "kbLangSkip6=" & SET "kbLangSkip7=" & SET "kbLangSkip8="
IF "%kbLangPageLoc%"=="%kbLangPages%" (
	SET "kbDisableNext=rem " & SET "lC10="
	IF "%kbLangRemainder%" GTR "0" (
		SET "lR1=1"
		SET "lELs=%kbLangRemainder%"
		IF NOT "%kbLangRemainder%" GTR "1" (SET "kbLangSkip1=rem ") ELSE (SET "lR2=2")
		IF NOT "%kbLangRemainder%" GTR "2" (SET "kbLangSkip2=rem ") ELSE (SET "lR3=3")
		IF NOT "%kbLangRemainder%" GTR "3" (SET "kbLangSkip3=rem ") ELSE (SET "lR4=4")
		IF NOT "%kbLangRemainder%" GTR "4" (SET "kbLangSkip4=rem ") ELSE (SET "lR5=5")
		IF NOT "%kbLangRemainder%" GTR "5" (SET "kbLangSkip5=rem ") ELSE (SET "lR6=6")
		IF NOT "%kbLangRemainder%" GTR "6" (SET "kbLangSkip6=rem ") ELSE (SET "lR7=7")
		IF NOT "%kbLangRemainder%" GTR "7" (SET "kbLangSkip7=rem ") ELSE (SET "lR8=8")
		IF NOT "%kbLangRemainder%" GTR "8" (SET "kbLangSkip8=rem ") ELSE (SET "lR9=9")
		SET "lC1=!lR1!" & SET "lC2=!lR2!" & SET "lC3=!lR3!" & SET "lC4=!lR4!" & SET "lC5=!lR5!" & SET "lC6=!lR6!" & SET "lC7=!lR7!" & SET "lC8=!lR8!" & SET "lC9=!lR9!"
	)
)
CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                           ^| Central AME Script %ver% ^| & ECHO. & ECHO.
SET /A "kbLangCount=%kbLangCount%+1"
ECHO                 [1] !lang%kbLangCount%! & SET "kbCComm1=!kbComm%kbLangCount%!"
%kbLangSkip1%SET /A "kbLangCount=%kbLangCount%+1"
%kbLangSkip1%ECHO                 [2] !lang%kbLangCount%! & SET "kbCComm2=!kbComm%kbLangCount%!"
%kbLangSkip2%SET /A "kbLangCount=%kbLangCount%+1"
%kbLangSkip2%ECHO                 [3] !lang%kbLangCount%! & SET "kbCComm3=!kbComm%kbLangCount%!"
%kbLangSkip3%SET /A "kbLangCount=%kbLangCount%+1"
%kbLangSkip3%ECHO                 [4] !lang%kbLangCount%! & SET "kbCComm4=!kbComm%kbLangCount%!"
%kbLangSkip4%SET /A "kbLangCount=%kbLangCount%+1"
%kbLangSkip4%ECHO                 [5] !lang%kbLangCount%! & SET "kbCComm5=!kbComm%kbLangCount%!"
%kbLangSkip5%SET /A "kbLangCount=%kbLangCount%+1"
%kbLangSkip5%ECHO                 [6] !lang%kbLangCount%! & SET "kbCComm6=!kbComm%kbLangCount%!"
%kbLangSkip6%SET /A "kbLangCount=%kbLangCount%+1"
%kbLangSkip6%ECHO                 [7] !lang%kbLangCount%! & SET "kbCComm7=!kbComm%kbLangCount%!"
%kbLangSkip7%SET /A "kbLangCount=%kbLangCount%+1"
%kbLangSkip7%ECHO                 [8] !lang%kbLangCount%! & SET "kbCComm8=!kbComm%kbLangCount%!"
%kbLangSkip8%SET /A "kbLangCount=%kbLangCount%+1"
%kbLangSkip8%ECHO                 [9] !lang%kbLangCount%! & SET "kbCComm9=!kbComm%kbLangCount%!"
ECHO.
%kbDisableNext%ECHO                 [N] Next Page
%kbDisablePrev%ECHO                 [P] Previous Page
ECHO                 [0] %kb0Opt% & ECHO                 [X] Exit
IF "%kbSub%"=="true" (ECHO.) ELSE (ECHO                                    Page %kbLangPageLoc%/%kbLangPages%)
ECHO            __________________________________________________________ &ECHO.

IF %kbLangPageLoc% LSS %kbLangPages% (SET /A kbLangCount=%kbLangCount%-9) ELSE (SET /A kbLangCount=%kbLangCount%-%kbLangRemainder%)
CHOICE /C %lC1%%lC2%%lC3%%lC4%%lC5%%lC6%%lC7%%lC8%%lC9%%lC10%%lC11%0X /N /M "%BS%          Choose a menu option: "
	SET "kbChoice=%ERRORLEVEL%"
	IF %kbChoice%==1 ENDLOCAL & %kbCComm1%
	%kbLangSkip1%IF %kbChoice%==2 ENDLOCAL & %kbCComm2%
	%kbLangSkip2%IF %kbChoice%==3 ENDLOCAL & %kbCComm3%
	%kbLangSkip3%IF %kbChoice%==4 ENDLOCAL & %kbCComm4%
	%kbLangSkip4%IF %kbChoice%==5 ENDLOCAL & %kbCComm5%
	%kbLangSkip5%IF %kbChoice%==6 ENDLOCAL & %kbCComm6%
	%kbLangSkip6%IF %kbChoice%==7 ENDLOCAL & %kbCComm7%
	%kbLangSkip7%IF %kbChoice%==8 ENDLOCAL & %kbCComm8%
	%kbLangSkip8%IF %kbChoice%==9 ENDLOCAL & %kbCComm9%
	SET /A "lEL10=%lELs%"
	%kbDisableNext%SET /A "lEL10=%lEL10%+1"
	%kbDisableNext%IF %kbChoice%==%lEL10% (
	%kbDisableNext%	SET /A kbLangPageLoc=%kbLangPageLoc%+1
	%kbDisableNext%	SET /A kbLangCount=%kbLangCount%+9
	%kbDisableNext%	GOTO KBLANG-MMS
	%kbDisableNext%)
	%kbDisablePrev%SET /A "lEL11=%lEL10%+1" & SET /A "lEL10=%lEL10%+1"
	%kbDisablePrev%IF %kbChoice%==%lEL11% (
	%kbDisablePrev%	SET /A kbLangPageLoc=%kbLangPageLoc%-1
	%kbDisablePrev%	SET /A kbLangCount=%kbLangCount%-9
	%kbDisablePrev%	GOTO KBLANG-MMS
	%kbDisablePrev%)
	SET /A "lEL12=%lEL10%+1"
	IF %kbChoice%==%lEL12% (
		IF "%kbSub%"=="true" (ENDLOCAL & GOTO KBLANG-LANGS) ELSE (ENDLOCAL & GOTO HOME-MAINMENU)
	)
	SET /A "lEL13=%lEL10%+2"
	IF %kbChoice%==%lEL13% EXIT /B 0
GOTO KBLANG-MMS

:KBLANG-DATABASE

REM DB-Languages
::1  |United States| |Chinese| |Hindi  (Devanagari) Traditional| |Spanish| |French| 
::2  |Arabic| |Russian| |Bangla| |Portuguese| |Albanian| 
::3  |Amharic| |Armenian| |Assamese - Inscript| |Azerbaijani| |Bashkir| 
::4  |Belarusian| |Belgian| |Bosnian  (Cyrillic)| |Buginese| |Bulgarian| 
::5  |Canadian| |Central Atlas Tamazight| |Central Kurdish| |Cherokee| |Croatian| 
::6  |Czech| |Danish| |Divehi| |Dutch| |Dzongkha| 
::7  |Estonian| |Faeroese| |Finnish| |Futhark| |Georgian| 
::8  |German| |Gothic| |Greek| |Greenlandic| |Guarani| 
::9  |Gujarati| |Hausa| |Hebrew| |Hungarian| |Icelandic| 
::10 |Igbo| |Indian| |Inuktitut| |Irish| |Italian| 
::11 |Japanese NON-FUNCTIONAL| |Javanese| |Kannada| |Kazakh| |Khmer| 
::12 |Konkoni  (Devanagari) - INSCRIPT| |Korean| |Kyrgyz Cyrillic| |Lao| |Latin American| 
::13 |Latvian| |Lisu| |Lithuanian| |Luxembourgish| |Macedonia| 
::14 |Malayalam| |Maltese| |Maori| |Marathi| |Mongolian| 
::15 |Myanmar| |N'ko| |Nepali| |New Tai Lue| |Norwegian| 
::16 |Odia| |Ol Chiki| |Old Italic| |Osmanya| |Pashto  (Afghanistan)| 
::17 |Persian| |Phags-pa| |Polish| |Punjabi| |Romanian| 
::18 |Sakha| |Sami| |Scottish Gaelic| |Serbian| |Sesotho sa Leboa| 
::19 |Setswana| |Sinhala| |Slovak| |Slovenian| |Sora| 
::20 |Sorbian| |Swedish| |Swiss| |Syriac| |Tai Le| 
::21 |Tajik| |Tamil| |Tatar| |Telugu| |Thai| 
::22 |Tibetan| |Tifinagh| |Tigrinya| |Turkish| |Turkmen| 
::23 |Uyghur| |Ukrainian| |United Kingdom| |Urdu| |Uzbek| 
::24 |Vietnamese| |Wolof| |Yakut| |Yoruba|

REM DB-Corresponding Commands
::1  |US& GOTO KBLANG-SUBLANGS| |CHI& GOTO KBLANG-SUBLANGS| |0439:00010439& GOTO KBLANG-PRESET| |SPA& GOTO KBLANG-SUBLANGS| |040c:0000040c& GOTO KBLANG-PRESET|
::2  |ARA& GOTO KBLANG-SUBLANGS| |RUS& GOTO KBLANG-SUBLANGS| |BAN& GOTO KBLANG-SUBLANGS| |POR& GOTO KBLANG-SUBLANGS| |041c:0000041c& GOTO KBLANG-PRESET|
::3  |045e:{E429B25A-E5D3-4D1F-9BE3-0C608477E3A1}{8F96574E-C86C-4bd6-9666-3F7327D4CBE8}& GOTO KBLANG-PRESET| |ARM& GOTO KBLANG-SUBLANGS| |044d:0000044d& GOTO KBLANG-PRESET| |AZ& GOTO KBLANG-SUBLANGS| |046d:0000046d& GOTO KBLANG-PRESET|
::4  |0423:00000423& GOTO KBLANG-PRESET| |BEL& GOTO KBLANG-SUBLANGS| |141a:00000201a& GOTO KBLANG-PRESET| |0421:000b0c00& GOTO KBLANG-PRESET| |BUL& GOTO KBLANG-SUBLANGS|
::5  |CAN& GOTO KBLANG-SUBLANGS| |085f:0000085f& GOTO KBLANG-PRESET| |0429:00000429& GOTO KBLANG-PRESET| |CHE& GOTO KBLANG-SUBLANGS| |041a:0000041a& GOTO KBLANG-PRESET|
::6  |CZE& GOTO KBLANG-SUBLANGS| |0406:00000406& GOTO KBLANG-PRESET| |DIV& GOTO KBLANG-SUBLANGS| |0413:00000413& GOTO KBLANG-PRESET| |0C51:00000C51|
::7  |0425:00000425& GOTO KBLANG-PRESET| |0438:00000438& GOTO KBLANG-PRESET| |FIN& GOTO KBLANG-SUBLANGS| |0407:00120c00& GOTO KBLANG-PRESET| |GEO& GOTO KBLANG-SUBLANGS|
::8  |GER& GOTO KBLANG-SUBLANGS| |0407:000c0c00& GOTO KBLANG-PRESET| |GRE& GOTO KBLANG-SUBLANGS| |046f:0000046f& GOTO KBLANG-PRESET| |0474:00000474& GOTO KBLANG-PRESET|
::9  |0447:00000447& GOTO KBLANG-PRESET| |0468:00000468& GOTO KBLANG-PRESET| |040d:0000040d& GOTO KBLANG-PRESET| |HUN& GOTO KBLANG-SUBLANGS| |040f:0000040f& GOTO KBLANG-PRESET|
::10 |0470:00000470& GOTO KBLANG-PRESET| |4009:00004009& GOTO KBLANG-PRESET| |INU& GOTO KBLANG-SUBLANGS| |083C:000001809& GOTO KBLANG-PRESET| |ITA& GOTO KBLANG-SUBLANGS|
::11 |0411:{03B5835F-F03C-411B-9CE2-AA23E1171E36}{A76C93D9-5523-4E90-AAFA-4DB112F9AC76}& GOTO KBLANG-PRESET| |0421:00110c00& GOTO KBLANG-PRESET| |044b:0000044b& GOTO KBLANG-PRESET| |043f:0000043f& GOTO KBLANG-PRESET| |KHM& GOTO KBLANG-SUBLANGS|
::12 |0457:00000439& GOTO KBLANG-PRESET| |KOR& GOTO KBLANG-SUBLANGS| |0440:00000440& GOTO KBLANG-PRESET| |0454:00000454& GOTO KBLANG-PRESET| |080a:0000080a& GOTO KBLANG-PRESET|
::13 |LAT& GOTO KBLANG-SUBLANGS| |LIS& GOTO KBLANG-SUBLANGS| |LIT& GOTO KBLANG-SUBLANGS| |046e:0000046e& GOTO KBLANG-PRESET| |MAC& GOTO KBLANG-SUBLANGS|
::14 |044c:0000044c& GOTO KBLANG-PRESET| |MAL& GOTO KBLANG-SUBLANGS| |0481:00000481& GOTO KBLANG-PRESET| |044e:0000044e& GOTO KBLANG-PRESET| |MON& GOTO KBLANG-SUBLANGS|
::15 |0455:00010c00& GOTO KBLANG-PRESET| |0409:000090c00& GOTO KBLANG-PRESET| |0461:00000461& GOTO KBLANG-PRESET| |0409:00020c00& GOTO KBLANG-PRESET| |NOR& GOTO KBLANG-SUBLANGS|
::16 |0448:00000448& GOTO KBLANG-PRESET| |0409:d0c00& GOTO KBLANG-PRESET| |0409:000f0c00& GOTO KBLANG-PRESET| |0409:000e0c00& GOTO KBLANG-PRESET| |0463:00000463& GOTO KBLANG-PRESET|
::17 |PER& GOTO KBLANG-SUBLANGS| |0409:000a0c00& GOTO KBLANG-PRESET| |POL& GOTO KBLANG-SUBLANGS| |0446:00000446& GOTO KBLANG-PRESET| |ROM& GOTO KBLANG-SUBLANGS|
::18 |0485:00000485& GOTO KBLANG-PRESET| |SAM& GOTO KBLANG-SUBLANGS| |0809:00011809& GOTO KBLANG-PRESET| |SER& GOTO KBLANG-SUBLANGS| |046c:0000046c& GOTO KBLANG-PRESET|
::19 |0432:00000432& GOTO KBLANG-PRESET| |SIN& GOTO KBLANG-SUBLANGS| |SLO& GOTO KBLANG-SUBLANGS| |0424:00000424& GOTO KBLANG-PRESET| |0409:00100c00& GOTO KBLANG-PRESET|
::20 |SOR& GOTO KBLANG-SUBLANGS| |SWE& GOTO KBLANG-SUBLANGS| |SWI& GOTO KBLANG-SUBLANGS| |SYR& GOTO KBLANG-SUBLANGS| |0409:00030c00& GOTO KBLANG-PRESET|
::21 |0428:00000428& GOTO KBLANG-PRESET| |TAM& GOTO KBLANG-SUBLANGS| |TAT& GOTO KBLANG-SUBLANGS| |044a:0000044a& GOTO KBLANG-PRESET| |THA& GOTO KBLANG-SUBLANGS|
::22 |TIB& GOTO KBLANG-SUBLANGS| |TIF& GOTO KBLANG-SUBLANGS| |0473:{E429B25A-E5D3-4D1F-9BE3-0C608477E3A1}{3CAB88B7-CC3E-46A6-9765-B772AD7761FF}& GOTO KBLANG-PRESET| |TUR& GOTO KBLANG-SUBLANGS| |0442:00000442& GOTO KBLANG-PRESET|
::23 |UYG& GOTO KBLANG-SUBLANGS| |UKR& GOTO KBLANG-SUBLANGS| |UK& GOTO KBLANG-SUBLANGS| |0420:00000420| |0843:00000843|
::24 |VIE& GOTO KBLANG-SUBLANGS| |0488:00000488& GOTO KBLANG-PRESET| |0485:00000485& GOTO KBLANG-PRESET| |046a:0000056a& GOTO KBLANG-PRESET|

REM DB-Submenu Input Methods
:: |ARA| |Arabic (101)| |Arabic (102)| |Arabic (102 AZERTY)|
:: |ARM| |Armenian Eastern| |Armenian Phonetic| |Armenian Typewriter| |Armenian Western|
:: |AZE| |Azerbaijani (Standard)| |Azerbaijani Cyrillic| |Azerbaijani Latin|
:: |BEL| |Belgian (Comma)| |Belgian (Period)| |Belgian French|
:: |BAN| |Bangla (Bangladesh)| |Bangla (India)| |Bangla (India) - Legacy|
:: |BUL| |Bulgarian| |Bulgarian Latin| |Bulgarian (Phonetic Layout)| |Bulgarian (Phonetic Traditonal)| |Bulgarian (Typewriter)|
:: |CAN| |Canadian French| |Canadian French (Legacy)| |Canadian Multilingual Standard|
:: |CHE| |Cherokee Nation| |Cherokee Nation Phonetic|
:: |CHI| |Chineese (Simplified)| |Chineese (Traditional) NON-FUNCTIONAL| |Chineese (Traditional, Hong Kong S.A.R.)| |Chineese (Traditonal Macao S.A.R.)| |Chineese (Simplified, Singapore)|
:: |CZE| |Czech| |Czech (QWERTY)| |Czech Programmers|
:: |DIV| |Divehi Phonetic| |Divehi Typewriter|
:: |FIN| |Finnish| |Finnish with Sami|
:: |GEO| |Georgian| |Georgian (Ergonomic)| |Georgian (QWERTY)| |Georgian Ministry of Education and Science Schools| |Georgian (Old Alphabets)|
:: |GER| |German| |German (IBM)|
:: |GRE| |Greek| |Greek (220)| |Greek (220) Latin| |Greek (319)| |Greek (319) Latin| |Greek Latin| |Greek Polytonic|
:: |HUN| |Hungarian| |Hungarian 101-key|
:: |INU| |Inuktitut - Latin| |Inuktitut - Naqittaut|
:: |ITA| |Italian| |Italian (142)|
:: |KHM| |Khmer| |Khmer (NIDA)|
:: |KOR| |Korean (Hangul)| |Korean (Old Hangul)|
:: |LAT| |Latvian (Standard)| |Latvian (Legacy)|
:: |LIS| |Lisu (Basic)| |Lisu (Standard)|
:: |LIT| |Lithuanian| |Lithuanian IBM| |Lithuanian Standard|
:: |MAC| |Macedonian (FYROM)| |Macedonian (FYROM) - Standard|
:: |MAL| |Maltese 47-key| |Maltese 48-key|
:: |MON| |Mongoloian (Mongolian Script - Legacy)| |Mongolian (Mongolian Script - Standard)| |Mongolian Cyrillic|
:: |NOR| |Norwegian| |Norwegian with Sami|
:: |PER| |Persian| |Persian (Standard)|
:: |POL| |Polish (214)| |Polish (Programmers)|
:: |POR| |Portuguese| |Portuguese (Brazilian ABNT)| |Portuguese (Brazilian ABNT2)|
:: |ROM| |Romanian (Legacy)| |Romanian (Programmers) |Romanian (Standard)|
:: |RUS| |Russian| |Russian - Mnemonic| |Russian (Typewriter)|
:: |SAM| |Sami Extended Finland-Sweden| |Sami Extended Norway|
:: |SER| |Serbian (Cyrillic)| |Serbian (Latin)|
:: |SIN| |Sinhala| |Sinhala - wij 9|
:: |SLO| |Slovak| |Slovak (QWERTY)|
:: |SOR| |Sorbian Extended| |Sorbian Standard| |Sorbian Standard (Legacy)|
:: |SPA| |Spanish (Spain)| |Spanish (Mexico)| |Spanish Variation|
:: |SWE| |Swedish| |Swedish with Sami|
:: |SWI| |Swiss French| |Swiss German|
:: |SYR| |Syriac| |Syriac Phonetic|
:: |TAM| |Tamil| |Tamil (99 Keyboard)|
:: |TAT| |Tatar| |Tatar (Legacy)|
:: |THA| |Thai Kedmanee| |Thai Kedmanee (non-ShiftLock)| |Thai Pattachote| |Thai Pattachote (non-ShiftLock)|
:: |TIB| |Tibetan (PRC - Standard)| |Tibetan (PRC - Legacy)|
:: |TIF| |Tifinagh (Basic)| |Tifinagh (Full)|
:: |TUR| |Turkish F| |Turkish Q|
:: |UYG| |Uyghur| |Uygher (Legacy)| |Uyghur (Greek 220)|
:: |UKR| |Ukrainian| |Ukrainian (Enhanced)|
:: |UK|  |United Kingdom| |United Kingdom Extended|
:: |US|  |United States - English| |United States - International| |United States - Dvorak| |United States - Dvorak (Left Hand)| |United States - Dvorak (Right Hand)|
:: |VIE| |Vietnamese| |Vietnamese Telex|

REM DB-SubCorresponding Input Methods
:: |ARA| |0401:00000401| |0401:00010401| |0401:00020401|
:: |ARM| |042b:0000042b| |042b:0002042b| |042b:0003042b| |042b:0001042b|
:: |AZE| |042c:0001042c| |042c:0000082c| |042c:0000042c|
:: |BEL| |080c:0001080c| |080c:00000813| |080c:0000080c|
:: |BAN| |0445:00000445| |0445:00020445| |0445:00010445|
:: |BUL| |042b:0003042b| |042b:0001042b| |042b:0002042b| |042b:0004042b| |042b:0000042b|
:: |CAN| |0c0c:00001009| |0c0c:00000c0c| |0c0c:00011009|
:: |CHE| |045c:0000045c| |045c:0001045c|
:: |CHI| |0804:{81D4E9C9-1D3B-41BC-9E6C-4B40BF79E35E}{FA550B04-5AD7-411f-A5AC-CA038EC515D7}| |0404:{B115690A-EA02-48D5-A231-E3578D2FDF80}{B2F9C502-1742-11D4-9790-0080C882687E}| |0404:00000c04| |0404:00001404| |0404:00001004|
:: |CZE| |2000:00000405| |2000:00010405| |2000:00020405|
:: |DIV| |0465:00000465| |0465:00010465|
:: |FIN| |040b:0000040b| |040b:0001083b|
:: |GEO| |0437:00020437| |0437:00010437| |0437:00030437| |0437:00040437|
:: |GER| |0407:00000407| |0407:00010407|
:: |GRE| |0408:00000408| |0408:00010408| |0408:00030408| |0408:00020408| |0408:00040408| |0408:00050408| |0408:00600408|
:: |HUN| |040e:0000040e| |040e:0001040e|
:: |INU| |085d:0000085d| |085d:0001045d|
:: |ITA| |0410:00000410| |0410:00010410|
:: |KHM| |0453:00000453| |0453:00010453|
:: |KOR| |0412:{A028AE76-01B1-46C2-99C4-ACD9858AE02F}{B5FE1F02-D5F2-4445-9C03-C568F23C99A1}| |0412:{a1e2b86b-924a-4d43-80f6-8a820df7190f}{b60af051-257a-46bc-b9d3-84dad819bafb}|
:: |LAT| |0426:00020426| |0426:00010426|
:: |LIS| |0409:00070c00| |0409:00080c00|
:: |LIT| |0427:00010427| |0427:00000427| |0427:00020427|
:: |MAC| |042f:0000042f| |042f:0001042f|
:: |MAL| |043a:0000043a| |043a:0001043a|
:: |MON| |0850:00000850| |0850:00020850| |0850:00000450|
:: |NOR| |0814:00000414| |0814:0000043b|
:: |PER| |0429:00000429| |0429:00050429|
:: |POL| |0415:00010415| |0415:00000415|
:: |POR| |0816:00000816| |0816:00000416| |0816:00010416|
:: |ROM| |0418:00000418| |0418:00020418| |0418:00010418|
:: |RUS| |0419:00000419| |0419:00020419| |0419:00010419|
:: |SAM| |083b:0002083b| |043b:0001043b|
:: |SER| |1C1A:00000c1a| |241A:0000081a|
:: |SIN| |045b:0000045b| |045b:0001045b|
:: |SLO| |041b:0000041b| |041b:0001041b|
:: |SOR| |042e:0001042e| |042e:0002042e| |042e:0000042e|
:: |SPA| |0c0a:0000040a| |080a:0000080a| |0c0a:0001040a|
:: |SWE| |041d:0000041d| |083b:0000083b|
:: |SWI| |100c:0000100c| |0807:00000807|
:: |SYR| |045a:0000045a| |045a:0001045a|
:: |TAM| |0449:00000449| |0449:00020449|
:: |TAT| |0444:00010444| |0444:00000444|
:: |THA| |041e:0000041e| |041e:0002041e| |041e:0001041e| |041e:0003041e|
:: |TIB| |0451:00010451| |0451:00000451|
:: |TIF| |0409:00050c00| |0409:00050c00|
:: |TUR| |041f:0001041f| |041f:0000041f|
:: |UYG| |0480:00010480| |0480:00000480| |0480:00010408|
:: |UKR| |0422:00000422| |0422:00020422|
:: |UK|  |0809:00000809| |0809:00000452|
:: |US|  |0409:00000409| |0409:00020409| |0409:00010409| |0409:00030409| |0409:00040409|
:: |VIE| |042a:0000042a| |042A:{C2CB2CF0-AF47-413E-9780-8BC3A3C16068}{5FB02EC5-0A77-4684-B4FA-DEF8A2195628}|
REM Marker

:KBLANG-PRESET

SETLOCAL
IF /I "%~1"=="kbLangSet" WAITFOR /SI Golden>NUL 2>&1 & SET "kbLangSel=%~2" & SET "kbMakeDef=%~3" & GOTO KBLANG-SETLANG
CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                           ^| Central AME Script %ver% ^| & ECHO. & ECHO.
CHOICE /C YN /N /M "%BS%           Make default keyboard language? (Y/N): "
	IF %ERRORLEVEL%==2 SET "kbMakeDef=false"
ECHO. & ECHO                           Adding keyboard language...
TIMEOUT /T 1 /NOBREAK > NUL
POWERSHELL -NoP -C "(Get-WmiObject Win32_Process -Filter ProcessId=$PID).ParentProcessId" > %temp%\CentralAMEProcessID.txt
	FOR /F "delims=" %%A IN (%temp%\CentralAMEProcessID.txt) DO SET "scriptPID=%%A" & DEL "%temp%\CentralAMEProcessID.txt"
REM Detects if the script is being run under the currently logged in user. If not it runs a second instance as the current user
FOR /F tokens^=2^ delims^=^" %%A IN ('TASKLIST /FI "PID eq %scriptPID%" /FI "USERNAME eq %currentUsername%" /NH /FO csv') DO SET processRunOut=%%A
	IF NOT "%processRunOut%"=="," (
		REM If %lim% = rem that means the script is NOT being run as administrator. There's a small chance someone attempts to run it as another non-admin user,
		REM this would be problematic as schtasks requires admin privilages.
		IF "%lim%"=="rem " ECHO. & ECHO. & ECHO                      Script must be run as the current user & ECHO                         or with administrator privilages. & ECHO            __________________________________________________________ & ECHO. & ENDLOCAL & PAUSE > NUL|SET /P =%BS%           Press any key to return to the Menu: & GOTO HOME-MAINMENU
		SETLOCAL ENABLEDELAYEDEXPANSION
		SCHTASKS /CREATE /tn SetDispLang /tr "CMD /C 'START /min '' '%scriptPath%' kbLangSet %kbLangSel% %kbMakeDef%'" /sc ONSTART /ru "%currentUsername%" /it /f > NUL
		REM RUNAS will work, however it requires the user to enter a password, and won't accept a blank one. This is a lot more simple and reliable
		SCHTASKS /RUN /tn SetDispLang > NUL
			WAITFOR Golden /T 10 > NUL 2>&1
				IF !ERRORLEVEL! LSS 1 SCHTASKS /DELETE /tn SetDispLang /f>NUL & GOTO KBLANG-COMPLETE
			SCHTASKS /DELETE /tn SetDispLang /f > NUL
			ENDLOCAL & ENDLOCAL & ECHO. & ECHO. & ECHO                             Action may have failed. & ECHO            __________________________________________________________ & ECHO. & PAUSE > NUL|SET /P =%BS%           Press any key to return to the Menu: & GOTO HOME-MAINMENU )

:KBLANG-SETLANG

POWERSHELL -NoP -C "$NewLangs=Get-WinUserLanguageList; $NewLangs[0].InputMethodTips.Add('%kbLangSel%'); Set-WinUserLanguageList $NewLangs -Force" > NUL
IF NOT "%kbMakeDef%"=="false" POWERSHELL -NoP -C "Set-WinDefaultInputMethodOverride -InputTip "%kbLangSel%""
IF /I "%~1"=="kbLangSet" EXIT 0

:KBLANG-COMPLETE

ECHO. & ECHO. & ECHO                       Keyboard language added successfully & ECHO            __________________________________________________________ & ECHO.
PAUSE > NUL|SET /P =%BS%           Press any key to return to the Menu: 
ENDLOCAL & GOTO HOME-MAINMENU
REM -------------------------KBLANG-END-------------------------



REM -------------------------NOUSERNAME-------------------------
:NOUSERNAME-MENU

SETLOCAL
CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                           ^| Central AME Script %ver% ^| & ECHO. & ECHO. & ECHO                  [1] Disable Username Requirement & ECHO                  [2] Enable Username Requirement & ECHO. & ECHO                  [0] Return to Menu & ECHO                  [X] Exit & ECHO. & ECHO            __________________________________________________________ & ECHO.
CHOICE /C 120X /N /M "%BS%           Choose a menu option: "
	IF %ERRORLEVEL%==1 GOTO NOUSERNAME-DISABLE
	IF %ERRORLEVEL%==2 GOTO NOUSERNAME-ENABLE
	IF %ERRORLEVEL%==3 ENDLOCAL & GOTO HOME-MAINMENU
	IF %ERRORLEVEL%==4 EXIT /B 0

:NOUSERNAME-DISABLE

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                           ^| Central AME Script %ver% ^| & ECHO.
TIMEOUT /T 1 /NOBREAK > NUL
REG DELETE "HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System" /v dontdisplaylastusername /f > NUL 2>&1
ECHO. & ECHO                  The username login requirement is now disabled & ECHO                       A restart is needed to take effect. & ECHO            __________________________________________________________ & ECHO.
CHOICE /C YN /N /M "%BS%           Would you like to restart now? (Y/N): "
	IF %ERRORLEVEL%==1 SHUTDOWN -R -T 0 & EXIT 0
	IF %ERRORLEVEL%==2 ENDLOCAL & GOTO HOME-MAINMENU

:NOUSERNAME-ENABLE

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                           ^| Central AME Script %ver% ^| & ECHO.
TIMEOUT /T 1 /NOBREAK > NUL
REG ADD "HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System" /v dontdisplaylastusername /t REG_DWORD /d 1 /f > NUL 2>&1
ECHO. & ECHO                  The username login requirement is now enabled & ECHO                       A restart is needed to take effect. & ECHO            __________________________________________________________ & ECHO.
CHOICE /C YN /N /M "%BS%           Would you like to restart now? (Y/N): "
	IF %ERRORLEVEL%==1 SHUTDOWN -R -T 0 & EXIT 0
	IF %ERRORLEVEL%==2 ENDLOCAL & GOTO HOME-MAINMENU
REM -----------------------NOUSERNAME-END-----------------------



REM -------------------------HIBERNATE-------------------------
:HIBERNATE-MENU

SETLOCAL
CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                          ^| Central AME Script %ver% ^| & ECHO. & ECHO. & ECHO                  [1] Enable Hibernation & ECHO                  [2] Disable Hibernation & ECHO. & ECHO                  [0] Return to Menu & ECHO                  [X] Exit & ECHO. & ECHO            __________________________________________________________ & ECHO.
CHOICE /C 120X /N /M "%BS%           Choose a menu option: "
	IF %ERRORLEVEL%==1 GOTO HIBERNATE-ENABLE
	IF %ERRORLEVEL%==2 GOTO HIBERNATE-DISABLE
	IF %ERRORLEVEL%==3 ENDLOCAL & GOTO HOME-MAINMENU
	IF %ERRORLEVEL%==4 EXIT /B 0

:HIBERNATE-ENABLE

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                           ^| Central AME Script %ver% ^| & ECHO. & ECHO                             Enabling Hibernation...
TIMEOUT /T 2 /NOBREAK > NUL
POWERCFG /HIBERNATE /TYPE FULL > NUL 2>&1
	IF %ERRORLEVEL% NEQ 0 (
		ECHO. & ECHO. & ECHO                          Failed to enable hibernation. & ECHO                Hibernation may not be supported by your firmware. & ECHO            __________________________________________________________ & ECHO.
		PAUSE > NUL|SET /P =%BS%           Press any key to return to the Menu: 
		ENDLOCAL & GOTO HOME-MAINMENU
	)
SET "hibernate=enable" & GOTO HIBERNATE-FINISH

:HIBERNATE-DISABLE

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                           ^| Central AME Script %ver% ^| & ECHO. & ECHO                             Disabling Hibernation...
TIMEOUT /T 2 /NOBREAK > NUL
POWERCFG /HIBERNATE OFF > NUL 2>&1
	IF %ERRORLEVEL% NEQ 0 (
		ECHO. & ECHO. & ECHO                          Failed to disable hibernation. & ECHO            __________________________________________________________ & ECHO.
		PAUSE > NUL|SET /P =%BS%           Press any key to return to the Menu: 
		ENDLOCAL & GOTO HOME-MAINMENU
	)
SET "hibernate=disable" & GOTO HIBERNATE-FINISH

:HIBERNATE-FINISH

IF "%hibernate%"=="enable" SET "hibernateResult=                           Hibernation is now enabled"
IF "%hibernate%"=="disable" SET "hibernateResult=                          Hibernation is now disabled"
ECHO. & ECHO. & ECHO %hibernateResult% & ECHO            __________________________________________________________ & ECHO.
PAUSE > NUL|SET /P =%BS%           Press any key to return to the Menu: 
ENDLOCAL & GOTO HOME-MAINMENU
REM -----------------------HIBERNATE-END-----------------------



REM ----------------------------WSH-----------------------------
:WSH-MENU

SETLOCAL
CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                          ^| Central AME Script %ver% ^| & ECHO. & ECHO. & ECHO                  [1] Enable WSH & ECHO                  [2] Disable WSH & ECHO. & ECHO                  [0] Return to Menu & ECHO                  [X] Exit & ECHO. & ECHO            __________________________________________________________ & ECHO.
CHOICE /C 120X /N /M "%BS%           Choose a menu option: "
	IF %ERRORLEVEL%==1 GOTO WSH-ENABLE
	IF %ERRORLEVEL%==2 GOTO WSH-DISABLE
	IF %ERRORLEVEL%==3 ENDLOCAL & GOTO HOME-MAINMENU
	IF %ERRORLEVEL%==4 EXIT /B 0

:WSH-ENABLE

SET "cenStr=Enabling WSH for %currentUsername%..." & CALL :AUX-CENTERTEXT
CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                           ^| Central AME Script %ver% ^| & ECHO. & ECHO%cenOut%
TIMEOUT /T 2 /NOBREAK > NUL
FOR /F "tokens=* USEBACKQ" %%F IN (`WMIC useraccount where "name="%currentUsername%"" get sid ^| FINDSTR "S-"`) DO SET WSHSID=%%F
	SET WSHSID=%WSHSID:~0,-3%
REG ADD "HKEY_USERS\%WSHSID%\SOFTWARE\Microsoft\Windows Script Host\Settings" /v Enabled /t REG_DWORD /d 1 /f > NUL
REG ADD "HKLM\SOFTWARE\Microsoft\Windows Script Host\Settings" /v Enabled /t REG_DWORD /d 1 /f > NUL
SET "wsh=enable" & GOTO WSH-FINISH

:WSH-DISABLE

SET "cenStr=Disabling WSH for %currentUsername%..." & CALL :AUX-CENTERTEXT
CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                           ^| Central AME Script %ver% ^| & ECHO. & ECHO%cenOut%
TIMEOUT /T 2 /NOBREAK > NUL
FOR /F "tokens=* USEBACKQ" %%F IN (`WMIC useraccount where "name="%currentUsername%"" get sid ^| FINDSTR "S-"`) DO SET WSHSID=%%F
	SET WSHSID=%WSHSID:~0,-3%
REG ADD "HKEY_USERS\%WSHSID%\SOFTWARE\Microsoft\Windows Script Host\Settings" /v Enabled /t REG_DWORD /d 0 /f > NUL
REG ADD "HKLM\SOFTWARE\Microsoft\Windows Script Host\Settings" /v Enabled /t REG_DWORD /d 0 /f > NUL
SET "wsh=disable" & GOTO WSH-FINISH

:WSH-FINISH

IF "%wsh%"=="enable" SET "cenStr=WSH is now enabled for %currentUsername%" & SET "wshRestartMsg=& ECHO                   A restart is required to complete the setup. "
IF "%wsh%"=="disable" SET "cenStr=WSH is now disabled for %currentUsername%" & SET "wshRestartMsg="
CALL :AUX-CENTERTEXT
ECHO. & ECHO. & ECHO%cenOut:~1% %wshRestartMsg%& ECHO            __________________________________________________________ & ECHO.
IF "%wsh%"=="enable" CHOICE /C YN /N /M "%BS%           Would you like to restart now? (Y/N): "
	IF %ERRORLEVEL%==1 SHUTDOWN -R -T 0 & EXIT 0
	IF %ERRORLEVEL%==2 ENDLOCAL & GOTO HOME-MAINMENU
PAUSE > NUL|SET /P =%BS%           Press any key to return to the Menu: 
ENDLOCAL & GOTO HOME-MAINMENU
REM --------------------------WSH-END--------------------------



REM ---------------------------NCSI----------------------------
:NCSI-MENU

SETLOCAL
CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                          ^| Central AME Script %ver% ^| & ECHO. & ECHO. & ECHO                  [1] Enable NCSI Active Probing & ECHO                  [2] Disable NCSI Active Probing & ECHO. & ECHO                  [0] Return to Menu & ECHO                  [X] Exit & ECHO. & ECHO            __________________________________________________________ & ECHO.
CHOICE /C 120X /N /M "%BS%           Choose a menu option: "
	IF %ERRORLEVEL%==1 GOTO NCSI-ENABLE
	IF %ERRORLEVEL%==2 GOTO NCSI-DISABLE
	IF %ERRORLEVEL%==3 ENDLOCAL & GOTO HOME-MAINMENU
	IF %ERRORLEVEL%==4 EXIT /B 0

:NCSI-ENABLE

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                           ^| Central AME Script %ver% ^| & ECHO. & ECHO                         Enabling NCSI Active Probing...
TIMEOUT /T 2 /NOBREAK > NUL
REG ADD "HKLM\SYSTEM\CurrentControlSet\Services\NlaSvc\Parameters\Internet" /v EnableActiveProbing /t REG_DWORD /d 1 /f > NUL
SET "ncsi=enable" & GOTO NCSI-FINISH

:NCSI-DISABLE

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                           ^| Central AME Script %ver% ^| & ECHO. & ECHO                         Disabling NCSI Active Probing...
TIMEOUT /T 2 /NOBREAK > NUL
REG ADD "HKLM\SYSTEM\CurrentControlSet\Services\NlaSvc\Parameters\Internet" /v EnableActiveProbing /t REG_DWORD /d 0 /f > NUL
SET "ncsi=disable" & GOTO NCSI-FINISH

:NCSI-FINISH

IF "%ncsi%"=="enable" SET "ncsiResult=                      NCSI Active Probing is now disabled"
IF "%ncsi%"=="disable" SET "ncsiResult=                       NCSI Active Probing is now enabled"
ECHO. & ECHO. & ECHO %ncsiResult% & ECHO                   A restart is required to take effect. & ECHO            __________________________________________________________ & ECHO.
CHOICE /C YN /N /M "%BS%           Would you like to restart now? (Y/N): "
	IF %ERRORLEVEL%==1 SHUTDOWN -R -T 0 & EXIT 0
	IF %ERRORLEVEL%==2 ENDLOCAL & GOTO HOME-MAINMENU
REM -------------------------NCSI-END--------------------------



REM --------------------------NEWUSER--------------------------
:NEWUSER-MENU

SETLOCAL

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                          ^| Central AME Script %ver% ^| & ECHO. & ECHO              WARNING: This is a beta feature, use at your own risk. & ECHO            __________________________________________________________ & ECHO.
PAUSE > NUL|SET /P =%BS%           Press any key to to continue: 

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                          ^| Central AME Script %ver% ^| & ECHO. & ECHO. & ECHO                  [1] Create a New User & ECHO                  [2] Remove Existing User & ECHO. & ECHO                  [0] Return to Menu & ECHO                  [X] Exit & ECHO. & ECHO            __________________________________________________________ & ECHO.
CHOICE /C 120X /N /M "%BS%           Choose a menu option: "
	IF %ERRORLEVEL%==1 GOTO NEWUSER-CREATE
	IF %ERRORLEVEL%==2 GOTO NEWUSER-REMOVE
	IF %ERRORLEVEL%==3 ENDLOCAL & GOTO HOME-MAINMENU
	IF %ERRORLEVEL%==4 EXIT /B 0

:NEWUSER-CREATE

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                           ^| Central AME Script %ver% ^| & ECHO.

SET /P "username=%BS%           Enter desired username, or enter 'Cancel' to quit: "
	IF /I "%username%"=="Cancel" ENDLOCAL & GOTO HOME-MAINMENU

	IF "%username%"=="" (
		ECHO. & ECHO. & ECHO                              Input cannot be blank. & ECHO            __________________________________________________________ & ECHO.
		PAUSE > NUL|SET /P =%BS%           Press any key to return to the Menu: 
		ENDLOCAL & GOTO HOME-MAINMENU
	)

ECHO.
SET /P "password=%BS%           Enter desired password, or enter 'Cancel' to quit: "
	IF /I "%password%"=="Cancel" ENDLOCAL & GOTO HOME-MAINMENU

ECHO. & ECHO                                 Creating user...

NET user "%username%" "%password%" /add > NUL 2>&1
	IF %ERRORLEVEL% NEQ 0 (
		ECHO. & ECHO. & ECHO                          Improper username or password. & ECHO            __________________________________________________________ & ECHO.
		PAUSE > NUL|SET /P =%BS%           Press any key to return to the Menu: 
		ENDLOCAL & GOTO HOME-MAINMENU
	)

TIMEOUT /T 1 /NOBREAK > NUL 2>&1

ECHO. & ECHO                             Configuring new user...

SCHTASKS /create /tn "AME NEWUSRREG" /tr "CMD /C 'FOR /F 'usebackq delims=' %%A IN (`REG QUERY 'HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Appx\AppxAllUserStore\InboxApplications'`) DO REG DELETE '%%A' /f'" /sc MONTHLY /f /rl HIGHEST /ru "SYSTEM" > NUL
SCHTASKS /run /tn "AME NEWUSRREG" > NUL
SCHTASKS /delete /tn "AME NEWUSRREG" /f > NUL

REG ADD "HKLM\SOFTWARE\Policies\Microsoft\Windows\OOBE" /v DisablePrivacyExperience /t REG_DWORD /d 1 /f > NUL 2>&1
REG ADD "HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System" /v EnableFirstLogonAnimation /t REG_DWORD /d 0 /f > NUL 2>&1

REG LOAD "HKU\DefaultHiveMount" "%SYSTEMDRIVE%\Users\Default\NTUSER.DAT" > NUL 2>&1

REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\OpenShell" /t REG_SZ /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\OpenShell\OpenShell" /t REG_SZ /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\OpenShell\OpenShell\Settings" /t REG_SZ /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\OpenShell\StartMenu" /t REG_SZ /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\OpenShell\StartMenu\Settings" /t REG_SZ /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\OpenShell\ClassicExplorer" /t REG_SZ /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\OpenShell\ClassicExplorer\Settings" /t REG_SZ /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\OpenShell\ClassicExplorer" /v "ShowedToolbar" /t REG_DWORD /d 1 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\OpenShell\ClassicExplorer" /v "NewLine" /t REG_DWORD /d 0 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\OpenShell\ClassicExplorer\Settings" /v "ShowStatusBar" /t REG_DWORD /d 0 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\OpenShell\StartMenu" /v "ShowedStyle2" /t REG_DWORD /d 1 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\OpenShell\StartMenu" /v "CSettingsDlg" /t REG_BINARY /d c80100001a0100000000000000000000360d00000100000000000000 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\OpenShell\StartMenu" /v "OldItems" /t REG_BINARY /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\OpenShell\StartMenu" /v "ItemRanks" /t REG_BINARY /d 0 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\OpenShell\StartMenu\MRU" /v "0" /t REG_SZ /d "%SYSTEMDRIVE%\Windows\regedit.exe" /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\OpenShell\StartMenu\Settings" /v "Version" /t REG_DWORD /d 04040098 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\OpenShell\StartMenu\Settings" /v "AllProgramsMetro" /t REG_DWORD /d 1 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\OpenShell\StartMenu\Settings" /v "RecentMetroApps" /t REG_DWORD /d 1 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\OpenShell\StartMenu\Settings" /v "StartScreenShortcut" /t REG_DWORD /d 0 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\OpenShell\StartMenu\Settings" /v "SearchInternet" /t REG_DWORD /d 0 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\OpenShell\StartMenu\Settings" /v "GlassOverride" /t REG_DWORD /d 1 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\OpenShell\StartMenu\Settings" /v "GlassColor" /t REG_DWORD /d 0 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\OpenShell\StartMenu\Settings" /v "SkinW7" /t REG_SZ /d "Fluent-Metro" /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\OpenShell\StartMenu\Settings" /v "SkinVariationW7" /t REG_SZ /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\OpenShell\StartMenu\Settings" /v "SkinOptionsW7" /t REG_MULTI_SZ /d "USER_IMAGE=1"\0"SMALL_ICONS=0"\0"LARGE_FONT=0"\0"DISABLE_MASK=0"\0"OPAQUE=0"\0"TRANSPARENT_LESS=0"\0"TRANSPARENT_MORE=1"\0"WHITE_SUBMENUS2=0" /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\OpenShell\StartMenu\Settings" /v "SkipMetro" /t REG_DWORD /d 1 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\OpenShell\StartMenu\Settings" /v "MenuItems7" /t REG_MULTI_SZ /d "Item1.Command=user_files"\0"Item1.Settings=NOEXPAND"\0"Item2.Command=user_documents"\0"Item2.Settings=NOEXPAND"\0"Item3.Command=user_pictures"\0"Item3.Settings=NOEXPAND"\0"Item4.Command=user_music"\0"Item4.Settings=NOEXPAND"\0"Item5.Command=user_videos"\0"Item5.Settings=NOEXPAND"\0"Item6.Command=downloads"\0"Item6.Settings=NOEXPAND"\0"Item7.Command=homegroup"\0"Item7.Settings=ITEM_DISABLED"\0"Item8.Command=separator"\0"Item9.Command=games"\0"Item9.Settings=TRACK_RECENT|NOEXPAND|ITEM_DISABLED"\0"Item10.Command=favorites"\0"Item10.Settings=ITEM_DISABLED"\0"Item11.Command=recent_documents"\0"Item12.Command=computer"\0"Item12.Settings=NOEXPAND"\0"Item13.Command=network"\0"Item13.Settings=ITEM_DISABLED"\0"Item14.Command=network_connections"\0"Item14.Settings=ITEM_DISABLED"\0"Item15.Command=separator"\0"Item16.Command=control_panel"\0"Item16.Settings=TRACK_RECENT"\0"Item17.Command=pc_settings"\0"Item17.Settings=TRACK_RECENT"\0"Item18.Command=admin"\0"Item18.Settings=TRACK_RECENT|ITEM_DISABLED"\0"Item19.Command=devices"\0"Item19.Settings=ITEM_DISABLED"\0"Item20.Command=defaults"\0"Item20.Settings=ITEM_DISABLED"\0"Item21.Command=help"\0"Item21.Settings=ITEM_DISABLED"\0"Item22.Command=run"\0"Item23.Command=apps"\0"Item23.Settings=ITEM_DISABLED"\0"Item24.Command=windows_security"\0"Item24.Settings=ITEM_DISABLED"\0"" /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\OpenShell\StartMenu\Settings" /v "SkinOptionsW7" /t REG_MULTI_SZ /d "DARK_MAIN=0"\0"METRO_MAIN=0"\0"LIGHT_MAIN=0"\0"AUTOMODE_MAIN=1"\0"DARK_SUBMENU=0"\0"METRO_SUBMENU=0"\0"LIGHT_SUBMENU=0"\0"AUTOMODE_SUBMENU=1"\0"SUBMENU_SEPARATORS=1"\0"DARK_SEARCH=0"\0"METRO_SEARCH=0"\0"LIGHT_SEARCH=0"\0"AUTOMODE_SEARCH=1"\0"SEARCH_FRAME=1"\0"SEARCH_COLOR=0"\0"MODERN_SEARCH=1"\0"SEARCH_ITALICS=0"\0"NONE=0"\0"SEPARATOR=0"\0"TWO_TONE=1"\0"CLASSIC_SELECTOR=1"\0"HALF_SELECTOR=0"\0"CURVED_MENUSEL=1"\0"CURVED_SUBMENU=0"\0"SELECTOR_REVEAL=1"\0"TRANSPARENT=0"\0"OPAQUE_SUBMENU=1"\0"OPAQUE_MENU=0"\0"OPAQUE=0"\0"STANDARD=0"\0"SMALL_MAIN2=1"\0"SMALL_ICONS=0"\0"COMPACT_SUBMENU=0"\0"PRESERVE_MAIN2=0"\0"LESS_PADDING=0"\0"EXTRA_PADDING=1"\0"24_PADDING=0"\0"LARGE_PROGRAMS=0"\0"TRANSPARENT_SHUTDOWN=0"\0"OUTLINE_SHUTDOWN=0"\0"BUTTON_SHUTDOWN=1"\0"EXPERIMENTAL_SHUTDOWN=0"\0"LARGE_FONT=0"\0"CONNECTED_BORDER=1"\0"FLOATING_BORDER=0"\0"LARGE_SUBMENU=0"\0"LARGE_LISTS=0"\0"THIN_MAIN2=0"\0"EXPERIMENTAL_MAIN2=1"\0"USER_IMAGE=1"\0"USER_OUTSIDE=0"\0"SCALING_USER=1"\0"56=0"\0"64=0"\0"TRANSPARENT_USER=0"\0"UWP_SCROLLBAR=0"\0"MODERN_SCROLLBAR=1"\0"SMALL_ARROWS=0"\0"ARROW_BACKGROUND=1"\0"ICON_FRAME=0"\0"SEARCH_SEPARATOR=0"\0"NO_PROGRAMS_BUTTON=0"\0"" /f > NUL 2>&1

REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\Microsoft\Windows\CurrentVersion\Search" /v SearchboxTaskbarMode /t REG_DWORD /d 0 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced" /v ShowTaskViewButton /t REG_DWORD /d 0 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer" /v EnableAutoTray /t REG_DWORD /d 0 /f > NUL 2>&1

REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\Microsoft\Windows\CurrentVersion\AdvertisingInfo" /v Enabled /t REG_DWORD /d 0 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\Microsoft\Windows\CurrentVersion\AppHost" /v EnableWebContentEvaluation /t REG_DWORD /d 0 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\Control Panel\International\User Profile" /v HttpAcceptLanguageOptOut /t REG_DWORD /d 1 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\Software\Policies\Microsoft\Windows\Explorer" /v DisableNotificationCenter /t REG_DWORD /d 1 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\Microsoft\Windows\CurrentVersion\GameDVR" /v AppCaptureEnabled /t REG_DWORD /d 0 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\System\GameConfigStore" /v GameDVR_Enabled /t REG_DWORD /d 0 /f > NUL 2>&1

REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Attachments" /v SaveZoneInformation /t REG_DWORD /d 1 /f > NUL 2>&1

REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\Microsoft\Windows\CurrentVersion\AppHost" /v ContentEvaluation /t REG_DWORD /d 0 /f > NUL 2>&1

REG ADD "HKEY_USERS\DefaultHiveMount\Control Panel\Desktop" /v WaitToKillAppTimeOut /t REG_SZ /d 2000 /f > NUL 2>&1

REG DELETE "HKEY_USERS\DefaultHiveMount\SOFTWARE\Microsoft\Windows\CurrentVersion\StorageSense" /f > NUL 2>&1

REG ADD "HKEY_USERS\DefaultHiveMount\Software\Microsoft\Windows\CurrentVersion\Search" /v "BingSearchEnabled" /t REG_DWORD /d 0 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\Software\Microsoft\Windows\CurrentVersion\Search" /v "CortanaConsent" /t REG_DWORD /d 0 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\Software\Microsoft\Windows\CurrentVersion\Search" /v "CortanaInAmbientMode" /t REG_DWORD /d 0 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\Software\Microsoft\Windows\CurrentVersion\Search" /v "HistoryViewEnabled" /t REG_DWORD 0 /f  > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\Software\Microsoft\Windows\CurrentVersion\Search" /v "HasAboveLockTips" /t REG_DWORD /d 0 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\Software\Microsoft\Windows\CurrentVersion\Search" /v "AllowSearchToUseLocation" /t REG_DWORD /d 0 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\Software\Microsoft\Windows\CurrentVersion\SearchSettings" /v "SafeSearchMode" /t REG_DWORD /d 0 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\Software\Policies\Microsoft\Windows\Explorer" /v "DisableSearchBoxSuggestions" /t REG_DWORD /d 1 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\Software\Microsoft\InputPersonalization" /v "RestrictImplicitTextCollection" /t REG_DWORD /d 1 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\Software\Microsoft\InputPersonalization" /v "RestrictImplicitInkCollection" /t REG_DWORD /d 1 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\Software\Microsoft\InputPersonalization\TrainedDataStore" /v "AcceptedPrivacyPolicy" /t REG_DWORD /d 0 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\Software\Microsoft\InputPersonalization\TrainedDataStore" /v "HarvestContacts" /t REG_DWORD /d 0 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\Software\Microsoft\Personalization\Settings" /v "AcceptedPrivacyPolicy" /t REG_DWORD /d 0 /f > NUL 2>&1

REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\Policies\Microsoft\Windows\Explorer" /v "DisableSearchBoxSuggestions" /t REG_DWORD /d 1 /f > NUL 2>&1

REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced" /v "NavPaneShowAllFolders" /t REG_DWORD /d 0 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced" /v LaunchTo /t REG_DWORD /d 1 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced" /v HideFileExt /t REG_DWORD /d 0 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced" /v Hidden /t REG_DWORD /d 1 /f > NUL 2>&1

REG UNLOAD "HKU\DefaultHiveMount" > NUL 2>&1

TIMEOUT /T 1 /NOBREAK > NUL 2>&1

ECHO. & ECHO. & ECHO                            User created successfully. & ECHO            __________________________________________________________ & ECHO.
PAUSE > NUL|SET /P =%BS%           Press any key to return to the Menu: 
ENDLOCAL & GOTO HOME-MAINMENU

:NEWUSER-REMOVE

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                           ^| Central AME Script %ver% ^| & ECHO.

SET /P "usernameRemove=%BS%           Enter the user to be removed, or enter 'Cancel' to quit: "
	IF /I "%username%"=="Cancel" ENDLOCAL & GOTO HOME-MAINMENU

	IF "%username%"=="" (
		ECHO. & ECHO. & ECHO                              Input cannot be blank. & ECHO            __________________________________________________________ & ECHO.
		PAUSE > NUL|SET /P =%BS%           Press any key to return to the Menu: 
		ENDLOCAL & GOTO HOME-MAINMENU
	)

	IF "%usernameRemove%"=="%currentUsername%" (
		ECHO. & ECHO. & ECHO                       User must not be the logged in user. & ECHO            __________________________________________________________ & ECHO.
		PAUSE > NUL|SET /P =%BS%           Press any key to return to the Menu: 
		ENDLOCAL & GOTO HOME-MAINMENU
	)

	NET USER "%usernameRemove%" > NUL 2>&1
		IF %ERRORLEVEL% NEQ 0 (
			ECHO. & ECHO. & ECHO                               User does not exist. & ECHO            __________________________________________________________ & ECHO.
			PAUSE > NUL|SET /P =%BS%           Press any key to return to the Menu: 
			ENDLOCAL & GOTO HOME-MAINMENU
		)

ECHO. & ECHO                                 Removing user...

NET user "%usernameRemove%" /delete > NUL 2>&1
	IF %ERRORLEVEL% NEQ 0 (
		ECHO. & ECHO. & ECHO                              Failed to remove user. & ECHO            __________________________________________________________ & ECHO.
		PAUSE > NUL|SET /P =%BS%           Press any key to return to the Menu: 
		ENDLOCAL & GOTO HOME-MAINMENU
	)

TIMEOUT /T 1 /NOBREAK > NUL 2>&1

ECHO. & ECHO. & ECHO                            User removed successfully. & ECHO            __________________________________________________________ & ECHO.
PAUSE > NUL|SET /P =%BS%           Press any key to return to the Menu: 
ENDLOCAL & GOTO HOME-MAINMENU
REM ------------------------NEWUSER-END------------------------

					REM ----------------
					REM Script Functions
					REM ----------------


REM -----------------------------------------------------------
:AUX-DOWNLOADFAILED

ECHO. & ECHO. & ECHO                                Download failed. & ECHO            __________________________________________________________ & ECHO.
PAUSE > NUL|SET /P =%BS%           Press any key to return to the Menu: 
GOTO HOME-MAINMENU
REM -----------------------------------------------------------



REM -----------------------------------------------------------
:AUX-CENTERTEXT

SETLOCAL ENABLEDELAYEDEXPANSION
SET "cenSize=58"
SET "offset=             "
SET "LEN=0"
	:CENTERTEXT-LOOP
	IF "!!cenStr:~%LEN%!!"=="" ENDLOCAL & GOTO CENTERTEXT-LOOPEND
    SET /A "LEN=%LEN%+1"
    FOR %%B IN ('%LEN%') DO ENDLOCAL & SET "LEN=%%B"
	SET "LEN=%LEN:~1,-1%
    GOTO CENTERTEXT-LOOP
	:CENTERTEXT-LOOPEND
SET "compare=__________________________________________________________"
SET "spaces=                                                                                                    "
SET /A "pref_len=%cenSize%-%LEN%-2" & SET /A "pref_len/=2"
SET /A "suf_len=%cenSize%-%LEN%-2-%pref_len%"
CALL SET "cenOutput=%%offset%%%%spaces:~0,%pref_len%%%%%CENSTR%%"
FOR /F "delims=" %%B in ("%cenOutput%") DO ENDLOCAL & SET "cenOut=%%B"
EXIT /B 0
REM -----------------------------------------------------------



REM -----------------------------------------------------------
:AUX-GETUSERNAME

REM Grabs current username. %username% can be problematic and %userprofile%
REM is hard to filter properly, thus why this method is used.
FOR /F "tokens=2 delims=\" %%B IN ('WMIC computersystem get username') DO SET currentUsername=%%B
	SET "currentUsername=%currentUsername:~0,-3%"
	REM Detection for if user changed their username without restarting
	IF "%currentUsername%"=="~0,-3" SET "currentUsername=RestartRequired"
	SET "possibleUserDir=%currentUsername%"
EXIT /B 0
REM -----------------------------------------------------------



REM -----------------------------------------------------------
:AUX-ELEVATIONCHECK

SET "elevCheckUsername="%currentUsername%""
IF /I "%currentUsername%"=="RestartRequired" SET "elevCheckUsername="
FOR /F "delims=" %%A IN ('NET user %elevCheckUsername% ^| FIND "Local Group Memberships"') DO SET "elevResult=%%A" > NUL 2>&1
ECHO "%elevResult%" | FINDSTR "Administrators" > NUL 2>&1
	IF %ERRORLEVEL% LEQ 0 SET "userStatus=Elevated"
	IF %ERRORLEVEL% GTR 0 SET "userStatus=Not Elevated"
	IF /I "%currentUsername%"=="" SET "currentUsername=RestartRequired"
EXIT /B 0
REM -----------------------------------------------------------
