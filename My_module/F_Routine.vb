Imports System.CodeDom.Compiler
Imports System.Linq
Public Class F_Seleziona_Routine
    Dim P_elenco_Routine As List(Of Class_ROUTINE)
    Dim P_elenco_Routine_selezionate As List(Of Class_ROUTINE)
    Dim P_elenco_Routine_Giornaliero As List(Of Class_Giornaliero_ROUTINE)
    Dim puntoInizialeClic As Point = Point.Empty
    ' ECCOLA QUI: Definisci la variabile per l'indice di partenza
    Dim indicePartenzaDrag As Integer = -1
    Dim P_ORDINA As Boolean = False
    Private isRefreshing As Boolean = False
    Private NONESEGUIRE = True
    Private bloccoSemaforo As Boolean = False
    Private Gb_N_utlimavoce As Integer = 0

    Function main(ID_foglio As Integer, Optional nuovo As Boolean = True) As List(Of Class_Giornaliero_ROUTINE)
        P_ORDINA = False
        Label11.Text = "Foglio n. " + Gbl_foglio.ID_foglio.ToString + " del " + Gbl_foglio.Data
        Label6.Text = Gbl_NOME_BAMBINO
        Carica_elenco_routine_disponibili()

        If nuovo Then
            Label5.Text = "Da preparare"
            Label5.ForeColor = Color.LightGreen
        Else
            For J = 0 To Gbl_foglio.RT.Count - 1
                Dim id_voce = Gbl_foglio.RT(J).ID_VOCE
                Dim ii = Trova_voce_rt(id_voce)
                CheckedListBox1.SetItemChecked(ii, True)
                CheckedListBox1.Items(ii).N_VOCE = Gbl_foglio.RT(J).N_voce
            Next
            SpuntaEOrdinaListaGiaCaricata()

        End If

        Me.ShowDialog()
        If Me.Tag = "OK" Then
            Return P_elenco_Routine_Giornaliero
        End If
    End Function

    Private Sub CheckedListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CheckedListBox1.SelectedIndexChanged
        Call Mostra_conta()
    End Sub
    Function Mostra_conta() As Integer
        Dim nvoci = CheckedListBox1.CheckedItems.Count
        Label10.Text = nvoci.ToString
        Return nvoci


    End Function
    Private Sub CheckedListBox1_MouseDown(sender As Object, e As MouseEventArgs) Handles CheckedListBox1.MouseDown
        ' Il comportamento iniziale del clic viene registrato normalmente
        If e.Button = MouseButtons.Left Then
            Dim indiceCliccato As Integer = CheckedListBox1.IndexFromPoint(e.Location)

            If indiceCliccato <> ListBox.NoMatches Then
                puntoInizialeClic = e.Location
                indicePartenzaDrag = indiceCliccato
            Else
                puntoInizialeClic = Point.Empty
                indicePartenzaDrag = -1
            End If
        End If
    End Sub
    Function Trova_N_ultimavoce() As Integer

    End Function
    Private Sub CheckedListBox1_DragOver(sender As Object, e As DragEventArgs) Handles CheckedListBox1.DragOver
        ' Permette visivamente l'effetto di spostamento
        e.Effect = DragDropEffects.Move
    End Sub

    Private Sub CheckedListBox1_DragDrop(sender As Object, e As DragEventArgs) Handles CheckedListBox1.DragDrop
        Dim puntoRilascio As Point = CheckedListBox1.PointToClient(New Point(e.X, e.Y))
        Dim indiceArrivo As Integer = CheckedListBox1.IndexFromPoint(puntoRilascio)
        Dim indicePartenza As Integer = indicePartenzaDrag

        If indicePartenza = -1 Then Return

        If indiceArrivo = ListBox.NoMatches Then
            indiceArrivo = CheckedListBox1.Items.Count - 1
        End If

        If indicePartenza = indiceArrivo Then Return

        Dim routineDaSpostare As Class_ROUTINE = CType(e.Data.GetData(GetType(Class_ROUTINE)), Class_ROUTINE)
        If routineDaSpostare Is Nothing Then Return

        ' 1. Aggiorna la lista logica in memoria
        Dim routineNellaLista As Class_ROUTINE = P_elenco_Routine(indicePartenza)
        P_elenco_Routine.RemoveAt(indicePartenza)
        P_elenco_Routine.Insert(indiceArrivo, routineNellaLista)

        ' 2. Aggiorna la grafica della CheckedListBox
        Dim statoSpunta As Boolean = CheckedListBox1.GetItemChecked(indicePartenza)
        CheckedListBox1.Items.RemoveAt(indicePartenza)
        CheckedListBox1.Items.Insert(indiceArrivo, routineDaSpostare)
        CheckedListBox1.SetItemChecked(indiceArrivo, statoSpunta)

        CheckedListBox1.SelectedIndex = indiceArrivo
        indicePartenzaDrag = -1
    End Sub
    Sub Carica_elenco_routine_giornaliere()
        Dim routine As New Class_Giornaliero_ROUTINE
        For j = 0 To Gbl_foglio.RT.Count - 1
            Dim idv = routine.ID_VOCE
            Dim testo = routine.TESTO
            Dim tot = routine.TOT
            Dim nv = j + 1
        Next
    End Sub
    Sub Carica_elenco_routine_disponibili()
        P_elenco_Routine = Class_ROUTINE.Load_all

        ' 2. Svuota la lista ed evita il DataSource
        CheckedListBox1.DataSource = Nothing
        CheckedListBox1.Items.Clear()
        CheckedListBox1.DisplayMember = "TESTO"

        ' 3. Popola il controllo riga per riga
        For Each routine In P_elenco_Routine
            ' Inserisce l'oggetto e imposta la spunta basandosi sulla proprietà IsChecked
            CheckedListBox1.Items.Add(routine)
        Next
        CheckedListBox1.DisplayMember = "TESTO" ' Imposta il DisplayMember PRIMA del DataSource

    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        P_elenco_Routine_Giornaliero = New List(Of Class_Giornaliero_ROUTINE)
        Dim n_voce = 0
        For Each item In CheckedListBox1.CheckedItems
            ' Converte l'elemento generico nell'oggetto reale della tua classe
            Dim routineSelezionata As Class_ROUTINE = CType(item, Class_ROUTINE)
            ' Carico l'elenco deille caselle spuntate 
            Dim temp As New Class_Giornaliero_ROUTINE
            temp.ID_VOCE = routineSelezionata.ID   ' ID della voce 
            temp.TESTO = routineSelezionata.TESTO  ' testo della voce 
            temp.ID_FOGLIO = Gbl_foglio.ID_foglio  ' ID del noglio a cui fa riferimento
            temp.TOT = 0                           'Totali
            temp.DATA = Gbl_foglio.Data            'data del foglio
            temp.ID = 0                            'ID della voce nella tabella routine_giornaliera 
            n_voce = n_voce + 1
            temp.N_voce = n_voce
            P_elenco_Routine_Giornaliero.Add(temp)
        Next
        NONESEGUIRE = True
        Me.Tag = "OK"
        Me.Hide()
    End Sub
    Public Sub SpuntaEOrdinaListaGiaCaricata()
        If NONESEGUIRE Then Exit Sub
        ' 1. Legge gli oggetti Class_Routine direttamente dalla CheckedListBox
        '    e calcola subito lo stato di spunta basato su N_voce > 0
        Dim elementi = CheckedListBox1.Items.Cast(Of Class_ROUTINE)().Select(
        Function(item) New With {
            .DatiRoutine = item,
            .StatoSpunta = (item.N_voce > 0) ' True se > 0, altrimenti False
        }
    ).ToList()

        ' 2. Ordina: prima i Checked (True va in cima con OrderByDescending)
        '    e poi all'interno dei due gruppi ordina per N_voce crescente
        Dim elementiOrdinati = elementi.
        OrderByDescending(Function(x) x.StatoSpunta).
        ThenBy(Function(x) x.DatiRoutine.N_voce).
        ToList()

        ' 3. Svuota il controllo e lo ripopola con il nuovo ordine e le spunte corrette
        CheckedListBox1.BeginUpdate()
        CheckedListBox1.Items.Clear()

        For Each elemento In elementiOrdinati
            CheckedListBox1.Items.Add(elemento.DatiRoutine, elemento.StatoSpunta)
        Next

        CheckedListBox1.EndUpdate()
    End Sub
    Private Sub F_Seleziona_Routine_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckedListBox1.CheckOnClick = True
        NONESEGUIRE = False
    End Sub
    Function Trova_voce_rt(idDaCercare As Integer) As Integer

        For i As Integer = 0 To CheckedListBox1.Items.Count - 1

            ' Esegui il cast dell'item corrente nell'oggetto della tua classe Routine
            Dim rt As Class_ROUTINE = CheckedListBox1.Items(i)
            If rt.ID = idDaCercare Then
                Return i
            End If
        Next
        Return -1
    End Function
    Private Sub CheckedListBox1_MouseMove(sender As Object, e As MouseEventArgs) Handles CheckedListBox1.MouseMove
        ' CONTROLLO CON LA TUA VARIABILE: 
        ' Se P_DRAGDROP è False, usciamo subito e il controllo si comporta normalmente (gestisce solo le spunte)
        If Not P_ORDINA OrElse
           e.Button <> MouseButtons.Left OrElse
           puntoInizialeClic = Point.Empty OrElse
           indicePartenzaDrag = -1 Then Return

        ' Calcola lo spostamento del mouse rispetto al clic iniziale
        Dim scostamentoX As Integer = Math.Abs(e.X - puntoInizialeClic.X)
        Dim scostamentoY As Integer = Math.Abs(e.Y - puntoInizialeClic.Y)

        ' Se lo spostamento supera la tolleranza di Windows, avvia il Drag
        If scostamentoX > SystemInformation.DragSize.Width OrElse scostamentoY > SystemInformation.DragSize.Height Then
            Dim oggettoDaTrascinare As Class_ROUTINE = CType(CheckedListBox1.Items(indicePartenzaDrag), Class_ROUTINE)

            puntoInizialeClic = Point.Empty ' Reset per evitare loop double-drag

            ' Avvia il trascinamento passando l'oggetto
            CheckedListBox1.DoDragDrop(oggettoDaTrascinare, DragDropEffects.Move)
        End If
    End Sub

    Private Sub CheckedListBox1_MouseUp(sender As Object, e As MouseEventArgs) Handles CheckedListBox1.MouseUp
        puntoInizialeClic = Point.Empty
        indicePartenzaDrag = -1
    End Sub

    Private Sub CheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox4.CheckedChanged
        P_ORDINA = CheckBox4.Checked
        If P_ORDINA Then
            CheckedListBox1.BackColor = MyColor.LightYellow
        Else
            CheckedListBox1.BackColor = SystemColors.Window
        End If
    End Sub

    Private Sub Btn_su_Click(sender As Object, e As EventArgs) Handles Btn_su.Click
        Dim indiceCorrente As Integer = CheckedListBox1.SelectedIndex

        ' Si può muovere in alto solo se l'elemento è valido e non è già in cima (indice 0)
        ' Inoltre verifichiamo che la voce sia effettivamente spuntata
        If indiceCorrente > 0 AndAlso CheckedListBox1.GetItemChecked(indiceCorrente) Then
            Dim indiceNuovo As Integer = indiceCorrente - 1

            bloccoSemaforo = True

            Dim routineDaSpostare As Class_ROUTINE = P_elenco_Routine(indiceCorrente)

            ' Spostamento nella lista logica
            P_elenco_Routine.RemoveAt(indiceCorrente)
            P_elenco_Routine.Insert(indiceNuovo, routineDaSpostare)

            ' Spostamento nella CheckedListBox grafica
            CheckedListBox1.Items.RemoveAt(indiceCorrente)
            CheckedListBox1.Items.Insert(indiceNuovo, routineDaSpostare)
            CheckedListBox1.SetItemChecked(indiceNuovo, True)

            CheckedListBox1.SelectedIndex = indiceNuovo

            bloccoSemaforo = False
        End If
    End Sub

    Private Sub Brn_giu_Click(sender As Object, e As EventArgs) Handles Brn_giu.Click
        Dim indiceCorrente As Integer = CheckedListBox1.SelectedIndex

        If indiceCorrente >= 0 AndAlso CheckedListBox1.GetItemChecked(indiceCorrente) Then
            ' Calcoliamo quanti elementi spuntati ci sono in totale
            Dim totaleSpuntati As Integer = 0
            For Each r In P_elenco_Routine
                If r.IsChecked Then totaleSpuntati += 1
            Next

            ' Permette lo spostamento in basso SOLO se non ha ancora raggiunto l'ultimo posto del gruppo spuntati
            If indiceCorrente < totaleSpuntati - 1 Then
                Dim indiceNuovo As Integer = indiceCorrente + 1

                bloccoSemaforo = True

                Dim routineDaSpostare As Class_ROUTINE = P_elenco_Routine(indiceCorrente)

                ' Spostamento nella lista logica
                P_elenco_Routine.RemoveAt(indiceCorrente)
                P_elenco_Routine.Insert(indiceNuovo, routineDaSpostare)

                ' Spostamento nella CheckedListBox grafica
                CheckedListBox1.Items.RemoveAt(indiceCorrente)
                CheckedListBox1.Items.Insert(indiceNuovo, routineDaSpostare)
                CheckedListBox1.SetItemChecked(indiceNuovo, True)

                CheckedListBox1.SelectedIndex = indiceNuovo

                bloccoSemaforo = False
            End If
        End If
    End Sub

    Private Sub CheckedListBox1_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles CheckedListBox1.ItemCheck
        If NONESEGUIRE Then Exit Sub
        ' 1. SE IL SEMAFORO DI RIGENERAZIONE È ATTIVO, ESCI SUBITO
        If bloccoSemaforo Then Return

        ' --- NUOVO CONTROLLO DI BLOCCO SPUNTE ---
        ' Se siamo in modalità ordinamento, annulliamo il cambio della spunta
        If P_ORDINA Then
            e.NewValue = e.CurrentValue ' Forza la spunta a rimanere com'era
            Return ' Blocca lo spostamento automatico in alto/basso
        End If
        ' ----------------------------------------

        Dim routineCliccata As Class_ROUTINE = CType(CheckedListBox1.Items(e.Index), Class_ROUTINE)
        Dim diventeràSpuntato As Boolean = (e.NewValue = CheckState.Checked)

        routineCliccata.IsChecked = diventeràSpuntato

        BeginInvoke(Sub()
                        bloccoSemaforo = True

                        Dim vecchioIndice As Integer = CheckedListBox1.Items.IndexOf(routineCliccata)
                        If vecchioIndice <> -1 Then CheckedListBox1.Items.RemoveAt(vecchioIndice)
                        P_elenco_Routine.Remove(routineCliccata)

                        Dim nuovoIndice As Integer = 0
                        Dim conteggioSpuntati As Integer = 0
                        For Each r In P_elenco_Routine
                            If r.IsChecked Then conteggioSpuntati += 1
                        Next

                        If diventeràSpuntato Then
                            nuovoIndice = conteggioSpuntati
                        Else
                            nuovoIndice = conteggioSpuntati
                        End If

                        P_elenco_Routine.Insert(nuovoIndice, routineCliccata)
                        CheckedListBox1.Items.Insert(nuovoIndice, routineCliccata)
                        CheckedListBox1.SetItemChecked(nuovoIndice, diventeràSpuntato)

                        CheckedListBox1.SelectedIndex = nuovoIndice

                        bloccoSemaforo = False
                    End Sub)
    End Sub
    Private Sub RiordinaEAlineaLista(elementoMosso As Class_ROUTINE, èStatoSpuntato As Boolean)
        ' Attiva il semaforo per bloccare temporaneamente l'evento ItemCheck
        isRefreshing = True

        ' 1. Rimuove l'elemento dalla sua posizione attuale nella lista logica
        P_elenco_Routine.Remove(elementoMosso)

        ' Conteggio tradizionale degli elementi selezionati
        Dim numeroSelezionati As Integer = 0
        For Each r As Class_ROUTINE In P_elenco_Routine
            If r.IsChecked Then
                numeroSelezionati += 1
            End If
        Next

        ' Lo inserisce nella nuova posizione logica
        P_elenco_Routine.Insert(numeroSelezionati, elementoMosso)

        ' 2. Aggiorna la grafica della CheckedListBox
        CheckedListBox1.Items.Clear()

        For Each routine As Class_ROUTINE In P_elenco_Routine
            ' Grazie al semaforo, questa riga NON farà scattare nuovamente l'evento in loop
            CheckedListBox1.Items.Add(routine, routine.IsChecked)
        Next

        ' 3. Ripristina la selezione visiva sull'oggetto appena mosso
        CheckedListBox1.SelectedIndex = P_elenco_Routine.IndexOf(elementoMosso)

        ' Disattiva il semaforo: il controllo è pronto per i prossimi clic dell'utente
        isRefreshing = False
    End Sub

    Private Sub Btn_su_DragEnter(sender As Object, e As DragEventArgs) Handles Btn_su.DragEnter

    End Sub

    Private Sub CheckedListBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles CheckedListBox1.KeyDown
        ' Il controllo si attiva SOLO se siamo in modalità ordinamento (P_ORDINA = True)
        If P_ORDINA Then

            ' Se l'utente preme la FRECCIA SU
            If e.KeyCode = Keys.Up Then
                ' Interrompiamo il movimento nativo del controllo
                e.Handled = True
                e.SuppressKeyPress = True

                ' Simuliamo il clic del pulsante "Sposta Su" eseguendo direttamente il suo codice
                Btn_su_Click(Btn_su, EventArgs.Empty)
            End If

            ' Se l'utente preme la FRECCIA GIÙ
            If e.KeyCode = Keys.Down Then
                ' Interrompiamo il movimento nativo del controllo
                e.Handled = True
                e.SuppressKeyPress = True

                ' Simuliamo il clic del pulsante "Sposta Giù" eseguendo direttamente il suo codice
                Brn_giu_Click(Brn_giu, EventArgs.Empty)
            End If

        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        NONESEGUIRE = True
        Me.Hide()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'F_Elenco_Comportamenti.Main("RT", True)

        Dim m = "Testo nella nuova voce:"
        Dim t = "Nuova voce"
        Dim x = ""
        x = InputBox(m, t, x)
        If x = "" Then Exit Sub

        m = "Nuova voce da inserire: " + vbCrLf + x.ToString + vbCrLf + vbCrLf
        m = m + "Vuoi salvarla?"
        If MsgBox(m, vbQuestion + vbYesNo, "Salvataggio nuova voce") <> vbYes Then Exit Sub

        Dim temp As New Class_ROUTINE
        temp.ID = 0
        temp.TESTO = x
        temp.ATTIVA = True
        Dim id = temp.Save()
        P_elenco_Routine.Add(temp)
        CheckedListBox1.Items.Add(temp, True)
    End Sub

    Private Sub TabPage2_Click(sender As Object, e As EventArgs)

    End Sub
End Class