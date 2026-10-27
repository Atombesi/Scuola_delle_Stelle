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
        TSM_Azzera_Totali = New ToolStripMenuItem()
        ToolStripSeparator1 = New ToolStripSeparator()
        TSM_Segna_da_compilare = New ToolStripMenuItem()
        ToolStripSeparator3 = New ToolStripSeparator()
        TSM_Elimina_foglio = New ToolStripMenuItem()
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
        tot = New DataGridViewTextBoxColumn()
        GroupBoxRT = New GroupBox()
        LabelRT = New Label()
        GroupBoxCP = New GroupBox()
        LabelCP = New Label()
        DGV3 = New DataGridView()
        DataGridViewTextBoxColumn1 = New DataGridViewTextBoxColumn()
        DataGridViewTextBoxColumn4 = New DataGridViewTextBoxColumn()
        DataGridViewTextBoxColumn6 = New DataGridViewTextBoxColumn()
        GroupBox_RT = New GroupBox()
        RadioButtonRT1 = New RadioButton()
        RadioButtonRT0 = New RadioButton()
        Button_RT = New Button()
        PictureBox0 = New PictureBox()
        PictureBox1 = New PictureBox()
        L_RT = New Label()
        Panel_risposte = New Panel()
        Label7 = New Label()
        Button1 = New Button()
        ToolStripMenuItem5 = New ToolStripMenuItem()
        ToolStripMenuItem6 = New ToolStripMenuItem()
        Panel1 = New Panel()
        L_Fogli_dacompilare = New Label()
        L_foglicompilati = New Label()
        L_Nfogli = New Label()
        Label13 = New Label()
        Label10 = New Label()
        Label8 = New Label()
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
        Panel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' GroupBox4
        ' 
        GroupBox4.Controls.Add(LabelV)
        GroupBox4.Controls.Add(DGV1)
        GroupBox4.Location = New Point(11, 185)
        GroupBox4.Margin = New Padding(4, 5, 4, 5)
        GroupBox4.Name = "GroupBox4"
        GroupBox4.Padding = New Padding(4, 5, 4, 5)
        GroupBox4.Size = New Size(717, 273)
        GroupBox4.TabIndex = 25
        GroupBox4.TabStop = False
        GroupBox4.Text = "Foglio Voci"
        ' 
        ' LabelV
        ' 
        LabelV.AutoSize = True
        LabelV.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        LabelV.Location = New Point(199, 0)
        LabelV.Margin = New Padding(4, 0, 4, 0)
        LabelV.Name = "LabelV"
        LabelV.Size = New Size(22, 25)
        LabelV.TabIndex = 7
        LabelV.Text = "0"
        ' 
        ' DGV1
        ' 
        DGV1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DGV1.Columns.AddRange(New DataGridViewColumn() {col1, Col2, Column4, Column5, j, stato})
        DGV1.Location = New Point(6, 37)
        DGV1.Margin = New Padding(4, 5, 4, 5)
        DGV1.MultiSelect = False
        DGV1.Name = "DGV1"
        DGV1.RowHeadersVisible = False
        DGV1.RowHeadersWidth = 62
        DGV1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DGV1.Size = New Size(709, 225)
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
        CheckBox4.Location = New Point(519, 62)
        CheckBox4.Margin = New Padding(4, 5, 4, 5)
        CheckBox4.Name = "CheckBox4"
        CheckBox4.Size = New Size(142, 29)
        CheckBox4.TabIndex = 38
        CheckBox4.Text = "Mostra Trend"
        CheckBox4.UseVisualStyleBackColor = True
        ' 
        ' Label23
        ' 
        Label23.Font = New Font("Segoe UI", 9F)
        Label23.Location = New Point(607, 7)
        Label23.Margin = New Padding(4, 0, 4, 0)
        Label23.Name = "Label23"
        Label23.Size = New Size(103, 25)
        Label23.TabIndex = 37
        Label23.Text = "Admitance"
        ' 
        ' Button6
        ' 
        Button6.Font = New Font("Segoe UI", 9F)
        Button6.ForeColor = SystemColors.ControlText
        Button6.Location = New Point(753, 878)
        Button6.Margin = New Padding(4, 5, 4, 5)
        Button6.Name = "Button6"
        Button6.Size = New Size(196, 50)
        Button6.TabIndex = 20
        Button6.Text = "Esercizio IMPORTA"
        Button6.UseVisualStyleBackColor = True
        ' 
        ' ComboBox1
        ' 
        ComboBox1.FormattingEnabled = True
        ComboBox1.Location = New Point(77, 38)
        ComboBox1.Margin = New Padding(4, 5, 4, 5)
        ComboBox1.Name = "ComboBox1"
        ComboBox1.Size = New Size(243, 33)
        ComboBox1.TabIndex = 28
        ' 
        ' Label11
        ' 
        Label11.AutoSize = True
        Label11.Location = New Point(11, 43)
        Label11.Margin = New Padding(4, 0, 4, 0)
        Label11.Name = "Label11"
        Label11.Size = New Size(63, 25)
        Label11.TabIndex = 22
        Label11.Text = "foglio:"
        ' 
        ' Dtp1
        ' 
        Dtp1.Enabled = False
        Dtp1.Format = DateTimePickerFormat.Short
        Dtp1.Location = New Point(50, 45)
        Dtp1.Margin = New Padding(4, 5, 4, 5)
        Dtp1.Name = "Dtp1"
        Dtp1.Size = New Size(113, 31)
        Dtp1.TabIndex = 26
        ' 
        ' Button5
        ' 
        Button5.Font = New Font("Segoe UI", 9F)
        Button5.ForeColor = SystemColors.ControlText
        Button5.Location = New Point(14, 65)
        Button5.Margin = New Padding(4, 5, 4, 5)
        Button5.Name = "Button5"
        Button5.Size = New Size(143, 42)
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
        GroupBox_VOCI.Location = New Point(749, 195)
        GroupBox_VOCI.Margin = New Padding(4, 5, 4, 5)
        GroupBox_VOCI.Name = "GroupBox_VOCI"
        GroupBox_VOCI.Padding = New Padding(4, 5, 4, 5)
        GroupBox_VOCI.Size = New Size(357, 277)
        GroupBox_VOCI.TabIndex = 26
        GroupBox_VOCI.TabStop = False
        GroupBox_VOCI.Text = "Voci elcar"
        ' 
        ' Button_voci
        ' 
        Button_voci.BackColor = SystemColors.Control
        Button_voci.Font = New Font("Segoe UI", 9F)
        Button_voci.ForeColor = SystemColors.ControlText
        Button_voci.Location = New Point(247, 230)
        Button_voci.Margin = New Padding(4, 5, 4, 5)
        Button_voci.Name = "Button_voci"
        Button_voci.Size = New Size(83, 37)
        Button_voci.TabIndex = 41
        Button_voci.Text = "Accetta"
        Button_voci.UseVisualStyleBackColor = False
        ' 
        ' Label21
        ' 
        Label21.AutoSize = True
        Label21.Font = New Font("Segoe UI", 9F)
        Label21.Location = New Point(259, 178)
        Label21.Margin = New Padding(4, 0, 4, 0)
        Label21.Name = "Label21"
        Label21.Size = New Size(61, 25)
        Label21.TabIndex = 35
        Label21.Text = "Titolo:"
        ' 
        ' Label20
        ' 
        Label20.Font = New Font("Segoe UI", 9F)
        Label20.Location = New Point(263, 130)
        Label20.Margin = New Padding(4, 0, 4, 0)
        Label20.Name = "Label20"
        Label20.Size = New Size(59, 25)
        Label20.TabIndex = 34
        Label20.Text = "Titolo:"
        ' 
        ' Label12
        ' 
        Label12.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label12.Location = New Point(10, 173)
        Label12.Margin = New Padding(4, 0, 4, 0)
        Label12.Name = "Label12"
        Label12.Size = New Size(21, 28)
        Label12.TabIndex = 27
        Label12.Text = "2"
        ' 
        ' TextBox5
        ' 
        TextBox5.BackColor = Color.WhiteSmoke
        TextBox5.Location = New Point(41, 172)
        TextBox5.Margin = New Padding(4, 5, 4, 5)
        TextBox5.Name = "TextBox5"
        TextBox5.ReadOnly = True
        TextBox5.Size = New Size(61, 31)
        TextBox5.TabIndex = 26
        ' 
        ' TextBox6
        ' 
        TextBox6.BackColor = Color.WhiteSmoke
        TextBox6.Location = New Point(113, 172)
        TextBox6.Margin = New Padding(4, 5, 4, 5)
        TextBox6.Name = "TextBox6"
        TextBox6.ReadOnly = True
        TextBox6.Size = New Size(61, 31)
        TextBox6.TabIndex = 25
        ' 
        ' TextBox7
        ' 
        TextBox7.BackColor = Color.WhiteSmoke
        TextBox7.Location = New Point(187, 172)
        TextBox7.Margin = New Padding(4, 5, 4, 5)
        TextBox7.Name = "TextBox7"
        TextBox7.ReadOnly = True
        TextBox7.Size = New Size(61, 31)
        TextBox7.TabIndex = 24
        ' 
        ' Label9
        ' 
        Label9.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label9.Location = New Point(11, 125)
        Label9.Margin = New Padding(4, 0, 4, 0)
        Label9.Name = "Label9"
        Label9.Size = New Size(21, 28)
        Label9.TabIndex = 22
        Label9.Text = "1"
        ' 
        ' L_V
        ' 
        L_V.BorderStyle = BorderStyle.Fixed3D
        L_V.Font = New Font("Segoe UI", 8F)
        L_V.ForeColor = Color.Navy
        L_V.Location = New Point(9, 30)
        L_V.Margin = New Padding(4, 0, 4, 0)
        L_V.Name = "L_V"
        L_V.Size = New Size(343, 50)
        L_V.TabIndex = 8
        L_V.Text = "Descizione del codice"
        L_V.TextAlign = ContentAlignment.TopCenter
        ' 
        ' Label4
        ' 
        Label4.Location = New Point(259, 90)
        Label4.Margin = New Padding(4, 0, 4, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(73, 28)
        Label4.TabIndex = 6
        Label4.Text = "Totale"
        ' 
        ' TextBox3
        ' 
        TextBox3.BackColor = Color.WhiteSmoke
        TextBox3.Location = New Point(190, 123)
        TextBox3.Margin = New Padding(4, 5, 4, 5)
        TextBox3.Name = "TextBox3"
        TextBox3.ReadOnly = True
        TextBox3.Size = New Size(61, 31)
        TextBox3.TabIndex = 5
        ' 
        ' Label3
        ' 
        Label3.Location = New Point(190, 90)
        Label3.Margin = New Padding(4, 0, 4, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(73, 28)
        Label3.TabIndex = 4
        Label3.Text = "Prompt"
        ' 
        ' TextBox2
        ' 
        TextBox2.BackColor = Color.WhiteSmoke
        TextBox2.Location = New Point(114, 123)
        TextBox2.Margin = New Padding(4, 5, 4, 5)
        TextBox2.Name = "TextBox2"
        TextBox2.ReadOnly = True
        TextBox2.Size = New Size(61, 31)
        TextBox2.TabIndex = 3
        ' 
        ' Label2
        ' 
        Label2.Location = New Point(113, 90)
        Label2.Margin = New Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(80, 28)
        Label2.TabIndex = 2
        Label2.Text = "Negativi"
        ' 
        ' TextBox1
        ' 
        TextBox1.BackColor = Color.WhiteSmoke
        TextBox1.Location = New Point(41, 123)
        TextBox1.Margin = New Padding(4, 5, 4, 5)
        TextBox1.Name = "TextBox1"
        TextBox1.ReadOnly = True
        TextBox1.Size = New Size(61, 31)
        TextBox1.TabIndex = 1
        ' 
        ' Label1
        ' 
        Label1.Location = New Point(39, 90)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(71, 28)
        Label1.TabIndex = 0
        Label1.Text = "Positivi"
        ' 
        ' TextBox8
        ' 
        TextBox8.BackColor = Color.WhiteSmoke
        TextBox8.Location = New Point(110, 93)
        TextBox8.Margin = New Padding(4, 5, 4, 5)
        TextBox8.Name = "TextBox8"
        TextBox8.ReadOnly = True
        TextBox8.Size = New Size(41, 31)
        TextBox8.TabIndex = 38
        TextBox8.TextAlign = HorizontalAlignment.Center
        ' 
        ' LCP1
        ' 
        LCP1.AutoSize = True
        LCP1.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        LCP1.Location = New Point(27, 102)
        LCP1.Margin = New Padding(4, 0, 4, 0)
        LCP1.Name = "LCP1"
        LCP1.Size = New Size(62, 25)
        LCP1.TabIndex = 37
        LCP1.Text = "totale"
        ' 
        ' Label5
        ' 
        Label5.BackColor = Color.FromArgb(CByte(192), CByte(255), CByte(192))
        Label5.BorderStyle = BorderStyle.Fixed3D
        Label5.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label5.Location = New Point(299, 5)
        Label5.Margin = New Padding(4, 0, 4, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(293, 28)
        Label5.TabIndex = 27
        Label5.Text = "Foglio compilato"
        Label5.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label6
        ' 
        Label6.BackColor = Color.White
        Label6.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label6.Location = New Point(16, 5)
        Label6.Margin = New Padding(4, 0, 4, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(269, 28)
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
        CMenuStrip1.Size = New Size(204, 74)
        ' 
        ' ToolStrip_modifica
        ' 
        ToolStrip_modifica.Image = My.Resources.Resources.Emoji_Objects_117
        ToolStrip_modifica.Name = "ToolStrip_modifica"
        ToolStrip_modifica.Size = New Size(203, 32)
        ToolStrip_modifica.Text = "Edit (ON/OFF)"
        ' 
        ' ToolStripSeparator2
        ' 
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New Size(200, 6)
        ' 
        ' ToolStrip_more
        ' 
        ToolStrip_more.DropDownItems.AddRange(New ToolStripItem() {TSM_Azzera_Totali, ToolStripSeparator1, TSM_Segna_da_compilare, ToolStripSeparator3, TSM_Elimina_foglio})
        ToolStrip_more.Name = "ToolStrip_more"
        ToolStrip_more.Size = New Size(203, 32)
        ToolStrip_more.Text = "More..."
        ' 
        ' TSM_Azzera_Totali
        ' 
        TSM_Azzera_Totali.ForeColor = Color.OrangeRed
        TSM_Azzera_Totali.Name = "TSM_Azzera_Totali"
        TSM_Azzera_Totali.Size = New Size(320, 34)
        TSM_Azzera_Totali.Text = "Azzera tutti i totali"
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(317, 6)
        ' 
        ' TSM_Segna_da_compilare
        ' 
        TSM_Segna_da_compilare.ForeColor = Color.OrangeRed
        TSM_Segna_da_compilare.Name = "TSM_Segna_da_compilare"
        TSM_Segna_da_compilare.Size = New Size(320, 34)
        TSM_Segna_da_compilare.Text = "Segna come da compilare"
        ' 
        ' ToolStripSeparator3
        ' 
        ToolStripSeparator3.Name = "ToolStripSeparator3"
        ToolStripSeparator3.Size = New Size(317, 6)
        ' 
        ' TSM_Elimina_foglio
        ' 
        TSM_Elimina_foglio.ForeColor = Color.Red
        TSM_Elimina_foglio.Image = My.Resources.Resources.warning_sign_by_kg_just_another_warning_sigh_with_exclamation_mark_UFwEFR_clipart
        TSM_Elimina_foglio.Name = "TSM_Elimina_foglio"
        TSM_Elimina_foglio.Size = New Size(320, 34)
        TSM_Elimina_foglio.Text = "Elimina foglio"
        ' 
        ' Label29
        ' 
        Label29.AutoSize = True
        Label29.Location = New Point(173, 53)
        Label29.Margin = New Padding(4, 0, 4, 0)
        Label29.Name = "Label29"
        Label29.Size = New Size(25, 25)
        Label29.TabIndex = 36
        Label29.Text = "a:"
        ' 
        ' Label28
        ' 
        Label28.AutoSize = True
        Label28.Location = New Point(16, 12)
        Label28.Margin = New Padding(4, 0, 4, 0)
        Label28.Name = "Label28"
        Label28.Size = New Size(77, 25)
        Label28.TabIndex = 35
        Label28.Text = "Periodo:"
        ' 
        ' CheckBox3
        ' 
        CheckBox3.AutoSize = True
        CheckBox3.Checked = True
        CheckBox3.CheckState = CheckState.Checked
        CheckBox3.Location = New Point(96, 12)
        CheckBox3.Margin = New Padding(4, 5, 4, 5)
        CheckBox3.Name = "CheckBox3"
        CheckBox3.Size = New Size(117, 29)
        CheckBox3.TabIndex = 34
        CheckBox3.Text = "Tutti i dati"
        CheckBox3.UseVisualStyleBackColor = True
        ' 
        ' DateTimePicker1
        ' 
        DateTimePicker1.Enabled = False
        DateTimePicker1.Format = DateTimePickerFormat.Short
        DateTimePicker1.Location = New Point(197, 45)
        DateTimePicker1.Margin = New Padding(4, 5, 4, 5)
        DateTimePicker1.Name = "DateTimePicker1"
        DateTimePicker1.Size = New Size(113, 31)
        DateTimePicker1.TabIndex = 33
        ' 
        ' Label14
        ' 
        Label14.AutoSize = True
        Label14.Location = New Point(13, 52)
        Label14.Margin = New Padding(4, 0, 4, 0)
        Label14.Name = "Label14"
        Label14.Size = New Size(38, 25)
        Label14.TabIndex = 32
        Label14.Text = "Da:"
        ' 
        ' ComboBox2
        ' 
        ComboBox2.FormattingEnabled = True
        ComboBox2.Items.AddRange(New Object() {"Tutti", "Da compilare", "Compilati"})
        ComboBox2.Location = New Point(84, 15)
        ComboBox2.Margin = New Padding(4, 5, 4, 5)
        ComboBox2.Name = "ComboBox2"
        ComboBox2.Size = New Size(250, 33)
        ComboBox2.TabIndex = 29
        ComboBox2.Text = "Tutti"
        ' 
        ' Label24
        ' 
        Label24.AutoSize = True
        Label24.Location = New Point(4, 20)
        Label24.Margin = New Padding(4, 0, 4, 0)
        Label24.Name = "Label24"
        Label24.Size = New Size(76, 25)
        Label24.TabIndex = 25
        Label24.Text = "Bambini"
        ' 
        ' Button4
        ' 
        Button4.BackColor = Color.DimGray
        Button4.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Button4.ForeColor = Color.Snow
        Button4.Location = New Point(1001, 873)
        Button4.Margin = New Padding(4, 5, 4, 5)
        Button4.Name = "Button4"
        Button4.Size = New Size(94, 58)
        Button4.TabIndex = 34
        Button4.Text = "Close"
        Button4.UseVisualStyleBackColor = False
        ' 
        ' Label26
        ' 
        Label26.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label26.ImageIndex = 5
        Label26.ImageList = ImageList1
        Label26.Location = New Point(669, 50)
        Label26.Margin = New Padding(4, 0, 4, 0)
        Label26.Name = "Label26"
        Label26.Size = New Size(47, 38)
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
        GroupBox3.Location = New Point(11, 85)
        GroupBox3.Margin = New Padding(4, 5, 4, 5)
        GroupBox3.Name = "GroupBox3"
        GroupBox3.Padding = New Padding(4, 5, 4, 5)
        GroupBox3.Size = New Size(717, 103)
        GroupBox3.TabIndex = 37
        GroupBox3.TabStop = False
        GroupBox3.Text = "elenco fogli"
        ' 
        ' Label19
        ' 
        Label19.BorderStyle = BorderStyle.Fixed3D
        Label19.Location = New Point(329, 42)
        Label19.Margin = New Padding(4, 0, 4, 0)
        Label19.Name = "Label19"
        Label19.Size = New Size(169, 35)
        Label19.TabIndex = 29
        Label19.Text = "n. 23 del 12-12-2026"
        Label19.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' CheckBox1
        ' 
        CheckBox1.AutoSize = True
        CheckBox1.Location = New Point(519, 28)
        CheckBox1.Margin = New Padding(4, 5, 4, 5)
        CheckBox1.Name = "CheckBox1"
        CheckBox1.Size = New Size(159, 29)
        CheckBox1.TabIndex = 29
        CheckBox1.Text = "NON Compilati"
        CheckBox1.UseVisualStyleBackColor = True
        ' 
        ' Label18
        ' 
        Label18.AutoSize = True
        Label18.Font = New Font("Segoe UI", 27.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label18.ForeColor = Color.Red
        Label18.Location = New Point(57, 443)
        Label18.Margin = New Padding(4, 0, 4, 0)
        Label18.Name = "Label18"
        Label18.Size = New Size(582, 74)
        Label18.TabIndex = 38
        Label18.Text = "Nessun Foglio Trovato"
        Label18.Visible = False
        ' 
        ' Tab_option
        ' 
        Tab_option.Controls.Add(TabPage1)
        Tab_option.Controls.Add(TabPage2)
        Tab_option.Location = New Point(749, 3)
        Tab_option.Margin = New Padding(4, 5, 4, 5)
        Tab_option.Name = "Tab_option"
        Tab_option.SelectedIndex = 0
        Tab_option.Size = New Size(357, 142)
        Tab_option.TabIndex = 39
        ' 
        ' TabPage1
        ' 
        TabPage1.Controls.Add(ComboBox2)
        TabPage1.Controls.Add(Label24)
        TabPage1.Location = New Point(4, 34)
        TabPage1.Margin = New Padding(4, 5, 4, 5)
        TabPage1.Name = "TabPage1"
        TabPage1.Padding = New Padding(4, 5, 4, 5)
        TabPage1.Size = New Size(349, 104)
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
        TabPage2.Location = New Point(4, 34)
        TabPage2.Margin = New Padding(4, 5, 4, 5)
        TabPage2.Name = "TabPage2"
        TabPage2.Padding = New Padding(4, 5, 4, 5)
        TabPage2.Size = New Size(349, 104)
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
        GroupBox_CP.Location = New Point(749, 490)
        GroupBox_CP.Margin = New Padding(4, 5, 4, 5)
        GroupBox_CP.Name = "GroupBox_CP"
        GroupBox_CP.Padding = New Padding(4, 5, 4, 5)
        GroupBox_CP.Size = New Size(357, 142)
        GroupBox_CP.TabIndex = 40
        GroupBox_CP.TabStop = False
        GroupBox_CP.Text = "Voci Comportamentali (CP)"
        ' 
        ' Button_CP
        ' 
        Button_CP.Font = New Font("Segoe UI", 9F)
        Button_CP.ForeColor = SystemColors.ControlText
        Button_CP.Location = New Point(247, 93)
        Button_CP.Margin = New Padding(4, 5, 4, 5)
        Button_CP.Name = "Button_CP"
        Button_CP.Size = New Size(83, 37)
        Button_CP.TabIndex = 40
        Button_CP.Text = "Accetta"
        Button_CP.UseVisualStyleBackColor = True
        ' 
        ' L_CP
        ' 
        L_CP.BorderStyle = BorderStyle.Fixed3D
        L_CP.Font = New Font("Segoe UI", 8F)
        L_CP.ForeColor = Color.Navy
        L_CP.Location = New Point(9, 35)
        L_CP.Margin = New Padding(4, 0, 4, 0)
        L_CP.Name = "L_CP"
        L_CP.Size = New Size(343, 50)
        L_CP.TabIndex = 39
        L_CP.Text = "Descizione del codice"
        L_CP.TextAlign = ContentAlignment.TopCenter
        ' 
        ' DGV2
        ' 
        DGV2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DGV2.Columns.AddRange(New DataGridViewColumn() {DataGridViewTextBoxColumn2, DataGridViewTextBoxColumn3, DataGridViewTextBoxColumn5, tot})
        DGV2.Location = New Point(9, 35)
        DGV2.Margin = New Padding(4, 5, 4, 5)
        DGV2.MultiSelect = False
        DGV2.Name = "DGV2"
        DGV2.RowHeadersVisible = False
        DGV2.RowHeadersWidth = 62
        DGV2.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DGV2.Size = New Size(697, 192)
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
        ' tot
        ' 
        tot.HeaderText = "Column1"
        tot.MinimumWidth = 8
        tot.Name = "tot"
        tot.Visible = False
        tot.Width = 150
        ' 
        ' GroupBoxRT
        ' 
        GroupBoxRT.Controls.Add(LabelRT)
        GroupBoxRT.Controls.Add(DGV2)
        GroupBoxRT.Location = New Point(17, 677)
        GroupBoxRT.Margin = New Padding(4, 5, 4, 5)
        GroupBoxRT.Name = "GroupBoxRT"
        GroupBoxRT.Padding = New Padding(4, 5, 4, 5)
        GroupBoxRT.Size = New Size(714, 237)
        GroupBoxRT.TabIndex = 41
        GroupBoxRT.TabStop = False
        GroupBoxRT.Text = "Foglio Routine"
        ' 
        ' LabelRT
        ' 
        LabelRT.AutoSize = True
        LabelRT.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        LabelRT.Location = New Point(199, 0)
        LabelRT.Margin = New Padding(4, 0, 4, 0)
        LabelRT.Name = "LabelRT"
        LabelRT.Size = New Size(22, 25)
        LabelRT.TabIndex = 41
        LabelRT.Text = "0"
        ' 
        ' GroupBoxCP
        ' 
        GroupBoxCP.Controls.Add(LabelCP)
        GroupBoxCP.Controls.Add(DGV3)
        GroupBoxCP.Location = New Point(14, 468)
        GroupBoxCP.Margin = New Padding(4, 5, 4, 5)
        GroupBoxCP.Name = "GroupBoxCP"
        GroupBoxCP.Padding = New Padding(4, 5, 4, 5)
        GroupBoxCP.Size = New Size(714, 198)
        GroupBoxCP.TabIndex = 42
        GroupBoxCP.TabStop = False
        GroupBoxCP.Text = "Voci comportamentali"
        ' 
        ' LabelCP
        ' 
        LabelCP.AutoSize = True
        LabelCP.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        LabelCP.Location = New Point(200, 0)
        LabelCP.Margin = New Padding(4, 0, 4, 0)
        LabelCP.Name = "LabelCP"
        LabelCP.Size = New Size(22, 25)
        LabelCP.TabIndex = 41
        LabelCP.Text = "0"
        ' 
        ' DGV3
        ' 
        DGV3.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DGV3.Columns.AddRange(New DataGridViewColumn() {DataGridViewTextBoxColumn1, DataGridViewTextBoxColumn4, DataGridViewTextBoxColumn6})
        DGV3.Location = New Point(9, 32)
        DGV3.Margin = New Padding(4, 5, 4, 5)
        DGV3.MultiSelect = False
        DGV3.Name = "DGV3"
        DGV3.RowHeadersVisible = False
        DGV3.RowHeadersWidth = 62
        DGV3.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DGV3.Size = New Size(697, 152)
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
        GroupBox_RT.BackColor = SystemColors.Control
        GroupBox_RT.Controls.Add(RadioButtonRT1)
        GroupBox_RT.Controls.Add(RadioButtonRT0)
        GroupBox_RT.Controls.Add(Button_RT)
        GroupBox_RT.Controls.Add(PictureBox0)
        GroupBox_RT.Controls.Add(PictureBox1)
        GroupBox_RT.Controls.Add(L_RT)
        GroupBox_RT.Location = New Point(749, 698)
        GroupBox_RT.Margin = New Padding(4, 5, 4, 5)
        GroupBox_RT.Name = "GroupBox_RT"
        GroupBox_RT.Padding = New Padding(4, 5, 4, 5)
        GroupBox_RT.Size = New Size(357, 152)
        GroupBox_RT.TabIndex = 43
        GroupBox_RT.TabStop = False
        GroupBox_RT.Text = "Voci Routine"
        ' 
        ' RadioButtonRT1
        ' 
        RadioButtonRT1.AutoSize = True
        RadioButtonRT1.Location = New Point(27, 108)
        RadioButtonRT1.Margin = New Padding(4, 5, 4, 5)
        RadioButtonRT1.Name = "RadioButtonRT1"
        RadioButtonRT1.Size = New Size(21, 20)
        RadioButtonRT1.TabIndex = 45
        RadioButtonRT1.TabStop = True
        RadioButtonRT1.UseVisualStyleBackColor = True
        ' 
        ' RadioButtonRT0
        ' 
        RadioButtonRT0.AutoSize = True
        RadioButtonRT0.Location = New Point(149, 108)
        RadioButtonRT0.Margin = New Padding(4, 5, 4, 5)
        RadioButtonRT0.Name = "RadioButtonRT0"
        RadioButtonRT0.Size = New Size(21, 20)
        RadioButtonRT0.TabIndex = 46
        RadioButtonRT0.TabStop = True
        RadioButtonRT0.UseVisualStyleBackColor = True
        ' 
        ' Button_RT
        ' 
        Button_RT.Font = New Font("Segoe UI", 9F)
        Button_RT.ForeColor = SystemColors.ControlText
        Button_RT.Location = New Point(247, 98)
        Button_RT.Margin = New Padding(4, 5, 4, 5)
        Button_RT.Name = "Button_RT"
        Button_RT.Size = New Size(83, 37)
        Button_RT.TabIndex = 48
        Button_RT.Text = "Accetta"
        Button_RT.UseVisualStyleBackColor = True
        ' 
        ' PictureBox0
        ' 
        PictureBox0.BackColor = SystemColors.Control
        PictureBox0.Image = CType(resources.GetObject("PictureBox0.Image"), Image)
        PictureBox0.InitialImage = CType(resources.GetObject("PictureBox0.InitialImage"), Image)
        PictureBox0.Location = New Point(177, 98)
        PictureBox0.Margin = New Padding(4, 5, 4, 5)
        PictureBox0.Name = "PictureBox0"
        PictureBox0.Size = New Size(37, 40)
        PictureBox0.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBox0.TabIndex = 47
        PictureBox0.TabStop = False
        ' 
        ' PictureBox1
        ' 
        PictureBox1.BackColor = SystemColors.Control
        PictureBox1.Image = My.Resources.Resources.icons8_segno_di_spunta_96
        PictureBox1.InitialImage = CType(resources.GetObject("PictureBox1.InitialImage"), Image)
        PictureBox1.Location = New Point(56, 98)
        PictureBox1.Margin = New Padding(4, 5, 4, 5)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(37, 40)
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBox1.TabIndex = 46
        PictureBox1.TabStop = False
        ' 
        ' L_RT
        ' 
        L_RT.BorderStyle = BorderStyle.Fixed3D
        L_RT.Font = New Font("Segoe UI", 8F)
        L_RT.ForeColor = Color.Navy
        L_RT.Location = New Point(9, 32)
        L_RT.Margin = New Padding(4, 0, 4, 0)
        L_RT.Name = "L_RT"
        L_RT.Size = New Size(343, 50)
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
        Panel_risposte.Location = New Point(107, 942)
        Panel_risposte.Margin = New Padding(4, 5, 4, 5)
        Panel_risposte.Name = "Panel_risposte"
        Panel_risposte.Size = New Size(357, 122)
        Panel_risposte.TabIndex = 44
        ' 
        ' Label7
        ' 
        Label7.BackColor = Color.Transparent
        Label7.BorderStyle = BorderStyle.Fixed3D
        Label7.Font = New Font("Segoe UI", 8F, FontStyle.Bold)
        Label7.ForeColor = Color.Black
        Label7.Location = New Point(11, 12)
        Label7.Margin = New Padding(4, 0, 4, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(336, 30)
        Label7.TabIndex = 40
        Label7.Text = "Modifiche Risposte Abilitate"
        Label7.TextAlign = ContentAlignment.TopCenter
        ' 
        ' Button1
        ' 
        Button1.Font = New Font("Segoe UI", 9F)
        Button1.ForeColor = SystemColors.ControlText
        Button1.Location = New Point(266, 65)
        Button1.Margin = New Padding(4, 5, 4, 5)
        Button1.Name = "Button1"
        Button1.Size = New Size(77, 42)
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
        ' Panel1
        ' 
        Panel1.BorderStyle = BorderStyle.Fixed3D
        Panel1.Controls.Add(L_Fogli_dacompilare)
        Panel1.Controls.Add(L_foglicompilati)
        Panel1.Controls.Add(L_Nfogli)
        Panel1.Controls.Add(Label13)
        Panel1.Controls.Add(Label10)
        Panel1.Controls.Add(Label8)
        Panel1.Location = New Point(19, 43)
        Panel1.Margin = New Padding(4, 5, 4, 5)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(711, 34)
        Panel1.TabIndex = 45
        ' 
        ' L_Fogli_dacompilare
        ' 
        L_Fogli_dacompilare.AutoSize = True
        L_Fogli_dacompilare.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        L_Fogli_dacompilare.Location = New Point(461, 3)
        L_Fogli_dacompilare.Margin = New Padding(4, 0, 4, 0)
        L_Fogli_dacompilare.Name = "L_Fogli_dacompilare"
        L_Fogli_dacompilare.Size = New Size(22, 25)
        L_Fogli_dacompilare.TabIndex = 43
        L_Fogli_dacompilare.Text = "0"
        ' 
        ' L_foglicompilati
        ' 
        L_foglicompilati.AutoSize = True
        L_foglicompilati.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        L_foglicompilati.Location = New Point(279, 3)
        L_foglicompilati.Margin = New Padding(4, 0, 4, 0)
        L_foglicompilati.Name = "L_foglicompilati"
        L_foglicompilati.Size = New Size(22, 25)
        L_foglicompilati.TabIndex = 42
        L_foglicompilati.Text = "0"
        ' 
        ' L_Nfogli
        ' 
        L_Nfogli.AutoSize = True
        L_Nfogli.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        L_Nfogli.Location = New Point(129, 3)
        L_Nfogli.Margin = New Padding(4, 0, 4, 0)
        L_Nfogli.Name = "L_Nfogli"
        L_Nfogli.Size = New Size(22, 25)
        L_Nfogli.TabIndex = 41
        L_Nfogli.Text = "0"
        ' 
        ' Label13
        ' 
        Label13.Location = New Point(333, 3)
        Label13.Margin = New Padding(4, 0, 4, 0)
        Label13.Name = "Label13"
        Label13.Size = New Size(120, 25)
        Label13.TabIndex = 40
        Label13.Text = "Da compilare:"
        ' 
        ' Label10
        ' 
        Label10.Location = New Point(177, 3)
        Label10.Margin = New Padding(4, 0, 4, 0)
        Label10.Name = "Label10"
        Label10.Size = New Size(94, 25)
        Label10.TabIndex = 39
        Label10.Text = "Compilati:"
        ' 
        ' Label8
        ' 
        Label8.Location = New Point(16, 3)
        Label8.Margin = New Padding(4, 0, 4, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(121, 25)
        Label8.TabIndex = 38
        Label8.Text = "N totali fogli:"
        ' 
        ' F_Foglio_esistente
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1121, 935)
        Controls.Add(Panel1)
        Controls.Add(Panel_risposte)
        Controls.Add(GroupBox_RT)
        Controls.Add(GroupBoxCP)
        Controls.Add(GroupBoxRT)
        Controls.Add(Label6)
        Controls.Add(GroupBox_CP)
        Controls.Add(Label5)
        Controls.Add(Label23)
        Controls.Add(Tab_option)
        Controls.Add(Button6)
        Controls.Add(GroupBox3)
        Controls.Add(Button4)
        Controls.Add(GroupBox_VOCI)
        Controls.Add(GroupBox4)
        Controls.Add(Label18)
        Margin = New Padding(4, 5, 4, 5)
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
        GroupBox_RT.PerformLayout()
        CType(PictureBox0, ComponentModel.ISupportInitialize).EndInit()
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        Panel_risposte.ResumeLayout(False)
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
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
    Friend WithEvents TSM_Azzera_Totali As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents TSM_Segna_da_compilare As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents TSM_Elimina_foglio As ToolStripMenuItem
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As DataGridViewImageColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As DataGridViewTextBoxColumn
    Friend WithEvents tot As DataGridViewTextBoxColumn
    Friend WithEvents RadioButtonRT1 As RadioButton
    Friend WithEvents RadioButtonRT0 As RadioButton
    Friend WithEvents Panel1 As Panel
    Friend WithEvents L_Fogli_dacompilare As Label
    Friend WithEvents L_foglicompilati As Label
    Friend WithEvents L_Nfogli As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label8 As Label
End Class
