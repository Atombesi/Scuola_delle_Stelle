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
End Class

