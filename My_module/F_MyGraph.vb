' Definiamo cosa contiene ogni set di dati
Imports System.ComponentModel.Design
Imports OpenTK.Graphics.OpenGL

'Public Structure DatiGrafico
'    Public Data As String
'    Public ValoriY As List(Of Integer) ' Può contenere 1 o 2 valori
'    Public Color As List(Of Integer) ' Può contenere 1 o 2 valori
'End Structure
'Public Gbl_datigrafico As New List(Of DatiGrafico)()

' La lista che conterrà tutti i tuoi set di dati (da 3 a 30)
Public Class F_MyGraph
    Private ListaDati As New List(Of DatiGrafico)()

    Private YMax As Double = 22.0 ' Il tuo fondo scala personalizzabile

    Function Mostra(Optional TESTO As String = "") As Boolean
        YMax = 25
        If Carica_dati_ELCAR() = 0 Then Return False


        Me.SetStyle(ControlStyles.ResizeRedraw, True)
        ' Diciamo alla Form di ridisegnarsi con i nuovi dati
        Me.Invalidate()

        Label1.Text = " (" + Gbl_Graph.Cod_ELCAR + ")"
        Label3.Text = Gbl_Graph.Cod_Testo
        tp1.SetToolTip(Label1, Gbl_Graph.Cod_Testo)

        Label1.Refresh()
        Call trend()
        Call Carica_DGV1()
        Panel1.Invalidate() ' Forza il Panel a ridisegnarsi

        Me.Show()
        Return True

    End Function
    Sub Carica_DGV1()
        Dim n = Gbl_Graph.Data.Count - 1
        Dim perc = 0, dt = ""
        DGV1.Rows.Clear()
        For j = n To 0 Step -1
            dt = Gbl_Graph.Data(j).Data + " (1)"
            perc = Calcola_Percentuale(Gbl_Graph.Data(j).TOT1_Pos)
            DGV1.Rows.Add(dt, Gbl_Graph.Data(j).TOT1_Pos, Gbl_Graph.Data(j).TOT1_Neg, Gbl_Graph.Data(j).Tot1_PROMPT, Gbl_Graph.Data(j).Tot1, perc)
            If Gbl_Graph.Data(j).Tot2 > 0 Then
                dt = Gbl_Graph.Data(j).Data + " (2)"
                perc = Calcola_Percentuale(Gbl_Graph.Data(j).TOT2_Pos)
                DGV1.Rows.Add(dt, Gbl_Graph.Data(j).TOT2_Pos, Gbl_Graph.Data(j).TOT2_Neg, Gbl_Graph.Data(j).Tot2_PROMPT, Gbl_Graph.Data(j).Tot2, perc)
            End If
        Next

    End Sub

    Function Carica_dati_ELCAR() As Integer
        Dim N = UBound(Gbl_Graph.Data)
        N = Gbl_Graph.Data.Length
        Dim cod = Gbl_Graph.Cod_ELCAR
        Dim X(N) As Date, Y(N) As Integer, DATAX As Date, XX = ""
        Dim s1 As New DatiGrafico
        Dim COL0 = 0, COL1 = 1, TOT1 = 0, TOT2 = 0, TOT = 0
        ListaDati.Clear()

        If N = 0 Then Return N
        Dim trovati = 0
        For j = 0 To N - 1
            trovati = trovati + 1
            XX = Gbl_Graph.Data(j).Data
            Try
                s1.Data = Date.ParseExact(XX, "dd/MM/yyyy", Nothing)
            Catch ex As Exception
                trovati = trovati - 1
                Continue For
            End Try
            s1.Data = Date.ParseExact(XX, "dd/MM/yyyy", Nothing)
            Y(j) = Gbl_Graph.Data(j).TOT1_Pos
            XX = Gbl_Graph.Data(j).Data + "]  (" + Gbl_Graph.Cod_ELCAR + ") " + Gbl_Graph.Data(j).Data
            's1.ValoriY(0) = Gbl_Graph(i).Data(j).TOT1_Pos

            TOT1 = Gbl_Graph.Data(j).Tot1
            TOT2 = Gbl_Graph.Data(j).Tot2
            TOT = TOT1 + TOT2
            If TOT = 0 Then Continue For ' nON CI SONO DATI

            If TOT2 = 0 Then
                s1.ValoriY = New List(Of Integer)({Gbl_Graph.Data(j).TOT1_Pos})
                s1.Color = New List(Of Integer)({COL0})
            Else
                s1.ValoriY = New List(Of Integer)({Gbl_Graph.Data(j).TOT1_Pos, Gbl_Graph.Data(j).TOT2_Pos})
                s1.Color = New List(Of Integer)({COL0, COL1})
            End If
            ListaDati.Add(s1)
        Next
        Gbl_datigrafico = ListaDati
        Return trovati
    End Function
    Private Sub F_MyGraph_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DoubleBuffered = True


    End Sub
    Private Function GetColoreDaIntero(ByVal val As Integer) As Color

        Dim perc = Calcola_Percentuale(val)
        Select Case perc
            Case = 0
                Return Color.White
            Case >= 80
                Return Color.Green   ' Verde
            Case Else
                Return Color.Gray ' Default: Grigio
        End Select
    End Function
    Sub trend()
        If Trend_1() Then
            Call mostra_label_l1()
            Call Trend_visualizza()
        End If

    End Sub
    Sub Trend_visualizza()
        '-----------------------------------------
        '-- mOSTRO IL TIPO DI TREND --
        '-----------------------------------------
        If Gbl_Graph.Trend = "SUPERATO" Then
            Label4.Text = "Superato"
            Label4.BackColor = Color.LightGreen
            Exit Sub
        End If

        If Gbl_Graph.Trend = "POS4" Then
            Label4.Text = "Trend POSITIVO (CONTINUARE)"
            Label4.BackColor = Color.LightBlue
            Exit Sub
        End If

        If Gbl_Graph.Trend = "NEG4" Then
            Label4.Text = "Trend NEGATIVO (CAMBIARE)"
            Label4.BackColor = Color.LightSalmon
            Exit Sub
        End If

        Dim ip = UBound(Gbl_Percentuale)
        If Gbl_Graph.Trend = "UNDEF" Then
            If ip < 5 Then
                Label4.Text = "Dati insufficienti (" + (ip + 1).ToString + ") (continuare)"
                Label4.BackColor = Color.LightYellow
                Exit Sub
            Else
                Label4.Text = "Trend NON DEFINITO (Valutare)"
                Label4.BackColor = Color.LightYellow
                Exit Sub
            End If
        End If
        Label4.Refresh()

    End Sub
    Sub mostra_label_l1()
        Dim xx = ""
        Dim perc As Integer = 0
        Dim IP = 0, i0 = 0, tipo = 0
        Dim n = UBound(Gbl_Percentuale)
        ' 3 Consecutivi >80% allora Passato
        ' 4 consecutivi trent positivo = Continuare
        ' 4 consecutivi trent negativo = Cambiare
        Dim x = Split(Gbl_Graph.Labeltag, ",")

        Try
            l1.Tag = x(0)
            l2.Tag = x(1)
            l3.Tag = x(2)
            l4.Tag = x(3)
            l5.Tag = x(4)
            L6.Tag = x(5)
            l7.Tag = x(6)
            l8.Tag = x(7)
        Catch ex As Exception
        End Try
        Dim lblSorgente() As Label = {l1, l2, l3, l4, l5, L6, l7, l8}


        For k As Integer = 0 To n

            ' Evita errori se il Tag è vuoto
            tp1.SetToolTip(lblSorgente(k), "Nessun dato presente")
            lblSorgente(k).ImageIndex = 1
            xx = lblSorgente(k).Tag

            perc = Gbl_Percentuale(k)
            lblSorgente(k).Text = perc.ToString
            If perc >= 80 Then
                lblSorgente(k).ImageIndex = 4
                xx = "Percentuale > 80% (" + perc.ToString + "%)"
            Else
                lblSorgente(k).ImageIndex = 5
                xx = "Percentuale < 80% (" + perc.ToString + "%)"
            End If
            tp1.SetToolTip(lblSorgente(k), xx)
            lblSorgente(k).Refresh()
        Next

        For j = n + 1 To UBound(lblSorgente) - 1
            lblSorgente(j).ImageIndex = 0
            xx = "Dato on disponibile"
            lblSorgente(j).Text = ""
        Next


        'N. di risposte /n. giorni
        Dim ngiorni = Gbl_Graph.Data.Count
        Dim ntot = 0, ntotok = 0
        For j = 0 To ngiorni - 1
            If Gbl_Graph.Data(j).Tot1 > 0 Then
                ntot = ntot + 1
                If Gbl_Graph.Data(j).Tot1 = 20 Then ntotok = ntotok + 1
            End If
            If Gbl_Graph.Data(j).Tot2 > 0 Then
                ntot = ntot + 1
                If Gbl_Graph.Data(j).Tot2 = 20 Then ntotok = ntotok + 1
            End If


        Next
        Label6.Text = ntot.ToString + " in " + ngiorni.ToString + " gg"
        Label2.Text = ntotok.ToString + "/" + ntot.ToString


    End Sub

    Private Sub Disegna_Grafico_Su(targetPanel As Panel, g As Graphics)
        ' Blocco di sicurezza: se non ci sono dati o il pannello è ridotto a icona, non fare nulla
        If ListaDati.Count = 0 OrElse targetPanel.ClientRectangle.Width <= 0 OrElse targetPanel.ClientRectangle.Height <= 0 Then Return

        g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        ' --- 1. IMPOSTAZIONI DI SPAZIATURA E AREA DEL GRAFICO (Riferite al Panel) ---
        Dim margineSinistro As Integer = 50
        Dim margineDestro As Integer = 50
        Dim margineSuperiore As Integer = 1
        Dim margineInferiore As Integer = 80

        ' NOTA: Ora usiamo targetPanel.ClientRectangle anziché Me.ClientSize
        Dim altezzaGrafico As Integer = targetPanel.ClientRectangle.Height - margineSuperiore - margineInferiore
        Dim larghezzaGrafico As Integer = targetPanel.ClientRectangle.Width - margineSinistro - margineDestro
        Dim baseGrafico_Y As Integer = targetPanel.ClientRectangle.Height - margineInferiore

        ' Evita divisioni per zero se l'area del grafico diventa troppo piccola durante il resize
        If larghezzaGrafico <= 0 OrElse altezzaGrafico <= 0 Then Return

        ' Calcola lo spazio totale per ogni set (Data + Colonne) per garantire l'equidistanza
        Dim spazioPerSet As Double = larghezzaGrafico / ListaDati.Count
        Dim larghezzaColonna As Integer = 18
        Dim spazioTraColonneInterne As Integer = 4

        ' --- 2. PENNELLI, PENNE E FONT ---
        Dim pennelloColonna2 As New SolidBrush(Color.Orange)
        Dim pennelloTestoData As New SolidBrush(Color.Black)
        Dim pennelloTestoValore As New SolidBrush(Color.White) ' Testo bianco dentro le colonne
        Dim pennaAssi As New Pen(Color.Gray, 1)

        ' Impostazioni della linea spezzata rossa
        Dim pennaSpezzata As New Pen(Color.Crimson, 3)
        Dim pennelloNodi As New SolidBrush(Color.Crimson)

        Dim fontData As New Font("Arial", 9)
        Dim fontValore As New Font("Arial", 8, FontStyle.Bold)

        Dim formatoVerticale As New StringFormat()
        formatoVerticale.FormatFlags = StringFormatFlags.DirectionVertical

        Dim formatoValoreCentrato As New StringFormat()
        formatoValoreCentrato.Alignment = StringAlignment.Center

        ' Lista per memorizzare i punti della spezzata
        Dim puntiSpezzata As New List(Of Point)()

        ' --- 3. DISEGNO ASSE DI BASE ---
        g.DrawLine(pennaAssi, margineSinistro, baseGrafico_Y, targetPanel.ClientRectangle.Width - margineDestro, baseGrafico_Y)

        ' --- 4. CICLO PRINCIPALE SUI DATI ---
        For i As Integer = 0 To ListaDati.Count - 1
            Dim setCorrente As DatiGrafico = ListaDati(i)

            ' Il centro X esatto di questo gruppo di dati
            Dim centroX_Set As Integer = CInt(margineSinistro + (i * spazioPerSet) + (spazioPerSet / 2))

            Dim numeroColonne As Integer = setCorrente.ValoriY.Count
            Dim larghezzaTotaleColonne As Integer = (numeroColonne * larghezzaColonna) + ((numeroColonne - 1) * spazioTraColonneInterne)
            Dim xInizioColonne As Integer = centroX_Set - (larghezzaTotaleColonne \ 2)
            Dim perc = 0
            ' Ciclo per disegnare le colonne interne al set (1 o 2)
            For j As Integer = 0 To numeroColonne - 1
                Dim valore As Double = setCorrente.ValoriY(j)
                If valore > YMax Then valore = YMax

                Dim altezzaPixel As Integer = CInt((valore / YMax) * altezzaGrafico)
                Dim xColonna As Integer = xInizioColonne + (j * (larghezzaColonna + spazioTraColonneInterne))
                Dim yColonna As Integer = baseGrafico_Y - altezzaPixel

                ' --- PROTEZIONE RESIZE E COLORE DINAMICO ---
                Dim idColore As Integer = 0
                If setCorrente.Color IsNot Nothing AndAlso j < setCorrente.Color.Count Then
                    idColore = setCorrente.Color(j)
                End If

                Dim coloreScelto As Color = GetColoreDaIntero(valore)

                ' DISEGNO DELLA COLONNA 
                Dim rettangoloColonna As New Rectangle(xColonna, yColonna, larghezzaColonna, altezzaPixel)
                Dim pennelloUsa As SolidBrush = New SolidBrush(coloreScelto)

                g.FillRectangle(pennelloUsa, rettangoloColonna)

                'If j = 0 Then pennelloUsa.Dispose()

                ' Inserimento del numero intero dentro la colonna
                Dim valoreIntero As Integer = CInt(Math.Round(setCorrente.ValoriY(j)))
                Dim testoValore As String = valoreIntero.ToString()

                If altezzaPixel > 15 Then
                    g.DrawString(testoValore, fontValore, pennelloTestoValore, New RectangleF(xColonna, yColonna + 5, larghezzaColonna, 20), formatoValoreCentrato)
                End If

                ' --- MODIFICA SPEZZATA ---
                Dim centroX_Colonna As Integer = xColonna + (larghezzaColonna \ 2)
                puntiSpezzata.Add(New Point(centroX_Colonna, yColonna))
            Next

            ' Disegno della data in verticale sotto l'asse delle X
            g.DrawString(setCorrente.Data, fontData, pennelloTestoData, centroX_Set - 5, baseGrafico_Y + 5, formatoVerticale)
        Next

        ' --- 5. DISEGNO DELLA SPEZZATA CENTRATA ---
        If puntiSpezzata.Count >= 2 Then
            g.DrawLines(pennaSpezzata, puntiSpezzata.ToArray())

            Dim raggioPallino As Integer = 4
            For Each pt As Point In puntiSpezzata
                g.FillEllipse(pennelloNodi, pt.X - raggioPallino, pt.Y - raggioPallino, raggioPallino * 2, raggioPallino * 2)
            Next
        End If

        ' --- 6. LIBERAZIONE MEMORIA ---
        pennelloColonna2.Dispose()
        pennelloTestoData.Dispose()
        pennelloTestoValore.Dispose()
        pennaAssi.Dispose()
        pennaSpezzata.Dispose()
        pennelloNodi.Dispose()
        fontData.Dispose()
        fontValore.Dispose()
        formatoVerticale.Dispose()
        formatoValoreCentrato.Dispose()
    End Sub
    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
        ' Passiamo l'oggetto Panel e l'oggetto Graphics dell'evento alla routine di disegno
        Disegna_Grafico_Su(Panel1, e.Graphics)
    End Sub

    ' Questo evento serve a ridisegnare il grafico se l'utente ridimensiona la Form (e di conseguenza il Panel)
    Private Sub F_MyGraph_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Panel1.Invalidate() ' Forza il Panel a ridisegnarsi
        'Me.Invalidate()

    End Sub
    Function Carica_dati_totale_giornaliero(dbbambino As String) As Integer
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

        For Each voce In Totale_voci
            idf = voce.id_foglio
            For Each vocert In totale_Rt
                If idf = vocert.id_foglio Then
                    voce.tot_RT = vocert.tot_RT
                    voce.tot = vocert.tot_RT + voce.tot_voci
                    s1.Data = Date.ParseExact(voce.Data, "dd/MM/yyyy", Nothing)
                    s1.ValoriY = New List(Of Integer)({voce.tot, voce.tot_POS_voci})
                    s1.Color = New List(Of Integer)({COL0, COL0})
                    ListaDati.Add(s1)
                    Exit For
                End If
            Next
        Next
        Gbl_datigrafico = ListaDati

    End Function


End Class