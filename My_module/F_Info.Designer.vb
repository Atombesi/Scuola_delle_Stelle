<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Info
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Info))
        GroupBox1 = New GroupBox()
        Label1 = New Label()
        ImageList1 = New ImageList(components)
        Label12 = New Label()
        Label11 = New Label()
        Label10 = New Label()
        Label9 = New Label()
        Label8 = New Label()
        Button4 = New Button()
        Label2 = New Label()
        Label3 = New Label()
        Label4 = New Label()
        Label5 = New Label()
        Label6 = New Label()
        Label7 = New Label()
        GroupBox1.SuspendLayout()
        SuspendLayout()
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(Label7)
        GroupBox1.Controls.Add(Label6)
        GroupBox1.Controls.Add(Label5)
        GroupBox1.Controls.Add(Label4)
        GroupBox1.Controls.Add(Label3)
        GroupBox1.Controls.Add(Label2)
        GroupBox1.Controls.Add(Label1)
        GroupBox1.Controls.Add(Label12)
        GroupBox1.Controls.Add(Label11)
        GroupBox1.Controls.Add(Label10)
        GroupBox1.Controls.Add(Label9)
        GroupBox1.Controls.Add(Label8)
        GroupBox1.Location = New Point(3, -2)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(205, 249)
        GroupBox1.TabIndex = 0
        GroupBox1.TabStop = False
        ' 
        ' Label1
        ' 
        Label1.Font = New Font("Calibri", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label1.ImageAlign = ContentAlignment.MiddleLeft
        Label1.ImageIndex = 1
        Label1.ImageList = ImageList1
        Label1.Location = New Point(6, 205)
        Label1.Name = "Label1"
        Label1.Size = New Size(188, 20)
        Label1.TabIndex = 12
        Label1.Text = "     Nessun dato inserito"
        Label1.TextAlign = ContentAlignment.MiddleLeft
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
        ' Label12
        ' 
        Label12.Font = New Font("Calibri", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label12.ImageAlign = ContentAlignment.MiddleLeft
        Label12.ImageIndex = 5
        Label12.ImageList = ImageList1
        Label12.Location = New Point(6, 127)
        Label12.Name = "Label12"
        Label12.Size = New Size(188, 26)
        Label12.TabIndex = 11
        Label12.Text = "     Nessun Trend (<8) (Valutare)"
        Label12.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label11
        ' 
        Label11.Font = New Font("Calibri", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label11.ImageAlign = ContentAlignment.MiddleLeft
        Label11.ImageIndex = 0
        Label11.ImageList = ImageList1
        Label11.Location = New Point(6, 166)
        Label11.Name = "Label11"
        Label11.Size = New Size(188, 26)
        Label11.TabIndex = 10
        Label11.Text = "     Dati insuffcienti  (Continuare)"
        Label11.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label10
        ' 
        Label10.Font = New Font("Calibri", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label10.ImageAlign = ContentAlignment.MiddleLeft
        Label10.ImageIndex = 2
        Label10.ImageList = ImageList1
        Label10.Location = New Point(6, 91)
        Label10.Name = "Label10"
        Label10.Size = New Size(188, 23)
        Label10.TabIndex = 9
        Label10.Text = "     Trend Negativo (Cambiare)"
        Label10.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label9
        ' 
        Label9.Font = New Font("Calibri", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label9.ImageAlign = ContentAlignment.MiddleLeft
        Label9.ImageIndex = 3
        Label9.ImageList = ImageList1
        Label9.Location = New Point(6, 51)
        Label9.Name = "Label9"
        Label9.Size = New Size(188, 22)
        Label9.TabIndex = 8
        Label9.Text = "     Trend Positivo (Continuare)"
        Label9.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label8
        ' 
        Label8.Font = New Font("Calibri", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label8.ImageAlign = ContentAlignment.MiddleLeft
        Label8.ImageIndex = 4
        Label8.ImageList = ImageList1
        Label8.Location = New Point(6, 12)
        Label8.Name = "Label8"
        Label8.Size = New Size(155, 26)
        Label8.TabIndex = 7
        Label8.Text = "     Obiettivo ragiunto"
        Label8.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Button4
        ' 
        Button4.BackColor = Color.DimGray
        Button4.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Button4.ForeColor = Color.Snow
        Button4.Location = New Point(142, 257)
        Button4.Name = "Button4"
        Button4.Size = New Size(66, 35)
        Button4.TabIndex = 22
        Button4.Text = "Close"
        Button4.UseVisualStyleBackColor = False
        ' 
        ' Label2
        ' 
        Label2.Font = New Font("Calibri", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label2.ImageAlign = ContentAlignment.MiddleLeft
        Label2.ImageIndex = 3
        Label2.Location = New Point(22, 33)
        Label2.Name = "Label2"
        Label2.Size = New Size(173, 17)
        Label2.TabIndex = 13
        Label2.Text = "3 eventi consecutivi >= 80%"
        Label2.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label3
        ' 
        Label3.Font = New Font("Calibri", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label3.ImageAlign = ContentAlignment.MiddleLeft
        Label3.ImageIndex = 3
        Label3.Location = New Point(21, 71)
        Label3.Name = "Label3"
        Label3.Size = New Size(173, 17)
        Label3.TabIndex = 14
        Label3.Text = "4 eventi consec. crescenti < 80%"
        Label3.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label4
        ' 
        Label4.Font = New Font("Calibri", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label4.ImageAlign = ContentAlignment.MiddleLeft
        Label4.ImageIndex = 3
        Label4.Location = New Point(21, 110)
        Label4.Name = "Label4"
        Label4.Size = New Size(173, 17)
        Label4.TabIndex = 15
        Label4.Text = "4 eventi consec. decrescenti "
        Label4.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label5
        ' 
        Label5.Font = New Font("Calibri", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label5.ImageAlign = ContentAlignment.MiddleLeft
        Label5.ImageIndex = 3
        Label5.Location = New Point(21, 189)
        Label5.Name = "Label5"
        Label5.Size = New Size(173, 17)
        Label5.TabIndex = 16
        Label5.Text = "meno di 4 registrazioni"
        Label5.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label6
        ' 
        Label6.Font = New Font("Calibri", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label6.ImageAlign = ContentAlignment.MiddleLeft
        Label6.ImageIndex = 3
        Label6.Location = New Point(22, 149)
        Label6.Name = "Label6"
        Label6.Size = New Size(173, 17)
        Label6.TabIndex = 17
        Label6.Text = ">4 dati con andamento non definito "
        Label6.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label7
        ' 
        Label7.Font = New Font("Calibri", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label7.ImageAlign = ContentAlignment.MiddleLeft
        Label7.ImageIndex = 3
        Label7.Location = New Point(22, 222)
        Label7.Name = "Label7"
        Label7.Size = New Size(173, 17)
        Label7.TabIndex = 18
        Label7.Text = "nessun dato inserito"
        Label7.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Info
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(212, 292)
        Controls.Add(Button4)
        Controls.Add(GroupBox1)
        FormBorderStyle = FormBorderStyle.FixedToolWindow
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "Info"
        Text = "Info"
        TopMost = True
        GroupBox1.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label12 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents Button4 As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label7 As Label
End Class
