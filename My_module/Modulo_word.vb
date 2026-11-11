Imports DocumentFormat.OpenXml
Imports DocumentFormat.OpenXml.Bibliography
Imports DocumentFormat.OpenXml.Drawing.Charts   ' Contiene le definizioni dei grafici (ChartSpace, NumberValues, ecc.)
Imports DocumentFormat.OpenXml.Packaging        ' Gestisce i pacchetti del file (ChartPart, MainDocumentPart)
Imports DocumentFormat.OpenXml.Wordprocessing
Imports System.Drawing.Printing
Imports System.Linq ' <-- Fondamentale per far funzionare .Descendants(Of T)()

Imports C = DocumentFormat.OpenXml.Drawing.Charts

Module Modulo_word

    ''' <summary>
    ''' Routine principale: Apre il documento Word, individua il primo grafico e lo aggiorna.
    ''' </summary>
    Public Function Calcola_angolo_di_Rotazione(x1, y1, x2, y2) As Single

        ' Calcola la differenza orizzontale e verticale
        Dim deltaX As Single = x2 - x1
        Dim deltaY As Single = y2 - y1

        ' Calcola l'angolo in radianti e convertilo in gradi
        Dim angoloRadianti As Double = System.Math.Atan2(deltaY, deltaX)
        Dim angoloGradi As Single = CSng(angoloRadianti * (180.0 / System.Math.PI))
        Return -angoloGradi
        ' Ora puoi raddrizzare l'intera pagina prima di ritagliare le 19 celle!
    End Function
    Public Function compila_foglio_giornaliero(body As Body, Fogliodastampare As Struct_foglio) As Boolean
        ' iNTESTAZONE ripeto 2 volte perche potrei avere 2 pagine
        SET_DOCX(body, "<<BAMBINO>>", Fogliodastampare.Bambino)
        SET_DOCX(body, "<<BAMBINO>>", Fogliodastampare.Bambino)
        Dim x = Fogliodastampare.Data.ToString + " (N. " + Fogliodastampare.ID_foglio.ToString + ")"
        SET_DOCX(body, "<<DATA>>", x)
        SET_DOCX(body, "<<DATA>>", x)

        Dim testo = ""

        For j = 0 To Fogliodastampare.VociDiUnFoglio.Count - 1
            Dim codice = Fogliodastampare.VociDiUnFoglio(j).Codice
            If codice = "" Then Continue For
            testo = Fogliodastampare.VociDiUnFoglio(j).Testo
            SET_DOCX(body, "<<TESTO" + (j + 1).ToString + ">>", testo)
        Next

        For j = 0 To Fogliodastampare.CP.Count - 1
            testo = Fogliodastampare.CP(j).TESTO
            If testo = "" Then Continue For
            SET_DOCX(body, "<<CP" + (j + 1).ToString + ">>", testo)
        Next

        For j = 0 To Fogliodastampare.RT.Count - 1
            testo = Fogliodastampare.RT(j).TESTO
            If testo = "" Then Continue For
            SET_DOCX(body, "<<VOCE" + (j + 1).ToString + ">>", testo)
            testo = Fogliodastampare.RT(j).N_voce.ToString
            SET_DOCX(body, "<<N" + (j + 1).ToString + ">>", testo)
        Next


        '--Azzero tutti i campi non riempiti --
        testo = " "
        For j = 1 To 60
            SET_DOCX(body, "<<COD" + (j).ToString + ">>", testo)
            SET_DOCX(body, "<<TESTO" + (j).ToString + ">>", testo)
            SET_DOCX(body, "<<CP" + (j + 1).ToString + ">>", testo)
            SET_DOCX(body, "<<N" + (j + 1).ToString + ">>", testo)
            SET_DOCX(body, "<<VOCE" + (j + 1).ToString + ">>", testo)

        Next

        Return True
    End Function
    Public Function compila_foglio_giornaliero_old(body As Body, nomebambino As String, data As String, id_foglio As Integer) As Boolean
        ' iNTESTAZONE 
        SET_DOCX(body, "<<BAMBINO>>", nomebambino)
        SET_DOCX(body, "<<FOGLIO>>", id_foglio.ToString)
        SET_DOCX(body, "<<DATA>>", data)
        Dim testo = ""
        For j = 0 To Gbl_VociDiUnFoglio.Count - 1
            Dim codice = Gbl_VociDiUnFoglio(j).Codice
            If codice = "" Then Continue For
            testo = Gbl_VociDiUnFoglio(j).Testo
            SET_DOCX(body, "<<TESTO" + (j + 1).ToString + ">>", testo)
        Next

        For j = 0 To Gbl_Voci_CP_DiUnFoglio.Count - 1
            testo = Gbl_Voci_CP_DiUnFoglio(j).TESTO
            If testo = "" Then Continue For
            SET_DOCX(body, "<<CP" + (j + 1).ToString + ">>", testo)
        Next

        '--Azzero tutti i campi non riempiti --
        testo = " "
        For j = 1 To 10
            SET_DOCX(body, "<<COD" + (j).ToString + ">>", testo)
            SET_DOCX(body, "<<TESTO" + (j).ToString + ">>", testo)
            SET_DOCX(body, "<<CP" + (j + 1).ToString + ">>", testo)

        Next

        Return True
    End Function
    Public Sub SET_DOCX(body As Body, codice As String, testo As String)
        ' Sicurezza: verifica che il body e i parametri non siano nulli
        If body Is Nothing OrElse String.IsNullOrEmpty(codice) Then Exit Sub
        If testo Is Nothing Then testo = String.Empty

        ' Recupera tutti i paragrafi presenti nel corpo del documento
        Dim paragrafi As IEnumerable(Of Paragraph) = body.Descendants(Of Paragraph)()

        For Each p As Paragraph In paragrafi
            ' Se il testo del paragrafo contiene il codice da sostituire
            If p.InnerText.Contains(codice) Then

                ' 1. Identifica il primo blocco di formattazione (Run) per usarlo come base
                Dim primoRun As Run = p.GetFirstChild(Of Run)()
                Dim proprietàFormattazione As RunProperties = Nothing

                ' Clona le proprietà di formattazione (Font, Dimensione, Grassetto...) se presenti
                If primoRun IsNot Nothing AndAlso primoRun.RunProperties IsNot Nothing Then
                    proprietàFormattazione = CType(primoRun.RunProperties.CloneNode(True), RunProperties)
                End If

                ' 2. Estrae l'intero testo unito del paragrafo ed esegue la sostituzione
                Dim testoUnito As String = p.InnerText.Replace(codice, testo)

                ' 3. Svuota completamente il paragrafo da tutti i vecchi nodi di testo frammentati
                p.RemoveAllChildren(Of Run)()

                ' 4. Crea un nuovo blocco di testo pulito e unito
                Dim nuovoRun As New Run()

                ' Ripristina la formattazione originale sul nuovo testo
                If proprietàFormattazione IsNot Nothing Then
                    nuovoRun.Append(proprietàFormattazione)
                End If

                ' Assegna il testo finale modificato
                nuovoRun.Append(New Text(testoUnito))

                ' Aggiunge il nuovo blocco al paragrafo
                p.Append(nuovoRun)
            End If
        Next
    End Sub

    Public Function OttieniStampantePredefinita() As String
        Try
            ' Creiamo un oggetto provvisorio per leggere le impostazioni di stampa di Windows
            Dim impostazioni As New PrinterSettings()

            ' Restituisce il nome della stampante che ha la spunta verde su Windows
            Return impostazioni.PrinterName
        Catch ex As Exception
            Return "Errore nella lettura della stampante: " & ex.Message
        End Try
    End Function

    '-------------------------------------------------------------------
    '-------------------------------------------------------------------
    '---- PARTE GRAFICA ------------------------------------------------
    '-------------------------------------------------------------------
    '-------------------------------------------------------------------
    ''' <summary>
    ''' Routine principale: Aggiorna l'intero grafico a barre (Asse X comune e le due serie Y1 e Y2).
    ''' </summary>
    ''' 
    Public Sub AggiornaTuttiIGraficiElcar(pathFileDocx As String, tuttiIDatiInInput As List(Of List(Of ElementoGrafico)))

        ' 1. Apriamo il file Word in modalità Scrittura (True)
        Using doc As WordprocessingDocument = WordprocessingDocument.Open(pathFileDocx, True)

            Dim partePrincipale As MainDocumentPart = doc.MainDocumentPart

            ' 2. Recuperiamo TUTTI i grafici presenti nel documento (mantenendo l'ordine di lettura della tabella)
            Dim elencoGrafici As List(Of ChartPart) = partePrincipale.ChartParts.ToList()

            ' Controllo di sicurezza
            If elencoGrafici.Count = 0 Then
                Throw New Exception("Nessun grafico trovato nel documento Word.")
            End If

            ' Sincronizziamo il ciclo in base al numero di grafici effettivi o ai dati passati (es. 8 grafici)
            Dim numeroGraficiDaAggiornare As Integer = System.Math.Min(elencoGrafici.Count, tuttiIDatiInInput.Count)

            ' 3. Eseguiamo il ciclo su ciascun grafico nella griglia
            For i As Integer = 0 To numeroGraficiDaAggiornare - 1

                Dim parteGraficoCorrente As ChartPart = elencoGrafici(i)
                Dim spazioGrafico As ChartSpace = parteGraficoCorrente.ChartSpace

                ' Individuiamo le serie specifiche del grafico corrente
                Dim tutteLeSerie As List(Of BarChartSeries) = spazioGrafico.Descendants(Of BarChartSeries)().ToList()

                ' Salta l'aggiornamento se il grafico nel template non ha le 2 serie attese
                If tutteLeSerie.Count >= 2 Then

                    ' Estraiamo la lista di dati dedicata unicamente a QUESTO grafico (grafico i-esimo)
                    Dim datiSpecificiGrafico As List(Of ElementoGrafico) = tuttiIDatiInInput(i)
                    Dim numeroElementi As UInt32 = Convert.ToUInt32(datiSpecificiGrafico.Count)

                    ' --- AGGIORNAMENTO DATI (Riutilizza le tue funzioni sicure) ---

                    ' SERIE 1: Aggiorna Asse X globale e i valori della prima barra (Y1)
                    Dim serie1 As BarChartSeries = tutteLeSerie(0)
                    AggiornaEtichetteAsseX_ELCAR(serie1, datiSpecificiGrafico, numeroElementi)
                    AggiornaValoriNumericiY_ELCAR(serie1, datiSpecificiGrafico, numeroElementi, UsaValoreY2:=False)

                    ' SERIE 2: Aggiorna SOLO i valori della seconda barra (Y2) lasciando intatto l'asse X
                    Dim serie2 As BarChartSeries = tutteLeSerie(1)
                    ' Nota: AggiornaEtichetteAsseX(serie2...) NON va chiamata per evitare corruzioni!
                    AggiornaValoriNumericiY_ELCAR(serie2, datiSpecificiGrafico, numeroElementi, UsaValoreY2:=True)

                    ' 4. Salviamo le modifiche strutturali specifiche di questo grafico
                    spazioGrafico.Save(parteGraficoCorrente)
                End If
            Next

            ' 5. Salviamo il documento Word globale una volta completato il ciclo
            partePrincipale.Document.Save()

        End Using
    End Sub
    Public Sub AggiornaGraficoDocumento_ELCAR(pathFileDocx As String, datiInInput As List(Of ElementoGrafico))

        ' 1. Apriamo il file Word in modalità Scrittura (True)
        Using doc As WordprocessingDocument = WordprocessingDocument.Open(pathFileDocx, True)

            ' 2. Entriamo nella parte principale del documento Word
            Dim partePrincipale As MainDocumentPart = doc.MainDocumentPart

            ' 3. Cerchiamo se nel documento esiste almeno un grafico
            Dim parteGrafico As ChartPart = partePrincipale.ChartParts.FirstOrDefault()

            If parteGrafico Is Nothing Then
                Throw New Exception("Nessun grafico trovato all'interno del documento Word fornito.")
            End If

            ' 4. Accediamo all'albero degli elementi XML del grafico
            Dim spazioGrafico As ChartSpace = parteGrafico.ChartSpace

            ' 5. Individuiamo le serie di dati (le colonne/barre) nell'XML.
            Dim tutteLeSerie As List(Of BarChartSeries) = spazioGrafico.Descendants(Of BarChartSeries)().ToList()

            ' Verifichiamo che il grafico abbia effettivamente le 2 serie che ci aspettiamo
            If tutteLeSerie.Count < 2 Then
                Throw New Exception("Il grafico nel template deve contenere almeno 2 serie di dati preimpostate.")
            End If

            ' -----------------------------------------------------------------
            ' FLUSSO DI AGGIORNAMENTO DATI
            ' -----------------------------------------------------------------
            Dim numeroElementi As UInt32 = Convert.ToUInt32(datiInInput.Count)

            ' AGGIORNAMENTO SERIE 1 (Asse X ed i valori Y1)
            Dim serie1 As BarChartSeries = tutteLeSerie(0)
            AggiornaEtichetteAsseX_ELCAR(serie1, datiInInput, numeroElementi)
            AggiornaValoriNumericiY_ELCAR(serie1, datiInInput, numeroElementi, UsaValoreY2:=False)

            ' AGGIORNAMENTO SERIE 2 (Solo i valori Y2, l'asse X è condiviso o speculare)
            Dim serie2 As BarChartSeries = tutteLeSerie(1)
            AggiornaEtichetteAsseX_ELCAR(serie2, datiInInput, numeroElementi) ' Sincronizza l'asse X anche qui per sicurezza
            AggiornaValoriNumericiY_ELCAR(serie2, datiInInput, numeroElementi, UsaValoreY2:=True)

            ' 6. Salviamo le modifiche apportate alla struttura del grafico
            spazioGrafico.Save(parteGrafico)

            ' 7. Salviamo il documento Word nel suo insieme
            partePrincipale.Document.Save()

        End Using
    End Sub
    Public Sub AggiornaGraficoDocumento_totali_giornalieri(pathFileDocx As String, datiInInput As List(Of ElementoGrafico))

        ' 1. Apriamo il file Word in modalità Scrittura (True)
        Using doc As WordprocessingDocument = WordprocessingDocument.Open(pathFileDocx, True)

            Dim partePrincipale As MainDocumentPart = doc.MainDocumentPart
            Dim parteGrafico As ChartPart = partePrincipale.ChartParts.FirstOrDefault()

            If parteGrafico Is Nothing Then
                Throw New Exception("Nessun grafico trovato all'interno del documento Word fornito.")
            End If

            Dim spazioGrafico As ChartSpace = parteGrafico.ChartSpace

            ' Individuiamo le serie di dati nell'XML
            Dim Serie1 As BarChartSeries = spazioGrafico.Descendants(Of BarChartSeries)().FirstOrDefault()
            Dim Serie2 As LineChartSeries = spazioGrafico.Descendants(Of LineChartSeries)().FirstOrDefault()

            If Serie1 Is Nothing OrElse Serie2 Is Nothing Then
                Throw New Exception("Impossibile trovare entrambe le serie (Barre e Linee) nel modello del grafico.")
            End If

            Dim numeroElementiFisso As UInt32 = 55

            ' -----------------------------------------------------------------
            ' AGGIORNAMENTO DELLE CACHE DELLE SERIE 
            ' -----------------------------------------------------------------
            AggiornaEtichetteAsseX_ELCAR(Serie1, datiInInput, numeroElementiFisso)
            AggiornaValoriNumericiY_ELCAR(Serie1, datiInInput, numeroElementiFisso, UsaValoreY2:=False)

            AggiornaEtichetteAsseX_Linechart(Serie2, datiInInput, numeroElementiFisso)
            AggiornaValoriNumericiY_Linechart(Serie2, datiInInput, numeroElementiFisso, UsaValoreY2:=True)

            ' ---------------------------------------------------------------------
            ' GESTIONE DEI VUOTI PER LA LINEA (Corretto posizionamento nel nodo <c:chart>)
            ' ---------------------------------------------------------------------
            Dim nodoChart As DocumentFormat.OpenXml.Drawing.Charts.Chart = spazioGrafico.Descendants(Of DocumentFormat.OpenXml.Drawing.Charts.Chart)().FirstOrDefault()
            If nodoChart IsNot Nothing Then
                Dim dispBlanks As DisplayBlanksAs = nodoChart.GetFirstChild(Of DisplayBlanksAs)()
                If dispBlanks IsNot Nothing Then
                    dispBlanks.Val = DisplayBlanksAsValues.Gap
                Else
                    ' Per evitare corruzioni di schema XML, inseriamo prima di determinati nodi
                    Dim showDataLab = nodoChart.GetFirstChild(Of ShowDataLabelsOverMaximum)()
                    If showDataLab IsNot Nothing Then
                        nodoChart.InsertBefore(New DisplayBlanksAs() With {.Val = DisplayBlanksAsValues.Gap}, showDataLab)
                    Else
                        nodoChart.AppendChild(New DisplayBlanksAs() With {.Val = DisplayBlanksAsValues.Gap})
                    End If
                End If
            End If

            ' ---------------------------------------------------------------------
            ' FORZATURA DELLE FORMULE DI RIFERIMENTO DELLE SERIE (55 colonne)
            ' ---------------------------------------------------------------------
            Dim f_Serie1_X As Formula = Serie1.Descendants(Of CategoryAxisData)().SelectMany(Function(x) x.Descendants(Of Formula)()).FirstOrDefault()
            If f_Serie1_X IsNot Nothing Then f_Serie1_X.Text = "Sheet1!$A$2:$A$56"

            Dim f_Serie1_Y As Formula = Serie1.Descendants(Of Values)().SelectMany(Function(v) v.Descendants(Of Formula)()).FirstOrDefault()
            If f_Serie1_Y IsNot Nothing Then f_Serie1_Y.Text = "Sheet1!$B$2:$B$56"

            Dim f_Serie2_X As Formula = Serie2.Descendants(Of CategoryAxisData)().SelectMany(Function(x) x.Descendants(Of Formula)()).FirstOrDefault()
            If f_Serie2_X IsNot Nothing Then f_Serie2_X.Text = "Sheet1!$A$2:$A$56"

            Dim f_Serie2_Y As Formula = Serie2.Descendants(Of Values)().SelectMany(Function(v) v.Descendants(Of Formula)()).FirstOrDefault()
            If f_Serie2_Y IsNot Nothing Then f_Serie2_Y.Text = "Sheet1!$C$2:$C$56"

            ' ---------------------------------------------------------------------
            ' AGGIORNAMENTO DEL FOGLIO EXCEL INCORPORATO
            ' ---------------------------------------------------------------------
            If parteGrafico.EmbeddedPackagePart IsNot Nothing Then
                AggiornaFoglioExcelIncorporato(parteGrafico.EmbeddedPackagePart, datiInInput)
            End If

            ' ---------------------------------------------------------------------
            ' FIX RE-RENDERING ASSE X (Posizionamento ordinato contro gli errori XSD)
            ' ---------------------------------------------------------------------
            Dim asseX As CategoryAxis = spazioGrafico.Descendants(Of CategoryAxis)().FirstOrDefault()
            If asseX IsNot Nothing Then
                If asseX.Delete IsNot Nothing Then asseX.Delete.Val = False

                ' Rimuove vecchi limiti ereditati dal modello
                Dim scaling As Scaling = asseX.GetFirstChild(Of Scaling)()
                If scaling IsNot Nothing Then
                    Dim vecchioMax = scaling.GetFirstChild(Of MaxAxisValue)()
                    If vecchioMax IsNot Nothing Then scaling.RemoveChild(vecchioMax)
                End If

                ' GESTIONE SICURA DEL TAG NoMultiLevelLabels
                Dim noMultiLvl As NoMultiLevelLabels = asseX.GetFirstChild(Of NoMultiLevelLabels)()
                If noMultiLvl IsNot Nothing Then
                    noMultiLvl.Val = True
                Else
                    ' L'XML esige che NoMultiLevelLabels stia PRIMA di CatAxExtensionList se presente
                    Dim extList = asseX.GetFirstChild(Of CatAxExtensionList)()
                    If extList IsNot Nothing Then
                        asseX.InsertBefore(New NoMultiLevelLabels() With {.Val = True}, extList)
                    Else
                        asseX.AppendChild(New NoMultiLevelLabels() With {.Val = True})
                    End If
                End If
            End If

            ' Salva le modifiche strutturali finali
            spazioGrafico.Save(parteGrafico)
            partePrincipale.Document.Save()

        End Using
    End Sub

    Public Sub AggiornaGraficoDocumento_totali_giornalieri_OLD(pathFileDocx As String, datiInInput As List(Of ElementoGrafico))

        ' 1. Apriamo il file Word in modalità Scrittura (True)
        Using doc As WordprocessingDocument = WordprocessingDocument.Open(pathFileDocx, True)

            ' 2. Entriamo nella parte principale del documento Word
            Dim partePrincipale As MainDocumentPart = doc.MainDocumentPart

            ' 3. Cerchiamo se nel documento esiste almeno un grafico
            Dim parteGrafico As ChartPart = partePrincipale.ChartParts.FirstOrDefault()

            If parteGrafico Is Nothing Then
                Throw New Exception("Nessun grafico trovato all'interno del documento Word fornito.")
            End If

            ' 4. Accediamo all'albero degli elementi XML del grafico
            Dim spazioGrafico As ChartSpace = parteGrafico.ChartSpace

            ' 5. Individuiamo le serie di dati (le colonne/barre e le linee) nell'XML.
            Dim Serie1 As BarChartSeries = spazioGrafico.Descendants(Of BarChartSeries)().FirstOrDefault()
            Dim Serie2 As LineChartSeries = spazioGrafico.Descendants(Of LineChartSeries)().FirstOrDefault()

            If Serie1 Is Nothing OrElse Serie2 Is Nothing Then
                Throw New Exception("Impossibile trovare entrambe le serie (Barre e Linee) nel modello del grafico.")
            End If

            ' -----------------------------------------------------------------
            ' CONFIGURAZIONE BLOCCO RIGIDO A 55 ELEMENTI
            ' -----------------------------------------------------------------
            Dim numeroElementiFisso As UInt32 = 55

            ' AGGIORNAMENTO SERIE 1 (Barre - Valori Y1)
            AggiornaEtichetteAsseX_ELCAR(Serie1, datiInInput, numeroElementiFisso)
            AggiornaValoriNumericiY_ELCAR(Serie1, datiInInput, numeroElementiFisso, UsaValoreY2:=False)

            ' AGGIORNAMENTO SERIE 2 (Spezzata - Valori Y2)
            AggiornaEtichetteAsseX_Linechart(Serie2, datiInInput, numeroElementiFisso)
            AggiornaValoriNumericiY_Linechart(Serie2, datiInInput, numeroElementiFisso, UsaValoreY2:=True)

            ' ---------------------------------------------------------------------
            ' NUOVO E RISOLUTIVO: FORZATURA DELLE FORMULE DI RIFERIMENTO DELLE SERIE
            ' Riga 56 = 55 elementi di dati + 1 riga di intestazione Excel
            ' ---------------------------------------------------------------------

            ' --- Sblocco Formule Serie 1 (Barre) ---
            Dim f_Serie1_X As Formula = Serie1.Descendants(Of CategoryAxisData)().SelectMany(Function(x) x.Descendants(Of Formula)()).FirstOrDefault()
            If f_Serie1_X IsNot Nothing Then f_Serie1_X.Text = "Sheet1!$A$2:$A$56"

            Dim f_Serie1_Y As Formula = Serie1.Descendants(Of Values)().SelectMany(Function(v) v.Descendants(Of Formula)()).FirstOrDefault()
            If f_Serie1_Y IsNot Nothing Then f_Serie1_Y.Text = "Sheet1!$B$2:$B$56"

            ' --- Sblocco Formule Serie 2 (Linea) ---
            Dim f_Serie2_X As Formula = Serie2.Descendants(Of CategoryAxisData)().SelectMany(Function(x) x.Descendants(Of Formula)()).FirstOrDefault()
            If f_Serie2_X IsNot Nothing Then f_Serie2_X.Text = "Sheet1!$A$2:$A$56"

            Dim f_Serie2_Y As Formula = Serie2.Descendants(Of Values)().SelectMany(Function(v) v.Descendants(Of Formula)()).FirstOrDefault()
            If f_Serie2_Y IsNot Nothing Then f_Serie2_Y.Text = "Sheet1!$C$2:$C$56"


            ' ---------------------------------------------------------------------
            ' AGGIORNAMENTO DEL FOGLIO EXCEL INCORPORATO
            ' ---------------------------------------------------------------------
            ' Assicurati che 'AggiornaFoglioExcelIncorporato' scriva fisicamente 55 righe nel file Excel
            If parteGrafico.EmbeddedPackagePart IsNot Nothing Then
                AggiornaFoglioExcelIncorporato(parteGrafico.EmbeddedPackagePart, datiInInput)
            End If

            ' ---------------------------------------------------------------------
            ' RE-SET DEL CONTATORE DI VISUALIZZAZIONE SULL'ASSE X (CategoryAxis)
            ' ---------------------------------------------------------------------
            Dim asseX As CategoryAxis = spazioGrafico.Descendants(Of CategoryAxis)().FirstOrDefault()
            If asseX IsNot Nothing Then
                ' Se nel modello c'erano filtri o vecchi limiti di scaling sull'asse, li azzeriamo
                If asseX.Delete IsNot Nothing Then asseX.Delete.Val = False
                Dim scaling As Scaling = asseX.GetFirstChild(Of Scaling)()
                If scaling IsNot Nothing Then
                    Dim vecchioMax = scaling.GetFirstChild(Of MaxAxisValue)()
                    If vecchioMax IsNot Nothing Then scaling.RemoveChild(vecchioMax)
                End If
            End If

            ' 6. Salviamo le modifiche apportate alla struttura del grafico
            spazioGrafico.Save(parteGrafico)

            ' 7. Salviamo il documento Word nel suo insieme
            partePrincipale.Document.Save()

        End Using
    End Sub

    Public Sub ForzaGraficoAColonneFisse(pathFileDocx As String, Optional maxcol As Integer = 55)
        ' 1. Apriamo il file Word in modalità Scrittura (True)
        Using doc As WordprocessingDocument = WordprocessingDocument.Open(pathFileDocx, True)

            Dim partePrincipale As MainDocumentPart = doc.MainDocumentPart
            Dim parteGrafico As ChartPart = partePrincipale.ChartParts.FirstOrDefault()

            If parteGrafico Is Nothing Then
                Throw New Exception("Nessun grafico trovato all'interno del documento Word.")
            End If

            ' 2. Accediamo alla radice XML del grafico
            Dim spazioGrafico As ChartSpace = parteGrafico.ChartSpace
            Dim totaleElementi As UInt32 = maxcol

            ' ---------------------------------------------------------------------
            ' 3. CONFIGURAZIONE DI TUTTE LE STRING CACHE (Assi X del Grafico)
            ' MODIFICATO: Preserva le date già esistenti inserite dalla tua funzione
            ' ---------------------------------------------------------------------
            Dim tutteLeStringCache = spazioGrafico.Descendants(Of StringCache)().ToList()

            For Each strCache In tutteLeStringCache
                ' Memorizziamo temporaneamente le etichette reali già presenti (es. "17/10", "23/10")
                ' mappandole sul loro indice originario
                Dim dizionarioEtichetteEsistenti As New Dictionary(Of UInt32, String)()
                For Each vecchioPunto In strCache.Descendants(Of StringPoint)()
                    Dim valNodo = vecchioPunto.Descendants(Of NumericValue)().FirstOrDefault()
                    If valNodo IsNot Nothing AndAlso Not String.IsNullOrEmpty(valNodo.Text) Then
                        dizionarioEtichetteEsistenti(vecchioPunto.Index.Value) = valNodo.Text
                    End If
                Next

                ' Impostiamo il contatore fisso a 55
                strCache.PointCount = New PointCount() With {.Val = totaleElementi}
                strCache.RemoveAllChildren(Of StringPoint)()

                ' Rigeneriamo i 55 slot
                For i As Integer = 0 To Convert.ToInt32(totaleElementi) - 1
                    Dim indexU32 As UInt32 = CType(i, UInt32Value)
                    Dim testoEtichetta As String = ""

                    ' SE esisteva già un'etichetta per questo indice, ripristinala!
                    If dizionarioEtichetteEsistenti.ContainsKey(indexU32) Then
                        testoEtichetta = dizionarioEtichetteEsistenti(indexU32)
                    End If

                    Dim strPoint As New StringPoint() With {.Index = indexU32}
                    strPoint.AppendChild(New NumericValue() With {.Text = testoEtichetta})
                    strCache.AppendChild(strPoint)
                Next
            Next

            ' ---------------------------------------------------------------------
            ' 4. CONFIGURAZIONE DI TUTTE LE NUMBERING CACHE (Valori Y delle Barre/Linee)
            ' MODIFICATO: Preserva i valori numerici reali già calcolati
            ' ---------------------------------------------------------------------
            Dim tutteLeNumberCache = spazioGrafico.Descendants(Of NumberingCache)().ToList()

            For Each numCache In tutteLeNumberCache
                ' Memorizziamo i valori numerici reali già presenti per non azzerare le barre
                Dim dizionarioValoriEsistenti As New Dictionary(Of UInt32, String)()
                For Each vecchioPunto In numCache.Descendants(Of NumericPoint)()
                    Dim valNodo = vecchioPunto.Descendants(Of NumericValue)().FirstOrDefault()
                    If valNodo IsNot Nothing AndAlso Not String.IsNullOrEmpty(valNodo.Text) Then
                        dizionarioValoriEsistenti(vecchioPunto.Index.Value) = valNodo.Text
                    End If
                Next

                numCache.PointCount = New PointCount() With {.Val = totaleElementi}
                numCache.RemoveAllChildren(Of NumericPoint)()

                ' Rigeneriamo i 55 slot dei valori
                For i As Integer = 0 To Convert.ToInt32(totaleElementi) - 1
                    Dim indexU32 As UInt32 = CType(i, UInt32Value)
                    Dim testoValore As String = ""

                    ' SE esisteva un valore inserito in precedenza, mantienilo
                    If dizionarioValoriEsistenti.ContainsKey(indexU32) Then
                        testoValore = dizionarioValoriEsistenti(indexU32)
                    End If

                    Dim numPoint As New NumericPoint() With {.Index = indexU32}
                    numPoint.AppendChild(New NumericValue() With {.Text = testoValore})
                    numCache.AppendChild(numPoint)
                Next
            Next

            ' ---------------------------------------------------------------------
            ' 5. FORZATURA DELLE FORMULE EXCEL (Invariato)
            ' ---------------------------------------------------------------------
            Dim tutteLeFormule = spazioGrafico.Descendants(Of Formula)().ToList()
            For Each formula In tutteLeFormule
                If formula.Text IsNot Nothing Then
                    Dim testoAttuale As String = formula.Text
                    If testoAttuale.Contains("$") Then
                        Dim parti = testoAttuale.Split(":"c)
                        If parti.Length = 2 AndAlso parti(1).Contains("$") Then
                            Dim indiceUltimoDollaro As Integer = parti(1).LastIndexOf("$"c)
                            Dim colonna As String = parti(1).Substring(0, indiceUltimoDollaro + 1)
                            formula.Text = $"{parti(0)}:{colonna}56"
                        End If
                    End If
                End If
            Next

            ' ---------------------------------------------------------------------
            ' 6. SBLOCCO DELL'ASSE X (Invariato)
            ' ---------------------------------------------------------------------
            Dim asseX As CategoryAxis = spazioGrafico.Descendants(Of CategoryAxis)().FirstOrDefault()
            If asseX IsNot Nothing Then
                If asseX.Delete IsNot Nothing Then asseX.Delete.Val = False

                Dim scaling As Scaling = asseX.GetFirstChild(Of Scaling)()
                If scaling IsNot Nothing Then
                    Dim vecchioMax = scaling.GetFirstChild(Of MaxAxisValue)()
                    If vecchioMax IsNot Nothing Then scaling.RemoveChild(vecchioMax)
                End If
            End If

            ' 7. Salviamo le modifiche strutturali nel file Word
            spazioGrafico.Save(parteGrafico)
            partePrincipale.Document.Save()
        End Using
    End Sub
    Private Sub ConfiguraNodiCacheGrafico(serie As OpenXmlCompositeElement, totaleElementi As UInt32)
        ' Configura la Cache dell'Asse X (StringCache)
        Dim stringCache As StringCache = serie.Descendants(Of StringCache)().FirstOrDefault()
        If stringCache IsNot Nothing Then
            stringCache.PointCount = New PointCount() With {.Val = totaleElementi}
            stringCache.RemoveAllChildren(Of StringPoint)()

            ' Genera 55 slot vuoti per l'Asse X
            For i As Integer = 0 To Convert.ToInt32(totaleElementi) - 1
                Dim strPoint As New StringPoint() With {.Index = CType(i, UInt32Value)}
                strPoint.AppendChild(New C.NumericValue() With {.Text = ""})
                stringCache.AppendChild(strPoint)
            Next
        End If

        ' Configura la Cache dei Valori Y (NumberCache)
        Dim numberCache As NumberingCache = serie.Descendants(Of NumberingCache)().FirstOrDefault()
        If numberCache IsNot Nothing Then
            numberCache.PointCount = New PointCount() With {.Val = totaleElementi}
            numberCache.RemoveAllChildren(Of NumericPoint)()

            ' Genera 55 slot vuoti per i Valori
            For i As Integer = 0 To Convert.ToInt32(totaleElementi) - 1
                Dim numPoint As New NumericPoint() With {.Index = CType(i, UInt32Value)}
                numPoint.AppendChild(New C.NumericValue() With {.Text = ""})
                numberCache.AppendChild(numPoint)
            Next
        End If
    End Sub
    Private Sub ForzaFormuleExcelSerie(serie As OpenXmlCompositeElement, rigaFinale As Integer, colonnaLettera As String)
        ' Aggiorna la formula dell'Asse X (Colonna A, es. Sheet1!$A$2:$A$56)
        Dim formulaAsseX As Formula = serie.Descendants(Of CategoryAxisData)().SelectMany(Function(x) x.Descendants(Of Formula)()).FirstOrDefault()
        If formulaAsseX IsNot Nothing Then
            formulaAsseX.Text = $"Sheet1!$A$2:$A${rigaFinale}"
        End If

        ' Aggiorna la formula dei Valori Y (Colonna B o C, es. Sheet1!$B$2:$B$56)
        Dim formulaValori As Formula = serie.Descendants(Of Values)().SelectMany(Function(v) v.Descendants(Of Formula)()).FirstOrDefault()
        If formulaValori IsNot Nothing Then
            formulaValori.Text = $"Sheet1!${colonnaLettera}$2:${colonnaLettera}${rigaFinale}"
        End If
    End Sub
    Private Sub AggiornaFoglioExcelIncorporato(excelPart As EmbeddedPackagePart, dati As List(Of ElementoGrafico))
        ' Apriamo il documento Spreadsheet (Excel) in modalità ReadWrite
        Using excelDoc As DocumentFormat.OpenXml.Packaging.SpreadsheetDocument =
        DocumentFormat.OpenXml.Packaging.SpreadsheetDocument.Open(excelPart.GetStream(System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite), True)

            Dim workbookPart As DocumentFormat.OpenXml.Packaging.WorkbookPart = excelDoc.WorkbookPart
            Dim worksheetPart As DocumentFormat.OpenXml.Packaging.WorksheetPart = workbookPart.WorksheetParts.FirstOrDefault()

            If worksheetPart IsNot Nothing Then
                Dim sheetData As DocumentFormat.OpenXml.Spreadsheet.SheetData = worksheetPart.Worksheet.GetFirstChild(Of DocumentFormat.OpenXml.Spreadsheet.SheetData)()

                ' Rimuoviamo le vecchie righe di dati (manteniamo solo la riga 1 che contiene le intestazioni)
                Dim righe = sheetData.Elements(Of DocumentFormat.OpenXml.Spreadsheet.Row)().ToList()
                For i As Integer = righe.Count - 1 To 1 Step -1
                    sheetData.RemoveChild(righe(i))
                Next

                ' Scriviamo i nuovi record riga per riga a partire dalla riga 2
                For i As Integer = 0 To dati.Count - 1

                    Dim rigaIndex As UInt32 = Convert.ToUInt32(i + 2)
                    Dim nuovaRiga As New DocumentFormat.OpenXml.Spreadsheet.Row() With {.RowIndex = rigaIndex}

                    ' Gestiamo il valore X in modo sicuro (se è Nothing o vuoto, mette una stringa vuota)
                    Dim valoreX As String = If(dati(i).X IsNot Nothing, dati(i).X.ToString(), "")

                    ' Colonna A: Valore X (Categoria)
                    Dim cellaA As New DocumentFormat.OpenXml.Spreadsheet.Cell() With {.CellReference = "A" & rigaIndex, .DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String}
                    cellaA.AppendChild(New DocumentFormat.OpenXml.Spreadsheet.CellValue(valoreX))
                    nuovaRiga.AppendChild(cellaA)

                    ' Colonna B: Valore Y1 (Barre)
                    Dim cellaB As New DocumentFormat.OpenXml.Spreadsheet.Cell() With {.CellReference = "B" & rigaIndex, .DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.Number}
                    cellaB.AppendChild(New DocumentFormat.OpenXml.Spreadsheet.CellValue(dati(i).Y1.ToString(System.Globalization.CultureInfo.InvariantCulture)))
                    nuovaRiga.AppendChild(cellaB)

                    ' Colonna C: Valore Y2 (Spezzata)
                    Dim cellaC As New DocumentFormat.OpenXml.Spreadsheet.Cell() With {.CellReference = "C" & rigaIndex, .DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.Number}
                    cellaC.AppendChild(New DocumentFormat.OpenXml.Spreadsheet.CellValue(dati(i).Y2.ToString(System.Globalization.CultureInfo.InvariantCulture)))
                    nuovaRiga.AppendChild(cellaC)

                    sheetData.AppendChild(nuovaRiga)
                Next

                ' Salviamo il foglio di lavoro Excel
                worksheetPart.Worksheet.Save()
            End If
        End Using
    End Sub

    Private Sub AggiornaEtichetteAsseX_ELCAR(serie As BarChartSeries, dati As List(Of ElementoGrafico), conteggio As UInt32)
        ' 1. Trova il contenitore dell'asse X senza distruggerlo
        Dim datiAsseX As CategoryAxisData = serie.Descendants(Of CategoryAxisData)().FirstOrDefault()

        If datiAsseX IsNot Nothing Then
            ' 2. Trova lo StringLiteral esistente (dove risiedono i testi delle categorie)
            Dim stringLit As StringLiteral = datiAsseX.Descendants(Of StringLiteral)().FirstOrDefault()

            If stringLit IsNot Nothing Then
                ' Pulisce SOLO i vecchi punti, lasciando intatti i nodi padre di Word
                stringLit.RemoveAllChildren()

                ' 3. Imposta il nuovo conteggio dei punti
                stringLit.AppendChild(New PointCount() With {.Val = conteggio})

                ' 4. Riscrive i punti dell'asse X
                For idx As Integer = 0 To dati.Count - 1
                    Dim puntoTesto As New StringPoint() With {.Index = Convert.ToUInt32(idx)}
                    Dim valoreTestuale As New NumericValue() With {.Text = dati(idx).X}

                    puntoTesto.AppendChild(valoreTestuale)
                    stringLit.AppendChild(puntoTesto)
                Next
            End If
        End If
    End Sub

    Private Sub AggiornaEtichetteAsseX_Linechart(serie As LineChartSeries, dati As List(Of ElementoGrafico), conteggio As UInt32)
        ' 1. Trova il contenitore dell'asse X senza distruggerlo
        Dim datiAsseX As CategoryAxisData = serie.Descendants(Of CategoryAxisData)().FirstOrDefault()

        If datiAsseX IsNot Nothing Then
            ' 2. Trova lo StringLiteral esistente (dove risiedono i testi delle categorie)
            Dim stringLit As StringLiteral = datiAsseX.Descendants(Of StringLiteral)().FirstOrDefault()

            If stringLit IsNot Nothing Then
                ' Pulisce SOLO i vecchi punti, lasciando intatti i nodi padre di Word
                stringLit.RemoveAllChildren()

                ' 3. Imposta il nuovo conteggio dei punti
                stringLit.AppendChild(New PointCount() With {.Val = conteggio})

                ' 4. Riscrive i punti dell'asse X
                For idx As Integer = 0 To dati.Count - 1
                    Dim puntoTesto As New StringPoint() With {.Index = Convert.ToUInt32(idx)}
                    Dim valoreTestuale As New NumericValue() With {.Text = dati(idx).X}

                    puntoTesto.AppendChild(valoreTestuale)
                    stringLit.AppendChild(puntoTesto)
                Next
            End If
        End If
    End Sub

    Private Sub AggiornaValoriNumericiY_Linechart(serie As LineChartSeries, dati As List(Of ElementoGrafico), conteggio As UInt32, UsaValoreY2 As Boolean)
        ' 1. Trova il contenitore dei valori Y senza distruggerlo
        Dim contenitoreValori As Values = serie.Descendants(Of Values)().FirstOrDefault()

        If contenitoreValori IsNot Nothing Then
            ' 2. Trova la NumberingCache esistente
            Dim cacheNumeri As NumberingCache = contenitoreValori.Descendants(Of NumberingCache)().FirstOrDefault()

            If cacheNumeri IsNot Nothing Then
                ' Pulisce SOLO i vecchi numeri, lasciando intatta la formattazione nativa di Word
                cacheNumeri.RemoveAllChildren()

                ' 3. Ripristina il codice di formattazione e il conteggio
                cacheNumeri.AppendChild(New FormatCode() With {.Text = "General"})
                cacheNumeri.AppendChild(New PointCount() With {.Val = conteggio})

                ' 4. Inserisce i nuovi valori Y1 o Y2
                For idx As Integer = 0 To dati.Count - 1
                    Dim numeroDaInserire As Integer = If(UsaValoreY2, dati(idx).Y2, dati(idx).Y1)

                    Dim puntoNumerico As New NumericPoint() With {.Index = Convert.ToUInt32(idx)}
                    Dim valoreNumerico As New NumericValue() With {
                        .Text = numeroDaInserire.ToString(System.Globalization.CultureInfo.InvariantCulture)
                    }

                    puntoNumerico.AppendChild(valoreNumerico)
                    cacheNumeri.AppendChild(puntoNumerico)
                Next
            End If
        End If
    End Sub

    Private Sub AggiornaValoriNumericiY_ELCAR(serie As BarChartSeries, dati As List(Of ElementoGrafico), conteggio As UInt32, UsaValoreY2 As Boolean)
        ' 1. Trova il contenitore dei valori Y senza distruggerlo
        Dim contenitoreValori As Values = serie.Descendants(Of Values)().FirstOrDefault()

        If contenitoreValori IsNot Nothing Then
            ' 2. Trova la NumberingCache esistente
            Dim cacheNumeri As NumberingCache = contenitoreValori.Descendants(Of NumberingCache)().FirstOrDefault()

            If cacheNumeri IsNot Nothing Then
                ' Pulisce SOLO i vecchi numeri, lasciando intatta la formattazione nativa di Word
                cacheNumeri.RemoveAllChildren()

                ' 3. Ripristina il codice di formattazione e il conteggio
                cacheNumeri.AppendChild(New FormatCode() With {.Text = "General"})
                cacheNumeri.AppendChild(New PointCount() With {.Val = conteggio})

                ' 4. Inserisce i nuovi valori Y1 o Y2
                For idx As Integer = 0 To dati.Count - 1
                    Dim numeroDaInserire As Integer = If(UsaValoreY2, dati(idx).Y2, dati(idx).Y1)

                    Dim puntoNumerico As New NumericPoint() With {.Index = Convert.ToUInt32(idx)}
                    Dim valoreNumerico As New NumericValue() With {
                        .Text = numeroDaInserire.ToString(System.Globalization.CultureInfo.InvariantCulture)
                    }

                    puntoNumerico.AppendChild(valoreNumerico)
                    cacheNumeri.AppendChild(puntoNumerico)
                Next
            End If
        End If
    End Sub
    Sub ImpostaLimitiAsseDataConOpenXml(pathDocumento As String, dataInizio As DateTime, dataFine As DateTime)
        ' imposta limiti asse x '
        ' 1. Apri il documento Word in modalità lettura/scrittura
        Using doc As WordprocessingDocument = WordprocessingDocument.Open(pathDocumento, True)

            ' 2. Accedi alla MainDocumentPart
            Dim mainPart As MainDocumentPart = doc.MainDocumentPart

            ' 3. Prendi la prima ChartPart presente nel documento
            ' Se hai più grafici, dovrai ciclare o identificare quella corretta tramite ID
            Dim chartPart As ChartPart = mainPart.ChartParts.FirstOrDefault()

            If chartPart IsNot Nothing Then
                ' Accedi allo spazio del grafico (ChartSpace)
                Dim chartSpace As ChartSpace = chartPart.ChartSpace

                ' 4. Trova l'asse delle date (DateAxis -> <c:dateAx>) nel PlotArea
                Dim dateAxis As DateAxis = chartSpace.Descendants(Of DateAxis)().FirstOrDefault()

                If dateAxis IsNot Nothing Then
                    ' 5. Definisci i limiti temporali (Intervallo di 55 giorni)
                    'Dim dataInizio As New DateTime(2026, 11, 1) ' Data di partenza desiderata
                    'Dim dataFine As DateTime = dataInizio.AddDays(54)     ' Fine intervallo (55 giorni totali inclusa la prima)

                    ' Converti le date nel formato seriale Double (OADate) e poi in Stringa
                    Dim stringaMin As String = dataInizio.ToOADate().ToString(System.Globalization.CultureInfo.InvariantCulture)
                    Dim stringaMax As String = dataFine.ToOADate().ToString(System.Globalization.CultureInfo.InvariantCulture)

                    ' 6. Accedi o crea l'elemento di Scaling dell'asse
                    Dim scaling As Scaling = dateAxis.GetFirstChild(Of Scaling)()
                    If scaling Is Nothing Then
                        scaling = New Scaling()
                        dateAxis.AppendChild(scaling)
                    End If

                    ' 7. Rimuovi eventuali vecchi limiti per evitare duplicati nell'XML
                    Dim vecchioMin = scaling.GetFirstChild(Of MinAxisValue)()
                    If vecchioMin IsNot Nothing Then scaling.RemoveChild(vecchioMin)

                    Dim vecchioMax = scaling.GetFirstChild(Of MaxAxisValue)()
                    If vecchioMax IsNot Nothing Then scaling.RemoveChild(vecchioMax)

                    ' 8. Assegna i nuovi limiti fissi (Disattiva l'autoscala di Word)
                    scaling.AppendChild(New MinAxisValue() With {.Val = stringaMin})
                    scaling.AppendChild(New MaxAxisValue() With {.Val = stringaMax})

                    ' 9. Opzionale: Configura le unità di misura principali (es. un'etichetta ogni 5 giorni)
                    ' Rimuovi eventuali impostazioni precedenti se presenti
                    Dim vecchiaUnita = dateAxis.GetFirstChild(Of MajorUnit)()
                    If vecchiaUnita IsNot Nothing Then dateAxis.RemoveChild(vecchiaUnita)

                    Dim vecchioTipoUnita = dateAxis.GetFirstChild(Of MajorTimeUnit)()
                    If vecchioTipoUnita IsNot Nothing Then dateAxis.RemoveChild(vecchioTipoUnita)

                    ' Aggiungi intervallo numerico (5) e l'unità di misura in Giorni (Days)
                    dateAxis.AppendChild(New MajorUnit() With {.Val = 5.0})
                    dateAxis.AppendChild(New MajorTimeUnit() With {.Val = TimeUnitValues.Days})

                    ' 10. Salva le modifiche apportate alla struttura XML del grafico
                    chartSpace.Save()

                    MsgBox("Struttura XML del grafico aggiornata con successo! Limiti asse X bloccati a 55 giorni.", MsgBoxStyle.Information)
                Else
                    MsgBox("Nessun asse di tipo 'Data' (DateAxis) trovato nel grafico.", MsgBoxStyle.Exclamation)
                End If
            Else
                MsgBox("Nessun grafico trovato nel documento Word.", MsgBoxStyle.Critical)
            End If
        End Using
    End Sub



    Public Sub StampaListaSuStampanteScelta(elencoFileDaStampare As List(Of String), nomeStampanteTarget As String)
        Dim m = "", cnt_ok = 0, cnt_err = 0
        For Each percorsoFile In elencoFileDaStampare
            If IO.File.Exists(percorsoFile) Then
                Try
                    Dim infoProcesso As New ProcessStartInfo()

                    ' Chiamiamo l'eseguibile di LibreOffice (soffice.exe)
                    infoProcesso.FileName = Trova_percorso_Soffice()
                    If infoProcesso.FileName = "" Then
                        m = "Processo di stampa (Soffuce.exe) NON Trovato"
                        Call APPEND_LOG(m, ini.File_Log, True, "E")
                        Call MsgBox("Soffice non trovato")
                        Exit Sub
                    End If

                    ' Argomenti: --headless (invisibile), --pt (invia alla stampante indicata)
                    infoProcesso.Arguments = $"--headless --pt ""{nomeStampanteTarget}"" ""{percorsoFile}"""
                    infoProcesso.CreateNoWindow = True
                    infoProcesso.WindowStyle = ProcessWindowStyle.Hidden
                    infoProcesso.UseShellExecute = False ' Obbligatorio impostare su False per passare gli argomenti a soffice.exe

                    ' Avvia il processo in background
                    Using proc As Process = Process.Start(infoProcesso)
                        ' Attendiamo fino a 15 secondi che il file sia inviato allo spooler 
                        ' prima di passare al documento successivo nella lista
                        proc.WaitForExit(15000)
                    End Using
                    cnt_ok = cnt_ok + 1
                Catch ex As Exception
                    m = "Errore di stampa del foglio: " + IO.Path.GetFileName(percorsoFile)
                    Call APPEND_LOG(m, ini.File_Log, True, "E")
                    Call MsgBox($"Errore nell'invio alla stampante del file {IO.Path.GetFileName(percorsoFile)}: {ex.Message}", MsgBoxStyle.Exclamation)
                    cnt_err = cnt_err + 1
                End Try
            End If
        Next
        MsgBox("Tutti i documenti della lista sono stati inviati alla stampante selezionata!", MsgBoxStyle.Information)
        If cnt_err = 0 Then
            m = "Stampati correttamente " + cnt_ok.ToString + "Fogli"
            Call APPEND_LOG(m, ini.File_Log, True, "1")
        Else
            m = "Stampati correttamente " + cnt_ok.ToString + "Fogli su " + elencoFileDaStampare.Count.ToString
            Call APPEND_LOG(m, ini.File_Log, True, "W")
        End If
    End Sub
    Function Trova_percorso_Soffice() As String
        ' 1. TROVA IL PERCORSO REALE DI LIBREOFFICE SUL DISCO FISSO
        Dim percorsoSoffice As String = ""

        ' Controlliamo i percorsi di installazione standard di Windows
        Dim percorsiPossibili As String() = {
            "C:\Program Files\LibreOffice\program\soffice.exe",
            "C:\Program Files (x86)\LibreOffice\program\soffice.exe"
        }

        For Each path In percorsiPossibili
            If IO.File.Exists(path) Then
                percorsoSoffice = path
                Return percorsoSoffice
                Exit Function
            End If
        Next

        ' SE NON TROVA LIBREOFFICE: blocchiamo l'esecuzione con un messaggio chiaro
        If String.IsNullOrEmpty(percorsoSoffice) Then
            Dim msg As String = "Impossibile avviare la stampa invisibile!" & vbCrLf &
                                "LibreOffice non è stato trovato nei percorsi standard di installazione." & vbCrLf &
                                "Verifica che LibreOffice sia installato correttamente sul computer."
            MsgBox(msg, MsgBoxStyle.Critical, "Errore di Sistema")
            Return ""
        End If
    End Function
End Module
