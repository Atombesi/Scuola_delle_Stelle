Public Class F_Client
    Public IdDispositivoDaAbilitare As String
    Public Nome_Dispositivo As String
    Public NotaDispositivo As String
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button_Abilita.Click
        Nome_Dispositivo = T_nomeclient.Text
        Nome_Dispositivo = InputBox("Nome identificativo del dispositivo:", "Nuovo dispositivo Android", Nome_Dispositivo)
        If String.IsNullOrWhiteSpace(Nome_Dispositivo) Then
            MessageBox.Show("Inserisci un nome da assegnare a questo dispositivo.")
            T_nomeclient.Focus()
            Return
        End If

        'Dim esito As Boolean = Class_DEVICE.RegistraNuovoDispositivo(IdDispositivoDaAbilitare, Nome_Dispositivo, True)
        Dim esito = Class_DEVICE.Abilita_dispositivo(IdDispositivoDaAbilitare, True)
        If esito Then
            MessageBox.Show("Dispositivo abilitato correttamente!")
            Dim m = "Nuovo Device registrato (ID:" + IdDispositivoDaAbilitare.ToString + ") (Nome:" + Nome_Dispositivo + ")"
            APPEND_LOG(m, ini.File_Log, True)
            Me.Close() ' Chiude la maschera
        Else
            MessageBox.Show("Errore durante l'abilitazione sul database.")
            Dim m = "[Errore 013] Nuovo Device (errore registrazione) (ID:" + IdDispositivoDaAbilitare.ToString + ") (Nome:" + Nome_Dispositivo + ")"
            APPEND_LOG(m, ini.File_Log, True)
        End If
    End Sub

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