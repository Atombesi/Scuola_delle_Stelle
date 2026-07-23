Imports Microsoft.Data.Sqlite

Public Class Class_COMPORTAMENTI
    Public Property ID As Integer
    Public Property NOTA As String
    Public Property Testo As String
    Private ReadOnly ConnectionString As String =
       "Server=.\SQLEXPRESS;Database=Scuoladellestelle;Trusted_Connection=True;"


    Public Sub Save()

        Using cn = DB.GetConnection_Scuoladellestelle()
            If ID = 0 Then
                Dim sql As String =
                    "INSERT INTO NOTA (NOTA,Testo) 
                     VALUES (@NOTA,@Testo);
                     SELECT last_insert_rowid();"
                Using cmd As New SqliteCommand(sql, cn)
                    cmd.Parameters.AddWithValue("@NOTA", NOTA)
                    cmd.Parameters.AddWithValue("@Testo", Testo)
                    ID = Convert.ToInt32(cmd.ExecuteScalar())
                End Using
            Else
                Dim sql As String =
                    "UPDATE NOTA
                     SET NOTA=@NOTA,
                     SET Testo=@Testo,
                     WHERE ID=@ID"

                Using cmd As New SqliteCommand(sql, cn)

                    cmd.Parameters.AddWithValue("@NOTA", NOTA)
                    cmd.Parameters.AddWithValue("@ID", ID)
                    cmd.Parameters.AddWithValue("@Testo", Testo)

                    cmd.ExecuteNonQuery()

                End Using

            End If

        End Using



    End Sub

    Public Function Load(id As Integer) As Class_COMPORTAMENTI

        Using cn = DB.GetConnection_Scuoladellestelle()

            Dim sql As String =
                "SELECT ID, NOTA,ID_SUB,Testo,Deleted
                 FROM NOTA
                 WHERE ID = @ID"

            Using cmd As New SqliteCommand(sql, cn)

                cmd.Parameters.AddWithValue("@ID", id)

                Using rd = cmd.ExecuteReader()

                    If rd.Read() Then
                        id = Convert.ToInt32(rd("ID"))
                        NOTA = Convert.ToString(rd("NOTA"))
                        Testo = Convert.ToString(rd("Testo"))
                    Else

                        Return Nothing

                    End If

                End Using

            End Using

        End Using

    End Function
End Class

