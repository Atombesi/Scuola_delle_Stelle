Public Class F_Seleziona_Bambino
    Function Mostra() As Integer
        Me.Tag = -1
        Me.ShowDialog()
        If CInt(Me.Tag) < 0 Then Return -1
        Return CInt(Me.Tag)


    End Function
    Private Sub F_Seleziona_Bambino_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If ComboBox1.Items.Count = 0 Then
            Call Carica()
            ComboBox1.SelectedIndex = 0
        End If

    End Sub
    Public Function Carica()
        Dim soloattivi = CheckBox1.Checked
        ComboBox1.Items.Clear()
        Gbl_Elenco_bambini = Class_ANAGRAFICA.CercaPerCognome("", soloattivi)
        ' Mostra il numero di record trovati
        Gbl_miaLista.Clear()
        ' Ciclo per leggere i dati trovati
        Dim id As Integer, n As String
        For Each persona In Gbl_Elenco_bambini
            n = persona.Cognome & " " & persona.Nome
            ComboBox1.Items.Add(n)
        Next
        ComboBox1.SelectedIndex = 0
    End Function

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim j = ComboBox1.SelectedIndex
        Label1.Text = Gbl_Elenco_bambini(j).Cognome + " " + Gbl_Elenco_bambini(j).Nome
        Label1.Tag = j
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Tag = Label1.Tag
        Me.Hide()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        Carica()

    End Sub
End Class