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
        Label6 = New Label()
        ImageList1 = New ImageList(components)
        Label5 = New Label()
        Label4 = New Label()
        Button4 = New Button()
        Button3 = New Button()
        Button2 = New Button()
        Button6 = New Button()
        Button5 = New Button()
        ComboBox1 = New ComboBox()
        Label2 = New Label()
        Label3 = New Label()
        CheckBox1 = New CheckBox()
        tp1 = New ToolTip(components)
        GroupBox1.SuspendLayout()
        SuspendLayout()
        ' 
        ' Button1
        ' 
        Button1.Font = New Font("Segoe UI", 9F)
        Button1.Location = New Point(6, 93)
        Button1.Name = "Button1"
        Button1.Size = New Size(109, 27)
        Button1.TabIndex = 0
        Button1.Text = "Voci Foglio"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(Label6)
        GroupBox1.Controls.Add(Label5)
        GroupBox1.Controls.Add(Label4)
        GroupBox1.Controls.Add(Button4)
        GroupBox1.Controls.Add(Button3)
        GroupBox1.Controls.Add(Button2)
        GroupBox1.Controls.Add(Button1)
        GroupBox1.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        GroupBox1.Location = New Point(13, 63)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(160, 167)
        GroupBox1.TabIndex = 1
        GroupBox1.TabStop = False
        GroupBox1.Text = "Tipo di grafico"
        ' 
        ' Label6
        ' 
        Label6.ImageIndex = 0
        Label6.ImageList = ImageList1
        Label6.Location = New Point(128, 60)
        Label6.Name = "Label6"
        Label6.Size = New Size(27, 30)
        Label6.TabIndex = 6
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
        ' Label5
        ' 
        Label5.ImageIndex = 0
        Label5.ImageList = ImageList1
        Label5.Location = New Point(128, 123)
        Label5.Name = "Label5"
        Label5.Size = New Size(27, 30)
        Label5.TabIndex = 5
        ' 
        ' Label4
        ' 
        Label4.ImageIndex = 2
        Label4.ImageList = ImageList1
        Label4.Location = New Point(128, 92)
        Label4.Name = "Label4"
        Label4.Size = New Size(27, 30)
        Label4.TabIndex = 4
        ' 
        ' Button4
        ' 
        Button4.Font = New Font("Segoe UI", 9F)
        Button4.Location = New Point(6, 60)
        Button4.Name = "Button4"
        Button4.Size = New Size(109, 27)
        Button4.TabIndex = 3
        Button4.Text = "comportamentali"
        Button4.UseVisualStyleBackColor = True
        ' 
        ' Button3
        ' 
        Button3.Font = New Font("Segoe UI", 9F)
        Button3.Location = New Point(6, 24)
        Button3.Name = "Button3"
        Button3.Size = New Size(109, 27)
        Button3.TabIndex = 2
        Button3.Text = "totali giornaliero"
        tp1.SetToolTip(Button3, "Grafica ultim 55 giorni")
        Button3.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Font = New Font("Segoe UI", 9F)
        Button2.Location = New Point(6, 126)
        Button2.Name = "Button2"
        Button2.Size = New Size(109, 27)
        Button2.TabIndex = 1
        Button2.Text = "8 Voci ELCAR"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Button6
        ' 
        Button6.Font = New Font("Segoe UI", 9F)
        Button6.Location = New Point(19, 306)
        Button6.Name = "Button6"
        Button6.Size = New Size(138, 27)
        Button6.TabIndex = 51
        Button6.Text = "Esporta tutti i dati "
        Button6.UseVisualStyleBackColor = True
        ' 
        ' Button5
        ' 
        Button5.BackColor = Color.DimGray
        Button5.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Button5.ForeColor = Color.Snow
        Button5.Location = New Point(122, 369)
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
        ' CheckBox1
        ' 
        CheckBox1.AutoSize = True
        CheckBox1.Checked = True
        CheckBox1.CheckState = CheckState.Checked
        CheckBox1.Location = New Point(5, 373)
        CheckBox1.Name = "CheckBox1"
        CheckBox1.Size = New Size(113, 19)
        CheckBox1.TabIndex = 50
        CheckBox1.Text = "Apri documento"
        CheckBox1.UseVisualStyleBackColor = True
        ' 
        ' F_Grafici
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        CancelButton = Button5
        ClientSize = New Size(187, 404)
        ControlBox = False
        Controls.Add(Button6)
        Controls.Add(CheckBox1)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(ComboBox1)
        Controls.Add(Button5)
        Controls.Add(GroupBox1)
        Name = "F_Grafici"
        StartPosition = FormStartPosition.CenterScreen
        Text = "F_Grafici"
        GroupBox1.ResumeLayout(False)
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
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents Label4 As Label
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Button6 As Button
    Friend WithEvents tp1 As ToolTip
End Class
