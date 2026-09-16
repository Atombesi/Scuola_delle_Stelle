Public Class F_Elenco_titoli
    Function Main(Optional titolo = "") As String
        ComboBox1.Items.Clear()
        Dim j0 = -1
        For j = 0 To Gbl_Elenco_titoli_usati.Count - 1
            ComboBox1.Items.Add(Gbl_Elenco_titoli_usati(j).Titolo)
            If Gbl_Elenco_titoli_usati(j).Titolo = titolo Then j0 = j
        Next
        Button1.Enabled = False
        Me.Tag = -1
        If j0 >= 0 Then
            ComboBox1.SelectedIndex = j0
            Button1.Enabled = True
            Label3.Text = Gbl_Elenco_titoli_usati(j0).Titolo
            Me.Tag = Label3.Text
        End If
        Me.ShowDialog()
        Return Me.Tag
    End Function
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Tag = ""
        Me.Hide()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        Dim t = "Nuovo titolo"
        Dim m = "Inserisci il nuovo titolo"
        Dim x = InputBox(m, t, "")
        If x <> "" Then
            m = "Confermi l'inserimento del titolo:" + vbCrLf + m
            If MsgBox(m, vbYesNo + vbInformation, "Nuovo titolo DirectCast esercizio") = vbYes Then
                ComboBox1.Items.Add(x)
            End If
        End If
    End Sub

    Private Sub F_Elenco_titoli_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Tag = Label3.Text
        Me.Hide()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Label3.Text = ComboBox1.Text
        Button1.Enabled = True

    End Sub
End Class