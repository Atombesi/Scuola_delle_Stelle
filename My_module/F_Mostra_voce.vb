Public Class F_Mostra_voce
    Public tipo_tabella As String
    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub
    Public Sub Main(tabella As String, Optional modo As String = "LIST")
        If modo <> "LIST" Then
            Panel3.Visible = True
            Label11.Text = tabella
            Label10.Text = ""
        End If
        tipo_tabella = tabella
        Call Setmode("VIEW")
        Call carica_voci
        Me.ShowDialog()
    End Sub
    Sub Setmode(modo)
        If modo = "VIEW" Then
            TextBox2.ReadOnly = True
            TextBox3.ReadOnly = True
            TextBox4.ReadOnly = True
            TextBox5.ReadOnly = True
            CheckBox3.Enabled = False
            Panel2.Visible = False
        Else
            TextBox2.ReadOnly = False
            TextBox3.ReadOnly = False
            TextBox4.ReadOnly = False
            TextBox5.ReadOnly = False
            CheckBox3.Enabled = True
            Panel2.Visible = True
        End If
    End Sub
    Sub carica_voci()
        If tipo_tabella = "VOCI_PERSONALI" Then
            Label9.Text = Gbl_Class_Voci_Personali.ID.ToString
            Label8.Text = Gbl_Class_Voci_Personali.ELCAR + "." + Gbl_Class_Voci_Personali.ID_SUB.ToString
            TextBox2.Text = Gbl_Class_Voci_Personali.ID_SUB
            TextBox3.Text = Gbl_Class_Voci_Personali.ELCAR
            TextBox4.Text = Gbl_Class_Voci_Personali.Testo
            Label13.Text = "(" + Len(TextBox4.Text).ToString + " Char)"
            TextBox5.Text = Gbl_Class_Voci_Personali.Testo_Lungo
            If Gbl_Class_Voci_Personali.Deleted Then
                CheckBox3.Checked = True
            Else
                CheckBox3.Checked = False
            End If
            Label10.Text = Gbl_DB_BAMBINO
            Label11.Text = "VOCI_PERSONALI"
            Exit Sub
        End If

        If tipo_tabella = "SUB2" Then
            Label9.Text = Gbl_Class_Voci_sub2.ID.ToString
            Label8.Text = Gbl_Class_Voci_sub2.ELCAR + "." + Gbl_Class_Voci_sub2.ID_SUB.ToString
            TextBox2.Text = Gbl_Class_Voci_sub2.ID_SUB
            TextBox3.Text = Gbl_Class_Voci_sub2.ELCAR
            TextBox4.Text = Gbl_Class_Voci_sub2.Testo
            Label13.Text = "(" + Len(TextBox4.Text).ToString + " Char)"
            TextBox5.Text = Gbl_Class_Voci_sub2.Testo_Lungo
            If Gbl_Class_Voci_sub2.Deleted Then
                CheckBox3.Checked = True
            Else
                CheckBox3.Checked = False
            End If
            Label10.Text = "SCUOLADELLESTELLE"
            Label11.Text = "VOCI_AGGIUNTIVE"
            Exit Sub
        End If

        If tipo_tabella = "SUB" Then
            Label9.Text = Gbl_Class_Voci_sub.ID.ToString
            Label8.Text = Gbl_Class_Voci_sub.ELCAR + "." + Gbl_Class_Voci_sub.ID_SUB.ToString
            TextBox2.Text = Gbl_Class_Voci_sub.ID_SUB
            TextBox3.Text = Gbl_Class_Voci_sub.ELCAR
            TextBox4.Text = Gbl_Class_Voci_sub.Testo
            Label13.Text = "(" + Len(TextBox4.Text).ToString + " Char)"
            TextBox5.Text = Gbl_Class_Voci_sub.Testo_Lungo
            If Gbl_Class_Voci_sub.Deleted Then
                CheckBox3.Checked = True
            Else
                CheckBox3.Checked = False
            End If
            Label10.Text = "SCUOLADELLESTELLE"
            Label11.Text = "VOCI_ELCAR (4 livello)"
            Exit Sub
        End If

    End Sub
    Private Sub F_Mostra_voce_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Call Setmode("EDIT")
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Call Setmode("VIEW")
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Hide()

    End Sub
End Class