<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F_MyGraph
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F_MyGraph))
        Label1 = New Label()
        tp1 = New ToolTip(components)
        ImageList1 = New ImageList(components)
        l1 = New Label()
        GroupBox1 = New GroupBox()
        l8 = New Label()
        l7 = New Label()
        L6 = New Label()
        Label2 = New Label()
        Label7 = New Label()
        Label6 = New Label()
        Label5 = New Label()
        l5 = New Label()
        l4 = New Label()
        l3 = New Label()
        l2 = New Label()
        Label4 = New Label()
        Label3 = New Label()
        TabControl1 = New TabControl()
        TabPage2 = New TabPage()
        Panel1 = New Panel()
        TabPage1 = New TabPage()
        DGV1 = New DataGridView()
        Column1 = New DataGridViewTextBoxColumn()
        Column2 = New DataGridViewTextBoxColumn()
        Column3 = New DataGridViewTextBoxColumn()
        Column4 = New DataGridViewTextBoxColumn()
        Column5 = New DataGridViewTextBoxColumn()
        Column6 = New DataGridViewTextBoxColumn()
        TabPage3 = New TabPage()
        Label12 = New Label()
        Label11 = New Label()
        Label10 = New Label()
        Label9 = New Label()
        Label8 = New Label()
        GroupBox1.SuspendLayout()
        TabControl1.SuspendLayout()
        TabPage2.SuspendLayout()
        TabPage1.SuspendLayout()
        CType(DGV1, ComponentModel.ISupportInitialize).BeginInit()
        TabPage3.SuspendLayout()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.BorderStyle = BorderStyle.Fixed3D
        Label1.Location = New Point(8, 7)
        Label1.Name = "Label1"
        Label1.Size = New Size(94, 21)
        Label1.TabIndex = 0
        Label1.Text = "Label1"
        ' 
        ' ImageList1
        ' 
        ImageList1.ColorDepth = ColorDepth.Depth32Bit
        ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), ImageListStreamer)
        ImageList1.TransparentColor = Color.Transparent
        ImageList1.Images.SetKeyName(0, "Emoji Symbols-199.png")
        ImageList1.Images.SetKeyName(1, "Emoji Symbols-200.png")
        ImageList1.Images.SetKeyName(2, "Emoji Symbols-201.png")
        ImageList1.Images.SetKeyName(3, "Emoji Symbols-202 - Copy.png")
        ImageList1.Images.SetKeyName(4, "Emoji Symbols-202.png")
        ImageList1.Images.SetKeyName(5, "Emoji Symbols-211  Yellow.png")
        ' 
        ' l1
        ' 
        l1.Font = New Font("Calibri", 6.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        l1.ImageAlign = ContentAlignment.TopCenter
        l1.ImageIndex = 2
        l1.ImageList = ImageList1
        l1.Location = New Point(10, 19)
        l1.Name = "l1"
        l1.Size = New Size(23, 26)
        l1.TabIndex = 1
        l1.Text = "55"
        l1.TextAlign = ContentAlignment.BottomCenter
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(l8)
        GroupBox1.Controls.Add(l7)
        GroupBox1.Controls.Add(L6)
        GroupBox1.Controls.Add(Label2)
        GroupBox1.Controls.Add(Label7)
        GroupBox1.Controls.Add(Label6)
        GroupBox1.Controls.Add(Label5)
        GroupBox1.Controls.Add(l5)
        GroupBox1.Controls.Add(l4)
        GroupBox1.Controls.Add(l3)
        GroupBox1.Controls.Add(l2)
        GroupBox1.Controls.Add(l1)
        GroupBox1.Location = New Point(2, 29)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(280, 74)
        GroupBox1.TabIndex = 2
        GroupBox1.TabStop = False
        GroupBox1.Text = "Trend"
        ' 
        ' l8
        ' 
        l8.Font = New Font("Calibri", 6.75F)
        l8.ImageAlign = ContentAlignment.TopCenter
        l8.ImageIndex = 1
        l8.ImageList = ImageList1
        l8.Location = New Point(229, 19)
        l8.Name = "l8"
        l8.Size = New Size(23, 26)
        l8.TabIndex = 15
        l8.TextAlign = ContentAlignment.BottomCenter
        ' 
        ' l7
        ' 
        l7.Font = New Font("Calibri", 6.75F)
        l7.ImageAlign = ContentAlignment.TopCenter
        l7.ImageIndex = 1
        l7.ImageList = ImageList1
        l7.Location = New Point(200, 19)
        l7.Name = "l7"
        l7.Size = New Size(23, 26)
        l7.TabIndex = 14
        l7.TextAlign = ContentAlignment.BottomCenter
        ' 
        ' L6
        ' 
        L6.Font = New Font("Calibri", 6.75F)
        L6.ImageAlign = ContentAlignment.TopCenter
        L6.ImageIndex = 1
        L6.ImageList = ImageList1
        L6.Location = New Point(171, 19)
        L6.Name = "L6"
        L6.Size = New Size(23, 26)
        L6.TabIndex = 13
        L6.TextAlign = ContentAlignment.BottomCenter
        ' 
        ' Label2
        ' 
        Label2.BorderStyle = BorderStyle.FixedSingle
        Label2.Location = New Point(226, 48)
        Label2.Name = "Label2"
        Label2.Size = New Size(42, 21)
        Label2.TabIndex = 12
        Label2.Text = "10/12"
        Label2.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label7
        ' 
        Label7.Location = New Point(147, 48)
        Label7.Name = "Label7"
        Label7.Size = New Size(72, 21)
        Label7.TabIndex = 11
        Label7.Text = "Completate:"
        Label7.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label6
        ' 
        Label6.BorderStyle = BorderStyle.FixedSingle
        Label6.Location = New Point(72, 48)
        Label6.Name = "Label6"
        Label6.Size = New Size(69, 21)
        Label6.TabIndex = 10
        Label6.Text = "12 in 10 gg"
        Label6.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label5
        ' 
        Label5.Location = New Point(6, 47)
        Label5.Name = "Label5"
        Label5.Size = New Size(73, 21)
        Label5.TabIndex = 9
        Label5.Text = "Ripetizioni:"
        Label5.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' l5
        ' 
        l5.Font = New Font("Calibri", 6.75F)
        l5.ImageAlign = ContentAlignment.TopCenter
        l5.ImageIndex = 1
        l5.ImageList = ImageList1
        l5.Location = New Point(142, 19)
        l5.Name = "l5"
        l5.Size = New Size(23, 26)
        l5.TabIndex = 5
        l5.TextAlign = ContentAlignment.BottomCenter
        ' 
        ' l4
        ' 
        l4.Font = New Font("Calibri", 6.75F)
        l4.ImageAlign = ContentAlignment.TopCenter
        l4.ImageIndex = 2
        l4.ImageList = ImageList1
        l4.Location = New Point(109, 19)
        l4.Name = "l4"
        l4.Size = New Size(23, 26)
        l4.TabIndex = 4
        l4.TextAlign = ContentAlignment.BottomCenter
        ' 
        ' l3
        ' 
        l3.Font = New Font("Calibri", 6.75F)
        l3.ImageAlign = ContentAlignment.TopCenter
        l3.ImageIndex = 2
        l3.ImageList = ImageList1
        l3.Location = New Point(76, 19)
        l3.Name = "l3"
        l3.Size = New Size(23, 26)
        l3.TabIndex = 3
        l3.TextAlign = ContentAlignment.BottomCenter
        ' 
        ' l2
        ' 
        l2.Font = New Font("Calibri", 6.75F)
        l2.ImageAlign = ContentAlignment.TopCenter
        l2.ImageIndex = 2
        l2.ImageList = ImageList1
        l2.Location = New Point(43, 19)
        l2.Name = "l2"
        l2.Size = New Size(23, 26)
        l2.TabIndex = 2
        l2.TextAlign = ContentAlignment.BottomCenter
        ' 
        ' Label4
        ' 
        Label4.BorderStyle = BorderStyle.Fixed3D
        Label4.Location = New Point(4, 110)
        Label4.Name = "Label4"
        Label4.Size = New Size(276, 19)
        Label4.TabIndex = 8
        Label4.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label3
        ' 
        Label3.Font = New Font("Calibri", 8F)
        Label3.Location = New Point(108, 7)
        Label3.Name = "Label3"
        Label3.Size = New Size(165, 25)
        Label3.TabIndex = 3
        Label3.Text = "Label3"
        Label3.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' TabControl1
        ' 
        TabControl1.Controls.Add(TabPage2)
        TabControl1.Controls.Add(TabPage1)
        TabControl1.Controls.Add(TabPage3)
        TabControl1.Location = New Point(2, 132)
        TabControl1.Name = "TabControl1"
        TabControl1.SelectedIndex = 0
        TabControl1.Size = New Size(289, 265)
        TabControl1.TabIndex = 5
        ' 
        ' TabPage2
        ' 
        TabPage2.Controls.Add(Panel1)
        TabPage2.Location = New Point(4, 24)
        TabPage2.Name = "TabPage2"
        TabPage2.Padding = New Padding(3)
        TabPage2.Size = New Size(281, 237)
        TabPage2.TabIndex = 1
        TabPage2.Text = "Graph"
        TabPage2.UseVisualStyleBackColor = True
        ' 
        ' Panel1
        ' 
        Panel1.Location = New Point(3, 3)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(273, 226)
        Panel1.TabIndex = 5
        ' 
        ' TabPage1
        ' 
        TabPage1.Controls.Add(DGV1)
        TabPage1.Location = New Point(4, 24)
        TabPage1.Name = "TabPage1"
        TabPage1.Padding = New Padding(3)
        TabPage1.Size = New Size(281, 237)
        TabPage1.TabIndex = 2
        TabPage1.Text = "Stat"
        TabPage1.UseVisualStyleBackColor = True
        ' 
        ' DGV1
        ' 
        DGV1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DGV1.Columns.AddRange(New DataGridViewColumn() {Column1, Column2, Column3, Column4, Column5, Column6})
        DGV1.Location = New Point(6, 6)
        DGV1.Name = "DGV1"
        DGV1.RowHeadersVisible = False
        DGV1.Size = New Size(266, 236)
        DGV1.TabIndex = 0
        ' 
        ' Column1
        ' 
        Column1.HeaderText = "Data"
        Column1.Name = "Column1"
        Column1.Width = 85
        ' 
        ' Column2
        ' 
        Column2.HeaderText = "pos"
        Column2.Name = "Column2"
        Column2.Width = 35
        ' 
        ' Column3
        ' 
        Column3.HeaderText = "neg"
        Column3.Name = "Column3"
        Column3.Width = 35
        ' 
        ' Column4
        ' 
        Column4.HeaderText = "Prmt"
        Column4.Name = "Column4"
        Column4.Width = 35
        ' 
        ' Column5
        ' 
        Column5.HeaderText = "tot"
        Column5.Name = "Column5"
        Column5.Width = 35
        ' 
        ' Column6
        ' 
        Column6.HeaderText = "%"
        Column6.Name = "Column6"
        Column6.Width = 35
        ' 
        ' TabPage3
        ' 
        TabPage3.Controls.Add(Label12)
        TabPage3.Controls.Add(Label11)
        TabPage3.Controls.Add(Label10)
        TabPage3.Controls.Add(Label9)
        TabPage3.Controls.Add(Label8)
        TabPage3.Location = New Point(4, 24)
        TabPage3.Name = "TabPage3"
        TabPage3.Padding = New Padding(3)
        TabPage3.Size = New Size(281, 237)
        TabPage3.TabIndex = 3
        TabPage3.Text = "Info"
        TabPage3.UseVisualStyleBackColor = True
        ' 
        ' Label12
        ' 
        Label12.Font = New Font("Calibri", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label12.ImageAlign = ContentAlignment.MiddleLeft
        Label12.ImageIndex = 5
        Label12.ImageList = ImageList1
        Label12.Location = New Point(6, 114)
        Label12.Name = "Label12"
        Label12.Size = New Size(232, 26)
        Label12.TabIndex = 6
        Label12.Text = "     Nessun Trend (<8) (Valutare)"
        Label12.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label11
        ' 
        Label11.Font = New Font("Calibri", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label11.ImageAlign = ContentAlignment.MiddleLeft
        Label11.ImageIndex = 0
        Label11.ImageList = ImageList1
        Label11.Location = New Point(6, 88)
        Label11.Name = "Label11"
        Label11.Size = New Size(232, 26)
        Label11.TabIndex = 5
        Label11.Text = "     Dati insuffcienti  (Continuare)"
        Label11.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label10
        ' 
        Label10.Font = New Font("Calibri", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label10.ImageAlign = ContentAlignment.MiddleLeft
        Label10.ImageIndex = 2
        Label10.ImageList = ImageList1
        Label10.Location = New Point(6, 62)
        Label10.Name = "Label10"
        Label10.Size = New Size(232, 26)
        Label10.TabIndex = 4
        Label10.Text = "     Trend Negativo (Cambiare)"
        Label10.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label9
        ' 
        Label9.Font = New Font("Calibri", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label9.ImageAlign = ContentAlignment.MiddleLeft
        Label9.ImageIndex = 3
        Label9.ImageList = ImageList1
        Label9.Location = New Point(6, 36)
        Label9.Name = "Label9"
        Label9.Size = New Size(232, 26)
        Label9.TabIndex = 3
        Label9.Text = "     Trend Positivo (Continuare)"
        Label9.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label8
        ' 
        Label8.Font = New Font("Calibri", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label8.ImageAlign = ContentAlignment.MiddleLeft
        Label8.ImageIndex = 4
        Label8.ImageList = ImageList1
        Label8.Location = New Point(6, 10)
        Label8.Name = "Label8"
        Label8.Size = New Size(155, 26)
        Label8.TabIndex = 2
        Label8.Text = "     Obiettivo ragiunto"
        Label8.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' F_MyGraph
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(291, 401)
        Controls.Add(TabControl1)
        Controls.Add(Label3)
        Controls.Add(GroupBox1)
        Controls.Add(Label1)
        Controls.Add(Label4)
        Name = "F_MyGraph"
        Text = "Trend"
        TopMost = True
        GroupBox1.ResumeLayout(False)
        TabControl1.ResumeLayout(False)
        TabPage2.ResumeLayout(False)
        TabPage1.ResumeLayout(False)
        CType(DGV1, ComponentModel.ISupportInitialize).EndInit()
        TabPage3.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents tp1 As ToolTip
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents l1 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents l5 As Label
    Friend WithEvents l4 As Label
    Friend WithEvents l3 As Label
    Friend WithEvents l2 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Panel1 As Panel
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents DGV1 As DataGridView
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents Column5 As DataGridViewTextBoxColumn
    Friend WithEvents Column6 As DataGridViewTextBoxColumn
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents l8 As Label
    Friend WithEvents l7 As Label
    Friend WithEvents L6 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label12 As Label
End Class
