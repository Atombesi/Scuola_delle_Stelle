Imports System.DirectoryServices.ActiveDirectory
Imports System.Globalization
Imports System.Linq
Imports System.Runtime.Intrinsics.X86 ' <-- Fondamentale per accedere a DateAxis, Scaling, ecc.
Imports System.Windows.Controls
Imports DocumentFormat.OpenXml
Imports DocumentFormat.OpenXml.Drawing.Charts
Imports DocumentFormat.OpenXml.Drawing.Spreadsheet
Imports DocumentFormat.OpenXml.EMMA
Imports DocumentFormat.OpenXml.Office2010.CustomUI
Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Spreadsheet
Imports DocumentFormat.OpenXml.Wordprocessing
Imports ScottPlot.Statistics



Public Class F_Grafici
    Public Totale_voci As New List(Of Dati_Totali_Giornaliero)
    Public Fogli_Bambino As List(Of Class_GIORNALIERO)
    Private ListaDati As New List(Of DatiGrafico)()
    Private Nome_bambino As String = ""
    Private DB_bambino As String = ""
    Private Listachecked As New List(Of Elenco_checklist)()
    Public documento_docx As String
    Public documento_xlsx As String


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'voci ELCAR disonibili da graficare 
        Dim dbBambino = GroupBox1.Tag

        Listachecked = Class_GIORNALIERO.Voci_Utilizzate(dbBambino)
        F_checklist.main(Listachecked, 8)

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim dbBambino = GroupBox1.Tag
        Dim doc = CaricaDatiGraficoGiornaliero_Excel(dbBambino)
        If CheckBox1.Checked And doc <> "" Then
            'Call AggiornaGrafico_TotaliGiornalieri(documento, Gbl_elementoGrafico)
            Apri_documento(doc)
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Hide()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim idf = Val(Button1.Tag)
        If idf <= 0 Then Exit Sub
        Dim doc = grafico_codici_elcar(idf)
        If CheckBox1.Checked And doc <> "" Then
            'Call AggiornaGrafico_TotaliGiornalieri(documento, Gbl_elementoGrafico)
            Apri_documento(doc)
        End If
        'doc = Stampa_Grafici_Foglio(idf)

    End Sub

    Private Sub F_Grafici_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Carica_elenco_bambini()

    End Sub

    Sub Carica_elenco_bambini()

        '--Azzero combobox
        Gbl_miaLista_fogli.Clear()
        ComboBox1.PopolaLeggero(Gbl_miaLista_fogli)


        For I = 0 To Gbl_Elenco_bambini.Count - 1
            If Not Gbl_Elenco_bambini(I).Attivo Then Continue For
            Dim x = Gbl_Elenco_bambini(I).Cognome + " " + Gbl_Elenco_bambini(I).Nome
            Dim elemento As New Class_ElementoCombo(I, x, Gbl_Elenco_bambini(I).DBname)
            Gbl_miaLista_fogli.Add(elemento)
        Next
        ComboBox1.PopolaLeggero(Gbl_miaLista_fogli)

        Try
            ComboBox1.SelectedIndex = 0
        Catch ex As Exception

        End Try
        ComboBox1.Enabled = True
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim elemento1 As Class_ElementoCombo
        elemento1 = CType(ComboBox1.SelectedItem, Class_ElementoCombo)
        If IsNothing(elemento1) Then
            Exit Sub
        End If
        GroupBox1.Tag = elemento1.Tag
        Gbl_DB_BAMBINO = Costruisci_filenameDB_bambino(GroupBox1.Tag)
        DB_bambino = elemento1.Tag
        Nome_bambino = elemento1.Testo
        Fogli_Bambino = Class_GIORNALIERO.Elenco_fogli_compilati
        Dim n = Fogli_Bambino.Count
        Label3.Text = n.ToString
        'Trovo id_foglio ultimo foglio inserito
        Dim idf = Class_GIORNALIERO.Trova_ultimo_IDFOGLIO
        Button1.Tag = idf
    End Sub
    Function Carico_totali_giornalieri(dbbambino As String) As List(Of Dati_Totali_Giornaliero)
        Dim Totale_voci As New List(Of Dati_Totali_Giornaliero)

        'ritorna tutti i totali delle voci ELCAR
        Totale_voci = Class_GIORNALIERO.totali_giornalieri(dbbambino)

        'ritorna tutti il numero delle voci RT
        Dim totale_Rt = Class_Giornaliero_ROUTINE.totali_RT_giornalieri

        ' unisco i due risultati nel totale_voce
        'NB di RT utilizzo solo il numero di eventi flaggati (tot=1)
        Dim idf = 0
        Dim s1 As New DatiGrafico
        Dim COL0 = 0
        ListaDati.Clear()

        For j = 0 To Totale_voci.Count - 1
            idf = Totale_voci(j).id_foglio
            Dim voce = Totale_voci(j)
            For Each vocert In totale_Rt
                If idf = vocert.id_foglio Then
                    voce.tot_RT = vocert.tot_RT
                    voce.tot = vocert.tot_RT + voce.tot_voci
                    s1.Data = Date.ParseExact(Totale_voci(j).Data, "dd/MM/yyyy", Nothing)
                    s1.ValoriY = New List(Of Integer)({voce.tot, voce.tot_POS_voci})
                    s1.Color = New List(Of Integer)({COL0, COL0})
                    ListaDati.Add(s1)
                    Totale_voci(j) = voce
                    Exit For
                End If
            Next
        Next
        Gbl_datigrafico = ListaDati
        Return Totale_voci


    End Function

    Private Sub ForzaScritturaTestoDiretto(sheetData As SheetData, indiceRiga As UInteger, indirizzoCella As String, valoreTesto As String)
        ' 1. Cerca la riga (es. riga 1)
        Dim row As Row = sheetData.Elements(Of Row)().FirstOrDefault(Function(r) r.RowIndex IsNot Nothing AndAlso r.RowIndex.Value = indiceRiga)

        If row Is Nothing Then
            row = New Row() With {.RowIndex = indiceRiga}
            sheetData.AppendChild(row)
        End If

        ' 2. Cerca la cella dentro la riga
        Dim cella As Cell = row.Elements(Of Cell)().FirstOrDefault(Function(c) c.CellReference IsNot Nothing AndAlso c.CellReference.Value = indirizzoCella)

        If cella Is Nothing Then
            cella = New Cell() With {.CellReference = indirizzoCella}
            row.AppendChild(cella)
        End If

        ' --- FIX RISOLUTIVO ---
        ' Rimuoviamo eventuali vecchi indici numerici (SharedStringIndex) ereditati dal modello
        ' che impedivano la sovrascrittura del testo
        cella.RemoveAllChildren()

        ' Forza il tipo di cella a "Stringa Diretta" (InlineString)
        cella.DataType = New EnumValue(Of CellValues)(CellValues.InlineString)

        ' Crea la struttura corretta per il testo diretto nell'XML di Excel (<c:is><t>Valore</t></c:is>)
        Dim inlineString As New InlineString()
        inlineString.AppendChild(New DocumentFormat.OpenXml.Spreadsheet.Text(valoreTesto))
        cella.AppendChild(inlineString)
    End Sub

    Public Function CaricaDatiGraficoGiornaliero_Excel(dbbambino As String) As String
        Dim totali_voci = Carico_totali_giornalieri(dbbambino)
        Dim bambino As String = ComboBox1.Text
        Dim dataDiOggi As String = Now.ToShortDateString()

        Dim percorsoCartellaStampa As String = dir_bar(ini.Path_GRAFICI_DaStampare)
        Dim destinazioneXlsx As String = IO.Path.Combine(percorsoCartellaStampa, bambino & ".Totale giornaliero.xlsx")
        Dim modelloXlsx As String = ini.Modello_Grafico_TotaleGiornaliero

        ' 1. Sdoppiamento del file modello
        Try
            If IO.File.Exists(destinazioneXlsx) Then IO.File.Delete(destinazioneXlsx)
            IO.File.Copy(modelloXlsx, destinazioneXlsx, True)
        Catch ex As Exception
            Call MsgBox("Errore duplicazione modello: " & ex.Message, MsgBoxStyle.Critical)
            Return ""
        End Try

        ' 2. Mappatura dei dati reali (max 55)
        Dim j0 = 0
        If totali_voci.Count > 55 Then j0 = totali_voci.Count - 55  ' Grafico ultimi 55 giorni 

        Dim listaElementi As New List(Of ElementoGrafico)()
        For j = j0 To totali_voci.Count - 1
            Dim voce = totali_voci(j)
            Dim record As New ElementoGrafico() With {.X = voce.Data, .Y1 = voce.tot, .Y2 = voce.tot_POS_voci}
            listaElementi.Add(record)
        Next

        ' 3. Elaborazione OpenXML sicura tramite NOMI DEI FOGLI
        Using docExcel As SpreadsheetDocument = SpreadsheetDocument.Open(destinazioneXlsx, True)
            Dim workbookPart As WorkbookPart = docExcel.WorkbookPart

            ' --- OPERAZIONE A: Scrittura Intestazioni sul foglio "Report" ---
            Dim foglioReportPart As WorksheetPart = OttieniWorksheetPartPerNome(workbookPart, "Report")
            If foglioReportPart IsNot Nothing Then
                Dim datiReport As SheetData = foglioReportPart.Worksheet.GetFirstChild(Of SheetData)()
                ScriviInCellaAssoluta(datiReport, "A1", dataDiOggi)
                ScriviInCellaAssoluta(datiReport, "G1", bambino)
                ' Pulizia precauzionale della riga 2 del report visivo
                ScriviInCellaAssoluta(datiReport, "B2", "")
                ScriviInCellaAssoluta(datiReport, "C2", "")
                foglioReportPart.Worksheet.Save()
            End If

            ' --- OPERAZIONE B: Scrittura dei 55 dati sul foglio "Dati_Grafico" ---
            Dim foglioDatiPart As WorksheetPart = OttieniWorksheetPartPerNome(workbookPart, "Dati_Grafico")
            If foglioDatiPart IsNot Nothing Then
                PopolaDatiMeccaniciGrafico_55Righe(foglioDatiPart, listaElementi)
            End If

            ' --- OPERAZIONE C: Nascondi il foglio dati ---
            Dim schedaDati As Sheet = workbookPart.Workbook.Descendants(Of Sheet)().FirstOrDefault(Function(s) s.Name IsNot Nothing AndAlso s.Name.Value.Equals("Dati_Grafico", StringComparison.OrdinalIgnoreCase))
            If schedaDati IsNot Nothing Then
                schedaDati.State = SheetStateValues.Hidden
            End If

            workbookPart.Workbook.Save()
        End Using

        Return destinazioneXlsx
    End Function
    Private Function OttieniWorksheetPartPerNome(workbookPart As WorkbookPart, nomeFoglio As String) As WorksheetPart
        Dim relId As String = ""
        Dim sheet As Sheet = workbookPart.Workbook.Descendants(Of Sheet)().FirstOrDefault(Function(s) s.Name IsNot Nothing AndAlso s.Name.Value.Equals(nomeFoglio, StringComparison.OrdinalIgnoreCase))

        If sheet IsNot Nothing Then
            relId = sheet.Id.Value
            Return CType(workbookPart.GetPartById(relId), WorksheetPart)
        End If
        Return Nothing
    End Function
    Private Sub PopolaDatiMeccaniciGrafico_55Righe(foglioDatiPart As WorksheetPart, datiInInput As List(Of ElementoGrafico))
        Dim datiFoglioMeccanico As SheetData = foglioDatiPart.Worksheet.GetFirstChild(Of SheetData)()
        Dim maxRigheTarget As Integer = 55

        For i As Integer = 0 To maxRigheTarget - 1
            Dim indiceRigaExcel As UInteger = Convert.ToUInt32(i + 2)

            Dim row As Row = datiFoglioMeccanico.Elements(Of Row)().FirstOrDefault(Function(r) r.RowIndex IsNot Nothing AndAlso r.RowIndex.Value = indiceRigaExcel)
            If row Is Nothing Then
                row = New Row() With {.RowIndex = indiceRigaExcel}
                datiFoglioMeccanico.AppendChild(row)
            End If

            Dim stringaData As String = ""
            Dim valoreY1 As String = ""
            Dim valoreY2 As String = ""

            If i < datiInInput.Count Then
                stringaData = datiInInput(i).X.ToString()
                valoreY1 = datiInInput(i).Y1.ToString(System.Globalization.CultureInfo.InvariantCulture)
                valoreY2 = datiInInput(i).Y2.ToString(System.Globalization.CultureInfo.InvariantCulture)
            End If

            AggiornaSingolaCellaNativa(row, "A" & indiceRigaExcel, stringaData, CellValues.String)
            AggiornaSingolaCellaNativa(row, "B" & indiceRigaExcel, valoreY1, CellValues.Number)
            AggiornaSingolaCellaNativa(row, "C" & indiceRigaExcel, valoreY2, CellValues.Number)
        Next
        foglioDatiPart.Worksheet.Save()
    End Sub

    Private Sub ScriviInCellaAssoluta(sheetData As SheetData, indirizzoCella As String, valoreTesto As String)
        ' Cerca la cella in tutto il foglio usando il riferimento testuale nativo (es. "A1")
        Dim cella As Cell = sheetData.Descendants(Of Cell)().FirstOrDefault(
        Function(c) c.CellReference IsNot Nothing AndAlso c.CellReference.Value.Equals(indirizzoCella, StringComparison.OrdinalIgnoreCase)
    )

        ' Se la cella non esiste fisicamente nell'XML (perché era vuota nel modello), la creiamo da zero
        If cella Is Nothing Then
            Dim stringaRiga As String = String.Join("", indirizzoCella.Where(AddressOf Char.IsDigit))
            Dim indiceRiga As UInteger = Convert.ToUInt32(stringaRiga)

            ' Cerca la riga nell'albero XML
            Dim row As Row = sheetData.Elements(Of Row)().FirstOrDefault(Function(r) r.RowIndex IsNot Nothing AndAlso r.RowIndex.Value = indiceRiga)
            If row Is Nothing Then
                row = New Row() With {.RowIndex = indiceRiga}
                sheetData.AppendChild(row)
            End If

            ' Crea la cella e la aggancia alla riga corretta
            cella = New Cell() With {.CellReference = indirizzoCella}
            row.AppendChild(cella)
        End If

        ' Svuota la cella da formule vecchie, indici di stringhe condivise o residui del modello
        cella.RemoveAllChildren()
        If cella.CellFormula IsNot Nothing Then cella.CellFormula.Text = ""

        ' Forza la scrittura come stringa diretta pulita (InlineString)
        cella.DataType = New EnumValue(Of CellValues)(CellValues.InlineString)

        Dim inlineString As New InlineString()
        inlineString.AppendChild(New DocumentFormat.OpenXml.Spreadsheet.Text(valoreTesto))
        cella.AppendChild(inlineString)
    End Sub

    Private Sub AggiornaSingolaCellaNativa(row As Row, indirizzoCella As String, testoValore As String, tipoDato As CellValues)
        Dim cella As Cell = row.Elements(Of Cell)().FirstOrDefault(Function(c) c.CellReference IsNot Nothing AndAlso c.CellReference.Value = indirizzoCella)
        If cella Is Nothing Then
            cella = New Cell() With {.CellReference = indirizzoCella}
            row.AppendChild(cella)
        End If
        cella.DataType = tipoDato
        ' Se il valore è vuoto omettiamo l'elemento del testo per non sporcare il disegno del grafico
        cella.CellValue = If(String.IsNullOrEmpty(testoValore), New CellValue(), New CellValue(testoValore))
    End Sub



    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click
        'comportamenti disponibili da graficare 
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        'voci ELCAR disonibili da graficare 
        Dim dbBambino = GroupBox1.Tag

        Listachecked = Class_GIORNALIERO.Voci_Utilizzate(dbBambino)
        F_checklist.main(Listachecked, 2)

    End Sub
    Function Esporta_dati_in_Excel() As String
        ' 1. Recupero delle tre liste separate dal database
        Dim dbBambino = GroupBox1.Tag
        Dim elenco_elcar As List(Of Esportazione_dati_ELCAR) = Esportazione_ottieni_tutti_elcar(dbBambino)
        Dim elenco_cp As List(Of Esportazione_dati_CTRT) = Esportazione_ottieni_tutti_CP(dbBambino)
        Dim elenco_rt As List(Of Esportazione_dati_CTRT) = Esportazione_ottieni_tutti_RT(dbBambino)

        Dim bambino As String = ComboBox1.Text
        Dim percorsoCartellaStampa As String = dir_bar(ini.Path_GRAFICI_DaStampare)

        ' Definiamo il nome del file finale unico
        Dim destinazioneXlsx As String = IO.Path.Combine(percorsoCartellaStampa, bambino & ".Dati_esportati.xlsx")

        ' 2. Pulizia precauzionale del vecchio file se esistente
        Try
            If IO.File.Exists(destinazioneXlsx) Then IO.File.Delete(destinazioneXlsx)
        Catch ex As Exception
            Call MsgBox("Impossibile sovrascrivere il file esistente: " & ex.Message, MsgBoxStyle.Exclamation)
            Return ""
        End Try

        ' --- NOTA DI ARCHITETTURA ---
        ' Abbiamo rimosso IO.File.Copy(ini.Modello_Excel_esportazione_dati...) 
        ' perché la nostra sotto-funzione genera autonomamente la struttura .xlsx da zero!

        ' 3. Eseguiamo l'esportazione iniettando i dati nei 3 fogli nativi (ELCAR, CP, RT)
        Try
            EsportaListeSeparateInExcel(elenco_elcar, elenco_cp, elenco_rt, destinazioneXlsx)
            MsgBox("Esportazione completata con successo nei tre fogli (ELCAR, CP, RT)!", MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox("Errore durante la generazione dei fogli Excel: " & ex.Message, MsgBoxStyle.Critical)
            Return ""
        End Try

        Return destinazioneXlsx
    End Function
    Private Sub PopolaFoglioELCAR(spreadsheetDoc As SpreadsheetDocument, sheets As Sheets, sheetId As UInteger, nomeFoglio As String, dati As List(Of Esportazione_dati_ELCAR))
        Dim workbookPart As WorkbookPart = spreadsheetDoc.WorkbookPart
        Dim worksheetPart As WorksheetPart = workbookPart.AddNewPart(Of WorksheetPart)()
        Dim sheetData As New SheetData()
        worksheetPart.Worksheet = New Worksheet(sheetData)

        sheets.AppendChild(New Sheet() With {.Id = workbookPart.GetIdOfPart(worksheetPart), .SheetId = sheetId, .Name = nomeFoglio})

        ' Raggruppamento LINQ per Codice e Descrizione
        Dim gruppi = dati.GroupBy(Function(d) New With {Key d.Codice, Key d.Descrizione}).OrderBy(Function(g) g.Key.Codice).ToList()
        Dim indiceRiga As UInteger = 1

        ' Intestazioni
        Dim rigaInt As Row = AssicuraRigaExcel(sheetData, indiceRiga)
        ScriviCellaNativaDati(rigaInt, "A" & indiceRiga, "Codice / Data", CellValues.String)
        ScriviCellaNativaDati(rigaInt, "B" & indiceRiga, "Descrizione", CellValues.String)
        ScriviCellaNativaDati(rigaInt, "C" & indiceRiga, "Tot 1", CellValues.String)
        ScriviCellaNativaDati(rigaInt, "D" & indiceRiga, "Tot 1 Pos", CellValues.String)
        ScriviCellaNativaDati(rigaInt, "E" & indiceRiga, "Tot 2", CellValues.String)
        ScriviCellaNativaDati(rigaInt, "F" & indiceRiga, "Tot 2 Pos", CellValues.String)
        indiceRiga += 2

        For Each gruppo In gruppi
            Dim rigaCod As Row = AssicuraRigaExcel(sheetData, indiceRiga)
            ScriviCellaNativaDati(rigaCod, "A" & indiceRiga, "CODICE: " & gruppo.Key.Codice, CellValues.String)
            ScriviCellaNativaDati(rigaCod, "B" & indiceRiga, gruppo.Key.Descrizione, CellValues.String)
            indiceRiga += 1

            For Each record In gruppo.OrderBy(Function(d) d.Data)
                Dim rigaDat As Row = AssicuraRigaExcel(sheetData, indiceRiga)
                ScriviCellaNativaDati(rigaDat, "A" & indiceRiga, record.Data.ToString("dd/MM/yyyy"), CellValues.String)
                ScriviCellaNativaDati(rigaDat, "C" & indiceRiga, record.Tot1.ToString(CultureInfo.InvariantCulture), CellValues.Number)
                ScriviCellaNativaDati(rigaDat, "D" & indiceRiga, record.Tot1_Pos.ToString(CultureInfo.InvariantCulture), CellValues.Number)
                ScriviCellaNativaDati(rigaDat, "E" & indiceRiga, record.Tot2.ToString(CultureInfo.InvariantCulture), CellValues.Number)
                ScriviCellaNativaDati(rigaDat, "F" & indiceRiga, record.Tot2_Pos.ToString(CultureInfo.InvariantCulture), CellValues.Number)
                indiceRiga += 1
            Next
            indiceRiga += 1
        Next
        worksheetPart.Worksheet.Save()
    End Sub
    Private Sub PopolaFoglioCTRT(spreadsheetDoc As SpreadsheetDocument, sheets As Sheets, sheetId As UInteger, nomeFoglio As String, dati As List(Of Esportazione_dati_CTRT))
        Dim workbookPart As WorkbookPart = spreadsheetDoc.WorkbookPart
        Dim worksheetPart As WorksheetPart = workbookPart.AddNewPart(Of WorksheetPart)()
        Dim sheetData As New SheetData()
        worksheetPart.Worksheet = New Worksheet(sheetData)

        sheets.AppendChild(New Sheet() With {.Id = workbookPart.GetIdOfPart(worksheetPart), .SheetId = sheetId, .Name = nomeFoglio})

        ' Raggruppamento LINQ solo per Descrizione
        Dim gruppi = dati.GroupBy(Function(d) d.Descrizione).OrderBy(Function(g) g.Key).ToList()
        Dim indiceRiga As UInteger = 1

        ' Intestazioni ridotte per la struttura CTRT
        Dim rigaInt As Row = AssicuraRigaExcel(sheetData, indiceRiga)
        ScriviCellaNativaDati(rigaInt, "A" & indiceRiga, "Descrizione / Data", CellValues.String)
        ScriviCellaNativaDati(rigaInt, "B" & indiceRiga, "Data", CellValues.String)
        ScriviCellaNativaDati(rigaInt, "C" & indiceRiga, "Totale", CellValues.String)
        indiceRiga += 2

        For Each gruppo In gruppi
            Dim rigaDesc As Row = AssicuraRigaExcel(sheetData, indiceRiga)
            ScriviCellaNativaDati(rigaDesc, "A" & indiceRiga, "DESCRIZIONE: " & gruppo.Key, CellValues.String)
            indiceRiga += 1

            For Each record In gruppo.OrderBy(Function(d) d.Data)
                Dim rigaDat As Row = AssicuraRigaExcel(sheetData, indiceRiga)
                ScriviCellaNativaDati(rigaDat, "A" & indiceRiga, "", CellValues.String) ' Lascia vuoto sotto la descrizione principale
                ScriviCellaNativaDati(rigaDat, "B" & indiceRiga, record.Data.ToString("dd/MM/yyyy"), CellValues.String)
                ScriviCellaNativaDati(rigaDat, "C" & indiceRiga, record.Tot.ToString(CultureInfo.InvariantCulture), CellValues.Number)
                indiceRiga += 1
            Next
            indiceRiga += 1
        Next
        worksheetPart.Worksheet.Save()
    End Sub
    Public Sub EsportaListeSeparateInExcel(elenco_elcar As List(Of Esportazione_dati_ELCAR),
                                       elenco_CP As List(Of Esportazione_dati_CTRT),
                                       elenco_RT As List(Of Esportazione_dati_CTRT),
                                       pathExcel As String)

        If IO.File.Exists(pathExcel) Then IO.File.Delete(pathExcel)

        Using spreadsheetDoc As SpreadsheetDocument = SpreadsheetDocument.Create(pathExcel, SpreadsheetDocumentType.Workbook)
            Dim workbookPart As WorkbookPart = spreadsheetDoc.AddWorkbookPart()
            workbookPart.Workbook = New Workbook()
            Dim sheets As Sheets = spreadsheetDoc.WorkbookPart.Workbook.AppendChild(Of Sheets)(New Sheets())

            ' 1. FOGLIO ELCAR (Struttura complessa con Codice, Tot1, Tot2...)
            PopolaFoglioELCAR(spreadsheetDoc, sheets, 1, "ELCAR", elenco_elcar)

            ' 2. FOGLIO CP (Struttura CTRT con Descrizione, Data, Tot)
            PopolaFoglioCTRT(spreadsheetDoc, sheets, 2, "CP", elenco_CP)

            ' 3. FOGLIO RT (Struttura CTRT con Descrizione, Data, Tot)
            PopolaFoglioCTRT(spreadsheetDoc, sheets, 3, "RT", elenco_RT)

            workbookPart.Workbook.Save()
        End Using
    End Sub
    Private Function AssicuraRigaExcel(sheetData As SheetData, indice As UInteger) As Row
        Dim row As Row = sheetData.Elements(Of Row)().FirstOrDefault(Function(r) r.RowIndex IsNot Nothing AndAlso r.RowIndex.Value = indice)
        If row Is Nothing Then
            row = New Row() With {.RowIndex = indice}
            sheetData.AppendChild(row)
        End If
        Return row
    End Function

    Private Sub ScriviCellaNativaDati(row As Row, indirizzo As String, valore As String, tipo As CellValues)
        Dim cella As Cell = row.Elements(Of Cell)().FirstOrDefault(Function(c) c.CellReference IsNot Nothing AndAlso c.CellReference.Value = indirizzo)
        If cella Is Nothing Then
            cella = New Cell() With {.CellReference = indirizzo}
            row.AppendChild(cella)
        End If
        cella.DataType = New EnumValue(Of CellValues)(tipo)
        cella.CellValue = If(String.IsNullOrEmpty(valore), New CellValue(), New CellValue(valore))
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim doc = Esporta_dati_in_Excel()
        If CheckBox1.Checked And doc <> "" Then
            'Call AggiornaGrafico_TotaliGiornalieri(documento, Gbl_elementoGrafico)
            Apri_documento(doc)
        End If

    End Sub



    Public Sub CompilaSingoloFoglioDati_Ciclico(workbookPart As WorkbookPart, nomeFoglio As String, bambino As String, testo As String, datiInInput As List(Of Esportazione_dati_ELCAR))
        ' 1. Trova la WorksheetPart corrispondente al nome del foglio passato come parametro
        Dim sheet As Sheet = workbookPart.Workbook.Descendants(Of Sheet)().FirstOrDefault(Function(s) s.Name IsNot Nothing AndAlso s.Name.Value.Equals(nomeFoglio, StringComparison.OrdinalIgnoreCase))

        If sheet Is Nothing Then
            ' Se il foglio non esiste nel modello (es. errore di battitura), salta per evitare crash
            Exit Sub
        End If

        Dim worksheetPart As WorksheetPart = CType(workbookPart.GetPartById(sheet.Id.Value), WorksheetPart)
        Dim sheetData As SheetData = worksheetPart.Worksheet.GetFirstChild(Of SheetData)()

        ' 2. COMPILAZIONE INTESTAZIONI (A1 e D1) come stringhe dirette pulite
        ScriviCellaInlineAssoluta(sheetData, "A1", bambino)
        ScriviCellaInlineAssoluta(sheetData, "D1", testo)

        ' 3. COMPILAZIONE BLOCCO RIGIDO DATI (Righe da 3 a 15 -> Esattamente 13 slot)
        Dim maxRigheTarget As Integer = 13
        Dim indiceInizioRiga As Integer = 3

        For i As Integer = 0 To maxRigheTarget - 1
            Dim indiceRigaExcel As UInteger = Convert.ToUInt32(i + indiceInizioRiga)

            ' Cerca o forza la creazione della riga XML nel foglio
            Dim row As Row = sheetData.Elements(Of Row)().FirstOrDefault(Function(r) r.RowIndex IsNot Nothing AndAlso r.RowIndex.Value = indiceRigaExcel)
            If row Is Nothing Then
                row = New Row() With {.RowIndex = indiceRigaExcel}
                sheetData.AppendChild(row)
            End If

            ' Valori predefiniti vuoti (se mancano dati reali per completare i 13 slot)
            Dim colA_data As String = ""
            Dim colB_progressivo As String = (i + 1).ToString() ' Numero progressivo automatico 1-13 in colonna B
            Dim colC_codice As String = ""
            Dim colD_tot1 As String = ""
            Dim colE_tot1_pos As String = ""
            Dim colF_tot1_neg As String = ""
            Dim colG_tot1_prmt As String = ""
            Dim colH_tot1_perc As String = ""

            ' Se abbiamo un record reale nella lista, estraiamo i dati mappandoli sulle 8 colonne
            If i < datiInInput.Count Then
                Dim record = datiInInput(i)
                colA_data = record.Data.ToString("dd/MM/yyyy")
                colC_codice = record.Codice ' Inserisce il testo del codice in colonna C
                colD_tot1 = record.Tot1.ToString(CultureInfo.InvariantCulture)
                colE_tot1_pos = record.Tot1_Pos.ToString(CultureInfo.InvariantCulture)
                colF_tot1_neg = record.Tot1_Neg.ToString(CultureInfo.InvariantCulture)
                colG_tot1_prmt = record.Tot1_prompt.ToString(CultureInfo.InvariantCulture)
                colH_tot1_perc = record.Tot1_perc.ToString(CultureInfo.InvariantCulture)
            End If

            ' Scrittura fisica dei dati nelle rispettive colonne del foglio corrente
            AggiornaCellaMeccanica(row, "A" & indiceRigaExcel, colA_data, CellValues.String)
            AggiornaCellaMeccanica(row, "B" & indiceRigaExcel, colB_progressivo, CellValues.Number)
            AggiornaCellaMeccanica(row, "C" & indiceRigaExcel, colC_codice, CellValues.String)
            AggiornaCellaMeccanica(row, "D" & indiceRigaExcel, colD_tot1, CellValues.Number)
            AggiornaCellaMeccanica(row, "E" & indiceRigaExcel, colE_tot1_pos, CellValues.Number)
            AggiornaCellaMeccanica(row, "F" & indiceRigaExcel, colF_tot1_neg, CellValues.Number)
            AggiornaCellaMeccanica(row, "G" & indiceRigaExcel, colG_tot1_prmt, CellValues.Number)
            AggiornaCellaMeccanica(row, "H" & indiceRigaExcel, colH_tot1_perc, CellValues.Number)
        Next

        ' Salva la WorksheetPart del singolo foglio elaborato
        worksheetPart.Worksheet.Save()
    End Sub

    Private Sub ScriviCellaInlineAssoluta(sheetData As SheetData, indirizzoCella As String, valoreTesto As String)
        Dim cella As Cell = sheetData.Descendants(Of Cell)().FirstOrDefault(Function(c) c.CellReference IsNot Nothing AndAlso c.CellReference.Value.Equals(indirizzoCella, StringComparison.OrdinalIgnoreCase))
        If cella Is Nothing Then
            Dim stringaRiga As String = String.Join("", indirizzoCella.Where(AddressOf Char.IsDigit))
            Dim indiceRiga As UInteger = Convert.ToUInt32(stringaRiga)
            Dim row As Row = sheetData.Elements(Of Row)().FirstOrDefault(Function(r) r.RowIndex IsNot Nothing AndAlso r.RowIndex.Value = indiceRiga)
            If row Is Nothing Then
                row = New Row() With {.RowIndex = indiceRiga}
                sheetData.AppendChild(row)
            End If
            cella = New Cell() With {.CellReference = indirizzoCella}
            row.AppendChild(cella)
        End If
        cella.RemoveAllChildren()
        If cella.CellFormula IsNot Nothing Then cella.CellFormula.Text = ""
        cella.DataType = New EnumValue(Of CellValues)(CellValues.InlineString)
        Dim inlineString As New InlineString()
        inlineString.AppendChild(New DocumentFormat.OpenXml.Spreadsheet.Text(valoreTesto))
        cella.AppendChild(inlineString)
    End Sub
    Private Sub AggiornaCellaMeccanica(row As Row, indirizzoCella As String, testoValore As String, tipoDato As CellValues)
        Dim cella As Cell = row.Elements(Of Cell)().FirstOrDefault(Function(c) c.CellReference IsNot Nothing AndAlso c.CellReference.Value = indirizzoCella)
        If cella Is Nothing Then
            cella = New Cell() With {.CellReference = indirizzoCella}
            row.AppendChild(cella)
        End If
        cella.DataType = tipoDato
        cella.CellValue = If(String.IsNullOrEmpty(testoValore), New CellValue(), New CellValue(testoValore))
    End Sub

    Public Function grafico_codici_elcar(id_foglio As Integer) As String
        Dim bambino As String = ComboBox1.Text
        Dim percorsoCartellaStampa As String = dir_bar(ini.Path_GRAFICI_DaStampare)
        Dim destinazioneXlsx As String = IO.Path.Combine(percorsoCartellaStampa, bambino & ".Report_Completo.xlsx")
        'Dim modelloXlsx As String = ini.Modello_Grafico_SetteFogli

        Dim contenitore As New List(Of Esportazione_dati_ELCAR)

        Dim superContenitoreDati As New List(Of List(Of ElementoGrafico))
        Dim riga As DataGridViewRow, cod As String
        Dim record1 As ElementoGrafico
        Gbl_foglio.DB_Bambino = GroupBox1.Tag

        '1) Trova il foglio richiesto ed estrae tutti i codici presenti (max 12)
        Dim fogli = Class_GIORNALIERO.Carica_Foglio(id_foglio)
        Dim nvoci = fogli.Count
        Dim gf As New Esportazione_dati_ELCAR
        Dim elenco_gf As List(Of Esportazione_dati_ELCAR)
        Dim perc As Integer = 0

        ' scelta del modello a 6 o 12 
        Dim modelloXlsx = ""
        If nvoci <= 6 Then
            modelloXlsx = ini.Modello_Grafico_codici6
        Else
            modelloXlsx = ini.Modello_Grafico_codici12
        End If

        ' 1. Copia fisica del modello base a 7 o 13  fogli
        Try
            If IO.File.Exists(destinazioneXlsx) Then IO.File.Delete(destinazioneXlsx)
            IO.File.Copy(modelloXlsx, destinazioneXlsx, True)
        Catch ex As Exception
            Return ""
        End Try

        Dim testo(nvoci)

        ' 2. Apertura del file tramite OpenXML
        Using docExcel As SpreadsheetDocument = SpreadsheetDocument.Open(destinazioneXlsx, True)
            Dim workbookPart As WorkbookPart = docExcel.WorkbookPart
            Dim numFoglio = 0
            For Each foglio In fogli
                cod = foglio.Codice
                If cod = "" Then Continue For
                Call Carica_dati_codice_elcar(cod)
                Dim listaDatiSpecifici = carica_dai_specifici_codice()  ' contiene tutti i dati da graficare di un codice 
                ' TESTI DA INSERIRE IN ALTO
                numFoglio = numFoglio + 1
                Dim testoA1 As String = bambino
                Dim testoD1 As String = Gbl_Graph.Cod_Testo
                Dim nomeFoglioTarget As String = "Dati_" & numFoglio ' Genera "Dati_1", "Dati_2"...
                testo(numFoglio) = foglio.Testo
                ' 🌟 CHIAMATA ALLA ROUTINE MODULARE CICLICA
                CompilaSingoloFoglioDati_Ciclico(workbookPart, nomeFoglioTarget, bambino, testoD1, listaDatiSpecifici)

            Next
            ' 1. Trova la WorksheetPart corrispondente al nome del foglio passato come parametro
            Dim sheet As Sheet = workbookPart.Workbook.Descendants(Of Sheet)().FirstOrDefault(Function(s) s.Name IsNot Nothing AndAlso s.Name.Value.Equals("Report", StringComparison.OrdinalIgnoreCase))
            Dim worksheetPart As WorksheetPart = CType(workbookPart.GetPartById(sheet.Id.Value), WorksheetPart)
            Dim sheetData As SheetData = worksheetPart.Worksheet.GetFirstChild(Of SheetData)()
            ScriviCellaInlineAssoluta(sheetData, "D1", bambino)
            ScriviCellaInlineAssoluta(sheetData, "F1", Now.ToShortDateString)
            ScriviCellaInlineAssoluta(sheetData, "D34", bambino)
            ScriviCellaInlineAssoluta(sheetData, "F34", Now.ToShortDateString)

            Dim coordinateCelle As New List(Of String)(New String() {"B3", "D3", "F3", "B18", "D18", "F18", "B36", "D36", "F36", "B51", "D51", "F51"})
            For j = 0 To nvoci - 1
                ScriviCellaInlineAssoluta(sheetData, coordinateCelle(j), testo(j + 1))
            Next

            Dim graficiDaNascondere As New List(Of Integer)()
            For j = nvoci + 1 To 12
                graficiDaNascondere.Add(j) ' Nasconde il 4° grafico
            Next
            GestisciVisibilitaGraficiReport(workbookPart, graficiDaNascondere)

            ' Salvataggio finale dell'intero pacchetto Excel
            workbookPart.Workbook.Save()

        End Using


        Return destinazioneXlsx
    End Function
    Public Sub GestisciVisibilitaGraficiReport(workbookPart As WorkbookPart, indiciGraficiDaNascondere As List(Of Integer))
        ' 1. Recupera il primo foglio ("Report")
        Dim sheet As Sheet = workbookPart.Workbook.Descendants(Of Sheet)().FirstOrDefault(Function(s) s.Name IsNot Nothing AndAlso s.Name.Value.Equals("Report", StringComparison.OrdinalIgnoreCase))

        If sheet Is Nothing Then Exit Sub

        Dim worksheetPart As WorksheetPart = CType(workbookPart.GetPartById(sheet.Id.Value), WorksheetPart)

        ' 2. Accedi alla DrawingsPart del foglio (lo strato dei disegni e grafici)
        Dim drawingsPart As DrawingsPart = worksheetPart.DrawingsPart
        If drawingsPart IsNot Nothing AndAlso drawingsPart.WorksheetDrawing IsNot Nothing Then

            ' 3. Estrai tutti i contenitori di grafici/immagini presenti (TwoCellAnchor)
            ' Nota: L'ordine nell'XML corrisponde all'ordine con cui sono stati inseriti nel modello
            Dim tuttiIContenitoriGrafici = drawingsPart.WorksheetDrawing.Elements(Of TwoCellAnchor)().ToList()

            ' Il ciclo gira su tutti i grafici del modello (base 0: il primo grafico è l'indice 0, il secondo è 1, ecc.)
            For i As Integer = 0 To tuttiIContenitoriGrafici.Count - 1

                ' Se l'indice del grafico corrente (i + 1) è presente nella tua lista dei grafici da nascondere
                If indiciGraficiDaNascondere.Contains(i + 1) Then
                    Dim contenitoreTarget As TwoCellAnchor = tuttiIContenitoriGrafici(i)

                    ' OPZIONE A: Rimozione totale e pulita dell'oggetto dall'albero XML
                    drawingsPart.WorksheetDrawing.RemoveChild(contenitoreTarget)

                    ' OPZIONE B (Alternativa): Se preferisci non distruggerlo ma renderlo solo invisibile,
                    ' puoi forzarne le dimensioni a zero (larghezza e altezza = 0 EMUs)
                    ' Dim transform2d = contenitoreTarget.Descendants(Of DocumentFormat.OpenXml.Drawing.Transform2D)().FirstOrDefault()
                    ' If transform2d IsNot Nothing Then
                    '     transform2d.Extents.Cx = 0L
                    '     transform2d.Extents.Cy = 0L
                    ' End If
                End If
            Next

            ' Salva le modifiche apportate ai disegni del foglio Report
            worksheetPart.Worksheet.Save()
        End If
    End Sub
    Function carica_dai_specifici_codice() As List(Of Esportazione_dati_ELCAR)
        'utilizza i dati caricati in  Gbl_Graph da Carica_dati_codice_elcar(cod)
        If Gbl_Graph.Data.Count = 0 Then Return Nothing
        Dim gf As New Esportazione_dati_ELCAR
        Dim elenco_gf As New List(Of Esportazione_dati_ELCAR)

        For j = 0 To Gbl_Graph.Data.Count - 1
            gf.Codice = Gbl_Graph.Cod_ELCAR
            gf.Descrizione = Gbl_Graph.Cod_Testo
            gf.Data = Gbl_Graph.Data(j).Data
            gf.Tot1 = Gbl_Graph.Data(j).Tot1
            gf.Tot1_Pos = Gbl_Graph.Data(j).TOT1_Pos
            gf.Tot1_Neg = Gbl_Graph.Data(j).TOT1_Neg
            gf.Tot1_prompt = Gbl_Graph.Data(j).Tot1_PROMPT
            If gf.Tot1 = 20 Then
                gf.Tot1_perc = (gf.Tot1_Pos * 100) / gf.Tot1
            Else
                gf.Tot1_perc = 0
            End If
            elenco_gf.Add(gf)
            If Gbl_Graph.Data(j).Tot2 = 0 Then Continue For
            gf.Tot1 = Gbl_Graph.Data(j).Tot2
            gf.Tot1_Pos = Gbl_Graph.Data(j).TOT2_Pos
            gf.Tot1_Neg = Gbl_Graph.Data(j).TOT2_Neg
            gf.Tot1_prompt = Gbl_Graph.Data(j).Tot2_PROMPT
            If gf.Tot1 = 20 Then
                gf.Tot1_perc = (gf.Tot1_Pos * 100) / gf.Tot1
            Else
                gf.Tot1_perc = 0
            End If
            elenco_gf.Add(gf)
        Next
        Return elenco_gf
    End Function
End Class