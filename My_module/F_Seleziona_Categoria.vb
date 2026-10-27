
Imports System.Runtime.InteropServices.JavaScript.JSType
Imports DocumentFormat.OpenXml
Imports DocumentFormat.OpenXml.EMMA
Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Spreadsheet
Imports DocumentFormat.OpenXml.Wordprocessing
Imports Microsoft.Data
Imports Microsoft.IdentityModel.Tokens
Imports OpenTK.Graphics.OpenGL
Imports ScottPlot.AxisLimitManagers
Imports ScottPlot.Colormaps
Imports ScottPlot.Statistics
Imports SkiaSharp.HarfBuzz.SKShaper
Imports SQLitePCL
Imports MyColor = System.Drawing.Color



Public Class F_Seleziona_Categoria
    Public UltimoFoglioGiornaliero_Inserito As Integer
    Public Giornaliero As New Class_GIORNALIERO
    Public Structure CODICELCAR
        Public Property CODICE As String
        Public Property TESTO As String
        Public Property J As Integer
    End Structure


    Private Sub F_Crea_Scheda_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' Definizione dei tooltip per i pulsanti
        TP1.SetToolTip(Button1, "Crea nuova scheda")
        TP1.SetToolTip(Button3, "Elimina scheda")
        TP1.SetToolTip(Button8, "Seleziona ragazzo")

        DGV1.AllowUserToAddRows = False   ' Impedisce di visualizzare l'ultima riga vuota 

        ReDim cod(7)

        Call Carica_Combobox0()   ' combo con codici elcar 1 e 2 (es V.A)

        ' Imposto la data del prossimo giorno lavorativo 
        Dim mydata = Prossimo_giorno_lavorativo(Now)
        Gbl_foglio.Data = mydata

        'Dim mydata As DateTime = DateTime.Today.AddDays(1)
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

        ComboBox8.Tag = 0   ' Memorizzo l'indice del bambino
        mostra_info_sul_foglio()


    End Sub
    Sub Azzera_totali_voci_combo(Optional start As Integer = 1)
        If start <= 3 Then Label12.Text = "Tot 0 voci" : Label12.Refresh()
        If start <= 4 Then Label7.Text = "Tot 0 voci" : Label7.Refresh()
        If start <= 5 Then Label16.Text = "Tot 0 voci" : Label16.Refresh()
        If start <= 6 Then Label17.Text = "Tot 0 voci" : Label17.Refresh()
    End Sub
    Function Seleziona_bambino(Gbl_Jb) As Integer

        ' Costruisci_codice_finale il nome del file del bambio e creo il database se non esiste
        Dim DB_BAMBINO = Gbl_Elenco_bambini(Gbl_Jb).DBname.ToUpper
        Dim filedb = Costruisci_filenameDB_bambino(DB_BAMBINO)
        'Gbl_NOME_BAMBINO = Gbl_Elenco_bambini(Gbl_Jb).Cognome.ToUpper + " " + Gbl_Elenco_bambini(Gbl_Jb).Nome.ToUpper
        'Gbl_NOME_BAMBINO = Gbl_Elenco_bambini(Gbl_Jb).Nome.ToUpper + " " + Gbl_Elenco_bambini(Gbl_Jb).Cognome.ToUpper
        DB_BAMBINO = Gbl_Elenco_bambini(Gbl_Jb).DBname.ToUpper
        If InStr(DB_BAMBINO, ".DB") = 0 Then DB_BAMBINO = DB_BAMBINO + ".DB"
        'Label9.Text = Gbl_Elenco_bambini(Gbl_Jb).Cognome + " " + Gbl_Elenco_bambini(Gbl_Jb).Nome : Label9.Refresh()
        Gbl_foglio.DB_Bambino = DB_BAMBINO
        Gbl_foglio.ID_Bambino = Gbl_Elenco_bambini(Gbl_Jb).ID

        Gbl_DB_BAMBINO = Gbl_foglio.DB_Bambino
        Label9.Tag = Gbl_Jb



        Gbl_UltimofoglioGiornaliero_inserito = Class_GIORNALIERO.Trova_ultimo_IDFOGLIO
        Label13.Text = Gbl_UltimofoglioGiornaliero_inserito + 1

        Dim risultati1 As List(Of Class_GIORNALIERO) = Class_GIORNALIERO.CercaPerCodice("")

        If risultati1.Count = 0 Then
            'ComboBox7.Items.Clear()
            Return risultati1.Count

        End If
        'cARICA_COMBO_FOGLI_BAMBINO()
        Return risultati1.Count


    End Function

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


    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged

        If ComboBox3.SelectedIndex <> -1 Then
            ' Il trucco è usare BeginInvoke: dice a Windows di eseguire lo spostamento 
            ' del cursore un millisecondo DOPO che la ComboBox ha finito di elaborare lo scorrimento del mouse.
            BeginInvoke(Sub()
                            ' Forza il cursore all'inizio del testo (posizione 0)
                            ComboBox3.SelectionStart = 0
                            ComboBox3.SelectionLength = 0
                        End Sub)
        End If

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

        If Button3.Tag <> "NEW" Then
            Call mostra_info_sul_foglio()
            Exit Sub
        End If
        Call Salva_nuovo_codice_personale()
        Call Mostra_Testo_riportato()
        Call mostra_info_sul_foglio()
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
                DGV1.Rows.Add(ImageList4.Images(6), Label15.Text, TextBox1.Text, "", 0)  ' Rombo Arancione 
            Else
                DGV1.Rows.Add(ImageList4.Images(6), Label15.Text, TextBox1.Text, "", 0) 'Bianca
            End If
        End If
        DGV1.Refresh()
        Label10.Text = Conta_codici_inseriti().NELCAR
        Return True

    End Function


    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Gbl_Jb = F_Seleziona_Bambino.Mostra()
        Call Seleziona_bambino(Gbl_Jb)
    End Sub
    Function Conta_codici_inseriti() As (NELCAR As Integer, NCP As Integer, NRT As Integer)
        ' Ritorna il numero di voce inserite ELCAR,CP,RT
        Dim conteggio = 0
        For j = 0 To DGV1.Rows.Count - 1
            If DGV1.Rows(j).Cells(1).Value <> "" Then conteggio = conteggio + 1
        Next

        Dim nelcar = 0, NCP = 0, nrT = 0
        Try
            nelcar = conteggio
        Catch ex As Exception
        End Try
        Try
            nrT = Gbl_foglio.RT.Count
        Catch ex As Exception

        End Try
        Try
            NCP = Gbl_foglio.CP.Count
        Catch ex As Exception
        End Try

        Return (nelcar, NCP, nrT)
    End Function
    Function Conta_codici_inseriti_OLD() As Integer
        ' Ritorna il numero di voce inserite ELCAR,CP,RT
        Dim conteggio = 0
        For j = 0 To DGV1.Rows.Count - 1
            If DGV1.Rows(j).Cells(1).Value <> "" Then conteggio = conteggio + 1
        Next
        Return conteggio
    End Function
    Private Sub DGV1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV1.CellContentClick

        Dim c = DGV1.CurrentCell.ColumnIndex

        If c > 1 Then Exit Sub
        Dim r As Integer = DGV1.CurrentRow.Index
        Dim cod = "", strindx = ""
        Button7.Enabled = False
        cod = DGV1(1, r).Value

        If cod = "" Then Exit Sub

        Allinea_combo_al_codice_Elcar(cod)
        If cod <> "" Then Button7.Enabled = True


        If c <> 0 Then Exit Sub

        If CheckBox4.Checked Then
            Call Carica_dati_codice_elcar(cod)
            Dim TESTO = DGV1(1, r).Value
            If Not IsNothing(TESTO) Then
                F_MyGraph.Mostra(TESTO)
            End If
        End If



    End Sub
    Sub Sincronizza_Combo_con_Griglia(Cod)
        Dim x = Split(Cod, ".")
        Dim n = x.Count
        'ComboBox1.Text = x(0) : ComboBox1.Refresh()
        'ComboBox2.Text = x(1) : ComboBox2.Refresh()

    End Sub
    Sub mostra_info_sul_foglio()

        Dim cnt = Conta_codici_inseriti()
        Dim dacontrollare = False
        If Gbl_foglio.ID_Bambino <= 0 Then dacontrollare = True
        If Gbl_foglio.DB_Bambino = "" Then dacontrollare = True

        If dacontrollare Then
            ' NON è DEFINITO L'iD DEL BAMBINO O IL SUO DATABASE
            Label22.Text = "CHECK!"
            Label22.BackColor = MyColor.Red
            Label22.ForeColor = MyColor.White
            GroupBox4.Enabled = False
            Exit Sub
        End If

        GroupBox4.Enabled = True



        Label13.Text = Gbl_UltimofoglioGiornaliero_inserito + 1
        Label10.Text = cnt.NELCAR

        'Trova la data, ovvero il prossimo giorno lavorativo 
        Dim nextday = Prossimo_giorno_lavorativo(Now)
        Gbl_foglio.Data = nextday



        If cnt.NELCAR < ini.Numero_min_voci_perpagina Then
            Button5.Enabled = False
            Button5.BackColor = MyColor.Gray
        Else
            Button5.Enabled = True
            Button5.BackColor = MyColor.DodgerBlue
        End If

        Aggiorna_pulsante_CP(cnt.NCP)
        Aggiorna_pulsante_RT(cnt.NRT)

        If cnt.NCP + cnt.NRT + cnt.NELCAR > 0 Then Label11.Enabled = True Else Label11.Enabled = False

        Call Mostra_avviso()

    End Sub
    Sub Mostra_avviso()
        'visualizza una bandierina 
        Label4.Visible = False


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
    Function Foglio_Prepara_salvataggio_di_un_nuovo_foglio() As Boolean
        ' Prepara la struttura Gbl_foglo per poter essere poi salvata 
        Dim idff = Label13.Text   ' nuovo id_foglio
        Dim is_compilato = False
        Gbl_foglio.ID_foglio = idff
        Gbl_foglio.DB_Bambino = Gbl_DB_BAMBINO
        Gbl_foglio.Compilato = False
        'Gbl_foglio.Bambino = Gbl_NOME_BAMBINO
        Gbl_foglio.Data = CDate(Label11.Tag).ToShortDateString

        Dim giornaliero1 As New Class_GIORNALIERO

        'Preparo Vocidiunfoglio 
        Dim errore_giornaliero = ""
        Dim ListofCLASS_GIORNALIERO As New List(Of Class_GIORNALIERO)

        For Each R In DGV1.Rows
            Dim j = R.Index
            ' controllo se è stato inserito un codice ELCAR
            Dim x = DGV1.Rows(j).Cells(1).Value?.ToString()
            If x = "" Then Continue For
            ' Carico i valori presenti din DGV1 nella struttura giornaliero
            giornaliero1 = Carica_Giornaliero_da_Dgv1(j, Gbl_foglio.Data, idff, True)
            ListofCLASS_GIORNALIERO.Add(giornaliero1)
        Next

        If ListofCLASS_GIORNALIERO.Count > 0 Then
            Gbl_foglio.VociDiUnFoglio = ListofCLASS_GIORNALIERO
        End If

        'Preparo le voci RT
        Gbl_RT_selezionate = Gbl_foglio.RT
        For j = 0 To Gbl_RT_selezionate.Count - 1
            Gbl_RT_selezionate(j).ID_FOGLIO = idff
            Gbl_RT_selezionate(j).DATA = Gbl_foglio.Data
            Gbl_RT_selezionate(j).COMPILATO = False
            Gbl_RT_selezionate(j).TOT = 0
            Gbl_RT_selezionate(j).ID = 0
        Next

        If Gbl_RT_selezionate.Count > 0 Then
            Gbl_foglio.RT = Gbl_RT_selezionate
        End If

        'Preparo le voci CP
        Gbl_CP_selezionate = Gbl_foglio.CP
        For j = 0 To Gbl_CP_selezionate.Count - 1
            If Gbl_CP_selezionate(j).ID_VOCE > 0 Then
                Gbl_CP_selezionate(j).ID_FOGLIO = idff
                Gbl_CP_selezionate(j).DATA = Gbl_foglio.Data
                Gbl_CP_selezionate(j).TOT = 0
                Gbl_CP_selezionate(j).COMPILATO = False
                Gbl_CP_selezionate(j).ID = 0
                Dim x = UCase(Gbl_CP_selezionate(j).TESTO)
                If x = "TRUE" Or x = "" Then
                    'trova il testo della voce CP
                    Dim cp = Class_COMPORTAMENTI.Load(Gbl_CP_selezionate(j).ID_VOCE)
                    Gbl_CP_selezionate(j).TESTO = cp.TESTO
                End If
            End If
        Next
        If Gbl_CP_selezionate.Count > 0 Then
            Gbl_foglio.CP = Gbl_CP_selezionate
        End If

    End Function
    Function Foglio_Verifica_consistenza_GBL_foglio() As (Successo As Boolean, Error_list As String())
        Dim error_list(0) As String
        Dim error_cnt = -1

        If Gbl_foglio.DB_Bambino = "" Then
            error_cnt = error_cnt + 1 : ReDim Preserve error_list(error_cnt)
            error_list(error_cnt) = "Nome database bambino assente"
        End If

        If Not IsDate(Gbl_foglio.Data) Then
            error_cnt = error_cnt + 1 : ReDim Preserve error_list(error_cnt)
            error_list(error_cnt) = "Data non corretta"
        End If

        If Val(Gbl_foglio.ID_foglio) <= 0 Then
            error_cnt = error_cnt + 1 : ReDim Preserve error_list(error_cnt)
            error_list(error_cnt) = "ID Foglio non inserito"
        End If

        If Val(Gbl_foglio.ID_Bambino) <= 0 Then
            error_cnt = error_cnt + 1 : ReDim Preserve error_list(error_cnt)
            error_list(error_cnt) = "ID Bambino non inserito"
        End If

        'Controllo vocidiunfoglio
        If Gbl_foglio.VociDiUnFoglio.Count = 0 Then
            error_cnt = error_cnt + 1 : ReDim Preserve error_list(error_cnt)
            error_list(error_cnt) = "Voci ELCAR non inserite"
        Else
            For j = 0 To Gbl_foglio.VociDiUnFoglio.Count - 1
                'Correggo eventuali discrepanza
                If Gbl_foglio.VociDiUnFoglio(j).ID_FOGLIO <> Gbl_foglio.ID_foglio Then Gbl_foglio.VociDiUnFoglio(j).ID_FOGLIO = Gbl_foglio.ID_foglio
                If Gbl_foglio.VociDiUnFoglio(j).DATA <> Gbl_foglio.Data Then Gbl_foglio.VociDiUnFoglio(j).DATA = Gbl_foglio.Data
                If Gbl_foglio.VociDiUnFoglio(j).Codice = "" Then
                    error_cnt = error_cnt + 1 : ReDim Preserve error_list(error_cnt)
                    error_list(error_cnt) = "CODICE non inserito"
                End If
            Next
        End If


        'Controllo RT
        If Gbl_foglio.RT.Count = 0 Then
            error_cnt = error_cnt + 1 : ReDim Preserve error_list(error_cnt)
            error_list(error_cnt) = "Voci RT non inserite"
        Else
            For j = 0 To Gbl_foglio.RT.Count - 1
                'Correggo eventuali discrepanza
                If Gbl_foglio.RT(j).ID_FOGLIO <> Gbl_foglio.ID_foglio Then Gbl_foglio.RT(j).ID_FOGLIO = Gbl_foglio.ID_foglio
                If Gbl_foglio.RT(j).DATA <> Gbl_foglio.Data Then Gbl_foglio.RT(j).DATA = Gbl_foglio.Data
                If Gbl_foglio.RT(j).ID_VOCE = 0 Then
                    error_cnt = error_cnt + 1 : ReDim Preserve error_list(error_cnt)
                    error_list(error_cnt) = "ID_VOCE RT non inserita"
                End If
            Next
        End If

        'Controllo CP
        If Gbl_foglio.CP.Count = 0 Then
            error_cnt = error_cnt + 1 : ReDim Preserve error_list(error_cnt)
            error_list(error_cnt) = "Voci CP non inserite"
        Else
            For j = 0 To Gbl_foglio.CP.Count - 1
                'Correggo eventuali discrepanza
                If Gbl_foglio.CP(j).ID_FOGLIO <> Gbl_foglio.ID_foglio Then Gbl_foglio.CP(j).ID_FOGLIO = Gbl_foglio.ID_foglio
                If Gbl_foglio.CP(j).DATA <> Gbl_foglio.Data Then Gbl_foglio.CP(j).DATA = Gbl_foglio.Data
                If Gbl_foglio.CP(j).ID_VOCE = 0 Then
                    error_cnt = error_cnt + 1 : ReDim Preserve error_list(error_cnt)
                    error_list(error_cnt) = "ID_VOCE CP non inserita"
                Else
                    Dim x = Gbl_foglio.CP(j).TESTO.ToUpper
                    If x = "" Or x = "TRUE" Then
                        Dim y = 1
                    End If
                End If
            Next
        End If

        Dim successo = True
        If error_cnt > 0 Then successo = False
        Return (successo, error_list)




    End Function
    Function Foglio_Carica_e_Salva() As Boolean

        '---------------------------------------------------------------------------------------
        '-- Carica tutti i dati nella variabile GBL_foglio per il salvataggio
        '---------------------------------------------------------------------------------------
        Foglio_Prepara_salvataggio_di_un_nuovo_foglio()

        '---------------------------------------------------------------------------------------
        '--- Verifica_codici_duplicati e completa i dati delle variabili di GBL_foglio
        '---------------------------------------------------------------------------------------
        Dim result = Foglio_Verifica_consistenza_GBL_foglio()
        If Not result.Successo Then
            Call MsgBox("Errore")
        End If

        '---------------------------------------------------------------------------------------
        ' Salva fisicamente i ddati nel database --
        '---------------------------------------------------------------------------------------
        Dim salvato = Foglio_salva()

        If salvato Then
            '---------------------------------------------------------------------------------------
            ' Aggiungo il foglio alla lista TODOLIST  --
            '---------------------------------------------------------------------------------------
            ToDoList_Aggiungi_voce_foglio(Gbl_foglio)
        End If

        Return salvato


    End Function
    Function Foglio_salva() As Boolean
        '-- Preparo messagio di conferma salvataggio
        Dim CPcnt = 0, RTcnt = 0, ELCARcnt = 0
        Dim salvare_cp = True, salvare_rt = True
        Dim t = "Salvataggio del foglio" + vbCrLf
        Try
            CPcnt = Gbl_foglio.CP.Count
        Catch ex As Exception
        End Try
        Try
            RTcnt = Gbl_foglio.RT.Count
        Catch ex As Exception
        End Try
        Try
            ELCARcnt = Gbl_foglio.VociDiUnFoglio.Count
        Catch ex As Exception
        End Try

        Dim testo = "Foglio n." + Gbl_foglio.ID_foglio.ToString + " del:" + Gbl_foglio.Data + vbCrLf
        testo = testo + "N. Esercizi:     " + ELCARcnt.ToString + vbCrLf
        testo = testo + "N. voci CP:      " + CPcnt.ToString + vbCrLf
        testo = testo + "N. voci ROUTINE: " + RTcnt.ToString + vbCrLf
        Dim m = testo + vbCrLf + "Vuoi salvare il nuovo foglio di esercizi?"
        If MsgBox(testo, vbYesNo + vbQuestion, t) <> vbYes Then
            Return False
        End If

        'Procedo con il salvataggio nelle 3 tabelle
        If CPcnt = 0 Then salvare_cp = False
        If RTcnt = 0 Then salvare_rt = False

        'Controllo se esiste gia nel database un foglio con lo stesso titolo e Data
        Dim elenco_fogli As New Class_Elenco_fogli()
        Dim trovato = elenco_fogli.Foglio_esistente(Gbl_foglio.Data)
        If trovato Then
            m = "Non è stato possibile salvare il nuovo foglio " + vbCrLf
            m = m + "perchè è stato trovato un altro foglio con la stesso stessa Data"
            m = m + "foglio id: " + Gbl_foglio.ID_foglio.ToString + ") del: " + Gbl_foglio.Data + ""
            Call MsgBox(m, vbCritical + vbOKOnly)
            m = "[Errore 001] Salvataggio foglio " + Gbl_foglio.ID_foglio.ToString + ") del: " + Gbl_foglio.Data + " " + Gbl_foglio.DB_Bambino
            APPEND_LOG(m, ini.File_Log, True)
            Dim st As New StackTrace()
            Dim frame As StackFrame = st.GetFrame(1)
            Return False
        End If


        '-- Registro nel GIORNALIERO del DB BAMBINO tutte le righe presenti -- 
        Dim salvato = True
        Dim idff = Label13.Text   ' nuovo id_foglio
        For j = 0 To ELCARcnt - 1

            If Gbl_foglio.VociDiUnFoglio(j).Codice = "" Then Continue For
            If Not Gbl_foglio.VociDiUnFoglio(j).Salva Then
                m = "Problemi salvataggio codice (" + Giornaliero.Codice + ") id_foglio:" + Giornaliero.ID_FOGLIO.ToString + " DB:" + Gbl_DB_BAMBINO
                APPEND_LOG(m, GbL_Logfile, True)
                salvato = False
            End If
        Next

        '-- Registro nel GIORNALIERO_CP del DB BAMBINO tutte le righe presenti -- 
        If salvare_cp Then
            Gbl_CP_selezionate = Gbl_foglio.CP
            For j = 0 To Gbl_CP_selezionate.Count - 1
                If Not Gbl_foglio.CP(j).Salva() Then
                    m = "[Errore 002] Problemi salvataggio CP (" + Gbl_foglio.CP(j).TESTO + ") id_foglio:" + Gbl_foglio.CP(j).ID_FOGLIO.ToString + " DB:" + Gbl_foglio.DB_Bambino
                    APPEND_LOG(m, GbL_Logfile, True)
                    salvato = False

                End If
            Next
        End If


        '-- Registro nel GIORNALIERO_ROUTINE del DB BAMBINO tutte le righe della routine presenti -- 
        If salvare_rt Then
            Gbl_RT_selezionate = Gbl_foglio.RT
            For j = 0 To Gbl_RT_selezionate.Count - 1
                If Not Gbl_foglio.RT(j).Salva() Then
                    m = "[Errore 003] Problemi salvataggio RT (" + Gbl_foglio.RT(j).TESTO + ") id_foglio:" + Gbl_foglio.RT(j).ID_FOGLIO.ToString + " DB:" + Gbl_foglio.DB_Bambino
                    APPEND_LOG(m, GbL_Logfile, True)
                    salvato = False

                End If
            Next
        End If
        Return salvato



    End Function
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        Dim salvato = Foglio_Carica_e_Salva()

        If Not salvato Then Exit Sub
        Stampa_foglio_giornaliero()

    End Sub
    Function Carica_Giornaliero_da_Dgv1(r As Integer, Data As String, idff As Integer, Optional Nuovo As Boolean = True) As Class_GIORNALIERO
        '-- Carico la struttura giornaliero con i valori contenuti nella riga R della DGV1 --
        'Dim md = CDate(Label11.Tag)
        'Dim idff = Label13.Text
        'Dim IDF = Convert.ToInt16(idff)
        Dim Giornaliero1 As New Class_GIORNALIERO
        If Nuovo Then
            Giornaliero1.ID = 0
            Giornaliero1.ID_FOGLIO = idff
            Giornaliero1.DATA = Data
            Giornaliero1.COMPILATO = False
        End If

        Giornaliero1.Codice = DGV1.Rows(r).Cells(1).Value?.ToString()
        Giornaliero1.Testo = DGV1.Rows(r).Cells(2).Value?.ToString()
        Giornaliero1.TOT1 = 0
        Giornaliero1.TOT1_NEG = 0
        Giornaliero1.TOT1_POS = 0
        Giornaliero1.TOT1_PROMPT = 0
        Giornaliero1.TOT2 = 0
        Giornaliero1.TOT2_NEG = 0
        Giornaliero1.TOT2_POS = 0
        Giornaliero1.TOT2_PROMPT = 0
        Giornaliero1.TUTTELERISPOSTE = True
        Giornaliero1.VALUTATIVO = CheckBox3.Checked
        Return Giornaliero1

    End Function


    Function Verifica_Se_gia_inserito_Nel_Foglio(cod) As Boolean
        Dim nrow = DGV1.Rows.Count - 1
        For i As Integer = 0 To nrow
            If DGV1.Rows(i).Cells(1).Value?.ToString() = "" Then Continue For
            If cod = DGV1.Rows(i).Cells(1).Value?.ToString() Then
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

    Public Sub Carica_dati_foglio_in_DGV1()
        'Carica l'elenco dei codici presenti in gbl_vocidiunfoglio nella datagridview
        Dim r = 0,
        ndati = 0  ' n. risposte trovate

        DGV1.Rows.Clear()
        Dim xtips = "", img_index = 0
        Dim indice = 0
        For Each foglio In Gbl_VociDiUnFoglio
            ndati = Carica_dati_codice_elcar(foglio.Codice)
            'If Carica_dati() = 0 Then Return
            If ndati >= 0 Then
                Call Trend_1()
                Dim result = Trova_immagine_corretta_DGV1()
                img_index = result.img_index
                xtips = result.xtips
            Else
                img_index = 1
                xtips = "Nessun Dato inserito"   ' Bianco
            End If
            DGV1.Rows.Add(ImageList4.Images(img_index), foglio.Codice, foglio.Testo, "", indice)
            r = DGV1.Rows.Count - 1
            DGV1.Rows(r).Cells(1).ToolTipText = xtips
            indice += 1

        Next
        DGV1.ShowCellToolTips = True
        DGV1.Refresh()
        Call mostra_info_sul_foglio()
    End Sub
    Function Trova_immagine_corretta_DGV1() As (img_index As Integer, xtips As String)
        Dim img_index = 0, xtips = ""

        If Gbl_Graph.Trend = "SUPERATO" Then
            img_index = 4 : xtips = "Soglia raggiunta - Cambiare Esercizio"   'Verde
        ElseIf Gbl_Graph.Trend = "POS4" Then
            img_index = 3 : xtips = "Trend Positivo (Continuare)"             ' Blu
        ElseIf Gbl_Graph.Trend = "NEG4" Then
            img_index = 2 : xtips = "Trend NEGATIVO (Cambiare esercizio)"     'Rosso
        ElseIf Gbl_Graph.Trend = "NODATA" Then
            img_index = 1 : xtips = "Nessun dato inserito (continuare)"   ' Nero
        ElseIf Gbl_Graph.Trend = "NOTREND" Then
            img_index = 5 : xtips = "Nessun Trend rilevabile (Riflettere)"   'giallo
        ElseIf Gbl_Graph.Trend = "UNDEF" Then
            img_index = 0 : xtips = "Statistica insufficiente (" + UBound(Gbl_Percentuale).ToString + ") (continuare)"   ' Nero
        Else
        End If
        Return (img_index, xtips)

    End Function

    Sub Allinea_combo_al_codice_Elcar(codice)
        Dim x = Split(codice, ".")
        Dim n = x.Count
        If n < 0 Then Exit Sub

        Dim risultati = -1
        Dim testodacercare = ""


        If n < 1 Then Exit Sub
        testodacercare = "(" + x(0) + "." + x(1) + ")"

        risultati = OttieniPrimoIndicediunacombo(ComboBox0, testodacercare)
        If risultati >= 0 Then
            ComboBox0.SelectedIndex = risultati
            'Gbl_Codice_Elcar_Selezionato = ComboBox1.Text + "." + ComboBox2.Text
            'cod(2) = ComboBox2.Text
            Dim xx = ComboBox0.SelectedItem.codice
            Dim yy = Split(xx, ".")
            cod(1) = yy(0)
            cod(2) = yy(1)
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
    Sub Carica_Combobox0()
        ComboBox0.BeginUpdate()

        ComboBox0.DataSource = Nothing
        ComboBox0.Items.Clear()

        ' Nota: DisplayMember funziona correttamente se l'oggetto ha una PROPRIETÀ pubblica chiamata TESTO
        ComboBox0.DisplayMember = "TESTO"
        ComboBox0.ValueMember = "CODICE" ' Opzionale: imposta il valore reale dietro la stringa
        For J As Integer = 0 To Gbl_Elenco_Codici_ELCAR.Count - 1
            ' FONDAMENTALE: Crea una NUOVA istanza di CODICELCAR a OGNI iterazione del ciclo
            Dim cod_E As New CODICELCAR()

            Dim x As String = "(" + Gbl_Elenco_Codici_ELCAR(J).CODICE + ") " + Gbl_Elenco_Codici_ELCAR(J).TESTO

            cod_E.TESTO = x
            cod_E.CODICE = Gbl_Elenco_Codici_ELCAR(J).CODICE
            cod_E.J = J

            ComboBox0.Items.Add(cod_E)
        Next

        ' Riabilita il ridisegno grafico della ComboBox
        ComboBox0.EndUpdate()

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
        ComboBox5.Items.Clear()
        ComboBox6.Items.Clear()

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
        Call Mostra_info_codice()
    End Sub
    Sub Mostra_info_codice()
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
            DGV1.BackgroundColor = MyColor.LightCyan
            GroupBox4.BackColor = MyColor.LightCyan
            CheckBox3.ForeColor = MyColor.Blue
            'GroupBox4.Text = "Foglio esercizi [ADMITTANCE]"
        Else
            DGV1.BackgroundColor = SystemColors.ControlDark
            GroupBox4.BackColor = MyColor.Transparent
            CheckBox3.ForeColor = MyColor.Black
            'GroupBox4.Text = "Foglio esercizi Standard]"
        End If
        Label11.BackColor = GroupBox4.BackColor
        Label22.BackColor = GroupBox4.BackColor

        DGV1.Refresh()
    End Sub


    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        '-- Carico utimo foglio --
        Dim j = 0, id_foglio = 0

        If Not Gbl_Caricamento_Immediato Then
            id_foglio = F_Foglio_esistente.Main(0, , False)
        Else
            id_foglio = Class_GIORNALIERO.Trova_ultimo_IDFOGLIO
        End If

        If id_foglio = 0 Then
            Label24.Text = ""
            Call MsgBox("Non è stato trovato nessun foglio valido!")
            Exit Sub
        End If

        carica_ultimo_foglio_bambino(id_foglio)






    End Sub
    Sub carica_ultimo_foglio_bambino(id_foglio As Integer, Optional DBbAMBINO As String = "")
        ' Carico utimo foglio

        ' do per scontato che Gbl_foglio.DB_Bambino sia corretta 
        If DBbAMBINO = "" Then
            DBbAMBINO = Gbl_foglio.DB_Bambino
        Else
            Gbl_foglio.DB_Bambino = DBbAMBINO
        End If

        Gbl_foglio = Carica_foglio(id_foglio, DBbAMBINO)

        Label24.Text = "Copiato dal foglio n. " + id_foglio.ToString + " del: " + Gbl_foglio.Data

        Gbl_VociDiUnFoglio = Gbl_foglio.VociDiUnFoglio

        ' Carico la griglia con i dati letti 
        Carica_dati_foglio_in_DGV1()

        '------------------------------------------------------------------------------------------
        '-- AZZERO TUTTE LE VARIABILI EVENTUALMENTE PRESENTI IN MODO CHE IL FOGLIO RISULTI NUOVO --
        '------------------------------------------------------------------------------------------
        '[VOCI] 
        Gbl_foglio.ID_foglio = 0
        For Each voce In Gbl_foglio.VociDiUnFoglio
            voce.ID_FOGLIO = 0 : voce.COMPILATO = 0
            voce.TOT1_POS = 0 : voce.TOT1_NEG = 0 : voce.TOT1_PROMPT = 0 : voce.TOT1 = 0
            voce.TOT2_POS = 0 : voce.TOT2_NEG = 0 : voce.TOT2_PROMPT = 0 : voce.TOT2 = 0
        Next

        '[RT] 
        For Each voce In Gbl_foglio.RT
            voce.ID_FOGLIO = 0 : voce.TOT = 0 : voce.COMPILATO = 0
        Next

        '[CP] 
        For Each voce In Gbl_foglio.CP
            voce.ID_FOGLIO = 0 : voce.TOT = 0 : voce.COMPILATO = 0
        Next


        TP1.SetToolTip(Label22, "Voci importate dal foglio n:" + id_foglio.ToString)
        Label22.BackColor = MyColor.LightYellow
        Label22.Text = "NUOVO FOGLIO"

        'Trova la data, ovvero il prossimo giorno lavorativo 
        Dim nextday = Prossimo_giorno_lavorativo(Now)
        Gbl_foglio.Data = nextday


        mostra_info_sul_foglio()

        Call Imposta_tipo_caricamento_ultimo_foglio(False)
        Dim n = Gbl_VociDiUnFoglio.Count






    End Sub
    Private Sub CheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox4.CheckedChanged
    End Sub


    Private Sub GroupBox5_Enter(sender As Object, e As EventArgs) Handles GroupBox5.Enter

    End Sub

    Private Sub ImmediatoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TSM_Immediato.Click
        'Imposta_tipo_caricamento_ultimo_foglio()


    End Sub

    Private Sub TSM_NONImmediato_Click(sender As Object, e As EventArgs)
        'Imposta_tipo_caricamento_ultimo_foglio()

    End Sub
    Sub Imposta_tipo_caricamento_ultimo_foglio(Optional modo = False)
        If modo Then
            Gbl_Caricamento_Immediato = True
            Button9.BackColor = MyColor.LightGreen
        Else
            Gbl_Caricamento_Immediato = False
            Button9.BackColor = SystemColors.Control

        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim r As Integer = DGV1.CurrentRow.Index
        Dim codice = DGV1(1, r).Value
        If codice = "" Then Exit Sub
        Dim desc = DGV1(2, r).Value
        Dim m = "Esercizio (" + codice + "):" + vbCrLf
        m = m + desc + vbCrLf
        m = m + "Vui rimuovere questo esercizio dal foglio?"
        If MsgBox(m, vbQuestion + vbYesNo, "Rimozione Esercizio") <> vbYes Then Exit Sub
        If Elimina_Codice(codice) Then
            m = "rimozione dell'esercizio (" + codice + "):" + desc + " dal foglio: (" + Gbl_foglio.ID_foglio.ToString + ") di:" + Gbl_foglio.DB_Bambino
            Call APPEND_LOG(m, GbL_Logfile, True)

            Carica_dati_foglio_in_DGV1()

        End If

    End Sub


    Private Sub ComboBox8_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox8.SelectedIndexChanged
        Dim current_index = Val(ComboBox8.Tag)
        Dim new_index = ComboBox8.SelectedIndex
        If current_index = new_index Then Exit Sub


        Call Azzera_foglio(True)
        DGV1.Rows.Clear()

        Gbl_Jb = new_index
        ComboBox8.Tag = new_index   ' Memorizzo l'indice del bambino
        Gbl_foglio.ID_Bambino = Gbl_Elenco_bambini(Gbl_Jb).ID
        Gbl_foglio.DB_Bambino = Gbl_Elenco_bambini(Gbl_Jb).DBname
        Gbl_foglio.Bambino = Gbl_Elenco_bambini(Gbl_Jb).NomeCompleto

        If Gbl_foglio.DB_Bambino = "" Then
            Button9.Enabled = False
        Else
            Button9.Enabled = True
        End If
        '-- Verifico se il bambino ha dei foglio gia inseriti
        If Seleziona_bambino(new_index) > 0 Then
            '-- Sono stati trovati fogli, Abilito il caricamento dell'ultimo --
            Gbl_Caricamento_Immediato = True
            Button9.BackColor = MyColor.LightGreen : Button9.Refresh()
            'Gbl_VociDiUnFoglio.Clear()
            'Gbl_NOME_BAMBINO = Gbl_Elenco_bambini(Gbl_Jb).NomeCompleto
            Exit Sub
        End If

        '-----------------------------------------------------------
        '-- Se sono qui NON sono stati trovati foglio del bambino --
        '-----------------------------------------------------------
        TSM_Immediato.Checked = False : Gbl_Caricamento_Immediato = False
        Call Azzera_foglio(True)
        DGV1.Rows.Clear()


        ' se non ho fogli caricati esco --   
        If DGV1.Rows.Count <= 1 Then
            Label10.Text = "0"
            Gbl_VociDiUnFoglio.Clear()
            Call mostra_info_sul_foglio()
            Exit Sub
        End If

        '-- Verifico se ho un foglio caricato di un altro bambino 
        Dim n = 0
        Try
            n = Gbl_elenco_fogli_bambino.Count
        Catch ex As Exception
        End Try

        If DGV1.Rows.Count <= 1 Then
            Label10.Text = "0"
            Gbl_VociDiUnFoglio.Clear()
            mostra_info_sul_foglio()
        End If

        '--  rimuovo da Gbl_vocidiunfoglio tutte quelle che non ci sono piu
        Dim X = "", j = 0

        For Each riga In DGV1.Rows
            j = riga.Cells(4).Value
            X = X + j.ToString + ";"
        Next
        Dim xx = Split(X, ";"), count = 0
        For i = 0 To Gbl_VociDiUnFoglio.Count - 1
            Dim trovato = False
            For j = 0 To xx.Count - 1
                If i = Val(xx(j)) Then
                    trovato = True
                End If
            Next
            If Not trovato Then
                Gbl_VociDiUnFoglio(i).Codice = ""
                count = count + 1
            End If
        Next
        Gbl_VociDiUnFoglio.RemoveAll(Function(elemento) String.IsNullOrWhiteSpace(elemento.Codice))

        DGV1.ShowCellToolTips = True
        DGV1.Refresh()
        Call mostra_info_sul_foglio()



        '-- Se ho gia un foglio caricato chiedo se voglio cancellarlo --
        'Dim m = Gbl_NOME_BAMBINO + " NON ha nessun foglio preparato " + vbCrLf + " Vuoi utilzzare gli esercizi gia caricati?" + vbCrLf
        Dim m = Gbl_foglio.Bambino + " NON ha nessun foglio preparato " + vbCrLf + " Vuoi utilzzare gli esercizi gia caricati?" + vbCrLf
        m = "sono presenti Esercizi di una altro Bambino" + vbCrLf
        m = m + "Vuoi Cancellarli?"

        If DGV1.Rows.Count > 1 Then
            Dim t = "Selezione Nuovo Bambino"
            m = "sono presenti Esercizi di una altro Bambino" + vbCrLf
            m = m + "Vuoi Cancellarli?"
            If MsgBox(m, vbYesNo + vbInformation, t) = vbYes Then
                DGV1.Rows.Clear()
            Else
                Dim risultato = Elimina_codici_personali_da_DGV1()
                If risultato.Deleted > 0 Then
                    m = "Sono stati eliminati " + risultato.Deleted.ToString + " esercizi personalizzati per il bambino precedente"
                    Call MsgBox(m, vbOKOnly + vbInformation)
                End If
            End If
        End If

        Seleziona_bambino(new_index)
        ComboBox8.Tag = new_index   ' Memorizzo l'indice del bambino

        Label10.Text = Conta_codici_inseriti().NELCAR

        '--  rimuovo da Gbl_vocidiunfoglio tutte quelle che non ci sono piu
        X = ""
        j = 0

        For Each riga In DGV1.Rows
            j = riga.Cells(4).Value
            X = X + j.ToString + ";"
        Next
        xx = Split(X, ";")
        count = 0
        For i = 0 To Gbl_VociDiUnFoglio.Count - 1
            Dim trovato = False
            For j = 0 To xx.Count - 1
                If i = Val(xx(j)) Then
                    trovato = True
                End If
            Next
            If Not trovato Then
                Gbl_VociDiUnFoglio(i).Codice = ""
                count = count + 1
            End If
        Next
        Gbl_VociDiUnFoglio.RemoveAll(Function(elemento) String.IsNullOrWhiteSpace(elemento.Codice))

        DGV1.ShowCellToolTips = True
        DGV1.Refresh()
        Call mostra_info_sul_foglio()


    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            F_MyGraph.Hide()
        Catch ex As Exception

        End Try

        Hide()
    End Sub
    Sub Azzera_foglio(Optional Anche_RT As Boolean = False)
        '-Azzero contenuto griglia
        DGV1.Rows.Clear()

        Try
            Gbl_foglio.VociDiUnFoglio.Clear()
        Catch ex As Exception
        End Try

        Try
            '-Azzero contenuto Variabile Gbl_VociDiUnFoglio
            Gbl_VociDiUnFoglio.Clear()
        Catch ex As Exception
        End Try


        Try
            '-Azzero contenuto Variabile Gbl_VociDiUnFoglio
            Gbl_foglio.CP.Clear()
        Catch ex As Exception
        End Try


        If Anche_RT Then
            Try
                Gbl_foglio.RT.Clear()
            Catch ex As Exception
            End Try
        End If

        Call mostra_info_sul_foglio()
    End Sub

    Function Elimina_codici_personali_da_DGV1() As (Lista As List(Of String), Deleted As Integer)
        Dim listaTabelle As New List(Of String)()
        Dim deleted = 0
        For i As Integer = DGV1.Rows.Count - 1 To 0 Step -1
            Dim riga As DataGridViewRow = DGV1.Rows(i)

            ' Salta la riga vuota dei nuovi inserimenti se presente
            If riga.IsNewRow Then Continue For

            ' Verifica che la cella non sia vuota prima di fare lo Split
            If riga.Cells(1).Value IsNot Nothing Then
                Dim x = Split(riga.Cells(1).Value.ToString(), ".")

                ' Se non soddisfa la condizione (nel tuo esempio, se x.Count = 6)
                If x.Count = 6 Then
                    ' ELIMINA RIGA: Rimuove la riga e compatta la DataGrid
                    listaTabelle.Add(Convert.ToString(x))
                    DGV1.Rows.RemoveAt(i)
                    deleted = deleted + 1
                End If
            End If
        Next
        Return (listaTabelle, deleted)
    End Function
    Private Sub Label19_Click(sender As Object, e As EventArgs) Handles Label19.Click
        Dim giorno = CDate(Gbl_foglio.Data)

        Dim dataScelta = SelezionaDataDaCalendario("Seleziona la data di esecuzione dell'esercizio", giorno)

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
    Function Stampa_tutti_Grafici_ELCAR() As String
        Dim superContenitoreDati As New List(Of List(Of ElementoGrafico))
        Dim riga As DataGridViewRow, cod As String
        Dim record1 As ElementoGrafico
        Dim gf(8) As Struct_Graph_Codice
        Dim i = 0
        For Each riga In DGV1.Rows
            cod = riga.Cells(1).Value
            If cod = "" Then Continue For
            Call Carica_dati_codice_elcar(cod)
            Call F_MyGraph.Carica_dati_ELCAR()

            Dim N = Gbl_datigrafico.Count
            Dim datiSingoloGrafico As New List(Of ElementoGrafico)
            For j = 0 To N - 1
                record1.X = j + 1
                record1.Y1 = Calcola_Percentuale(Gbl_datigrafico(j).ValoriY(0))
                If Gbl_datigrafico(j).ValoriY.Count = 2 Then
                    record1.Y2 = Calcola_Percentuale(Gbl_datigrafico(j).ValoriY(1))
                Else
                    record1.Y2 = 0
                End If
                datiSingoloGrafico.Add(record1)
            Next
            superContenitoreDati.Add(datiSingoloGrafico)
            gf(i).Cod_ELCAR = cod
            gf(i).Cod_Testo = Gbl_VociDiUnFoglio(i).Testo
            i = i + 1
        Next

        Dim m_Data = DateTime.Now.ToString("dd-MM-yyyy")
        Dim m_IDFOGLIO = Gbl_Graph.ID_Foglio.ToString
        'Dim m_bambino = Gbl_NOME_BAMBINO
        Dim m_bambino = Gbl_foglio.Bambino

        Dim mx = ""

        Dim word_graf_modello = dir_bar(ini.Path_GRAFICI_DaStampare) + "Grafico_codici_1.docx"
        Dim nomedoc = m_bambino + "_(" + m_Data + ").DOCX"
        Dim documento = dir_bar(ini.Path_GRAFICI_DaStampare) + nomedoc
        'IO.File.Copy(ini.Modello_Grafico_codici, documento, True)


        AggiornaTuttiIGraficiElcar(documento, superContenitoreDati)


        ' -- riapro il docx e aggiorno tutti i campi testo
        Using doc As WordprocessingDocument =
            WordprocessingDocument.Open(documento, True)
            Dim body As Body = doc.MainDocumentPart.Document.Body
            SET_DOCX(body, "<BAMBINO>", m_bambino)
            SET_DOCX(body, "<FOGLIO>", m_IDFOGLIO)
            SET_DOCX(body, "<DATA>", m_Data)
            For j = 0 To i - 1
                Dim testo = "(" + gf(j).Cod_ELCAR + ") " + gf(j).Cod_Testo
                mx = "<COD" + (j + 1).ToString + ">"
                SET_DOCX(body, mx, testo)
            Next
            doc.MainDocumentPart.Document.Save()

        End Using
        Return documento
    End Function



    Private Sub Label21_Click(sender As Object, e As EventArgs) Handles Label21.Click
        Info.Show()

    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Dim path = ini.Path_SCHEDE_DaStampare
        Open_file_manager(path)
    End Sub


    Private Sub Label23_Click(sender As Object, e As EventArgs) Handles Label23.Click


        Dim m = "Vuoi cancellare tutti gli esercizi inseriti nel foglio?"
        If MsgBox(m, vbQuestion + vbYesNo, "Azzera foglio") <> vbYes Then Exit Sub
        Dim full = False, nrt = 0

        Try
            nrt = Gbl_foglio.RT.Count
        Catch ex As Exception
        End Try
        If nrt > 0 Then
            m = "Vuoi azzerare anche tutte le voci presenti nel foglio della ROUTINE?"
            If MsgBox(m, vbYesNo + vbQuestion + vbDefaultButton2, "Azzeramento Voci della routine") = vbYes Then full = True
        End If

        Call Azzera_foglio(full)
        DGV1.Rows.Clear()



    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Dim sel As New List(Of Class_Giornaliero_CP)
        Dim selcp As New List(Of Class_Giornaliero_CP)
        sel = Gbl_foglio.CP
        selcp = F_Seleziona_CP.Mostra(sel)
        Dim n = 0
        Try
            n = selcp.Count
        Catch ex As Exception
            Call MsgBox("Selezione non effettuata")
            Exit Sub
        End Try
        Gbl_foglio.CP = selcp
        Aggiorna_pulsante_CP(n)
        'Gbl_CP_selezionate = sel
    End Sub

    Sub Aggiorna_pulsante_CP(n)
        Dim testo = "      CP (" + n.ToString + ")"
        Button10.Text = testo
        Dim m = "Nessuna attività comportamentale Inserita"
        If n <= 0 Then
            Button10.Text = "      CP (0)"
            Button10.ImageIndex = 2
            m = "Nessuna attività comportamentale Inserita"
            Button10.BackColor = MyColor.Yellow
            Button10.ForeColor = MyColor.Black
        Else
            Button10.Text = "      CP (" + n.ToString + ")"
            Button10.ImageIndex = 4
            m = "Inserita " + n.ToString + " attività comportamentale"
            'Dim cb = MyColor.DodgerBlue, cf = MyColor.White
            Dim cb = SystemColors.Control, cf = MyColor.Black
            Button10.BackColor = cb : Button10.ForeColor = cf
        End If
        Button10.Refresh()
        TP1.SetToolTip(Button10, m)


    End Sub
    Sub Aggiorna_pulsante_RT(n)

        Dim m = "Inserita " + n.ToString + " attività di routine"
        Dim testo = "      RT (" + n.ToString + ")"
        Button14.Text = testo

        If n <= 0 Then
            Button14.ImageIndex = 2
            m = "Nessuna attività di routine Inserita"
            Button14.BackColor = MyColor.Yellow
            Button14.ForeColor = MyColor.Black
        Else
            Button14.ImageIndex = 4
            'Dim cb = MyColor.DodgerBlue, cf = MyColor.White
            Dim cb = SystemColors.Control, cf = MyColor.Black
            Button14.BackColor = cb : Button14.ForeColor = cf

        End If
        Button14.Refresh()
        TP1.SetToolTip(Button14, m)


    End Sub

    Private Sub Label15_Click(sender As Object, e As EventArgs) Handles Label15.Click

    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click
        Mostra_info_codice()
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs)
        Dim doc = Stampa_tutti_Grafici_ELCAR()
        If MsgBox("Vuoi aprire il foglio appena creato?", vbYesNo, vbQuestion) <> vbYes Then Exit Sub
        Apri_documento(doc)
        MsgBox("Bisogna aprire un menu per decidere cosa stampare")

    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        'verifico se ci sono dati caricati nel foglio
        Dim cnt = 0
        Try
            cnt = Gbl_foglio.RT.Count
        Catch ex As Exception
        End Try
        Dim id_foglio = Val(Label13.Text)
        Dim RT = New List(Of Class_Giornaliero_ROUTINE)
        If cnt = 0 Then
            Gbl_foglio.RT = New List(Of Class_Giornaliero_ROUTINE)
            RT = F_Seleziona_Routine.main(id_foglio, True)
        Else
            RT = F_Seleziona_Routine.main(id_foglio, False)
        End If

        cnt = 0
        Try
            cnt = RT.Count
            Gbl_foglio.RT = RT
        Catch ex As Exception
            cnt = Gbl_foglio.RT.Count

        End Try
        Call Aggiorna_pulsante_RT(cnt)

    End Sub

    Private Sub Button6_Click_1(sender As Object, e As EventArgs) Handles Button6.Click
        Stampa_foglio_giornaliero()

    End Sub

    Private Sub ComboBox0_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox0.SelectedIndexChanged
        Dim x = ComboBox0.SelectedItem.codice
        Dim y = Split(x, ".")
        cod(1) = y(0)
        cod(2) = y(1)
        Gbl_Codice_Elcar_Selezionato = x
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

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Dim nvoci_elcar = Gbl_foglio.VociDiUnFoglio.Count
        Dim nvoci_RT = Gbl_foglio.RT.Count
        Dim modello = Ritorna_modello_da_utilizzare(nvoci_elcar, nvoci_RT)
        Dim m = "Modello Word:" + modello


    End Sub
End Class