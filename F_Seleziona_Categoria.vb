
Imports System.Runtime.InteropServices.JavaScript.JSType
Imports DocumentFormat.OpenXml
Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Spreadsheet
Imports DocumentFormat.OpenXml.Wordprocessing
Imports MyColor = System.Drawing.Color
Public Class F_Seleziona_Categoria
    Public UltimoFoglioGiornaliero_Inserito As Integer
    Public Giornaliero As New Class_GIORNALIERO

    Private Sub F_Crea_Scheda_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim n = UBound(ini.ELCAR_LETTERE)
        ReDim Preserve CODICI_ELCAR(n)
        ReDim cod(7)
        ComboBox1.Items.Clear()
        For j = 0 To n - 1
            ComboBox1.Items.Add(CODICI_ELCAR(j))
        Next
        If Label9.Text = "" Or Gbl_DB_BAMBINO = "" Then
            Call Seleziona_bambino()

        End If

    End Sub
    Sub Azzera_totali_voci_combo(Optional start As Integer = 1)
        If start <= 3 Then Label12.Text = "Tot 0 voci" : Label12.Refresh()
        If start <= 4 Then Label7.Text = "Tot 0 voci" : Label7.Refresh()
        If start <= 5 Then Label16.Text = "Tot 0 voci" : Label16.Refresh()
        If start <= 6 Then Label17.Text = "Tot 0 voci" : Label17.Refresh()
    End Sub
    Sub Seleziona_bambino()
        Dim jb = F_Seleziona_Bambino.Mostra()
        If jb < 0 Then
            Call MsgBox("Bambino non selezionato")
            Exit Sub
        End If

        Gbl_NOME_BAMBINO = Gbl_Elenco_bambini(jb).DBname.ToUpper
        If InStr(Gbl_DB_BAMBINO, ".DB") = 0 Then Gbl_DB_BAMBINO = Gbl_NOME_BAMBINO + ".DB"

        Label9.Text = Gbl_Elenco_bambini(jb).Cognome + " " + Gbl_Elenco_bambini(jb).Nome : Label9.Refresh()
        Label9.Tag = jb

        Dim filedb = IO.Path.Combine(ini.DB_Bambini_path, Gbl_DB_BAMBINO).ToUpper
        If Not IO.File.Exists(filedb) Then
            Dim m = "Il database" + Gbl_DB_BAMBINO + vbCrLf + "NON è stato trovato"
            m = m + "Vuoi crearlo?"
            If MsgBox(m, vbYesNo) <> vbYes Then
                Label9.Text = "" : Label9.Refresh()
                Label9.Tag = -1
                Exit Sub
            End If
            Crea_DB_Bimbo(filedb)
        End If

        ' Se non essite la directory delle schede la crea
        Gbl_SCHEDA_BAMBINO = ini.Path_SCHEDE_DaCompilare
        If Not IO.Directory.Exists(Gbl_SCHEDA_BAMBINO) Then
            IO.Directory.CreateDirectory(Gbl_SCHEDA_BAMBINO)
        End If


        Gbl_UltimofoglioGiornaliero_inserito = Class_GIORNALIERO.Trova_ultimo_IDFOGLIO
        Label13.Text = Gbl_UltimofoglioGiornaliero_inserito + 1

        Dim risultati1 As List(Of Class_GIORNALIERO) = Class_GIORNALIERO.CercaPerCodice("")

        Trova_tutti_Fogli_bambino()
        If risultati1.Count = 0 Then Return


    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim index = ComboBox1.SelectedIndex
        Dim y = ini.ELCAR_LETTERE(index)
        cod(1) = ComboBox1.Text
        Call Carica_Combobox2(y)

    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        cod(2) = ComboBox2.Text
        Gbl_Codice_Elcar_Selezionato = cod(1) + "." + cod(2)
        ComboBox3.Items.Clear() : ComboBox4.Items.Clear() : ComboBox5.Items.Clear() : ComboBox6.Items.Clear()
        For jj = 3 To 7 : cod(jj) = "" : Next
        ComboBox3.Items.Clear() :
        ComboBox4.Items.Clear() :
        ComboBox5.Items.Clear() :
        ComboBox6.Items.Clear()

        ' Carico combo3 (Elcar_VOCI)
        Call Carica_Combobox3(Gbl_Codice_Elcar_Selezionato)
        Label12.Text = "Tot " + ComboBox3.Items.Count.ToString + " voci" : Label12.Refresh()
        If ComboBox3.Items.Count = 0 Then ComboBox3.Text = ""

        'Label5.Text = "codice non trovato!"
        'Label5.Tag = -1
        'Label5.Refresh()

    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        Dim j = ComboBox3.SelectedIndex
        cod(3) = Gbl_Elenco_Codici_ELCARVOCI(j).CODICE_VOCE
        For jj = 4 To 7 : cod(jj) = "" : Next


        ComboBox3.Tag = ComboBox2.Tag + "." + Gbl_Elenco_Codici_ELCARVOCI(j).CODICE_VOCE
        Gbl_Codice_Elcar_Selezionato = cod(1) + "." + cod(2) + "." + cod(3)
        Gbl_Codice_Elcar_TestoSelezionato = ComboBox3.Text
        Gbl_Codice_Elcar_TestoLungo = Gbl_Elenco_Codici_ELCARVOCI(j).TESTO_Lungo
        If Trim(Gbl_Codice_Elcar_TestoLungo) <> "" Then
            PictureBox1.Visible = True
            TP1.SetToolTip(PictureBox1, Gbl_Codice_Elcar_TestoLungo)
        Else
            PictureBox1.Visible = False
            TP1.SetToolTip(PictureBox1, """")
        End If

        Mostra_Testo_riportato()

        Label2.Text = "Sub Item ELCAR (" + cod(1) + "." + cod(2) + "):" : Label2.Refresh()
        Label12.Text = "Tot " + ComboBox3.Items.Count.ToString + " voci" : Label12.Refresh()
        Label6.Text = "Sub Item ELCAR (" + ComboBox3.Tag + "):" : Label6.Refresh()

        'Carico combo4 (Elcar_VOCI_SUB)
        Call Carica_Combobox4(Gbl_Codice_Elcar_Selezionato)
        Azzera_totali_voci_combo(4)
        Label7.Text = "Tot " + ComboBox4.Items.Count.ToString + " voci" : Label7.Refresh()
        If ComboBox4.Items.Count = 0 Then ComboBox4.Text = ""

    End Sub
    Private Sub ComboBox4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox4.SelectedIndexChanged
        Dim j = ComboBox4.SelectedIndex
        ComboBox5.Items.Clear() : ComboBox5.Refresh()

        cod(4) = Gbl_Elenco_Codici_ELCARVOCISUB(j).CODICE_VOCE
        For jj = 5 To 7 : cod(jj) = "" : Next
        ComboBox4.Tag = ComboBox3.Tag + "." + Gbl_Elenco_Codici_ELCARVOCISUB(j).CODICE_VOCE

        Gbl_Codice_Elcar_Selezionato = cod(1) + "." + cod(2) + "." + cod(3) + "." + cod(4)
        Gbl_Codice_Elcar_TestoSelezionato = Pulisci_Testo_selezionato(ComboBox4.Text)
        Button1.Tag = j
        Mostra_Testo_riportato()

        ComboBox3.Tag = cod(1) + "." + cod(2) + "." + cod(3)
        Label6.Text = "Sub Item ELCAR (" + ComboBox3.Tag + "):" : Label6.Refresh()

        '----------------------------------------------------------------------------------
        '-- Carica e mostra combo di eventuali codici aggiuntivi (CODICI_ELCAR_VOCI_SUB2)--
        '----------------------------------------------------------------------------------
        GroupBox2.Text = "Ulteriori Codici aggiuntivi a " + Gbl_Codice_Elcar_Selezionato : GroupBox2.Refresh()
        Dim caricati As Boolean = False

        If CheckBox1.Checked Then
            caricati = Carica_elenco_CODICI_ELCAR_VOCI_SUB2("")
        Else
            caricati = Carica_elenco_CODICI_ELCAR_VOCI_SUB2(Gbl_Codice_Elcar_Selezionato, True)
        End If

        If caricati Then
            Carica_combobox_ELCAR_VOCI_SUB2(CheckBox1.Checked)
            Gbl_Codice_Elcar_TestoSelezionato = Gbl_Elenco_Codici_ELCARVOCISUB2(0).TESTO
            Azzera_totali_voci_combo(5)
            Label16.Text = "Tot " + ComboBox5.Items.Count.ToString + " voci" : Label16.Refresh()
            If ComboBox5.Items.Count = 0 Then ComboBox5.Text = ""

        End If

        '---------------------------------------------------------------------------------------
        '-- Carica e mostra combo di eventuali codici dedl BAMBINO (CODICI_ELCAR_VOCI_BAMBINO)--
        '---------------------------------------------------------------------------------------

        If CheckBox2.Checked Then
            caricati = Carica_elenco_CODICI_ELCAR_VOCI_BAMBINO("")
        Else
            caricati = Carica_elenco_CODICI_ELCAR_VOCI_BAMBINO(ComboBox4.Tag, True)
        End If
        If caricati Then
            Carica_combobox_ELCAR_VOCI_bambino(CheckBox1.Checked)
            Gbl_Codice_Elcar_TestoSelezionato = Gbl_Elenco_Codici_ELCARVOCISUB2(0).TESTO
        End If
        Mostra_Testo_riportato()



        Dim XC = 1
    End Sub
    Function Pulisci_Testo_selezionato(testo, Optional maxleng = 30) As String
        If InStr(testo, "] ") > 0 Then
            Dim x = Split(testo, "] ")
            testo = Strings.Trim(x(1))
        End If
        testo = CorreggiCodificaTesto(testo)
        Return testo
    End Function
    Private Sub ComboBox5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox5.SelectedIndexChanged
        Dim j = 0

        If ComboBox5.Text = "" Then
            Button1.Tag = -1
            Dim x = Costruisci_codice_finale()
            Gbl_Codice_Elcar_TestoSelezionato = ""
            Mostra_Testo_riportato()
            Exit Sub
        End If

        Try
            j = ComboBox5.SelectedIndex
            Button1.Tag = j
        Catch ex As Exception
            Exit Sub
        End Try
        cod(6) = ""
        cod(5) = Gbl_Elenco_Codici_ELCARVOCISUB2(j).CODICE_VOCE
        Gbl_Codice_Elcar_TestoSelezionato = Gbl_Elenco_Codici_ELCARVOCISUB2(j).TESTO
        Gbl_Codice_Elcar_Selezionato = Costruisci_codice_finale()

        'If CheckBox1.Checked Then
        '    Gbl_Codice_Elcar_Selezionato = Gbl_Elenco_Codici_ELCARVOCISUB2(j).CODICE_ELCAR + "." + Gbl_Elenco_Codici_ELCARVOCISUB2(j).CODICE_VOCE
        'End If

        Mostra_Testo_riportato()
        ComboBox6.Items.Clear()
        If ComboBox6.Items.Count = 0 Then ComboBox6.Text = ""
        If Carica_elenco_CODICI_ELCAR_VOCI_BAMBINO(Gbl_Codice_Elcar_Selezionato) Then
            Carica_combobox_ELCAR_VOCI_bambino()
            Label17.Text = "Tot " + ComboBox6.Items.Count.ToString + " voci" : Label17.Refresh()
        End If


    End Sub
    Function Carica_combobox_ELCAR_VOCI()
        ComboBox3.Items.Clear()
        Dim tot = 0, x = "", id = 0
        Try
            tot = UBound(Gbl_Elenco_Codici_ELCARVOCI)

        Catch ex As Exception
            Exit Function
        End Try

        For j = 0 To tot - 1
            x = Strings.Right("  " + Gbl_Elenco_Codici_ELCARVOCI(j).CODICE_VOCE, 2) + "] "
            x = x + Gbl_Elenco_Codici_ELCARVOCI(j).TESTO
            ComboBox3.Items.Add(x)
        Next
        ComboBox3.Refresh()
    End Function

    Function Carica_combobox_ELCAR_VOCI_SUB()
        ComboBox4.Items.Clear()
        Dim tot = 0, x = "", id = 0
        Try
            tot = UBound(Gbl_Elenco_Codici_ELCARVOCISUB)
        Catch ex As Exception
            Exit Function
        End Try

        For j = 0 To tot - 1
            x = Strings.Right("  " + Gbl_Elenco_Codici_ELCARVOCISUB(j).CODICE_VOCE, 2) + "] "
            x = x + Gbl_Elenco_Codici_ELCARVOCISUB(j).TESTO
            ComboBox4.Items.Add(x)
        Next
        ComboBox4.Refresh()
    End Function
    Function Carica_combobox_ELCAR_VOCI_SUB2(Optional mostratutti As Boolean = False)
        ComboBox5.Items.Clear()
        Dim tot = 0, x = "", id = 0
        Try
            tot = UBound(Gbl_Elenco_Codici_ELCARVOCISUB2)
        Catch ex As Exception
            Exit Function
        End Try


        For j = 0 To tot
            If mostratutti Then
                x = Gbl_Elenco_Codici_ELCARVOCISUB2(j).CODICE_ELCAR + "." + Gbl_Elenco_Codici_ELCARVOCISUB2(j).CODICE_VOCE
            Else
                x = Strings.Right("  " + Gbl_Elenco_Codici_ELCARVOCISUB2(j).CODICE_VOCE, 2)
            End If
            x = x + "] "
            x = x + Gbl_Elenco_Codici_ELCARVOCISUB2(j).TESTO
            ComboBox5.Items.Add(x)
        Next
        ComboBox5.Refresh()
    End Function
    Function Carica_combobox_ELCAR_VOCI_bambino(Optional mostratutti As Boolean = False)
        ComboBox6.Items.Clear()
        Dim tot = 0, x = "", id = 0
        Try
            tot = UBound(Gbl_Elenco_Codici_ELCARVOCIPERSONALI)
        Catch ex As Exception
            Exit Function
        End Try


        For j = 0 To tot
            If mostratutti Then
                x = Gbl_Elenco_Codici_ELCARVOCIPERSONALI(j).CODICE_ELCAR + "." + Gbl_Elenco_Codici_ELCARVOCIPERSONALI(j).CODICE_VOCE
            Else
                x = Strings.Right("  " + Gbl_Elenco_Codici_ELCARVOCIPERSONALI(j).CODICE_VOCE, 2)
            End If
            x = x + "] "
            x = x + Gbl_Elenco_Codici_ELCARVOCIPERSONALI(j).TESTO
            ComboBox6.Items.Add(x)
        Next
        ComboBox6.Refresh()
    End Function

    Function Carica_elenco_CODICI_ELCAR_VOCI(Optional codice As String = "", Optional testoesatto As Boolean = False) As Boolean
        Dim risultati1 As List(Of Class_ELCAR_VOCI) = Class_ELCAR_VOCI.CercaPerCodice(codice, testoesatto)
        If risultati1.Count = 0 Then Return False
        Dim id As Integer, x As String, tot As Integer
        tot = risultati1.Count - 1
        ReDim Gbl_Elenco_Codici_ELCARVOCI(tot)
        For j = 0 To tot - 1

            Gbl_Elenco_Codici_ELCARVOCI(j).CODICE_ELCAR = risultati1(j).CODICE_ELCAR
            Gbl_Elenco_Codici_ELCARVOCI(j).CODICE_VOCE = risultati1(j).CODICE_VOCE
            Gbl_Elenco_Codici_ELCARVOCI(j).ID = risultati1(j).ID
            Gbl_Elenco_Codici_ELCARVOCI(j).TESTO = risultati1(j).Testo
            Gbl_Elenco_Codici_ELCARVOCI(j).TESTO_Lungo = risultati1(j).Testo_Lungo
        Next

        Return True
    End Function

    Function Carica_elenco_CODICI_ELCAR_VOCI_SUB(Optional codice As String = "", Optional testoesatto As Boolean = False) As Boolean
        Dim risultati1 As List(Of Class_ELCAR_VOCI_SUB) = Class_ELCAR_VOCI_SUB.CercaPerCodice(codice, testoesatto)
        If risultati1.Count = 0 Then
            ReDim Gbl_Elenco_Codici_ELCARVOCISUB(0)
            Return False
        End If
        Dim id As Integer, x As String, tot As Integer
        tot = risultati1.Count - 1
        ReDim Gbl_Elenco_Codici_ELCARVOCISUB(tot)
        For j = 0 To tot - 1

            Gbl_Elenco_Codici_ELCARVOCISUB(j).CODICE_ELCAR = risultati1(j).ELCAR
            Gbl_Elenco_Codici_ELCARVOCISUB(j).CODICE_VOCE = risultati1(j).ID_SUB
            Gbl_Elenco_Codici_ELCARVOCISUB(j).ID = risultati1(j).ID
            Gbl_Elenco_Codici_ELCARVOCISUB(j).TESTO = risultati1(j).Testo
            Gbl_Elenco_Codici_ELCARVOCISUB(j).TESTO_Lungo = risultati1(j).Testo_Lungo
            Gbl_Elenco_Codici_ELCARVOCISUB(j).Deleted = risultati1(j).Deleted
        Next

        Return True
    End Function

    Function Carica_elenco_CODICI_ELCAR_VOCI_SUB2(Optional codice As String = "", Optional testoesatto As Boolean = False) As Boolean
        If IsNothing(codice) Then
            ReDim Gbl_Elenco_Codici_ELCARVOCISUB2(0)
            Return False
        End If
        Dim risultati1 As List(Of Class_ELCAR_VOCI_SUB2) = Class_ELCAR_VOCI_SUB2.CercaPerCodice(codice, testoesatto)
        If risultati1.Count = 0 Then
            ReDim Gbl_Elenco_Codici_ELCARVOCISUB2(0)
            Return False
        End If
        Dim id As Integer, x As String, tot As Integer
        tot = risultati1.Count - 1
        ReDim Gbl_Elenco_Codici_ELCARVOCISUB2(tot)
        For j = 0 To tot

            Gbl_Elenco_Codici_ELCARVOCISUB2(j).CODICE_ELCAR = risultati1(j).ELCAR
            Gbl_Elenco_Codici_ELCARVOCISUB2(j).CODICE_VOCE = risultati1(j).ID_SUB
            Gbl_Elenco_Codici_ELCARVOCISUB2(j).ID = risultati1(j).ID
            Gbl_Elenco_Codici_ELCARVOCISUB2(j).TESTO = risultati1(j).Testo
            Gbl_Elenco_Codici_ELCARVOCISUB2(j).TESTO_Lungo = risultati1(j).Testo_Lungo
            Gbl_Elenco_Codici_ELCARVOCISUB2(j).Deleted = risultati1(j).Deleted
        Next

        Return True
    End Function

    Function Carica_elenco_CODICI_ELCAR_VOCI_BAMBINO(Optional codice As String = "", Optional testoesatto As Boolean = False) As Boolean
        Dim risultati1 As List(Of Class_ELCAR_VOCI_PERSONALI) = Class_ELCAR_VOCI_PERSONALI.CercaPerCodice(codice, testoesatto)
        If risultati1.Count = 0 Then
            ReDim Gbl_Elenco_Codici_ELCARVOCIPERSONALI(0)
            Return False
        End If
        Dim id As Integer, x As String, tot As Integer
        tot = risultati1.Count - 1
        ReDim Gbl_Elenco_Codici_ELCARVOCIPERSONALI(tot)
        For j = 0 To tot
            Gbl_Elenco_Codici_ELCARVOCIPERSONALI(j).CODICE_ELCAR = risultati1(j).ELCAR
            Gbl_Elenco_Codici_ELCARVOCIPERSONALI(j).CODICE_VOCE = risultati1(j).ID_SUB
            Gbl_Elenco_Codici_ELCARVOCIPERSONALI(j).ID = risultati1(j).ID
            Gbl_Elenco_Codici_ELCARVOCIPERSONALI(j).TESTO = risultati1(j).Testo
            Gbl_Elenco_Codici_ELCARVOCIPERSONALI(j).TESTO_Lungo = risultati1(j).Testo_Lungo
            Gbl_Elenco_Codici_ELCARVOCIPERSONALI(j).Deleted = risultati1(j).Deleted
        Next

        Return True
    End Function
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Aggiunge un codice nuovo
        Dim j = Val(Button1.Tag)

        Dim Codice_ELCARVOCISUB As New Class_ELCAR_VOCI_SUB2
        Codice_ELCARVOCISUB.ID_SUB = Gbl_Elenco_Codici_ELCARVOCISUB(j).CODICE_VOCE
        Codice_ELCARVOCISUB.ELCAR = Gbl_Elenco_Codici_ELCARVOCISUB(j).CODICE_ELCAR
        Codice_ELCARVOCISUB.Testo = Gbl_Elenco_Codici_ELCARVOCISUB(j).TESTO
        Codice_ELCARVOCISUB.Testo_Lungo = Gbl_Elenco_Codici_ELCARVOCISUB(j).TESTO_Lungo
        Codice_ELCARVOCISUB.Deleted = Gbl_Elenco_Codici_ELCARVOCISUB(j).Deleted
        Dim codice_gentore = Codice_ELCARVOCISUB.ELCAR + "." + Codice_ELCARVOCISUB.ID_SUB.ToString
        F_CODICE_ADD.Mostra(Codice_ELCARVOCISUB, ComboBox4.Text)

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        If Not inserisci_il_codice_nella_Griglia() Then
            Exit Sub
        End If

        If Button3.Tag <> "NEW" Then Exit Sub
        Call Salva_nuovo_codice_personale()
        Call Mostra_Testo_riportato()
    End Sub
    Sub Salva_nuovo_codice_personale()
        Gbl_Class_Voci_Personali.ID = 0
        Gbl_Class_Voci_Personali.ID_SUB = cod(6)
        Gbl_Class_Voci_Personali.Testo = TextBox1.Text
        Gbl_Class_Voci_Personali.Deleted = False
        Gbl_Class_Voci_Personali.ELCAR = cod(1) + "." + cod(2) + "." + cod(3) + "." + cod(4) + "." + cod(5)
        If Gbl_Class_Voci_Personali.Save() Then
            Carica_elenco_CODICI_ELCAR_VOCI_BAMBINO(Gbl_Class_Voci_Personali.ELCAR)
            Carica_combobox_ELCAR_VOCI_bambino()
        End If
    End Sub
    Function inserisci_il_codice_nella_Griglia(Optional edit As Boolean = False) As Boolean
        If Verifica_Se_gia_inserito_Nel_Foglio(Label15.Text) Then
            Dim m = "L'esercizio selezionato è gia presente nel foglio!"
            Call MsgBox(m, vbInformation, "Esercizio già inserito")
            Return False
        End If

        If edit Then
            Dim idr As Integer = DGV1.CurrentRow.Index
            DGV1.Rows(idr).Cells(0).Value = Label15.Text
            DGV1.Rows(idr).Cells(1).Value = TextBox1.Text
        Else
            DGV1.Rows.Add(Label15.Text, TextBox1.Text)
        End If
        DGV1.Refresh()
        Label10.Text = DGV1.Rows.Count
        Return True

    End Function


    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Call Seleziona_bambino()

    End Sub

    Private Sub DGV1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV1.CellContentClick
        Dim c = DGV1.CurrentCell.ColumnIndex
        If c = 0 Then
            Dim r As Integer = DGV1.CurrentRow.Index
            Dim cod = "", strindx = ""
            cod = DGV1(0, r).Value
            'cod = DGV1(3, r).Value
            'Call Sincronizza_Combo_con_Griglia(cod)
            Allinea_combo_al_codice_Elcar(cod)
        Else
            Dim m = "Vuoi modificare il codice inserito con quello nuovo Selezionato?" + vbCrLf
            If MsgBox(m, vbYesNo) <> vbYes Then Exit Sub
            Call inserisci_il_codice_nella_Griglia(True)
        End If


    End Sub
    Sub Sincronizza_Combo_con_Griglia(Cod)
        Dim x = Split(Cod, ".")
        Dim n = x.Count
        ComboBox1.Text = x(0) : ComboBox1.Refresh()
        ComboBox2.Text = x(1) : ComboBox2.Refresh()
        If x(2) <> "" Then
            'ComboBox3.SelectedIndex = Combo_index(3)
            'ComboBox3.Text = Combo_text(3)
        End If
        If x(3) <> "" Then
            'ComboBox4.SelectedIndex = Combo_index(4)
            'ComboBox4.Text = Combo_text(4)

        End If
        If x(4) <> "" Then
            'ComboBox5.SelectedIndex = Combo_index(4)
            'ComboBox5.Text = Combo_text(5)
        End If
        If x(5) <> "" Then
            'ComboBox6.SelectedIndex = Combo_index(5)
            'ComboBox6.Text = Combo_text(6)
        End If

    End Sub
    Sub Mostra_Testo_riportato()
        Label15.Text = Gbl_Codice_Elcar_Selezionato
        TextBox1.Text = Gbl_Codice_Elcar_TestoSelezionato
        Dim N = Split(Gbl_Codice_Elcar_Selezionato, ".").Count

        If N > 5 Then
            Label15.BackColor = MyColor.Orange
            TP1.SetToolTip(Label15, "è stato utilizzato un codice personalizzato per il ragazzo")
        Else
            Label15.BackColor = System.Drawing.SystemColors.Control
            TP1.SetToolTip(Label15, "")
        End If

        Label3.Text = "(" + Len(TextBox1.Text).ToString + ") Char" : Label3.Refresh()
        TextBox1.Refresh()
        Button3.BackColor = System.Drawing.SystemColors.Control
        TP1.SetToolTip(Button3, "")
        Label15.Refresh()

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        Dim caricati As Boolean = False
        If CheckBox1.Checked Then
            caricati = Carica_elenco_CODICI_ELCAR_VOCI_SUB2("")
        Else
            caricati = Carica_elenco_CODICI_ELCAR_VOCI_SUB2(ComboBox4.Tag, True)
        End If
        If caricati Then
            Carica_combobox_ELCAR_VOCI_SUB2(CheckBox1.Checked)
            Gbl_Codice_Elcar_TestoSelezionato = Gbl_Elenco_Codici_ELCARVOCISUB2(0).TESTO
        Else
            ComboBox5.Items.Clear()
        End If


    End Sub



    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        Dim caricati As Boolean = False
        ComboBox6.Items.Clear()
        If CheckBox2.Checked Then
            caricati = Carica_elenco_CODICI_ELCAR_VOCI_BAMBINO("")
        Else
            caricati = Carica_elenco_CODICI_ELCAR_VOCI_BAMBINO(ComboBox4.Tag, True)
        End If
        If caricati Then
            Carica_combobox_ELCAR_VOCI_bambino(CheckBox2.Checked)
            Gbl_Codice_Elcar_TestoSelezionato = Gbl_Elenco_Codici_ELCARVOCIPERSONALI(0).TESTO
        End If

    End Sub

    Private Sub ComboBox6_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox6.SelectedIndexChanged
        Dim j = 0

        If ComboBox6.Text = "" Then
            'Dim x = Costruisci_codice_finale().Stringa_ELCAR
            Mostra_Testo_riportato()
            Exit Sub
        End If

        Try
            j = ComboBox6.SelectedIndex
        Catch ex As Exception
            Exit Sub
        End Try

        cod(6) = Gbl_Elenco_Codici_ELCARVOCIPERSONALI(j).CODICE_VOCE
        If CheckBox2.Checked Then
            Dim x = Split(Gbl_Elenco_Codici_ELCARVOCIPERSONALI(j).CODICE_ELCAR, ".")
            Dim N = x.Count - 1
            For i = 0 To N : cod(i + 1) = x(i) : Next
        End If

        Gbl_Codice_Elcar_TestoSelezionato = Gbl_Elenco_Codici_ELCARVOCIPERSONALI(j).TESTO
        Gbl_Codice_Elcar_Selezionato = Costruisci_codice_finale()


        Mostra_Testo_riportato()


    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Call Inserisci_nuovo_esercizio
    End Sub

    Sub Inserisci_nuovo_Esercizio()
        Button3.Tag = ""
        Dim x = TextBox1.Text
        Dim h = CalcolaAltezzaOttimaleTextBox(2)
        Dim notaInserita As String = InputBoxMultiLine("Inserimento nuova voce", "Testo multilinea (max 80 char su 2 righe)", h, x)
        If Len(notaInserita) > 82 Then
            MessageBox.Show("Testo troppo lungo:" & Environment.NewLine & notaInserita)
            Exit Sub
        End If

        If notaInserita = "" Then Exit Sub

        'Dim x1 = InputBox("Testo da inserire nel Foglio (max 40 Char)", "Modifica Manuale del testo inserito", x)
        TextBox1.Text = notaInserita
        TextBox1.Refresh()

        Dim j = Testo_NONpresente_traicodicipersonali(notaInserita)
        If j < 0 Then
            Dim i = Class_ELCAR_VOCI_PERSONALI.Trova_ultimo_IDSUB_Manuale() + 1
            cod(6) = i
            Gbl_Codice_Elcar_Selezionato = Costruisci_codice_finale()
            Gbl_Codice_Elcar_TestoSelezionato = TextBox1.Text
            Mostra_Testo_riportato()
            Button3.BackColor = System.Drawing.Color.Orange
            Button3.Tag = "NEW"
            TP1.SetToolTip(Button3, "Il nuovo Esercizio verrà aggiunto agli esercizi personali del bambino quando inserito nel foglio")
        Else
            Dim m = "Il testo è gia presente nell'elenco degli esercizi personali" + vbCrLf
            m = m + " con il codice: " + Gbl_Elenco_Codici_ELCARVOCIPERSONALI(j).CODICE_ELCAR + "." + Gbl_Elenco_Codici_ELCARVOCIPERSONALI(j).CODICE_VOCE
            Call MsgBox(m, vbInformation, "Testo inserito gia presente")
            Mostra_Testo_riportato()
        End If

    End Sub
    Function Testo_NONpresente_traicodicipersonali(x1) As Integer
        Dim n = Gbl_Elenco_Codici_ELCARVOCIPERSONALI.Count - 1
        x1 = Trim(x1.ToString.ToUpper)
        Dim x2 = ""
        For j = 0 To n
            x2 = Trim(Gbl_Elenco_Codici_ELCARVOCIPERSONALI(j).TESTO.ToUpper)
            If x2 = x1 Then
                Return j
            End If
        Next
        Return -1
    End Function
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        If MsgBox("Vuoi salvare il nuovo foglio di esercizi", vbYesNo) <> vbYes Then
            Exit Sub
        End If
        Dim j = 0
        For Each R In DGV1.Rows
            j = R.Index

            Call Salva_Foglio(j)

        Next
    End Sub
    Sub Carica_Classe_Giornaliero(j)
    End Sub
    Sub Salva_Foglio(r As Integer, Optional Nuovo As Boolean = True)
        Dim idff = Label13.Text
        Dim IDF = Convert.ToInt16(idff)
        If Nuovo Then Giornaliero.ID = 0
        Giornaliero.Codice = DGV1.Rows(r).Cells(0).Value?.ToString()
        If IsNothing(Giornaliero.Codice) Then Exit Sub
        Giornaliero.Testo = DGV1.Rows(r).Cells(1).Value?.ToString()
        Giornaliero.ID_FOGLIO = IDF
        Giornaliero.DATA = DateTimePicker1.Text
        Giornaliero.TOT = 0
        Giornaliero.TOT_NEG = 0
        Giornaliero.TOT_POS = 0
        Giornaliero.TOT_PROMPT = 0
        Giornaliero.TUTTELERISPOSTE = True
        Giornaliero.VALUTATIVO = False
        If Not Giornaliero.Salva Then
            Call MsgBox("Problemi nel salvataggio")
        End If

    End Sub
    Sub Stampa_Foglio_Giornaliero()
        Dim foglio = CInt(Label13.Text)
        Dim data = DateTimePicker1.Value.ToString("yyyy-MM-dd")
        'Dim nomefoglio = foglio.ToString("D6") + " (" + data + ")"
        Dim nomefoglio = Gbl_NOME_BAMBINO + "_" + data + " (" + foglio.ToString + ").DOCX"
        Dim documento As String = dir_bar(Gbl_SCHEDA_BAMBINO) + nomefoglio

        If IO.File.Exists(nomefoglio) Then
            Dim x = "Esiste giàu un foglio con lo stesso nome" + vbCrLf + nomefoglio
            Call MsgBox(x)
        End If
        IO.File.Copy(ini.Modello_Schede, documento, True)

        Using doc As WordprocessingDocument =
            WordprocessingDocument.Open(documento, True)

            Dim body As Body = doc.MainDocumentPart.Document.Body

            ' Qui inserisci le tue modifiche

            doc.MainDocumentPart.Document.Save()

        End Using
    End Sub
    Function Verifica_Se_gia_inserito_Nel_Foglio(cod) As Boolean
        Dim nrow = DGV1.Rows.Count - 1
        For i As Integer = 0 To nrow
            If DGV1.Rows(i).Cells(0).Value?.ToString() = "" Then Continue For
            If cod = DGV1.Rows(i).Cells(0).Value?.ToString() Then
                Return True
            End If
        Next
        Return False
    End Function
    Function Verifica_codici_duplicati() As List(Of Integer)
        Dim j = 0
        Dim trovato = False, cod0 = ""
        Dim nrow = DGV1.Rows.Count


        Dim indiciDuplicati As New List(Of Integer)()

        ' Dizionario per memorizzare la stringa (Chiave) e il suo primo indice (Valore)
        Dim mappaStringhe As New Dictionary(Of String, Integer)()

        ' Cicla l'array usando l'indice numerico
        For i As Integer = 0 To nrow - 1
            Dim stringaCorrente As String = DGV1.Rows(i).Cells(0).Value?.ToString()
            If stringaCorrente = "" Then Continue For
            ' Se la stringa è già presente nel dizionario, l'indice corrente è un duplicato
            If mappaStringhe.ContainsKey(stringaCorrente) Then
                indiciDuplicati.Add(i)
            Else
                ' Altrimenti, registra la stringa con il suo primo indice trovato
                mappaStringhe.Add(stringaCorrente, i)
            End If
        Next

        Return indiciDuplicati

    End Function

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Call Stampa_Foglio_Giornaliero()
    End Sub
    Sub Trova_tutti_Fogli_bambino(Optional soloUltimo As Boolean = False)
        Dim elenco_fogli As New Class_Elenco_fogli()
        Gbl_elenco_fogli_bambino = elenco_fogli.Trova_tutti_fogli()
        ComboBox7.Items.Clear()

        For Each foglio As Class_Elenco_fogli In Gbl_elenco_fogli_bambino
            Dim x = foglio.Data.ToString + " (" + foglio.ID_Foglio.ToString + ")"
            ComboBox7.Items.Add(x)
        Next

    End Sub
    Sub Carica_foglio_esistente(j)

    End Sub

    Private Sub ComboBox7_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox7.SelectedIndexChanged
        Dim J = ComboBox7.SelectedIndex
        Dim modo = True
        If CInt(Label10.Text) > 0 Then modo = False
        F_Foglio_esistente.Main(J, Label9.Text,, modo)

        Dim ID_foglio = CInt(Gbl_elenco_fogli_bambino(J).ID_Foglio)

        Gbl_VociDiUnFoglio = Gbl_Giornaliero.Carica_Foglio(ID_foglio)
        Dim n = Gbl_VociDiUnFoglio.Count

    End Sub
    Public Sub Carica_foglio(j)
        Dim ID_foglio = CInt(Gbl_elenco_fogli_bambino(j).ID_Foglio)
        Gbl_VociDiUnFoglio = Gbl_Giornaliero.Carica_Foglio(ID_foglio)

        DGV1.Rows.Clear()
        For Each foglio In Gbl_VociDiUnFoglio
            DGV1.Rows.Add(foglio.Codice, foglio.Testo)
        Next
        DGV1.Refresh()

    End Sub

    Sub Allinea_combo_al_codice_Elcar(codice)
        Dim x = Split(codice, ".")
        Dim n = x.Count
        If n < 0 Then Exit Sub

        Dim risultati = -1
        Dim testodacercare = x(0)
        For J = 0 To ComboBox1.Items.Count - 1
            If ComboBox1.Items(J).ToString = testodacercare Then
                risultati = J
            End If
        Next
        If risultati >= 0 Then
            ComboBox1.SelectedIndex = risultati
            Gbl_Codice_Elcar_Selezionato = ComboBox1.Text
            cod(1) = ComboBox1.Text
        End If


        If n < 1 Then Exit Sub
        testodacercare = x(1)
        risultati = OttieniPrimoIndicediunacombo(ComboBox2, testodacercare)
        If risultati >= 0 Then
            ComboBox2.SelectedIndex = risultati
            Gbl_Codice_Elcar_Selezionato = ComboBox1.Text + "." + ComboBox2.Text
            cod(2) = ComboBox2.Text
        End If

        ComboBox3.Items.Clear() : ComboBox3.Text = "" : ComboBox3.Refresh()
        ComboBox4.Items.Clear() : ComboBox4.Text = "" : ComboBox4.Refresh()
        ComboBox5.Items.Clear() : ComboBox5.Text = "" : ComboBox5.Refresh()
        ComboBox6.Items.Clear() : ComboBox6.Text = "" : ComboBox6.Refresh()

        If n < 2 Then Exit Sub
        Dim y = x(0) + "." + x(1)
        Call Carica_Combobox3(y)
        testodacercare = x(2) + "]"
        risultati = OttieniPrimoIndicediunacombo(ComboBox3, testodacercare)
        If risultati >= 0 Then
            ComboBox3.SelectedIndex = risultati
            cod(3) = x(2)
        End If

        If n <= 3 Then Exit Sub
        y = x(0) + "." + x(1) + "." + x(2)
        Call Carica_Combobox4(y)
        testodacercare = x(3) + "] "
        risultati = OttieniPrimoIndicediunacombo(ComboBox4, testodacercare)
        If risultati >= 0 Then
            cod(4) = x(3)
            Costruisci_codice_finale()
            ComboBox4.SelectedIndex = risultati
            Call Mostra_Testo_riportato()
        End If

        If n <= 4 Then Exit Sub
        testodacercare = x(4) + "] "
        risultati = OttieniPrimoIndicediunacombo(ComboBox5, testodacercare)
        If risultati >= 0 Then
            cod(5) = x(4)
            Costruisci_codice_finale()
            ComboBox5.SelectedIndex = risultati
            Call Mostra_Testo_riportato()
        End If

        If n <= 5 Then Exit Sub
        testodacercare = x(5) + "] "
        risultati = OttieniPrimoIndicediunacombo(ComboBox6, testodacercare)
        If risultati >= 0 Then
            cod(6) = x(5)
            Costruisci_codice_finale()
            ComboBox6.SelectedIndex = risultati
            Call Mostra_Testo_riportato()
        Else
            Call MsgBox("Codice NON trovato")
        End If
        Call Mostra_Testo_riportato()

    End Sub
    Sub Carica_Combobox2(y)
        ComboBox2.Items.Clear()
        Dim x = Split(y, ",")
        For j = 0 To x.Count - 1
            ComboBox2.Items.Add(x(j))
        Next
        ComboBox2.SelectedIndex = 0
        cod(2) = ComboBox2.Text
        ComboBox2.Refresh()


    End Sub
    Sub Carica_Combobox3(y)
        'Trovo l
        ComboBox3.Items.Clear()

        For J = 0 To Gbl_Elenco_Codici_ELCAR.Count - 1
            If Gbl_Elenco_Codici_ELCAR(J).CODICE <> y Then Continue For
            If Carica_elenco_CODICI_ELCAR_VOCI(y, True) Then
                Carica_combobox_ELCAR_VOCI()
            End If
        Next
        'Label5.Text = Gbl_Elenco_Codici_ELCAR(J).TESTO
        'Label5.Tag = Gbl_Elenco_Codici_ELCAR(J).ID
        'Label5.Refresh()
    End Sub
    Sub Carica_Combobox4(y)
        ComboBox4.Items.Clear()

        If Not Carica_elenco_CODICI_ELCAR_VOCI_SUB(y, True) Then Exit Sub
        Carica_combobox_ELCAR_VOCI_SUB()
        Dim nvoci = ComboBox4.Items.Count
        Label7.Text = "Tot " + nvoci.ToString + " voci" : Label7.Refresh()
        Label6.Text = "Sub Item ELCAR (" + y + ")" : Label6.Refresh()

    End Sub
    Sub Carica_Combobox5(y)
        ComboBox5.Items.Clear()
        ComboBox5.SelectedIndex = 0
        ComboBox5.Refresh()
    End Sub
    Sub Carica_Combobox6(y)
        ComboBox6.Items.Clear()
        ComboBox6.SelectedIndex = 0
        ComboBox6.Refresh()
    End Sub

    Private Sub ComboBox3_FormatStringChanged(sender As Object, e As EventArgs) Handles ComboBox3.FormatStringChanged

    End Sub

    Private Sub ComboBox5_SelectedValueChanged(sender As Object, e As EventArgs) Handles ComboBox5.SelectedValueChanged

    End Sub

    Private Sub ComboBox6_DoubleClick(sender As Object, e As EventArgs) Handles ComboBox6.DoubleClick
    End Sub

    Private Sub ComboBox6_RegionChanged(sender As Object, e As EventArgs) Handles ComboBox6.RegionChanged
    End Sub


    Private Sub Label15_DoubleClick(sender As Object, e As EventArgs) Handles Label15.DoubleClick
        If cod(6) <> "" Then
            Dim GENITORE = cod(1) + "." + cod(2) + "." + cod(3) + "." + cod(4) + "." + cod(5)
            Dim ID = Class_ELCAR_VOCI_PERSONALI.Trova_ID(GENITORE, cod(6))
            Gbl_Class_Voci_Personali = Class_ELCAR_VOCI_PERSONALI.Load(ID)
            F_Mostra_voce.Main("VOCI_PERSONALI", "VIEW")
            Exit Sub
        End If

        If cod(5) <> "" Then
            Dim GENITORE = cod(1) + "." + cod(2) + "." + cod(3) + "." + cod(4)

            Dim ID = Class_ELCAR_VOCI_SUB2.Trova_ID(GENITORE, cod(5))
            Gbl_Class_Voci_sub2 = Class_ELCAR_VOCI_SUB2.Load(ID)
            F_Mostra_voce.Main("SUB2", "VIEW")
            Exit Sub
        End If

        If cod(4) <> "" Then
            Dim GENITORE = cod(1) + "." + cod(2) + "." + cod(3)

            Dim ID = Class_ELCAR_VOCI_SUB.Trova_ID(GENITORE, cod(4))
            Gbl_Class_Voci_sub = Class_ELCAR_VOCI_SUB.Load(ID)
            F_Mostra_voce.Main("SUB", "VIEW")
            Exit Sub
        End If

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub TextBox1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles TextBox1.MouseDoubleClick
        Call Inserisci_nuovo_Esercizio()

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Call MsgBox(Gbl_Codice_Elcar_TestoLungo)

    End Sub
End Class