Imports System.Data.SqlTypes
Imports System.Windows.Forms.VisualStyles
Imports DocumentFormat.OpenXml.Wordprocessing
Imports Microsoft.Data.Sqlite

Public Class Class_TODOLIST
    Public Property ID As Integer
    Public Property Registrato As Integer ' true se prsente nel database, false se non ancora registrato
    Public Property ID_BAMBINO As Integer
    Public Property ID_FOGLIO As Integer
    Public Property DATA_FOGLIO As String
    Public Property NOMEFILE As String
    Public Property DB_BAMBINO As String
    Public Property COMPILATO As Integer
    Public Property BAMBINO As String
    Public Property CLIENT_ID As Integer
    Public Property CLIENT_NOME As String
    Public Property CLIENT_DATAINVIO As String
    Public Property CLIENT_DATARICONSEGNA As String



    Private ReadOnly ConnectionString As String =
           "Server=.\SQLEXPRESS;Database=Scuoladellestelle;Trusted_Connection=True;"


    Public Shared Function LoadAll(Optional solocompilati As Boolean = False) As List(Of Class_TODOLIST)
        ' Inizializza la lista che conterrà i risultati trovati
        Dim listaRisultati As New List(Of Class_TODOLIST)()
        Dim sql_string = ""
        Using cn = DB.GetConnection_Scuoladellestelle()

            sql_string = "SELECT ID, ID_BAMBINO,ID_FOGLIO, DATA_FOGLIO, NOMEFILE, DB_BAMBINO, COMPILATO, CLIENT_ID, CLIENT_NOME, CLIENT_DATAINVIO, BAMBINO, CLIENT_DATARICONSEGNA " &
            "FROM TODOLIST " &
            "ORDER BY BAMBINO,ID_FOGLIO;"



            Using cmd As New SqliteCommand(sql_string, cn)

                ' Il carattere % indica a SQLite di trovare qualsiasi carattere prima e dopo il testo
                'cmd.Parameters.AddWithValue("@Testo", "%" & testoCercato & "%")

                Using rd = cmd.ExecuteReader()

                    ' Usa While al posto di If perché ci possono essere più record corrispondenti
                    While rd.Read()
                        ' Crea una nuova istanza della classe per ogni riga trovata
                        Dim record As New Class_TODOLIST()
                        record.ID = Convert.ToInt32(rd("ID"))
                        record.ID_BAMBINO = Convert.ToInt32(rd("ID_BAMBINO"))
                        record.ID_FOGLIO = Convert.ToInt32(rd("ID_FOGLIO"))
                        record.DATA_FOGLIO = Convert.ToString(rd("DATA_FOGLIO"))
                        record.NOMEFILE = Convert.ToString(rd("NOMEFILE"))
                        record.DB_BAMBINO = Convert.ToString(rd("DB_BAMBINO"))
                        record.BAMBINO = Convert.ToString(rd("BAMBINO"))
                        record.COMPILATO = Convert.ToInt32(rd("COMPILATO"))
                        record.CLIENT_ID = Convert.ToInt32(rd("CLIENT_ID"))
                        record.CLIENT_NOME = Convert.ToString(rd("CLIENT_NOME"))
                        record.CLIENT_DATAINVIO = Convert.ToString(rd("CLIENT_DATAINVIO"))
                        record.CLIENT_DATARICONSEGNA = Convert.ToString(rd("CLIENT_DATARICONSEGNA"))
                        Dim i = Convert.ToBoolean(1)
                        listaRisultati.Add(record)
                    End While

                End Using
            End Using
        End Using

        ' Restituisce la lista (sarà vuota se non trova nessuna corrispondenza)
        Return listaRisultati
    End Function
    Public Shared Function Load_By_IDT_IDF(Id_tablet As Integer) As Class_TODOLIST
        ' Inizializza la lista che conterrà i risultati trovati
        Dim listaRisultati As New List(Of Class_TODOLIST)()
        Dim sql_string = ""
        Dim record As New Class_TODOLIST()

        Using cn = DB.GetConnection_Scuoladellestelle()


            sql_string = "SELECT * FROM TODOLIST WHERE CLIENT_ID = @CLIENT_ID"
            If cn.State <> ConnectionState.Open Then
                cn.Open()
            End If

            Using cmd As New SqliteCommand(sql_string, cn)
                cmd.Parameters.AddWithValue("@CLIENT_ID", Id_tablet)

                ' Il carattere % indica a SQLite di trovare qualsiasi carattere prima e dopo il testo
                'cmd.Parameters.AddWithValue("@Testo", "%" & testoCercato & "%")
                Try

                    Using rd As SqliteDataReader = cmd.ExecuteReader()

                        ' Usa While al posto di If perché ci possono essere più record corrispondenti
                        If rd.Read() Then
                            ' Crea una nuova istanza della classe per ogni riga trovata
                            record.ID = Convert.ToInt32(rd("ID"))
                            record.ID_FOGLIO = Convert.ToInt32(rd("ID_FOGLIO"))
                            record.DB_BAMBINO = Convert.ToString(rd("DB_BAMBINO"))
                            record.ID_BAMBINO = Convert.ToInt32(rd("ID_BAMBINO"))
                            record.DATA_FOGLIO = Convert.ToString(rd("DATA_FOGLIO"))
                            record.NOMEFILE = Convert.ToString(rd("NOMEFILE"))
                            record.BAMBINO = Convert.ToString(rd("BAMBINO"))
                            record.COMPILATO = Convert.ToInt32(rd("COMPILATO"))
                            record.CLIENT_ID = Convert.ToInt32(rd("CLIENT_ID"))
                            record.CLIENT_NOME = Convert.ToString(rd("CLIENT_NOME"))
                            record.CLIENT_DATAINVIO = Convert.ToString(rd("CLIENT_DATAINVIO"))
                            record.CLIENT_DATARICONSEGNA = Convert.ToString(rd("CLIENT_DATARICONSEGNA"))
                            Dim i = Convert.ToBoolean(1)
                            Return record
                        End If

                    End Using
                Catch ex As Exception
                    Console.WriteLine(ex.ToString)
                End Try

            End Using
        End Using

        ' Restituisce la lista (sarà vuota se non trova nessuna corrispondenza)
        Return Nothing
    End Function

    Public Shared Function Load_By_ID(Idd As Integer) As Class_TODOLIST
        ' Inizializza la lista che conterrà i risultati trovati
        Dim listaRisultati As New List(Of Class_TODOLIST)()
        Dim sql_string = ""
        Dim record As New Class_TODOLIST()

        Using cn = DB.GetConnection_Scuoladellestelle()


            sql_string = "SELECT * FROM TODOLIST WHERE ID = @ID"
            If cn.State <> ConnectionState.Open Then
                cn.Open()
            End If

            Using cmd As New SqliteCommand(sql_string, cn)
                cmd.Parameters.AddWithValue("@ID", Idd)

                ' Il carattere % indica a SQLite di trovare qualsiasi carattere prima e dopo il testo
                'cmd.Parameters.AddWithValue("@Testo", "%" & testoCercato & "%")
                Try

                    Using rd As SqliteDataReader = cmd.ExecuteReader()

                        ' Usa While al posto di If perché ci possono essere più record corrispondenti
                        If rd.Read() Then
                            ' Crea una nuova istanza della classe per ogni riga trovata
                            record.ID = Convert.ToInt32(rd("ID"))
                            record.ID_FOGLIO = Convert.ToInt32(rd("ID_FOGLIO"))
                            record.DB_BAMBINO = Convert.ToString(rd("DB_BAMBINO"))
                            record.ID_BAMBINO = Convert.ToInt32(rd("ID_BAMBINO"))
                            record.DATA_FOGLIO = Convert.ToString(rd("DATA_FOGLIO"))
                            record.NOMEFILE = Convert.ToString(rd("NOMEFILE"))
                            record.BAMBINO = Convert.ToString(rd("BAMBINO"))
                            record.COMPILATO = Convert.ToInt32(rd("COMPILATO"))
                            record.CLIENT_ID = Convert.ToInt32(rd("CLIENT_ID"))
                            record.CLIENT_NOME = Convert.ToString(rd("CLIENT_NOME"))
                            record.CLIENT_DATAINVIO = Convert.ToString(rd("CLIENT_DATAINVIO"))
                            record.CLIENT_DATARICONSEGNA = Convert.ToString(rd("CLIENT_DATARICONSEGNA"))
                            Dim i = Convert.ToBoolean(1)
                            Return record
                        End If

                    End Using
                Catch ex As Exception
                    Console.WriteLine(ex.ToString)
                End Try

            End Using
        End Using

        ' Restituisce la lista (sarà vuota se non trova nessuna corrispondenza)
        Return Nothing
    End Function

    Public Shared Function EliminaRecordPerID(ByVal idDaEliminare As Integer) As Boolean
        ' 1. Definisci la stringa di connessione al tuo file SQLite (.db o .sqlite)


        ' 2. Scrivi la query SQL usando il parametro di sicurezza @id
        Dim querySql As String = "DELETE FROM TODOLIST WHERE ID = @id;"

        ' 3. Utilizza i blocchi Using per garantire la corretta chiusura delle risorse
        Using cn = DB.GetConnection_Scuoladellestelle()

            Using cmd As New SqliteCommand(querySql, cn)

                ' 4. Associa il valore numerico ID al parametro @id della query
                cmd.Parameters.AddWithValue("@id", idDaEliminare)
                Try
                    ' 5. Apri la connessione ed esegui il comando
                    cn.Open()
                    Dim righeCoinvolte As Integer = cmd.ExecuteNonQuery()
                    Return True
                Catch ex As Exception
                    ' Gestione di eventuali errori (es. database bloccato o file non trovato)
                    Dim m = "Errore durante l'eliminazione: " & ex.Message
                    APPEND_LOG(m, ini.File_Log, True)
                    Return False
                End Try

            End Using
        End Using

    End Function

    Public Sub Save()
        Using cn = DB.GetConnection_Scuoladellestelle()
            ' Assicurati di aprire la connessione se GetConnection non lo fa già in automatico
            If cn.State <> ConnectionState.Open Then cn.Open()

            If ID = 0 Then
                ' --- BLOCCO INSERIMENTO ---
                ' Nota il punto e virgola (;) aggiunto prima del SELECT
                Dim sql As String =
                    "INSERT INTO TODOLIST (ID_BAMBINO, ID_FOGLIO, DATA_FOGLIO, NOMEFILE, DB_BAMBINO, COMPILATO, CLIENT_ID, CLIENT_NOME, CLIENT_DATAINVIO, BAMBINO, CLIENT_DATARICONSEGNA) 
                 VALUES (@ID_BAMBINO, @ID_FOGLIO, @DATA_FOGLIO, @NOMEFILE, @DB_BAMBINO, @COMPILATO, @CLIENT_ID, @CLIENT_NOME, @CLIENT_DATAINVIO, @BAMBINO, @CLIENT_DATARICONSEGNA);
                 SELECT last_insert_rowid();"

                Using cmd As New SqliteCommand(sql, cn)
                    cmd.Parameters.AddWithValue("@ID_BAMBINO", ID_BAMBINO)
                    cmd.Parameters.AddWithValue("@ID_FOGLIO", ID_FOGLIO)
                    cmd.Parameters.AddWithValue("@DATA_FOGLIO", DATA_FOGLIO)
                    cmd.Parameters.AddWithValue("@NOMEFILE", NOMEFILE)
                    cmd.Parameters.AddWithValue("@DB_BAMBINO", DB_BAMBINO)
                    cmd.Parameters.AddWithValue("@COMPILATO", COMPILATO)
                    cmd.Parameters.AddWithValue("@CLIENT_ID", CLIENT_ID)
                    cmd.Parameters.AddWithValue("@CLIENT_DATAINVIO", CLIENT_DATAINVIO)
                    cmd.Parameters.AddWithValue("@CLIENT_NOME", CLIENT_NOME)
                    cmd.Parameters.AddWithValue("@BAMBINO", BAMBINO)
                    cmd.Parameters.AddWithValue("@CLIENT_DATARICONSEGNA", CLIENT_DATARICONSEGNA)

                    ' Recupera l'ID appena generato dall'autoincrement
                    ID = Convert.ToInt32(cmd.ExecuteScalar())
                End Using
            Else
                ' --- BLOCCO AGGIORNAMENTO ---
                ' Corretti tutti i campi inserendo le "@" davanti ai parametri e rimossa la virgola finale prima di WHERE
                Dim sql As String =
                    "UPDATE TODOLIST
                 SET ID_BAMBINO = @ID_BAMBINO,
                     ID_FOGLIO = @ID_FOGLIO,
                     DATA_FOGLIO = @DATA_FOGLIO,
                     NOMEFILE = @NOMEFILE,
                     DB_BAMBINO = @DB_BAMBINO,
                     COMPILATO = @COMPILATO,
                     CLIENT_ID = @CLIENT_ID,
                     CLIENT_DATAINVIO = @CLIENT_DATAINVIO,
                     CLIENT_NOME = @CLIENT_NOME,
                     BAMBINO = @BAMBINO,
                     CLIENT_DATARICONSEGNA = @CLIENT_DATARICONSEGNA
                 WHERE ID = @ID;"

                Using cmd As New SqliteCommand(sql, cn)
                    cmd.Parameters.AddWithValue("@ID_BAMBINO", ID_BAMBINO)
                    cmd.Parameters.AddWithValue("@ID_FOGLIO", ID_FOGLIO)
                    cmd.Parameters.AddWithValue("@DATA_FOGLIO", DATA_FOGLIO)
                    cmd.Parameters.AddWithValue("@NOMEFILE", NOMEFILE)
                    cmd.Parameters.AddWithValue("@DB_BAMBINO", DB_BAMBINO)
                    cmd.Parameters.AddWithValue("@COMPILATO", COMPILATO)
                    cmd.Parameters.AddWithValue("@CLIENT_ID", CLIENT_ID)
                    cmd.Parameters.AddWithValue("@CLIENT_DATAINVIO", CLIENT_DATAINVIO)
                    cmd.Parameters.AddWithValue("@CLIENT_NOME", CLIENT_NOME)
                    cmd.Parameters.AddWithValue("@BAMBINO", BAMBINO)
                    cmd.Parameters.AddWithValue("@CLIENT_DATARICONSEGNA", CLIENT_DATARICONSEGNA)
                    cmd.Parameters.AddWithValue("@ID", ID)

                    cmd.ExecuteNonQuery()
                End Using
            End If
        End Using
    End Sub

    Public Function Delete(ID) As Boolean
        ' Controllo di sicurezza: se l'ID è 0, il record non esiste nel database
        If ID = 0 Then Return False


        Try
            Using cn = DB.GetConnection_Scuoladellestelle()
                If cn.State <> ConnectionState.Open Then cn.Open()

                Dim sql As String = "DELETE FROM TODOLIST WHERE ID = @ID;"

                Using cmd As New SqliteCommand(sql, cn)
                    cmd.Parameters.AddWithValue("@ID", ID)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            ID = 0
            Return True
        Catch ex As SqliteException
            ' Se il database è bloccato da DB Browser, entrerà qui
            Dim m = "Impossibile eliminare. Il database potrebbe essere bloccato da un altro programma: " & ex.Message
            APPEND_LOG(m, ini.File_Log, True)
            Return False
        End Try
        Return True
    End Function
    Public Overrides Function ToString() As String
        ' Dice alla ComboBox (e a qualsiasi griglia) di mostrare il testo di NOMEFILE
        Return Me.NOMEFILE
    End Function
End Class
