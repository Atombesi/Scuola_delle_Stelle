Public Class F_checklist
    Dim elenco_unchecked As List(Of Elenco_checklist)
    Dim elenco_checked As List(Of Elenco_checklist)
    Dim elenco_returned As List(Of Elenco_checklist)

    Private noneseguire
    Private MAX_VOCI As Integer = 0
    Function main(elencoLista As List(Of Elenco_checklist), Optional max_checked As Integer = 0) As List(Of Elenco_checklist)
        ListBox1.Items.Clear()
        ListBox2.Items.Clear()
        If elencoLista.Count = 0 Then Return Nothing
        Label2.Tag = max_checked
        MAX_VOCI = max_checked
        For Each elemento In elencoLista
            Dim statoCheck As CheckState = If(elemento.Stato, CheckState.Checked, CheckState.Unchecked)
            ' Passi l'oggetto 'elemento' intero, non solo la stringa!
            If statoCheck Then
                ListBox1.Items.Add(elemento)
            Else
                ListBox2.Items.Add(elemento)
            End If
        Next
        Button1.Enabled = False
        Me.ShowDialog()
        If Me.Tag = "OK" Then
            prepara_elenco_returned()
            Return elenco_returned
        Else
            Return Nothing
        End If
    End Function
    Sub mostra()
        Label2.Text = ListBox1.Items.Count.ToString + "\" + MAX_VOCI.ToString
        Label5.Text = ListBox2.Items.Count.ToString
        If ListBox1.Items.Count > 0 Then
            Button1.Enabled = True
        Else
            Button1.Enabled = False
        End If
    End Sub
    Sub prepara_elenco_returned()
        Dim v As New Elenco_checklist
        elenco_returned = New List(Of Elenco_checklist)()
        For Each voce In ListBox1.Items
            elenco_returned.Add(voce)
        Next
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Tag = ""
        Me.Hide()

    End Sub

    Private Sub ListBox2_DoubleClick(sender As Object, e As EventArgs) Handles ListBox2.DoubleClick
        If noneseguire Then Exit Sub
        noneseguire = True
        Dim voce = ListBox2.SelectedItem
        If ListBox1.Items.Count >= MAX_VOCI Then
            Call MsgBox("Il numero massimo di voci selezionabili è stato raggiunto", vbInformation + vbOKOnly)
            Exit Sub
        End If
        ListBox1.Items.Add(voce)
        ListBox2.Items.Remove(voce)
        Call mostra()
        noneseguire = False

    End Sub

    Private Sub ListBox1_DoubleClick(sender As Object, e As EventArgs) Handles ListBox1.DoubleClick
        If noneseguire Then Exit Sub
        noneseguire = True
        Dim voce = ListBox1.SelectedItem
        ListBox2.Items.Add(voce)
        ListBox1.Items.Remove(voce)
        Call mostra()

        noneseguire = False

    End Sub

    Private Sub F_checklist_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Tag = "OK"
        Hide

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        Tag = "AZZERA"
        Hide()

    End Sub
End Class