<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class F_main
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
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

    'Descrizione: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F_main))
        Button1 = New Button()
        Button2 = New Button()
        Button3 = New Button()
        Button4 = New Button()
        Button6 = New Button()
        ImageList1 = New ImageList(components)
        Label1 = New Label()
        CheckBox1 = New CheckBox()
        P_schede = New Panel()
        P_Stampe = New Panel()
        Button7 = New Button()
        P_liste = New Panel()
        Button10 = New Button()
        Button9 = New Button()
        Button8 = New Button()
        CheckBox2 = New CheckBox()
        P_Setting = New Panel()
        Button11 = New Button()
        Button12 = New Button()
        Button13 = New Button()
        Button14 = New Button()
        CheckBox3 = New CheckBox()
        CheckBox4 = New CheckBox()
        TP1 = New ToolTip(components)
        PctNotifica = New Label()
        P_schede.SuspendLayout()
        P_Stampe.SuspendLayout()
        P_liste.SuspendLayout()
        P_Setting.SuspendLayout()
        SuspendLayout()
        ' 
        ' Button1
        ' 
        Button1.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Button1.Location = New Point(5, 48)
        Button1.Name = "Button1"
        Button1.Size = New Size(108, 41)
        Button1.TabIndex = 0
        Button1.Text = "Compilazione Schede"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Button2.Location = New Point(5, 3)
        Button2.Name = "Button2"
        Button2.Size = New Size(109, 41)
        Button2.TabIndex = 1
        Button2.Text = "Schede Giornaliere"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Button3
        ' 
        Button3.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Button3.Location = New Point(5, 32)
        Button3.Name = "Button3"
        Button3.Size = New Size(109, 25)
        Button3.TabIndex = 9
        Button3.Text = "Anagrafiche"
        Button3.UseVisualStyleBackColor = True
        ' 
        ' Button4
        ' 
        Button4.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Button4.Location = New Point(7, 5)
        Button4.Name = "Button4"
        Button4.Size = New Size(107, 25)
        Button4.TabIndex = 10
        Button4.Text = "Stampa Fogli"
        Button4.UseVisualStyleBackColor = True
        ' 
        ' Button6
        ' 
        Button6.BackColor = Color.DimGray
        Button6.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Button6.Location = New Point(64, 306)
        Button6.Name = "Button6"
        Button6.Size = New Size(59, 36)
        Button6.TabIndex = 12
        Button6.Text = "ESCI"
        Button6.UseVisualStyleBackColor = False
        ' 
        ' ImageList1
        ' 
        ImageList1.ColorDepth = ColorDepth.Depth32Bit
        ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), ImageListStreamer)
        ImageList1.TransparentColor = Color.Transparent
        ImageList1.Images.SetKeyName(0, "icons8-informazioni-144.png")
        ImageList1.Images.SetKeyName(1, "icons8-wi-fi-connesso-96.png")
        ImageList1.Images.SetKeyName(2, "icons8-wi-fi-disconnected-96.png")
        ImageList1.Images.SetKeyName(3, "icons8-wi-fi-off-96.png")
        ImageList1.Images.SetKeyName(4, "CP03.png")
        ImageList1.Images.SetKeyName(5, "Emoji Objects-45.png")
        ImageList1.Images.SetKeyName(6, "Emoji Symbols-201.png")
        ImageList1.Images.SetKeyName(7, "Client-1.png")
        ImageList1.Images.SetKeyName(8, "Client-2.png")
        ImageList1.Images.SetKeyName(9, "Client-3.png")
        ImageList1.Images.SetKeyName(10, "Client-4.png")
        ' 
        ' Label1
        ' 
        Label1.ImageIndex = 0
        Label1.ImageList = ImageList1
        Label1.Location = New Point(5, 251)
        Label1.Name = "Label1"
        Label1.Size = New Size(29, 29)
        Label1.TabIndex = 13
        Label1.Text = " "
        ' 
        ' CheckBox1
        ' 
        CheckBox1.AutoSize = True
        CheckBox1.Location = New Point(7, 316)
        CheckBox1.Name = "CheckBox1"
        CheckBox1.Size = New Size(50, 19)
        CheckBox1.TabIndex = 14
        CheckBox1.Text = "Dark"
        CheckBox1.UseVisualStyleBackColor = True
        ' 
        ' P_schede
        ' 
        P_schede.Controls.Add(Button2)
        P_schede.Controls.Add(Button1)
        P_schede.Location = New Point(4, 7)
        P_schede.Name = "P_schede"
        P_schede.Size = New Size(120, 92)
        P_schede.TabIndex = 16
        ' 
        ' P_Stampe
        ' 
        P_Stampe.BackColor = Color.DeepSkyBlue
        P_Stampe.Controls.Add(Button7)
        P_Stampe.Controls.Add(Button4)
        P_Stampe.Location = New Point(4, 105)
        P_Stampe.Name = "P_Stampe"
        P_Stampe.Size = New Size(120, 64)
        P_Stampe.TabIndex = 17
        ' 
        ' Button7
        ' 
        Button7.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Button7.Location = New Point(7, 34)
        Button7.Name = "Button7"
        Button7.Size = New Size(107, 25)
        Button7.TabIndex = 11
        Button7.Text = "Stampa Grafici"
        Button7.UseVisualStyleBackColor = True
        ' 
        ' P_liste
        ' 
        P_liste.BackColor = Color.MediumAquamarine
        P_liste.Controls.Add(Button10)
        P_liste.Controls.Add(Button9)
        P_liste.Controls.Add(Button8)
        P_liste.Controls.Add(Button3)
        P_liste.Location = New Point(130, 6)
        P_liste.Name = "P_liste"
        P_liste.Size = New Size(120, 161)
        P_liste.TabIndex = 18
        ' 
        ' Button10
        ' 
        Button10.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Button10.Location = New Point(6, 91)
        Button10.Name = "Button10"
        Button10.Size = New Size(109, 25)
        Button10.TabIndex = 12
        Button10.Text = "Voci CP"
        Button10.UseVisualStyleBackColor = True
        ' 
        ' Button9
        ' 
        Button9.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Button9.Location = New Point(6, 62)
        Button9.Name = "Button9"
        Button9.Size = New Size(109, 25)
        Button9.TabIndex = 11
        Button9.Text = "Voci routine"
        Button9.UseVisualStyleBackColor = True
        ' 
        ' Button8
        ' 
        Button8.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Button8.Location = New Point(5, 2)
        Button8.Name = "Button8"
        Button8.Size = New Size(109, 25)
        Button8.TabIndex = 10
        Button8.Text = "Gestione fogli"
        Button8.UseVisualStyleBackColor = True
        ' 
        ' CheckBox2
        ' 
        CheckBox2.Appearance = Appearance.Button
        CheckBox2.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CheckBox2.Location = New Point(9, 177)
        CheckBox2.Name = "CheckBox2"
        CheckBox2.Size = New Size(112, 27)
        CheckBox2.TabIndex = 19
        CheckBox2.Text = "Gestione Liste"
        CheckBox2.TextAlign = ContentAlignment.MiddleCenter
        CheckBox2.UseVisualStyleBackColor = True
        ' 
        ' P_Setting
        ' 
        P_Setting.BackColor = Color.LemonChiffon
        P_Setting.Controls.Add(Button11)
        P_Setting.Controls.Add(Button12)
        P_Setting.Controls.Add(Button13)
        P_Setting.Controls.Add(Button14)
        P_Setting.Location = New Point(246, 7)
        P_Setting.Name = "P_Setting"
        P_Setting.Size = New Size(120, 161)
        P_Setting.TabIndex = 20
        ' 
        ' Button11
        ' 
        Button11.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Button11.Location = New Point(5, 90)
        Button11.Name = "Button11"
        Button11.Size = New Size(108, 25)
        Button11.TabIndex = 12
        Button11.Text = "Voci CP"
        Button11.UseVisualStyleBackColor = True
        ' 
        ' Button12
        ' 
        Button12.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Button12.Location = New Point(5, 61)
        Button12.Name = "Button12"
        Button12.Size = New Size(108, 25)
        Button12.TabIndex = 11
        Button12.Text = "Network"
        Button12.UseVisualStyleBackColor = True
        ' 
        ' Button13
        ' 
        Button13.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Button13.Location = New Point(4, 2)
        Button13.Name = "Button13"
        Button13.Size = New Size(108, 25)
        Button13.TabIndex = 10
        Button13.Text = "Opend File Ini"
        Button13.UseVisualStyleBackColor = True
        ' 
        ' Button14
        ' 
        Button14.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Button14.Location = New Point(4, 31)
        Button14.Name = "Button14"
        Button14.Size = New Size(108, 25)
        Button14.TabIndex = 9
        Button14.Text = "Open file log"
        Button14.UseVisualStyleBackColor = True
        ' 
        ' CheckBox3
        ' 
        CheckBox3.Appearance = Appearance.Button
        CheckBox3.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CheckBox3.Location = New Point(8, 211)
        CheckBox3.Name = "CheckBox3"
        CheckBox3.Size = New Size(110, 27)
        CheckBox3.TabIndex = 21
        CheckBox3.Text = "Impostazioni"
        CheckBox3.TextAlign = ContentAlignment.MiddleCenter
        CheckBox3.UseVisualStyleBackColor = True
        ' 
        ' CheckBox4
        ' 
        CheckBox4.Appearance = Appearance.Button
        CheckBox4.BackColor = Color.Salmon
        CheckBox4.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CheckBox4.ImageAlign = ContentAlignment.BottomCenter
        CheckBox4.ImageIndex = 2
        CheckBox4.ImageList = ImageList1
        CheckBox4.Location = New Point(65, 251)
        CheckBox4.Name = "CheckBox4"
        CheckBox4.Size = New Size(32, 27)
        CheckBox4.TabIndex = 22
        CheckBox4.TextAlign = ContentAlignment.MiddleCenter
        CheckBox4.UseVisualStyleBackColor = False
        ' 
        ' PctNotifica
        ' 
        PctNotifica.ImageAlign = ContentAlignment.MiddleLeft
        PctNotifica.ImageIndex = 5
        PctNotifica.ImageList = ImageList1
        PctNotifica.Location = New Point(96, 252)
        PctNotifica.Name = "PctNotifica"
        PctNotifica.Size = New Size(24, 27)
        PctNotifica.TabIndex = 23
        PctNotifica.Text = "    "
        TP1.SetToolTip(PctNotifica, "Richiesta di Associazione")
        PctNotifica.Visible = False
        ' 
        ' F_main
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        CancelButton = Button6
        ClientSize = New Size(129, 347)
        Controls.Add(PctNotifica)
        Controls.Add(CheckBox4)
        Controls.Add(CheckBox3)
        Controls.Add(P_Setting)
        Controls.Add(CheckBox2)
        Controls.Add(P_liste)
        Controls.Add(P_Stampe)
        Controls.Add(P_schede)
        Controls.Add(CheckBox1)
        Controls.Add(Label1)
        Controls.Add(Button6)
        FormBorderStyle = FormBorderStyle.FixedToolWindow
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "F_main"
        Text = "Scuola delle Stelle"
        P_schede.ResumeLayout(False)
        P_Stampe.ResumeLayout(False)
        P_liste.ResumeLayout(False)
        P_Setting.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Button6 As Button
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents Label1 As Label
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents P_schede As Panel
    Friend WithEvents P_Stampe As Panel
    Friend WithEvents Button7 As Button
    Friend WithEvents P_liste As Panel
    Friend WithEvents Button8 As Button
    Friend WithEvents Button10 As Button
    Friend WithEvents Button9 As Button
    Friend WithEvents CheckBox2 As CheckBox
    Friend WithEvents P_Setting As Panel
    Friend WithEvents Button11 As Button
    Friend WithEvents Button12 As Button
    Friend WithEvents Button13 As Button
    Friend WithEvents Button14 As Button
    Friend WithEvents CheckBox3 As CheckBox
    Friend WithEvents CheckBox4 As CheckBox
    Friend WithEvents TP1 As ToolTip
    Friend WithEvents PctNotifica As Label

End Class
