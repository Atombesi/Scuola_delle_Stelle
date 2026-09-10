Imports System.IO

Public Class F_Elenco_da_Stampare
    Dim info_foglio As Struct_FogliDaStampare
    Private Sub F_Elenco_da_Stampare_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Sub Main(data As String)
        Dim datascelta As Date = CDate(data)
        Label11.Text = datascelta.ToString("dddd d MMMM yyyy")
        Label11.Tag = data

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
            End If
        Next

        'Aggiungo alla grid'
        Dim trovato = False, f0 = ""

        DGV1.Rows.Clear()
        Dim img = 5
        For i = 0 To cnt - 1
            f = Gbl_Elenco_fogli_stampa(i).filename
            b = Gbl_Elenco_fogli_stampa(i).Bambino
            d = Gbl_Elenco_fogli_stampa(i).Data
            dafare = True
            If Gbl_Elenco_fogli_stampa(i).doc_Esistente Then
                img = 0
                dafare = False
            End If
            f = Path.GetFileName(f).ToUpper
            DGV1.Rows.Add(dafare, ImageList1.Images(img), b, d, f)
        Next


        'Aggiungo alla grid i docx vecchi trovati'
        Dim percorso = ini.Path_SCHEDE_DaCompilare
        Dim fileTrovati As String() = Directory.GetFiles(percorso, "*.docx")

        Dim cnt1 = fileTrovati.Count - 1, dacancellare = 0

        For i = 0 To cnt1  ' ciclo sui file trovati nella cartella 
            trovato = False
            Dim FTrovati = Path.GetFileName(fileTrovati(i)).ToUpper
            For j = 0 To cnt - 1
                f0 = Path.GetFileName(Gbl_Elenco_fogli_stampa(j).filename).ToUpper
                If f0 = FTrovati Then
                    trovato = True
                    d = Strings.Left(FTrovati(i), 10)
                    Exit For
                End If
            Next
            If Not trovato Then
                DGV1.Rows.Add(False, ImageList1.Images(9), "OLD", d, FTrovati)
                dacancellare = dacancellare + 1
            End If
        Next

        If dacancellare Then
            Button3.Enabled = True
        Else
            Button3.Enabled = False
        End If
        Me.ShowDialog()

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
            Gbl_Elenco_fogli_stampa(idx).Compilato = riga("Compilato")
            Gbl_Elenco_fogli_stampa(idx).id_foglio = Convert.ToInt32(riga("id_foglio"))
            'Gbl_Elenco_fogli_stampa(idx).id_foglio = Convert.ToInt64(riga("id_foglio"))
            Gbl_Elenco_fogli_stampa(idx).Titolo = riga("titolo")
            Gbl_Elenco_fogli_stampa(idx).Data = riga("data")

            Dim ID_foglio = Gbl_Elenco_fogli_stampa(idx).id_foglio
            titolo = Gbl_Elenco_fogli_stampa(idx).Titolo
            Dim Data = Strings.Format(CDate(Gbl_Elenco_fogli_stampa(idx).Data), "yyyy-MM-dd")
            Dim nomefoglio = Costruisci_nome_foglio(ID_foglio, titolo, Data)
            Dim documento = dir_bar(ini.Path_SCHEDE_DaCompilare) + nomefoglio
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
        Dim dataScelta As DateTime? = SelezionaDataDaCalendario("Seleziona la data di esecuzione dell'esercizio")

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
        Dim percorsoFileCompleto = ini.Path_SCHEDE_DaCompilare
        percorsoFileCompleto = dir_bar(percorsoFileCompleto)
        aprifilemanager(percorsoFileCompleto)
    End Sub
End Class