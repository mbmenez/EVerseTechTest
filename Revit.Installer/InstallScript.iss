; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "EVerseTechTestApp"
#define MyAppVersion "1.0.0"
#define MyAppPublisher "MBM"

[Setup]
; NOTE: The value of AppId uniquely identifies this application. Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{738C3863-690B-44E1-9EDD-13227F7C5FE4}}
AppName={#MyAppName}
#define installerPath "C:\"
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
DefaultDirName={autopf}\EVerseTechTest
DisableDirPage=yes
DefaultGroupName=EVerseTechTestApp
DisableProgramGroupPage=yes
; PrivilegesRequired=lowest
OutputDir=.\Output
SetupIconFile="C:\Users\Mariana Menezes\source\repos\EVerseTechTest\Resources\Images\EVerseLogo30x30.ico"
OutputBaseFilename={#MyAppName} {#MyAppVersion}
Compression=lzma
SolidCompression=yes
WizardStyle=modern

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Files]
Source: "..\EVerseTechTest\bin\Debug\*.dll"; DestDir: "C:\ProgramData\Autodesk\Revit\Addins\2024\"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "..\EVerseTechTest\EVerseTechTestApp.addin"; DestDir: "C:\ProgramData\Autodesk\Revit\Addins\2024\"; Flags: ignoreversion recursesubdirs createallsubdirs


; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"