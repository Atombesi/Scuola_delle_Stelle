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
        Dim abilitato = -1

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
                            Abilitato = reader("ENABLED")
                            ' Esiste! Ritorna il valore reale del flag (1 o 0)
                            If abilitato = 1 Then
                                Return 1
                            Else
                                Return 0
                            End If
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
    Public Shared Function Load(id) As Class_DEVICE
        Try
            Using cn = DB.GetConnection_Scuoladellestelle()
                If cn.State <> ConnectionState.Open Then cn.Open()

                Dim queryRilevamento As String = "SELECT * FROM DEVICE WHERE MAC = @mac"
                Using cmd As New SqliteCommand(queryRilevamento, cn)
                    cmd.Parameters.AddWithValue("@mac", id)

                    Using reader As SqliteDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Dim record As New Class_DEVICE()
                            record.NOME = reader("NOME").ToString()
                            record.ID = CInt(reader("ID"))
                            record.MAC = reader("MAC").ToString()
                            record.ENABLED = CBool(reader("ENABLED"))
                            record.STATO = reader("STATO").ToString
                            Return record
                        Else
                            ' Non esiste proprio nel DB
                            Return Nothing
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Console.WriteLine("Errore SQLite: " & ex.Message)
            Return Nothing
        End Try

    End Function
    Public Shared Function Trova_dispositivo(deviceId As String) As (Esito As Boolean, Devices As Class_DEVICE)

        Try
            Using cn = DB.GetConnection_Scuoladellestelle()
                If cn.State <> ConnectionState.Open Then cn.Open()

                Dim queryRilevamento As String = "SELECT * FROM DEVICE WHERE MAC = @mac"
                Using cmd As New SqliteCommand(queryRilevamento, cn)
                    cmd.Parameters.AddWithValue("@mac", deviceId)

                    Using reader As SqliteDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Dim record As New Class_DEVICE()
                            record.NOME = reader("NOME").ToString()
                            record.ID = CInt(reader("ID"))
                            record.MAC = reader("MAC").ToString()
                            record.ENABLED = CBool(reader("ENABLED"))
                            record.STATO = reader("STATO").ToString
                            Return (True, record)
                        Else
                            ' Non esiste proprio nel DB
                            Return (False, Nothing)
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Console.WriteLine("Errore SQLite: " & ex.Message)
            Return (False, Nothing)
        End Try

    End Function
    Public Shared Function Abilita_dispositivo(ID_device As String, Optional Abilitato As Boolean = False) As Boolean
        Dim result = Trova_dispositivo(ID_device)
        If result.esito Then
            Dim record = Load(ID_device)
            If Abilitato Then
                record.ENABLED = 1
                record.STATO = "OK"
            Else
                record.ENABLED = 0
            End If
            record.Save()
            Return True

        End If
    End Function


    Public Shared Function RegistraNuovoDispositivo(ByVal deviceId As String, ByVal messaggioUtente As String, Optional Abilitato As Boolean = False) As Boolean
        ' In SQLite, i nomi dei campi e parametri seguono lo stesso standard standardizzato
        Dim queryInserimento As String = "INSERT INTO DEVICE (NOME, ENABLED, MAC, STATO) VALUES (@nome, 0, @mac, @stato)"

        Try
            Using cn = DB.GetConnection_Scuoladellestelle()
                If cn.State <> ConnectionState.Open Then cn.Open()

                Using cmdInsert As New SqliteCommand(queryInserimento, cn)
                    ' Configurazione dei parametri inseriti dall'utente del tablet
                    cmdInsert.Parameters.AddWithValue("@nome", "Richiesta: " & messaggioUtente)
                    cmdInsert.Parameters.AddWithValue("@mac", deviceId)
                    If Abilitato Then
                        cmdInsert.Parameters.AddWithValue("@enabled", 1)
                        cmdInsert.Parameters.AddWithValue("@stato", "Abilitato")
                    Else
                        cmdInsert.Parameters.AddWithValue("@enabled", 0)
                        cmdInsert.Parameters.AddWithValue("@stato", "In attesa")
                    End If
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
