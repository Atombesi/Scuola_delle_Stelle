Imports System.IO
Imports System.Windows.Controls
Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Spreadsheet
Imports Microsoft.IdentityModel.Tokens
Imports MyColor = System.Drawing.Color


Public Class F_Elenco_da_Stampare
    Dim info_foglio As Struct_FogliDaStampare
    Dim listaFoglidaStampare As New List(Of String)
    Dim stampante_predefinita = " Nessuna"
    Private Sub F_Elenco_da_Stampare_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DGV1.AllowUserToAddRows = False   ' Impedisce di visualizzare l'ultima riga vuota 

    End Sub
    Sub Main(data As String)
        Dim datascelta As Date = CDate(data)
        Label11.Text = datascelta.ToString("dddd d MMMM yyyy")
        Label11.Tag = data
        stampante_predefinita = ini.Stampante_defaut
        Label20.Text = "       " + Strings.Left(stampante_predefinita, 25) + "..."

        Call Reload()
        Me.ShowDialog()
        Exit Sub



        If IsNothing(Gbl_DB_BAMBINO) Then
            Gbl_DB_BAMBINO = Gbl_Elenco_bambini(0).DBname
        End If
        Dim temp = Gbl_DB_BAMBINO   '
        Dim cnt = 0

        'Ciclo su tutti i bambini attvi -
        ReDim Gbl_Elenco_fogli_stampa(50)
        For Each voce In Gbl_Elenco_bambini

            info_foglio = New Struct_FogliDaStampare
            If Not voce.Attivo Then Continue For
            info_foglio.Bambino = voce.Cognome + " " + voce.Nome
            info_foglio.Bambino = voce.NomeCompleto
            info_foglio.DB_Bambino = voce.DBname
            info_foglio.ID_Bambino = voce.ID
            info_foglio.Data = datascelta
            Gbl_DB_BAMBINO = voce.DBname
            'NB: sono fogli da stampare tutti quelli presenti nella tabella GIORNALIERA e NON COMPILATI --
            Dim DB_BAMBINO = Build_fuldbname_Bambino(Gbl_DB_BAMBINO)
            If Not IO.File.Exists(DB_BAMBINO) Then Continue For
            cnt = Trova_Foglidastampare(cnt)
        Next

        ReDim Preserve Gbl_Elenco_fogli_stampa(cnt)
        'cotrollo nel folder quali files esistono 
        Dim f = "", b = "", d = "", dafare = False
        For i = 0 To cnt - 1
            f = Gbl_Elenco_fogli_stampa(i).filename
            Gbl_Elenco_fogli_stampa(i).DaStampare = True
            If IO.File.Exists(f) Then
                Gbl_Elenco_fogli_stampa(i).DaStampare = False
                Gbl_Elenco_fogli_stampa(i).doc_Esistente = True
            End If
        Next

        Dim dacancellare = Add_to_DGV1()
        Conta_selezionati()

        Me.ShowDialog()
        Gbl_DB_BAMBINO = temp
    End Sub
    Sub Reload()
        'Label11.Text = datascelta.ToString("dddd d MMMM yyyy")
        'Label11.Tag = Data
        If IsNothing(Gbl_DB_BAMBINO) Then
            Gbl_DB_BAMBINO = Gbl_Elenco_bambini(0).DBname
        End If
        Dim temp = Gbl_DB_BAMBINO   '
        Dim cnt = 0

        'Ciclo su tutti i bambini attvi -
        ReDim Gbl_Elenco_fogli_stampa(50)
        For Each voce In Gbl_Elenco_bambini

            info_foglio = New Struct_FogliDaStampare
            If Not voce.Attivo Then Continue For
            info_foglio.Bambino = voce.NomeCompleto

            info_foglio.DB_Bambino = voce.DBname
            info_foglio.ID_Bambino = voce.ID
            info_foglio.Data = Label1.Tag
            Gbl_DB_BAMBINO = voce.DBname

            'NB: sono fogli da stampare tutti quelli presenti nella tabella GIORNALIERA e NON COMPILATI --
            Dim DB_BAMBINO = Build_fuldbname_Bambino(Gbl_DB_BAMBINO)
            If Not IO.File.Exists(DB_BAMBINO) Then Continue For
            cnt = Trova_Foglidastampare(cnt)   'solo fogli NON compilati
        Next

        ReDim Preserve Gbl_Elenco_fogli_stampa(cnt)
        'cotrollo nel folder quali files esistono 
        Dim f = "", b = "", d = "", dafare = False
        For i = 0 To cnt - 1
            f = Gbl_Elenco_fogli_stampa(i).filename
            Gbl_Elenco_fogli_stampa(i).DaStampare = True
            If IO.File.Exists(f) Then
                Gbl_Elenco_fogli_stampa(i).DaStampare = False
                Gbl_Elenco_fogli_stampa(i).doc_Esistente = True
            End If
        Next

        Dim dacancellare = Add_to_DGV1()
        Conta_selezionati()
        Call Selezione_Automatica(CheckBox1.Checked)

        Gbl_DB_BAMBINO = temp
    End Sub
    Function Add_to_DGV1() As Integer
        ' Aggiungo alla DGV!  tutti i fogli da stampare trovati e li marco opportunamente --
        ' a) Red Ball    (9) _ file docx è presente  Data trascorsa, foglio non trovato o è gia compilato
        ' c) green Ball  (2) : Il foglio è da stampare (data corretta) ma esiste gia

        ' b) white ball  (5) : Il foglio  è da stampare (data corretta )e file NON presente 
        ' d) Yellow ball (3) : Foglio da compilare con data precedente ad oggi (Presente o non presente)
        'Public Registrato As Boolean    'True iL FOGLIO è PRESENTE NEL DATABASE DEL BAMBINO
        'Public doc_Esistente As Boolean 'True esiste gia il file docx
        'Public Compilato As Boolean     'True il foglio risulta compilato
        'Public DaStampare As Boolean    'True bisogna preparare il file docx
        'Public Color As Integer         ' colore di visualizzazione


        Dim trovato = False, f0 = ""
        Dim f = "", b = "", d = "", dafare = False, dd As Date

        DGV1.Rows.Clear()
        Dim img = 5
        Dim cnt = Gbl_Elenco_fogli_stampa.Count - 1

        ' -----------------------------------------------------------------
        ' Ciclo su tutti i fogli (trovati nei database) non compilati --
        ' -----------------------------------------------------------------
        For i = 0 To cnt - 1
            f = Gbl_Elenco_fogli_stampa(i).filename
            b = Gbl_Elenco_fogli_stampa(i).Bambino
            d = Gbl_Elenco_fogli_stampa(i).Data
            dd = CDate(d)


            img = 2 ' verdi data del foglio odierna o successiva
            dafare = True
            f = Path.GetFileName(f).ToUpper


            If Gbl_Elenco_fogli_stampa(i).doc_Esistente Then
                'FOGLIO DOCX PRESENTE Verde o rossa
                If dd.Date < DateTime.Today Then img = 14 Else img = 2  ' Verde
                dafare = False
            Else
                'FOGLIO DOCX NON PRESENTE BIANCA O GIALLA
                If dd.Date < DateTime.Today Then img = 3 Else img = 5  ' Bianca
                dafare = True
            End If
            DGV1.Rows.Add(dafare, ImageList1.Images(img), b, d, f, i, img)
        Next

        '--------------------------------------------------------------
        '-- file docx presente ma foglio non trovato o gia compilato --
        '--------------------------------------------------------------
        Dim percorso = ini.Path_SCHEDE_DaStampare
        Dim fileTrovati As String() = Directory.GetFiles(percorso, "*.docx")

        Dim cnt1 = fileTrovati.Count - 1, dacancellare = 0, j0 = 0

        For i = 0 To cnt1  ' ciclo sui file trovati nella cartella 
            trovato = False
            Dim FTrovati = Path.GetFileName(fileTrovati(i)).ToUpper
            For j = 0 To cnt - 1 ' ciclo su tutti i fogli letti nel database 
                f0 = Path.GetFileName(Gbl_Elenco_fogli_stampa(j).filename).ToUpper
                If f0 = FTrovati Then
                    'TROVATO IL Foglio (NON COMPILATO) del File docx 
                    trovato = True
                    Gbl_Elenco_fogli_stampa(j).doc_Esistente = True
                    Gbl_Elenco_fogli_stampa(j).Compilato = False
                    Exit For
                End If
            Next

            If Not trovato Then
                If Strings.Left(FTrovati, 2) = "~$" Then Continue For
                d = Strings.Left(FTrovati, 10)
                Dim n = DGV1.Rows.Add(False, ImageList1.Images(9), "OLD già compilato", d, FTrovati, -1, 9) ' ROSSO
                Dim x = DGV1.Rows(n).Cells(2).Value
                DGV1.Rows(n).ReadOnly = True
                DGV1.Rows(n).DefaultCellStyle.BackColor = MyColor.DarkGray
                DGV1.Rows(n).DefaultCellStyle.ForeColor = MyColor.Snow
                dacancellare = dacancellare + 1
            Else
            End If
        Next

        Button3.Tag = dacancellare
        If dacancellare > 0 Then
            Button3.Enabled = True
        Else
            Button3.Enabled = False
        End If

        Return dacancellare

    End Function
    Sub mostra()

    End Sub
    Function Trova_Foglidastampare(idx) As Integer

        'Per ogni bimbo vedo quali fogli (non compilati) sono presenti ordinati per data in modo decrescente

        Dim dt As DataTable = Class_GIORNALIERO.OttieniRecordOrderbydata(True)
        Dim n = dt.Rows.Count
        If n = 0 Then Return idx
        Dim titolo = ""
        For Each riga As DataRow In dt.Rows
            ' 1. Lettura del campo codice
            Gbl_Elenco_fogli_stampa(idx).Bambino = info_foglio.Bambino
            Gbl_Elenco_fogli_stampa(idx).Data = info_foglio.Data
            Gbl_Elenco_fogli_stampa(idx).ID_Bambino = info_foglio.ID_Bambino
            Gbl_Elenco_fogli_stampa(idx).DB_Bambino = info_foglio.DB_Bambino
            Gbl_Elenco_fogli_stampa(idx).Compilato = riga("Compilato")
            Gbl_Elenco_fogli_stampa(idx).id_foglio = Convert.ToInt32(riga("id_foglio"))
            'Gbl_Elenco_fogli_stampa(idx).id_foglio = Convert.ToInt64(riga("id_foglio"))
            Gbl_Elenco_fogli_stampa(idx).Data = riga("data")

            Dim ID_foglio = Gbl_Elenco_fogli_stampa(idx).id_foglio
            Dim bambino = Gbl_Elenco_fogli_stampa(idx).Bambino
            Dim Data = Strings.Format(CDate(Gbl_Elenco_fogli_stampa(idx).Data), "yyyy-MM-dd")
            Dim nomefoglio = Costruisci_nome_foglio(ID_foglio, Data, bambino)
            Dim documento = dir_bar(ini.Path_SCHEDE_DaStampare) + nomefoglio
            Gbl_Elenco_fogli_stampa(idx).filename = documento
            Gbl_Elenco_fogli_stampa(idx).DaStampare = False
            Gbl_Elenco_fogli_stampa(idx).doc_Esistente = False
            idx = idx + 1
        Next

        Return idx




    End Function
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()
    End Sub


    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim percorsoFileCompleto = ini.Path_SCHEDE_DaStampare
        percorsoFileCompleto = dir_bar(percorsoFileCompleto)
        aprifilemanager(percorsoFileCompleto)
    End Sub

    Private Sub DGV1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV1.CellContentClick
        Dim r = DGV1.CurrentRow.Index
        Dim j = DGV1.Rows(r).Cells(5).Value

        If DGV1.Rows(r).Cells(6).Value = 9 Then
            'File dox presente ma foglio gia compilato
            GroupBox1.Enabled = False
            Label3.Text = "- -"
            Label5.Text = "- -"
            Label7.Text = "- -"
            Label10.Text = "- -"
            Label13.Text = "- -"
            Label8.Text = "FOGLIO GIA COMPILATO" : Label8.BackColor = MyColor.Orange
            Label8.ImageIndex = 16
            Label8.Refresh()
            Button5.Visible = False
            Exit Sub
        Else
            GroupBox1.Enabled = True
        End If

        Button5.Visible = False

        '-- visualizzo le informazione del foglio --
        Dim idf = Gbl_Elenco_fogli_stampa(j).id_foglio
        Dim DB_BAMBINO = Gbl_Elenco_fogli_stampa(j).DB_Bambino
        Gbl_foglio = Carica_foglio(idf, DB_BAMBINO)

        Label3.Text = Gbl_Elenco_fogli_stampa(j).Bambino
        Label7.Text = Path.GetFileName(Gbl_Elenco_fogli_stampa(j).filename).ToUpper
        Label5.Text = Gbl_foglio.Data
        Label13.Text = Gbl_foglio.VociDiUnFoglio.Count.ToString

        Dim img_col = Val(DGV1.Rows(r).Cells(6).Value)
        Dim Data_OK As Boolean = False


        If IO.File.Exists(Gbl_Elenco_fogli_stampa(j).filename) Then
            If img_col = 3 Then ' Gialla --> Data vecchia
                Label8.Text = "     DOCX VECCHIO (rimuovere)" : Label8.BackColor = MyColor.Yellow
                TP1.SetToolTip(Label8, "FOGLIO VECCHIO (ancora non cancellato)")
            ElseIf img_col = 2 Then ' Verde --> Data OK
                Label8.Text = "     DOCX PRONTO" : Label8.BackColor = MyColor.LightGreen
                TP1.SetToolTip(Label8, "FOGLIO VALIDO E PRONTO")
            ElseIf img_col = 14 Then ' rombo --> Foglio NON compilato ma vecchio
                Label8.Text = "     NON PREPARARE (vecchio)" : Label8.BackColor = MyColor.Salmon
                TP1.SetToolTip(Label8, "NON PREPARARE! (Foglio vecchio NON ancora compilato)")
            End If
            Button5.Visible = True
            Button5.Tag = Gbl_Elenco_fogli_stampa(j).filename
        Else
            If img_col = 3 Then ' Gialla --> Data vecchia
                Label8.Text = "     NON PREPARARE (vecchio)" : Label8.BackColor = MyColor.Yellow
                TP1.SetToolTip(Label8, "Foglio vecchio. NON preparare")
            ElseIf img_col = 5 Then ' Bianca--> Data OK
                Label8.Text = "     DOCX da PREPARARE" : Label8.BackColor = MyColor.DeepSkyBlue
                TP1.SetToolTip(Label8, "FOGLIO DA PREPARARE! (Foglio nuovo)")
            ElseIf img_col = 14 Then ' rombo --> Foglio NON compilato ma vecchio
                Label8.Text = "     NON PREPARARE (vecchio)" : Label8.BackColor = MyColor.Salmon
                TP1.SetToolTip(Label8, "NON PREPARARE! (Foglio vecchio NON ancora compilato)")
            End If
            Button5.Tag = ""
            Button5.Visible = False
        End If
        Label8.ImageIndex = img_col
        Label8.Refresh()
        Conta_selezionati()

    End Sub
    Function Conta_selezionati() As Integer
        Dim cnt_selezionate = 0, cnt_dastampare = 0, cnt_dapreparare = 0, cnt_tot = 0

        For Each r In DGV1.Rows
            If IsNothing(r.cells(2).value) Then Continue For
            If r.cells(6).value = 9 Then Continue For   ' Rosse non si contano
            cnt_tot = cnt_tot + 1
            If r.cells(0).value = True Then
                cnt_selezionate = cnt_selezionate + 1
                If r.cells(6).value = 2 Then ' pallino verde -> gia stampabile
                    cnt_dastampare = cnt_dastampare + 1
                ElseIf r.cells(6).value = 5 Then ' pallino Bianco o giallo, da preparare 
                    cnt_dapreparare = cnt_dapreparare + 1
                End If

            End If
        Next

        Label15.Text = cnt_selezionate.ToString + "/" + cnt_tot.ToString
        Label17.Text = cnt_dapreparare.ToString + "/" + cnt_selezionate.ToString
        Label19.Text = cnt_dastampare.ToString + "/" + cnt_selezionate.ToString
        Label15.Tag = cnt_selezionate
        Label17.Tag = cnt_dapreparare
        Label19.Tag = cnt_dastampare
        Button2.Tag = cnt_dapreparare  ' button preparazione foglio docx
        Button6.Tag = cnt_dastampare   ' button stampa foglio docx

        Button2.Text = "Prepara (" + cnt_selezionate.ToString + ")"
        If cnt_selezionate > 0 Then
            Button2.Enabled = True
        Else
            Button2.Enabled = False
        End If


        Button6.Text = "Stampa (" + cnt_dastampare.ToString + ")"
        If cnt_dastampare > 0 Then
            Button6.Enabled = True
        Else
            Button6.Enabled = False
        End If



        Return cnt_tot

    End Function
    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim cnt = 0
        Dim NN = Label15.Tag - Label17.Tag

        'Dim preparatutto As Boolean = False
        'If NN > 0 Then
        '    Dim m = "Sono presenti " + NN.ToString + ") fogli gia pronti?" + vbCrLf + vbCrLf
        '    m = m + "Vuoi Rifare anche quelli?"
        '    If MsgBox(m, vbInformation + vbYesNo + vbDefaultButton2, "Preparazione docx per stampa") = vbYes Then preparatutto = True
        'End If

        Dim n = DGV1.Rows.Count - 1
        For jr = 0 To n
            If Not DGV1.Rows(jr).Cells(0).Value Then Continue For  ' Non selezionata 
            'If Not DGV1.Rows(jr).Cells(6).Value = 2 Then           ' Verde, gia pronto
            '    If Not preparatutto Then Continue For
            'End If
            Dim j = DGV1.Rows(jr).Cells(5).Value
            Dim ID_foglio = Gbl_Elenco_fogli_stampa(j).id_foglio
            Gbl_DB_BAMBINO = Gbl_Elenco_fogli_stampa(j).DB_Bambino
            Gbl_foglio = Carica_foglio(ID_foglio, Gbl_DB_BAMBINO)

            Dim Data = Strings.Format(CDate(Gbl_Elenco_fogli_stampa(j).Data), "yyyy-MM-dd")
            Dim bambino = Gbl_Elenco_fogli_stampa(j).Bambino
            Dim nomefoglio = Costruisci_nome_foglio(ID_foglio, Data, bambino)
            Dim documento = dir_bar(ini.Path_SCHEDE_DaStampare) + nomefoglio
            Stampa_foglio_giornaliero()
            cnt = cnt + 1
        Next

    End Sub



    Private Sub DGV1_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles DGV1.CurrentCellDirtyStateChanged
        If DGV1.IsCurrentCellDirty AndAlso TypeOf DGV1.CurrentCell Is DataGridViewCheckBoxCell Then
            ' ...forza il salvataggio immediato del valore in memoria!
            DGV1.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim m = "sono stati trovati " + Button3.Tag.ToString + " fogli vecchi gia compilati" + vbCrLf
        m = m + "Voi cancellarli ?"
        If MsgBox(m, vbYesNo + vbQuestion, "Eliminazione Vecchi file") <> vbYes Then Exit Sub
        Dim f = ""
        Dim p = dir_bar(ini.Path_SCHEDE_DaStampare)
        Dim ERR = 0
        For j = 0 To DGV1.Rows.Count - 1
            If DGV1.Rows(j).Cells(6).Value <> 9 Then Continue For
            f = p + DGV1.Rows(j).Cells(4).Value
            If IO.File.Exists(f) Then
                Try
                    IO.File.Delete(f)
                Catch ex As Exception
                    ERR = ERR + 1
                End Try
                f = ""
            End If
        Next

        If ERR = 0 Then
            m = "ELIMINNAZIONE EFFETTUATA CORRETTAMENTE"
        Else
            m = "Problemi con l'eliminazione di " + ERR.ToString + " files DOCX"
            APPEND_LOG(m, ini.File_Log, True, "E")
        End If
        Call MsgBox(m, vbInformation + vbOKOnly)
        Call Reload()


    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If Button5.Tag <> "" Then
            Apri_documento(Button5.Tag)
        End If

    End Sub

    Private Sub Button1_DragEnter(sender As Object, e As DragEventArgs) Handles Button1.DragEnter

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Call Reload()

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Dim giorno = CDate(Label11.Tag)
        Dim datascelta As DateTime? = SelezionaDataDaCalendario("Seleziona la data di esecuzione dell'esercizio", giorno)

        ' Verifica se l'utente ha effettivamente scelto una data o se ha annullato
        If Not datascelta.HasValue Then
            ' Gestisci il caso in cui l'utente ha chiuso il calendario senza scegliere
            MessageBox.Show("Nessuna data selezionata.", "Avviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        If datascelta.Value < DateTime.Today Then
            Dim m = "Hai selezionato una Data passata!" + vbCrLf + vbCrLf + "Confermi la selzione?"
            If MsgBox(m, vbQuestion + vbYesNo + vbDefaultButton2) <> vbYes Then Exit Sub
        End If
        Label11.Tag = datascelta.Value.ToShortDateString
        Label11.Text = "(" + datascelta.Value.ToString("dddd d MMMM yyyy") + ")"

        Call Reload()

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        'costruisco elenco file da tastmpare 
        listaFoglidaStampare.Clear()

        For j = 0 To DGV1.Rows.Count - 1
            If DGV1.Rows(j).Cells(0).Value = False Then Continue For
            If DGV1.Rows(j).Cells(6).Value <> 2 Then Continue For   ' solo pallino verde
            Dim i = DGV1.Rows(j).Cells(5).Value
            Dim f = Gbl_Elenco_fogli_stampa(i).filename
            If IO.File.Exists(f) Then listaFoglidaStampare.Add(f)
        Next


        If ini.Stampante_defaut = "Nessuna" Then
            Dim x = Imposta_stampante()
            stampante_predefinita = x
            Label20.Tag = x
            x = "        " + Strings.Left(x, 25) + "..."
            Label20.Text = x
        End If
        StampaListaSuStampanteScelta(listaFoglidaStampare, stampante_predefinita)
    End Sub
    Sub Selezione_Automatica(Optional MODO As Boolean = False, Optional azzera As Boolean = True)
        'se true eseguo la selezione automatica, altrimenti elimino tutte le selezioni

        If MODO = False And azzera Then
            For j = 0 To DGV1.Rows.Count - 1
                DGV1.Rows(j).Cells(0).Value = False
            Next
            Exit Sub
        End If

        ' Ho chiesto la selezione automatica 
        For j = 0 To DGV1.Rows.Count - 1
            If DGV1.Rows(j).Cells(6).Value = 5 Or DGV1.Rows(j).Cells(6).Value = 2 Then 'Seleziono in automatico i bianchi e i verdi
                DGV1.Rows(j).Cells(0).Value = True
            Else
                DGV1.Rows(j).Cells(0).Value = False
            End If
        Next


    End Sub
    Private Sub Button9_Click(sender As Object, e As EventArgs)
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        Dim azzera = False
        If CheckBox1.Checked Then
            azzera = False
        Else
            If Val(Label15.Tag) > 0 Then
                If MsgBox("Vuoi azzerare le selezioni correnti?", vbInformation + vbYesNo, vbDefaultButton1) = vbYes Then
                    azzera = True
                End If
            End If
        End If
        Call Selezione_Automatica(CheckBox1.Checked, azzera)
        Conta_selezionati()

    End Sub

    Function Imposta_stampante() As String
        ' 1. Impostiamo la stampante predefinita attuale come prima scelta del dialogo per comodità
        Dim impostazioniAvanzate As New System.Drawing.Printing.PrinterSettings()
        PrintDialog1.PrinterSettings = impostazioniAvanzate

        ' 2. Mostriamo la finestra nativa a schermo e attendiamo la scelta dell'utente
        If PrintDialog1.ShowDialog() = DialogResult.OK Then

            ' 3. Catturiamo il nome esatto della stampante che l'utente ha selezionato
            Dim stampanteSelezionata As String = PrintDialog1.PrinterSettings.PrinterName
            Return stampanteSelezionata
        End If
        Return ""


    End Function

    Private Sub Label20_Click(sender As Object, e As EventArgs) Handles Label20.Click
        Dim x = Imposta_stampante()
        stampante_predefinita = x
        Label20.Tag = x
        x = "        " + Strings.Left(x, 25) + "..."

        Label20.Text = x
    End Sub
End Class