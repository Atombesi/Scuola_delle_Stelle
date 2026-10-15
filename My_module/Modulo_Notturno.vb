Module ModoNotturno
    ' Variabile globale per ricordarsi lo stato attuale
    Public IsDarkMode As Boolean = False

    ' Definiamo le tavolozze dei colori
    Private ColorSfondoScuro As Color = Color.FromArgb(32, 32, 32)   ' Grigio molto scuro
    Private ColorTestoChiaro As Color = Color.FromArgb(240, 240, 240) ' Bianco opaco

    Private ColorSfondoChiaro As Color = SystemColors.Control        ' Sfondo standard Windows
    Private ColorTestoScuro As Color = SystemColors.ControlText      ' Testo nero standard

    ''' <summary>
    ''' Applica i colori corretti a tutta la Form e a tutti i suoi controlli figli
    ''' </summary>
    Public Sub ApplicaTema(ByVal parentForm As Form)
        ' Determina i colori da usare in base allo stato
        Dim sfondo As Color = If(IsDarkMode, ColorSfondoScuro, ColorSfondoChiaro)
        Dim testo As Color = If(IsDarkMode, ColorTestoChiaro, ColorTestoScuro)

        ' Applica il colore alla Form principale
        parentForm.BackColor = sfondo
        parentForm.ForeColor = testo

        ' Avvia il ciclo su tutti i controlli della Form
        CiclaEApplicaControlli(parentForm.Controls, sfondo, testo)
    End Sub

    Private Sub CiclaEApplicaControlli(controlli As Control.ControlCollection, sfondo As Color, testo As Color)
        For Each ctrl As Control In controlli

            ' CONTROLLO CRITICO: Verifica se il controllo corrente è una DataGridView
            If TypeOf ctrl Is DataGridView Then
                Dim dgv As DataGridView = DirectCast(ctrl, DataGridView)

                ' 1. Imposta lo sfondo dell'area vuota interna alla griglia
                dgv.BackgroundColor = sfondo

                ' 2. Imposta lo sfondo e il testo delle celle standard
                dgv.DefaultCellStyle.BackColor = sfondo
                dgv.DefaultCellStyle.ForeColor = testo

                ' 3. Imposta i colori per la riga selezionata dall'utente (in modalità notte)
                ' Altrimenti la riga selezionata potrebbe rimanere con i colori standard illeggibili
                If IsDarkMode Then
                    dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(60, 60, 60) ' Grigio scuro di selezione
                    dgv.DefaultCellStyle.SelectionForeColor = Color.White
                Else
                    dgv.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight ' Colore blu standard di Windows
                    dgv.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText
                End If

                ' 4. (Opzionale) Gestione delle intestazioni di colonna (Header)
                ' Nota: per far funzionare i colori sugli Header devi disabilitare lo stile visuale di Windows su di essi
                dgv.EnableHeadersVisualStyles = False
                dgv.ColumnHeadersDefaultCellStyle.BackColor = sfondo
                dgv.ColumnHeadersDefaultCellStyle.ForeColor = testo

                ' 5. (Opzionale) Gestione delle linee della griglia per non farle sparire nel buio
                If IsDarkMode Then
                    dgv.GridColor = Color.FromArgb(50, 50, 50)
                Else
                    dgv.GridColor = SystemColors.ControlDark
                End If

            Else
                ' Per tutti gli altri controlli normali applichi la logica standard che avevi già
                ctrl.BackColor = sfondo
                ctrl.ForeColor = testo
            End If

            ' Se il controllo contiene altri controlli interni (es. Panel, GroupBox, TabControl), continua il ciclo ricorsivo
            If ctrl.HasChildren Then
                CiclaEApplicaControlli(ctrl.Controls, sfondo, testo)
            End If
        Next
    End Sub
End Module

