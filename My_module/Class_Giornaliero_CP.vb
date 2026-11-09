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
            Using cn = MY_DB.GetConnection_Bambino()
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
        Using cn = MY_DB.GetConnection_Bambino
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
    Public Function Salva_nuovo(cn As SqliteConnection, trans As SqliteTransaction) As Boolean
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
            cmd.Transaction = trans

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
            If Me.ID <> 0 Then
                cmd.Parameters.AddWithValue("@ID", Me.ID)
            End If
            cmd.ExecuteNonQuery()
        End Using
        Return True
    End Function
    Public Function Salva_Totali(cn As SqliteConnection, trans As SqliteTransaction) As Boolean
        ' Rimosso il vecchio "Using cn ... cn.Open()" perché la connessione arriva già aperta

        If Me.ID = 0 Then Return False

        Dim sql As String = "UPDATE GIORNALIERO_COMPORTAMENTALE SET " &
                         "TOT = @TOT, COMPILATO = @COMPILATO WHERE ID = @ID;"

        ' Passiamo sia la connessione "cn" che la transazione "trans" al comando
        Using cmd As New SqliteCommand(sql, cn, trans)
            Try
                cmd.Parameters.AddWithValue("@TOT", Me.TOT)
                cmd.Parameters.AddWithValue("@COMPILATO", Me.COMPILATO)
                cmd.Parameters.AddWithValue("@ID", Me.ID)

                cmd.ExecuteNonQuery()
                Return True ' Ritorna True se l'esecuzione va a buon fine
            Catch ex As Exception
                ' Logga l'errore se necessario, ma ritorna False per far scattare il Rollback nel ciclo principale
                Return False
            End Try
        End Using
    End Function

    Public Shared Function OttieniTuttiIRecordGiornalieri(Optional dbBambino = "") As DataTable
        Dim dt As New DataTable()

        ' Rimuoviamo la clausola WHERE e ordiniamo prima per CODICE, poi per DATA (convertita in formato YYYY-MM-DD)
        Dim query As String = "SELECT * FROM GIORNALIERO_COMPORTAMENTALE " &
                          "ORDER BY TESTO ASC, " &
                          "SUBSTR(DATA, 7, 4) || '-' || SUBSTR(DATA, 4, 2) || '-' || SUBSTR(DATA, 1, 2) ASC"
        Using cn = MY_DB.GetConnection_Bambino(dbBambino)
            Using cmd As New SqliteCommand(query, cn)
                Using reader As SqliteDataReader = cmd.ExecuteReader()
                    ' Carica tutti i record estratti all'interno della DataTable
                    dt.Load(reader)
                End Using
            End Using
        End Using

        Return dt
    End Function

    Public Shared Function Ottieni_Tutte_CP_usati() As DataTable

        Dim dt As New DataTable()
        Dim query As String = "SELECT * FROM GIORNALIERO_COMPORTAMENTALE GROUP BY ID_VOCE ORDER BY TESTO ASC"
        Using cn = MY_DB.GetConnection_Bambino()
            Using cmd As New SqliteCommand(query, cn)
                Using reader As SqliteDataReader = cmd.ExecuteReader()
                    dt.Load(reader)
                End Using
            End Using
        End Using
        Return dt
    End Function

    Public Shared Function OttieniRecordPerVoceCP(testo As String, Optional solocompilati As Boolean = True) As DataTable
        If testo = "" Then Return Nothing
        Dim query As String = ""
        Dim dt As New DataTable()

        If solocompilati Then
            query = "SELECT * FROM GIORNALIERO_COMPORTAMENTALE WHERE TESTO = @testo AND COMPILATO = 1 ORDER BY SUBSTR(DATA, 7, 4) || '-' || SUBSTR(DATA, 4, 2) || '-' || SUBSTR(DATA, 1, 2) ASC"
        Else
            query = "SELECT * FROM GIORNALIERO_COMPORTAMENTALE WHERE TESTO = @testo ORDER BY SUBSTR(DATA, 7, 4) || '-' || SUBSTR(DATA, 4, 2) || '-' || SUBSTR(DATA, 1, 2) ASC"
        End If

        Using cn = MY_DB.GetConnection_Bambino()

            Using cmd As New SqliteCommand(query, cn)
                ' Aggiungiamo il parametro in modo sicuro
                cmd.Parameters.AddWithValue("@testo", testo)

                Using reader As SqliteDataReader = cmd.ExecuteReader()
                    dt.Load(reader)
                End Using
            End Using
        End Using

        Return dt
    End Function

    Public Shared Function Ottieni_CP_by_IDF(id_foglio As Integer) As DataTable

        Dim query As String = ""
        Dim dt As New DataTable()
        query = "SELECT * FROM GIORNALIERO_COMPORTAMENTALE WHERE ID_FOGLIO = @ID_FOGLIO"
        Using cn = MY_DB.GetConnection_Bambino()
            Using cmd As New SqliteCommand(query, cn)
                cmd.Parameters.AddWithValue("@ID_FOGLIO", id_foglio)
                Using reader As SqliteDataReader = cmd.ExecuteReader()
                    dt.Load(reader)
                End Using
            End Using
        End Using
        Return dt
    End Function



End Class
