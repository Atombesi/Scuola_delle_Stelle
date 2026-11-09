Imports Microsoft.Data.Sqlite

Public Class Class_ELCAR_VOCI
    Public Property ID As Integer
    Public Property CODICE_ELCAR As String
    Public Property CODICE_VOCE As String
    Public Property Testo As String
    Public Property Testo_Lungo As String
    Private ReadOnly ConnectionString As String =
       "Server=.\SQLEXPRESS;Database=Scuoladellestelle;Trusted_Connection=True;"



    Public Shared Function CercaPerCodice(testoCercato As String, Optional testoesatto As Boolean = False) As List(Of Class_ELCAR_VOCI)
        'ID As Integer
        'CODICE_ELCAR As String
        'CODICE_VOCE  As String
        'Testo As String
        'Testo:Lungo As String

        Dim listaRisultati As New List(Of Class_ELCAR_VOCI)()
        Dim sql As String =
                "SELECT ID, CODICE_ELCAR,CODICE_VOCE,Testo,Testo_Lungo " &
                "FROM ELCAR_VOCI " &
                "WHERE CODICE_ELCAR LIKE @Testo"

        If testoesatto Then
            sql =
                "SELECT ID, CODICE_ELCAR,CODICE_VOCE,Testo,Testo_Lungo " &
                "FROM ELCAR_VOCI " &
                "WHERE CODICE_ELCAR = @Testo"
        End If

        Using cn = MY_DB.GetConnection_Scuoladellestelle()
            ' Usa l'operatore LIKE per cercare corrispondenze parziali
            Using cmd As New SqliteCommand(sql, cn)
                ' Il carattere % indica a SQLite di trovare qualsiasi carattere prima e dopo il testo
                If testoesatto Then
                    cmd.Parameters.AddWithValue("@Testo", testoCercato)
                Else
                    cmd.Parameters.AddWithValue("@Testo", "%" & testoCercato & "%")
                End If

                Using rd = cmd.ExecuteReader()

                    ' Usa While al posto di If perché ci possono essere più record corrispondenti
                    While rd.Read()
                        ' Crea una nuova istanza della classe per ogni riga trovata
                        Dim record As New Class_ELCAR_VOCI()
                        record.Testo = Convert.ToString(rd("TESTO"))
                        record.Testo_Lungo = Convert.ToString(rd("TESTO_LUNGO"))
                        record.CODICE_ELCAR = Convert.ToString(rd("Codice_elcar"))
                        record.CODICE_VOCE = Convert.ToString(rd("Codice_voce"))
                        record.ID = Convert.ToInt32(rd("ID"))
                        ' Aggiunge il record alla lista dei risultati solo se attivi
                        listaRisultati.Add(record)
                    End While

                End Using
            End Using
        End Using
        Return listaRisultati
    End Function

    Public Shared Function CercaPerCodice_Esatto(testoCercato As String, codice_voce As String) As Class_ELCAR_VOCI
        'ID As Integer
        'CODICE_ELCAR As String
        'CODICE_VOCE  As String
        'Testo As String
        'Testo:Lungo As String

        Dim listaRisultati As New List(Of Class_ELCAR_VOCI)()
        Dim sql As String =
                "SELECT ID, CODICE_ELCAR,CODICE_VOCE,Testo,Testo_Lungo " &
                "FROM ELCAR_VOCI " &
                "WHERE CODICE_ELCAR = @Testo and CODICE_VOCE = @codice_voce"

        Using cn = MY_DB.GetConnection_Scuoladellestelle()
            ' Usa l'operatore LIKE per cercare corrispondenze parziali
            Using cmd As New SqliteCommand(sql, cn)
                ' Il carattere % indica a SQLite di trovare qualsiasi carattere prima e dopo il testo
                cmd.Parameters.AddWithValue("@Testo", testoCercato)
                cmd.Parameters.AddWithValue("@codice_voce", codice_voce)

                Using rd = cmd.ExecuteReader()
                    ' Usa While al posto di If perché ci possono essere più record corrispondenti
                    While rd.Read()
                        ' Crea una nuova istanza della classe per ogni riga trovata
                        Dim record As New Class_ELCAR_VOCI()
                        record.Testo = Convert.ToString(rd("TESTO"))
                        record.Testo_Lungo = Convert.ToString(rd("TESTO_LUNGO"))
                        record.CODICE_ELCAR = Convert.ToString(rd("Codice_elcar"))
                        record.CODICE_VOCE = Convert.ToString(rd("Codice_voce"))
                        record.ID = Convert.ToInt32(rd("ID"))
                        ' Aggiunge il record alla lista dei risultati solo se attivi
                        Return record
                    End While

                End Using
            End Using
        End Using

    End Function

    Public Shared Function GetALL_ByID() As List(Of Class_ELCAR_VOCI)

        Dim listaRisultati As New List(Of Class_ELCAR_VOCI)()
        Dim sql As String =
                "SELECT * FROM ELCAR_VOCI ORDER BY ID"

        Dim cn = MY_DB.GetConnection_Scuoladellestelle()
        Using cmd As New SqliteCommand(sql, cn)
            Using rd = cmd.ExecuteReader()
                ' Usa While al posto di If perché ci possono essere più record corrispondenti
                While rd.Read()
                    ' Crea una nuova istanza della classe per ogni riga trovata
                    Dim record As New Class_ELCAR_VOCI()
                    record.Testo = Convert.ToString(rd("TESTO"))
                    record.Testo_Lungo = Convert.ToString(rd("TESTO_LUNGO"))
                    record.CODICE_ELCAR = Convert.ToString(rd("Codice_elcar"))
                    record.CODICE_VOCE = Convert.ToString(rd("Codice_voce"))
                    Try
                        record.ID = Convert.ToInt32(rd("ID"))
                    Catch ex As Exception
                        record.ID = 0
                    End Try
                    ' Aggiunge il record alla lista dei risultati solo se attivi
                    listaRisultati.Add(record)
                End While
                Return listaRisultati
            End Using
        End Using
    End Function

    Public Function Save() As Boolean
        ' Se l'oggetto passato è nullo, esce subito restituendo False
        If Me Is Nothing Then Return False

        Dim cn = MY_DB.GetConnection_Scuoladellestelle()
        Dim sql As String = ""

        ' Controlla se inserire un nuovo record o aggiornare uno esistente
        If Me.ID = 0 Then
            ' QUERY DI INSERIMENTO
            ' Nota: "last_insert_rowid()" restituisce l'ID generato automaticamente da SQLite
            sql = "INSERT INTO ELCAR_VOCI (CODICE_ELCAR, CODICE_VOCE, Testo, Testo_Lungo) " &
                  "VALUES (@CODICE_ELCAR, @CODICE_VOCE, @Testo, @Testo_Lungo); " &
                  "SELECT last_insert_rowid();"
        Else
            ' QUERY DI AGGIORNAMENTO
            sql = "UPDATE ELCAR_VOCI " &
                  "SET ELCAR = @CODICE_ELCAR, CODICE_VOCE = @CODICE_VOCE, Testo = @Testo, Testo_Lungo = @Testo_Lungo " &
                  "WHERE ID = @ID;"
        End If

        Using cmd As New SqliteCommand(sql, cn)
            ' Assegna i parametri comuni a entrambe le query
            cmd.Parameters.AddWithValue("@CODICE_ELCAR", If(Me.CODICE_ELCAR, DBNull.Value))
            cmd.Parameters.AddWithValue("@CODICE_VOCE", Me.CODICE_VOCE)
            cmd.Parameters.AddWithValue("@Testo", If(Me.Testo, DBNull.Value))
            cmd.Parameters.AddWithValue("@Testo_Lungo", If(Me.Testo_Lungo, DBNull.Value))

            Try
                If Me.ID = 0 Then
                    ' Esegue la INSERT e legge l'ID autogenerato da SQLite
                    Dim nuovoId As Object = cmd.ExecuteScalar()
                    If nuovoId IsNot Nothing AndAlso Not IsDBNull(nuovoId) Then
                        Me.ID = Convert.ToInt32(nuovoId)
                        Return True
                    End If
                    Return False
                Else
                    ' Aggiunge il parametro @ID necessario solo per la UPDATE
                    cmd.Parameters.AddWithValue("@ID", Me.ID)

                    ' Esegue la UPDATE e controlla che abbia modificato almeno una riga
                    Dim righeInteressate As Integer = cmd.ExecuteNonQuery()
                    Return righeInteressate > 0
                End If
            Catch ex As Exception
                ' Gestione dell'errore (puoi loggarlo o gestirlo come preferisci)
                ' Throw ex ' Scommenta questa riga se vuoi bloccare l'app in caso di errore DB
                Return False
            End Try
        End Using
    End Function

End Class

