<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F_Foglio_esistente
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
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F_Foglio_esistente))
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
        GroupBox4 = New GroupBox()
        LabelV = New Label()
        DGV1 = New DataGridView()
        col1 = New DataGridViewTextBoxColumn()
        Col2 = New DataGridViewTextBoxColumn()
        Column4 = New DataGridViewTextBoxColumn()
        Column5 = New DataGridViewTextBoxColumn()
        j = New DataGridViewTextBoxColumn()
        stato = New DataGridViewImageColumn()
        CheckBox4 = New CheckBox()
        Label23 = New Label()
        Button6 = New Button()
        ComboBox1 = New ComboBox()
        Label11 = New Label()
        Dtp1 = New DateTimePicker()
        Button5 = New Button()
        GroupBox_VOCI = New GroupBox()
        Button_voci = New Button()
        Label21 = New Label()
        Label20 = New Label()
        Label12 = New Label()
        TextBox5 = New TextBox()
        TextBox6 = New TextBox()
        TextBox7 = New TextBox()
        Label9 = New Label()
        L_V = New Label()
        Label4 = New Label()
        TextBox3 = New TextBox()
        Label3 = New Label()
        TextBox2 = New TextBox()
        Label2 = New Label()
        TextBox1 = New TextBox()
        Label1 = New Label()
        TextBox8 = New TextBox()
        LCP1 = New Label()
        Label5 = New Label()
        Label6 = New Label()
        ImageList1 = New ImageList(components)
        CMenuStrip1 = New ContextMenuStrip(components)
        ToolStrip_modifica = New ToolStripMenuItem()
        ToolStripSeparator2 = New ToolStripSeparator()
        ToolStrip_more = New ToolStripMenuItem()
        ToolStripMenuItem1 = New ToolStripMenuItem()
        ToolStripSeparator1 = New ToolStripSeparator()
        ToolStripMenuItem2 = New ToolStripMenuItem()
        ToolStripSeparator3 = New ToolStripSeparator()
        ToolStripMenuItem3 = New ToolStripMenuItem()
        Label29 = New Label()
        Label28 = New Label()
        CheckBox3 = New CheckBox()
        DateTimePicker1 = New DateTimePicker()
        Label14 = New Label()
        ComboBox2 = New ComboBox()
        Label24 = New Label()
        Button4 = New Button()
        Label26 = New Label()
        GroupBox3 = New GroupBox()
        Label19 = New Label()
        CheckBox1 = New CheckBox()
        TP1 = New ToolTip(components)
        Label18 = New Label()
        Tab_option = New TabControl()
        TabPage1 = New TabPage()
        TabPage2 = New TabPage()
        GroupBox_CP = New GroupBox()
        Button_CP = New Button()
        L_CP = New Label()
        DGV2 = New DataGridView()
        DataGridViewTextBoxColumn2 = New DataGridViewTextBoxColumn()
        DataGridViewTextBoxColumn3 = New DataGridViewImageColumn()
        DataGridViewTextBoxColumn5 = New DataGridViewTextBoxColumn()
        GroupBoxRT = New GroupBox()
        LabelRT = New Label()
        GroupBoxCP = New GroupBox()
        LabelCP = New Label()
        DGV3 = New DataGridView()
        DataGridViewTextBoxColumn1 = New DataGridViewTextBoxColumn()
        DataGridViewTextBoxColumn4 = New DataGridViewTextBoxColumn()
        DataGridViewTextBoxColumn6 = New DataGridViewTextBoxColumn()
        GroupBox_RT = New GroupBox()
        Button_RT = New Button()
        PictureBox0 = New PictureBox()
        PictureBox1 = New PictureBox()
        CheckBoxRT0 = New CheckBox()
        CheckBoxRT1 = New CheckBox()
        L_RT = New Label()
        Panel_risposte = New Panel()
        Label7 = New Label()
        Button1 = New Button()
        ToolStripMenuItem5 = New ToolStripMenuItem()
        ToolStripMenuItem6 = New ToolStripMenuItem()
        GroupBox4.SuspendLayout()
        CType(DGV1, ComponentModel.ISupportInitialize).BeginInit()
        GroupBox_VOCI.SuspendLayout()
        CMenuStrip1.SuspendLayout()
        GroupBox3.SuspendLayout()
        Tab_option.SuspendLayout()
        TabPage1.SuspendLayout()
        TabPage2.SuspendLayout()
        GroupBox_CP.SuspendLayout()
        CType(DGV2, ComponentModel.ISupportInitialize).BeginInit()
        GroupBoxRT.SuspendLayout()
        GroupBoxCP.SuspendLayout()
        CType(DGV3, ComponentModel.ISupportInitialize).BeginInit()
        GroupBox_RT.SuspendLayout()
        CType(PictureBox0, ComponentModel.ISupportInitialize).BeginInit()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        Panel_risposte.SuspendLayout()
        SuspendLayout()
        ' 
        ' GroupBox4
        ' 
        GroupBox4.Controls.Add(LabelV)
        GroupBox4.Controls.Add(DGV1)
        GroupBox4.Location = New Point(8, 88)
        GroupBox4.Name = "GroupBox4"
        GroupBox4.Size = New Size(502, 176)
        GroupBox4.TabIndex = 25
        GroupBox4.TabStop = False
        GroupBox4.Text = "Foglio Voci"
        ' 
        ' LabelV
        ' 
        LabelV.AutoSize = True
        LabelV.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        LabelV.Location = New Point(139, 0)
        LabelV.Name = "LabelV"
        LabelV.Size = New Size(14, 15)
        LabelV.TabIndex = 7
        LabelV.Text = "0"
        ' 
        ' DGV1
        ' 
        DGV1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DGV1.Columns.AddRange(New DataGridViewColumn() {col1, Col2, Column4, Column5, j, stato})
        DGV1.Location = New Point(4, 22)
        DGV1.MultiSelect = False
        DGV1.Name = "DGV1"
        DGV1.RowHeadersVisible = False
        DGV1.RowHeadersWidth = 62
        DGV1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DGV1.Size = New Size(496, 146)
        DGV1.TabIndex = 0
        ' 
        ' col1
        ' 
        col1.HeaderText = "Codice"
        col1.MinimumWidth = 8
        col1.Name = "col1"
        col1.Width = 90
        ' 
        ' Col2
        ' 
        DataGridViewCellStyle1.Font = New Font("Calibri", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Col2.DefaultCellStyle = DataGridViewCellStyle1
        Col2.HeaderText = "Testo inserito"
        Col2.MinimumWidth = 8
        Col2.Name = "Col2"
        Col2.Width = 280
        ' 
        ' Column4
        ' 
        Column4.HeaderText = "Tot1"
        Column4.MinimumWidth = 8
        Column4.Name = "Column4"
        Column4.Width = 40
        ' 
        ' Column5
        ' 
        Column5.HeaderText = "Tot2"
        Column5.MinimumWidth = 8
        Column5.Name = "Column5"
        Column5.ReadOnly = True
        Column5.Width = 40
        ' 
        ' j
        ' 
        j.HeaderText = "index"
        j.MinimumWidth = 8
        j.Name = "j"
        j.Visible = False
        j.Width = 150
        ' 
        ' stato
        ' 
        stato.HeaderText = ""
        stato.MinimumWidth = 8
        stato.Name = "stato"
        stato.Resizable = DataGridViewTriState.True
        stato.SortMode = DataGridViewColumnSortMode.Automatic
        stato.Width = 150
        ' 
        ' CheckBox4
        ' 
        CheckBox4.AutoSize = True
        CheckBox4.Location = New Point(363, 37)
        CheckBox4.Name = "CheckBox4"
        CheckBox4.Size = New Size(96, 19)
        CheckBox4.TabIndex = 38
        CheckBox4.Text = "Mostra Trend"
        CheckBox4.UseVisualStyleBackColor = True
        ' 
        ' Label23
        ' 
        Label23.Font = New Font("Segoe UI", 9F)
        Label23.Location = New Point(424, 5)
        Label23.Name = "Label23"
        Label23.Size = New Size(72, 15)
        Label23.TabIndex = 37
        Label23.Text = "Admitance"
        ' 
        ' Button6
        ' 
        Button6.Font = New Font("Segoe UI", 9F)
        Button6.ForeColor = SystemColors.ControlText
        Button6.Location = New Point(527, 527)
        Button6.Name = "Button6"
        Button6.Size = New Size(137, 30)
        Button6.TabIndex = 20
        Button6.Text = "Esercizio IMPORTA"
        Button6.UseVisualStyleBackColor = True
        ' 
        ' ComboBox1
        ' 
        ComboBox1.FormattingEnabled = True
        ComboBox1.Location = New Point(54, 23)
        ComboBox1.Name = "ComboBox1"
        ComboBox1.Size = New Size(171, 23)
        ComboBox1.TabIndex = 28
        ' 
        ' Label11
        ' 
        Label11.AutoSize = True
        Label11.Location = New Point(8, 26)
        Label11.Name = "Label11"
        Label11.Size = New Size(41, 15)
        Label11.TabIndex = 22
        Label11.Text = "foglio:"
        ' 
        ' Dtp1
        ' 
        Dtp1.Enabled = False
        Dtp1.Format = DateTimePickerFormat.Short
        Dtp1.Location = New Point(35, 27)
        Dtp1.Name = "Dtp1"
        Dtp1.Size = New Size(80, 23)
        Dtp1.TabIndex = 26
        ' 
        ' Button5
        ' 
        Button5.Font = New Font("Segoe UI", 9F)
        Button5.ForeColor = SystemColors.ControlText
        Button5.Location = New Point(10, 39)
        Button5.Name = "Button5"
        Button5.Size = New Size(100, 25)
        Button5.TabIndex = 19
        Button5.Text = "Salva Modifiche"
        Button5.UseVisualStyleBackColor = True
        ' 
        ' GroupBox_VOCI
        ' 
        GroupBox_VOCI.Controls.Add(Button_voci)
        GroupBox_VOCI.Controls.Add(Label21)
        GroupBox_VOCI.Controls.Add(Label20)
        GroupBox_VOCI.Controls.Add(Label12)
        GroupBox_VOCI.Controls.Add(TextBox5)
        GroupBox_VOCI.Controls.Add(TextBox6)
        GroupBox_VOCI.Controls.Add(TextBox7)
        GroupBox_VOCI.Controls.Add(Label9)
        GroupBox_VOCI.Controls.Add(L_V)
        GroupBox_VOCI.Controls.Add(Label4)
        GroupBox_VOCI.Controls.Add(TextBox3)
        GroupBox_VOCI.Controls.Add(Label3)
        GroupBox_VOCI.Controls.Add(TextBox2)
        GroupBox_VOCI.Controls.Add(Label2)
        GroupBox_VOCI.Controls.Add(TextBox1)
        GroupBox_VOCI.Controls.Add(Label1)
        GroupBox_VOCI.Location = New Point(524, 94)
        GroupBox_VOCI.Name = "GroupBox_VOCI"
        GroupBox_VOCI.Size = New Size(248, 166)
        GroupBox_VOCI.TabIndex = 26
        GroupBox_VOCI.TabStop = False
        GroupBox_VOCI.Text = "Voci elcar"
        ' 
        ' Button_voci
        ' 
        Button_voci.BackColor = SystemColors.Control
        Button_voci.Font = New Font("Segoe UI", 9F)
        Button_voci.ForeColor = SystemColors.ControlText
        Button_voci.Location = New Point(173, 138)
        Button_voci.Name = "Button_voci"
        Button_voci.Size = New Size(58, 22)
        Button_voci.TabIndex = 41
        Button_voci.Text = "Accetta"
        Button_voci.UseVisualStyleBackColor = False
        ' 
        ' Label21
        ' 
        Label21.AutoSize = True
        Label21.Font = New Font("Segoe UI", 9F)
        Label21.Location = New Point(184, 108)
        Label21.Name = "Label21"
        Label21.Size = New Size(41, 15)
        Label21.TabIndex = 35
        Label21.Text = "Titolo:"
        ' 
        ' Label20
        ' 
        Label20.Font = New Font("Segoe UI", 9F)
        Label20.Location = New Point(184, 78)
        Label20.Name = "Label20"
        Label20.Size = New Size(41, 15)
        Label20.TabIndex = 34
        Label20.Text = "Titolo:"
        ' 
        ' Label12
        ' 
        Label12.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label12.Location = New Point(7, 104)
        Label12.Name = "Label12"
        Label12.Size = New Size(15, 17)
        Label12.TabIndex = 27
        Label12.Text = "2"
        ' 
        ' TextBox5
        ' 
        TextBox5.Location = New Point(29, 103)
        TextBox5.Name = "TextBox5"
        TextBox5.ReadOnly = True
        TextBox5.Size = New Size(44, 23)
        TextBox5.TabIndex = 26
        ' 
        ' TextBox6
        ' 
        TextBox6.Location = New Point(79, 103)
        TextBox6.Name = "TextBox6"
        TextBox6.ReadOnly = True
        TextBox6.Size = New Size(44, 23)
        TextBox6.TabIndex = 25
        ' 
        ' TextBox7
        ' 
        TextBox7.Location = New Point(131, 103)
        TextBox7.Name = "TextBox7"
        TextBox7.ReadOnly = True
        TextBox7.Size = New Size(44, 23)
        TextBox7.TabIndex = 24
        ' 
        ' Label9
        ' 
        Label9.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label9.Location = New Point(8, 75)
        Label9.Name = "Label9"
        Label9.Size = New Size(15, 17)
        Label9.TabIndex = 22
        Label9.Text = "1"
        ' 
        ' L_V
        ' 
        L_V.BorderStyle = BorderStyle.Fixed3D
        L_V.Font = New Font("Segoe UI", 8F)
        L_V.ForeColor = Color.Navy
        L_V.Location = New Point(6, 18)
        L_V.Name = "L_V"
        L_V.Size = New Size(240, 30)
        L_V.TabIndex = 8
        L_V.Text = "Descizione del codice"
        L_V.TextAlign = ContentAlignment.TopCenter
        ' 
        ' Label4
        ' 
        Label4.Location = New Point(181, 54)
        Label4.Name = "Label4"
        Label4.Size = New Size(51, 17)
        Label4.TabIndex = 6
        Label4.Text = "Totale"
        ' 
        ' TextBox3
        ' 
        TextBox3.Location = New Point(133, 74)
        TextBox3.Name = "TextBox3"
        TextBox3.ReadOnly = True
        TextBox3.Size = New Size(44, 23)
        TextBox3.TabIndex = 5
        ' 
        ' Label3
        ' 
        Label3.Location = New Point(133, 54)
        Label3.Name = "Label3"
        Label3.Size = New Size(51, 17)
        Label3.TabIndex = 4
        Label3.Text = "Prompt"
        ' 
        ' TextBox2
        ' 
        TextBox2.Location = New Point(80, 74)
        TextBox2.Name = "TextBox2"
        TextBox2.ReadOnly = True
        TextBox2.Size = New Size(44, 23)
        TextBox2.TabIndex = 3
        ' 
        ' Label2
        ' 
        Label2.Location = New Point(79, 54)
        Label2.Name = "Label2"
        Label2.Size = New Size(56, 17)
        Label2.TabIndex = 2
        Label2.Text = "Negativi"
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(29, 74)
        TextBox1.Name = "TextBox1"
        TextBox1.ReadOnly = True
        TextBox1.Size = New Size(44, 23)
        TextBox1.TabIndex = 1
        ' 
        ' Label1
        ' 
        Label1.Location = New Point(27, 54)
        Label1.Name = "Label1"
        Label1.Size = New Size(50, 17)
        Label1.TabIndex = 0
        Label1.Text = "Positivi"
        ' 
        ' TextBox8
        ' 
        TextBox8.Location = New Point(77, 56)
        TextBox8.Name = "TextBox8"
        TextBox8.ReadOnly = True
        TextBox8.Size = New Size(30, 23)
        TextBox8.TabIndex = 38
        TextBox8.TextAlign = HorizontalAlignment.Center
        ' 
        ' LCP1
        ' 
        LCP1.AutoSize = True
        LCP1.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        LCP1.Location = New Point(19, 61)
        LCP1.Name = "LCP1"
        LCP1.Size = New Size(40, 15)
        LCP1.TabIndex = 37
        LCP1.Text = "totale"
        ' 
        ' Label5
        ' 
        Label5.BackColor = Color.FromArgb(CByte(192), CByte(255), CByte(192))
        Label5.BorderStyle = BorderStyle.Fixed3D
        Label5.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label5.Location = New Point(208, 4)
        Label5.Name = "Label5"
        Label5.Size = New Size(205, 17)
        Label5.TabIndex = 27
        Label5.Text = "Foglio compilato"
        Label5.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label6
        ' 
        Label6.BackColor = Color.White
        Label6.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label6.Location = New Point(10, 4)
        Label6.Name = "Label6"
        Label6.Size = New Size(188, 17)
        Label6.TabIndex = 28
        Label6.Text = "Pinco Pallino giuseppe"
        Label6.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' ImageList1
        ' 
        ImageList1.ColorDepth = ColorDepth.Depth32Bit
        ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), ImageListStreamer)
        ImageList1.TransparentColor = Color.Transparent
        ImageList1.Images.SetKeyName(0, "Emoji Symbols-200.png")
        ImageList1.Images.SetKeyName(1, "Emoji Symbols-202.png")
        ImageList1.Images.SetKeyName(2, "Emoji Symbols-211  Yellow.png")
        ImageList1.Images.SetKeyName(3, "Emoji Symbols-201.png")
        ImageList1.Images.SetKeyName(4, "Emoji Symbols-202 - Copy.png")
        ImageList1.Images.SetKeyName(5, "Setting.png")
        ImageList1.Images.SetKeyName(6, "Emoji Objects-68.png")
        ImageList1.Images.SetKeyName(7, "icons8-eliminare-96.png")
        ImageList1.Images.SetKeyName(8, "icons8-segno-di-spunta-96.png")
        ' 
        ' CMenuStrip1
        ' 
        CMenuStrip1.ImageScalingSize = New Size(24, 24)
        CMenuStrip1.Items.AddRange(New ToolStripItem() {ToolStrip_modifica, ToolStripSeparator2, ToolStrip_more})
        CMenuStrip1.Name = "CMenuStrip1"
        CMenuStrip1.Size = New Size(158, 70)
        ' 
        ' ToolStrip_modifica
        ' 
        ToolStrip_modifica.Image = My.Resources.Resources.Emoji_Objects_117
        ToolStrip_modifica.Name = "ToolStrip_modifica"
        ToolStrip_modifica.Size = New Size(157, 30)
        ToolStrip_modifica.Text = "Edit (ON/OFF)"
        ' 
        ' ToolStripSeparator2
        ' 
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New Size(154, 6)
        ' 
        ' ToolStrip_more
        ' 
        ToolStrip_more.DropDownItems.AddRange(New ToolStripItem() {ToolStripMenuItem1, ToolStripSeparator1, ToolStripMenuItem2, ToolStripSeparator3, ToolStripMenuItem3})
        ToolStrip_more.Name = "ToolStrip_more"
        ToolStrip_more.Size = New Size(157, 30)
        ToolStrip_more.Text = "More..."
        ' 
        ' ToolStripMenuItem1
        ' 
        ToolStripMenuItem1.ForeColor = Color.OrangeRed
        ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        ToolStripMenuItem1.Size = New Size(211, 22)
        ToolStripMenuItem1.Text = "Azzera tutti i totali"
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(208, 6)
        ' 
        ' ToolStripMenuItem2
        ' 
        ToolStripMenuItem2.ForeColor = Color.OrangeRed
        ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        ToolStripMenuItem2.Size = New Size(211, 22)
        ToolStripMenuItem2.Text = "Segna come da compilare"
        ' 
        ' ToolStripSeparator3
        ' 
        ToolStripSeparator3.Name = "ToolStripSeparator3"
        ToolStripSeparator3.Size = New Size(208, 6)
        ' 
        ' ToolStripMenuItem3
        ' 
        ToolStripMenuItem3.ForeColor = Color.Red
        ToolStripMenuItem3.Image = My.Resources.Resources.warning_sign_by_kg_just_another_warning_sigh_with_exclamation_mark_UFwEFR_clipart
        ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        ToolStripMenuItem3.Size = New Size(211, 22)
        ToolStripMenuItem3.Text = "Elimina foglio"
        ' 
        ' Label29
        ' 
        Label29.AutoSize = True
        Label29.Location = New Point(121, 32)
        Label29.Name = "Label29"
        Label29.Size = New Size(16, 15)
        Label29.TabIndex = 36
        Label29.Text = "a:"
        ' 
        ' Label28
        ' 
        Label28.AutoSize = True
        Label28.Location = New Point(11, 7)
        Label28.Name = "Label28"
        Label28.Size = New Size(51, 15)
        Label28.TabIndex = 35
        Label28.Text = "Periodo:"
        ' 
        ' CheckBox3
        ' 
        CheckBox3.AutoSize = True
        CheckBox3.Checked = True
        CheckBox3.CheckState = CheckState.Checked
        CheckBox3.Location = New Point(67, 7)
        CheckBox3.Name = "CheckBox3"
        CheckBox3.Size = New Size(80, 19)
        CheckBox3.TabIndex = 34
        CheckBox3.Text = "Tutti i dati"
        CheckBox3.UseVisualStyleBackColor = True
        ' 
        ' DateTimePicker1
        ' 
        DateTimePicker1.Enabled = False
        DateTimePicker1.Format = DateTimePickerFormat.Short
        DateTimePicker1.Location = New Point(138, 27)
        DateTimePicker1.Name = "DateTimePicker1"
        DateTimePicker1.Size = New Size(80, 23)
        DateTimePicker1.TabIndex = 33
        ' 
        ' Label14
        ' 
        Label14.AutoSize = True
        Label14.Location = New Point(9, 31)
        Label14.Name = "Label14"
        Label14.Size = New Size(24, 15)
        Label14.TabIndex = 32
        Label14.Text = "Da:"
        ' 
        ' ComboBox2
        ' 
        ComboBox2.FormattingEnabled = True
        ComboBox2.Items.AddRange(New Object() {"Tutti", "Da compilare", "Compilati"})
        ComboBox2.Location = New Point(70, 9)
        ComboBox2.Name = "ComboBox2"
        ComboBox2.Size = New Size(177, 23)
        ComboBox2.TabIndex = 29
        ComboBox2.Text = "Tutti"
        ' 
        ' Label24
        ' 
        Label24.AutoSize = True
        Label24.Location = New Point(3, 12)
        Label24.Name = "Label24"
        Label24.Size = New Size(51, 15)
        Label24.TabIndex = 25
        Label24.Text = "Bambini"
        ' 
        ' Button4
        ' 
        Button4.BackColor = Color.DimGray
        Button4.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Button4.ForeColor = Color.Snow
        Button4.Location = New Point(701, 524)
        Button4.Name = "Button4"
        Button4.Size = New Size(66, 35)
        Button4.TabIndex = 34
        Button4.Text = "Close"
        Button4.UseVisualStyleBackColor = False
        ' 
        ' Label26
        ' 
        Label26.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label26.ImageIndex = 5
        Label26.ImageList = ImageList1
        Label26.Location = New Point(468, 30)
        Label26.Name = "Label26"
        Label26.Size = New Size(33, 23)
        Label26.TabIndex = 35
        ' 
        ' GroupBox3
        ' 
        GroupBox3.Controls.Add(Label19)
        GroupBox3.Controls.Add(ComboBox1)
        GroupBox3.Controls.Add(CheckBox1)
        GroupBox3.Controls.Add(Label11)
        GroupBox3.Controls.Add(CheckBox4)
        GroupBox3.Controls.Add(Label26)
        GroupBox3.Location = New Point(8, 23)
        GroupBox3.Name = "GroupBox3"
        GroupBox3.Size = New Size(502, 62)
        GroupBox3.TabIndex = 37
        GroupBox3.TabStop = False
        GroupBox3.Text = "elenco fogli"
        ' 
        ' Label19
        ' 
        Label19.BorderStyle = BorderStyle.Fixed3D
        Label19.Location = New Point(230, 25)
        Label19.Name = "Label19"
        Label19.Size = New Size(118, 21)
        Label19.TabIndex = 29
        Label19.Text = "n. 23 del 12-12-2026"
        Label19.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' CheckBox1
        ' 
        CheckBox1.AutoSize = True
        CheckBox1.Location = New Point(363, 17)
        CheckBox1.Name = "CheckBox1"
        CheckBox1.Size = New Size(108, 19)
        CheckBox1.TabIndex = 29
        CheckBox1.Text = "NON Compilati"
        CheckBox1.UseVisualStyleBackColor = True
        ' 
        ' Label18
        ' 
        Label18.AutoSize = True
        Label18.Font = New Font("Segoe UI", 27.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label18.ForeColor = Color.Red
        Label18.Location = New Point(40, 243)
        Label18.Name = "Label18"
        Label18.Size = New Size(388, 50)
        Label18.TabIndex = 38
        Label18.Text = "Nessun Foglio Trovato"
        Label18.Visible = False
        ' 
        ' Tab_option
        ' 
        Tab_option.Controls.Add(TabPage1)
        Tab_option.Controls.Add(TabPage2)
        Tab_option.Location = New Point(516, 2)
        Tab_option.Name = "Tab_option"
        Tab_option.SelectedIndex = 0
        Tab_option.Size = New Size(261, 85)
        Tab_option.TabIndex = 39
        ' 
        ' TabPage1
        ' 
        TabPage1.Controls.Add(ComboBox2)
        TabPage1.Controls.Add(Label24)
        TabPage1.Location = New Point(4, 24)
        TabPage1.Name = "TabPage1"
        TabPage1.Padding = New Padding(3)
        TabPage1.Size = New Size(253, 57)
        TabPage1.TabIndex = 0
        TabPage1.Text = "Option"
        TabPage1.UseVisualStyleBackColor = True
        ' 
        ' TabPage2
        ' 
        TabPage2.Controls.Add(Label29)
        TabPage2.Controls.Add(Dtp1)
        TabPage2.Controls.Add(Label28)
        TabPage2.Controls.Add(Label14)
        TabPage2.Controls.Add(CheckBox3)
        TabPage2.Controls.Add(DateTimePicker1)
        TabPage2.Location = New Point(4, 24)
        TabPage2.Name = "TabPage2"
        TabPage2.Padding = New Padding(3)
        TabPage2.Size = New Size(253, 57)
        TabPage2.TabIndex = 1
        TabPage2.Text = "More..."
        TabPage2.UseVisualStyleBackColor = True
        ' 
        ' GroupBox_CP
        ' 
        GroupBox_CP.Controls.Add(Button_CP)
        GroupBox_CP.Controls.Add(L_CP)
        GroupBox_CP.Controls.Add(TextBox8)
        GroupBox_CP.Controls.Add(LCP1)
        GroupBox_CP.Location = New Point(524, 271)
        GroupBox_CP.Name = "GroupBox_CP"
        GroupBox_CP.Size = New Size(251, 85)
        GroupBox_CP.TabIndex = 40
        GroupBox_CP.TabStop = False
        GroupBox_CP.Text = "Voci Comportamentali (CP)"
        ' 
        ' Button_CP
        ' 
        Button_CP.Font = New Font("Segoe UI", 9F)
        Button_CP.ForeColor = SystemColors.ControlText
        Button_CP.Location = New Point(173, 56)
        Button_CP.Name = "Button_CP"
        Button_CP.Size = New Size(58, 22)
        Button_CP.TabIndex = 40
        Button_CP.Text = "Accetta"
        Button_CP.UseVisualStyleBackColor = True
        ' 
        ' L_CP
        ' 
        L_CP.BorderStyle = BorderStyle.Fixed3D
        L_CP.Font = New Font("Segoe UI", 8F)
        L_CP.ForeColor = Color.Navy
        L_CP.Location = New Point(6, 21)
        L_CP.Name = "L_CP"
        L_CP.Size = New Size(240, 30)
        L_CP.TabIndex = 39
        L_CP.Text = "Descizione del codice"
        L_CP.TextAlign = ContentAlignment.TopCenter
        ' 
        ' DGV2
        ' 
        DGV2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DGV2.Columns.AddRange(New DataGridViewColumn() {DataGridViewTextBoxColumn2, DataGridViewTextBoxColumn3, DataGridViewTextBoxColumn5})
        DGV2.Location = New Point(6, 21)
        DGV2.MultiSelect = False
        DGV2.Name = "DGV2"
        DGV2.RowHeadersVisible = False
        DGV2.RowHeadersWidth = 62
        DGV2.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DGV2.Size = New Size(488, 134)
        DGV2.TabIndex = 39
        ' 
        ' DataGridViewTextBoxColumn2
        ' 
        DataGridViewCellStyle2.Font = New Font("Calibri", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewTextBoxColumn2.DefaultCellStyle = DataGridViewCellStyle2
        DataGridViewTextBoxColumn2.HeaderText = "Testo inserito"
        DataGridViewTextBoxColumn2.MinimumWidth = 8
        DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        DataGridViewTextBoxColumn2.Width = 430
        ' 
        ' DataGridViewTextBoxColumn3
        ' 
        DataGridViewTextBoxColumn3.HeaderText = "Stato"
        DataGridViewTextBoxColumn3.MinimumWidth = 8
        DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        DataGridViewTextBoxColumn3.Resizable = DataGridViewTriState.True
        DataGridViewTextBoxColumn3.SortMode = DataGridViewColumnSortMode.Automatic
        DataGridViewTextBoxColumn3.Width = 40
        ' 
        ' DataGridViewTextBoxColumn5
        ' 
        DataGridViewTextBoxColumn5.HeaderText = "index"
        DataGridViewTextBoxColumn5.MinimumWidth = 8
        DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        DataGridViewTextBoxColumn5.Visible = False
        DataGridViewTextBoxColumn5.Width = 150
        ' 
        ' GroupBoxRT
        ' 
        GroupBoxRT.Controls.Add(LabelRT)
        GroupBoxRT.Controls.Add(DGV2)
        GroupBoxRT.Location = New Point(12, 396)
        GroupBoxRT.Name = "GroupBoxRT"
        GroupBoxRT.Size = New Size(500, 163)
        GroupBoxRT.TabIndex = 41
        GroupBoxRT.TabStop = False
        GroupBoxRT.Text = "Foglio Routine"
        ' 
        ' LabelRT
        ' 
        LabelRT.AutoSize = True
        LabelRT.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        LabelRT.Location = New Point(139, 0)
        LabelRT.Name = "LabelRT"
        LabelRT.Size = New Size(14, 15)
        LabelRT.TabIndex = 41
        LabelRT.Text = "0"
        ' 
        ' GroupBoxCP
        ' 
        GroupBoxCP.Controls.Add(LabelCP)
        GroupBoxCP.Controls.Add(DGV3)
        GroupBoxCP.Location = New Point(10, 271)
        GroupBoxCP.Name = "GroupBoxCP"
        GroupBoxCP.Size = New Size(500, 119)
        GroupBoxCP.TabIndex = 42
        GroupBoxCP.TabStop = False
        GroupBoxCP.Text = "Voci comportamentali"
        ' 
        ' LabelCP
        ' 
        LabelCP.AutoSize = True
        LabelCP.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        LabelCP.Location = New Point(140, 0)
        LabelCP.Name = "LabelCP"
        LabelCP.Size = New Size(14, 15)
        LabelCP.TabIndex = 41
        LabelCP.Text = "0"
        ' 
        ' DGV3
        ' 
        DGV3.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DGV3.Columns.AddRange(New DataGridViewColumn() {DataGridViewTextBoxColumn1, DataGridViewTextBoxColumn4, DataGridViewTextBoxColumn6})
        DGV3.Location = New Point(6, 19)
        DGV3.MultiSelect = False
        DGV3.Name = "DGV3"
        DGV3.RowHeadersVisible = False
        DGV3.RowHeadersWidth = 62
        DGV3.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DGV3.Size = New Size(488, 91)
        DGV3.TabIndex = 39
        ' 
        ' DataGridViewTextBoxColumn1
        ' 
        DataGridViewCellStyle3.Font = New Font("Calibri", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewTextBoxColumn1.DefaultCellStyle = DataGridViewCellStyle3
        DataGridViewTextBoxColumn1.HeaderText = "Testo inserito"
        DataGridViewTextBoxColumn1.MinimumWidth = 8
        DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        DataGridViewTextBoxColumn1.Width = 430
        ' 
        ' DataGridViewTextBoxColumn4
        ' 
        DataGridViewTextBoxColumn4.HeaderText = "tot"
        DataGridViewTextBoxColumn4.MinimumWidth = 8
        DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        DataGridViewTextBoxColumn4.Width = 40
        ' 
        ' DataGridViewTextBoxColumn6
        ' 
        DataGridViewTextBoxColumn6.HeaderText = "index"
        DataGridViewTextBoxColumn6.MinimumWidth = 8
        DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        DataGridViewTextBoxColumn6.Visible = False
        DataGridViewTextBoxColumn6.Width = 150
        ' 
        ' GroupBox_RT
        ' 
        GroupBox_RT.BackColor = Color.LightYellow
        GroupBox_RT.Controls.Add(Button_RT)
        GroupBox_RT.Controls.Add(PictureBox0)
        GroupBox_RT.Controls.Add(PictureBox1)
        GroupBox_RT.Controls.Add(CheckBoxRT0)
        GroupBox_RT.Controls.Add(CheckBoxRT1)
        GroupBox_RT.Controls.Add(L_RT)
        GroupBox_RT.Location = New Point(524, 396)
        GroupBox_RT.Name = "GroupBox_RT"
        GroupBox_RT.Size = New Size(251, 91)
        GroupBox_RT.TabIndex = 43
        GroupBox_RT.TabStop = False
        GroupBox_RT.Text = "Voci Routine"
        ' 
        ' Button_RT
        ' 
        Button_RT.Font = New Font("Segoe UI", 9F)
        Button_RT.ForeColor = SystemColors.ControlText
        Button_RT.Location = New Point(173, 59)
        Button_RT.Name = "Button_RT"
        Button_RT.Size = New Size(58, 22)
        Button_RT.TabIndex = 48
        Button_RT.Text = "Accetta"
        Button_RT.UseVisualStyleBackColor = True
        ' 
        ' PictureBox0
        ' 
        PictureBox0.Image = CType(resources.GetObject("PictureBox0.Image"), Image)
        PictureBox0.InitialImage = CType(resources.GetObject("PictureBox0.InitialImage"), Image)
        PictureBox0.Location = New Point(82, 59)
        PictureBox0.Name = "PictureBox0"
        PictureBox0.Size = New Size(26, 24)
        PictureBox0.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBox0.TabIndex = 47
        PictureBox0.TabStop = False
        ' 
        ' PictureBox1
        ' 
        PictureBox1.Image = My.Resources.Resources.icons8_segno_di_spunta_96
        PictureBox1.InitialImage = CType(resources.GetObject("PictureBox1.InitialImage"), Image)
        PictureBox1.Location = New Point(20, 59)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(26, 24)
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBox1.TabIndex = 46
        PictureBox1.TabStop = False
        ' 
        ' CheckBoxRT0
        ' 
        CheckBoxRT0.ImageKey = "icons8-eliminare-96.png"
        CheckBoxRT0.Location = New Point(117, 61)
        CheckBoxRT0.Name = "CheckBoxRT0"
        CheckBoxRT0.Size = New Size(15, 20)
        CheckBoxRT0.TabIndex = 45
        CheckBoxRT0.TextAlign = ContentAlignment.MiddleCenter
        CheckBoxRT0.UseVisualStyleBackColor = True
        ' 
        ' CheckBoxRT1
        ' 
        CheckBoxRT1.ImageIndex = 8
        CheckBoxRT1.Location = New Point(54, 61)
        CheckBoxRT1.Name = "CheckBoxRT1"
        CheckBoxRT1.Size = New Size(15, 21)
        CheckBoxRT1.TabIndex = 44
        CheckBoxRT1.TextAlign = ContentAlignment.MiddleCenter
        CheckBoxRT1.UseVisualStyleBackColor = True
        ' 
        ' L_RT
        ' 
        L_RT.BorderStyle = BorderStyle.Fixed3D
        L_RT.Font = New Font("Segoe UI", 8F)
        L_RT.ForeColor = Color.Navy
        L_RT.Location = New Point(6, 19)
        L_RT.Name = "L_RT"
        L_RT.Size = New Size(240, 30)
        L_RT.TabIndex = 43
        L_RT.Text = "Descizione del codice"
        L_RT.TextAlign = ContentAlignment.TopCenter
        ' 
        ' Panel_risposte
        ' 
        Panel_risposte.BackColor = Color.PaleTurquoise
        Panel_risposte.Controls.Add(Label7)
        Panel_risposte.Controls.Add(Button1)
        Panel_risposte.Controls.Add(Button5)
        Panel_risposte.Location = New Point(75, 565)
        Panel_risposte.Name = "Panel_risposte"
        Panel_risposte.Size = New Size(253, 73)
        Panel_risposte.TabIndex = 44
        ' 
        ' Label7
        ' 
        Label7.BackColor = Color.Transparent
        Label7.BorderStyle = BorderStyle.Fixed3D
        Label7.Font = New Font("Segoe UI", 8F, FontStyle.Bold)
        Label7.ForeColor = Color.Black
        Label7.Location = New Point(10, 7)
        Label7.Name = "Label7"
        Label7.Size = New Size(235, 18)
        Label7.TabIndex = 40
        Label7.Text = "Modifiche Risposte Abilitate"
        Label7.TextAlign = ContentAlignment.TopCenter
        ' 
        ' Button1
        ' 
        Button1.Font = New Font("Segoe UI", 9F)
        Button1.ForeColor = SystemColors.ControlText
        Button1.Location = New Point(186, 39)
        Button1.Name = "Button1"
        Button1.Size = New Size(54, 25)
        Button1.TabIndex = 31
        Button1.Text = "Esci"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' ToolStripMenuItem5
        ' 
        ToolStripMenuItem5.Name = "ToolStripMenuItem5"
        ToolStripMenuItem5.Size = New Size(136, 22)
        ToolStripMenuItem5.Text = "Standard"
        ' 
        ' ToolStripMenuItem6
        ' 
        ToolStripMenuItem6.Name = "ToolStripMenuItem6"
        ToolStripMenuItem6.Size = New Size(136, 22)
        ToolStripMenuItem6.Text = "Admittance"
        ' 
        ' F_Foglio_esistente
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(785, 563)
        Controls.Add(Panel_risposte)
        Controls.Add(Label23)
        Controls.Add(GroupBox_RT)
        Controls.Add(GroupBoxCP)
        Controls.Add(Label5)
        Controls.Add(Label6)
        Controls.Add(GroupBoxRT)
        Controls.Add(GroupBox_CP)
        Controls.Add(Tab_option)
        Controls.Add(Button6)
        Controls.Add(GroupBox3)
        Controls.Add(Button4)
        Controls.Add(GroupBox_VOCI)
        Controls.Add(GroupBox4)
        Controls.Add(Label18)
        Name = "F_Foglio_esistente"
        Text = "Fogli_Esistenti"
        GroupBox4.ResumeLayout(False)
        GroupBox4.PerformLayout()
        CType(DGV1, ComponentModel.ISupportInitialize).EndInit()
        GroupBox_VOCI.ResumeLayout(False)
        GroupBox_VOCI.PerformLayout()
        CMenuStrip1.ResumeLayout(False)
        GroupBox3.ResumeLayout(False)
        GroupBox3.PerformLayout()
        Tab_option.ResumeLayout(False)
        TabPage1.ResumeLayout(False)
        TabPage1.PerformLayout()
        TabPage2.ResumeLayout(False)
        TabPage2.PerformLayout()
        GroupBox_CP.ResumeLayout(False)
        GroupBox_CP.PerformLayout()
        CType(DGV2, ComponentModel.ISupportInitialize).EndInit()
        GroupBoxRT.ResumeLayout(False)
        GroupBoxRT.PerformLayout()
        GroupBoxCP.ResumeLayout(False)
        GroupBoxCP.PerformLayout()
        CType(DGV3, ComponentModel.ISupportInitialize).EndInit()
        GroupBox_RT.ResumeLayout(False)
        CType(PictureBox0, ComponentModel.ISupportInitialize).EndInit()
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        Panel_risposte.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Dtp1 As DateTimePicker
    Friend WithEvents Label11 As Label
    Friend WithEvents Button6 As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents LabelV As Label
    Friend WithEvents DGV1 As DataGridView
    Friend WithEvents GroupBox_VOCI As GroupBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents L_V As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents TextBox5 As TextBox
    Friend WithEvents TextBox6 As TextBox
    Friend WithEvents TextBox7 As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Label21 As Label
    Friend WithEvents Label20 As Label
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents Label23 As Label
    Friend WithEvents CMenuStrip1 As ContextMenuStrip
    Friend WithEvents ToolStrip_modifica As ToolStripMenuItem
    Friend WithEvents ComboBox2 As ComboBox
    Friend WithEvents Label24 As Label
    Friend WithEvents Button4 As Button
    Friend WithEvents Label26 As Label
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents CheckBox3 As CheckBox
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents Label14 As Label
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents Label29 As Label
    Friend WithEvents Label28 As Label
    Friend WithEvents TextBox8 As TextBox
    Friend WithEvents LCP1 As Label
    Friend WithEvents TP1 As ToolTip
    Friend WithEvents CheckBox4 As CheckBox
    Friend WithEvents Label18 As Label
    Friend WithEvents Tab_option As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents GroupBox_CP As GroupBox
    Friend WithEvents DGV2 As DataGridView
    Friend WithEvents Label19 As Label
    Friend WithEvents GroupBoxRT As GroupBox
    Friend WithEvents LabelRT As Label
    Friend WithEvents GroupBoxCP As GroupBox
    Friend WithEvents LabelCP As Label
    Friend WithEvents DGV3 As DataGridView
    Friend WithEvents GroupBox_RT As GroupBox
    Friend WithEvents L_RT As Label
    Friend WithEvents CheckBoxRT0 As CheckBox
    Friend WithEvents CheckBoxRT1 As CheckBox
    Friend WithEvents L_CP As Label
    Friend WithEvents col1 As DataGridViewTextBoxColumn
    Friend WithEvents Col2 As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents Column5 As DataGridViewTextBoxColumn
    Friend WithEvents j As DataGridViewTextBoxColumn
    Friend WithEvents stato As DataGridViewImageColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As DataGridViewTextBoxColumn
    Friend WithEvents PictureBox0 As PictureBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As DataGridViewImageColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As DataGridViewTextBoxColumn
    Friend WithEvents Panel_risposte As Panel
    Friend WithEvents Button1 As Button
    Friend WithEvents Label7 As Label
    Friend WithEvents Button_voci As Button
    Friend WithEvents Button_CP As Button
    Friend WithEvents Button_RT As Button
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents ToolStrip_more As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem5 As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem6 As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ToolStripMenuItem2 As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents ToolStripMenuItem3 As ToolStripMenuItem
End Class
