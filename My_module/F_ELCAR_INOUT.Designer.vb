<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F_ELCAR_INOUT
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
        components = New ComponentModel.Container()
        Button1 = New Button()
        Button2 = New Button()
        GroupBox1 = New GroupBox()
        Button5 = New Button()
        Button3 = New Button()
        RadioButton3 = New RadioButton()
        RadioButton2 = New RadioButton()
        RadioButton1 = New RadioButton()
        TP1 = New ToolTip(components)
        Button4 = New Button()
        GroupBox1.SuspendLayout()
        SuspendLayout()
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(17, 8)
        Button1.Name = "Button1"
        Button1.Size = New Size(156, 29)
        Button1.TabIndex = 0
        Button1.Text = "Esportazione Voci ELCAR"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.BackColor = Color.DimGray
        Button2.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Button2.Location = New Point(124, 264)
        Button2.Name = "Button2"
        Button2.Size = New Size(65, 26)
        Button2.TabIndex = 60
        Button2.Text = "Chiudi"
        Button2.UseVisualStyleBackColor = False
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(Button5)
        GroupBox1.Controls.Add(Button3)
        GroupBox1.Controls.Add(RadioButton3)
        GroupBox1.Controls.Add(RadioButton2)
        GroupBox1.Controls.Add(RadioButton1)
        GroupBox1.Location = New Point(4, 83)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(187, 162)
        GroupBox1.TabIndex = 61
        GroupBox1.TabStop = False
        GroupBox1.Text = "Importazione Voci"
        ' 
        ' Button5
        ' 
        Button5.BackColor = SystemColors.Control
        Button5.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Button5.Location = New Point(130, 128)
        Button5.Name = "Button5"
        Button5.Size = New Size(47, 26)
        Button5.TabIndex = 62
        Button5.Text = "Esci"
        Button5.UseVisualStyleBackColor = False
        ' 
        ' Button3
        ' 
        Button3.BackColor = SystemColors.Control
        Button3.Enabled = False
        Button3.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Button3.Location = New Point(3, 127)
        Button3.Name = "Button3"
        Button3.Size = New Size(108, 26)
        Button3.TabIndex = 61
        Button3.Text = "Importa Elenco"
        Button3.UseVisualStyleBackColor = False
        ' 
        ' RadioButton3
        ' 
        RadioButton3.AutoSize = True
        RadioButton3.Location = New Point(11, 76)
        RadioButton3.Name = "RadioButton3"
        RadioButton3.Size = New Size(175, 19)
        RadioButton3.TabIndex = 2
        RadioButton3.TabStop = True
        RadioButton3.Text = "ELCAR VOCI (personalizzate)"
        TP1.SetToolTip(RadioButton3, "Voci Personalizzate COMUNI")
        RadioButton3.UseVisualStyleBackColor = True
        ' 
        ' RadioButton2
        ' 
        RadioButton2.AutoSize = True
        RadioButton2.Location = New Point(11, 51)
        RadioButton2.Name = "RadioButton2"
        RadioButton2.Size = New Size(114, 19)
        RadioButton2.TabIndex = 1
        RadioButton2.TabStop = True
        RadioButton2.Text = "ELCAR VOCI SUB"
        TP1.SetToolTip(RadioButton2, "Ultimo livello voci ELCAR")
        RadioButton2.UseVisualStyleBackColor = True
        ' 
        ' RadioButton1
        ' 
        RadioButton1.AutoSize = True
        RadioButton1.Location = New Point(11, 26)
        RadioButton1.Name = "RadioButton1"
        RadioButton1.Size = New Size(90, 19)
        RadioButton1.TabIndex = 0
        RadioButton1.TabStop = True
        RadioButton1.Text = "ELCAR VOCI"
        RadioButton1.UseVisualStyleBackColor = True
        ' 
        ' Button4
        ' 
        Button4.Location = New Point(17, 43)
        Button4.Name = "Button4"
        Button4.Size = New Size(156, 29)
        Button4.TabIndex = 62
        Button4.Text = "Importazione Voci ELCAR"
        Button4.UseVisualStyleBackColor = True
        ' 
        ' F_ELCAR_INOUT
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(196, 298)
        ControlBox = False
        Controls.Add(Button2)
        Controls.Add(Button4)
        Controls.Add(GroupBox1)
        Controls.Add(Button1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Name = "F_ELCAR_INOUT"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Import Export voci ELCAR"
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents RadioButton3 As RadioButton
    Friend WithEvents TP1 As ToolTip
    Friend WithEvents RadioButton2 As RadioButton
    Friend WithEvents RadioButton1 As RadioButton
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Button5 As Button
End Class
