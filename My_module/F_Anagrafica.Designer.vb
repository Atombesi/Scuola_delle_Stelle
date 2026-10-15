<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F_Anagrafica
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F_Anagrafica))
        CheckBox1 = New CheckBox()
        Label2 = New Label()
        ComboBox1 = New ComboBox()
        Panel1 = New Panel()
        PictureBox1 = New PictureBox()
        TextBox3 = New TextBox()
        CheckBox2 = New CheckBox()
        TextBox1 = New TextBox()
        Label7 = New Label()
        TextBox5 = New TextBox()
        Label6 = New Label()
        Label5 = New Label()
        TextBox2 = New TextBox()
        Label4 = New Label()
        Label1 = New Label()
        TextBox4 = New TextBox()
        Button1 = New Button()
        Button2 = New Button()
        Button3 = New Button()
        ImageList1 = New ImageList(components)
        Button4 = New Button()
        Button5 = New Button()
        Label3 = New Label()
        CheckBox3 = New CheckBox()
        Panel1.SuspendLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' CheckBox1
        ' 
        CheckBox1.AutoSize = True
        CheckBox1.Checked = True
        CheckBox1.CheckState = CheckState.Checked
        CheckBox1.Location = New Point(130, 7)
        CheckBox1.Name = "CheckBox1"
        CheckBox1.Size = New Size(78, 19)
        CheckBox1.TabIndex = 9
        CheckBox1.Text = "Solo attivi"
        CheckBox1.UseVisualStyleBackColor = True
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(8, 7)
        Label2.Name = "Label2"
        Label2.Size = New Size(76, 15)
        Label2.TabIndex = 8
        Label2.Text = "Elenco Bimbi"
        ' 
        ' ComboBox1
        ' 
        ComboBox1.DropDownStyle = ComboBoxStyle.Simple
        ComboBox1.FormattingEnabled = True
        ComboBox1.Location = New Point(8, 32)
        ComboBox1.Name = "ComboBox1"
        ComboBox1.Size = New Size(200, 203)
        ComboBox1.TabIndex = 7
        ' 
        ' Panel1
        ' 
        Panel1.BorderStyle = BorderStyle.FixedSingle
        Panel1.Controls.Add(CheckBox3)
        Panel1.Controls.Add(PictureBox1)
        Panel1.Controls.Add(TextBox3)
        Panel1.Controls.Add(CheckBox2)
        Panel1.Controls.Add(TextBox1)
        Panel1.Controls.Add(Label7)
        Panel1.Controls.Add(TextBox5)
        Panel1.Controls.Add(Label6)
        Panel1.Controls.Add(Label5)
        Panel1.Controls.Add(TextBox2)
        Panel1.Controls.Add(Label4)
        Panel1.Controls.Add(Label1)
        Panel1.Controls.Add(TextBox4)
        Panel1.Location = New Point(225, 32)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(241, 193)
        Panel1.TabIndex = 11
        ' 
        ' PictureBox1
        ' 
        PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), Image)
        PictureBox1.Location = New Point(71, 134)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(13, 13)
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBox1.TabIndex = 11
        PictureBox1.TabStop = False
        ' 
        ' TextBox3
        ' 
        TextBox3.Location = New Point(93, 72)
        TextBox3.Name = "TextBox3"
        TextBox3.Size = New Size(134, 23)
        TextBox3.TabIndex = 5
        ' 
        ' CheckBox2
        ' 
        CheckBox2.AutoSize = True
        CheckBox2.Checked = True
        CheckBox2.CheckState = CheckState.Checked
        CheckBox2.Location = New Point(12, 161)
        CheckBox2.Name = "CheckBox2"
        CheckBox2.RightToLeft = RightToLeft.Yes
        CheckBox2.Size = New Size(58, 19)
        CheckBox2.TabIndex = 10
        CheckBox2.Text = "Attivo"
        CheckBox2.TextAlign = ContentAlignment.MiddleRight
        CheckBox2.UseVisualStyleBackColor = True
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(93, 13)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(134, 23)
        TextBox1.TabIndex = 1
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Location = New Point(12, 133)
        Label7.Name = "Label7"
        Label7.Size = New Size(55, 15)
        Label7.TabIndex = 8
        Label7.Text = "DB name"
        ' 
        ' TextBox5
        ' 
        TextBox5.Location = New Point(93, 130)
        TextBox5.Name = "TextBox5"
        TextBox5.Size = New Size(134, 23)
        TextBox5.TabIndex = 9
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(12, 104)
        Label6.Name = "Label6"
        Label6.Size = New Size(73, 15)
        Label6.TabIndex = 6
        Label6.Text = "Data Nascita"
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(12, 75)
        Label5.Name = "Label5"
        Label5.Size = New Size(53, 15)
        Label5.TabIndex = 4
        Label5.Text = "nikname"
        ' 
        ' TextBox2
        ' 
        TextBox2.Location = New Point(93, 42)
        TextBox2.Name = "TextBox2"
        TextBox2.Size = New Size(134, 23)
        TextBox2.TabIndex = 3
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(12, 45)
        Label4.Name = "Label4"
        Label4.Size = New Size(40, 15)
        Label4.TabIndex = 2
        Label4.Text = "Nome"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(12, 16)
        Label1.Name = "Label1"
        Label1.Size = New Size(60, 15)
        Label1.TabIndex = 0
        Label1.Text = "Cognome"
        ' 
        ' TextBox4
        ' 
        TextBox4.Location = New Point(93, 101)
        TextBox4.Name = "TextBox4"
        TextBox4.Size = New Size(134, 23)
        TextBox4.TabIndex = 7
        ' 
        ' Button1
        ' 
        Button1.BackColor = Color.DimGray
        Button1.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Button1.ForeColor = Color.Snow
        Button1.Location = New Point(391, 278)
        Button1.Name = "Button1"
        Button1.Size = New Size(75, 31)
        Button1.TabIndex = 12
        Button1.Text = "Close"
        Button1.UseVisualStyleBackColor = False
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(225, 231)
        Button2.Name = "Button2"
        Button2.Size = New Size(75, 28)
        Button2.TabIndex = 13
        Button2.Text = "Modifica"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Button3
        ' 
        Button3.Location = New Point(345, 232)
        Button3.Name = "Button3"
        Button3.Size = New Size(56, 28)
        Button3.TabIndex = 14
        Button3.Text = "Annulla"
        Button3.UseVisualStyleBackColor = True
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
        ' Button4
        ' 
        Button4.Location = New Point(410, 231)
        Button4.Name = "Button4"
        Button4.Size = New Size(56, 28)
        Button4.TabIndex = 16
        Button4.Text = "Salva"
        Button4.UseVisualStyleBackColor = True
        ' 
        ' Button5
        ' 
        Button5.Location = New Point(8, 232)
        Button5.Name = "Button5"
        Button5.Size = New Size(75, 28)
        Button5.TabIndex = 17
        Button5.Text = "Aggiungi"
        Button5.UseVisualStyleBackColor = True
        ' 
        ' Label3
        ' 
        Label3.Location = New Point(225, 8)
        Label3.Name = "Label3"
        Label3.Size = New Size(241, 17)
        Label3.TabIndex = 18
        Label3.Text = "Elenco Bimbi"
        Label3.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' CheckBox3
        ' 
        CheckBox3.AutoSize = True
        CheckBox3.Checked = True
        CheckBox3.CheckState = CheckState.Checked
        CheckBox3.Location = New Point(153, 159)
        CheckBox3.Name = "CheckBox3"
        CheckBox3.RightToLeft = RightToLeft.Yes
        CheckBox3.Size = New Size(71, 19)
        CheckBox3.TabIndex = 12
        CheckBox3.Text = "Presente"
        CheckBox3.TextAlign = ContentAlignment.MiddleRight
        CheckBox3.UseVisualStyleBackColor = True
        ' 
        ' F_Anagrafica
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(478, 316)
        Controls.Add(Label3)
        Controls.Add(Button5)
        Controls.Add(Button4)
        Controls.Add(Button3)
        Controls.Add(Button2)
        Controls.Add(Button1)
        Controls.Add(Panel1)
        Controls.Add(CheckBox1)
        Controls.Add(Label2)
        Controls.Add(ComboBox1)
        Name = "F_Anagrafica"
        Text = "Anagrafica Ragazzi"
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents Label2 As Label
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents CheckBox2 As CheckBox
    Friend WithEvents TextBox5 As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Panel_TRASPARENT1 As Panel
    Friend WithEvents Button4 As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents CheckBox3 As CheckBox
End Class
