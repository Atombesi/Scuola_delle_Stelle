Imports Microsoft.Data.Sqlite
Public Class Class_GIORNALIERO
    Public Property ID As Integer
    Public Property ID_FOGLIO As Integer
    Public Property DATA As String
    Public Property VALUTATIVO As Boolean
    Public Property TUTTELERISPOSTE As Boolean
    Public Property Codice As String
    Public Property Testo As String
    Public Property TOT_POS As Integer
    Public Property TOT_NEG As Integer
    Public Property TOT_PROMPT As Integer
    Public Property TOT As Integer
    Public Property INTERRUZIONE As Boolean
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
                "SELECT ID, id_foglio,data,valutativo,tuttelerisposte,Codice,Testo,tot_pos,tot_neg,tot_prompt,tot,interruzione,termine " &
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
                        record.TOT_POS = Convert.ToString(rd("TOT_pos"))
                        record.TOT_NEG = Convert.ToString(rd("TOT_neg"))
                        record.TOT_PROMPT = Convert.ToString(rd("TOT_prompt"))
                        record.TOT = Convert.ToString(rd("TOT"))
                        record.DATA = Convert.ToString(rd("data"))
                        record.VALUTATIVO = Convert.ToBoolean(rd("valutativo"))
                        record.TUTTELERISPOSTE = Convert.ToBoolean(rd("TUTTELERISPOSTE"))

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
                      "TOT_POS, TOT_NEG, TOT_PROMPT, TOT, INTERRUZIONE, TERMINE) " &
                      "VALUES (@ID_FOGLIO, @DATA, @VALUTATIVO, @TUTTELERISPOSTE, @Codice, @Testo, " &
                      "@TOT_POS, @TOT_NEG, @TOT_PROMPT, @TOT, @INTERRUZIONE, @TERMINE); " &
                      "SELECT last_insert_rowid();"
            Else
                sql = "UPDATE GIORNALIERO SET " &
                      "ID_FOGLIO = @ID_FOGLIO, DATA = @DATA, VALUTATIVO = @VALUTATIVO, " &
                      "TUTTELERISPOSTE = @TUTTELERISPOSTE, Codice = @Codice, Testo = @Testo, " &
                      "TOT_POS = @TOT_POS, TOT_NEG = @TOT_NEG, TOT_PROMPT = @TOT_PROMPT, " &
                      "TOT = @TOT, INTERRUZIONE = @INTERRUZIONE, TERMINE = @TERMINE " &
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
                    cmd.Parameters.AddWithValue("@TOT_POS", Me.TOT_POS)
                    cmd.Parameters.AddWithValue("@TOT_NEG", Me.TOT_NEG)
                    cmd.Parameters.AddWithValue("@TOT_PROMPT", Me.TOT_PROMPT)
                    cmd.Parameters.AddWithValue("@TOT", Me.TOT)
                    cmd.Parameters.AddWithValue("@INTERRUZIONE", If(Me.INTERRUZIONE, 1, 0))
                    cmd.Parameters.AddWithValue("@TERMINE", If(Me.TERMINE, 1, 0))


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
                            .TOT_POS = Convert.ToInt32(reader("TOT_POS")),
                            .TOT_NEG = Convert.ToInt32(reader("TOT_NEG")),
                            .TOT_PROMPT = Convert.ToInt32(reader("TOT_PROMPT")),
                            .TOT = Convert.ToInt32(reader("TOT")),
                            .INTERRUZIONE = Convert.ToBoolean(reader("INTERRUZIONE")),
                            .TERMINE = Convert.ToBoolean(reader("TERMINE"))
                        }

                        lista.Add(record)

                    End While

                End Using

            End Using

        End Using

        Return lista

    End Function
End Class
