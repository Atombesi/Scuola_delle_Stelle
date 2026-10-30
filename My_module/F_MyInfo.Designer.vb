<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F_MyInfo
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F_MyInfo))
        Label3 = New Label()
        LinkMail = New LinkLabel()
        ImageList1 = New ImageList(components)
        Label5 = New Label()
        Label6 = New Label()
        LinkWP = New LinkLabel()
        Label7 = New Label()
        LinkGitHub = New LinkLabel()
        Button4 = New Button()
        Label4 = New Label()
        LinkLabel1 = New LinkLabel()
        SuspendLayout()
        ' 
        ' Label3
        ' 
        Label3.BackColor = Color.Transparent
        Label3.Location = New Point(545, 9)
        Label3.Name = "Label3"
        Label3.Size = New Size(65, 38)
        Label3.TabIndex = 2
        ' 
        ' LinkMail
        ' 
        LinkMail.AutoSize = True
        LinkMail.BackColor = Color.Transparent
        LinkMail.Font = New Font("Segoe UI Semibold", 14.25F, FontStyle.Bold Or FontStyle.Italic, GraphicsUnit.Point, CByte(0))
        LinkMail.ForeColor = Color.LemonChiffon
        LinkMail.LinkColor = Color.FromArgb(CByte(255), CByte(255), CByte(192))
        LinkMail.Location = New Point(219, 105)
        LinkMail.Name = "LinkMail"
        LinkMail.Size = New Size(231, 25)
        LinkMail.TabIndex = 5
        LinkMail.TabStop = True
        LinkMail.Text = "BRZ.Modena@gmail.com"
        ' 
        ' ImageList1
        ' 
        ImageList1.ColorDepth = ColorDepth.Depth32Bit
        ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), ImageListStreamer)
        ImageList1.TransparentColor = Color.Transparent
        ImageList1.Images.SetKeyName(0, "Emoji Orte-91.png")
        ImageList1.Images.SetKeyName(1, "Emoji Objects-89.png")
        ImageList1.Images.SetKeyName(2, "Emoji Orte-90.png")
        ImageList1.Images.SetKeyName(3, "icons8-github-96.png")
        ImageList1.Images.SetKeyName(4, "icons8-internet-96.png")
        ' 
        ' Label5
        ' 
        Label5.BackColor = Color.Transparent
        Label5.Font = New Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label5.ForeColor = Color.White
        Label5.ImageIndex = 1
        Label5.ImageList = ImageList1
        Label5.Location = New Point(168, 100)
        Label5.Name = "Label5"
        Label5.Size = New Size(47, 38)
        Label5.TabIndex = 6
        ' 
        ' Label6
        ' 
        Label6.BackColor = Color.Transparent
        Label6.Font = New Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label6.ForeColor = Color.White
        Label6.ImageIndex = 4
        Label6.ImageList = ImageList1
        Label6.Location = New Point(168, 144)
        Label6.Name = "Label6"
        Label6.Size = New Size(47, 38)
        Label6.TabIndex = 8
        ' 
        ' LinkWP
        ' 
        LinkWP.AutoSize = True
        LinkWP.BackColor = Color.Transparent
        LinkWP.Font = New Font("Segoe UI Semibold", 14.25F, FontStyle.Bold Or FontStyle.Italic, GraphicsUnit.Point, CByte(0))
        LinkWP.ForeColor = Color.LemonChiffon
        LinkWP.LinkColor = Color.FromArgb(CByte(255), CByte(255), CByte(192))
        LinkWP.Location = New Point(219, 149)
        LinkWP.Name = "LinkWP"
        LinkWP.Size = New Size(282, 25)
        LinkWP.TabIndex = 7
        LinkWP.TabStop = True
        LinkWP.Text = "AndreaTombesi.wordpress.com"
        ' 
        ' Label7
        ' 
        Label7.BackColor = Color.Transparent
        Label7.Font = New Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label7.ForeColor = Color.White
        Label7.ImageIndex = 3
        Label7.ImageList = ImageList1
        Label7.Location = New Point(168, 190)
        Label7.Name = "Label7"
        Label7.Size = New Size(47, 38)
        Label7.TabIndex = 10
        ' 
        ' LinkGitHub
        ' 
        LinkGitHub.AutoSize = True
        LinkGitHub.BackColor = Color.Transparent
        LinkGitHub.Font = New Font("Segoe UI Semibold", 14.25F, FontStyle.Bold Or FontStyle.Italic, GraphicsUnit.Point, CByte(0))
        LinkGitHub.ForeColor = Color.LemonChiffon
        LinkGitHub.LinkColor = Color.FromArgb(CByte(255), CByte(255), CByte(192))
        LinkGitHub.Location = New Point(219, 195)
        LinkGitHub.Name = "LinkGitHub"
        LinkGitHub.Size = New Size(160, 25)
        LinkGitHub.TabIndex = 9
        LinkGitHub.TabStop = True
        LinkGitHub.Text = "Github/Atombesi"
        ' 
        ' Button4
        ' 
        Button4.BackColor = Color.DimGray
        Button4.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Button4.ForeColor = Color.Snow
        Button4.Location = New Point(516, 317)
        Button4.Name = "Button4"
        Button4.Size = New Size(66, 35)
        Button4.TabIndex = 50
        Button4.Text = "Close"
        Button4.UseVisualStyleBackColor = False
        ' 
        ' Label4
        ' 
        Label4.BackColor = Color.Transparent
        Label4.Font = New Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label4.ForeColor = Color.White
        Label4.ImageIndex = 2
        Label4.ImageList = ImageList1
        Label4.Location = New Point(168, 233)
        Label4.Name = "Label4"
        Label4.Size = New Size(47, 38)
        Label4.TabIndex = 52
        ' 
        ' LinkLabel1
        ' 
        LinkLabel1.AutoSize = True
        LinkLabel1.BackColor = Color.Transparent
        LinkLabel1.Font = New Font("Segoe UI Semibold", 14.25F, FontStyle.Bold Or FontStyle.Italic, GraphicsUnit.Point, CByte(0))
        LinkLabel1.ForeColor = Color.LemonChiffon
        LinkLabel1.LinkColor = Color.FromArgb(CByte(255), CByte(255), CByte(192))
        LinkLabel1.Location = New Point(219, 238)
        LinkLabel1.Name = "LinkLabel1"
        LinkLabel1.Size = New Size(138, 25)
        LinkLabel1.TabIndex = 51
        LinkLabel1.TabStop = True
        LinkLabel1.Text = "Modena (Italy)"
        ' 
        ' F_MyInfo
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), Image)
        BackgroundImageLayout = ImageLayout.Stretch
        ClientSize = New Size(594, 362)
        ControlBox = False
        Controls.Add(Label4)
        Controls.Add(LinkLabel1)
        Controls.Add(Button4)
        Controls.Add(Label7)
        Controls.Add(LinkGitHub)
        Controls.Add(Label6)
        Controls.Add(LinkWP)
        Controls.Add(Label5)
        Controls.Add(LinkMail)
        Controls.Add(Label3)
        FormBorderStyle = FormBorderStyle.FixedSingle
        Name = "F_MyInfo"
        StartPosition = FormStartPosition.CenterScreen
        Text = "F_MyInfo"
        TopMost = True
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents Label3 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label4 As Label
    Friend WithEvents LinkMail As LinkLabel
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents LinkWP As LinkLabel
    Friend WithEvents Label7 As Label
    Friend WithEvents LinkGitHub As LinkLabel
    Friend WithEvents Button4 As Button
    Friend WithEvents LinkLabel1 As LinkLabel
End Class
