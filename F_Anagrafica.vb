Public Class F_Anagrafica
    Dim Scheda_bimbo As New Class_ANAGRAFICA
    Private SolaLettura As Boolean = True
    Private Sub F_Anagrafica_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Carica_elenco_bambini(True)
        Call EDIT(False)

    End Sub
    Sub Carica_elenco_bambini(Optional soloattivi = False)
        Dim risultati As List(Of Class_ANAGRAFICA) = Class_ANAGRAFICA.CercaPerCognome("", soloattivi)

        ' Mostra il numero di record trovati
        Gbl_miaLista.Clear()
        ' Ciclo per leggere i dati trovati
        Dim id As Integer, n As String
        For Each persona In risultati
            n = persona.Cognome & " " & persona.Nome
            n = persona.Cognome & " " & persona.Nome
            Dim elemento As New Class_ElementoCombo(persona.ID, n, persona.DBname)
            Gbl_miaLista.Add(elemento)
        Next
        ComboBox1.PopolaLeggero(Gbl_miaLista)

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim elemento As Class_ElementoCombo
        elemento = CType(ComboBox1.SelectedItem, Class_ElementoCombo)
        If IsNothing(elemento) Then Exit Sub
        Dim id = CInt(elemento.ID)

        Scheda_bimbo = Class_ANAGRAFICA.Load(id)
        Call mostra_scheda_bimbo

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
            TextBox5.ForeColor = TextBox4.ForeColor
        Else
            PictureBox1.Image = ImageList1.Images(2)
            TextBox5.ForeColor = Color.Red
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


    Function EDIT(Stato As Boolean)
        TextBox1.ReadOnly = Not Stato
        TextBox2.ReadOnly = Not Stato
        TextBox3.ReadOnly = Not Stato
        TextBox4.ReadOnly = Not Stato
        TextBox5.ReadOnly = Not Stato
        ComboBox1.Enabled = Not Stato
        CheckBox2.Enabled = Stato
        Button3.Visible = Stato
        Button4.Visible = Stato
        Button2.Visible = Not Stato
    End Function


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Hide()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Call EDIT(True)
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Call EDIT(False)

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

    End Sub
End Class