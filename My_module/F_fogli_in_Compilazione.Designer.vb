<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F_fogli_in_Compilazione
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F_fogli_in_Compilazione))
        dgv1 = New DataGridView()
        Column11 = New DataGridViewImageColumn()
        Column1 = New DataGridViewTextBoxColumn()
        Column2 = New DataGridViewTextBoxColumn()
        Column3 = New DataGridViewTextBoxColumn()
        Column12 = New DataGridViewTextBoxColumn()
        Column4 = New DataGridViewTextBoxColumn()
        Column14 = New DataGridViewTextBoxColumn()
        Label1 = New Label()
        GroupBox1 = New GroupBox()
        Label17 = New Label()
        Label18 = New Label()
        Label4 = New Label()
        ImageList1 = New ImageList(components)
        Label3 = New Label()
        Label2 = New Label()
        GroupBox2 = New GroupBox()
        Dgv2 = New DataGridView()
        Column10 = New DataGridViewImageColumn()
        DataGridViewTextBoxColumn2 = New DataGridViewTextBoxColumn()
        DataGridViewTextBoxColumn3 = New DataGridViewTextBoxColumn()
        DataGridViewTextBoxColumn4 = New DataGridViewTextBoxColumn()
        DataGridViewTextBoxColumn1 = New DataGridViewTextBoxColumn()
        Column8 = New DataGridViewTextBoxColumn()
        Column5 = New DataGridViewTextBoxColumn()
        Column6 = New DataGridViewTextBoxColumn()
        Column7 = New DataGridViewTextBoxColumn()
        Column9 = New DataGridViewTextBoxColumn()
        Column13 = New DataGridViewTextBoxColumn()
        TabControl1 = New TabControl()
        TabPage1 = New TabPage()
        Label15 = New Label()
        Label16 = New Label()
        Label13 = New Label()
        Label14 = New Label()
        Label11 = New Label()
        Label12 = New Label()
        Label10 = New Label()
        Label9 = New Label()
        Button3 = New Button()
        Button1 = New Button()
        Button6 = New Button()
        TabPage2 = New TabPage()
        Panel2 = New Panel()
        Button2 = New Button()
        Button5 = New Button()
        Panel1 = New Panel()
        Button8 = New Button()
        Button9 = New Button()
        Button7 = New Button()
        TextBox1 = New TextBox()
        Label5 = New Label()
        TabPage3 = New TabPage()
        Label21 = New Label()
        Button11 = New Button()
        Label19 = New Label()
        DGV3 = New DataGridView()
        Column15 = New DataGridViewImageColumn()
        DataGridViewTextBoxColumn9 = New DataGridViewTextBoxColumn()
        DataGridViewTextBoxColumn6 = New DataGridViewTextBoxColumn()
        DataGridViewTextBoxColumn7 = New DataGridViewTextBoxColumn()
        DataGridViewTextBoxColumn8 = New DataGridViewTextBoxColumn()
        DataGridViewTextBoxColumn10 = New DataGridViewTextBoxColumn()
        DataGridViewTextBoxColumn11 = New DataGridViewTextBoxColumn()
        DataGridViewTextBoxColumn12 = New DataGridViewTextBoxColumn()
        GroupBox3 = New GroupBox()
        Label8 = New Label()
        Label7 = New Label()
        Label6 = New Label()
        CMenuStrip1 = New ContextMenuStrip(components)
        ToolStrip_modifica = New ToolStripMenuItem()
        ToolStripSeparator2 = New ToolStripSeparator()
        ToolStrip_more = New ToolStripMenuItem()
        TSM_Azzera_Totali = New ToolStripMenuItem()
        ToolStripSeparator1 = New ToolStripSeparator()
        TSM_Segna_da_compilare = New ToolStripMenuItem()
        ToolStripSeparator3 = New ToolStripSeparator()
        TSM_Elimina_foglio = New ToolStripMenuItem()
        Button4 = New Button()
        tp1 = New ToolTip(components)
        CType(dgv1, ComponentModel.ISupportInitialize).BeginInit()
        GroupBox1.SuspendLayout()
        GroupBox2.SuspendLayout()
        CType(Dgv2, ComponentModel.ISupportInitialize).BeginInit()
        TabControl1.SuspendLayout()
        TabPage1.SuspendLayout()
        TabPage2.SuspendLayout()
        Panel2.SuspendLayout()
        Panel1.SuspendLayout()
        TabPage3.SuspendLayout()
        CType(DGV3, ComponentModel.ISupportInitialize).BeginInit()
        GroupBox3.SuspendLayout()
        CMenuStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' dgv1
        ' 
        dgv1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgv1.Columns.AddRange(New DataGridViewColumn() {Column11, Column1, Column2, Column3, Column12, Column4, Column14})
        dgv1.Location = New Point(9, 22)
        dgv1.Name = "dgv1"
        dgv1.RowHeadersVisible = False
        dgv1.Size = New Size(562, 140)
        dgv1.TabIndex = 0
        ' 
        ' Column11
        ' 
        Column11.HeaderText = ""
        Column11.Name = "Column11"
        Column11.Width = 30
        ' 
        ' Column1
        ' 
        Column1.HeaderText = "ID"
        Column1.Name = "Column1"
        Column1.ReadOnly = True
        Column1.Width = 60
        ' 
        ' Column2
        ' 
        Column2.HeaderText = "Nome"
        Column2.Name = "Column2"
        Column2.ReadOnly = True
        ' 
        ' Column3
        ' 
        Column3.HeaderText = "Stato"
        Column3.Name = "Column3"
        Column3.Width = 120
        ' 
        ' Column12
        ' 
        Column12.HeaderText = "UniqueID"
        Column12.Name = "Column12"
        Column12.Width = 120
        ' 
        ' Column4
        ' 
        Column4.HeaderText = "Enabled"
        Column4.Name = "Column4"
        Column4.Visible = False
        ' 
        ' Column14
        ' 
        Column14.HeaderText = "r"
        Column14.Name = "Column14"
        Column14.Visible = False
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(8, 20)
        Label1.Name = "Label1"
        Label1.Size = New Size(55, 15)
        Label1.TabIndex = 1
        Label1.Text = "IP Server:"
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(Label17)
        GroupBox1.Controls.Add(Label18)
        GroupBox1.Controls.Add(Label4)
        GroupBox1.Controls.Add(Label3)
        GroupBox1.Controls.Add(Label2)
        GroupBox1.Controls.Add(Label1)
        GroupBox1.Location = New Point(3, 2)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(637, 46)
        GroupBox1.TabIndex = 2
        GroupBox1.TabStop = False
        GroupBox1.Text = "Network status"
        ' 
        ' Label17
        ' 
        Label17.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label17.ImageAlign = ContentAlignment.MiddleLeft
        Label17.ImageIndex = 3
        Label17.Location = New Point(523, 16)
        Label17.Name = "Label17"
        Label17.Size = New Size(24, 20)
        Label17.TabIndex = 6
        Label17.Text = "0"
        Label17.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label18
        ' 
        Label18.AutoSize = True
        Label18.Location = New Point(392, 19)
        Label18.Name = "Label18"
        Label18.Size = New Size(129, 15)
        Label18.TabIndex = 5
        Label18.Text = "N. fogli compilati oggi:"
        ' 
        ' Label4
        ' 
        Label4.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label4.ImageAlign = ContentAlignment.MiddleLeft
        Label4.ImageIndex = 3
        Label4.ImageList = ImageList1
        Label4.Location = New Point(251, 16)
        Label4.Name = "Label4"
        Label4.Size = New Size(58, 20)
        Label4.TabIndex = 4
        Label4.Text = "        OFF"
        Label4.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' ImageList1
        ' 
        ImageList1.ColorDepth = ColorDepth.Depth32Bit
        ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), ImageListStreamer)
        ImageList1.TransparentColor = Color.Transparent
        ImageList1.Images.SetKeyName(0, "Emoji Symbols-201.png")
        ImageList1.Images.SetKeyName(1, "Emoji Symbols-202.png")
        ImageList1.Images.SetKeyName(2, "icons8-wi-fi-connesso-96.png")
        ImageList1.Images.SetKeyName(3, "icons8-wi-fi-disconnected-96.png")
        ImageList1.Images.SetKeyName(4, "icons8-aggiorna-96.png")
        ImageList1.Images.SetKeyName(5, "Emoji Symbols-211  Yellow.png")
        ImageList1.Images.SetKeyName(6, "icons8-segno-di-spunta-96.png")
        ImageList1.Images.SetKeyName(7, "icons8-informazioni-144.png")
        ImageList1.Images.SetKeyName(8, "Setting.png")
        ImageList1.Images.SetKeyName(9, "icons8-rimuovere-96.png")
        ImageList1.Images.SetKeyName(10, "Emoji Objects-108.png")
        ImageList1.Images.SetKeyName(11, "icons8-eliminare-96.png")
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(176, 19)
        Label3.Name = "Label3"
        Label3.Size = New Size(69, 15)
        Label3.TabIndex = 3
        Label3.Text = "Stato Server"
        ' 
        ' Label2
        ' 
        Label2.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label2.Location = New Point(69, 20)
        Label2.Name = "Label2"
        Label2.Size = New Size(93, 15)
        Label2.TabIndex = 2
        Label2.Text = "182.168.1.129"
        ' 
        ' GroupBox2
        ' 
        GroupBox2.Controls.Add(dgv1)
        GroupBox2.Location = New Point(6, 6)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Size = New Size(600, 176)
        GroupBox2.TabIndex = 3
        GroupBox2.TabStop = False
        GroupBox2.Text = "Client registrati"
        ' 
        ' Dgv2
        ' 
        Dgv2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Dgv2.Columns.AddRange(New DataGridViewColumn() {Column10, DataGridViewTextBoxColumn2, DataGridViewTextBoxColumn3, DataGridViewTextBoxColumn4, DataGridViewTextBoxColumn1, Column8, Column5, Column6, Column7, Column9, Column13})
        Dgv2.Location = New Point(5, 9)
        Dgv2.Name = "Dgv2"
        Dgv2.RowHeadersVisible = False
        Dgv2.Size = New Size(579, 188)
        Dgv2.TabIndex = 0
        ' 
        ' Column10
        ' 
        Column10.HeaderText = ""
        Column10.Name = "Column10"
        Column10.Resizable = DataGridViewTriState.True
        Column10.SortMode = DataGridViewColumnSortMode.Automatic
        Column10.Width = 30
        ' 
        ' DataGridViewTextBoxColumn2
        ' 
        DataGridViewTextBoxColumn2.HeaderText = "Device"
        DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        DataGridViewTextBoxColumn2.ReadOnly = True
        DataGridViewTextBoxColumn2.Width = 60
        ' 
        ' DataGridViewTextBoxColumn3
        ' 
        DataGridViewTextBoxColumn3.HeaderText = "Bambino"
        DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        DataGridViewTextBoxColumn3.Width = 160
        ' 
        ' DataGridViewTextBoxColumn4
        ' 
        DataGridViewTextBoxColumn4.HeaderText = "Foglio"
        DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        DataGridViewTextBoxColumn4.Width = 120
        ' 
        ' DataGridViewTextBoxColumn1
        ' 
        DataGridViewTextBoxColumn1.HeaderText = "Stato"
        DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        DataGridViewTextBoxColumn1.ReadOnly = True
        DataGridViewTextBoxColumn1.Width = 90
        ' 
        ' Column8
        ' 
        Column8.HeaderText = "Data carico"
        Column8.Name = "Column8"
        Column8.Width = 130
        ' 
        ' Column5
        ' 
        Column5.HeaderText = "ID"
        Column5.Name = "Column5"
        Column5.Visible = False
        ' 
        ' Column6
        ' 
        Column6.HeaderText = "ID_FOGLIO"
        Column6.Name = "Column6"
        Column6.Visible = False
        ' 
        ' Column7
        ' 
        Column7.HeaderText = "ID_Bambino"
        Column7.Name = "Column7"
        Column7.Visible = False
        ' 
        ' Column9
        ' 
        Column9.HeaderText = "Client_ID"
        Column9.Name = "Column9"
        Column9.Visible = False
        ' 
        ' Column13
        ' 
        Column13.HeaderText = "j"
        Column13.Name = "Column13"
        Column13.Visible = False
        ' 
        ' TabControl1
        ' 
        TabControl1.Controls.Add(TabPage1)
        TabControl1.Controls.Add(TabPage2)
        TabControl1.Controls.Add(TabPage3)
        TabControl1.Location = New Point(3, 54)
        TabControl1.Name = "TabControl1"
        TabControl1.SelectedIndex = 0
        TabControl1.Size = New Size(637, 283)
        TabControl1.TabIndex = 5
        ' 
        ' TabPage1
        ' 
        TabPage1.Controls.Add(Label15)
        TabPage1.Controls.Add(Label16)
        TabPage1.Controls.Add(Label13)
        TabPage1.Controls.Add(Label14)
        TabPage1.Controls.Add(Label11)
        TabPage1.Controls.Add(Label12)
        TabPage1.Controls.Add(Label10)
        TabPage1.Controls.Add(Label9)
        TabPage1.Controls.Add(Button3)
        TabPage1.Controls.Add(Button1)
        TabPage1.Controls.Add(Button6)
        TabPage1.Controls.Add(Dgv2)
        TabPage1.Location = New Point(4, 24)
        TabPage1.Name = "TabPage1"
        TabPage1.Padding = New Padding(3)
        TabPage1.Size = New Size(629, 255)
        TabPage1.TabIndex = 0
        TabPage1.Text = "Fogli in compilazione"
        TabPage1.UseVisualStyleBackColor = True
        ' 
        ' Label15
        ' 
        Label15.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label15.Location = New Point(273, 208)
        Label15.Name = "Label15"
        Label15.Size = New Size(155, 15)
        Label15.TabIndex = 69
        Label15.Text = "182.168.1.129"
        ' 
        ' Label16
        ' 
        Label16.AutoSize = True
        Label16.Location = New Point(196, 207)
        Label16.Name = "Label16"
        Label16.Size = New Size(43, 15)
        Label16.TabIndex = 68
        Label16.Text = "Foglio:"
        ' 
        ' Label13
        ' 
        Label13.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label13.Location = New Point(66, 208)
        Label13.Name = "Label13"
        Label13.Size = New Size(117, 15)
        Label13.TabIndex = 67
        Label13.Text = "182.168.1.129"
        ' 
        ' Label14
        ' 
        Label14.AutoSize = True
        Label14.Location = New Point(7, 207)
        Label14.Name = "Label14"
        Label14.Size = New Size(58, 15)
        Label14.TabIndex = 66
        Label14.Text = "Bambino:"
        ' 
        ' Label11
        ' 
        Label11.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label11.Location = New Point(270, 231)
        Label11.Name = "Label11"
        Label11.Size = New Size(165, 15)
        Label11.TabIndex = 65
        Label11.Text = "21-12-1999 12:00:12"
        ' 
        ' Label12
        ' 
        Label12.Location = New Point(194, 231)
        Label12.Name = "Label12"
        Label12.Size = New Size(77, 15)
        Label12.TabIndex = 64
        Label12.Text = "Richiesta del:"
        ' 
        ' Label10
        ' 
        Label10.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label10.Location = New Point(65, 231)
        Label10.Name = "Label10"
        Label10.Size = New Size(116, 16)
        Label10.TabIndex = 63
        Label10.Text = "182.168.1.129"
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.Location = New Point(6, 231)
        Label9.Name = "Label9"
        Label9.Size = New Size(45, 15)
        Label9.TabIndex = 62
        Label9.Text = "Device:"
        ' 
        ' Button3
        ' 
        Button3.BackColor = SystemColors.Control
        Button3.Enabled = False
        Button3.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Button3.ForeColor = SystemColors.ControlText
        Button3.ImageAlign = ContentAlignment.MiddleLeft
        Button3.ImageIndex = 9
        Button3.ImageList = ImageList1
        Button3.Location = New Point(486, 224)
        Button3.Name = "Button3"
        Button3.Size = New Size(76, 25)
        Button3.TabIndex = 61
        Button3.Text = "      Libera"
        tp1.SetToolTip(Button3, "Rendi disponibile per la compilazione")
        Button3.UseVisualStyleBackColor = False
        ' 
        ' Button1
        ' 
        Button1.BackColor = SystemColors.Control
        Button1.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Button1.ForeColor = SystemColors.ControlText
        Button1.ImageIndex = 7
        Button1.ImageList = ImageList1
        Button1.Location = New Point(589, 171)
        Button1.Name = "Button1"
        Button1.Size = New Size(31, 28)
        Button1.TabIndex = 56
        Button1.UseVisualStyleBackColor = False
        ' 
        ' Button6
        ' 
        Button6.BackColor = SystemColors.Control
        Button6.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Button6.ForeColor = SystemColors.ControlText
        Button6.ImageIndex = 4
        Button6.ImageList = ImageList1
        Button6.Location = New Point(589, 9)
        Button6.Name = "Button6"
        Button6.Size = New Size(31, 28)
        Button6.TabIndex = 55
        Button6.UseVisualStyleBackColor = False
        ' 
        ' TabPage2
        ' 
        TabPage2.Controls.Add(Panel2)
        TabPage2.Controls.Add(Panel1)
        TabPage2.Controls.Add(Button7)
        TabPage2.Controls.Add(TextBox1)
        TabPage2.Controls.Add(Label5)
        TabPage2.Controls.Add(GroupBox2)
        TabPage2.Location = New Point(4, 24)
        TabPage2.Name = "TabPage2"
        TabPage2.Padding = New Padding(3)
        TabPage2.Size = New Size(629, 255)
        TabPage2.TabIndex = 1
        TabPage2.Text = "Device"
        TabPage2.UseVisualStyleBackColor = True
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(Button2)
        Panel2.Controls.Add(Button5)
        Panel2.Location = New Point(358, 197)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(179, 33)
        Panel2.TabIndex = 60
        Panel2.Visible = False
        ' 
        ' Button2
        ' 
        Button2.BackColor = SystemColors.Control
        Button2.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Button2.ForeColor = SystemColors.ControlText
        Button2.ImageIndex = 7
        Button2.Location = New Point(15, 5)
        Button2.Name = "Button2"
        Button2.Size = New Size(69, 23)
        Button2.TabIndex = 62
        Button2.Text = "Rinomina"
        Button2.UseVisualStyleBackColor = False
        ' 
        ' Button5
        ' 
        Button5.BackColor = SystemColors.Control
        Button5.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Button5.ForeColor = SystemColors.ControlText
        Button5.ImageIndex = 7
        Button5.Location = New Point(111, 5)
        Button5.Name = "Button5"
        Button5.Size = New Size(56, 23)
        Button5.TabIndex = 63
        Button5.Text = "Abilita"
        Button5.UseVisualStyleBackColor = False
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(Button8)
        Panel1.Controls.Add(Button9)
        Panel1.Location = New Point(360, 234)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(179, 33)
        Panel1.TabIndex = 59
        Panel1.Visible = False
        ' 
        ' Button8
        ' 
        Button8.BackColor = SystemColors.Control
        Button8.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Button8.ForeColor = SystemColors.ControlText
        Button8.ImageIndex = 7
        Button8.Location = New Point(110, 5)
        Button8.Name = "Button8"
        Button8.Size = New Size(56, 23)
        Button8.TabIndex = 65
        Button8.Text = "Esci"
        Button8.UseVisualStyleBackColor = False
        ' 
        ' Button9
        ' 
        Button9.BackColor = SystemColors.Control
        Button9.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Button9.ForeColor = SystemColors.ControlText
        Button9.ImageIndex = 7
        Button9.Location = New Point(14, 5)
        Button9.Name = "Button9"
        Button9.Size = New Size(69, 23)
        Button9.TabIndex = 64
        Button9.Text = "Salva"
        Button9.UseVisualStyleBackColor = False
        ' 
        ' Button7
        ' 
        Button7.BackColor = SystemColors.Control
        Button7.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Button7.ForeColor = SystemColors.ControlText
        Button7.ImageIndex = 9
        Button7.ImageList = ImageList1
        Button7.Location = New Point(545, 201)
        Button7.Name = "Button7"
        Button7.Size = New Size(25, 26)
        Button7.TabIndex = 64
        Button7.UseVisualStyleBackColor = False
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(141, 204)
        TextBox1.Name = "TextBox1"
        TextBox1.ReadOnly = True
        TextBox1.Size = New Size(205, 23)
        TextBox1.TabIndex = 61
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(11, 209)
        Label5.Name = "Label5"
        Label5.Size = New Size(122, 15)
        Label5.TabIndex = 60
        Label5.Text = "Nome del dispositivo:"
        ' 
        ' TabPage3
        ' 
        TabPage3.Controls.Add(Label21)
        TabPage3.Controls.Add(Button11)
        TabPage3.Controls.Add(Label19)
        TabPage3.Controls.Add(DGV3)
        TabPage3.Location = New Point(4, 24)
        TabPage3.Name = "TabPage3"
        TabPage3.Padding = New Padding(3)
        TabPage3.Size = New Size(629, 255)
        TabPage3.TabIndex = 2
        TabPage3.Text = "Stato compilazione"
        TabPage3.UseVisualStyleBackColor = True
        ' 
        ' Label21
        ' 
        Label21.BackColor = SystemColors.Control
        Label21.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label21.ImageAlign = ContentAlignment.MiddleLeft
        Label21.ImageIndex = 10
        Label21.ImageList = ImageList1
        Label21.Location = New Point(350, 8)
        Label21.Name = "Label21"
        Label21.Size = New Size(189, 23)
        Label21.TabIndex = 71
        Label21.Text = "         Data:"
        Label21.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Button11
        ' 
        Button11.BackColor = SystemColors.Control
        Button11.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Button11.ForeColor = SystemColors.ControlText
        Button11.ImageIndex = 4
        Button11.ImageList = ImageList1
        Button11.Location = New Point(6, 6)
        Button11.Name = "Button11"
        Button11.Size = New Size(31, 28)
        Button11.TabIndex = 70
        Button11.UseVisualStyleBackColor = False
        ' 
        ' Label19
        ' 
        Label19.BorderStyle = BorderStyle.Fixed3D
        Label19.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label19.Location = New Point(40, 12)
        Label19.Name = "Label19"
        Label19.Size = New Size(278, 18)
        Label19.TabIndex = 68
        Label19.Text = "Risassunto dello stato di compilazione dei fogli:"
        ' 
        ' DGV3
        ' 
        DGV3.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DGV3.Columns.AddRange(New DataGridViewColumn() {Column15, DataGridViewTextBoxColumn9, DataGridViewTextBoxColumn6, DataGridViewTextBoxColumn7, DataGridViewTextBoxColumn8, DataGridViewTextBoxColumn10, DataGridViewTextBoxColumn11, DataGridViewTextBoxColumn12})
        DGV3.Location = New Point(17, 45)
        DGV3.Name = "DGV3"
        DGV3.RowHeadersVisible = False
        DGV3.Size = New Size(596, 198)
        DGV3.TabIndex = 1
        ' 
        ' Column15
        ' 
        Column15.HeaderText = ""
        Column15.Name = "Column15"
        Column15.Width = 40
        ' 
        ' DataGridViewTextBoxColumn9
        ' 
        DataGridViewTextBoxColumn9.HeaderText = "Data"
        DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        DataGridViewTextBoxColumn9.Width = 80
        ' 
        ' DataGridViewTextBoxColumn6
        ' 
        DataGridViewTextBoxColumn6.HeaderText = "Bambino"
        DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        DataGridViewTextBoxColumn6.Width = 130
        ' 
        ' DataGridViewTextBoxColumn7
        ' 
        DataGridViewTextBoxColumn7.HeaderText = "IDF"
        DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        DataGridViewTextBoxColumn7.Width = 30
        ' 
        ' DataGridViewTextBoxColumn8
        ' 
        DataGridViewTextBoxColumn8.HeaderText = "Stato"
        DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        DataGridViewTextBoxColumn8.ReadOnly = True
        ' 
        ' DataGridViewTextBoxColumn10
        ' 
        DataGridViewTextBoxColumn10.HeaderText = "Note"
        DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        DataGridViewTextBoxColumn10.Width = 200
        ' 
        ' DataGridViewTextBoxColumn11
        ' 
        DataGridViewTextBoxColumn11.HeaderText = "ID_FOGLIO"
        DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        DataGridViewTextBoxColumn11.Visible = False
        ' 
        ' DataGridViewTextBoxColumn12
        ' 
        DataGridViewTextBoxColumn12.HeaderText = "DBBambino"
        DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
        DataGridViewTextBoxColumn12.Visible = False
        ' 
        ' GroupBox3
        ' 
        GroupBox3.Controls.Add(Label8)
        GroupBox3.Controls.Add(Label7)
        GroupBox3.Controls.Add(Label6)
        GroupBox3.Location = New Point(646, 12)
        GroupBox3.Name = "GroupBox3"
        GroupBox3.Size = New Size(135, 116)
        GroupBox3.TabIndex = 56
        GroupBox3.TabStop = False
        GroupBox3.Text = "Info"
        GroupBox3.Visible = False
        ' 
        ' Label8
        ' 
        Label8.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label8.ImageAlign = ContentAlignment.MiddleLeft
        Label8.ImageKey = "icons8-segno-di-spunta-96.png"
        Label8.ImageList = ImageList1
        Label8.Location = New Point(6, 77)
        Label8.Name = "Label8"
        Label8.Size = New Size(123, 20)
        Label8.TabIndex = 8
        Label8.Text = "         Compilato"
        Label8.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label7
        ' 
        Label7.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label7.ImageAlign = ContentAlignment.MiddleLeft
        Label7.ImageKey = "Emoji Symbols-211  Yellow.png"
        Label7.ImageList = ImageList1
        Label7.Location = New Point(6, 49)
        Label7.Name = "Label7"
        Label7.Size = New Size(123, 20)
        Label7.TabIndex = 7
        Label7.Text = "         In compilazione"
        Label7.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label6
        ' 
        Label6.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label6.ImageAlign = ContentAlignment.MiddleLeft
        Label6.ImageKey = "Emoji Symbols-202.png"
        Label6.ImageList = ImageList1
        Label6.Location = New Point(6, 20)
        Label6.Name = "Label6"
        Label6.Size = New Size(123, 20)
        Label6.TabIndex = 6
        Label6.Text = "         da compilare"
        Label6.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' CMenuStrip1
        ' 
        CMenuStrip1.ImageScalingSize = New Size(24, 24)
        CMenuStrip1.Items.AddRange(New ToolStripItem() {ToolStrip_modifica, ToolStripSeparator2, ToolStrip_more})
        CMenuStrip1.Name = "CMenuStrip1"
        CMenuStrip1.Size = New Size(174, 70)
        ' 
        ' ToolStrip_modifica
        ' 
        ToolStrip_modifica.Image = My.Resources.Resources.Emoji_Objects_117
        ToolStrip_modifica.Name = "ToolStrip_modifica"
        ToolStrip_modifica.Size = New Size(173, 30)
        ToolStrip_modifica.Text = "Rendi disponibile"
        ' 
        ' ToolStripSeparator2
        ' 
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New Size(170, 6)
        ' 
        ' ToolStrip_more
        ' 
        ToolStrip_more.DropDownItems.AddRange(New ToolStripItem() {TSM_Azzera_Totali, ToolStripSeparator1, TSM_Segna_da_compilare, ToolStripSeparator3, TSM_Elimina_foglio})
        ToolStrip_more.Name = "ToolStrip_more"
        ToolStrip_more.Size = New Size(173, 30)
        ToolStrip_more.Text = "More..."
        ' 
        ' TSM_Azzera_Totali
        ' 
        TSM_Azzera_Totali.ForeColor = Color.OrangeRed
        TSM_Azzera_Totali.Name = "TSM_Azzera_Totali"
        TSM_Azzera_Totali.Size = New Size(211, 22)
        TSM_Azzera_Totali.Text = "Azzera tutti i totali"
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(208, 6)
        ' 
        ' TSM_Segna_da_compilare
        ' 
        TSM_Segna_da_compilare.ForeColor = Color.OrangeRed
        TSM_Segna_da_compilare.Name = "TSM_Segna_da_compilare"
        TSM_Segna_da_compilare.Size = New Size(211, 22)
        TSM_Segna_da_compilare.Text = "Segna come da compilare"
        ' 
        ' ToolStripSeparator3
        ' 
        ToolStripSeparator3.Name = "ToolStripSeparator3"
        ToolStripSeparator3.Size = New Size(208, 6)
        ' 
        ' TSM_Elimina_foglio
        ' 
        TSM_Elimina_foglio.ForeColor = Color.Red
        TSM_Elimina_foglio.Image = My.Resources.Resources.warning_sign_by_kg_just_another_warning_sigh_with_exclamation_mark_UFwEFR_clipart
        TSM_Elimina_foglio.Name = "TSM_Elimina_foglio"
        TSM_Elimina_foglio.Size = New Size(211, 22)
        TSM_Elimina_foglio.Text = "Elimina foglio"
        ' 
        ' Button4
        ' 
        Button4.BackColor = Color.DimGray
        Button4.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Button4.ForeColor = Color.Snow
        Button4.Location = New Point(561, 343)
        Button4.Name = "Button4"
        Button4.Size = New Size(66, 31)
        Button4.TabIndex = 58
        Button4.Text = "Close"
        Button4.UseVisualStyleBackColor = False
        ' 
        ' F_fogli_in_Compilazione
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(643, 378)
        ControlBox = False
        Controls.Add(Button4)
        Controls.Add(GroupBox3)
        Controls.Add(TabControl1)
        Controls.Add(GroupBox1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Name = "F_fogli_in_Compilazione"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Fogli Todolist"
        CType(dgv1, ComponentModel.ISupportInitialize).EndInit()
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        GroupBox2.ResumeLayout(False)
        CType(Dgv2, ComponentModel.ISupportInitialize).EndInit()
        TabControl1.ResumeLayout(False)
        TabPage1.ResumeLayout(False)
        TabPage1.PerformLayout()
        TabPage2.ResumeLayout(False)
        TabPage2.PerformLayout()
        Panel2.ResumeLayout(False)
        Panel1.ResumeLayout(False)
        TabPage3.ResumeLayout(False)
        CType(DGV3, ComponentModel.ISupportInitialize).EndInit()
        GroupBox3.ResumeLayout(False)
        CMenuStrip1.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents dgv1 As DataGridView
    Friend WithEvents Label1 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents Dgv2 As DataGridView
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Button6 As Button
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents CMenuStrip1 As ContextMenuStrip
    Friend WithEvents ToolStrip_modifica As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents ToolStrip_more As ToolStripMenuItem
    Friend WithEvents TSM_Azzera_Totali As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents TSM_Segna_da_compilare As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents TSM_Elimina_foglio As ToolStripMenuItem
    Friend WithEvents Button4 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Column11 As DataGridViewImageColumn
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents Column12 As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents Column14 As DataGridViewTextBoxColumn
    Friend WithEvents Button7 As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Button8 As Button
    Friend WithEvents Button9 As Button
    Friend WithEvents tp1 As ToolTip
    Friend WithEvents Label17 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents Label19 As Label
    Friend WithEvents DGV3 As DataGridView
    Friend WithEvents Button11 As Button
    Friend WithEvents Label21 As Label
    Friend WithEvents Column10 As DataGridViewImageColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents Column8 As DataGridViewTextBoxColumn
    Friend WithEvents Column5 As DataGridViewTextBoxColumn
    Friend WithEvents Column6 As DataGridViewTextBoxColumn
    Friend WithEvents Column7 As DataGridViewTextBoxColumn
    Friend WithEvents Column9 As DataGridViewTextBoxColumn
    Friend WithEvents Column13 As DataGridViewTextBoxColumn
    Friend WithEvents Column15 As DataGridViewImageColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn11 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn12 As DataGridViewTextBoxColumn
End Class
