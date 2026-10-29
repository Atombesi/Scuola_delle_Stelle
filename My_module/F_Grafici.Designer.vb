<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F_Grafici
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F_Grafici))
        Button1 = New Button()
        GroupBox1 = New GroupBox()
        CheckBox1 = New CheckBox()
        ImageList1 = New ImageList(components)
        CheckBox3 = New CheckBox()
        CheckBox2 = New CheckBox()
        Button4 = New Button()
        Button3 = New Button()
        Button2 = New Button()
        Button6 = New Button()
        Button5 = New Button()
        ComboBox1 = New ComboBox()
        Label2 = New Label()
        Label3 = New Label()
        tp1 = New ToolTip(components)
        GroupBox1.SuspendLayout()
        SuspendLayout()
        ' 
        ' Button1
        ' 
        Button1.Font = New Font("Segoe UI", 9F)
        Button1.Location = New Point(7, 93)
        Button1.Name = "Button1"
        Button1.Size = New Size(114, 27)
        Button1.TabIndex = 0
        Button1.Text = "Voci Foglio"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(CheckBox1)
        GroupBox1.Controls.Add(CheckBox3)
        GroupBox1.Controls.Add(CheckBox2)
        GroupBox1.Controls.Add(Button4)
        GroupBox1.Controls.Add(Button3)
        GroupBox1.Controls.Add(Button2)
        GroupBox1.Controls.Add(Button1)
        GroupBox1.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        GroupBox1.Location = New Point(10, 63)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(166, 167)
        GroupBox1.TabIndex = 1
        GroupBox1.TabStop = False
        GroupBox1.Text = "Tipo di grafico"
        ' 
        ' CheckBox1
        ' 
        CheckBox1.Appearance = Appearance.Button
        CheckBox1.AutoSize = True
        CheckBox1.ImageIndex = 0
        CheckBox1.ImageList = ImageList1
        CheckBox1.Location = New Point(132, 60)
        CheckBox1.Name = "CheckBox1"
        CheckBox1.Size = New Size(28, 28)
        CheckBox1.TabIndex = 9
        CheckBox1.UseVisualStyleBackColor = True
        ' 
        ' ImageList1
        ' 
        ImageList1.ColorDepth = ColorDepth.Depth32Bit
        ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), ImageListStreamer)
        ImageList1.TransparentColor = Color.Transparent
        ImageList1.Images.SetKeyName(0, "icons8-lista-di-cose-da-fare-96.png")
        ImageList1.Images.SetKeyName(1, "icons8-elenco-puntato-48.png")
        ImageList1.Images.SetKeyName(2, "Emoji Objects-108.png")
        ' 
        ' CheckBox3
        ' 
        CheckBox3.Appearance = Appearance.Button
        CheckBox3.AutoSize = True
        CheckBox3.ImageIndex = 2
        CheckBox3.ImageList = ImageList1
        CheckBox3.Location = New Point(131, 92)
        CheckBox3.Name = "CheckBox3"
        CheckBox3.Size = New Size(28, 28)
        CheckBox3.TabIndex = 8
        CheckBox3.UseVisualStyleBackColor = True
        ' 
        ' CheckBox2
        ' 
        CheckBox2.Appearance = Appearance.Button
        CheckBox2.AutoSize = True
        CheckBox2.ImageIndex = 0
        CheckBox2.ImageList = ImageList1
        CheckBox2.Location = New Point(131, 125)
        CheckBox2.Name = "CheckBox2"
        CheckBox2.Size = New Size(28, 28)
        CheckBox2.TabIndex = 7
        CheckBox2.UseVisualStyleBackColor = True
        ' 
        ' Button4
        ' 
        Button4.Font = New Font("Segoe UI", 9F)
        Button4.Location = New Point(7, 60)
        Button4.Name = "Button4"
        Button4.Size = New Size(114, 27)
        Button4.TabIndex = 3
        Button4.Text = "Comportamentali"
        Button4.UseVisualStyleBackColor = True
        ' 
        ' Button3
        ' 
        Button3.Font = New Font("Segoe UI", 9F)
        Button3.Location = New Point(7, 24)
        Button3.Name = "Button3"
        Button3.Size = New Size(152, 27)
        Button3.TabIndex = 2
        Button3.Text = "Totale giornaliero (55)"
        tp1.SetToolTip(Button3, "Grafica ultimi 55 giorni")
        Button3.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.BackColor = SystemColors.Control
        Button2.Enabled = False
        Button2.Font = New Font("Segoe UI", 9F)
        Button2.Location = New Point(7, 126)
        Button2.Name = "Button2"
        Button2.Size = New Size(114, 27)
        Button2.TabIndex = 1
        Button2.Text = " Voci  (max 12)"
        tp1.SetToolTip(Button2, "Numero massimo di voci selezionabili (12)")
        Button2.UseVisualStyleBackColor = False
        ' 
        ' Button6
        ' 
        Button6.Font = New Font("Segoe UI", 9F)
        Button6.Location = New Point(14, 251)
        Button6.Name = "Button6"
        Button6.Size = New Size(151, 27)
        Button6.TabIndex = 51
        Button6.Text = "Esporta tutti i dati "
        Button6.UseVisualStyleBackColor = True
        ' 
        ' Button5
        ' 
        Button5.BackColor = Color.DimGray
        Button5.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Button5.ForeColor = Color.Snow
        Button5.Location = New Point(122, 332)
        Button5.Name = "Button5"
        Button5.Size = New Size(57, 27)
        Button5.TabIndex = 45
        Button5.Text = "Close"
        Button5.UseVisualStyleBackColor = False
        ' 
        ' ComboBox1
        ' 
        ComboBox1.FormattingEnabled = True
        ComboBox1.Location = New Point(5, 5)
        ComboBox1.Name = "ComboBox1"
        ComboBox1.Size = New Size(175, 23)
        ComboBox1.TabIndex = 47
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(20, 36)
        Label2.Name = "Label2"
        Label2.Size = New Size(100, 15)
        Label2.TabIndex = 48
        Label2.Text = "n. fogli compilati:"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label3.Location = New Point(130, 36)
        Label3.Name = "Label3"
        Label3.Size = New Size(14, 15)
        Label3.TabIndex = 49
        Label3.Text = "0"
        ' 
        ' F_Grafici
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        CancelButton = Button5
        ClientSize = New Size(187, 366)
        ControlBox = False
        Controls.Add(Button6)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(ComboBox1)
        Controls.Add(Button5)
        Controls.Add(GroupBox1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Name = "F_Grafici"
        StartPosition = FormStartPosition.CenterScreen
        Text = "F_Grafici"
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Button2 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents Button6 As Button
    Friend WithEvents tp1 As ToolTip
    Friend WithEvents CheckBox2 As CheckBox
    Friend WithEvents CheckBox3 As CheckBox
    Friend WithEvents CheckBox1 As CheckBox
End Class
