<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F_Gestione_Codici_ELCAR
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
        TabControl1 = New TabControl()
        TabPage1 = New TabPage()
        TreeView1 = New TreeView()
        TabPage3 = New TabPage()
        TreeView2 = New TreeView()
        Button7 = New Button()
        Button1 = New Button()
        B_esporta = New Button()
        B_edit = New Button()
        Panel1 = New Panel()
        Label9 = New Label()
        TextBox2 = New TextBox()
        Label7 = New Label()
        CheckBox1 = New CheckBox()
        TextBox1 = New TextBox()
        Label8 = New Label()
        Label3 = New Label()
        Label4 = New Label()
        Label2 = New Label()
        Label1 = New Label()
        Panel2 = New Panel()
        B_esci = New Button()
        B_salva = New Button()
        TabControl1.SuspendLayout()
        TabPage1.SuspendLayout()
        TabPage3.SuspendLayout()
        Panel1.SuspendLayout()
        Panel2.SuspendLayout()
        SuspendLayout()
        ' 
        ' TabControl1
        ' 
        TabControl1.Controls.Add(TabPage1)
        TabControl1.Controls.Add(TabPage3)
        TabControl1.Location = New Point(4, 3)
        TabControl1.Margin = New Padding(2)
        TabControl1.Name = "TabControl1"
        TabControl1.SelectedIndex = 0
        TabControl1.Size = New Size(606, 207)
        TabControl1.TabIndex = 15
        ' 
        ' TabPage1
        ' 
        TabPage1.Controls.Add(TreeView1)
        TabPage1.Location = New Point(4, 24)
        TabPage1.Margin = New Padding(2)
        TabPage1.Name = "TabPage1"
        TabPage1.Padding = New Padding(2)
        TabPage1.Size = New Size(598, 179)
        TabPage1.TabIndex = 0
        TabPage1.Text = "ELCAR ORIGINALI"
        TabPage1.UseVisualStyleBackColor = True
        ' 
        ' TreeView1
        ' 
        TreeView1.Location = New Point(14, 8)
        TreeView1.Margin = New Padding(2)
        TreeView1.Name = "TreeView1"
        TreeView1.Size = New Size(573, 165)
        TreeView1.TabIndex = 0
        ' 
        ' TabPage3
        ' 
        TabPage3.Controls.Add(TreeView2)
        TabPage3.Controls.Add(Button7)
        TabPage3.Location = New Point(4, 24)
        TabPage3.Margin = New Padding(2)
        TabPage3.Name = "TabPage3"
        TabPage3.Size = New Size(598, 179)
        TabPage3.TabIndex = 2
        TabPage3.Text = "personalizzati BAMBINO"
        TabPage3.UseVisualStyleBackColor = True
        ' 
        ' TreeView2
        ' 
        TreeView2.Location = New Point(6, 11)
        TreeView2.Margin = New Padding(2)
        TreeView2.Name = "TreeView2"
        TreeView2.Size = New Size(585, 165)
        TreeView2.TabIndex = 16
        ' 
        ' Button7
        ' 
        Button7.BackColor = Color.DimGray
        Button7.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Button7.Location = New Point(511, 359)
        Button7.Name = "Button7"
        Button7.Size = New Size(65, 26)
        Button7.TabIndex = 15
        Button7.Text = "Chiudi"
        Button7.UseVisualStyleBackColor = False
        ' 
        ' Button1
        ' 
        Button1.BackColor = Color.DimGray
        Button1.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Button1.Location = New Point(515, 359)
        Button1.Name = "Button1"
        Button1.Size = New Size(65, 26)
        Button1.TabIndex = 59
        Button1.Text = "Chiudi"
        Button1.UseVisualStyleBackColor = False
        ' 
        ' B_esporta
        ' 
        B_esporta.BackColor = Color.DodgerBlue
        B_esporta.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        B_esporta.ForeColor = Color.Snow
        B_esporta.ImeMode = ImeMode.NoControl
        B_esporta.Location = New Point(19, 355)
        B_esporta.Name = "B_esporta"
        B_esporta.Size = New Size(169, 25)
        B_esporta.TabIndex = 57
        B_esporta.Text = "Esporta Importa VOCI"
        B_esporta.UseVisualStyleBackColor = False
        B_esporta.Visible = False
        ' 
        ' B_edit
        ' 
        B_edit.BackColor = SystemColors.Control
        B_edit.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        B_edit.ForeColor = SystemColors.ControlText
        B_edit.ImeMode = ImeMode.NoControl
        B_edit.Location = New Point(344, 355)
        B_edit.Name = "B_edit"
        B_edit.Size = New Size(76, 25)
        B_edit.TabIndex = 56
        B_edit.Text = "Modifica"
        B_edit.UseVisualStyleBackColor = False
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(Label9)
        Panel1.Controls.Add(TextBox2)
        Panel1.Controls.Add(Label7)
        Panel1.Controls.Add(CheckBox1)
        Panel1.Controls.Add(TextBox1)
        Panel1.Controls.Add(Label8)
        Panel1.Controls.Add(Label3)
        Panel1.Controls.Add(Label4)
        Panel1.Controls.Add(Label2)
        Panel1.Controls.Add(Label1)
        Panel1.Location = New Point(9, 214)
        Panel1.Margin = New Padding(2)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(588, 133)
        Panel1.TabIndex = 1
        ' 
        ' Label9
        ' 
        Label9.BackColor = Color.Silver
        Label9.BorderStyle = BorderStyle.Fixed3D
        Label9.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label9.Location = New Point(4, 4)
        Label9.Margin = New Padding(2, 0, 2, 0)
        Label9.Name = "Label9"
        Label9.Size = New Size(149, 17)
        Label9.TabIndex = 11
        Label9.Text = "--"
        Label9.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' TextBox2
        ' 
        TextBox2.Location = New Point(66, 76)
        TextBox2.Margin = New Padding(2)
        TextBox2.Multiline = True
        TextBox2.Name = "TextBox2"
        TextBox2.ReadOnly = True
        TextBox2.Size = New Size(498, 49)
        TextBox2.TabIndex = 10
        ' 
        ' Label7
        ' 
        Label7.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label7.Location = New Point(10, 84)
        Label7.Margin = New Padding(2, 0, 2, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(40, 35)
        Label7.TabIndex = 9
        Label7.Text = "Testo lungo:"
        ' 
        ' CheckBox1
        ' 
        CheckBox1.AutoSize = True
        CheckBox1.Location = New Point(476, 4)
        CheckBox1.Margin = New Padding(2)
        CheckBox1.Name = "CheckBox1"
        CheckBox1.Size = New Size(84, 19)
        CheckBox1.TabIndex = 8
        CheckBox1.Text = "Disabilitata"
        CheckBox1.UseVisualStyleBackColor = True
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(67, 30)
        TextBox1.Margin = New Padding(2)
        TextBox1.Multiline = True
        TextBox1.Name = "TextBox1"
        TextBox1.ReadOnly = True
        TextBox1.Size = New Size(496, 36)
        TextBox1.TabIndex = 7
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label8.Location = New Point(10, 38)
        Label8.Margin = New Padding(2, 0, 2, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(40, 15)
        Label8.TabIndex = 6
        Label8.Text = "Testo:"
        ' 
        ' Label3
        ' 
        Label3.Location = New Point(366, 5)
        Label3.Margin = New Padding(2, 0, 2, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(87, 15)
        Label3.TabIndex = 3
        Label3.Text = "XIII.12.14.12"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label4.Location = New Point(284, 5)
        Label4.Margin = New Padding(2, 0, 2, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(79, 15)
        Label4.TabIndex = 2
        Label4.Text = "Codice ECAR:"
        ' 
        ' Label2
        ' 
        Label2.Location = New Point(211, 5)
        Label2.Margin = New Padding(2, 0, 2, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(48, 15)
        Label2.TabIndex = 1
        Label2.Text = "Label2"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label1.Location = New Point(187, 5)
        Label1.Margin = New Padding(2, 0, 2, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(23, 15)
        Label1.TabIndex = 0
        Label1.Text = "ID:"
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(B_esci)
        Panel2.Controls.Add(B_salva)
        Panel2.Location = New Point(209, 352)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(217, 29)
        Panel2.TabIndex = 59
        Panel2.Visible = False
        ' 
        ' B_esci
        ' 
        B_esci.BackColor = SystemColors.Control
        B_esci.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        B_esci.ForeColor = SystemColors.ControlText
        B_esci.ImeMode = ImeMode.NoControl
        B_esci.Location = New Point(132, 3)
        B_esci.Name = "B_esci"
        B_esci.Size = New Size(76, 25)
        B_esci.TabIndex = 58
        B_esci.Text = "Esci"
        B_esci.UseVisualStyleBackColor = False
        ' 
        ' B_salva
        ' 
        B_salva.BackColor = SystemColors.Control
        B_salva.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        B_salva.ForeColor = SystemColors.ControlText
        B_salva.ImeMode = ImeMode.NoControl
        B_salva.Location = New Point(8, 3)
        B_salva.Name = "B_salva"
        B_salva.Size = New Size(76, 25)
        B_salva.TabIndex = 57
        B_salva.Text = "Salva"
        B_salva.UseVisualStyleBackColor = False
        ' 
        ' F_Gestione_Codici_ELCAR
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        AutoValidate = AutoValidate.EnablePreventFocusChange
        ClientSize = New Size(608, 391)
        ControlBox = False
        Controls.Add(Button1)
        Controls.Add(TabControl1)
        Controls.Add(B_esporta)
        Controls.Add(Panel1)
        Controls.Add(B_edit)
        Controls.Add(Panel2)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Margin = New Padding(2)
        Name = "F_Gestione_Codici_ELCAR"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Gestione voci ELCAR"
        TransparencyKey = Color.IndianRed
        TabControl1.ResumeLayout(False)
        TabPage1.ResumeLayout(False)
        TabPage3.ResumeLayout(False)
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        Panel2.ResumeLayout(False)
        ResumeLayout(False)
    End Sub
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents TreeView1 As TreeView
    Friend WithEvents Label8 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents B_esporta As Button
    Friend WithEvents B_esci As Button
    Friend WithEvents B_salva As Button
    Friend WithEvents B_edit As Button
    Friend WithEvents Button7 As Button
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Button1 As Button
    Friend WithEvents Label9 As Label
    Friend WithEvents TreeView2 As TreeView
End Class
