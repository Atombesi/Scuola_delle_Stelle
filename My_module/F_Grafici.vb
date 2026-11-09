Imports System.DirectoryServices.ActiveDirectory
Imports System.Globalization
Imports System.Linq
Imports System.Runtime.Intrinsics.X86 ' <-- Fondamentale per accedere a DateAxis, Scaling, ecc.
Imports System.Windows.Controls
Imports DocumentFormat.OpenXml
Imports DocumentFormat.OpenXml.Drawing
Imports DocumentFormat.OpenXml.Drawing.Charts
Imports DocumentFormat.OpenXml.Drawing.Spreadsheet
Imports DocumentFormat.OpenXml.EMMA
Imports DocumentFormat.OpenXml.Office.CustomUI
Imports DocumentFormat.OpenXml.Office2010.CustomUI
Imports DocumentFormat.OpenXml.Office2010.Excel
Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Spreadsheet
Imports DocumentFormat.OpenXml.Wordprocessing
Imports ScottPlot.Statistics



Public Class F_Grafici
    Public Totale_voci As New List(Of Dati_Totali_Giornaliero)
    Public Fogli_Bambino As List(Of Class_GIORNALIERO)
    Private ListaDati As New List(Of DatiGrafico)()
    Dim Checklist_Codice As New List(Of Elenco_checklist)
    Dim Checklist_Fogli As New List(Of Elenco_checklist)
    Dim Checklist_CP As New List(Of Elenco_checklist)
    Private Nome_bambino As String = ""
    Private DB_bambino As String = ""
    Public Listachecked_ELCAR As New List(Of Elenco_checklist)()
    Public Listachecked_CP As New List(Of Elenco_checklist)()
    Private Maxdati_grafico As Integer = 20
    Public documento_docx As String
    Public documento_xlsx As String
    Private Last_idf As Integer = 0  'Ultimo foglio compilato


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'voci ELCAR disonibili da graficare 
        Dim dbBambino = GroupBox1.Tag
        If Listachecked_ELCAR.Count = 0 Then
            Call MsgBox("Nessuna voce CP selezionata", vbInformation + vbOKOnly)
            Exit Sub
        End If

        Dim doc = grafico_codici_elcar_by_codlist(Listachecked_ELCAR)
        If doc = "" Then Exit Sub
        Dim t = "Grafico preparato correttamente"
        Dim m = "Vuoi aprire il file:" + vbCrLf
        m = m + doc
        If MsgBox(m, vbInformation + vbYesNo + vbDefaultButton1, t) <> vbYes Then Exit Sub
        Apri_documento(doc)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim dbBambino = GroupBox1.Tag
        Dim doc = CaricaDatiGraficoGiornaliero_Excel(dbBambino)
        If doc = "" Then Exit Sub
        Dim t = "Grafico creato correttamente"
        Dim m = "Vuoi aprire il file:" + vbCrLf
        m = m + doc
        If MsgBox(m, vbInformation + vbYesNo + vbDefaultButton1, t) <> vbYes Then Exit Sub
        Apri_documento(doc)

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Hide()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim idf = Val(Button1.Tag)
        If idf <= 0 Then Exit Sub
        Dim doc = grafico_codici_elcar_by_IDF(idf)
        If doc = "" Then Exit Sub
        Dim t = "Grafico preparato correttamente"
        Dim m = "Vuoi aprire il file:" + vbCrLf
        m = m + doc
        If MsgBox(m, vbInformation + vbYesNo + vbDefaultButton1, t) <> vbYes Then Exit Sub
        Apri_documento(doc)

    End Sub

    Private Sub F_Grafici_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Carica_elenco_bambini()
        Checklist_Codice = Carica_Checklist("ELCAR")
        Checklist_Fogli = Carica_Checklist("FOGLI")
        Checklist_CP = Carica_Checklist("CP")
        Maxdati_grafico = 20
    End Sub
    Function Carica_Checklist(type As String) As List(Of Elenco_checklist)
        Dim elenco As New List(Of Elenco_checklist)
        Dim voce_check As New Elenco_checklist
        If type = "ELCAR" Then ' Carico elenco di tutti i codici utilizzati
            Dim dt = Class_GIORNALIERO.Ottieni_Tutti_codici_usati
            Dim n = dt.Rows.Count
            If n = 0 Then Return Nothing
            For Each riga As DataRow In dt.Rows
                voce_check.voce = "[" + riga("codice") + "] " + riga("testo")
                voce_check.TAG = riga("codice")
                voce_check.Stato = False
                elenco.Add(voce_check)
            Next
            Return elenco
        End If

        If type = "FOGLI" Then
            Dim temp = Class_GIORNALIERO.Elenco_fogli_compilati()
            For Each voce In temp
                voce_check.voce = voce.DATA
                voce_check.TAG = voce.ID_FOGLIO
                voce_check.Stato = False
                elenco.Add(voce_check)
            Next
            Return elenco
        End If

        If type = "CP" Then
            Dim dt = Class_Giornaliero_CP.Ottieni_Tutte_CP_usati
            Dim n = dt.Rows.Count
            If n = 0 Then Return Nothing
            For Each riga As DataRow In dt.Rows
                voce_check.voce = riga("testo")
                voce_check.TAG = riga("ID_VOCE")
                voce_check.Stato = False
                elenco.Add(voce_check)
            Next
            Return elenco
        End If

    End Function
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
        Dim idf = Class_GIORNALIERO.Trova_ultimo_IDFOGLIO(True)
        Last_idf = idf
        Button1.Tag = idf

    End Sub
    Function Carico_totali_giornalieri_OLD(dbbambino As String) As List(Of Dati_Totali_Giornaliero)
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
        'ListaDati.Clear()

        For j = 0 To Totale_voci.Count - 1
            idf = Totale_voci(j).id_foglio
            Dim voce = Totale_voci(j)
            For Each vocert In totale_Rt
                If idf = vocert.id_foglio Then
                    voce.tot_RT = vocert.tot_RT
                    voce.tot = vocert.tot_RT + voce.tot_voci
                    's1.Data = Date.ParseExact(Totale_voci(j).Data, "dd/MM/yyyy", Nothing)
                    's1.ValoriY = New List(Of Integer)({voce.tot, voce.tot_POS_voci})
                    's1.Color = New List(Of Integer)({COL0, COL0})
                    'ListaDati.Add(s1)
                    Totale_voci(j) = voce
                    Exit For
                End If
            Next
        Next
        'Gbl_datigrafico = ListaDati
        Return Totale_voci


    End Function



    Public Function CaricaDatiGraficoGiornaliero_Excel(dbbambino As String) As String
        Dim totali_voci = Carico_totali_giornalieri(dbbambino)
        Dim bambino As String = ComboBox1.Text
        Dim dataDiOggi As String = Now.ToShortDateString()

        Dim percorsoCartellaStampa As String = dir_bar(ini.Path_GRAFICI_DaStampare)
        Dim destinazioneXlsx As String = IO.Path.Combine(percorsoCartellaStampa, bambino & ".Totale giornaliero.xlsx")
        Dim modelloXlsx As String = ini.Modello_Grafico_TotaleGiornaliero
        'venot IO.File.Exists(destinazioneXlsx) Then IO.File.Delete(destinazioneXlsx)

        ' 1. Sdoppiamento del file modello
        Dim m = "", m1 = ""
        If Not IO.File.Exists(modelloXlsx) Then
            m1 = "il modello:" + modelloXlsx + "NON ESISTE!"
            m = "il modello:" + vbCrLf
            m = m + modelloXlsx + vbCrLf + "NON ESISTE!"
            APPEND_LOG(m1, ini.File_Log, True, "E")
            Call MsgBox(m, vbCritical + vbOKOnly)
        End If
        Try

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
            Dim foglioReportPart As WorksheetPart = EXCEL_OttieniWorksheetPartPerNome(workbookPart, "Report")
            If foglioReportPart IsNot Nothing Then
                Dim datiReport As SheetData = foglioReportPart.Worksheet.GetFirstChild(Of SheetData)()
                EXCEL_ScriviInCellaAssoluta(datiReport, "A1", dataDiOggi)
                EXCEL_ScriviInCellaAssoluta(datiReport, "G1", bambino)
                ' Pulizia precauzionale della riga 2 del report visivo
                EXCEL_ScriviInCellaAssoluta(datiReport, "B2", "")
                EXCEL_ScriviInCellaAssoluta(datiReport, "C2", "")
                foglioReportPart.Worksheet.Save()
            End If

            ' --- OPERAZIONE B: Scrittura dei 55 dati sul foglio "Dati_Grafico" ---
            Dim foglioDatiPart As WorksheetPart = EXCEL_OttieniWorksheetPartPerNome(workbookPart, "Dati_Grafico")
            If foglioDatiPart IsNot Nothing Then
                EXCEL_PopolaDatiMeccaniciGrafico_55Righe(foglioDatiPart, listaElementi)
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
    Private Function EXCEL_OttieniWorksheetPartPerNome(workbookPart As WorkbookPart, nomeFoglio As String) As WorksheetPart
        Dim relId As String = ""
        Dim sheet As Sheet = workbookPart.Workbook.Descendants(Of Sheet)().FirstOrDefault(Function(s) s.Name IsNot Nothing AndAlso s.Name.Value.Equals(nomeFoglio, StringComparison.OrdinalIgnoreCase))

        If sheet IsNot Nothing Then
            relId = sheet.Id.Value
            Return CType(workbookPart.GetPartById(relId), WorksheetPart)
        End If
        Return Nothing
    End Function
    Private Sub EXCEL_PopolaDatiMeccaniciGrafico_55Righe(foglioDatiPart As WorksheetPart, datiInInput As List(Of ElementoGrafico))
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

            EXCEL_AggiornaSingolaCellaNativa(row, "A" & indiceRigaExcel, stringaData, CellValues.String)
            EXCEL_AggiornaSingolaCellaNativa(row, "B" & indiceRigaExcel, valoreY1, CellValues.Number)
            EXCEL_AggiornaSingolaCellaNativa(row, "C" & indiceRigaExcel, valoreY2, CellValues.Number)
        Next
        foglioDatiPart.Worksheet.Save()
    End Sub

    Private Sub EXCEL_ScriviInCellaAssoluta(sheetData As SheetData, indirizzoCella As String, valoreTesto As String)
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

    Private Sub EXCEL_AggiornaSingolaCellaNativa(row As Row, indirizzoCella As String, testoValore As String, tipoDato As CellValues)
        Dim cella As Cell = row.Elements(Of Cell)().FirstOrDefault(Function(c) c.CellReference IsNot Nothing AndAlso c.CellReference.Value = indirizzoCella)
        If cella Is Nothing Then
            cella = New Cell() With {.CellReference = indirizzoCella}
            row.AppendChild(cella)
        End If
        cella.DataType = tipoDato
        ' Se il valore è vuoto omettiamo l'elemento del testo per non sporcare il disegno del grafico
        cella.CellValue = If(String.IsNullOrEmpty(testoValore), New CellValue(), New CellValue(testoValore))
    End Sub



    Private Sub Label6_Click(sender As Object, e As EventArgs)
        'comportamenti disponibili da graficare 
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs)
        'voci ELCAR disonibili da graficare 
        Dim dbBambino = GroupBox1.Tag

        Listachecked_ELCAR = Class_GIORNALIERO.Voci_Utilizzate(dbBambino)
        F_checklist.main(Listachecked_ELCAR, 2)

    End Sub
    Function Esporta_Liste_dati_in_Excel() As String
        ' 1. Recupero delle tre liste separate dal database
        Dim dbBambino = GroupBox1.Tag
        Dim elenco_elcar As List(Of Esportazione_dati_ELCAR) = Esportazione_ottieni_tutti_elcar(dbBambino)
        Dim elenco_cp As List(Of Esportazione_dati_CPRT) = Esportazione_ottieni_tutti_CP(dbBambino)
        Dim elenco_rt As List(Of Esportazione_dati_CPRT) = Esportazione_ottieni_tutti_RT(dbBambino)
        Dim elenco_gn As List(Of Esportazione_dati_ELCAR) = Esportazione_giornaliero(dbBambino)

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

            If IO.File.Exists(destinazioneXlsx) Then IO.File.Delete(destinazioneXlsx)

            Using spreadsheetDoc As SpreadsheetDocument = SpreadsheetDocument.Create(destinazioneXlsx, SpreadsheetDocumentType.Workbook)
                Dim workbookPart As WorkbookPart = spreadsheetDoc.AddWorkbookPart()
                workbookPart.Workbook = New Workbook()
                Dim sheets As Sheets = spreadsheetDoc.WorkbookPart.Workbook.AppendChild(Of Sheets)(New Sheets())

                ' 1. FOGLIO ELCAR (Struttura complessa con Codice, Tot1, Tot2...)
                PopolaFoglioELCAR(spreadsheetDoc, sheets, 1, "ELCAR", elenco_elcar)

                ' 2. FOGLIO CP (Struttura CTRT con Descrizione, Data, Tot)
                PopolaFoglioCPRT(spreadsheetDoc, sheets, 2, "CP", elenco_cp)

                ' 3. FOGLIO RT (Struttura CTRT con Descrizione, Data, Tot)
                PopolaFoglioCPRT(spreadsheetDoc, sheets, 3, "RT", elenco_rt)

                ' 4. FOGLIO totali giornalieri
                PopolaFoglioTotaliGiornaliero(spreadsheetDoc, sheets, 4, "TOT", elenco_gn)

                workbookPart.Workbook.Save()
            End Using



            MsgBox("Esportazione completata con successo nei 4 fogli (ELCAR, CP, RT, TOT)!", MsgBoxStyle.Information)
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
        Dim rigaInt As Row = EXCEL_AssicuraRiga(sheetData, indiceRiga)
        EXCEL_ScriviCellaNativaDati(rigaInt, "A" & indiceRiga, "Codice / Data", CellValues.String)
        EXCEL_ScriviCellaNativaDati(rigaInt, "B" & indiceRiga, "Descrizione", CellValues.String)
        EXCEL_ScriviCellaNativaDati(rigaInt, "C" & indiceRiga, "Tot 1", CellValues.String)
        EXCEL_ScriviCellaNativaDati(rigaInt, "D" & indiceRiga, "Tot 1 Pos", CellValues.String)
        EXCEL_ScriviCellaNativaDati(rigaInt, "E" & indiceRiga, "Tot 2", CellValues.String)
        EXCEL_ScriviCellaNativaDati(rigaInt, "F" & indiceRiga, "Tot 2 Pos", CellValues.String)
        indiceRiga += 2

        For Each gruppo In gruppi
            Dim rigaCod As Row = EXCEL_AssicuraRiga(sheetData, indiceRiga)
            EXCEL_ScriviCellaNativaDati(rigaCod, "A" & indiceRiga, "CODICE: " & gruppo.Key.Codice, CellValues.String)
            EXCEL_ScriviCellaNativaDati(rigaCod, "B" & indiceRiga, gruppo.Key.Descrizione, CellValues.String)
            indiceRiga += 1

            For Each record In gruppo.OrderBy(Function(d) d.Data)
                Dim rigaDat As Row = EXCEL_AssicuraRiga(sheetData, indiceRiga)
                EXCEL_ScriviCellaNativaDati(rigaDat, "A" & indiceRiga, record.Data.ToString("dd/MM/yyyy"), CellValues.String)
                EXCEL_ScriviCellaNativaDati(rigaDat, "C" & indiceRiga, record.Tot1.ToString(CultureInfo.InvariantCulture), CellValues.Number)
                EXCEL_ScriviCellaNativaDati(rigaDat, "D" & indiceRiga, record.Tot1_Pos.ToString(CultureInfo.InvariantCulture), CellValues.Number)
                EXCEL_ScriviCellaNativaDati(rigaDat, "E" & indiceRiga, record.Tot2.ToString(CultureInfo.InvariantCulture), CellValues.Number)
                EXCEL_ScriviCellaNativaDati(rigaDat, "F" & indiceRiga, record.Tot2_Pos.ToString(CultureInfo.InvariantCulture), CellValues.Number)
                indiceRiga += 1
            Next
            indiceRiga += 1
        Next
        worksheetPart.Worksheet.Save()
    End Sub
    Private Sub PopolaFoglioTotaliGiornaliero(spreadsheetDoc As SpreadsheetDocument, sheets As Sheets, sheetId As UInteger, nomeFoglio As String, dati As List(Of Esportazione_dati_ELCAR))
        Dim workbookPart As WorkbookPart = spreadsheetDoc.WorkbookPart
        Dim worksheetPart As WorksheetPart = workbookPart.AddNewPart(Of WorksheetPart)()
        Dim sheetData As New SheetData()
        worksheetPart.Worksheet = New Worksheet(sheetData)

        sheets.AppendChild(New Sheet() With {.Id = workbookPart.GetIdOfPart(worksheetPart), .SheetId = sheetId, .Name = nomeFoglio})

        ' MODIFICA: Rimuoviamo il raggruppamento. Ordiniamo direttamente l'intera lista per Data.
        ' Se preferisci ordinare prima per Codice e poi per Data, puoi fare:
        ' Dim datiOrdinati = dati.OrderBy(Function(d) d.Codice).ThenBy(Function(d) d.Data).ToList()
        Dim datiOrdinati = dati.OrderBy(Function(d) d.Data).ToList()

        Dim indiceRiga As UInteger = 1

        ' Intestazioni (Mantenute fisse come le avevi impostate)
        Dim rigaInt As Row = EXCEL_AssicuraRiga(sheetData, indiceRiga)
        EXCEL_ScriviCellaNativaDati(rigaInt, "A" & indiceRiga, "Data", CellValues.String)
        EXCEL_ScriviCellaNativaDati(rigaInt, "C" & indiceRiga, "Tot ", CellValues.String)
        EXCEL_ScriviCellaNativaDati(rigaInt, "D" & indiceRiga, "Tot Pos", CellValues.String)

        ' Passiamo alla riga 3 per iniziare a scrivere i dati (lasciando la riga 2 vuota sotto l'intestazione)
        indiceRiga += 2

        ' MODIFICA: Un singolo ciclo lineare che scorre i record uno dopo l'altro senza interruzioni
        For Each record In datiOrdinati
            Dim rigaDat As Row = EXCEL_AssicuraRiga(sheetData, indiceRiga)

            EXCEL_ScriviCellaNativaDati(rigaDat, "A" & indiceRiga, record.Data.ToString("dd/MM/yyyy"), CellValues.String)
            EXCEL_ScriviCellaNativaDati(rigaDat, "C" & indiceRiga, record.Tot1.ToString(CultureInfo.InvariantCulture), CellValues.Number)
            EXCEL_ScriviCellaNativaDati(rigaDat, "D" & indiceRiga, record.Tot1_Pos.ToString(CultureInfo.InvariantCulture), CellValues.Number)

            ' Incremento rigido di 1: la riga successiva sarà immediatamente sotto, senza salti visivi
            indiceRiga += 1
        Next

        worksheetPart.Worksheet.Save()
    End Sub
    Private Sub PopolaFoglioCPRT(spreadsheetDoc As SpreadsheetDocument, sheets As Sheets, sheetId As UInteger, nomeFoglio As String, dati As List(Of Esportazione_dati_CPRT))
        Dim workbookPart As WorkbookPart = spreadsheetDoc.WorkbookPart
        Dim worksheetPart As WorksheetPart = workbookPart.AddNewPart(Of WorksheetPart)()
        Dim sheetData As New SheetData()
        worksheetPart.Worksheet = New Worksheet(sheetData)

        sheets.AppendChild(New Sheet() With {.Id = workbookPart.GetIdOfPart(worksheetPart), .SheetId = sheetId, .Name = nomeFoglio})

        ' Raggruppamento LINQ solo per Descrizione
        Dim gruppi = dati.GroupBy(Function(d) d.Descrizione).OrderBy(Function(g) g.Key).ToList()
        Dim indiceRiga As UInteger = 1

        ' Intestazioni ridotte per la struttura CTRT
        Dim rigaInt As Row = EXCEL_AssicuraRiga(sheetData, indiceRiga)
        EXCEL_ScriviCellaNativaDati(rigaInt, "A" & indiceRiga, "Descrizione / Data", CellValues.String)
        EXCEL_ScriviCellaNativaDati(rigaInt, "B" & indiceRiga, "Data", CellValues.String)
        EXCEL_ScriviCellaNativaDati(rigaInt, "C" & indiceRiga, "Totale", CellValues.String)
        indiceRiga += 2

        For Each gruppo In gruppi
            Dim rigaDesc As Row = EXCEL_AssicuraRiga(sheetData, indiceRiga)
            EXCEL_ScriviCellaNativaDati(rigaDesc, "A" & indiceRiga, "DESCRIZIONE: " & gruppo.Key, CellValues.String)
            indiceRiga += 1

            For Each record In gruppo.OrderBy(Function(d) d.Data)
                Dim rigaDat As Row = EXCEL_AssicuraRiga(sheetData, indiceRiga)
                EXCEL_ScriviCellaNativaDati(rigaDat, "A" & indiceRiga, "", CellValues.String) ' Lascia vuoto sotto la descrizione principale
                EXCEL_ScriviCellaNativaDati(rigaDat, "B" & indiceRiga, record.Data.ToString("dd/MM/yyyy"), CellValues.String)
                EXCEL_ScriviCellaNativaDati(rigaDat, "C" & indiceRiga, record.Tot.ToString(CultureInfo.InvariantCulture), CellValues.Number)
                indiceRiga += 1
            Next
            indiceRiga += 1
        Next
        worksheetPart.Worksheet.Save()
    End Sub


    Private Function EXCEL_AssicuraRiga(sheetData As SheetData, indice As UInteger) As Row
        Dim row As Row = sheetData.Elements(Of Row)().FirstOrDefault(Function(r) r.RowIndex IsNot Nothing AndAlso r.RowIndex.Value = indice)
        If row Is Nothing Then
            row = New Row() With {.RowIndex = indice}
            sheetData.AppendChild(row)
        End If
        Return row
    End Function

    Private Sub EXCEL_ScriviCellaNativaDati(row As Row, indirizzo As String, valore As String, tipo As CellValues)
        Dim cella As Cell = row.Elements(Of Cell)().FirstOrDefault(Function(c) c.CellReference IsNot Nothing AndAlso c.CellReference.Value = indirizzo)
        If cella Is Nothing Then
            cella = New Cell() With {.CellReference = indirizzo}
            row.AppendChild(cella)
        End If
        cella.DataType = New EnumValue(Of CellValues)(tipo)
        cella.CellValue = If(String.IsNullOrEmpty(valore), New CellValue(), New CellValue(valore))
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim doc = Esporta_Liste_dati_in_Excel()
        If doc = "" Then Exit Sub
        Dim t = "Esportazione Effettuata correttamente"
        Dim m = "Vuoi aprire il file:" + vbCrLf
        m = m + doc
        If MsgBox(m, vbInformation + vbYesNo + vbDefaultButton1, t) <> vbYes Then Exit Sub
        Apri_documento(doc)

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
        EXCEL_ScriviCellaInlineAssoluta(sheetData, "A1", bambino)
        EXCEL_ScriviCellaInlineAssoluta(sheetData, "D1", testo)

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
            Dim colI_Valutativo As String = ""

            ' Se abbiamo un record reale nella lista, estraiamo i dati mappandoli sulle 8 colonne
            If i < datiInInput.Count Then
                Dim record = datiInInput(i)
                colA_data = record.Data.ToString("dd/MM/yy")
                colC_codice = record.Codice ' Inserisce il testo del codice in colonna C
                colD_tot1 = record.Tot1.ToString(CultureInfo.InvariantCulture)
                colE_tot1_pos = record.Tot1_Pos.ToString(CultureInfo.InvariantCulture)
                colF_tot1_neg = record.Tot1_Neg.ToString(CultureInfo.InvariantCulture)
                colG_tot1_prmt = record.Tot1_prompt.ToString(CultureInfo.InvariantCulture)
                colH_tot1_perc = record.Tot1_perc.ToString(CultureInfo.InvariantCulture)
                colI_Valutativo = record.valutativo.ToString(CultureInfo.InvariantCulture)

            End If

            ' Scrittura fisica dei dati nelle rispettive colonne del foglio corrente
            EXCEL_AggiornaCellaMeccanica(row, "A" & indiceRigaExcel, colA_data, CellValues.String)
            EXCEL_AggiornaCellaMeccanica(row, "B" & indiceRigaExcel, colB_progressivo, CellValues.Number)
            EXCEL_AggiornaCellaMeccanica(row, "C" & indiceRigaExcel, colC_codice, CellValues.String)
            EXCEL_AggiornaCellaMeccanica(row, "D" & indiceRigaExcel, colD_tot1, CellValues.Number)
            EXCEL_AggiornaCellaMeccanica(row, "E" & indiceRigaExcel, colE_tot1_pos, CellValues.Number)
            EXCEL_AggiornaCellaMeccanica(row, "F" & indiceRigaExcel, colF_tot1_neg, CellValues.Number)
            EXCEL_AggiornaCellaMeccanica(row, "G" & indiceRigaExcel, colG_tot1_prmt, CellValues.Number)
            EXCEL_AggiornaCellaMeccanica(row, "H" & indiceRigaExcel, colH_tot1_perc, CellValues.Number)
            EXCEL_AggiornaCellaMeccanica(row, "I" & indiceRigaExcel, colI_Valutativo, CellValues.Number)
        Next

        ' Salva la WorksheetPart del singolo foglio elaborato
        worksheetPart.Worksheet.Save()
    End Sub
    ''' <summary>
    ''' Versione corretta: Elimina i nodi formula e cancella gli attributi SharedString per evitare la corruzione
    ''' </summary>
    ''' <summary>
    ''' Versione blindata: azzera gli stili e le formule in modo nativo senza generare KeyNotFoundException
    ''' </summary>



    Private Sub EXCEL_ScriviCellaInlineAssoluta(sheetData As SheetData, indirizzoCella As String, valoreTesto As String)
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
    Private Sub EXCEL_AggiornaCellaMeccanica(row As Row, indirizzoCella As String, testoValore As String, tipoDato As CellValues)
        Dim cella As Cell = row.Elements(Of Cell)().FirstOrDefault(Function(c) c.CellReference IsNot Nothing AndAlso c.CellReference.Value = indirizzoCella)
        If cella Is Nothing Then
            cella = New Cell() With {.CellReference = indirizzoCella}
            row.AppendChild(cella)
        End If
        cella.DataType = tipoDato
        cella.CellValue = If(String.IsNullOrEmpty(testoValore), New CellValue(), New CellValue(testoValore))
    End Sub

    Public Function grafico_codici_elcar_by_codlist(listachecked) As String
        Dim bambino As String = ComboBox1.Text
        Dim percorsoCartellaStampa As String = dir_bar(ini.Path_GRAFICI_DaStampare)
        Dim destinazioneXlsx As String = IO.Path.Combine(percorsoCartellaStampa, bambino & ".Report_Voci_selezionate.xlsx")
        'Dim modelloXlsx As String = ini.Modello_Grafico_SetteFogli

        Dim contenitore As New List(Of Esportazione_dati_ELCAR)

        Dim superContenitoreDati As New List(Of List(Of ElementoGrafico))
        Dim riga As DataGridViewRow, cod As String
        Dim record1 As ElementoGrafico
        Gbl_foglio.DB_Bambino = GroupBox1.Tag

        '1) Trova il foglio richiesto ed estrae tutti i codici presenti (max 12)
        Dim nvoci = listachecked.count
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
            For Each voce In listachecked
                cod = voce.tag
                If cod = "" Then Continue For
                Call Carica_dati_codice_elcar(cod)
                Dim listaDatiSpecifici = carica_dati_specifici_codice(Maxdati_grafico)  ' contiene tutti i dati da graficare di un codice 
                ' TESTI DA INSERIRE IN ALTO
                numFoglio = numFoglio + 1
                Dim testoA1 As String = bambino
                Dim testoD1 As String = Gbl_Graph.Cod_Testo
                Dim nomeFoglioTarget As String = "Dati_" & numFoglio ' Genera "Dati_1", "Dati_2"...
                testo(numFoglio) = voce.voce
                ' 🌟 CHIAMATA ALLA ROUTINE MODULARE CICLICA
                CompilaSingoloFoglioDati_Ciclico(workbookPart, nomeFoglioTarget, bambino, testoD1, listaDatiSpecifici)

            Next
            ' 1. Trova la WorksheetPart corrispondente al nome del foglio passato come parametro
            Dim sheet As Sheet = workbookPart.Workbook.Descendants(Of Sheet)().FirstOrDefault(Function(s) s.Name IsNot Nothing AndAlso s.Name.Value.Equals("Report", StringComparison.OrdinalIgnoreCase))
            Dim worksheetPart As WorksheetPart = CType(workbookPart.GetPartById(sheet.Id.Value), WorksheetPart)
            Dim sheetData As SheetData = worksheetPart.Worksheet.GetFirstChild(Of SheetData)()
            EXCEL_ScriviCellaInlineAssoluta(sheetData, "D1", bambino)
            EXCEL_ScriviCellaInlineAssoluta(sheetData, "F1", Now.ToShortDateString)
            EXCEL_ScriviCellaInlineAssoluta(sheetData, "B1", "Selezione Manuale")
            If nvoci > 6 Then
                ' Scrive i dati per la seconda pagina
                EXCEL_ScriviCellaInlineAssoluta(sheetData, "D34", bambino)
                EXCEL_ScriviCellaInlineAssoluta(sheetData, "F34", Now.ToShortDateString)
                EXCEL_ScriviCellaInlineAssoluta(sheetData, "B34", "Selezione Manuale")
            End If

            Dim coordinateCelle As New List(Of String)(New String() {"B3", "D3", "F3", "B18", "D18", "F18", "B36", "D36", "F36", "B51", "D51", "F51"})
            For j = 0 To nvoci - 1
                EXCEL_ScriviCellaInlineAssoluta(sheetData, coordinateCelle(j), testo(j + 1))
            Next

            Dim graficiDaNascondere As New List(Of String)()
            Dim x = ""
            For j = nvoci + 1 To 12
                x = "Chart " + j.ToString
                graficiDaNascondere.Add(x) ' Nasconde il 4° grafico
                'graficiDaNascondere.Add(j) ' Nasconde il 4° grafico
            Next

            ' Chiamata alla nuova routine chirurgica per nome
            EXCEL_GestisciVisibilitaGraficiPerNome(workbookPart, graficiDaNascondere)


            'GestisciVisibilitaGraficiReport(workbookPart, graficiDaNascondere)


            ' =====================================================================
            ' 🌟 FIX DEFINITIVO APERTURA: ELIMINAZIONE DELLA CATENA DI CALCOLO 🌟
            ' Rimuove il file calcChain.xml orfano per azzerare l'errore al primo avvio
            ' =====================================================================
            Dim calcChainPart As CalculationChainPart = workbookPart.CalculationChainPart
            If calcChainPart IsNot Nothing Then
                workbookPart.DeletePart(calcChainPart)
            End If

            ' Salvataggio finale dell'intero pacchetto Excel
            workbookPart.Workbook.Save()

        End Using


        Return destinazioneXlsx
    End Function

    Public Function grafico_voci_CP(listachecked) As String
        Dim bambino As String = ComboBox1.Text
        Dim percorsoCartellaStampa As String = dir_bar(ini.Path_GRAFICI_DaStampare)
        Dim destinazioneXlsx As String = IO.Path.Combine(percorsoCartellaStampa, bambino & ".Report_Voci_CP_selezionate.xlsx")
        Dim contenitore As New List(Of Esportazione_dati_CPRT)
        Dim cod As String
        Gbl_foglio.DB_Bambino = GroupBox1.Tag
        Dim gf As New Esportazione_dati_CPRT
        Dim elenco_gf As List(Of Esportazione_dati_CPRT)
        Dim perc As Integer = 0

        ' scelta del modello a 6 o 12 
        Dim nvoci = listachecked.count
        Dim modelloXlsx = ""
        modelloXlsx = ini.Modello_Grafico_CP_6

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
            For Each voce In listachecked
                cod = voce.voce
                If cod = "" Then Continue For
                Call Carica_dati_voce_CP(cod, True)
                Dim listaDatiSpecifici = carica_dati_specifici_codice(Maxdati_grafico)  ' contiene tutti i dati da graficare di un codice 
                ' TESTI DA INSERIRE IN ALTO
                numFoglio = numFoglio + 1
                Dim testoA1 As String = bambino
                Dim testoD1 As String = Gbl_Graph.Cod_Testo
                Dim nomeFoglioTarget As String = "Dati_" & numFoglio ' Genera "Dati_1", "Dati_2"...
                testo(numFoglio) = voce.voce
                ' 🌟 CHIAMATA ALLA ROUTINE MODULARE CICLICA
                CompilaSingoloFoglioDati_Ciclico(workbookPart, nomeFoglioTarget, bambino, testoD1, listaDatiSpecifici)

            Next
            ' 1. Trova la WorksheetPart corrispondente al nome del foglio passato come parametro
            Dim sheet As Sheet = workbookPart.Workbook.Descendants(Of Sheet)().FirstOrDefault(Function(s) s.Name IsNot Nothing AndAlso s.Name.Value.Equals("Report", StringComparison.OrdinalIgnoreCase))
            Dim worksheetPart As WorksheetPart = CType(workbookPart.GetPartById(sheet.Id.Value), WorksheetPart)
            Dim sheetData As SheetData = worksheetPart.Worksheet.GetFirstChild(Of SheetData)()
            EXCEL_ScriviCellaInlineAssoluta(sheetData, "D1", bambino)
            EXCEL_ScriviCellaInlineAssoluta(sheetData, "F1", Now.ToShortDateString)
            EXCEL_ScriviCellaInlineAssoluta(sheetData, "B1", "Selezione Manuale")
            If nvoci > 6 Then
                ' Scrive i dati per la seconda pagina
                EXCEL_ScriviCellaInlineAssoluta(sheetData, "D34", bambino)
                EXCEL_ScriviCellaInlineAssoluta(sheetData, "F34", Now.ToShortDateString)
                EXCEL_ScriviCellaInlineAssoluta(sheetData, "B34", "Selezione Manuale")
            End If

            Dim coordinateCelle As New List(Of String)(New String() {"B3", "D3", "F3", "B18", "D18", "F18", "B36", "D36", "F36", "B51", "D51", "F51"})
            For j = 0 To nvoci - 1
                EXCEL_ScriviCellaInlineAssoluta(sheetData, coordinateCelle(j), testo(j + 1))
            Next

            Dim graficiDaNascondere As New List(Of String)()
            Dim x = ""
            For j = nvoci + 1 To 12
                x = "Chart " + j.ToString
                graficiDaNascondere.Add(x) ' Nasconde il 4° grafico
                'graficiDaNascondere.Add(j) ' Nasconde il 4° grafico
            Next

            ' Chiamata alla nuova routine chirurgica per nome
            EXCEL_GestisciVisibilitaGraficiPerNome(workbookPart, graficiDaNascondere)


            'GestisciVisibilitaGraficiReport(workbookPart, graficiDaNascondere)


            ' =====================================================================
            ' 🌟 FIX DEFINITIVO APERTURA: ELIMINAZIONE DELLA CATENA DI CALCOLO 🌟
            ' Rimuove il file calcChain.xml orfano per azzerare l'errore al primo avvio
            ' =====================================================================
            Dim calcChainPart As CalculationChainPart = workbookPart.CalculationChainPart
            If calcChainPart IsNot Nothing Then
                workbookPart.DeletePart(calcChainPart)
            End If

            ' Salvataggio finale dell'intero pacchetto Excel
            workbookPart.Workbook.Save()

        End Using


        Return destinazioneXlsx
    End Function
    Public Function grafico_codici_elcar_by_IDF(id_foglio As Integer) As String
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
            Call MsgBox(ex.Message)
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
                Dim listaDatiSpecifici = carica_dati_specifici_codice(Maxdati_grafico)  ' contiene tutti i dati da graficare di un codice (max 20)
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
            EXCEL_ScriviCellaInlineAssoluta(sheetData, "D1", bambino)
            EXCEL_ScriviCellaInlineAssoluta(sheetData, "F1", Now.ToShortDateString)
            EXCEL_ScriviCellaInlineAssoluta(sheetData, "D34", bambino)
            EXCEL_ScriviCellaInlineAssoluta(sheetData, "F34", Now.ToShortDateString)

            Dim coordinateCelle As New List(Of String)(New String() {"B3", "D3", "F3", "B18", "D18", "F18", "B36", "D36", "F36", "B51", "D51", "F51"})
            For j = 0 To nvoci - 1
                EXCEL_ScriviCellaInlineAssoluta(sheetData, coordinateCelle(j), testo(j + 1))
            Next

            Dim graficiDaNascondere As New List(Of String)()
            For j = nvoci + 1 To 12
                Dim x = "Chart " + j.ToString
                graficiDaNascondere.Add(x) ' Nasconde il 4° grafico
                'graficiDaNascondere.Add(j) ' Nasconde il 4° grafico
            Next

            ' Chiamata alla nuova routine chirurgica per nome
            EXCEL_GestisciVisibilitaGraficiPerNome(workbookPart, graficiDaNascondere)



            ' =====================================================================
            ' 🌟 FIX DEFINITIVO APERTURA: ELIMINAZIONE DELLA CATENA DI CALCOLO 🌟
            ' Rimuove il file calcChain.xml orfano per azzerare l'errore al primo avvio
            ' =====================================================================
            Dim calcChainPart As CalculationChainPart = workbookPart.CalculationChainPart
            If calcChainPart IsNot Nothing Then
                workbookPart.DeletePart(calcChainPart)
            End If

            ' Salvataggio finale dell'intero pacchetto Excel
            workbookPart.Workbook.Save()

        End Using


        Return destinazioneXlsx

    End Function
    Public Sub EXCEL_GestisciVisibilitaGraficiPerNome(workbookPart As DocumentFormat.OpenXml.Packaging.WorkbookPart, nomiGraficiDaNascondere As List(Of String))
        ' 1. Recupera il primo foglio ("Report")
        Dim sheet As DocumentFormat.OpenXml.Spreadsheet.Sheet =
        workbookPart.Workbook.Descendants(Of DocumentFormat.OpenXml.Spreadsheet.Sheet)().FirstOrDefault(
            Function(s) s.Name IsNot Nothing AndAlso s.Name.Value.Equals("Report", StringComparison.OrdinalIgnoreCase)
        )

        If sheet Is Nothing Then Exit Sub

        Dim worksheetPart As DocumentFormat.OpenXml.Packaging.WorksheetPart =
        CType(workbookPart.GetPartById(sheet.Id.Value), DocumentFormat.OpenXml.Packaging.WorksheetPart)

        Dim drawingsPart As DocumentFormat.OpenXml.Packaging.DrawingsPart = worksheetPart.DrawingsPart

        ' 2. Accediamo allo strato dei disegni
        If drawingsPart IsNot Nothing AndAlso drawingsPart.WorksheetDrawing IsNot Nothing Then

            ' --- RISOLUZIONE BC30456 & BC32023 ---
            ' Estraiamo gli elementi usando il percorso completo dell'SDK OpenXML (SpreadsheetDrawing)
            ' Evitiamo .ToList() e usiamo l'interfaccia IEnumerable nativa riconosciuta dal For Each
            Dim tuttiIGraficiAtomici As IEnumerable(Of DocumentFormat.OpenXml.Drawing.Spreadsheet.GraphicFrame) =
            drawingsPart.WorksheetDrawing.Descendants(Of DocumentFormat.OpenXml.Drawing.Spreadsheet.GraphicFrame)()

            For Each grafico As DocumentFormat.OpenXml.Drawing.Spreadsheet.GraphicFrame In tuttiIGraficiAtomici

                ' --- RISOLUZIONE NONVISUALDRAWINGPROPERTIES ---
                ' Specifichiamo il percorso completo della classe interna ai fogli di calcolo (Spreadsheet)
                Dim propNonVisive As DocumentFormat.OpenXml.Drawing.Spreadsheet.NonVisualDrawingProperties =
                grafico.Descendants(Of DocumentFormat.OpenXml.Drawing.Spreadsheet.NonVisualDrawingProperties)().FirstOrDefault()

                If propNonVisive IsNot Nothing AndAlso propNonVisive.Name IsNot Nothing Then
                    Dim nomeGraficoReale As String = propNonVisive.Name.Value ' Legge es. "Chart 1"

                    ' Se il nome del grafico è nella lista dei grafici da nascondere, attiviamo il flag Hidden
                    If nomiGraficiDaNascondere.Contains(nomeGraficoReale, StringComparer.OrdinalIgnoreCase) Then

                        ' Attiviamo la proprietà nativa del foglio OpenXML per nascondere l'oggetto
                        propNonVisive.Hidden = New DocumentFormat.OpenXml.BooleanValue(True)

                    End If
                End If
            Next

            ' Salva le modifiche apportate ai disegni del foglio Report
            worksheetPart.Worksheet.Save()
        End If
    End Sub


    Function carica_dati_specifici_codice(Optional max = 0) As List(Of Esportazione_dati_ELCAR)
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
            gf.valutativo = 0
            If Gbl_Graph.Data(j).Valutativo Then
                gf.valutativo = 100
            End If

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
            gf.valutativo = 0
            If gf.Tot1 = 20 Then
                gf.Tot1_perc = (gf.Tot1_Pos * 100) / gf.Tot1
            Else
                gf.Tot1_perc = 0
            End If
            elenco_gf.Add(gf)
        Next

        If max <= 0 Then Return elenco_gf

        ' se devo limito il numero di dati al valore massimo 
        Dim elementiDaRimuovere As Integer = elenco_gf.Count - max
        If elementiDaRimuovere <= 0 Then Return elenco_gf
        elenco_gf.RemoveRange(0, elementiDaRimuovere)
        Dim m = "Rimozione (" + elementiDaRimuovere.ToString + " dati in eccesso per il grafico " + Nome_bambino
        APPEND_LOG(m, ini.File_Log, True, "W")
        Return elenco_gf

    End Function

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked Then
            Dim n = 0
            Listachecked_ELCAR = F_checklist.main(Checklist_Codice, 12)
            Try
                n = Listachecked_ELCAR.Count
            Catch ex As Exception
                CheckBox2.Checked = False
                Exit Sub
            End Try
            Button2.BackColor = MyColor.PapayaWhip
            Button2.Enabled = True
            Button2.Text = n.ToString + " Voci scelte"
            tp1.SetToolTip(Button2, "Usa voci utilizzate nell'ultimo foglio compilato")
        Else
            Button2.Text = "voci (max 12)"
            Button2.BackColor = SystemColors.Control
            Button2.Enabled = False
            tp1.SetToolTip(Button2, "Usa voci Selezionate")
        End If
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked Then
            Listachecked_ELCAR = F_checklist.main(Checklist_Fogli, 1)
            If Listachecked_ELCAR.Count = 0 Then Exit Sub
            Button1.Tag = Val(Listachecked_ELCAR(0).TAG)
            Dim m = "Foglio (" + Button1.Tag.ToString + ") del:" + Listachecked_ELCAR(0).voce
            tp1.SetToolTip(Button1, m)
            Button1.BackColor = MyColor.PapayaWhip
        Else
            Button1.Tag = Last_idf
            tp1.SetToolTip(Button1, "Ultimo foglio compilato")
            Button1.BackColor = SystemColors.Control
        End If

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        If Not CheckBox1.Checked Then
            'leggo  le cp dall'ultimofoglio
            Dim idf = CInt(Button1.Tag)
            Dim dt = Class_Giornaliero_CP.Ottieni_CP_by_IDF(idf)
            Dim voce As New Elenco_checklist
            For Each riga As DataRow In dt.Rows
                voce.TAG = Convert.ToInt32(riga("ID_voce"))
                voce.voce = Convert.ToString(riga("testo"))
                voce.Stato = True
                Listachecked_CP.Add(voce)
            Next
            If Listachecked_CP.Count = 0 Then
                Call MsgBox("Non è stata trovata nessuna voce CP nell'ultimo foglio", vbInformation + vbOKOnly)
                Exit Sub
            End If
        End If

        If Listachecked_CP.Count = 0 Then
            Call MsgBox("Nessuna voce CP selezionata", vbInformation + vbOKOnly)
            Exit Sub
        End If
        Dim doc = grafico_voci_CP(Listachecked_CP)
        If doc = "" Then Exit Sub
        Dim t = "Grafico preparato correttamente"
        Dim m = "Vuoi aprire il file:" + vbCrLf
        m = m + doc
        If MsgBox(m, vbInformation + vbYesNo + vbDefaultButton1, t) <> vbYes Then Exit Sub
        Apri_documento(doc)




    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            Dim n = 0
            Listachecked_CP = F_checklist.main(Checklist_CP, 6)
            Try
                n = Listachecked_CP.Count
            Catch ex As Exception
                CheckBox1.Checked = False
                Exit Sub
            End Try
            Button4.BackColor = MyColor.PapayaWhip
            Button4.Enabled = True
            Button4.Text = n.ToString + " Voci scelte"
            tp1.SetToolTip(Button4, "Usa voci CP dell'ultimo foglio compilato")

        Else
            Button4.Text = "Comportamentali"
            Button4.BackColor = SystemColors.Control
            Button4.Enabled = True
            tp1.SetToolTip(Button4, "Usa voci CP selezionate")
        End If

    End Sub
End Class