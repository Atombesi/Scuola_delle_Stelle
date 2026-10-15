Imports System.Diagnostics.Eventing
Imports Microsoft.Data.Sqlite

Public Class Class_Elenco_fogli
    Public Property ID_Foglio As Integer
    Public Property Data As String
    Public titolo As String
    Public Compilato As Boolean

    Public Function Foglio_esistente(Mydata As String) As Boolean
        Dim trovato = False
        Dim sql As String = "SELECT 1 FROM GIORNALIERO WHERE DATA = @data LIMIT 1"

        Using cn = DB.GetConnection_Bambino()
            cn.Open()
            Try
                Using cmd As New SqliteCommand(sql, cn)
                    ' Assegnazione dei parametri in modo sicuro (entrambi trattati come stringhe)
                    cmd.Parameters.AddWithValue("@data", Mydata) ' Formato atteso "gg/mm/yyyy"

                    ' ExecuteScalar restituisce il valore della prima colonna del primo record trovato
                    Dim risultato As Object = cmd.ExecuteScalar()

                    ' Se risultato non è Nothing, significa che la query ha trovato il record
                    If risultato IsNot Nothing Then
                        Return True
                    Else
                        Return False
                    End If
                End Using

            Catch ex As Exception
                ' Gestione degli errori (puoi loggarlo o mostrarlo in base alle tue esigenze)
                MessageBox.Show("Errore durante la verifica del foglio: " & ex.Message, "Errore DB", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End Try
        End Using

        Return trovato


    End Function


    Public Function Trova_tutti_fogli() As List(Of Class_Elenco_fogli)


        Dim lista As New List(Of Class_Elenco_fogli)

        Using cn = DB.GetConnection_Bambino()

            cn.Open()

            Dim sql As String =
                "SELECT DISTINCT ID_Foglio, DATA, COMPILATO From GIORNALIERO Order By ID_Foglio DESC"

            Using cmd As New SqliteCommand(sql, cn)

                Using reader As SqliteDataReader = cmd.ExecuteReader()

                    While reader.Read()

                        Dim foglio As New Class_Elenco_fogli With {
                            .ID_Foglio = reader.GetInt32(
                                reader.GetOrdinal("ID_Foglio")
                            ),
                            .Data = reader("DATA").ToString(),
                            .Compilato = Convert.ToBoolean(reader("COMPILATO"))
                        }

                        lista.Add(foglio)

                    End While

                End Using

            End Using

        End Using

        Return lista

    End Function
End Class
