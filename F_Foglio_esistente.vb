Public Class F_Foglio_esistente
    Function Main(J As Integer, BAMBINO As String, Optional modo As String = "COMPILE", Optional Duplica As Boolean = False) As String
        Label6.Text = BAMBINO
        Label7.Tag = J
        Dim ID_foglio = CInt(Gbl_elenco_fogli_bambino(J).ID_Foglio)
        Gbl_VociDiUnFoglio = Gbl_Giornaliero.Carica_Foglio(ID_foglio)
        If Gbl_VociDiUnFoglio.Count = 0 Then Return ""


        If modo = "COMPILE" Then
            Label5.Text = "Foglio da compilare"
            Label5.BackColor = Color.LightGreen
            Button6.Enabled = False
        ElseIf modo = "LAST" Then
            Label5.Text = "Ultimo foglio Complilato"
            Label5.BackColor = Color.Yellow
            Button6.Enabled = Duplica
        ElseIf modo = "OLD" Then
            Label5.Text = "Fogli di Archivio"
            Label5.BackColor = Color.LightGray
            Button6.Enabled = False
        End If
        Button6.Enabled = True
        Label6.BackColor = Label5.BackColor
        Call Mostra_Foglio(J)
        Me.Show()


    End Function

    Sub Mostra_Foglio(j)
        Dim ID_foglio = CInt(Gbl_elenco_fogli_bambino(j).ID_Foglio)
        Gbl_VociDiUnFoglio = Gbl_Giornaliero.Carica_Foglio(ID_foglio)
        Dim n = Gbl_VociDiUnFoglio.Count
        If n = 0 Then
            Call MsgBox("Nessuna voce trovata")
            Exit Sub
        Else
        End If

        DGV1.Rows.Clear()
        For Each foglio In Gbl_VociDiUnFoglio
            DGV1.Rows.Add(foglio.Codice, foglio.Testo)
        Next
        DGV1.Refresh()


    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim j = CInt(Label7.Tag)
        F_Seleziona_Categoria.Carica_foglio(j)
        Me.Hide()
    End Sub

    Private Sub GroupBox4_Enter(sender As Object, e As EventArgs) Handles GroupBox4.Enter

    End Sub
End Class