Imports System.IO
Imports System.Text

Module Funzioni_comuni_2026
    Private Declare Auto Function GetPrivateProfileString Lib “kernel32.dll” (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
    Private Declare Auto Function WritePrivateProfileString Lib “kernel32.dll” (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer
    Public Gb_IsDebug As Boolean = False
    Public Function ReadINIString(ByVal Filename As String, ByVal Section As String, ByVal Key As String, Optional ByVal lpDefault As String = “”, Optional ByVal bRaiseError As Boolean = False) As String
        Dim RetVal As String = New String(” “, 255)
        Dim LenResult As Integer
        Dim ErrString As String

        LenResult = GetPrivateProfileString(Section, Key, "!-!", RetVal, RetVal.Length, Filename)
        If InStr(RetVal, "!-!") = 1 Then
            WriteINIString(Filename, Section, Key, lpDefault)
            LenResult = GetPrivateProfileString(Section, Key, "!-!", RetVal, RetVal.Length, Filename)
        End If

        If LenResult = 0 AndAlso bRaiseError Then
            If Not (System.IO.File.Exists(Filename)) Then
                ErrString = “Impossibile aprire il file INI” & Filename
            Else
                ErrString = “La sezione o la chiave sono errate oppure l'accesso al file non è consentito”
            End If
            Throw New Exception(ErrString)
        End If


        Return RetVal.Substring(0, LenResult)

    End Function

    Friend Function WriteINIString(ByVal Filename As String, ByVal Section As String, ByVal Key As String, ByVal Value As String, Optional ByVal bRaiseError As Boolean = False) As Boolean

        Dim LenResult As Integer

        Dim ErrString As String

        LenResult = WritePrivateProfileString(Section, Key, Value, Filename)

        If LenResult = 0 And bRaiseError Then

            If Not (System.IO.File.Exists(Filename)) Then

                ErrString = “Impossibile aprire il file INI” & Filename

            Else

                ErrString = “Accesso al file non consentito”

            End If

            Throw New Exception(ErrString)

        End If

        Return IIf(LenResult = 0, False, True)

        End

    End Function

    Public Function dir_bar(ByVal testo As String) As String
        ' Se la stringa è vuota o nulla, restituisce solo un backslash
        If String.IsNullOrEmpty(testo) Then Return "\"

        ' Rimuove tutti i backslash finali (se presenti) e ne aggiunge esattamente uno
        Return testo.TrimEnd("\"c) & "\"
    End Function


    Public Function extr_dir(ByVal fullname As String) As String
        ' Se la stringa è vuota, restituisce una stringa vuota
        If String.IsNullOrEmpty(fullname) Then Return String.Empty

        ' GetDirectoryName estrae il percorso escludendo il nome del file 
        ' e omette nativamente il backslash finale.
        Dim path As String = IO.Path.GetDirectoryName(fullname)

        ' Se il file si trova nella root (es. "C:\file.txt"), GetDirectoryName restituisce "C:\"
        ' Se vogliamo togliere il backslash anche in quel caso, usiamo TrimEnd
        Return path.TrimEnd("\"c)
    End Function

    Public Function CorreggiCodificaTesto(testoErrato As String) As String
        If String.IsNullOrEmpty(testoErrato) Then Return String.Empty

        Try
            ' 1. Definisce la codifica Windows-1252 (quella letta erroneamente da Excel/DGV)
            Dim win1252 As Encoding = Encoding.GetEncoding("Windows-1252")

            ' 2. Definisce la codifica UTF-8 (quella originale del testo)
            Dim utf8 As Encoding = Encoding.UTF8

            ' 3. Converte la stringa errata in un array di byte usando Windows-1252
            Dim byteSbagliati As Byte() = win1252.GetBytes(testoErrato)

            ' 4. Riconverte i byte nella stringa corretta usando UTF-8
            Return utf8.GetString(byteSbagliati)
        Catch ex As Exception
            ' In caso di errore imprevisto, restituisce il testo originale
            Return testoErrato
        End Try
    End Function

    Public Function OttieniPrimoIndicediunacombo(cbo As ComboBox, testoCercato As String) As Integer
        For i As Integer = 0 To cbo.Items.Count - 1
            ' Estrae il testo dell'elemento corrente
            Dim testoItem As String = cbo.GetItemText(cbo.Items(i))

            ' Verifica se contiene la sotto-stringa (ignorando maiuscole/minuscole)
            If testoItem.IndexOf(testoCercato, StringComparison.OrdinalIgnoreCase) >= 0 Then
                Return i ' Ritorna immediatamente l'indice trovato
            End If
        Next
        Return -1 ' Nessun elemento corrisponde alla ricerca
    End Function

    Public Function InputBoxMultiLine_old(titolo As String, messaggio As String, Optional testoPredefinito As String = "") As String
        Using f As New Form()
            f.Text = titolo
            f.Width = 400
            f.Height = 300
            f.FormBorderStyle = FormBorderStyle.FixedDialog
            f.MaximizeBox = False
            f.MinimizeBox = False
            f.StartPosition = FormStartPosition.CenterParent

            Dim lbl As New Label() With {.Text = messaggio, .Left = 10, .Top = 10, .Width = 360, .Height = 30}
            f.Controls.Add(lbl)

            ' Configurazione TextBox per accettare l'andare a capo
            Dim txt As New TextBox()
            txt.Multiline = True
            txt.AcceptsReturn = True ' <--- FONDAMENTALE: permette all'Invio di andare a capo nella TextBox
            txt.ScrollBars = ScrollBars.Vertical
            txt.Left = 10
            txt.Top = 45
            txt.Width = 360
            txt.Height = 160
            txt.Text = testoPredefinito
            txt.SelectionStart = txt.Text.Length
            f.Controls.Add(txt)

            ' Pulsante OK (rimossa l'associazione con f.AcceptButton)
            Dim btnOk As New Button() With {.Text = "OK", .Left = 200, .Top = 215, .Width = 80, .DialogResult = DialogResult.OK}
            f.Controls.Add(btnOk)

            ' Pulsante Annulla
            Dim btnCancel As New Button() With {.Text = "Annulla", .Left = 290, .Top = 215, .Width = 80, .DialogResult = DialogResult.Cancel}
            f.Controls.Add(btnCancel)
            f.CancelButton = btnCancel ' Questo puoi lasciarlo: il tasto ESC chiuderà la finestra come "Annulla"

            If f.ShowDialog() = DialogResult.OK Then
                Return txt.Text
            Else
                Return ""
            End If
        End Using
    End Function
    Public Function InputBoxMultiLine_2(titolo As String, messaggio As String, Optional testoPredefinito As String = "", Optional maxLinee As Integer = 2, Optional maxCaratteri As Integer = 80) As String
        Using f As New Form()
            f.Text = titolo
            f.Width = 400
            f.Height = 300
            f.FormBorderStyle = FormBorderStyle.FixedDialog
            f.MaximizeBox = False
            f.MinimizeBox = False
            f.StartPosition = FormStartPosition.CenterParent

            Dim lbl As New Label() With {.Text = messaggio, .Left = 10, .Top = 10, .Width = 360, .Height = 30}
            f.Controls.Add(lbl)

            Dim txt As New TextBox()
            txt.Multiline = True
            txt.AcceptsReturn = True
            txt.ScrollBars = ScrollBars.Vertical
            txt.Left = 10
            txt.Top = 45
            txt.Width = 360
            txt.Height = 160

            ' Imposta il limite massimo di caratteri totali
            txt.MaxLength = maxCaratteri

            txt.Text = testoPredefinito
            txt.SelectionStart = txt.Text.Length
            f.Controls.Add(txt)

            ' GESTIONE DEL LIMITE DELLE LINEE MASSIME
            ' Questo evento intercetta la tastiera in tempo reale
            AddHandler txt.KeyPress, Sub(sender As Object, e As KeyPressEventArgs)
                                         ' Se l'utente preme INVIO (codice carattere 13)
                                         If e.KeyChar = Convert.ToChar(13) Then
                                             ' Conta quante linee ci sono attualmente nella TextBox
                                             If txt.Lines.Length >= maxLinee Then
                                                 ' Blocca l'inserimento del carattere di a capo
                                                 e.Handled = True
                                                 ' Segnale acustico di avviso (opzionale)
                                                 System.Media.SystemSounds.Beep.Play()
                                             End If
                                         End If
                                     End Sub

            Dim btnOk As New Button() With {.Text = "OK", .Left = 200, .Top = 215, .Width = 80, .DialogResult = DialogResult.OK}
            f.Controls.Add(btnOk)

            Dim btnCancel As New Button() With {.Text = "Annulla", .Left = 290, .Top = 215, .Width = 80, .DialogResult = DialogResult.Cancel}
            f.Controls.Add(btnCancel)
            f.CancelButton = btnCancel

            If f.ShowDialog() = DialogResult.OK Then
                Return txt.Text
            Else
                Return ""
            End If
        End Using
    End Function
    Public Function CalcolaAltezzaOttimaleTextBox(maxLinee As Integer) As Integer
        ' Ogni linea di testo occupa circa 22 pixel. Aggiungiamo 8 pixel di margine interno per la TextBox.
        Dim altezzaCalcolata As Integer = (maxLinee * 22) + 8

        ' Impostiamo un limite minimo di 45px (circa 2 linee) e massimo di 300px per evitare finestre giganti
        Return Math.Max(45, Math.Min(300, altezzaCalcolata))
    End Function
    Public Function InputBoxMultiLine(titolo As String, messaggio As String, altezzaTextBoxPixel As Integer, Optional testoPredefinito As String = "", Optional maxLinee As Integer = 5, Optional maxCaratteri As Integer = 500) As String
        Using f As New Form()
            ' Calcola l'altezza totale della Form partendo dall'altezza della TextBox passata come parametro
            Dim altezzaForm As Integer = 45 + altezzaTextBoxPixel + 75

            f.Text = titolo
            f.Width = 400
            f.Height = altezzaForm
            f.FormBorderStyle = FormBorderStyle.FixedDialog
            f.MaximizeBox = False
            f.MinimizeBox = False
            f.StartPosition = FormStartPosition.CenterParent

            Dim lbl As New Label() With {.Text = messaggio, .Left = 10, .Top = 10, .Width = 360, .Height = 30}
            f.Controls.Add(lbl)

            Dim txt As New TextBox()
            txt.Multiline = True
            txt.AcceptsReturn = True
            txt.ScrollBars = ScrollBars.Vertical
            txt.Left = 10
            txt.Top = 45
            txt.Width = 360
            txt.Height = altezzaTextBoxPixel ' <-- Altezza passata manualmente
            txt.MaxLength = maxCaratteri
            txt.Text = testoPredefinito
            txt.SelectionStart = txt.Text.Length
            f.Controls.Add(txt)

            ' Gestione limite linee da tastiera
            AddHandler txt.KeyPress, Sub(sender As Object, e As KeyPressEventArgs)
                                         If e.KeyChar = Convert.ToChar(13) Then
                                             If txt.Lines.Length >= maxLinee Then
                                                 e.Handled = True
                                                 System.Media.SystemSounds.Beep.Play()
                                             End If
                                         End If
                                     End Sub

            ' Posiziona i pulsanti dinamicamente sotto la TextBox
            Dim coordinataTopPulsanti As Integer = txt.Top + txt.Height + 12

            Dim btnOk As New Button() With {.Text = "OK", .Left = 200, .Top = coordinataTopPulsanti, .Width = 80, .DialogResult = DialogResult.OK}
            f.Controls.Add(btnOk)

            Dim btnCancel As New Button() With {.Text = "Annulla", .Left = 290, .Top = coordinataTopPulsanti, .Width = 80, .DialogResult = DialogResult.Cancel}
            f.Controls.Add(btnCancel)
            f.CancelButton = btnCancel

            If f.ShowDialog() = DialogResult.OK Then
                Return txt.Text
            Else
                Return ""
            End If
        End Using
    End Function

    Public Sub APPEND_LOG_OLD(msg_LOG$, LOG_FILE$, Optional HDR As Boolean = False)
        REM ===== Appendi messaggio  al log_file ==========
        If LOG_FILE$ = "" Then Exit Sub
        'Dim ch1% = FreeFile()
        Dim a$ = ""

        If Dir(LOG_FILE$) = "" Then Cycle_LOG(LOG_FILE$) ' se non esiste crea
        'Open LOG_FILE$ For Append As #ch1%
        Select Case HDR
            Case True
                If Gb_IsDebug Then
                    a$ = "#Debug" + Format$(Now, "dd-MM-yyyy HH:mm") + "," + msg_LOG$ + vbCr
                Else
                    a$ = "#" + Format$(Now, "dd-MM-yyyy HH:mm") + "," + msg_LOG$ + vbCr
                End If
            Case False
                a$ = msg_LOG$ + vbCr
        End Select
        'Print #ch1%, a$
        'Close #ch1%
        Try
            IO.File.AppendAllText(LOG_FILE, a$ + vbCrLf)

        Catch ex As Exception

        End Try


    End Sub
    Public Sub APPEND_LOG(msg_LOG As String, LOG_FILE As String, Optional HDR As Boolean = False)
        ' ===== Appendi messaggio al log_file ==========
        If String.IsNullOrEmpty(LOG_FILE) Then Exit Sub

        ' Se il file non esiste, invoca la routine per crearlo
        If Not IO.File.Exists(LOG_FILE) Then Cycle_LOG(LOG_FILE)

        Dim rigaLog As String = ""
        Dim dataFormattata As String = DateTime.Now.ToString("dd-MM-yyyy HH:mm")

        Select Case HDR
            Case True
                If Gb_IsDebug Then
                    rigaLog = $"#Debug{dataFormattata},{msg_LOG}"
                Else
                    rigaLog = $"#{dataFormattata},{msg_LOG}"
                End If
            Case False
                rigaLog = msg_LOG
        End Select

        Try
            ' AppendAllText aggiunge automaticamente il testo e gestisce l'apertura/chiusura del file.
            ' Aggiungiamo un solo vbCrLf alla fine della riga.
            IO.File.AppendAllText(LOG_FILE, rigaLog & Environment.NewLine)
        Catch ex As Exception
            ' Gestione errore silente come nel tuo codice originale
        End Try
    End Sub
    Public Sub Cycle_LOG(LOG_FILE$)
        REM ================ Esegue il cycling del file di log =================
        Dim k% = Month(Now)
        k% = k% - 1 : If k% = 0 Then k% = 12 ' a Gennaio chiudi dicembre
        Dim EXT_LOG$ = Format$(k%, "000")
        k% = InStr(LOG_FILE$, ".")
        Dim OLD_LOG$ = Left$(LOG_FILE$, k%) + EXT_LOG$
        If Dir(OLD_LOG$) <> "" Then IO.File.Delete(OLD_LOG$)
        If Dir$(LOG_FILE$) <> "" Then
            FileCopy(LOG_FILE$, OLD_LOG$)
            IO.File.Delete(LOG_FILE$)
        End If
        ' Dim ch1% = FreeFile()
        'Dim d$ = EXTR_dir(LOG_FILE$)


        'If Not IO.Directory.Exists(d$) Then IO.Directory.CreateDirectory(d$)

        Dim stringa As New StringBuilder
        stringa.AppendLine(StrDup(50, "#"))
        Dim a$ = StrDup(5, "#") + "  " + Format$(Now, "dd-MM-yyyy") + " Start LOG :" + UCase(Path.GetFileName(LOG_FILE$))
        stringa.AppendLine(a)
        stringa.AppendLine(StrDup(50, "#"))
        stringa.AppendLine(StrDup(30, " "))
        stringa.AppendLine(StrDup(30, " "))
        stringa.AppendLine("$$")
        Dim p As String = LOG_FILE
        p = Mid(p, 1, p.LastIndexOf("\"))
        IO.Directory.CreateDirectory(p)
        If Not File.Exists(LOG_FILE) Then
            Using sw As StreamWriter = New StreamWriter(LOG_FILE, False)
                sw.Write(stringa.ToString)
            End Using
        Else
            Using sw As StreamWriter = New StreamWriter(LOG_FILE, True)
                sw.Write(stringa.ToString)
            End Using
        End If


        'Open LOG_FILE$ For Append As #ch1%
        'Print #ch1%, strdup(50, "#")
        '        Dim a$ = StrDup(5, "#") + "  " + Format$(Now, "DD-MM-YYYY") + " Start LOG :" + UCase(EXTR_file$(LOG_FILE$))
        'Print #ch1%, a$
        'Print #ch1%, strdup(50, "#")
        'Print #ch1%, strdup(30, " ")
        'Print #ch1%, strdup(30, " ")
        'Print #ch1%, "$$"
        'Close #ch1%
    End Sub

End Module
