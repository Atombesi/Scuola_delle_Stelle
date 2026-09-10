Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Reflection.Emit
Imports System.Runtime.InteropServices
Imports Microsoft.Data.SqlClient
Imports Microsoft.Data.Sqlite
Imports System.Text.RegularExpressions
Imports DocumentFormat.OpenXml.Wordprocessing
Imports MyColor = System.Drawing.Color


Imports QRCoder
Imports ZXing
Imports ZXing.Common
Imports ZXing.Windows.Compatibility
Imports System.Windows.Documents.DocumentStructures
Imports OpenTK.Graphics.OpenGL
Module Codice
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
        Public DB_Bambini_path As String   ' Percorso dove si trovano tutti i database dei ragazzi
        Public Tema As String
        Public Scanrow_Offset As Integer
        Public Scanrow_Height As Integer
        Public Cell_Height As Integer
        Public Size_Riga As Struct_Size_Offset
        Public Size_Cella As Struct_Size_Offset
        Public ELCAR_LETTERE() As String
        Public Modello_Schede As String ' Full_path_Modello Foglio schede
        Public Path_SCHEDE_DaCompilare As String
        Public Path_SCHEDE_COMPILATE As String
        Public Graph_interval As Integer ' Intervallo di default per i dati del grafico
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
    End Structure

    Public Structure Struct_Elenco_titoli_usati
        ' serve nella struttura graph
        Public Titolo As String
        Public id_foglio() As Long
    End Structure


    Public Structure Struct_FogliDaStampare
        Public Bambino As String
        Public ID_Bambino As Integer
        Public DB_Bambino As String
        Public Data As String
        Public Titolo As String
        Public filename As String
        Public id_foglio As Integer
        Public Registrato As Boolean    'True iL FOGLIO è PRESENTE NEL DATABASE DEL BAMBINO
        Public doc_Esistente As Boolean 'True esiste gia il file docx
        Public Compilato As Boolean     'True il foglio risulta compilato
        Public DaStampare As Boolean    'True bisogna preparare il file docx

    End Structure
    Public Gbl_Elenco_fogli_stampa() As Struct_FogliDaStampare

    Public Gbl_Elenco_titoli_usati() As Struct_Elenco_titoli_usati

    Public Gbl_Graph As Struct_Graph_Codice
    Public Gbl_Graph1() As Struct_Graph_Codice

    Public Gbl_anagrafica As New Class_ANAGRAFICA()
    Public Gbl_Elenco_bambini As List(Of Class_ANAGRAFICA)
    Public Gbl_elenco_fogli_bambino As List(Of Class_Elenco_fogli)
    Public Gbl_Giornaliero As New Class_GIORNALIERO
    Public Gbl_Class_Voci_Personali As New Class_ELCAR_VOCI_PERSONALI
    Public Gbl_Class_Voci_sub2 As New Class_ELCAR_VOCI_SUB2
    Public Gbl_Class_Voci_sub As New Class_ELCAR_VOCI_SUB
    Public Gbl_miaLista As New List(Of Class_ElementoCombo)()
    Public Gbl_miaLista_Anagrafiche As New List(Of Class_ElementoCombo)()
    Public Gbl_miaLista_fogli As New List(Of Class_ElementoCombo)()


    Public Gbl_VociDiUnFoglio As New List(Of Class_GIORNALIERO)
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

    Public cod(7) As String


    Dim img, bmp As Bitmap
    Public CODICI_ELCAR = New String() {"I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X", "XI", "XII", "XIII", "XIV", "XV"}




    Public Class DB

        Public Shared ReadOnly ConnectionString As String =
        "Server=.\SQLEXPRESS;Database=Scuola;Trusted_Connection=True;"
        Public Shared Function GetConnection() As SqlConnection
            Return New SqlConnection(ConnectionString)
        End Function

    End Class

    Function Costruisci_nome_foglio(ID_foglio, titolo, data) As String
        Dim nomefoglio = data + "-" + Gbl_NOME_BAMBINO + "-(" + titolo + ")-(" + ID_foglio.ToString + ").DOCX"
        Return nomefoglio

    End Function
    Sub aprifilemanager(PATH)
        Dim percorsoCartella As String = PATH
        If Not System.IO.Directory.Exists(percorsoCartella) Then
            MsgBox("La cartella non esiste o il percorso è errato.")
        End If


        Process.Start(New ProcessStartInfo(percorsoCartella) With {.UseShellExecute = True})
    End Sub
    Public Function Costruisci_filenameDB_bambino(Indexof_Gbl_Elenco_bambini As Integer, Optional Autocrea As Boolean = True) As String
        Dim jb = Indexof_Gbl_Elenco_bambini
        If jb < 0 Then Return ""

        Dim DB_BAMBINO = Gbl_Elenco_bambini(Gbl_Jb).DBname.ToUpper
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

    Sub Read_ini()
        Dim x = ""
        Gbl_app_path = Mid(System.Reflection.Assembly.GetExecutingAssembly.Location, 1, System.Reflection.Assembly.GetExecutingAssembly.Location.LastIndexOf("\")).ToUpper
        If InStr(Gbl_app_path, "BIN") > 0 Then
            Dim io = InStr(Gbl_app_path, "BIN")
            Gbl_app_path = Left(Gbl_app_path, io - 2)
        End If
        Dim fileini = Gbl_app_path + "\Scuola_delle_Stelle.ini"

        Dim k = "PATH"


        Dim y = Gbl_app_path + "\ScuoladelleStelle.db"
        ini.Database = ReadINIString(fileini, k, "Database", y)

        ini.DB_Bambini_path = ReadINIString(fileini, k, "Db_Bimbi_path", Gbl_app_path)
        If Not IO.Directory.Exists(ini.DB_Bambini_path) Then
            IO.Directory.CreateDirectory(ini.DB_Bambini_path)
        End If

        x = dir_bar(Gbl_app_path) + "SCHEDE_DA_COMPILARE"
        ini.Path_SCHEDE_DaCompilare = ReadINIString(fileini, k, "Schede_da_Compilare", x)
        If Not IO.Directory.Exists(ini.Path_SCHEDE_DaCompilare) Then
            IO.Directory.CreateDirectory(ini.Path_SCHEDE_DaCompilare)
        End If

        x = dir_bar(Gbl_app_path) + "SCHEDE_COMPILATE"
        ini.Path_SCHEDE_COMPILATE = ReadINIString(fileini, k, "SCHEDE_COMPILATE", x)
        If Not IO.Directory.Exists(ini.Path_SCHEDE_COMPILATE) Then
            IO.Directory.CreateDirectory(ini.Path_SCHEDE_COMPILATE)
        End If

        x = dir_bar(Gbl_app_path) + "Modello_scheda.docx"
        ini.Modello_Schede = ReadINIString(fileini, k, "Modello_Schede_Esercizi", x)
        If Not IO.Directory.Exists(ini.Path_SCHEDE_DaCompilare) Then
            IO.Directory.CreateDirectory(ini.Path_SCHEDE_DaCompilare)
        End If



        k = "GENERAL"




        k = "PARAMETER_SCAN"
        ini.Scanrow_Offset = CInt(ReadINIString(fileini, k, "Scanrow_Offset", "50"))
        Gbl_Offset = ini.Scanrow_Offset

        ini.Size_Riga.Height = CInt(ReadINIString(fileini, k, "Row_Height", "180"))
        ini.Size_Riga.Width = CInt(ReadINIString(fileini, k, "Row_Width", "1960"))
        ini.Size_Riga.xOffset = CInt(ReadINIString(fileini, k, "Row_XOffset", "995")) 'distanza (in pixel) della prima cella dal 3 spot del qrcode della riga

        k = "ELCAR"
        Dim tot = CInt(ReadINIString(fileini, k, "TOT", "11"))
        ReDim ini.ELCAR_LETTERE(tot)
        For J = 0 To tot - 1
            y = ReadINIString(fileini, k, CODICI_ELCAR(J), "A")
            ini.ELCAR_LETTERE(J) = y
        Next

        k = "GRAPH"
        ini.Graph_interval = CInt(ReadINIString(fileini, k, "Intervallo default dati grafico  (gg)", "15"))

    End Sub

    Function Carica_bambini(Optional soloattivi As Boolean = True) As List(Of Class_ANAGRAFICA)

        Dim risultati As List(Of Class_ANAGRAFICA) = Class_ANAGRAFICA.CercaPerCognome("", soloattivi)
        Return risultati

    End Function
    Public Function GeneraQRCode_Senzabordo(testo As String, Optional pixelPerModulo As Integer = 10, Optional Spessorebordo As Integer = 1) As Bitmap

        Using qrGenerator As New QRCodeGenerator()
            Using qrCodeData As QRCodeData = qrGenerator.CreateQrCode(testo, QRCodeGenerator.ECCLevel.Q)
                Using qrCode As New QRCode(qrCodeData)

                    ' 1. Genera il QR senza il bordo della libreria (False)
                    Using qrSenzaBordo As Bitmap = qrCode.GetGraphic(pixelPerModulo, MyColor.Black, MyColor.White, False)

                        ' 2. Scegli lo spessore del tuo bordo personalizzato (es. 6 pixel)

                        ' 3. Crea una nuova bitmap più grande per ospitare il bordo
                        Dim qrConBordo As New Bitmap(qrSenzaBordo.Width + (Spessorebordo * 2), qrSenzaBordo.Height + (Spessorebordo * 2))

                        ' 4. Disegna lo sfondo bianco e incolla il QR al centro
                        Using g As Graphics = Graphics.FromImage(qrConBordo)
                            g.Clear(MyColor.White) ' Colore del bordo
                            g.DrawImage(qrSenzaBordo, Spessorebordo, Spessorebordo)
                        End Using

                        Return qrConBordo
                    End Using

                End Using
            End Using
        End Using
    End Function

    Function ROTATE(BMP As Image, Optional angolo As Integer = 90) As Image
        'BMP = img
        If angolo = 90 Then
            BMP.RotateFlip(RotateFlipType.Rotate270FlipNone)
        ElseIf angolo = 180 Then
            BMP.RotateFlip(RotateFlipType.Rotate180FlipNone)
        End If
        Return BMP
    End Function


    Public Function RuotaImmagine(ByVal imgOrigine As Bitmap, ByVal angoloGradi As Single) As Bitmap
        ' Calcola le nuove dimensioni per evitare che gli angoli vengano tagliati durante la rotazione
        ' (Facoltativo: se l'angolo è minimo, es. < 2°, puoi mantenere le stesse dimensioni di imgOrigine)
        Dim nuovaLarghezza As Integer = imgOrigine.Width
        Dim nuovaAltezza As Integer = imgOrigine.Height

        ' Crea una nuova bitmap vuota che conterrà l'immagine ruotata
        Dim imgRuotata As New Bitmap(nuovaLarghezza, nuovaAltezza, imgOrigine.PixelFormat)

        ' Imposta la stessa risoluzione DPI (fondamentale per le scansioni A4)
        imgRuotata.SetResolution(imgOrigine.HorizontalResolution, imgOrigine.VerticalResolution)

        ' Inizializza l'oggetto Graphics per disegnare sulla nuova bitmap
        Using g As Graphics = Graphics.FromImage(imgRuotata)
            ' Massima qualità di rendering (ideale per non perdere dettagli sui testi/QR code)
            g.InterpolationMode = InterpolationMode.HighQualityBicubic
            g.SmoothingMode = SmoothingMode.HighQuality
            g.PixelOffsetMode = PixelOffsetMode.HighQuality

            ' Sposta l'origine degli assi (0,0) esattamente al centro dell'immagine
            g.TranslateTransform(nuovaLarghezza / 2, nuovaAltezza / 2)

            ' Applica la rotazione dell'angolo desiderato (valori positivi = orario, negativi = antiorario)
            g.RotateTransform(angoloGradi)

            ' Riposiziona l'asse per disegnare l'immagine centrata rispetto al nuovo centro
            g.TranslateTransform(-imgOrigine.Width / 2, -imgOrigine.Height / 2)

            ' Disegna l'immagine originale applicando la trasformazione geometrica
            g.DrawImage(imgOrigine, New Point(0, 0))
        End Using

        Return imgRuotata
    End Function
    Function Calcola_angolo_di_Rotazione(x1, y1, x2, y2) As Single

        ' Calcola la differenza orizzontale e verticale
        Dim deltaX As Single = x2 - x1
        Dim deltaY As Single = y2 - y1

        ' Calcola l'angolo in radianti e convertilo in gradi
        Dim angoloRadianti As Double = Math.Atan2(deltaY, deltaX)
        Dim angoloGradi As Single = CSng(angoloRadianti * (180.0 / Math.PI))
        Return -angoloGradi
        ' Ora puoi raddrizzare l'intera pagina prima di ritagliare le 19 celle!
    End Function
    Function Crea_DB_Bimbo(dbname) As Boolean
        If IO.File.Exists(dbname) Then Return True
        Dim db_empty = dir_bar(extr_dir(ini.Database)) + "DB_BAMBINO.db"
        If InStr(dbname, ".DB") = 0 Then dbname = dbname + ".DB"
        IO.File.Copy(db_empty, dbname)
        If IO.File.Exists(dbname) Then
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
    Public Sub SET_DOCX(body As Body, codice As String, testo As String)
        ' Sicurezza: verifica che il body e i parametri non siano nulli
        If body Is Nothing OrElse String.IsNullOrEmpty(codice) Then Exit Sub
        If testo Is Nothing Then testo = String.Empty

        ' Recupera tutti i paragrafi presenti nel corpo del documento
        Dim paragrafi As IEnumerable(Of Paragraph) = body.Descendants(Of Paragraph)()

        For Each p As Paragraph In paragrafi
            ' Se il testo del paragrafo contiene il codice da sostituire
            If p.InnerText.Contains(codice) Then

                ' 1. Identifica il primo blocco di formattazione (Run) per usarlo come base
                Dim primoRun As Run = p.GetFirstChild(Of Run)()
                Dim proprietàFormattazione As RunProperties = Nothing

                ' Clona le proprietà di formattazione (Font, Dimensione, Grassetto...) se presenti
                If primoRun IsNot Nothing AndAlso primoRun.RunProperties IsNot Nothing Then
                    proprietàFormattazione = CType(primoRun.RunProperties.CloneNode(True), RunProperties)
                End If

                ' 2. Estrae l'intero testo unito del paragrafo ed esegue la sostituzione
                Dim testoUnito As String = p.InnerText.Replace(codice, testo)

                ' 3. Svuota completamente il paragrafo da tutti i vecchi nodi di testo frammentati
                p.RemoveAllChildren(Of Run)()

                ' 4. Crea un nuovo blocco di testo pulito e unito
                Dim nuovoRun As New Run()

                ' Ripristina la formattazione originale sul nuovo testo
                If proprietàFormattazione IsNot Nothing Then
                    nuovoRun.Append(proprietàFormattazione)
                End If

                ' Assegna il testo finale modificato
                nuovoRun.Append(New Text(testoUnito))

                ' Aggiunge il nuovo blocco al paragrafo
                p.Append(nuovoRun)
            End If
        Next
    End Sub

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
    Function Return_descrizione_codice(cod As String) As String

    End Function
    Public Function Carica_dati_codice_elcar(cod As String, Optional mostragrafico As Boolean = False) As Integer
        'dalla tabella giornaliero del database del ragazzo, Carica tutti i record del codice richiesto leggendo tutti i record presenti che rientrano nell'intervallo di tempo indicato
        ' se intervallo =0 allora utilizza intervallo di default letto dal file ini
        'i valori dell'inrervallo possono essere: 15 gg, 1 mese, 3 mesi, 6 mesi, 1 anno,0  tutto

        Dim dt As DataTable = Class_GIORNALIERO.OttieniRecordPerCodice(cod)
        Do While dt.Rows.Count = 0
            Dim m = "nessun Codice trovato:" + vbCrLf
            Return 0
        Loop
        ' Ciclo che attraversa ogni riga della tabella
        Dim J = 0, i = -1

        Gbl_Graph.Cod_ELCAR = cod
        Gbl_Graph.Cod_Testo = dt.Rows(0)("testo")


        Dim xx = ""

        For Each riga As DataRow In dt.Rows
            ' 1. Lettura del campo codice
            cod = riga("Codice").ToString()
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

    End Function



    Public Sub Trend_1()
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
        ReDim Preserve Gbl_Percentuale(IP)

        Call trend_2(IP)

        Exit Sub



    End Sub
    Public Sub trend_2(ip)
        '---------------------------------------------
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
        '-- controllo il se il trend è positivo --
        '-----------------------------------------
        Dim trend_pos = 0
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
                trend_pos = 0
            End If
        Next

        If trend_pos >= 4 Then
            Gbl_Graph.Trend = "POS4"
            Exit Sub
        End If

        '-----------------------------------------
        '-- controllo il se il trend è negativo --
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

        Gbl_Graph.Trend = "UNDEF"

    End Sub
    Function Calcola_Percentuale(POS, Optional MAX = 20) As Integer
        Dim i = POS * 100 / 20
        Return i
    End Function

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
End Module
