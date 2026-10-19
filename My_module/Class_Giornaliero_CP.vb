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

        Try
            Using cn = DB.GetConnection_Bambino()
                ' Forza l'apertura fisica del database se chiuso
                If cn.State <> ConnectionState.Open Then cn.Open()

                ' 🎯 CORREZIONE SQL:
                ' 1. Sostituito LIKE con = per gli ID numerici
                ' 2. Aggiunto IFNULL su data, TESTO e sul campo NOTA (se è NULL sul DB, restituisce "")
                Dim sql = ""
                sql = "SELECT * FROM GIORNALIERO_COMPORTAMENTALE WHERE ID_FOGLIO = @ID_FOGLIO"

                Using cmd As New SqliteCommand(sql, cn)
                    cmd.Parameters.AddWithValue("@ID_FOGLIO", ID_Foglio)

                    Using rd As SqliteDataReader = cmd.ExecuteReader()
                        While rd.Read()
                            Dim record As New Class_Giornaliero_CP()

                            record.ID = Convert.ToInt32(rd("ID"))
                            record.ID_FOGLIO = Convert.ToInt32(rd("ID_FOGLIO"))
                            record.ID_VOCE = Convert.ToInt32(rd("ID_VOCE"))

                            ' .ToString() nativo per bloccare i crash da valori NULL indotti
                            record.TOT = Convert.ToInt32(rd("TOT"))
                            If Not rd.IsDBNull(rd.GetOrdinal("TESTO")) Then record.TESTO = rd("TESTO").ToString() Else record.TESTO = ""
                            If Not rd.IsDBNull(rd.GetOrdinal("DATA")) Then record.DATA = rd("DATA").ToString() Else record.DATA = ""
                            'record.TESTO = rd("TESTO").ToString()
                            'record.DATA = rd("data").ToString()

                            ' Mappiamo il flag compilato leggendo il valore intero nativo di SQLite
                            record.COMPILATO = Convert.ToInt32(rd("COMPILATO"))

                            listaRisultati.Add(record)
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Console.WriteLine("Errore in Carica_per_Foglio CP: " & ex.Message)
        End Try

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
    Public Function Salva_Totali(dbbambino) As Boolean
        Using cn = DB.GetConnection_Bambino(dbbambino)
            cn.Open()
            Dim sql As String = ""

            ' Determina se inserire un nuovo record o aggiornare quello esistente
            If Me.ID = 0 Then Return False
            sql = "UPDATE GIORNALIERO_COMPORTAMENTALE SET " &
                      "TOT = @TOT, COMPILATO = @COMPILATO WHERE ID = @ID;"

            Using cmd As New SqliteCommand(sql, cn)
                Try
                    ' Assegnazione dei parametri comuni a entrambe le query
                    cmd.Parameters.AddWithValue("@TOT", Me.TOT)
                    cmd.Parameters.AddWithValue("@COMPILATO", Me.COMPILATO)

                Catch ex As Exception
                    Return False
                End Try
                ' Aggiunge il parametro ID ed esegue l'aggiornamento
                cmd.Parameters.AddWithValue("@ID", Me.ID)
                cmd.ExecuteNonQuery()
            End Using
        End Using
        Return True
    End Function
End Class
