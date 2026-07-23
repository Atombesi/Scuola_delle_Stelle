<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class F_main
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
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

    'Descrizione: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Button1 = New Button()
        Button2 = New Button()
        MenuStrip1 = New MenuStrip()
        NuovoArrivoToolStripMenuItem = New ToolStripMenuItem()
        MaintenanceToolStripMenuItem = New ToolStripMenuItem()
        DatabasePrincipaleToolStripMenuItem = New ToolStripMenuItem()
        MenuStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(12, 64)
        Button1.Name = "Button1"
        Button1.Size = New Size(125, 31)
        Button1.TabIndex = 0
        Button1.Text = "CODICI ELCAR"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(12, 27)
        Button2.Name = "Button2"
        Button2.Size = New Size(125, 31)
        Button2.TabIndex = 1
        Button2.Text = "Scheda Giornaliera"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' MenuStrip1
        ' 
        MenuStrip1.Items.AddRange(New ToolStripItem() {NuovoArrivoToolStripMenuItem, MaintenanceToolStripMenuItem})
        MenuStrip1.Location = New Point(0, 0)
        MenuStrip1.Name = "MenuStrip1"
        MenuStrip1.Size = New Size(284, 24)
        MenuStrip1.TabIndex = 8
        MenuStrip1.Text = "MenuStrip1"
        ' 
        ' NuovoArrivoToolStripMenuItem
        ' 
        NuovoArrivoToolStripMenuItem.Name = "NuovoArrivoToolStripMenuItem"
        NuovoArrivoToolStripMenuItem.Size = New Size(83, 20)
        NuovoArrivoToolStripMenuItem.Text = "Anagrafiche"
        ' 
        ' MaintenanceToolStripMenuItem
        ' 
        MaintenanceToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {DatabasePrincipaleToolStripMenuItem})
        MaintenanceToolStripMenuItem.Name = "MaintenanceToolStripMenuItem"
        MaintenanceToolStripMenuItem.Size = New Size(88, 20)
        MaintenanceToolStripMenuItem.Text = "Maintenance"
        ' 
        ' DatabasePrincipaleToolStripMenuItem
        ' 
        DatabasePrincipaleToolStripMenuItem.Name = "DatabasePrincipaleToolStripMenuItem"
        DatabasePrincipaleToolStripMenuItem.Size = New Size(177, 22)
        DatabasePrincipaleToolStripMenuItem.Text = "Database Principale"
        ' 
        ' F_main
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(284, 450)
        Controls.Add(Button2)
        Controls.Add(Button1)
        Controls.Add(MenuStrip1)
        MainMenuStrip = MenuStrip1
        Name = "F_main"
        Text = "Form1"
        MenuStrip1.ResumeLayout(False)
        MenuStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents MaintenanceToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DatabasePrincipaleToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents NuovoArrivoToolStripMenuItem As ToolStripMenuItem

End Class
