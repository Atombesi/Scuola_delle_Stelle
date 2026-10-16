<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F_Client
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Label1 = New Label()
        Label2 = New Label()
        Label3 = New Label()
        LblDeviceID = New Label()
        T_nomeclient = New TextBox()
        Button_Abilita = New Button()
        Button2 = New Button()
        LblNotaClient = New Label()
        Label5 = New Label()
        Button6 = New Button()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label1.Location = New Point(6, 2)
        Label1.Name = "Label1"
        Label1.Size = New Size(299, 17)
        Label1.TabIndex = 0
        Label1.Text = "Richiesta di Registrazione da client Android"
        Label1.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(9, 37)
        Label2.Name = "Label2"
        Label2.Size = New Size(67, 15)
        Label2.TabIndex = 1
        Label2.Tag = "ID univoco_:"
        Label2.Text = "ID univoco:"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(8, 106)
        Label3.Name = "Label3"
        Label3.Size = New Size(115, 15)
        Label3.TabIndex = 2
        Label3.Tag = "ID univoco_:"
        Label3.Text = "Nome Identificativo:"
        ' 
        ' LblDeviceID
        ' 
        LblDeviceID.BorderStyle = BorderStyle.Fixed3D
        LblDeviceID.Location = New Point(90, 37)
        LblDeviceID.Name = "LblDeviceID"
        LblDeviceID.Size = New Size(220, 18)
        LblDeviceID.TabIndex = 3
        LblDeviceID.Tag = ""
        ' 
        ' T_nomeclient
        ' 
        T_nomeclient.Location = New Point(130, 103)
        T_nomeclient.Name = "T_nomeclient"
        T_nomeclient.Size = New Size(180, 23)
        T_nomeclient.TabIndex = 4
        ' 
        ' Button_Abilita
        ' 
        Button_Abilita.Location = New Point(13, 147)
        Button_Abilita.Name = "Button_Abilita"
        Button_Abilita.Size = New Size(69, 31)
        Button_Abilita.TabIndex = 5
        Button_Abilita.Text = "Accetta"
        Button_Abilita.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(124, 147)
        Button2.Name = "Button2"
        Button2.Size = New Size(69, 31)
        Button2.TabIndex = 6
        Button2.Text = "Rifiuta"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' LblNotaClient
        ' 
        LblNotaClient.BorderStyle = BorderStyle.Fixed3D
        LblNotaClient.Location = New Point(90, 60)
        LblNotaClient.Name = "LblNotaClient"
        LblNotaClient.Size = New Size(220, 34)
        LblNotaClient.TabIndex = 8
        LblNotaClient.Tag = ""
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(10, 66)
        Label5.Name = "Label5"
        Label5.Size = New Size(67, 15)
        Label5.TabIndex = 7
        Label5.Tag = ""
        Label5.Text = "Messaggio:"
        ' 
        ' Button6
        ' 
        Button6.BackColor = Color.DimGray
        Button6.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Button6.Location = New Point(246, 142)
        Button6.Name = "Button6"
        Button6.Size = New Size(65, 36)
        Button6.TabIndex = 13
        Button6.Text = "Chiudi"
        Button6.UseVisualStyleBackColor = False
        ' 
        ' F_Client
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(323, 193)
        Controls.Add(Button6)
        Controls.Add(LblNotaClient)
        Controls.Add(Label5)
        Controls.Add(Button2)
        Controls.Add(Button_Abilita)
        Controls.Add(T_nomeclient)
        Controls.Add(LblDeviceID)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Name = "F_Client"
        StartPosition = FormStartPosition.CenterScreen
        Text = "F_Client"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents LblDeviceID As Label
    Friend WithEvents T_nomeclient As TextBox
    Friend WithEvents Button_Abilita As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents L_messaggio As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents LblNotaClient As Label
    Friend WithEvents Button6 As Button
End Class
