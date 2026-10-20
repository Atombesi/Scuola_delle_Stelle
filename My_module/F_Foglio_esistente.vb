Public Class F_Foglio_esistente
    Private textBoxes() As TextBox
    Dim Noneseguire As Boolean = True
    Dim G_DB_Bambino As String
    Dim G_ID_FOGLIO As Integer
    Dim G_NOME_BAMBINO As String
    Dim G_foglio As New Struct_foglio
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
            L_V.Tag = J
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

            If CheckBox1.Checked And foglio.Compilato Then Continue For  ' foglio compilato non deve essere mostrato

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
            'Button6.Enabled = True
            'CheckBox1.Enabled = True

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
        LabelCP.Text = "0"
        LabelRT.Text = "0"
        LabelV.Text = "0"
        Label5.Text = "- -"
        'Button6.Enabled = False
        'CheckBox1.Enabled = False
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
        Dim Color = GroupBox3.BackColor
        If Edit Then
            Panel_risposte.Visible = True
            Tab_option.Visible = False
            Button6.Enabled = False
            CheckBox1.Enabled = False
            Color = MyColor.LightYellow
            GroupBox3.Enabled = False 'pANNELLO CON ELENCO FOGLI
        Else
            Panel_risposte.Visible = False
            Tab_option.Visible = True
            Button6.Enabled = True
            CheckBox1.Enabled = True
            Color = GroupBox3.BackColor
            GroupBox3.Enabled = True 'pANNELLO CON ELENCO FOGLI
        End If
        Panel_risposte.BackColor = Color
        GroupBox_VOCI.BackColor = Color
        GroupBox_CP.BackColor = Color
        GroupBox_RT.BackColor = Color
        TextBox1.ReadOnly = modo
        TextBox2.ReadOnly = modo
        TextBox3.ReadOnly = modo
        TextBox5.ReadOnly = modo
        TextBox6.ReadOnly = modo
        TextBox7.ReadOnly = modo
        TextBox8.ReadOnly = modo
        CheckBoxRT0.Enabled = Edit
        CheckBoxRT1.Enabled = Edit
        Button_voci.Visible = Edit
        Button_RT.Visible = Edit
        Button_CP.Visible = Edit

    End Function
    Function IS_foglio_compilato() As Boolean
        Dim iscompilato As Boolean = True
        For Each voce In G_foglio.VociDiUnFoglio
            If Not voce.COMPILATO Then
                iscompilato = False
            End If
        Next

        For Each voce In G_foglio.RT
            If voce.COMPILATO = 0 Then
                iscompilato = False
            End If
        Next

        For Each voce In G_foglio.CP
            If voce.COMPILATO = 0 Then
                iscompilato = False
            End If
        Next


        Return iscompilato
    End Function

    Sub mostra_totale_voci_compilate()
        Dim n = 0, nc = 0
        n = Gbl_VociDiUnFoglio.Count
        LabelV.Text = n.ToString

        For i = 0 To n - 1
            If Gbl_VociDiUnFoglio(i).COMPILATO Then nc = nc + 1
        Next

        LabelRT.Text = nc.ToString

    End Sub
    Sub Mostra_Totali(j)
        L_V.Text = Gbl_VociDiUnFoglio(j).Testo : L_V.Refresh()
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




    End Sub
    Sub Mostra_Foglio(j)
        Dim ID_foglio = CInt(Gbl_elenco_fogli_bambino(j).ID_Foglio)
        Gbl_VociDiUnFoglio = Gbl_giornaliero.Carica_Foglio(ID_foglio)

        Dim dbBambino = G_DB_Bambino
        G_foglio = Carica_foglio(ID_foglio, dbBambino, True)

        Dim voci_rt = G_foglio.RT
        Dim voci_cp = G_foglio.CP
        Dim voci = G_foglio.VociDiUnFoglio

        Dim nv = voci.Count
        Dim ncp = voci_cp.Count
        Dim nrt = voci_rt.Count

        Label19.Text = ID_foglio.ToString + " (" + G_foglio.Data + ")"
        LabelCP.Text = voci_cp.Count.ToString
        LabelRT.Text = voci_rt.Count.ToString
        LabelV.Text = voci.Count.ToString

        If nv + ncp + nrt = 0 Then
            Call MsgBox("Nessuna voce trovata")
            Exit Sub
        End If

        Dim iscompilato = IS_foglio_compilato()
        If Not iscompilato Then
            Label5.Text = "Foglio da compilare"
            Label5.BackColor = Color.Orange
        Else
            Label5.Text = "Foglio già compilato"
            Label5.BackColor = Color.LightGreen
        End If

        If G_foglio.Valutativo Then
            Label23.Visible = True
        Else
            Label23.Visible = False
        End If


        '-- Carica griglia voci ELCAR
        DGV1.Rows.Clear()
        DGV1.AllowUserToAddRows = False
        For i As Integer = 0 To voci.Count - 1
            ' Estrai l'elemento corrente usando l'indice i
            Dim voce = voci(i)
            DGV1.Rows.Add(voce.Codice, voce.Testo, voce.TOT1, voce.TOT2, i)
        Next
        DGV1.Refresh()
        DGV1.CurrentCell = DGV1.Rows(0).Cells(1)
        DGV1.Rows(0).Selected = True

        '-- Carica griglia voci RT
        Dim img0 = PictureBox0.Image
        Dim img1 = PictureBox1.Image
        DGV2.Rows.Clear()
        DGV2.AllowUserToAddRows = False
        DirectCast(DGV2.Columns(1), DataGridViewImageColumn).ImageLayout = DataGridViewImageCellLayout.Zoom
        For i As Integer = 0 To voci_rt.Count - 1
            ' Estrai l'elemento corrente usando l'indice i
            Dim voce = voci_rt(i)
            If voce.TOT = 0 Then
                DGV2.Rows.Add(voce.TESTO, img0, i)
            Else
                DGV2.Rows.Add(voce.TESTO, img1, i)
            End If

        Next
        DGV2.Refresh()
        DGV2.CurrentCell = DGV2.Rows(0).Cells(1)
        DGV2.Rows(0).Selected = True


        '-- Carica griglia voci CP
        DGV3.Rows.Clear()
        DGV3.AllowUserToAddRows = False
        For i As Integer = 0 To voci_cp.Count - 1
            ' Estrai l'elemento corrente usando l'indice i
            Dim voce = voci_cp(i)
            DGV3.Rows.Add(voce.TESTO, voce.TOT, i)
        Next
        DGV3.Refresh()
        DGV3.CurrentCell = DGV3.Rows(0).Cells(1)
        DGV3.Rows(0).Selected = True




    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim j = CInt(L_V.Tag)
        Dim ID_foglio = CInt(Gbl_elenco_fogli_bambino(j).ID_Foglio)
        Gbl_VociDiUnFoglio = Gbl_giornaliero.Carica_Foglio(ID_foglio)
        F_Seleziona_Categoria.Carica_dati_foglio_in_DGV1()
        Me.Tag = ID_foglio
        Hide()
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

    Sub Mostra_totali(Type As String)
        If Type = "VOCI" Then
            Dim r = DGV1.CurrentRow
            Dim j = r.Cells(4).Value
            L_V.Text = G_foglio.VociDiUnFoglio(j).Testo
            TextBox1.Text = G_foglio.VociDiUnFoglio(j).TOT1_POS
            TextBox2.Text = G_foglio.VociDiUnFoglio(j).TOT1_NEG
            TextBox3.Text = G_foglio.VociDiUnFoglio(j).TOT1_PROMPT
            Label20.Text = G_foglio.VociDiUnFoglio(j).TOT1.ToString
            TextBox5.Text = G_foglio.VociDiUnFoglio(j).TOT2_POS
            TextBox6.Text = G_foglio.VociDiUnFoglio(j).TOT2_NEG
            TextBox7.Text = G_foglio.VociDiUnFoglio(j).TOT2_PROMPT
            Label21.Text = G_foglio.VociDiUnFoglio(j).TOT2.ToString

        ElseIf Type = "RT" Then
            Dim r = DGV2.CurrentRow
            Dim j = r.Cells(2).Value
            Dim stato = G_foglio.RT(j).TOT
            L_RT.Text = G_foglio.RT(j).TESTO
            If stato = 0 Then
                CheckBoxRT0.Checked = True
                CheckBoxRT1.Checked = False
                PictureBox0.Enabled = True
                PictureBox1.Enabled = False
            Else
                CheckBoxRT1.Checked = True
                CheckBoxRT0.Checked = False
                PictureBox1.Enabled = True
                PictureBox0.Enabled = False
            End If
        ElseIf Type = "CP" Then

        End If
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim elemento As Class_ElementoCombo
        elemento = CType(ComboBox1.SelectedItem, Class_ElementoCombo)
        If IsNothing(elemento) Then Exit Sub
        G_ID_FOGLIO = CInt(elemento.ID)
        Dim j = ComboBox1.SelectedIndex
        Call Mostra_Foglio(j)
        Me.Tag = G_ID_FOGLIO

    End Sub

    Private Sub DGV1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DGV1.CellMouseClick
        Dim r = e.RowIndex
        Dim j = CInt(DGV1.Rows(r).Cells(4).Value)
        Call Mostra_Totali(j)

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
            MsgBox(m, vbOKOnly + vbExclamation)
            Exit Sub
        End If

        'CheckBox1.Checked = False

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
            GroupBoxCP.Visible = False
            GroupBoxRT.Visible = False
            GroupBox3.Enabled = False
        Else
            Label18.Visible = False
            GroupBox4.Visible = True
            GroupBox4.Visible = True
            GroupBoxCP.Visible = True
            GroupBox3.Enabled = True
        End If
        Carica_combo_elenco_fogli()
    End Sub


    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Hide()

    End Sub

    Private Sub Label26_Click(sender As Object, e As EventArgs) Handles Label26.Click
        ' Fa apparire il menu esattamente sotto il pulsante
        CMenuStrip1.Show(Label26, New Point(0, Label26.Height))

    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs)
        If Not ToolStripMenuItem2.Checked Then
            Edit_ONOFF(True)
            ToolStripMenuItem2.Checked = True
            ToolStripMenuItem2.Image = ImageList1.Images(8)
        Else
            Edit_ONOFF(False)
            ToolStripMenuItem2.Checked = False
            ToolStripMenuItem2.Image = Nothing
        End If
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs)
        Azzera_esiti_risposte()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If Noneseguire Then Exit Sub
        Carica_combo_elenco_fogli()
    End Sub



    Private Sub DGV1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV1.CellClick

        Mostra_totali("VOCI")

        Dim r As Integer = DGV1.CurrentRow.Index

        Dim cod = DGV1(0, r).Value
        Call Carica_dati_codice_elcar(cod)
        Dim TESTO = DGV1(1, r).Value

        If Not CheckBox4.Checked Then Exit Sub
        If Not IsNothing(TESTO) Then
            F_MyGraph.Mostra(TESTO)
        End If

    End Sub

    Private Sub DGV2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV2.CellClick
        Mostra_totali("RT")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Edit_ONOFF(False)
    End Sub



    Private Sub ToolStrip_modifica_Click(sender As Object, e As EventArgs) Handles ToolStrip_modifica.Click
        If Not ToolStripMenuItem2.Checked Then
            Edit_ONOFF(True)
        Else
            Edit_ONOFF(False)
        End If


    End Sub
End Class