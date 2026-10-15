Imports System.Windows
Imports Microsoft.Data.Sqlite

Public Class F_main
    Public my_ELCAR As New Class_GIORNALIERO
    Private isCaricato As Boolean = False
    Private Noneseguire As Boolean = False
    Private MioServer As Class_WebServerTablet

    Private Sub F_main_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        P_liste.Visible = False
        P_Setting.Visible = False
        P_liste.Left = P_schede.Left
        P_liste.Top = P_schede.Top
        P_Setting.Top = P_schede.Top
        P_Setting.Left = P_schede.Left

        Call Read_ini()
        Carica_elenco_CODICI_ELCAR()
        'Carico elenco comportamenti
        If Gbl_Elenco_CP Is Nothing Then
            Gbl_Elenco_CP = Carica_Comportamenti()
        End If

        isCaricato = True
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



    Private Sub Label2_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub NuovoArrivoToolStripMenuItem_Click(sender As Object, e As EventArgs)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Gbl_Elenco_bambini = Carica_bambini()
        F_Compilazione.ShowDialog()
        Dim N = 0
        Try
            N = Gbl_Elenco_bambini.Count
        Catch ex As Exception
            Dim risultati = Class_ANAGRAFICA.CercaPerCognome("", True)
            Gbl_Elenco_bambini = risultati

        End Try


        Dim XX = F_Foglio_esistente.Main(-1, True, False)
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
        Dim ip = Class_WebServerTablet.OttieniIPAscoltoReale
        Call MsgBox(ip)
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

    Private Sub CheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox4.CheckedChanged
        If CheckBox4.Checked Then
            CheckBox4.ImageIndex = 1
            MioServer = New Class_WebServerTablet()
            If MioServer.StartServer() Then
                CheckBox4.BackColor = MyColor.PaleGreen
                Dim ip As String
                ip = "IP: " + Class_WebServerTablet.OttieniIPAscoltoReale
                TP1.SetToolTip(CheckBox4, ip)
            Else
                CheckBox4.BackColor = MyColor.Salmon
            End If
        Else
            CheckBox4.ImageIndex = 2
            If MioServer IsNot Nothing Then
                MioServer.StopServer()
                CheckBox4.BackColor = Button2.BackColor
            End If
        End If
    End Sub
End Class
