#define MyAppName "Scuoladellestelle"
#define MyAppVersion "1.0"
#define MyAppPublisher "Andrea Tombesi"
#define MyAppExeName "scuola_delle_stelle.exe" ; <--- SCRIVI QUI IL NOME ESATTO DEL TUO .EXE (es. Gestionale.exe)
#define MySrcPath "G:\My\My_Sorgenti\Visualstudio\Scuola_delle_Stelle\bin" ; <--- SCRIVI QUI IL NOME ESATTO DEL TUO .EXE (es. Gestionale.exe)

[Setup]
AppId={{83C77845-DBA0-4B34-AAD9-7E1E21335D05}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}

; Cartella di installazione automatica
;DefaultDirName={autopf}\{#MyAppName}
UninstallDisplayIcon={app}\{#MyAppExeName}
DefaultDirName={userdesktop}\Scuoladellestelle
; Nome del file di installazione che verrà creato
OutputBaseFilename=Installazione_Scuoladellestelle
OutputDir=G:\My\My_Sorgenti\Visualstudio\Scuola_delle_Stelle\_SETUP
Compression=lzma2
SolidCompression=yes

; MODIFICATO: Interfaccia moderna con passaggi chiari (mostra la fine dell'installazione)
WizardStyle=modern
; 1. Forza la comparsa della pagina di selezione cartella (impostato su 'no')
DisableDirPage=no

; 2. CRUCIALE: Dice a Inno Setup di NON ricordarsi della vecchia installazione.
; In questo modo chiederà la cartella ad OGNI singolo avvio del setup.
UsePreviousAppDir=no
[Languages]
; MODIFICATO: Ora l'installer parlerà in Italiano!
Name: "italian"; MessagesFile: "compiler:Languages\Italian.isl"

[Tasks]
; Crea la spunta per l'icona sul desktop
Name: "desktopicon"; Description: "Crea una icona sul Desktop"; GroupDescription: "Icone aggiuntive:"; Flags: unchecked

[Files]
; =======================================================================================
; CRUCIALE: Sostituisci il percorso sotto con la cartella REALE dove Visual Studio 
; compila il tuo file .exe (di solito dentro la cartella bin\Release del tuo progetto)
; =======================================================================================



; 2. CRUCIALE: Copia TUTTI gli altri file e sotto-cartelle presenti in bin\Release
; L'asterisco * indica a Inno di prendere ogni minima DLL o dipendenza necessaria.
Source: "G:\My\My_Sorgenti\Visualstudio\Scuola_delle_Stelle\bin\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

[Dirs]
; Sblocca i permessi della cartella per tutti gli utenti, permettendo la creazione del file .ini
Name: "{app}"; Permissions: users-modify

[Icons]
; Crea il collegamento nel menu Start e sul Desktop
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
; Mostra la casella di spunta alla fine per avviare subito il programma
Filename: "{app}\{#MyAppExeName}"; Description: "Avvia {#MyAppName} adesso"; Flags: nowait postinstall skipifsilent
