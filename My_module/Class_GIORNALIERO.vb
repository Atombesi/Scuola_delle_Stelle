Imports DocumentFormat.OpenXml.Spreadsheet
Imports Microsoft.Data.Sqlite
Public Class Class_GIORNALIERO
    Public Property ID As Integer
    Public Property ID_FOGLIO As Integer
    Public Property DATA As String
    Public Property VALUTATIVO As Boolean
    Public Property TUTTELERISPOSTE As Boolean
    Public Property Codice As String
    Public Property Testo As String
    Public Property TOT1_POS As Integer
    Public Property TOT1_NEG As Integer
    Public Property TOT1_PROMPT As Integer
    Public Property TOT1 As Integer
    Public Property TOT2_POS As Integer
    Public Property TOT2_NEG As Integer
    Public Property TOT2_PROMPT As Integer
    Public Property TOT2 As Integer
    Public Property COMPILATO As Boolean
    Public Property TERMINE As Boolean



    'Private ReadOnly ConnectionString As String =
    '   "Server=.\SQLEXPRESS;Database=Scuoladellestelle;Trusted_Connection=True;"


    Public Shared Function Load(id As Integer) As Class_GIORNALIERO

        Using cn = DB.GetConnection_Bambino()


            Dim sql As String =
                "SELECT ID, CODICE,Testo,Testo_Lungo 
                 FROM ELCAR
                 WHERE ID = @ID"

            Using cmd As New SqliteCommand(sql, cn)

                cmd.Parameters.AddWithValue("@ID", id)

                Using rd = cmd.ExecuteReader()

                    If rd.Read() Then
                        Dim record As New Class_GIORNALIERO()
                        record.ID = Convert.ToInt32(rd("ID"))
                        record.Codice = Convert.ToString(rd("Codice"))
                        record.Testo = Convert.ToString(rd("Testo"))
                    Else
                        Return Nothing

                    End If

                End Using

            End Using

        End Using

    End Function
    Public Shared Function CercaPerCodice(testoCercato As String) As List(Of Class_GIORNALIERO)
        Dim listaRisultati As New List(Of Class_GIORNALIERO)()
        Using cn = DB.GetConnection_Bambino()

            ' Usa l'operatore LIKE per cercare corrispondenze parziali
            Dim sql As String =
                "SELECT ID, id_foglio,data,valutativo,tuttelerisposte,Codice,Testo,tot1_pos,tot1_neg,tot1_prompt,tot1,tot2_pos,tot2_neg,tot2_prompt,tot2,COMPILATO,termine " &
                "FROM GIORNALIERO " &
                "WHERE Codice LIKE @Testo"
            Using cmd As New SqliteCommand(sql, cn)
                ' Il carattere % indica a SQLite di trovare qualsiasi carattere prima e dopo il testo
                cmd.Parameters.AddWithValue("@Testo", "%" & testoCercato & "%")

                Using rd = cmd.ExecuteReader()

                    ' Usa While al posto di If perché ci possono essere più record corrispondenti
                    While rd.Read()
                        ' Crea una nuova istanza della classe per ogni riga trovata
                        Dim record As New Class_GIORNALIERO()
                        record.ID = Convert.ToInt32(rd("ID"))
                        record.ID = Convert.ToInt32(rd("ID_FOGLIO"))
                        record.Testo = Convert.ToString(rd("TESTO"))
                        record.Codice = Convert.ToString(rd("CODICE"))
                        record.TOT1_POS = Convert.ToString(rd("TOT1_pos"))
                        record.TOT1_NEG = Convert.ToString(rd("TOT1_neg"))
                        record.TOT1_PROMPT = Convert.ToString(rd("TOT1_prompt"))
                        record.TOT1 = Convert.ToString(rd("TOT1"))
                        record.TOT2_POS = Convert.ToString(rd("TOT1_pos"))
                        record.TOT2_NEG = Convert.ToString(rd("TOT1_neg"))
                        record.TOT2_PROMPT = Convert.ToString(rd("TOT1_prompt"))
                        record.TOT2 = Convert.ToString(rd("TOT1"))
                        record.DATA = Convert.ToString(rd("data"))
                        record.VALUTATIVO = Convert.ToBoolean(rd("valutativo"))
                        record.TUTTELERISPOSTE = Convert.ToBoolean(rd("TUTTELERISPOSTE"))
                        record.COMPILATO = Convert.ToBoolean(rd("COMPILATO"))


                        ' Aggiunge il record alla lista dei risultati solo se attivi
                        listaRisultati.Add(record)
                    End While

                End Using
            End Using
        End Using
        Return listaRisultati
    End Function
    Public Shared Function Trova_ultimo_IDFOGLIO() As Integer
        ' Valore predefinito se non vengono trovati record per quel codice ELCAR
        Dim ultimoId As Integer = 0

        ' Se il codice passato è vuoto o nullo, restituisce subito 0

        Using cn = DB.GetConnection_Bambino
            ' Applica il filtro WHERE sulla colonna ELCAR e converte ID_SUB in INTEGER per il calcolo del MAX
            Dim sql As String = "SELECT MAX(CAST(ID_FOGLIO AS INTEGER)) " &
                                "FROM GIORNALIERO "

            Using cmd As New SqliteCommand(sql, cn)
                Dim risultato = cmd.ExecuteScalar()
                If risultato IsNot Nothing AndAlso risultato IsNot DBNull.Value Then
                    ultimoId = Convert.ToInt32(risultato)
                End If
            End Using
        End Using

        Return ultimoId
    End Function

    Public Function Salva() As Boolean
        Using cn = DB.GetConnection_Bambino
            cn.Open()
            Dim sql As String = ""

            ' Determina se inserire un nuovo record o aggiornare quello esistente
            If Me.ID = 0 Then
                sql = "INSERT INTO GIORNALIERO (ID_FOGLIO, DATA, VALUTATIVO, TUTTELERISPOSTE, Codice, Testo, " &
                      "TOT1_POS, TOT1_NEG, TOT1_PROMPT, TOT1,TOT2_POS, TOT2_NEG, TOT2_PROMPT, TOT2, COMPILATO, TERMINE) " &
                      "VALUES (@ID_FOGLIO, @DATA, @VALUTATIVO, @TUTTELERISPOSTE, @Codice, @Testo, " &
                      "@TOT1_POS, @TOT1_NEG, @TOT1_PROMPT, @TOT1,@TOT2_POS, @TOT2_NEG, @TOT2_PROMPT, @TOT2, @COMPILATO, @TERMINE); " &
                      "SELECT last_insert_rowid();"
            Else
                sql = "UPDATE GIORNALIERO SET " &
                      "ID_FOGLIO = @ID_FOGLIO, DATA = @DATA, VALUTATIVO = @VALUTATIVO, " &
                      "TUTTELERISPOSTE = @TUTTELERISPOSTE, Codice = @Codice, Testo = @Testo, " &
                      "TOT1_POS = @TOT1_POS, TOT1_NEG = @TOT1_NEG, TOT1_PROMPT = @TOT1_PROMPT,TOT1 = @TOT1, " &
                      "TOT2_POS = @TOT2_POS, TOT2_NEG = @TOT2_NEG, TOT2_PROMPT = @TOT2_PROMPT,TOT2 = @TOT2, " &
                      "COMPILATO = @COMPILATO, TERMINE = @TERMINE " &
                      "WHERE ID = @ID;"
            End If

            Using cmd As New SqliteCommand(sql, cn)
                Try
                    ' Assegnazione dei parametri comuni a entrambe le query
                    cmd.Parameters.AddWithValue("@ID_FOGLIO", Me.ID_FOGLIO)
                    cmd.Parameters.AddWithValue("@DATA", Me.DATA)
                    ' SQLite gestisce i Boolean nativamente come Integer (0 o 1)
                    cmd.Parameters.AddWithValue("@VALUTATIVO", If(Me.VALUTATIVO, 1, 0))
                    cmd.Parameters.AddWithValue("@TUTTELERISPOSTE", If(Me.TUTTELERISPOSTE, 1, 0))
                    cmd.Parameters.AddWithValue("@Codice", Me.Codice)
                    cmd.Parameters.AddWithValue("@Testo", Me.Testo)
                    cmd.Parameters.AddWithValue("@TOT1_POS", Me.TOT1_POS)
                    cmd.Parameters.AddWithValue("@TOT1_NEG", Me.TOT1_NEG)
                    cmd.Parameters.AddWithValue("@TOT1_PROMPT", Me.TOT1_PROMPT)
                    cmd.Parameters.AddWithValue("@TOT1", Me.TOT1)
                    cmd.Parameters.AddWithValue("@TOT2_POS", Me.TOT2_POS)
                    cmd.Parameters.AddWithValue("@TOT2_NEG", Me.TOT2_NEG)
                    cmd.Parameters.AddWithValue("@TOT2_PROMPT", Me.TOT2_PROMPT)
                    cmd.Parameters.AddWithValue("@TOT2", Me.TOT2)
                    cmd.Parameters.AddWithValue("@COMPILATO", If(Me.COMPILATO, 1, 0))
                    cmd.Parameters.AddWithValue("@TERMINE", If(Me.TERMINE, 1, 0))


                Catch ex As Exception
                    Return -1
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


    Public Function Carica_Foglio(idFoglio As Integer) As List(Of Class_GIORNALIERO)

        Dim lista As New List(Of Class_GIORNALIERO)

        Dim sql As String =
            "SELECT * " &
            "FROM GIORNALIERO " &
            "WHERE ID_Foglio = @ID_Foglio " &
            "ORDER BY ID"

        Using cn = DB.GetConnection_Bambino
            cn.Open()

            Using cmd As New SqliteCommand(sql, cn)

                cmd.Parameters.AddWithValue(
                    "@ID_Foglio",
                    idFoglio
                )

                Using reader As SqliteDataReader =
                    cmd.ExecuteReader()

                    While reader.Read()

                        Dim record As New Class_GIORNALIERO With {
                            .ID = Convert.ToInt32(reader("ID")),
                            .ID_FOGLIO = Convert.ToInt32(reader("ID_Foglio")),
                            .DATA = reader("DATA").ToString(),
                            .VALUTATIVO = Convert.ToBoolean(reader("VALUTATIVO")),
                            .TUTTELERISPOSTE = Convert.ToBoolean(reader("TUTTELERISPOSTE")),
                            .Codice = reader("Codice").ToString(),
                            .Testo = reader("Testo").ToString(),
                            .TOT1_POS = Convert.ToInt32(reader("TOT1_POS")),
                            .TOT1_NEG = Convert.ToInt32(reader("TOT1_NEG")),
                            .TOT1_PROMPT = Convert.ToInt32(reader("TOT1_PROMPT")),
                            .TOT1 = Convert.ToInt32(reader("TOT1")),
                            .TOT2_POS = Convert.ToInt32(reader("TOT2_POS")),
                            .TOT2_NEG = Convert.ToInt32(reader("TOT2_NEG")),
                            .TOT2_PROMPT = Convert.ToInt32(reader("TOT2_PROMPT")),
                            .TOT2 = Convert.ToInt32(reader("TOT2")),
                            .COMPILATO = Convert.ToBoolean(reader("COMPILATO")),
                            .TERMINE = Convert.ToBoolean(reader("TERMINE"))
                        }

                        lista.Add(record)

                    End While

                End Using

            End Using

        End Using

        Return lista

    End Function

    Public Shared Function OttieniRecordPerDate(data1 As DateTime, data2 As DateTime) As DataTable
        Dim dt As New DataTable()


        Dim query As String = "SELECT * FROM Giornaliero WHERE DATA BETWEEN @DataInizio AND @DataFine ORDER BY Codice, Data"
        query = "
        SELECT * 
        FROM Giornaliero 
        WHERE (SUBSTR(DATA, 7, 4) || '-' || SUBSTR(DATA, 4, 2) || '-' || SUBSTR(DATA, 1, 2)) 
        BETWEEN @DataInizio AND @DataFine 
        ORDER BY Codice, DATA"
        Using cn = DB.GetConnection_Bambino()

            Using cmd As New SqliteCommand(query, cn)
                cmd.Parameters.AddWithValue("@DataInizio", data1.ToString("yyyy-MM-dd"))
                cmd.Parameters.AddWithValue("@DataFine", data2.ToString("yyyy-MM-dd"))
                'cmd.Parameters.AddWithValue("@DataInizio", data1.ToString("dd/MM/yyyy"))
                'cmd.Parameters.AddWithValue("@DataFine", data2.ToString("dd/MM/yyyy"))

                Using reader As SqliteDataReader = cmd.ExecuteReader()
                    dt.Load(reader)
                End Using
            End Using
        End Using

        Return dt
    End Function

    Public Shared Function OttieniRecordPerCodice(codice As String) As DataTable
        If codice = "" Then Return Nothing

        Dim dt As New DataTable()



        Dim query As String = "SELECT * FROM Giornaliero WHERE CODICE = @codice ORDER BY SUBSTR(DATA, 7, 4) || '-' || SUBSTR(DATA, 4, 2) || '-' || SUBSTR(DATA, 1, 2) ASC"
        Using cn = DB.GetConnection_Bambino()

            Using cmd As New SqliteCommand(query, cn)
                ' Aggiungiamo il parametro in modo sicuro
                cmd.Parameters.AddWithValue("@codice", codice)

                Using reader As SqliteDataReader = cmd.ExecuteReader()
                    dt.Load(reader)
                End Using
            End Using
        End Using

        Return dt
    End Function
    Public Shared Function OttieniRecordOrderbydata(Optional soloNoncompilati As Boolean = False) As DataTable

        Dim dt As New DataTable()

        Dim Query = ""
        If soloNoncompilati Then
            Query = "SELECT * FROM Giornaliero WHERE COMPILATO = false GROUP BY ID_FOGLIO ORDER BY SUBSTR(DATA, 7, 4) || '-' || SUBSTR(DATA, 4, 2) || '-' || SUBSTR(DATA, 1, 2) DESC"
        Else
            Query = "SELECT * FROM Giornaliero ORDER BY SUBSTR(DATA, 7, 4) || '-' || SUBSTR(DATA, 4, 2) || '-' || SUBSTR(DATA, 1, 2) ASC"
        End If
        Using cn = DB.GetConnection_Bambino()

            Using cmd As New SqliteCommand(query, cn)
                ' Aggiungiamo il parametro in modo sicuro
                Try
                    Using reader As SqliteDataReader = cmd.ExecuteReader()
                        dt.Load(reader)
                    End Using
                Catch ex As Exception
                    Dim m = "Errore nell'apertura della query in OttieniRecordOrderbydata (" + cn.ConnectionString + ")"
                    Call APPEND_LOG(m, GbL_Logfile, True)
                    'm = "Errore nell'esecuzione della query  in OttieniRecordOrderbydata:" + vbCrLf + "in: " + cn.ConnectionString
                    'Call MsgBox(m, vbOK + vbCritical)
                End Try
            End Using
        End Using

        Return dt
    End Function

    Public Shared Function OttienTitoli() As DataTable

        Dim dt As New DataTable()



        'Dim query As String = "Select Case DISTINCT titolo FROM FROM Giornaliero order by titolo"
        'Dim query As String = "SELECT DISTINCT titolo, COUNT(titolo) As quante_volte, GROUP_CONCAT(ID_Foglio, ';') AS listaid FROM Giornaliero GROUP BY titolo ORDER BY titolo ASC"
        Dim query As String = "SELECT DISTINCT titolo FROM Giornaliero GROUP BY titolo ORDER BY titolo ASC"
        Using cn = DB.GetConnection_Bambino()

            Using cmd As New SqliteCommand(query, cn)

                Using reader As SqliteDataReader = cmd.ExecuteReader()
                    dt.Load(reader)
                End Using
            End Using
        End Using

        Return dt
    End Function

    Public Shared Function Elenco_fogli_non_compilati() As List(Of Class_GIORNALIERO)
        ' Trova_elenco_fogli_da_compilare tutti i fogli non compilati di un bambino

        Dim listaGiornalieri As New List(Of Class_GIORNALIERO)()

        Dim query As String = "SELECT MIN(ID) AS ID, ID_FOGLIO, " &
                      "substr(MIN(DATA), 7, 4) || '-' || substr(MIN(DATA), 4, 2) || '-' || substr(MIN(DATA), 1, 2) AS DATA " &
                      "FROM GIORNALIERO " &
                      "WHERE COMPILATO = 0 " &
                      "GROUP BY ID_FOGLIO " &
                      "ORDER BY ID_FOGLIO DESC;"
        Using cn = DB.GetConnection_Bambino()
            Using cmd As New SqliteCommand(query, cn)
                ' Esegui il comando SQLite...
                Using reader As SqliteDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim elemento As New Class_GIORNALIERO() With {
                            .ID = Convert.ToInt32(reader("ID")),
                            .ID_FOGLIO = Convert.ToInt32(reader("ID_FOGLIO")),
                            .DATA = reader("DATA").ToString()
                        }
                        listaGiornalieri.Add(elemento)
                    End While
                End Using
            End Using
        End Using
        Return listaGiornalieri
    End Function


End Class
