Imports Microsoft.Data.Sqlite
Public Class Class_ELCAR
    Public Property ID As Integer
    Public Property Codice As String
    Public Property Testo As String
    Public Property Testo_Lungo As String

    Private ReadOnly ConnectionString As String =
       "Server=.\SQLEXPRESS;Database=Scuoladellestelle;Trusted_Connection=True;"



    Public Shared Function Load(id As Integer) As Class_ELCAR

        Using cn = MY_DB.GetConnection_Scuoladellestelle
            Dim sql As String =
                "SELECT ID, CODICE,Testo,Testo_Lungo FROM ELCAR WHERE ID = @ID"
            Using cmd As New SqliteCommand(sql, cn)
                cmd.Parameters.AddWithValue("@ID", id)
                Using rd = cmd.ExecuteReader()
                    If rd.Read() Then
                        Dim record As New Class_ELCAR()
                        record.ID = Convert.ToInt32(rd("ID"))
                        record.Codice = Convert.ToString(rd("Codice"))
                        record.Testo = Convert.ToString(rd("Testo"))
                        record.Testo_Lungo = Convert.ToString(rd("Testo_Lungo"))
                        Return record
                    Else
                        Return Nothing
                    End If
                End Using
            End Using
        End Using
    End Function

    Public Function Save() As Boolean
        ' Se l'oggetto passato è nullo, esce subito restituendo False
        If Me Is Nothing Then Return False
        Using cn = MY_DB.GetConnection_Scuoladellestelle
            Dim sql As String = ""
            ' Controlla se inserire un nuovo record o aggiornare uno esistente
            If Me.ID = 0 Then
                ' QUERY DI INSERIMENTO
                ' Nota: "last_insert_rowid()" restituisce l'ID generato automaticamente da SQLite
                sql = "INSERT INTO ELCAR (CODICE, Testo, Testo_Lungo) " &
                  "VALUES (@CODICE, @Testo, @Testo_Lungo); " &
                  "SELECT last_insert_rowid();"
            Else
                ' QUERY DI AGGIORNAMENTO
                sql = "UPDATE ELCAR " &
                  "SET CODICE = @CODICE, Testo = @Testo, Testo_Lungo = @Testo_Lungo " &
                  "WHERE ID = @ID;"
            End If

            Using cmd As New SqliteCommand(sql, cn)
                ' Assegna i parametri comuni a entrambe le query
                cmd.Parameters.AddWithValue("@CODICE", If(Me.Codice, DBNull.Value))
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
        End Using
    End Function
    Public Shared Function CercaPerCodice(testoCercato As String) As List(Of Class_ELCAR)
        Dim listaRisultati As New List(Of Class_ELCAR)()
        Using cn = MY_DB.GetConnection_Scuoladellestelle
            ' Usa l'operatore LIKE per cercare corrispondenze parziali
            Dim sql As String =
                "SELECT ID, Codice,Testo,Testo_Lungo " &
                "FROM ELCAR " &
                "WHERE Codice LIKE @Testo"
            Using cmd As New SqliteCommand(sql, cn)
                ' Il carattere % indica a SQLite di trovare qualsiasi carattere prima e dopo il testo
                cmd.Parameters.AddWithValue("@Testo", "%" & testoCercato & "%")

                Using rd = cmd.ExecuteReader()

                    ' Usa While al posto di If perché ci possono essere più record corrispondenti
                    While rd.Read()
                        ' Crea una nuova istanza della classe per ogni riga trovata
                        Dim record As New Class_ELCAR()

                        record.Testo = Convert.ToString(rd("TESTO"))
                        record.Testo_Lungo = Convert.ToString(rd("TESTO_LUNGO"))
                        record.Codice = Convert.ToString(rd("CODICE"))
                        record.ID = Convert.ToInt32(rd("ID"))
                        ' Aggiunge il record alla lista dei risultati solo se attivi
                        listaRisultati.Add(record)
                    End While

                End Using
            End Using
        End Using
        Return listaRisultati
    End Function
End Class
