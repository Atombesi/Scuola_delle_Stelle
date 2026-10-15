<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F_Elenco_Comportamenti
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F_Elenco_Comportamenti))
        B_Add = New Button()
        B_EditTesto = New Button()
        Button1 = New Button()
        CheckBox1 = New CheckBox()
        Label2 = New Label()
        ComboBox1 = New ComboBox()
        B_Disattiva = New Button()
        Panel1 = New Panel()
        Button2 = New Button()
        Label1 = New Label()
        ImageList1 = New ImageList(components)
        Label3 = New Label()
        Label4 = New Label()
        Label5 = New Label()
        Panel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' B_Add
        ' 
        B_Add.Location = New Point(3, 3)
        B_Add.Name = "B_Add"
        B_Add.Size = New Size(75, 28)
        B_Add.TabIndex = 27
        B_Add.Text = "Aggiungi"
        B_Add.UseVisualStyleBackColor = True
        ' 
        ' B_EditTesto
        ' 
        B_EditTesto.Location = New Point(85, 3)
        B_EditTesto.Name = "B_EditTesto"
        B_EditTesto.Size = New Size(75, 28)
        B_EditTesto.TabIndex = 24
        B_EditTesto.Text = "Modifica"
        B_EditTesto.UseVisualStyleBackColor = True
        ' 
        ' Button1
        ' 
        Button1.BackColor = Color.DimGray
        Button1.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Button1.ForeColor = Color.Snow
        Button1.Location = New Point(302, 278)
        Button1.Name = "Button1"
        Button1.Size = New Size(75, 31)
        Button1.TabIndex = 23
        Button1.Text = "Close"
        Button1.UseVisualStyleBackColor = False
        ' 
        ' CheckBox1
        ' 
        CheckBox1.AutoSize = True
        CheckBox1.Checked = True
        CheckBox1.CheckState = CheckState.Checked
        CheckBox1.Location = New Point(301, 9)
        CheckBox1.Name = "CheckBox1"
        CheckBox1.Size = New Size(78, 19)
        CheckBox1.TabIndex = 21
        CheckBox1.Text = "Solo attivi"
        CheckBox1.UseVisualStyleBackColor = True
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(12, 10)
        Label2.Name = "Label2"
        Label2.Size = New Size(130, 15)
        Label2.TabIndex = 20
        Label2.Text = "Elenco Comportamenti"
        ' 
        ' ComboBox1
        ' 
        ComboBox1.DropDownStyle = ComboBoxStyle.Simple
        ComboBox1.FormattingEnabled = True
        ComboBox1.Location = New Point(7, 32)
        ComboBox1.Name = "ComboBox1"
        ComboBox1.Size = New Size(378, 173)
        ComboBox1.TabIndex = 19
        ' 
        ' B_Disattiva
        ' 
        B_Disattiva.Location = New Point(166, 3)
        B_Disattiva.Name = "B_Disattiva"
        B_Disattiva.Size = New Size(75, 28)
        B_Disattiva.TabIndex = 28
        B_Disattiva.Text = "Disattiva"
        B_Disattiva.UseVisualStyleBackColor = True
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(B_Add)
        Panel1.Controls.Add(B_EditTesto)
        Panel1.Controls.Add(B_Disattiva)
        Panel1.Location = New Point(70, 237)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(245, 34)
        Panel1.TabIndex = 29
        Panel1.Visible = False
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(7, 278)
        Button2.Name = "Button2"
        Button2.Size = New Size(131, 28)
        Button2.TabIndex = 30
        Button2.Text = "Seleziona"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.BorderStyle = BorderStyle.Fixed3D
        Label1.ImageList = ImageList1
        Label1.Location = New Point(34, 202)
        Label1.Name = "Label1"
        Label1.Size = New Size(347, 27)
        Label1.TabIndex = 32
        Label1.Text = "-"
        Label1.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' ImageList1
        ' 
        ImageList1.ColorDepth = ColorDepth.Depth32Bit
        ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), ImageListStreamer)
        ImageList1.TransparentColor = Color.Transparent
        ImageList1.Images.SetKeyName(0, "Emoji Symbols-202.png")
        ImageList1.Images.SetKeyName(1, "Emoji Symbols-201.png")
        ' 
        ' Label3
        ' 
        Label3.ImageIndex = 0
        Label3.ImageList = ImageList1
        Label3.Location = New Point(9, 207)
        Label3.Name = "Label3"
        Label3.Size = New Size(21, 16)
        Label3.TabIndex = 33
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(167, 9)
        Label4.Name = "Label4"
        Label4.Size = New Size(47, 15)
        Label4.TabIndex = 34
        Label4.Text = "N. voci:"
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(220, 9)
        Label5.Name = "Label5"
        Label5.Size = New Size(13, 15)
        Label5.TabIndex = 35
        Label5.Text = "0"
        ' 
        ' F_Comportamenti
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(390, 312)
        ControlBox = False
        Controls.Add(Label5)
        Controls.Add(Label4)
        Controls.Add(Label3)
        Controls.Add(Label1)
        Controls.Add(Button2)
        Controls.Add(Panel1)
        Controls.Add(Button1)
        Controls.Add(CheckBox1)
        Controls.Add(Label2)
        Controls.Add(ComboBox1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Name = "F_Elenco_Comportamenti"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Elenco Comportamenti"
        Panel1.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents B_Add As Button
    Friend WithEvents B_EditTesto As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents Label2 As Label
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents B_Disattiva As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Button2 As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
End Class
