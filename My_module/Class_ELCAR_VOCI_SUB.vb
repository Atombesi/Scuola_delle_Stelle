Imports Microsoft.Data.Sqlite

Public Class Class_ELCAR_VOCI_SUB
    Public Property ID As Integer
    Public Property ELCAR As String
    Public Property ID_SUB As String
    Public Property Testo As String
    Public Property Testo_Lungo As String
    Public Property Deleted As Boolean
    Private ReadOnly ConnectionString As String =
       "Server=.\SQLEXPRESS;Database=Scuoladellestelle;Trusted_Connection=True;"


    Public Shared Function Load(id As Integer) As Class_ELCAR_VOCI_SUB
        Using cn = DB.GetConnection_Scuoladellestelle()
            Dim sql As String =
            "SELECT ID, ELCAR, ID_SUB, Testo, Testo_Lungo, Deleted
             FROM ELCAR_VOCI_SUB
             WHERE ID = @ID"

            Using cmd As New SqliteCommand(sql, cn)
                cmd.Parameters.AddWithValue("@ID", id)

                Using rd = cmd.ExecuteReader()
                    If rd.Read() Then
                        ' 1. Crea una nuova istanza della classe da restituire
                        Dim voce As New Class_ELCAR_VOCI_SUB()

                        ' 2. Popola le proprietà dell'oggetto creato (nota il prefisso "voce.")
                        voce.ID = Convert.ToInt32(rd("ID"))
                        voce.ELCAR = Convert.ToString(rd("ELCAR"))

                        ' NOTA: Se la colonna nel DB si chiama ID_SUB, usa rd("ID_SUB") invece di rd("ID_ELCAR")
                        voce.ID_SUB = Convert.ToInt16(rd("ID_SUB"))

                        voce.Testo = Convert.ToString(rd("Testo"))
                        voce.Testo_Lungo = Convert.ToString(rd("Testo_Lungo"))
                        voce.Deleted = Convert.ToBoolean(rd("Deleted"))

                        ' 3. Restituisce l'oggetto popolato
                        Return voce
                    Else
                        ' Se non trova nulla nel database restituisce Nothing
                        Return Nothing
                    End If
                End Using
            End Using
        End Using

    End Function

    Public Shared Function CercaPerCodice(testoCercato As String, Optional testoesatto As Boolean = False) As List(Of Class_ELCAR_VOCI_SUB)
        'ID As Integer
        'ELCAR As String
        'ID_SUB as string
        'CODICE_VOCE  As String
        'Testo As String
        'Testo:Lungo As String

        Dim listaRisultati As New List(Of Class_ELCAR_VOCI_SUB)()
        Dim sql As String =
                "SELECT ID, ELCAR,ID_SUB,Testo,Testo_Lungo,deleted " &
                "FROM ELCAR_VOCI_SUB " &
                "WHERE ELCAR LIKE @Testo"

        If testoesatto Then
            sql =
                "SELECT ID, ELCAR,ID_SUB,Testo,Testo_Lungo,deleted " &
                "FROM ELCAR_VOCI_SUB " &
                "WHERE ELCAR = @Testo"
        End If

        Using cn = DB.GetConnection_Scuoladellestelle()
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
                        Dim record As New Class_ELCAR_VOCI_SUB()
                        record.Testo = Convert.ToString(rd("TESTO"))
                        record.Testo_Lungo = Convert.ToString(rd("TESTO_LUNGO"))
                        record.ELCAR = Convert.ToString(rd("elcar"))
                        record.ID_SUB = Convert.ToString(rd("ID_SUB"))
                        record.ID = Convert.ToInt32(rd("ID"))
                        record.Deleted = Convert.ToBoolean(rd("Deleted"))
                        ' Aggiunge il record alla lista dei risultati solo se attivi
                        listaRisultati.Add(record)
                    End While

                End Using
            End Using
        End Using
        Return listaRisultati
    End Function
    Public Shared Function Trova_ID(elcarCercato As String, codiceVoceSubCercato As String) As Integer
        ' Definisci la stringa di connessione (modifica il percorso con il tuo file .db)

        ' La query SQL con i parametri contrassegnati dal simbolo @
        Dim query As String = "SELECT ID FROM ELCAR_VOCI_SUB WHERE ELCAR = @elcar AND ID_SUB = @codice"

        Try
            ' Apri la connessione e prepara il comando
            Using cn = DB.GetConnection_Scuoladellestelle()
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

End Class

