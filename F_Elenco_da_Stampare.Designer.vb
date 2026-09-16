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
        Label19 = New Label()
        ImageList1 = New ImageList(components)
        DGV1 = New DataGridView()
        Column1 = New DataGridViewCheckBoxColumn()
        Column5 = New DataGridViewImageColumn()
        Column2 = New DataGridViewTextBoxColumn()
        Column3 = New DataGridViewTextBoxColumn()
        Column4 = New DataGridViewTextBoxColumn()
        Column6 = New DataGridViewTextBoxColumn()
        Label1 = New Label()
        GroupBox1 = New GroupBox()
        Label10 = New Label()
        Label9 = New Label()
        Label6 = New Label()
        Label7 = New Label()
        Label4 = New Label()
        Label5 = New Label()
        Label8 = New Label()
        Label2 = New Label()
        Label3 = New Label()
        Label13 = New Label()
        Label12 = New Label()
        Label14 = New Label()
        Label15 = New Label()
        Button1 = New Button()
        Button2 = New Button()
        Button3 = New Button()
        Button4 = New Button()
        CType(DGV1, ComponentModel.ISupportInitialize).BeginInit()
        GroupBox1.SuspendLayout()
        SuspendLayout()
        ' 
        ' Label11
        ' 
        Label11.BackColor = SystemColors.Control
        Label11.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label11.Location = New Point(41, 10)
        Label11.Name = "Label11"
        Label11.Size = New Size(189, 17)
        Label11.TabIndex = 23
        Label11.Text = "Data:"
        ' 
        ' Label19
        ' 
        Label19.Font = New Font("Segoe UI", 9F)
        Label19.ImageIndex = 8
        Label19.ImageList = ImageList1
        Label19.Location = New Point(240, 9)
        Label19.Name = "Label19"
        Label19.Size = New Size(17, 14)
        Label19.TabIndex = 36
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
        ' 
        ' DGV1
        ' 
        DGV1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DGV1.Columns.AddRange(New DataGridViewColumn() {Column1, Column5, Column2, Column3, Column4, Column6})
        DGV1.Location = New Point(2, 38)
        DGV1.Name = "DGV1"
        DGV1.RowHeadersVisible = False
        DGV1.Size = New Size(536, 275)
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
        Column3.HeaderText = "Data"
        Column3.Name = "Column3"
        Column3.Width = 60
        ' 
        ' Column4
        ' 
        DataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.Font = New Font("Calibri", 6.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle4.WrapMode = DataGridViewTriState.True
        Column4.DefaultCellStyle = DataGridViewCellStyle4
        Column4.HeaderText = "Filename"
        Column4.Name = "Column4"
        Column4.Width = 300
        ' 
        ' Column6
        ' 
        Column6.HeaderText = "j"
        Column6.Name = "Column6"
        Column6.Visible = False
        ' 
        ' Label1
        ' 
        Label1.BackColor = SystemColors.Control
        Label1.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label1.Location = New Point(2, 9)
        Label1.Name = "Label1"
        Label1.Size = New Size(41, 17)
        Label1.TabIndex = 38
        Label1.Text = "Data:"
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(Label10)
        GroupBox1.Controls.Add(Label9)
        GroupBox1.Controls.Add(Label6)
        GroupBox1.Controls.Add(Label7)
        GroupBox1.Controls.Add(Label4)
        GroupBox1.Controls.Add(Label5)
        GroupBox1.Controls.Add(Label8)
        GroupBox1.Controls.Add(Label2)
        GroupBox1.Controls.Add(Label3)
        GroupBox1.Font = New Font("Microsoft Sans Serif", 8.25F)
        GroupBox1.Location = New Point(6, 347)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(532, 76)
        GroupBox1.TabIndex = 41
        GroupBox1.TabStop = False
        GroupBox1.Text = "Dettagli"
        ' 
        ' Label10
        ' 
        Label10.BackColor = SystemColors.Control
        Label10.Font = New Font("Microsoft Sans Serif", 8.25F)
        Label10.Location = New Point(361, 36)
        Label10.Name = "Label10"
        Label10.Size = New Size(165, 17)
        Label10.TabIndex = 47
        Label10.Text = "- -"
        ' 
        ' Label9
        ' 
        Label9.BackColor = SystemColors.Control
        Label9.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label9.Location = New Point(316, 36)
        Label9.Name = "Label9"
        Label9.Size = New Size(42, 17)
        Label9.TabIndex = 46
        Label9.Text = "Titolo:"
        ' 
        ' Label6
        ' 
        Label6.BackColor = SystemColors.Control
        Label6.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label6.Location = New Point(11, 53)
        Label6.Name = "Label6"
        Label6.Size = New Size(61, 17)
        Label6.TabIndex = 44
        Label6.Text = "Filename:"
        ' 
        ' Label7
        ' 
        Label7.BackColor = SystemColors.Control
        Label7.Font = New Font("Microsoft Sans Serif", 8.25F)
        Label7.Location = New Point(74, 53)
        Label7.Name = "Label7"
        Label7.Size = New Size(290, 17)
        Label7.TabIndex = 43
        Label7.Text = "- -"
        ' 
        ' Label4
        ' 
        Label4.BackColor = SystemColors.Control
        Label4.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label4.Location = New Point(11, 16)
        Label4.Name = "Label4"
        Label4.Size = New Size(110, 17)
        Label4.TabIndex = 42
        Label4.Text = "File da stampare:"
        ' 
        ' Label5
        ' 
        Label5.BackColor = SystemColors.Control
        Label5.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label5.Location = New Point(438, 35)
        Label5.Name = "Label5"
        Label5.Size = New Size(88, 17)
        Label5.TabIndex = 41
        Label5.Text = "- -"
        ' 
        ' Label8
        ' 
        Label8.BackColor = SystemColors.Control
        Label8.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label8.Location = New Point(367, 53)
        Label8.Name = "Label8"
        Label8.Size = New Size(157, 17)
        Label8.TabIndex = 45
        Label8.Text = "Stampato:"
        Label8.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label2
        ' 
        Label2.BackColor = SystemColors.Control
        Label2.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label2.Location = New Point(11, 36)
        Label2.Name = "Label2"
        Label2.Size = New Size(61, 17)
        Label2.TabIndex = 40
        Label2.Text = "Bambino:"
        ' 
        ' Label3
        ' 
        Label3.BackColor = SystemColors.Control
        Label3.Font = New Font("Microsoft Sans Serif", 8.25F)
        Label3.Location = New Point(74, 36)
        Label3.Name = "Label3"
        Label3.Size = New Size(189, 17)
        Label3.TabIndex = 39
        Label3.Text = "- -"
        ' 
        ' Label13
        ' 
        Label13.BackColor = SystemColors.Control
        Label13.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label13.Location = New Point(263, 328)
        Label13.Name = "Label13"
        Label13.Size = New Size(61, 17)
        Label13.TabIndex = 49
        Label13.Text = "0"
        ' 
        ' Label12
        ' 
        Label12.BackColor = SystemColors.Control
        Label12.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label12.Location = New Point(179, 327)
        Label12.Name = "Label12"
        Label12.Size = New Size(61, 17)
        Label12.TabIndex = 48
        Label12.Text = "N. voci:"
        ' 
        ' Label14
        ' 
        Label14.BackColor = SystemColors.Control
        Label14.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label14.Location = New Point(302, 322)
        Label14.Name = "Label14"
        Label14.Size = New Size(125, 17)
        Label14.TabIndex = 43
        Label14.Text = "N. fogli selezionati:"
        ' 
        ' Label15
        ' 
        Label15.BackColor = SystemColors.Control
        Label15.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label15.Location = New Point(435, 322)
        Label15.Name = "Label15"
        Label15.Size = New Size(49, 17)
        Label15.TabIndex = 42
        Label15.Text = "- -"
        ' 
        ' Button1
        ' 
        Button1.BackColor = Color.DimGray
        Button1.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Button1.ForeColor = Color.Snow
        Button1.Location = New Point(401, 429)
        Button1.Name = "Button1"
        Button1.Size = New Size(81, 27)
        Button1.TabIndex = 44
        Button1.Text = "Close"
        Button1.UseVisualStyleBackColor = False
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(11, 429)
        Button2.Name = "Button2"
        Button2.Size = New Size(102, 27)
        Button2.TabIndex = 45
        Button2.Text = "Prepara tutto"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Button3
        ' 
        Button3.Enabled = False
        Button3.ImageAlign = ContentAlignment.MiddleLeft
        Button3.ImageIndex = 9
        Button3.ImageList = ImageList1
        Button3.Location = New Point(262, 429)
        Button3.Name = "Button3"
        Button3.Size = New Size(123, 27)
        Button3.TabIndex = 46
        Button3.Text = "    Elimina Vecchi"
        Button3.UseVisualStyleBackColor = True
        ' 
        ' Button4
        ' 
        Button4.ImageIndex = 10
        Button4.ImageList = ImageList1
        Button4.Location = New Point(122, 429)
        Button4.Name = "Button4"
        Button4.Size = New Size(37, 27)
        Button4.TabIndex = 47
        Button4.UseVisualStyleBackColor = True
        ' 
        ' F_Elenco_da_Stampare
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(542, 461)
        Controls.Add(Label13)
        Controls.Add(Button4)
        Controls.Add(Label12)
        Controls.Add(Button3)
        Controls.Add(Button2)
        Controls.Add(Button1)
        Controls.Add(Label14)
        Controls.Add(Label15)
        Controls.Add(GroupBox1)
        Controls.Add(Label1)
        Controls.Add(DGV1)
        Controls.Add(Label19)
        Controls.Add(Label11)
        Name = "F_Elenco_da_Stampare"
        Text = "F_Elenco_da_Stampare"
        CType(DGV1, ComponentModel.ISupportInitialize).EndInit()
        GroupBox1.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents Label11 As Label
    Friend WithEvents Label19 As Label
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
    Friend WithEvents Label13 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Column1 As DataGridViewCheckBoxColumn
    Friend WithEvents Column5 As DataGridViewImageColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents Column6 As DataGridViewTextBoxColumn
End Class
