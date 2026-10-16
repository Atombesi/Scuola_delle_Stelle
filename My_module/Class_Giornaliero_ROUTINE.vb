Imports Microsoft.Data.Sqlite

Public Class Class_Giornaliero_ROUTINE
    Public Property ID As Integer
    Public Property ID_VOCE As Integer
    Public Property ID_FOGLIO As Integer
    Public Property NOTA As String
    Public Property TESTO As String
    Public Property TOT As Integer
    Public Property DATA As String
    Public Property COMPILATO As Integer
    Public Property N_voce As Integer


    Private ReadOnly ConnectionString As String =
           "Server=.\SQLEXPRESS;Database=Scuoladellestelle;Trusted_Connection=True;"
    Public Function Carica_per_Foglio(ID_Foglio As Integer) As List(Of Class_Giornaliero_ROUTINE)
        Dim listaRisultati As New List(Of Class_Giornaliero_ROUTINE)()

        Try
            Using cn = DB.GetConnection_Bambino()
                ' Forza l'apertura se chiusa
                If cn.State <> ConnectionState.Open Then cn.Open()


                Dim sql = "SELECT * FROM GIORNALIERO_ROUTINE WHERE ID_FOGLIO = @ID_FOGLIO"


                Using cmd As New SqliteCommand(sql, cn)
                    cmd.Parameters.AddWithValue("@ID_FOGLIO", ID_Foglio)

                    Using rd As SqliteDataReader = cmd.ExecuteReader()
                        While rd.Read()
                            Dim record As New Class_Giornaliero_ROUTINE()

                            record.ID = Convert.ToInt32(rd("ID"))
                            record.ID_FOGLIO = Convert.ToInt32(rd("ID_FOGLIO"))
                            record.ID_VOCE = Convert.ToInt32(rd("ID_VOCE"))
                            record.TOT = Convert.ToInt32(rd("TOT"))
                            If Not rd.IsDBNull(rd.GetOrdinal("TESTO")) Then record.TESTO = rd("TESTO").ToString() Else record.TESTO = ""
                            If Not rd.IsDBNull(rd.GetOrdinal("DATA")) Then record.DATA = rd("DATA").ToString() Else record.DATA = ""
                            ' Converte lo 0 o 1 del DB in Boolean/Integer a seconda della tua classe
                            record.COMPILATO = Convert.ToInt32(rd("COMPILATO"))
                            record.N_voce = Convert.ToInt32(rd("N_voce"))

                            listaRisultati.Add(record)
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Console.WriteLine("Errore in Carica_per_Foglio Routine: " & ex.Message)
        End Try

        Return listaRisultati
    End Function



    Public Function Salva() As Boolean
        Using cn = DB.GetConnection_Bambino
            cn.Open()
            Dim sql As String = ""

            ' Determina se inserire un nuovo record o aggiornare quello esistente
            If Me.ID = 0 Then
                sql = "INSERT INTO GIORNALIERO_ROUTINE (ID_FOGLIO, ID_VOCE, DATA, TESTO, TOT, COMPILATO, N_VOCE) " &
                      "VALUES (@ID_FOGLIO, @ID_VOCE, @DATA, @TESTO, @TOT, @COMPILATO, @N_VOCE); " &
                      "SELECT last_insert_rowid();"
            Else
                sql = "UPDATE GIORNALIERO_ROUTINE SET " &
                      "ID_FOGLIO = @ID_FOGLIO, DATA = @DATA, ID_VOCE = @ID_VOCE, " &
                      "TESTO = @TESTO, TOT = @TOT, COMPILATO = @COMPILATO, N_VOCE = @N_VOCE " &
                      "WHERE ID = @ID;"


            End If

            Using cmd As New SqliteCommand(sql, cn)
                Try
                    ' Assegnazione dei parametri comuni a entrambe le query
                    cmd.Parameters.AddWithValue("@ID_FOGLIO", Me.ID_FOGLIO)
                    cmd.Parameters.AddWithValue("@ID_VOCE", Me.ID_VOCE)
                    cmd.Parameters.AddWithValue("@DATA", Me.DATA)
                    cmd.Parameters.AddWithValue("@TESTO", Me.TESTO)
                    cmd.Parameters.AddWithValue("@TOT", Me.TOT)
                    cmd.Parameters.AddWithValue("@COMPILATO", Me.COMPILATO)
                    cmd.Parameters.AddWithValue("@N_VOCE", Me.N_voce)

                Catch ex As Exception
                    Return False
                End Try
                If Me.ID = 0 Then
                    ' Esegue l'inserimento e recupera l'ID appena generato da SQLite
                    Dim nuovoId = cmd.ExecuteScalar()
                    If nuovoId IsNot Nothing AndAlso nuovoId IsNot DBNull.Value Then
                        Me.ID = Convert.ToInt32(nuovoId)
                    End If
                Else
                    ' Aggiunge il parametro ID ed esegue l'aggiornamento
                    cmd.Parameters.AddWithValue("@ID", Me.ID)
                    cmd.ExecuteNonQuery()
                End If
            End Using
        End Using
        Return Me.ID


    End Function

End Class
