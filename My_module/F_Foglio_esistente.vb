Public Class F_Foglio_esistente
    Private textBoxes() As TextBox
    Dim Noneseguire As Boolean = True
    Dim G_DB_Bambino As String
    Dim G_ID_FOGLIO As Integer
    Dim G_NOME_BAMBINO As String
    Function Main(J As Integer, Optional SoloDacompilare As Boolean = False, Optional ALL As Boolean = False) As String

        'Se j>=0 allora ho passato indicazioni per il foglo di un bambino e quindi preparo la form disabilitando la scelta del bambino
        'Se j< 0 allora Preparo form vuota e abilito la scelta del bambino
        'Se all=true abilito forzatamente la possibilià di modificare il bambino
        '   in questo caso devo stare attento a come carico i dati

        CheckBox1.Checked = SoloDacompilare
        Noneseguire = True
        If J >= 0 Then
            ' Ho passato un nome di bambino da visualizzare
            '-Azzero combobox
            G_NOME_BAMBINO = Gbl_foglio.Bambino
            G_DB_Bambino = Gbl_foglio.DB_Bambino
            Gbl_miaLista_fogli.Clear()
            ComboBox2.PopolaLeggero(Gbl_miaLista_fogli)
            ComboBox2.Text = G_NOME_BAMBINO : ComboBox2.Enabled = False
            Label6.Text = G_NOME_BAMBINO
            Label7.Tag = J
            Call Carica_combo_elenco_fogli()
            Noneseguire = False
            Try
                ComboBox1.SelectedIndex = 0   'Foglio selezionato
            Catch ex As Exception

            End Try

            Button6.Visible = True
            Me.ShowDialog()
            J = Val(Me.Tag)

            Return Val(Me.Tag)   ' RITORNA ID_Foglio

        Else
            Noneseguire = False
            Carica_elenco_bambini()
            ComboBox2.SelectedIndex = 0   'sELEZIONO IL PRIMO BAMBINO
            Button6.Visible = False
            Me.ShowDialog()
            Return -1
        End If

        Dim Dainserire = True




    End Function
    Sub Carica_combo_elenco_fogli()
        ' Carico l'elenco dei fogli disponibili per il bambino - 
        Dim xx = ""
        '-- Azzero la combo con l'elenco dei fogli -- 
        Gbl_miaLista_fogli.Clear()
        ComboBox1.PopolaLeggero(Gbl_miaLista_fogli)

        Dim foglio As New Class_Elenco_fogli
        Dim da_inserire = True
        Dim N = 0
        DGV1.Enabled = False

        Try
            N = Gbl_elenco_fogli_bambino.Count - 1
        Catch ex As Exception
            Trova_tutti_fogli_Bambino()
        End Try

        DGV1.Enabled = True

        For i = 0 To Gbl_elenco_fogli_bambino.Count - 1
            foglio = Gbl_elenco_fogli_bambino(i)
            Dim x = foglio.Data.ToString + " (" + foglio.ID_Foglio.ToString + ") " + foglio.titolo
            Dim id = foglio.ID_Foglio
            da_inserire = True

            If CheckBox2.Checked And foglio.Compilato Then Continue For  ' foglio compilato non deve essere mostrato

            If foglio.Compilato Then x = x + " [Compilato]"
            Dim elemento As New Class_ElementoCombo(id, x, "")
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



    End Sub
    Sub Carica_elenco_bambini()

        '--Azzero combobox
        Gbl_miaLista_fogli.Clear()
        ComboBox2.PopolaLeggero(Gbl_miaLista_fogli)


        For I = 0 To Gbl_Elenco_bambini.Count - 1
            If Not Gbl_Elenco_bambini(I).Attivo Then Continue For
            Dim x = Gbl_Elenco_bambini(I).Cognome + " " + Gbl_Elenco_bambini(I).Nome
            Dim elemento As New Class_ElementoCombo(I, x, Gbl_Elenco_bambini(I).DBname)
            Gbl_miaLista_fogli.Add(elemento)
        Next
        ComboBox2.PopolaLeggero(Gbl_miaLista_fogli)

        Try
            ComboBox2.SelectedIndex = 0
        Catch ex As Exception

        End Try
        ComboBox2.Enabled = True


    End Sub


    Sub Nessun_foglio_da_mostrare()
        DGV1.Rows.Clear()
        Label13.Text = "0"
        Label16.Text = "0"
        Label10.Text = "0"
        Label5.Text = "- -"
        Button6.Enabled = False
        CheckBox1.Enabled = False
    End Sub
    Sub Carica_foglio_loc(j)
        'Dim IdD_foglio = CInt(Gbl_elenco_fogli_bambino(j).ID_Foglio)
        'Gbl_VociDiUnFoglio = Gbl_giornaliero.Carica_Foglio(IdD_foglio)

        Gbl_VociDiUnFoglio = Gbl_foglio.VociDiUnFoglio
        Dtp1.Text = Gbl_elenco_fogli_bambino(j).Data
        'Mostra_Totali(j)
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
        Dim m = "", mm = ""
        ' Salvo in giornaliero tutte le voci del foglio
        For i = 0 To nvoci
            Try
                Gbl_VociDiUnFoglio(i).COMPILATO = True
                Gbl_VociDiUnFoglio(i).Salva()
            Catch ex As Exception
                m = "Problemi salvataggio TOT Giornaliero (id:" + Gbl_VociDiUnFoglio(i).ID + ")  DB:" + Gbl_DB_BAMBINO
                APPEND_LOG(m, GbL_Logfile, True)
                Salvato = False
                mm = "Problemi salvataggio in GIORNALIERO"
            End Try
        Next

        'salvo in Giornaliero_CP i risultati
        nvoci = Gbl_foglio.CP.Count - 1
        For i = 0 To nvoci
            If Not Gbl_foglio.CP(i).Salva() Then
                m = "Problemi salvataggio TOT_CP Giornaliero_CP (id:" + Gbl_foglio.CP(i).ID + ")  DB:" + Gbl_DB_BAMBINO + " "
                APPEND_LOG(m, GbL_Logfile, True)
                If mm = "" Then
                    mm = "Problemi salvataggio in GIORNALIERO_CP"
                Else
                    mm = "Problemi salvataggio in GIORNALIERO e GIORNALIERO"
                End If
                Salvato = False
            End If
        Next


        If Not Salvato Then
            Call MsgBox(mm, vbCritical + vbOKOnly)
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

            TextBox8.ReadOnly = True
            TextBox9.ReadOnly = True
            TextBox10.ReadOnly = True
        Else
            TextBox1.ReadOnly = False
            TextBox2.ReadOnly = False
            TextBox3.ReadOnly = False
            TextBox5.ReadOnly = False
            TextBox6.ReadOnly = False
            TextBox7.ReadOnly = False

            TextBox8.ReadOnly = False
            TextBox9.ReadOnly = False
            TextBox10.ReadOnly = False
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
        Try
            Label15.Text = Gbl_VociDiUnFoglio(j).Codice : Label15.Refresh()

        Catch ex As Exception
            Exit Sub
        End Try
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
    Sub Mostra_CP()
        TP1.SetToolTip(LCP1, "")
        TP1.SetToolTip(LCP2, "")
        TP1.SetToolTip(LCP3, "")

        LCP1.Visible = False : TextBox8.Visible = False
        LCP2.Visible = False : TextBox9.Visible = False
        LCP3.Visible = False : TextBox10.Visible = False

        If Gbl_foglio.CP.Count = 0 Then
            LCP0.Visible = True
            LCP0.Refresh()
            Exit Sub
        End If
        LCP0.Visible = False : LCP0.Refresh()




        If Gbl_foglio.CP.Count > 0 Then
            TextBox8.Text = Gbl_foglio.CP(0).TOT
            TextBox8.ReadOnly = False
            TextBox8.Visible = True
            LCP1.Visible = True
            TP1.SetToolTip(LCP1, Gbl_foglio.CP(0).TESTO)
        End If
        If Gbl_foglio.CP.Count > 1 Then
            TextBox9.Text = Gbl_foglio.CP(1).TOT
            TextBox9.ReadOnly = False
            LCP2.Visible = True
            TextBox9.Visible = True
            TP1.SetToolTip(LCP2, Gbl_foglio.CP(1).TESTO)
        End If
        If Gbl_foglio.CP.Count > 2 Then
            TextBox10.Text = Gbl_foglio.CP(2).TOT
            TextBox10.ReadOnly = False
            LCP3.Visible = True
            TextBox10.Visible = True
            TP1.SetToolTip(LCP3, Gbl_foglio.CP(2).TESTO)
        End If
        LCP1.Refresh()
        LCP2.Refresh()
        LCP3.Refresh()

    End Sub
    Sub Mostra_Foglio(j)
        Dim ID_foglio = CInt(Gbl_elenco_fogli_bambino(j).ID_Foglio)
        Gbl_VociDiUnFoglio = Gbl_giornaliero.Carica_Foglio(ID_foglio)
        Dim n = Gbl_VociDiUnFoglio.Count
        Label13.Text = ID_foglio : Label13.Refresh()

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
        DGV1.CurrentCell = DGV1.Rows(0).Cells(1)
        DGV1.Rows(0).Selected = True

        ' 3. SCATENA L'EVENTO: Chiama manualmente la Sub che gestisce il click.
        ' Passiamo come mittente (sender) la cella stessa e un oggetto DataGridViewCellEventArgs vuoto
        Dim args As New DataGridViewCellEventArgs(0, 0) ' (IndiceColonna, IndiceRiga)
        Dim mouseArgs As New DataGridViewCellMouseEventArgs(0, 0, 0, 0, New MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0))
        DGV1_CellMouseClick(DGV1, mouseArgs)

        CheckBox1.Checked = False

        Call mostra_cp


    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim j = CInt(Label7.Tag)
        Dim ID_foglio = CInt(Gbl_elenco_fogli_bambino(j).ID_Foglio)
        Gbl_VociDiUnFoglio = Gbl_giornaliero.Carica_Foglio(ID_foglio)
        F_Seleziona_Categoria.Carica_dati_foglio_in_DGV1()
        Me.Tag = ID_foglio
        Hide()
    End Sub

    Private Sub GroupBox4_Enter(sender As Object, e As EventArgs) Handles GroupBox4.Enter

    End Sub

    Private Sub F_Foglio_esistente_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Noneseguire = True
        textBoxes = New TextBox() {TextBox1, TextBox2, TextBox3, TextBox5, TextBox6, TextBox7}

        ' 2. Associa dinamicamente gli eventi a tutti i TextBox dell'array
        For Each txt As TextBox In textBoxes
            AddHandler txt.KeyDown, AddressOf TextBox_KeyDown
            AddHandler txt.Enter, AddressOf TextBox_Enter
            AddHandler txt.Leave, AddressOf TextBox_Leave
        Next
        Noneseguire = False
        DGV1.Enabled = False
        CheckBox1.Checked = False

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
        If e.KeyCode <> Keys.Enter Then Exit Sub
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
        If Not textBoxes(nextIndex).ReadOnly Then
            textBoxes(nextIndex).Focus()
        Else
            Button1.Focus()
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
        G_ID_FOGLIO = CInt(elemento.ID)
        Gbl_foglio = Carica_foglio(G_ID_FOGLIO, G_DB_Bambino)
        Dim j = ComboBox1.SelectedIndex
        Carica_foglio_loc(j)
        Call Mostra_CP()
        Me.Tag = G_ID_FOGLIO

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
            TextBox8.Focus()
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

        Dim ok = True
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
        Try
            Gbl_foglio.CP(0).TOT = 0
            Gbl_foglio.CP(1).TOT = 0
            Gbl_foglio.CP(2).TOT = 0
        Catch ex As Exception

        End Try

        If Not SALVATO Then
            Call MsgBox("PROBLEMI NEL SALVATAGGIO")
        Else
            Call MsgBox("Azzeramento risposte effettuato")
        End If
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs)
        If Noneseguire Then Exit Sub
        Carica_combo_elenco_fogli()

    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs)
        If Noneseguire Then Exit Sub
        Carica_combo_elenco_fogli()

    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs)
        If Noneseguire Then Exit Sub
        Carica_combo_elenco_fogli()

    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        If Noneseguire Then Exit Sub
        ' SE SELEZIONO UN BAMBINO ALLORA DEVO MODIFICARE IL RIFERIMENTO DEL DATABASE
        Dim elemento1 As Class_ElementoCombo
        elemento1 = CType(ComboBox2.SelectedItem, Class_ElementoCombo)
        If IsNothing(elemento1) Then
            Exit Sub
        End If
        G_DB_Bambino = elemento1.Tag
        Gbl_DB_BAMBINO = Costruisci_filenameDB_bambino(G_DB_Bambino)
        If Trova_tutti_fogli_Bambino() = 0 Then
            Label18.Visible = True
            GroupBox4.Visible = False
            GroupBox3.Enabled = False
        Else
            Label18.Visible = False
            GroupBox4.Visible = True
            GroupBox3.Enabled = True
        End If
        Carica_combo_elenco_fogli()
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs)
        If Noneseguire Then Exit Sub
        Carica_combo_elenco_fogli
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
        Call Azzera_esiti_risposte()
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If Noneseguire Then Exit Sub
        Carica_combo_elenco_fogli()
    End Sub

    Private Sub ComboBox1_Paint(sender As Object, e As PaintEventArgs) Handles ComboBox1.Paint

    End Sub

    Private Sub DGV1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV1.CellContentClick
        Dim r As Integer = DGV1.CurrentRow.Index
        Dim cod = DGV1(0, r).Value
        Call Carica_dati_codice_elcar(cod)
        Dim TESTO = DGV1(1, r).Value

        If Not CheckBox4.Checked Then Exit Sub
        If Not IsNothing(TESTO) Then
            F_MyGraph.Mostra(TESTO)
        End If

    End Sub

    Private Sub LCP1_Click(sender As Object, e As EventArgs) Handles LCP1.Click

    End Sub

    Private Sub Label27_Click(sender As Object, e As EventArgs) Handles Label27.Click

    End Sub
End Class