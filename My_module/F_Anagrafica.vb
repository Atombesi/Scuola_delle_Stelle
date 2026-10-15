Imports DocumentFormat.OpenXml.Wordprocessing
Imports Microsoft.IdentityModel.Tokens


Public Class F_Anagrafica
    Dim Scheda_bimbo As New Class_ANAGRAFICA
    Private SolaLettura As Boolean = True
    Private Sub F_Anagrafica_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Carica_elenco_bambini(True)
        Call EDIT(False)

    End Sub
    Sub Carica_elenco_bambini(Optional soloattivi = False)
        Dim risultati As List(Of Class_ANAGRAFICA) = Class_ANAGRAFICA.CercaPerCognome("", soloattivi)
        Gbl_Elenco_bambini = risultati
        ' Mostra il numero di record trovati
        Gbl_miaLista_Anagrafiche.Clear()
        ' Ciclo per leggere i dati trovati
        Dim id As Integer, n As String
        For Each persona In risultati
            n = persona.Cognome & " " & persona.Nome
            Dim elemento As New Class_ElementoCombo(persona.ID, n, persona.DBname)
            Gbl_miaLista_Anagrafiche.Add(elemento)
        Next
        ComboBox1.PopolaLeggero(Gbl_miaLista_Anagrafiche)

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim elemento As Class_ElementoCombo
        elemento = CType(ComboBox1.SelectedItem, Class_ElementoCombo)
        If IsNothing(elemento) Then Exit Sub
        Dim id = CInt(elemento.ID)

        Scheda_bimbo = Class_ANAGRAFICA.Load(id)
        Call Mostra_scheda_bimbo()

    End Sub

    Sub Mostra_scheda_bimbo()
        TextBox1.Text = Scheda_bimbo.Cognome
        TextBox2.Text = Scheda_bimbo.Nome
        TextBox3.Text = Scheda_bimbo.Nik
        TextBox4.Text = Scheda_bimbo.DAta_nascita
        TextBox5.Text = Scheda_bimbo.DBname
        Dim db = dir_bar(ini.DB_Bambini_path) + Scheda_bimbo.DBname + ".db"
        TextBox5.Tag = db
        If IO.File.Exists(db) Then
            PictureBox1.Image = ImageList1.Images(4)
            PictureBox1.Tag = "OK"
            TextBox5.ForeColor = TextBox4.ForeColor
        Else
            PictureBox1.Tag = ""
            PictureBox1.Image = ImageList1.Images(2)
            TextBox5.ForeColor = MyColor.Red
        End If

        If Scheda_bimbo.Assente Then
            CheckBox3.Checked = False
        Else
            CheckBox3.Checked = True
        End If

        If Scheda_bimbo.Attivo Then
            CheckBox2.Checked = True
        Else
            CheckBox2.Checked = False
        End If

        PictureBox1.Refresh()
        TextBox5.Refresh()


    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

        If CheckBox1.Checked Then
            Carica_elenco_bambini(True)
        Else
            Carica_elenco_bambini(False)

        End If


    End Sub


    Function EDIT(editing As Boolean)

        Label3.Visible = editing

        TextBox1.ReadOnly = Not editing
        TextBox2.ReadOnly = Not editing
        TextBox3.ReadOnly = Not editing
        TextBox4.ReadOnly = Not editing
        TextBox5.ReadOnly = Not editing
        ComboBox1.Enabled = Not editing
        CheckBox2.Enabled = editing
        CheckBox3.Enabled = editing
        Button3.Visible = editing
        Button4.Visible = editing
        Button2.Visible = Not editing

        ComboBox1.Enabled = Not editing
        Button5.Enabled = Not editing
        TextBox1.Focus()

    End Function


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Hide()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Label3.Text = "Modifica: " + Scheda_bimbo.Cognome + " " + Scheda_bimbo.Nome
        Label3.BackColor = MyColor.LightGreen
        EDIT(True)
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Call EDIT(False)
    End Sub
    Sub SALVA()
        Scheda_bimbo.Cognome = TextBox1.Text
        Scheda_bimbo.Nome = TextBox2.Text
        Scheda_bimbo.Nik = TextBox3.Text
        Scheda_bimbo.DAta_nascita = TextBox4.Text
        Scheda_bimbo.DBname = TextBox5.Text
        Scheda_bimbo.Data_Creazione = Now.ToShortDateString
        Scheda_bimbo.Attivo = True
        If CheckBox3.Checked Then
            Scheda_bimbo.Assente = False
        Else
            Scheda_bimbo.Assente = True
        End If
        Scheda_bimbo.Save()


    End Sub
    Function Verifica() As Boolean
        Dim ok = True
        If TextBox1.Text = "" Then ok = False
        If TextBox2.Text = "" Then ok = False
        If TextBox4.Text = "" Then ok = False
        If TextBox5.Text = "" Then
            If TextBox1.Text <> "" And TextBox2.Text <> "" Then
                TextBox5.Text = TextBox1.Text + TextBox2.Text
            End If
        End If
        If TextBox5.Text = "" Then ok = False
        TextBox1.Text = TextBox1.Text.ToUpper
        TextBox2.Text = TextBox2.Text.ToUpper
        TextBox3.Text = TextBox3.Text.ToUpper
        TextBox5.Text = TextBox5.Text.ToUpper
        Button4.Enabled = ok
        Return ok
    End Function
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim m = "Vuoi salvare i dati?"
        If MsgBox(M, vbYesNo) <> vbYes Then Exit Sub
        Call SALVA()
        Call EDIT(False)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Label3.Text = "Inserimento Nuovo Bambino"
        Label3.BackColor = MyColor.Yellow
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        Scheda_bimbo = New Class_ANAGRAFICA()
        Call EDIT(True)
    End Sub


    Private Sub TextBox1_LostFocus(sender As Object, e As EventArgs) Handles TextBox1.LostFocus, TextBox2.LostFocus, TextBox3.LostFocus, TextBox4.LostFocus, TextBox5.LostFocus
        Call Verifica()

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Dim m = ""
        If PictureBox1.Tag = "OK" Then
            m = "Database gia esistente!"
            Call MsgBox(m, vbInformation + vbOKOnly)
            Exit Sub
        End If

        Dim DB_BAMBINO = Scheda_bimbo.DBname
        Dim filedb = Costruisci_filenameDB_bambino(DB_BAMBINO)
        m = "Problemi nella creazione del database"
        If Crea_DB_Bimbo(filedb) Then
            m = "database creato correttamente"
        End If
        Call MsgBox(m, vbInformation + vbOKOnly)
        Call Mostra_scheda_bimbo()
    End Sub
End Class