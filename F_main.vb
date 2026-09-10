Imports Microsoft.Data.Sqlite

Public Class F_main
    Public my_ELCAR As New Class_GIORNALIERO
    Private isCaricato As Boolean = False
    Private Noneseguire As Boolean = False

    Private Sub F_main_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Call Read_ini()

        Carica_elenco_CODICI_ELCAR()
        isCaricato = True
    End Sub




    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        F_Seleziona_Categoria.Show()
    End Sub

    Sub test_db()
        Dim tabelle As List(Of String) = Class_ANAGRAFICA.ElencaTabelleDisponibili()

        If tabelle.Count = 0 Then
            MessageBox.Show("ATTENZIONE: Il codice vede 0 tabelle! " &
                    "Significa che si sta connettendo a un file di database VUOTO. " &
                    "Controlla il percorso del file nella stringa di connessione.")
        Else
            Dim messaggio As String = "Tabelle trovate nel database dal codice:" & Environment.NewLine
            For Each tabella In tabelle
                messaggio &= "- " & tabella & Environment.NewLine
            Next
            MessageBox.Show(messaggio)
        End If
    End Sub



    Private Sub Label2_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub NuovoArrivoToolStripMenuItem_Click(sender As Object, e As EventArgs)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub

    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click
        F_Anagrafica.ShowDialog()

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Try
            F_MyGraph.Hide()
        Catch ex As Exception

        End Try

        Try
            F_Anagrafica.Hide()
        Catch ex As Exception
        End Try

        Try
            F_Foglio_esistente.Hide()
        Catch ex As Exception
        End Try

        Try
            F_Seleziona_Categoria.Hide()
        Catch ex As Exception

        End Try


        End
    End Sub

    Private Sub DatabasePrincipaleToolStripMenuItem_Click(sender As Object, e As EventArgs)

    End Sub
End Class
