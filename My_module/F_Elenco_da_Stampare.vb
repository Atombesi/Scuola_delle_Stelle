Imports System.IO
Imports System.Windows.Controls
Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Spreadsheet
Imports Microsoft.IdentityModel.Tokens
Imports MyColor = System.Drawing.Color


Public Class F_Elenco_da_Stampare
    Dim info_foglio As Struct_FogliDaStampare
    Private Sub F_Elenco_da_Stampare_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Sub Main(data As String)
        Dim datascelta As Date = CDate(data)
        Label11.Text = datascelta.ToString("dddd d MMMM yyyy")
        Label11.Tag = data
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
    Function Add_to_DGV1() As Integer
        ' Aggiungo alla DGV!  tutti i fogli da stampare trovati e li marco opportunamente --
        ' Rossi   = file docx presente ma foglio non trovato o gia compilato

        Dim trovato = False, f0 = ""
        Dim f = "", b = "", d = "", dafare = False, dd As Date

        DGV1.Rows.Clear()
        Dim img = 5
        Dim cnt = Gbl_Elenco_fogli_stampa.Count - 1

        ' Ciclo su tutti i fogli da stampare trovati ma non compilati --
        For i = 0 To cnt - 1
            f = Gbl_Elenco_fogli_stampa(i).filename
            b = Gbl_Elenco_fogli_stampa(i).Bambino
            d = Gbl_Elenco_fogli_stampa(i).Data
            dd = CDate(d)
            img = 2 ' verdi data del foglio odierna o successiva
            dafare = True
            If Gbl_Elenco_fogli_stampa(i).doc_Esistente Then
                ' Il foglio è da stampare ma esiste gia
                img = 2  ' Verde
                dafare = False
            Else
                ' Il foglio è da stampare e NON ESISTE
                img = 5  ' bianco
                dafare = True
            End If
            If dd.Date < DateTime.Today Then img = 3  ' Gialla perche data precedente ad oggi 

            f = Path.GetFileName(f).ToUpper
            DGV1.Rows.Add(dafare, ImageList1.Images(img), b, d, f, i)
        Next

        '--------------------------------------------------------------
        '-- file docx presente ma foglio non trovato o gia compilato --
        '--------------------------------------------------------------
        Dim percorso = ini.Path_SCHEDE_DaStampare
        Dim fileTrovati As String() = Directory.GetFiles(percorso, "*.docx")

        Dim cnt1 = fileTrovati.Count - 1, dacancellare = 0

        For i = 0 To cnt1  ' ciclo sui file trovati nella cartella 
            trovato = False
            Dim FTrovati = Path.GetFileName(fileTrovati(i)).ToUpper
            For j = 0 To cnt - 1
                f0 = Path.GetFileName(Gbl_Elenco_fogli_stampa(j).filename).ToUpper
                If f0 = FTrovati Then
                    trovato = True
                    Gbl_Elenco_fogli_stampa(j).doc_Esistente = True
                    Exit For
                End If
            Next

            If Not trovato Then
                If Strings.Left(FTrovati, 2) = "~$" Then Continue For
                d = Strings.Left(FTrovati, 10)
                Dim n = DGV1.Rows.Add(False, ImageList1.Images(9), "OLD", d, FTrovati, -1) ' ROSSO
                Dim x = DGV1.Rows(n).Cells(2).Value
                DGV1.Rows(n).ReadOnly = True
                DGV1.Rows(n).DefaultCellStyle.BackColor = MyColor.DarkGray
                DGV1.Rows(n).DefaultCellStyle.ForeColor = MyColor.Snow
                dacancellare = dacancellare + 1
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

    Private Sub Label19_Click(sender As Object, e As EventArgs) Handles Label19.Click
        Dim giorno = CDate(Gbl_foglio.Data)
        Dim dataScelta As DateTime? = SelezionaDataDaCalendario("Seleziona la data di esecuzione dell'esercizio", giorno)

        ' Verifica se l'utente ha effettivamente scelto una data o se ha annullato
        If dataScelta.HasValue Then
            ' Mostra la data nella label usando il formato esteso che abbiamo visto prima
            Label11.Tag = dataScelta.Value.ToShortDateString
            Label11.Text = "(" + dataScelta.Value.ToString("dddd d MMMM yyyy") + ")"
        Else
            ' Gestisci il caso in cui l'utente ha chiuso il calendario senza scegliere
            MessageBox.Show("Nessuna data selezionata.", "Avviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim percorsoFileCompleto = ini.Path_SCHEDE_DaStampare
        percorsoFileCompleto = dir_bar(percorsoFileCompleto)
        aprifilemanager(percorsoFileCompleto)
    End Sub

    Private Sub DGV1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV1.CellContentClick
        Dim r = DGV1.CurrentRow.Index
        Dim j = DGV1.Rows(r).Cells(5).Value

        If DGV1.Rows(r).Cells(2).Value = "OLD" Then
            GroupBox1.Enabled = False
            Label3.Text = "- -"
            Label5.Text = "- -"
            Label7.Text = "- -"
            Label10.Text = "- -"
            Label13.Text = "- -"
            Label8.Text = "OLD FILE" : Label8.BackColor = MyColor.Orange
            Button5.Visible = False
        Else
            GroupBox1.Enabled = True
        End If

        Button5.Visible = False
        If j >= 0 Then
            Dim idf = Gbl_Elenco_fogli_stampa(j).id_foglio
            Dim DB_BAMBINO = Gbl_Elenco_fogli_stampa(j).DB_Bambino
            Gbl_foglio = Carica_foglio(idf, DB_BAMBINO)

            Label3.Text = Gbl_Elenco_fogli_stampa(j).Bambino
            Label7.Text = Path.GetFileName(Gbl_Elenco_fogli_stampa(j).filename).ToUpper
            Label5.Text = Gbl_foglio.Data
            Label13.Text = Gbl_foglio.VociDiUnFoglio.Count.ToString

            'Label7.Text = Path.GetFileName(Gbl_Elenco_fogli_stampa(j).filename).ToUpper
            'Label10.Text = Gbl_Elenco_fogli_stampa(j).Titolo
            'Label5.Text = Gbl_Elenco_fogli_stampa(j).Data

            If IO.File.Exists(Gbl_Elenco_fogli_stampa(j).filename) Then
                Label8.Text = "PREPARATO" : Label8.BackColor = MyColor.LawnGreen
                Button5.Visible = True
                Button5.Tag = Gbl_Elenco_fogli_stampa(j).filename
            Else
                Label8.Text = "DA PREPARARE" : Label8.BackColor = MyColor.Yellow
                Button5.Tag = ""
            End If

            Label8.Refresh()
        Else
            Label3.Text = "- -"
            Label5.Text = "- -"
            Label7.Text = "- -"
            Label10.Text = "- -"
            'CheckBox1.Checked = False
        End If


        Conta_selezionati()
    End Sub
    Function Conta_selezionati() As Integer
        Dim cnt = 0
        For j = 0 To DGV1.Rows.Count - 1
            If CBool(DGV1.Rows(j).Cells(0).Value) Then
                cnt = cnt + 1
            End If
        Next
        Label15.Text = cnt.ToString
        Label15.Refresh()

        If cnt > 0 Then
            Button2.Enabled = True
        Else
            Button2.Enabled = False
        End If

        Return cnt

    End Function
    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim n = DGV1.Rows.Count - 1
        For jr = 0 To n
            If DGV1.Rows(jr).Cells(0).Value Then
                Dim j = DGV1.Rows(jr).Cells(5).Value
                Dim ID_foglio = Gbl_Elenco_fogli_stampa(j).id_foglio
                Gbl_DB_BAMBINO = Gbl_Elenco_fogli_stampa(j).DB_Bambino
                Gbl_foglio = Carica_foglio(ID_foglio, Gbl_DB_BAMBINO)

                Dim Data = Strings.Format(CDate(Gbl_Elenco_fogli_stampa(j).Data), "yyyy-MM-dd")
                Dim bambino = Gbl_Elenco_fogli_stampa(j).Bambino
                Dim nomefoglio = Costruisci_nome_foglio(ID_foglio, Data, bambino)
                Dim documento = dir_bar(ini.Path_SCHEDE_DaStampare) + nomefoglio
                Stampa_Foglio_Giornaliero()

            End If
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
            If DGV1.Rows(j).Cells(2).Value <> "OLD" Then Continue For
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
            m = "Problemi con l'eliminazione di " + ERR.ToString + " files"
        End If
        Call MsgBox(m, vbInformation + vbOKOnly)



    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If Button5.Tag <> "" Then
            Apri_documento(Button5.Tag)
        End If

    End Sub

    Private Sub Button1_DragEnter(sender As Object, e As DragEventArgs) Handles Button1.DragEnter

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim percorso = ini.Path_SCHEDE_DaStampare
        Dim filename = "Poriva.json"

        ' 3. Unisci tutto in un'unica variabile percorso completo
        Dim percorsoCompleto As String = Path.Combine(percorso, filename)
        JSON_SalvaStruttura(Gbl_foglio, percorsoCompleto)

    End Sub
End Class