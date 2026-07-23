Imports System.Runtime.CompilerServices


Public Module EstensioniControlli


    <Extension()>
    Public Sub PopolaLeggero(ByVal combo As ComboBox, ByVal lista As List(Of Class_ElementoCombo))
        combo.DataSource = Nothing
        combo.DataSource = lista
        combo.DisplayMember = "Testo" ' Indica la proprietà "Testo" della tua classe da mostrare a schermo
        combo.ValueMember = "ID"      ' Indica la proprietà "ID" da usare come chiave nascosta
    End Sub

    <Extension()>
    Public Sub PopolaLeggero(ByVal listaGrafica As ListBox, ByVal lista As List(Of Class_ElementoCombo))
        listaGrafica.DataSource = Nothing
        listaGrafica.DataSource = lista
        listaGrafica.DisplayMember = "Testo"
        listaGrafica.ValueMember = "ID"
    End Sub


End Module
