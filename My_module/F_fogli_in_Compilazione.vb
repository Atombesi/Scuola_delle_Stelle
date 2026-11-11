Imports OpenTK.Graphics.ES20

Public Class F_fogli_in_Compilazione
    Dim elenco_stato_compilazione As New Struct_Stato_compilazione

    Sub main(Optional tab = "DEVICE")
        Carica_dgv2()
        Carica_dgv1()
        mostra_stato_server()
        If tab = "DEVICE" Then
            TabControl1.SelectedIndex = 1
        Else
            TabControl1.SelectedIndex = 0
        End If

    End Sub
    Private Sub GroupBox2_Enter(sender As Object, e As EventArgs) Handles GroupBox2.Enter

    End Sub

    Private Sub F_fogli_in_Compilazione_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GroupBox3.Left = 452 : GroupBox3.Top = 5
        mostra_stato_server()
        Label21.Tag = Now
        Label21.Text = "       " + Now.ToString("dddd d MMMM yyyy")

        'Carica elenco device registrati 
        Panel1.Top = Panel2.Top
        Panel1.Left = Panel2.Left
        Panel2.Visible = True
        Panel1.Visible = False

        Carica_dgv2()
        Carica_dgv1()
        Call Mostra_riassunto_GIORNALIERO_compilazione(Label21.Tag)

    End Sub
    Sub mostra_stato_server()
        If Gbl_server_connected Then
            Label4.ImageIndex = 2
            Label4.Text = "        ON"
        Else
            Label4.ImageIndex = 3
            Label4.Text = "        OFF"
        End If
        Label2.Text = Gbl_IP_SERVER
        Label17.Text = ini.Numero_fogli_compilati.ToString


    End Sub

    Function Server_on_off() As Boolean
        Dim MioServer = New Class_WebServerTablet()
        If Gbl_server_connected = True Then
            ' Devo fermare il server 
            'MioServer.StopServer()
            Gbl_server_connected = False
            Label4.ImageIndex = 3
            Label4.Text = "        OFF"
            F_main.CheckBox4.Checked = False
        Else
            ' Devo Avviare il server 
            'MioServer.StartServer()
            Gbl_server_connected = True
            Label4.ImageIndex = 2
            Label4.Text = "        ON"
            F_main.CheckBox4.Checked = True
        End If
        Label4.BackColor = F_main.CheckBox4.BackColor
        Label4.Refresh()

    End Function

    Sub Carica_dgv1()
        Gbl_Elenco_Device = Class_DEVICE.LoadAll
        dgv1.Rows.Clear()
        Dim img_index = 0, j = 0
        For Each voce In Gbl_Elenco_Device
            Dim stato = "In attesa" : img_index = 5
            If voce.ENABLED = 1 Or voce.ENABLED = -1 Then
                stato = "Abilitato" : img_index = 6
            End If
            Dim r = dgv1.Rows.Count - 1
            dgv1.Rows.Add(ImageList1.Images(img_index), voce.ID, voce.NOME, stato, voce.MAC, voce.ENABLED, r)
        Next

    End Sub

    Sub Carica_dgv2()
        Call Todolist_Azzera_e_Ricrea()
        Gbl_VociDiTodolist = Class_TODOLIST.LoadAll()
        Dgv2.Rows.Clear()
        Dim img_index = 0, j = 0
        For Each voce In Gbl_VociDiTodolist
            Dim foglio = "(" + voce.ID_FOGLIO.ToString + ") del: " + voce.DATA_FOGLIO
            Dim stato = "Da compilare" : img_index = 1
            If voce.COMPILATO = 0 And voce.CLIENT_ID > 0 Then
                stato = "In Compilazione" : img_index = 5
            End If
            If voce.COMPILATO = 1 Then
                stato = "Compilato" : img_index = 6
            End If
            Dim r = Dgv2.Rows.Count - 1
            Dgv2.Rows.Add(ImageList1.Images(img_index), voce.CLIENT_NOME, voce.BAMBINO, foglio, stato, voce.CLIENT_DATAINVIO, voce.ID, voce.ID_FOGLIO, voce.ID_BAMBINO, voce.CLIENT_ID, r)
        Next
        mostra_stato_server()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Carica_dgv2()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub

    Private Sub Button1_MouseDown(sender As Object, e As MouseEventArgs) Handles Button1.MouseDown
        GroupBox3.Visible = True
    End Sub

    Private Sub Button1_MouseUp(sender As Object, e As MouseEventArgs) Handles Button1.MouseUp
        GroupBox3.Visible = False
    End Sub

    Private Sub Dgv2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv2.CellContentClick
        Mostra_dati_griglia()

    End Sub
    Function Mostra_dati_griglia()
        Dim r = Dgv2.CurrentRow.Cells(10).Value
        Button3.Tag = r
        Label10.Text = Gbl_VociDiTodolist(r).CLIENT_NOME
        Label11.Text = Gbl_VociDiTodolist(r).CLIENT_DATAINVIO
        Label13.Text = Gbl_VociDiTodolist(r).BAMBINO
        Label15.Text = "[" + Gbl_VociDiTodolist(r).ID_FOGLIO.ToString + "] " + Gbl_VociDiTodolist(r).DATA_FOGLIO
        Button3.Enabled = False
        Button3.Text = "      Libera"
        If Gbl_VociDiTodolist(r).COMPILATO = 1 Then Button3.Text = "      Importa"
        Button3.Refresh()
        If Gbl_VociDiTodolist(r).CLIENT_ID > 0 Then Button3.Enabled = True
        mostra_stato_server()

    End Function
    Private Sub TabPage1_Click(sender As Object, e As EventArgs) Handles TabPage1.Click

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim m = ""
        Dim r = Val(Button3.Tag)
        If Trim(Button3.Text) <> "Libera" Then Exit Sub
        m = "Vuoi annullare l'operazione in corso e rendere nuovamente disponibile a tutti il foglio per la compilazione?" + vbCrLf
        m = m + "Bambino:" + Gbl_VociDiTodolist(r).BAMBINO + " (" + Gbl_VociDiTodolist(r).DATA_FOGLIO + ")" + vbCrLf
        m = m + "Device: " + Gbl_VociDiTodolist(r).CLIENT_NOME + " (" + Gbl_VociDiTodolist(r).CLIENT_DATAINVIO + ")" + vbCrLf

        If MsgBox(m, vbQuestion + vbYesNo + vbDefaultButton2, "Annullamento compilazione client") <> vbYes Then Exit Sub
        Dim id = Gbl_VociDiTodolist(r).ID
        Dim thistodo = Class_TODOLIST.Load_By_ID(id)
        thistodo.CLIENT_DATAINVIO = ""
        thistodo.CLIENT_DATARICONSEGNA = ""
        thistodo.CLIENT_ID = 0
        thistodo.CLIENT_NOME = ""
        thistodo.COMPILATO = 0
        thistodo.Save()
        m = "Annullamento compilazione client (" + thistodo.CLIENT_NOME + ") " + Gbl_VociDiTodolist(r).BAMBINO + "-" + Gbl_VociDiTodolist(r).ID_FOGLIO.ToString
        If Class_DEVICE.Delete(id) Then
            Call MsgBox(m, vbInformation + vbOKOnly)
            APPEND_LOG(m, ini.File_Log, True, "1")
        End If
        Carica_dgv2()
        F_main.Mostra_stato_rete_e_compilazione()
        mostra_stato_server()
    End Sub

    Private Sub Dgv2_MouseClick(sender As Object, e As MouseEventArgs) Handles Dgv2.MouseClick
        Mostra_dati_griglia()
    End Sub


    Private Sub dgv1_MouseClick(sender As Object, e As MouseEventArgs) Handles dgv1.MouseClick
        Dim r = dgv1.CurrentRow.Cells(6).Value
        Label5.Tag = r
        TextBox1.Text = Gbl_Elenco_Device(r).NOME
        Button7.Enabled = True  ' elimina 
        If Gbl_Elenco_Device(r).ENABLED = 1 Then
            Button5.Enabled = False ' Abilita
            Button2.Enabled = True  ' rinomina
            TextBox1.ReadOnly = True
            Exit Sub
        Else
            Button5.Enabled = True  ' Abilita
            Button2.Enabled = False ' rinomina
            TextBox1.ReadOnly = False
            TextBox1.Focus()
            Exit Sub
        End If


    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Panel2.Visible = False
        Panel1.Visible = True
        TextBox1.ReadOnly = False
        Button9.Tag = "RENAME"
        TextBox1.Focus()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Panel1.Visible = False
        Panel2.Visible = True

    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        ' devo salvare 
        Dim R = Val(Label5.Tag)
        Dim id = Gbl_Elenco_Device(R).ID

        Dim mydev = Class_DEVICE.Load_by_ID(id)
        Dim x = ""
        Try
            x = mydev.NOME
        Catch ex As Exception
            Call MsgBox("Dispositivo NON trovato")
            Exit Sub
        End Try

        mydev.NOME = Trim(TextBox1.Text)
        If Button9.Tag = "ABILITA" Then
            mydev.ENABLED = 1
            mydev.STATO = "OK"
        End If
        mydev.Save()
        Button9.Tag = ""
        Panel1.Visible = False
        Panel2.Visible = True
        Carica_dgv1()

    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Button9.Tag = "ABILITA"
        Panel2.Visible = False
        Panel1.Visible = True
        TextBox1.ReadOnly = False

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim m = ""
        Dim R = Val(Label5.Tag)
        Dim id = Gbl_Elenco_Device(R).ID
        Dim mydev = Class_DEVICE.Load_by_ID(id)

        m = "Vuoi Rimuovere il dispositivo da quelli Abilitati?" + vbCrLf + vbCrLf
        m = m + "Device: " + mydev.NOME

        If MsgBox(m, vbQuestion + vbYesNo + vbDefaultButton2, "Rimozione dispostivo abilitato") <> vbYes Then Exit Sub
        m = "Device: " + mydev.NOME + "Rimosso!"
        If Class_DEVICE.Delete(id) Then
            Call MsgBox(m, vbInformation + vbOKOnly)
            m = "Rimozione device Abilitato (" + mydev.NOME + ")"
            APPEND_LOG(m, ini.File_Log, True, "1")
        End If
        Carica_dgv1()

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Dim m = ""
        If Gbl_server_connected Then
            m = "Vuoi Disconnettere il server?"
        Else
            m = "Vuoi Attivare il server?"
        End If
        If MsgBox(m, vbYesNo + vbQuestion) <> vbYes Then Exit Sub
        Server_on_off()


    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Hide()

    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs)
        Mostra_riassunto_GIORNALIERO_compilazione(Label21.Tag)

    End Sub

    Sub Mostra_riassunto_GIORNALIERO_compilazione(data)
        'Mostral lo stato di compilazione degli ultiimi fogli 
        '1) trovo tutti i fogli della data 
        Dim voce As New Struct_Stato_compilazione
        Dim elenco_vocistatocompilazione As New List(Of Struct_Stato_compilazione)
        For Each bambino In Gbl_Elenco_bambini
            voce.Bambino = bambino.NomeCompleto
            voce.DB_Bambino = bambino.DBname
            voce.ID_Bambino = bambino.ID
            Dim IDF = Class_GIORNALIERO.Trova_ultimo_IDFOGLIO(False, voce.DB_Bambino)
            Dim FOGLIO = Class_GIORNALIERO.Load(IDF, voce.DB_Bambino)
            voce.Data = FOGLIO.DATA
            voce.Compilato = FOGLIO.COMPILATO
            elenco_vocistatocompilazione.Add(voce)
        Next
        '2) integro informazioni con informazioni della todolist
        For Each el_todolist In Gbl_VociDiTodolist
            For Each voceb In elenco_vocistatocompilazione
                If el_todolist.ID_BAMBINO <> voceb.ID_Bambino Then Continue For
                If voceb.Compilato Then Continue For
                voceb.TODOLIS = el_todolist.CLIENT_NOME + " (" + el_todolist.CLIENT_DATAINVIO + ")"
            Next
        Next

        DGV3.Rows.Clear()
        Dim img_index = 0, stato = ""
        For Each voceb In elenco_vocistatocompilazione
            If voceb.Compilato Then
                img_index = 6 : stato = "Compilato"
            Else
                img_index = 11 : stato = "NON Compilato"
            End If
            DGV3.Rows.Add(ImageList1.Images(img_index), voceb.Data, voceb.Bambino, voceb.id_foglio, stato, voceb.TODOLIS)
        Next


    End Sub

    Private Sub TabPage3_Click(sender As Object, e As EventArgs) Handles TabPage3.Click

    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Call Mostra_riassunto_GIORNALIERO_compilazione(Label21.Tag)

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub TextBox1_GotFocus(sender As Object, e As EventArgs) Handles TextBox1.GotFocus
        TextBox1.BackColor = MyColor.LightYellow
    End Sub

    Private Sub TextBox1_LostFocus(sender As Object, e As EventArgs) Handles TextBox1.LostFocus
        TextBox1.BackColor = SystemColors.Control
    End Sub
End Class