
Imports System.Runtime.InteropServices.JavaScript.JSType
Imports DocumentFormat.OpenXml
Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Spreadsheet
Imports DocumentFormat.OpenXml.Wordprocessing
Imports Microsoft.IdentityModel.Tokens
Imports ScottPlot.Statistics
Imports MyColor = System.Drawing.Color

Public Class F_Seleziona_Categoria
    Public UltimoFoglioGiornaliero_Inserito As Integer
    Public Giornaliero As New Class_GIORNALIERO

    Private Sub F_Crea_Scheda_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' Definizione dei tooltip per i pulsanti
        TP1.SetToolTip(Button1, "Crea nuova scheda")
        TP1.SetToolTip(Button3, "Elimina scheda")
        TP1.SetToolTip(Button8, "Seleziona ragazzo")
        TP1.SetToolTip(Button10, "Impostazioni")

        Dim n = UBound(ini.ELCAR_LETTERE)
        ReDim Preserve CODICI_ELCAR(n)
        ReDim cod(7)
        ComboBox1.Items.Clear()
        For j = 0 To n - 1
            ComboBox1.Items.Add(CODICI_ELCAR(j))
        Next

        ' Imposto la data presentia dell'esercizio in preparazione 
        Dim mydata As DateTime = DateTime.Today.AddDays(1)
        Label11.Tag = mydata.ToString("dddd d MMMM yyyy")
        Label11.Text = "(" + mydata.ToString("dddd d MMMM yyyy") + ")"


        Gbl_miaLista_Anagrafiche.Clear()
        Gbl_Elenco_bambini = Carica_bambini()
        ' Ciclo per leggere i dati trovati
        Dim id As Integer, x = ""
        For Each persona In Gbl_Elenco_bambini
            x = persona.Cognome & " " & persona.Nome
            Dim elemento As New Class_ElementoCombo(persona.ID, x, persona.DBname)
            Gbl_miaLista_Anagrafiche.Add(elemento)
        Next
        ' Popolo la combo con i nomi dei bambini 
        ComboBox8.PopolaLeggero(Gbl_miaLista_Anagrafiche)

        ' Seleziono il primo bambino dell'elenco 
        Seleziona_bambino(0)



    End Sub
    Sub Azzera_totali_voci_combo(Optional start As Integer = 1)
        If start <= 3 Then Label12.Text = "Tot 0 voci" : Label12.Refresh()
        If start <= 4 Then Label7.Text = "Tot 0 voci" : Label7.Refresh()
        If start <= 5 Then Label16.Text = "Tot 0 voci" : Label16.Refresh()
        If start <= 6 Then Label17.Text = "Tot 0 voci" : Label17.Refresh()
    End Sub
    Sub Seleziona_bambino(Gbl_Jb)

        ' Costruisci_codice_finale il nome del file del bambio e creo il database se non esiste
        Dim filedb = Costruisci_filenameDB_bambino(Gbl_Jb)

        'If Gbl_Jb < 0 Then
        '    Call MsgBox("Bambino non selezionato")
        '    Exit Sub
        'End If

        Gbl_NOME_BAMBINO = Gbl_Elenco_bambini(Gbl_Jb).Cognome.ToUpper + " " + Gbl_Elenco_bambini(Gbl_Jb).Nome.ToUpper
        'Gbl_NOME_BAMBINO = Gbl_Elenco_bambini(Gbl_Jb).Nome.ToUpper + " " + Gbl_Elenco_bambini(Gbl_Jb).Cognome.ToUpper
        Gbl_DB_BAMBINO = Gbl_Elenco_bambini(Gbl_Jb).DBname.ToUpper
        If InStr(Gbl_DB_BAMBINO, ".DB") = 0 Then Gbl_DB_BAMBINO = Gbl_NOME_BAMBINO + ".DB"

        Label9.Text = Gbl_Elenco_bambini(Gbl_Jb).Cognome + " " + Gbl_Elenco_bambini(Gbl_Jb).Nome : Label9.Refresh()
        Label9.Tag = Gbl_Jb

        'Dim filedb = IO.Path.Combine(ini.DB_Bambini_path, Gbl_DB_BAMBINO).ToUpper
        'If Not IO.File.Exists(filedb) Then
        '    Dim m = "Il database" + Gbl_DB_BAMBINO + vbCrLf + "NON è stato trovato"
        '    m = m + "Vuoi crearlo?"
        '    If MsgBox(m, vbYesNo) <> vbYes Then
        '        Exit Sub
        '    End If
        '    Crea_DB_Bimbo(filedb)
        'End If

        ' Se non essite la directory delle schede la crea
        'Gbl_SCHEDA_BAMBINO = ini.Path_SCHEDE_DaCompilare
        'If Not IO.Directory.Exists(Gbl_SCHEDA_BAMBINO) Then
        '    IO.Directory.CreateDirectory(Gbl_SCHEDA_BAMBINO)
        'End If


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
        Try
            ComboBox2.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        cod(2) = ComboBox2.Text
        Gbl_Codice_Elcar_Selezionato = cod(1) + "." + cod(2)
        ComboBox3.Items.Clear() : ComboBox4.Items.Clear() : ComboBox5.Items.Clear() : ComboBox6.Items.Clear()
        For jj = 3 To 7 : cod(jj) = "" : Next
        ComboBox3.Items.Clear()
        ComboBox4.Items.Clear()
        ComboBox5.Items.Clear()
        ComboBox6.Items.Clear()

        ' Carico combo3 (Elcar_VOCI)
        Call Carica_Combobox3(Gbl_Codice_Elcar_Selezionato)
        Label12.Text = "Tot " + ComboBox3.Items.Count.ToString + " voci" : Label12.Refresh()
        If ComboBox3.Items.Count = 0 Then ComboBox3.Text = ""

        Try
            ComboBox3.SelectedIndex = 0
        Catch ex As Exception

        End Try

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

        Try
            ComboBox4.SelectedIndex = 0
        Catch ex As Exception

        End Try

    End Sub
    Private Sub ComboBox4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox4.SelectedIndexChanged
        Call my_ComboBox4_SelectedIndexChanged()

    End Sub
    Sub my_ComboBox4_SelectedIndexChanged()
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
        Try
            ComboBox5.SelectedIndex = 0
        Catch ex As Exception

        End Try




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
        Dim j = Val(cod(5)) + 1
        Dim x = TextBox1.Text
        Dim h = CalcolaAltezzaOttimaleTextBox(2)
        Dim notaInserita As String = InputBoxMultiLine("Inserimento nuova voce", "Testo multilinea (max 80 char su 2 righe)", h, x)
        If Len(notaInserita) = 0 Then Exit Sub

        Dim Genitore = cod(1) + "." + cod(2) + "." + cod(3) + "." + cod(4)
        Dim Codice_SUB2 As New Class_ELCAR_VOCI_SUB2
        Codice_SUB2.ID = 0
        Codice_SUB2.ID_SUB = 0
        Codice_SUB2.ELCAR = Genitore
        Codice_SUB2.Testo = notaInserita
        Codice_SUB2.Testo_Lungo = ""
        Codice_SUB2.Deleted = False

        If F_CODICE_ADD.Mostra(Codice_SUB2) Then
            'Ricarico la combo 
            Call my_ComboBox4_SelectedIndexChanged()
        End If


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
            DGV1.Rows(idr).Cells(1).Value = Label15.Text
            DGV1.Rows(idr).Cells(2).Value = TextBox1.Text
            'DGV1.Rows(idr).Cells(0).Value = ImageList1.Images(5)

        Else
            If Button3.Tag = "NEW" Then
                DGV1.Rows.Add(ImageList1.Images(6), Label15.Text, TextBox1.Text)  ' Blu
            Else
                DGV1.Rows.Add(ImageList1.Images(5), Label15.Text, TextBox1.Text) 'Bianca
            End If
        End If
        DGV1.Refresh()
        Label10.Text = DGV1.Rows.Count
        Return True

    End Function


    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Gbl_Jb = F_Seleziona_Bambino.Mostra()
        Call Seleziona_bambino(Gbl_Jb)
    End Sub

    Private Sub DGV1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV1.CellContentClick
        Dim c = DGV1.CurrentCell.ColumnIndex
        Dim j = 0
        If c > 1 Then Exit Sub
        Dim r As Integer = DGV1.CurrentRow.Index
        Dim cod = "", strindx = ""
        Button7.Enabled = False
        cod = DGV1(1, r).Value

        If cod = "" Then Exit Sub

        Allinea_combo_al_codice_Elcar(cod)
        'Call Carica_dati_codice_elcar(cod)


        If c <> 0 Then Exit Sub

        If CheckBox4.Checked Then
            Call Carica_dati_codice_elcar(cod)
            Dim TESTO = DGV1(1, r).Value
            F_MyGraph.Mostra(TESTO)
        End If
        If cod <> "" Then
            Button7.Enabled = True
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
    Sub mostra_info_sul_foglio()
        Dim j = Val(Label18.Tag)
        Label18.Text = Gbl_elenco_fogli_bambino(j).titolo
        Label13.Text = Gbl_UltimofoglioGiornaliero_inserito + 1
        Label10.Text = DGV1.Rows.Count - 1
        If DGV1.Rows.Count > 6 Then
            Button5.Enabled = True
        Else
            Button5.Enabled = False
        End If
    End Sub
    Sub Mostra_Testo_riportato()
        Label15.Text = Gbl_Codice_Elcar_Selezionato
        TextBox1.Text = Gbl_Codice_Elcar_TestoSelezionato
        Dim N = Split(Gbl_Codice_Elcar_Selezionato, ".").Count

        If N > 5 Then
            Label15.BackColor = MyColor.LightBlue
            TP1.SetToolTip(Label15, "è stato utilizzato un codice personalizzato per il ragazzo")
            Label9.Visible = True
        Else
            Label15.BackColor = System.Drawing.SystemColors.Control
            TP1.SetToolTip(Label15, "")
            Label9.Visible = False
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
        Dim caricati = False
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
        Call Inserisci_nuovo_Esercizio()
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
            If IsNothing(Gbl_Elenco_Codici_ELCARVOCIPERSONALI(j).TESTO) Then Continue For
            x2 = Trim(Gbl_Elenco_Codici_ELCARVOCIPERSONALI(j).TESTO.ToUpper)
            If x2 = x1 Then
                Return j
            End If
        Next
        Return -1
    End Function
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim t = "salvataggio e stampa del foglio" + vbCrLf
        Dim testo = "Titolo: " + Label18.Text + vbCrLf
        Dim md = CDate(Label11.Tag)
        testo = testo + "  Foglio n." + Label13.Text + " del:" + md.ToShortDateString

        Dim m = testo + vbCrLf + vbCrLf + "Vuoi salvare il nuovo foglio di esercizi?"
        If MsgBox(testo, vbYesNo) <> vbYes Then
            Exit Sub
        End If
        MostraMessaggioModeless(t, testo, vbInformation)
        Dim j = 0

        '-- Registro nel DB BAMBINO tutte le righe presenti -- 
        Dim salvato = True
        For Each R In DGV1.Rows
            j = R.Index
            If Not Salva_Foglio(j) Then salvato = False
        Next
        If Not salvato Then
            MsgBox("Problemi nella scrittura nel database")
            Exit Sub
        End If
        Stampa_Foglio_Giornaliero()

    End Sub

    Function Carica_datinelFoglio(r As Integer, Optional Nuovo As Boolean = True) As Boolean
        ' Registra nel database BAMBINO Una riga del foglio --
        Dim md = CDate(Label11.Tag)

        Dim idff = Label13.Text
        Dim IDF = Convert.ToInt16(idff)
        If Nuovo Then Giornaliero.ID = 0
        Giornaliero.Codice = DGV1.Rows(r).Cells(0).Value?.ToString()
        If IsNothing(Giornaliero.Codice) Then Return False
        Giornaliero.Testo = DGV1.Rows(r).Cells(1).Value?.ToString()
        Giornaliero.ID_FOGLIO = IDF
        Giornaliero.DATA = md.ToShortDateString
        Giornaliero.TOT1 = 0
        Giornaliero.TOT1_NEG = 0
        Giornaliero.TOT1_POS = 0
        Giornaliero.TOT1_PROMPT = 0
        Giornaliero.TOT2 = 0
        Giornaliero.TOT2_NEG = 0
        Giornaliero.TOT2_POS = 0
        Giornaliero.TOT2_PROMPT = 0
        Giornaliero.TUTTELERISPOSTE = True
        Giornaliero.VALUTATIVO = CheckBox3.Checked

        Return True
    End Function

    Function Salva_Foglio(r As Integer, Optional Nuovo As Boolean = True) As Boolean
        ' Registra nel database BAMBINO Una riga del foglio --
        Dim md = CDate(Label11.Tag)

        Dim idff = Label13.Text
        Dim IDF = Convert.ToInt16(idff)
        If Nuovo Then Giornaliero.ID = 0
        Giornaliero.Codice = DGV1.Rows(r).Cells(0).Value?.ToString()
        If IsNothing(Giornaliero.Codice) Then Return False
        Giornaliero.Testo = DGV1.Rows(r).Cells(1).Value?.ToString()
        Giornaliero.ID_FOGLIO = IDF
        Giornaliero.DATA = md.ToShortDateString
        Giornaliero.TOT1 = 0
        Giornaliero.TOT1_NEG = 0
        Giornaliero.TOT1_POS = 0
        Giornaliero.TOT1_PROMPT = 0
        Giornaliero.TOT2 = 0
        Giornaliero.TOT2_NEG = 0
        Giornaliero.TOT2_POS = 0
        Giornaliero.TOT2_PROMPT = 0
        Giornaliero.TUTTELERISPOSTE = True
        Giornaliero.VALUTATIVO = CheckBox3.Checked

        If Not Giornaliero.Salva Then Return False
        Return True
    End Function
    Sub Stampa_Foglio_Giornaliero(Optional Prova = False)
        Dim ID_foglio = CInt(Label13.Text)
        Dim titolo = Gbl_Giornaliero.Titolo
        Dim Data = Strings.Format(CDate(Gbl_VociDiUnFoglio(0).DATA), "yyyy-MM-dd")

        Dim nomefoglio = Costruisci_nome_foglio(ID_foglio, titolo, Data)
        Dim documento = dir_bar(ini.Path_SCHEDE_DaCompilare) + nomefoglio

        '-- controllo se esiste gia un file con o stesso nome --
        If IO.File.Exists(documento) Then
            Dim mm = "E' già presente un foglio con lo stesso nome" + vbCrLf
            mm = mm + "path:" + ini.Path_SCHEDE_DaCompilare + vbCrLf
            mm = mm + "nome:" + nomefoglio + vbCrLf
            mm = mm + "Vuoi salvarlo ugualmente?"
            If MsgBox(mm, vbYesNo + vbInformation, "Foglio gia presente") = vbNo Then
                Call MsgBox("Foglio non salvato")
                Exit Sub
            Else
                Try
                    IO.File.Delete(documento)
                Catch ex As Exception
                    Call MsgBox("NON è stato possibile cancellare il foglio esistente")
                    Exit Sub
                End Try
            End If
        End If

        '-- Salvo il foglio --
        IO.File.Copy(ini.Modello_Schede, documento, True)


        ID_foglio = CInt(Label13.Text)
        Data = Strings.Format(CDate(Gbl_VociDiUnFoglio(0).DATA), "yyyy-MM-dd")
        titolo = Gbl_Giornaliero.Titolo
        If Not Prova Then
            Gbl_VociDiUnFoglio = Gbl_Giornaliero.Carica_Foglio(ID_foglio)
        End If
        '-- compilo il foglio --
        Using doc As WordprocessingDocument =
            WordprocessingDocument.Open(documento, True)

            Dim body As Body = doc.MainDocumentPart.Document.Body

            If compila_foglio_giornaliero(body, Gbl_NOME_BAMBINO, Data, ID_foglio, titolo) Then

                doc.MainDocumentPart.Document.Save()
            End If

        End Using
        Dim m = "Foglio (" + nomefoglio + ") preparato" + vbCrLf
        m = m + "in: " + ini.Path_SCHEDE_DaCompilare

        Call MsgBox(m)
    End Sub

    Function compila_foglio_giornaliero(body As Body, nomebambino As String, data As String, id_foglio As Integer, titolo As String) As Boolean
        ' iNTESTAZONE 
        SET_DOCX(body, "<<BAMBINO>>", nomebambino)
        SET_DOCX(body, "<<FOGLIO>>", id_foglio.ToString)
        SET_DOCX(body, "<<DATA>>", data)
        SET_DOCX(body, "<<TITOLO>>", titolo)

        For j = 0 To Gbl_VociDiUnFoglio.Count - 1
            Dim codice = Gbl_VociDiUnFoglio(j).Codice
            If codice = "" Then Continue For
            Dim testo = Gbl_VociDiUnFoglio(j).Testo
            SET_DOCX(body, "<<COD" + (j + 1).ToString + ">>", codice)
            SET_DOCX(body, "<<TESTO" + (j + 1).ToString + ">>", testo)
        Next
        Return True
    End Function
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

    Private Sub Button6_Click(sender As Object, e As EventArgs)
        Stampa_Foglio_Giornaliero()
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

    Private Sub ComboBox7_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox7.SelectedIndexChanged
        Dim J = ComboBox7.SelectedIndex
        Dim modo = True
        If CInt(Label10.Text) > 0 Then modo = False
        F_Foglio_esistente.Main(J,, modo)

        Dim ID_foglio = CInt(Gbl_elenco_fogli_bambino(J).ID_Foglio)

        Gbl_VociDiUnFoglio = Gbl_Giornaliero.Carica_Foglio(ID_foglio)
        Dim n = Gbl_VociDiUnFoglio.Count


    End Sub

    Public Sub Carica_foglio(j)
        Label18.Tag = j
        Dim ID_foglio = CInt(Gbl_elenco_fogli_bambino(j).ID_Foglio)
        Gbl_VociDiUnFoglio = Gbl_Giornaliero.Carica_Foglio(ID_foglio)
        Dim r = 0
        DGV1.Rows.Clear()
        Dim xtips = "", img_index = 0
        For Each foglio In Gbl_VociDiUnFoglio
            Call Carica_dati_codice_elcar(foglio.Codice)
            Call Trend_1()



            If Gbl_Graph.Trend = "SUPERATO" Then
                img_index = 2 : xtips = "Soglia raggiunta - Cambiare Esercizio"
            ElseIf Gbl_Graph.Trend = "POS4" Then
                img_index = 6 : xtips = "Trend Positivo (Continuare)"
            ElseIf Gbl_Graph.Trend = "NEG4" Then
                img_index = 3 : xtips = "Trend NEGATIVO (Cambiare esercizio)"
            Else
                If UBound(Gbl_Percentuale) < 3 Then
                    img_index = 4 : xtips = "Statistica insufficiente (" + UBound(Gbl_Percentuale).ToString + ") (continuare)"
                ElseIf UBound(Gbl_Percentuale) < 6 Then
                    img_index = 3 : xtips = "Statistica insufficiente (" + UBound(Gbl_Percentuale).ToString + ") (continuare)"
                Else
                    img_index = 8 : xtips = "trend NON DEFINITO (Riflettere)"
                End If
            End If
            DGV1.Rows.Add(ImageList1.Images(img_index), foglio.Codice, foglio.Testo)
            r = DGV1.Rows.Count - 1
            DGV1.Rows(r).Cells(1).ToolTipText = xtips

        Next
        DGV1.ShowCellToolTips = True
        DGV1.Refresh()
        Call mostra_info_sul_foglio()
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
        MsgBox(Gbl_Codice_Elcar_TestoLungo)

    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked Then
            DGV1.BackgroundColor = MyColor.Aquamarine
            GroupBox4.BackColor = MyColor.Aquamarine
            CheckBox3.ForeColor = MyColor.Blue
            GroupBox4.Text = "Foglio esercizi [ADMITTANCE]"
        Else
            DGV1.BackgroundColor = SystemColors.ControlDark
            GroupBox4.BackColor = MyColor.Transparent
            CheckBox3.ForeColor = MyColor.Black
            GroupBox4.Text = "Foglio esercizi Standard]"
        End If
        DGV1.Refresh()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        Dim m = "Vuoi modificare il codice inserito con quello nuovo Selezionato?" + vbCrLf
        If MsgBox(m, vbYesNo) <> vbYes Then Exit Sub
        inserisci_il_codice_nella_Griglia(True)

    End Sub

    Private Sub GroupBox4_Enter(sender As Object, e As EventArgs) Handles GroupBox4.Enter

    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click


        ' Ultimo foglio del bambino 
        If Gbl_elenco_fogli_bambino.Count = 0 Then
            Call MsgBox("Nessun foglio trovato")
            Exit Sub
        End If

        Dim ID_foglio = Gbl_elenco_fogli_bambino(0).ID_Foglio
        If ID_foglio = 0 Then
            Call MsgBox("Nessun foglio valido trovato")
        End If


        Dim modo = True

        'If CInt(Label10.Text) > 0 Then modo = False
        If Label18.Text = " - -" Then modo = False  ' non cè un titolo del foglio
        F_Foglio_esistente.Main(0, , modo)
        Call Imposta_tipo_caricamento_ultimo_foglio()

        Gbl_VociDiUnFoglio = Gbl_Giornaliero.Carica_Foglio(ID_foglio)
        Dim n = Gbl_VociDiUnFoglio.Count


    End Sub
    Sub carica_ultimo_foglio_bambino()


        Dim ID_foglio = CInt(Gbl_elenco_fogli_bambino(1).ID_Foglio)
        Gbl_VociDiUnFoglio = Gbl_Giornaliero.Carica_Foglio(ID_foglio)
        Dim n = Gbl_VociDiUnFoglio.Count
        'Dim j = CInt(Label7.Tag)
        Carica_foglio(1)

    End Sub
    Private Sub CheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox4.CheckedChanged
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        CMenuStrip1.Show(Button10, New Point(0, Button10.Height))

    End Sub

    Private Sub GroupBox5_Enter(sender As Object, e As EventArgs) Handles GroupBox5.Enter

    End Sub

    Private Sub ImmediatoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TSM_Immediato.Click
        Imposta_tipo_caricamento_ultimo_foglio()


    End Sub

    Private Sub TSM_NONImmediato_Click(sender As Object, e As EventArgs)
        Imposta_tipo_caricamento_ultimo_foglio()

    End Sub
    Sub Imposta_tipo_caricamento_ultimo_foglio()
        If TSM_Immediato.Checked Then
            Gbl_Caricamento_Immediato = True
            Button9.BackColor = MyColor.LightGreen
        Else
            Gbl_Caricamento_Immediato = False
            TSM_Immediato.Checked = False
            Button9.BackColor = SystemColors.Control
        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click

    End Sub

    Private Sub Label18_Click(sender As Object, e As EventArgs) Handles Label18.Click
        Call Gbl_Giornaliero.Carica_titoli()
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub ComboBox8_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox8.SelectedIndexChanged

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            F_MyGraph.Hide()
        Catch ex As Exception

        End Try

        Hide()
    End Sub

    Private Sub Label19_Click(sender As Object, e As EventArgs) Handles Label19.Click
        Dim dataScelta As DateTime? = SelezionaDataDaCalendario("Seleziona la data di esecuzione dell'esercizio")

        ' Verifica se l'utente ha effettivamente scelto una data o se ha annullato
        If dataScelta.HasValue Then
            ' Mostra la data nella label usando il formato esteso che abbiamo visto prima
            Label11.Tag = dataScelta.Value.ToString("dddd d MMMM yyyy")
            Label11.Text = "(" + dataScelta.Value.ToString("dddd d MMMM yyyy") + ")"
        Else
            ' Gestisci il caso in cui l'utente ha chiuso il calendario senza scegliere
            MessageBox.Show("Nessuna data selezionata.", "Avviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If


    End Sub

    Private Sub Button6_Click_1(sender As Object, e As EventArgs) Handles Button6.Click
        Dim j = 0
        For Each R In DGV1.Rows
            j = R.Index
            Carica_datinelFoglio(j)
        Next

        Stampa_Foglio_Giornaliero(True)

    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        F_Elenco_da_Stampare.Main(Label11.Tag)
    End Sub
End Class