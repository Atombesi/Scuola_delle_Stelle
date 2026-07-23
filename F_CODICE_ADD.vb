Public Class F_CODICE_ADD
    Public Nuovo_Codice As Class_ELCAR_VOCI_SUB2
    Public Nuovo_Codice_Bambino As Class_ELCAR_VOCI_PERSONALI
    Public Codice_Genitore As String
    Public dB_Bambino As Boolean = False
    Dim ultimoSub As Integer

    Function Mostra(new_cod As Class_ELCAR_VOCI_SUB2, ORIGINE As String, Optional DB_Bambino As String = "") As Class_ELCAR_VOCI_SUB2
        Nuovo_Codice = new_cod


        Codice_Genitore = Nuovo_Codice.ELCAR + "." + Nuovo_Codice.ID_SUB.ToString


        ' Recupera l'ultimo ID_SUB specifico per quel codice ELCAR
        ultimoSub = Class_ELCAR_VOCI_SUB2.Trova_ultimo_IDSUB(Codice_Genitore)
        ' Suggerisce il valore successivo per il nuovo inserimento
        Dim prossimoSub As Integer = ultimoSub + 1
        Label7.Text = Codice_Genitore + "." + prossimoSub.ToString : Label7.Refresh()
        Label7.Tag = prossimoSub

        Label2.Text = "Nuovo Codice Aggiuntivo per: " + Codice_Genitore
        TextBox2.Text = Nuovo_Codice.Testo
        TextBox3.Text = Nuovo_Codice.Testo_Lungo
        CheckBox1.Checked = Not Nuovo_Codice.Deleted

        Label1.Text = ORIGINE
        Me.ShowDialog()
        If Me.Tag = "" Then
            Return Nothing
        Else
            Return Nuovo_Codice

        End If
    End Function

    Function Mostra_Bambino(new_cod_bambino As Class_ELCAR_VOCI_PERSONALI, ORIGINE As String, jb As Integer) As Class_ELCAR_VOCI_PERSONALI
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

        TextBox2.Text = Nuovo_Codice.Testo
        TextBox3.Text = Nuovo_Codice.Testo_Lungo
        CheckBox1.Checked = Not Nuovo_Codice.Deleted

    End Function


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Tag = ""
        Me.Hide()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Tag = "OK"
        Nuovo_Codice.Testo = TextBox2.Text
        Nuovo_Codice.Testo_Lungo = TextBox3.Text
        Nuovo_Codice.Deleted = Not (CheckBox1.Checked)
        Nuovo_Codice.ID_SUB = Label7.Tag
        Nuovo_Codice.ELCAR = Codice_Genitore
        Dim M = ""
        M = "Vuoi salvare il nuovo codice personalizzato del Bambino?"
        If dB_Bambino Then
            M = "Vuoi salvare il nuovo codice inserito?"
        End If
        If MsgBox(M, vbYesNo) <> vbYes Then Exit Sub

        If Not Nuovo_Codice.Save() Then
            Call MsgBox("Nuovo codice NON SALVATO")
        Else
            Call MsgBox("OK")
        End If
        Me.Hide()

    End Sub

    Private Sub F_CODICE_ADD_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class