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
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        GroupBox4 = New GroupBox()
        ComboBox7 = New ComboBox()
        DateTimePicker1 = New DateTimePicker()
        Label13 = New Label()
        Label14 = New Label()
        Label11 = New Label()
        Button6 = New Button()
        Label10 = New Label()
        Label8 = New Label()
        DGV1 = New DataGridView()
        col1 = New DataGridViewTextBoxColumn()
        Col2 = New DataGridViewTextBoxColumn()
        Column1 = New DataGridViewTextBoxColumn()
        Column2 = New DataGridViewTextBoxColumn()
        Column3 = New DataGridViewTextBoxColumn()
        Column4 = New DataGridViewTextBoxColumn()
        Button5 = New Button()
        GroupBox1 = New GroupBox()
        Button1 = New Button()
        Label7 = New Label()
        TextBox4 = New TextBox()
        Label4 = New Label()
        TextBox3 = New TextBox()
        Label3 = New Label()
        TextBox2 = New TextBox()
        Label2 = New Label()
        TextBox1 = New TextBox()
        Label1 = New Label()
        Label5 = New Label()
        Label6 = New Label()
        GroupBox4.SuspendLayout()
        CType(DGV1, ComponentModel.ISupportInitialize).BeginInit()
        GroupBox1.SuspendLayout()
        SuspendLayout()
        ' 
        ' GroupBox4
        ' 
        GroupBox4.Controls.Add(ComboBox7)
        GroupBox4.Controls.Add(DateTimePicker1)
        GroupBox4.Controls.Add(Label13)
        GroupBox4.Controls.Add(Label14)
        GroupBox4.Controls.Add(Label11)
        GroupBox4.Controls.Add(Button6)
        GroupBox4.Controls.Add(Label10)
        GroupBox4.Controls.Add(Label8)
        GroupBox4.Controls.Add(DGV1)
        GroupBox4.Location = New Point(12, 38)
        GroupBox4.Name = "GroupBox4"
        GroupBox4.Size = New Size(502, 452)
        GroupBox4.TabIndex = 25
        GroupBox4.TabStop = False
        GroupBox4.Text = "Foglio Esercizi "
        ' 
        ' ComboBox7
        ' 
        ComboBox7.FormattingEnabled = True
        ComboBox7.Location = New Point(296, 21)
        ComboBox7.Name = "ComboBox7"
        ComboBox7.Size = New Size(195, 23)
        ComboBox7.TabIndex = 28
        ' 
        ' DateTimePicker1
        ' 
        DateTimePicker1.Enabled = False
        DateTimePicker1.Format = DateTimePickerFormat.Short
        DateTimePicker1.Location = New Point(55, 21)
        DateTimePicker1.Name = "DateTimePicker1"
        DateTimePicker1.Size = New Size(89, 23)
        DateTimePicker1.TabIndex = 26
        ' 
        ' Label13
        ' 
        Label13.AutoSize = True
        Label13.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label13.Location = New Point(213, 25)
        Label13.Name = "Label13"
        Label13.Size = New Size(14, 15)
        Label13.TabIndex = 25
        Label13.Text = "0"
        ' 
        ' Label14
        ' 
        Label14.AutoSize = True
        Label14.Location = New Point(153, 26)
        Label14.Name = "Label14"
        Label14.Size = New Size(58, 15)
        Label14.TabIndex = 24
        Label14.Text = "N. Foglio:"
        ' 
        ' Label11
        ' 
        Label11.AutoSize = True
        Label11.Location = New Point(15, 25)
        Label11.Name = "Label11"
        Label11.Size = New Size(34, 15)
        Label11.TabIndex = 22
        Label11.Text = "Data:"
        ' 
        ' Button6
        ' 
        Button6.Font = New Font("Segoe UI", 9F)
        Button6.ForeColor = SystemColors.ControlText
        Button6.Location = New Point(6, 419)
        Button6.Name = "Button6"
        Button6.Size = New Size(67, 22)
        Button6.TabIndex = 20
        Button6.Text = "Riutilizza"
        Button6.UseVisualStyleBackColor = True
        ' 
        ' Label10
        ' 
        Label10.AutoSize = True
        Label10.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label10.Location = New Point(150, 66)
        Label10.Name = "Label10"
        Label10.Size = New Size(14, 15)
        Label10.TabIndex = 7
        Label10.Text = "0"
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Location = New Point(11, 66)
        Label8.Name = "Label8"
        Label8.Size = New Size(133, 15)
        Label8.TabIndex = 6
        Label8.Text = "Numero esercizi inseriti:"
        ' 
        ' DGV1
        ' 
        DGV1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DGV1.Columns.AddRange(New DataGridViewColumn() {col1, Col2, Column1, Column2, Column3, Column4})
        DGV1.Location = New Point(6, 84)
        DGV1.Name = "DGV1"
        DGV1.RowHeadersVisible = False
        DGV1.Size = New Size(496, 329)
        DGV1.TabIndex = 0
        ' 
        ' col1
        ' 
        col1.HeaderText = "Codice"
        col1.Name = "col1"
        col1.Width = 90
        ' 
        ' Col2
        ' 
        DataGridViewCellStyle1.Font = New Font("Calibri", 6.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Col2.DefaultCellStyle = DataGridViewCellStyle1
        Col2.HeaderText = "Testo inserito"
        Col2.Name = "Col2"
        Col2.Width = 200
        ' 
        ' Column1
        ' 
        Column1.FillWeight = 40F
        Column1.HeaderText = "Pos"
        Column1.Name = "Column1"
        Column1.Width = 40
        ' 
        ' Column2
        ' 
        Column2.HeaderText = "Neg"
        Column2.Name = "Column2"
        Column2.Width = 40
        ' 
        ' Column3
        ' 
        Column3.HeaderText = "Prompt"
        Column3.Name = "Column3"
        Column3.Width = 40
        ' 
        ' Column4
        ' 
        Column4.HeaderText = "Tot"
        Column4.Name = "Column4"
        Column4.Width = 40
        ' 
        ' Button5
        ' 
        Button5.Font = New Font("Segoe UI", 9F)
        Button5.ForeColor = SystemColors.ControlText
        Button5.Location = New Point(578, 457)
        Button5.Name = "Button5"
        Button5.Size = New Size(127, 22)
        Button5.TabIndex = 19
        Button5.Text = "Conferma e Salva"
        Button5.UseVisualStyleBackColor = True
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(Button1)
        GroupBox1.Controls.Add(Label7)
        GroupBox1.Controls.Add(TextBox4)
        GroupBox1.Controls.Add(Label4)
        GroupBox1.Controls.Add(TextBox3)
        GroupBox1.Controls.Add(Label3)
        GroupBox1.Controls.Add(TextBox2)
        GroupBox1.Controls.Add(Label2)
        GroupBox1.Controls.Add(TextBox1)
        GroupBox1.Controls.Add(Label1)
        GroupBox1.Location = New Point(520, 227)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(217, 224)
        GroupBox1.TabIndex = 26
        GroupBox1.TabStop = False
        GroupBox1.Text = "Risultati"
        ' 
        ' Button1
        ' 
        Button1.Font = New Font("Segoe UI", 9F)
        Button1.ForeColor = SystemColors.ControlText
        Button1.Location = New Point(59, 186)
        Button1.Name = "Button1"
        Button1.Size = New Size(67, 22)
        Button1.TabIndex = 21
        Button1.Text = "Next"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Label7
        ' 
        Label7.Font = New Font("Segoe UI", 8F)
        Label7.Location = New Point(8, 32)
        Label7.Name = "Label7"
        Label7.Size = New Size(250, 31)
        Label7.TabIndex = 8
        Label7.Text = "sd sd d sd s s sd ù"
        ' 
        ' TextBox4
        ' 
        TextBox4.Location = New Point(162, 93)
        TextBox4.Name = "TextBox4"
        TextBox4.Size = New Size(44, 23)
        TextBox4.TabIndex = 7
        ' 
        ' Label4
        ' 
        Label4.Location = New Point(160, 73)
        Label4.Name = "Label4"
        Label4.Size = New Size(51, 17)
        Label4.TabIndex = 6
        Label4.Text = "Totale"
        ' 
        ' TextBox3
        ' 
        TextBox3.Location = New Point(112, 93)
        TextBox3.Name = "TextBox3"
        TextBox3.Size = New Size(44, 23)
        TextBox3.TabIndex = 5
        ' 
        ' Label3
        ' 
        Label3.Location = New Point(112, 73)
        Label3.Name = "Label3"
        Label3.Size = New Size(51, 17)
        Label3.TabIndex = 4
        Label3.Text = "Prompt"
        ' 
        ' TextBox2
        ' 
        TextBox2.Location = New Point(59, 93)
        TextBox2.Name = "TextBox2"
        TextBox2.Size = New Size(44, 23)
        TextBox2.TabIndex = 3
        ' 
        ' Label2
        ' 
        Label2.Location = New Point(58, 73)
        Label2.Name = "Label2"
        Label2.Size = New Size(56, 17)
        Label2.TabIndex = 2
        Label2.Text = "Negativi"
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(8, 93)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(44, 23)
        TextBox1.TabIndex = 1
        ' 
        ' Label1
        ' 
        Label1.Location = New Point(6, 73)
        Label1.Name = "Label1"
        Label1.Size = New Size(50, 17)
        Label1.TabIndex = 0
        Label1.Text = "Positivi"
        ' 
        ' Label5
        ' 
        Label5.BackColor = Color.FromArgb(CByte(192), CByte(255), CByte(192))
        Label5.BorderStyle = BorderStyle.Fixed3D
        Label5.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label5.Location = New Point(237, 9)
        Label5.Name = "Label5"
        Label5.Size = New Size(272, 17)
        Label5.TabIndex = 27
        Label5.Text = "Foglio da compilare"
        Label5.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label6
        ' 
        Label6.BackColor = Color.FromArgb(CByte(192), CByte(255), CByte(192))
        Label6.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label6.Location = New Point(12, 9)
        Label6.Name = "Label6"
        Label6.Size = New Size(218, 17)
        Label6.TabIndex = 28
        Label6.Text = "Pinco Pallino giuseppe"
        Label6.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' F_Foglio_esistente
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(747, 494)
        Controls.Add(Label6)
        Controls.Add(Label5)
        Controls.Add(GroupBox1)
        Controls.Add(GroupBox4)
        Controls.Add(Button5)
        Name = "F_Foglio_esistente"
        Text = "F_Foglio_esistete"
        GroupBox4.ResumeLayout(False)
        GroupBox4.PerformLayout()
        CType(DGV1, ComponentModel.ISupportInitialize).EndInit()
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents ComboBox7 As ComboBox
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents Label13 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Button6 As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents Label10 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents DGV1 As DataGridView
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents col1 As DataGridViewTextBoxColumn
    Friend WithEvents Col2 As DataGridViewTextBoxColumn
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents Button1 As Button
End Class
