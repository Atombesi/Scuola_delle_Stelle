Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Reflection.Emit
Imports System.Runtime.InteropServices
Imports Microsoft.Data.SqlClient
Imports Microsoft.Data.Sqlite
Imports QRCoder
Imports ZXing
Imports ZXing.Common
Imports ZXing.Windows.Compatibility
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
    End Structure
    Public ini As Struct_ini


    Public Gbl_anagrafica As New Class_ANAGRAFICA()
    Public Gbl_Elenco_bambini As List(Of Class_ANAGRAFICA)
    Public Gbl_elenco_fogli_bambino As List(Of Class_Elenco_fogli)
    Public Gbl_Giornaliero As New Class_GIORNALIERO
    Public Gbl_Class_Voci_Personali As New Class_ELCAR_VOCI_PERSONALI
    Public Gbl_Class_Voci_sub2 As New Class_ELCAR_VOCI_SUB2
    Public Gbl_Class_Voci_sub As New Class_ELCAR_VOCI_SUB
    Public Gbl_miaLista As New List(Of Class_ElementoCombo)()
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

    End Sub


    Public Function GeneraQRCode_Senzabordo(testo As String, Optional pixelPerModulo As Integer = 10, Optional Spessorebordo As Integer = 1) As Bitmap

        Using qrGenerator As New QRCodeGenerator()
            Using qrCodeData As QRCodeData = qrGenerator.CreateQrCode(testo, QRCodeGenerator.ECCLevel.Q)
                Using qrCode As New QRCode(qrCodeData)

                    ' 1. Genera il QR senza il bordo della libreria (False)
                    Using qrSenzaBordo As Bitmap = qrCode.GetGraphic(pixelPerModulo, Color.Black, Color.White, False)

                        ' 2. Scegli lo spessore del tuo bordo personalizzato (es. 6 pixel)

                        ' 3. Crea una nuova bitmap più grande per ospitare il bordo
                        Dim qrConBordo As New Bitmap(qrSenzaBordo.Width + (Spessorebordo * 2), qrSenzaBordo.Height + (Spessorebordo * 2))

                        ' 4. Disegna lo sfondo bianco e incolla il QR al centro
                        Using g As Graphics = Graphics.FromImage(qrConBordo)
                            g.Clear(Color.White) ' Colore del bordo
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

End Module
