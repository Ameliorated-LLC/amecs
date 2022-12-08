@ECHO OFF
GOTO SCRIPT-START
REM --------------------------DATABASE--------------------------
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
::1  |US& GOTO KBLANG-SUBLANGS| |CHI& GOTO KBLANG-SUBLANGS| |0439:00010439| |SPA& GOTO KBLANG-SUBLANGS| |040c:0000040c|
::2  |ARA& GOTO KBLANG-SUBLANGS| |RUS& GOTO KBLANG-SUBLANGS| |BAN& GOTO KBLANG-SUBLANGS| |POR& GOTO KBLANG-SUBLANGS| |041c:0000041c|
::3  |045e:{E429B25A-E5D3-4D1F-9BE3-0C608477E3A1}{8F96574E-C86C-4bd6-9666-3F7327D4CBE8}| |ARM& GOTO KBLANG-SUBLANGS| |044d:0000044d| |AZ& GOTO KBLANG-SUBLANGS| |046d:0000046d|
::4  |0423:00000423| |BEL& GOTO KBLANG-SUBLANGS| |141a:00000201a| |0421:000b0c00| |BUL& GOTO KBLANG-SUBLANGS|
::5  |CAN& GOTO KBLANG-SUBLANGS| |085f:0000085f| |0429:00000429| |CHE& GOTO KBLANG-SUBLANGS| |041a:0000041a|
::6  |CZE& GOTO KBLANG-SUBLANGS| |0406:00000406| |DIV& GOTO KBLANG-SUBLANGS| |0413:00000413| |0C51:00000C51|
::7  |0425:00000425| |0438:00000438| |FIN& GOTO KBLANG-SUBLANGS| |0407:00120c00| |GEO& GOTO KBLANG-SUBLANGS|
::8  |GER& GOTO KBLANG-SUBLANGS| |0407:000c0c00| |GRE& GOTO KBLANG-SUBLANGS| |046f:0000046f| |0474:00000474|
::9  |0447:00000447| |0468:00000468| |040d:0000040d| |HUN& GOTO KBLANG-SUBLANGS| |040f:0000040f|
::10 |0470:00000470| |4009:00004009| |INU& GOTO KBLANG-SUBLANGS| |083C:000001809| |ITA& GOTO KBLANG-SUBLANGS|
::11 |0411:{03B5835F-F03C-411B-9CE2-AA23E1171E36}{A76C93D9-5523-4E90-AAFA-4DB112F9AC76}| |0421:00110c00| |044b:0000044b| |043f:0000043f| |KHM& GOTO KBLANG-SUBLANGS|
::12 |0457:00000439| |KOR& GOTO KBLANG-SUBLANGS| |0440:00000440| |0454:00000454| |080a:0000080a|
::13 |LAT& GOTO KBLANG-SUBLANGS| |LIS& GOTO KBLANG-SUBLANGS| |LIT& GOTO KBLANG-SUBLANGS| |046e:0000046e| |MAC& GOTO KBLANG-SUBLANGS|
::14 |044c:0000044c| |MAL& GOTO KBLANG-SUBLANGS| |0481:00000481| |044e:0000044e| |MON& GOTO KBLANG-SUBLANGS|
::15 |0455:00010c00| |0409:000090c00| |0461:00000461| |0409:00020c00| |NOR& GOTO KBLANG-SUBLANGS|
::16 |0448:00000448| |0409:d0c00| |0409:000f0c00| |0409:000e0c00| |0463:00000463|
::17 |PER& GOTO KBLANG-SUBLANGS| |0409:000a0c00| |POL& GOTO KBLANG-SUBLANGS| |0446:00000446| |ROM& GOTO KBLANG-SUBLANGS|
::18 |0485:00000485| |SAM& GOTO KBLANG-SUBLANGS| |0809:00011809| |SER& GOTO KBLANG-SUBLANGS| |046c:0000046c|
::19 |0432:00000432| |SIN& GOTO KBLANG-SUBLANGS| |SLO& GOTO KBLANG-SUBLANGS| |0424:00000424| |0409:00100c00|
::20 |SOR& GOTO KBLANG-SUBLANGS| |SWE& GOTO KBLANG-SUBLANGS| |SWI& GOTO KBLANG-SUBLANGS| |SYR& GOTO KBLANG-SUBLANGS| |0409:00030c00|
::21 |0428:00000428| |TAM& GOTO KBLANG-SUBLANGS| |TAT& GOTO KBLANG-SUBLANGS| |044a:0000044a| |THA& GOTO KBLANG-SUBLANGS|
::22 |TIB& GOTO KBLANG-SUBLANGS| |TIF& GOTO KBLANG-SUBLANGS| |0473:{E429B25A-E5D3-4D1F-9BE3-0C608477E3A1}{3CAB88B7-CC3E-46A6-9765-B772AD7761FF}| |TUR& GOTO KBLANG-SUBLANGS| |0442:00000442|
::23 |UYG& GOTO KBLANG-SUBLANGS| |UKR& GOTO KBLANG-SUBLANGS| |UK& GOTO KBLANG-SUBLANGS| |0420:00000420| |0843:00000843|
::24 |VIE& GOTO KBLANG-SUBLANGS| |0488:00000488| |0485:00000485| |046a:0000056a|






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
:: |BUL| |0402:00030402| |0402:00010402| |0402:00020402| |0402:00040402| |0402:00000402|
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
REM ------------------------DATABASE-END--------------------------



REM ---------------------------START----------------------------
:SCRIPT-START

IF NOT "%~1"=="-debug" (
	MODE 80, 26
)
COLOR 70
TITLE Central AME Script
SET "ver=v1.3"
IF "%~1"=="permsCheck" EXIT 0

REM Allows for more flexibility with these two variables
SET "dirPath=%~dp0"
SET "scriptPath=%~f0"
FOR /F %%A IN ('"prompt $H &echo on &for %%B in (1) do rem"') DO SET BS=%%A
SET /A "auxWaitCount=0"

CALL :AUX-GETUSERNAME
IF /I "%~1"=="LangSet" GOTO DISPLANG-USERCHECK
IF /I "%~1"=="kbLangSet" GOTO KBLANG-PRESET
IF /I "%~1"=="kbLangRem" GOTO KBLANG-REMOVELANG
IF /I "%~1"=="wslInstall" GOTO ALTCHILD-WSL-DISTROINSTALL
IF /I "%~1"=="wslRemove" GOTO ALTCHILD-WSL-DISTROREMOVE

:SCRIPT-ADMINCHECK1

POWERSHELL -NoP -C "[Console]::CursorVisible = $False"

CALL :AUX-GENRND "7"
FOR /F "usebackq tokens=1 delims= " %%A IN (`WMIC process where "name='cmd.exe' and CommandLine like '%%%rndOut%%%'" get ParentProcessId 2^>^&1 ^| FINDSTR "1 2 3 4 5 6 7 8 9 0"`) DO SET "scriptPID=%%A"
	IF "%scriptPID%"=="" (
		CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^|
		POWERSHELL -NoP -C "Write-Host """`n`n`n                       Failed to fetch script process ID.`n           __________________________________________________________`n`n           Press any key to Exit: """ -NoNewLine; [Console]::CursorVisible = $True; $NULL = $Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown')"
		EXIT /B 0
	)

FOR /F "usebackq tokens=1 delims= " %%A IN (`WMIC useraccount where "name='%currentUsername:'=\'%'" get sid 2^>^&1 ^| FINDSTR "S-"`) DO SET "userSID=%%A"

IF /I NOT "%username%"=="%currentUsername%" (
	CALL :AUX-GETUSERENV
	SET "altRun=true"
) ELSE (
	SET "userTemp=%TEMP%"
	SET "userLocalAppData=%LOCALAPPDATA%"
	SET "userLocalAppData=%APPDATA%"
)

CALL :AUX-ELEVATIONCHECK
NET SESSION > NUL 2>&1
	IF %ERRORLEVEL% GTR 0 GOTO SCRIPT-ADMINCHECK2

IF /I "%~1"=="wslUnattend" SET "wslDistro=%~2" & SET "wslGroups=%~3" & SET "wslUnattendRun=true" & GOTO WSL-DISTROINSTALL

IF "%userRestart%"=="true" (
	CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^|
	POWERSHELL -NoP -C "Write-Host """`n`n`n                   Running this script after a username change`n                     may cause serious damage! Run anyways?`n                                    [Y]   [N]`n           __________________________________________________________`n`n           Choose an option: """ -NoNewLine; [Console]::CursorVisible = $True; CHOICE /C YN /N /M '%BS%'; [Console]::CursorVisible = $False; EXIT $LastExitCode"
		IF ERRORLEVEL 2 EXIT /B 0
)

WMIC process where "name='cmd.exe' and ProcessId!='%scriptPID%' or name='WindowsTerminal.exe' and ProcessId!='%scriptPID%'" get name 2>&1 | FINDSTR /i /c:"cmd.exe" /c:"WindowsTerminal.exe" > NUL 2>&1
	IF %ERRORLEVEL% EQU 1 (
		DEL /Q /F "%TEMP%\[amecs]*" > NUL 2>&1
		DEL /Q /F "%userTemp%\[amecs]*" > NUL 2>&1
	)

GOTO HOME-MAINMENU

:SCRIPT-ADMINCHECK2

IF /I "%~1"=="wslUnattend" SET "wslDistro=%~2" & SET "wslGroups=%~3" & SET "wslUnattendRun=true" & SET "adminPrivs=false" & GOTO WSL-DISTROINSTALL

POWERSHELL -NoP -C "Start-Process '%scriptPath:'=''%' -Verb RunAs" > NUL 2>&1
IF %ERRORLEVEL% GTR 0 (
	IF "%altRun%"=="true" (
		CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^|
		POWERSHELL -NoP -C "Write-Host """`n`n`n                     Script must be run as the current user`n                        or with administrator privilages.`n           __________________________________________________________`n`n           Press any key to Exit: """ -NoNewLine; [Console]::CursorVisible = $True; $NULL = $Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown')"
		EXIT /B 0
	)
	CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^|
	POWERSHELL -NoP -C "Write-Host """`n`n`n               Elevation canceled, run with limited functionality?`n                                    [Y]   [N]`n           __________________________________________________________`n`n           Choose an option: """ -NoNewLine; [Console]::CursorVisible = $True; CHOICE /C YN /N /M '%BS%'; [Console]::CursorVisible = $False; EXIT $LastExitCode"
		IF ERRORLEVEL 2 (
			EXIT /B 0
		) ELSE (
			SET "adminPrivs=false"
			TASKLIST /FI "WINDOWTITLE eq Central AME Script" /FI "PID ne %scriptPID%" 2>&1 | FINDSTR /i /c:".exe">NUL 2>&1 || TASKLIST /FI "WINDOWTITLE eq Administrator:  Central AME Script" /FI "PID ne %scriptPID%" 2>&1 | FINDSTR /i /c:".exe" > NUL 2>&1
				IF ERRORLEVEL 1 DEL /Q /F "%TEMP%\[amecs]*" > NUL 2>&1
			GOTO HOME-MAINMENU
		)
)

EXIT /B 0
REM -------------------------START-END--------------------------


					REM ------------
					REM Menu Section
					REM ------------


REM ----------------------------MENU----------------------------
:HOME-MAINMENU

IF "%adminPrivs%"=="false" GOTO HOME-LIMMAINMENU

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO. & ECHO.

CALL :MO-CHOICE -InitChoices "1234567EX" "+GOTO USERPASS-MENU+GOTO LOCKSCREEN-GRABIMAGE+GOTO PFP-GRABIMAGE+GOTO HOME-LANGUAGE+GOTO !homeElevLoc!+GOTO !homeNULoc!+GOTO !homeALLoc!+GOTO HOME-EXTRA+EXIT /B 0+"
CALL :MO-MAINMENU
CALL :MO-CHOICE -StartChoices "$(' '.padleft(17, ' '))[1] Change Username or Password`n$(' '.padleft(17, ' '))[2] Change Lockscreen Image`n$(' '.padleft(17, ' '))[3] Change Profile Image`n$(' '.padleft(17, ' '))[4] Manage Language Settings`n$(' '.padleft(17, ' '))[5] %homeElevMsg%`n$(' '.padleft(17, ' '))[6] %homeNUMsg%`n$(' '.padleft(17, ' '))[7] %homeALMsg%`n`n$(' '.padleft(17, ' '))[E] Extra`n$(' '.padleft(17, ' '))[X] Exit`n"

:HOME-EXTRA

IF "%adminPrivs%"=="false" GOTO HOME-LIMEXTRA

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO. & ECHO.

CALL :MO-CHOICE -InitChoices "1234567890X" "+GOTO HOME-WSL+GOTO !homeHIBLoc!+GOTO !homeNOTIFCENLoc!+GOTO !homeNOTIFLoc!+GOTO !homeWSHLoc!+GOTO !homeVBSLoc!+GOTO !homeNCSILoc!+CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            | Central AME Script %ver% | & POWERSHELL -NoP -C ""Write-Host """"""""`n`n`n$(' '.padleft(13, ' '))WARNING: This is a beta feature, use at your own risk.`n$(' '.padleft(11, ' '))__________________________________________________________`n`n$(' '.padleft(11, ' '))Press any key to continue: """""""" -NoNewLine; [Console]::CursorVisible = $True; $NULL = $Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown')"" & GOTO NEWUSER-MENU+GOTO !homeNVCPLoc!+GOTO HOME-MAINMENU+EXIT /B 0+"
CALL :MO-EXTRA
CALL :MO-CHOICE -StartChoices "$(' '.padleft(17, ' '))[1] Manage WSL%homeExtWSLMsg%`n$(' '.padleft(17, ' '))[2] %homeHIBMsg%`n$(' '.padleft(17, ' '))[3] %homeNOTIFCENMsg%`n$(' '.padleft(17, ' '))[4] %homeNOTIFMsg%`n$(' '.padleft(17, ' '))[5] %homeWSHMsg%`n$(' '.padleft(17, ' '))[6] %homeVBSMsg%`n$(' '.padleft(17, ' '))[7] %homeNCSIMsg%`n$(' '.padleft(17, ' '))[8] Create New User (Beta)""""; %homeNVCPMsg%; Write-Host """"`n$(' '.padleft(17, ' '))[0] Return to Menu`n$(' '.padleft(17, ' '))[X] Exit`n"

:HOME-LANGUAGE

IF "%adminPrivs%"=="false" GOTO HOME-LIMLANGUAGE

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO. & ECHO. 

CALL :MO-CHOICE -InitChoices "123450X" "+SET ""lpStatus="" & GOTO DISPLANG-MENUP1+SET ""kbLangLoc=:COMM:& GOTO KBLANG-PRESET"" & GOTO KBLANG-LANGS+SET ""kbLangLoc=:COMM:& GOTO KBLANG-REMOVELANG"" & GOTO KBLANG-LANGS+SET ""lpStatus=added"" & GOTO DISPLANG-MENUP1+SET ""lpStatus=removed"" & GOTO DISPLANG-MENUP1+GOTO HOME-MAINMENU+EXIT /B 0+"
CALL :MO-CHOICE -StartChoices "$(' '.padleft(17, ' '))[1] Change Display Language`n$(' '.padleft(17, ' '))[2] Add Keyboard Language`n$(' '.padleft(17, ' '))[3] Remove Keyboard Language`n$(' '.padleft(17, ' '))[4] Install Language Pack`n$(' '.padleft(17, ' '))[5] Uninstall Language Pack`n`n$(' '.padleft(17, ' '))[0] Return to Menu`n$(' '.padleft(17, ' '))[X] Exit`n"

:HOME-WSL

IF "%adminPrivs%"=="false" GOTO HOME-LIMWSL

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO. & ECHO. 

CALL :MO-CHOICE -InitChoices "123U0X" "+GOTO !homeWSLLoc!+SET ""wslMenuLoc=WSL-DISTROINSTALL"" & GOTO WSL-DISTROMENUP1+SET ""wslMenuLoc=WSL-DISTROREMOVE"" & GOTO WSL-DISTROMENUP1+POWERSHELL -NoP -C ""[Console]::SetCursorPosition(17,10); Write-Host '[U] Unattended Distro Install (Enabled) ' -NoNewLine -ForegroundColor Green; [Console]::SetCursorPosition(17,7); Write-Host '[2] Install WSL Distro' -NoNewLine; [Console]::SetCursorPosition(0,!homeWSLChPos!); Write-Host '           Choose a menu option:  ' -NoNewLine; [Console]::SetCursorPosition(33,!homeWSLChPos!)"" & SET ""wslUnattend=true"" & GOTO INTERNAL-HOME_WSL-MARKER+GOTO HOME-MAINMENU+EXIT /B 0+"
CALL :MO-WSL
CALL :MO-CHOICE -StartChoices "$(' '.padleft(17, ' '))[1] %homeWSLMsg%%homeWSLDistroMsgs%`n$(' '.padleft(17, ' '))[0] Return to Menu`n$(' '.padleft(17, ' '))[X] Exit%homeWSLStatus%"

:INTERNAL-HOME_WSL-MARKER

IF "%wslUnattend%"=="true" (
	POWERSHELL -NoP -C "[Console]::CursorVisible = $True; CHOICE /C 12U0X /N /M '%BS%'; [Console]::CursorVisible = $False; EXIT $LastExitCode"
		IF ERRORLEVEL 5 EXIT /B 0
		IF ERRORLEVEL 4 GOTO HOME-MAINMENU
		IF ERRORLEVEL 3 POWERSHELL -NoP -C "[Console]::SetCursorPosition(17,10); Write-Host '[U] Unattended Distro Install (Disabled)' -NoNewLine; [Console]::SetCursorPosition(17,7); Write-Host '[2] Install WSL Distro' -ForegroundColor DarkGray -NoNewLine; [Console]::SetCursorPosition(0,%homeWSLChPos%); Write-Host '           Choose a menu option:  ' -NoNewLine; [Console]::SetCursorPosition(33,%homeWSLChPos%)" & SET "wslUnattend=false" & GOTO INTERNAL-HOME_WSL-MARKER
		IF ERRORLEVEL 2 SET "wslMenuLoc=WSL-DISTROINSTALL" & GOTO WSL-DISTROMENUP1
		IF ERRORLEVEL 1 GOTO %homeWSLLoc%
)

IF NOT "%wslUnattend%"=="" POWERSHELL -NoP -C "[Console]::CursorVisible = $True; CHOICE /C 1U0X /N /M '%BS%'; [Console]::CursorVisible = $False; EXIT $LastExitCode"
	IF ERRORLEVEL 4 EXIT /B 0
	IF ERRORLEVEL 3 GOTO HOME-MAINMENU
	IF ERRORLEVEL 2 POWERSHELL -NoP -C "[Console]::SetCursorPosition(17,10); Write-Host '[U] Unattended Distro Install (Enabled) ' -NoNewLine -ForegroundColor Green; [Console]::SetCursorPosition(17,7); Write-Host '[2] Install WSL Distro' -NoNewLine; [Console]::SetCursorPosition(0,%homeWSLChPos%); Write-Host '           Choose a menu option:  ' -NoNewLine; [Console]::SetCursorPosition(33,%homeWSLChPos%)" & SET "wslUnattend=true" & GOTO INTERNAL-HOME_WSL-MARKER
	IF ERRORLEVEL 1 GOTO %homeWSLLoc%

:HOME-LIMMAINMENU

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO. & ECHO.

CALL :MO-CHOICE -InitChoices "4EX" "+GOTO HOME-LIMLANGUAGE+GOTO HOME-EXTRA+EXIT /B 0+"
CALL :MO-LIMMAINMENU
CALL :MO-CHOICE -StartChoices "$(' '.padleft(17, ' '))[1] Change Username or Password"""" -NoNewLine -ForegroundColor DarkGray; Write-Host ' [Admin Required]' -ForegroundColor Red; Write-Host """"$(' '.padleft(17, ' '))[2] Change Lockscreen Image"""" -NoNewLine -ForegroundColor DarkGray; Write-Host ' [Admin Required]' -ForegroundColor Red; Write-Host """"$(' '.padleft(17, ' '))[3] Change Profile Image"""" -NoNewLine -ForegroundColor DarkGray; Write-Host ' [Admin Required]' -ForegroundColor Red; Write-Host """"$(' '.padleft(17, ' '))[4] Manage Language Settings""""; Write-Host """"$(' '.padleft(17, ' '))[5] %homeElevMsg%"""" -NoNewLine -ForegroundColor DarkGray; Write-Host ' [Admin Required]' -ForegroundColor Red; Write-Host """"$(' '.padleft(17, ' '))[6] %homeNUMsg%"""" -NoNewLine -ForegroundColor DarkGray; Write-Host ' [Admin Required]' -ForegroundColor Red; Write-Host """"$(' '.padleft(17, ' '))[7] %homeALMsg%"""" -NoNewLine -ForegroundColor DarkGray; Write-Host ' [Admin Required]' -ForegroundColor Red; Write-Host """"`n$(' '.padleft(17, ' '))[E] Extra`n$(' '.padleft(17, ' '))[X] Exit`n"

:HOME-LIMEXTRA

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO. & ECHO.

CALL :MO-CHOICE -InitChoices "140X" "+GOTO HOME-LIMWSL+GOTO !homeNOTIFLoc!+GOTO HOME-MAINMENU+EXIT /B 0+"
CALL :MO-LIMEXTRA
CALL :MO-CHOICE -StartChoices "$(' '.padleft(17, ' '))[1] Manage WSL""""%homeExtWSLMsg%; Write-Host """"$(' '.padleft(17, ' '))[2] %homeHIBMsg%"""" -NoNewLine -ForegroundColor DarkGray; Write-Host ' [Admin Required]' -ForegroundColor Red; Write-Host """"$(' '.padleft(17, ' '))[3] %%homeNOTIFCENMsg%%"""" -NoNewLine -ForegroundColor DarkGray; Write-Host ' [Admin Required]' -ForegroundColor Red; Write-Host """"$(' '.padleft(17, ' '))[4] %%homeNOTIFMsg%%""""; Write-Host """"$(' '.padleft(17, ' '))[5] %homeWSHMsg%"""" -NoNewLine -ForegroundColor DarkGray; Write-Host ' [Admin Required]' -ForegroundColor Red; Write-Host """"$(' '.padleft(17, ' '))[6] %homeVBSMsg%"""" -NoNewLine -ForegroundColor DarkGray; Write-Host ' [Admin Required]' -ForegroundColor Red; Write-Host """"$(' '.padleft(17, ' '))[7] %homeNCSIMsg%"""" -NoNewLine -ForegroundColor DarkGray; Write-Host ' [Admin Required]' -ForegroundColor Red; Write-Host """"$(' '.padleft(17, ' '))[8] Create New User (Beta)"""" -NoNewLine -ForegroundColor DarkGray; Write-Host ' [Admin Required]' -ForegroundColor Red; %homeNVCPMsg%; Write-Host """"`n$(' '.padleft(17, ' '))[0] Return to Menu`n$(' '.padleft(17, ' '))[X] Exit`n"

:HOME-LIMLANGUAGE

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO. & ECHO.

CALL :MO-CHOICE -InitChoices "230X" "+SET ""kbLangLoc=:COMM:& GOTO KBLANG-PRESET"" & GOTO KBLANG-LANGS+SET ""kbLangLoc=:COMM:& GOTO KBLANG-REMOVELANG"" & GOTO KBLANG-LANGS+GOTO HOME-MAINMENU+EXIT /B 0+"
CALL :MO-CHOICE -StartChoices "$(' '.padleft(17, ' '))[1] Change Display Language"""" -NoNewLine -ForegroundColor DarkGray; Write-Host ' [Admin Required]' -ForegroundColor Red; Write-Host """"$(' '.padleft(17, ' '))[2] Add Keyboard Language`n$(' '.padleft(17, ' '))[3] Remove Keyboard Language""""; Write-Host """"$(' '.padleft(17, ' '))[4] Install Language Pack"""" -NoNewLine -ForegroundColor DarkGray; Write-Host ' [Admin Required]' -ForegroundColor Red; Write-Host """"$(' '.padleft(17, ' '))[5] Uninstall Language Pack"""" -NoNewLine -ForegroundColor DarkGray; Write-Host ' [Admin Required]' -ForegroundColor Red; Write-Host """"`n$(' '.padleft(17, ' '))[0] Return to Menu`n$(' '.padleft(17, ' '))[X] Exit`n"

:HOME-LIMWSL

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO. & ECHO. 

CALL :MO-CHOICE -InitChoices "230X" "+SET ""wslMenuLoc=WSL-DISTROINSTALL"" & GOTO WSL-DISTROMENUP1+SET ""wslMenuLoc=WSL-DISTROREMOVE"" & GOTO WSL-DISTROMENUP1+GOTO HOME-MAINMENU+EXIT /B 0+"
CALL :MO-LIMWSL
CALL :MO-CHOICE -StartChoices "$(' '.padleft(17, ' '))[1] %homeWSLMsg%"""" -NoNewLine -ForegroundColor DarkGray; Write-Host ' [Admin Required]' -ForegroundColor Red; Write-Host """"%homeWSLDistroMsgs%`n$(' '.padleft(17, ' '))[0] Return to Menu`n$(' '.padleft(17, ' '))[X] Exit`n"
REM --------------------------MENU-END--------------------------


                    REM -----------------
                    REM Primary Functions
                    REM -----------------


REM --------------------------USERPASS--------------------------
:USERPASS-MENU	

SETLOCAL

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO. & ECHO.

POWERSHELL -NoP -C "Write-Host """                 [1] Change Username`n                 [2] Change Password`n                 [3] Change Administrator Password`n`n                 [0] Return to Menu`n                 [X] Exit`n`n           __________________________________________________________`n`n           Choose a menu option: """ -NoNewLine; [Console]::CursorVisible = $True; CHOICE /C 1230X /N /M '%BS%'; [Console]::CursorVisible = $False; EXIT $LastExitCode"
	IF %ERRORLEVEL%==1 GOTO USERPASS-USERNAME
	IF %ERRORLEVEL%==2 GOTO USERPASS-PASSWORD
	IF %ERRORLEVEL%==3 GOTO USERPASS-ADMINPASSWORD
	IF %ERRORLEVEL%==4 ENDLOCAL & GOTO HOME-MAINMENU
	IF %ERRORLEVEL%==5 EXIT /B 0

:USERPASS-USERNAME

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO.

CALL :AUX-INPUTLOOP "newUsername" "Enter new username, or 'Cancel' to quit" "0" "5"
	IF %ERRORLEVEL% EQU 3 ENDLOCAL & GOTO HOME-MAINMENU

ENDLOCAL & SET "currentUsername=%newUsername%"
CALL :AUX-RETURN "Username changed successfully" -HNR R:L.sign-out -L "A sign-out is recommended."

:USERPASS-PASSWORD

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO.

SET "C4Username=%currentUsername%"
CALL :AUX-INPUTLOOP "newPassword" "Enter new password, or 'Cancel' to quit" "0" "4" -Secure
	IF %ERRORLEVEL% EQU 3 ENDLOCAL & GOTO HOME-MAINMENU

CALL :AUX-RETURN "Password changed successfully" -H

:USERPASS-ADMINPASSWORD

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO.

SET "C4Username=Administrator"
CALL :AUX-INPUTLOOP "newPassword" "Enter new Administrator password, or 'Cancel' to quit" "0" "4" -Secure
	IF %ERRORLEVEL% EQU 3 ENDLOCAL & GOTO HOME-MAINMENU

CALL :AUX-RETURN "Administrator password changed successfully" -H
REM ------------------------USERPASS-END------------------------



REM -------------------------LOCKSCREEN-------------------------
:LOCKSCREEN-GRABIMAGE

SETLOCAL
REM Original Author & Co-Author: Logan Darklock, lucid
CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO.

ECHO. & ECHO                                 Select your image

DIR /B "%SYSTEMDRIVE%\Users" | FINDSTR /x "%possibleUserDir%" > NUL 2>&1
	IF %ERRORLEVEL% LEQ 0 SET "UserPath=\%possibleUserDir%"

FOR /F "usebackq delims=" %%I in (`POWERSHELL -NoP -C "Start-Sleep -Milliseconds 200; [System.Reflection.Assembly]::LoadWithPartialName('System.windows.forms')|Out-Null;$OFD = New-Object System.Windows.Forms.OpenFileDialog;$OFD.Multiselect = $False;$OFD.Filter = 'Image Files (*.jpg; *.jpeg; *.png; *.bmp; *.jfif)| *.jpg; *.jpeg; *.png; *.bmp; *.jfif';$OFD.InitialDirectory = '%SYSTEMDRIVE%\Users%UserPath%';$OFD.ShowDialog()|out-null;$OFD.FileNames"`) DO SET "lockImgPath=%%~I"
	IF "%lockImgPath%"=="" CALL :AUX-RETURN "You must select an image." -H -E

POWERSHELL -NoP -C "Write-Host """`n           Remove lockscreen blur? (Y/N): """ -NoNewLine; [Console]::CursorVisible = $True; CHOICE /C YN /N /M '%BS%'; [Console]::CursorVisible = $False; EXIT $LastExitCode"
	IF %ERRORLEVEL%==1 REG ADD "HKLM\SOFTWARE\Policies\Microsoft\Windows\System" /v DisableAcrylicBackgroundOnLogon /t REG_DWORD /d 1 /f > NUL
	IF %ERRORLEVEL%==2 REG DELETE "HKLM\SOFTWARE\Policies\Microsoft\Windows" /v DisableAcrylicBackgroundOnLogon /f > NUL 2>&1

:LOCKSCREEN-DEPLOY

ECHO. & ECHO                            Setting lockscreen image...

TIMEOUT /T 1 /NOBREAK > NUL
REM Necessary for updated 21H2+ versions if RotatingLockScreenEnabled is not already set to 0
REG ADD "HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Authentication\LogonUI\Creative\%userSID%" /v "RotatingLockScreenEnabled" /t REG_DWORD /d 0 /f > NUL
REG ADD "HKU\%userSID%\SOFTWARE\Microsoft\Windows\CurrentVersion\ContentDeliveryManager" /v "RotatingLockScreenEnabled" /t REG_DWORD /d 0 /f > NUL

REM Copy wallpaper to the right spot
TAKEOWN /F "%WINDIR%\Web\Screen\img100.jpg">NUL 2>&1 & TAKEOWN /F "%WINDIR%\Web\Screen\img103.png">NUL 2>&1 & TAKEOWN /F "%WINDIR%\Web\Wallpaper\Windows\img0.jpg" > NUL 2>&1
ICACLS "%WINDIR%\Web\Screen\img100.jpg" /reSET>NUL & ICACLS "%WINDIR%\Web\Screen\img103.png" /reSET>NUL & ICACLS "%WINDIR%\Web\Wallpaper\Windows\img0.jpg" /reSET > NUL
COPY "%lockImgPath%" "%WINDIR%\Web\Screen\img100.jpg" /y>NUL & COPY "%lockImgPath%" "%WINDIR%\Web\Screen\img103.png" /y>NUL & COPY "%lockImgPath%" "%WINDIR%\Web\Wallpaper\Windows\img0.jpg" /y > NUL
REM Clear cache
TAKEOWN /R /D Y /F "%PROGRAMDATA%\Microsoft\Windows\SystemData" > NUL
ICACLS "%PROGRAMDATA%\Microsoft\Windows\SystemData" /reSET /t > NUL
FOR /D %%x in ("%PROGRAMDATA%\Microsoft\Windows\SystemData\*") do (
FOR /D %%y in ("%%x\ReadOnly\LockScreen_*") do rd /s /q "%%y" )

CALL :AUX-RETURN "Lockscreen image changed successfully" -H
REM -----------------------LOCKSCREEN-END-----------------------



REM ----------------------------PFP-----------------------------
:PFP-GRABIMAGE

SETLOCAL
REM Original Author & Co-Author: Logan Darklock, lucid
CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO.

ECHO. & ECHO                                 Select your image

REM Used for default starting directory for file selection window
DIR /B "%SYSTEMDRIVE%\Users" | FINDSTR /x "%possibleUserDir%" > NUL 2>&1
	IF %ERRORLEVEL% LEQ 0 SET "UserPath=\%possibleUserDir%"

FOR /F "usebackq delims=" %%I in (`POWERSHELL -NoP -C "Start-Sleep -Milliseconds 200; [System.Reflection.Assembly]::LoadWithPartialName('System.windows.forms')|Out-Null;$OFD = New-Object System.Windows.Forms.OpenFileDialog;$OFD.Multiselect = $False;$OFD.Filter = 'Image Files (*.jpg; *.jpeg; *.png; *.bmp; *.jfif)| *.jpg; *.jpeg; *.png; *.bmp; *.jfif';$OFD.InitialDirectory = '%SYSTEMDRIVE%\Users%UserPath%';$OFD.ShowDialog()|out-null;$OFD.FileNames"`) DO SET "pfpImgPath=%%~I"
	IF "%pfpImgPath%"=="" CALL :AUX-RETURN "You must select an image." -H -E

:PFP-DEPLOY

ECHO. & ECHO                             Setting profile image...

REM On recent Windows 10 versions, resolutions called for are:
REM 32x32, 40x40, 48x48, 64x64, 96x96, 192x192, 208x208, 240x240, 424x424,
REM 448x448, 1080x1080
SET "usrPfpDir=%PUBLIC%\AccountPictures\%userSID%"

MKDIR "%usrPfpDir%" > NUL 2>&1
TAKEOWN /r /d Y /f "%usrPfpDir%" > NUL
ICACLS "%usrPfpDir%" /reset /t > NUL
DEL /Q /F "%usrPfpDir%\*" > NUL

POWERSHELL -NoP -C "Add-Type -AssemblyName System.Drawing; $img = [System.Drawing.Image]::FromFile((Get-Item '%pfpImgPath%')); $a = New-Object System.Drawing.Bitmap(32, 32); $graph = [System.Drawing.Graphics]::FromImage($a); $graph.DrawImage($img, 0, 0, 32, 32); $a.Save('%usrPfpDir%\32x32.png'); $b = New-Object System.Drawing.Bitmap(40, 40); $graph = [System.Drawing.Graphics]::FromImage($b); $graph.DrawImage($img, 0, 0, 40, 40); $b.Save('%usrPfpDir%\40x40.png'); $c = New-Object System.Drawing.Bitmap(48, 48); $graph = [System.Drawing.Graphics]::FromImage($c); $graph.DrawImage($img, 0, 0, 48, 48); $c.Save('%usrPfpDir%\48x48.png'); $d = New-Object System.Drawing.Bitmap(64, 64); $graph = [System.Drawing.Graphics]::FromImage($d); $graph.DrawImage($img, 0, 0, 64, 64); $d.Save('%usrPfpDir%\64x64.png'); $e = New-Object System.Drawing.Bitmap(96, 96); $graph = [System.Drawing.Graphics]::FromImage($e); $graph.DrawImage($img, 0, 0, 96, 96); $e.Save('%usrPfpDir%\96x96.png'); $f = New-Object System.Drawing.Bitmap(192, 192); $graph = [System.Drawing.Graphics]::FromImage($f); $graph.DrawImage($img, 0, 0, 192, 192); $f.Save('%usrPfpDir%\192x192.png'); $g = New-Object System.Drawing.Bitmap(208, 208); $graph = [System.Drawing.Graphics]::FromImage($g); $graph.DrawImage($img, 0, 0, 208, 208); $g.Save('%usrPfpDir%\208x208.png'); $h = New-Object System.Drawing.Bitmap(240, 240); $graph = [System.Drawing.Graphics]::FromImage($h); $graph.DrawImage($img, 0, 0, 240, 240); $h.Save('%usrPfpDir%\240x240.png'); $i = New-Object System.Drawing.Bitmap(424, 424); $graph = [System.Drawing.Graphics]::FromImage($i); $graph.DrawImage($img, 0, 0, 424, 424); $i.Save('%usrPfpDir%\424x424.png'); $j = New-Object System.Drawing.Bitmap(448, 448); $graph = [System.Drawing.Graphics]::FromImage($j); $graph.DrawImage($img, 0, 0, 448, 448); $j.Save('%usrPfpDir%\448x448.png'); $k = New-Object System.Drawing.Bitmap(1080, 1080); $graph = [System.Drawing.Graphics]::FromImage($k); $graph.DrawImage($img, 0, 0, 1080, 1080); $k.Save('%usrPfpDir%\1080x1080.png')"

SET "usrPfpRegKey=HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\AccountPicture\Users\%userSID%"

REM Runs the reg delete command as SYSTEM
SCHTASKS /create /tn "[amecs]-PFPREG" /tr "CMD /C 'REG DELETE '%usrPfpRegKey%' /f'" /sc MONTHLY /f /rl HIGHEST /ru "SYSTEM" > NUL
	IF %ERRORLEVEL% NEQ 0 SCHTASKS /DELETE /TN "[amecs]-PFPREG" /F>NUL 2>&1 & CALL :AUX-RETURN "Failed to create scheduled task. (1)" -H -E
POWERSHELL -NoP -C "$TaskSet = New-ScheduledTaskSettingsSet -AllowStartIfOnBatteries -DontStopIfGoingOnBatteries; Set-ScheduledTask -TaskName '[amecs]-PFPREG' -Settings $TaskSet" > NUL 2>&1
	IF %ERRORLEVEL% NEQ 0 SCHTASKS /DELETE /TN "[amecs]-PFPREG" /F>NUL 2>&1 & CALL :AUX-RETURN "Failed to create scheduled task. (2)" -H -E

SCHTASKS /run /tn "[amecs]-PFPREG" > NUL
SCHTASKS /delete /tn "[amecs]-PFPREG" /f > NUL

REG ADD "%usrPfpRegKey%" /f > NUL
REG ADD "%usrPfpRegKey%" /v Image32 /t REG_SZ /d "%usrPfpDir%\32x32.png" /f > NUL
REG ADD "%usrPfpRegKey%" /v Image40 /t REG_SZ /d "%usrPfpDir%\40x40.png" /f > NUL
REG ADD "%usrPfpRegKey%" /v Image48 /t REG_SZ /d "%usrPfpDir%\48x48.png" /f > NUL
REG ADD "%usrPfpRegKey%" /v Image64 /t REG_SZ /d "%usrPfpDir%\64x64.png" /f > NUL
REG ADD "%usrPfpRegKey%" /v Image96 /t REG_SZ /d "%usrPfpDir%\96x96.png" /f > NUL
REG ADD "%usrPfpRegKey%" /v Image192 /t REG_SZ /d "%usrPfpDir%\192x192.png" /f > NUL
REG ADD "%usrPfpRegKey%" /v Image208 /t REG_SZ /d "%usrPfpDir%\208x208.png" /f > NUL
REG ADD "%usrPfpRegKey%" /v Image240 /t REG_SZ /d "%usrPfpDir%\240x240.png" /f > NUL
REG ADD "%usrPfpRegKey%" /v Image424 /t REG_SZ /d "%usrPfpDir%\424x424.png" /f > NUL
REG ADD "%usrPfpRegKey%" /v Image448 /t REG_SZ /d "%usrPfpDir%\448x448.png" /f > NUL
REG ADD "%usrPfpRegKey%" /v Image1080 /t REG_SZ /d "%usrPfpDir%\1080x1080.png" /f > NUL
REG ADD "HKU\%userSID%\SOFTWARE\OpenShell\StartMenu\Settings" /v UserPicturePath /t REG_SZ /d "%usrPfpDir%\448x448.png" /f > NUL 2>&1

GPUPDATE /force > NUL

CALL :AUX-RETURN "Profile image changed successfully" -H
REM --------------------------PFP-END---------------------------



REM -------------------------ELEVATION--------------------------
:ELEVATE-ELEVATE

SETLOCAL

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO.

ECHO. & ECHO                   Granting Admin rights to the current user...

IF "%userStatus%"=="Elevated" CALL :AUX-RETURN "The current user is already an Administrator." -H -E

TIMEOUT /T 2 /NOBREAK > NUL
NET localgroup administrators "%currentUsername%" /add > NUL 2>&1
	IF %ERRORLEVEL% GTR 0 CALL :AUX-RETURN "Failed to change user permissions" -H -L "A restart may fix this." -E
	IF %ERRORLEVEL% LEQ 0 ENDLOCAL & SET "userStatus=Elevated" & CALL :AUX-RETURN "The current user is now an Administrator" -HNR R:L.sign-out -L "A sign-out is required to take effect."

:ELEVATE-REVOKE

SETLOCAL

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO.

ECHO. & ECHO                  Revoking Admin rights from the current user...

IF "%userStatus%"=="Not Elevated" CALL :AUX-RETURN "The current user is not an Administrator." -H -E

TIMEOUT /T 2 /NOBREAK > NUL 2>&1
NET localgroup administrators "%currentUsername%" /delete > NUL 2>&1
	IF %ERRORLEVEL% GTR 0 CALL :AUX-RETURN "Failed to change user permissions" -H -L "A restart may fix this." -E
	IF %ERRORLEVEL% LEQ 0 ENDLOCAL & SET "userStatus=Not Elevated" & CALL :AUX-RETURN "Admin rights have been revoked for the current user" -HNR R:L.sign-out -L "A sign-out is required to take effect."
REM -----------------------ELEVATION-END------------------------



REM --------------------------DISPLANG--------------------------
:DISPLANG-MENUP1

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO. & ECHO.
POWERSHELL -NoP -C "Write-Host """                 [1] Arabic (ar-SA)`n                 [2] Bulgarian (bg-BG)`n                 [3] Chineese [Simplified] (zh-CN)`n                 [4] Chineese [Traditional] (zh-TW)`n                 [5] Croatian (hr-HR)`n                 [6] Czech (cs-CZ)`n                 [7] Danish (da-DK)`n                 [8] Dutch (nl-NL)`n                 [9] English [US] (en-US)`n`n                 [N] Next Page`n                 [0] Return to Menu`n                 [X] Exit`n                                    Page 1/6`n           __________________________________________________________`n`n           Choose a menu option: """ -NoNewLine; [Console]::CursorVisible = $True; CHOICE /C 123456789N0X /N /M  %BS%; [Console]::CursorVisible = $False; EXIT $LastExitCode"
	IF %ERRORLEVEL%==1 SET "langSel=ar-SA" & SET "dispDl=2480000" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==2 SET "langSel=bg-BG" & SET "dispDl=2480000" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==3 SET "langSel=zh-CN" & SET "dispDl=3230000" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==4 SET "langSel=zh-TW" & SET "dispDl=3230000" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==5 SET "langSel=hr-HR" & SET "dispDl=2900000" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==6 SET "langSel=cs-CZ" & SET "dispDl=2480000" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==7 SET "langSel=da-DK" & SET "dispDl=2480000" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==8 SET "langSel=nl-NL" & SET "dispDl=2900000" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==9 SET "langSel=en-US" & SET "dispDl=2480000" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==10 GOTO DISPLANG-MENUP2
	IF %ERRORLEVEL%==11 GOTO HOME-MAINMENU
	IF %ERRORLEVEL%==12 EXIT /B 0

:DISPLANG-MENUP2

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO. & ECHO.
POWERSHELL -NoP -C "Write-Host """                 [1] English [UK] (en-GB)`n                 [2] Estonian (et-EE)`n                 [3] Finnish (fi-FI)`n                 [4] French [Canada] (fr-CA)`n                 [5] French [France] (fr-FR)`n                 [6] German (de-DE)`n                 [7] Greek (el-GR)`n                 [8] Hebrew (he-IL)`n                 [9] Hungarian (hu-HU)`n`n                 [N] Next Page`n                 [P] Previous Page`n                 [0] Return to Menu`n                 [X] Exit`n                                    Page 2/5`n           __________________________________________________________`n`n           Choose a menu option: """ -NoNewLine; [Console]::CursorVisible = $True; CHOICE /C 123456789NP0X /N /M  %BS%; [Console]::CursorVisible = $False; EXIT $LastExitCode"
	IF %ERRORLEVEL%==1 SET "langSel=en-GB" & SET "dispDl=2480000" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==2 SET "langSel=et-EE" & SET "dispDl=2480000" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==3 SET "langSel=fi-FI" & SET "dispDl=2480000" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==4 SET "langSel=fr-CA" & SET "dispDl=2480000" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==5 SET "langSel=fr-FR" & SET "dispDl=2900000" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==6 SET "langSel=de-DE" & SET "dispDl=2480000" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==7 SET "langSel=el-GR" & SET "dispDl=2480000" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==8 SET "langSel=he-IL" & SET "dispDl=2900000" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==9 SET "langSel=hu-HU" & SET "dispDl=2900000" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==10 GOTO DISPLANG-MENUP3
	IF %ERRORLEVEL%==11 GOTO DISPLANG-MENUP1
	IF %ERRORLEVEL%==12 GOTO HOME-MAINMENU
	IF %ERRORLEVEL%==13 EXIT /B 0

:DISPLANG-MENUP3

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO. & ECHO.
POWERSHELL -NoP -C "Write-Host """                 [1] Italian (it-IT)`n                 [2] Japanese (ja-JP)`n                 [3] Korean (ko-KR)`n                 [4] Latvian (lv-LV)`n                 [5] Lithuanian (lt-LT)`n                 [6] Norwegian (nb-NO)`n                 [7] Polish (pl-PL)`n                 [8] Portugeese [Brazil] (pt-BR)`n                 [9] Portugeese [Portugal] (pt-PT)`n`n                 [N] Next Page`n                 [P] Previous Page`n                 [0] Return to Menu`n                 [X] Exit`n                                    Page 3/5`n           __________________________________________________________`n`n           Choose a menu option: """ -NoNewLine; [Console]::CursorVisible = $True; CHOICE /C 123456789NP0X /N /M  %BS%; [Console]::CursorVisible = $False; EXIT $LastExitCode"
	IF %ERRORLEVEL%==1 SET "langSel=it-IT" & SET "dispDl=2900000" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==2 SET "langSel=ja-JP" & SET "dispDl=2900000" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==3 SET "langSel=ko-KR" & SET "dispDl=2900000" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==4 SET "langSel=lv-LV" & SET "dispDl=2900000" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==5 SET "langSel=lt-LT" & SET "dispDl=2900000" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==6 SET "langSel=nb-NO" & SET "dispDl=2900000" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==7 SET "langSel=pl-PL" & SET "dispDl=3230000" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==8 SET "langSel=pt-BR" & SET "dispDl=3230000" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==9 SET "langSel=pt-PT" & SET "dispDl=3230000" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==10 GOTO DISPLANG-MENUP4
	IF %ERRORLEVEL%==11 GOTO DISPLANG-MENUP2
	IF %ERRORLEVEL%==12 GOTO HOME-MAINMENU
	IF %ERRORLEVEL%==13 EXIT /B 0

:DISPLANG-MENUP4

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO. & ECHO.
POWERSHELL -NoP -C "Write-Host """                 [1] Romanian (ro-RO)`n                 [2] Russian (ru-RU)`n                 [3] Serbian (sr-Latn-RS)`n                 [4] Slovak (sk-SK)`n                 [5] Slovenian (sl-SI)`n                 [6] Spanish [Mexico] (es-MX)`n                 [7] Spanish [Spain] (es-ES)`n                 [8] Swedish (sv-SE)`n                 [9] Thai (th-TH)`n`n                 [N] Next Page`n                 [P] Previous Page`n                 [0] Return to Menu`n                 [X] Exit`n                                    Page 4/5`n           __________________________________________________________`n`n           Choose a menu option: """ -NoNewLine; [Console]::CursorVisible = $True; CHOICE /C 123456789NP0X /N /M  %BS%; [Console]::CursorVisible = $False; EXIT $LastExitCode"
	IF %ERRORLEVEL%==1 SET "langSel=ro-RO" & SET "dispDl=3230000" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==2 SET "langSel=ru-RU" & SET "dispDl=3230000" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==3 SET "langSel=sr-Latn-RS" & SET "dispDl=3230000" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==4 SET "langSel=sk-SK" & SET "dispDl=3230000" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==5 SET "langSel=sl-SI" & SET "dispDl=3230000" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==6 SET "langSel=es-MX" & SET "dispDl=2480000" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==7 SET "langSel=es-ES" & SET "dispDl=2480000" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==8 SET "langSel=sv-SE" & SET "dispDl=3230000" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==9 SET "langSel=th-TH" & SET "dispDl=3230000" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==10 GOTO DISPLANG-MENUP5
	IF %ERRORLEVEL%==11 GOTO DISPLANG-MENUP3
	IF %ERRORLEVEL%==12 GOTO HOME-MAINMENU
	IF %ERRORLEVEL%==13 EXIT /B 0

:DISPLANG-MENUP5

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO. & ECHO.
POWERSHELL -NoP -C "Write-Host """                 [1] Turkish (tr-TR)`n                 [2] Ukrainian (uk-UA)`n`n                 [P] Previous Page`n                 [0] Return to Menu`n                 [X] Exit`n                                    Page 5/5`n           __________________________________________________________`n`n           Choose a menu option: """ -NoNewLine; [Console]::CursorVisible = $True; CHOICE /C 12P0X /N /M  %BS%; [Console]::CursorVisible = $False; EXIT $LastExitCode"
	IF %ERRORLEVEL%==1 SET "langSel=tr-TR" & SET "dispDl=3230000" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==2 SET "langSel=uk-UA" & SET "dispDl=3230000" & GOTO DISPLANG-DOWNLOAD
	IF %ERRORLEVEL%==3 GOTO DISPLANG-MENUP4
	IF %ERRORLEVEL%==4 GOTO HOME-MAINMENU
	IF %ERRORLEVEL%==5 EXIT /B 0

:DISPLANG-DOWNLOAD

SETLOCAL

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO.

IF "%lpStatus%"=="removed" GOTO DISPLANG-LPREMOVE

CALL :AUX-GENRND "7"

REM Check if language pack is already installed
DISM /Online /Get-Intl /English | FINDSTR /I /R /c:"Installed language(s):.* %langSel%" /c:"Fallback Languages.* %langSel%[^ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789]*" > NUL 2>&1
	IF %ERRORLEVEL% LEQ 0 GOTO DISPLANG-USERCHECK

SET "ZIPLoc=7z.exe"
FOR /F "usebackq tokens=1,* delims=\" %%A IN (`WHERE 7z.exe 2^>^&1`) DO IF EXIST "%SYSTEMDRIVE%%%B" SET "dispSkip0=rem "
	IF NOT "%dispSkip0%"=="rem " (
		IF EXIST "%SYSTEMDRIVE%\Program Files\7-Zip\7z.exe" (
			SET "ZIPLoc=%SYSTEMDRIVE%\Program Files\7-Zip\7z.exe"
			SET "dispSkip0=rem "
		) ELSE (
			IF EXIST "%SYSTEMDRIVE%\Program Files (x86)\7-Zip\7z.exe" (
				SET "ZIPLoc=%SYSTEMDRIVE%\Program Files (x86)\7-Zip\7z.exe"
				SET "dispSkip0=rem "
			)
		)
	)

WHERE choco.exe>NUL 2>&1 && SET "dispChoco=true"

IF NOT "%dispChoco%"=="true" (
	IF NOT "%dispSkip0%"=="rem " CALL :AUX-RETURN "7-Zip or Chocolatey must be installed." "HOME-LANGUAGE" -E
)

TASKLIST /FI "IMAGENAME eq lpksetup.exe" 2>&1 | FINDSTR /i /c:"lpksetup.exe" > NUL 2>&1
	IF %ERRORLEVEL% EQU 0 CALL :AUX-RETURN "All instances of lpksetup.exe must be closed." "HOME-LANGUAGE" -E
IF "%dispDl%"=="2480000" POWERSHELL -NoP -C "Write-Host """`n           A ~2.5GB Language Packs ISO must be downloaded`n           Continue? (Y/N): """ -NoNewLine; [Console]::CursorVisible = $True; CHOICE /C YN /N /M '%BS%'; [Console]::CursorVisible = $False; EXIT $LastExitCode"
IF "%dispDl%"=="2900000" POWERSHELL -NoP -C "Write-Host """`n           A ~2.9GB Language Packs ISO must be downloaded`n           Continue? (Y/N): """ -NoNewLine; [Console]::CursorVisible = $True; CHOICE /C YN /N /M '%BS%'; [Console]::CursorVisible = $False; EXIT $LastExitCode"
IF "%dispDl%"=="3230000" POWERSHELL -NoP -C "Write-Host """`n           A ~3.2GB Language Packs ISO must be downloaded`n           Continue? (Y/N): """ -NoNewLine; [Console]::CursorVisible = $True; CHOICE /C YN /N /M '%BS%'; [Console]::CursorVisible = $False; EXIT $LastExitCode"
	IF %ERRORLEVEL%==2 ENDLOCAL & GOTO HOME-MAINMENU

IF "%altRun%"=="true" (
	SET "dispISOLoc=%userUserProfile%\Desktop"
) ELSE (
	SET "dispISOLoc=%USERPROFILE%\Desktop"
)
IF NOT EXIST "%dispISOLoc%" SET "dispISOLoc=%dirPath:~0,-1%"
FOR /F "tokens=2 delims==" %%A IN ('WMIC logicaldisk where "DeviceID='%dispISOLoc:~0,2%'" get FreeSpace /format:value') DO SET "dispISOSpace=%%A"
	SET "dispISOSpace=%dispISOSpace:~0,-9%"
		ECHO "%dispISOSpace%" | FINDSTR "1 2 3 4 5 6 7 8 9 0" > NUL
			IF %ERRORLEVEL% GTR 0 CALL :AUX-RETURN "Failed to check free space of target drive (%dispISOLoc:~0,2%)." "HOME-LANGUAGE" -E

IF %dispISOSpace% LEQ 80 CALL :AUX-RETURN "Not enough space available in target drive (%dispISOLoc:~0,2%)." "HOME-LANGUAGE" -E

CALL :AUX-NETWORKCHECK
	IF %ERRORLEVEL% NEQ 0 CALL :AUX-RETURN "Internet is required for this action." "HOME-LANGUAGE" -E

CALL :AUX-GENRND "7"

REM If 7zip must be installed, there will not be enough space to display everything in 25 lines (script height) without this line
%dispSkip0%CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO.
ECHO. & ECHO                                 Download Progress
IF "%dispDl%"=="2480000" CURL --range 0-2480000000 -L --progress-bar "https://software-download.microsoft.com/download/pr/19041.1.191206-1406.vb_release_CLIENTLANGPACKDVD_OEM_MULTI.iso" --output "%dispISOLoc%\LangPacks%rndOut%.ISO"
IF "%dispDl%"=="2900000" CURL --range 0-2900000000 -L --progress-bar "https://software-download.microsoft.com/download/pr/19041.1.191206-1406.vb_release_CLIENTLANGPACKDVD_OEM_MULTI.iso" --output "%dispISOLoc%\LangPacks%rndOut%.ISO"
IF "%dispDl%"=="3230000" CURL --range 0-3230000000 -L --progress-bar "https://software-download.microsoft.com/download/pr/19041.1.191206-1406.vb_release_CLIENTLANGPACKDVD_OEM_MULTI.iso" --output "%dispISOLoc%\LangPacks%rndOut%.ISO"
FOR %%A IN ("%dispISOLoc%\LangPacks%rndOut%.ISO") DO SET "langISOSize=%%~zA"
	CALL :AUX-KILOBYTEFETCH "%dispDl%" -Compare "%langISOSize%"
		IF %ERRORLEVEL% NEQ 0 (
		DEL /Q /F "%dispISOLoc%\LangPacks%rndOut%.ISO" > NUL 2>&1
		CALL :AUX-RETURN "Failed to download file." "HOME-LANGUAGE" -E
	)

:DISPLANG-INSTALL

%dispSkip0%ECHO. & ECHO                                Installing 7zip... & choco install -y --force --allow-empty-checksums "7zip" > NUL
POWERSHELL -NoP -C "Start-Process '%ZIPLoc:'=''%' -ArgumentList 'e','-y','-o""""%dispISOLoc:'=''%\LangPacks%rndOut%""""','""""%dispISOLoc:'=''%\LangPacks%rndOut%.ISO""""','x64\langpacks\*.cab' -NoNewWindow -Wait" > NUL 2>&1
DEL /Q /F "%dispISOLoc%\LangPacks%rndOut%.ISO" > NUL

ECHO. & ECHO                 Installing language pack, this may take awhile...
FOR /F "tokens=2" %%A IN ('DATE /T') DO SET "dateAfter=%%A"
SET "timeAfter=%TIME:~0,-3%"
LPKSETUP /i %langSel% /p "%dispISOLoc%\LangPacks%rndOut%\Microsoft-Windows-Client-Language-Pack_x64_%langSel%.cab" /r > NUL
:lpkInstLogLoop
	TIMEOUT /T 1 /NOBREAK > NUL
	POWERSHELL -NoP -C "$timeAfter = Get-Date -Date '%dateAfter% %timeAfter%'; Get-WinEvent -FilterHashtable @{Logname='Microsoft-Windows-LanguagePackSetup/Operational';StartTime=$timeAfter}" | FINDSTR "2014 2007" > NUL
		IF %ERRORLEVEL% LSS 1 (
			%dispSkip0%ECHO. & ECHO                                 Removing 7zip... & choco uninstall 7zip -y --force-dependencies --allow-empty-checksums>NUL & ECHO.
			RMDIR /Q /S "%dispISOLoc%\LangPacks%rndOut%" & CALL :AUX-RETURN "lpksetup failed." "HOME-LANGUAGE" -E
		)
	POWERSHELL -NoP -C "$timeAfter = Get-Date -Date '%dateAfter% %timeAfter%'; Get-WinEvent -FilterHashtable @{Logname='Microsoft-Windows-LanguagePackSetup/Operational';StartTime=$timeAfter}" | FINDSTR "2000" > NUL
		IF %ERRORLEVEL% LSS 1 (
			%dispSkip0%ECHO. & ECHO                                 Removing 7zip... & choco uninstall 7zip -y --force-dependencies --allow-empty-checksums>NUL & ECHO.
			IF "%lpStatus%"=="added" RMDIR /Q /S "%dispISOLoc%\LangPacks%rndOut%" & GOTO DISPLANG-LPCOMPLETE 
			RMDIR /Q /S "%dispISOLoc%\LangPacks%rndOut%" & GOTO DISPLANG-USERCHECK
		)
	GOTO :lpkInstLogLoop

:DISPLANG-USERCHECK

IF /I "%~1"=="LangSet" SET "langSel=%~2" & SET "rndOut=%~3" & SET "makeKBDef=%~4" & GOTO DISPLANG-SETLANG
POWERSHELL -NoP -C "Write-Host """`n           Make default keyboard language? (Y/N): """ -NoNewLine; [Console]::CursorVisible = $True; CHOICE /C YN /N /M '%BS%'; [Console]::CursorVisible = $False; EXIT $LastExitCode"
	IF %ERRORLEVEL%==2 SET "makeKBDef=false"

IF "%altRun%"=="true" GOTO DISPLANG-ALT

:DISPLANG-SETLANG

ECHO. & ECHO                                Setting language...
FOR /F "delims=" %%A IN ('POWERSHELL -NoP -C "Get-WinDefaultInputMethodOverride"') DO SET "possibleLangDef=%%A"
	IF NOT "%possibleLangDef%"=="" (
		SET "currentLangDef=%possibleLangDef%"
	) ELSE ( 
		FOR /F "delims=" %%A IN ('POWERSHELL -NoP -C "(Get-WinUserLanguageList)[0].InputMethodTips"') DO SET "currentLangDef=%%A"
	)
SETLOCAL ENABLEDELAYEDEXPANSION
REM Accounts for zero input methods. Very unlikely scenario
POWERSHELL -NoP -c "(Get-WinUserLanguageList).InputMethodTips" | FINDSTR /c:":" > NUL 2>&1
	IF %ERRORLEVEL% EQU 0 (
		FOR /F "delims=" %%A IN ('POWERSHELL -NoP -C "(Get-WinUserLanguageList).InputMethodTips"') DO SET "oldInputMethods=!oldInputMethods!;$LangList[0].InputMethodTips.Add('%%A')"
	)
POWERSHELL -NoP -C "Set-WinSystemLocale %langSel%; $LangList = New-WinUserLanguageList %langSel%%oldInputMethods%; Set-WinUserLanguageList $LangList -Force"
REM Clears override
IF "%makeKBDef%"=="" POWERSHELL -NoP -C "Set-WinDefaultInputMethodOverride"
IF "%makeKBDef%"=="false" POWERSHELL -NoP -C "Set-WinDefaultInputMethodOverride '%currentLangDef%'"
ENDLOCAL
IF /I "%~1"=="LangSet" ECHO Golden> "%TEMP%\[amecs]-LangComm%rndOut%.txt" & EXIT 0

CALL :AUX-RETURN "Display language changed to %langSel%" -H "R:R -T 0.restart" -L "A restart is required to take effect."

:DISPLANG-LPCOMPLETE

%dispSkip0%ECHO. & ECHO                                 Removing 7zip... & choco uninstall 7zip.install -y --force > NUL
%dispSkip0%choco uninstall 7zip -y --force > NUL

CALL :AUX-RETURN "Language Pack %langSel% installed successfully" -H -L "A restart is recommended."

:DISPLANG-LPREMOVE

SET "dispSkip0=rem "

FOR /F tokens^=2^ delims^=^" %%A IN ('TASKLIST /FI "IMAGENAME eq lpksetup.exe" /NH /FO csv') DO SET "lpkStatus=%%A"
	IF "%lpkStatus%"=="," CALL :AUX-RETURN "All instances of lpksetup.exe must be closed." "HOME-LANGUAGE" -E
FOR /F "tokens=2" %%A IN ('DATE /T') DO SET "dateAfter=%%A"
SET "timeAfter=%TIME:~0,-3%"
ECHO. & ECHO                        Uninstalling %langSel% LanguagePack...
LPKSETUP /u %langSel% /r
:LPREMOVE-LOOP
	TIMEOUT /T 1 /NOBREAK > NUL
	POWERSHELL -command "$timeAfter = Get-Date -Date '%dateAfter% %timeAfter%'; Get-WinEvent -FilterHashtable @{Logname='Microsoft-Windows-LanguagePackSetup/Operational';StartTime=$timeAfter}" | FINDSTR "2014 2008" > NUL
		IF %ERRORLEVEL% LSS 1 CALL :AUX-RETURN "lpksetup.exe failed." "HOME-LANGUAGE" -E
	POWERSHELL -command "$timeAfter = Get-Date -Date '%dateAfter% %timeAfter%'; Get-WinEvent -FilterHashtable @{Logname='Microsoft-Windows-LanguagePackSetup/Operational';StartTime=$timeAfter}" | FINDSTR "2001" > NUL
		IF %ERRORLEVEL% LSS 1 CALL :AUX-RETURN "Language Pack %langSel% removed successfully" -H -L "A restart is recommended."
	GOTO LPREMOVE-LOOP

:DISPLANG-ALT

CALL :AUX-ALTSTART "SetDispLang" "CMD /C 'START /min '' '|Script|' LangSet %langSel% |rndOut| %makeKBDef%'"
	IF %ERRORLEVEL% EQU 1 CALL :AUX-RETURN "Failed to create scheduled task. (1)" "HOME-LANGUAGE" -E -C
	IF %ERRORLEVEL% EQU 2 CALL :AUX-RETURN "Failed to create scheduled task. (2)" "HOME-LANGUAGE" -E -C

	CALL :AUX-WAITLOOP "-C:Golden" "%userTemp%\[amecs]-LangComm%rndOut%.txt" -TME "30"
		IF %ERRORLEVEL% NEQ 0 CALL :AUX-RETURN "Failed to set language settings." "HOME-LANGUAGE" -C -E
		CALL :AUX-RETURN "Display language changed to %langSel%" -H "R:R -T 0.restart" -L "A restart is required to take effect." -C
REM ------------------------DISPLANG-END------------------------



REM ---------------------------KBLANG---------------------------
:KBLANG-LANGS

SETLOCAL ENABLEDELAYEDEXPANSION
SET /A count=0
SET "kbSub=false"
FOR /F "usebackq tokens=1, 2, 4, 6, 8, 10, 12, 14 skip=4 delims=|" %%A IN ("%~f0") DO (
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

SET /A kbCCommCount=0
FOR /F "usebackq tokens=1, 2, 4, 6, 8, 10, 12, 14 skip=35 delims=|" %%A IN ("%~f0") DO (
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
SET /A count=0
SET "kbSub=true"
FOR /F "usebackq tokens=1, 2, 4, 6, 8, 10, 12, 14, 16, 18 skip=66 delims=|" %%A IN ("%~f0") DO (
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

SET /A skbCCommCount=0
FOR /F "usebackq tokens=1, 2, 4, 6, 8, 10, 12, 14, 16, 18 skip=125 delims=|" %%A IN ("%~f0") DO (
	IF "%%A"=="REM Marker" GOTO KBLANG-PREMMS
	IF "%%B"=="%kbLangSel%" (
		IF "%%C"=="" GOTO KBLANG-PREMMS
		SET /A skbCCommCount=!skbCCommCount!+1 & SET "kbComm!skbCCommCount!=SET kbLangSel=%%C"
		IF "%%D"=="" GOTO KBLANG-PREMMS
		SET /A "skbCCommCount=!skbCCommCount!+1" & SET "kbComm!skbCCommCount!=SET kbLangSel=%%D"
		IF "%%E"=="" GOTO KBLANG-PREMMS
		SET /A "skbCCommCount=!skbCCommCount!+1" & SET "kbComm!skbCCommCount!=SET kbLangSel=%%E"
		IF "%%F"=="" GOTO KBLANG-PREMMS
		SET /A "skbCCommCount=!skbCCommCount!+1" & SET "kbComm!skbCCommCount!=SET kbLangSel=%%F"
		IF "%%G"=="" GOTO KBLANG-PREMMS
		SET /A "skbCCommCount=!skbCCommCount!+1" & SET "kbComm!skbCCommCount!=SET kbLangSel=%%G"
		IF "%%H"=="" GOTO KBLANG-PREMMS
		SET /A "skbCCommCount=!skbCCommCount!+1" & SET "kbComm!skbCCommCount!=SET kbLangSel=%%H"
		IF "%%I"=="" GOTO KBLANG-PREMMS
		SET /A "skbCCommCount=!skbCCommCount!+1" & SET "kbComm!skbCCommCount!=SET kbLangSel=%%I"
		IF "%%J"=="" GOTO KBLANG-PREMMS
		SET /A "skbCCommCount=!skbCCommCount!+1" & SET "kbComm!skbCCommCount!=SET kbLangSel=%%J"
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

SET "kbDisablePrev=" & SET "kbPrevMsg=`n                [P] Previous Page" & SET "lC10=N" & SET "kbDisableNext=" & SET "kbNextMsg=`n                [N] Next Page" & SET "lC11=P"
IF "%kbLangPageLoc%" EQU "1" SET "kbDisablePrev=rem " & SET "kbPrevMsg=" & SET "lC11="

SET "lC1=1" & SET "lC2=2" & SET "lC3=3" & SET "lC4=4" & SET "lC5=5" & SET "lC6=6" & SET "lC7=7" & SET "lC8=8" & SET "lC9=9"
SET "kbLangSkip1=" & SET "kbLangSkip2=" & SET "kbLangSkip3=" & SET "kbLangSkip4=" & SET "kbLangSkip5=" & SET "kbLangSkip6=" & SET "kbLangSkip7=" & SET "kbLangSkip8="
SET /A "kbLangCount=%kbLangCount%+1"
SET "kbLangMsg1=                [1] !lang%kbLangCount%!" & SET "kbCComm1=!kbComm%kbLangCount%!" & SET /A "kbLangCount=%kbLangCount%+1"
SET "kbLangMsg2=`n                [2] !lang%kbLangCount%!" & SET "kbCComm2=!kbComm%kbLangCount%!" & SET /A "kbLangCount=%kbLangCount%+1"
SET "kbLangMsg3=`n                [3] !lang%kbLangCount%!" & SET "kbCComm3=!kbComm%kbLangCount%!" & SET /A "kbLangCount=%kbLangCount%+1"
SET "kbLangMsg4=`n                [4] !lang%kbLangCount%!" & SET "kbCComm4=!kbComm%kbLangCount%!" & SET /A "kbLangCount=%kbLangCount%+1"
SET "kbLangMsg5=`n                [5] !lang%kbLangCount%!" & SET "kbCComm5=!kbComm%kbLangCount%!" & SET /A "kbLangCount=%kbLangCount%+1"
SET "kbLangMsg6=`n                [6] !lang%kbLangCount%!" & SET "kbCComm6=!kbComm%kbLangCount%!" & SET /A "kbLangCount=%kbLangCount%+1"
SET "kbLangMsg7=`n                [7] !lang%kbLangCount%!" & SET "kbCComm7=!kbComm%kbLangCount%!" & SET /A "kbLangCount=%kbLangCount%+1"
SET "kbLangMsg8=`n                [8] !lang%kbLangCount%!" & SET "kbCComm8=!kbComm%kbLangCount%!" & SET /A "kbLangCount=%kbLangCount%+1"
SET "kbLangMsg9=`n                [9] !lang%kbLangCount%!" & SET "kbCComm9=!kbComm%kbLangCount%!"

IF "%kbLangPageLoc%"=="%kbLangPages%" (
	SET "kbDisableNext=rem " & SET "kbNextMsg=" & SET "lC10="
	IF "%kbLangRemainder%" GTR "0" (
		SET "lR1=1"
		SET "lELs=%kbLangRemainder%"
		IF NOT "%kbLangRemainder%" GTR "1" (SET "kbLangSkip1=rem " & SET "kbLangMsg2=" & SET /A "kbLangCount=!kbLangCount!-1") ELSE (SET "lR2=2")
		IF NOT "%kbLangRemainder%" GTR "2" (SET "kbLangSkip2=rem " & SET "kbLangMsg3=" & SET /A "kbLangCount=!kbLangCount!-1") ELSE (SET "lR3=3")
		IF NOT "%kbLangRemainder%" GTR "3" (SET "kbLangSkip3=rem " & SET "kbLangMsg4=" & SET /A "kbLangCount=!kbLangCount!-1") ELSE (SET "lR4=4")
		IF NOT "%kbLangRemainder%" GTR "4" (SET "kbLangSkip4=rem " & SET "kbLangMsg5=" & SET /A "kbLangCount=!kbLangCount!-1") ELSE (SET "lR5=5")
		IF NOT "%kbLangRemainder%" GTR "5" (SET "kbLangSkip5=rem " & SET "kbLangMsg6=" & SET /A "kbLangCount=!kbLangCount!-1") ELSE (SET "lR6=6")
		IF NOT "%kbLangRemainder%" GTR "6" (SET "kbLangSkip6=rem " & SET "kbLangMsg7=" & SET /A "kbLangCount=!kbLangCount!-1") ELSE (SET "lR7=7")
		IF NOT "%kbLangRemainder%" GTR "7" (SET "kbLangSkip7=rem " & SET "kbLangMsg8=" & SET /A "kbLangCount=!kbLangCount!-1") ELSE (SET "lR8=8")
		IF NOT "%kbLangRemainder%" GTR "8" (SET "kbLangSkip8=rem " & SET "kbLangMsg9=" & SET /A "kbLangCount=!kbLangCount!-1") ELSE (SET "lR9=9")
		SET "lC1=!lR1!" & SET "lC2=!lR2!" & SET "lC3=!lR3!" & SET "lC4=!lR4!" & SET "lC5=!lR5!" & SET "lC6=!lR6!" & SET "lC7=!lR7!" & SET "lC8=!lR8!" & SET "lC9=!lR9!"
	)
)
SET "kbLangCommGo="
IF "%kbSub%"=="true" (SET "kbPageMsg=`n") ELSE (SET "kbPageMsg=`n                                   Page %kbLangPageLoc%/%kbLangPages%")

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO. & ECHO.
POWERSHELL -NoP -C "Write-Host """%kbLangMsg1%%kbLangMsg2%%kbLangMsg3%%kbLangMsg4%%kbLangMsg5%%kbLangMsg6%%kbLangMsg7%%kbLangMsg8%%kbLangMsg9%`n%kbNextMsg%%kbPrevMsg%`n                [0] %kb0Opt%`n                [X] Exit%kbPageMsg%`n           __________________________________________________________`n`n           Choose a menu option: """ -NoNewLine; [Console]::CursorVisible = $True; CHOICE /C %lC1%%lC2%%lC3%%lC4%%lC5%%lC6%%lC7%%lC8%%lC9%%lC10%%lC11%0X /N /M '%BS%'; [Console]::CursorVisible = $False; EXIT $LastExitCode"
SET "kbChoice=%ERRORLEVEL%"
IF %kbLangPageLoc% LSS %kbLangPages% (SET /A kbLangCount=%kbLangCount%-9) ELSE (SET /A kbLangCount=%kbLangCount%-%kbLangRemainder%)
	IF %kbChoice%==1 ENDLOCAL & CALL SET "kbLangCommGo=%%kbLangLoc::COMM:=%kbCComm1%%%" & SET "kbChoice=NULL"
	%kbLangSkip1%IF %kbChoice%==2 ENDLOCAL & CALL SET "kbLangCommGo=%%kbLangLoc::COMM:=%kbCComm2%%%" & SET "kbChoice=NULL"
	%kbLangSkip2%IF %kbChoice%==3 ENDLOCAL & CALL SET "kbLangCommGo=%%kbLangLoc::COMM:=%kbCComm3%%%" & SET "kbChoice=NULL"
	%kbLangSkip3%IF %kbChoice%==4 ENDLOCAL & CALL SET "kbLangCommGo=%%kbLangLoc::COMM:=%kbCComm4%%%" & SET "kbChoice=NULL"
	%kbLangSkip4%IF %kbChoice%==5 ENDLOCAL & CALL SET "kbLangCommGo=%%kbLangLoc::COMM:=%kbCComm5%%%" & SET "kbChoice=NULL"
	%kbLangSkip5%IF %kbChoice%==6 ENDLOCAL & CALL SET "kbLangCommGo=%%kbLangLoc::COMM:=%kbCComm6%%%" & SET "kbChoice=NULL"
	%kbLangSkip6%IF %kbChoice%==7 ENDLOCAL & CALL SET "kbLangCommGo=%%kbLangLoc::COMM:=%kbCComm7%%%" & SET "kbChoice=NULL"
	%kbLangSkip7%IF %kbChoice%==8 ENDLOCAL & CALL SET "kbLangCommGo=%%kbLangLoc::COMM:=%kbCComm8%%%" & SET "kbChoice=NULL"
	%kbLangSkip8%IF %kbChoice%==9 ENDLOCAL & CALL SET "kbLangCommGo=%%kbLangLoc::COMM:=%kbCComm9%%%" & SET "kbChoice=NULL"
	%kbLangCommGo%
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

:KBLANG-PRESET

SETLOCAL

IF /I "%~1"=="kbLangSet" SET "kbLangSel=%~2" & SET "rndOut=%~3" & SET "kbMakeDef=%~4" & GOTO KBLANG-SETLANG

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO.

POWERSHELL -NoP -C "Write-Host """`n           Make default keyboard language? (Y/N): """ -NoNewLine; [Console]::CursorVisible = $True; CHOICE /C YN /N /M '%BS%'; [Console]::CursorVisible = $False; EXIT $LastExitCode"
	IF %ERRORLEVEL%==2 SET "kbMakeDef=false"

IF "%altRun%"=="true" GOTO :KBLANG-ALTADD

POWERSHELL -NoP -c "(Get-WinUserLanguageList).InputMethodTips" | FINDSTR /I /c:"%kbLangSel%" > NUL 2>&1
	IF %ERRORLEVEL% EQU 0 (
		IF /I "%~1"=="kbLangSet" ECHO AME-ERROR> "%TEMP%\[amecs]-LangComm%rndOut%.txt"& EXIT 0
		CALL :AUX-RETURN "Selected keyboard language is already installed." "HOME-LANGUAGE" -E
	)

ECHO. & ECHO                            Adding keyboard language...

:KBLANG-SETLANG

IF NOT "%~1"=="kbLangSet" TIMEOUT /T 1 /NOBREAK > NUL
POWERSHELL -NoP -C "$NewLangs=Get-WinUserLanguageList; $NewLangs[0].InputMethodTips.Add('%kbLangSel%'); Set-WinUserLanguageList $NewLangs -Force" > NUL
	IF %ERRORLEVEL% NEQ 0 (
		IF /I "%~1"=="kbLangSet" ECHO AME-ERROR1> "%TEMP%\[amecs]-LangComm%rndOut%.txt"& EXIT 0
		CALL :AUX-RETURN "Failed to set language settings." "HOME-LANGUAGE" -E
	)
IF NOT "%kbMakeDef%"=="false" POWERSHELL -NoP -C "Set-WinDefaultInputMethodOverride -InputTip '%kbLangSel%'"
IF /I "%~1"=="kbLangSet" ECHO Golden> "%TEMP%\[amecs]-LangComm%rndOut%.txt" & EXIT 0

CALL :AUX-RETURN "Keyboard language added successfully" -H

:KBLANG-REMOVELANG

SETLOCAL ENABLEDELAYEDEXPANSION

IF /I "%~1"=="kbLangRem" SET "kbLangSel=%~2" & SET "rndOut=%~3"

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO.

IF "%altRun%"=="true" GOTO :KBLANG-ALTREMOVE

ECHO. & ECHO                           Removing keyboard language...

IF NOT "%~1"=="kbLangRem" TIMEOUT /T 1 /NOBREAK > NUL
REM Accounts for zero input methods. Very unlikely scenario
POWERSHELL -NoP -c "(Get-WinUserLanguageList).InputMethodTips" | FINDSTR /I /c:"%kbLangSel%" > NUL 2>&1
	IF %ERRORLEVEL% EQU 0 (
		FOR /F "usebackq delims=" %%A IN (`POWERSHELL -NoP -C "(Get-WinUserLanguageList).InputMethodTips" ^| FINDSTR /V /I /c:"%kbLangSel%"`) DO SET "oldInputMethods=!oldInputMethods!;$LangList[0].InputMethodTips.Add('%%A')"
	) ELSE (
		IF /I "%~1"=="kbLangRem" ECHO AME-ERROR> "%TEMP%\[amecs]-LangComm%rndOut%.txt"& EXIT 0
		CALL :AUX-RETURN "Selected keyboard language is not installed." "HOME-LANGUAGE" -E
	)
POWERSHELL -NoP -C "$LangTag = (Get-WinUserLanguageList)[0].LanguageTag; $LangList = New-WinUserLanguageList $LangTag%oldInputMethods%; Set-WinUserLanguageList $LangList -Force"
	IF %ERRORLEVEL% NEQ 0 (
		IF /I "%~1"=="kbLangRem" ECHO AME-ERROR1> "%TEMP%\[amecs]-LangComm%rndOut%.txt"& EXIT 0
		CALL :AUX-RETURN "Failed to set language settings." "HOME-LANGUAGE" -E
	)
POWERSHELL -NoP -C "(Get-WinUserLanguageList).InputMethodTips" | FINDSTR /I /c:"%kbLangSel%" > NUL 2>&1
	IF %ERRORLEVEL% EQU 0 (
		IF /I "%~1"=="kbLangRem" ECHO AME-ERROR2> "%TEMP%\[amecs]-LangComm%rndOut%.txt"& EXIT 0
		CALL :AUX-RETURN "Selected keyboard language cannot be removed." "HOME-LANGUAGE" -E
	)

IF /I "%~1"=="kbLangRem" ECHO Golden> "%TEMP%\[amecs]-LangComm%rndOut%.txt"& EXIT 0

CALL :AUX-RETURN "Keyboard language removed successfully" -H

:KBLANG-ALTADD

ECHO. & ECHO                            Adding keyboard language...

CALL :AUX-ALTSTART "SetkbLang" "CMD /C 'START /min '' '|Script|' kbLangSet %kbLangSel% |rndOut| %kbMakeDef%'"
	IF %ERRORLEVEL% EQU 1 CALL :AUX-RETURN "Failed to create scheduled task. (1)" "HOME-LANGUAGE" -E -C
	IF %ERRORLEVEL% EQU 2 CALL :AUX-RETURN "Failed to create scheduled task. (2)" "HOME-LANGUAGE" -E -C

	CALL :AUX-WAITLOOP "-C:Golden" "%userTemp%\[amecs]-LangComm%rndOut%.txt" -TME "30"
		IF %ERRORLEVEL% EQU 2 CALL :AUX-RETURN "Selected keyboard language is already installed." "HOME-LANGUAGE" -C -E
		IF %ERRORLEVEL% EQU 3 CALL :AUX-RETURN "Failed to set language settings. (1)" "HOME-LANGUAGE" -C -E
		IF %ERRORLEVEL% NEQ 0 CALL :AUX-RETURN "Failed to set language settings. (2)" "HOME-LANGUAGE" -C -E
		CALL :AUX-RETURN "Keyboard language added successfully" -H -C

:KBLANG-ALTREMOVE

ECHO. & ECHO                           Removing keyboard language...

CALL :AUX-ALTSTART "RemkbLang" "CMD /C 'START /min '' '|Script|' kbLangRem %kbLangSel% |rndOut|'"
	IF %ERRORLEVEL% EQU 1 CALL :AUX-RETURN "Failed to create scheduled task. (1)" "HOME-LANGUAGE" -E -C
	IF %ERRORLEVEL% EQU 2 CALL :AUX-RETURN "Failed to create scheduled task. (2)" "HOME-LANGUAGE" -E -C

	CALL :AUX-WAITLOOP "-C:Golden" "%userTemp%\[amecs]-LangComm%rndOut%.txt" -TME "30"
		IF %ERRORLEVEL% EQU 2 CALL :AUX-RETURN "Selected keyboard language is not installed." "HOME-LANGUAGE" -C -E
		IF %ERRORLEVEL% EQU 3 CALL :AUX-RETURN "Failed to set language settings. (1)" "HOME-LANGUAGE" -C -E
		IF %ERRORLEVEL% EQU 4 CALL :AUX-RETURN "Selected keyboard language cannot be removed." "HOME-LANGUAGE" -C -E
		IF %ERRORLEVEL% NEQ 0 CALL :AUX-RETURN "Failed to set language settings. (2)" "HOME-LANGUAGE" -C -E
		CALL :AUX-RETURN "Keyboard language removed successfully" -H -C
REM -------------------------KBLANG-END-------------------------



REM -------------------------NOUSERNAME-------------------------
:NOUSERNAME-DISABLE

SETLOCAL

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO.

ECHO. & ECHO                      Disabling username login requirement...
TIMEOUT /T 2 /NOBREAK > NUL
REG DELETE "HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System" /v dontdisplaylastusername /f > NUL 2>&1

CALL :AUX-RETURN "The username login requirement is now disabled" -H

:NOUSERNAME-ENABLE

SETLOCAL

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO.

ECHO. & ECHO                      Enabling username login requirement...
TIMEOUT /T 2 /NOBREAK > NUL
REG ADD "HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System" /v dontdisplaylastusername /t REG_DWORD /d 1 /f > NUL 2>&1

CALL :AUX-RETURN "The username login requirement is now enabled" -H R:L.sign-out -L "A sign-out is required to take effect."
REM -----------------------NOUSERNAME-END-----------------------



REM -------------------------AUTOLOGON--------------------------
:AUTOLOGON-ENABLE

SETLOCAL

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO.

CALL :AUX-INPUTLOOP "userPassword" "Enter your password, or enter 'Cancel' to exit" "0" "9" -Secure
	IF %ERRORLEVEL% EQU 3 ENDLOCAL & GOTO HOME-MAINMENU

IF %inpLenOut% GEQ 11 CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO.

ECHO. & ECHO                               Enabling AutoLogon...

CERTUTIL /f /decode "%scriptPath%" "%TEMP%\[amecs]-AutoLogon%rndOut%.exe" > NUL 2>&1

POWERSHELL -NoP -C "EXIT (Start-Process '%TEMP:'=''%\[amecs]-AutoLogon%rndOut%.exe' -ArgumentList '/DEL' -Wait -NoNewWindow).ExitCode" > NUL 2>&1
	IF %ERRORLEVEL% NEQ 0 (
		IF %ERRORLEVEL% EQU 7 CALL :AUX-RETURN "Failed to enable AutoLogon. (1)" -H -E -C
		CALL :AUX-RETURN "Failed to enable AutoLogon. (2)" -H -E -C
	)

REG QUERY "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon" /v "DefaultUsername" 2>&1 | FINDSTR /c:"%currentUsername%" > NUL 2>&1
	IF %ERRORLEVEL% EQU 0 (
		REG QUERY "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon" /v "AutoAdminLogon" 2>&1 | FINDSTR /R /X /C:".*AutoAdminLogon[ ].*REG_SZ.*[ ]1" > NUL 2>&1
			IF NOT ERRORLEVEL 1 CALL :AUX-RETURN "Failed to enable AutoLogon. (3)" -H -E -C
	)

IF NOT "%userPassword%"=="" (
	POWERSHELL -NoP -C "EXIT (Start-Process '%TEMP:'=''%\[amecs]-AutoLogon%rndOut%.exe' -ArgumentList '""""%currentUsername:'=''%""""','""""%userPassword:'=''%""""','/DISABLECAD' -Wait -NoNewWindow).ExitCode" > NUL 2>&1
) ELSE (
	POWERSHELL -NoP -C "EXIT (Start-Process '%TEMP:'=''%\[amecs]-AutoLogon%rndOut%.exe' -ArgumentList '""""%currentUsername:'=''%""""','""""""','/DISABLECAD' -Wait -NoNewWindow).ExitCode" > NUL 2>&1
)
	IF %ERRORLEVEL% NEQ 0 (
		POWERSHELL -NoP -C "Start-Process '%TEMP:'=''%\[amecs]-AutoLogon%rndOut%.exe' -ArgumentList '/DEL' -NoNewWindow" > NUL 2>&1
		IF %ERRORLEVEL% EQU 7 CALL :AUX-RETURN "Failed to enable AutoLogon. (4)" -H -E -C
		CALL :AUX-RETURN "Failed to enable AutoLogon. (5)" -H -E -C
	)

REG QUERY "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon" /v "DefaultUsername" 2>&1 | FINDSTR /c:"%currentUsername%" > NUL 2>&1
	IF %ERRORLEVEL% EQU 0 (
		REG QUERY "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon" /v "AutoAdminLogon" 2>&1 | FINDSTR /R /X /C:".*AutoAdminLogon[ ].*REG_SZ.*[ ]1" > NUL 2>&1
			IF ERRORLEVEL 1 POWERSHELL -NoP -C "Start-Process '%TEMP:'=''%\[amecs]-AutoLogon%rndOut%.exe' -ArgumentList '/DEL' -NoNewWindow">NUL 2>&1 & CALL :AUX-RETURN "Failed to enable AutoLogon. (6)" -H -E -C
	) ELSE (
		IF ERRORLEVEL 1 POWERSHELL -NoP -C "Start-Process '%TEMP:'=''%\[amecs]-AutoLogon%rndOut%.exe' -ArgumentList '/DEL' -NoNewWindow">NUL 2>&1 & CALL :AUX-RETURN "Failed to enable AutoLogon. (7)" -H -E -C
	)

CALL :AUX-RETURN "Enabled AutoLogon successfully" -H -C

:AUTOLOGON-DISABLE

SETLOCAL

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO.

ECHO. & ECHO                              Disabling AutoLogon...

CALL :AUX-GENRND "7"

CERTUTIL /f /decode "%scriptPath%" "%TEMP%\[amecs]-AutoLogon%rndOut%.exe" > NUL 2>&1

POWERSHELL -NoP -C "EXIT (Start-Process '%TEMP:'=''%\[amecs]-AutoLogon%rndOut%.exe' -ArgumentList '/DEL' -Wait -NoNewWindow).ExitCode" > NUL 2>&1
	IF %ERRORLEVEL% NEQ 0 (
		IF %ERRORLEVEL% EQU 7 CALL :AUX-RETURN "Failed to disable AutoLogon. (1)" -H -E -C
		CALL :AUX-RETURN "Failed to disable AutoLogon. (2)" -H -E -C
	)

REG QUERY "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon" /v "DefaultUsername" 2>&1 | FINDSTR /I /E /c:"    %currentUsername%" > NUL 2>&1
	IF %ERRORLEVEL% EQU 0 (
		REG QUERY "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon" /v "AutoAdminLogon" 2>&1 | FINDSTR /R /X /C:".*AutoAdminLogon[ ].*REG_SZ.*[ ]1" > NUL 2>&1
			IF NOT ERRORLEVEL 1 CALL :AUX-RETURN "Failed to disable AutoLogon. (3)" -H -E -C
	)

CALL :AUX-RETURN "Disabled AutoLogon successfully" -H -C
REM -----------------------AUTOLOGON-END------------------------


                    REM ---------------
                    REM Extra Functions
                    REM ---------------


REM ----------------------------WSL-----------------------------
:WSL-ENABLE

SETLOCAL

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO.

DISM /Online /Get-FeatureInfo /FeatureName:Microsoft-Windows-Subsystem-Linux /English | FINDSTR /I /c:"State : Enabled" > NUL 2>&1
	IF %ERRORLEVEL% EQU 0 (
		WHERE wsl.exe > NUL 2>&1
			IF NOT ERRORLEVEL 1 CALL :AUX-RETURN "WSL is already enabled." "HOME-WSL" -E
	)

ECHO. & ECHO                                  Enabling WSL...
DISM /Online /Enable-Feature /FeatureName:Microsoft-Windows-Subsystem-Linux -NoRestart /English | FINDSTR /I /c:"Error"
	IF %ERRORLEVEL% EQU 0 CALL :AUX-RETURN "Failed to enable WSL. (1)" "HOME-WSL" -E

DISM /Online /Get-FeatureInfo /FeatureName:Microsoft-Windows-Subsystem-Linux /English | FINDSTR /I /c:"State : Disabled" > NUL 2>&1
	IF %ERRORLEVEL% EQU 0 CALL :AUX-RETURN "Failed to enable WSL. (2)" "HOME-WSL" -E

WHERE wsl.exe > NUL 2>&1
	IF %ERRORLEVEL% EQU 0 CALL :AUX-RETURN "Enabled WSL successfully" -H

CALL :AUX-RETURN "Enabled WSL successfully" -H "R:R -T 0.restart" -L "A restart is required to complete the setup."

:WSL-DISABLE

SETLOCAL

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO.

DISM /Online /Get-FeatureInfo /FeatureName:Microsoft-Windows-Subsystem-Linux /English | FINDSTR /I /c:"State : Disabled" > NUL 2>&1
	IF %ERRORLEVEL% EQU 0 (
		WHERE wsl.exe > NUL 2>&1
			IF NOT ERRORLEVEL 1 CALL :AUX-RETURN "WSL is already disabled." "HOME-WSL" -E
	)
ECHO. & ECHO                                 Disabling WSL...
DISM /Online /Disable-Feature /FeatureName:Microsoft-Windows-Subsystem-Linux -NoRestart /English | FINDSTR /I /c:"Error"
	IF %ERRORLEVEL% EQU 0 CALL :AUX-RETURN "Failed to disable WSL. (1)" "HOME-WSL" -E

DISM /Online /Get-FeatureInfo /FeatureName:Microsoft-Windows-Subsystem-Linux /English | FINDSTR /I /c:"State : Enabled" > NUL 2>&1
	IF %ERRORLEVEL% EQU 0 CALL :AUX-RETURN "Failed to disable WSL. (2)" "HOME-WSL" -E

WHERE wsl.exe > NUL 2>&1
	IF %ERRORLEVEL% NEQ 0 CALL :AUX-RETURN "Disabled WSL successfully" -H

CALL :AUX-RETURN "Disabled WSL successfully" -H "R:R -T 0.restart" -L "A restart is required to complete the setup."

:WSL-DISTROMENUP1

SETLOCAL

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO. & ECHO.

CURL store.rg-adguard.net 2>&1 | FINDSTR /I /c:"Cloudflare Ray ID" > NUL 2>&1
	IF %ERRORLEVEL% EQU 0 (
		SET "homeWSL1Ch=123456N0X"
		SET "homeWSL2Ch=12345678NP0X"
		SET "homeWSL3Ch=23P0X"
		SET "homeWSLUnavailable1=""" -NoNewLine; Write-Host '                 :REP:' -ForegroundColor DarkGray -NoNewLine; Write-Host ' [Server Unavailable]' -ForegroundColor Red -NoNewLine; Write-Host """"
		SET "homeWSLUnavailable2=                 :REP:""" -ForegroundColor DarkGray -NoNewLine; Write-Host ' [Server Unavailable]' -ForegroundColor Red -NoNewLine; Write-Host """"
	) ELSE (
		SET "homeWSL1Ch=123456789N0X"
		SET "homeWSL2Ch=123456789NP0X"
		SET "homeWSL3Ch=123P0X"
		SET "homeWSLUnavailable1=                 :REP:"
		SET "homeWSLUnavailable2=                 :REP:"
	)

POWERSHELL -NoP -C "Write-Host """                 [1] Ubuntu 20.04 LTS`n                 [2] Ubuntu 18.04 LTS`n                 [3] Ubuntu 16.04 LTS`n                 [4] Debian Stable`n                 [5] Kali Linux`n                 [6] Fedora Remix`n%homeWSLUnavailable1::REP:=[7] Pengwin%`n%homeWSLUnavailable2::REP:=[8] Pengwin Enterprise 7%`n%homeWSLUnavailable2::REP:=[9] Alpine Linux%`n`n                 [N] Next Page`n                 [0] Return to Menu`n                 [X] Exit`n                                    Page 1/3`n           __________________________________________________________`n`n           Choose a menu option: """ -NoNewLine; [Console]::CursorVisible = $True; CHOICE /C  %homeWSL1Ch% /N /M  %BS%; [Console]::CursorVisible = $False; EXIT $LastExitCode"
	IF "%homeWSL1Ch%"=="123456N0X" (
		IF %ERRORLEVEL%==1 SET "wslDistro=Ubuntu" & SET "wslGroups=adm,dialout,cdrom,floppy,sudo,audio,dip,video,plugdev,netdev" & GOTO %wslMenuLoc%
		IF %ERRORLEVEL%==2 SET "wslDistro=Ubuntu-18.04" & SET "wslGroups=adm,dialout,cdrom,floppy,sudo,audio,dip,video,plugdev,lxd,netdev" & GOTO %wslMenuLoc%
		IF %ERRORLEVEL%==3 SET "wslDistro=Ubuntu-16.04" & SET "wslGroups=adm,dialout,cdrom,floppy,sudo,audio,dip,video,plugdev,netdev,lxd" & GOTO %wslMenuLoc%
		IF %ERRORLEVEL%==4 SET "wslDistro=Debian" & SET "wslGroups=adm,cdrom,sudo,dip,plugdev" & GOTO %wslMenuLoc%
		IF %ERRORLEVEL%==5 SET "wslDistro=kali-linux" & SET "wslGroups=adm,cdrom,sudo,dip,plugdev" & GOTO %wslMenuLoc%
		IF %ERRORLEVEL%==6 SET "wslDistro=fedoraremix" & SET "wslGroups=adm,wheel,cdrom" & GOTO %wslMenuLoc%
		IF %ERRORLEVEL%==7 GOTO WSL-DISTROMENUP2
		IF %ERRORLEVEL%==8 GOTO HOME-MAINMENU
		IF %ERRORLEVEL%==9 EXIT /B 0
	)

	IF %ERRORLEVEL%==1 SET "wslDistro=Ubuntu" & SET "wslGroups=adm,dialout,cdrom,floppy,sudo,audio,dip,video,plugdev,netdev" & GOTO %wslMenuLoc%
	IF %ERRORLEVEL%==2 SET "wslDistro=Ubuntu-18.04" & SET "wslGroups=adm,dialout,cdrom,floppy,sudo,audio,dip,video,plugdev,lxd,netdev" & GOTO %wslMenuLoc%
	IF %ERRORLEVEL%==3 SET "wslDistro=Ubuntu-16.04" & SET "wslGroups=adm,dialout,cdrom,floppy,sudo,audio,dip,video,plugdev,netdev,lxd" & GOTO %wslMenuLoc%
	IF %ERRORLEVEL%==4 SET "wslDistro=Debian" & SET "wslGroups=adm,cdrom,sudo,dip,plugdev" & GOTO %wslMenuLoc%
	IF %ERRORLEVEL%==5 SET "wslDistro=kali-linux" & SET "wslGroups=adm,cdrom,sudo,dip,plugdev" & GOTO %wslMenuLoc%
	IF %ERRORLEVEL%==6 SET "wslDistro=fedoraremix" & SET "wslGroups=adm,wheel,cdrom" & GOTO %wslMenuLoc%
	IF %ERRORLEVEL%==7 SET "wslDistro=WLinux" & SET "wslGroups=adm,cdrom,sudo,dip,plugdev" & GOTO %wslMenuLoc%
	IF %ERRORLEVEL%==8 SET "wslDistro=WLE" & SET "wslGroups=adm,wheel,cdrom" & GOTO %wslMenuLoc%
	IF %ERRORLEVEL%==9 SET "wslDistro=Alpine" & SET "wslGroups=adm,wheel,floppy,cdrom,tape,ping" & GOTO %wslMenuLoc%
	IF %ERRORLEVEL%==10 GOTO WSL-DISTROMENUP2
	IF %ERRORLEVEL%==11 GOTO HOME-MAINMENU
	IF %ERRORLEVEL%==12 EXIT /B 0

:WSL-DISTROMENUP2

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO. & ECHO.
POWERSHELL -NoP -C "Write-Host """                 [1] openSUSE 4.2`n                 [2] openSUSE Tumbleweed`n                 [3] openSUSE Leap 15.1`n                 [4] openSUSE Leap 15.2`n                 [5] openSUSE Leap 15.3`n                 [6] Oracle Linux 7.9`n                 [7] Oracle Linux 8.5`n                 [8] SLES 12 SP2`n%homeWSLUnavailable1::REP:=[9] SLES 12 SP5%`n`n                 [N] Next Page`n                 [P] Previous Page`n                 [0] Return to Menu`n                 [X] Exit`n                                    Page 2/3`n           __________________________________________________________`n`n           Choose a menu option: """ -NoNewLine; [Console]::CursorVisible = $True; CHOICE /C  %homeWSL2Ch% /N /M  %BS%; [Console]::CursorVisible = $False; EXIT $LastExitCode"
	IF "%homeWSL2Ch%"=="12345678NP0X" (
		IF %ERRORLEVEL%==1 SET "wslDistro=openSUSE-42" & SET "wslGroups=users" & GOTO %wslMenuLoc%
		IF %ERRORLEVEL%==2 SET "wslDistro=openSUSE-Tumbleweed" & SET "wslGroups=users" & GOTO %wslMenuLoc%
		IF %ERRORLEVEL%==3 SET "wslDistro=openSUSE-Leap-15-1" & SET "wslGroups=users" & GOTO %wslMenuLoc%
		IF %ERRORLEVEL%==4 SET "wslDistro=openSUSE-Leap-15.2" & SET "wslGroups=users" & GOTO %wslMenuLoc%
		IF %ERRORLEVEL%==5 SET "wslDistro=openSUSE-Leap-15.3" & SET "wslGroups=users" & GOTO %wslMenuLoc%
		IF %ERRORLEVEL%==6 SET "wslDistro=OracleLinux_7_9" & SET "wslGroups=adm,wheel,cdrom" & GOTO %wslMenuLoc%
		IF %ERRORLEVEL%==7 SET "wslDistro=OracleLinux_8_5" & SET "wslGroups=adm,wheel,cdrom" & GOTO %wslMenuLoc%
		IF %ERRORLEVEL%==8 SET "wslDistro=SLES-12" & SET "wslGroups=users" & GOTO %wslMenuLoc%
		IF %ERRORLEVEL%==9 GOTO WSL-DISTROMENUP3
		IF %ERRORLEVEL%==10 GOTO WSL-DISTROMENUP1
		IF %ERRORLEVEL%==11 GOTO HOME-MAINMENU
		IF %ERRORLEVEL%==12 EXIT /B 0
	)
	IF %ERRORLEVEL%==1 SET "wslDistro=openSUSE-42" & SET "wslGroups=users" & GOTO %wslMenuLoc%
	IF %ERRORLEVEL%==2 SET "wslDistro=openSUSE-Tumbleweed" & SET "wslGroups=users" & GOTO %wslMenuLoc%
	IF %ERRORLEVEL%==3 SET "wslDistro=openSUSE-Leap-15-1" & SET "wslGroups=users" & GOTO %wslMenuLoc%
	IF %ERRORLEVEL%==4 SET "wslDistro=openSUSE-Leap-15.2" & SET "wslGroups=users" & GOTO %wslMenuLoc%
	IF %ERRORLEVEL%==5 SET "wslDistro=openSUSE-Leap-15.3" & SET "wslGroups=users" & GOTO %wslMenuLoc%
	IF %ERRORLEVEL%==6 SET "wslDistro=OracleLinux_7_9" & SET "wslGroups=adm,wheel,cdrom" & GOTO %wslMenuLoc%
	IF %ERRORLEVEL%==7 SET "wslDistro=OracleLinux_8_5" & SET "wslGroups=adm,wheel,cdrom" & GOTO %wslMenuLoc%
	IF %ERRORLEVEL%==8 SET "wslDistro=SLES-12" & SET "wslGroups=users" & GOTO %wslMenuLoc%
	IF %ERRORLEVEL%==9 SET "wslDistro=SUSE-Linux-Enterprise-Server-12-SP5" & SET "wslGroups=users" & GOTO %wslMenuLoc%
	IF %ERRORLEVEL%==10 GOTO WSL-DISTROMENUP3
	IF %ERRORLEVEL%==11 GOTO WSL-DISTROMENUP1
	IF %ERRORLEVEL%==12 GOTO HOME-MAINMENU
	IF %ERRORLEVEL%==13 EXIT /B 0

:WSL-DISTROMENUP3

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO. & ECHO.
POWERSHELL -NoP -C "Write-Host """%homeWSLUnavailable2::REP:=[1] SLES 15 SP1%`n                 [2] SLES 15 SP2`n                 [3] SLES 15 SP3`n`n                 [P] Previous Page`n                 [0] Return to Menu`n                 [X] Exit`n                                    Page 3/3`n           __________________________________________________________`n`n           Choose a menu option: """ -NoNewLine; [Console]::CursorVisible = $True; CHOICE /C %homeWSL3Ch% /N /M  %BS%; [Console]::CursorVisible = $False; EXIT $LastExitCode"
	IF "%homeWSL3Ch%"=="23P0X" (
		IF %ERRORLEVEL%==1 SET "wslDistro=SUSE-Linux-Enterprise-Server-15-SP2" & SET "wslGroups=users" & GOTO %wslMenuLoc%
		IF %ERRORLEVEL%==2 SET "wslDistro=SUSE-Linux-Enterprise-Server-15-SP3" & SET "wslGroups=users" & GOTO %wslMenuLoc%
		IF %ERRORLEVEL%==3 GOTO WSL-DISTROMENUP2
		IF %ERRORLEVEL%==4 GOTO HOME-MAINMENU
		IF %ERRORLEVEL%==5 EXIT /B 0
	)
	IF %ERRORLEVEL%==1 SET "wslDistro=SLES-15-SP1" & SET "wslGroups=users" & GOTO %wslMenuLoc%
	IF %ERRORLEVEL%==2 SET "wslDistro=SUSE-Linux-Enterprise-Server-15-SP2" & SET "wslGroups=users" & GOTO %wslMenuLoc%
	IF %ERRORLEVEL%==3 SET "wslDistro=SUSE-Linux-Enterprise-Server-15-SP3" & SET "wslGroups=users" & GOTO %wslMenuLoc%
	IF %ERRORLEVEL%==4 GOTO WSL-DISTROMENUP2
	IF %ERRORLEVEL%==5 GOTO HOME-MAINMENU
	IF %ERRORLEVEL%==6 EXIT /B 0

:WSL-DISTROINSTALL

IF "%altRun%"=="true" GOTO ALTPARENT-WSL-DISTROINSTALL

SETLOCAL

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO.

IF "%wslUnattend%"=="true" GOTO :WSL-DISTROUNATTEND

CALL :AUX-GENRND "7"

IF NOT "%adminPrivs%"=="false" (
	DISM /Online /Get-FeatureInfo:Microsoft-Windows-Subsystem-Linux /English | FINDSTR /x /c:"State : Enabled" > NUL 2>&1
		IF ERRORLEVEL 1 (
			POWERSHELL -NoP -C "Write-Host """`n`n                                WSL is disabled.""" -ForegroundColor Red; Write-Host """           __________________________________________________________`n`n           Would you like to enable it now? ^(Y/N^): """ -NoNewLine; [Console]::CursorVisible = $True; CHOICE /C YN /N /M '%BS%'; [Console]::CursorVisible = $False; EXIT $LastExitCode"
				IF ERRORLEVEL 2 ENDLOCAL & GOTO HOME-WSL
				IF ERRORLEVEL 1 ENDLOCAL & GOTO WSL-ENABLE
		)
	WHERE wsl.exe > NUL 2>&1
		IF ERRORLEVEL 1 CALL :AUX-RETURN "A restart is required for WSL functionality." "HOME-WSL" -E
) ELSE (
	WHERE wsl.exe > NUL 2>&1
		IF ERRORLEVEL 1 CALL :AUX-RETURN "WSL is disabled." "HOME-WSL" -E
)

POWERSHELL -NoP -C "[console]::OutputEncoding = [Text.UnicodeEncoding]::Unicode; WSL -l -q | FINDSTR /X /c:'%wslDistro%'">NUL 2>&1 && SET "distroReg=true" || SET "distroReg=false"

IF "%distroReg%"=="" CALL :AUX-RETURN "Failed to check registered distros." "HOME-WSL" -E

DIR /B /A:d "%LOCALAPPDATA%\AME-WSL" 2>&1 | FINDSTR /X /c:"%wslDistro%" > NUL 2>&1 && SET "distroFiles=true"

IF "%distroReg%"=="true" (
	CALL :AUX-RETURN "Distro is already installed." "HOME-WSL" -E
) ELSE (
	IF "%distroFiles%"=="true" (
		POWERSHELL -NoP -C "Write-Host """`n`n                  Traces of previous distro installation found.""" -ForegroundColor Red; Write-Host """           __________________________________________________________`n`n           Remove installation files and reinstall distro? ^(Y/N^): """ -NoNewLine; [Console]::CursorVisible = $True; CHOICE /C NY /N /M '%BS%'; [Console]::CursorVisible = $False; EXIT $LastExitCode"
			IF ERRORLEVEL 2 (
				CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO.
				ECHO. & ECHO                          Removing installation files...
				TIMEOUT /T 1 /NOBREAK > NUL 2>&1
				FOR /F "usebackq delims=" %%A IN (`DIR /B /A:d "%LOCALAPPDATA%\AME-WSL" 2^>^&1 ^| FINDSTR /X /c:"%wslDistro%"`) DO (
					FOR /F "usebackq delims=" %%B IN (`DIR /S /B "%LOCALAPPDATA%\AME-WSL\%%A\*.exe" 2^>^&1`) DO (
						FOR /F "usebackq delims=" %%C IN (`POWERSHELL -NoP -C "Get-Process | Where-Object {$_.Path -eq '%%~B'} | Select-Object -ExpandProperty Id" 2^>^&1`) DO (
							TASKKILL /F /T /PID "%%~C" > NUL 2>&1
						)
					)
					RMDIR /Q /S "%LOCALAPPDATA%\AME-WSL\%%A" > NUL
				)
			) ELSE (
				TASKKILL /F /T /PID "%distroAltPID%" > NUL 2>&1
				ENDLOCAL & GOTO HOME-WSL
			)
	)
)

IF "%wslDistro%"=="Alpine" (
	SET "wslLShell=/bin/ash"
	SET "sudo="
) ELSE (
	SET "wslLShell=/bin/bash"
	SET "sudo=sudo "
)

IF NOT "%wslUnattendRun%"=="true" (
	POWERSHELL -NoP -C "Write-Host """`n           A Linux distro must be downloaded`n           Continue? ^(Y/N^): """ -NoNewLine; [Console]::CursorVisible = $True; CHOICE /C YN /N /M '%BS%'; [Console]::CursorVisible = $False; EXIT $LastExitCode"
		IF ERRORLEVEL 2 ENDLOCAL & GOTO HOME-MAINMENU
)

ECHO. & ECHO                             Fetching download link...

IF "%wslUnattendRun%"=="true" CALL :AUX-NETWORKCHECK -L

CALL :AUX-FETCHLINK "HOME-WSL" "%wslDistro%" -Download "%TEMP%\[amecs]-%wslDistro%%rndOut%.zip" "Downloading distro"
	IF %ERRORLEVEL% EQU 5 CALL :AUX-RETURN "%fetchMsgOut%" "HOME-WSL" -E

ECHO. & ECHO                       Preparing distro for installation...

IF NOT EXIST "%LOCALAPPDATA%\AME-WSL" MKDIR "%LOCALAPPDATA%\AME-WSL"
RMDIR /Q /S "%LOCALAPPDATA%\AME-WSL\%wslDistro%%rndOut%-Tmp" > NUL 2>&1
MKDIR "%LOCALAPPDATA%\AME-WSL\%wslDistro%%rndOut%-Tmp"

POWERSHELL -NoP -C "Expand-Archive -LiteralPath '%TEMP%\[amecs]-%wslDistro%%rndOut%.zip' -DestinationPath '%LOCALAPPDATA%\AME-WSL\%wslDistro%%rndOut%-Tmp'"
DEL /Q /F "%TEMP%\[amecs]-%wslDistro%%rndOut%.zip"
FOR /F "usebackq delims=" %%A IN (`DIR /B "%LOCALAPPDATA%\AME-WSL\%wslDistro%%rndOut%-Tmp" ^| FINDSTR /i ".*_x64\.appx .*_x64\.msix .*\.exe"`) DO (
	IF /i "%%~xA"==".exe" (
		RENAME "%LOCALAPPDATA%\AME-WSL\%wslDistro%%rndOut%-Tmp" "%wslDistro%"
		SET "wslExe=%LOCALAPPDATA%\AME-WSL\%wslDistro%\%%~A"
		SET "wslExeName=%%~nxA"
	) ELSE (
		RENAME "%LOCALAPPDATA%\AME-WSL\%wslDistro%%rndOut%-Tmp\%%~A" "%%~nA.zip"
		MKDIR "%LOCALAPPDATA%\AME-WSL\%wslDistro%"
		POWERSHELL -NoP -C "$ProgressPreference = 'SilentlyContinue'; Expand-Archive -LiteralPath '%LOCALAPPDATA%\AME-WSL\%wslDistro%%rndOut%-Tmp\%%~nA.zip' -DestinationPath '%LOCALAPPDATA%\AME-WSL\%wslDistro%'"
		RMDIR /Q /S "%LOCALAPPDATA%\AME-WSL\%wslDistro%%rndOut%-Tmp"

		FOR /F "usebackq delims=" %%B IN (`DIR /B "%LOCALAPPDATA%\AME-WSL\%wslDistro%" ^| FINDSTR /i ".*\.exe"`) DO (
			IF /i "%%~B"=="PengwinUI.exe" (
				COPY "%LOCALAPPDATA%\AME-WSL\%wslDistro%\DistroLauncher\pengwin.exe" "%LOCALAPPDATA%\AME-WSL\%wslDistro%\pengwin.exe" /y > NUL
				SET "wslExe=%LOCALAPPDATA%\AME-WSL\%wslDistro%\pengwin.exe"
				SET "wslExeName=pengwin.exe"
			) ELSE (
				SET "wslExe=%LOCALAPPDATA%\AME-WSL\%wslDistro%\%%~B"
				SET "wslExeName=%%~nxB"
			)
		)
	)
)

IF "%wslExe%"=="" (
	DEL /Q /F "%TEMP%\%wslDistro%%rndOut%.zip" > NUL
	RMDIR /Q /S "%LOCALAPPDATA%\AME-WSL\%wslDistro%%rndOut%-Tmp" > NUL 2>&1
	RMDIR /Q /S "%LOCALAPPDATA%\AME-WSL\%wslDistro%" > NUL 2>&1
	CALL :AUX-RETURN "Failed to locate distro executable." "HOME-WSL" -E
)

ECHO. & ECHO                    Installing distro, this may take awhile...

SET /A "count0=0"
SET /A "count1=0"

POWERSHELL -NoP -C "(Start-Process 'CMD' -ArgumentList '/K','POWERSHELL -NoP -C """"(Start-Process ''%wslExe:'=''''%'' -NoNewWindow -PassThru).Id | Out-File -LiteralPath ''%TEMP:'=''''%\[amecs]-DistroPID%rndOut%.txt'' -Encoding default""""' -WindowStyle Hidden -PassThru).Id" 1> "%TEMP%\[amecs]-DistroHostPID%rndOut%.txt"

:WSL-DISTROPROGRESS

TIMEOUT /T 2 /NOBREAK > NUL

IF %count0% GEQ 15 (
	TASKKILL /F /T /PID "%distroPID%" /PID "%distroHostPID%" > NUL 2>&1
	CALL :AUX-RETURN "Failed to fetch distro process IDs." "HOME-WSL" -C -E
)

IF %count1% GTR 500 (
	TASKKILL /F /T /PID "%distroPID%" /PID "%distroHostPID%" > NUL 2>&1
	CALL :AUX-RETURN "Distro installation timed out." "HOME-WSL" -C -E
)

FINDSTR "1 2 3 4 5 6 7 8 9 0" "%TEMP%\[amecs]-DistroHostPID%rndOut%.txt" > NUL 2>&1
	IF %ERRORLEVEL% NEQ 0 (
		SET /A "count0=%count0%+1"
		GOTO WSL-DISTROPROGRESS
	) ELSE (
		SET /P "distroHostPID=" < "%TEMP%\[amecs]-DistroHostPID%rndOut%.txt"
	)
FINDSTR "1 2 3 4 5 6 7 8 9 0" "%TEMP%\[amecs]-DistroPID%rndOut%.txt" > NUL 2>&1
	IF %ERRORLEVEL% NEQ 0 (
		SET /A "count0=%count0%+1"
		GOTO WSL-DISTROPROGRESS
	) ELSE (
		SET /P "distroPID=" < "%TEMP%\[amecs]-DistroPID%rndOut%.txt"
	)

POWERSHELL -NoP -C "[console]::OutputEncoding = [Text.UnicodeEncoding]::Unicode; WSL -l -q | FINDSTR /X /c:'%wslDistro%'" > NUL 2>&1
	IF %ERRORLEVEL% EQU 0 (
		TASKKILL /F /T /PID "%distroPID%" /PID "%distroHostPID%" > NUL 2>&1
		GOTO WSL-DISTROCONFIG
	)

TASKLIST /FI "IMAGENAME eq cmd.exe" /FI "PID eq %distroHostPID%" 2>&1 | FINDSTR /i /c:"cmd.exe">NUL 2>&1 || SET /A "count1=%count1%+50"

SET /A "count1=%count1%+1"
GOTO WSL-DISTROPROGRESS


:WSL-DISTROCONFIG

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO.

CALL :AUX-INPUTLOOP "wslRootPass" "Enter new root password" "0" "1" -Secure

CALL :AUX-INPUTLOOP "wslUser" "Enter new UNIX username" "%inpLenOut%" "2"

IF NOT "%wslUser%"==":None:" CALL :AUX-INPUTLOOP "wslUserPass" "Enter new UNIX password" "%inpLenOut%" "1" -Secure

IF NOT "%wslRootPass%"==":None:" SET "wslRootArg=echo -e """"%wslRootPass%\n%wslRootPass%"""" | passwd """"root"""" && "

IF NOT "%wslUser%"==":None:" SET "wslUserArg=useradd -m -G %wslGroups% -s %wslLShell% """"%wslUser%"""" && echo -e """"\n[user]\ndefault=%wslUser%"""" >> """"/etc/wsl.conf"""" && "

IF NOT "%wslUserPass%"==":None:" SET "wslUserPassArg=echo -e """"%wslUserPass%\n%wslUserPass%"""" | passwd """"%wslUser%"""" && "

POWERSHELL -NoP -C "Write-Host -NoNewLine '%wslRootArg%%sudo%%wslUserArg%%wslUserPassArg%echo """"Blank""""'" > "%TEMP%\[amecs]-WSLLin%rndOut%.txt" 2>&1

WSL -d %wslDistro% < "%TEMP%\[amecs]-WSLLin%rndOut%.txt" > NUL 2>&1
WSL -t %wslDistro% > NUL 2>&1

:WSL-DISTROCOMPLETE

ECHO "%wslDistro%" | FINDSTR /b /c:""""SUSE-Linux-Enterprise" /c:""""SLES-" > NUL 2>&1
	IF %ERRORLEVEL% EQU 0  CALL :AUX-RETURN "Distro installed successfully" -H -L "Use the SUSEConnect command to register this distro." -C

CALL :AUX-RETURN "Distro installed successfully" -H -C

:WSL-DISTROREMOVE

IF "%altRun%"=="true" GOTO ALTPARENT-WSL-DISTROREMOVE

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO.

POWERSHELL -NoP -C "[console]::OutputEncoding = [Text.UnicodeEncoding]::Unicode; WSL -l -q | FINDSTR /X /c:'%wslDistro%'">NUL 2>&1 && SET "distroReg=true" || SET "distroReg=false"
IF "%distroReg%"=="" (
	TASKKILL /F /T /PID "%distroAltPID%" > NUL 2>&1
	CALL :AUX-RETURN "Failed to check registered distros." "HOME-WSL" -E
)

DIR /B /A:d "%LOCALAPPDATA%\AME-WSL" 2>&1 | FINDSTR /X /c:"%wslDistro%" > NUL 2>&1 && SET "distroRemFiles=true"

IF NOT "%distroReg%"=="true" (
	TASKKILL /F /T /PID "%distroAltPID%" > NUL 2>&1
	IF NOT "%distroRemFiles%"=="true" CALL :AUX-RETURN "Distro is not installed." "HOME-WSL" -E
)

POWERSHELL -NoP -C "Write-Host """`n           Are you sure you want to remove this distro? ^(Y/N^): """ -NoNewLine; [Console]::CursorVisible = $True; CHOICE /C YN /N /M '%BS%'; [Console]::CursorVisible = $False; EXIT $LastExitCode"
	IF %ERRORLEVEL%==2 TASKKILL /F /T /PID "%distroAltPID%">NUL 2>&1 & ENDLOCAL & GOTO HOME-MAINMENU

ECHO. & ECHO                                Removing distro...

IF "%distroReg%"=="true" (
	WSL --unregister %wslDistro% > NUL 2>&1
		IF NOT ERRORLEVEL 0 CALL :AUX-RETURN "Failed to unregister distro." "HOME-WSL" -E
)
FOR /F "usebackq delims=" %%A IN (`DIR /B /A:d "%LOCALAPPDATA%\AME-WSL" 2^>^&1 ^| FINDSTR /X /c:"%wslDistro%"`) DO (
	FOR /F "usebackq delims=" %%B IN (`DIR /S /B "%LOCALAPPDATA%\AME-WSL\%%A\*.exe" 2^>^&1`) DO (
		FOR /F "usebackq delims=" %%C IN (`POWERSHELL -NoP -C "Get-Process | Where-Object {$_.Path -eq '%%~B'} | Select-Object -ExpandProperty Id" 2^>^&1`) DO (
			TASKKILL /F /T /PID "%%~C" > NUL 2>&1
		)
	)
	RMDIR /Q /S "%LOCALAPPDATA%\AME-WSL\%%A" > NUL
)
FOR /F "usebackq delims=" %%A IN (`DIR /B "%LOCALAPPDATA%\AME-WSL" 2^>^&1`) DO SET "contentsEmpty=false"
IF NOT "%contentsEmpty%"=="false" RMDIR /Q /S "%LOCALAPPDATA%\AME-WSL" > NUL 2>&1

TIMEOUT /T 1 /NOBREAK > NUL

CALL :AUX-RETURN "Distro removed successfully" -H

:WSL-DISTROUNATTEND

CALL :AUX-NETWORKCHECK
	IF %ERRORLEVEL% NEQ 0 CALL :AUX-RETURN "Internet is required for an unattended installation." "HOME-WSL" -E

SET "unattendMsg=AutoLogon"
REG QUERY "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon" /v "DefaultUsername" 2>&1 | FINDSTR /c:"%currentUsername%" > NUL 2>&1
	IF %ERRORLEVEL% EQU 0 (
		REG QUERY "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon" /v "AutoAdminLogon" 2>&1 | FINDSTR /R /X /C:".*AutoAdminLogon[ ].*REG_SZ.*[ ]1" > NUL 2>&1
			IF NOT ERRORLEVEL 1 SET "autoLogon=enabled" & SET "unattendMsg=checks"
	)
CALL :AUX-CENTERTEXT "Unattended mode requires your password for %unattendMsg%"
POWERSHELL -NoP -C "Write-Host """`n%cenOut%"""; Write-Host '                  YOUR COMPUTER WILL RESTART DURING THE PROCESS' -ForegroundColor Red"

CALL :AUX-INPUTLOOP "userPassword" "Enter your password, or enter 'Cancel' to exit" "3" "9" -Secure
	IF %ERRORLEVEL% EQU 3 ENDLOCAL & GOTO HOME-MAINMENU

IF NOT "%userPassword%"=="" (
	POWERSHELL -NoP -C "$Pass = ConvertTo-SecureString -AsPlainText -String '%userPassword:'=''%' -Force; $Creds = New-Object System.Management.Automation.PSCredential '%currentUsername:'=''%',$Pass; Start-Process '%scriptPath:'=''%' -Credential $Creds -ArgumentList 'permsCheck' -WindowStyle Hidden" > NUL 2>&1
		IF ERRORLEVEL 1 CALL :AUX-RETURN "User %currentUsername% must be able to read and execute this script." "HOME-WSL" -E
)

IF %inpLenOut% GEQ 11 CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO. & SET /A "inpLenOut=0"

DISM /Online /Get-FeatureInfo /FeatureName:Microsoft-Windows-Subsystem-Linux /English | FINDSTR /I /c:"State : Enabled" > NUL 2>&1
	IF %ERRORLEVEL% NEQ 0 (
		ECHO. & ECHO                                  Enabling WSL...
		SET /A "inpLenOut=%inpLenOut%+2"

		DISM /Online /Enable-Feature /FeatureName:Microsoft-Windows-Subsystem-Linux -NoRestart /English | FINDSTR /I /c:"Error"
			IF %ERRORLEVEL% EQU 0 CALL :AUX-RETURN "Failed to enable WSL. (1)" "HOME-WSL" -E

		DISM /Online /Get-FeatureInfo /FeatureName:Microsoft-Windows-Subsystem-Linux /English | FINDSTR /I /c:"State : Disabled" > NUL 2>&1
			IF %ERRORLEVEL% EQU 0 CALL :AUX-RETURN "Failed to enable WSL. (2)" "HOME-WSL" -E
)

CALL :AUX-GENRND "7"
IF NOT "%userPassword%"=="" SET "userPassword=%userPassword:'=''%"

IF NOT "%autoLogon%"=="enabled" (
	IF %inpLenOut% GEQ 11 CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO. & SET /A "inpLenOut=0"

	ECHO. & ECHO                               Enabling AutoLogon...
	SET /A "inpLenOut=%inpLenOut%+2"

	CERTUTIL /f /decode "%scriptPath%" "%TEMP%\[amecs]-AutoLogon%rndOut%.exe" > NUL 2>&1

	POWERSHELL -NoP -C "Start-Process '%TEMP:'=''%\[amecs]-AutoLogon%rndOut%.exe' -ArgumentList '/DEL' -Wait -NoNewWindow" > NUL 2>&1

	IF NOT "%userPassword%"=="" (
		POWERSHELL -NoP -C "EXIT (Start-Process '%TEMP:'=''%\[amecs]-AutoLogon%rndOut%.exe' -ArgumentList '""""%currentUsername:'=''%""""','""""%userPassword:'=''%""""','1','/DISABLECAD' -Wait -NoNewWindow).ExitCode" > NUL 2>&1
	) ELSE (
		POWERSHELL -NoP -C "EXIT (Start-Process '%TEMP:'=''%\[amecs]-AutoLogon%rndOut%.exe' -ArgumentList '""""%currentUsername:'=''%""""','""""""','1','/DISABLECAD' -Wait -NoNewWindow).ExitCode" > NUL 2>&1
	)
		IF ERRORLEVEL 1 (
			POWERSHELL -NoP -C "Start-Process '%TEMP:'=''%\[amecs]-AutoLogon%rndOut%.exe' -ArgumentList '/DEL' -NoNewWindow" > NUL 2>&1
			IF ERRORLEVEL 7 CALL :AUX-RETURN "Failed to enable AutoLogon. (4)" -H -E -C
			CALL :AUX-RETURN "Failed to enable AutoLogon. (5)" -H -E -C
		)
	SET "userPassword="

	REG QUERY "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon" /v "DefaultUsername" 2>&1 | FINDSTR /I /E /c:"    %currentUsername%" > NUL 2>&1
		IF NOT ERRORLEVEL 1 (
			REG QUERY "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon" /v "AutoAdminLogon" 2>&1 | FINDSTR /R /X /C:".*AutoAdminLogon[ ].*REG_SZ.*[ ]1" > NUL 2>&1
				IF ERRORLEVEL 1 POWERSHELL -NoP -C "Start-Process '%TEMP:'=''%\[amecs]-AutoLogon%rndOut%.exe' -ArgumentList '/DEL' -NoNewWindow">NUL 2>&1 & CALL :AUX-RETURN "Failed to enable AutoLogon. (6)" -H -E -C
		) ELSE (
			IF ERRORLEVEL 1 POWERSHELL -NoP -C "Start-Process '%TEMP:'=''%\[amecs]-AutoLogon%rndOut%.exe' -ArgumentList '/DEL' -NoNewWindow">NUL 2>&1 & CALL :AUX-RETURN "Failed to enable AutoLogon. (7)" -H -E -C
		)
)

IF %inpLenOut% GEQ 11 CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO.

ECHO. & ECHO                    Creating task and restarting computer...

IF "%userStatus%"=="Elevated" (
	SCHTASKS /CREATE /TN "[amecs]-WSLUNATTENDSTART" /TR "CMD /C 'SCHTASKS /DELETE /TN '[amecs]-WSLUNATTENDSTART' /F>NUL&'%scriptPath%' wslUnattend '%wslDistro%' '%wslGroups%''" /SC ONLOGON /RL HIGHEST /RU "%currentUsername%" /F> NUL 2>&1 < NUL
		IF ERRORLEVEL 1 POWERSHELL -NoP -C "Start-Process '%TEMP:'=''%\[amecs]-AutoLogon%rndOut%.exe' -ArgumentList '/DEL' -NoNewWindow">NUL 2>&1 & SCHTASKS /DELETE /TN "[amecs]-WSLUNATTENDSTART" /F>NUL 2>&1 & CALL :AUX-RETURN "Failed to create scheduled task. (1)" -H -E -C
	POWERSHELL -NoP -C "$TaskSet = New-ScheduledTaskSettingsSet -AllowStartIfOnBatteries -DontStopIfGoingOnBatteries; Set-ScheduledTask -TaskName '[amecs]-WSLUNATTENDSTART' -Settings $TaskSet" > NUL 2>&1
		IF ERRORLEVEL 1 POWERSHELL -NoP -C "Start-Process '%TEMP:'=''%\[amecs]-AutoLogon%rndOut%.exe' -ArgumentList '/DEL' -NoNewWindow">NUL 2>&1 & SCHTASKS /DELETE /TN "[amecs]-WSLUNATTENDSTART" /F>NUL 2>&1 & CALL :AUX-RETURN "Failed to create scheduled task. (2)" -H -E -C
) ELSE (
	EVENTCREATE /L APPLICATION /T INFORMATION /ID 10 /D "Set up event source." /SO "AMECS" > NUL 2>&1
		IF ERRORLEVEL 1 POWERSHELL -NoP -C "Start-Process '%TEMP:'=''%\[amecs]-AutoLogon%rndOut%.exe' -ArgumentList '/DEL' -NoNewWindow">NUL 2>&1 & CALL :AUX-RETURN "Failed to create scheduled task. (1)" -H -E -C

	SCHTASKS /CREATE /TN "[amecs]-WSLUNATTENDTASKDEL" /TR "CMD /C 'SCHTASKS /DELETE /TN '[amecs]-WSLUNATTENDSTART' /F & SCHTASKS /DELETE /TN '[amecs]-WSLUNATTENDTASKDEL' /F & POWERSHELL -NoP -C 'Remove-EventLog -Source ''''AMECS'''''" /sc ONEVENT /MO "*[System[Provider[@Name='AMECS'] and EventID=10]]" /EC Application /RL HIGHEST /RU "SYSTEM" /F> NUL 2>&1
		IF ERRORLEVEL 1 POWERSHELL -NoP -C "Start-Process '%TEMP:'=''%\[amecs]-AutoLogon%rndOut%.exe' -ArgumentList '/DEL' -NoNewWindow">NUL 2>&1 & SCHTASKS /DELETE /TN "[amecs]-WSLUNATTENDTASKDEL" /F>NUL 2>&1 & CALL :AUX-RETURN "Failed to create scheduled task. (2)" -H -E -C
	POWERSHELL -NoP -C "$TaskSet = New-ScheduledTaskSettingsSet -AllowStartIfOnBatteries -DontStopIfGoingOnBatteries; Set-ScheduledTask -TaskName '[amecs]-WSLUNATTENDTASKDEL' -Settings $TaskSet" > NUL 2>&1
		IF ERRORLEVEL 1 POWERSHELL -NoP -C "Start-Process '%TEMP:'=''%\[amecs]-AutoLogon%rndOut%.exe' -ArgumentList '/DEL' -NoNewWindow">NUL 2>&1 & SCHTASKS /DELETE /TN "[amecs]-WSLUNATTENDTASKDEL" /F>NUL 2>&1 & CALL :AUX-RETURN "Failed to create scheduled task. (3)" -H -E -C

	SCHTASKS /CREATE /TN "[amecs]-WSLUNATTENDSTART" /TR "CMD /C 'EVENTCREATE /L APPLICATION /T INFORMATION /ID 10 /D TR /SO AMECS>NUL&'%scriptPath%' wslUnattend '%wslDistro%' '%wslGroups%''" /SC ONLOGON /RL HIGHEST /RU "%currentUsername%" /F > NUL 2>&1
		IF ERRORLEVEL 1 POWERSHELL -NoP -C "Start-Process '%TEMP:'=''%\[amecs]-AutoLogon%rndOut%.exe' -ArgumentList '/DEL' -NoNewWindow">NUL 2>&1 & SCHTASKS /DELETE /TN "[amecs]-WSLUNATTENDSTART" /F>NUL 2>&1 & SCHTASKS /DELETE /TN "[amecs]-WSLUNATTENDTASKDEL" /F>NUL 2>&1 & CALL :AUX-RETURN "Failed to create scheduled task. (4)" -H -E -C
	POWERSHELL -NoP -C "$TaskSet = New-ScheduledTaskSettingsSet -AllowStartIfOnBatteries -DontStopIfGoingOnBatteries; Set-ScheduledTask -TaskName '[amecs]-WSLUNATTENDSTART' -Settings $TaskSet" > NUL 2>&1
		IF ERRORLEVEL 1 POWERSHELL -NoP -C "Start-Process '%TEMP:'=''%\[amecs]-AutoLogon%rndOut%.exe' -ArgumentList '/DEL' -NoNewWindow">NUL 2>&1 & SCHTASKS /DELETE /TN "[amecs]-WSLUNATTENDSTART" /F>NUL 2>&1 & SCHTASKS /DELETE /TN "[amecs]-WSLUNATTENDTASKDEL" /F>NUL 2>&1 & CALL :AUX-RETURN "Failed to create scheduled task. (5)" -H -E -C
)

DEL /Q /F "%TEMP%\[amecs]*%rndOut%.*" > NUL 2>&1

SHUTDOWN -R -T 0 & EXIT 0
REM --------------------------WSL-END---------------------------



REM ---------------------------NOTIF----------------------------
:NOTIF-ENABLE

SETLOCAL

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO.

ECHO. & ECHO                         Enabling desktop notifications...

TIMEOUT /T 2 /NOBREAK > NUL
REG ADD "HKU\%userSID%\SOFTWARE\Microsoft\Windows\CurrentVersion\PushNotifications" /v ToastEnabled /t REG_DWORD /d 1 /f > NUL
CALL :AUX-RETURN "Desktop notifications are now enabled" -H R:L.sign-out -L "A sign-out is required to take effect."

:NOTIF-DISABLE

SETLOCAL

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO.

ECHO. & ECHO                        Disabling desktop notifications...

TIMEOUT /T 2 /NOBREAK > NUL
REG ADD "HKU\%userSID%\SOFTWARE\Microsoft\Windows\CurrentVersion\PushNotifications" /v ToastEnabled /t REG_DWORD /d 0 /f > NUL
CALL :AUX-RETURN "Desktop notifications are now disabled" -H R:L.sign-out -L "A sign-out is required to take effect."
REM -------------------------NOTIF-END-------------------------



REM -------------------------NOTIFCEN--------------------------
:NOTIFCEN-ENABLE

SETLOCAL

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO.

ECHO. & ECHO                          Enabling Notification Center...

TIMEOUT /T 2 /NOBREAK > NUL
REG ADD "HKU\%userSID%\Software\Policies\Microsoft\Windows\Explorer" /v DisableNotificationCenter /t REG_DWORD /d 0 /f > NUL
CALL :AUX-RETURN "The Notification Center is now enabled" -H R:L.sign-out -L "A sign-out is required to take effect."

:NOTIFCEN-DISABLE

SETLOCAL

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO.

ECHO. & ECHO                         Disabling Notification Center...

TIMEOUT /T 2 /NOBREAK > NUL
REG ADD "HKU\%userSID%\Software\Policies\Microsoft\Windows\Explorer" /v DisableNotificationCenter /t REG_DWORD /d 1 /f > NUL
CALL :AUX-RETURN "The Notification Center is now disabled" -H R:L.sign-out -L "A sign-out is required to take effect."
REM -----------------------NOTIFCEN-END------------------------



REM -------------------------HIBERNATE-------------------------
:HIBERNATE-ENABLE

SETLOCAL

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO.

ECHO. & ECHO                              Enabling hibernation...

TIMEOUT /T 2 /NOBREAK > NUL
POWERCFG /HIBERNATE /TYPE FULL > NUL 2>&1
	IF %ERRORLEVEL% NEQ 0 CALL :AUX-RETURN "Failed to enable hibernation." "HOME-EXTRA" -L "Hibernation may not be supported by your firmware." -E

CALL :AUX-RETURN "Hibernation is now enabled" -H

:HIBERNATE-DISABLE

SETLOCAL

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO.

ECHO. & ECHO                             Disabling hibernation...

TIMEOUT /T 2 /NOBREAK > NUL
POWERCFG /HIBERNATE OFF > NUL 2>&1
	IF %ERRORLEVEL% NEQ 0 CALL :AUX-RETURN "Failed to disable hibernation." "HOME-EXTRA" -E

CALL :AUX-RETURN "Hibernation is now disabled" -H
REM -----------------------HIBERNATE-END-----------------------



REM ----------------------------WSH-----------------------------
:WSH-ENABLE

SETLOCAL

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO.

ECHO. & ECHO                          Enabling Windows Script Host...

TIMEOUT /T 2 /NOBREAK > NUL
REG ADD "HKU\%userSID%\SOFTWARE\Microsoft\Windows Script Host\Settings" /v Enabled /t REG_DWORD /d 1 /f > NUL
REG ADD "HKCU\SOFTWARE\Microsoft\Windows Script Host\Settings" /v Enabled /t REG_DWORD /d 1 /f > NUL
REG ADD "HKLM\SOFTWARE\Microsoft\Windows Script Host\Settings" /v Enabled /t REG_DWORD /d 1 /f > NUL

CALL :AUX-RETURN "WSH is now enabled" -H
REM R:L.sign-out -L "A sign-out is required to complete the setup."

:WSH-DISABLE

SETLOCAL

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO.

ECHO. & ECHO                         Disabling Windows Script Host...

TIMEOUT /T 2 /NOBREAK > NUL
REG ADD "HKU\%userSID%\SOFTWARE\Microsoft\Windows Script Host\Settings" /v Enabled /t REG_DWORD /d 0 /f > NUL
REG ADD "HKCU\SOFTWARE\Microsoft\Windows Script Host\Settings" /v Enabled /t REG_DWORD /d 0 /f > NUL
REG ADD "HKLM\SOFTWARE\Microsoft\Windows Script Host\Settings" /v Enabled /t REG_DWORD /d 0 /f > NUL

CALL :AUX-RETURN "WSH is now disabled" -H
REM "R:R -T 0.restart" -L "A sign-out is required to complete."
REM --------------------------WSH-END--------------------------



REM ----------------------------VBS-----------------------------
:VBS-ENABLE

SETLOCAL

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO.

ECHO. & ECHO                          Enabling Visual Basic Script...

TIMEOUT /T 2 /NOBREAK > NUL
ASSOC .vbs=VBSFile> NUL

CALL :AUX-RETURN "VBS is now enabled" -H

:VBS-DISABLE

SETLOCAL

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO.

ECHO. & ECHO                         Disabling Visual Basic Script...

TIMEOUT /T 2 /NOBREAK > NUL
ASSOC .vbs=> NUL

CALL :AUX-RETURN "VBS is now disabled" -H
REM --------------------------VBS-END--------------------------



REM ---------------------------NCSI----------------------------
:NCSI-ENABLE

SETLOCAL

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO.

ECHO. & ECHO                          Enabling NCSI Active Probing...

TIMEOUT /T 2 /NOBREAK > NUL
REG ADD "HKLM\SYSTEM\CurrentControlSet\Services\NlaSvc\Parameters\Internet" /v EnableActiveProbing /t REG_DWORD /d 1 /f > NUL
CALL :AUX-RETURN "NCSI Active Probing is now enabled" -H -R -L "A restart is required to take effect."

:NCSI-DISABLE

SETLOCAL

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO.

ECHO. & ECHO                         Disabling NCSI Active Probing...

TIMEOUT /T 2 /NOBREAK > NUL
REG ADD "HKLM\SYSTEM\CurrentControlSet\Services\NlaSvc\Parameters\Internet" /v EnableActiveProbing /t REG_DWORD /d 0 /f > NUL
CALL :AUX-RETURN "NCSI Active Probing is now disabled" -H -R -L "A restart is required to take effect."
REM -------------------------NCSI-END--------------------------



REM --------------------------NEWUSER--------------------------
:NEWUSER-MENU

SETLOCAL

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO. & ECHO.

POWERSHELL -NoP -C "Write-Host """                 [1] Create a New User`n                 [2] Remove Existing User`n`n                 [0] Return to Menu`n                 [X] Exit`n`n           __________________________________________________________`n`n           Choose a menu option: """ -NoNewLine; [Console]::CursorVisible = $True; CHOICE /C 120X /N /M '%BS%'; [Console]::CursorVisible = $False; EXIT $LastExitCode"
	IF %ERRORLEVEL%==1 GOTO NEWUSER-CREATE
	IF %ERRORLEVEL%==2 GOTO NEWUSER-REMOVE
	IF %ERRORLEVEL%==3 ENDLOCAL & GOTO HOME-MAINMENU
	IF %ERRORLEVEL%==4 EXIT /B 0

:NEWUSER-CREATE

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO.

CALL :AUX-INPUTLOOP "newUsername" "Enter new username, or 'Cancel' to quit" "0" "7"
	IF %ERRORLEVEL% EQU 3 ENDLOCAL & GOTO HOME-MAINMENU

CALL :AUX-INPUTLOOP "newPassword" "Enter new password, or 'Cancel' to quit" "%inpLenOut%" "6" -Secure
	IF %ERRORLEVEL% EQU 3 ENDLOCAL & GOTO HOME-MAINMENU

IF %inpLenOut% GEQ 11 CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO. & SET /A "inpLenOut=0"

ECHO. & ECHO                                 Creating user...

TIMEOUT /T 2 /NOBREAK > NUL 2>&1
NET user "%newUsername%" "%newPassword%" /add /y > NUL 2>&1
	IF %ERRORLEVEL% NEQ 0 NET user "%newUsername%" /delete /y>NUL 2>&1 & CALL :AUX-RETURN "Failed to add new user." "NEWUSER-MENU" -E

IF %inpLenOut% GEQ 9 CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO.

ECHO. & ECHO                              Configuring new user...

REM SCHTASKS /create /tn "[amecs]-NEWUSERREG" /tr "CMD /C 'FOR /F 'usebackq delims=' %%A IN (`REG QUERY HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Appx\AppxAllUserStore\InboxApplications^^&REG QUERY HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Appx\AppxAllUserStore\Config`) DO REG DELETE '%%A' /f'" /sc MONTHLY /f /rl HIGHEST /ru "SYSTEM" > NUL
	REM IF %ERRORLEVEL% NEQ 0 SCHTASKS /DELETE /TN "[amecs]-NEWUSERREG" /F>NUL 2>&1 & NET user "%newUsername%" /delete /y>NUL 2>&1 & CALL :AUX-RETURN "Failed to create scheduled task. (1)" -H -E
REM POWERSHELL -NoP -C "$TaskSet = New-ScheduledTaskSettingsSet -AllowStartIfOnBatteries -DontStopIfGoingOnBatteries; Set-ScheduledTask -TaskName '[amecs]-NEWUSERREG' -Settings $TaskSet" > NUL 2>&1
	REM IF %ERRORLEVEL% NEQ 0 SCHTASKS /DELETE /TN "[amecs]-NEWUSERREG" /F>NUL 2>&1 & NET user "%newUsername%" /delete /y>NUL 2>&1 & CALL :AUX-RETURN "Failed to create scheduled task. (2)" -H -E

REM SCHTASKS /run /tn "[amecs]-NEWUSERREG" > NUL
REM SCHTASKS /delete /tn "[amecs]-NEWUSERREG" /f > NUL

FOR /F "usebackq tokens=3 delims= " %%A IN (`SC queryex "AppReadiness" 2^>^&1 ^| FINDSTR /R /X /c:"[ ]*PID[ ]*:[ ].*"`) DO (
	IF NOT "%%A"=="0" TASKKILL /PID "%%A" /F > NUL 2>&1
	SC delete "AppReadiness" > NUL 2>&1
)

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
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\OpenShell\StartMenu\Settings" /v "SkipMetro" /t REG_DWORD /d 1 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\OpenShell\StartMenu\Settings" /v "MenuItems7" /t REG_MULTI_SZ /d "Item1.Command=user_files"\0"Item1.Settings=NOEXPAND"\0"Item2.Command=user_documents"\0"Item2.Settings=NOEXPAND"\0"Item3.Command=user_pictures"\0"Item3.Settings=NOEXPAND"\0"Item4.Command=user_music"\0"Item4.Settings=NOEXPAND"\0"Item5.Command=user_videos"\0"Item5.Settings=NOEXPAND"\0"Item6.Command=downloads"\0"Item6.Settings=NOEXPAND"\0"Item7.Command=homegroup"\0"Item7.Settings=ITEM_DISABLED"\0"Item8.Command=separator"\0"Item9.Command=games"\0"Item9.Settings=TRACK_RECENT|NOEXPAND|ITEM_DISABLED"\0"Item10.Command=favorites"\0"Item10.Settings=ITEM_DISABLED"\0"Item11.Command=recent_documents"\0"Item12.Command=computer"\0"Item12.Settings=NOEXPAND"\0"Item13.Command=network"\0"Item13.Settings=ITEM_DISABLED"\0"Item14.Command=network_connections"\0"Item14.Settings=ITEM_DISABLED"\0"Item15.Command=separator"\0"Item16.Command=control_panel"\0"Item16.Settings=TRACK_RECENT"\0"Item17.Command=pc_settings"\0"Item17.Settings=TRACK_RECENT"\0"Item18.Command=admin"\0"Item18.Settings=TRACK_RECENT|ITEM_DISABLED"\0"Item19.Command=devices"\0"Item19.Settings=ITEM_DISABLED"\0"Item20.Command=defaults"\0"Item20.Settings=ITEM_DISABLED"\0"Item21.Command=help"\0"Item21.Settings=ITEM_DISABLED"\0"Item22.Command=run"\0"Item23.Command=apps"\0"Item23.Settings=ITEM_DISABLED"\0"Item24.Command=windows_security"\0"Item24.Settings=ITEM_DISABLED"\0"" /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\OpenShell\StartMenu\Settings" /v "SkinOptionsW7" /t REG_MULTI_SZ /d "DARK_MAIN=0"\0"METRO_MAIN=0"\0"LIGHT_MAIN=0"\0"AUTOMODE_MAIN=1"\0"DARK_SUBMENU=0"\0"METRO_SUBMENU=0"\0"LIGHT_SUBMENU=0"\0"AUTOMODE_SUBMENU=1"\0"SUBMENU_SEPARATORS=1"\0"DARK_SEARCH=0"\0"METRO_SEARCH=0"\0"LIGHT_SEARCH=0"\0"AUTOMODE_SEARCH=1"\0"SEARCH_FRAME=1"\0"SEARCH_COLOR=0"\0"MODERN_SEARCH=1"\0"SEARCH_ITALICS=0"\0"NONE=0"\0"SEPARATOR=0"\0"TWO_TONE=1"\0"CLASSIC_SELECTOR=1"\0"HALF_SELECTOR=0"\0"CURVED_MENUSEL=1"\0"CURVED_SUBMENU=0"\0"SELECTOR_REVEAL=1"\0"TRANSPARENT=0"\0"OPAQUE_SUBMENU=1"\0"OPAQUE_MENU=0"\0"OPAQUE=0"\0"STANDARD=0"\0"SMALL_MAIN2=1"\0"SMALL_ICONS=0"\0"COMPACT_SUBMENU=0"\0"PRESERVE_MAIN2=0"\0"LESS_PADDING=0"\0"EXTRA_PADDING=1"\0"24_PADDING=0"\0"LARGE_PROGRAMS=0"\0"TRANSPARENT_SHUTDOWN=0"\0"OUTLINE_SHUTDOWN=0"\0"BUTTON_SHUTDOWN=1"\0"EXPERIMENTAL_SHUTDOWN=0"\0"LARGE_FONT=0"\0"CONNECTED_BORDER=1"\0"FLOATING_BORDER=0"\0"LARGE_SUBMENU=0"\0"LARGE_LISTS=0"\0"THIN_MAIN2=0"\0"EXPERIMENTAL_MAIN2=1"\0"USER_IMAGE=1"\0"USER_OUTSIDE=0"\0"SCALING_USER=1"\0"56=0"\0"64=0"\0"TRANSPARENT_USER=0"\0"UWP_SCROLLBAR=0"\0"MODERN_SCROLLBAR=1"\0"SMALL_ARROWS=0"\0"ARROW_BACKGROUND=1"\0"ICON_FRAME=0"\0"SEARCH_SEPARATOR=0"\0"NO_PROGRAMS_BUTTON=0"\0"" /f > NUL 2>&1

REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\Microsoft\Windows\CurrentVersion\Search" /v SearchboxTaskbarMode /t REG_DWORD /d 0 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced" /v ShowTaskViewButton /t REG_DWORD /d 0 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer" /v EnableAutoTray /t REG_DWORD /d 0 /f > NUL 2>&1

REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\Microsoft\Windows\CurrentVersion\AdvertisingInfo" /v Enabled /t REG_DWORD /d 0 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\Microsoft\Windows\CurrentVersion\AppHost" /v EnableWebContentEvaluation /t REG_DWORD /d 0 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\Control Panel\International\User Profile" /v HttpAcceptLanguageOptOut /t REG_DWORD /d 1 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\Policies\Microsoft\Windows\Explorer" /v DisableNotificationCenter /t REG_DWORD /d 1 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\Microsoft\Windows\CurrentVersion\GameDVR" /v AppCaptureEnabled /t REG_DWORD /d 0 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\System\GameConfigStore" /v GameDVR_Enabled /t REG_DWORD /d 0 /f > NUL 2>&1

REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Attachments" /v SaveZoneInformation /t REG_DWORD /d 1 /f > NUL 2>&1

REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\Microsoft\Windows\CurrentVersion\AppHost" /v ContentEvaluation /t REG_DWORD /d 0 /f > NUL 2>&1

REG ADD "HKEY_USERS\DefaultHiveMount\Control Panel\Desktop" /v WaitToKillAppTimeOut /t REG_SZ /d 2000 /f > NUL 2>&1

REG DELETE "HKEY_USERS\DefaultHiveMount\SOFTWARE\Microsoft\Windows\CurrentVersion\StorageSense" /f > NUL 2>&1

REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\Microsoft\Windows\CurrentVersion\Search" /v "BingSearchEnabled" /t REG_DWORD /d 0 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\Microsoft\Windows\CurrentVersion\Search" /v "CortanaConsent" /t REG_DWORD /d 0 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\Microsoft\Windows\CurrentVersion\Search" /v "CortanaInAmbientMode" /t REG_DWORD /d 0 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\Microsoft\Windows\CurrentVersion\Search" /v "HistoryViewEnabled" /t REG_DWORD 0 /f  > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\Microsoft\Windows\CurrentVersion\Search" /v "HasAboveLockTips" /t REG_DWORD /d 0 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\Microsoft\Windows\CurrentVersion\Search" /v "AllowSearchToUseLocation" /t REG_DWORD /d 0 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\Microsoft\Windows\CurrentVersion\SearchSettings" /v "SafeSearchMode" /t REG_DWORD /d 0 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\Policies\Microsoft\Windows\Explorer" /v "DisableSearchBoxSuggestions" /t REG_DWORD /d 1 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\Microsoft\InputPersonalization" /v "RestrictImplicitTextCollection" /t REG_DWORD /d 1 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\Microsoft\InputPersonalization" /v "RestrictImplicitInkCollection" /t REG_DWORD /d 1 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\Microsoft\InputPersonalization\TrainedDataStore" /v "AcceptedPrivacyPolicy" /t REG_DWORD /d 0 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\Microsoft\InputPersonalization\TrainedDataStore" /v "HarvestContacts" /t REG_DWORD /d 0 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\Microsoft\Personalization\Settings" /v "AcceptedPrivacyPolicy" /t REG_DWORD /d 0 /f > NUL 2>&1

REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\Policies\Microsoft\Windows\Explorer" /v "DisableSearchBoxSuggestions" /t REG_DWORD /d 1 /f > NUL 2>&1

REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced" /v "NavPaneShowAllFolders" /t REG_DWORD /d 0 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced" /v LaunchTo /t REG_DWORD /d 1 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced" /v HideFileExt /t REG_DWORD /d 0 /f > NUL 2>&1
REG ADD "HKEY_USERS\DefaultHiveMount\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced" /v Hidden /t REG_DWORD /d 1 /f > NUL 2>&1

REG UNLOAD "HKU\DefaultHiveMount" > NUL 2>&1

TIMEOUT /T 2 /NOBREAK > NUL 2>&1

CALL :AUX-RETURN "User created successfully" -H

:NEWUSER-REMOVE

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO.

CALL :AUX-INPUTLOOP "usernameRemove" "Enter user to be removed, or 'Cancel' to quit" "0" "8"
	IF %ERRORLEVEL% EQU 3 ENDLOCAL & GOTO HOME-MAINMENU

IF "%usernameRemove%"=="%currentUsername%" (
		POWERSHELL -NoP -C "Write-Host """The specified user is the current user ^(""""%currentUsername%""""^)`n           Continue anyways? ^(Y/N^): """ -NoNewLine; [Console]::CursorVisible = $True; CHOICE /C YN /N /M '%BS%'; [Console]::CursorVisible = $False; EXIT $LastExitCode"
			IF ERRORLEVEL 2 ENDLOCAL & GOTO NEWUSER-MENU
)

IF %inpLenOut% GEQ 11 CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO.

ECHO. & ECHO                               Removing user...

TIMEOUT /T 2 /NOBREAK > NUL 2>&1

NET user "%usernameRemove%" /delete /y > NUL 2>&1
	IF %ERRORLEVEL% NEQ 0 CALL :AUX-RETURN "Failed to remove user." "NEWUSER-MENU" -E

CALL :AUX-RETURN "User removed successfully" -H
REM ------------------------NEWUSER-END------------------------



REM ----------------------------NVCP---------------------------
:NVCP-INSTALL

SETLOCAL

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO.

SC query "NVDisplay.ContainerLocalSystem" > NUL 2>&1
	IF %ERRORLEVEL% NEQ 0 CALL :AUX-RETURN "No NVIDIA driver detected." "HOME-EXTRA" -E

IF EXIST "%SYSTEMDRIVE%\Program Files\WindowsApps" (
	FOR /F "usebackq delims=" %%A IN (`DIR /A:d /B "%SYSTEMDRIVE%\Program Files\WindowsApps" ^| FINDSTR /c:"NVIDIACorp.NVIDIAControlPanel"`) DO (
		DIR /B "%SYSTEMDRIVE%\Program Files\WindowsApps\%%A" | FINDSTR /i /x /c:"nvcplui.exe" > NUL 2>&1
			IF NOT ERRORLEVEL 1 (
					ECHO. & ECHO                        Installing NVIDIA Control Panel...
					TIMEOUT /T 2 /NOBREAK > NUL 2>&1
					TAKEOWN /f "%SYSTEMDRIVE%\Program Files\WindowsApps\%%A" /r /a > NUL 2>&1
					ICACLS "%SYSTEMDRIVE%\Program Files\WindowsApps\%%A" /grant Administrators:F /t > NUL 2>&1
					ICACLS "%SYSTEMDRIVE%\Program Files\WindowsApps\%%A" /grant Users:RX /t > NUL 2>&1
					RMDIR /Q /S "%SYSTEMDRIVE%\Program Files\%%A" > NUL 2>&1
					RMDIR /Q /S "%SYSTEMDRIVE%\Program Files\NVIDIA Control Panel" > NUL 2>&1
					MOVE /Y "%SYSTEMDRIVE%\Program Files\WindowsApps\%%A" "%SYSTEMDRIVE%\Program Files" > NUL 2>&1
						IF ERRORLEVEL 1 GOTO NVCP-FETCH
					RENAME "%SYSTEMDRIVE%\Program Files\%%A" "NVIDIA Control Panel" > NUL 2>&1
					GOTO NVCP-CONFIG
			)
	)
)

:NVCP-FETCH

POWERSHELL -NoP -C "Write-Host """`n           NVIDIA Control Panel must be downloaded`n           Continue? ^(Y/N^): """ -NoNewLine; [Console]::CursorVisible = $True; CHOICE /C YN /N /M '%BS%'; [Console]::CursorVisible = $False; EXIT $LastExitCode"
	IF %ERRORLEVEL%==2 ENDLOCAL & GOTO HOME-MAINMENU

ECHO. & ECHO                             Fetching download link...

CALL :AUX-GENRND "7"

CALL :AUX-FETCHLINK "HOME-EXTRA" "NVIDIA-Control-Panel" -Download "%TEMP%\[amecs]-NVCP%rndOut%.zip" "Downloading NVIDIA Control Panel"
	IF %ERRORLEVEL% EQU 5 CALL :AUX-RETURN "%fetchMsgOut%" "HOME-WSL" -E

ECHO. & ECHO                        Installing NVIDIA Control Panel...
IF EXIST "%SYSTEMDRIVE%\Program Files\NVIDIA Control Panel" (
	TASKKILL /FI "IMAGENAME eq nvcplui.exe" /F > NUL 2>&1
		IF NOT ERRORLEVEL 1 (
			TIMEOUT /T 3 /NOBREAK > NUL 2>&1
		)
	RMDIR /Q /S "%SYSTEMDRIVE%\Program Files\NVIDIA Control Panel" > NUL 2>&1
)

POWERSHELL -NoP -C "$ProgressPreference = 'SilentlyContinue'; Expand-Archive -LiteralPath '%TEMP%\[amecs]-NVCP%rndOut%.zip' -DestinationPath '%SYSTEMDRIVE%\Program Files\NVIDIA Control Panel'"

IF NOT EXIST "%SYSTEMDRIVE%\Program Files\NVIDIA Control Panel\nvcplui.exe" (
	RMDIR /Q /S "%SYSTEMDRIVE%\Program Files\NVIDIA Control Panel" > NUL 2>&1

	CALL :AUX-RETURN "Download is missing critical executable." "HOME-EXTRA" -E
)

:NVCP-CONFIG

DEL /Q /F "%SYSTEMDRIVE%\ProgramData\Microsoft\Windows\Start Menu\Programs\NVIDIA Control Panel.lnk" > NUL 2>&1
POWERSHELL -NoP -C "$ws = New-Object -ComObject WScript.Shell; $s = $ws.CreateShortcut('%SYSTEMDRIVE%\ProgramData\Microsoft\Windows\Start Menu\Programs\NVIDIA Control Panel.lnk'); $S.TargetPath = '%SYSTEMDRIVE%\Program Files\NVIDIA Control Panel\nvcplui.exe'; $S.Save()" > NUL 2>&1

REG ADD "HKLM\System\CurrentControlSet\Services\nvlddmkm\Global\NVTweak" /v "DisableStoreNvCplNotifications" /t REG_DWORD /d 1 /f > NUL

SC config "NVDisplay.ContainerLocalSystem" start=auto > NUL 2>&1
SC start "NVDisplay.ContainerLocalSystem" > NUL 2>&1

CALL :AUX-RETURN "Installed NVIDIA Control Panel successfully" -H

:NVCP-UNINSTALL

SETLOCAL

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO.

ECHO. & ECHO                       Uninstalling NVIDIA Control Panel...
TASKKILL /FI "IMAGENAME eq nvcplui.exe" /F /T > NUL 2>&1
	IF %ERRORLEVEL% EQU 0 (
		TIMEOUT /T 2 /NOBREAK > NUL 2>&1
	)
TIMEOUT /T 1 /NOBREAK > NUL 2>&1
DEL /Q /F "%SYSTEMDRIVE%\ProgramData\Microsoft\Windows\Start Menu\Programs\NVIDIA Control Panel.lnk" > NUL 2>&1
RMDIR /Q /S "%SYSTEMDRIVE%\Program Files\NVIDIA Control Panel" > NUL 2>&1

CALL :AUX-RETURN "Uninstalled NVIDIA Control Panel successfully" -H
REM --------------------------NVCP-END-------------------------


					REM ------------
					REM Menu Options
					REM ------------


REM -----------------------------------------------------------
:MO-MAINMENU

CALL :AUX-ELEVATIONCHECK
	IF %ERRORLEVEL% EQU 0 (
		SET "homeElevMsg=De-elevate User"
		SET "homeElevLoc=ELEVATE-REVOKE"
	) ELSE (
		SET "homeElevMsg=Elevate User to Administrator"
		SET "homeElevLoc=ELEVATE-ELEVATE"
	)

REG QUERY "HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System" /v dontdisplaylastusername 2>&1 | FINDSTR /R /X /C:".*dontdisplaylastusername[ ].*REG_DWORD[ ].*0x1" > NUL 2>&1
	IF %ERRORLEVEL% EQU 0 (
		SET "homeNUMsg=Disable Username Login Requirement"
		SET "homeNULoc=NOUSERNAME-DISABLE"
	) ELSE (
		SET "homeNUMsg=Enable Username Login Requirement"
		SET "homeNULoc=NOUSERNAME-ENABLE"
	)


REG QUERY "HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon" /v "DefaultUsername" 2>&1 | FINDSTR /c:"%currentUsername%" > NUL 2>&1
	IF %ERRORLEVEL% EQU 0 (
		REG QUERY "HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon" /v "AutoAdminLogon" 2>&1 | FINDSTR /R /X /C:".*AutoAdminLogon[ ].*REG_SZ.*[ ]1" > NUL 2>&1
			IF NOT ERRORLEVEL 1 (
				SET "homeALMsg=Disable AutoLogon"
				SET "homeALLoc=AUTOLOGON-DISABLE"
			) ELSE (
				SET "homeALMsg=Enable AutoLogon"
				SET "homeALLoc=AUTOLOGON-ENABLE"
			)
	) ELSE (
		SET "homeALMsg=Enable AutoLogon"
		SET "homeALLoc=AUTOLOGON-ENABLE"
	)
EXIT /B 0
REM -----------------------------------------------------------



REM -----------------------------------------------------------
:MO-EXTRA

REG QUERY "HKU\%userSID%\SOFTWARE\Microsoft\Windows\CurrentVersion\PushNotifications" /v ToastEnabled 2>&1 | FINDSTR /R /X /C:".*ToastEnabled[ ].*REG_DWORD[ ].*0x0" > NUL 2>&1
	IF %ERRORLEVEL% NEQ 0 (
		SET "homeNOTIFMsg=Disable Desktop Notifications"
		SET "homeNOTIFLoc=NOTIF-DISABLE"
	) ELSE (
		SET "homeNOTIFMsg=Enable Desktop Notifications"
		SET "homeNOTIFLoc=NOTIF-ENABLE"
	)

REG QUERY "HKU\%userSID%\SOFTWARE\Policies\Microsoft\Windows\Explorer" /v DisableNotificationCenter 2>&1 | FINDSTR /R /X /C:".*DisableNotificationCenter[ ].*REG_DWORD[ ].*0x1" > NUL 2>&1
	IF %ERRORLEVEL% EQU 0 (
		SET "homeNOTIFCENMsg=Enable Notification Center"
		SET "homeNOTIFCENLoc=NOTIFCEN-ENABLE"
	) ELSE (
		SET "homeNOTIFCENMsg=Disable Notification Center"
		SET "homeNOTIFCENLoc=NOTIFCEN-DISABLE"
	)

REG QUERY "HKLM\SYSTEM\CurrentControlSet\Control\Power" /v HibernateEnabled 2>&1 | FINDSTR /R /X /C:".*HibernateEnabled[ ].*REG_DWORD[ ].*0x1" > NUL 2>&1
	IF %ERRORLEVEL% EQU 0 (
		REG QUERY "HKLM\SYSTEM\CurrentControlSet\Control\Power" /v HiberFileType 2>&1 | FINDSTR /R /X /C:".*HiberFileType[ ].*REG_DWORD[ ].*0x1" > NUL 2>&1
			IF NOT ERRORLEVEL 1 (
				SET "homeHIBMsg=Disable Hibernation"
				SET "homeHIBLoc=HIBERNATE-DISABLE"
			) ELSE (
				SET "homeHIBMsg=Enable Hibernation"
				SET "homeHIBLoc=HIBERNATE-ENABLE"
			)
	) ELSE (
		SET "homeHIBMsg=Enable Hibernation"
		SET "homeHIBLoc=HIBERNATE-ENABLE"
	)

REG QUERY "HKLM\SOFTWARE\Microsoft\Windows Script Host\Settings" /v Enabled 2>&1 | FINDSTR /R /X /C:".*Enabled[ ].*REG_DWORD[ ].*0x0" > NUL 2>&1
	IF %ERRORLEVEL% EQU 0 (
		REG QUERY "HKU\%userSID%\SOFTWARE\Microsoft\Windows Script Host\Settings" /v Enabled 2>&1 | FINDSTR /R /X /C:".*Enabled[ ].*REG_DWORD[ ].*0x1" > NUL 2>&1
			IF ERRORLEVEL 1 (
				SET "homeWSHMsg=Enable Windows Script Host [WSH] (Legacy)"
				SET "homeWSHLoc=WSH-ENABLE"
			) ELSE (
				SET "homeWSHMsg=Disable Windows Script Host [WSH] (Legacy)"
				SET "homeWSHLoc=WSH-DISABLE"

			)
	) ELSE (
		REG QUERY "HKU\%userSID%\SOFTWARE\Microsoft\Windows Script Host\Settings" /v Enabled 2>&1 | FINDSTR /R /X /C:".*Enabled[ ].*REG_DWORD[ ].*0x0" > NUL 2>&1
			IF NOT ERRORLEVEL 1 (
				SET "homeWSHMsg=Enable Windows Script Host [WSH] (Legacy)"
				SET "homeWSHLoc=WSH-ENABLE"
			) ELSE (
				SET "homeWSHMsg=Disable Windows Script Host [WSH] (Legacy)"
				SET "homeWSHLoc=WSH-DISABLE"

			)
	)

ASSOC .vbs 2>&1| FINDSTR /I /X /c:".vbs=VBSFile" > NUL 2>&1
	IF %ERRORLEVEL% EQU 0 (
		SET "homeVBSMsg=Disable Visual Basic Script [VBS] (Legacy)"
		SET "homeVBSLoc=VBS-DISABLE"
	) ELSE (
		SET "homeVBSMsg=Enable Visual Basic Script [VBS] (Legacy)"
		SET "homeVBSLoc=VBS-ENABLE"
	)

REG QUERY "HKLM\SYSTEM\CurrentControlSet\Services\NlaSvc\Parameters\Internet" /v EnableActiveProbing 2>&1 | FINDSTR /R /X /C:".*EnableActiveProbing[ ].*REG_DWORD[ ].*0x1" > NUL 2>&1
	IF %ERRORLEVEL% EQU 0 (
		SET "homeNCSIMsg=Disable NCSI Active Probing (Legacy)"
		SET "homeNCSILoc=NCSI-DISABLE"
	) ELSE (
		SET "homeNCSIMsg=Enable NCSI Active Probing (Legacy)"
		SET "homeNCSILoc=NCSI-ENABLE"
	)

SET "homeNVCPMsg=Write-Host '                 [9] Install NVIDIA Control Panel'"
IF EXIST "%SYSTEMDRIVE%\Program Files\NVIDIA Control Panel\nvcplui.exe" (
	SET "homeNVCPLoc=NVCP-UNINSTALL"
	SET "homeNVCPMsg=Write-Host '                 [9] Uninstall NVIDIA Control Panel'"
) ELSE (
	SET "homeNVCPLoc=NVCP-INSTALL"
	WMIC path win32_VideoController get name | FINDSTR "NVIDIA GeForce GTX RTX" > NUL 2>&1
	IF ERRORLEVEL 1 (
		CALL :MO-CHOICE -DelChoice 9
		SET "homeNVCPMsg=Write-Host '                 [9] Install NVIDIA Control Panel' -NoNewLine -ForegroundColor DarkGray; Write-Host ' [No NVIDIA GPU]' -ForegroundColor Red"
	) ELSE (
		SC query "NVDisplay.ContainerLocalSystem" > NUL 2>&1
			IF ERRORLEVEL 1 (
				CALL :MO-CHOICE -DelChoice 9
				SET "homeNVCPMsg=Write-Host '                 [9] Install NVIDIA Control Panel' -NoNewLine -ForegroundColor DarkGray; Write-Host ' [No NVIDIA Driver]' -ForegroundColor Red"
			) ELSE (
				IF EXIST "%SYSTEMDRIVE%\Program Files\WindowsApps" (
					DIR /A:d /B "%SYSTEMDRIVE%\Program Files\WindowsApps" | FINDSTR /c:"NVIDIACorp.NVIDIAControlPanel" > NUL 2>&1
						IF NOT ERRORLEVEL 1 (
							FOR /F "usebackq delims=" %%A IN (`DIR /A:d /B "%SYSTEMDRIVE%\Program Files\WindowsApps" ^| FINDSTR /c:"NVIDIACorp.NVIDIAControlPanel"`) DO (
								DIR /B "%SYSTEMDRIVE%\Program Files\WindowsApps\%%A" | FINDSTR /i /x /c:"nvcplui.exe" > NUL 2>&1
									IF ERRORLEVEL 1 (
										CURL store.rg-adguard.net 2>&1 | FINDSTR /I /c:"Cloudflare Ray ID" > NUL 2>&1
											IF NOT ERRORLEVEL 1 CALL :MO-CHOICE -DelChoice 9 & SET "homeNVCPMsg=Write-Host '                 [9] Install NVIDIA Control Panel' -NoNewLine -ForegroundColor DarkGray; Write-Host ' [Server Unavailable]' -ForegroundColor Red"
									)
							)
						) ELSE (
							CURL store.rg-adguard.net 2>&1 | FINDSTR /I /c:"Cloudflare Ray ID" > NUL 2>&1
								IF NOT ERRORLEVEL 1 CALL :MO-CHOICE -DelChoice 9 & SET "homeNVCPMsg=Write-Host '                 [9] Install NVIDIA Control Panel' -NoNewLine -ForegroundColor DarkGray; Write-Host ' [Server Unavailable]' -ForegroundColor Red"
						)
				)
			)
	)
)
CMD /C WSL --help 2>&1 | FINDSTR /I /R /c:"-.-.i.n.s.t.a.l.l.*<.O.p.t.i.o.n.s.>" > NUL 2>&1
	IF %ERRORLEVEL% EQU 0 (
		CALL :MO-CHOICE -DelChoice 1
		SET "homeExtWSLMsg="""" -ForegroundColor DarkGray -NoNewLine; Write-Host ' [Not Supported]' -ForegroundColor Red -NoNewLine; Write-Host """""
	) ELSE (
		SET "homeExtWSLMsg="
	)
EXIT /B 0
REM -----------------------------------------------------------



REM -----------------------------------------------------------
:MO-LANGUAGE

REM NULL
EXIT /B 0
REM -----------------------------------------------------------



REM -----------------------------------------------------------
:MO-WSL


SET "wslUnattend="
SET "homeWSLChPos=16"
SET "homeWSLStatus=`n"

DISM /Online /Get-FeatureInfo:Microsoft-Windows-Subsystem-Linux /English | FINDSTR /x /c:"State : Enabled" > NUL 2>&1
	IF %ERRORLEVEL% NEQ 0 (
		SET "homeWSLMsg=Enable WSL"
		SET "homeWSLLoc=WSL-ENABLE"
	) ELSE (
		SET "homeWSLMsg=Disable WSL"
		SET "homeWSLLoc=WSL-DISABLE"
		WHERE WSL.exe > NUL 2>&1
			IF ERRORLEVEL 1 (
				SET "homeWSLStatus=""""; Write-Host """"`n$(' '.padleft('18', ' '))A restart is required for WSL functionality."""" -ForegroundColor Red -NoNewLine; Write-Host """""
				SET "homeWSLChPos=17"
			)
	)

WHERE WSL.exe > NUL 2>&1
	IF %ERRORLEVEL% NEQ 0 (
		CALL :MO-CHOICE -DelChoice 2
		CALL :MO-CHOICE -DelChoice 3
		SET "homeWSLDistroMsgs=""""; Write-Host """"$(' '.padleft('17', ' '))[2] Install WSL Distro`n$(' '.padleft('17', ' '))[3] Remove WSL Distro`n"""" -ForegroundColor DarkGray; Write-Host """"$(' '.padleft('17', ' '))[U] Unattended Distro Install [Disabled]"""" -NoNewLine; Write-Host """" "
	) ELSE (
		CALL :MO-CHOICE -DelChoice U
		SET "homeWSLDistroMsgs=`n$(' '.padleft('17', ' '))[2] Install WSL Distro`n$(' '.padleft('17', ' '))[3] Remove WSL Distro`n"
	)
EXIT /B 0
REM -----------------------------------------------------------



REM -----------------------------------------------------------
:MO-LIMMAINMENU

CALL :AUX-ELEVATIONCHECK
	IF %ERRORLEVEL% EQU 0 (
		SET "homeElevMsg=De-elevate User"
	) ELSE (
		SET "homeElevMsg=Elevate User to Administrator"
	)

REG QUERY "HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System" /v dontdisplaylastusername 2>&1 | FINDSTR /R /X /C:".*dontdisplaylastusername[ ].*REG_DWORD[ ].*0x1" > NUL 2>&1
	IF %ERRORLEVEL% EQU 0 (
		SET "homeNUMsg=Disable Username Login Requirement"
	) ELSE (
		SET "homeNUMsg=Enable Username Login Requirement"
	)

REG QUERY "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon" /v "DefaultUsername" 2>&1 | FINDSTR /c:"%currentUsername%" > NUL 2>&1
	IF %ERRORLEVEL% EQU 0 (
		REG QUERY "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon" /v "AutoAdminLogon" 2>&1 | FINDSTR /R /X /C:".*AutoAdminLogon[ ].*REG_SZ.*[ ]1" > NUL 2>&1
			IF NOT ERRORLEVEL 1 (
				SET "homeALMsg=Disable AutoLogon"
			) ELSE (
				SET "homeALMsg=Enable AutoLogon"
			)
	) ELSE (
		SET "homeALMsg=Enable AutoLogon"
	)
EXIT /B 0
REM -----------------------------------------------------------



REM -----------------------------------------------------------
:MO-LIMEXTRA

REG QUERY "HKU\%userSID%\SOFTWARE\Microsoft\Windows\CurrentVersion\PushNotifications" /v ToastEnabled 2>&1 | FINDSTR /R /X /C:".*ToastEnabled[ ].*REG_DWORD[ ].*0x1" > NUL 2>&1
	IF %ERRORLEVEL% EQU 0 (
		SET "homeNOTIFMsg=Disable Desktop Notifications"
		SET "homeNOTIFLoc=NOTIF-DISABLE"
	) ELSE (
		SET "homeNOTIFMsg=Enable Desktop Notifications"
		SET "homeNOTIFLoc=NOTIF-ENABLE"
	)

REG QUERY "HKU\%userSID%\SOFTWARE\Policies\Microsoft\Windows\Explorer" /v DisableNotificationCenter 2>&1| FINDSTR /R /X /C:".*DisableNotificationCenter[ ].*REG_DWORD[ ].*0x1" > NUL 2>&1
	IF %ERRORLEVEL% EQU 0 (
		SET "homeNOTIFCENMsg=Enable Notification Center"
	) ELSE (
		SET "homeNOTIFCENMsg=Disable Notification Center"
	)

REG QUERY "HKLM\SYSTEM\CurrentControlSet\Control\Power" /v HibernateEnabled 2>&1 | FINDSTR /R /X /C:".*HibernateEnabled[ ].*REG_DWORD[ ].*0x1" > NUL 2>&1
	IF %ERRORLEVEL% EQU 0 (
		REG QUERY "HKLM\SYSTEM\CurrentControlSet\Control\Power" /v HiberFileType 2>&1 | FINDSTR /R /X /C:".*HiberFileType[ ].*REG_DWORD[ ].*0x1" > NUL 2>&1
			IF NOT ERRORLEVEL 1 (
				SET "homeHIBMsg=Disable Hibernation"
			) ELSE (
				SET "homeHIBMsg=Enable Hibernation"
			)
	) ELSE (
		SET "homeHIBMsg=Enable Hibernation"
	)

REG QUERY "HKLM\SOFTWARE\Microsoft\Windows Script Host\Settings" /v Enabled 2>&1 | FINDSTR /R /X /C:".*Enabled[ ].*REG_DWORD[ ].*0x0" > NUL 2>&1
	IF %ERRORLEVEL% EQU 0 (
		REG QUERY "HKEY_USERS\%userSID%\SOFTWARE\Microsoft\Windows Script Host\Settings" /v Enabled 2>&1 | FINDSTR /R /X /C:".*Enabled[ ].*REG_DWORD[ ].*0x1" > NUL 2>&1
			IF ERRORLEVEL 1 (
				SET "homeWSHMsg=Enable Windows Script Host [WSH] (Legacy)"
			) ELSE (
				SET "homeWSHMsg=Disable Windows Script Host [WSH] (Legacy)"

			)
	) ELSE (
		REG QUERY "HKEY_USERS\%userSID%\SOFTWARE\Microsoft\Windows Script Host\Settings" /v Enabled 2>&1 | FINDSTR /R /X /C:".*Enabled[ ].*REG_DWORD[ ].*0x0" > NUL 2>&1
			IF NOT ERRORLEVEL 1 (
				SET "homeWSHMsg=Enable Windows Script Host [WSH] (Legacy)"
			) ELSE (
				SET "homeWSHMsg=Disable Windows Script Host [WSH] (Legacy)"
			)
	)

ASSOC .vbs 2>&1| FINDSTR /I /X /c:".vbs=VBSFile" > NUL 2>&1
	IF %ERRORLEVEL% EQU 0 (
		SET "homeVBSMsg=Disable Visual Basic Script [VBS] (Legacy)"
		SET "homeVBSLoc=VBS-DISABLE"
	) ELSE (
		SET "homeVBSMsg=Enable Visual Basic Script [VBS] (Legacy)"
		SET "homeVBSLoc=VBS-ENABLE"
	)

REG QUERY "HKLM\SYSTEM\CurrentControlSet\Services\NlaSvc\Parameters\Internet" /v EnableActiveProbing 2>&1 | FINDSTR /R /X /C:".*EnableActiveProbing[ ].*REG_DWORD[ ].*0x1" > NUL 2>&1
	IF %ERRORLEVEL% EQU 0 (
		SET "homeNCSIMsg=Disable NCSI Active Probing (Legacy)"
	) ELSE (
		SET "homeNCSIMsg=Enable NCSI Active Probing (Legacy)"
	)

SET "homeNVCPMsg=Write-Host '                 [9] Install NVIDIA Control Panel' -NoNewLine -ForegroundColor DarkGray; Write-Host ' [Admin Required]' -ForegroundColor Red"
IF EXIST "%SYSTEMDRIVE%\Program Files\NVIDIA Control Panel\nvcplui.exe" (
	SET "homeNVCPMsg=Write-Host '                 [9] Uninstall NVIDIA Control Panel' -NoNewLine -ForegroundColor DarkGray; Write-Host ' [Admin Required]' -ForegroundColor Red"
)

CMD /C WSL --help 2>&1 | FINDSTR /I /R /c:"-.-.i.n.s.t.a.l.l.*<.O.p.t.i.o.n.s.>" > NUL 2>&1
	IF %ERRORLEVEL% EQU 0 (
		SET "homeExtWSLMsg= -ForegroundColor DarkGray -NoNewLine; Write-Host ' [Not Supported]' -ForegroundColor Red"
	) ELSE (
		SET "homeExtWSLMsg="
	)
EXIT /B 0
REM -----------------------------------------------------------



REM -----------------------------------------------------------
:MO-LIMLANGUAGE

REM NULL
EXIT /B 0
REM -----------------------------------------------------------



REM -----------------------------------------------------------
:MO-LIMWSL

WHERE WSL.exe > NUL 2>&1
	IF %ERRORLEVEL% NEQ 0 (
		SET "homeWSLMsg=Enable WSL"
	) ELSE (
		SET "homeWSLMsg=Disable WSL"
	)

SET "homeLIMWSLCh=230X"

WHERE WSL.exe > NUL 2>&1
	IF %ERRORLEVEL% NEQ 0 (
		CALL :MO-CHOICE -DelChoice 2
		CALL :MO-CHOICE -DelChoice 3
		SET "homeWSLDistroMsgs=$(' '.padleft(17, ' '))[2] Install WSL Distro`n$(' '.padleft(17, ' '))[3] Remove WSL Distro`n`n$(' '.padleft(17, ' '))[U] Unattended Distro Install"""" -ForegroundColor DarkGray -NoNewLine; Write-Host ' [Admin Required]' -ForegroundColor Red -NoNewLine; Write-Host """""
	) ELSE (
		SET "homeWSLDistroMsgs=$(' '.padleft(17, ' '))[2] Install WSL Distro`n$(' '.padleft(17, ' '))[3] Remove WSL Distro`n"
	)
EXIT /B 0
REM -----------------------------------------------------------
:MO-CHOICE

IF "%~1"=="-InitChoices" CALL :INTERNAL-MO_CHOICE-InitChoices "%~2" "%~3"
IF "%~1"=="-DelChoice" CALL :INTERNAL-MO_CHOICE-DelChoice "%~2"
IF "%~1"=="-StartChoices" CALL :INTERNAL-MO_CHOICE-StartChoices "%~2"

EXIT /B 0
:INTERNAL-MO_CHOICE-InitChoices

SET "moChoices=%~1"
FOR /F "usebackq tokens=2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17 delims=+" %%A IN (`ECHO "%~2"`) DO SET "MOCHI1=%%A" & SET "MOCHI2=%%B" & SET "MOCHI3=%%C" & SET "MOCHI4=%%D" & SET "MOCHI5=%%E" & SET "MOCHI6=%%F" & SET "MOCHI7=%%G" & SET "MOCHI8=%%H" & SET "MOCHI9=%%I" & SET "MOCHI10=%%J" & SET "MOCHI11=%%K" & SET "MOCHI12=%%L" & SET "MOCHI13=%%M" & SET "MOCHI14=%%N" & SET "MOCHI15=%%O" & SET "MOCHI16=%%P"

EXIT /B 0

:INTERNAL-MO_CHOICE-DelChoice

FOR /F "usebackq tokens=1,2 delims=%~1" %%A IN (`ECHO %moChoices%`) DO (
	SET "moChoices=%%A%%B"
	IF NOT "%%B"=="" (
		CALL :AUX-LENGTHFETCH "%%A:"
	) ELSE (
		IF "%moChoices:~0,1%"=="%~1" (
			SET "chRemove=1"
		) ELSE (
			CALL :AUX-LENGTHFETCH "%%A:"
		)
	)
)
IF NOT "%chRemove%"=="1" SET "chRemove=%lenOut%"

SET /A "chShift=%chRemove%+1"
CALL SET "MOCHI%chRemove%=%%MOCHI%chShift%%%"
SET /A "chShiftAlt=%chShift%+1"
CALL SET "MOCHI%chShift%=%%MOCHI%chShiftAlt%%%"
SET /A "chShift=%chShiftAlt%+1"
CALL SET "MOCHI%chShiftAlt%=%%MOCHI%chShift%%%"
SET /A "chShiftAlt=%chShift%+1"
CALL SET "MOCHI%chShift%=%%MOCHI%chShiftAlt%%%"
SET /A "chShift=%chShiftAlt%+1"
CALL SET "MOCHI%chShiftAlt%=%%MOCHI%chShift%%%"
SET /A "chShiftAlt=%chShift%+1"
CALL SET "MOCHI%chShift%=%%MOCHI%chShiftAlt%%%"
SET /A "chShift=%chShiftAlt%+1"
CALL SET "MOCHI%chShiftAlt%=%%MOCHI%chShift%%%"
SET /A "chShiftAlt=%chShift%+1"
CALL SET "MOCHI%chShift%=%%MOCHI%chShiftAlt%%%"
SET /A "chShift=%chShiftAlt%+1"
CALL SET "MOCHI%chShiftAlt%=%%MOCHI%chShift%%%"
SET /A "chShiftAlt=%chShift%+1"
CALL SET "MOCHI%chShift%=%%MOCHI%chShiftAlt%%%"
SET /A "chShift=%chShiftAlt%+1"
CALL SET "MOCHI%chShiftAlt%=%%MOCHI%chShift%%%"
SET /A "chShiftAlt=%chShift%+1"
CALL SET "MOCHI%chShift%=%%MOCHI%chShiftAlt%%%"
SET /A "chShift=%chShiftAlt%+1"
CALL SET "MOCHI%chShiftAlt%=%%MOCHI%chShift%%%"
SET /A "chShiftAlt=%chShift%+1"
CALL SET "MOCHI%chShift%=%%MOCHI%chShiftAlt%%%"
SET /A "chShift=%chShiftAlt%+1"
CALL SET "MOCHI%chShiftAlt%=%%MOCHI%chShift%%%"
SET /A "chShiftAlt=%chShift%+1"
CALL SET "MOCHI%chShift%=%%MOCHI%chShiftAlt%%%"

EXIT /B 0

:INTERNAL-MO_CHOICE-StartChoices

SETLOCAL ENABLEDELAYEDEXPANSION
POWERSHELL -NoP -C "Write-Host """"%~1`n$(' '.padleft(11, ' '))__________________________________________________________`n`n$(' '.padleft(11, ' '))Choose a menu option: """" -NoNewLine; [Console]::CursorVisible = $True; CHOICE /C %moChoices% /N /M '%BS%'; [Console]::CursorVisible = $False; EXIT $LastExitCode"
CALL SET "moChoice=%%MOCHI%ERRORLEVEL%%%"
SET "moChoice=%moChoice:|=^|%"

ENDLOCAL & (GOTO) 2>NUL & (GOTO) 2>NUL & %moChoice:""="%
REM -----------------------------------------------------------


					REM ----------------
					REM Script Functions
					REM ----------------


REM -----------------------------------------------------------
:AUX-RETURN

SETLOCAL

IF /I "%~3"=="-C" (
	DEL /Q /F "%TEMP%\[amecs]*%rndOut%.*" > NUL 2>&1
	DEL /Q /F "%userTemp%\[amecs]*%rndOut%.*" > NUL 2>&1
) ELSE (
	IF /I "%~4"=="-C" (
		DEL /Q /F "%TEMP%\[amecs]*%rndOut%.*" > NUL 2>&1
		DEL /Q /F "%userTemp%\[amecs]*%rndOut%.*" > NUL 2>&1
	) ELSE (
		IF /I "%~5"=="-C" (
			DEL /Q /F "%TEMP%\[amecs]*%rndOut%.*" > NUL 2>&1
			DEL /Q /F "%userTemp%\[amecs]*%rndOut%.*" > NUL 2>&1
		) ELSE (
			IF /I "%~6"=="-C" (
				DEL /Q /F "%TEMP%\[amecs]*%rndOut%.*" > NUL 2>&1
				DEL /Q /F "%userTemp%\[amecs]*%rndOut%.*" > NUL 2>&1	
			) ELSE (
				IF /I "%~7"=="-C" (
					DEL /Q /F "%TEMP%\[amecs]*%rndOut%.*" > NUL 2>&1
					DEL /Q /F "%userTemp%\[amecs]*%rndOut%.*" > NUL 2>&1
				)
			)
		)
	)
)

CALL :AUX-CENTERTEXT "%~1"
	SET "returnOutComm=%cenout%"

IF /I "%~3"=="-E" (
	SET "errorColor= -ForegroundColor Red"
) ELSE (
	IF /I "%~4"=="-E" (
		SET "errorColor= -ForegroundColor Red"
	) ELSE (
		IF /I "%~5"=="-E" (
			SET "errorColor= -ForegroundColor Red"
		) ELSE (
			IF /I "%~6"=="-E" (
				SET "errorColor= -ForegroundColor Red"
			) ELSE (
				IF /I "%~7"=="-E" (
					SET "errorColor= -ForegroundColor Red"
				) ELSE (
					SET "errorColor="
				)
			)
		)
	)
)

SET "cenOut="

IF /I "%~3"=="-L" (
	CALL :AUX-CENTERTEXT "%~4"
) ELSE (
	IF /I "%~4"=="-L" (
		CALL :AUX-CENTERTEXT "%~5"
	) ELSE (
		IF /I "%~5"=="-L" (
			CALL :AUX-CENTERTEXT "%~6"
		) ELSE (
			IF /I "%~6"=="-L" (
				CALL :AUX-CENTERTEXT "%~7"
			) ELSE (
				IF /I "%~7"=="-L" (
					CALL :AUX-CENTERTEXT "%~8"
				)
			)
		)
	)
)

IF "%cenOut%"=="" (
	SET "returnMsg=Write-Host """`n"""; Write-Host '%returnOutComm%'%errorColor%; Write-Host """           __________________________________________________________`n"""; "
) ELSE (
	SET "returnMsg=Write-Host """`n"""; Write-Host '%returnOutComm%'%errorColor%; Write-Host '%cenOut%'%errorColor%; Write-Host """           __________________________________________________________`n"""; "
)

IF /I "%~d3"=="R:" (
	POWERSHELL -NoP -C "%returnMsg%Write-Host """           Would you like to $^('%~x3'.replace^('.'^, ''^).replace('-',' '^)^) now? ^(Y/N^): """ -NoNewLine; [Console]::CursorVisible = $True; CHOICE /C NY /N /M '%BS%'; [Console]::CursorVisible = $False; EXIT $LastExitCode"
		IF ERRORLEVEL 2 SHUTDOWN -%~n3 & EXIT 0
) ELSE (
	IF /I "%~d4"=="R:" (
		POWERSHELL -NoP -C "%returnMsg%Write-Host """           Would you like to $^('%~x4'.replace^('.'^, ''^).replace('-',' '^)^) now? ^(Y/N^): """ -NoNewLine; [Console]::CursorVisible = $True; CHOICE /C NY /N /M '%BS%'; [Console]::CursorVisible = $False; EXIT $LastExitCode"
			IF ERRORLEVEL 2 SHUTDOWN -%~n4 & EXIT 0
	) ELSE (
		IF /I "%~d5"=="R:" (
			POWERSHELL -NoP -C "%returnMsg%Write-Host """           Would you like to $^('%~x5'.replace^('.'^, ''^).replace('-',' '^)^) now? ^(Y/N^): """ -NoNewLine; [Console]::CursorVisible = $True; CHOICE /C NY /N /M '%BS%'; [Console]::CursorVisible = $False; EXIT $LastExitCode"
				IF ERRORLEVEL 2 SHUTDOWN -%~n5 & EXIT 0
		) ELSE (
			IF /I "%~d6"=="R:" (
				POWERSHELL -NoP -C "%returnMsg%Write-Host """           Would you like to $^('%~x6'.replace^('.'^, ''^).replace('-',' '^)^) now? ^(Y/N^): """ -NoNewLine; [Console]::CursorVisible = $True; CHOICE /C NY /N /M '%BS%'; [Console]::CursorVisible = $False; EXIT $LastExitCode"
					IF ERRORLEVEL 2 SHUTDOWN -%~n6 & EXIT 0
			) ELSE (
				IF /I "%~d7"=="R:" (
					POWERSHELL -NoP -C "%returnMsg%Write-Host """           Would you like to $^('%~x7'.replace^('.'^, ''^).replace('-',' '^)^) now? ^(Y/N^): """ -NoNewLine; [Console]::CursorVisible = $True; CHOICE /C NY /N /M '%BS%'; [Console]::CursorVisible = $False; EXIT $LastExitCode"
						IF ERRORLEVEL 2 SHUTDOWN -%~n7 & EXIT 0
				) ELSE (
					POWERSHELL -NoP -C "%returnMsg%Write-Host -NoNewLine '           Press any key to return to the Menu: '; [Console]::CursorVisible = $True; $NULL = $Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown'); [Console]::CursorVisible = $False"
				)
			)
		)
	)
)

IF "%wslUnattendRun%"=="true" (
	SET "wslUnattendRun=false"
	IF "%adminPrivs%"=="false" (
		POWERSHELL -NoP -C "Start-Process '%scriptPath:'=''%' -Verb RunAs" > NUL 2>&1
			IF ERRORLEVEL 1 (
				CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^|
				POWERSHELL -NoP -C "Write-Host """`n`n`n               Elevation canceled, run with limited functionality?`n                                    [Y]   [N]`n           __________________________________________________________`n`n           Choose an option: """ -NoNewLine; [Console]::CursorVisible = $True; CHOICE /C YN /N /M '%BS%'; [Console]::CursorVisible = $False; EXIT $LastExitCode"
					IF ERRORLEVEL 2 (
						EXIT 0
					)
			) ELSE (
				EXIT 0
			)
	)
)
IF /I "%~2"=="-H" (
	ENDLOCAL & (GOTO) 2>NUL & ENDLOCAL & GOTO HOME-MAINMENU
) ELSE (
	IF "%~2"=="-HNR" (
		ENDLOCAL & (GOTO) 2>NUL & GOTO HOME-MAINMENU
	) ELSE (
		ENDLOCAL & (GOTO) 2>NUL & ENDLOCAL & GOTO %~2
	)
)
REM -----------------------------------------------------------
:AUX-CENTERTEXT

SETLOCAL
SET "spaces=                                                                                                    "
SET "cenSize=58"
SET /A "LEN=0"

CALL :AUX-LENGTHFETCH "%~1"
IF NOT "%~2"=="" (
	IF %lenOut% GEQ 59 ENDLOCAL & ENDLOCAL & SET "cenOut=                                  Output Error" & EXIT /B 0
) ELSE (
	IF %lenOut% GEQ 59 CALL :AUX-LENGTHFETCH "%~2"
)
IF %lenOut% GEQ 59 ENDLOCAL & ENDLOCAL & SET "cenOut=                                  Output Error" & EXIT /B 0

SET /A "oddCheck=%lenOut% %% 2"
IF "%oddCheck%"=="0" (SET "space=") ELSE (SET "space= ")
IF "%lenOut%"=="58" SET "space="

SET /A "pref_len=%cenSize%-%lenOut%" & SET /A "pref_len/=2"
CALL SET "cenOut=%space%           %%spaces:~0,%pref_len%%%%~1"
ENDLOCAL & SET "cenOut=%cenOut%"
EXIT /B 0
REM -----------------------------------------------------------



REM -----------------------------------------------------------
:AUX-GETUSERNAME

REM Grabs current username. %username% can be problematic and %userprofile%
REM is hard to filter properly, thus why this method is used.
FOR /F "usebackq tokens=1,* delims=\" %%A IN (`WMIC computersystem get username ^| FINDSTR /c:"\\"`) DO SET "currentUsername=%%B"
	SET "currentUsername=%currentUsername:~0,-3%"
	REM Detection for if user changed their username without restarting
	IF "%currentUsername%"=="~0,-3" SET "currentUsername=RestartRequired"
	SET "possibleUserDir=%currentUsername%"
EXIT /B 0
REM -----------------------------------------------------------



REM -----------------------------------------------------------
:AUX-GETUSERENV

FOR /F "usebackq tokens=2,* delims= " %%A IN (`REG QUERY "HKU\%userSID%\Volatile Environment" /v "APPDATA" 2^>^&1 ^| FINDSTR /R /X /C:".*APPDATA[ ]*REG_SZ[ ].*"`) DO SET "userAppData=%%B"
FOR /F "usebackq tokens=2,* delims= " %%A IN (`REG QUERY "HKU\%userSID%\Volatile Environment" /v "LOCALAPPDATA" 2^>^&1 ^| FINDSTR /R /X /C:".*LOCALAPPDATA[ ]*REG_SZ[ ].*"`) DO SET "userLocalAppData=%%B"
FOR /F "usebackq tokens=2,* delims= " %%A IN (`REG QUERY "HKU\%userSID%\Environment" /v "TEMP" 2^>^&1 ^| FINDSTR /R /X /C:".*TEMP[ ]*REG_EXPAND_SZ[ ].*"`) DO SET "userTemp=%%B"
FOR /F "usebackq tokens=2,* delims= " %%A IN (`REG QUERY "HKU\%userSID%\Volatile Environment" /v "USERPROFILE" 2^>^&1 ^| FINDSTR /R /X /C:".*USERPROFILE[ ]*REG_SZ[ ].*"`) DO SET "userUserProfile=%%B"

SET "userUserProfileTmp=%userUserProfile:!=:AINV:%"
SETLOCAL ENABLEDELAYEDEXPANSION
SET "userTemp=!userTemp:%%USERPROFILE%%=%userUserProfileTmp%!"
ENDLOCAL & SET "userTemp=%userTemp::AINV:=!%"

EXIT /B 0
REM -----------------------------------------------------------



REM -----------------------------------------------------------
:AUX-ELEVATIONCHECK

IF "%currentUsername%"=="RestartRequired" SET "userStatus=Unknown" & SET "userRestart=true" & EXIT /B 0
NET user "%currentUsername%" /y | FINDSTR /R /X /C:".*[ ][ ][ ][ ][ ][ ]\*Administrators[ ][ ][ ][ ][ ][ ][ ].*" > NUL 2>&1
	IF %ERRORLEVEL% NEQ 0 (
		SET "userStatus=Not Elevated" & EXIT /B 1
	) ELSE (
		SET "userStatus=Elevated" & EXIT /B 0
	)
REM -----------------------------------------------------------



REM -----------------------------------------------------------
:AUX-NETWORKCHECK

IF NOT "%firstLoop%"=="true" SETLOCAL & SET "firstLoop=true" & SET /A "count=0"

IF %count% GTR 8 EXIT /B 1

PING -n 1 archlinux.org -w 20000 > NUL 2>&1
	IF %ERRORLEVEL% GTR 0 (
		PING -n 1 wikipedia.org -w 20000 > NUL 2>&1
			IF ERRORLEVEL 1 (
				PING -n 1 github.com -w 20000 > NUL 2>&1
					IF ERRORLEVEL 1 (
						IF /I "%~1"=="-L" SET /A "count=%count%+1" & TIMEOUT /T 3 /NOBREAK>NUL 2>&1 & GOTO :AUX-NETWORKCHECK
						ENDLOCAL & EXIT /B 1
					)
			)
	)

ENDLOCAL & EXIT /B 0
REM -----------------------------------------------------------



REM -----------------------------------------------------------
:AUX-BACKLINE

POWERSHELL -NoP -C "$posY = $host.UI.RawUI.CursorPosition.Y; $origPosY = $posY - 1; [Console]::SetCursorPosition(%~1,$origPosY); Write-Host """None`r""" -ForegroundColor DarkGray"
EXIT /B 0
REM -----------------------------------------------------------



REM -----------------------------------------------------------
:AUX-INPUTLOOP

SET "lenOut="

IF NOT "%firstLoop%"=="false" (
	SETLOCAL

	SET /A "count=%~3"
	SET /A "countAdd=0"
	SET "input=%inpTextOut%"
	SET "varSet=%~1"
	SET "prompt=%~2"
	CALL :AUX-LENGTHFETCH "%~2"
	SET "filter=%~4"
	SET "firstLoop=false"
	SET "tmpRND=%rndOut%"
	CALL :AUX-GENRND "15"
)

IF NOT "%lenOut%"=="" (
	SET /A "promptLen=%lenOut%+2"
	SET "inpOutFile=%rndOut%"
	SET "rndOut=%tmpRND%"
)

SET "input=%input::AINV:=''''%"

IF %count% GEQ 12 CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO. & ECHO.%input::QUOTE:="%%relayMsg% & SET /A "count=2+%countAdd%"

SET /A "countAdd=0"
SET /A "count=%count%+2"
SET "relayMsg="
SET "tmpVar="
SET "tmpVarDec="

IF "%~5"=="-Secure" (
	POWERSHELL -NoP -C "Write-Host """`n           %prompt%: """ -NoNewLine; [Console]::CursorVisible = $True; $SecIn = Read-Host -AsSecureString; [Console]::CursorVisible = $False; $SecConv = [System.Runtime.InteropServices.Marshal]::SecureStringToBSTR($SecIn); [System.Runtime.InteropServices.Marshal]::PtrToStringAuto($SecConv) | Out-File '%TEMP%\%inpOutFile%' -Encoding default" & SET /P "tmpVar=" < "%TEMP%\%inpOutFile%"
) ELSE (
	POWERSHELL -NoP -C "Write-Host """`n           %prompt%: """ -NoNewLine; [Console]::CursorVisible = $True; Read-Host | Out-File '%TEMP%\%inpOutFile%' -Encoding default; [Console]::CursorVisible = $False" & SET /P "tmpVar=" < "%TEMP%\%inpOutFile%"
)
DEL /Q /F "%TEMP%\%rndOut%" > NUL 2>&1
SET "tmpVar=%tmpVar:"=:AINV:%"
SET "tmpVar=%tmpVar:"=:AINV:%"
IF "%tmpVar%"==":AINV:=:AINV:" SET "tmpVar="

CALL :AUX-LENGTHFETCH "%tmpVar%"
SET /A "lineLen=%lenOut%+%promptLen%"
IF %lineLen% GEQ 69 SET /A "count=%count%+1" & SET /A "countAdd=%countAdd%+1"
SET /A "extLen=%lineLen%-69"
SET /A "extLenDiv=%extLen%/80"

IF NOT "%extLen:~0,1%"=="-" SET /A "count=%count%+%extLenDiv%+1" & SET /A "countAdd=%countAdd%+1"

SET /A "inSpace=69-%promptLen%"
IF %lenOut% GTR %inSpace% SET "tmpVarDec=..." & SET /A "inSpace=%inSpace%-4"

IF %count% GEQ 11 (
	SET "cancelOut= > NUL"
) ELSE (
    SET "cancelOut="
)

CALL :FILTERCALL-%filter%
	IF %ERRORLEVEL% EQU 5 (
		ENDLOCAL & SET "inpLenOut=%count%" & SET "inpTextOut=%input%" & SET "%varSet%=:None:" & EXIT /B 5
	)
	IF %ERRORLEVEL% EQU 3 (
		ENDLOCAL & EXIT /B 3
	)
	IF %ERRORLEVEL% EQU 1 (
		SET /A "count=%count%+1"
		SET /A "countAdd=%countAdd%+1"
		ECHO %relayMsg:~8%%cancelOut%
		GOTO :AUX-INPUTLOOP
	) ELSE (
		ENDLOCAL & SET "inpLenOut=%count%" & SET "inpTextOut=%input%" & SET "%varSet%=%tmpVar%"
		EXIT /B 0
	)
REM -----------------------------------------------------------



REM -----------------------------------------------------------
:AUX-LENGTHFETCH

SET "auxLenStr=%~1"
IF "%auxLenStr%"=="" SET "lenOut=0" & SET "lenAstOut=" & EXIT /B 0
SET "auxLenStr=%auxLenStr::AINV:=.%"
IF /I "%~2"=="-L" (
	SET "auxLenStr=%auxLenStr:\\=.%"
) ELSE (
	IF /I "%~3"=="-L" SET "auxLenStr=%auxLenStr:\\=.%"
)
SET /A "auxLen=0"
SET "auxAsterisks=****************************************************************************************************"

FOR /F "usebackq delims=" %%A IN (`POWERSHELL -NoP -C "'%auxLenStr:'=''%'.Length"`) DO SET "auxLen=%%A"

SET /A "auxLenRem=%auxLen%%%100"
SET /A "auxAstDiv=%auxLen%/100" 
CALL SET  "auxLenAst=%%auxAsterisks:~0,%auxLenRem%%%"

:LENGTHFETCH-LOOP

IF %auxAstDiv% LEQ 0 GOTO LENGTHFETCH-LOOPEND

SET /A "auxAstDiv=%auxAstDiv%-1"
CALL SET "auxLenAst=%auxLenAst%%auxAsterisks%"

GOTO :LENGTHFETCH-LOOP

:LENGTHFETCH-LOOPEND

IF /I "%~2"=="-Mask" (
	ENDLOCAL & SET "lenAstOut=%auxLenAst%" & SET "lenOut=%auxLen%"
	EXIT /B 0
) ELSE (
	IF /I "%~3"=="-Mask" (
		ENDLOCAL & SET "lenAstOut=%auxLenAst%" & SET "lenOut=%auxLen%"
		EXIT /B 0
	)
)

ENDLOCAL & SET "lenOut=%auxLen%"
EXIT /B 0
REM -----------------------------------------------------------



REM -----------------------------------------------------------
:AUX-WAITLOOP

SETLOCAL

SET "arg1=%~1"
SET "arg1=%arg1:-C:=%"
	IF NOT "%arg1%"=="%~1" SET "findArgs=/c:"
SET "arg1Tmp=%arg1:-X:=%"
	IF NOT "%arg1%"=="%arg1Tmp%" SET "findArgs=/X /c:"

IF "%~3"=="-TME" (
	SET "time=%~4"
) ELSE (
	IF "%~5"=="-TME" (
		SET "time=%~6"
	) ELSE (
		IF "%~7"=="-TME" (
		SET "time=%~8"
		) ELSE (
			SET "time=1500"
		)
	)
)
IF "%~3"=="-ALT" (
	SET "alt=%~4"
) ELSE (
	IF "%~5"=="-ALT" (
		SET "alt=%~6"
	) ELSE (
		IF "%~7"=="-ALT" (
		SET "alt=%~8"
		) ELSE (
			SET "alt=|:AME-NULL:|"
		)
	)
)

:WAITLOOP-MARKER

SET /A "auxWaitCount=%auxWaitCount%+1"

IF %auxWaitCount% GTR %time% ENDLOCAL & EXIT /B 1

TIMEOUT /T 1 /NOBREAK > NUL 2>&1

IF "%~3"=="-PID" (
	WMIC process where "name='cmd.exe' and ProcessId='%~4' or name='WindowsTerminal.exe' and ProcessId='%~4'" get name 2>&1 | FINDSTR /c:"No Instance(s) Available.">NUL 2>&1 && SET /A "auxWaitCount=%auxWaitCount%+500"
) ELSE (
	IF "%~5"=="-PID" (
		WMIC process where "name='cmd.exe' and ProcessId='%~6' or name='WindowsTerminal.exe' and ProcessId='%~6'" get name 2>&1 | FINDSTR /c:"No Instance(s) Available.">NUL 2>&1 && SET /A "auxWaitCount=%auxWaitCount%+500"
	) ELSE (
		IF "%~7"=="-PID" (
			WMIC process where "name='cmd.exe' and ProcessId='%~8' or name='WindowsTerminal.exe' and ProcessId='%~8'" get name 2>&1 | FINDSTR /c:"No Instance(s) Available.">NUL 2>&1 && SET /A "auxWaitCount=%auxWaitCount%+500"
		)
	)
)

IF /i "%arg1:"=:AINV:%"=="-WindowTitle" (
	FOR /F "usebackq tokens=2 delims= " %%A IN (`TASKLIST /FI "IMAGENAME eq cmd.exe" /FI "WINDOWTITLE eq %~2" /FO list /svc ^| FINDSTR /b /c:"PID: " ^|^| ECHO RESULT NULL`) DO (
		IF NOT "%%A"=="NULL" (
			ECHO "%%A" | FINDSTR "1 2 3 4 5 6 7 8 9 0">NUL 2>&1 && ENDLOCAL && SET "waitPIDOut=%%A" && EXIT /B 0
			ENDLOCAL & EXIT /B 1
			
		)
	)
	GOTO WAITLOOP-MARKER
)

FINDSTR /c:"%alt%" "%~2" > NUL 2>&1
	IF %ERRORLEVEL% EQU 0 (
		WSL -d %wslDistro% < "%TEMP%\[amecs]-WSLLin%rndOut%.txt" 2>&1 | FINDSTR /c:"|AME-WSLUSERTAKEN|" /c:"|AME-WSLGROUPTAKEN|">NUL 2>&1 && ECHO UserGroupCheck: Taken> "%TEMP%\[amecs]-WSLCom%rndOut%.txt" || ECHO UserGroupCheck: Open> "%TEMP%\[amecs]-WSLCom%rndOut%.txt"
	)

FINDSTR %findArgs%"%arg1%" "%~2" > NUL 2>&1 && ENDLOCAL && EXIT /B 0

FINDSTR /X /c:"AME-ERROR" "%~2" > NUL 2>&1 && ENDLOCAL && EXIT /B 2
FINDSTR /X /c:"AME-ERROR1" "%~2" > NUL 2>&1 && ENDLOCAL && EXIT /B 3
FINDSTR /X /c:"AME-ERROR2" "%~2" > NUL 2>&1 && ENDLOCAL && EXIT /B 4
GOTO WAITLOOP-MARKER
REM -----------------------------------------------------------



REM -----------------------------------------------------------
:AUX-FETCHLINK

SETLOCAL

SET /A "count0=0"

CALL :AUX-NETWORKCHECK
	IF %ERRORLEVEL% NEQ 0 ENDLOCAL & SET "fetchMsgOut=Internet is required for this action." & EXIT /B 5

SET "name=%~2"

PING -n 1 git.ameliorated.info -w 20000 > NUL 2>&1
	IF %ERRORLEVEL% NEQ 0 (
		ENDLOCAL & SET "fetchMsgOut=Link database unavailable."
		EXIT /B 5
	)

FOR /F "usebackq tokens=2,4,6,8,10 delims=|" %%A IN (`POWERSHELL -NoP -C "$ProgressPreference = 'SilentlyContinue'; (Invoke-WebRequest https://git.ameliorated.info/Joe/central-ame-script/src/branch/master/links.txt -UseBasicParsing | Select-Object -Property Content).Content" ^| FINDSTR /i /c:"%name% ="`) DO (
	SET "link=%%~A"
	SET "arg=%%~B"
	IF NOT "%%~C"=="" (
		SET "alt=true"
		SET "altLink=%%~C"
		SET "altArg=%%~D"
		SET "altName=%%~E"
	)
)

:FETCHLINK-MARKER

SET "link=%link:"=:AINV:%"
SET "link=%link:"=:AINV:%"

	ECHO "%link%" | FINDSTR /c:""""""" " /c:":AINV:" > NUL 2>&1
		IF %ERRORLEVEL% EQU 0 ENDLOCAL & SET "fetchMsgOut=Download link is invalid." & EXIT /B 5

		IF "%link%"=="REMOVED" ENDLOCAL & SET "fetchMsgOut=Download no longer available." & EXIT /B 5

ECHO "%link%" | FINDSTR /i /c:"apps.microsoft.com" > NUL 2>&1
	IF %ERRORLEVEL% EQU 0 (
		PING -n 1 store.rg-adguard.net -w 20000 > NUL 2>&1
			IF ERRORLEVEL 1 (
				ENDLOCAL & SET "fetchMsgOut=Failed to fetch download link. (2)" & EXIT /B 5
			)
		FOR /F usebackq^ tokens^=4^,13^ delims^=^" %%A IN (`POWERSHELL -NoP -C "$ProgressPreference = 'SilentlyContinue'; (Invoke-WebRequest -Method 'POST' -Uri 'https://store.rg-adguard.net/api/GetFiles' -Body 'type=url&url=%link%&ring=Retail' -UseBasicParsing | Select-Object -Property Content).Content" ^| FINDSTR /i "%arg%"`) DO (
			SET "link=%%~A"
			SET "size=%%~B"
			SET "haveLink=true"
		)
	) ELSE (
		SET "size=%Arg%"
		SET "haveLink=true"
	)
IF %count0% GEQ 3 (
	IF NOT "%retry1%"=="true" (
			IF "%alt%"=="true" (
				ECHO. & ECHO                  Failed to process link, trying another link...
				SET "retry1=true"
				SET "link=%altLink%"
				SET "arg=%altArg%"
				SET "name=%altName%"
				GOTO FETCHLINK-MARKER
				)
		)
		ENDLOCAL & SET "fetchMsgOut=Failed to fetch download link. (1)" & EXIT /B 5
)
IF NOT "%haveLink%"=="true" SET /A "count0=%count0%+1" & GOTO FETCHLINK-MARKER

SET "size=%size:</td></tr>=%"
SET "size=%size:>=%"
ECHO "%size%" | FINDSTR "MB KB GB">NUL 2>&1 || SET "size=0 MB"

IF NOT "%~3"=="-Download" ENDLOCAL & SET "linkOut=%link%" & SET "sizeOut=%size%" & SET "wslDistro=%name%" & EXIT /B 0

CALL :AUX-CENTERTEXT "%~5 (%size%)..."
ECHO. & ECHO %cenOut%
DEL /Q /F "%~4" > NUL 2>&1
CURL -L --progress-bar "%link%" --output "%~4"
	FOR %%A IN ("%~4") DO SET "compareSize=%%~zA"
		IF "%compareSize%"=="" SET "compareSize=0"
		CALL :AUX-KILOBYTEFETCH "%size%" -Compare "%compareSize%"
			IF %ERRORLEVEL% EQU 1 (
				IF NOT "%retry2%"=="true" (
					IF "%alt%"=="true" (
						ECHO "%altLink%" | FINDSTR /i /c:"apps.microsoft.com" > NUL 2>&1
							IF NOT ERRORLEVEL 1 (
								IF NOT "%homeWSLUnavailable1%"=="                 :REP:" ENDLOCAL & SET "fetchMsgOut=Failed to download files. (2)" & EXIT /B 5
							)
						ECHO. & ECHO                      Download failed, trying another link...
						SET "retry2=true"
						SET "link=%altLink%"
						SET "arg=%altArg%"
						SET "name=%altName%"
						SET /A "count0=0"
						GOTO FETCHLINK-MARKER
					)
				)
			DEL /Q /F "%~4" > NUL
			ENDLOCAL & SET "fetchMsgOut=Failed to download files. (1)" & EXIT /B 5
			)

ENDLOCAL & SET "wslDistro=%name%"
EXIT /B 0
REM -----------------------------------------------------------



REM -----------------------------------------------------------
:AUX-KILOBYTEFETCH

SETLOCAL ENABLEDELAYEDEXPANSION

SET "num1=%~1"
SET "num1=%num1: =.%"

FOR /F "usebackq delims=. tokens=1,2,3" %%A IN (`ECHO %num1%`) DO (
	SET "whole=%%A"
	IF "%%C"=="" (
		SET "type=%%B"
	) ELSE (
		CALL :AUX-LENGTHFETCH "%%B"
		SET "deci=%%B"
		SET "type=%%C"
	)

)
IF "%type%"=="MB" SET "zeros=000"
IF "%type%"=="GB" SET "zeros=000000"
SET "add=!zeros:~%lenOut%!"
ECHO "%add%" | FINDSTR /c:"~">NUL 2>&1 && SET "add="

SET "kilobytes=%whole%%deci%%add%"

IF NOT "%~2"=="-Compare" ENDLOCAL & SET "kiloOut=%kilobytes%" & EXIT /B 0

SET /A "num1Div=%kilobytes%/10"
SET /A "num1=%kilobytes%-%num1Div%"

SET "num2=%~3"
IF "%num2%"=="" SET "num2=0"
SET "num2=%num2:~0,-3%"
IF "%num2%"=="" SET "num2=0"

IF "%num2%" LSS "%num1%" (
	ENDLOCAL & EXIT /B 1
) ELSE (
	ENDLOCAL & EXIT /B 0
)
REM -----------------------------------------------------------



REM -----------------------------------------------------------
:AUX-GENRND

SETLOCAL ENABLEDELAYEDEXPANSION

:GENRND-MARKER

SET "RNDConsist=ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
SET /A "RND=%RANDOM% %% 36"
SET "RNDResult=!RNDResult!!RNDConsist:~%RND%,1!"
IF "!RNDResult:~%~1!"=="" GOTO GENRND-MARKER


ENDLOCAL & SET "rndOut=%RNDResult%" & EXIT /B 0
REM -----------------------------------------------------------



REM -----------------------------------------------------------
:AUX-ALTSTART

SETLOCAL

SET "taskRun=%~2"

CALL :AUX-GENRND "7"

CALL SET "taskRun=%%taskRun:|Script|=%userTemp%\[amecs]-amecs%rndOut%.cmd%%"
CALL SET "taskRun=%%taskRun:|rndOut|=%rndOut%%%"

COPY /Y "%scriptPath%" "%userTemp%\[amecs]-amecs%rndOut%.cmd" > NUL
SCHTASKS /CREATE /tn "[amecs]-%~1" /tr "%taskRun%" /sc ONLOGON /ru "%currentUsername%" /it /f > NUL
	IF %ERRORLEVEL% NEQ 0 SCHTASKS /DELETE /TN "[amecs]-%~1" /F>NUL 2>&1 & EXIT /B 1
POWERSHELL -NoP -C "$TaskSet = New-ScheduledTaskSettingsSet -AllowStartIfOnBatteries -DontStopIfGoingOnBatteries; Set-ScheduledTask -TaskName '[amecs]-%~1' -Settings $TaskSet" > NUL 2>&1
	IF %ERRORLEVEL% NEQ 0 SCHTASKS /DELETE /TN "[amecs]-%~1" /F>NUL 2>&1 & EXIT /B 2
REM RUNAS will work, however it requires the user to enter a password, and won't accept a blank one. This is a lot more simple and reliable
SCHTASKS /RUN /tn "[amecs]-%~1" > NUL
SCHTASKS /DELETE /tn "[amecs]-%~1" /f > NUL 2>&1
ENDLOCAL & SET "rndOut=%rndOut%" & EXIT /B 0
REM -----------------------------------------------------------


					REM ------------
					REM Filter Calls
					REM ------------


REM -----------------------------------------------------------
:FILTERCALL-1
REM WSL password filter

IF "%tmpVar%"=="" CALL :AUX-BACKLINE "36" && SET "input= & POWERSHELL -NoP -C :QUOTE:Write-Host '           %prompt:'=''%: ' -NoNewLine; Write-Host 'None' -ForegroundColor DarkGray:QUOTE:" && EXIT /B 5
CALL :AUX-LENGTHFETCH "%tmpVar%" -Mask
CALL SET "tmpVarIn=%%lenAstOut:~0,%inSpace%%%%tmpVarDec%"
SET "input= & POWERSHELL -NoP -C :QUOTE:Write-Host '           %prompt%: %tmpVarIn:'=''%':QUOTE:"

SET "tmpVar=%tmpVar:\=\\%"
ECHO "%tmpVar%" | FINDSTR /c:":AINV:">NUL 2>&1 && SET "relayMsg= & ECHO            Input cannot contain double quotes." && EXIT /B 1
EXIT /B 0

:FILTERCALL-2
REM WSL username filter

IF "%tmpVar%"=="" CALL :AUX-BACKLINE "36" && SET "input= & POWERSHELL -NoP -C :QUOTE:Write-Host '           %prompt:'=''%: ' -NoNewLine; Write-Host 'None' -ForegroundColor DarkGray:QUOTE:" && EXIT /B 5
CALL SET "tmpVarIn=%%tmpVar:~0,%inSpace%%%%tmpVarDec%"
SET "input= & POWERSHELL -NoP -C :QUOTE:Write-Host '           %prompt:'=''%: %tmpVarIn:'=''%':QUOTE:"

ECHO "%tmpVar%" | FINDSTR "\\ :AINV: ( ) ~ ` ! @ # %% ^ & * + = [ ] { } : ; , . < > ' | / ?">NUL 2>&1 && SET "relayMsg= & ECHO            Input cannot contain special characters except '_' ^& '-'." && EXIT /B 1
ECHO "%tmpVar%" | FINDSTR "A B C D E F G H I J K L M N O P Q R S T U V W X Y Z">NUL 2>&1 && SET "relayMsg= & ECHO            Input cannot contain capital letters." && EXIT /B 1
ECHO "%tmpVar%"| FINDSTR /c:" ">NUL 2>&1 && SET "relayMsg= & ECHO            Input cannot contain spaces." && EXIT /B 1
CALL :AUX-LENGTHFETCH "%tmpVar%"
	IF %lenOut% GEQ 32 SET "relayMsg= & ECHO            Input cannot exceed 32 characters." && EXIT /B 1
ECHO "%tmpVar:~0,1%" | FINDSTR /c:"-">NUL 2>&1 && SET "relayMsg= & ECHO            Input must follow the NAME_REGEX pattern." && EXIT /B 1
ECHO "%tmpVar:~0,-1%" | FINDSTR /c:"$">NUL 2>&1 && SET "relayMsg= & ECHO            Input must follow the NAME_REGEX pattern." && EXIT /B 1
ECHO getent groups ^| grep "^%tmpVar%" ^|^| getent group ^| grep "^%tmpVar%:" ^&^& echo "|AME-WSLUSERTAKEN|"; getent passwd ^| grep "^%tmpVar%:" ^&^& echo "|AME-WSLGROUPTAKEN|" > "%TEMP%\[amecs]-WSLLin%rndOut%.txt"
WSL -d %wslDistro% < "%TEMP%\[amecs]-WSLLin%rndOut%.txt" 2>&1 | FINDSTR /c:"|AME-WSLUSERTAKEN|" /c:"|AME-WSLGROUPTAKEN|" > NUL 2>&1
		IF %ERRORLEVEL% EQU 0 SET "relayMsg= & ECHO            Username or group name already taken." && EXIT /B 1
EXIT /B 0

:FILTERCALL-3
REM WSL ALTRUN username filter

IF "%tmpVar%"=="" CALL :AUX-BACKLINE "36" && SET "input= & POWERSHELL -NoP -C :QUOTE:Write-Host '           %prompt:'=''%: ' -NoNewLine; Write-Host 'None' -ForegroundColor DarkGray:QUOTE:" && EXIT /B 5
CALL SET "tmpVarIn=%%tmpVar:~0,%inSpace%%%%tmpVarDec%"
SET "input= & POWERSHELL -NoP -C :QUOTE:Write-Host '           %prompt:'=''%: %tmpVarIn:'=''%':QUOTE:"

ECHO "%tmpVar%" | FINDSTR "\\ :AINV: ( ) ~ ` ! @ # %% ^ & * + = [ ] { } : ; , . < > ' | / ?">NUL 2>&1 && SET "relayMsg= & ECHO            Input cannot contain special characters except '_' ^& '-'." && EXIT /B 1
ECHO "%tmpVar%" | FINDSTR "A B C D E F G H I J K L M N O P Q R S T U V W X Y Z">NUL 2>&1 && SET "relayMsg= & ECHO            Input cannot contain capital letters." && EXIT /B 1
ECHO "%tmpVar%"| FINDSTR /c:" ">NUL 2>&1 && SET "relayMsg= & ECHO            Input cannot contain spaces." && EXIT /B 1
CALL :AUX-LENGTHFETCH "%tmpVar%"
	IF %lenOut% GEQ 32 SET "relayMsg= & ECHO            Input cannot exceed 32 characters." && EXIT /B 1
ECHO "%tmpVar:~0,1%" | FINDSTR /c:"-">NUL 2>&1 && SET "relayMsg= & ECHO            Input must follow the NAME_REGEX pattern." && EXIT /B 1
ECHO "%tmpVar:~0,-1%" | FINDSTR /c:"$">NUL 2>&1 && SET "relayMsg= & ECHO            Input must follow the NAME_REGEX pattern." && EXIT /B 1
ECHO getent groups ^| grep "^%tmpVar%" ^|^| getent group ^| grep "^%tmpVar%:" ^&^& echo "|AME-WSLUSERTAKEN|"; getent passwd ^| grep "^%tmpVar%:" ^&^& echo "|AME-WSLGROUPTAKEN|" > "%userTemp%\[amecs]-WSLLin%rndOut%.txt"

ECHO AME-USERCHECK > "%userTemp%\[amecs]-WSLCom%rndOut%.txt"
CALL :AUX-WAITLOOP "-C:UserGroupCheck: " "%userTemp%\[amecs]-WSLCom%rndOut%.txt" -PID "%altRunPID%" -TME "12"
	IF %ERRORLEVEL% EQU 0 (
		FINDSTR /c:"UserGroupCheck: Taken" "%userTemp%\[amecs]-WSLCom%rndOut%.txt">NUL 2>&1 && SET "relayMsg= & ECHO            Username or group name already taken." && EXIT /B 1
	) ELSE (
		ECHO            WAITLOOP Error!
	)
EXIT /B 0

:FILTERCALL-4
REM Windows password change filter

IF "%tmpVar%"=="" (
	CALL :AUX-BACKLINE "52"
	SET "input= & POWERSHELL -NoP -C :QUOTE:Write-Host '           %prompt:'=''%: ':QUOTE:"
	NET user "%C4Username%" "%tmpVar%" /y > NUL 2>&1
		IF ERRORLEVEL 1 SET "relayMsg= & ECHO            Failed to change user password." && EXIT /B 1
	EXIT /B 0
)

CALL :AUX-LENGTHFETCH "%tmpVar%" -Mask
CALL SET "tmpVarIn=%%lenAstOut:~0,%inSpace%%%%tmpVarDec%"
SET "input= & POWERSHELL -NoP -C :QUOTE:Write-Host '           %prompt:'=''%: %tmpVarIn:'=''%':QUOTE:"

IF /I "%tmpVar%"=="Cancel" EXIT /B 3
ECHO "%tmpVar%" | FINDSTR /c:":AINV:">NUL 2>&1 && SET "relayMsg= & ECHO            Input cannot contain quotes." && EXIT /B 1

NET user "%C4Username%" "%tmpVar%" /y > NUL 2>&1
	IF %ERRORLEVEL% NEQ 0 SET "relayMsg= & ECHO            An error occured or the input was invalid." && EXIT /B 1
EXIT /B 0

:FILTERCALL-5
REM Windows username change filter

IF "%tmpVar%"=="" SET "relayMsg= & ECHO            Input cannot be blank." && SET "input= & POWERSHELL -NoP -C :QUOTE:Write-Host '           %prompt:'=''%: ':QUOTE:" && EXIT /B 1
CALL SET "tmpVarIn=%%tmpVar:~0,%inSpace%%%%tmpVarDec%"
SET "input= & POWERSHELL -NoP -C :QUOTE:Write-Host '           %prompt:'=''%: %tmpVarIn:'=''%':QUOTE:"

IF /I "%tmpVar%"=="Cancel" EXIT /B 3
ECHO "%tmpVar%" | FINDSTR /c:":AINV:">NUL 2>&1 && SET "relayMsg= & ECHO            Input cannot contain double quotes." && EXIT /B 1

TIMEOUT /T 1 /NOBREAK > NUL
FOR /F "usebackq tokens=3" %%A IN (`WMIC useraccount where "name='%currentUsername:'=\'%'" rename "%tmpVar%"  2^>^&1 ^| FINDSTR /c:"0;" /c:"Available." /c:"9;"`) DO SET "wmicOutput=%%A" > NUL 2>&1
	IF "%wmicOutput%"=="0;" EXIT /B 0
	REM This should only happen if the user changes their username AND closes/re-opens the .cmd before restarting.
	IF "%wmicOutput%"=="Available." SET "relayMsg= & ECHO            You must restart before changing your username again." && EXIT /B 1
	IF "%wmicOutput%"=="9;" SET "relayMsg= & ECHO            Invalid input." && EXIT /B 1

SET "relayMsg= & ECHO            Failed to parse WMIC output."
EXIT /B 1

:FILTERCALL-6
REM Windows password filter backline

IF /I "%tmpVar%"=="" CALL :AUX-BACKLINE "52" && SET "input= & POWERSHELL -NoP -C :QUOTE:Write-Host '           %prompt:'=''%: ' -NoNewLine; Write-Host 'None' -ForegroundColor DarkGray:QUOTE:" && EXIT /B 0
CALL :AUX-LENGTHFETCH "%tmpVar%" -Mask
CALL SET "tmpVarIn=%%lenAstOut:~0,%inSpace%%%%tmpVarDec%"
SET "input= & POWERSHELL -NoP -C :QUOTE:Write-Host '           %prompt:'=''%: %tmpVarIn:'=''%':QUOTE:"

IF /I "%tmpVar%"=="Cancel" EXIT /B 3
ECHO "%tmpVar%" | FINDSTR /c:":AINV:">NUL 2>&1 && SET "relayMsg= & ECHO            Input cannot contain quotes." && EXIT /B 1

EXIT /B 0

:FILTERCALL-7
REM Windows username add filter

IF "%tmpVar%"=="" SET "relayMsg= & ECHO            Input cannot be blank." && SET "input= & POWERSHELL -NoP -C :QUOTE:Write-Host '           %prompt:'=''%: ':QUOTE:" && EXIT /B 1
CALL :AUX-LENGTHFETCH "%tmpVar%" -Mask
CALL SET "tmpVarIn=%%tmpVar:~0,%inSpace%%%%tmpVarDec%"
SET "input= & POWERSHELL -NoP -C :QUOTE:Write-Host '           %prompt:'=''%: %tmpVarIn:'=''%':QUOTE:"

IF /I "%tmpVar%"=="Cancel" EXIT /B 3
ECHO "%tmpVar%" | FINDSTR /c:":AINV:">NUL 2>&1 && SET "relayMsg= & ECHO            Input cannot contain double quotes." && EXIT /B 1

NET user "%tmpVar%" /y > NUL 2>&1
	IF %ERRORLEVEL% EQU 0 SET "relayMsg= & ECHO            User already exists." & EXIT /B 1

NET user "%tmpVar%" /add /y > NUL 2>&1
	IF %ERRORLEVEL% NEQ 0 SET "relayMsg= & ECHO            Invalid input." && EXIT /B 1

NET user "%tmpVar%" /delete /y > NUL 2>&1
EXIT /B 0

:FILTERCALL-8
REM Windows username remove filter

IF "%tmpVar%"=="" SET "relayMsg= & ECHO            Input cannot be blank." && SET "input= & POWERSHELL -NoP -C :QUOTE:Write-Host '           %prompt:'=''%: ':QUOTE:" && EXIT /B 1
CALL SET "tmpVarIn=%%tmpVar:~0,%inSpace%%%%tmpVarDec%"
SET "input= & POWERSHELL -NoP -C :QUOTE:Write-Host '           %prompt:'=''%: %tmpVarIn:'=''%':QUOTE:"

IF /I "%tmpVar%"=="Cancel" EXIT /B 3
ECHO "%tmpVar%" | FINDSTR /c:":AINV:">NUL 2>&1 && SET "relayMsg= & ECHO            Input cannot contain double quotes." && EXIT /B 1

NET user "%tmpVar%" /y > NUL 2>&1
	IF %ERRORLEVEL% NEQ 0 SET "relayMsg= & ECHO            User does not exist." & EXIT /B 1
EXIT /B 0

:FILTERCALL-9
REM User password filter

IF "%tmpVar%"=="" (
	CALL :AUX-BACKLINE "59"
	SET "input= & POWERSHELL -NoP -C :QUOTE:Write-Host '           %prompt:'=''%: ' -NoNewLine; Write-Host 'None' -ForegroundColor DarkGray:QUOTE:"
	SCHTASKS /CREATE /TN "[amecs]-USERPASSCHECK" /TR "CMD /C 'SCHTASKS /DELETE /TN '[amecs]-USERPASSCHECK' /F'" /SC ONSTART /RU "%currentUsername%" /RP "" /F<NUL 2>&1 | FINDSTR /c:"blank passwords aren't allowed" > NUL 2>&1
		IF ERRORLEVEL 1 (
			SCHTASKS /DELETE /TN '[amecs]-USERPASSCHECK' /F > NUL 2>&1
			SET "relayMsg= & ECHO            Password is invalid." & EXIT /B 1
		)
	EXIT /B 0
)

CALL :AUX-LENGTHFETCH "%tmpVar%" -Mask
CALL SET "tmpVarIn=%%lenAstOut:~0,%inSpace%%%%tmpVarDec%"
SET "input= & POWERSHELL -NoP -C :QUOTE:Write-Host '           %prompt:'=''%: %tmpVarIn:'=''%':QUOTE:"
IF /I "%tmpVar%"=="Cancel" EXIT /B 3
ECHO "%tmpVar%" | FINDSTR /c:":AINV:">NUL 2>&1 && SET "relayMsg= & ECHO            Input cannot contain double quotes." && EXIT /B 1


SCHTASKS /CREATE /TN "[amecs]-USERPASSCHECK" /TR "CMD /C 'SCHTASKS /DELETE /TN '[amecs]-USERPASSCHECK' /F'" /SC ONSTART /RU "%currentUsername%" /RP "%tmpVar%" /F<NUL > NUL 2>&1
	IF %ERRORLEVEL% NEQ 0 (
		SCHTASKS /DELETE /TN "[amecs]-USERPASSCHECK" /F > NUL 2>&1
		SET "relayMsg= & ECHO            Password is invalid." & EXIT /B 1
	)


SCHTASKS /DELETE /TN "[amecs]-USERPASSCHECK" /F > NUL 2>&1
EXIT /B 0
REM -----------------------------------------------------------


					REM ------
					REM ALTRUN
					REM ------


REM -----------------------------------------------------------
:ALTPARENT-WSL-DISTROINSTALL

SETLOCAL

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO.

IF "%wslUnattend%"=="true" GOTO :WSL-DISTROUNATTEND

DISM /Online /Get-FeatureInfo:Microsoft-Windows-Subsystem-Linux /English | FINDSTR /x /c:"State : Enabled" > NUL 2>&1
	IF %ERRORLEVEL% NEQ 0 (
			POWERSHELL -NoP -C "Write-Host """`n`n                                WSL is disabled.""" -ForegroundColor Red; Write-Host """           __________________________________________________________`n`n           Would you like to enable it now? ^(Y/N^): """ -NoNewLine; [Console]::CursorVisible = $True; CHOICE /C YN /N /M '%BS%'; [Console]::CursorVisible = $False; EXIT $LastExitCode"
			IF ERRORLEVEL 2 ENDLOCAL & GOTO HOME-WSL
			IF ERRORLEVEL 1 ENDLOCAL & GOTO WSL-ENABLE
	)
WHERE wsl.exe > NUL 2>&1
	IF %ERRORLEVEL% NEQ 0  CALL :AUX-RETURN "A restart is required for WSL functionality." "HOME-WSL" -E
REM ::::::::::::::::::::::::::::P-WSL1-AR::::::::::::::::::::::::::::
ECHO. & ECHO                            Launching alt installer...
CALL :AUX-ALTSTART "WSLDistroInstall" "CMD /C 'START /min '' POWERSHELL -NoP -C 'Start-Process ''''|Script|'''' -ArgumentList ''''wslInstall'''',''''|rndOut|'''' -WindowStyle Hidden'"
	IF %ERRORLEVEL% EQU 1 CALL :AUX-RETURN "Failed to create scheduled task. (1)" "HOME-WSL" -E -C
	IF %ERRORLEVEL% EQU 2 CALL :AUX-RETURN "Failed to create scheduled task. (2)" "HOME-WSL" -E -C

REM CALL :AUX-ALTSTART "WSLDistroInstall" "CMD /K '|Script| wslInstall |rndOut|'"

CALL :AUX-WAITLOOP -WindowTitle "AMECS-AltRun-%rndOut%" -TME "30"
	IF ERRORLEVEL 1  CALL :AUX-RETURN "Failed to launch alt process." "HOME-WSL" -E -C
	SET "altRunPID=%waitPIDOut%"
	ECHO "Distro: |%wslDistro%|">> "%userTemp%\[amecs]-WSLCom%rndOut%.txt"
	ECHO "PID: |%scriptPID%|">> "%userTemp%\[amecs]-WSLCom%rndOut%.txt"

	CALL :AUX-WAITLOOP "-C:Reg: " "%userTemp%\[amecs]-WSLCom%rndOut%.txt" -PID "%altRunPID%"
		IF NOT ERRORLEVEL 1 (
			FINDSTR /c:"Reg: True" "%userTemp%\[amecs]-WSLCom%rndOut%.txt" > NUL && SET "distroReg=true"
			FINDSTR /c:"Reg: False" "%userTemp%\[amecs]-WSLCom%rndOut%.txt" > NUL && SET "distroReg=false"
		) ELSE (
			TASKKILL /F /T /PID "%altRunPID%" > NUL 2>&1
		)
REM :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

DIR /B /A:d "%userLocalAppData%\AME-WSL" 2>&1 | FINDSTR /X /c:"%wslDistro%" > NUL 2>&1 && SET "distroFiles=true"

IF "%distroReg%"=="true" (
	TASKKILL /F /T /PID "%altRunPID%" > NUL 2>&1
	CALL :AUX-RETURN "Distro is already installed." "HOME-WSL" -C -E
) ELSE (
	IF "%distroFiles%"=="true" (
		POWERSHELL -NoP -C "Write-Host """`n`n                  Traces of previous distro installation found.""" -ForegroundColor Red; Write-Host """           __________________________________________________________`n`n           Remove installation files and reinstall distro? ^(Y/N^): """ -NoNewLine; [Console]::CursorVisible = $True; CHOICE /C NY /N /M '%BS%'; [Console]::CursorVisible = $False; EXIT $LastExitCode"
			IF ERRORLEVEL 2 (
				CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO.
				ECHO. & ECHO                          Removing installation files...
				TIMEOUT /T 1 /NOBREAK > NUL 2>&1
				FOR /F "usebackq delims=" %%A IN (`DIR /B /A:d "%userLocalAppData%\AME-WSL" 2^>^&1 ^| FINDSTR /X /c:"%wslDistro%"`) DO (
					FOR /F "usebackq delims=" %%B IN (`DIR /S /B "%userLocalAppData%\AME-WSL\%%A\*.exe" 2^>^&1`) DO (
						FOR /F "usebackq delims=" %%C IN (`POWERSHELL -NoP -C "Get-Process | Where-Object {$_.Path -eq '%%~B'} | Select-Object -ExpandProperty Id" 2^>^&1`) DO (
							TASKKILL /F /T /PID "%%~C" > NUL 2>&1
						)
					)
					RMDIR /Q /S "%userLocalAppData%\AME-WSL\%%A" > NUL
				)
			) ELSE (
				TASKKILL /F /T /PID "%altRunPID%" > NUL 2>&1
				DEL /Q /F "%userTemp%\[amecs]*%rndOut%.*" > NUL 2>&1
				ENDLOCAL & GOTO HOME-WSL
			)
	)
)

IF "%wslDistro%"=="Alpine" (
	SET "wslLShell=/bin/ash"
	SET "sudo="
) ELSE (
	SET "wslLShell=/bin/bash"
	SET "sudo=sudo "
)

POWERSHELL -NoP -C "Write-Host """`n           A Linux distro must be downloaded`n           Continue? ^(Y/N^): """ -NoNewLine; [Console]::CursorVisible = $True; CHOICE /C YN /N /M '%BS%'; [Console]::CursorVisible = $False; EXIT $LastExitCode"
	IF %ERRORLEVEL%==2 TASKKILL /F /T /PID "%altRunPID%">NUL 2>&1 & DEL /Q /F "%userTemp%\[amecs]*%rndOut%.*">NUL 2>&1 & ENDLOCAL & GOTO HOME-MAINMENU

ECHO. & ECHO                             Fetching download link...

CALL :AUX-FETCHLINK "HOME-WSL" "%wslDistro%" -Download "%TEMP%\[amecs]-%wslDistro%%rndOut%.zip" "Downloading distro"
	IF %ERRORLEVEL% EQU 5 TASKKILL /F /T /PID "%altRunPID%">NUL 2>&1 & CALL :AUX-RETURN "%fetchMsgOut%" "HOME-WSL" -E -C

ECHO. & ECHO                       Preparing distro for installation...

IF NOT EXIST "%userLocalAppData%\AME-WSL" MKDIR "%userLocalAppData%\AME-WSL"
RMDIR /Q /S "%userLocalAppData%\AME-WSL\%wslDistro%%rndOut%-Tmp" > NUL 2>&1
MKDIR "%userLocalAppData%\AME-WSL\%wslDistro%%rndOut%-Tmp"

POWERSHELL -NoP -C "Expand-Archive -LiteralPath '%TEMP%\[amecs]-%wslDistro%%rndOut%.zip' -DestinationPath '%userLocalAppData%\AME-WSL\%wslDistro%%rndOut%-Tmp'"
DEL /Q /F "%TEMP%\[amecs]-%wslDistro%%rndOut%.zip"
FOR /F "usebackq delims=" %%A IN (`DIR /B "%userLocalAppData%\AME-WSL\%wslDistro%%rndOut%-Tmp" ^| FINDSTR /i ".*_x64\.appx .*_x64\.msix .*\.exe"`) DO (
	IF /i "%%~xA"==".exe" (
		RENAME "%userLocalAppData%\AME-WSL\%wslDistro%%rndOut%-Tmp" "%wslDistro%"
		SET "wslExe=%userLocalAppData%\AME-WSL\%wslDistro%\%%~A"
		SET "wslExeName=%%~nxA"
	) ELSE (
		RENAME "%userLocalAppData%\AME-WSL\%wslDistro%%rndOut%-Tmp\%%~A" "%%~nA.zip"
		MKDIR "%userLocalAppData%\AME-WSL\%wslDistro%"
		POWERSHELL -NoP -C "$ProgressPreference = 'SilentlyContinue'; Expand-Archive -LiteralPath '%userLocalAppData%\AME-WSL\%wslDistro%%rndOut%-Tmp\%%~nA.zip' -DestinationPath '%userLocalAppData%\AME-WSL\%wslDistro%'"
		RMDIR /Q /S "%userLocalAppData%\AME-WSL\%wslDistro%%rndOut%-Tmp"

		FOR /F "usebackq delims=" %%B IN (`DIR /B "%userLocalAppData%\AME-WSL\%wslDistro%" ^| FINDSTR /i ".*\.exe"`) DO (
			IF /i "%%~B"=="PengwinUI.exe" (
				COPY "%userLocalAppData%\AME-WSL\%wslDistro%\DistroLauncher\pengwin.exe" "%userLocalAppData%\AME-WSL\%wslDistro%\pengwin.exe" /y > NUL
				SET "wslExe=%userLocalAppData%\AME-WSL\%wslDistro%\pengwin.exe"
				SET "wslExeName=pengwin.exe"
			) ELSE (
				SET "wslExe=%userLocalAppData%\AME-WSL\%wslDistro%\%%~B"
				SET "wslExeName=%%~nxB"
			)
		)
	)
)

IF "%wslExe%"=="" (
	TASKKILL /F /T /PID "%altRunPID%">NUL 2>&1
	DEL /Q /F "%userTemp%\[amecs]-%wslDistro%%rndOut%.zip" > NUL
	RMDIR /Q /S "%userLocalAppData%\AME-WSL\%wslDistro%%rndOut%-Tmp" > NUL 2>&1
	RMDIR /Q /S "%userLocalAppData%\AME-WSL\%wslDistro%" > NUL 2>&1
	CALL :AUX-RETURN "Failed to locate distro executable." "HOME-WSL" -E -C
)

REM ::::::::::::::::::::::::::::P-WSL2-AR::::::::::::::::::::::::::::
ECHO "Exe: |%wslExe%|" >> "%userTemp%\[amecs]-WSLCom%rndOut%.txt"
REM :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

SET "count1=0"

ECHO. & ECHO                    Installing distro, this may take awhile...

:ALTPARENT-WSL-DISTROPROGRESS

IF %count1% GTR 800 (
	IF "%distroPID%"=="" (
		IF "%distroHostPID%"=="" (
			TASKKILL /F /T /PID "%altRunPID%">NUL 2>&1
			CALL :AUX-RETURN "Failed to fetch distro process IDs." "HOME-WSL" -C -E
		)
	)
	TASKKILL /F /T /PID "%altRunPID%" /PID "%distroPID%" /PID "%distroHostPID%" > NUL 2>&1
	CALL :AUX-RETURN "Distro installation timed out." "HOME-WSL" -C -E
)

FINDSTR "1 2 3 4 5 6 7 8 9 0" "%userTemp%\[amecs]-DistroHostPID%rndOut%.txt" > NUL 2>&1
	IF %ERRORLEVEL% EQU 0 SET /P "distroHostPID=" < "%userTemp%\[amecs]-DistroHostPID%rndOut%.txt"
FINDSTR "1 2 3 4 5 6 7 8 9 0" "%userTemp%\[amecs]-DistroPID%rndOut%.txt" > NUL 2>&1
	IF %ERRORLEVEL% EQU 0 SET /P "distroPID=" < "%userTemp%\[amecs]-DistroPID%rndOut%.txt"

FINDSTR /X /c:"AME-ERROR" "%userTemp%\[amecs]-WSLCom%rndOut%.txt">NUL 2>&1 && SET /A "count2=%count2%+500"
FINDSTR /X /c:"AME-INPUTREQ" "%userTemp%\[amecs]-WSLCom%rndOut%.txt">NUL 2>&1 && GOTO ALTPARENT-WSL-CONFIG
TASKLIST /FI "IMAGENAME eq cmd.exe" /FI "PID eq %altRunPID%" 2>&1 | FINDSTR /i /c:"cmd.exe">NUL 2>&1 || SET /A "count1=%count1%+70"
TIMEOUT /T 2 /NOBREAK > NUL 2>&1
SET /A "count1=%count2%+1"
GOTO ALTPARENT-WSL-DISTROPROGRESS

:ALTPARENT-WSL-CONFIG

TASKKILL /F /T /PID "%distroPID%" /PID "%distroHostPID%" > NUL 2>&1

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO.

CALL :AUX-INPUTLOOP "wslRootPass" "Enter new root password" "0" "1" -Secure

CALL :AUX-INPUTLOOP "wslUser" "Enter new UNIX username" "%inpLenOut%" "3"

IF NOT "%wslUser%"==":None:" CALL :AUX-INPUTLOOP "wslUserPass" "Enter new UNIX password" "%inpLenOut%" "1" -Secure

IF NOT "%wslRootPass%"==":None:" SET "wslRootArg=echo -e """"%wslRootPass%\n%wslRootPass%"""" | passwd """"root"""" && "

IF NOT "%wslUser%"==":None:" SET "wslUserArg=useradd -m -G %wslGroups% -s %wslLShell% """"%wslUser%"""" && echo -e """"\n[user]\ndefault=%wslUser%"""" >> """"/etc/wsl.conf"""" && "

IF NOT "%wslUserPass%"==":None:" SET "wslUserPassArg=echo -e """"%wslUserPass%\n%wslUserPass%"""" | passwd """"%wslUser%"""" && "

POWERSHELL -NoP -C "Write-Host -NoNewLine '%wslRootArg%%sudo%%wslUserArg%%wslUserPassArg%echo """"Blank""""'" > "%userTemp%\[amecs]-WSLLin%rndOut%.txt" 2>&1

ECHO "Username: |%wslUser%|" > "%userTemp%\[amecs]-WSLCom%rndOut%.txt"

ECHO AME-INPUTSENT>> "%userTemp%\[amecs]-WSLCom%rndOut%.txt"

CALL :AUX-WAITLOOP "-C:AME-DONE" "%userTemp%\[amecs]-WSLCom%rndOut%.txt" -PID "%altRunPID%" -TME "150"
	IF %ERRORLEVEL% NEQ 0 (
		TASKKILL /F /T /PID "%altRunPID%" > NUL 2>&1
		CALL :AUX-RETURN "Distro account setup timed out." "HOME-WSL" -C -E
	)

TASKKILL /F /T /PID "%altRunPID%" > NUL 2>&1

ECHO "%wslDistro%" | FINDSTR /b /c:""""SUSE-Linux-Enterprise" /c:""""SLES-" > NUL 2>&1
	IF %ERRORLEVEL% EQU 0 CALL :AUX-RETURN "Distro installed successfully" "HOME-WSL" -L "Use the SUSEConnect command to license this distro." -C

CALL :AUX-RETURN "Distro installed successfully" -H -C
REM -----------------------------------------------------------
REM |
REM |
REM |
REM -----------------------------------------------------------
:ALTCHILD-WSL-DISTROINSTALL

REM ::::::::::::::::::::::::::::C-WSL1-2A::::::::::::::::::::::::::::
SET "rndOut=%~2"
TITLE AMECS-AltRun-%rndOut%
SET /A "count0=0" & SET /A "count1=0" 

CALL :AUX-WAITLOOP "-C:Distro: |" "%TEMP%\[amecs]-WSLCom%rndOut%.txt"
	IF NOT ERRORLEVEL 1 (
		FOR /F "usebackq tokens=2 delims=|" %%A IN (`FINDSTR /c:"Distro: |" "%TEMP%\[amecs]-WSLCom%rndOut%.txt"`) DO (
			SET "wslDistro=%%A"
			FOR /F "usebackq tokens=2 delims=|" %%B IN (`FINDSTR /c:"PID: |" "%TEMP%\[amecs]-WSLCom%rndOut%.txt"`) DO (
				SET "parentPID=%%B"

				POWERSHELL -NoP -C "[console]::OutputEncoding = [Text.UnicodeEncoding]::Unicode; WSL -l -q | FINDSTR /X /c:'%%A'">NUL 2>&1 && ECHO "Reg: True" >> "%TEMP%\[amecs]-WSLCom%rndOut%.txt" || ECHO "Reg: False" >> "%TEMP%\[amecs]-WSLCom%rndOut%.txt"
				CALL :AUX-WAITLOOP "-C:Exe: " "%TEMP%\[amecs]-WSLCom%rndOut%.txt" -PID "%%B" -TME "86400"
					IF NOT ERRORLEVEL 1 (
						FOR /F "usebackq tokens=2 delims=|" %%C IN (`FINDSTR /c:"Exe: |" "%TEMP%\[amecs]-WSLCom%rndOut%.txt"`) DO SET "wslExe=%%C"
					) ELSE (
						EXIT 1
					)
			)
		)
	) ELSE (
		EXIT 1
	)
REM :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

POWERSHELL -NoP -C "(Start-Process 'CMD' -ArgumentList '/K','POWERSHELL -NoP -C """"(Start-Process ''%wslExe:'=''''%'' -NoNewWindow -PassThru).Id | Out-File -LiteralPath ''%TEMP:'=''''%\[amecs]-DistroPID%rndOut%.txt'' -Encoding default""""' -WindowStyle Hidden -PassThru).Id" 1> "%TEMP%\[amecs]-DistroHostPID%rndOut%.txt"

:ALTCHILD-WSL-DISTROPROGRESS

TIMEOUT /T 2 /NOBREAK > NUL

IF %count0% GEQ 15 (
	TASKKILL /F /T /PID "%distroPID%" /PID "%distroHostPID%" > NUL 2>&1
	ECHO AME-ERROR>> "%TEMP%\[amecs]-WSLCom%rndOut%.txt"
)

IF %count1% GTR 500 (
	TASKKILL /F /T /PID "%distroPID%" /PID "%distroHostPID%" > NUL 2>&1
	ECHO AME-ERROR>> "%TEMP%\[amecs]-WSLCom%rndOut%.txt"
)

FINDSTR "1 2 3 4 5 6 7 8 9 0" "%TEMP%\[amecs]-DistroHostPID%rndOut%.txt" > NUL 2>&1
	IF %ERRORLEVEL% NEQ 0 (
		SET /A "count0=%count0%+1"
		GOTO ALTCHILD-WSL-DISTROPROGRESS
	) ELSE (
		SET /P "distroHostPID=" < "%TEMP%\[amecs]-DistroHostPID%rndOut%.txt"
	)
FINDSTR "1 2 3 4 5 6 7 8 9 0" "%TEMP%\[amecs]-DistroPID%rndOut%.txt" > NUL 2>&1
	IF %ERRORLEVEL% NEQ 0 (
		SET /A "count0=%count0%+1"
		GOTO ALTCHILD-WSL-DISTROPROGRESS
	) ELSE (
		SET /P "distroPID=" < "%TEMP%\[amecs]-DistroPID%rndOut%.txt"
	)

POWERSHELL -NoP -C "[console]::OutputEncoding = [Text.UnicodeEncoding]::Unicode; WSL -l -q | FINDSTR /X /c:'%wslDistro%'" > NUL 2>&1
	IF %ERRORLEVEL% EQU 0 (
		TASKKILL /F /T /PID "%distroPID%" /PID "%distroHostPID%" > NUL 2>&1
		GOTO ALTCHILD-WSL-CONFIG
	)

TASKLIST /FI "IMAGENAME eq cmd.exe" /FI "PID eq %distroHostPID%" 2>&1 | FINDSTR /i /c:"cmd.exe">NUL 2>&1 || SET /A "count1=%count1%+50"

SET /A "count1=%count1%+1"
GOTO ALTCHILD-WSL-DISTROPROGRESS

:ALTCHILD-WSL-CONFIG

ECHO AME-INPUTREQ>> "%TEMP%\[amecs]-WSLCom%rndOut%.txt"
	CALL :AUX-WAITLOOP "-C:AME-INPUTSENT" "%TEMP%\[amecs]-WSLCom%rndOut%.txt" -PID "%parentPID%" -ALT "AME-USERCHECK" -TME "86400"
		IF %ERRORLEVEL% GEQ 1 EXIT 0

FOR /F "usebackq tokens=2 delims=|" %%A IN (`FINDSTR /c:"Username: |" "%TEMP%\[amecs]-WSLCom%rndOut%.txt"`) DO SET "wslUser=%%A" 

WSL -d %wslDistro% < "%TEMP%\[amecs]-WSLLin%rndOut%.txt" > NUL 2>&1
WSL -t %wslDistro% > NUL 2>&1


ECHO AME-DONE>> "%TEMP%\[amecs]-WSLCom%rndOut%.txt" & EXIT 0
REM -----------------------------------------------------------



REM -----------------------------------------------------------
:ALTPARENT-WSL-DISTROREMOVE

CLS & ECHO. & ECHO            __________________________________________________________ & ECHO. & ECHO                            ^| Central AME Script %ver% ^| & ECHO.

ECHO. & ECHO                           Checking installed distros...
CALL :AUX-ALTSTART "WSLDistroRemove" "CMD /C 'START /min '' POWERSHELL -NoP -C 'Start-Process ''''|Script|'''' -ArgumentList ''''wslRemove'''',''''|rndOut|'''' -WindowStyle Hidden'"
	IF %ERRORLEVEL% EQU 1 CALL :AUX-RETURN "Failed to create scheduled task. (1)" "HOME-WSL" -E -C
	IF %ERRORLEVEL% EQU 2 CALL :AUX-RETURN "Failed to create scheduled task. (2)" "HOME-WSL" -E -C

REM CALL :AUX-ALTSTART "WSLDistroRemove" "CMD /K '|Script| wslRemove |rndOut|'"

CALL :AUX-WAITLOOP -WindowTitle "AMECS-AltRun-%rndOut%" -TME "30"
	IF ERRORLEVEL 1 CALL :AUX-RETURN "Failed to launch alt process." "HOME-WSL" -C -E

	SET "altRunPID=%waitPIDOut%"
	ECHO "Distro: |%wslDistro%|">> "%userTemp%\[amecs]-WSLCom%rndOut%.txt"
	ECHO "PID: |%scriptPID%|">> "%userTemp%\[amecs]-WSLCom%rndOut%.txt"

	CALL :AUX-WAITLOOP "-C:Reg: " "%userTemp%\[amecs]-WSLCom%rndOut%.txt" -PID "%altRunPID%"
		IF NOT ERRORLEVEL 1 (
			FINDSTR /c:"Reg: True" "%userTemp%\[amecs]-WSLCom%rndOut%.txt" > NUL && SET "distroReg=true"
			FINDSTR /c:"Reg: False" "%userTemp%\[amecs]-WSLCom%rndOut%.txt" > NUL && SET "distroReg=false"
		) ELSE (
			TASKKILL /F /T /PID "%altRunPID%" > NUL 2>&1
		)

IF "%distroReg%"=="" TASKKILL /F /T /PID "%altRunPID%">NUL 2>&1 & CALL :AUX-RETURN "Failed to check registered distros." "HOME-WSL" -C -E

DIR /B /A:d "%userLocalAppData%\AME-WSL" 2>&1 | FINDSTR /X /c:"%wslDistro%" > NUL 2>&1 && SET "distroRemFiles=true"

IF NOT "%distroReg%"=="true" (
	TASKKILL /F /T /PID "%altRunPID%" > NUL 2>&1
	IF NOT "%distroRemFiles%"=="true" CALL :AUX-RETURN "Distro is not installed." "HOME-WSL" -C -E
)

POWERSHELL -NoP -C "Write-Host """`n           Are you sure you want to remove this distro? ^(Y/N^): """ -NoNewLine; [Console]::CursorVisible = $True; CHOICE /C YN /N /M '%BS%'; [Console]::CursorVisible = $False; EXIT $LastExitCode"
	IF %ERRORLEVEL%==2 TASKKILL /F /T /PID "%altRunPID%">NUL 2>&1 & ENDLOCAL & GOTO HOME-MAINMENU

ECHO. & ECHO                                Removing distro...

IF "%distroReg%"=="true" (
	ECHO "Remove: |True|">> "%userTemp%\[amecs]-WSLCom%rndOut%.txt"
	CALL :AUX-WAITLOOP "-C:AME-REMDONE" "%userTemp%\[amecs]-WSLCom%rndOut%.txt" -PID "%altRunPID%"
		IF NOT ERRORLEVEL 1 (
			TASKKILL /F /T /PID "%altRunPID%" > NUL 2>&1
		) ELSE (
			TASKKILL /F /T /PID "%altRunPID%" > NUL 2>&1
			CALL :AUX-RETURN "Failed to unregister distro." "HOME-WSL" -E
		)

)
FOR /F "usebackq delims=" %%A IN (`DIR /B /A:d "%userLocalAppData%\AME-WSL" 2^>^&1 ^| FINDSTR /X /c:"%wslDistro%"`) DO (
	FOR /F "usebackq delims=" %%B IN (`DIR /S /B "%userLocalAppData%\AME-WSL\%%A\*.exe" 2^>^&1`) DO (
		FOR /F "usebackq delims=" %%C IN (`POWERSHELL -NoP -C "Get-Process | Where-Object {$_.Path -eq '%%~B'} | Select-Object -ExpandProperty Id" 2^>^&1`) DO (
			TASKKILL /F /T /PID "%%~C" > NUL 2>&1
		)
	)
	RMDIR /Q /S "%userLocalAppData%\AME-WSL\%%A" > NUL
)

FOR /F "usebackq delims=" %%A IN (`DIR /B "%userLocalAppData%\AME-WSL" 2^>^&1`) DO SET "contentsEmpty=false"
IF NOT "%contentsEmpty%"=="false" RMDIR /Q /S "%userLocalAppData%\AME-WSL" > NUL 2>&1

TIMEOUT /T 1 /NOBREAK > NUL

CALL :AUX-RETURN "Distro removed successfully" -H -C
REM -----------------------------------------------------------
REM |
REM |
REM |
REM -----------------------------------------------------------
:ALTCHILD-WSL-DISTROREMOVE

SET "rndOut=%~2"
TITLE AMECS-AltRun-%rndOut%

CALL :AUX-WAITLOOP "-C:Distro: |" "%TEMP%\[amecs]-WSLCom%rndOut%.txt"
	IF NOT ERRORLEVEL 1 (
		FOR /F "usebackq tokens=2 delims=|" %%A IN (`FINDSTR /c:"Distro: " "%TEMP%\[amecs]-WSLCom%rndOut%.txt"`) DO (
			SET SET "wslDistro=%%A"
			FOR /F "usebackq tokens=2 delims=|" %%B IN (`FINDSTR /c:"PID: " "%TEMP%\[amecs]-WSLCom%rndOut%.txt"`) DO (
				SET "parentPID=%%B"
				POWERSHELL -NoP -C "[console]::OutputEncoding = [Text.UnicodeEncoding]::Unicode; WSL -l -q | FINDSTR /X /c:'%%A'">NUL 2>&1 && ECHO "Reg: True" >> "%TEMP%\[amecs]-WSLCom%rndOut%.txt" || ECHO "Reg: False" >> "%TEMP%\[amecs]-WSLCom%rndOut%.txt"
					IF ERRORLEVEL 1 EXIT 0
					CALL :AUX-WAITLOOP "-C:Remove: |True|" "%TEMP%\[amecs]-WSLCom%rndOut%.txt" -PID "%%B" -TME "86400"
						IF NOT ERRORLEVEL 1 (
							WSL --unregister %%A > NUL 2>&1
								IF NOT ERRORLEVEL 0 (
									ECHO AME-ERROR>> "%TEMP%\[amecs]-WSLCom%rndOut%.txt"
								) ELSE (
								ECHO AME-REMDONE>> "%TEMP%\[amecs]-WSLCom%rndOut%.txt"
								)
						) ELSE (
							EXIT 0
						)
			)
		)
	) ELSE (
		EXIT 0
	)
REM -----------------------------------------------------------


					REM -------------------
					REM Encoded Executables
					REM -------------------


REM -----------------------------------------------------------
REM Encoded AutoLogon executable modifed/forked from https://github.com/rzander/AutoLogon developed by Roger Zander

-----BEGIN CERTIFICATE-----
TVqQAAMAAAAEAAAA//8AALgAAAAAAAAAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
AAAAAAAAAAAAAAAAgAAAAA4fug4AtAnNIbgBTM0hVGhpcyBwcm9ncmFtIGNhbm5v
dCBiZSBydW4gaW4gRE9TIG1vZGUuDQ0KJAAAAAAAAABQRQAATAEDADrD0GIAAAAA
AAAAAOAAAgELAQsAABQAAAAIAAAAAAAAnjIAAAAgAAAAQAAAAABAAAAgAAAAAgAA
BAAAAAAAAAAEAAAAAAAAAACAAAAAAgAAAAAAAAMAQIUAABAAABAAAAAAEAAAEAAA
AAAAABAAAAAAAAAAAAAAAEQyAABXAAAAAEAAANgEAAAAAAAAAAAAAAAAAAAAAAAA
AGAAAAwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
AAAAAAAAAAAAIAAACAAAAAAAAAAAAAAACCAAAEgAAAAAAAAAAAAAAC50ZXh0AAAA
pBIAAAAgAAAAFAAAAAIAAAAAAAAAAAAAAAAAACAAAGAucnNyYwAAANgEAAAAQAAA
AAYAAAAWAAAAAAAAAAAAAAAAAABAAABALnJlbG9jAAAMAAAAAGAAAAACAAAAHAAA
AAAAAAAAAAAAAAAAQAAAQgAAAAAAAAAAAAAAAAAAAACAMgAAAAAAAEgAAAACAAUA
iCQAALwNAAABAAAACAAABgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
AAAAAAAAAAAAAAAAAAAAACYAAv4VAwAAAioAABMwBACmAQAAAQAAEQAWagp+BwAA
CgsWDAgoCAAACgsSA/4VAwAAAh8gEwR+BwAAChMFEgb+FQQAAAISBhZ9BAAABBIG
fgcAAAp9BQAABBIGFn0HAAAEEgZ+BwAACn0IAAAEEgZ+BwAACn0JAAAEEgMSBhEE
EgUoAgAABhMHEQcoBAAABm4KBhZq/gETCxELLSQAcgEAAHAGjA4AAAEoCQAACigK
AAAKAB0oCwAACgAAOO4AAAAAF40DAAACEwgRCBaPAwAAAv4VAwAAAhEIFo8DAAAC
AigMAAAKfQMAAAQRCBaPAwAAAgJvDQAAChha0X0BAAAEEQgWjwMAAAICbw0AAAoX
WBha0X0CAAAEF40DAAACEwkRCRaPAwAAAv4VAwAAAhEJFo8DAAACAygMAAAKfQMA
AAQRCRaPAwAAAgNvDQAAChha0X0BAAAEEQkWjwMAAAIDbw0AAAoXWBha0X0CAAAE
EQURCBEJKAMAAAYmBhZq/gETCxELLR8AcikAAHAGjA4AAAEoCQAACigKAAAKAB0o
CwAACgAAEQUoBQAABiYABygBAAAGJgYTCisAEQoqAAAbMAQALgIAAAIAABEAcw4A
AAoKAAITBRYTBisgEQURBpoLAAYHKA8AAApvEAAACm8RAAAKJgARBhdYEwYRBhEF
jmn+BBMHEQct0gZyYwAAcG8SAAAKFv4BEwcRBzqeAAAAAAB+EwAACgwIcm0AAHAX
bxQAAAoMCHLZAABwcvkAAHBvFQAACgAIcvsAAHAWbxYAAAoACHIZAQBwFm8WAAAK
AAhyNwEAcBZvFgAACgAIclUBAHAWbxYAAAoACHJrAQBwFm8WAAAKAHJrAQBwcvkA
AHAoBwAABiYA3iINAHKLAQBwCW8XAAAKKBgAAAooCgAACgAdKAsAAAoAAN4AAAAG
bxkAAAoY/gQTBxEHOigBAAAAAH4TAAAKDAhybQAAcBdvFAAACgwIctkAAHACFppv
FQAACgAIcpsBAHAoGgAACm8VAAAKAAhy+wAAcHK/AQBwbxUAAAoACHJrAQBwFm8W
AAAKAAZvGQAAChj+Ahb+ARMHEQctTQACGJoSBCgbAAAKFv4BEwcRBy02ABEEFv4C
Fv4BEwcRBy0XAAhyGQEAcBEEjBcAAAFvFQAACgAAKw8ACHIZAQBwFm8WAAAKAAAA
ACsPAAhyGQEAcBZvFgAACgAABnLDAQBwbxIAAAoW/gETBxEHLRMACHJVAQBwcr8B
AHBvFQAACgAAcmsBAHACF5ooBwAABiZy2wEAcCgKAAAKAADeIg0AcosBAHAJbxcA
AAooGAAACigKAAAKAB0oCwAACgAA3gAAACoAAEE0AAAAAAAAVgAAAHkAAADPAAAA
IgAAABYAAAEAAAAABgEAAAMBAAAJAgAAIgAAABYAAAEeAigcAAAKKkJTSkIBAAEA
AAAAAAwAAAB2NC4wLjMwMzE5AAAAAAUAbAAAAAwEAAAjfgAAeAQAAJQFAAAjU3Ry
aW5ncwAAAAAMCgAADAIAACNVUwAYDAAAEAAAACNHVUlEAAAAKAwAAJQBAAAjQmxv
YgAAAAAAAAACAAABVx8CFAkCAAAA+iUzABYAAAEAAAAXAAAABgAAABcAAAAJAAAA
DQAAAAEAAAAcAAAADQAAAAMAAAACAAAAAgAAAAUAAAABAAAAAQAAAAMAAAAAAAoA
AQAAAAAABgCEAH0ABgCLAH0ABgCVAH0ABgChAH0ABgAdA/4CBgBkA30ABgCaA3oD
BgC6A3oDBgDYA/4CBgABBP4CBgAXBP4CBgAiBH0ABgAuBP4CBgBDBH0ABgBJBH0A
BgBXBH0ABgBpBH0ABgCrBJgEBgDKBLUEBgAQBQAFBgAZBQAFBgBSBX0ABgCCBX0A
AAAAAAEAAAAAAAEAAQCAARAAFgAAAAUAAQABAAoBEAAoAAAACQABAAYACgEQADsA
AAAJAAQABwACAQAAUQAAABEACgAHAAAAEABiAGoABQAYAAcABgDvADEABgD2ADEA
BgAEATQABgDvADsABgATATQABgAhAT4ABgAsAUIABgA3ATQABgBKATQABgZjAUUA
VoBrAUgAVoCJAUgAVoCnAUgAVoDGAUgAVoDZAUgAVoDvAUgAVoAEAkgAVoAcAkgA
VoA8AkgAVoBaAkgAVoBxAkgAVoCFAkgAVoCZAkgAAAAAAIAAliCmAAoAAQAAAAAA
gACWIK4ADwACAAAAAACAAJYgvAAcAAYAAAAAAIAAliDQACcACQAAAAAAgACWIOYA
LAAKAFAgAAAAAOYBCwE3AAsAXCAAAAAAlgCtAsEACwAQIgAAAACRALcCxwANAIAk
AAAAAIYYvAI3AA4AAAABAMICAAABAMcCAAACANICAAADAOMCAgAEAPECAAABAPEC
AAACACoDAAADADIDAAABAD4DAAABAEUDAAABAFIDAAACAFoDAAABAF8DAwANACkA
vAI3ADEAvALNADkAvALSAEEAvAI3AEkAvALXAFEAvALcAGEAKQQ0AGkANgTiAHkA
UATnAIEAXwTtAIkAdQTyAGkAegT3AHkAjQT8AJEAvAI3AJkA1gQVAXkA6wQaAZEA
8wQgAZEA9wQlAaEAJQUqAakAMgUuAakAPQU1AakARgU7AbEAXAVBAXkAUARFAZEA
aAX8AIkAcgVLAbkAiAVPAQkAvAI3AAoALABMAAoAMABVAAoANABeAAoAOABnAAoA
PABwAAoAQAB5AAoARACCAAoASACLAAoATACUAAoAUACdAAoAVACmAAoAWACvAAoA
XAC4AC4AEwBlAS4AGwBrAS4AIwB0AQABVgHrA/QDAAEDAKYAAQAAAQUArgACAEAB
BwC8AAIAAAEJANAAAgAAAQsA5gACAASAAAAAAAAAAAAAAAAAAAAAAGIAAAAEAAAA
AAAAAAAAAAABAHQAAAAAAAMAAgAEAAIABQACAAAAAAAAPE1vZHVsZT4AUHJvZ3Jh
bS5leGUAU2FmZU5hdGl2ZU1ldGhvZHMATFNBX1VOSUNPREVfU1RSSU5HAExTQV9P
QkpFQ1RfQVRUUklCVVRFUwBMU0FfQWNjZXNzUG9saWN5AFByb2dyYW0AQXV0b0xv
Z29uAG1zY29ybGliAFN5c3RlbQBPYmplY3QAVmFsdWVUeXBlAElEaXNwb3NhYmxl
AEVudW0ARnJlZVNpZABMc2FPcGVuUG9saWN5AExzYVN0b3JlUHJpdmF0ZURhdGEA
THNhTnRTdGF0dXNUb1dpbkVycm9yAExzYUNsb3NlAExlbmd0aABNYXhpbXVtTGVu
Z3RoAEJ1ZmZlcgBEaXNwb3NlAFJvb3REaXJlY3RvcnkAT2JqZWN0TmFtZQBBdHRy
aWJ1dGVzAFNlY3VyaXR5RGVzY3JpcHRvcgBTZWN1cml0eVF1YWxpdHlPZlNlcnZp
Y2UAdmFsdWVfXwBQT0xJQ1lfVklFV19MT0NBTF9JTkZPUk1BVElPTgBQT0xJQ1lf
VklFV19BVURJVF9JTkZPUk1BVElPTgBQT0xJQ1lfR0VUX1BSSVZBVEVfSU5GT1JN
QVRJT04AUE9MSUNZX1RSVVNUX0FETUlOAFBPTElDWV9DUkVBVEVfQUNDT1VOVABQ
T0xJQ1lfQ1JFQVRFX1NFQ1JFVABQT0xJQ1lfQ1JFQVRFX1BSSVZJTEVHRQBQT0xJ
Q1lfU0VUX0RFRkFVTFRfUVVPVEFfTElNSVRTAFBPTElDWV9TRVRfQVVESVRfUkVR
VUlSRU1FTlRTAFBPTElDWV9BVURJVF9MT0dfQURNSU4AUE9MSUNZX1NFUlZFUl9B
RE1JTgBQT0xJQ1lfTE9PS1VQX05BTUVTAFBPTElDWV9OT1RJRklDQVRJT04AU3Rv
cmVEYXRhAE1haW4ALmN0b3IAcFNpZABTeXN0ZW1OYW1lAE9iamVjdEF0dHJpYnV0
ZXMARGVzaXJlZEFjY2VzcwBQb2xpY3lIYW5kbGUAU3lzdGVtLlJ1bnRpbWUuSW50
ZXJvcFNlcnZpY2VzAE91dEF0dHJpYnV0ZQBLZXlOYW1lAFByaXZhdGVEYXRhAHN0
YXR1cwBPYmplY3RIYW5kbGUAa2V5TmFtZQBEYXRhAGFyZ3MAQ0xTQ29tcGxpYW50
QXR0cmlidXRlAFN5c3RlbS5SdW50aW1lLkNvbXBpbGVyU2VydmljZXMAQ29tcGls
YXRpb25SZWxheGF0aW9uc0F0dHJpYnV0ZQBSdW50aW1lQ29tcGF0aWJpbGl0eUF0
dHJpYnV0ZQBEbGxJbXBvcnRBdHRyaWJ1dGUAYWR2YXBpMzIAYWR2YXBpMzIuZGxs
AFN0cnVjdExheW91dEF0dHJpYnV0ZQBMYXlvdXRLaW5kAEludFB0cgBaZXJvAE1h
cnNoYWwAQWxsb2NIR2xvYmFsAEludDY0AFN0cmluZwBDb25jYXQAQ29uc29sZQBX
cml0ZUxpbmUARW52aXJvbm1lbnQARXhpdABTdHJpbmdUb0hHbG9iYWxVbmkAZ2V0
X0xlbmd0aABTeXN0ZW0uQ29sbGVjdGlvbnMAQXJyYXlMaXN0AFN5c3RlbS5HbG9i
YWxpemF0aW9uAEN1bHR1cmVJbmZvAGdldF9JbnZhcmlhbnRDdWx0dXJlAFRvVXBw
ZXIAQWRkAENvbnRhaW5zAE1pY3Jvc29mdC5XaW4zMgBSZWdpc3RyeQBSZWdpc3Ry
eUtleQBMb2NhbE1hY2hpbmUAT3BlblN1YktleQBTZXRWYWx1ZQBEZWxldGVWYWx1
ZQBFeGNlcHRpb24AZ2V0X01lc3NhZ2UAZ2V0X0NvdW50AGdldF9NYWNoaW5lTmFt
ZQBJbnQzMgBUcnlQYXJzZQAAAAAAJ08AcABlAG4AUABvAGwAaQBjAHkAIABmAGEA
aQBsAGUAZAA6ACAAADlMAHMAYQBTAHQAbwByAGUAUAByAGkAdgBhAHQAZQBEAGEA
dABhACAAZgBhAGkAbABlAGQAOgAgAAAJLwBEAEUATAAAa1MATwBGAFQAVwBBAFIA
RQBcAE0AaQBjAHIAbwBzAG8AZgB0AFwAVwBpAG4AZABvAHcAcwAgAE4AVABcAEMA
dQByAHIAZQBuAHQAVgBlAHIAcwBpAG8AbgBcAFcAaQBuAGwAbwBnAG8AbgAAH0QA
ZQBmAGEAdQBsAHQAVQBzAGUAcgBOAGEAbQBlAAABAB1BAHUAdABvAEEAZABtAGkA
bgBMAG8AZwBvAG4AAB1BAHUAdABvAEwAbwBnAG8AbgBDAG8AdQBuAHQAAB1GAG8A
cgBjAGUAQQB1AHQAbwBMAG8AZwBvAG4AABVEAGkAcwBhAGIAbABlAEMAQQBEAAAf
RABlAGYAYQB1AGwAdABQAGEAcwBzAHcAbwByAGQAAA9FAHIAcgBvAHIAOgAgAAAj
RABlAGYAYQB1AGwAdABEAG8AbQBhAGkAbgBOAGEAbQBlAAADMQAAFy8ARABJAFMA
QQBCAEwARQBDAEEARAAALUEAdQB0AG8AbABvAGcAbwBuACAAYQBjAHQAaQB2AGEA
dABlAGQALgAuAC4AAAAAAFaAIBFOwL1Hhay/5JRgKxwACLd6XFYZNOCJBAABGBgM
AAQJEBEMEBEQCBAYCgADCRgdEQwdEQwEAAEJCQQAAQkYAgYHAgYYAyAAAQIGCAMG
EQwCBgkCBgoDBhEUCAEAAAAAAAAACAIAAAAAAAAACAQAAAAAAAAACAgAAAAAAAAA
CBAAAAAAAAAACCAAAAAAAAAACEAAAAAAAAAACIAAAAAAAAAACAABAAAAAAAACAAC
AAAAAAAACAAEAAAAAAAACAAIAAAAAAAACAAQAAAAAAAABQACCg4OBQABAR0OBCAB
AQIEIAEBCAQgAQEOBSABAREtBAABGAgFAAIOHBwEAAEBDgQAAQEIBAABGA4DIAAI
FAcMChgIEQwIGBEQCR0RDB0RDAoCBAAAEk0FIAEOEk0EIAEIHAQgAQIcAwYSVQYg
AhJVDgIFIAIBDhwFIAIBDgIDIAAOBQACDg4OAwAADgYAAgIOEAgOBwgSSQ4SVRJZ
CB0OCAIFAQABAAAIAQAIAAAAAAAeAQABAFQCFldyYXBOb25FeGNlcHRpb25UaHJv
d3MBAGwyAAAAAAAAAAAAAI4yAAAAIAAAAAAAAAAAAAAAAAAAAAAAAAAAAACAMgAA
AAAAAAAAAAAAAAAAAAAAAAAAX0NvckV4ZU1haW4AbXNjb3JlZS5kbGwAAAAAAP8l
ACBAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAgAQAAAAIAAAgBgAAAA4AACA
AAAAAAAAAAAAAAAAAAABAAEAAABQAACAAAAAAAAAAAAAAAAAAAABAAEAAABoAACA
AAAAAAAAAAAAAAAAAAABAAAAAACAAAAAAAAAAAAAAAAAAAAAAAABAAAAAACQAAAA
oEAAAEQCAAAAAAAAAAAAAOhCAADqAQAAAAAAAAAAAABEAjQAAABWAFMAXwBWAEUA
UgBTAEkATwBOAF8ASQBOAEYATwAAAAAAvQTv/gAAAQAAAAAAAAAAAAAAAAAAAAAA
PwAAAAAAAAAEAAAAAQAAAAAAAAAAAAAAAAAAAEQAAAABAFYAYQByAEYAaQBsAGUA
SQBuAGYAbwAAAAAAJAAEAAAAVAByAGEAbgBzAGwAYQB0AGkAbwBuAAAAAAAAALAE
pAEAAAEAUwB0AHIAaQBuAGcARgBpAGwAZQBJAG4AZgBvAAAAgAEAAAEAMAAwADAA
MAAwADQAYgAwAAAALAACAAEARgBpAGwAZQBEAGUAcwBjAHIAaQBwAHQAaQBvAG4A
AAAAACAAAAAwAAgAAQBGAGkAbABlAFYAZQByAHMAaQBvAG4AAAAAADAALgAwAC4A
MAAuADAAAAA4AAwAAQBJAG4AdABlAHIAbgBhAGwATgBhAG0AZQAAAFAAcgBvAGcA
cgBhAG0ALgBlAHgAZQAAACgAAgABAEwAZQBnAGEAbABDAG8AcAB5AHIAaQBnAGgA
dAAAACAAAABAAAwAAQBPAHIAaQBnAGkAbgBhAGwARgBpAGwAZQBuAGEAbQBlAAAA
UAByAG8AZwByAGEAbQAuAGUAeABlAAAANAAIAAEAUAByAG8AZAB1AGMAdABWAGUA
cgBzAGkAbwBuAAAAMAAuADAALgAwAC4AMAAAADgACAABAEEAcwBzAGUAbQBiAGwA
eQAgAFYAZQByAHMAaQBvAG4AAAAwAC4AMAAuADAALgAwAAAAAAAAAO+7vzw/eG1s
IHZlcnNpb249IjEuMCIgZW5jb2Rpbmc9IlVURi04IiBzdGFuZGFsb25lPSJ5ZXMi
Pz4NCjxhc3NlbWJseSB4bWxucz0idXJuOnNjaGVtYXMtbWljcm9zb2Z0LWNvbTph
c20udjEiIG1hbmlmZXN0VmVyc2lvbj0iMS4wIj4NCiAgPGFzc2VtYmx5SWRlbnRp
dHkgdmVyc2lvbj0iMS4wLjAuMCIgbmFtZT0iTXlBcHBsaWNhdGlvbi5hcHAiLz4N
CiAgPHRydXN0SW5mbyB4bWxucz0idXJuOnNjaGVtYXMtbWljcm9zb2Z0LWNvbTph
c20udjIiPg0KICAgIDxzZWN1cml0eT4NCiAgICAgIDxyZXF1ZXN0ZWRQcml2aWxl
Z2VzIHhtbG5zPSJ1cm46c2NoZW1hcy1taWNyb3NvZnQtY29tOmFzbS52MyI+DQog
ICAgICAgIDxyZXF1ZXN0ZWRFeGVjdXRpb25MZXZlbCBsZXZlbD0iYXNJbnZva2Vy
IiB1aUFjY2Vzcz0iZmFsc2UiLz4NCiAgICAgIDwvcmVxdWVzdGVkUHJpdmlsZWdl
cz4NCiAgICA8L3NlY3VyaXR5Pg0KICA8L3RydXN0SW5mbz4NCjwvYXNzZW1ibHk+
DQoAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
AAAAAAAAAAAAAAAAAAAAAAAwAAAMAAAAoDIAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
-----END CERTIFICATE-----
REM -----------------------------------------------------------