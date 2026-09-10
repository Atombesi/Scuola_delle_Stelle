Imports System.Windows.Forms
Module M_Calendario
    Public Function SelezionaDataDaCalendario(titoloFinestra As String) As DateTime?

        ' 1. Crea la Form contenitore a runtime
        Dim frm As New Form With {
            .Text = titoloFinestra,
            .FormBorderStyle = FormBorderStyle.FixedDialog,
            .MaximizeBox = False,
            .MinimizeBox = False,
            .StartPosition = FormStartPosition.CenterParent,
            .Width = 260,
            .Height = 240,
            .ShowInTaskbar = False
        }

        ' 2. Crea il controllo MonthCalendar a runtime
        Dim cal As New MonthCalendar With {
            .Location = New System.Drawing.Point(10, 10),
            .MaxSelectionCount = 1 ' Permette di selezionare un solo giorno
        }
        frm.Controls.Add(cal)

        ' Variabile di supporto per salvare la data scelta
        Dim dataSelezionata As DateTime? = Nothing

        ' 3. Gestisce l'evento di selezione (singolo clic deciso o invio)
        AddHandler cal.DateSelected, Sub(sender As Object, e As DateRangeEventArgs)
                                         dataSelezionata = cal.SelectionStart
                                         frm.DialogResult = DialogResult.OK
                                         frm.Close()
                                     End Sub

        ' 4. Mostra la finestra in modalità modale (attende l'utente)
        If frm.ShowDialog() = DialogResult.OK Then
            Return dataSelezionata
        Else
            Return Nothing ' L'utente ha chiuso la finestra senza selezionare
        End If

    End Function

End Module
