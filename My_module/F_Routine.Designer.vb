<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F_Seleziona_Routine
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F_Seleziona_Routine))
        Label11 = New Label()
        Label6 = New Label()
        Label5 = New Label()
        Brn_giu = New Button()
        ImageList1 = New ImageList(components)
        Btn_su = New Button()
        CheckedListBox1 = New CheckedListBox()
        CheckBox4 = New CheckBox()
        Label16 = New Label()
        Label17 = New Label()
        Button6 = New Button()
        Label10 = New Label()
        Label8 = New Label()
        Button2 = New Button()
        Button4 = New Button()
        SuspendLayout()
        ' 
        ' Label11
        ' 
        Label11.Location = New Point(222, 7)
        Label11.Name = "Label11"
        Label11.Size = New Size(145, 15)
        Label11.TabIndex = 22
        Label11.Text = "Foglio n. 1 del 12-09-2026"
        ' 
        ' Label6
        ' 
        Label6.BackColor = Color.Transparent
        Label6.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label6.Location = New Point(8, 6)
        Label6.Name = "Label6"
        Label6.Size = New Size(208, 17)
        Label6.TabIndex = 40
        Label6.Text = "Pinco Pallino giuseppe"
        Label6.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label5
        ' 
        Label5.BackColor = Color.FromArgb(CByte(192), CByte(255), CByte(192))
        Label5.BorderStyle = BorderStyle.Fixed3D
        Label5.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label5.Location = New Point(381, 6)
        Label5.Name = "Label5"
        Label5.Size = New Size(145, 17)
        Label5.TabIndex = 39
        Label5.Text = "Da compilare"
        Label5.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Brn_giu
        ' 
        Brn_giu.ImageIndex = 1
        Brn_giu.ImageList = ImageList1
        Brn_giu.Location = New Point(490, 247)
        Brn_giu.Name = "Brn_giu"
        Brn_giu.Size = New Size(33, 33)
        Brn_giu.TabIndex = 41
        Brn_giu.UseVisualStyleBackColor = True
        ' 
        ' ImageList1
        ' 
        ImageList1.ColorDepth = ColorDepth.Depth32Bit
        ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), ImageListStreamer)
        ImageList1.TransparentColor = Color.Transparent
        ImageList1.Images.SetKeyName(0, "Emoji Symbols-31.png")
        ImageList1.Images.SetKeyName(1, "Emoji Symbols-32.png")
        ' 
        ' Btn_su
        ' 
        Btn_su.ImageIndex = 0
        Btn_su.ImageList = ImageList1
        Btn_su.Location = New Point(490, 187)
        Btn_su.Name = "Btn_su"
        Btn_su.Size = New Size(33, 33)
        Btn_su.TabIndex = 40
        Btn_su.UseVisualStyleBackColor = True
        ' 
        ' CheckedListBox1
        ' 
        CheckedListBox1.AllowDrop = True
        CheckedListBox1.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        CheckedListBox1.FormattingEnabled = True
        CheckedListBox1.Location = New Point(8, 67)
        CheckedListBox1.Name = "CheckedListBox1"
        CheckedListBox1.Size = New Size(475, 344)
        CheckedListBox1.TabIndex = 39
        ' 
        ' CheckBox4
        ' 
        CheckBox4.AutoSize = True
        CheckBox4.Location = New Point(11, 422)
        CheckBox4.Name = "CheckBox4"
        CheckBox4.Size = New Size(132, 19)
        CheckBox4.TabIndex = 38
        CheckBox4.Text = "Abilita ordinamento"
        CheckBox4.UseVisualStyleBackColor = True
        ' 
        ' Label16
        ' 
        Label16.AutoSize = True
        Label16.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label16.Location = New Point(246, 30)
        Label16.Name = "Label16"
        Label16.Size = New Size(14, 15)
        Label16.TabIndex = 28
        Label16.Text = "0"
        ' 
        ' Label17
        ' 
        Label17.AutoSize = True
        Label17.Location = New Point(135, 30)
        Label17.Name = "Label17"
        Label17.Size = New Size(103, 15)
        Label17.TabIndex = 27
        Label17.Text = "N. voci compilate:"
        ' 
        ' Button6
        ' 
        Button6.Font = New Font("Segoe UI", 9F)
        Button6.ForeColor = SystemColors.ControlText
        Button6.Location = New Point(19, 472)
        Button6.Name = "Button6"
        Button6.Size = New Size(93, 24)
        Button6.TabIndex = 20
        Button6.Text = "Salva e chiudi"
        Button6.UseVisualStyleBackColor = True
        ' 
        ' Label10
        ' 
        Label10.AutoSize = True
        Label10.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label10.Location = New Point(98, 31)
        Label10.Name = "Label10"
        Label10.Size = New Size(14, 15)
        Label10.TabIndex = 7
        Label10.Text = "0"
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Location = New Point(7, 30)
        Label8.Name = "Label8"
        Label8.Size = New Size(88, 15)
        Label8.TabIndex = 6
        Label8.Text = "N. voci Inserite:"
        ' 
        ' Button2
        ' 
        Button2.Font = New Font("Segoe UI", 9F)
        Button2.ForeColor = SystemColors.ControlText
        Button2.Location = New Point(346, 417)
        Button2.Name = "Button2"
        Button2.Size = New Size(117, 24)
        Button2.TabIndex = 43
        Button2.Text = "Aggiungi voce"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Button4
        ' 
        Button4.BackColor = Color.DimGray
        Button4.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Button4.ForeColor = Color.Snow
        Button4.Location = New Point(436, 467)
        Button4.Name = "Button4"
        Button4.Size = New Size(66, 35)
        Button4.TabIndex = 42
        Button4.Text = "Close"
        Button4.UseVisualStyleBackColor = False
        ' 
        ' F_Seleziona_Routine
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(532, 514)
        Controls.Add(Button2)
        Controls.Add(CheckedListBox1)
        Controls.Add(Brn_giu)
        Controls.Add(Button4)
        Controls.Add(Btn_su)
        Controls.Add(CheckBox4)
        Controls.Add(Label11)
        Controls.Add(Button6)
        Controls.Add(Label6)
        Controls.Add(Label5)
        Controls.Add(Label16)
        Controls.Add(Label17)
        Controls.Add(Label8)
        Controls.Add(Label10)
        Name = "F_Seleziona_Routine"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Preparazione foglio ROUTINE"
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents Label11 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents CheckBox4 As CheckBox
    Friend WithEvents Label16 As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents Button6 As Button
    Friend WithEvents Label10 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents CheckedListBox1 As CheckedListBox
    Friend WithEvents Btn_su As Button
    Friend WithEvents Brn_giu As Button
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents Button4 As Button
    Friend WithEvents Button2 As Button
End Class
