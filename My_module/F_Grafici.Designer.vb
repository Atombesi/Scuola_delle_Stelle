<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F_Grafici
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
        Button1 = New Button()
        GroupBox1 = New GroupBox()
        Button2 = New Button()
        Button3 = New Button()
        Button4 = New Button()
        GroupBox1.SuspendLayout()
        SuspendLayout()
        ' 
        ' Button1
        ' 
        Button1.Font = New Font("Segoe UI", 9F)
        Button1.Location = New Point(6, 27)
        Button1.Name = "Button1"
        Button1.Size = New Size(109, 27)
        Button1.TabIndex = 0
        Button1.Text = "4 Voci ELCAR"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(Button4)
        GroupBox1.Controls.Add(Button3)
        GroupBox1.Controls.Add(Button2)
        GroupBox1.Controls.Add(Button1)
        GroupBox1.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        GroupBox1.Location = New Point(7, 8)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(123, 163)
        GroupBox1.TabIndex = 1
        GroupBox1.TabStop = False
        GroupBox1.Text = "GIORNALIERI"
        ' 
        ' Button2
        ' 
        Button2.Font = New Font("Segoe UI", 9F)
        Button2.Location = New Point(6, 60)
        Button2.Name = "Button2"
        Button2.Size = New Size(109, 27)
        Button2.TabIndex = 1
        Button2.Text = "8 Voci ELCAR"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Button3
        ' 
        Button3.Font = New Font("Segoe UI", 9F)
        Button3.Location = New Point(6, 95)
        Button3.Name = "Button3"
        Button3.Size = New Size(109, 27)
        Button3.TabIndex = 2
        Button3.Text = "totali giornaliero"
        Button3.UseVisualStyleBackColor = True
        ' 
        ' Button4
        ' 
        Button4.Font = New Font("Segoe UI", 9F)
        Button4.Location = New Point(6, 128)
        Button4.Name = "Button4"
        Button4.Size = New Size(109, 27)
        Button4.TabIndex = 3
        Button4.Text = "comportamentali"
        Button4.UseVisualStyleBackColor = True
        ' 
        ' F_Grafici
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(553, 450)
        Controls.Add(GroupBox1)
        Name = "F_Grafici"
        Text = "F_Grafici"
        GroupBox1.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Button2 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Button3 As Button
End Class
