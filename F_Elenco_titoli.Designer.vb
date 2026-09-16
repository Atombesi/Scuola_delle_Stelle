<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F_Elenco_titoli
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
        Button5 = New Button()
        ComboBox1 = New ComboBox()
        Button1 = New Button()
        Button2 = New Button()
        Label3 = New Label()
        Label1 = New Label()
        SuspendLayout()
        ' 
        ' Button5
        ' 
        Button5.Location = New Point(129, 241)
        Button5.Name = "Button5"
        Button5.Size = New Size(71, 28)
        Button5.TabIndex = 19
        Button5.Text = "Aggiungi"
        Button5.UseVisualStyleBackColor = True
        ' 
        ' ComboBox1
        ' 
        ComboBox1.DropDownStyle = ComboBoxStyle.Simple
        ComboBox1.FormattingEnabled = True
        ComboBox1.Location = New Point(3, 52)
        ComboBox1.Name = "ComboBox1"
        ComboBox1.Size = New Size(200, 188)
        ComboBox1.TabIndex = 18
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(3, 241)
        Button1.Name = "Button1"
        Button1.Size = New Size(74, 28)
        Button1.TabIndex = 20
        Button1.Text = "Conferma"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.BackColor = Color.DimGray
        Button2.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Button2.ForeColor = Color.Snow
        Button2.Location = New Point(128, 276)
        Button2.Name = "Button2"
        Button2.Size = New Size(75, 31)
        Button2.TabIndex = 21
        Button2.Text = "Close"
        Button2.UseVisualStyleBackColor = False
        ' 
        ' Label3
        ' 
        Label3.BorderStyle = BorderStyle.Fixed3D
        Label3.Location = New Point(6, 29)
        Label3.Name = "Label3"
        Label3.Size = New Size(196, 17)
        Label3.TabIndex = 22
        Label3.Text = "Elenco Bimbi"
        Label3.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label1
        ' 
        Label1.Location = New Point(3, 8)
        Label1.Name = "Label1"
        Label1.Size = New Size(112, 17)
        Label1.TabIndex = 23
        Label1.Text = "Titolo selezionato"
        Label1.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' F_Elenco_titoli
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(207, 313)
        Controls.Add(Label1)
        Controls.Add(Label3)
        Controls.Add(Button2)
        Controls.Add(Button1)
        Controls.Add(Button5)
        Controls.Add(ComboBox1)
        Name = "F_Elenco_titoli"
        Text = "Elenco titoli"
        ResumeLayout(False)
    End Sub

    Friend WithEvents Button5 As Button
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents Label1 As Label
End Class
