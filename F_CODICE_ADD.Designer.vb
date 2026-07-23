<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F_CODICE_ADD
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
        Label4 = New Label()
        Button1 = New Button()
        Button2 = New Button()
        Label5 = New Label()
        TextBox2 = New TextBox()
        TextBox3 = New TextBox()
        CheckBox1 = New CheckBox()
        Label6 = New Label()
        Label3 = New Label()
        Label7 = New Label()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(85, 39)
        Label1.Name = "Label1"
        Label1.Size = New Size(11, 14)
        Label1.TabIndex = 12
        Label1.Text = "-"
        ' 
        ' Label2
        ' 
        Label2.BorderStyle = BorderStyle.Fixed3D
        Label2.Font = New Font("Calibri", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label2.Location = New Point(6, 8)
        Label2.Name = "Label2"
        Label2.Size = New Size(384, 21)
        Label2.TabIndex = 15
        Label2.Text = "Nuovo Codice Aggiuntivo condiviso"
        Label2.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(5, 66)
        Label4.Name = "Label4"
        Label4.Size = New Size(36, 14)
        Label4.TabIndex = 16
        Label4.Text = "Testo"
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(9, 221)
        Button1.Name = "Button1"
        Button1.Size = New Size(99, 32)
        Button1.TabIndex = 17
        Button1.Text = "Close"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(291, 221)
        Button2.Name = "Button2"
        Button2.Size = New Size(99, 32)
        Button2.TabIndex = 18
        Button2.Text = "Save"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(5, 96)
        Label5.Name = "Label5"
        Label5.Size = New Size(71, 14)
        Label5.TabIndex = 19
        Label5.Text = "Testo Lungo"
        ' 
        ' TextBox2
        ' 
        TextBox2.Location = New Point(85, 63)
        TextBox2.Name = "TextBox2"
        TextBox2.Size = New Size(305, 22)
        TextBox2.TabIndex = 20
        ' 
        ' TextBox3
        ' 
        TextBox3.Location = New Point(85, 93)
        TextBox3.Multiline = True
        TextBox3.Name = "TextBox3"
        TextBox3.Size = New Size(305, 55)
        TextBox3.TabIndex = 21
        ' 
        ' CheckBox1
        ' 
        CheckBox1.AutoSize = True
        CheckBox1.Location = New Point(7, 154)
        CheckBox1.Name = "CheckBox1"
        CheckBox1.RightToLeft = RightToLeft.Yes
        CheckBox1.Size = New Size(94, 18)
        CheckBox1.TabIndex = 24
        CheckBox1.Text = "Codice Attivo"
        CheckBox1.UseVisualStyleBackColor = True
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(6, 39)
        Label6.Name = "Label6"
        Label6.Size = New Size(73, 14)
        Label6.TabIndex = 22
        Label6.Text = "Provenienza"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(9, 182)
        Label3.Name = "Label3"
        Label3.Size = New Size(82, 14)
        Label3.TabIndex = 25
        Label3.Text = "Nuovo codice:"
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Location = New Point(97, 182)
        Label7.Name = "Label7"
        Label7.Size = New Size(11, 14)
        Label7.TabIndex = 26
        Label7.Text = "-"
        ' 
        ' F_CODICE_ADD
        ' 
        AutoScaleDimensions = New SizeF(6F, 14F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(402, 266)
        Controls.Add(Label7)
        Controls.Add(Label3)
        Controls.Add(CheckBox1)
        Controls.Add(Label6)
        Controls.Add(TextBox3)
        Controls.Add(TextBox2)
        Controls.Add(Label5)
        Controls.Add(Button2)
        Controls.Add(Button1)
        Controls.Add(Label4)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Font = New Font("Calibri", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Name = "F_CODICE_ADD"
        Text = "F_CODICE_ADD"
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label7 As Label
End Class
