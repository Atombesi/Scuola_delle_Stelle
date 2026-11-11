Public Class F_Client
    Public IdDispositivoDaAbilitare As String
    Public Nome_Dispositivo As String
    Public NotaDispositivo As String

    ' TABLET-REG-02A - INIZIO: approvazione e salvataggio del nome assegnato
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button_Abilita.Click

        Nome_Dispositivo = InputBox(
        "Nome identificativo del dispositivo:",
        "Nuovo dispositivo Android",
        T_nomeclient.Text).Trim()

        If String.IsNullOrWhiteSpace(Nome_Dispositivo) Then
            MessageBox.Show("Inserisci un nome da assegnare a questo dispositivo.")
            T_nomeclient.Focus()
            Return
        End If

        Try
            Dim dispositivo As Class_DEVICE = Class_DEVICE.Load_by_MAC(IdDispositivoDaAbilitare)

            If dispositivo Is Nothing Then
                MessageBox.Show("Dispositivo non trovato nel database.")
                Return
            End If

            dispositivo.NOME = Nome_Dispositivo
            dispositivo.ENABLED = 1
            dispositivo.STATO = "OK"

            dispositivo.Save()

            MessageBox.Show("Dispositivo abilitato correttamente!")

            Dim m As String = "Nuovo Device registrato (ID:" &
                          IdDispositivoDaAbilitare &
                          ") (Nome:" & Nome_Dispositivo & ")"

            APPEND_LOG(m, ini.File_Log, True, "1")
            Me.Close()

        Catch ex As Exception
            MessageBox.Show("Errore durante l'abilitazione sul database: " & ex.Message)

            Dim m As String = "[013] Errore abilitazione Device (ID:" &
                          IdDispositivoDaAbilitare & "): " & ex.Message

            APPEND_LOG(m, ini.File_Log, True, "E")
        End Try

    End Sub
    ' TABLET-REG-02A - FINE


    Private Sub F_Client_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LblDeviceID.Text = IdDispositivoDaAbilitare
        LblNotaClient.Text = NotaDispositivo

        ' Puliamo la casella di testo e mettiamo il focus per digitare subito
        T_nomeclient.Text = ""
        T_nomeclient.Focus()

    End Sub



    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        DialogResult = DialogResult.Cancel
        Close()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Call MsgBox("Devo rimuovere la voce dal Daabase")
    End Sub
End Class