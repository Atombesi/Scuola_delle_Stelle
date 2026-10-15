Imports System.Windows
Imports System.Windows.Documents
Imports System.Windows.Media

Public Class F_Compilazione

    Public Structure Struc_compilazione
        Dim ELCAR_compilato As Boolean
        Dim ELCAR_errore As String
        Dim RT_compilato As Boolean
        Dim RT_errore As String
        Dim CP_compilato As Boolean
        Dim CP_errore As String
        Dim Salvato As Boolean
        Dim Salvabile As Boolean
    End Structure
    Public Gb_Stato_compilazione As Struc_compilazione


    Private textBoxes() As TextBox

    Dim gb_top_Routine_elcar_off
    Dim gb_top_Routine_ELCAR_on
    Dim gb_top_cp_ROUTINE_off
    Dim gb_top_CP_ROUTINE_on
    Dim PANEL_HIGHT_ON = 288
    Dim gb_DGV_Rindex = -1

    Sub init_panel()
        Dim top = 55, left = 13
        top = Panel_ELCAR.Top
        left = Panel_ELCAR.Left
        Panel_CP.Left = left
        Panel_ROUTINE.Left = left

        ' condizione iniziale
        Panel_ELCAR.Height = 25
        Panel_ELCAR.Left = 9
        GroupBox_ELCAR.Left = 9

        Panel_CP.Height = Panel_ELCAR.Height
        Panel_CP.Left = Panel_ELCAR.Left

        Panel_ROUTINE.Height = Panel_ELCAR.Height
        Panel_ROUTINE.Left = Panel_ELCAR.Left

        GroupBox_CP.Left = GroupBox_ELCAR.Left
        GroupBox_Routine.Left = GroupBox_ELCAR.Left

        GroupBox_CP.Visible = False
        GroupBox_ELCAR.Visible = False
        GroupBox_Routine.Visible = False


        gb_top_Routine_ELCAR_on = 350
        gb_top_Routine_elcar_off = 86
        gb_top_CP_ROUTINE_on = 380
        gb_top_cp_ROUTINE_off = 118

        'GroupBox_CP.Top = 147
        'GroupBox_Routine.Top = top


    End Sub

    Function Stato_compilazione() As Struc_compilazione
        Dim stato As New Struc_compilazione()
        stato.ELCAR_compilato = Conta_voci_compilate_ELCAR().Successo
        stato.RT_compilato = Conta_voci_compilate_ROUTINE().Successo
        stato.CP_compilato = Conta_voci_compilate_CP().Successo

        stato.Salvabile = stato.ELCAR_compilato And stato.RT_compilato And stato.CP_compilato

        Return stato
    End Function

    Sub Panel_setting(testo As String)
        Label_ELCAR1.BackColor = MyColor.Transparent
        Label_ROUTINE1.BackColor = MyColor.Transparent
        Label_CP1.BackColor = MyColor.Transparent
        Panel_ROUTINE.Height = 25
        Panel_CP.Height = 25
        Panel_ELCAR.Height = 25
        GroupBox_CP.Visible = False
        GroupBox_Routine.Visible = False
        GroupBox_ELCAR.Visible = False
        Panel_ELCAR.Visible = False
        Panel_ROUTINE.Visible = False
        Panel_CP.Visible = False

        Dim TOP = Panel0.Top + Panel0.Height + 5

        Panel_ELCAR.Top = TOP
        Panel_ROUTINE.Top = TOP
        Panel_CP.Top = TOP

        If Gb_Stato_compilazione.Salvabile Then
            Button5.Enabled = True
            Button5.BackColor = MyColor.LightGreen
        Else
            Button5.Enabled = False
            Button5.BackColor = Button3.Backcolor
        End If


        If testo = "ELCAR" Then
            Label_ELCAR1.BackColor = MyColor.LightGreen
            Panel_ELCAR.Height = PANEL_HIGHT_ON
            Panel_ELCAR.Visible = True

            ' Posizione Groupbox ELCAR
            GroupBox_ELCAR.Top = Panel_ELCAR.Top + Panel_ELCAR.Height + 5
            GroupBox_ELCAR.Visible = True
            GroupBox_ELCAR.Refresh()
            Panel_ELCAR.Visible = True
        ElseIf testo = "ROUTINE" Then
            Label_ROUTINE1.BackColor = MyColor.LightGreen
            Panel_ROUTINE.Height = PANEL_HIGHT_ON
            Panel_ROUTINE.Visible = True

            ' Posizione Groupbox Routine
            GroupBox_Routine.Top = Panel_ROUTINE.Top + Panel_ROUTINE.Height + 5
            GroupBox_Routine.Visible = True
            GroupBox_Routine.Refresh()

        ElseIf testo = "CP" Then
            Label_CP1.BackColor = MyColor.LightGreen
            Panel_CP.Visible = True

            ' Posizione Groupbox CP
            GroupBox_CP.Top = Panel_CP.Top + Panel_CP.Height + 5
            GroupBox_CP.Visible = True
            GroupBox_CP.Refresh()

        End If
    End Sub



    Private Sub F_Compilazione_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        init_panel()

        textBoxes = New TextBox() {T_pos1, T_NEG1, T_PROMPT1, T_POS2, T_NEG2, T_PROMPT2}

        ' 2. Associa dinamicamente gli eventi a tutti i TextBox dell'array
        For Each txt As TextBox In textBoxes
            AddHandler txt.KeyDown, AddressOf TextBox_KeyDown
            AddHandler txt.Enter, AddressOf TextBox_Enter
            AddHandler txt.Leave, AddressOf TextBox_Leave
        Next

        Gbl_VociDiTodolist = Class_TODOLIST.LoadAll()
        Carica_combo_fogli_da_compilare()

        Dim x = 0
    End Sub

    Sub Carica_combo_fogli_da_compilare()




        Gbl_VociDiTodolist = Class_TODOLIST.LoadAll()


        ComboBox1.DataSource = Nothing

        ' 2. Imposta il DisplayMember
        ComboBox1.DisplayMember = "NOMEFILE"

        ' 3. Assegna la lista reale
        ComboBox1.DataSource = Gbl_VociDiTodolist


        ComboBox1.SelectedIndex = 0
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Hide()
    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label_ELCAR.Click, Label_CP.Click, Label_ROUTINE.Click, Label_ELCAR1.Click, Label_ROUTINE1.Click, Label_CP1.Click
        Dim label = CType(sender, Label)
        Panel_setting(label.Tag)
        T_pos1.Focus()

    End Sub
    Function Conta_voci_compilate_ELCAR() As (Successo As Boolean, Count As Integer)

        Dim n_voci = 0
        Try
            n_voci = Gbl_foglio.VociDiUnFoglio.Count
        Catch ex As Exception
            Return (False, 0)
        End Try


        Dim CNT = 0, Successo = False
        For Each voce In Gbl_foglio.VociDiUnFoglio
            If voce.COMPILATO Then
                CNT += 1
            End If
        Next
        Label16.Text = CNT & "/" & n_voci
        Label20.Text = Label16.Text

        If CNT = n_voci Then
            Label6.ImageIndex = 8
            Successo = True
        Else
            Label6.ImageIndex = 7
            Successo = False
        End If

        Gb_Stato_compilazione.ELCAR_compilato = Successo
        Gb_Stato_compilazione.Salvabile = Gb_Stato_compilazione.ELCAR_compilato And Gb_Stato_compilazione.RT_compilato And Gb_Stato_compilazione.CP_compilato
        controlla_se_da_salvare()
        Return (Successo, CNT)

    End Function
    Function Conta_voci_compilate_ROUTINE() As (Successo As Boolean, Count As Integer)
        Dim n_voci = Gbl_foglio.RT.Count
        Dim CNT = 0, Successo = False

        For Each voce In Gbl_foglio.RT
            If voce.COMPILATO Then
                CNT += 1
            End If
        Next
        Label7.Text = CNT & "/" & n_voci
        Label34.Text = Label7.Text

        If CNT = n_voci Then
            Label9.ImageIndex = 8
            Successo = True
        Else
            Label9.ImageIndex = 7
            Successo = False
        End If

        Gb_Stato_compilazione.RT_compilato = Successo
        Gb_Stato_compilazione.Salvabile = Gb_Stato_compilazione.ELCAR_compilato And Gb_Stato_compilazione.RT_compilato And Gb_Stato_compilazione.CP_compilato
        controlla_se_da_salvare()
        Return (Successo, CNT)
    End Function

    Function Conta_voci_compilate_CP() As (Successo As Boolean, Count As Integer)
        Dim n_voci = Gbl_foglio.CP.Count
        Dim CNT = 0
        For Each voce In Gbl_foglio.CP
            If voce.COMPILATO Then
                CNT += 1
            End If
        Next
        Label10.Text = CNT & "/" & n_voci
        Label38.Text = Label10.Text


        Dim successo = False
        If CNT = n_voci Then
            Label17.ImageIndex = 8
            successo = True
        Else
            Label17.ImageIndex = 7
        End If

        Gb_Stato_compilazione.CP_compilato = successo
        Gb_Stato_compilazione.Salvabile = Gb_Stato_compilazione.ELCAR_compilato And Gb_Stato_compilazione.RT_compilato And Gb_Stato_compilazione.CP_compilato

        controlla_se_da_salvare()
        Return (successo, CNT)


    End Function
    Sub CARICA_GRID_ELCAR()
        DGV_ELCAR.DataSource = Nothing
        DGV_ELCAR.AllowUserToAddRows = False
        Dim img_Index = 7, CNT = 0, R = 0
        Dim i0 = -1, COMPILATO = 0

        Panel0.BackColor = MyColor.Transparent
        If Gbl_foglio.VociDiUnFoglio.Count = 0 Then
            Panel0.BackColor = MyColor.Yellow
            Exit Sub
        End If


        For i As Integer = 0 To Gbl_foglio.VociDiUnFoglio.Count - 1
            CNT = CNT + 1
            If Gbl_foglio.VociDiUnFoglio(i).COMPILATO Then
                img_Index = 8
            Else
                img_Index = 7
                If i0 = -1 Then i0 = i
            End If
            COMPILATO = CInt(Val(Gbl_foglio.VociDiUnFoglio(i).COMPILATO))
            DGV_ELCAR.Rows.Add(ImageList1.Images(img_Index), CNT, Gbl_foglio.VociDiUnFoglio(i).Testo, Gbl_foglio.VociDiUnFoglio(i).TOT1, Gbl_foglio.VociDiUnFoglio(i).TOT2, i, COMPILATO)
            Dim ii = DGV_ELCAR.CurrentRow.Index
            SET_DGV_ELCAR_STATO(i)
        Next
        DGV_ELCAR.CurrentCell = DGV_ELCAR.Rows(i0).Cells(0)
        DGV_ELCAR.Rows(i0).Selected = True
    End Sub
    Sub SET_DGV_ELCAR_STATO(i)
        Dim compilato = CInt(DGV_ELCAR.Rows(i).Cells(8).Value)
        If compilato <> 1 Then
            DGV_ELCAR.Rows(i).Cells(0).Value = ImageList1.Images(7)
            Exit Sub
        End If
        Dim tot = CInt(DGV_ELCAR.Rows(i).Cells(7).Value)
        If tot = 0 Then
            DGV_ELCAR.Rows(i).Cells(0).Value = ImageList1.Images(2) ' pallino rosso
            Exit Sub
        ElseIf tot = 20 Then
            DGV_ELCAR.Rows(i).Cells(0).Value = ImageList1.Images(8) ' Ok
        Else
            DGV_ELCAR.Rows(i).Cells(0).Value = ImageList1.Images(5) ' pallino giallo
        End If
        DGV_ELCAR.Refresh()

    End Sub
    Sub SET_DGV_ROUTINE_STATO(i)
        Dim compilato = CInt(DGV_ROUTINE.Rows(i).Cells(5).Value)
        If compilato <> 1 Then
            DGV_ROUTINE.Rows(i).Cells(0).Value = ImageList1.Images(7)
        Else
            DGV_ROUTINE.Rows(i).Cells(0).Value = ImageList1.Images(8) ' Ok
        End If
        DGV_ROUTINE.Refresh()

        Exit Sub

    End Sub



    Sub carica_voci_cp()
        Dim nv = Gbl_foglio.CP.Count
        P_CP1.Visible = False
        P_CP2.Visible = False
        P_CP3.Visible = False
        Panel3.BackColor = MyColor.Transparent

        If nv = 0 Then
            Panel3.BackColor = MyColor.Yellow
            Exit Sub

        End If

        If nv > 0 Then
            L_CP1.Text = Gbl_foglio.CP(0).TESTO
            T_CP1.Text = Gbl_foglio.CP(0).TOT
            P_CP1.Visible = True
        End If

        If nv > 1 Then
            L_CP2.Text = Gbl_foglio.CP(1).TESTO
            T_CP2.Text = Gbl_foglio.CP(1).TOT
            P_CP2.Visible = True

        End If

        If nv > 2 Then
            L_CP3.Text = Gbl_foglio.CP(2).TESTO
            T_CP3.Text = Gbl_foglio.CP(2).TOT
            P_CP3.Visible = True

        End If



    End Sub
    Sub CARICA_GRID_ROUTINE()
        DGV_ROUTINE.DataSource = Nothing
        DGV_ROUTINE.AllowUserToAddRows = False
        Panel2.BackColor = MyColor.Transparent
        If Gbl_foglio.RT.Count = 0 Then
            Panel2.BackColor = MyColor.Yellow
            Exit Sub
        End If
        Dim img_Index = 7, CNT = 0, R = 0
        Dim i0 = -1
        For i As Integer = 0 To Gbl_foglio.RT.Count - 1
            CNT = CNT + 1
            If Gbl_foglio.RT(i).COMPILATO Then
                img_Index = 8
            Else
                img_Index = 7
                If i0 = -1 Then i0 = i
            End If
            DGV_ROUTINE.Rows.Add(ImageList1.Images(img_Index), CNT, Gbl_foglio.RT(i).TESTO, Gbl_foglio.RT(i).TOT, i)
        Next

        ' mi posiziono sul primo non compilato
        DGV_ROUTINE.CurrentCell = DGV_ROUTINE.Rows(i0).Cells(0)
        DGV_ROUTINE.Rows(i0).Selected = True
    End Sub

    Sub Mostra(Optional modo = "*")
        If modo = "*" Or modo = "ELCAR" Then
            Conta_voci_compilate_ELCAR()
            CARICA_GRID_ELCAR()
        End If

        If modo = "*" Or modo = "ROUTINE" Then
            Conta_voci_compilate_ROUTINE()
            CARICA_GRID_ROUTINE()
        End If

        If modo = "*" Or modo = "CP" Then
            carica_voci_cp()
            Conta_voci_compilate_CP()
        End If
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim id_foglio = 0
        Dim G_DB_Bambino = ComboBox1.SelectedItem.db_bambino
        id_foglio = ComboBox1.SelectedItem.id_foglio
        Gbl_foglio = Carica_foglio(id_foglio, G_DB_Bambino)
        Dim nv = 0
        Try
            nv = Gbl_foglio.VociDiUnFoglio.Count
        Catch ex As Exception
            Exit Sub
        End Try


        Label2.Text = Gbl_foglio.ID_foglio
        Label3.Text = Gbl_foglio.Data
        Call Mostra()
        Panel_setting("ELCAR")
        T_pos1.Focus()


    End Sub

    Private Sub TextBox_Enter(sender As Object, e As EventArgs)
        Dim currentTextBox As TextBox = CType(sender, TextBox)
        currentTextBox.BackColor = MyColor.LightYellow
        'If currentTextBox.Text = 0 Then TextBox1.SelectAll()
    End Sub

    ' Ripristina lo sfondo originale (bianco) quando il TextBox perde il fuoco
    Private Sub TextBox_Leave(sender As Object, e As EventArgs)
        Dim currentTextBox As TextBox = CType(sender, TextBox)
        currentTextBox.BackColor = System.Drawing.SystemColors.Window
    End Sub
    Private Sub TextBox_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode <> Keys.Enter Then Exit Sub
        Dim tot = 0
        ' Impedisce il "bip" acustico predefinito di Windows alla pressione di Invio
        e.SuppressKeyPress = True

        Dim currentTextBox As TextBox = CType(sender, TextBox)
        Dim currentIndex As Integer = Array.IndexOf(textBoxes, currentTextBox)

        If currentIndex <= 2 Then ' sono nei totali 1
            tot = CInt(Val(T_pos1.Text)) + CInt(Val(T_NEG1.Text)) + CInt(Val(T_PROMPT1.Text))
            Label15.Text = tot.ToString
        Else
            Try
                tot = CInt(Val(T_POS2.Text)) + CInt(Val(T_NEG2.Text)) + CInt(Val(T_PROMPT2.Text))
            Catch ex As Exception
                tot = 0
            End Try
            Label21.Text = tot.ToString
        End If


        ' Calcola l'indice successivo in modo ciclico (da 7 torna a 0)
        Dim nextIndex As Integer = (currentIndex + 1) Mod textBoxes.Length

        ' Verifica se devo saltare la riga (non ho scritto nulla in pos2
        If currentIndex = 3 And T_POS2.Text = "" Then
            Dim Completata = Riporta_totali_ELCAR()
            If Completata Then Panel_setting("ROUTINE")
            Focus_to_next_empy_elcar_row()
            Exit Sub
        End If

        If currentIndex = 5 Then
            Dim Completata = Riporta_totali_ELCAR()
            If Completata Then Panel_setting("ROUTINE")
            Focus_to_next_empy_elcar_row()
            Exit Sub
        End If

        ' Sposta il fuoco sul controllo successivo
        If Not textBoxes(nextIndex).ReadOnly Then
            textBoxes(nextIndex).Focus()
        Else
            Focus_to_next_empy_elcar_row()
        End If
    End Sub

    Function Focus_to_next_empy_elcar_row() As Integer
        Dim n As Integer = DGV_ELCAR.Rows.Count, V = 0

        ' Se la griglia è vuota, ritorna -1
        If n = 0 Then Return -1
        Dim currentRowIndex As Integer = DGV_ELCAR.CurrentRow.Index
        ' Il ciclo gira esattamente N volte per controllare tutte le righe
        For i As Integer = 1 To n
            ' Calcola l'indice da controllare partendo dalla riga successiva (currentRowIndex + i)
            ' L'operatore Mod riporta l'indice a 0 quando si supera la fine della griglia
            Dim checkIndex As Integer = (currentRowIndex + 1) Mod n
            V = DGV_ELCAR.Rows(checkIndex).Cells(8).Value
            ' Evita l'errore se la cella o il valore sono nulli
            If V = 0 Then
                DGV_ELCAR.CurrentCell = DGV_ELCAR.Rows(checkIndex).Cells(0)
                Return i
            End If
        Next

        ' Se nessuna riga contiene "X", ritorna -1
        Return -1

    End Function

    Function Focus_to_next_empy_routine_row() As Integer
        Dim n As Integer = DGV_ROUTINE.Rows.Count, Compilato = 0

        ' Se la griglia è vuota, ritorna -1
        If n = 0 Then Return -1
        Dim currentRowIndex As Integer = DGV_ROUTINE.CurrentRow.Index
        ' Il ciclo gira esattamente N volte per controllare tutte le righe
        For i As Integer = 1 To n
            ' Calcola l'indice da controllare partendo dalla riga successiva (currentRowIndex + i)
            ' L'operatore Mod riporta l'indice a 0 quando si supera la fine della griglia
            Dim checkIndex As Integer = (currentRowIndex + 1) Mod n
            Compilato = DGV_ROUTINE.Rows(checkIndex).Cells(5).Value  ' compilato
            ' Evita l'errore se la cella o il valore sono nulli
            If Compilato = 0 Then
                DGV_ROUTINE.CurrentCell = DGV_ROUTINE.Rows(checkIndex).Cells(0)
                Return i
            End If
        Next

        ' Se nessuna riga contiene "X", ritorna -1
        Return -1

    End Function

    Function Focus_to_next_empy_CP() As Integer

        Dim n As Integer = Gbl_foglio.CP.Count, compilato = 0

        ' Se la griglia è vuota, ritorna -1
        If n = 0 Then Return -1
        Dim currentRowIndex As Integer = CInt(Val(GroupBox_CP.Tag))

        ' Il ciclo gira esattamente N volte per controllare tutte le righe
        For i As Integer = 1 To n
            ' Calcola l'indice da controllare partendo dalla riga successiva (currentRowIndex + i)
            ' L'operatore Mod riporta l'indice a 0 quando si supera la fine della griglia
            Dim checkIndex As Integer = (currentRowIndex + 1) Mod n
            compilato = Gbl_foglio.CP(checkIndex).COMPILATO  ' compilato
            ' Evita l'errore se la cella o il valore sono nulli
            If compilato = 0 Then
                If i = 0 Then
                    T_CP1.Focus()
                ElseIf i = 1 Then
                    T_CP2.Focus()
                ElseIf i = 2 Then
                    T_CP3.Focus()
                End If
                Return i
            End If
        Next

        ' Se nessuna riga contiene "X", ritorna -1
        Return -1



    End Function

    Function Riporta_totali_ELCAR() As Boolean
        ' carica i valori inseriti nel foglio
        ' ritorna true se tutte le voci ELCAR sono state compilate
        Dim i = T_pos1.Tag
        Dim TOT = 0, TOTTOT = 0
        Gbl_foglio.VociDiUnFoglio(i).TOT1_POS = CInt(Val(T_pos1.Text))
        Gbl_foglio.VociDiUnFoglio(i).TOT1_NEG = CInt(Val(T_NEG1.Text))
        Gbl_foglio.VociDiUnFoglio(i).TOT1_PROMPT = CInt(Val(T_PROMPT1.Text))
        Gbl_foglio.VociDiUnFoglio(i).TOT2_POS = CInt(Val(T_POS2.Text))
        Gbl_foglio.VociDiUnFoglio(i).TOT2_NEG = CInt(Val(T_NEG2.Text))
        Gbl_foglio.VociDiUnFoglio(i).TOT2_PROMPT = CInt(Val(T_PROMPT2.Text))
        Gbl_foglio.VociDiUnFoglio(i).COMPILATO = True
        TOT = Gbl_foglio.VociDiUnFoglio(i).TOT1_POS + Gbl_foglio.VociDiUnFoglio(i).TOT1_NEG + Gbl_foglio.VociDiUnFoglio(i).TOT1_PROMPT
        TOTTOT = TOT
        Gbl_foglio.VociDiUnFoglio(i).TOT1 = TOT
        TOT = Gbl_foglio.VociDiUnFoglio(i).TOT2_POS + Gbl_foglio.VociDiUnFoglio(i).TOT2_NEG + Gbl_foglio.VociDiUnFoglio(i).TOT2_PROMPT
        Gbl_foglio.VociDiUnFoglio(i).TOT2 = TOT
        TOTTOT = TOTTOT + TOT
        'aggiorna la DGV e prepara per i successivi
        DGV_ELCAR.Rows(i).Cells(3).Value = Gbl_foglio.VociDiUnFoglio(i).TOT1
        DGV_ELCAR.Rows(i).Cells(4).Value = Gbl_foglio.VociDiUnFoglio(i).TOT2
        DGV_ELCAR.Rows(i).Cells(7).Value = TOTTOT
        DGV_ELCAR.Rows(i).Cells(8).Value = 1 ' compilato
        SET_DGV_ELCAR_STATO(i)
        Dim result = Conta_voci_compilate_ELCAR()
        Return result.Successo

    End Function
    Function Riporta_totali_ROUTINE() As Boolean
        ' carica i valori inseriti nel foglio
        ' ritorna true se tutte le voci ELCAR sono state compilate
        Dim i = TextBox1.Tag
        Dim TOT = 0, TOTTOT = 0
        Gbl_foglio.RT(i).TOT = CInt(Val(TextBox1.Text))
        Gbl_foglio.RT(i).COMPILATO = 1
        'aggiorna la DGV e prepara per i successivi
        DGV_ROUTINE.Rows(i).Cells(3).Value = Gbl_foglio.RT(i).TOT
        DGV_ROUTINE.Rows(i).Cells(5).Value = 1 ' compilato
        SET_DGV_ROUTINE_STATO(i)
        Dim result = Conta_voci_compilate_ROUTINE()
        Return result.Successo

    End Function

    Function Riporta_totali_CP(i) As Boolean
        ' carica i valori inseriti nel foglio
        ' ritorna true se tutte le voci CP sono state compilate


        If i = 1 Then
            Gbl_foglio.CP(0).TOT = CInt(Val(T_CP1.Text))
            Gbl_foglio.CP(0).COMPILATO = 1
        ElseIf i = 2 Then
            Gbl_foglio.CP(1).TOT = CInt(Val(T_CP2.Text))
            Gbl_foglio.CP(1).COMPILATO = 1

        ElseIf i = 3 Then
            Gbl_foglio.CP(2).TOT = CInt(Val(T_CP3.Text))
            Gbl_foglio.CP(2).COMPILATO = 1
        End If

        Dim tot_cp = 0

        Dim result = Conta_voci_compilate_CP()
        If Gb_Stato_compilazione.Salvabile Then

        End If
        Return result.Successo

    End Function

    Sub Carica_totali_elcar(i, n)
        If i >= Gbl_foglio.VociDiUnFoglio.Count Then
            i = 0
            n = DGV_ELCAR.Rows(0).Cells(1).Value
        End If
        If Gbl_foglio.VociDiUnFoglio(i).COMPILATO Then
            T_pos1.Text = Gbl_foglio.VociDiUnFoglio(i).TOT1_POS
            T_NEG1.Text = Gbl_foglio.VociDiUnFoglio(i).TOT1_NEG
            T_PROMPT1.Text = Gbl_foglio.VociDiUnFoglio(i).TOT1_PROMPT
            T_POS2.Text = Gbl_foglio.VociDiUnFoglio(i).TOT2_POS
            T_NEG2.Text = Gbl_foglio.VociDiUnFoglio(i).TOT2_NEG
            T_PROMPT2.Text = Gbl_foglio.VociDiUnFoglio(i).TOT2_PROMPT
        Else
            T_pos1.Text = ""
            T_NEG1.Text = ""
            T_PROMPT1.Text = ""
            T_POS2.Text = ""
            T_NEG2.Text = ""
            T_PROMPT2.Text = ""
        End If
        Label28.Text = Gbl_foglio.VociDiUnFoglio(i).Testo
        Label29.Text = "Voce n. " + n.ToString
        T_pos1.Tag = i
        T_pos1.Focus()
    End Sub
    Sub Carica_totali_routine(i, n)
        If i >= Gbl_foglio.RT.Count Then
            i = 0
            n = DGV_ROUTINE.Rows(0).Cells(1).Value
        End If
        If Gbl_foglio.RT(i).COMPILATO Then
            TextBox1.Text = Gbl_foglio.RT(i).TOT
        Else
            TextBox1.Text = ""
        End If
        TextBox1.Refresh()
        Label30.Text = Gbl_foglio.RT(i).TESTO
        Label31.Text = "Voce n. " + n.ToString
        TextBox1.Tag = i
        TextBox1.Focus()
    End Sub
    Sub Carica_totali_CP(i)
        Dim l = "", t = 0
        l = Gbl_foglio.CP(i).TESTO
        t = Gbl_foglio.CP(i).TOT
        If i = 1 Then
            L_CP1.Text = l
            T_CP1.Text = t
            T_CP1.Focus()
        ElseIf i = 2 Then
            L_CP2.Text = l
            T_CP2.Text = t
            T_CP2.Focus()
        ElseIf i = 3 Then
            L_CP3.Text = l
            T_CP3.Text = t
            T_CP3.Focus()
        End If
    End Sub


    Private Sub DGV_ELCAR_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DGV_ELCAR.RowEnter
        Dim i = e.RowIndex
        If i >= Gbl_foglio.VociDiUnFoglio.Count Then
            i = Focus_to_next_empy_elcar_row()
        End If
        Dim n = DGV_ELCAR.Rows(i).Cells(1).Value
        Carica_totali_elcar(i, n)

    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Focus_to_next_empy_elcar_row()
    End Sub

    Private Sub DGV_ROUTINE_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DGV_ROUTINE.RowEnter
        Dim i = e.RowIndex
        If i >= Gbl_foglio.RT.Count Then
            i = Focus_to_next_empy_routine_row()
        End If
        Dim n = DGV_ROUTINE.Rows(i).Cells(1).Value
        Carica_totali_routine(i, n)

    End Sub

    Private Sub TextBox1_Enter(sender As Object, e As EventArgs) Handles TextBox1.Enter
        Dim currentTextBox As TextBox = CType(sender, TextBox)
        currentTextBox.BackColor = MyColor.LightYellow

    End Sub

    Private Sub TextBox1_Leave(sender As Object, e As EventArgs) Handles TextBox1.Leave
        Dim currentTextBox As TextBox = CType(sender, TextBox)
        currentTextBox.BackColor = System.Drawing.SystemColors.Window


    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode <> Keys.Enter Then Exit Sub
        Dim tot = 0
        ' Impedisce il "bip" acustico predefinito di Windows alla pressione di Invio
        e.SuppressKeyPress = True

        Dim currentTextBox As TextBox = CType(sender, TextBox)

        Dim Completata = Riporta_totali_ROUTINE()
        If Completata Then
            Panel_setting("CP")
            Focus_to_next_empy_CP()
            Exit Sub

        End If
        Focus_to_next_empy_routine_row()


    End Sub
    Function controlla_se_da_salvare() As Boolean
        If Gb_Stato_compilazione.Salvabile Then
            Button5.Enabled = True
            Button5.BackColor = MyColor.LightGreen
        Else
            Button5.Enabled = False
        End If
        Return Gb_Stato_compilazione.Salvabile
    End Function

    Private Sub T_CP1_Enter(sender As Object, e As EventArgs) Handles T_CP1.Enter, T_CP2.Enter, T_CP3.Enter
        Dim currentTextBox As TextBox = CType(sender, TextBox)
        currentTextBox.BackColor = MyColor.LightYellow

    End Sub

    Private Sub T_CP1_Leave(sender As Object, e As EventArgs) Handles T_CP1.Leave, T_CP2.Leave, T_CP3.Leave
        Dim currentTextBox As TextBox = CType(sender, TextBox)
        currentTextBox.BackColor = System.Drawing.SystemColors.Window

    End Sub



    Private Sub T_CP_KeyDown(sender As Object, e As KeyEventArgs) Handles T_CP1.KeyDown, T_CP2.KeyDown, T_CP3.KeyDown

        If e.KeyCode <> Keys.Enter Then Exit Sub
        Dim tot = 0
        ' Impedisce il "bip" acustico predefinito di Windows alla pressione di Invio
        e.SuppressKeyPress = True

        Dim currentTextBox As TextBox = CType(sender, TextBox)
        Dim i = Val(currentTextBox.Tag)
        GroupBox_CP.Tag = i
        Dim Completata = Riporta_totali_CP(i)
        If Completata Then
            controlla_se_da_salvare()
            Exit Sub

        End If
        Focus_to_next_empy_CP()

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

        Dim m = "Vuoi Pulire e aggiornare l'elenco dei fogli da compilare?"
        If MsgBox(m, vbYesNo + vbQuestion, "") <> vbYes Then Exit Sub
        Call Todolist_Azzera_e_Ricrea()
        Gbl_VociDiTodolist = Class_TODOLIST.LoadAll()
        Carica_combo_fogli_da_compilare()

    End Sub
End Class