Public Class F_Foglio_esistente
    Private textBoxes() As TextBox
    Function Main(J As Integer, Optional modo As String = "COMPILE", Optional Duplica As Boolean = False) As String

        Label6.Text = Gbl_NOME_BAMBINO
        Label7.Tag = J
        Dim Dainserire = True

        Call Gbl_Giornaliero.Carica_titoli()
        ComboBox3.Items.Clear()
        ComboBox3.Items.Add("Tutti")
        For i = 0 To Gbl_Elenco_titoli_usati.Length - 1
            ComboBox3.Items.Add(Gbl_Elenco_titoli_usati(i).Titolo)
        Next
        ComboBox3.SelectedIndex = 0   ' iMPOSTO SU TUTTI L'ELENCO TITOLI
        ComboBox2.SelectedIndex = 0   ' iMPOSTO SU TUTTI IL TIPO DI FOGLIO DA MOSTRARE


        Call Carica_combo_elenco_fogli()
        ComboBox1.SelectedIndex = 0


        If Gbl_Caricamento_Immediato Then
            F_Seleziona_Categoria.Carica_foglio(J)
            Hide()
            Exit Function
        End If
        Me.Show()


    End Function
    Sub Carica_combo_elenco_fogli()
        Dim xx = ""
        Gbl_miaLista_fogli.Clear()
        ComboBox1.PopolaLeggero(Gbl_miaLista_fogli)
        Dim foglio As New Class_Elenco_fogli
        For i = 0 To Gbl_elenco_fogli_bambino.Count - 1
            foglio = Gbl_elenco_fogli_bambino(i)
            Dim x = foglio.Data.ToString + " (" + foglio.ID_Foglio.ToString + ") " + foglio.titolo
            Dim da_inserire = True
            If ComboBox3.Text <> "Tutti" Then  ' tutti i titoli 
                If foglio.titolo <> ComboBox3.Text Then
                    Continue For
                End If
            End If

            If ComboBox2.Text = "Compilati" And Not foglio.Compilato Then
                Continue For
            ElseIf ComboBox2.Text = "Da compilare" And foglio.Compilato Then
                Continue For
            End If
            If foglio.Compilato Then x = x + " [Compilato]"
            Dim elemento As New Class_ElementoCombo(i, x, "")
            Gbl_miaLista_fogli.Add(elemento)


        Next
        ComboBox1.PopolaLeggero(Gbl_miaLista_fogli)
        If ComboBox1.Items.Count = 0 Then
            ' Azzero la griglia 
            Call Nessun_foglio_da_mostrare()
            Exit Sub
        End If

        Try
            ComboBox1.SelectedIndex = 0
            Button6.Enabled = True
            CheckBox1.Enabled = True

        Catch ex As Exception

        End Try

        Exit Sub


    End Sub
    Sub Nessun_foglio_da_mostrare()
        DGV1.Rows.Clear()
        Label18.Text = "- -"
        Label13.Text = "0"
        Label16.Text = "0"
        Label10.Text = "0"
        Label5.Text = "- -"
        Button6.Enabled = False
        CheckBox1.Enabled = False
    End Sub
    Sub Carica_foglio(j)
        Dim ID_foglio = CInt(Gbl_elenco_fogli_bambino(j).ID_Foglio)
        Gbl_VociDiUnFoglio = Gbl_Giornaliero.Carica_Foglio(ID_foglio)
        Dtp1.Text = Gbl_elenco_fogli_bambino(j).Data
        Mostra_Totali(j)
        If Gbl_VociDiUnFoglio.Count = 0 Then Exit Sub

        'Controlla se sono state compilate tutte le voci
        Dim iscompilato = IS_foglio_compilato()

        If iscompilato Then
            CheckBox1.Text = "Modifica"
        Else
            CheckBox1.Text = "Compila"
        End If

        Button6.Enabled = True
        Label6.BackColor = Label5.BackColor
        Call Mostra_Foglio(j)

    End Sub

    Function salva_foglio() As Boolean
        Dim nvoci = Gbl_VociDiUnFoglio.Count - 1
        Dim Salvato = True


        For i = 0 To nvoci
            Try
                Gbl_VociDiUnFoglio(i).COMPILATO = True
                Gbl_VociDiUnFoglio(i).Salva()
            Catch ex As Exception
                Salvato = False
            End Try
        Next

        If Salvato Then

        End If
        Return Salvato
    End Function

    Function Edit_ONOFF(Optional Edit As Boolean = False)
        Dim modo = Not Edit
        If modo Then
            TextBox1.ReadOnly = True
            TextBox2.ReadOnly = True
            TextBox3.ReadOnly = True
            TextBox5.ReadOnly = True
            TextBox6.ReadOnly = True
            TextBox7.ReadOnly = True
        Else
            TextBox1.ReadOnly = False
            TextBox2.ReadOnly = False
            TextBox3.ReadOnly = False
            TextBox5.ReadOnly = False
            TextBox6.ReadOnly = False
            TextBox7.ReadOnly = False
        End If
    End Function
    Function IS_foglio_compilato() As Boolean
        Dim iscompilato As Boolean = True
        For Each voce In Gbl_VociDiUnFoglio
            If Not voce.COMPILATO Then iscompilato = False
        Next

        If Not iscompilato Then
            Label5.Text = "Foglio da compilare"
            Label5.BackColor = Color.Orange
            Edit_ONOFF(True)
        Else
            Label5.Text = "Foglio già compilato"
            Label5.BackColor = Color.LightGreen
            Edit_ONOFF()
        End If

        Return iscompilato
    End Function

    Sub mostra_totale_voci_compilate()
        Dim n = 0, nc = 0
        n = Gbl_VociDiUnFoglio.Count
        Label10.Text = n.ToString

        For i = 0 To n - 1
            If Gbl_VociDiUnFoglio(i).COMPILATO Then nc = nc + 1
        Next

        Label16.Text = nc.ToString

    End Sub
    Sub Mostra_Totali(j)
        Label15.Text = Gbl_VociDiUnFoglio(j).Codice : Label15.Refresh()
        Label7.Text = Gbl_VociDiUnFoglio(j).Testo : Label7.Refresh()
        TextBox1.Text = Gbl_VociDiUnFoglio(j).TOT1_POS
        TextBox2.Text = Gbl_VociDiUnFoglio(j).TOT1_NEG
        TextBox3.Text = Gbl_VociDiUnFoglio(j).TOT1_PROMPT
        Label20.Text = Gbl_VociDiUnFoglio(j).TOT1

        If Gbl_VociDiUnFoglio(j).TOT2 > 0 Then
            TextBox5.Text = Gbl_VociDiUnFoglio(j).TOT2_POS
            TextBox6.Text = Gbl_VociDiUnFoglio(j).TOT2_NEG
            TextBox7.Text = Gbl_VociDiUnFoglio(j).TOT2_PROMPT
            Label21.Text = Gbl_VociDiUnFoglio(j).TOT2
        Else
            TextBox5.Text = ""
            TextBox6.Text = ""
            TextBox7.Text = ""
            Label21.Text = ""
        End If
        mostra_totale_voci_compilate()
    End Sub
    Sub Mostra_Foglio(j)
        Dim ID_foglio = CInt(Gbl_elenco_fogli_bambino(j).ID_Foglio)
        Gbl_VociDiUnFoglio = Gbl_Giornaliero.Carica_Foglio(ID_foglio)
        Dim n = Gbl_VociDiUnFoglio.Count
        Label13.Text = ID_foglio : Label13.Refresh()
        Label18.Text = Gbl_elenco_fogli_bambino(j).titolo

        If n = 0 Then
            Call MsgBox("Nessuna voce trovata")
            Exit Sub
        Else
        End If

        DGV1.Rows.Clear()
        For i As Integer = 0 To Gbl_VociDiUnFoglio.Count - 1
            ' Estrai l'elemento corrente usando l'indice i
            Dim foglio = Gbl_VociDiUnFoglio(i)
            DGV1.Rows.Add(foglio.Codice, foglio.Testo, foglio.TOT1, foglio.TOT2, i)
        Next


        DGV1.Refresh()


    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim j = CInt(Label7.Tag)
        F_Seleziona_Categoria.Carica_foglio(j)
        Hide()
    End Sub

    Private Sub GroupBox4_Enter(sender As Object, e As EventArgs) Handles GroupBox4.Enter

    End Sub

    Private Sub F_Foglio_esistente_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        textBoxes = New TextBox() {TextBox1, TextBox2, TextBox3, TextBox5, TextBox6, TextBox7}

        ' 2. Associa dinamicamente gli eventi a tutti i TextBox dell'array
        For Each txt As TextBox In textBoxes
            AddHandler txt.KeyDown, AddressOf TextBox_KeyDown
            AddHandler txt.Enter, AddressOf TextBox_Enter
            AddHandler txt.Leave, AddressOf TextBox_Leave
        Next
    End Sub
    Private Sub TextBox_Enter(sender As Object, e As EventArgs)
        Dim currentTextBox As TextBox = CType(sender, TextBox)
        currentTextBox.BackColor = Color.LightYellow
    End Sub

    ' Ripristina lo sfondo originale (bianco) quando il TextBox perde il fuoco
    Private Sub TextBox_Leave(sender As Object, e As EventArgs)
        Dim currentTextBox As TextBox = CType(sender, TextBox)
        currentTextBox.BackColor = SystemColors.Window
    End Sub
    Private Sub TextBox_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            Dim tot = 0
            ' Impedisce il "bip" acustico predefinito di Windows alla pressione di Invio
            e.SuppressKeyPress = True

            Dim currentTextBox As TextBox = CType(sender, TextBox)
            Dim currentIndex As Integer = Array.IndexOf(textBoxes, currentTextBox)

            If currentIndex <= 2 Then ' sono nei totali 1
                tot = CInt(Val(TextBox1.Text)) + CInt(Val(TextBox2.Text)) + CInt(Val(TextBox3.Text))
                Label20.Text = tot.ToString
            Else
                Try
                    tot = CInt(Val(TextBox5.Text)) + CInt(Val(TextBox6.Text)) + CInt(Val(TextBox7.Text))
                Catch ex As Exception
                    tot = 0
                End Try
                Label21.Text = tot.ToString
            End If
            ' Calcola l'indice successivo in modo ciclico (da 7 torna a 0)
            Dim nextIndex As Integer = (currentIndex + 1) Mod textBoxes.Length

            ' Verifica se devo saltare la riga (non ho scritto nulla in pos2
            If currentIndex = 3 And TextBox5.Text = "" Then
                riporta_totali()
                Button1.Focus()
                Exit Sub
            End If

            If currentIndex = 5 Then
                riporta_totali()
                Button1.Focus()
                Exit Sub
            End If


            ' Sposta il fuoco sul controllo successivo
            textBoxes(nextIndex).Focus()
        End If
    End Sub
    Sub riporta_totali()
        Dim r = DGV1.CurrentRow.Index, v = 0
        Dim j = CInt(DGV1.Rows(r).Cells(4).Value)

        v = CInt(Val(Label20.Text))
        If Gbl_VociDiUnFoglio(j).TOT1 <> v Then
            Gbl_VociDiUnFoglio(j).TOT1 = v
            DGV1.Rows(r).Cells(2).Value = v
            DGV1.Rows(r).Cells(2).Style.BackColor = Color.Turquoise
        End If

        v = CInt(Val(Label21.Text))
        If Gbl_VociDiUnFoglio(j).TOT2 <> v Then
            Gbl_VociDiUnFoglio(j).TOT2 = v
            DGV1.Rows(r).Cells(3).Value = v
            DGV1.Rows(r).Cells(3).Style.BackColor = Color.Turquoise
        End If
        v = CInt(Val(TextBox1.Text))
        If Gbl_VociDiUnFoglio(j).TOT1_POS <> v Then
            Gbl_VociDiUnFoglio(j).TOT1_POS = v
        End If

        v = CInt(Val(TextBox2.Text))
        If Gbl_VociDiUnFoglio(j).TOT1_NEG <> v Then
            Gbl_VociDiUnFoglio(j).TOT1_NEG = v
        End If

        v = CInt(Val(TextBox3.Text))
        If Gbl_VociDiUnFoglio(j).TOT1_PROMPT <> v Then
            Gbl_VociDiUnFoglio(j).TOT1_PROMPT = v
        End If


        v = CInt(Val(TextBox5.Text))
        If Gbl_VociDiUnFoglio(j).TOT2_POS <> v Then
            Gbl_VociDiUnFoglio(j).TOT2_POS = v
        End If

        v = CInt(Val(TextBox6.Text))
        If Gbl_VociDiUnFoglio(j).TOT2_NEG <> v Then
            Gbl_VociDiUnFoglio(j).TOT2_NEG = v
        End If

        v = CInt(Val(TextBox7.Text))
        If Gbl_VociDiUnFoglio(j).TOT2_PROMPT <> v Then
            Gbl_VociDiUnFoglio(j).TOT2_PROMPT = v
        End If

        v = CInt(Val(Label21.Text))
        If Gbl_VociDiUnFoglio(j).TOT2 <> v Then
            Gbl_VociDiUnFoglio(j).TOT2 = v
        End If



    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim elemento As Class_ElementoCombo
        elemento = CType(ComboBox1.SelectedItem, Class_ElementoCombo)
        If IsNothing(elemento) Then Exit Sub
        Dim j = CInt(elemento.ID)
        Carica_foglio(j)

    End Sub

    Private Sub DGV1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DGV1.CellMouseClick
        Dim r = e.RowIndex
        Dim j = CInt(DGV1.Rows(r).Cells(4).Value)
        Call Mostra_Totali(j)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If DGV1.Rows.Count = 0 Then Exit Sub
        Dim r = DGV1.CurrentRow.Index


        Dim currentIndex = -1

        ' Recupera l'indice della riga attualmente attiva, se esiste
        If DGV1.CurrentRow IsNot Nothing Then
            currentIndex = DGV1.CurrentRow.Index
        End If

        ' Calcola l'indice della riga successiva in modo ciclico
        Dim nextIndex = (currentIndex + 1) Mod DGV1.Rows.Count

        ' Rimuove le selezioni precedenti per evitare selezioni multiple
        DGV1.ClearSelection()

        If r >= Gbl_VociDiUnFoglio.Count - 1 Then
            r = 0
        Else
            r = r + 1
        End If
        nextIndex = r
        ' Seleziona la riga intera (evidenziazione blu standard)
        DGV1.Rows(nextIndex).Selected = True

        ' Sposta la cella attiva sulla prima colonna della nuova riga (sposta il focus e la freccetta laterale)
        DGV1.CurrentCell = DGV1.Rows(nextIndex).Cells(0)
        Dim j = CInt(DGV1.Rows(nextIndex).Cells(4).Value)
        Mostra_Totali(j)
        If CheckBox1.Checked Then TextBox1.Focus()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        GroupBox1.Enabled = CheckBox1.Checked
        If CheckBox1.Checked Then
            CheckBox1.BackColor = Color.PaleGreen
            Call Edit_ONOFF(True)
            TextBox1.Focus()
            Button5.Visible = True
        Else
            CheckBox1.BackColor = SystemColors.Control
            Call Edit_ONOFF(False)
            Button5.Visible = False

        End If

    End Sub



    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        '1- Verifico che tutte le voci siano compilate (non ci siano dei TOT1 =0)
        If Not voci_compilate() Then
            Dim m = "Sono resenti degli esercizi non compilati. Vuoi comunque salvare il foglio?"
            If MsgBox(m, vbYesNo + vbExclamation) = vbNo Then Exit Sub
        End If

        '2- Salvo il foglio e lo marco come compilato
        If Not salva_foglio() Then
            Dim m = "Ci sono stati dei problemi durante la procedura di salvataggio"
            Call MsgBox(m, vbOKOnly + vbExclamation)
            Exit Sub
        End If

        CheckBox1.Checked = False
        Button5.Enabled = False

    End Sub
    Function voci_compilate() As Boolean
        For i = 0 To Gbl_VociDiUnFoglio.Count - 1
            If Gbl_VociDiUnFoglio(i).TOT1 = 0 Then Return False
        Next
        Return True
    End Function
    Sub Azzera_esiti_risposte()
        Dim m = "Vuoi davvero azzerare tutte i totali e impostare il foglio come da compilare?" + vbCrLf
        m = m + "(Questa operazione è irreversiile)"
        If MsgBox(m, vbYesNo + vbExclamation, "Azzeamento Risposte") <> vbYes Then Exit Sub
        Dim SALVATO As Boolean = True
        For i = 0 To Gbl_VociDiUnFoglio.Count - 1
            Gbl_VociDiUnFoglio(i).COMPILATO = False
            Gbl_VociDiUnFoglio(i).TOT1 = 0
            Gbl_VociDiUnFoglio(i).TOT1_POS = 0
            Gbl_VociDiUnFoglio(i).TOT1_NEG = 0
            Gbl_VociDiUnFoglio(i).TOT1_PROMPT = 0
            Gbl_VociDiUnFoglio(i).TOT2 = 0
            Gbl_VociDiUnFoglio(i).TOT2_POS = 0
            Gbl_VociDiUnFoglio(i).TOT2_NEG = 0
            Gbl_VociDiUnFoglio(i).TOT2_PROMPT = 0
            Try
                Gbl_VociDiUnFoglio(i).Salva()
            Catch ex As Exception
                SALVATO = False
            End Try
        Next
        If Not SALVATO Then
            Call MsgBox("PROBLEMI NEL SALVATAGGIO")
        Else
            Call MsgBox("Azzeramento risposte effettuato")
        End If
    End Sub

    Private Sub Dtp1_ValueChanged(sender As Object, e As EventArgs) Handles Dtp1.ValueChanged

    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub


    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs)
        Carica_combo_elenco_fogli()

    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs)
        Carica_combo_elenco_fogli()

    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs)
        Carica_combo_elenco_fogli()

    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        Carica_combo_elenco_fogli()
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        Carica_combo_elenco_fogli()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Hide()

    End Sub

    Private Sub Label26_Click(sender As Object, e As EventArgs) Handles Label26.Click
        ' Fa apparire il menu esattamente sotto il pulsante
        CMenuStrip1.Show(Label26, New Point(0, Label26.Height))

    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
        If Not ToolStripMenuItem2.Checked Then
            Edit_ONOFF(True)
            ToolStripMenuItem2.Checked = True
            Label27.Visible = True
            Label27.Text = "Force Editing ON"
        Else
            Edit_ONOFF(False)
            ToolStripMenuItem2.Checked = False
            Label27.Visible = False
        End If
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        Call Azzera_esiti_risposte
    End Sub
End Class