COPY "G:\My\My_Sorgenti\Visualstudio\Scuola_delle_Stelle\_SETUP\Installazione_Scuoladellestelle.exe" "J:\_MY_COPIE\Dropbox\At\Scuola_delle_Stelle\_SETUP\" /Y
COPY "G:\My\My_Sorgenti\Visualstudio\Scuola_delle_Stelle\Scuoladellestelle*.db" "J:\_MY_COPIE\Dropbox\At\Scuola_delle_Stelle" /Y
ROBOCOPY "G:\My\My_Sorgenti\Visualstudio\Scuola_delle_Stelle\My_Module" "J:\_MY_COPIE\Dropbox\At\Scuola_delle_Stelle\Sorgenti\My_Module" /E
ROBOCOPY "G:\My\My_Sorgenti\Visualstudio\Scuola_delle_Stelle\Modelli" "J:\_MY_COPIE\Dropbox\At\Scuola_delle_Stelle\Sorgenti\Modelli" /E
ROBOCOPY "G:\My\My_Sorgenti\Visualstudio\Scuola_delle_Stelle\DB_RAGAZZI" "J:\_MY_COPIE\Dropbox\At\Scuola_delle_Stelle\Sorgenti\DB_RAGAZZI" /E
pause
