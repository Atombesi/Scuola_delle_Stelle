Public Class F_Seleziona_CP
    Dim sel As New List(Of Class_COMPORTAMENTI)
    Dim selCP As New List(Of Class_Giornaliero_CP)

    Private Sub F_Seleziona_CP_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call Caricacombo()

        If ComboBox1.Items.Count > 0 Then
            ComboBox1.SelectedIndex = 0
            Button0.Enabled = True
        End If
    End Sub
    Sub Caricacombo()
        ComboBox1.Items.Clear()
        'Azzera()
        ComboBox1.DisplayMember = "Testo"  ' Mostra il testo a schermo
        ComboBox1.ValueMember = "ID"        ' Usa l'ID come valo
        For Each elemento In Gbl_Elenco_CP
            ComboBox1.Items.Add(elemento)
        Next
    End Sub
    Sub Azzera()
        Label3.Text = "" : Label4.Text = "" : Label6.Text = ""
        Label3.Tag = -1 : Label4.Tag = -1 : Label6.Tag = -1
        Button3.Visible = False : Button3.Tag = -1
        Button4.Visible = False : Button4.Tag = -1
        Button6.Visible = False : Button6.Tag = -1
        Button0.Enabled = False
        Button1.Enabled = False
        Label8.ImageIndex = 2
        Label9.ImageIndex = 2
        Label10.ImageIndex = 2

        If selCP.Count = 0 Then Exit Sub

        If selCP.Count >= 1 Then
            Label3.Text = selCP(0).TESTO

        End If

    End Sub
    'Function Mostra(sel1 As List(Of Class_COMPORTAMENTI)) As List(Of Class_COMPORTAMENTI)
    Function Mostra(sel1cp As List(Of Class_Giornaliero_CP)) As List(Of Class_Giornaliero_CP)
        Azzera()
        If ComboBox1.Items.Count = 0 Then
            Call Caricacombo()
        End If

        If Not IsNothing(sel1cp) Then
            selCP = sel1cp
            Mostra_sel()
        End If
        Me.ShowDialog()

        If Me.Tag = "OK" Then
            Prepara_Selcp_da_sel()

            Return selCP
        Else
            Return Nothing
        End If
    End Function
    Sub Prepara_Selcp_da_sel()
        'carico sel con il contenuto selcp
        Dim id = 0
        selCP.Clear()
        Try
            Dim sx As New Class_Giornaliero_CP
            id = sel(0).ID
            sx.ID_VOCE = sel(0).ID
            sx.TESTO = sel(0).ATTIVA
            selCP.Add(sx)
        Catch ex As Exception
            Exit Sub
        End Try

        Try
            Dim sx As New Class_Giornaliero_CP
            id = sel(1).ID
            sx.ID_VOCE = sel(1).ID
            sx.TESTO = sel(1).ATTIVA
            selCP.Add(sx)
        Catch ex As Exception
            Exit Sub
        End Try

        Try
            Dim sx As New Class_Giornaliero_CP
            id = sel(2).ID
            sx.ID_VOCE = sel(2).ID
            sx.TESTO = sel(2).ATTIVA
            selCP.Add(sx)
        Catch ex As Exception
            Exit Sub
        End Try



    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button0.Click
        sel.Clear()
        Me.Tag = ""
        Me.Hide()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim j = ComboBox1.SelectedIndex

        If CP_Gia_inserita(j) Then
            Call MsgBox("Voce già inserita", vbInformation + vbOKOnly)
            Exit Sub
        End If

        If Label3.Tag = -1 Then
            Label3.Text = ComboBox1.Text
            Dim x = ComboBox1.Items(j)
            Label3.Tag = j
            Button3.Visible = True
            Button1.Enabled = True
            totali_selezionati()
            mostra_label()

            Exit Sub
        End If

        If Label4.Tag = -1 Then
            Label4.Text = ComboBox1.Text
            Dim x = ComboBox1.Items(j)
            Label4.Tag = j
            Button4.Visible = True
            Button1.Enabled = True
            totali_selezionati()
            mostra_label()

            Exit Sub

        End If

        If Label6.Tag = -1 Then
            Label6.Text = ComboBox1.Text
            Dim x = ComboBox1.Items(j)
            Label6.Tag = j
            Button6.Enabled = True
            Button1.Enabled = True
            totali_selezionati()
            mostra_label()

            Exit Sub

        End If


    End Sub
    Function totali_selezionati() As Integer
        Dim cnt = 0
        If Label6.Tag >= 0 Then cnt = cnt + 1
        If Label4.Tag >= 0 Then cnt = cnt + 1
        If Label3.Tag >= 0 Then cnt = cnt + 1
        If cnt > 0 Then
            Button1.Enabled = True
        Else
            Button1.Enabled = False
        End If
        Return cnt

    End Function

    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click
        Label3.Text = "" : Label3.Tag = -1
        Call Riordina()
    End Sub
    Sub mostra_label()
        If Label3.Text <> "" Then
            Label8.ImageIndex = 3
        Else
            Label8.ImageIndex = 2
        End If
        If Label4.Text <> "" Then
            Label9.ImageIndex = 3
        Else
            Label9.ImageIndex = 2
        End If
        If Label6.Text <> "" Then
            Label10.ImageIndex = 3
        Else
            Label10.ImageIndex = 2
        End If

    End Sub
    Sub Riordina()
        If Label3.Text = "" Then
            If Label4.Text <> "" Then
                Label3.Text = Label4.Text
                Label3.Tag = Label4.Tag
                Label4.Text = Label6.Text
                Label4.Tag = Label6.Tag
                Label6.Text = "" : Label6.Tag = -1 : Button6.Visible = False
            End If
        End If

        If Label4.Text = "" Then
            If Label6.Text <> "" Then
                Label4.Text = Label6.Text
                Label4.Tag = Label6.Tag
                Label6.Text = "" : Label6.Tag = -1
            End If
        End If


        If Label3.Text <> "" Then Button3.Visible = True Else Button3.Visible = False
        If Label4.Text <> "" Then Button4.Visible = True Else Button4.Visible = False
        If Label6.Text <> "" Then Button6.Visible = True Else Button6.Visible = False
        mostra_label()
        totali_selezionati()

    End Sub
    Function TROVA_J_DELLA_COMBO(ID) As Integer
        For I = 0 To ComboBox1.Items.Count - 1
            If ComboBox1.Items(I).ID = ID Then Return I
        Next
        Return -1
    End Function
    Sub Mostra_sel()
        Dim ID_VOCE = -1, J = -1
        If selCP.Count > 0 Then
            ID_voce = selCP(0).ID_VOCE
            J = TROVA_J_DELLA_COMBO(ID_voce)
            If J >= 0 Then
                Label3.Tag = J
                Label3.Text = ComboBox1.Items(J).TESTO
                Label8.ImageIndex = 3
            End If
        End If
        If selCP.Count > 1 Then
            ID_voce = selCP(1).ID_VOCE
            J = TROVA_J_DELLA_COMBO(ID_voce)
            If J >= 0 Then
                Label4.Tag = J
                Label4.Text = ComboBox1.Items(J).TESTO
                Label9.ImageIndex = 3
            End If
        End If
        If selCP.Count > 2 Then
            ID_voce = selCP(2).ID_VOCE
            J = TROVA_J_DELLA_COMBO(ID_voce)
            If J >= 0 Then
                Label6.Tag = J
                Label6.Text = ComboBox1.Items(J).TESTO
                Label10.ImageIndex = 3

            End If
        End If
        Riordina()
    End Sub
    Sub Carica_sel()
        Dim x As New Class_COMPORTAMENTI
        Dim j = 0
        Try
            sel.Clear()

        Catch ex As Exception

        End Try
        If Label3.Tag >= 0 Then
            j = Val(Label3.Tag)
            x = ComboBox1.Items(j)
            sel.Add(x)
        End If
        If Label4.Tag >= 0 Then
            j = Val(Label4.Tag)
            x = ComboBox1.Items(j)
            sel.Add(x)
        End If
        If Label6.Tag >= 0 Then
            j = Val(Label6.Tag)
            x = ComboBox1.Items(j)
            sel.Add(x)
        End If

    End Sub
    Function CP_Gia_inserita(j) As Boolean
        Dim inserita = False
        If Label3.Tag = j Then inserita = True
        If Label4.Tag = j Then inserita = True
        If Label6.Tag = j Then inserita = True
        Return inserita
    End Function
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Label4.Text = "" : Label4.Tag = -1
        Call Riordina()

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Label6.Text = "" : Label6.Tag = -1
        Call Riordina()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Carica_sel()
        Me.Tag = "OK"
        Me.Hide()
    End Sub

    Private Sub Button6_ControlAdded(sender As Object, e As ControlEventArgs) Handles Button6.ControlAdded

    End Sub
End Class