Public Class Class_ElementoCombo
    Public Property ID As Integer
    Public Property Testo As String
    Public Property Tag As String
    Public Property TagObj As Object

    ' Costruttore per creare l'oggetto rapidamente
    Public Sub New(ByVal id As Integer, ByVal testo As String, ByVal tag As String, Optional tagobj As Object = Nothing)
        Me.ID = id
        Me.Tag = tag
        Me.Testo = testo
        Me.TagObj = tagobj
    End Sub
End Class
