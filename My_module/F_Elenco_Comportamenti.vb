Imports System.CodeDom.Compiler
Imports DocumentFormat.OpenXml.Bibliography

Public Class F_Elenco_Comportamenti
    Dim P_Modo As String
    Dim P_elenco_cp As List(Of Class_COMPORTAMENTI)
    Dim P_elenco_Routine As List(Of Class_ROUTINE)
    Dim P_Edit As Boolean = False
    Dim P_TESTO As String
    Dim P_ID As Integer
    Dim P_Attiva As Boolean
    Dim p_last_j As Integer
    Dim Noneseguire As Boolean = True
    Sub Main(Optional modo = "CP", Optional editing = False)
        P_Modo = modo
        P_Edit = editing
        p_last_j = 0
        Panel1.Visible = False
        CheckBox1.Checked = True
        If editing = False Then
            Button2.Text = "Seleziona"
            CheckBox1.Visible = False
        Else
            CheckBox1.Visible = True
            Button2.Text = "Modifica"
        End If
        Call Carica()
        Label5.Text = ComboBox1.Items.Count.ToString

        Noneseguire = False

        ComboBox1.DisplayMember = "TESTO"
        Me.ShowDialog()
    End Sub
    Sub Carica()
        ' 1. Rimuovi la gestione dei vecchi eventi prima di cambiare i dati
        ' (Sostituisci "ComboBox1_SelectedIndexChanged" con il nome reale del tuo evento se diverso)
        RemoveHandler ComboBox1.SelectedIndexChanged, AddressOf ComboBox1_SelectedIndexChanged

        ' 2. Svuota solo il DataSource (Items.Clear non serve e può generare conflitti quando c'è un DataSource)
        ComboBox1.DataSource = Nothing

        ' 3. Carica i dati in base al modo
        If P_Modo = "CP" Then
            P_elenco_cp = Class_COMPORTAMENTI.Load_all(CheckBox1.Checked)
            ComboBox1.DisplayMember = "TESTO" ' Imposta il DisplayMember PRIMA del DataSource
            ComboBox1.DataSource = P_elenco_cp
        Else
            P_elenco_Routine = Class_ROUTINE.Load_all(CheckBox1.Checked)
            ComboBox1.DisplayMember = "TESTO" ' Imposta il DisplayMember PRIMA del DataSource
            ComboBox1.DataSource = P_elenco_Routine
        End If

        ' 4. Ripristina il gestore degli eventi
        AddHandler ComboBox1.SelectedIndexChanged, AddressOf ComboBox1_SelectedIndexChanged

        If Noneseguire Then Exit Sub

        ' 5. Gestione sicura dell'indice di selezione
        If ComboBox1.Items.Count > 0 Then
            ' Verifica che p_last_j sia un indice valido per la lista attuale
            If p_last_j >= 0 AndAlso p_last_j < ComboBox1.Items.Count Then
                ComboBox1.SelectedIndex = p_last_j
            Else
                ComboBox1.SelectedIndex = 0
            End If
        Else
            ComboBox1.SelectedIndex = -1
        End If
        Label5.Text = ComboBox1.Items.Count.ToString
        ' Nota: ComboBox1.Refresh() non è più necessario
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

        P_TESTO = ComboBox1.SelectedItem.testo
        P_Attiva = CBool(ComboBox1.SelectedItem.attiva)
        P_ID = ComboBox1.SelectedItem.id
        p_last_j = ComboBox1.SelectedIndex
        Label1.Text = P_TESTO
        Label1.Tag = P_ID
        Call mostra()

        Me.Tag = Label1.Text + "," + Label1.Tag.ToString
    End Sub
    Sub mostra()
        If P_Attiva Then
            Label3.ImageIndex = 0
            B_Disattiva.Text = "Disattiva"
        Else
            Label3.ImageIndex = 1
            B_Disattiva.Text = "Attiva"
        End If
        Label1.Text = P_TESTO

    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Tag = ""
        Me.Hide()
    End Sub


    Private Sub B_Seleziona_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Not P_Edit Then
            Me.Hide()
            Exit Sub
        End If

        If Button2.Text = "Modifica" Then
            Panel1.Visible = True
            Button2.Text = "Esci da modifica"
        Else
            Panel1.Visible = False
            Button2.Text = "Modifica"
        End If


    End Sub
    Function Carica_valori_e_salva() As Boolean


        If P_Modo = "CP" Then
            Dim temp As New Class_COMPORTAMENTI
            temp.ID = P_ID
            temp.TESTO = P_TESTO
            temp.ATTIVA = P_Attiva
            temp.Save()
        Else
            Dim temp As New Class_ROUTINE
            temp.ID = P_ID
            temp.TESTO = P_TESTO
            temp.ATTIVA = P_Attiva
            temp.Save()
        End If

    End Function

    Private Sub B_Disattiva_Click(sender As Object, e As EventArgs) Handles B_Disattiva.Click
        If P_Attiva Then
            P_Attiva = False
        Else
            P_Attiva = True
        End If
        Call Carica_valori_e_salva()
        ComboBox1.SelectedItem.attiva = P_Attiva
        ComboBox1.SelectedItem.Testo = P_TESTO
        Call mostra()
        ComboBox1.Refresh()
        'Call Carica()
    End Sub

    Private Sub B_Add_Click(sender As Object, e As EventArgs) Handles B_Add.Click
        Dim m = "Testo nella nuova voce:"
        Dim t = "Nuova voce"
        Dim x = ""
        x = InputBox(m, t, x)
        If x = "" Then Exit Sub

        m = "Nuova voce da inserire: " + vbCrLf + x.ToString + vbCrLf + vbCrLf
        m = m + "Vuoi salvarla?"
        If MsgBox(m, vbQuestion + vbYesNo, "Salvataggio nuova voce") <> vbYes Then Exit Sub
        P_ID = 0
        P_Attiva = True
        P_TESTO = x
        Call Carica_valori_e_salva()
        Call Carica()


    End Sub

    Private Sub B_EditTesto_Click(sender As Object, e As EventArgs) Handles B_EditTesto.Click
        Dim m = "Testo da modificare:"
        Dim t = "Modifica voce"
        Dim x = P_TESTO
        x = InputBox(m, t, x)
        If x = "" Then Exit Sub

        m = "Voce modificata: " + vbCrLf + x.ToString + vbCrLf + vbCrLf
        m = m + "Vuoi salvarla?"
        If MsgBox(m, vbQuestion + vbYesNo, "Salvataggio Modifica voce") <> vbYes Then Exit Sub
        P_Attiva = True
        P_TESTO = x
        Call Carica_valori_e_salva()
        ComboBox1.SelectedItem.attiva = P_Attiva
        ComboBox1.SelectedItem.Testo = P_TESTO
        Dim cm As CurrencyManager = CType(ComboBox1.BindingContext(ComboBox1.DataSource), CurrencyManager)
        If cm IsNot Nothing Then
            cm.Refresh()
        End If
        Call mostra()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        Carica()
    End Sub

    Private Sub F_Comportamenti_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class