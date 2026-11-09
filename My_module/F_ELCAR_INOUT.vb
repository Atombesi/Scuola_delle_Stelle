Imports System.IO

Public Class F_ELCAR_INOUT
    Dim Nuova_voce_ELCAR As New Class_ELCAR_VOCI
    Dim Nuova_lista_ELCAR As New List(Of Class_ELCAR_VOCI)
    Dim Nuova_voce_SUB As New Class_ELCAR_VOCI_SUB
    Dim Nuova_lista_SUB As New List(Of Class_ELCAR_VOCI_SUB)
    Dim Nuova_voce_SUB2 As New Class_ELCAR_VOCI_SUB2
    Dim Nuova_lista_SUB2 As New List(Of Class_ELCAR_VOCI_SUB2)
    Dim VociAttuali_ELCAR As New List(Of Class_ELCAR_VOCI)
    Dim VociAttuali_SUB As New List(Of Class_ELCAR_VOCI_SUB)
    Dim VociAttuali_SUB2 As New List(Of Class_ELCAR_VOCI_SUB2)
    Dim voci_eliminate As String = ""
    Dim voci_modificate As String = ""


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Hide()
    End Sub
    Sub importaONOFF(Optional importa As Boolean = False)
        If importa Then
            Me.Height = 330
            Button2.Top = 260
            Button1.Enabled = False
            Button4.Enabled = False
            GroupBox1.Visible = True
        Else
            Me.Height = 176
            Button2.Top = 85
            Button1.Enabled = True
            Button4.Enabled = True
            GroupBox1.Visible = False
        End If
    End Sub
    Private Sub F_ELCAR_INOUT_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        importaONOFF()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        importaONOFF(True)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        importaONOFF(False)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim m = "Vuoi esportare in files CSV tutte le voci ELCAR?"
        If MsgBox(m, vbYesNo + vbQuestion, "Esporta voci ELCAR") <> vbYes Then Exit Sub
        Esporta_voci_ELCAR()
        m = "Esportazioni Elenco voci ELCAR"
        APPEND_LOG(m, ini.File_Log, True)
    End Sub

    Sub Esporta_voci_ELCAR(Optional tipo As String = "ALL")
        'Preparo Elenco VOCI_ELCAR
        Dim listaVoci As List(Of Class_ELCAR_VOCI) = Class_ELCAR_VOCI.GetALL_ByID
        'Preparo Elenco VOCI_ELCAR_SUB
        Dim listaVocisub As List(Of Class_ELCAR_VOCI_SUB) = Class_ELCAR_VOCI_SUB.GetALL_ByID
        'Preparo Elenco VOCI_ELCAR_SUB 2
        Dim listaVocisub2 As List(Of Class_ELCAR_VOCI_SUB2) = Class_ELCAR_VOCI_SUB2.GetALL_ByID
        Dim path = IO.Path.GetDirectoryName(ini.Database)
        Dim m1 = ""

        If tipo = "ALL" Or tipo = "ELCAR" Then
            'Esporto in CSV VOCI_ELCAR
            Dim filePath As String = IO.Path.Combine(path, "Voci_ELCAR.csv")
            Using writer As New IO.StreamWriter(filePath)
                ' Scrive l'intestazione del file CSV
                writer.WriteLine("ID;CODICE_ELCAR;CODICE_VOCE;TESTO;TESTO_LUNGO")
                ' Scrive i dati delle voci ELCAR nel file CSV
                For Each voce In listaVoci
                    writer.WriteLine($"{voce.ID};{voce.CODICE_ELCAR};{voce.CODICE_VOCE};{voce.Testo};{voce.Testo_Lungo}")
                Next
            End Using
            m1 = "VOCI ELCAR esportatei in:" + vbCrLf + filePath
        End If

        If tipo = "ALL" Or tipo = "SUB" Then
            'Esporto in CSV VOCI_ELCAR_SUB
            Dim filePath = IO.Path.Combine(path, "Voci_ELCAR_SUB.csv")
            Using writer As New IO.StreamWriter(filePath)
                ' Scrive l'intestazione del file CSV
                writer.WriteLine("ID;ELCAR;ID_SUB;TESTO;TESTO_LUNGO;Deleted")
                ' Scrive i dati delle voci ELCAR nel file CSV
                For Each voce In listaVocisub
                    writer.WriteLine($"{voce.ID};{voce.ELCAR};{voce.ID_SUB};{voce.Testo};{voce.Testo_Lungo};{voce.Deleted}")
                Next
            End Using
            m1 = "VOCI ELCAR_SUB esportate in:" + vbCrLf + filePath
        End If

        If tipo = "ALL" Or tipo = "SUB2" Then
            'Esporto in CSV VOCI_ELCAR_SUB2
            Dim filePath = IO.Path.Combine(path, "Voci_ELCAR_SUB2.csv")
            Using writer As New IO.StreamWriter(filePath)
                ' Scrive l'intestazione del file CSV
                writer.WriteLine("ID;ELCAR;ID_SUB;TESTO;TESTO_LUNGO;Deleted")
                ' Scrive i dati delle voci ELCAR nel file CSV
                For Each voce In listaVocisub2
                    writer.WriteLine($"{voce.ID};{voce.ELCAR};{voce.ID_SUB};{voce.Testo};{voce.Testo_Lungo};{voce.Deleted}")
                Next
            End Using
            m1 = "VOCI ELCAR SUB2 esportate in:" + vbCrLf + filePath
        End If

        If tipo = "ALL" Then
            m1 = "3 File CSV esportati in:" + vbCrLf + path
        End If
        MsgBox(m1, vbOK + vbInformation, "Esportazione voci ELCAR")

        importaONOFF(False)
    End Sub

    Sub Importa_voci_ELCAR(tipo As String)
        '1) mostro informazioni sulla procedura di mportazione 
        '2) verifico esistenza del file da caricare
        '3) leggo il file e lo carico nella variabile
        '4) verifico la congruita del file letto
        '5) Carico la classe con i valori letti
        '6) eseguo alcuni controlli con le voci già presenti nel database
        '7) Esporto la tabella in un file CSV di backup
        '8) svuoto la tabella
        '9) carico le voci lette dal file CSV

        '-- fase 1 mostro informazioni sulla procedura di mportazione 
        Dim f = "", nvoci = 0, riga0 = ""

        If tipo = "ELCAR" Then
            f = "NEW_VOCI_ELCAR.CSV"
            riga0 = "ID;CODICE_ELCAR;CODICE_VOCE;TESTO;TESTO_LUNGO"
        End If
        If tipo = "SUB" Then
            f = "NEW_VOCI_ELCAR_SUB.CSV"
            riga0 = "ID;ELCAR;ID_SUB;TESTO;TESTO_LUNGO;Deleted"
        End If
        If tipo = "SUB2" Then
            f = "NEW_VOCI_ELCAR_SUB2.CSV"
            riga0 = "ID;ELCAR;ID_SUB;TESTO;TESTO_LUNGO;Deleted"
        End If
        If f = "" Then Exit Sub

        Dim t = "Conferma importazione"
        Dim m = "ATTENZIONE: Tutte le voci ELCAR presenti nel database saranno eliminate e sostituite con quelle lette!" + vbCrLf + vbCrLf
        m = m + "I dati verranno importati dal file CSV (;): " + vbCrLf + f + vbCrLf
        If MsgBox(m, vbYesNo + vbQuestion, t) <> vbYes Then Exit Sub

        '-- fase 2 verifico esistenza del file da caricare

        Dim path = IO.Path.GetDirectoryName(ini.Database)
        f = IO.Path.Combine(path, f)
        If Not IO.File.Exists(f) Then
            m = "Il file da importare" + vbCrLf + f + vbCrLf + "NON è stato trovato!"
            MsgBox(m + vbCrLf + f, vbOK + vbCritical, "File non trovato ")
            Exit Sub
        End If

        '--- fase 3 leggo il file e lo carico in un array 
        Dim listavoci As New List(Of String)
        Using reader As New StreamReader(f)
            Dim riga As String = reader.ReadLine()
            ' 3. Cicliamo finché ci sono righe nel file
            While riga IsNot Nothing
                ' Saltiamo eventuali righe vuote di sicurezza
                If Not String.IsNullOrWhiteSpace(riga) Then
                    listavoci.Add(riga)
                End If
                riga = reader.ReadLine()
            End While
        End Using


        '--- fase 4 Verifico corretta formattazione dei dati letti 
        If listavoci.Count < 1 Then
            Call MsgBox("Il file importato non contiene voci", vbCritical + vbOKOnly)
            Exit Sub
        End If
        Dim voce = listavoci(0)
        If voce <> riga0 Then
            m = "L'intestazione delle colonne NON è conforme!" + vbCrLf + vbCrLf
            m = m + "Orig:" + riga0 + vbCrLf
            m = m + "Read:" + voce + vbCrLf
            t = "Intestazione colonne NON conforme"
            Call MsgBox(m, vbCritical + vbOKOnly)
            Exit Sub
        End If


        Dim num_attuale = 0, num_nuovo = 0
        Dim listcontrolli As New List(Of String)


        If tipo = "ELCAR" Then
            Nuova_lista_ELCAR.Clear()
            For Each voce In listavoci
                'ID;CODICE_ELCAR;CODICE_VOCE;TESTO;TESTO_LUNGO
                Dim Nuova_voce_ELCAR = New Class_ELCAR_VOCI
                Dim x = Split(voce, ";")
                Nuova_voce_ELCAR.ID = Val(x(0))
                Nuova_voce_ELCAR.CODICE_ELCAR = x(1)
                Nuova_voce_ELCAR.CODICE_VOCE = x(2)
                Nuova_voce_ELCAR.Testo = x(3)
                Nuova_voce_ELCAR.Testo_Lungo = x(4)
                Nuova_lista_ELCAR.Add(Nuova_voce_ELCAR)
            Next
            VociAttuali_ELCAR = Class_ELCAR_VOCI.GetALL_ByID
            num_attuale = VociAttuali_ELCAR.Count
            num_nuovo = Nuova_lista_ELCAR.Count - 1   ' tolgo l'intestazione 
            '6 Eseguo i controlli incrociati
            listcontrolli = Controlli_Incrociati(tipo)
            If Not MostraControlliEConferma(listcontrolli) Then
                Exit Sub
            End If
            importa_nuove_voci_ELCAR(tipo)
        ElseIf tipo = "SUB" Then
            Nuova_lista_SUB.Clear()
            For Each voce In listavoci
                'ID;ELCAR;ID_SUB;TESTO;TESTO_LUNGO;Deleted
                Dim Nuova_voce_SUB As New Class_ELCAR_VOCI_SUB
                Dim x = Split(voce, ";")
                Nuova_voce_SUB.ID = Val(x(0))
                Nuova_voce_SUB.ELCAR = x(1)
                Nuova_voce_SUB.ID_SUB = x(2)
                Nuova_voce_SUB.Testo = x(3)
                Nuova_voce_SUB.Testo_Lungo = x(4)
                Nuova_voce_SUB.Deleted = CBool(x(5))
                Nuova_lista_SUB.Add(Nuova_voce_SUB)
            Next
            VociAttuali_SUB = Class_ELCAR_VOCI_SUB.GetALL_ByID
            num_attuale = VociAttuali_SUB.Count
            num_nuovo = Nuova_lista_SUB.Count - 1   ' tolgo l'intestazione 
        ElseIf tipo = "SUB2" Then
            Nuova_lista_SUB2.Clear()
            For Each voce In listavoci
                Dim Nuova_voce_SUB2 As New Class_ELCAR_VOCI_SUB2
                'ID;ELCAR;ID_SUB;TESTO;TESTO_LUNGO;Deleted
                Dim x = Split(voce, ";")
                Nuova_voce_SUB2.ID = Val(x(0))
                Nuova_voce_SUB2.ELCAR = x(1)
                Nuova_voce_SUB2.ID_SUB = x(2)
                Nuova_voce_SUB2.Testo = x(3)
                Nuova_voce_SUB2.Testo_Lungo = x(4)
                Nuova_voce_SUB2.Deleted = CBool(x(5))
                Nuova_lista_SUB2.Add(Nuova_voce_SUB2)
            Next
            VociAttuali_SUB2 = Class_ELCAR_VOCI_SUB2.GetALL_ByID
            num_attuale = VociAttuali_SUB.Count
            num_nuovo = Nuova_lista_SUB.Count - 1   ' tolgo l'intestazione 
        End If
    End Sub
    Public Function importa_nuove_voci_ELCAR(tipo) As Boolean

        If tipo = "ELCAR" Then
            'fase 1 Aggiungo tutte le voci non presenti
            'fase 2 Modifico eventualmente il testo delle voci esistente 
            'fase 3 Elimino eventuali voci non piu presenti


            Dim v_orig As New Class_ELCAR_VOCI
            'Controllo le voci gia presenti
            'elenco.Add("")
            'For Each v_nuova In Nuova_lista_ELCAR
            '    trovato = False
            '    x2 = v_nuova.CODICE_ELCAR + "." + v_nuova.CODICE_VOCE
            '    For Each v_orig In VociAttuali_ELCAR
            '        x1 = v_orig.CODICE_ELCAR + "." + v_orig.CODICE_VOCE
            '        If x1 = x2 Then
            '            'Codice trovato
            '            trovato = True
            '            Exit For
            '        End If
        End If
    End Function
    Public Function MostraControlliEConferma(lista As List(Of String)) As Boolean
        ' 1. Configurazione della Finestra temporanea
        Dim f As New Form()
        f.Text = $"Verifica Controlli ({lista.Count} elementi da esaminare)"
        f.Size = New Size(600, 500) ' Finestra bella capiente
        f.StartPosition = FormStartPosition.CenterScreen
        f.FormBorderStyle = FormBorderStyle.FixedDialog
        f.MaximizeBox = False
        f.MinimizeBox = False

        ' 2. Label informativa in alto
        Dim lbl As New Label()
        lbl.Text = "Elenco delle modifiche nelle voci. Scorri la lista e conferma se desideri procedere:"
        lbl.Location = New Point(15, 15)
        lbl.Size = New Size(550, 20)
        lbl.Font = New Font(lbl.Font, FontStyle.Bold)
        f.Controls.Add(lbl)

        ' 3. Casella di testo SCORREVOLE per contenere anche migliaia di righe
        Dim txt As New TextBox()
        txt.Multiline = True
        txt.ScrollBars = ScrollBars.Both ' Barra di scorrimento verticale E ORIZZONTALE sempre attiva
        txt.ReadOnly = True                  ' L'utente può solo leggere, non modificare
        txt.BackColor = Color.White          ' Sfondo bianco pulito
        txt.Text = String.Join(Environment.NewLine, lista)
        txt.Location = New Point(15, 45)
        txt.Size = New Size(555, 340)        ' Ampio spazio per lo scorrimento
        f.Controls.Add(txt)

        ' 4. Pulsante SÌ (Procedi)
        Dim btnYes As New Button()
        btnYes.Text = "Sì, procedi"
        btnYes.DialogResult = DialogResult.Yes
        btnYes.Location = New Point(160, 405)
        btnYes.Size = New Size(130, 40)
        btnYes.Font = New Font(btnYes.Font.FontFamily, 10, FontStyle.Regular)
        f.Controls.Add(btnYes)

        ' 5. Pulsante NO (Annulla ed Esci)
        Dim btnNo As New Button()
        btnNo.Text = "No, ferma tutto"
        btnNo.DialogResult = DialogResult.No
        btnNo.Location = New Point(310, 405)
        btnNo.Size = New Size(130, 40)
        btnNo.Font = New Font(btnNo.Font.FontFamily, 10, FontStyle.Regular)
        f.Controls.Add(btnNo)

        ' Associazioni tastiera: Invio esegue Sì, Esc esegue No
        f.AcceptButton = btnYes
        f.CancelButton = btnNo

        ' 6. Mostra la finestra e blocca l'esecuzione finché l'utente non sceglie
        Dim risultato As DialogResult = f.ShowDialog()

        ' Rilascia la memoria della form subito dopo la chiusura
        f.Dispose()

        ' Ritorna True se l'utente ha premuto "Sì, procedi"
        Return (risultato = DialogResult.Yes)
    End Function

    Function Controlli_Incrociati(tipo) As List(Of String)
        '--- fase 6 Mostro i risultati dei controlli incrociati


        Dim n_identiche = 0, N_nuove = 0, N_eliminate = 0, trovato = False, n_modificate = 0
        Dim x1 = "", x2 = "", elenco As New List(Of String), x = ""
        Dim voci_nuove As String, voci_eliminate As String, voci_modificate As String
        Dim elenco_voci_nuove As New List(Of String)
        Dim elenco_voci_eliminate As New List(Of String)
        Dim elenco_voci_modificate As New List(Of String)

        If tipo = "ELCAR" Then
            Dim v_orig As New Class_ELCAR_VOCI
            'Controllo le voci gia presenti
            For Each v_nuova In Nuova_lista_ELCAR
                trovato = False
                x2 = v_nuova.CODICE_ELCAR + "." + v_nuova.CODICE_VOCE
                If x2 = "CODICE_ELCAR.CODICE_VOCE" Then Continue For
                For Each v_orig In VociAttuali_ELCAR
                    x1 = v_orig.CODICE_ELCAR + "." + v_orig.CODICE_VOCE
                    If x1 = x2 Then
                        trovato = True 'Codice trovato
                        Exit For
                    End If
                Next
                If Not trovato Then
                    N_nuove = N_nuove + 1
                    x = " [NUOVA];" + v_nuova.ID.ToString + "; (" + x1 + ") " + v_nuova.Testo
                    elenco_voci_nuove.Add(x)
                Else
                    'la voce nuova è stata trovata, controllo il testo 
                    If v_nuova.Testo.ToUpper <> v_orig.Testo.ToUpper Then
                        n_modificate = n_modificate + 1
                        x = "[MODIFICATA];" + v_orig.ID.ToString + "; (" + x1 + ") " + v_nuova.Testo
                        elenco_voci_modificate.Add(x)
                        voci_modificate = voci_modificate + ";" + v_orig.ID.ToString
                    Else
                        n_identiche = n_identiche + 1
                    End If
                End If
            Next
            ' controllo se qualche voce esistente è stata eliminata 
            For Each v_orig In VociAttuali_ELCAR
                x1 = v_orig.CODICE_ELCAR + "." + v_orig.CODICE_VOCE
                trovato = False
                For Each v_nuova In Nuova_lista_ELCAR
                    x2 = v_nuova.CODICE_ELCAR + "." + v_nuova.CODICE_VOCE
                    If x1 = x2 Then
                        trovato = True
                        Exit For
                    End If
                Next
                If Not trovato Then
                    'la voce originale non e' piu presente
                    N_eliminate = N_eliminate + 1
                    x = "[ELIMINATA];" + v_orig.ID.ToString + "; (" + x1 + ") " + v_orig.Testo
                    elenco_voci_eliminate.Add(x)
                    voci_eliminate = voci_eliminate + ";" + v_orig.ID.ToString
                End If
            Next
            elenco.Clear()
            x = "(voci nuove: " + N_nuove.ToString + ")  (voci eliminate: " + N_eliminate.ToString + ")  (voci modificate: " + n_modificate.ToString + ") "
            elenco.Add(x)
            For Each x In elenco_voci_nuove : elenco.Add(x) : Next
            For Each x In elenco_voci_modificate : elenco.Add(x) : Next
            For Each x In elenco_voci_eliminate : elenco.Add(x) : Next


            Return elenco
        End If

    End Function

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If RadioButton1.Checked Then
            Importa_voci_ELCAR("ELCAR")
        ElseIf RadioButton2.Checked Then
            Importa_voci_ELCAR("SUB")
        ElseIf RadioButton3.Checked Then
            Importa_voci_ELCAR("SUB2")
        End If

    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged, RadioButton2.CheckedChanged, RadioButton3.CheckedChanged
        Button3.Enabled = True
    End Sub
End Class