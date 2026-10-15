Imports Microsoft.Data
Imports Microsoft.Data.SqlClient
Imports Microsoft.Data.Sqlite

Public Class Class_DEVICE
    Public Property ID As Integer
    Public Property NOME As String
    Public Property ENABLED As Integer
    Public Property MAC As String
    Public Property STATO As String

    Private ReadOnly ConnectionString As String =
           "Server=.\SQLEXPRESS;Database=Scuoladellestelle;Trusted_Connection=True;"


    Public Sub Save()
        Using cn = DB.GetConnection_Scuoladellestelle()
            ' Assicurati di aprire la connessione se GetConnection non lo fa già in automatico
            If cn.State <> ConnectionState.Open Then cn.Open()

            If ID = 0 Then
                ' --- BLOCCO INSERIMENTO ---
                ' Nota il punto e virgola (;) aggiunto prima del SELECT
                Dim sql As String =
                    "INSERT INTO DEVICE (NOME, ENABLED, MAC, STATO) 
                 VALUES (@NOME, @ENABLED, @MAC, @STATO);
                 SELECT last_insert_rowid();"

                Using cmd As New SqliteCommand(sql, cn)
                    cmd.Parameters.AddWithValue("@NOME", NOME)
                    cmd.Parameters.AddWithValue("@ENABLED", ENABLED)
                    cmd.Parameters.AddWithValue("@MAC", MAC)
                    cmd.Parameters.AddWithValue("@STATO", STATO)
                    ' Recupera l'ID appena generato dall'autoincrement
                    ID = Convert.ToInt32(cmd.ExecuteScalar())
                End Using
            Else
                ' --- BLOCCO AGGIORNAMENTO ---
                ' Corretti tutti i campi inserendo le "@" davanti ai parametri e rimossa la virgola finale prima di WHERE
                Dim sql As String =
                    "UPDATE DEVICE
                 SET NOME = @NOME,
                     ENABLED = @ENABLED,
                     MAC = @MAC,
                     STATO = @STATO
                 WHERE ID = @ID;"

                Using cmd As New SqliteCommand(sql, cn)
                    cmd.Parameters.AddWithValue("@NOME", NOME)
                    cmd.Parameters.AddWithValue("@ENABLED", ENABLED)
                    cmd.Parameters.AddWithValue("@MAC", MAC)
                    cmd.Parameters.AddWithValue("@STATO", STATO)
                    cmd.Parameters.AddWithValue("@ID", ID)

                    cmd.ExecuteNonQuery()
                End Using
            End If
        End Using
    End Sub


    Public Shared Function Dispositivo_Registrato_Stato(ByVal deviceId As String, ByRef nomeDevice As String) As Integer
        ' Ritorna: 1 = Approvato, 0 = In Attesa, -1 = Non Presente
        nomeDevice = ""

        Try
            Using cn = DB.GetConnection_Scuoladellestelle()
                If cn.State <> ConnectionState.Open Then cn.Open()

                Dim queryRilevamento As String = "SELECT NOME, ENABLED FROM DEVICE WHERE MAC = @mac"
                Using cmd As New SqliteCommand(queryRilevamento, cn)
                    cmd.Parameters.AddWithValue("@mac", deviceId)

                    Using reader As SqliteDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Dim enabled As Integer = Convert.ToInt32(reader("ENABLED"))
                            nomeDevice = reader("NOME").ToString()

                            ' Esiste! Ritorna il valore reale del flag (1 o 0)
                            Return enabled
                        Else
                            ' Non esiste proprio nel DB
                            Return -1
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Console.WriteLine("Errore SQLite: " & ex.Message)
            Return -1
        End Try
    End Function

    Public Shared Function RegistraNuovoDispositivo(ByVal deviceId As String, ByVal messaggioUtente As String) As Boolean
        ' In SQLite, i nomi dei campi e parametri seguono lo stesso standard standardizzato
        Dim queryInserimento As String = "INSERT INTO DEVICE (NOME, ENABLED, MAC, STATO) VALUES (@nome, 0, @mac, @stato)"

        Try
            Using cn = DB.GetConnection_Scuoladellestelle()
                If cn.State <> ConnectionState.Open Then cn.Open()

                Using cmdInsert As New SqliteCommand(queryInserimento, cn)
                    ' Configurazione dei parametri inseriti dall'utente del tablet
                    cmdInsert.Parameters.AddWithValue("@nome", "Richiesta: " & messaggioUtente)
                    cmdInsert.Parameters.AddWithValue("@mac", deviceId)
                    cmdInsert.Parameters.AddWithValue("@stato", "In attesa")

                    ' Esegue l'inserimento fisico nel file del database SQLite
                    cmdInsert.ExecuteNonQuery()
                    Return True
                End Using
            End Using
        Catch ex As Exception
            Console.WriteLine("Errore SQLite durante RegistraNuovoDispositivo: " & ex.Message)
            Return False
        End Try
    End Function
End Class
