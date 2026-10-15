Imports DocumentFormat.OpenXml
Imports DocumentFormat.OpenXml.Bibliography
Imports DocumentFormat.OpenXml.Drawing.Charts   ' Contiene le definizioni dei grafici (ChartSpace, NumberValues, ecc.)
Imports DocumentFormat.OpenXml.Packaging        ' Gestisce i pacchetti del file (ChartPart, MainDocumentPart)
Imports DocumentFormat.OpenXml.Wordprocessing
Module Modulo_word
    Public Structure ElementoGrafico   ' Server per i grafici su word 
        Public X As String   ' L'etichetta sull'asse delle X (es. "Gennaio" o "Cat1")
        Public Y1 As Integer ' Il valore della prima barra (Serie 1)
        Public Y2 As Integer ' Il valore della seconda barra (Serie 2)
    End Structure
    Public Gbl_elementoGrafico() As ElementoGrafico

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

    '-------------------------------------------------------------------
    '-------------------------------------------------------------------
    '---- PARTE GRAFICA ------------------------------------------------
    '-------------------------------------------------------------------
    '-------------------------------------------------------------------
    ''' <summary>
    ''' Routine principale: Aggiorna l'intero grafico a barre (Asse X comune e le due serie Y1 e Y2).
    ''' </summary>
    ''' 
    Public Sub AggiornaTuttiIGraficiNellaGriglia(pathFileDocx As String, tuttiIDatiInInput As List(Of List(Of ElementoGrafico)))

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
                    AggiornaEtichetteAsseX(serie1, datiSpecificiGrafico, numeroElementi)
                    AggiornaValoriNumericiY(serie1, datiSpecificiGrafico, numeroElementi, UsaValoreY2:=False)

                    ' SERIE 2: Aggiorna SOLO i valori della seconda barra (Y2) lasciando intatto l'asse X
                    Dim serie2 As BarChartSeries = tutteLeSerie(1)
                    ' Nota: AggiornaEtichetteAsseX(serie2...) NON va chiamata per evitare corruzioni!
                    AggiornaValoriNumericiY(serie2, datiSpecificiGrafico, numeroElementi, UsaValoreY2:=True)

                    ' 4. Salviamo le modifiche strutturali specifiche di questo grafico
                    spazioGrafico.Save(parteGraficoCorrente)
                End If
            Next

            ' 5. Salviamo il documento Word globale una volta completato il ciclo
            partePrincipale.Document.Save()

        End Using
    End Sub
    Public Sub AggiornaGraficoDocumento(pathFileDocx As String, datiInInput As List(Of ElementoGrafico))

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
            AggiornaEtichetteAsseX(serie1, datiInInput, numeroElementi)
            AggiornaValoriNumericiY(serie1, datiInInput, numeroElementi, UsaValoreY2:=False)

            ' AGGIORNAMENTO SERIE 2 (Solo i valori Y2, l'asse X è condiviso o speculare)
            Dim serie2 As BarChartSeries = tutteLeSerie(1)
            AggiornaEtichetteAsseX(serie2, datiInInput, numeroElementi) ' Sincronizza l'asse X anche qui per sicurezza
            AggiornaValoriNumericiY(serie2, datiInInput, numeroElementi, UsaValoreY2:=True)

            ' 6. Salviamo le modifiche apportate alla struttura del grafico
            spazioGrafico.Save(parteGrafico)

            ' 7. Salviamo il documento Word nel suo insieme
            partePrincipale.Document.Save()

        End Using
    End Sub

    Private Sub AggiornaEtichetteAsseX(serie As BarChartSeries, dati As List(Of ElementoGrafico), conteggio As UInt32)
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

    Private Sub AggiornaValoriNumericiY(serie As BarChartSeries, dati As List(Of ElementoGrafico), conteggio As UInt32, UsaValoreY2 As Boolean)
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


End Module
