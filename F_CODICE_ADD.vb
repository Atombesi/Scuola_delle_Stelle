Public Class F_CODICE_ADD
    Public Nuova_SUB2 As Class_ELCAR_VOCI_SUB2
    Public Nuovo_Codice_Bambino As Class_ELCAR_VOCI_PERSONALI
    Public Codice_Genitore As String
    Public dB_Bambino As Boolean = False
    Dim ultimoSub As Integer

    Function Mostra(sub2 As Class_ELCAR_VOCI_SUB2, Optional Testo As String = "") As Boolean
        'Function Mostra(new_cod As Class_ELCAR_VOCI_SUB2, ORIGINE As String, Optional DB_Bambino As String = "") As Class_ELCAR_VOCI_SUB2
        Nuova_SUB2 = sub2

        Dim codice_genitore As String = Nuova_SUB2.ELCAR
        ' Recupera l'ultimo ID_SUB specifico per quel codice ELCAR
        ultimoSub = sub2.ID_SUB
        TextBox2.Text = Nuova_SUB2.Testo
        TextBox3.Text = Nuova_SUB2.Testo_Lungo
        CheckBox1.Checked = Not Nuova_SUB2.Deleted

        If sub2.ID = 0 Then
            'é una nuova voce 
            ultimoSub = Class_ELCAR_VOCI_SUB2.Trova_ultimo_IDSUB(codice_genitore)
            Nuova_SUB2.ID_SUB = ultimoSub + 1
            Label2.Text = "Nuova voce Aggiuntiva per: " + Nuova_SUB2.ELCAR
            Label1.Text = codice_genitore
            Label7.Text = codice_genitore + "." + Nuova_SUB2.ID_SUB.ToString : Label7.Refresh()
        Else
            Label2.Text = "Modifica voce di: " + Nuova_SUB2.ELCAR + "." + Nuova_SUB2.ID_SUB.ToString
            Label1.Text = codice_genitore
            Label7.Text = codice_genitore
        End If

        Me.ShowDialog()
        If Me.Tag = "" Then Return False

        Return True
    End Function

    Function XX_Mostra_Bambino(new_cod_bambino As Class_ELCAR_VOCI_PERSONALI, ORIGINE As String, jb As Integer) As Class_ELCAR_VOCI_PERSONALI
        Dim ultimoSub As Integer
        If jb < 0 Then
            Exit Function
        End If

        Gbl_DB_BAMBINO = Gbl_Elenco_bambini(jb).DBname.ToUpper
        If InStr(Gbl_DB_BAMBINO, ".DB") = 0 Then Gbl_DB_BAMBINO = Gbl_DB_BAMBINO + ".DB"

        Dim nome_bambino = Gbl_Elenco_bambini(jb).Cognome + " " + Gbl_Elenco_bambini(jb).Nome
        Label2.Text = "Nuovo Codice Aggiuntivo per: " + nome_bambino
        Label2.Tag = jb

        Nuovo_Codice_Bambino = new_cod_bambino


        Codice_Genitore = new_cod_bambino.ELCAR + "." + new_cod_bambino.ID_SUB.ToString

        ' Recupera l'ultimo ID_SUB specifico per quel BAMBINO
        ultimoSub = Class_ELCAR_VOCI_PERSONALI.Trova_ultimo_IDSUB(Codice_Genitore)

        ' Recupera l'ultimo ID_SUB specifico per quel codice ELCAR
        ultimoSub = Class_ELCAR_VOCI_SUB2.Trova_ultimo_IDSUB(Codice_Genitore)
        ' Suggerisce il valore successivo per il nuovo inserimento
        Dim prossimoSub As Integer = ultimoSub + 1
        Label7.Text = Codice_Genitore + "." + prossimoSub.ToString : Label7.Refresh()
        Label7.Tag = prossimoSub

        TextBox2.Text = Nuova_SUB2.Testo
        TextBox3.Text = Nuova_SUB2.Testo_Lungo
        CheckBox1.Checked = Not Nuova_SUB2.Deleted

    End Function


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Tag = ""
        Me.Hide()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Tag = "OK"
        Nuova_SUB2.Testo = TextBox2.Text
        Nuova_SUB2.Testo_Lungo = TextBox3.Text
        Nuova_SUB2.Deleted = Not (CheckBox1.Checked)
        'Nuova_SUB2.ID = 0

        Dim M = "Vuoi salvare la nuova del codice [" + Nuova_SUB2.ELCAR + "] ?"
        If MsgBox(M, vbYesNo) <> vbYes Then Exit Sub

        If Not Nuova_SUB2.Save() Then
            Call MsgBox("Voce NON SALVATA", vbExclamation)
        Else
            Call MsgBox("Voce salvata correttamente", vbInformation)
        End If
        Me.Hide()

    End Sub

    Private Sub F_CODICE_ADD_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class