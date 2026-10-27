Imports System.Printing
Imports System.Windows.Shell
Imports DocumentFormat.OpenXml.Drawing
Imports DocumentFormat.OpenXml.Drawing.Charts
Imports DocumentFormat.OpenXml.Office2010.CustomUI
Imports DocumentFormat.OpenXml.Office2013.Drawing.Chart
Imports DocumentFormat.OpenXml.Office2013.Drawing.ChartStyle
Imports DocumentFormat.OpenXml.Wordprocessing
Imports ScottPlot
Imports ScottPlot.AxisPanels
Imports SQLitePCL

Public Class F_Foglio_esistente
    Private textBoxes() As TextBox
    Dim Noneseguire As Boolean = True
    Dim G_DB_Bambino As String
    Dim G_ID_FOGLIO As Integer
    Dim G_NOME_BAMBINO As String
    Dim G_foglio As New Struct_foglio
    Private txtMatrice(5) As TextBox

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
            Mostra_riassunto_fogli_bambno()
            Call Nessun_foglio_da_mostrare()
            Exit Sub
        End If

        Try
            ComboBox1.SelectedIndex = 0
            'Button6.Enabled = True
            'CheckBox1.Enabled = True

        Catch ex As Exception

        End Try
        Mostra_riassunto_fogli_bambno()


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
        RadioButtonRT0.Enabled = Edit
        RadioButtonRT1.Enabled = Edit
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
            Label5.BackColor = MyColor.Orange
        Else
            Label5.Text = "Foglio già compilato"
            Label5.BackColor = MyColor.LightGreen
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
                DGV2.Rows.Add(voce.TESTO, img0, i, 0)
            Else
                DGV2.Rows.Add(voce.TESTO, img1, i, 1)
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


        Dim args As New DataGridViewCellEventArgs(0, 0)
        DGV1.CurrentCell = DGV1.Rows(0).Cells(0)
        DGV1_CellContentClick(DGV1, args)
        DGV2.CurrentCell = DGV2.Rows(0).Cells(0)
        DGV2_CellContentClick(DGV2, args)
        DGV3.CurrentCell = DGV3.Rows(0).Cells(0)
        DGV3_CellContentClick(DGV3, args)

        ' 2. Crea gli argomenti dell'evento impostando colonna e riga

        ' 3. Ruba lo scatto all'evento richiamando la sua sub


    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim ID_foglio = CInt(G_foglio.ID_foglio)
        Dim m = "Foglio N. " + ID_foglio.ToString + " del " + G_foglio.Data
        m = m + "Vuoi Utilizzare Le voci di questo Foglio?"
        If MsgBox(m, vbInformation + vbYesNo, "Importazioni voci foglio essitente") <> vbYes Then Exit Sub

        F_Seleziona_Categoria.Label24.Text = Label24.Text = "Copiato dal foglio n. " + ID_foglio.ToString + " del " + G_foglio.Data
        F_Seleziona_Categoria.carica_ultimo_foglio_bambino(ID_foglio)
        Me.Tag = ID_foglio
        Hide()
    End Sub

    Private Sub TextBox_Enter(sender As Object, e As EventArgs)
        Dim currentTextBox As TextBox = CType(sender, TextBox)
        currentTextBox.BackColor = MyColor.LightYellow
    End Sub

    ' Ripristina lo sfondo originale (bianco) quando il TextBox perde il fuoco
    Private Sub TextBox_Leave(sender As Object, e As EventArgs)
        Dim currentTextBox As TextBox = CType(sender, TextBox)
        currentTextBox.BackColor = SystemColors.Window
    End Sub
    Function Mostra_totali_voci_da_testo() As (tot1 As Integer, tot2 As Integer)
        ' Calcola i totali dalle caselle di testo
        ' Controlla che siano accettabili
        ' Aggiorna la label
        ' Ritorna i totali
        Dim tot1 = 0, tot2 = 0
        tot1 = CInt(Val(txtMatrice(0).Text)) + CInt(Val(txtMatrice(1).Text)) + CInt(Val(txtMatrice(2).Text))
        tot2 = CInt(Val(txtMatrice(3).Text)) + CInt(Val(txtMatrice(4).Text)) + CInt(Val(txtMatrice(5).Text))


        Label20.Text = tot1 : Label21.Text = tot2
        If tot1 < 0 Then
            TP1.SetToolTip(Label20, "Impssibile accettare totali negativi")
            Button_voci.Enabled = False
            Label20.BackColor = MyColor.Red : Label20.ForeColor = MyColor.White
            Return (tot1, tot2)
        End If
        If tot2 < 0 Then
            Button_voci.Enabled = False
            TP1.SetToolTip(Label21, "Impssibile accettare totali negativi")
            Label21.BackColor = MyColor.Red : Label21.ForeColor = MyColor.White
            Return (tot1, tot2)
        End If

        Dim nok = True
        If Not G_foglio.Valutativo Then
            ' Controlla che i totali siano 20
            If tot1 > 0 And tot1 <> 20 Then
                TP1.SetToolTip(Label20, "I totali delle voci DEVONO chiudere a 20")
                Button_voci.Enabled = False
                Label20.BackColor = MyColor.Red : Label20.ForeColor = MyColor.White
                Return (tot1, tot2)
            End If
            If tot2 > 0 And tot2 <> 20 Then
                TP1.SetToolTip(Label21, "I totali delle voci DEVONO chiudere a 20")
                Button_voci.Enabled = False
                Label21.BackColor = MyColor.Red : Label21.ForeColor = MyColor.White
                Return (tot1, tot2)
            End If
        End If
        Button_voci.Enabled = True

        TP1.SetToolTip(Label21, "totali corretti")
        Label21.BackColor = SystemColors.Control : Label21.ForeColor = SystemColors.ControlText
        Label20.BackColor = SystemColors.Control : Label20.ForeColor = SystemColors.ControlText
        Return (tot1, tot2)
    End Function
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
                RadioButtonRT0.Checked = True
                RadioButtonRT1.Checked = False
                PictureBox0.Enabled = True
                PictureBox1.Enabled = False
                PictureBox0.BackColor = MyColor.MistyRose
                PictureBox1.BackColor = SystemColors.Control

            Else
                RadioButtonRT1.Checked = True
                RadioButtonRT0.Checked = False
                PictureBox1.Enabled = True
                PictureBox0.Enabled = False
                PictureBox1.BackColor = MyColor.PaleGreen
                PictureBox0.BackColor = SystemColors.Control
            End If
        ElseIf Type = "CP" Then
            Dim r = DGV3.CurrentRow
            Dim j = r.Cells(2).Value
            L_CP.Text = G_foglio.CP(j).TESTO
            TextBox8.Text = G_foglio.CP(j).TOT
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
        If IS_foglio_compilato() Then
            ToolStrip_modifica.Enabled = True
        Else
            ToolStrip_modifica.Enabled = False
        End If

    End Sub



    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        '1- Verifico che tutte le voci siano compilate (non ci siano dei TOT1 =0)

        Dim m = "Vuoi aggiornare il foglio con le modifiche apportate?"
        If MsgBox(m, vbInformation + vbYesNo + vbDefaultButton2) <> vbYes Then Exit Sub

        Dim dbbambno = G_foglio.DB_Bambino
        Dim stato = Foglio_Esistente_Aggiorna(G_foglio, dbbambno)
        Dim m1 = ""
        If stato.OK Then
            m = "Foglio Modificato (" + G_foglio.ID_foglio.ToString + ") di: " + G_foglio.Bambino
            m1 = "Modifiche Registrate Correttamente"
        Else
            m = "[Errore 002] " + stato.Err + "(" + G_foglio.ID_foglio.ToString + ") di: " + G_foglio.Bambino
            APPEND_LOG(m, ini.File_Log, True)
        End If

        Call MsgBox(m, vbOKOnly + vbInformation)
        Call Edit_ONOFF(False)
        ' Ricarico il foglio appena salvato
        Dim j = ComboBox1.SelectedIndex
        Call Mostra_Foglio(j)
        Call Fogliodasalvare(False)


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
            GroupBoxRT.Visible = True
            GroupBox3.Enabled = True
        End If
        Carica_combo_elenco_fogli()
    End Sub


    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Hide()

    End Sub

    Private Sub Label26_Click(sender As Object, e As EventArgs) Handles Label26.Click
        ' Fa apparire il menu esattamente sotto il pulsante
        CMenuStrip1.Show(Label26, 0, Label26.Height)

    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs)
        If Not TSM_Segna_da_compilare.Checked Then
            Edit_ONOFF(True)
            TSM_Segna_da_compilare.Checked = True
            TSM_Segna_da_compilare.Image = ImageList1.Images(8)
        Else
            Edit_ONOFF(False)
            TSM_Segna_da_compilare.Checked = False
            TSM_Segna_da_compilare.Image = Nothing
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
    Private Sub DGV3_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV3.CellClick
        Mostra_totali("CP")
    End Sub

    Private Sub DGV2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV2.CellClick
        Mostra_totali("RT")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Edit_ONOFF(False)
        ' Ricarico il foglio appena salvato
        Dim j = ComboBox1.SelectedIndex
        Call Fogliodasalvare(False)
        Call Mostra_Foglio(j)

    End Sub



    Private Sub ToolStrip_modifica_Click(sender As Object, e As EventArgs) Handles ToolStrip_modifica.Click
        If Not TSM_Segna_da_compilare.Checked Then
            Edit_ONOFF(True)
        Else
            Edit_ONOFF(False)
        End If


    End Sub

    Private Sub TSM_Azzera_Totali_Click(sender As Object, e As EventArgs) Handles TSM_Azzera_Totali.Click
        Dim m = "Vuoi Azzerare tutte le risposte già inserti nel foglio?"
        If MsgBox(m, vbYesNo + vbInformation, "Azzeramento di tutte le Risposte") <> vbYes Then Exit Sub
        m = "Questa operazione NON è reversibile. Vuoi procedere ugualmente?"
        If MsgBox(m, vbYesNo + vbCritical, "Azzeramento di tutte le Risposte") <> vbYes Then Exit Sub
        For Each voce In G_foglio.VociDiUnFoglio
            voce.TOT1 = 0 : voce.TOT1_POS = 0 : voce.TOT1_NEG = 0 : voce.TOT1_PROMPT = 0
            voce.TOT2 = 0 : voce.TOT2_POS = 0 : voce.TOT2_NEG = 0 : voce.TOT2_PROMPT = 0
            voce.COMPILATO = 0
        Next
        For Each voce In G_foglio.RT
            voce.TOT = 0 : voce.COMPILATO = 0
        Next
        For Each voce In G_foglio.CP
            voce.TOT = 0 : voce.COMPILATO = 0
        Next
        Dim dbBambino = G_foglio.DB_Bambino
        Dim stato = Foglio_Esistente_Aggiorna(G_foglio, dbBambino)
        Dim m1 = ""
        If stato.OK Then
            m = "Azzeramento foglio completato " + vbCrLf + "Foglio n.(" + G_foglio.ID_foglio.ToString + ") di: " + G_foglio.Bambino
            Call MsgBox(m, vbOKOnly + vbInformation)
            Exit Sub
        Else
            m = "[Errore 003] " + stato.Err + "(" + G_foglio.ID_foglio.ToString + ") di: " + G_foglio.Bambino
            APPEND_LOG(m, ini.File_Log, True)
            m = "Errore nell'azzeramento delle voci del foglio"
            Call MsgBox(m, vbOKOnly + vbInformation)
        End If
    End Sub

    Private Sub TSM_Segna_da_compilare_Click(sender As Object, e As EventArgs) Handles TSM_Segna_da_compilare.Click
        Dim m = "Questa operazione segnare il foglio come da compilare senza azzerare le risposte già inserte nel foglio" + vbCrLf + "vuoi Continuare ?"
        If MsgBox(m, vbYesNo + vbInformation, "Segna foglio come da compilare") <> vbYes Then Exit Sub
        m = "Questa operazione NON è reversibile. Vuoi procedere ugualmente?"
        If MsgBox(m, vbYesNo + vbCritical, "Segna foglio come da compilare") <> vbYes Then Exit Sub
        For Each voce In G_foglio.VociDiUnFoglio
            voce.COMPILATO = 0
        Next
        For Each voce In G_foglio.RT
            voce.COMPILATO = 0
        Next
        For Each voce In G_foglio.CP
            voce.COMPILATO = 0
        Next
        Dim dbBambino = G_foglio.DB_Bambino
        Dim stato = Foglio_Esistente_Aggiorna(G_foglio, dbBambino)
        Dim m1 = ""
        If stato.OK Then
            m = "Annullamento Compilazione foglio (" + G_foglio.ID_foglio.ToString + ") di: " + G_foglio.Bambino
            Call MsgBox(m, vbOKOnly + vbInformation)
        Else
            m = "[Errore 004] " + stato.Err + "(" + G_foglio.ID_foglio.ToString + ") di: " + G_foglio.Bambino
            APPEND_LOG(m, ini.File_Log, True)
            m1 = "Errore nell'Annullamento Compilazione"
            Call MsgBox(m, vbOKOnly + vbInformation)
        End If
    End Sub

    Private Sub Button_voci_Click(sender As Object, e As EventArgs) Handles Button_voci.Click
        ' Riporta i toltali modificati dlle voci ELCAR nella griglia e in g_foglio
        Dim r = DGV1.CurrentRow
        Dim j = r.Cells(4).Value
        G_foglio.VociDiUnFoglio(j).TOT1_POS = CInt(Val(TextBox1.Text))
        G_foglio.VociDiUnFoglio(j).TOT1_NEG = CInt(Val(TextBox2.Text))
        G_foglio.VociDiUnFoglio(j).TOT1_PROMPT = CInt(Val(TextBox3.Text))
        G_foglio.VociDiUnFoglio(j).TOT1 = G_foglio.VociDiUnFoglio(j).TOT1_PROMPT + G_foglio.VociDiUnFoglio(j).TOT1_POS + G_foglio.VociDiUnFoglio(j).TOT1_NEG
        G_foglio.VociDiUnFoglio(j).TOT2_POS = CInt(Val(TextBox5.Text))
        G_foglio.VociDiUnFoglio(j).TOT2_NEG = CInt(Val(TextBox6.Text))
        G_foglio.VociDiUnFoglio(j).TOT2_PROMPT = CInt(Val(TextBox7.Text))
        G_foglio.VociDiUnFoglio(j).TOT2 = G_foglio.VociDiUnFoglio(j).TOT2_PROMPT + G_foglio.VociDiUnFoglio(j).TOT2_POS + G_foglio.VociDiUnFoglio(j).TOT2_NEG
        r.Cells(2).Value = G_foglio.VociDiUnFoglio(j).TOT1
        r.Cells(3).Value = G_foglio.VociDiUnFoglio(j).TOT2
        DGV1.CurrentRow.Cells(2).Style.BackColor = MyColor.Yellow
        DGV1.CurrentRow.Cells(2).Style.SelectionBackColor = MyColor.Yellow
        DGV1.CurrentRow.Cells(2).Style.SelectionForeColor = MyColor.Black

        DGV1.CurrentRow.Cells(3).Style.BackColor = MyColor.Yellow
        DGV1.CurrentRow.Cells(3).Style.SelectionBackColor = MyColor.Yellow
        DGV1.CurrentRow.Cells(3).Style.SelectionForeColor = MyColor.Black
        DGV1.Refresh()
        Call Fogliodasalvare(True)
    End Sub
    Sub Fogliodasalvare(stato As Boolean)
        If stato Then
            Button5.Enabled = True
            Button5.BackColor = MyColor.PaleGreen
        Else
            Button5.Enabled = False
            Button5.BackColor = SystemColors.Control
        End If
    End Sub
    Sub Mostra_riassunto_fogli_bambno()
        Dim tot = 0, totc = 0, totnc = 0, totaccept = 0, n = -1
        Try
            n = Gbl_elenco_fogli_bambino.Count - 1
        Catch ex As Exception

        End Try
        For i = 0 To n
            tot = tot + 1
            If Gbl_elenco_fogli_bambino(i).Compilato Then
                totc = totc + 1
            Else
                totnc = totnc + 1
            End If
        Next
        L_Nfogli.Text = tot.ToString
        L_foglicompilati.Text = totc.ToString
        L_Fogli_dacompilare.Text = totnc.ToString

    End Sub
    Private Sub F_Foglio_esistente_Load(sender As Object, e As EventArgs) Handles Me.Load
        For Each col As DataGridViewColumn In DGV1.Columns : col.SortMode = DataGridViewColumnSortMode.NotSortable : Next
        For Each col As DataGridViewColumn In DGV2.Columns : col.SortMode = DataGridViewColumnSortMode.NotSortable : Next
        For Each col As DataGridViewColumn In DGV3.Columns : col.SortMode = DataGridViewColumnSortMode.NotSortable : Next


        'Associo piu textbox ad un unico evento
        txtMatrice(0) = TextBox1
        txtMatrice(1) = TextBox2
        txtMatrice(2) = TextBox3
        txtMatrice(3) = TextBox5
        txtMatrice(4) = TextBox6
        txtMatrice(5) = TextBox7
        ' Collega dinamicamente tutte le TextBox della matrice all'unico evento KeyPress
        For Each txt As TextBox In txtMatrice
            AddHandler txt.KeyDown, AddressOf txtMatrice_KeyDown
            AddHandler txt.GotFocus, AddressOf txtMatrice_GotFocus
            AddHandler txt.LostFocus, AddressOf txtMatrice_LostFocus
            AddHandler txt.TextChanged, AddressOf txtMatrice_TextChanged
            AddHandler txt.GotFocus, AddressOf txtMatrice_GotFocus
            AddHandler txt.LostFocus, AddressOf txtMatrice_LostFocus
            AddHandler txt.Enter, AddressOf txtMatrice_TextEnter
        Next
        '61; 85
        Panel_risposte.Visible = False
        Panel_risposte.Top = Tab_option.Top
        Panel_risposte.Left = GroupBox_VOCI.Left
        Call Fogliodasalvare(False)
        ToolStrip_modifica.Enabled = False


    End Sub

    Private Sub TextMatrice_KeyPress(sender As Object, e As KeyPressEventArgs)
        ' Il parametro "sender" rappresenta la TextBox specifica che ha scatenato l'evento
        Dim txtAttiva As TextBox = DirectCast(sender, TextBox)

        ' Esempio: Trova l'indice della TextBox corrente all'interno della matrice
        Dim indice As Integer = Array.IndexOf(txtMatrice, txtAttiva)

        ' --- Inserisci qui la tua logica comune ---
        ' Esempio: Accetta solo numeri
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True ' Blocca il carattere
        End If

        ' Esempio d'uso dell'indice: se premi invio, passa alla TextBox successiva della matrice
        If e.KeyChar = Convert.ToChar(Keys.Enter) Then
            e.Handled = True
            Dim prossimoIndice As Integer = (indice + 1) Mod 6
            txtMatrice(prossimoIndice).Focus()
        End If
    End Sub




    Private Sub txtMatrice_GotFocus(sender As Object, e As EventArgs)
        Dim txtAttiva As TextBox = DirectCast(sender, TextBox)
        If txtAttiva.ReadOnly Then Exit Sub

        txtAttiva.BackColor = MyColor.Yellow

    End Sub
    Private Sub txtMatrice_LostFocus(sender As Object, e As EventArgs)
        Dim txtAttiva As TextBox = DirectCast(sender, TextBox)
        If txtAttiva.ReadOnly Then Exit Sub
        txtAttiva.BackColor = MyColor.WhiteSmoke

    End Sub



    Private Sub txtMatrice_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            ' Impedisce il "bip" acustico di sistema alla pressione di Invio
            e.SuppressKeyPress = True
            Dim tb As TextBox = DirectCast(sender, TextBox)
            If tb.ReadOnly Then Exit Sub
            SpostaAlSuccessivo(tb)
            Dim tot = Mostra_totali_voci_da_testo()
        End If
    End Sub
    Private Sub SpostaAlSuccessivo(currentTextBox As TextBox)
        ' Trova l'indice della TextBox corrente nella tua matrice
        Dim index As Integer = Array.IndexOf(txtMatrice, currentTextBox)

        ' Se esiste ed è rintracciabile, e non è l'ultima TextBox (indice 5)
        If index >= 0 AndAlso index < txtMatrice.Length - 1 Then
            ' Passa il fuoco alla TextBox successiva
            txtMatrice(index + 1).Focus()
        End If
    End Sub
    Private Sub txtMatrice_TextChanged(sender As Object, e As EventArgs)
        Dim tb As TextBox = DirectCast(sender, TextBox)
        If tb.ReadOnly Then Exit Sub

        ' Se sono stati digitati almeno 2 caratteri, sposta il fuoco
        If tb.Text.Length >= 2 Then
            SpostaAlSuccessivo(tb)
        End If
    End Sub

    Private Sub txtMatrice_TextEnter(sender As Object, e As EventArgs)
        Dim txtAttiva As TextBox = DirectCast(sender, TextBox)

        txtAttiva.BeginInvoke(New Action(Sub()
                                             txtAttiva.SelectAll()
                                         End Sub))
    End Sub

    Private Sub Button_CP_Click(sender As Object, e As EventArgs) Handles Button_CP.Click
        Dim r = DGV3.CurrentRow
        Dim j = r.Cells(2).Value
        G_foglio.CP(j).TOT = CInt(Val(TextBox8.Text))
        r.Cells(1).Value = G_foglio.CP(j).TOT
        DGV3.CurrentRow.Cells(1).Style.BackColor = MyColor.Yellow
        DGV3.CurrentRow.Cells(1).Style.SelectionBackColor = MyColor.Yellow
        DGV3.CurrentRow.Cells(1).Style.SelectionForeColor = MyColor.Black
        Call Fogliodasalvare(True)

    End Sub

    Private Sub Button_RT_Click(sender As Object, e As EventArgs) Handles Button_RT.Click
        Dim r = DGV2.CurrentRow
        Dim j = r.Cells(2).Value
        Dim img0 = PictureBox0.Image
        Dim img1 = PictureBox1.Image
        If RadioButtonRT0.Checked Then
            r.Cells(3).Value = 0
            r.Cells(1).Value = img0
            G_foglio.RT(j).TOT = 0
        ElseIf RadioButtonRT1.Checked Then
            r.Cells(3).Value = 1
            r.Cells(1).Value = img1
            G_foglio.RT(j).TOT = 1
        End If
        DGV2.CurrentRow.Cells(1).Style.BackColor = MyColor.Yellow
        DGV2.CurrentRow.Cells(1).Style.SelectionBackColor = MyColor.Yellow
        DGV2.CurrentRow.Cells(1).Style.SelectionForeColor = MyColor.Black
        Call Fogliodasalvare(True)


    End Sub
    Private Sub RadioButtonRT1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonRT1.CheckedChanged
        If RadioButtonRT0.Checked Then
            PictureBox0.BackColor = MyColor.MistyRose
            PictureBox1.BackColor = SystemColors.Control
        Else
            PictureBox1.BackColor = MyColor.PaleGreen
            PictureBox0.BackColor = SystemColors.Control
        End If
    End Sub
End Class
