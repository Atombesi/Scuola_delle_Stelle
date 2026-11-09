Imports DocumentFormat.OpenXml.Wordprocessing
Imports Microsoft.Data.Sqlite

Public Class Class_COMPORTAMENTI
    Public Property ID As Integer
    Public Property TESTO As String
    Public Property ATTIVA As Boolean

    Private ReadOnly ConnectionString As String =
       "Server=.\SQLEXPRESS;Database=Scuoladellestelle;Trusted_Connection=True;"


    Public Sub Save()

        Using cn = MY_DB.GetConnection_Scuoladellestelle()

            If ID = 0 Then
                Dim sql As String =
                    "INSERT INTO COMPORTAMENTI (TESTO,ATTIVA) 
                     VALUES (@TESTO,@ATTIVA);
                     SELECT last_insert_rowid();"
                Using cmd As New SqliteCommand(sql, cn)
                    cmd.Parameters.AddWithValue("@TESTO", TESTO)
                    cmd.Parameters.AddWithValue("@ATTIVA", ATTIVA)
                    ID = Convert.ToInt32(cmd.ExecuteScalar())
                End Using
            Else
                Dim sql As String =
                    "UPDATE COMPORTAMENTI SET TESTO=@TESTO, ATTIVA=@ATTIVA WHERE ID=@ID"

                Using cmd As New SqliteCommand(sql, cn)

                    cmd.Parameters.AddWithValue("@TESTO", TESTO)
                    cmd.Parameters.AddWithValue("@ID", ID)
                    cmd.Parameters.AddWithValue("@ATTIVA", ATTIVA)

                    cmd.ExecuteNonQuery()

                End Using

            End If

        End Using



    End Sub

    Public Shared Function Load(id As Integer) As Class_COMPORTAMENTI

        Using cn = MY_DB.GetConnection_Scuoladellestelle()

            Dim sql As String =
                "SELECT * FROM COMPORTAMENTI WHERE ID = @ID"

            Using cmd As New SqliteCommand(sql, cn)

                cmd.Parameters.AddWithValue("@ID", id)

                Using rd = cmd.ExecuteReader()

                    If rd.Read() Then
                        Dim cp As New Class_COMPORTAMENTI
                        cp.ID = Convert.ToInt32(rd("ID"))
                        cp.TESTO = Convert.ToString(rd("TESTO"))
                        cp.ATTIVA = Convert.ToInt32(rd("ATTIVA"))
                        Return cp
                    Else

                        Return Nothing

                    End If

                End Using

            End Using

        End Using

    End Function

    Public Shared Function Load_all(Optional soloattivi As Boolean = False) As List(Of Class_COMPORTAMENTI)

        Dim listaRisultati As New List(Of Class_COMPORTAMENTI)()

        Using cn = MY_DB.GetConnection_Scuoladellestelle()

            ' Usa l'operatore LIKE per cercare corrispondenze parziali
            Dim sql As String =
                "SELECT ID, ATTIVA, TESTO FROM COMPORTAMENTI ORDER BY ATTIVA"

            Using cmd As New SqliteCommand(sql, cn)

                Using rd = cmd.ExecuteReader()

                    ' Usa While al posto di If perché ci possono essere più record corrispondenti
                    While rd.Read()
                        ' Crea una nuova istanza della classe per ogni riga trovata
                        Dim record As New Class_COMPORTAMENTI()

                        record.ID = Convert.ToInt32(rd("ID"))
                        Try
                            record.ATTIVA = Convert.ToInt32(rd("ATTIVA"))
                        Catch ex As Exception
                            record.ATTIVA = False
                        End Try
                        record.TESTO = Convert.ToString(rd("TESTO"))
                        ' Aggiunge il record alla lista dei risultati solo se attivi
                        If soloattivi And Not record.ATTIVA Then
                            Continue While
                        End If

                        listaRisultati.Add(record)
                    End While

                End Using
            End Using
        End Using

        ' Restituisce la lista (sarà vuota se non trova nessuna corrispondenza)
        Return listaRisultati
    End Function

End Class

