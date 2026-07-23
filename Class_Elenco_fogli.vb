Imports Microsoft.Data.Sqlite

Public Class Class_Elenco_fogli
    Public Property ID_Foglio As Integer
    Public Property Data As String


    Public Function Trova_tutti_fogli() As List(Of Class_Elenco_fogli)


        Dim lista As New List(Of Class_Elenco_fogli)

        Using cn = DB.GetConnection_Bambino()

            cn.Open()

            Dim sql As String =
                "SELECT DISTINCT ID_Foglio, DATA From GIORNALIERO Order By ID_Foglio DESC"

            Using cmd As New SqliteCommand(sql, cn)

                Using reader As SqliteDataReader = cmd.ExecuteReader()

                    While reader.Read()

                        Dim foglio As New Class_Elenco_fogli With {
                            .ID_Foglio = reader.GetInt32(
                                reader.GetOrdinal("ID_Foglio")
                            ),
                            .Data = reader("DATA").ToString()
                        }

                        lista.Add(foglio)

                    End While

                End Using

            End Using

        End Using

        Return lista

    End Function
End Class
