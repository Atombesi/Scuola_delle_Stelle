Imports Microsoft.Data.Sqlite

Public Class Class_ELCAR_VOCI_PERSONALI
    Public Property Testo As String
    Public Property Testo_Lungo As String
    Public Property Deleted As Boolean
    Public Property ID As Integer
    Public Property ELCAR As String
    Public Property ID_SUB As String

    'Private ReadOnly ConnectionString As String =
    '   "Server=.\SQLEXPRESS;Database=Scuoladellestelle;Trusted_Connection=True;"


    Public Shared Function Trova_ultimo_IDSUB(codiceElcar As String) As Integer
        ' Valore predefinito se non vengono trovati record per quel codice ELCAR
        Dim ultimoIdSub As Integer = 0

        ' Se il codice passato è vuoto o nullo, restituisce subito 0
        If String.IsNullOrEmpty(codiceElcar) Then Return 0

        Using cn = DB.GetConnection_Bambino
            ' Applica il filtro WHERE sulla colonna ELCAR e converte ID_SUB in INTEGER per il calcolo del MAX
            Dim sql As String = "SELECT MAX(CAST(ID_SUB AS INTEGER)) " &
                                "FROM ELCAR_VOCI_PERSONALI " &
                                "WHERE ELCAR = @ELCAR"

            Using cmd As New SqliteCommand(sql, cn)
                ' Assegna il codice passato come parametro alla query
                cmd.Parameters.AddWithValue("@ELCAR", codiceElcar)

                Try
                    Dim risultato As Object = cmd.ExecuteScalar()

                    ' Controlla che il database abbia restituito un valore valido
                    If risultato IsNot Nothing AndAlso Not IsDBNull(risultato) Then
                        ultimoIdSub = Convert.ToInt32(risultato)
                    End If
                Catch ex As Exception
                    ' In caso di errore restituisce 0
                    ultimoIdSub = 0
                End Try
            End Using
        End Using

        Return ultimoIdSub
    End Function

    Public Shared Function Trova_ultimo_IDSUB_Manuale() As Integer
        Dim codice = Costruisci_codice_finale()
        Dim CodiceElcar = ""
        ' Valore predefinito se non vengono trovati record per quel codice ELCAR
        Dim ultimoIdSub As Integer = 0
        Dim x = Split(codice, ".")
        Dim n = x.Count - 1
        If n = 5 Then
            CodiceElcar = cod(1) + "." + cod(2) + "." + cod(3) + "." + cod(4) + "." + cod(5)
        ElseIf n = 4 Then
            CodiceElcar = codice
        ElseIf n = 3 Then
            CodiceElcar = cod(1) + "." + cod(2) + "." + cod(3) + ".."
        ElseIf n = 2 Then
            CodiceElcar = cod(1) + "." + cod(2) + "..."
        End If
        ' Se il codice passato è vuoto o nullo, restituisce subito 0
        If String.IsNullOrEmpty(CodiceElcar) Then Return 0

        Using cn = DB.GetConnection_Bambino
            ' Applica il filtro WHERE sulla colonna ELCAR e converte ID_SUB in INTEGER per il calcolo del MAX
            Dim sql As String = "SELECT MAX(CAST(ID_SUB AS INTEGER)) " &
                                "FROM ELCAR_VOCI_PERSONALI " &
                                "WHERE ELCAR LIKE @Testo"


            Using cmd As New SqliteCommand(sql, cn)
                ' Assegna il codice passato come parametro alla query
                cmd.Parameters.AddWithValue("@Testo", CodiceElcar)

                Try
                    Dim risultato As Object = cmd.ExecuteScalar()

                    ' Controlla che il database abbia restituito un valore valido
                    If risultato IsNot Nothing AndAlso Not IsDBNull(risultato) Then
                        ultimoIdSub = Convert.ToInt32(risultato)
                    End If
                Catch ex As Exception
                    ' In caso di errore restituisce 0
                    ultimoIdSub = 0
                End Try
            End Using
        End Using

        Return ultimoIdSub
    End Function

    Public Shared Function Trova_ID(elcarCercato As String, codiceVoceSubCercato As String) As Integer
        ' Definisci la stringa di connessione (modifica il percorso con il tuo file .db)

        ' La query SQL con i parametri contrassegnati dal simbolo @
        Dim query As String = "SELECT ID FROM ELCAR_VOCI_Personali WHERE ELCAR = @elcar AND ID_SUB = @codice"

        Try
            ' Apri la connessione e prepara il comando
            Using cn = DB.GetConnection_Bambino
                cn.Open()

                Using cmd As New SqliteCommand(query, cn)
                    ' Assegna in modo sicuro i valori ai parametri SQL
                    cmd.Parameters.AddWithValue("@elcar", elcarCercato)
                    cmd.Parameters.AddWithValue("@codice", codiceVoceSubCercato)

                    ' ExecuteScalar esegue la query e restituisce la prima colonna della prima riga (l'ID)
                    Dim risultato As Object = cmd.ExecuteScalar()

                    ' Se il risultato non è nullo, restituisci l'ID convertito in Integer
                    If risultato IsNot DBNull.Value AndAlso risultato IsNot Nothing Then
                        Return Convert.ToInt32(risultato)
                    End If
                End Using
            End Using
        Catch ex As Exception
            ' Gestione dell'errore (puoi loggarlo o mostrarlo a schermo)
            MessageBox.Show("Errore durante la ricerca dell'ID: " & ex.Message)
        End Try

        ' Restituisce -1 se il record non è stato trovato o se si è verificato un errore
        Return -1
    End Function


    Public Shared Function Load(id As Integer) As Class_ELCAR_VOCI_PERSONALI
        ' 1. Dichiara l'oggetto all'inizio della funzione (visibile ovunque)
        Dim voce As Class_ELCAR_VOCI_PERSONALI = Nothing

        Using cn = DB.GetConnection_Bambino
            Dim sql As String =
        "SELECT ID, ELCAR, ID_SUB, Testo, Testo_Lungo, Deleted " &
        "FROM ELCAR_VOCI_PERSONALI " &
        "WHERE ID = @ID"

            Using cmd As New SqliteCommand(sql, cn)
                cmd.Parameters.AddWithValue("@ID", id)

                Using rd = cmd.ExecuteReader()
                    If rd.Read() Then
                        ' 2. Se il record esiste, inizializza l'istanza
                        voce = New Class_ELCAR_VOCI_PERSONALI()

                        ' 3. Popola le proprietà
                        voce.ID = Convert.ToInt32(rd("ID"))
                        voce.ELCAR = Convert.ToString(rd("ELCAR"))

                        ' Nota: se CODICE_VOCESUB è una String nella classe, usa Convert.ToString
                        ' Se invece è un Integer/Short nella classe, Convert.ToInt16 va bene.
                        voce.ID_SUB = Convert.ToString(rd("ID_SUB"))

                        voce.Testo = Convert.ToString(rd("Testo"))
                        voce.Testo_Lungo = Convert.ToString(rd("Testo_Lungo"))
                        voce.Deleted = False
                        Try
                            voce.Deleted = Convert.ToBoolean(rd("Deleted"))
                        Catch ex As Exception

                        End Try

                    End If
                End Using
            End Using
        End Using

        ' 4. Restituisce l'oggetto (sarà popolato se trovato, altrimenti rimarrà Nothing)
        Return voce
    End Function

    Public Shared Function CercaPerCodice(testoCercato As String, Optional testoesatto As Boolean = False) As List(Of Class_ELCAR_VOCI_PERSONALI)
        'ID As Integer
        'ELCAR As String
        'ID_SUB as string
        'CODICE_VOCE  As String
        'Testo As String
        'Testo:Lungo As String

        Dim listaRisultati As New List(Of Class_ELCAR_VOCI_PERSONALI)()
        Dim sql As String =
                "SELECT ID, ELCAR,ID_SUB,Testo,Testo_Lungo,deleted " &
                "FROM ELCAR_VOCI_Personali " &
                "WHERE ELCAR LIKE @Testo"

        If testoesatto Then
            sql =
                "SELECT ID, ELCAR,ID_SUB,Testo,Testo_Lungo,deleted " &
                "FROM ELCAR_VOCI_Personali " &
                "WHERE ELCAR = @Testo"
        End If

        Using cn = DB.GetConnection_Bambino()
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
                        Dim record As New Class_ELCAR_VOCI_PERSONALI()
                        record.Testo = Convert.ToString(rd("TESTO"))
                        record.Testo_Lungo = Convert.ToString(rd("TESTO_LUNGO"))
                        record.ELCAR = Convert.ToString(rd("elcar"))
                        record.ID_SUB = Convert.ToString(rd("ID_SUB"))
                        record.ID = Convert.ToInt32(rd("ID"))
                        Try
                            record.Deleted = Convert.ToBoolean(rd("Deleted"))
                        Catch ex As Exception
                            record.Deleted = False
                        End Try
                        ' Aggiunge il record alla lista dei risultati solo se attivi
                        listaRisultati.Add(record)
                    End While

                End Using
            End Using
        End Using
        Return listaRisultati
    End Function
    Public Function Save() As Boolean
        ' Se l'oggetto passato è nullo, esce subito restituendo False
        If Me Is Nothing Then Return False

        Using cn = DB.GetConnection_Bambino()
            Dim sql As String = ""

            ' Controlla se inserire un nuovo record o aggiornare uno esistente
            If Me.ID = 0 Then
                ' QUERY DI INSERIMENTO
                ' Nota: "last_insert_rowid()" restituisce l'ID generato automaticamente da SQLite
                sql = "INSERT INTO ELCAR_VOCI_Personali (ELCAR, ID_SUB, Testo, Testo_Lungo, Deleted) " &
                  "VALUES (@ELCAR, @ID_SUB, @Testo, @Testo_Lungo, @Deleted); " &
                  "SELECT last_insert_rowid();"
            Else
                ' QUERY DI AGGIORNAMENTO
                sql = "UPDATE ELCAR_VOCI_Personali " &
                  "SET ELCAR = @ELCAR, ID_SUB = @ID_SUB, Testo = @Testo, Testo_Lungo = @Testo_Lungo, Deleted = @Deleted " &
                  "WHERE ID = @ID;"
            End If

            Using cmd As New SqliteCommand(sql, cn)
                ' Assegna i parametri comuni a entrambe le query
                cmd.Parameters.AddWithValue("@ELCAR", If(Me.ELCAR, DBNull.Value))
                cmd.Parameters.AddWithValue("@ID_SUB", Me.ID_SUB)
                cmd.Parameters.AddWithValue("@Testo", If(Me.Testo, DBNull.Value))
                cmd.Parameters.AddWithValue("@Testo_Lungo", If(Me.Testo_Lungo, DBNull.Value))
                cmd.Parameters.AddWithValue("@Deleted", Me.Deleted)

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
        End Using
    End Function

End Class

