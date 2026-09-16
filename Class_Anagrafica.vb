Imports Microsoft.Data.Sqlite
Public Class Class_ANAGRAFICA
    Public Property ID As Integer
    Public Property Nome As String
    Public Property Cognome As String
    Public Property DAta_nascita As String
    Public Property Data_Creazione As String
    Public Property Nik As String
    Public Property DBname As String
    Public Property Attivo As Boolean
    Public Property Assente As Boolean


    Private ReadOnly ConnectionString As String =
       "Server=.\SQLEXPRESS;Database=Scuoladellestelle;Trusted_Connection=True;"


    Public Sub Save()

        Using cn = DB.GetConnection_Scuoladellestelle()
            If ID = 0 Then
                Dim sql As String =
                    "INSERT INTO ANAGRAFICA  (Nome,Cognome,DAta_nascita,Data_Creazione,Nik,DBname,Attivo,assente) 
                     VALUES (@Nome,@Cognome,@DAta_nascita,@Data_Creazione,@Nik,@DBname,@Attivo,@assente);
                     SELECT last_insert_rowid();"
                Using cmd As New SqliteCommand(sql, cn)
                    cmd.Parameters.AddWithValue("@Nome", Nome)
                    cmd.Parameters.AddWithValue("@Cognome", Cognome)
                    cmd.Parameters.AddWithValue("@DAta_nascita", DAta_nascita)
                    cmd.Parameters.AddWithValue("@Data_Creazione", Data_Creazione)
                    cmd.Parameters.AddWithValue("@Nik", Nik)
                    cmd.Parameters.AddWithValue("@DBname", DBname)
                    cmd.Parameters.AddWithValue("@Attivo", Attivo)
                    cmd.Parameters.AddWithValue("@Assente", Assente)

                    ID = Convert.ToInt32(cmd.ExecuteScalar())
                End Using
            Else

                Dim sql As String =
                    "UPDATE ANAGRAFICA
                     SET Nome=@Nome,
                     Cognome=@Cognome,
                     DAta_nascita=@DAta_nascita,
                     Data_Creazione=@Data_Creazione,
                     Nik=@Nik,
                     DBname=@DBname,
                     Attivo=@Attivo,
                     Assente=@Assente
                     WHERE ID=@ID"

                Using cmd As New SqliteCommand(sql, cn)

                    cmd.Parameters.AddWithValue("@Nome", Nome)
                    cmd.Parameters.AddWithValue("@Cognome", Cognome)
                    cmd.Parameters.AddWithValue("@DAta_nascita", DAta_nascita)
                    cmd.Parameters.AddWithValue("@Data_Creazione", Data_Creazione)
                    cmd.Parameters.AddWithValue("@Nik", Nik)
                    cmd.Parameters.AddWithValue("@DBname", DBname)
                    cmd.Parameters.AddWithValue("@Attivo", Attivo)
                    cmd.Parameters.AddWithValue("@Assente", Assente)
                    cmd.Parameters.AddWithValue("@ID", ID)

                    cmd.ExecuteNonQuery()

                End Using

            End If

        End Using



    End Sub

    Public Shared Function Load(id As Integer) As Class_ANAGRAFICA
        Using cn = DB.GetConnection_Scuoladellestelle()

            Dim sql As String =
            "SELECT ID, Nome, Cognome, DAta_nascita, Data_Creazione, Nik, DBname, Attivo, Assente " &
            "FROM ANAGRAFICA " &
            "WHERE ID = @ID"

            Using cmd As New SqliteCommand(sql, cn)
                cmd.Parameters.AddWithValue("@ID", id)

                Using rd = cmd.ExecuteReader()
                    If rd.Read() Then

                        ' 1. CREA UN NUOVO OGGETTO DA RESTITUIRE
                        Dim record As New Class_ANAGRAFICA()

                        ' 2. ASSEGNA I VALORI AL NUOVO OGGETTO (Nota il prefisso "record.")
                        record.ID = Convert.ToInt32(rd("ID"))
                        record.Nome = Convert.ToString(rd("Nome"))
                        record.Cognome = Convert.ToString(rd("Cognome"))
                        record.DAta_nascita = Convert.ToString(rd("DAta_nascita"))
                        record.Data_Creazione = Convert.ToString(rd("Data_Creazione"))
                        record.Nik = Convert.ToString(rd("Nik"))
                        record.DBname = Convert.ToString(rd("DBname"))
                        record.Attivo = Convert.ToInt32(rd("Attivo"))
                        record.Assente = Convert.ToInt32(rd("Assente"))

                        ' 3. RESTITUISCE IL RECORD TROVATO
                        Return record

                    Else
                        ' Se l'ID non esiste nel database, restituisce il vuoto
                        Return Nothing
                    End If
                End Using
            End Using
        End Using

    End Function

    Public Shared Function CercaPerCognome(testoCercato As String, Optional soloattivi As Boolean = False) As List(Of Class_ANAGRAFICA)
        ' Inizializza la lista che conterrà i risultati trovati
        Dim listaRisultati As New List(Of Class_ANAGRAFICA)()

        Using cn = DB.GetConnection_Scuoladellestelle()

            ' Usa l'operatore LIKE per cercare corrispondenze parziali
            Dim sql As String =
                "SELECT ID, Nome, Cognome, DAta_nascita, Data_Creazione, Nik, DBname,Attivo, Assente " &
                "FROM ANAGRAFICA " &
                "WHERE Cognome LIKE @Testo"

            Using cmd As New SqliteCommand(sql, cn)

                ' Il carattere % indica a SQLite di trovare qualsiasi carattere prima e dopo il testo
                cmd.Parameters.AddWithValue("@Testo", "%" & testoCercato & "%")

                Using rd = cmd.ExecuteReader()

                    ' Usa While al posto di If perché ci possono essere più record corrispondenti
                    While rd.Read()
                        ' Crea una nuova istanza della classe per ogni riga trovata
                        Dim record As New Class_ANAGRAFICA()

                        record.ID = Convert.ToInt32(rd("ID"))
                        record.Nome = Convert.ToString(rd("Nome"))
                        record.Cognome = Convert.ToString(rd("Cognome"))
                        record.DAta_nascita = Convert.ToString(rd("DAta_nascita"))
                        record.Data_Creazione = Convert.ToString(rd("Data_Creazione"))
                        record.Nik = Convert.ToString(rd("Nik"))
                        record.DBname = Convert.ToString(rd("DBname"))
                        record.Attivo = Convert.ToInt32(rd("Attivo"))
                        record.Assente = Convert.ToInt32(rd("Assente"))

                        ' Aggiunge il record alla lista dei risultati solo se attivi
                        If soloattivi And Not record.Attivo Then
                            Continue While
                        End If
                        listaRisultati.Add(record)
                    End While

                End Using
            End Using
        End Using

        ' Restituisce la lista (sarà vuota se non trova nessuna corrispondenza)
        Return listaRisultati
    End Function

    Public Shared Function ElencaTabelleDisponibili() As List(Of String)
        Dim listaTabelle As New List(Of String)()

        Try
            Using cn = DB.GetConnection_Scuoladellestelle()
                ' Query di sistema SQLite per elencare tutte le tabelle utente
                Dim sql As String = "SELECT name FROM sqlite_master WHERE type='table' AND name NOT LIKE 'sqlite_%' ORDER BY name;"

                Using cmd As New SqliteCommand(sql, cn)
                    Using rd = cmd.ExecuteReader()
                        While rd.Read()
                            ' Aggiunge il nome della tabella alla lista
                            listaTabelle.Add(Convert.ToString(rd("name")))
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Errore durante la lettura delle tabelle: " & ex.Message)
        End Try

        Return listaTabelle
    End Function

End Class
