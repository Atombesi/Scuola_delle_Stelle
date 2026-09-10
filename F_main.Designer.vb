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
        Button5 = New Button()
        Button6 = New Button()
        ImageList1 = New ImageList(components)
        Label1 = New Label()
        SuspendLayout()
        ' 
        ' Button1
        ' 
        Button1.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Button1.Location = New Point(103, 6)
        Button1.Name = "Button1"
        Button1.Size = New Size(90, 41)
        Button1.TabIndex = 0
        Button1.Text = "Compilazione Schede"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Button2.Location = New Point(5, 6)
        Button2.Name = "Button2"
        Button2.Size = New Size(91, 41)
        Button2.TabIndex = 1
        Button2.Text = "Schede Giornaliere"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Button3
        ' 
        Button3.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Button3.Location = New Point(5, 53)
        Button3.Name = "Button3"
        Button3.Size = New Size(91, 41)
        Button3.TabIndex = 9
        Button3.Text = "Anagrafiche"
        Button3.UseVisualStyleBackColor = True
        ' 
        ' Button4
        ' 
        Button4.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Button4.Location = New Point(103, 53)
        Button4.Name = "Button4"
        Button4.Size = New Size(90, 41)
        Button4.TabIndex = 10
        Button4.Text = "Stampe"
        Button4.UseVisualStyleBackColor = True
        ' 
        ' Button5
        ' 
        Button5.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Button5.Location = New Point(5, 100)
        Button5.Name = "Button5"
        Button5.Size = New Size(91, 41)
        Button5.TabIndex = 11
        Button5.Text = "Impostazioni"
        Button5.UseVisualStyleBackColor = True
        ' 
        ' Button6
        ' 
        Button6.BackColor = Color.DimGray
        Button6.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Button6.Location = New Point(5, 147)
        Button6.Name = "Button6"
        Button6.Size = New Size(90, 33)
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
        ' 
        ' Label1
        ' 
        Label1.ImageIndex = 0
        Label1.ImageList = ImageList1
        Label1.Location = New Point(152, 150)
        Label1.Name = "Label1"
        Label1.Size = New Size(41, 29)
        Label1.TabIndex = 13
        Label1.Text = " "
        ' 
        ' F_main
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        CancelButton = Button6
        ClientSize = New Size(199, 184)
        Controls.Add(Label1)
        Controls.Add(Button6)
        Controls.Add(Button5)
        Controls.Add(Button4)
        Controls.Add(Button3)
        Controls.Add(Button2)
        Controls.Add(Button1)
        FormBorderStyle = FormBorderStyle.FixedToolWindow
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "F_main"
        Text = "Scuola delle Stelle"
        ResumeLayout(False)
    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents Button6 As Button
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents Label1 As Label

End Class
