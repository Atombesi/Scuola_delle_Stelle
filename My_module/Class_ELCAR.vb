Imports Microsoft.Data.Sqlite
Public Class Class_ELCAR
    Public Property ID As Integer
    Public Property Codice As String
    Public Property Testo As String
    Public Property Testo_Lungo As String

    Private ReadOnly ConnectionString As String =
       "Server=.\SQLEXPRESS;Database=Scuoladellestelle;Trusted_Connection=True;"



    Public Shared Function Load(id As Integer) As Class_GIORNALIERO

        Using cn = DB.GetConnection_Scuoladellestelle

            Dim sql As String =
                "SELECT ID, CODICE,Testo,Testo_Lungo 
                 FROM ELCAR
                 WHERE ID = @ID"

            Using cmd As New SqliteCommand(sql, cn)

                cmd.Parameters.AddWithValue("@ID", id)

                Using rd = cmd.ExecuteReader()

                    If rd.Read() Then
                        Dim record As New Class_ELCAR()
                        record.ID = Convert.ToInt32(rd("ID"))
                        record.Codice = Convert.ToString(rd("Codice"))
                        record.Testo = Convert.ToString(rd("Testo"))
                        record.Testo_Lungo = Convert.ToString(rd("Testo_Lungo"))
                    Else
                        Return Nothing

                    End If

                End Using

            End Using

        End Using

    End Function
    Public Shared Function CercaPerCodice(testoCercato As String) As List(Of Class_ELCAR)
        Dim listaRisultati As New List(Of Class_ELCAR)()
        Using cn = DB.GetConnection_Scuoladellestelle
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
