Option Strict Off
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Math
Imports System.Reflection.Emit
Imports System.Runtime.InteropServices
Imports System.Text.Json
Imports System.Text.RegularExpressions
Imports System.Windows.Documents.DocumentStructures
Imports System.Windows.Forms.AxHost
Imports System.Xaml
Imports DocumentFormat.OpenXml.Bibliography
Imports DocumentFormat.OpenXml.Office2010.Excel
Imports DocumentFormat.OpenXml.Office2013.Drawing.Chart
Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Wordprocessing
Imports Microsoft.Data.SqlClient
Imports Microsoft.Data.Sqlite
Imports OpenTK.Graphics.OpenGL
Imports MyColor = System.Drawing.Color



Public Structure Esportazione_dati_ELCAR
    Public Codice As String
    Public Descrizione As String
    Public Data As DateTime
    Public Tot1 As Integer
    Public Tot1_Pos As Integer
    Public Tot1_Neg As Integer
    Public Tot1_prompt As Integer
    Public Tot1_perc As Integer
    Public Tot2 As Integer
    Public Tot2_Pos As Integer
    Public Tot2_Neg As Integer
    Public Tot2_prompt As Integer
    Public Tot2_perc As Integer
    Public valutativo As Integer
End Structure
Public Structure Esportazione_dati_CPRT
    Public Descrizione As String
    Public Data As DateTime
    Public Tot As Double
End Structure


Public Module Modulo_Codice

    Public Structure ElementoGrafico   ' Server per i grafici su word 
        Public X As String   ' L'etichetta sull'asse delle X (es. "Gennaio" o "Cat1")
        Public Y1 As Integer ' Il valore % della prima barra (Serie 1)
        Public Y2 As Integer ' Il valore % della seconda barra (Serie 2)
        Public Y1abs As Integer ' Il valore assoluto della prima barra (Serie 1)
        Public Y2abs As Integer ' Il valore assoluto della seconda barra (Serie 2)
    End Structure
    Public Gbl_elementoGrafico() As ElementoGrafico

    Public Structure DatiGrafico
        Public Data As String
        Public ValoriY As List(Of Integer) ' Può contenere 1 o 2 valori
        Public Color As List(Of Integer) ' Può contenere 1 o 2 valori

    End Structure
    Public Gbl_datigrafico As New List(Of DatiGrafico)()

    Public Structure Elenco_checklist
        Public voce As String
        Public TAG As String
        Public Stato As Boolean
        Public j As Integer
        Public Overrides Function ToString() As String
            Return Me.voce
        End Function
    End Structure
    Public Gbl_elenco_checklist As New List(Of Elenco_checklist)


    Public Structure Dati_Totali_Giornaliero
        Public Data As String
        Public tot As Integer
        Public tot_voci As Integer
        Public tot_POS_voci As Integer
        Public id_foglio As Integer
        Public tot_RT As Integer
        Public n_record As Integer
    End Structure

    Public Structure Struct_Size_Offset
        Public xOffset As Integer
        Public YOffset As Integer
        Public Width As Integer
        Public Height As Integer
    End Structure
    Public Structure Struct_Codice_ELCAR
        Public ID As Integer
        Public CODICE As String
        Public TESTO As String
        Public TESTO_Lungo As String
    End Structure

    Public Structure Struct_Codice_ELCARVOCI
        Public ID As Integer
        Public CODICE_VOCE As String
        Public CODICE_ELCAR As String
        Public TESTO As String
        Public TESTO_Lungo As String
    End Structure
    Public Structure Struct_Codice_ELCARVOCISUB
        Public ID As Integer
        Public CODICE_VOCE As String
        Public CODICE_ELCAR As String   'ELCAR
        Public TESTO As String
        Public TESTO_Lungo As String
        Public Deleted As Boolean
    End Structure


    Public Structure Struct_ini
        Public Database As String
        Public Modello_DB_Bambini As String
        Public DB_Bambini_path As String   ' Percorso dove si trovano tutti i database dei ragazzi
        Public Tema As String
        Public Scanrow_Offset As Integer
        Public Scanrow_Height As Integer
        Public Cell_Height As Integer
        Public Size_Riga As Struct_Size_Offset
        Public Size_Cella As Struct_Size_Offset
        Public ELCAR_LETTERE() As String
        Public Modello_Schede1 As String ' Full_path_Modello Foglio schede
        Public Modello_Schede1_condizioni As String ' tipo NmaxELCAR,Nmax_RT
        Public Modello_Schede2 As String ' Full_path_Modello Foglio schede
        Public Modello_Schede2_condizioni As String ' tipo NmaxELCAR,Nmax_RT
        Public Modello_Grafico_codici2 As String ' Full_path_Modello Foglio grafici
        Public Modello_Grafico_codici6 As String ' Full_path_Modello Foglio grafici
        Public Modello_Grafico_codici12 As String ' Full_path_Modello Foglio grafici
        Public Modello_Grafico_CP_6 As String ' Full_path_Modello Foglio grafici

        Public Modello_Grafico_TotaleGiornaliero As String ' Full_path_Modello Foglio grafici
        Public Modello_Excel_esportazione_dati As String
        Public Path_SCHEDE_DaStampare As String
        Public Path_GRAFICI_DaStampare As String
        Public File_Log As String
        Public Stampante_defaut As String
        Public Graph_interval As Integer ' Intervallo di default per i dati del grafico
        Public Perc_soglia_ok As Integer
        Public Numero_Totale_Risposte As Integer
        Public Numero_min_CP As Integer
        Public Numero_min_RT As Integer
        Public Numero_min_voci_perpagina As Integer
        Public Numero_fogli_compilati As Integer


    End Structure
    Public ini As Struct_ini
    Public Structure Struct_Graph_data
        ' serve nella struttura graph
        Public Data As String
        Public N_day As Integer
        Public TOT1_Pos As Integer
        Public TOT1_Neg As Integer
        Public Tot1_PROMPT As Integer
        Public TOT2_Pos As Integer
        Public TOT2_Neg As Integer
        Public Tot2_PROMPT As Integer
        Public Tot1 As Integer
        Public Tot2 As Integer
        Public Compilato As Boolean
        Public Valutativo As Boolean
        Public tuttelerisposte As Boolean
        Public ID As Integer
        Public Perc As Integer
    End Structure
    Public Structure Struct_Graph_Codice
        ' serve nella struttura graph
        Public Cod_ELCAR As String
        Public Cod_Testo As String
        Public ID_Foglio As Integer
        Public Data() As Struct_Graph_data
        Public Trend As String 'PASSATO,NEG4,POS4,UNDEF
        Public Labeltag As String
        Public Bambino As String
    End Structure
    Public Structure Struct_Stato_compilazione
        Public Bambino As String
        Public ID_Bambino As Integer
        Public DB_Bambino As String
        Public Data As String
        Public id_foglio As Integer
        Public Compilato As Boolean    'True iL FOGLIO è PRESENTE NEL DATABASE DEL BAMBINO
        Public TODOLIS As String
    End Structure


    Public Structure Struct_FogliDaStampare
        Public Bambino As String
        Public ID_Bambino As Integer
        Public DB_Bambino As String
        Public Data As String
        Public filename As String
        Public id_foglio As Integer
        Public Registrato As Boolean    'True iL FOGLIO è PRESENTE NEL DATABASE DEL BAMBINO
        Public doc_Esistente As Boolean 'True esiste gia il file docx
        Public Compilato As Boolean     'True il foglio risulta compilato
        Public DaStampare As Boolean    'True bisogna preparare il file docx
        Public Color As Integer         ' colore di visualizzazione
        Public CP As List(Of Class_COMPORTAMENTI)
        Public ROUTINE As List(Of Class_ROUTINE)
    End Structure

    Public Structure Struct_foglio
        'Contine_carica tutto di un foglio Bambino 
        Public Bambino As String
        Public ID_foglio As Integer
        Public ID_Bambino As Integer
        Public DB_Bambino As String
        Public Data As String
        Public Compilato As Boolean     'True il foglio risulta compilato (risposte scritte)
        Public Valutativo As Boolean    'True il foglio risulta tio valutativo
        Public Nuovo As Boolean         'Un foglio in fase di preparazione non salvato nel database
        Public VociDiUnFoglio As List(Of Class_GIORNALIERO)
        Public RT As List(Of Class_Giornaliero_ROUTINE)
        Public Graph As Struct_Graph_Codice
        Public CP As List(Of Class_Giornaliero_CP)
    End Structure
    Public Gbl_foglio As Struct_foglio


    Public Structure Struct_foglio_Json
        ' Contiene_carica tutto di un foglio Bambino 
        Public Property ID_Questionario As Integer
        Public Property Data_creazione As String
        Public Property Bambino As String
        Public Property ID_foglio As Integer
        Public Property ID_Bambino As Integer
        Public Property Data_foglio As String
        Public Property Stato As String 'DA COMPILARE,COMPILATO,IN COMPILAZIONE

        Public Property VociDiUnFoglio As List(Of Class_GIORNALIERO)
        Public Property RT As List(Of Class_Giornaliero_ROUTINE)
        Public Property CP As List(Of Class_Giornaliero_CP)
    End Structure

    Public Structure Struct_Elencofogli_Json
        ' Contiene_carica tutto di un foglio Bambino 
        Public Property ID_Questionario As Integer
        Public Property Data_foglio As String
        Public Property Bambino As String
        Public Property ID_foglio As Integer
        Public Property ID_Bambino As Integer
        Public Property Filename As String
        Public Property note As String  ' ci vuole la minuscola per android dio cane
        Public Property disponibile As Integer ' ci vuole la minuscola per android dio cane
    End Structure
    Structure Struct_Decode_Android_message
        Public deviceId As String
        Public type As String
    End Structure


    Public Gbl_datigrafico_totalegiornaliero As New List(Of Dati_Totali_Giornaliero)()
    Public Gbl_Elenco_fogli_stampa() As Struct_FogliDaStampare
    Public Gbl_Graph As Struct_Graph_Codice
    Public Gbl_Graph1() As Struct_Graph_Codice

    Public Gbl_Routine As New Class_ROUTINE
    Public Gbl_anagrafica As New Class_ANAGRAFICA()
    Public Gbl_Elenco_bambini As List(Of Class_ANAGRAFICA)      'Elenco di tutti i bambini presenti in anagrafica 
    Public Gbl_Elenco_CP As List(Of Class_COMPORTAMENTI)        ' elenco di tutte le possibili voci comportamentali
    Public Gbl_CP_selezionate As List(Of Class_Giornaliero_CP)   ' elenco CP selezionate per quel foglio
    Public Gbl_RT_selezionate As List(Of Class_Giornaliero_ROUTINE)   ' elenco CP selezionate per quel foglio

    Public Gbl_elenco_fogli_bambino As List(Of Class_Elenco_fogli)  ' elenco di tutti i fogli realizzati per un bambino
    Public Gbl_Class_Voci_Personali As New Class_ELCAR_VOCI_PERSONALI
    Public Gbl_Class_Voci_sub2 As New Class_ELCAR_VOCI_SUB2
    Public Gbl_Class_Voci_sub As New Class_ELCAR_VOCI_SUB
    Public Gbl_miaLista As New List(Of Class_ElementoCombo)()
    Public Gbl_miaLista_Anagrafiche As New List(Of Class_ElementoCombo)()
    Public Gbl_miaLista_fogli As New List(Of Class_ElementoCombo)()


    Public Gbl_giornaliero As New Class_GIORNALIERO
    Public Gbl_VociDiUnFoglio As New List(Of Class_GIORNALIERO)
    Public Gbl_Voci_CP_DiUnFoglio As New List(Of Class_Giornaliero_CP)
    Public Gbl_VociDiTodolist As New List(Of Class_TODOLIST)
    Public Gbl_app_path As String
    Public Gbl_Offset As Integer
    Public Gbl_Elenco_Codici_ELCAR() As Struct_Codice_ELCAR
    Public Gbl_Elenco_Codici_ELCARVOCI() As Struct_Codice_ELCARVOCI
    Public Gbl_Elenco_Codici_ELCARVOCISUB() As Struct_Codice_ELCARVOCISUB
    Public Gbl_Elenco_Codici_ELCARVOCISUB2() As Struct_Codice_ELCARVOCISUB
    Public Gbl_Elenco_Codici_ELCARVOCIPERSONALI() As Struct_Codice_ELCARVOCISUB
    Public Gbl_Codice_Elcar_Selezionato As String
    Public Gbl_Codice_Elcar_TestoSelezionato As String
    Public Gbl_Codice_Elcar_TestoLungo As String
    Public Gbl_NOME_BAMBINO As String
    Public Gbl_DB_BAMBINO As String
    Public Gbl_SCHEDA_BAMBINO As String  ' Full path storage dei fogli esercizi
    Public Gbl_CnString_Bambino As String
    Public Gbl_UltimofoglioGiornaliero_inserito As Integer
    Public Gbl_Jb As Integer 'Puntatore al bambino
    Public Gbl_nuovavoce As Boolean = False ' Inserimento nuova voce elcar personalizzata
    Public Gbl_Caricamento_Immediato As Boolean = True
    Public Gbl_Percentuale(8)
    Public GbL_Logfile As String
    Public cod(7) As String
    Public Gbl_IP_SERVER As String
    Public Gbl_server_connected As Boolean
    Public Gbl_Elenco_Device As List(Of Class_DEVICE)
    Public Gbl_MioServer As Class_WebServerTablet
    Public Gbl_versione As String = "1.0"

    ' --- VARIABILI DI STATO TABLET ---
    Public DispositiviInAttesa As New List(Of String)
    ' Carica questo valore all'avvio dal tuo DB SQLite locale (True se già abilitato in passato)
    Private Gbl_TabletAbilitato As Boolean = False
    ' Contatore di sicurezza per evitare che server e tablet si scambino rifiuti all'infinito
    Private TentativiAbilitazione As Integer = 0
    ' Il timer per gestire il timeout di 5 secondi (creato con il prefisso WithEvents per intercettare il ticchettio)
    Private WithEvents TimeoutTimer As New System.Windows.Forms.Timer()

    Dim img, bmp As Bitmap


    Function Carica_Foglio_da_inviare_al_Tablet(id_todolist) As String
        'Trova_tutti_fogli_Bambino ID foglio e DBNome 
        If id_todolist = 0 Then Return Nothing
        Dim xx = Class_TODOLIST.Load_By_ID(id_todolist)
        Dim id_foglio = CInt(xx.ID_FOGLIO)
        Dim DB_BAMBINO = xx.DB_BAMBINO

        If id_foglio = 0 Then Return Nothing
        If DB_BAMBINO = "" Then Return Nothing

        Dim foglio = Carica_foglio(id_foglio, DB_BAMBINO)

        Dim Foglio_json As Struct_foglio_Json
        Foglio_json.Bambino = foglio.Bambino
        Foglio_json.Data_foglio = foglio.Data
        Foglio_json.ID_foglio = foglio.ID_foglio
        Foglio_json.ID_Bambino = foglio.ID_Bambino
        Foglio_json.ID_Questionario = id_todolist
        Foglio_json.Stato = "IN COMPILAZIONE"
        Foglio_json.VociDiUnFoglio = foglio.VociDiUnFoglio
        Foglio_json.RT = foglio.RT
        Foglio_json.CP = foglio.CP
        Foglio_json.Data_creazione = Now.ToString

        ' 🎯 CHIAMATA ASSOLUTA: Specifichiamo tutto il percorso per evitare conflitti con System.Text
        Dim jsonOttenuto As String = System.Text.Json.JsonSerializer.Serialize(Foglio_json)
        Return jsonOttenuto


    End Function
    Function Trova_tutti_fogli_Bambino() As Integer
        Dim elenco_fogli As New Class_Elenco_fogli()
        Gbl_elenco_fogli_bambino = elenco_fogli.Trova_tutti_fogli()
        Return Gbl_elenco_fogli_bambino.Count

    End Function

    Function Open_file_manager(Optional path As String = "") As String

        Dim percorsoCartella As String = ""
        If path <> "" Then
            percorsoCartella = path
            If Not System.IO.Directory.Exists(percorsoCartella) Then
                Call MsgBox("Percorso non trovato")
                percorsoCartella = ""
            End If
        End If

        Try
            Process.Start("explorer.exe", percorsoCartella)
        Catch ex As Exception
            MessageBox.Show("Impossibile aprire la cartella: " & ex.Message)
        End Try
    End Function
    Function Exist_DB_Bambino(dbname) As Boolean
        dbname = dbname.ToUpper
        If InStr(dbname, ".DB") = 0 Then dbname = dbname + ".DB"
        Dim percorsoFileBambino As String = IO.Path.Combine(ini.DB_Bambini_path, dbname).ToUpper
        If Not IO.File.Exists(percorsoFileBambino) Then
            Return False
        End If

        Return True
    End Function



    Function ToDoList_Pulisci_Compilati() As Integer
        ' Carico tutti i record di todolist
        Dim voci_todolist As List(Of Class_TODOLIST) = Class_TODOLIST.LoadAll()

        'rimuovo quelli gia compilat
        Dim cnt = 0, ok As Boolean, errori = "", todo As Boolean = True
        For Each voce In voci_todolist
            todo = False
            If voce.COMPILATO <> 0 Then todo = True

            If Not todo Then Continue For
            ok = Class_TODOLIST.EliminaRecordPerID(voce.ID)
            If ok Then
                cnt = cnt + 1
            Else
                errori = errori + 1
            End If
        Next


    End Function
    Function ToDoList_Aggiungi_voce_foglio(foglio As Struct_foglio, Optional nuovo As Boolean = True) As Integer
        ' Carico tutti i record di todolist
        Dim voce_todolist As New Class_TODOLIST

        If nuovo Then
            voce_todolist.ID = 0
        End If
        voce_todolist.ID_BAMBINO = foglio.ID_Bambino
        voce_todolist.DB_BAMBINO = foglio.DB_Bambino
        voce_todolist.ID_FOGLIO = foglio.ID_foglio
        voce_todolist.DATA_FOGLIO = foglio.Data
        voce_todolist.COMPILATO = 0
        voce_todolist.BAMBINO = foglio.Bambino
        voce_todolist.NOMEFILE = Costruisci_nome_foglio(foglio.ID_foglio, foglio.Data, foglio.Bambino)
        voce_todolist.CLIENT_DATAINVIO = ""
        voce_todolist.CLIENT_DATARICONSEGNA = ""
        voce_todolist.CLIENT_NOME = ""
        voce_todolist.CLIENT_ID = 0
        voce_todolist.CLIENT_ID = 0

        'Aggiungo la nuova voce 
        voce_todolist.Save()


    End Function

    Function Todolist_Azzera_e_Ricrea(Optional Completa As Boolean = True)
        '1) Elimino tutti i record presenti in todolist che sono compilati
        '2) se completa=true allora Elimina tutti i record che non sono uso da client Tablet
        '3) Ricarica tutti i record con i fogli da compilare trovati 

        '1 Elimino tutti i record presenti in todolist che sono compilati
        ToDoList_Pulisci_Compilati()

        '-- Rileggo la tabella TODOLIST 
        Dim voci_todolist As List(Of Class_TODOLIST) = Class_TODOLIST.LoadAll()

        If Completa Then
            '-- Elimina tutti i record che non sono uso da client Tablet --
            Dim voce As New Class_TODOLIST
            For Each voce In voci_todolist
                'Tengo  i record in uso dal tablet e non riconsegnati compilati
                If voce.CLIENT_ID > 0 And Not voce.COMPILATO Then Continue For
                voce.Delete(voce.ID)
            Next
        End If

        '-- Cerco e ricarico tutti i fogli ancora da compilare 
        For Each bambino In Gbl_Elenco_bambini
            Gbl_DB_BAMBINO = bambino.DBname
            Dim id_bambino = bambino.ID
            If Not Exist_DB_Bambino(Gbl_DB_BAMBINO) Then Continue For
            ' cerco i fogli non compilati 
            Dim dt As DataTable = Class_GIORNALIERO.OttieniRecordOrderbydata(True)
            If dt.Rows.Count = 0 Then Continue For
            For Each riga As DataRow In dt.Rows
                ' 1. Lettura del campo codiceùdim id_foglio?
                Dim idf = Convert.ToInt32(riga("id_foglio"))
                If Not Class_TODOLIST.Trova_By_IDfoglio_IDbambino(idf, id_bambino).Esito Then
                    Dim foglio = Carica_foglio(idf, Gbl_DB_BAMBINO)
                    ToDoList_Aggiungi_voce_foglio(foglio)
                End If
            Next
        Next



    End Function


    Function Carica_foglio(ID_FOGLIO, DBBAMBINO, Optional Load_graph = False) As Struct_foglio
        Dim OLD_DBBAMBINO = Gbl_DB_BAMBINO

        ' 1. Resetta tutte le variabili primitive (String, Integer, Boolean) e cancella le strutture nidificate
        ' 2. Inizializza nuovamente le liste interne come nuove liste vuote
        Dim foglio = New Struct_foglio()
        foglio.VociDiUnFoglio = New List(Of Class_GIORNALIERO)()
        foglio.CP = New List(Of Class_Giornaliero_CP)()


        foglio.DB_Bambino = DBBAMBINO
        foglio.ID_foglio = ID_FOGLIO
        Gbl_DB_BAMBINO = DBBAMBINO

        Dim anag = New Class_ANAGRAFICA
        anag = Class_ANAGRAFICA.CercaPerDBName(DBBAMBINO)
        Try
            foglio.Bambino = anag.NomeCompleto
        Catch ex As Exception
            Call MsgBox("Foglio non trovato")
            Return Nothing
        End Try
        foglio.ID_Bambino = anag.ID


        'foglio.Bambino = anagrafica.fullname
        ' Carico tutti i codici inseriti nel foglio
        Dim giornaliero As New Class_GIORNALIERO
        foglio.VociDiUnFoglio = giornaliero.Carica_Foglio(ID_FOGLIO)
        If foglio.VociDiUnFoglio.Count = 0 Then
            Call MsgBox("Voci non trovate")
            Return Nothing
        End If

        ' Carico le variabili generali utilizzando il primo record 
        foglio.Data = foglio.VociDiUnFoglio(0).DATA
        foglio.Compilato = foglio.VociDiUnFoglio(0).COMPILATO
        foglio.Valutativo = foglio.VociDiUnFoglio(0).VALUTATIVO

        ' Carico le coci CP (Comportamentali)
        Dim cp As New Class_Giornaliero_CP
        foglio.CP = cp.Carica_per_Foglio(ID_FOGLIO)

        '-- Controlo che il testo delle note sia scritto --
        For Each voce In foglio.CP
            Dim x = voce.TESTO.ToUpper
            If x = "TRUE" Or x = "" Then
                'trova il testo della voce CP
                Dim cp1 = Class_COMPORTAMENTI.Load(voce.ID_VOCE)
                voce.TESTO = cp1.TESTO
            End If
        Next

        ' Carico le coci RT (Routine)
        Dim rt As New Class_Giornaliero_ROUTINE
        foglio.RT = rt.Carica_per_Foglio(ID_FOGLIO)


        If Load_graph Then

        End If
        Gbl_DB_BAMBINO = OLD_DBBAMBINO

        Return foglio
    End Function




    Function Costruisci_nome_foglio(ID_foglio, data, BAMBINO) As String
        Dim nomefoglio = data.ToString + "." + BAMBINO + "." + ID_foglio.ToString + ".DOCX"
        Return nomefoglio

    End Function
    Sub aprifilemanager(PATH)
        Dim percorsoCartella As String = PATH
        If Not System.IO.Directory.Exists(percorsoCartella) Then
            MsgBox("La cartella non esiste o il percorso è errato.")
        End If


        Process.Start(New ProcessStartInfo(percorsoCartella) With {.UseShellExecute = True})
    End Sub
    Public Function Costruisci_filenameDB_bambino(DB_BAMBINO As String, Optional Autocrea As Boolean = True) As String

        If DB_BAMBINO = "" Then Return ""
        If InStr(DB_BAMBINO, ".DB") = 0 Then DB_BAMBINO = DB_BAMBINO + ".DB"
        Gbl_Elenco_bambini(Gbl_Jb).DBname = DB_BAMBINO
        Dim filedb = IO.Path.Combine(ini.DB_Bambini_path, DB_BAMBINO).ToUpper

        'Verifico che esista il database del bambino. (nel caso lo creo)
        If Not IO.File.Exists(filedb) Then
            If Not Autocrea Then Return ""
            Crea_DB_Bimbo(filedb)
        End If
        Return filedb

    End Function
    Function Fogli_compilati_aggiungi_numero(Optional num_da_aggiungere As Integer = 1) As Integer
        Dim fileini = Gbl_app_path + "\Scuola_delle_Stelle.ini"
        'Rileggo il numero registrato 
        Dim y = "0;" + Now.ToShortDateString
        y = ReadINIString(fileini, "GENERAL", "Numero Fogli Compilati", y)
        Dim xx = Split(y, ";")
        If xx(1) = Now.ToShortDateString Then
            ini.Numero_fogli_compilati = CInt(xx(0))
        Else
            ini.Numero_fogli_compilati = 0
        End If

        'Aggiugo la quantita passata 
        ini.Numero_fogli_compilati = ini.Numero_fogli_compilati + num_da_aggiungere

        'Registro il nuovo valore 
        y = ini.Numero_fogli_compilati.ToString + ";" + Now.ToShortDateString
        WriteINIString(fileini, "GENERAL", "Numero Fogli Compilati", y)
        Return ini.Numero_fogli_compilati


    End Function
    Sub Read_ini()
        Dim x = ""

        'Gbl_app_path = Mid(System.Reflection.Assembly.GetExecutingAssembly.Location, 1, System.Reflection.Assembly.GetExecutingAssembly.Location.LastIndexOf("\")).ToUpper
        'Gbl_app_path = IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location).ToUpper()

        Dim K = "GENERAL"

        Gbl_app_path = AppContext.BaseDirectory.ToUpper()
        If InStr(Gbl_app_path, "BIN") > 0 Then
            Dim io = InStr(Gbl_app_path, "BIN")
            Gbl_app_path = Strings.Left(Gbl_app_path, io - 2)
        End If


        If Not IO.File.Exists(GbL_Logfile) Then
        End If


        Dim fileini = Gbl_app_path + "\Scuola_delle_Stelle.ini"
        If Not IO.File.Exists(fileini) Then
            Call MsgBox(fileini)
        End If

        Dim y = "0;" + Now.ToShortDateString
        y = ReadINIString(fileini, K, "Numero Fogli Compilati", y)
        Dim xx = Split(y, ";")
        If xx(1) = Now.ToShortDateString Then
            ini.Numero_fogli_compilati = CInt(xx(0))
        Else
            ini.Numero_fogli_compilati = 0
        End If

        y = "Nessuna"
        ini.Stampante_defaut = ReadINIString(fileini, K, "Stampante Default", y)



        '------------------------------------------
        '--- PATH ---
        '------------------------------------------
        K = "PATH"

        y = Gbl_app_path + "\Scuola_delle_Stelle.log"
        ini.File_Log = ReadINIString(fileini, K, "File log", y)
        GbL_Logfile = ini.File_Log

        If Not IO.File.Exists(GbL_Logfile) Then
            APPEND_LOG("Creazione file log", GbL_Logfile, True)
        End If


        y = Gbl_app_path + "\db_ragazzi"
        ini.DB_Bambini_path = ReadINIString(fileini, K, "Db_Bimbi_path", y)
        If Not IO.Directory.Exists(ini.DB_Bambini_path) Then
            IO.Directory.CreateDirectory(ini.DB_Bambini_path)
        End If

        x = dir_bar(Gbl_app_path) + "DA_STAMPARE_SCHEDE"
        ini.Path_SCHEDE_DaStampare = ReadINIString(fileini, K, "Schede_da_Stampare", x)
        If Not IO.Directory.Exists(ini.Path_SCHEDE_DaStampare) Then
            IO.Directory.CreateDirectory(ini.Path_SCHEDE_DaStampare)
        End If


        x = dir_bar(Gbl_app_path) + "DA_STAMPARE_GRAFICI"
        ini.Path_GRAFICI_DaStampare = ReadINIString(fileini, K, "grafici_da_Stampare", x)
        If Not IO.Directory.Exists(ini.Path_GRAFICI_DaStampare) Then
            IO.Directory.CreateDirectory(ini.Path_GRAFICI_DaStampare)
        End If

        '------------------------------------------
        '--- DATABASE ---
        '------------------------------------------
        K = "DATABASE"
        y = Gbl_app_path + "\ScuoladelleStelle.db"
        ini.Database = ReadINIString(fileini, K, "Database", y)

        y = Gbl_app_path + "\DB_BAMBINO_Vuoto.DB.db"
        ini.Modello_DB_Bambini = ReadINIString(fileini, K, "MODELLO_DB_Bambino_Vuoto", y)





        '------------------------------------------
        '--- MODELLI ---
        '------------------------------------------
        K = "MODELLI"
        ' Creo folder MODELLI se non esiste
        y = Gbl_app_path + "\MODELLI"
        If Not IO.Directory.Exists(y) Then IO.Directory.CreateDirectory(y)


        x = dir_bar(y) + "Modello_scheda.docx"
        ini.Modello_Schede1 = ReadINIString(fileini, K, "Schede Modello 1", x)

        x = "10;45"
        ini.Modello_Schede1_condizioni = ReadINIString(fileini, K, "Schede Condizioni utilizzo Modello 1 (Elcar;RT)", x)

        x = dir_bar(y) + "Modello_scheda.docx"
        ini.Modello_Schede2 = ReadINIString(fileini, K, "Schede Modello 2", x)

        x = "10;45"
        ini.Modello_Schede1_condizioni = ReadINIString(fileini, K, "Schede Condizioni utilizzo Modello 2 (Elcar;RT)", x)

        x = dir_bar(y) + "Modello_Grafico_Elcar 2.xlsx"
        ini.Modello_Grafico_codici2 = ReadINIString(fileini, K, "Grafici Modello2", x)

        x = dir_bar(y) + "Modello_Grafico_Elcar 6.xlsx"
        ini.Modello_Grafico_codici6 = ReadINIString(fileini, K, "Grafici Modello6 ", x)

        x = dir_bar(y) + "Modello_Grafico_Elcar 12.xlsx"
        ini.Modello_Grafico_codici12 = ReadINIString(fileini, K, "Grafici Modello12 ", x)

        x = dir_bar(y) + "Modello_Grafico_CP 6.xlsx"
        ini.Modello_Grafico_CP_6 = ReadINIString(fileini, K, "Grafici CP Modello6 ", x)


        x = dir_bar(y) + "Modello_Grafico_Totali_Giornaliero.xlsx"
        ini.Modello_Grafico_TotaleGiornaliero = ReadINIString(fileini, K, "Modello Grafico Totale Giornaliero", x)

        x = dir_bar(y) + "Modello_Esportazione_dati.xslx"
        ini.Modello_Excel_esportazione_dati = ReadINIString(fileini, K, "Modello esportazione dati", x)


        '------------------------------------------
        '--- PARAMETER_SCAN ---
        '------------------------------------------

        'K = "PARAMETER_SCAN"
        'ini.Scanrow_Offset = CInt(ReadINIString(fileini, k, "Scanrow_Offset", "50"))
        'Gbl_Offset = ini.Scanrow_Offset
        'ini.Size_Riga.Height = CInt(ReadINIString(fileini, k, "Row_Height", "180"))
        'ini.Size_Riga.Width = CInt(ReadINIString(fileini, k, "Row_Width", "1960"))
        'ini.Size_Riga.xOffset = CInt(ReadINIString(fileini, k, "Row_XOffset", "995")) 'distanza (in pixel) della prima cella dal 3 spot del qrcode della riga


        '------------------------------------------
        '--- SOGLIE ---
        '------------------------------------------
        K = "SOGLIE"
        ini.Numero_min_voci_perpagina = CInt(ReadINIString(fileini, K, "Numero minimo voci per Pagina", "4"))
        ini.Numero_Totale_Risposte = CInt(ReadINIString(fileini, K, "Numero di risposte necessario per la prova", "20"))
        ini.Numero_min_CP = CInt(ReadINIString(fileini, K, "Numero Minimo di voci Comportamentali inseribili", "1"))
        ini.Numero_min_RT = CInt(ReadINIString(fileini, K, "Numero Minimo di voci di routine inseribili", "10"))

        K = "GRAPH"
        ini.Graph_interval = CInt(ReadINIString(fileini, K, "Intervallo default dati grafico  (gg)", "15"))
        ini.Perc_soglia_ok = CInt(ReadINIString(fileini, K, "Valore per il superamento della prova (0-100%)", "80"))



    End Sub

    Function Ritorna_modello_da_utilizzare(n1, n2) As String
        ' Controllo usabilita modello 1
        Dim y = Split(ini.Modello_Schede1_condizioni, ";")
        Dim maxn1 = Val(y(0)), maxn2 = Val(y(1))
        If n1 <= maxn1 And n2 <= maxn2 Then
            Return ini.Modello_Schede1
        End If

        ' Controllo usabilita modello 2
        y = Split(ini.Modello_Schede2_condizioni, ";")
        maxn1 = Val(y(0))
        maxn2 = Val(y(1))
        If n1 <= maxn1 And n2 <= maxn2 Then
            Return ini.Modello_Schede2
        End If

        Return ""

    End Function
    Function Carica_bambini(Optional soloattivi As Boolean = True) As List(Of Class_ANAGRAFICA)

        Dim risultati As List(Of Class_ANAGRAFICA) = Class_ANAGRAFICA.CercaPerCognome("", soloattivi)
        Return risultati

    End Function

    Function Carica_Comportamenti() As List(Of Class_COMPORTAMENTI)

        Dim risultati As List(Of Class_COMPORTAMENTI) = Class_COMPORTAMENTI.Load_all
        Return risultati

    End Function


    Function Crea_DB_Bimbo(db_filename) As Boolean
        If IO.File.Exists(db_filename) Then Return True
        Dim db_empty = ini.Modello_DB_Bambini

        If Not IO.File.Exists(db_empty) Then
            Dim m = "il database:" + vbCrLf + db_empty.ToString + vbCrLf + "NON è stato trovato!"
            Call MsgBox(m, vbCritical + vbOKOnly, "Database non trovato")
            Exit Function
        End If
        If InStr(db_filename, ".DB") = 0 Then db_filename = db_filename + ".DB"
        IO.File.Copy(db_empty, db_filename)
        If IO.File.Exists(db_filename) Then
            Return True
        End If
        Return False
    End Function

    Function Carica_elenco_CODICI_ELCAR(Optional codice As String = "") As Boolean
        Dim risultati As List(Of Class_ELCAR) = Class_ELCAR.CercaPerCodice(codice)
        If risultati.Count = 0 Then Return False
        Dim id As Integer, x As String, tot As Integer
        tot = risultati.Count - 1
        ReDim Gbl_Elenco_Codici_ELCAR(tot)
        For j = 0 To tot - 1
            Gbl_Elenco_Codici_ELCAR(j).CODICE = risultati(j).Codice
            Gbl_Elenco_Codici_ELCAR(j).ID = risultati(j).ID
            Gbl_Elenco_Codici_ELCAR(j).TESTO = risultati(j).Testo
        Next
        Return True
    End Function

    Public Function Costruisci_codice_finale(Optional Genitore As Boolean = False) As String
        Dim ret = cod(1) + "." + cod(2)
        If cod(3) <> "" Then
            ret = ret + "." + cod(3)
        Else
            ret = ret + ".."
        End If

        If cod(4) <> "" Then
            ret = ret + "." + cod(4)
        Else
            ret = ret + ".."
        End If

        If cod(5) <> "" Then
            ret = ret + "." + cod(5)
        Else
            ret = ret + ".."
        End If

        If cod(6) <> "" Then
            ret = ret + "." + cod(6)
        End If

        If Genitore Then
            'ritorna il codice prima dell'ultimo punto non vuoto

        End If

        Return ret


    End Function

    Public Function Carica_Grafico(Optional datainiziale As String = "", Optional datafinale As String = "", Optional mostragrafico As Boolean = False) As Integer
        'Public Function Carica_Grafico(Optional datainiziale As String = "", Optional datafinale As String = "", Optional mostragrafico As Boolean = False) As Integer
        'dalla tabella giornaliero del database del ragazzo, leggendo tutti i record presenti che rientrano nell'intervallo di tempo indicato
        ' se intervallo =0 allora utilizza intervallo di default letto dal file ini
        'i valori dell'inrervallo possono essere: 15 gg, 1 mese, 3 mesi, 6 mesi, 1 anno,0  tutto
        Dim dataf As Date, datai As Date

        If datafinale = "" Then
            dataf = Now
        Else
            dataf = CDate(datafinale)
        End If

        If datainiziale = "" Then
            datai = dataf.AddMonths(-1)

        Else
            datai = CDate(datainiziale)
        End If


        Dim dt As DataTable = Class_GIORNALIERO.OttieniRecordPerDate(datai, dataf)
        Do While dt.Rows.Count = 0
            Dim m = "nessun esercizio trovato nel periodo:" + vbCrLf
            m = m + datai.ToShortDateString + "-" + dataf.ToShortDateString
            Dim risultato = f_scegli_periodo.Apri(1, dataf.ToShortDateString)
            If IsNothing(risultato.datai) Then Exit Function
            dt = Class_GIORNALIERO.OttieniRecordPerDate(risultato.datai, risultato.dataf)
        Loop
        ' Ciclo che attraversa ogni riga della tabella
        Dim Codice = "", cod = "", J = 0, i = -1
        'ReDim Gbl_Graph
        Gbl_Graph.Cod_ELCAR = dt.Rows(0)("CODICE").ToString()
        Gbl_Graph.Cod_Testo = dt.Rows(0)("TESTO").ToString()
        Dim xx = ""

        For Each riga As DataRow In dt.Rows

            ' 1. Lettura del campo codice
            cod = riga("Codice").ToString()
            If cod <> Gbl_Graph.Cod_ELCAR Then
                J = J + 1
                ReDim Preserve Gbl_Graph1(J)
                Gbl_Graph1(J).Cod_ELCAR = cod
                Gbl_Graph1(J).ID_Foglio = Convert.ToInt32(riga("ID_FOGLIO"))

                i = -1
            End If
            i = i + 1
            ReDim Preserve Gbl_Graph1(J).Data(i)
            Gbl_Graph1(J).Data(i).ID = Convert.ToInt32(riga("ID"))
            Gbl_Graph1(J).Data(i).Data = Convert.ToString(riga("DATA"))
            Gbl_Graph1(J).Data(i).TOT1_Pos = Convert.ToInt32(riga("tot1_pos"))
            Gbl_Graph1(J).Data(i).TOT1_Neg = Convert.ToInt32(riga("tot1_neg"))
            Gbl_Graph1(J).Data(i).Tot1_PROMPT = Convert.ToInt32(riga("tot1_prompt"))
            Gbl_Graph1(J).Data(i).TOT2_Pos = Convert.ToInt32(riga("tot2_pos"))
            Gbl_Graph1(J).Data(i).TOT2_Neg = Convert.ToInt32(riga("tot2_neg"))
            Gbl_Graph1(J).Data(i).Tot2_PROMPT = Convert.ToInt32(riga("tot2_prompt"))
            Gbl_Graph1(J).Data(i).Valutativo = Convert.ToBoolean(riga("valutativo"))
            Gbl_Graph1(J).Data(i).tuttelerisposte = Convert.ToBoolean(riga("tuttelerisposte"))
            Gbl_Graph1(J).Data(i).Compilato = Convert.ToBoolean(riga("compilato"))
        Next

    End Function
    Public Function Carica_dati_codice_elcar(cod As String, Optional mostragrafico As Boolean = False) As Integer
        'dalla tabella giornaliero del database del ragazzo, Carica tutti i record del codice richiesto leggendo tutti i record presenti che rientrano nell'intervallo di tempo indicato
        ' se intervallo =0 allora utilizza intervallo di default letto dal file ini
        'i valori dell'inrervallo possono essere: 15 gg, 1 mese, 3 mesi, 6 mesi, 1 anno,0  tutto

        Dim dt As DataTable = Class_GIORNALIERO.OttieniRecordPerCodice(cod)
        Do While dt.Rows.Count = 0
            ReDim Gbl_Graph.Data(0)
            Dim m = "nessun Codice trovato:" + vbCrLf
            Return -1
        Loop
        ' Ciclo che attraversa ogni riga della tabella
        Dim i = -1

        Gbl_Graph.Cod_ELCAR = cod
        Gbl_Graph.Cod_Testo = dt.Rows(0)("testo")



        For Each riga As DataRow In dt.Rows
            ' 1. Lettura del campo codice
            'cod = riga("Codice").ToString()
            i = i + 1
            ReDim Preserve Gbl_Graph.Data(i)
            Gbl_Graph.Data(i).ID = Convert.ToInt32(riga("ID"))
            Gbl_Graph.Data(i).Data = Convert.ToString(riga("DATA"))
            Gbl_Graph.Data(i).TOT1_Pos = Convert.ToInt32(riga("tot1_pos"))
            Gbl_Graph.Data(i).TOT1_Neg = Convert.ToInt32(riga("tot1_neg"))
            Gbl_Graph.Data(i).Tot1_PROMPT = Convert.ToInt32(riga("tot1_prompt"))
            Gbl_Graph.Data(i).Tot1 = Convert.ToInt32(riga("tot1"))
            Gbl_Graph.Data(i).Tot2 = Convert.ToInt32(riga("tot2"))
            Gbl_Graph.Data(i).TOT2_Pos = Convert.ToInt32(riga("tot2_pos"))
            Gbl_Graph.Data(i).TOT2_Neg = Convert.ToInt32(riga("tot2_neg"))
            Gbl_Graph.Data(i).Tot2_PROMPT = Convert.ToInt32(riga("tot2_prompt"))
            Gbl_Graph.Data(i).Valutativo = Convert.ToBoolean(riga("valutativo"))
            Gbl_Graph.Data(i).tuttelerisposte = Convert.ToBoolean(riga("tuttelerisposte"))
            Gbl_Graph.Data(i).Compilato = Convert.ToBoolean(riga("compilato"))
        Next
        Return i
    End Function
    Public Function Carica_dati_voce_CP(cod As String, Optional solocompilati As Boolean = True) As Integer
        Dim dt As DataTable = Class_Giornaliero_CP.OttieniRecordPerVoceCP(cod, solocompilati)
        Do While dt.Rows.Count = 0
            ReDim Gbl_Graph.Data(0)
            Dim m = "nessun Codice trovato:" + vbCrLf
            Return -1
        Loop

        ' Ciclo che attraversa ogni riga della tabella
        Dim i = -1

        Gbl_Graph.Cod_ELCAR = cod
        Gbl_Graph.Cod_Testo = dt.Rows(0)("testo")

        For Each riga As DataRow In dt.Rows
            i = i + 1
            ReDim Preserve Gbl_Graph.Data(i)
            Gbl_Graph.Data(i).ID = Convert.ToInt32(riga("ID"))
            Gbl_Graph.Data(i).Data = Convert.ToString(riga("DATA"))
            Gbl_Graph.Data(i).Tot1 = Convert.ToInt32(riga("tot"))
        Next
        Return i
    End Function




    Public Function Trend_1() As Boolean
        'Azzera 

        Dim ntot = Gbl_Graph.Data.Count
        Dim i0 = ntot - 1, perc As Int16 = 0
        Dim trovato = 0
        Dim xx = ""
        Dim max_risposte = 20
        Dim nv = 8  ' numero di label 
        Dim cnt = 6
        Dim totx1 = 0, totx2 = 0, tot2 = 0, finito = False
        Dim tot(nv, 1), tot_ok(nv, 1)
        Dim valori(nv) As String
        ReDim Gbl_Percentuale(8)
        Dim gg = 0

        ' Trova ultimi nv valori validi
        Dim idx(nv)

        If ntot > nv Then i0 = ntot - nv
        Do While Not finito
            If i0 < 0 Then Exit Do
            If Not Gbl_Graph.Data(i0).Compilato Then
                i0 = i0 - 1
                Continue Do
            End If
            totx2 = 0 : totx1 = 0
            Try
                totx1 = Gbl_Graph.Data(i0).Tot1
                totx2 = Gbl_Graph.Data(i0).Tot2
                gg = gg + 1
            Catch ex As Exception
                Exit Do
            End Try

            If totx2 > 0 Then
                xx = i0.ToString + ";2," + xx
            End If
            If totx1 > 0 Then
                xx = i0.ToString + ";1," + xx
            End If
            If totx1 = 0 And totx2 = 0 Then
                xx = i0.ToString + ";" + xx
            End If
            i0 = i0 - 1
        Loop

        Gbl_Graph.Labeltag = xx

        Dim x = Split(xx, ",")
        Dim n = x.Length - 1
        For jj = 0 To n

            valori(jj) = x(jj)
        Next
        'Scrittura del valore (indice e indice tot) dentro il tag della label


        Dim pos As Integer = 0
        Dim neg As Integer = 0
        Dim totale As Integer = 0
        Dim tot_pos_consecutivi = 0
        Dim tot_neg_consecutivi = 0
        Dim tot_80_consecutivi = 0
        'Dim Gbl_Percentuale(8) As Integer
        Dim IP = 0
        ' 3 Consecutivi >80% allora Passato
        ' 4 consecutivi trent positivo = Continuare
        ' 4 consecutivi trent negativo = Cambiare


        For k As Integer = 0 To n
            pos = 0 : neg = 0 : totale = 0

            If valori(k) = "" Then Continue For
            Dim x1 = Split(valori(k), ";")
            Dim i1 = Val(x1(0))
            Dim tipo = Val(x1(1))
            If Not Gbl_Graph.Data(i1).Compilato Then Continue For

            ' Recupera i dati in base al tipo (1 o 2)
            If tipo = 1 Then
                pos = Gbl_Graph.Data(i1).TOT1_Pos
                neg = Gbl_Graph.Data(i1).TOT1_Neg
                totale = Gbl_Graph.Data(i1).Tot1
            ElseIf tipo = 2 Then
                pos = Gbl_Graph.Data(i1).TOT2_Pos
                neg = Gbl_Graph.Data(i1).TOT2_Neg
                totale = Gbl_Graph.Data(i1).Tot2
            End If
            perc = Calcola_Percentuale(pos)
            Gbl_Percentuale(IP) = perc : IP = IP + 1
            Gbl_Graph.Data(i1).Perc = perc
        Next


        '-- Ora effettuo i controlli veri e propri sl trend 
        '-- Calcolo e scrivo la variabile  Gbl_Graph.Trend
        IP = IP - 1
        If IP < 0 Then
            Return False
        End If
        ReDim Preserve Gbl_Percentuale(IP)

        Call trend_2(IP)

        Return True



    End Function
    Public Sub trend_2(ip)
        '----- VERDE ---------------------------------
        '---- controllo se ho raggiunto la soglia ----
        '---- 3 Consecutivi >80% allora Passato   ----
        '---------------------------------------------
        Dim cnt1 = 0
        For i = 0 To ip
            If Gbl_Percentuale(i) >= 80 Then
                cnt1 = cnt1 + 1
            Else
                cnt1 = 0
            End If
        Next

        If cnt1 >= 3 Then
            Gbl_Graph.Trend = "SUPERATO"
            Exit Sub
        End If


        '-----------------------------------------
        '-- controllo il se il trend è positivo -- BLU
        '-- 4 risultati consecutivi in crescita >= 
        '-----------------------------------------
        Dim trend_pos = 0
        If Gbl_Percentuale(0) >= 80 Then trend_pos = trend_pos + 1
        For i = 1 To ip
            If Gbl_Percentuale(i) >= 80 Then
                trend_pos = trend_pos + 1
                Continue For
            End If
            If Gbl_Percentuale(i) >= Gbl_Percentuale(i - 1) Then
                If i = 1 Then
                    trend_pos = trend_pos + 2
                Else
                    trend_pos = trend_pos + 1
                End If
            Else
                If Gbl_Percentuale(i) < 80 Then
                    trend_pos = 0
                End If
            End If
        Next

        If trend_pos >= 4 Then
            Gbl_Graph.Trend = "POS4"
            Exit Sub
        ElseIf trend_pos >= 2 And cnt1 >= 2 Then
            Gbl_Graph.Trend = "POS4"
            Exit Sub
        End If

        '-----------------------------------------
        '-- controllo il se il trend è negativo -- ROSSO
        '-----------------------------------------
        Dim trend_neg = 0
        For i = 1 To ip
            If Gbl_Percentuale(i) < Gbl_Percentuale(i - 1) Then
                trend_neg = trend_neg + 1
            Else
                trend_neg = 0
            End If
        Next

        If trend_neg >= 4 Then
            Gbl_Graph.Trend = "NEG4"
            Exit Sub
        End If


        Dim ngiorni = Gbl_Graph.Data.Count
        Dim ntot = 0, ntotok = 0
        For j = 0 To ngiorni - 1
            If Gbl_Graph.Data(j).Tot1 > 0 Then
                ntot = ntot + 1
                If Gbl_Graph.Data(j).Tot1 = 20 Then ntotok = ntotok + 1
            End If
            If Gbl_Graph.Data(j).Tot2 > 0 Then
                ntot = ntot + 1
                If Gbl_Graph.Data(j).Tot2 = 20 Then ntotok = ntotok + 1
            End If
        Next


        '-------------------------------
        '-- controllo se ho zero dati -- BIANCO
        '-------------------------------
        If ntot = 0 Then
            Gbl_Graph.Trend = "NODATA"
            Exit Sub
        End If


        '----------------------------------------
        '-- controllo se ho troppi pochi dati -- NERO
        '----------------------------------------
        If ntot < 4 Then
            Gbl_Graph.Trend = "UNDEF"
            Exit Sub
        End If

        Gbl_Graph.Trend = "NOTREND"

    End Sub
    Function Calcola_Percentuale(POS, Optional MAX = 20) As Integer
        Dim i = POS * 100 / 20
        Return i
    End Function
    'Function Calcola_totali_giornaliero(dbbambino) As List(Of DatiGraficoGiornaliero)
    '    'devo sommare:
    '    'tutti i totale delle voci elcar
    '    'tutte le voci RT
    '    'NO voci CP
    '    '1) trovo elenco fogli di un bambino
    '    '2) per ogni foglio estraggo: 
    '    '   a) Totali delle voci elcar Tot1+tot2
    '    '   b) Totali delle voci positive elcar Tot1_pos+tot2_pos
    '    '   c) N. delle voci RT  


    'End Function
    Function soglia_Esercizio_Raggiunta() As Boolean

        Dim SUPERATO = 0, v = 0, tot = 0

        For i0 = 0 To Gbl_Graph.Data.Count - 1
            If Not Gbl_Graph.Data(i0).Compilato Then Continue For

            tot = Gbl_Graph.Data(i0).Tot1
            If tot = 20 Then
                v = Calcola_Percentuale(Gbl_Graph.Data(i0).TOT1_Pos)
                If v >= 80 Then
                    SUPERATO = SUPERATO + 1
                Else
                    SUPERATO = 0
                End If
            ElseIf tot < 20 And tot > 0 Then
                SUPERATO = 0
            End If
            If SUPERATO >= 2 Then Exit For


            tot = Gbl_Graph.Data(i0).Tot2
            If tot = 20 Then
                v = Calcola_Percentuale(Gbl_Graph.Data(i0).TOT2_Pos)
                If v >= 80 Then
                    SUPERATO = SUPERATO + 1
                Else
                    SUPERATO = 0
                End If
            ElseIf tot < 20 And tot > 0 Then
                SUPERATO = 0
            End If

            If SUPERATO >= 2 Then Exit For
        Next

        If SUPERATO >= 2 Then
            Return True
        Else
            Return False
        End If
    End Function
    Function Elimina_Codice(codice) As Boolean
        ' Elimina la voce che contiene il codice passato dall'elenco Gbl_elenco da
        For j = Gbl_VociDiUnFoglio.Count - 1 To 0 Step -1

            If Gbl_VociDiUnFoglio(j).Codice = codice Then
                Gbl_VociDiUnFoglio.RemoveAt(j)
                Return True
            End If
        Next

        Return False
    End Function
    Public Function IsWorkingDay(checkDate As Date) As Boolean

        ' 1. DEFINIZIONE DEI GIORNI LAVORATIVI DELLA SETTIMANA
        ' Puoi decidere se escludere o includere determinati giorni cambiando questo controllo
        If checkDate.DayOfWeek = DayOfWeek.Saturday OrElse checkDate.DayOfWeek = DayOfWeek.Sunday Then
            Return False
        End If

        ' 2. FESTIVITÀ FISSE ITALIANE (Giorno, Mese)
        ' Abbiamo aggiunto anche il 24/12 (Vigilia) come da tua richiesta
        Dim fixedHolidays As New List(Of (Giorno As Integer, Mese As Integer)) From {
            (1, 1),   ' Capodanno
            (6, 1),   ' Epifania
            (25, 4),  ' Liberazione
            (1, 5),   ' Festa dei Lavoratori
            (2, 6),   ' Festa della Repubblica
            (15, 8),  ' Ferragosto
            (1, 11),  ' Ognissanti
            (8, 12),  ' Immacolata Concezione
            (24, 12), ' Vigilia di Natale (Richiesta specifica)
            (25, 12), ' Natale
            (26, 12)  ' Santo Stefano
        }

        ' Controllo se la data coincide con una festa fissa
        For Each holiday In fixedHolidays
            If checkDate.Day = holiday.Giorno AndAlso checkDate.Month = holiday.Mese Then
                Return False
            End If
        Next

        ' 3. FESTIVITÀ MOBILI (Pasqua e Pasquetta tramite Algoritmo di Gauss)
        Dim dataPasqua As Date = CalcolaPasqua(checkDate.Year)
        Dim dataPasquetta As Date = dataPasqua.AddDays(1)

        If checkDate.Date = dataPasqua.Date OrElse checkDate.Date = dataPasquetta.Date Then
            Return False
        End If

        ' Se ha superato tutti i controlli, il giorno è lavorativo
        Return True
    End Function

    ''' <summary>
    ''' Algoritmo matematico di Gauss per calcolare la Domenica di Pasqua di qualsiasi anno.
    ''' </summary>
    Private Function CalcolaPasqua(anno As Integer) As Date
        Dim a As Integer = anno Mod 19
        Dim b As Integer = anno \ 100
        Dim c As Integer = anno Mod 100

        Dim p As Integer = (19 * a + b - (b \ 4) - ((b - ((b + 8) \ 25) + 1) \ 3) + 15) Mod 30
        Dim q As Integer = (32 + 2 * (b Mod 4) + 2 * (c \ 4) - p - (c Mod 4)) Mod 7
        Dim r As Integer = (p + q - 7 * ((a + 11 * p + 22 * q) \ 451) + 114)

        Dim mese As Integer = r \ 31
        Dim giorno As Integer = (r Mod 31) + 1

        Return New Date(anno, mese, giorno)
    End Function
    Function Prossimo_giorno_lavorativo(startDate As Date) As Date
        Dim nextDay As Date = startDate.AddDays(1)

        ' Ciclo continuo: finché il giorno analizzato NON è lavorativo, aggiunge un giorno
        While Not IsWorkingDay(nextDay)
            nextDay = nextDay.AddDays(1)
        End While

        ' Non appena il ciclo si interrompe, abbiamo trovato il giorno corretto
        Return nextDay
    End Function

    Sub Apri_documento(percorsoFile)
        Try

            Process.Start(New ProcessStartInfo(percorsoFile) With {.UseShellExecute = True})
        Catch ex As Exception
            MessageBox.Show("Impossibile aprire il file: " & ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Sub Stampa_foglio_giornaliero()
        '-- Esegue la stampa del foglio GBL_foglio Caricato --
        Dim m = ""
        Dim ID_foglio = Gbl_foglio.ID_foglio
        'Dim titolo = Gbl_giornaliero.Titolo
        'Dim bambino = Gbl_NOME_BAMBINO
        Dim bambino = Gbl_foglio.Bambino
        Dim Data = Strings.Format(CDate(Gbl_foglio.VociDiUnFoglio(0).DATA), "yyyy-MM-dd")

        Dim nomefoglio = Costruisci_nome_foglio(ID_foglio, Data, bambino)
        Dim documento = dir_bar(ini.Path_SCHEDE_DaStampare) + nomefoglio

        '-- controllo se esiste gia un file con o stesso nome --
        If IO.File.Exists(documento) Then
            Dim mm = "E' già presente un foglio con lo stesso nome" + vbCrLf
            mm = mm + nomefoglio + vbCrLf
            mm = mm + "Vuoi salvarlo ugualmente?"
            If MsgBox(mm, vbYesNo + vbInformation, "Foglio gia presente") = vbNo Then
                Call MsgBox("Foglio non salvato")
                Exit Sub
            Else
                Try
                    IO.File.Delete(documento)
                Catch ex As Exception
                    Call MsgBox("NON è stato possibile cancellare il foglio esistente")
                    Exit Sub
                End Try
            End If
        End If

        '-- Determino il modello da utilizzare --
        Dim nvoci_elcar = Gbl_foglio.VociDiUnFoglio.Count
        Dim nvoci_RT = Gbl_foglio.RT.Count

        Dim modello = Ritorna_modello_da_utilizzare(nvoci_elcar, nvoci_RT)
        If modello = "" Then
            Call MsgBox("Non è stato trovato nessun modello utilizzabile per questi fogli")
            m = "Non è stato trovato nessun modello utilizzabile (N_elcar:" + nvoci_elcar.ToString + " N_RT:" + nvoci_RT.ToString
            APPEND_LOG(m, ini.File_Log, True, "W")

            Exit Sub
        End If

        If Not IO.File.Exists(modello) Then
            m = "Il modello:" + vbCrLf + modello + vbCrLf + "NON è stato trovato"
            Call MsgBox(m, vbCritical + vbOKOnly, "MOdello non trovato")
            m = "Il file:" + modello + " non è stato trovato"
            APPEND_LOG(m, ini.File_Log, True, "E")
            Exit Sub
        End If

        '-- Creo il foglio da stampare copiandolo dal modello --
        IO.File.Copy(modello, documento, True)


        '-- compilo il foglio --

        Using doc As WordprocessingDocument =
            WordprocessingDocument.Open(documento, True)

            Dim body As Body = doc.MainDocumentPart.Document.Body

            If compila_foglio_giornaliero(body, Gbl_foglio) Then

                doc.MainDocumentPart.Document.Save()
            End If

        End Using
        m = "Foglio (" + nomefoglio + ") preparato" + vbCrLf
        m = m + "in: " + ini.Path_SCHEDE_DaStampare

        Call MsgBox(m)
    End Sub

    Function Build_fuldbname_Bambino(DB_name_bambino) As String
        Dim DB_BAMBINO = DB_name_bambino.ToUpper
        If InStr(DB_BAMBINO, ".DB") = 0 Then DB_BAMBINO = DB_BAMBINO + ".DB"
        Dim percorsoFileBambino As String = IO.Path.Combine(ini.DB_Bambini_path, DB_BAMBINO).ToUpper
        Return percorsoFileBambino

    End Function


    Public Sub XXX_JSON_SalvaStruttura(foglio As Struct_foglio, percorsoFile As String)
        Dim Jfoglio = JSON_Caricavoce_da_foglio(foglio)

        Try
            ' Configurazione opzioni
            Dim opzioni As New JsonSerializerOptions With {
                .WriteIndented = True ' Rende il file JSON leggibile e incolonnato
            }

            ' Serializza la struttura in stringa JSON
            Dim stringaJson As String = JsonSerializer.Serialize(Jfoglio, opzioni)

            ' Scrive sul disco
            File.WriteAllText(percorsoFile, stringaJson, System.Text.Encoding.UTF8)

        Catch ex As Exception
            Throw New Exception("Errore durante il salvataggio del JSON: " & ex.Message)
        End Try
    End Sub
    Public Function JSON_Caricavoce_da_foglio(foglio As Struct_foglio) As Struct_foglio_Json
        Dim f_json As New Struct_foglio_Json
        f_json.CP = foglio.CP
        f_json.RT = foglio.RT
        f_json.VociDiUnFoglio = foglio.VociDiUnFoglio
        f_json.ID_Bambino = foglio.ID_Bambino
        f_json.Bambino = foglio.Bambino
        f_json.Data_foglio = foglio.Data
        f_json.Stato = "DA COMPILARE"
        f_json.Data_foglio = Now.ToString
        Return f_json


    End Function
    Public Sub TODOLIST_Aggiorna_Foglio_In_Compilazione(ByVal jsonGrezzo As String)
        Try
            ' Il parsing avviene qui dentro in modo isolato e pulito
            Dim parser As System.Text.Json.JsonDocument = System.Text.Json.JsonDocument.Parse(jsonGrezzo)
            Dim root As System.Text.Json.JsonElement = parser.RootElement

            Dim id_questionario As Integer = root.GetProperty("id_questionario").GetInt32()
            Dim tabletId As String = root.GetProperty("device_id").GetString()
            'Dim idbambino As Integer = root.GetProperty("id_bambino").GetInt32()

            If Not Verifica_ID_messaggio(tabletId) Then Exit Sub

            '1) Trovo id Device
            Dim device As New Class_DEVICE
            Dim result = device.Trova_dispositivo(tabletId)
            If result.Esito Then
                'Aggiorno e todolist 
                Dim id = result.Devices.ID
                Dim todolist = Class_TODOLIST.Load_By_ID(id_questionario)
                todolist.CLIENT_ID = id
                todolist.CLIENT_NOME = result.Devices.NOME
                todolist.CLIENT_DATAINVIO = Now.ToString
                todolist.CLIENT_DATARICONSEGNA = "INVIATO PER LA COMPILAZIONE IL " + todolist.CLIENT_DATAINVIO
                todolist.Save()
            End If
            Console.WriteLine("🎯 DB AGGIORNATO: Foglio " & id_questionario & " assegnato a " & tabletId)
        Catch ex As Exception
            Console.WriteLine("ERRORE NEL PARSING DELLA CONFERMA DB: " & ex.Message)
        End Try
    End Sub

    Public Function TODOLIST_rimuovi_Foglio_Compilato(ID_TODOLIST As Integer) As Boolean
        'Elimina il record dalla todolist
        Try

            If Not Class_TODOLIST.Delete(ID_TODOLIST) Then
                Dim m = "[009] TODOLIST Errore cancellazione record " + ID_TODOLIST.ToString
                APPEND_LOG(m, ini.File_Log, True, "E")
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
    Public Function Verifica_ID_messaggio(ByVal ID_Messaggio As String) As Boolean
        ' Se la stringa è vuota o composta solo da spazi, non è valida
        If String.IsNullOrWhiteSpace(ID_Messaggio) Then Return False

        ' Espressione regolare per Android ID:
        ' ^         = Inizio della stringa
        ' [0-9a-fA-F] = Accetta solo numeri da 0 a 9 e lettere da A a F (sia minuscole che maiuscole)
        ' {16}      = Devono essere ESATTAMENTE 16 caratteri
        ' $         = Fine della stringa
        Dim espressioneAndroid As String = "^[0-9a-fA-F]{16}$"

        ' Ritorna True se rispetta il formato, altrimenti False
        Return Regex.IsMatch(ID_Messaggio, espressioneAndroid)
    End Function


    Public Function Decode_Android_message(ByVal msg As String) As Struct_Decode_Android_message
        Dim info As New Struct_Decode_Android_message
        info.type = ""
        info.deviceId = ""

        Try
            If Not String.IsNullOrWhiteSpace(msg) AndAlso (msg.Trim().StartsWith("{") OrElse msg.Trim().StartsWith("[")) Then
                Using parser As System.Text.Json.JsonDocument = System.Text.Json.JsonDocument.Parse(msg)
                    Dim root As System.Text.Json.JsonElement = parser.RootElement

                    ' 1. Estrazione sicura del Tipo Comando
                    If root.TryGetProperty("type", Nothing) Then
                        info.type = root.GetProperty("type").GetString()
                    End If

                    ' 2. Estrazione sicura del Dispositivo (cerca sia device_id che il minuscolo device_id hardware)
                    If root.TryGetProperty("device_id", Nothing) Then
                        info.deviceId = root.GetProperty("device_id").GetString()
                    End If
                End Using
            End If
        Catch ex As Exception
            Console.WriteLine("⚠️ Errore silenziato nel pre-parsing del messaggio JSON: " & ex.Message)
        End Try

        Return info
    End Function

    Public Function Comunica_Compilazione_annullata(ByVal idToDoList As Integer) As Boolean
        If idToDoList = 0 Then
            Dim m = "Annullamento Compilazione Foglio: " + Gbl_foglio.ID_foglio.ToString + ") di: " + Gbl_foglio.Bambino
            APPEND_LOG(m, ini.File_Log, True, "1")

        End If
        Try
            Dim passato = TODOLIST_rimuovi_Foglio_Compilato(idToDoList)
            If passato Then
                Dim m = "Annullamento Compilazione Foglio: " + Gbl_foglio.ID_foglio.ToString + ") di: " + Gbl_foglio.Bambino
                APPEND_LOG(m, ini.File_Log, True, "1")
            End If
        Catch ex As Exception
            Console.WriteLine("CRASH DURANTE IL SALVATAGGIO DEL JSON COMPILATO IN VB: " & ex.Message)
            Return False
        End Try
    End Function



    Public Function SalvaFoglioCompilato_Json(ByVal messaggioRicevuto As String) As Boolean
        If String.IsNullOrWhiteSpace(messaggioRicevuto) Then Return False

        Try
            ' 1. Parsing dell'involucro esterno inviato dal tablet
            Using parser As System.Text.Json.JsonDocument = System.Text.Json.JsonDocument.Parse(messaggioRicevuto)
                Dim root As System.Text.Json.JsonElement = parser.RootElement

                ' 2. Estraiamo la proprietà "dati" che contiene il vero e proprio foglio compilato
                If Not root.TryGetProperty("dati", Nothing) Then
                    Console.WriteLine("⚠️ Errore: Proprietà 'dati' non trovata nel JSON del tablet.")
                    Return False
                End If

                Dim foglioElement As System.Text.Json.JsonElement = root.GetProperty("dati")
                Dim foglioGrezzoJson As String = foglioElement.GetRawText()

                ' 3. Deserializzazione speculare nella tua struttura Struct_foglio_Json
                Dim opzioni As New System.Text.Json.JsonSerializerOptions()
                opzioni.PropertyNameCaseInsensitive = True ' 🎯 Garanzia totale contro problemi di maiuscole/minuscole

                Dim foglioCompilato As Struct_foglio_Json = System.Text.Json.JsonSerializer.Deserialize(Of Struct_foglio_Json)(foglioGrezzoJson, opzioni)

                ' 4. Recupero dei puntatori anagrafici dal foglio riesumato
                Dim idTodoList As Integer = foglioCompilato.ID_Questionario
                Dim idFoglio As Integer = foglioCompilato.ID_foglio
                Dim idbambino As Integer = foglioCompilato.ID_Bambino


                ' Sicurezza: verifichiamo la stabilità dei parametri chiave prima di toccare il database
                If idTodoList = 0 OrElse idFoglio = 0 Then
                    Console.WriteLine("⚠️ Errore: ID_Questionario o ID_foglio pari a zero. Salvataggio abortito.")
                    Return False
                End If
                '  "✅❌⚠️ℹ️"

                ' 5. SALVATAGGIO REALE NEL DATABASE SQLITE DI WINDOWS
                ' Qui invochiamo la tua routine industriale per sovrascrivere il foglio già esistente.
                ' Passiamo gli array compilati (VociDiUnFoglio, RT, CP) per salvarli nel DB.


                Dim dbBambino = Class_ANAGRAFICA.Load(idbambino).DBname
                Dim foglio = Carica_foglio(idFoglio, dbBambino)

                foglio.CP = foglioCompilato.CP
                foglio.RT = foglioCompilato.RT
                foglio.VociDiUnFoglio = foglioCompilato.VociDiUnFoglio
                foglio.Compilato = 1

                Dim salvato = MY_DB.Foglio_Esistente_Aggiorna_solo_Totali(foglio, dbBambino)

                Dim salvataggiook = False

                If salvato.OK Then
                    ' Aggiorna anche lo stato del record generale nella tabella TODOLIST del PC Windows
                    Dim passato = TODOLIST_rimuovi_Foglio_Compilato(idTodoList)
                    Fogli_compilati_aggiungi_numero(1)
                    salvataggiook = True
                    Console.WriteLine("✅ Foglio ID " & idFoglio & " del bambino " & foglio.Bambino & " aggiornato nel DB di Windows.")
                    Return True
                End If

                '**** Nessun record salvato
                Console.WriteLine("❌ Errore durante la query di aggiornamento SQLite su Windows: " & salvato.erroreSu)
                Dim m = "[005] " + foglio.Bambino + "(" + salvato.erroreSu + ")"
                APPEND_LOG(m, ini.File_Log, True, "W")
                Return False

            End Using

        Catch ex As Exception
            Console.WriteLine("CRASH DURANTE IL SALVATAGGIO DEL JSON COMPILATO IN VB: " & ex.Message)
            Return False
        End Try
    End Function



    Public Function Esportazione_ottieni_tutti_elcar(dbBambino, Optional solocompilati = True) As List(Of Esportazione_dati_ELCAR)
        Dim dt As DataTable = Class_GIORNALIERO.OttieniTuttiIRecordGiornalieri(dbBambino, solocompilati)
        Dim elenco_ELCAR_Esportazione As New List(Of Esportazione_dati_ELCAR)
        For Each riga As DataRow In dt.Rows
            Dim voce As New Esportazione_dati_ELCAR
            voce.Codice = riga("Codice").ToString()
            voce.Descrizione = riga("testo").ToString()
            voce.Data = riga("data").ToString()
            voce.Tot1 = riga("tot1").ToString()
            voce.Tot1_Pos = riga("tot1_pos").ToString()
            voce.Tot2 = riga("tot2").ToString()
            voce.Tot2_Pos = riga("tot2_pos").ToString()
            elenco_ELCAR_Esportazione.Add(voce)
        Next
        Return elenco_ELCAR_Esportazione
    End Function

    Public Function Esportazione_giornaliero(dbBambino) As List(Of Esportazione_dati_ELCAR)
        Dim totali_voci = Carico_totali_giornalieri(dbBambino)
        Dim elenco_CTRT_Esportazione As New List(Of Esportazione_dati_ELCAR)
        For Each riga In totali_voci
            Dim voce As New Esportazione_dati_ELCAR
            voce.Descrizione = riga.tot
            voce.Data = riga.Data
            voce.Tot1 = riga.tot
            voce.Tot1_Pos = riga.tot_POS_voci
            elenco_CTRT_Esportazione.Add(voce)
        Next
        Return elenco_CTRT_Esportazione

    End Function

    Public Function Esportazione_ottieni_tutti_RT(dbBambino) As List(Of Esportazione_dati_CPRT)
        Dim dt As DataTable = Class_Giornaliero_ROUTINE.OttieniTuttiIRecordGiornalieri(dbBambino)
        Dim elenco_CTRT_Esportazione As New List(Of Esportazione_dati_CPRT)
        For Each riga As DataRow In dt.Rows
            Dim voce As New Esportazione_dati_CPRT
            voce.Descrizione = riga("testo").ToString()
            voce.Data = riga("data").ToString()
            voce.Tot = riga("tot").ToString()
            elenco_CTRT_Esportazione.Add(voce)
        Next
        Return elenco_CTRT_Esportazione
    End Function
    Public Function Esportazione_ottieni_tutti_CP(dbBambino) As List(Of Esportazione_dati_CPRT)
        Dim dt As DataTable = Class_Giornaliero_CP.OttieniTuttiIRecordGiornalieri(dbBambino)
        Dim elenco_CTRT_Esportazione As New List(Of Esportazione_dati_CPRT)
        For Each riga As DataRow In dt.Rows
            Dim voce As New Esportazione_dati_CPRT
            voce.Descrizione = riga("testo").ToString()
            voce.Data = riga("data").ToString()
            voce.Tot = riga("tot").ToString()
            elenco_CTRT_Esportazione.Add(voce)
        Next
        Return elenco_CTRT_Esportazione
    End Function

    Function Carico_totali_giornalieri(dbbambino As String) As List(Of Dati_Totali_Giornaliero)
        Dim Totale_voci As New List(Of Dati_Totali_Giornaliero)

        'ritorna tutti i totali delle voci ELCAR
        Totale_voci = Class_GIORNALIERO.totali_giornalieri(dbbambino)

        'ritorna tutti il numero delle voci RT
        Dim totale_Rt = Class_Giornaliero_ROUTINE.totali_RT_giornalieri

        ' unisco i due risultati nel totale_voce
        'NB di RT utilizzo solo il numero di eventi flaggati (tot=1)
        Dim idf = 0
        Dim s1 As New DatiGrafico
        Dim COL0 = 0
        'ListaDati.Clear()

        For j = 0 To Totale_voci.Count - 1
            idf = Totale_voci(j).id_foglio
            Dim voce = Totale_voci(j)
            For Each vocert In totale_Rt
                If idf = vocert.id_foglio Then
                    voce.tot_RT = vocert.tot_RT
                    voce.tot = vocert.tot_RT + voce.tot_voci
                    's1.Data = Date.ParseExact(Totale_voci(j).Data, "dd/MM/yyyy", Nothing)
                    's1.ValoriY = New List(Of Integer)({voce.tot, voce.tot_POS_voci})
                    's1.Color = New List(Of Integer)({COL0, COL0})
                    'ListaDati.Add(s1)
                    Totale_voci(j) = voce
                    Exit For
                End If
            Next
        Next
        'Gbl_datigrafico = ListaDati
        Return Totale_voci


    End Function

    Public Sub PopolaAlberoElcar(treeView As TreeView, stringaConnessione As String)
        treeView.Nodes.Clear()

        Dim listaElementi As New List(Of ElementoAlbero)()
        Dim dizionarioNodi As New Dictionary(Of String, TreeNode)()

        Dim query As String = "SELECT CODICE AS CodiceNodo, NULL AS CodicePadre, TESTO FROM ELCAR " &
                          "UNION ALL " &
                          "SELECT (CODICE_ELCAR || '.' || CODICE_VOCE), CODICE_ELCAR, TESTO FROM ELCAR_VOCI " &
                          "UNION ALL " &
                          "SELECT (ELCAR || '.' || ID_SUB), ELCAR, TESTO FROM ELCAR_VOCI_SUB WHERE Deleted = 0 OR Deleted IS NULL;"
        '"SELECT (ELCAR || '.' || ID_SUB), ELCAR, TESTO FROM ELCAR_VOCI_SUB WHERE Deleted = 0 OR Deleted IS NULL " &
        '"UNION ALL " &
        '"SELECT (ELCAR || '.' || ID_SUB), ELCAR, TESTO FROM ELCAR_VOCI_SUB2 WHERE Deleted = 0 OR Deleted IS NULL;"

        Using conn As New SqliteConnection(stringaConnessione)
            Using cmd As New SqliteCommand(query, conn)
                Try
                    conn.Open()
                    Using reader As SqliteDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim elem As New ElementoAlbero With {
                            .CodiceNodo = reader("CodiceNodo").ToString().Trim(),
                            .CodicePadre = If(reader("CodicePadre") Is DBNull.Value, String.Empty, reader("CodicePadre").ToString().Trim()),
                            .Testo = reader("TESTO").ToString()
                        }
                            listaElementi.Add(elem)
                        End While
                    End Using
                Catch ex As Exception
                    MessageBox.Show("Errore di lettura: " & ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            End Using
        End Using

        ' --- FASE DI COSTRUZIONE E ORDINAMENTO ALBERO ---
        treeView.BeginUpdate()

        ' 1. Dividiamo le Radici (Livello 1) dai Figli (Livelli 2, 3, 4)
        Dim nodiRadice = listaElementi.Where(Function(x) String.IsNullOrEmpty(x.CodicePadre)).ToList()
        Dim nodiFigli = listaElementi.Where(Function(x) Not String.IsNullOrEmpty(x.CodicePadre)).ToList()

        ' 2. Ordiniamo le Radici convertendo i numeri romani
        Dim nodiRadiceOrdinati = nodiRadice.OrderBy(Function(x) EstraiChiaveOrdinamentoRomana(x.CodiceNodo)).ToList()

        ' 3. Inseriamo le Radici evitando duplicati
        For Each radice In nodiRadiceOrdinati
            If Not dizionarioNodi.ContainsKey(radice.CodiceNodo) Then
                Dim nuovoNodo As New TreeNode(radice.CodiceNodo & " - " & radice.Testo) With {.Name = radice.CodiceNodo}
                treeView.Nodes.Add(nuovoNodo)
                dizionarioNodi.Add(radice.CodiceNodo, nuovoNodo)
            End If
        Next

        ' 4. ORDINAMENTO CRUCIALE PER I FIGLI:
        ' Ordiniamo prima in base al numero di punti '.' presenti nel codice (Livello 2 ha 1 punto, Livello 3 ha 2 punti, Livello 4 ha 3 punti).
        ' In questo modo i padri di livello 2 e 3 verranno SEMPRE inseriti prima dei loro rispettivi figli di livello 3 e 4.
        ' Successivamente ordiniamo alfabeticamente/numericamente per il codice.
        Dim nodiFigliOrdinati = nodiFigli.
        OrderBy(Function(x) x.CodiceNodo.Count(Function(c) c = "."c)).
        ThenBy(Function(x) x.CodiceNodo.Length).
        ThenBy(Function(x) x.CodiceNodo).
        ToList()

        ' 5. Inseriamo i figli associandoli al rispettivo padre con controllo duplicati incorporato
        For Each figlio In nodiFigliOrdinati
            ' Se il nodo figlio è già stato inserito (duplicato nel DB), lo saltiamo per evitare l'errore ArgumentException
            If dizionarioNodi.ContainsKey(figlio.CodiceNodo) Then Continue For

            Dim nuovoNodo As New TreeNode(figlio.CodiceNodo & " - " & figlio.Testo) With {.Name = figlio.CodiceNodo}

            If dizionarioNodi.ContainsKey(figlio.CodicePadre) Then
                ' Associa il nodo al suo vero padre gerarchico
                dizionarioNodi(figlio.CodicePadre).Nodes.Add(nuovoNodo)
                dizionarioNodi.Add(figlio.CodiceNodo, nuovoNodo)
            Else
                ' Se il padre manca temporaneamente o è orfano, lo mette alla radice per sicurezza
                treeView.Nodes.Add(nuovoNodo)
                dizionarioNodi.Add(figlio.CodiceNodo, nuovoNodo)
            End If
        Next

        treeView.EndUpdate()
    End Sub

    Private Class ElementoAlbero
        Public Property CodiceNodo As String
        Public Property CodicePadre As String
        Public Property Testo As String
    End Class

    Private Function EstraiChiaveOrdinamentoRomana(codice As String) As String
        If String.IsNullOrEmpty(codice) Then Return ""
        Dim parti As String() = codice.Split("."c)
        Dim parteRomana As String = parti(0).ToUpper()
        Dim valoreArabo As Integer = RomanoInArabo(parteRomana)

        If valoreArabo > 0 Then
            Dim restoCodice As String = If(parti.Length > 1, "." & parti(1), "")
            Return valoreArabo.ToString("D4") & restoCodice
        End If
        Return codice
    End Function

    Private Function RomanoInArabo(romano As String) As Integer
        Dim dizionarioRomano As New Dictionary(Of Char, Integer) From {
        {"I"c, 1}, {"V"c, 5}, {"X"c, 10}, {"L"c, 50}, {"C"c, 100}, {"D"c, 500}, {"M"c, 1000}
    }
        Dim totale As Integer = 0
        Dim valorePrecedente As Integer = 0
        For i As Integer = romano.Length - 1 To 0 Step -1
            Dim carattereCorrente As Char = romano(i)
            If dizionarioRomano.ContainsKey(carattereCorrente) Then
                Dim valoreCorrente As Integer = dizionarioRomano(carattereCorrente)
                If valoreCorrente < valorePrecedente Then
                    totale -= valoreCorrente
                Else
                    totale += valoreCorrente
                End If
                valorePrecedente = valoreCorrente
            Else
                Return 0
            End If
        Next
        Return totale
    End Function

End Module
