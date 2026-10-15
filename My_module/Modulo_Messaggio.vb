Imports System.Drawing
Imports System.Windows.Forms

Module ModuloMessaggi

    ' Variabile globale del modulo per tracciare la finestra aperta
    Public FormMessaggioRuntime As Form = Nothing

    ''' <summary>
    ''' Mostra una form modeless a runtime da qualsiasi punto dell'applicazione
    ''' </summary>
    Public Sub MostraMessaggioModeless(titolo As String, testo As String, iconaMsgBox As MessageBoxIcon)

        ' Se c'è già una finestra aperta, la chiudiamo prima di aprirne una nuova
        ChiudiMessaggioModeless()

        ' 1. Configurazione della Form
        FormMessaggioRuntime = New Form With {
            .Text = titolo,
            .FormBorderStyle = FormBorderStyle.FixedDialog,
            .MaximizeBox = False,
            .MinimizeBox = False,
            .StartPosition = FormStartPosition.CenterScreen,
            .Width = 400,
            .Height = 150,
            .ShowInTaskbar = False,
            .TopMost = True ' Mantiene la finestra sopra le altre mentre il processo lavora
        }

        ' 2. Recupero dell'icona di sistema stile MsgBox
        Dim iconaDisegnata As Icon = Nothing
        Select Case iconaMsgBox
            Case MessageBoxIcon.Information
                iconaDisegnata = SystemIcons.Information
            Case MessageBoxIcon.Warning
                iconaDisegnata = SystemIcons.Warning
            Case MessageBoxIcon.Error
                iconaDisegnata = SystemIcons.Error
            Case MessageBoxIcon.Question
                iconaDisegnata = SystemIcons.Question
        End Select

        ' 3. Creazione della PictureBox per l'icona
        If iconaDisegnata IsNot Nothing Then
            Dim picIcona As New PictureBox With {
                .Image = iconaDisegnata.ToBitmap(),
                .Location = New Point(20, 30),
                .Size = New Size(32, 32),
                .SizeMode = PictureBoxSizeMode.StretchImage
            }
            FormMessaggioRuntime.Controls.Add(picIcona)
        End If

        ' 4. Creazione della Label per il testo
        Dim lblTesto As New Label With {
            .Text = testo,
            .Location = New Point(70, 30),
            .Size = New Size(290, 60),
            .Font = New Font("Segoe UI", 9, FontStyle.Regular),
            .TextAlign = ContentAlignment.MiddleLeft
        }
        FormMessaggioRuntime.Controls.Add(lblTesto)

        ' 5. Mostra la form senza bloccare il codice (Modeless)
        FormMessaggioRuntime.Show()
        FormMessaggioRuntime.Refresh()
    End Sub

    ''' <summary>
    ''' Chiude la form di messaggio a runtime se è attualmente aperta
    ''' </summary>
    Public Sub ChiudiMessaggioModeless()
        If FormMessaggioRuntime IsNot Nothing AndAlso Not FormMessaggioRuntime.IsDisposed Then
            FormMessaggioRuntime.Close()
            FormMessaggioRuntime = Nothing
        End If
    End Sub

End Module