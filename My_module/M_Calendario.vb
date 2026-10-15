Imports System.Windows.Forms
Module M_Calendario
    Public Function SelezionaDataDaCalendario(titoloFinestra As String, Optional giorno As Date = Nothing) As DateTime?

        ' 1. Crea la Form contenitore (altezza aumentata a 280 per fare spazio al tasto)
        Dim frm As New Form With {
        .Text = titoloFinestra,
        .FormBorderStyle = FormBorderStyle.FixedDialog,
        .MaximizeBox = False,
        .MinimizeBox = False,
        .StartPosition = FormStartPosition.CenterParent,
        .Width = 260,
        .Height = 280,
        .ShowInTaskbar = False
    }

        ' 2. Crea il controllo MonthCalendar
        Dim cal As New MonthCalendar With {
        .Location = New System.Drawing.Point(10, 10),
        .MaxSelectionCount = 1
    }
        frm.Controls.Add(cal)

        ' 3. Creazione del pulsante OK a runtime
        Dim btnOk As New Button With {
        .Text = "OK",
        .Location = New System.Drawing.Point(10, 195), ' Posizionato subito sotto il calendario
        .Width = 227, ' Allineato perfettamente alla larghezza del calendario
        .Height = 30,
        .DialogResult = DialogResult.OK ' Assegna il valore OK e chiude la Form al clic
    }
        frm.Controls.Add(btnOk)

        ' Associa il tasto OK all'invio della tastiera (pulsante predefinito)
        frm.AcceptButton = btnOk

        ' Se la data passata è valida, posiziona il calendario sul giorno e sul mese corretto
        If giorno <> Nothing AndAlso giorno.Year > 2025 Then
            cal.SelectionStart = giorno
            cal.TodayDate = giorno
        End If

        ' 4. Mostra la finestra in modalità modale
        If frm.ShowDialog() = DialogResult.OK Then
            ' Ritorna la data selezionata solo se l'utente ha premuto OK o Invio
            Return cal.SelectionStart
        Else
            Return Nothing ' L'utente ha chiuso con la "X" o premuto Esc
        End If

    End Function

End Module
