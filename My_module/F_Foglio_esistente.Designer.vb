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
        GroupBox1 = New GroupBox()
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
        CheckBox1 = New CheckBox()
        ImageList1 = New ImageList(components)
        CMenuStrip1 = New ContextMenuStrip(components)
        MODIFICA = New ToolStripMenuItem()
        ToolStripMenuItem4 = New ToolStripMenuItem()
        ToolStripMenuItem5 = New ToolStripMenuItem()
        ToolStripMenuItem6 = New ToolStripMenuItem()
        ToolStripSeparator2 = New ToolStripSeparator()
        ToolStripMenuItem2 = New ToolStripMenuItem()
        ToolStripMenuItem3 = New ToolStripMenuItem()
        SegnaComeDaCompilareToolStripMenuItem = New ToolStripMenuItem()
        ToolStripSeparator1 = New ToolStripSeparator()
        ToolStripMenuItem1 = New ToolStripMenuItem()
        ToolStripSeparator3 = New ToolStripSeparator()
        ToolStripMenuItem7 = New ToolStripMenuItem()
        Label29 = New Label()
        Label28 = New Label()
        CheckBox3 = New CheckBox()
        DateTimePicker1 = New DateTimePicker()
        Label14 = New Label()
        ComboBox2 = New ComboBox()
        Label24 = New Label()
        Button4 = New Button()
        Label26 = New Label()
        Label27 = New Label()
        GroupBox3 = New GroupBox()
        Label19 = New Label()
        CheckBox2 = New CheckBox()
        TP1 = New ToolTip(components)
        Label18 = New Label()
        TabControl1 = New TabControl()
        TabPage1 = New TabPage()
        TabPage2 = New TabPage()
        GroupBox2 = New GroupBox()
        L_CP = New Label()
        DGV2 = New DataGridView()
        DataGridViewTextBoxColumn2 = New DataGridViewTextBoxColumn()
        DataGridViewTextBoxColumn3 = New DataGridViewTextBoxColumn()
        DataGridViewTextBoxColumn5 = New DataGridViewTextBoxColumn()
        GroupBoxRT = New GroupBox()
        LabelRT = New Label()
        GroupBoxCP = New GroupBox()
        LabelCP = New Label()
        DGV3 = New DataGridView()
        DataGridViewTextBoxColumn1 = New DataGridViewTextBoxColumn()
        DataGridViewTextBoxColumn4 = New DataGridViewTextBoxColumn()
        DataGridViewTextBoxColumn6 = New DataGridViewTextBoxColumn()
        GroupBox7 = New GroupBox()
        CheckBoxRT0 = New CheckBox()
        CheckBoxRT1 = New CheckBox()
        L_RT = New Label()
        GroupBox4.SuspendLayout()
        CType(DGV1, ComponentModel.ISupportInitialize).BeginInit()
        GroupBox1.SuspendLayout()
        CMenuStrip1.SuspendLayout()
        GroupBox3.SuspendLayout()
        TabControl1.SuspendLayout()
        TabPage1.SuspendLayout()
        TabPage2.SuspendLayout()
        GroupBox2.SuspendLayout()
        CType(DGV2, ComponentModel.ISupportInitialize).BeginInit()
        GroupBoxRT.SuspendLayout()
        GroupBoxCP.SuspendLayout()
        CType(DGV3, ComponentModel.ISupportInitialize).BeginInit()
        GroupBox7.SuspendLayout()
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
        CheckBox4.Location = New Point(543, 515)
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
        Button6.Location = New Point(15, 572)
        Button6.Name = "Button6"
        Button6.Size = New Size(138, 27)
        Button6.TabIndex = 20
        Button6.Text = "Importa Esercizio"
        Button6.UseVisualStyleBackColor = True
        ' 
        ' ComboBox1
        ' 
        ComboBox1.FormattingEnabled = True
        ComboBox1.Location = New Point(54, 17)
        ComboBox1.Name = "ComboBox1"
        ComboBox1.Size = New Size(177, 23)
        ComboBox1.TabIndex = 28
        ' 
        ' Label11
        ' 
        Label11.AutoSize = True
        Label11.Location = New Point(8, 20)
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
        Button5.Location = New Point(715, 541)
        Button5.Name = "Button5"
        Button5.Size = New Size(57, 25)
        Button5.TabIndex = 19
        Button5.Text = "Salva"
        Button5.UseVisualStyleBackColor = True
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(Label21)
        GroupBox1.Controls.Add(Label20)
        GroupBox1.Controls.Add(Label12)
        GroupBox1.Controls.Add(TextBox5)
        GroupBox1.Controls.Add(TextBox6)
        GroupBox1.Controls.Add(TextBox7)
        GroupBox1.Controls.Add(Label9)
        GroupBox1.Controls.Add(L_V)
        GroupBox1.Controls.Add(Label4)
        GroupBox1.Controls.Add(TextBox3)
        GroupBox1.Controls.Add(Label3)
        GroupBox1.Controls.Add(TextBox2)
        GroupBox1.Controls.Add(Label2)
        GroupBox1.Controls.Add(TextBox1)
        GroupBox1.Controls.Add(Label1)
        GroupBox1.Location = New Point(524, 106)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(248, 142)
        GroupBox1.TabIndex = 26
        GroupBox1.TabStop = False
        GroupBox1.Text = "Voci elcar"
        ' 
        ' Label21
        ' 
        Label21.AutoSize = True
        Label21.Font = New Font("Segoe UI", 9F)
        Label21.Location = New Point(181, 101)
        Label21.Name = "Label21"
        Label21.Size = New Size(41, 15)
        Label21.TabIndex = 35
        Label21.Text = "Titolo:"
        ' 
        ' Label20
        ' 
        Label20.Font = New Font("Segoe UI", 9F)
        Label20.Location = New Point(181, 69)
        Label20.Name = "Label20"
        Label20.Size = New Size(41, 15)
        Label20.TabIndex = 34
        Label20.Text = "Titolo:"
        ' 
        ' Label12
        ' 
        Label12.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label12.Location = New Point(4, 97)
        Label12.Name = "Label12"
        Label12.Size = New Size(15, 17)
        Label12.TabIndex = 27
        Label12.Text = "2"
        ' 
        ' TextBox5
        ' 
        TextBox5.Location = New Point(26, 96)
        TextBox5.Name = "TextBox5"
        TextBox5.ReadOnly = True
        TextBox5.Size = New Size(44, 23)
        TextBox5.TabIndex = 26
        ' 
        ' TextBox6
        ' 
        TextBox6.Location = New Point(76, 96)
        TextBox6.Name = "TextBox6"
        TextBox6.ReadOnly = True
        TextBox6.Size = New Size(44, 23)
        TextBox6.TabIndex = 25
        ' 
        ' TextBox7
        ' 
        TextBox7.Location = New Point(128, 96)
        TextBox7.Name = "TextBox7"
        TextBox7.ReadOnly = True
        TextBox7.Size = New Size(44, 23)
        TextBox7.TabIndex = 24
        ' 
        ' Label9
        ' 
        Label9.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label9.Location = New Point(5, 66)
        Label9.Name = "Label9"
        Label9.Size = New Size(15, 17)
        Label9.TabIndex = 22
        Label9.Text = "1"
        ' 
        ' L_V
        ' 
        L_V.Font = New Font("Segoe UI", 8F)
        L_V.Location = New Point(8, 18)
        L_V.Name = "L_V"
        L_V.Size = New Size(213, 28)
        L_V.TabIndex = 8
        L_V.Text = "Descizione del codice"
        ' 
        ' Label4
        ' 
        Label4.Location = New Point(178, 45)
        Label4.Name = "Label4"
        Label4.Size = New Size(51, 17)
        Label4.TabIndex = 6
        Label4.Text = "Totale"
        ' 
        ' TextBox3
        ' 
        TextBox3.Location = New Point(130, 65)
        TextBox3.Name = "TextBox3"
        TextBox3.ReadOnly = True
        TextBox3.Size = New Size(44, 23)
        TextBox3.TabIndex = 5
        ' 
        ' Label3
        ' 
        Label3.Location = New Point(130, 45)
        Label3.Name = "Label3"
        Label3.Size = New Size(51, 17)
        Label3.TabIndex = 4
        Label3.Text = "Prompt"
        ' 
        ' TextBox2
        ' 
        TextBox2.Location = New Point(77, 65)
        TextBox2.Name = "TextBox2"
        TextBox2.ReadOnly = True
        TextBox2.Size = New Size(44, 23)
        TextBox2.TabIndex = 3
        ' 
        ' Label2
        ' 
        Label2.Location = New Point(76, 45)
        Label2.Name = "Label2"
        Label2.Size = New Size(56, 17)
        Label2.TabIndex = 2
        Label2.Text = "Negativi"
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(26, 65)
        TextBox1.Name = "TextBox1"
        TextBox1.ReadOnly = True
        TextBox1.Size = New Size(44, 23)
        TextBox1.TabIndex = 1
        ' 
        ' Label1
        ' 
        Label1.Location = New Point(24, 45)
        Label1.Name = "Label1"
        Label1.Size = New Size(50, 17)
        Label1.TabIndex = 0
        Label1.Text = "Positivi"
        ' 
        ' TextBox8
        ' 
        TextBox8.Location = New Point(77, 48)
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
        LCP1.Location = New Point(19, 53)
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
        ' CheckBox1
        ' 
        CheckBox1.Appearance = Appearance.Button
        CheckBox1.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CheckBox1.Location = New Point(167, 571)
        CheckBox1.Name = "CheckBox1"
        CheckBox1.Size = New Size(107, 30)
        CheckBox1.TabIndex = 30
        CheckBox1.Text = "Modifica"
        CheckBox1.TextAlign = ContentAlignment.MiddleCenter
        CheckBox1.UseVisualStyleBackColor = True
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
        CMenuStrip1.Items.AddRange(New ToolStripItem() {MODIFICA})
        CMenuStrip1.Name = "CMenuStrip1"
        CMenuStrip1.Size = New Size(131, 26)
        ' 
        ' MODIFICA
        ' 
        MODIFICA.DropDownItems.AddRange(New ToolStripItem() {ToolStripMenuItem4, ToolStripSeparator2, ToolStripMenuItem2, ToolStripMenuItem3, SegnaComeDaCompilareToolStripMenuItem, ToolStripSeparator1, ToolStripMenuItem1, ToolStripSeparator3, ToolStripMenuItem7})
        MODIFICA.Name = "MODIFICA"
        MODIFICA.Size = New Size(130, 22)
        MODIFICA.Text = "MODIFICA"
        ' 
        ' ToolStripMenuItem4
        ' 
        ToolStripMenuItem4.DropDownItems.AddRange(New ToolStripItem() {ToolStripMenuItem5, ToolStripMenuItem6})
        ToolStripMenuItem4.Name = "ToolStripMenuItem4"
        ToolStripMenuItem4.Size = New Size(222, 22)
        ToolStripMenuItem4.Text = "Tipologia di foglio"
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
        ' ToolStripSeparator2
        ' 
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New Size(219, 6)
        ' 
        ' ToolStripMenuItem2
        ' 
        ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        ToolStripMenuItem2.Size = New Size(222, 22)
        ToolStripMenuItem2.Text = "Abilita Modifiche"
        ' 
        ' ToolStripMenuItem3
        ' 
        ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        ToolStripMenuItem3.Size = New Size(222, 22)
        ToolStripMenuItem3.Text = "Modifica Titolo foglio"
        ' 
        ' SegnaComeDaCompilareToolStripMenuItem
        ' 
        SegnaComeDaCompilareToolStripMenuItem.Name = "SegnaComeDaCompilareToolStripMenuItem"
        SegnaComeDaCompilareToolStripMenuItem.Size = New Size(222, 22)
        SegnaComeDaCompilareToolStripMenuItem.Text = "Segna come ""Da compilare"""
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(219, 6)
        ' 
        ' ToolStripMenuItem1
        ' 
        ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        ToolStripMenuItem1.Size = New Size(222, 22)
        ToolStripMenuItem1.Text = "Azzera Esiti Risposte"
        ' 
        ' ToolStripSeparator3
        ' 
        ToolStripSeparator3.Name = "ToolStripSeparator3"
        ToolStripSeparator3.Size = New Size(219, 6)
        ' 
        ' ToolStripMenuItem7
        ' 
        ToolStripMenuItem7.Name = "ToolStripMenuItem7"
        ToolStripMenuItem7.Size = New Size(222, 22)
        ToolStripMenuItem7.Text = "Elimina il foglio"
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
        Label28.Location = New Point(3, 7)
        Label28.Name = "Label28"
        Label28.Size = New Size(51, 15)
        Label28.TabIndex = 35
        Label28.Text = "Periodo:"
        ' 
        ' CheckBox3
        ' 
        CheckBox3.AutoSize = True
        CheckBox3.Location = New Point(59, 8)
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
        Button4.Location = New Point(706, 587)
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
        Label26.Location = New Point(468, 17)
        Label26.Name = "Label26"
        Label26.Size = New Size(33, 23)
        Label26.TabIndex = 35
        ' 
        ' Label27
        ' 
        Label27.BackColor = Color.White
        Label27.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label27.Location = New Point(239, 604)
        Label27.Name = "Label27"
        Label27.Size = New Size(162, 17)
        Label27.TabIndex = 36
        Label27.Text = "Force Editing ON"
        Label27.TextAlign = ContentAlignment.MiddleCenter
        Label27.Visible = False
        ' 
        ' GroupBox3
        ' 
        GroupBox3.Controls.Add(Label19)
        GroupBox3.Controls.Add(ComboBox1)
        GroupBox3.Controls.Add(CheckBox2)
        GroupBox3.Controls.Add(Label11)
        GroupBox3.Controls.Add(Label26)
        GroupBox3.Location = New Point(8, 23)
        GroupBox3.Name = "GroupBox3"
        GroupBox3.Size = New Size(502, 61)
        GroupBox3.TabIndex = 37
        GroupBox3.TabStop = False
        GroupBox3.Text = "elenco fogli"
        ' 
        ' Label19
        ' 
        Label19.BorderStyle = BorderStyle.Fixed3D
        Label19.Location = New Point(240, 19)
        Label19.Name = "Label19"
        Label19.Size = New Size(118, 21)
        Label19.TabIndex = 29
        Label19.Text = "n. 23 del 12-12-2026"
        Label19.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' CheckBox2
        ' 
        CheckBox2.AutoSize = True
        CheckBox2.Location = New Point(364, 19)
        CheckBox2.Name = "CheckBox2"
        CheckBox2.Size = New Size(102, 19)
        CheckBox2.TabIndex = 29
        CheckBox2.Text = "Non compilati"
        CheckBox2.UseVisualStyleBackColor = True
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
        ' TabControl1
        ' 
        TabControl1.Controls.Add(TabPage1)
        TabControl1.Controls.Add(TabPage2)
        TabControl1.Location = New Point(516, 2)
        TabControl1.Name = "TabControl1"
        TabControl1.SelectedIndex = 0
        TabControl1.Size = New Size(261, 85)
        TabControl1.TabIndex = 39
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
        ' GroupBox2
        ' 
        GroupBox2.Controls.Add(L_CP)
        GroupBox2.Controls.Add(TextBox8)
        GroupBox2.Controls.Add(LCP1)
        GroupBox2.Location = New Point(524, 271)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Size = New Size(251, 85)
        GroupBox2.TabIndex = 40
        GroupBox2.TabStop = False
        GroupBox2.Text = "Voci Comportamentali (CP)"
        ' 
        ' L_CP
        ' 
        L_CP.Font = New Font("Segoe UI", 8F)
        L_CP.Location = New Point(13, 21)
        L_CP.Name = "L_CP"
        L_CP.Size = New Size(226, 28)
        L_CP.TabIndex = 39
        L_CP.Text = "Descizione del codice"
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
        ' GroupBox7
        ' 
        GroupBox7.Controls.Add(CheckBoxRT0)
        GroupBox7.Controls.Add(CheckBoxRT1)
        GroupBox7.Controls.Add(L_RT)
        GroupBox7.Location = New Point(524, 396)
        GroupBox7.Name = "GroupBox7"
        GroupBox7.Size = New Size(251, 91)
        GroupBox7.TabIndex = 43
        GroupBox7.TabStop = False
        GroupBox7.Text = "Voci Routine"
        ' 
        ' CheckBoxRT0
        ' 
        CheckBoxRT0.Appearance = Appearance.Button
        CheckBoxRT0.ImageKey = "icons8-eliminare-96.png"
        CheckBoxRT0.ImageList = ImageList1
        CheckBoxRT0.Location = New Point(131, 51)
        CheckBoxRT0.Name = "CheckBoxRT0"
        CheckBoxRT0.Size = New Size(66, 26)
        CheckBoxRT0.TabIndex = 45
        CheckBoxRT0.TextAlign = ContentAlignment.MiddleCenter
        CheckBoxRT0.UseVisualStyleBackColor = True
        ' 
        ' CheckBoxRT1
        ' 
        CheckBoxRT1.Appearance = Appearance.Button
        CheckBoxRT1.ImageIndex = 8
        CheckBoxRT1.ImageList = ImageList1
        CheckBoxRT1.Location = New Point(24, 51)
        CheckBoxRT1.Name = "CheckBoxRT1"
        CheckBoxRT1.Size = New Size(66, 27)
        CheckBoxRT1.TabIndex = 44
        CheckBoxRT1.TextAlign = ContentAlignment.MiddleCenter
        CheckBoxRT1.UseVisualStyleBackColor = True
        ' 
        ' L_RT
        ' 
        L_RT.Font = New Font("Segoe UI", 8F)
        L_RT.Location = New Point(18, 19)
        L_RT.Name = "L_RT"
        L_RT.Size = New Size(213, 28)
        L_RT.TabIndex = 43
        L_RT.Text = "Descizione del codice"
        ' 
        ' F_Foglio_esistente
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(785, 634)
        Controls.Add(Label23)
        Controls.Add(GroupBox7)
        Controls.Add(GroupBoxCP)
        Controls.Add(CheckBox4)
        Controls.Add(Label5)
        Controls.Add(Label6)
        Controls.Add(GroupBoxRT)
        Controls.Add(GroupBox2)
        Controls.Add(Button5)
        Controls.Add(TabControl1)
        Controls.Add(Button6)
        Controls.Add(GroupBox3)
        Controls.Add(Label27)
        Controls.Add(Button4)
        Controls.Add(CheckBox1)
        Controls.Add(GroupBox1)
        Controls.Add(GroupBox4)
        Controls.Add(Label18)
        Name = "F_Foglio_esistente"
        Text = "Fogli_Esistenti"
        GroupBox4.ResumeLayout(False)
        GroupBox4.PerformLayout()
        CType(DGV1, ComponentModel.ISupportInitialize).EndInit()
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        CMenuStrip1.ResumeLayout(False)
        GroupBox3.ResumeLayout(False)
        GroupBox3.PerformLayout()
        TabControl1.ResumeLayout(False)
        TabPage1.ResumeLayout(False)
        TabPage1.PerformLayout()
        TabPage2.ResumeLayout(False)
        TabPage2.PerformLayout()
        GroupBox2.ResumeLayout(False)
        GroupBox2.PerformLayout()
        CType(DGV2, ComponentModel.ISupportInitialize).EndInit()
        GroupBoxRT.ResumeLayout(False)
        GroupBoxRT.PerformLayout()
        GroupBoxCP.ResumeLayout(False)
        GroupBoxCP.PerformLayout()
        CType(DGV3, ComponentModel.ISupportInitialize).EndInit()
        GroupBox7.ResumeLayout(False)
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
    Friend WithEvents GroupBox1 As GroupBox
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
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents Label21 As Label
    Friend WithEvents Label20 As Label
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents Label23 As Label
    Friend WithEvents CMenuStrip1 As ContextMenuStrip
    Friend WithEvents MODIFICA As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem4 As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem5 As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem6 As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ToolStripMenuItem7 As ToolStripMenuItem
    Friend WithEvents ComboBox2 As ComboBox
    Friend WithEvents Label24 As Label
    Friend WithEvents Button4 As Button
    Friend WithEvents Label26 As Label
    Friend WithEvents Label27 As Label
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents SegnaComeDaCompilareToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents CheckBox3 As CheckBox
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents Label14 As Label
    Friend WithEvents CheckBox2 As CheckBox
    Friend WithEvents Label29 As Label
    Friend WithEvents Label28 As Label
    Friend WithEvents TextBox8 As TextBox
    Friend WithEvents LCP1 As Label
    Friend WithEvents TP1 As ToolTip
    Friend WithEvents CheckBox4 As CheckBox
    Friend WithEvents Label18 As Label
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents DGV2 As DataGridView
    Friend WithEvents Label19 As Label
    Friend WithEvents GroupBoxRT As GroupBox
    Friend WithEvents LabelRT As Label
    Friend WithEvents GroupBoxCP As GroupBox
    Friend WithEvents LabelCP As Label
    Friend WithEvents DGV3 As DataGridView
    Friend WithEvents GroupBox7 As GroupBox
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
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As DataGridViewTextBoxColumn
End Class
