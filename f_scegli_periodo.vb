Imports DocumentFormat.OpenXml.Vml.Presentation

Public Class f_scegli_periodo
    Public dataF As Date
    Public dataI As Date

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Tag = ""
        Me.Hide()

    End Sub
    Function Apri(Intervallo, datafINALE) As (datai As Date, dataf As Date)
        Me.Tag = ""
        If Not Date.TryParse(datafINALE, dataF) Then
            dataF = Now
        End If

        If Intervallo = 15 Then             '15 gg
            RadioButton1.Checked = True
            dataI = dataF.AddDays(-15)
        ElseIf Intervallo = 1 Then          '1 mese
            RadioButton2.Checked = True
            dataI = dataF.AddMonths(-1)
        ElseIf Intervallo = 3 Then          '3 mese
            RadioButton3.Checked = True
            dataI = dataF.AddMonths(-3)
            RadioButton4.Checked = True
        ElseIf Intervallo = 6 Then         '6 mesi
            RadioButton5.Checked = True
            dataI = dataF.AddMonths(-6)
        ElseIf Intervallo = 12 Then         '1 anno
            RadioButton5.Checked = True
            dataI = dataF.AddYears(-1)
        ElseIf Intervallo = 0 Then         'tutto
            RadioButton6.Checked = True
            dataF = Now
            dataI = dataF.AddYears(-15)
        End If
        Mostra_date()
        Me.ShowDialog()
        If Me.Tag = "ok" Then
            Return (dataI, dataF)
        Else
            Return (Nothing, Nothing)
        End If

    End Function
    Sub Mostra_date()
        If RadioButton1.Checked = True Then
            dataI = dataF.AddDays(-15)
        ElseIf RadioButton2.Checked = True Then         '1 mese
            dataI = dataF.AddMonths(-1)
        ElseIf RadioButton3.Checked = True Then         '1 mese
            dataI = dataF.AddMonths(-3)
        ElseIf RadioButton4.Checked = True Then          '6 mesi
            dataI = dataF.AddMonths(-6)
        ElseIf RadioButton5.Checked = True Then        '1 anno
            dataI = dataF.AddYears(-1)
        ElseIf RadioButton6.Checked = True Then         'tutto
            dataF = Now
            dataI = dataF.AddYears(-15)
        End If
        Label3.Text = dataI.ToShortDateString
        Label4.Text = dataF.ToShortDateString

    End Sub
    Private Sub f_scegli_periodo_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        Mostra_date()
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        Mostra_date()
    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        Mostra_date()
    End Sub

    Private Sub RadioButton4_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton4.CheckedChanged
        Mostra_date()
    End Sub

    Private Sub RadioButton5_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton5.CheckedChanged
        Mostra_date()
    End Sub

    Private Sub RadioButton6_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton6.CheckedChanged
        Mostra_date()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Tag = "ok"
        Me.Hide()
    End Sub
End Class