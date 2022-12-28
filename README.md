# Central AME Script (amecs)

Script for automating a large assortment of AME related actions.

![Central AME Script Screenshot](screenshot.png)

## Usage

You can download the script by going to the [latest release](https://git.ameliorated.info/Styris/amecs/releases/latest) and selecting `amecs.zip` from the **Downloads** section. Once downloaded, simply extract `amecs.zip` and run `amecs.cmd`.

Alternatively, you could clone the repository:

    git clone https://git.ameliorated.info/Styris/amecs.git

Once cloned, simply run `amecs.cmd`.

## Primary Functions

There are many actions in AME that require commands, the following functions essentially work as an interactive user interface for those.

#### Username/Password

This function allows for changing the user's username or password.

At its core, the following command is used for changing the username:

    wmic useraccount where "name='<User's Username>'" rename '<New Username>'

Similarly, the following command is used for changing the password:

    net user "<User's Username>" "<New Password>"

#### Lockscreen Image

This function allows for changing the lockscreen image.

It works by taking ownership of the existing profile image files, and replacing them with the new image supplied by the user.

This is a modified version of [LoganDark's lockscreen-img script](https://git.ameliorated.info/LoganDark/lockscreen-img).

#### Profile Image

This function allows for changing the user's profile image (PFP).

It does this by taking ownership of the existing profile image files, and replacing them with the new image supplied by the user. Several necessary registry changes are made as well.

This is a modified version of [LoganDark's profile-img script](https://git.ameliorated.info/LoganDark/profile-img).

#### User Elevation

This function allows for elevating or de-elevating the user to or from administrator. Elevating the user disables the password requirement when trying to run an executable as administrator. However, this has large security implications, thus why it is not the default setting.

At its core, it uses the following command:

    net localgroup administrators "<User's Username>" /add

Or the following for de-elevating the user:

    net localgroup administrators "<User's Username>" /delete

#### Display Language

This function allows for changing the user's display language.

Firstly, it prompts the user to download a portion of a \~5.5GB language pack ISO file. Unfortunately, Microsoft no longer publicly distributes individual language pack files, so this is necessary.

Once the ISO is downloaded, it extracts the ISO file, and installs the language pack for the selected display language using the following commands:

    7z e -y -o"<Script Path>\LangPacks" "<User's Desktop>\LangPacks.ISO" x64\langpacks\*.cab
    lpksetup /i <Language/region ID> /p "<User's Desktop>\LangPacks\Microsoft-Windows-Client-Language-Pack_x64_<Language/region ID>.cab"

After the language pack is installed, the display language can finally be set. At its core, this is done by using the following commands:

    PowerShell -NoP -C "Set-WinSystemLocale <Language/region ID>"
    PowerShell -NoP -C "Set-WinUserLanguageList <Language/region ID>"

The Language/region ID for a given language can be found [here](https://docs.microsoft.com/en-us/windows-hardware/manufacture/desktop/available-language-packs-for-windows?view=windows-11#language-packs).

The script heavily enchances these simple commands. For instance, it keeps the existing keyboard input method (language), and let's the user decide whether or not to make the new language the default keyboard input method.

#### Keyboard Language

These functions allow for adding or removing a keyboard language.

At its core, this is done by using the following command:

    PowerShell -NoP -C "$NewLangs=Get-WinUserLanguageList; $NewLangs[0].InputMethodTips.Add('<Language/region ID>:<Keyboard Identifier>'); Set-WinUserLanguageList $NewLangs -Force"

If the user chose to make their selection the new default input method, the following command will also be run:

    PowerShell -NoP -C "Set-WinDefaultInputMethodOverride -InputTip "<Language/region ID>:<Keyboard Identifier>""

The Language/region ID and Keyboard identifier for a given language can be found [here](https://docs.microsoft.com/en-us/windows-hardware/manufacture/desktop/available-language-packs-for-windows?view=windows-11#language-packs) and [here](https://docs.microsoft.com/en-us/windows-hardware/manufacture/desktop/windows-language-pack-default-values?view=windows-11) respectively.

To remove a keyboard language, it fetches the existing language list, filters out the selected language, and sets the modified language list.

#### Username Login Requirement

This function allows for disabling or enabling the username login requirement.

At its core, it uses the following command:

    reg delete "HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System" /v dontdisplaylastusername /f

Or the following for enabling the requirement:

    reg add "HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System" /v dontdisplaylastusername /t REG_DWORD /d 1 /f

#### AutoLogon

This function allows for enabling or disabling the automatic login of the current user.

It does this by extracting an `AutoLogon.exe` file embedded within the script, and subsequently using it to enable AutoLogon.

The mentioned `AutoLogon.exe` program is a modified version of [rzander's AutoLogon program](https://github.com/rzander/AutoLogon).

## Extra Functions

This section contains beta, legacy, or less used functions. Legacy functions are only useful for versions of AME predating the [REDACTED].

#### Windows Subsystem for Linux (WSL)

These functions allow for various WSL related actions.

##### Enable/Disable WSL

This function allows for enabling or disabling WSL functionality.

At its core, this is done by using the following command:

    DISM /Online /Enable-Feature /FeatureName:Microsoft-Windows-Subsystem-Linux -NoRestart

Or the following for disabling the requirement:

    DISM /Online /Disable-Feature /FeatureName:Microsoft-Windows-Subsystem-Linux -NoRestart 

##### Install WSL Distro

This function allows for installing a WSL distribution.

Firstly, it fetches the distro download link from this git repository (`links.txt`), and downloads the distro from said link\*. Once the distro is downloaded, the script extracts and places the distro in `<User's Local AppData Folder>\AME-WSL`, and subsequently installs the distro by running the distro's `.exe` file contained within.

After the distro has been installed, the script sends several commands to the distro that apply the username and password(s) supplied by the user.

\*If the distro link includes apps.microsoft.com, [store.rg-adguard.net](https://store.rg-adguard.net/) is used to fetch the download link.

##### Remove WSL Distro

This function allows for removing a WSL distribution.

At its core, it does this by unregistering the distro using the following command:

    WSL --unregister <WSL Distro>

And subsequently removing the distro's installation files located in `<User's Local AppData Folder>\AME-WSL`.

#### Hibernation

This function allows for enabling or disabling the hibernation option in Windows.

At its core, the following commands are used:

    powercfg /HIBERNATE /TYPE FULL

Or the following for disabling hibernation:

    powercfg /HIBERNATE OFF

#### Notification Center

This function allows for enabling or disabling the Notification Center in the bottom right of the taskbar.

At its core, it uses the following command:

    reg add "HKU\<User's SID>\Software\Policies\Microsoft\Windows\Explorer" /v DisableNotificationCenter /t REG_DWORD /d 0 /f

Or the following for disabling the Notification Center:

    reg add "HKU\<User's SID>\Software\Policies\Microsoft\Windows\Explorer" /v DisableNotificationCenter /t REG_DWORD /d 1 /f

#### Desktop Notifications

This function allows for enabling or disabling desktop toast notifications.

At its core, it uses the following command:

    reg add "HKU\<User's SID>\SOFTWARE\Microsoft\Windows\CurrentVersion\PushNotifications" /v ToastEnabled /t REG_DWORD /d 1 /f

Or the following for disabling desktop notifications:

    reg add "HKU\<User's SID>\SOFTWARE\Microsoft\Windows\CurrentVersion\PushNotifications" /v ToastEnabled /t REG_DWORD /d 0 /f

#### Windows Script Host (Legacy)

This function allows for enabling or disabling Windows Script Host (WSH). WSH is necessary for some programs.

At its core, the following commands are used:

    reg add "HKU\<User's SID>\SOFTWARE\Microsoft\Windows Script Host\Settings" /v Enabled /t REG_DWORD /d 1 /f
    reg add "HKLM\SOFTWARE\Microsoft\Windows Script Host\Settings" /v Enabled /t REG_DWORD /d 1 /f

Or the following for disabling WSH:

    reg add "HKU\<User's SID>\SOFTWARE\Microsoft\Windows Script Host\Settings" /v Enabled /t REG_DWORD /d 0 /f
    reg add "HKLM\SOFTWARE\Microsoft\Windows Script Host\Settings" /v Enabled /t REG_DWORD /d 0 /f

#### Visual Basic Script (Legacy)

This function allows for enabling or disabling Visual Basic Script (VBS). VBS is necessary for some programs.

At its core, the following command is used:

    assoc .vbs=VBSFile

Or the following for disabling VBS:

    assoc .vbs=

#### NCSI Active Probing (Legacy)

This function allows for enabling or disabling NCSI Active Probing. Some applications require this to be enabled.

At its core, it uses the following command:

    reg add "HKLM\SYSTEM\CurrentControlSet\Services\NlaSvc\Parameters\Internet" /v EnableActiveProbing /t REG_DWORD /d 1 /f

Or the following for disabling NCSI Active Probing:

    reg add "HKLM\SYSTEM\CurrentControlSet\Services\NlaSvc\Parameters\Internet" /v EnableActiveProbing /t REG_DWORD /d 0 /f

#### New User

This function allows for creating partially functional and pre-configured users in Windows AME.

To do this, it uses a standard user creation command, followed by many registry edits to make the new user usable.

#### NVIDIA Control Panel

This function allows for installing or uninstalling NVIDIA Control Panel. This is useful since NVIDIA Control Panel no longer properly installs during a driver installation* due to it using an APPX deployment (Not supported in AME).

Firstly, it fetches the store link from this git repository (`links.txt`), and fetches the download link from [store.rg-adguard.net](https://store.rg-adguard.net/). Afterwards, it downloads NVIDIA Control Panel from said link\*. Once NVIDIA Control Panel is downloaded, the script extracts and places it in `<System Drive>\Program Files\NVIDIA Control Panel`, and subsequently creates a start menu shortcut for it.

For removal, it simply removes the `<System Drive>\Program Files\NVIDIA Control Panel` directory, as well as the start menu shortcut.

Note: Even though NVIDIA Control Panel installation failes during a driver installation, it still creates the necessary NVIDIA Control Panel files in `<System Drive>\Program Files\WindowsApps`. If this is the case, the script will attempt to use those files instead of downloading them.

\*Standard drivers do not have this issue, however those have been discontinued by NVIDIA.

## Known Issues

Some keyboard languages may not work, and a few are improperly tagged.