Imports System.Data.SQLite
Imports System.Text.RegularExpressions
Imports DocumentFormat.OpenXml.Office2010.Excel
Imports DocumentFormat.OpenXml.Office2010.PowerPoint
Imports Microsoft.Data.Sqlite




Public Class F_Gestione_Codici_ELCAR
    Dim nodoSelezionato As TreeNode
    Public Structure Elenco_voci_personali
        Public Bambino As String
        Public DBambino As String
        Public elenco As List(Of Class_ELCAR_VOCI_PERSONALI)
        Public Tot As Double
    End Structure
    Dim ElencoVocipersonali As New List(Of Elenco_voci_personali)



    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Hide()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles B_esporta.Click
        F_ELCAR_INOUT.ShowDialog()
    End Sub

    Function Edit_onoff(stato As Boolean)
        Panel2.Visible = stato ' Pannello pulsanto salva
        B_edit.Visible = Not stato
        TreeView1.Enabled = Not stato
        B_esporta.Enabled = Not stato
        TextBox1.ReadOnly = Not stato
        TextBox2.ReadOnly = Not stato


    End Function

    Public Sub PopolaAlberoElcar(treeView As TreeView, stringaConnessione As String)
        treeView.Nodes.Clear()

        Dim listaElementi As New List(Of ElementoAlbero)()
        Dim dizionarioNodi As New Dictionary(Of String, TreeNode)()

        Dim query As String = "SELECT CODICE AS CodiceNodo, NULL AS CodicePadre, TESTO FROM ELCAR " &
                          "UNION ALL " &
                          "SELECT (CODICE_ELCAR || '.' || CODICE_VOCE), CODICE_ELCAR, TESTO FROM ELCAR_VOCI " &
                          "UNION ALL " &
                          "SELECT (ELCAR || '.' || ID_SUB), ELCAR, TESTO FROM ELCAR_VOCI_SUB WHERE Deleted = 0 OR Deleted IS NULL " &
                          "UNION ALL " &
                          "SELECT (ELCAR || '.' || ID_SUB), ELCAR, TESTO FROM ELCAR_VOCI_SUB2 WHERE Deleted = 0 OR Deleted IS NULL;"

        Using conn As New SqliteConnection(stringaConnessione)
            Using cmd As New SqliteCommand(query, conn)
                Try
                    conn.Open()
                    Using reader As SqliteDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim elem As New ElementoAlbero With {
                            .CodiceNodo = reader("CodiceNodo").ToString().Trim(),
                            .CodicePadre = If(reader("CodicePadre") Is DBNull.Value, String.Empty, reader("CodicePadre").ToString().Trim()),
                            .Testo = reader("TESTO").ToString()
                        }
                            listaElementi.Add(elem)
                        End While
                    End Using
                Catch ex As Exception
                    MessageBox.Show("Errore di lettura: " & ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            End Using
        End Using

        ' --- FASE DI COSTRUZIONE E ORDINAMENTO ALBERO ---
        treeView.BeginUpdate()

        ' 1. Dividiamo le Radici (Livello 1) dai Figli (Livelli 2, 3, 4)
        Dim nodiRadice = listaElementi.Where(Function(x) String.IsNullOrEmpty(x.CodicePadre)).ToList()
        Dim nodiFigli = listaElementi.Where(Function(x) Not String.IsNullOrEmpty(x.CodicePadre)).ToList()

        ' 2. Ordiniamo le Radici convertendo i numeri romani
        Dim nodiRadiceOrdinati = nodiRadice.OrderBy(Function(x) EstraiChiaveOrdinamentoRomana(x.CodiceNodo)).ToList()

        ' 3. Inseriamo le Radici evitando duplicati
        For Each radice In nodiRadiceOrdinati
            If Not dizionarioNodi.ContainsKey(radice.CodiceNodo) Then
                Dim nuovoNodo As New TreeNode(radice.CodiceNodo & " - " & radice.Testo) With {.Name = radice.CodiceNodo}
                treeView.Nodes.Add(nuovoNodo)
                dizionarioNodi.Add(radice.CodiceNodo, nuovoNodo)
            End If
        Next

        ' 4. ORDINAMENTO CRUCIALE PER I FIGLI:
        ' Ordiniamo prima in base al numero di punti '.' presenti nel codice (Livello 2 ha 1 punto, Livello 3 ha 2 punti, Livello 4 ha 3 punti).
        ' In questo modo i padri di livello 2 e 3 verranno SEMPRE inseriti prima dei loro rispettivi figli di livello 3 e 4.
        ' Successivamente ordiniamo alfabeticamente/numericamente per il codice.
        Dim nodiFigliOrdinati = nodiFigli.
        OrderBy(Function(x) x.CodiceNodo.Count(Function(c) c = "."c)).
        ThenBy(Function(x) x.CodiceNodo.Length).
        ThenBy(Function(x) x.CodiceNodo).
        ToList()

        ' 5. Inseriamo i figli associandoli al rispettivo padre con controllo duplicati incorporato
        For Each figlio In nodiFigliOrdinati
            ' Se il nodo figlio è già stato inserito (duplicato nel DB), lo saltiamo per evitare l'errore ArgumentException
            If dizionarioNodi.ContainsKey(figlio.CodiceNodo) Then Continue For

            Dim nuovoNodo As New TreeNode(figlio.CodiceNodo & " - " & figlio.Testo) With {.Name = figlio.CodiceNodo}

            If dizionarioNodi.ContainsKey(figlio.CodicePadre) Then
                ' Associa il nodo al suo vero padre gerarchico
                dizionarioNodi(figlio.CodicePadre).Nodes.Add(nuovoNodo)
                dizionarioNodi.Add(figlio.CodiceNodo, nuovoNodo)
            Else
                ' Se il padre manca temporaneamente o è orfano, lo mette alla radice per sicurezza
                treeView.Nodes.Add(nuovoNodo)
                dizionarioNodi.Add(figlio.CodiceNodo, nuovoNodo)
            End If
        Next

        treeView.EndUpdate()
    End Sub

    Public Sub PopolaAlberoBambini()
        TreeView2.Nodes.Clear()
        '1)Trovo tutti i bambini
        '2)carico tutti i codici personalizzati 
        Dim risultati As List(Of Class_ANAGRAFICA) = Class_ANAGRAFICA.CercaPerCognome("", True)
        Gbl_Elenco_bambini = risultati
        ElencoVocipersonali.Clear()
        For Each bambino In Gbl_Elenco_bambini
            Dim vocepersonale As New Elenco_voci_personali
            vocepersonale.Bambino = bambino.NomeCompleto
            vocepersonale.DBambino = bambino.DBname
            vocepersonale.elenco = Class_ELCAR_VOCI_PERSONALI.Get_all(bambino.DBname)
            ElencoVocipersonali.Add(vocepersonale)
        Next
        ' 1. Pulisci la TreeView prima di popolarla
        TreeView2.Nodes.Clear()

        ' 2. Ciclo principale per scorrere l'elenco dei bambini (nodi principali)
        For i As Integer = 0 To ElencoVocipersonali.Count - 1
            Dim voceBambino As Elenco_voci_personali = ElencoVocipersonali(i)

            ' Crea il nodo principale con il nome del bambino
            Dim nodoBambino As New TreeNode(voceBambino.Bambino)

            ' Come Tag del nodo principale puoi mettere l'indice o l'oggetto stesso, se ti serve
            nodoBambino.Tag = Tuple.Create(Of Integer, Integer, String)(i, -1, "")
            ' 3. Ciclo secondario per scorrere le voci di quel bambino (sottonodi)
            ' (Assumo che gli elementi dentro .elenco abbiano le proprietà .ELCAR e .testo)
            For J As Integer = 0 To voceBambino.elenco.Count - 1

                'For Each singolaVoce In voceBambino.elenco

                ' Costruisci il testo del sottonodo come richiesto: "[ELCAR] testo"
                Dim testoNodo As String = "[" & voceBambino.elenco(J).ELCAR & "] " & voceBambino.elenco(J).Testo
                Dim nodoVoce As New TreeNode(testoNodo)

                ' Imposta come Tag l'indice della lista principale per ritrovarla facilmente
                Dim i0 = voceBambino.elenco(J).ID_SUB
                nodoVoce.Tag = Tuple.Create(Of Integer, Integer, String)(i, J, i0)
                ' Aggiunge il sottonodo al nodo del bambino corrente
                nodoBambino.Nodes.Add(nodoVoce)
                Next

                ' 4. Aggiunge il nodo del bambino (con tutti i suoi sottonodi) alla TreeView
                TreeView2.Nodes.Add(nodoBambino)
            Next

            ' Opzionale: Espande tutti i nodi dell'albero all'avvio
            TreeView2.ExpandAll()
    End Sub

    Private Class ElementoAlbero
        Public Property CodiceNodo As String
        Public Property CodicePadre As String
        Public Property Testo As String
    End Class

    Private Function EstraiChiaveOrdinamentoRomana(codice As String) As String
        If String.IsNullOrEmpty(codice) Then Return ""
        Dim parti As String() = codice.Split("."c)
        Dim parteRomana As String = parti(0).ToUpper()
        Dim valoreArabo As Integer = RomanoInArabo(parteRomana)

        If valoreArabo > 0 Then
            Dim restoCodice As String = If(parti.Length > 1, "." & parti(1), "")
            Return valoreArabo.ToString("D4") & restoCodice
        End If
        Return codice
    End Function

    Private Function RomanoInArabo(romano As String) As Integer
        Dim dizionarioRomano As New Dictionary(Of Char, Integer) From {
        {"I"c, 1}, {"V"c, 5}, {"X"c, 10}, {"L"c, 50}, {"C"c, 100}, {"D"c, 500}, {"M"c, 1000}
    }
        Dim totale As Integer = 0
        Dim valorePrecedente As Integer = 0
        For i As Integer = romano.Length - 1 To 0 Step -1
            Dim carattereCorrente As Char = romano(i)
            If dizionarioRomano.ContainsKey(carattereCorrente) Then
                Dim valoreCorrente As Integer = dizionarioRomano(carattereCorrente)
                If valoreCorrente < valorePrecedente Then
                    totale -= valoreCorrente
                Else
                    totale += valoreCorrente
                End If
                valorePrecedente = valoreCorrente
            Else
                Return 0
            End If
        Next
        Return totale
    End Function
    Sub Popola_treeview()
        Dim cn = "Data Source=" & ini.Database
        PopolaAlberoElcar(TreeView1, cn)

    End Sub
    Private Sub F_Gestione_Codici_ELCAR_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Edit_onoff(False)
        Popola_treeview()
        PopolaAlberoBambini()
    End Sub

    Private Sub TreeView1_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeView1.AfterSelect
        nodoSelezionato = e.Node
        Label3.Text = nodoSelezionato.Name
        Nodo_mostra(nodoSelezionato)
    End Sub
    Sub Nodo_mostra(nodoselezionato As TreeNode)
        Dim codice = nodoselezionato.Name
        'Estraggo il livello contando il numero di .
        Dim x = Split(codice, ".")
        Dim nvoci = x.Count
        If nvoci <= 1 Then
            'cancellare..
        ElseIf nvoci = 2 Then
            'ricerca su ELCAR
            Dim voce = Class_ELCAR.CercaPerCodice(codice)
            TextBox1.Text = voce(0).Testo
            TextBox2.Text = voce(0).Testo_Lungo
            Label2.Text = voce(0).ID
            CheckBox1.Visible = False
            Label2.Tag = ""
            Label9.Text = "ELCAR (0)"
            Label9.BackColor = MyColor.Silver
            Label9.Tag = "ELCAR"
        ElseIf nvoci = 3 Then
            'ricerca su ELCAR_VOCI
            codice = x(0).ToString + "." + x(1).ToString
            Label3.Tag = codice
            Dim id = x(2).ToString
            Dim voce = Class_ELCAR_VOCI.CercaPerCodice_Esatto(codice, id)
            TextBox1.Text = voce.Testo
            TextBox2.Text = voce.Testo_Lungo
            Label2.Text = voce.ID
            CheckBox1.Visible = False
            Label2.Tag = id
            Label9.Text = "ELCAR_VOCI (1)"
            Label9.Tag = "ELCAR_VOCI"
        ElseIf nvoci = 4 Then
            'ricerca su ELCAR:VOCI_SUB
            codice = x(0).ToString + "." + x(1).ToString + "." + x(2).ToString
            Label3.Tag = codice
            Dim id = x(3).ToString
            Dim voce = Class_ELCAR_VOCI_SUB.CercaPerCodice_Esatto(codice, id)
            If Not voce Is Nothing Then
                TextBox1.Text = voce.Testo
                TextBox2.Text = voce.Testo_Lungo
                Label2.Text = voce.ID
                CheckBox1.Visible = True
                CheckBox1.Checked = voce.Deleted
                Label2.Tag = id
                Label9.Text = "ELCAR_VOCI_SUB (2)"
                Label9.BackColor = MyColor.Silver
                Label9.Tag = "ELCAR_VOCI_SUB"
            End If

        ElseIf nvoci = 5 Then
            'ricerca su ELCAR:VOCI_SUB2
            codice = x(0).ToString + "." + x(1).ToString + "." + x(2).ToString + "." + x(3).ToString
            Label3.Tag = codice
            Dim id = x(4).ToString
            Dim voce = Class_ELCAR_VOCI_SUB2.CercaPerCodice_Esatto(codice, id)
            If Not voce Is Nothing Then
                TextBox1.Text = voce.Testo
                TextBox2.Text = voce.Testo_Lungo
                Label2.Text = voce.ID
                CheckBox1.Visible = True
                CheckBox1.Checked = voce.Deleted
                Label2.Tag = id
                Label9.Text = "PERSONALIZZATE"
                Label9.BackColor = MyColor.Yellow
                Label9.Tag = "ELCAR_VOCI_SUB2"
            End If

        End If


    End Sub

    Private Sub B_edit_Click(sender As Object, e As EventArgs) Handles B_edit.Click
        Edit_onoff(True)
        TextBox1.Focus()
    End Sub

    Private Sub B_salva_Click(sender As Object, e As EventArgs) Handles B_salva.Click
        Dim id = Val(Label2.Text), CODICE = Label3.Tag, CODICESUB = Label2.Tag
        Dim salvato As Boolean = True, m = ""
        If Label9.Tag = "ELCAR" Then
            Dim voce = Class_ELCAR.Load(id)
            voce.Testo = TextBox1.Text
            voce.Testo_Lungo = TextBox2.Text
            If Not voce.Save() Then salvato = False
            m = "Modifica ELCAR (" + CODICE + ")"
        ElseIf Label9.Tag = "ELCAR_VOCI" Then
            Dim voce = Class_ELCAR_VOCI.CercaPerCodice_Esatto(CODICE, CODICESUB)
            voce.Testo = TextBox1.Text
            voce.Testo_Lungo = TextBox2.Text
            If Not voce.Save() Then salvato = False
            m = "Modifica ELCAR_VOCI (" + CODICE + "." + CODICESUB + ")"
        ElseIf Label9.Tag = "ELCAR_VOCI_SUB" Then
            Dim voce = Class_ELCAR_VOCI_SUB.CercaPerCodice_Esatto(CODICE, CODICESUB)
            voce.Testo = TextBox1.Text
            voce.Testo_Lungo = TextBox2.Text
            voce.Deleted = CheckBox1.Checked
            If Not voce.Save() Then salvato = False
            m = "Modifica ELCAR_VOCI_SUB (" + CODICE + "." + CODICESUB + ")"

        ElseIf Label9.Tag = "ELCAR_VOCI_SUB2" Then
            Dim voce = Class_ELCAR_VOCI_SUB2.CercaPerCodice_Esatto(CODICE, CODICESUB)
            voce.Testo = TextBox1.Text
            voce.Testo_Lungo = TextBox2.Text
            voce.Deleted = CheckBox1.Checked
            If Not voce.Save() Then salvato = False
            m = "Modifica ELCAR_VOCI_SUB2 (" + CODICE + "." + CODICESUB + ")"
        ElseIf Label9.Tag = "VOCI_PERSONALI" Then
            Dim i = Val(TextBox2.Tag), j = Val(TextBox1.Tag)
            Dim dbbambino = ElencoVocipersonali(j).DBambino
            Dim k = ElencoVocipersonali(j).elenco(i).ID
            Dim voce = Class_ELCAR_VOCI_PERSONALI.Load(k, dbbambino)
            voce.Testo = TextBox1.Text
            voce.Testo_Lungo = TextBox2.Text
            voce.Deleted = CheckBox1.Checked
            If Not voce.Save(dbbambino) Then salvato = False
            CODICESUB = Label2.Tag
            m = "Modifica VOCI_PERSONALI (" + ElencoVocipersonali(j).Bambino + "." + CODICESUB + ")"
        End If

        If Not salvato Then
            APPEND_LOG(m, ini.File_Log, True, "E")
            Call MsgBox("Problemi durante il salvataggio", vbOKOnly + vbCritical)
        Else
            APPEND_LOG(m, ini.File_Log, True, "1")
            nodoSelezionato.Text = Label3.Text + " - " + TextBox1.Text
            nodoSelezionato.ForeColor = MyColor.Red
        End If
        Edit_onoff(False)
    End Sub

    Private Sub B_esci_Click(sender As Object, e As EventArgs) Handles B_esci.Click
        Edit_onoff(False)
    End Sub


    Private Sub TextBox1_GotFocus(sender As Object, e As EventArgs) Handles TextBox1.GotFocus
        If TextBox1.ReadOnly Then Exit Sub
        TextBox1.BackColor = MyColor.LightYellow
    End Sub

    Private Sub TextBox2_LostFocus(sender As Object, e As EventArgs) Handles TextBox2.LostFocus
        TextBox2.BackColor = SystemColors.Control

    End Sub

    Private Sub TextBox2_GotFocus(sender As Object, e As EventArgs) Handles TextBox2.GotFocus
        If TextBox2.ReadOnly Then Exit Sub
        TextBox2.BackColor = MyColor.LightYellow

    End Sub

    Private Sub TextBox1_LostFocus(sender As Object, e As EventArgs) Handles TextBox1.LostFocus
        TextBox1.BackColor = SystemColors.Control

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()

    End Sub


    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        If TabControl1.SelectedTab.Name = "TabPage1" Then
            B_esporta.Visible = True
        Else
            B_esporta.Visible = False
        End If

    End Sub

    Private Sub TreeView2_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeView2.AfterSelect
        nodoSelezionato = e.Node
        Dim RET = e.Node.Tag
        Dim J = RET.item1
        Dim I = RET.item2
        Label2.Text = RET.item3
        Label9.Text = ElencoVocipersonali(J).Bambino
        Dim x = Split(nodoSelezionato.Text, "]")
        Label3.Text = x(0) + "]"
        TextBox1.Text = ElencoVocipersonali(J).elenco(I).Testo
        TextBox1.Tag = J
        TextBox2.Text = ElencoVocipersonali(J).elenco(I).Testo_Lungo
        TextBox2.Tag = I
        Label2.Tag = x(0) + "." + RET.item3 + "]"

        CheckBox1.Visible = True
        CheckBox1.Checked = ElencoVocipersonali(J).elenco(I).Deleted
        Label9.BackColor = MyColor.LightGreen
        Label9.Tag = "VOCI_PERSONALI"


    End Sub
End Class