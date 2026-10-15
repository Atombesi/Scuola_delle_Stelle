Imports Microsoft.Data.Sqlite

Public Class Class_Giornaliero_CP
    Public Property ID As Integer
    Public Property ID_VOCE As Integer
    Public Property ID_FOGLIO As Integer
    Public Property NOTA As String
    Public Property TESTO As String
    Public Property TOT As Integer
    Public Property DATA As String
    Public Property COMPILATO As Integer

    Private ReadOnly ConnectionString As String =
           "Server=.\SQLEXPRESS;Database=Scuoladellestelle;Trusted_Connection=True;"
    Public Function Carica_per_Foglio(ID_Foglio As Integer) As List(Of Class_Giornaliero_CP)
        Dim listaRisultati As New List(Of Class_Giornaliero_CP)()
        Using cn = DB.GetConnection_Bambino()

            ' Usa l'operatore LIKE per cercare corrispondenze parziali
            Dim sql As String =
                "SELECT ID, ID_FOGLIO, ID_VOCE, data, TESTO, TOT, COMPILATO " &
                "FROM GIORNALIERO_COMPORTAMENTALE " &
                "WHERE ID_FOGLIO LIKE @ID_FOGLIO"
            Using cmd As New SqliteCommand(sql, cn)
                ' Il carattere % indica a SQLite di trovare qualsiasi carattere prima e dopo il testo
                cmd.Parameters.AddWithValue("@ID_FOGLIO", ID_Foglio)

                Using rd = cmd.ExecuteReader()

                    ' Usa While al posto di If perché ci possono essere più record corrispondenti
                    While rd.Read()
                        ' Crea una nuova istanza della classe per ogni riga trovata
                        Dim record As New Class_Giornaliero_CP()
                        record.ID = Convert.ToInt32(rd("ID"))
                        record.ID_FOGLIO = Convert.ToInt32(rd("ID_FOGLIO"))
                        record.ID_VOCE = Convert.ToInt32(rd("ID_VOCE"))
                        record.TESTO = Convert.ToString(rd("TESTO"))
                        record.TOT = Convert.ToInt32(rd("TOT"))
                        record.DATA = Convert.ToString(rd("data"))
                        record.COMPILATO = Convert.ToBoolean(rd("COMPILATO"))
                        ' Aggiunge il record alla lista dei risultati solo se attivi
                        listaRisultati.Add(record)
                    End While

                End Using
            End Using
        End Using
        Return listaRisultati
    End Function


    Public Function Salva() As Boolean
        Using cn = DB.GetConnection_Bambino
            cn.Open()
            Dim sql As String = ""

            ' Determina se inserire un nuovo record o aggiornare quello esistente
            If Me.ID = 0 Then
                sql = "INSERT INTO GIORNALIERO_COMPORTAMENTALE (ID_FOGLIO, ID_VOCE, DATA, TESTO, TOT, COMPILATO) " &
                      "VALUES (@ID_FOGLIO, @ID_VOCE, @DATA, @TESTO, @TOT, @COMPILATO); " &
                      "SELECT last_insert_rowid();"
            Else
                sql = "UPDATE GIORNALIERO_COMPORTAMENTALE SET " &
                      "ID_FOGLIO = @ID_FOGLIO, DATA = @DATA, ID_VOCE = @ID_VOCE, " &
                      "TESTO = @TESTO, TOT = @TOT, COMPILATO = @COMPILATO " &
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
        Return True
    End Function

End Class
