<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F_checklist
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
        Button5 = New Button()
        Button1 = New Button()
        Label2 = New Label()
        Label3 = New Label()
        Label4 = New Label()
        Label5 = New Label()
        ListBox1 = New ListBox()
        ListBox2 = New ListBox()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.BorderStyle = BorderStyle.Fixed3D
        Label1.Location = New Point(2, 2)
        Label1.Name = "Label1"
        Label1.Size = New Size(377, 22)
        Label1.TabIndex = 1
        Label1.Text = "Label1"
        Label1.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Button5
        ' 
        Button5.BackColor = Color.DimGray
        Button5.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Button5.ForeColor = Color.Snow
        Button5.Location = New Point(303, 411)
        Button5.Name = "Button5"
        Button5.Size = New Size(75, 27)
        Button5.TabIndex = 46
        Button5.Text = "Close"
        Button5.UseVisualStyleBackColor = False
        ' 
        ' Button1
        ' 
        Button1.BackColor = Color.DodgerBlue
        Button1.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Button1.ForeColor = Color.Snow
        Button1.Location = New Point(11, 411)
        Button1.Name = "Button1"
        Button1.Size = New Size(126, 27)
        Button1.TabIndex = 47
        Button1.Text = "Accetta selezione"
        Button1.UseVisualStyleBackColor = False
        ' 
        ' Label2
        ' 
        Label2.Location = New Point(131, 33)
        Label2.Name = "Label2"
        Label2.Size = New Size(64, 22)
        Label2.TabIndex = 48
        Label2.Text = "Label2"
        Label2.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label3
        ' 
        Label3.Location = New Point(9, 34)
        Label3.Name = "Label3"
        Label3.Size = New Size(108, 22)
        Label3.TabIndex = 49
        Label3.Text = "N. voci selezionate:"
        Label3.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label4
        ' 
        Label4.Location = New Point(9, 181)
        Label4.Name = "Label4"
        Label4.Size = New Size(108, 22)
        Label4.TabIndex = 53
        Label4.Text = "N. voci disponibili:"
        Label4.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label5
        ' 
        Label5.Location = New Point(131, 180)
        Label5.Name = "Label5"
        Label5.Size = New Size(64, 22)
        Label5.TabIndex = 52
        Label5.Text = "Label5"
        Label5.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' ListBox1
        ' 
        ListBox1.FormattingEnabled = True
        ListBox1.Location = New Point(6, 64)
        ListBox1.Name = "ListBox1"
        ListBox1.Size = New Size(378, 109)
        ListBox1.TabIndex = 54
        ' 
        ' ListBox2
        ' 
        ListBox2.FormattingEnabled = True
        ListBox2.Location = New Point(8, 219)
        ListBox2.Name = "ListBox2"
        ListBox2.Size = New Size(378, 154)
        ListBox2.TabIndex = 55
        ' 
        ' F_checklist
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(390, 450)
        ControlBox = False
        Controls.Add(ListBox2)
        Controls.Add(ListBox1)
        Controls.Add(Label4)
        Controls.Add(Label5)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(Button1)
        Controls.Add(Button5)
        Controls.Add(Label1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Name = "F_checklist"
        StartPosition = FormStartPosition.CenterScreen
        Text = "F_checklist"
        ResumeLayout(False)
    End Sub
    Friend WithEvents Label1 As Label
    Friend WithEvents Button5 As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents ListBox2 As ListBox
End Class
