Imports System.Windows
Imports DocumentFormat.OpenXml.Drawing
Imports DocumentFormat.OpenXml.Office2010.PowerPoint
Imports DocumentFormat.OpenXml.Spreadsheet
Imports Microsoft.Data.Sqlite

Public Class F_main
    Public my_ELCAR As New Class_GIORNALIERO
    Private isCaricato As Boolean = False
    Private Noneseguire As Boolean = False

    Private Sub F_main_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Width = 145
        Class_WebServerTablet.FormPrincipaleRiferimento = Me
        P_liste.Visible = False
        P_Setting.Visible = False
        P_liste.Left = P_schede.Left
        P_liste.Top = P_schede.Top
        P_Setting.Top = P_schede.Top
        P_Setting.Left = P_schede.Left

        Call Read_ini()
        APPEND_LOG("Avvio programma", ini.File_Log, True, "I")
        Dim m = "Printer:" + ini.Stampante_defaut
        TP1.SetToolTip(Button11, m)
        Carica_elenco_CODICI_ELCAR()
        Gbl_Elenco_bambini = Carica_bambini()
        'Carico elenco comportamenti
        If Gbl_Elenco_CP Is Nothing Then
            Gbl_Elenco_CP = Carica_Comportamenti()
        End If

        isCaricato = True
        Gbl_MioServer = New Class_WebServerTablet
        Server_OnOff(True)
        Mostra_stato_rete_e_compilazione()
        Timer1.Enabled = True
        ' "✅❌"



    End Sub




    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        F_Seleziona_Categoria.Show()
    End Sub

    Sub test_db()
        Dim tabelle As List(Of String) = Class_ANAGRAFICA.ElencaTabelleDisponibili()

        If tabelle.Count = 0 Then
            MessageBox.Show("ATTENZIONE: Il codice vede 0 tabelle! " &
                    "Significa che si sta connettendo a un file di database VUOTO. " &
                    "Controlla il percorso del file nella stringa di connessione.")
        Else
            Dim messaggio As String = "Tabelle trovate nel database dal codice:" & Environment.NewLine
            For Each tabella In tabelle
                messaggio &= "- " & tabella & Environment.NewLine
            Next
            MessageBox.Show(messaggio)
        End If
    End Sub


    Private Sub PctNotifica_Click(sender As Object, e As EventArgs) Handles PctNotifica.Click
        ' Se per qualche motivo la lista si è svuotata, nascondi l'icona ed esci
        If DispositiviInAttesa.Count = 0 Then
            PctNotifica.Visible = False
            PctNotifica.Refresh()
            Return
        End If

        ' Prendi il primo dispositivo della coda
        Dim prossimoDeviceId = DispositiviInAttesa(0)


        Dim frm As New F_Client
        frm.IdDispositivoDaAbilitare = prossimoDeviceId
        frm.T_nomeclient.Text = "Richiesta di connessione in sospeso..."

        ' Mostra la maschera
        frm.ShowDialog()

        ' Una volta chiusa la maschera, rimuoviamo il dispositivo dalla coda 
        ' (sia che sia stato approvato, sia che sia stato rifiutato/rimandato)
        DispositiviInAttesa.RemoveAt(0)

        ' Se non ci sono più dispositivi in attesa, nascondi l'icona
        If DispositiviInAttesa.Count = 0 Then
            PctNotifica.Visible = False
        Else
            PctNotifica.Visible = True
            ' Se ce ne sono altri, aggiorna l'interfaccia (es. se mostri un contatore)
        End If
        PctNotifica.Refresh()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Gbl_Elenco_bambini = Carica_bambini()
        F_Compilazione.Show()
    End Sub

    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click
        F_Anagrafica.ShowDialog()

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Try
            F_MyGraph.Hide()
        Catch ex As Exception

        End Try

        Try
            F_Anagrafica.Hide()
        Catch ex As Exception
        End Try

        Try
            F_Foglio_esistente.Hide()
        Catch ex As Exception
        End Try

        Try
            F_Seleziona_Categoria.Hide()
        Catch ex As Exception

        End Try


        End
    End Sub

    Private Sub DatabasePrincipaleToolStripMenuItem_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button4_Click_1(sender As Object, e As EventArgs) Handles Button4.Click
        If Gbl_Elenco_bambini Is Nothing Then
            Gbl_Elenco_bambini = Carica_bambini()
        End If

        Dim oggi = Now.ToShortDateString
        F_Elenco_da_Stampare.Main(oggi)


    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        F_MyInfo.ShowDialog()

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        ' Inverte lo stato (se era True diventa False, e viceversa)
        If CheckBox1.Checked Then
            IsDarkMode = True
        Else
            IsDarkMode = False
        End If

        ' Aggiorna istantaneamente la Form corrente
        ApplicaTema(Me)

        ' Se anche l'altra Form è aperta, aggiorna anche lei (sostituisci Form2 con il nome reale)
        If F_Seleziona_Categoria.Visible Then
            ApplicaTema(F_Seleziona_Categoria)
        End If
        If F_Foglio_esistente.Visible Then
            ApplicaTema(F_Foglio_esistente)
        End If
        If F_Elenco_da_Stampare.Visible Then
            ApplicaTema(F_Elenco_da_Stampare)
        End If

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs)

        F_Elenco_Comportamenti.Main("RT", True)
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked Then
            P_liste.Visible = True
            P_schede.Visible = False
            P_Setting.Visible = False
            CheckBox2.BackColor = MyColor.MediumAquamarine
            If CheckBox3.Checked Then CheckBox3.Checked = False
        Else
            P_liste.Visible = False
            P_schede.Visible = True
            CheckBox2.BackColor = Button2.BackColor
        End If
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked Then
            P_Setting.Visible = True
            P_schede.Visible = False
            CheckBox3.BackColor = MyColor.LemonChiffon
            If CheckBox2.Checked Then CheckBox2.Checked = False
        Else
            P_Setting.Visible = False
            P_schede.Visible = True
            CheckBox3.BackColor = Button2.BackColor
        End If

    End Sub
    Function Server_OnOff(Optional da_attivare As Boolean = True) As Boolean
        Dim stato As Boolean
        'comando da dare se non si hanno i privilegi per connettersi
        'netsh http add urlacl url=http://+:8080/ user=Everyone

        If da_attivare And Not Gbl_server_connected Then
            'Gbl_MioServer = New Class_WebServerTablet
            stato = Gbl_MioServer.StartServer
            Gbl_server_connected = stato
        End If

        If Not da_attivare And Gbl_server_connected Then
            'Gbl_MioServer = New Class_WebServerTablet
            Gbl_MioServer.StopServer()
            stato = False
            Gbl_server_connected = stato
        End If


        'Accendo il server 

        If Gbl_server_connected Then
            Gbl_IP_SERVER = Class_WebServerTablet.OttieniIPAscoltoReale
            CheckBox4.BackColor = MyColor.PaleGreen
            CheckBox4.ImageIndex = 1
            TP1.SetToolTip(CheckBox4, "Attivo IP: " + Gbl_IP_SERVER)

        Else
            CheckBox4.BackColor = Button2.BackColor
            CheckBox4.ImageIndex = 2
            TP1.SetToolTip(CheckBox4, "Spento IP: " + Gbl_IP_SERVER)
        End If
        CheckBox4.Refresh()

    End Function

    Private Sub CheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox4.CheckedChanged
        If CheckBox4.Checked Then
            Server_OnOff(True)
        Else
            Server_OnOff(False)
        End If
        Exit Sub

        If CheckBox4.Checked Then
            CheckBox4.ImageIndex = 1
            Gbl_MioServer = New Class_WebServerTablet
            If Gbl_MioServer.StartServer Then
                CheckBox4.BackColor = MyColor.PaleGreen
                Gbl_server_connected = True
                Dim ip As String
                Gbl_IP_SERVER = Class_WebServerTablet.OttieniIPAscoltoReale
                ip = "Connesso IP: " + Gbl_IP_SERVER
                TP1.SetToolTip(CheckBox4, ip)
            Else
                CheckBox4.BackColor = MyColor.Salmon
            End If
            CheckBox4.Refresh()
        Else
            CheckBox4.ImageIndex = 2
            If Gbl_MioServer IsNot Nothing Then
                Gbl_server_connected = False
                Gbl_MioServer.StopServer()
                CheckBox4.BackColor = Button2.BackColor
                CheckBox4.Refresh()
            End If
        End If
        CheckBox4.Refresh()



    End Sub
    Sub Mostra_stato_rete_e_compilazione()
        Dim n = Class_TODOLIST.fogli_in_compilazione
        If n > 0 Then
            Label5.BackColor = MyColor.Yellow
            Dim m = n.ToString + "Fogli in compilazione"
            TP1.SetToolTip(Label5, "Nessun Foglio in compilazione")
        Else
            Label5.BackColor = MyColor.Transparent
            TP1.SetToolTip(Label5, "Nessun Foglio in compilazione")
        End If
        'verifico se ci sono dei fogli in compilazione
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        F_Grafici.ShowDialog()
    End Sub

    Private Sub Button5_Click_1(sender As Object, e As EventArgs) Handles Button5.Click
        F_Foglio_esistente.Main(-1)
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        Dim fileini = Gbl_app_path + "\Scuola_delle_Stelle.ini"
        If Not IO.File.Exists(fileini) Then
            Dim m = "File non trovato!" + vbCrLf + fileini
            Call MsgBox(m, vbCritical + vbOKOnly)
            Exit Sub
        End If
        Apri_documento(fileini)
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        If Not IO.File.Exists(ini.File_Log) Then
            Dim m = "File log non trovato!" + vbCrLf + ini.File_Log
            MsgBox(m, vbInformation + vbOKOnly)
            Exit Sub
        End If
        Apri_documento(ini.File_Log)

    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        F_Elenco_Comportamenti.Main("CP", True)
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        F_Elenco_Comportamenti.Main("RT", True)

    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        F_Gestione_Codici_ELCAR.ShowDialog()
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs)
        F_fogli_in_Compilazione.ShowDialog()
    End Sub

    Private Sub CheckBox5_CheckedChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        F_fogli_in_Compilazione.ShowDialog()

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Mostra_stato_rete_e_compilazione()
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Dim impostazioniAvanzate As New System.Drawing.Printing.PrinterSettings()
        PrintDialog1.PrinterSettings = impostazioniAvanzate
        ' 2. Mostriamo la finestra nativa a schermo e attendiamo la scelta dell'utente
        If PrintDialog1.ShowDialog() = DialogResult.OK Then
            ' 3. Catturiamo il nome esatto della stampante che l'utente ha selezionato
            Dim x As String = PrintDialog1.PrinterSettings.PrinterName
            Dim m = "Stampante selezionata selezionata:" + vbCrLf
            m = m + x + vbCrLf + vbCrLf
            m = m + "Vuoi salvare questa stampante cone quella di Default?"
            If MsgBox(m, vbQuestion + vbYesNo + vbDefaultButton1, "Selezione stampante default") <> vbYes Then Exit Sub
            Dim fileini = Gbl_app_path + "\Scuola_delle_Stelle.ini"
            WriteINIString(fileini, "GENERAL", "Stampante Default", x)
            ini.Stampante_defaut = x
            m = "Modifica stampante di default: " + ini.Stampante_defaut
            APPEND_LOG(m, ini.File_Log, True, "I")
        End If


    End Sub

    Private Sub TransparentskControl1_PaintSurface(sender As Object, e As SkiaSharp.Views.Desktop.SKPaintSurfaceEventArgs) Handles TransparentskControl1.PaintSurface

    End Sub
End Class
