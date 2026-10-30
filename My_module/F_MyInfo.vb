Public Class F_MyInfo
    Private Sub F_MyInfo_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        Me.Hide()
    End Sub



    Private Sub LinkMail_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkMail.LinkClicked
        Try
            ' Avvia il programma di posta predefinito con l'indirizzo specificato
            Process.Start(New ProcessStartInfo("mailto:brz.modena@gmail.com") With {.UseShellExecute = True})
        Catch ex As Exception
            MessageBox.Show("Impossibile aprire il programma di posta: " & ex.Message)
        End Try

    End Sub

    Private Sub LinkWP_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkWP.LinkClicked
        Try
            ' Avvia il browser predefinito di sistema aprendo l'URL specificato
            Process.Start(New ProcessStartInfo("https://Andreatombesi.wordpress.com") With {.UseShellExecute = True})
        Catch ex As Exception
            MessageBox.Show("Impossibile aprire la pagina web: " & ex.Message)
        End Try

    End Sub

    Private Sub LinkGitHub_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkGitHub.LinkClicked
        Try
            ' Avvia il browser predefinito di sistema aprendo l'URL specificato
            Process.Start(New ProcessStartInfo("https://ghihub.com/Atombesi") With {.UseShellExecute = True})
        Catch ex As Exception
            MessageBox.Show("Impossibile aprire la pagina web: " & ex.Message)
        End Try

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Hide()
    End Sub
End Class