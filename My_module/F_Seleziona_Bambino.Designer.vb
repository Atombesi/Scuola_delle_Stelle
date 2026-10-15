<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F_Seleziona_Bambino
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
        ComboBox1 = New ComboBox()
        Button1 = New Button()
        Button2 = New Button()
        Label1 = New Label()
        CheckBox1 = New CheckBox()
        MenuStrip1 = New MenuStrip()
        NuovoToolStripMenuItem = New ToolStripMenuItem()
        MenuStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' ComboBox1
        ' 
        ComboBox1.DropDownStyle = ComboBoxStyle.Simple
        ComboBox1.FormattingEnabled = True
        ComboBox1.Location = New Point(1, 58)
        ComboBox1.Name = "ComboBox1"
        ComboBox1.Size = New Size(200, 247)
        ComboBox1.TabIndex = 0
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(5, 367)
        Button1.Name = "Button1"
        Button1.Size = New Size(84, 25)
        Button1.TabIndex = 1
        Button1.Text = "Seleziona"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(134, 367)
        Button2.Name = "Button2"
        Button2.Size = New Size(61, 25)
        Button2.TabIndex = 2
        Button2.Text = "Close"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.BorderStyle = BorderStyle.Fixed3D
        Label1.Location = New Point(3, 28)
        Label1.Name = "Label1"
        Label1.Size = New Size(200, 23)
        Label1.TabIndex = 4
        Label1.Text = "Label1"
        Label1.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' CheckBox1
        ' 
        CheckBox1.AutoSize = True
        CheckBox1.Checked = True
        CheckBox1.CheckState = CheckState.Checked
        CheckBox1.Location = New Point(3, 308)
        CheckBox1.Name = "CheckBox1"
        CheckBox1.RightToLeft = RightToLeft.Yes
        CheckBox1.Size = New Size(117, 19)
        CheckBox1.TabIndex = 8
        CheckBox1.Text = "Mostra solo attivi"
        CheckBox1.UseVisualStyleBackColor = True
        ' 
        ' MenuStrip1
        ' 
        MenuStrip1.Items.AddRange(New ToolStripItem() {NuovoToolStripMenuItem})
        MenuStrip1.Location = New Point(0, 0)
        MenuStrip1.Name = "MenuStrip1"
        MenuStrip1.Size = New Size(203, 24)
        MenuStrip1.TabIndex = 9
        MenuStrip1.Text = "MenuStrip1"
        ' 
        ' NuovoToolStripMenuItem
        ' 
        NuovoToolStripMenuItem.Name = "NuovoToolStripMenuItem"
        NuovoToolStripMenuItem.Size = New Size(55, 20)
        NuovoToolStripMenuItem.Text = "Nuovo"
        ' 
        ' F_Seleziona_Bambino
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(203, 401)
        Controls.Add(CheckBox1)
        Controls.Add(Label1)
        Controls.Add(Button2)
        Controls.Add(Button1)
        Controls.Add(ComboBox1)
        Controls.Add(MenuStrip1)
        MainMenuStrip = MenuStrip1
        Name = "F_Seleziona_Bambino"
        Text = "F_Seleziona_Bambino"
        MenuStrip1.ResumeLayout(False)
        MenuStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents NuovoToolStripMenuItem As ToolStripMenuItem
End Class
