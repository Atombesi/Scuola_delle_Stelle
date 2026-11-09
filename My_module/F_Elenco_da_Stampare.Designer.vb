<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F_Elenco_da_Stampare
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F_Elenco_da_Stampare))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Label11 = New Label()
        ImageList1 = New ImageList(components)
        DGV1 = New DataGridView()
        Column1 = New DataGridViewCheckBoxColumn()
        Column5 = New DataGridViewImageColumn()
        Column2 = New DataGridViewTextBoxColumn()
        Column3 = New DataGridViewTextBoxColumn()
        Column4 = New DataGridViewTextBoxColumn()
        Column6 = New DataGridViewTextBoxColumn()
        Column7 = New DataGridViewTextBoxColumn()
        Label1 = New Label()
        GroupBox1 = New GroupBox()
        Button8 = New Button()
        Label12 = New Label()
        Label13 = New Label()
        Button5 = New Button()
        Label10 = New Label()
        Label4 = New Label()
        Label9 = New Label()
        Label6 = New Label()
        Label7 = New Label()
        Label5 = New Label()
        Label8 = New Label()
        Label2 = New Label()
        Label3 = New Label()
        Label14 = New Label()
        Label15 = New Label()
        Button1 = New Button()
        Button2 = New Button()
        Button3 = New Button()
        Button4 = New Button()
        TP1 = New ToolTip(components)
        Button7 = New Button()
        CheckBox1 = New CheckBox()
        Button6 = New Button()
        Label16 = New Label()
        Label17 = New Label()
        Label18 = New Label()
        Label19 = New Label()
        Label20 = New Label()
        FolderBrowserDialog1 = New FolderBrowserDialog()
        PrintDialog1 = New PrintDialog()
        CType(DGV1, ComponentModel.ISupportInitialize).BeginInit()
        GroupBox1.SuspendLayout()
        SuspendLayout()
        ' 
        ' Label11
        ' 
        Label11.BackColor = SystemColors.Control
        Label11.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label11.Location = New Point(143, 11)
        Label11.Name = "Label11"
        Label11.Size = New Size(189, 17)
        Label11.TabIndex = 23
        Label11.Text = "Data:"
        Label11.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' ImageList1
        ' 
        ImageList1.ColorDepth = ColorDepth.Depth32Bit
        ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), ImageListStreamer)
        ImageList1.TransparentColor = Color.Transparent
        ImageList1.Images.SetKeyName(0, "Emoji Objects-117.png")
        ImageList1.Images.SetKeyName(1, "Emoji Smiley-72.png")
        ImageList1.Images.SetKeyName(2, "Emoji Symbols-202.png")
        ImageList1.Images.SetKeyName(3, "Emoji Symbols-211  Yellow.png")
        ImageList1.Images.SetKeyName(4, "Emoji Symbols-199.png")
        ImageList1.Images.SetKeyName(5, "Emoji Symbols-200.png")
        ImageList1.Images.SetKeyName(6, "Emoji Symbols-202 - Copy.png")
        ImageList1.Images.SetKeyName(7, "Setting.png")
        ImageList1.Images.SetKeyName(8, "Emoji Objects-108.png")
        ImageList1.Images.SetKeyName(9, "Emoji Symbols-201.png")
        ImageList1.Images.SetKeyName(10, "xp0005.ico")
        ImageList1.Images.SetKeyName(11, "icons8-docx-64.png")
        ImageList1.Images.SetKeyName(12, "icons8-aggiorna-96.png")
        ImageList1.Images.SetKeyName(13, "icons8-rimuovere-96.png")
        ImageList1.Images.SetKeyName(14, "Emoji Symbols-206.png")
        ImageList1.Images.SetKeyName(15, "word.ico")
        ImageList1.Images.SetKeyName(16, "icons8-eliminare-96.png")
        ImageList1.Images.SetKeyName(17, "icons8-printer-96.png")
        ' 
        ' DGV1
        ' 
        DGV1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DGV1.Columns.AddRange(New DataGridViewColumn() {Column1, Column5, Column2, Column3, Column4, Column6, Column7})
        DGV1.Location = New Point(2, 38)
        DGV1.Name = "DGV1"
        DGV1.RowHeadersVisible = False
        DGV1.Size = New Size(535, 268)
        DGV1.TabIndex = 37
        ' 
        ' Column1
        ' 
        Column1.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.Font = New Font("Calibri", 6.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle1.NullValue = False
        Column1.DefaultCellStyle = DataGridViewCellStyle1
        Column1.HeaderText = ""
        Column1.Name = "Column1"
        Column1.ToolTipText = "Foglio da stampare"
        Column1.Width = 30
        ' 
        ' Column5
        ' 
        Column5.HeaderText = "Stato"
        Column5.Name = "Column5"
        Column5.Resizable = DataGridViewTriState.True
        Column5.SortMode = DataGridViewColumnSortMode.Automatic
        Column5.Width = 40
        ' 
        ' Column2
        ' 
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.WrapMode = DataGridViewTriState.True
        Column2.DefaultCellStyle = DataGridViewCellStyle2
        Column2.HeaderText = "Bambino"
        Column2.Name = "Column2"
        Column2.Resizable = DataGridViewTriState.True
        Column2.Width = 120
        ' 
        ' Column3
        ' 
        DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter
        Column3.DefaultCellStyle = DataGridViewCellStyle3
        Column3.HeaderText = "Data Utilizzo"
        Column3.Name = "Column3"
        ' 
        ' Column4
        ' 
        DataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.Font = New Font("Calibri", 6.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle4.WrapMode = DataGridViewTriState.True
        Column4.DefaultCellStyle = DataGridViewCellStyle4
        Column4.HeaderText = "Filename"
        Column4.Name = "Column4"
        Column4.Width = 270
        ' 
        ' Column6
        ' 
        Column6.HeaderText = "j"
        Column6.Name = "Column6"
        Column6.Visible = False
        ' 
        ' Column7
        ' 
        Column7.HeaderText = "IMG_INDEX"
        Column7.Name = "Column7"
        Column7.Visible = False
        ' 
        ' Label1
        ' 
        Label1.BackColor = SystemColors.Control
        Label1.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label1.ImageAlign = ContentAlignment.MiddleLeft
        Label1.ImageIndex = 8
        Label1.ImageList = ImageList1
        Label1.Location = New Point(81, 11)
        Label1.Name = "Label1"
        Label1.Size = New Size(58, 17)
        Label1.TabIndex = 38
        Label1.Text = "Data:"
        Label1.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(Button8)
        GroupBox1.Controls.Add(Label12)
        GroupBox1.Controls.Add(Label13)
        GroupBox1.Controls.Add(Button5)
        GroupBox1.Controls.Add(Label10)
        GroupBox1.Controls.Add(Label4)
        GroupBox1.Controls.Add(Label9)
        GroupBox1.Controls.Add(Label6)
        GroupBox1.Controls.Add(Label7)
        GroupBox1.Controls.Add(Label5)
        GroupBox1.Controls.Add(Label8)
        GroupBox1.Controls.Add(Label2)
        GroupBox1.Controls.Add(Label3)
        GroupBox1.Font = New Font("Microsoft Sans Serif", 8.25F)
        GroupBox1.Location = New Point(6, 309)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(532, 84)
        GroupBox1.TabIndex = 41
        GroupBox1.TabStop = False
        GroupBox1.Text = "Dettagli"
        ' 
        ' Button8
        ' 
        Button8.ImageIndex = 13
        Button8.ImageList = ImageList1
        Button8.Location = New Point(507, 55)
        Button8.Name = "Button8"
        Button8.Size = New Size(23, 23)
        Button8.TabIndex = 51
        TP1.SetToolTip(Button8, "Open DOCX")
        Button8.UseVisualStyleBackColor = True
        Button8.Visible = False
        ' 
        ' Label12
        ' 
        Label12.BackColor = SystemColors.Control
        Label12.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label12.Location = New Point(168, 59)
        Label12.Name = "Label12"
        Label12.Size = New Size(68, 17)
        Label12.TabIndex = 50
        Label12.Text = "N. Esercizi:"
        ' 
        ' Label13
        ' 
        Label13.BackColor = SystemColors.Control
        Label13.BorderStyle = BorderStyle.Fixed3D
        Label13.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label13.Location = New Point(240, 59)
        Label13.Name = "Label13"
        Label13.Size = New Size(29, 17)
        Label13.TabIndex = 49
        Label13.Text = "- -"
        ' 
        ' Button5
        ' 
        Button5.ImageIndex = 15
        Button5.ImageList = ImageList1
        Button5.Location = New Point(283, 55)
        Button5.Name = "Button5"
        Button5.Size = New Size(32, 23)
        Button5.TabIndex = 48
        TP1.SetToolTip(Button5, "Open DOCX")
        Button5.UseVisualStyleBackColor = True
        Button5.Visible = False
        ' 
        ' Label10
        ' 
        Label10.BackColor = SystemColors.Control
        Label10.BorderStyle = BorderStyle.Fixed3D
        Label10.Font = New Font("Microsoft Sans Serif", 8.25F)
        Label10.Location = New Point(351, 15)
        Label10.Name = "Label10"
        Label10.Size = New Size(155, 17)
        Label10.TabIndex = 47
        Label10.Text = "- -"
        ' 
        ' Label4
        ' 
        Label4.BackColor = SystemColors.Control
        Label4.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label4.Location = New Point(11, 60)
        Label4.Name = "Label4"
        Label4.Size = New Size(39, 17)
        Label4.TabIndex = 42
        Label4.Text = "Data:"
        ' 
        ' Label9
        ' 
        Label9.BackColor = SystemColors.Control
        Label9.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label9.Location = New Point(295, 15)
        Label9.Name = "Label9"
        Label9.Size = New Size(42, 17)
        Label9.TabIndex = 46
        Label9.Text = "Titolo:"
        ' 
        ' Label6
        ' 
        Label6.BackColor = SystemColors.Control
        Label6.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label6.Location = New Point(11, 38)
        Label6.Name = "Label6"
        Label6.Size = New Size(61, 17)
        Label6.TabIndex = 44
        Label6.Text = "Filename:"
        ' 
        ' Label7
        ' 
        Label7.BackColor = SystemColors.Control
        Label7.BorderStyle = BorderStyle.Fixed3D
        Label7.Font = New Font("Microsoft Sans Serif", 8.25F)
        Label7.Location = New Point(74, 38)
        Label7.Name = "Label7"
        Label7.Size = New Size(432, 17)
        Label7.TabIndex = 43
        Label7.Text = "- -"
        ' 
        ' Label5
        ' 
        Label5.BackColor = SystemColors.Control
        Label5.BorderStyle = BorderStyle.Fixed3D
        Label5.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label5.Location = New Point(74, 60)
        Label5.Name = "Label5"
        Label5.Size = New Size(88, 17)
        Label5.TabIndex = 41
        Label5.Text = "- -"
        ' 
        ' Label8
        ' 
        Label8.BackColor = SystemColors.Control
        Label8.Font = New Font("Segoe UI", 9F)
        Label8.ImageAlign = ContentAlignment.MiddleLeft
        Label8.ImageIndex = 9
        Label8.ImageList = ImageList1
        Label8.Location = New Point(322, 58)
        Label8.Name = "Label8"
        Label8.Size = New Size(181, 17)
        Label8.TabIndex = 45
        Label8.Text = "     Docx presente (Data vecchia)"
        Label8.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label2
        ' 
        Label2.BackColor = SystemColors.Control
        Label2.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label2.Location = New Point(11, 16)
        Label2.Name = "Label2"
        Label2.Size = New Size(61, 17)
        Label2.TabIndex = 40
        Label2.Text = "Bambino:"
        ' 
        ' Label3
        ' 
        Label3.BackColor = SystemColors.Control
        Label3.BorderStyle = BorderStyle.Fixed3D
        Label3.Font = New Font("Microsoft Sans Serif", 8.25F)
        Label3.Location = New Point(74, 16)
        Label3.Name = "Label3"
        Label3.Size = New Size(189, 17)
        Label3.TabIndex = 39
        Label3.Text = "- -"
        ' 
        ' Label14
        ' 
        Label14.BackColor = SystemColors.Control
        Label14.Font = New Font("Segoe UI", 9F)
        Label14.Location = New Point(77, 400)
        Label14.Name = "Label14"
        Label14.Size = New Size(108, 17)
        Label14.TabIndex = 43
        Label14.Text = "N. fogli selezionati:"
        ' 
        ' Label15
        ' 
        Label15.BackColor = SystemColors.Control
        Label15.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label15.Location = New Point(188, 400)
        Label15.Name = "Label15"
        Label15.Size = New Size(29, 17)
        Label15.TabIndex = 42
        Label15.Text = "- -"
        ' 
        ' Button1
        ' 
        Button1.BackColor = Color.DimGray
        Button1.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Button1.ForeColor = Color.Snow
        Button1.Location = New Point(467, 459)
        Button1.Name = "Button1"
        Button1.Size = New Size(69, 27)
        Button1.TabIndex = 44
        Button1.Text = "Close"
        Button1.UseVisualStyleBackColor = False
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(131, 428)
        Button2.Name = "Button2"
        Button2.Size = New Size(91, 27)
        Button2.TabIndex = 45
        Button2.Text = "Prepara"
        TP1.SetToolTip(Button2, "Prepara i file .DOCX per la stampa")
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Button3
        ' 
        Button3.Enabled = False
        Button3.ImageAlign = ContentAlignment.MiddleLeft
        Button3.ImageIndex = 9
        Button3.ImageList = ImageList1
        Button3.Location = New Point(9, 428)
        Button3.Name = "Button3"
        Button3.Size = New Size(112, 27)
        Button3.TabIndex = 46
        Button3.Text = "    Elimina Vecchi "
        TP1.SetToolTip(Button3, "Elimina tutti i vecchi DOCX dal disco")
        Button3.UseVisualStyleBackColor = True
        ' 
        ' Button4
        ' 
        Button4.ImageIndex = 10
        Button4.ImageList = ImageList1
        Button4.Location = New Point(17, 395)
        Button4.Name = "Button4"
        Button4.Size = New Size(37, 24)
        Button4.TabIndex = 47
        Button4.UseVisualStyleBackColor = True
        ' 
        ' Button7
        ' 
        Button7.ImageIndex = 12
        Button7.ImageList = ImageList1
        Button7.Location = New Point(9, 5)
        Button7.Name = "Button7"
        Button7.Size = New Size(37, 27)
        Button7.TabIndex = 49
        TP1.SetToolTip(Button7, "Reload")
        Button7.UseVisualStyleBackColor = True
        ' 
        ' CheckBox1
        ' 
        CheckBox1.ImageAlign = ContentAlignment.MiddleLeft
        CheckBox1.ImageIndex = 5
        CheckBox1.Location = New Point(383, 9)
        CheckBox1.Name = "CheckBox1"
        CheckBox1.Size = New Size(142, 25)
        CheckBox1.TabIndex = 51
        CheckBox1.Text = "Selezione automatica "
        CheckBox1.TextAlign = ContentAlignment.MiddleRight
        TP1.SetToolTip(CheckBox1, "Selezione atutomatica fogli da preparare e/0 stampare")
        CheckBox1.UseVisualStyleBackColor = True
        ' 
        ' Button6
        ' 
        Button6.Location = New Point(232, 428)
        Button6.Name = "Button6"
        Button6.Size = New Size(88, 27)
        Button6.TabIndex = 48
        Button6.Text = "Stampa con:"
        Button6.UseVisualStyleBackColor = True
        ' 
        ' Label16
        ' 
        Label16.BackColor = SystemColors.Control
        Label16.Font = New Font("Segoe UI", 9F)
        Label16.Location = New Point(225, 400)
        Label16.Name = "Label16"
        Label16.Size = New Size(102, 17)
        Label16.TabIndex = 53
        Label16.Text = "N.  da preparare:"
        ' 
        ' Label17
        ' 
        Label17.BackColor = SystemColors.Control
        Label17.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label17.Location = New Point(324, 400)
        Label17.Name = "Label17"
        Label17.Size = New Size(29, 17)
        Label17.TabIndex = 52
        Label17.Text = "- -"
        ' 
        ' Label18
        ' 
        Label18.BackColor = SystemColors.Control
        Label18.Font = New Font("Segoe UI", 9F)
        Label18.Location = New Point(379, 400)
        Label18.Name = "Label18"
        Label18.Size = New Size(98, 17)
        Label18.TabIndex = 55
        Label18.Text = "N.   da stampare:"
        ' 
        ' Label19
        ' 
        Label19.BackColor = SystemColors.Control
        Label19.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label19.Location = New Point(478, 401)
        Label19.Name = "Label19"
        Label19.Size = New Size(29, 17)
        Label19.TabIndex = 54
        Label19.Text = "- -"
        ' 
        ' Label20
        ' 
        Label20.BackColor = SystemColors.Control
        Label20.Font = New Font("Segoe UI", 9F)
        Label20.ImageAlign = ContentAlignment.MiddleLeft
        Label20.ImageIndex = 17
        Label20.ImageList = ImageList1
        Label20.Location = New Point(329, 434)
        Label20.Name = "Label20"
        Label20.Size = New Size(200, 17)
        Label20.TabIndex = 56
        Label20.Text = "       Nessuna"
        TP1.SetToolTip(Label20, "Click per selezionare la stampante")
        ' 
        ' PrintDialog1
        ' 
        PrintDialog1.UseEXDialog = True
        ' 
        ' F_Elenco_da_Stampare
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(539, 489)
        ControlBox = False
        Controls.Add(Label20)
        Controls.Add(Label18)
        Controls.Add(Label19)
        Controls.Add(Label16)
        Controls.Add(Label17)
        Controls.Add(CheckBox1)
        Controls.Add(Button7)
        Controls.Add(Button6)
        Controls.Add(Button4)
        Controls.Add(Button3)
        Controls.Add(Button2)
        Controls.Add(Button1)
        Controls.Add(Label14)
        Controls.Add(Label15)
        Controls.Add(GroupBox1)
        Controls.Add(Label1)
        Controls.Add(DGV1)
        Controls.Add(Label11)
        FormBorderStyle = FormBorderStyle.Fixed3D
        Name = "F_Elenco_da_Stampare"
        StartPosition = FormStartPosition.CenterScreen
        Text = "F_Elenco_da_Stampare"
        CType(DGV1, ComponentModel.ISupportInitialize).EndInit()
        GroupBox1.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents Label11 As Label
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents DGV1 As DataGridView
    Friend WithEvents Label1 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents TP1 As ToolTip
    Friend WithEvents Label12 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Button6 As Button
    Friend WithEvents Button7 As Button
    Friend WithEvents Column1 As DataGridViewCheckBoxColumn
    Friend WithEvents Column5 As DataGridViewImageColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents Column6 As DataGridViewTextBoxColumn
    Friend WithEvents Column7 As DataGridViewTextBoxColumn
    Friend WithEvents Button8 As Button
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents Label16 As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents Label19 As Label
    Friend WithEvents Label20 As Label
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents PrintDialog1 As PrintDialog
End Class
