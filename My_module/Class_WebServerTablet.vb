Imports System.IO
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading
Imports System.Net.WebSockets
Imports System.Text.Json
Imports System.Windows

Public Class Class_WebServerTablet

    Private _listener As HttpListener
    Private _isListening As Boolean = False

    ' Avvia il server in ascolto sulla porta 8080
    Public Function StartServer() As Boolean
        If _isListening Then Return True

        _listener = New HttpListener()

        ' IMPORTANTE: l'asterisco * permette di ricevere connessioni da QUALSIASI dispositivo nella rete Wi-Fi, non solo dal PC locale
        '_listener.Prefixes.Add("http://*:8080/")
        _listener.Prefixes.Add("http://+:8080/")

        Try
            _listener.Start()
            _isListening = True

            ' Avvia un thread in background per non bloccare l'interfaccia grafica del tuo programma
            Dim serverThread As New Thread(AddressOf AscoltaRichieste)
            serverThread.IsBackground = True
            serverThread.Start()

            Console.WriteLine("Server Wi-Fi avviato in ascolto sulla porta 8080...")
            Return True
        Catch ex As Exception
            Dim m = "C:\Windows\System32>netsh http add urlacl url=http://+:8080/ user=Everyone listen=yes"
            Dim x = "Errore durante l'avvio del server: " & ex.Message & vbCrLf & "Nota: Potrebbe essere necessario eseguire Visual Studio come Amministratore per sbloccare la porta."
            x = x + vbCrLf + "Digitare questa riga nella finestra di comando aperta in modalita ammistratore" + vbCrLf + vbCrLf + m
            MessageBox.Show(x)
            Return False
        End Try
    End Function

    ' Arresta il server
    Public Sub StopServer()
        If Not _isListening Then Return
        _listener.Stop()
        _isListening = False
    End Sub

    Private Async Sub AscoltaRichieste_OLD()
        While _isListening
            Try
                ' Il server si mette in pausa finché un tablet non si collega
                Dim context As HttpListenerContext = Await _listener.GetContextAsync()
                Dim request As HttpListenerRequest = context.Request
                Dim response As HttpListenerResponse = context.Response

                ' 🌐 CONTROLLO: Il tablet sta tentando di aprire un canale WebSocket?
                If context.Request.IsWebSocketRequest Then

                    ' Accetta la connessione WebSocket in modo asincrono nativo
                    Dim webSocketContext As HttpListenerWebSocketContext = Await context.AcceptWebSocketAsync(subProtocol:=Nothing)
                    Dim ws As WebSocket = webSocketContext.WebSocket

                    Console.WriteLine("🟢 CANALE APERTO: Un tablet si è connesso in WebSocket!")

                    '' 🎯 STEP 1 AUTOMATICO: Inviamo subito l'HANDSHAKE_OK per far accendere il pallino verde sul tablet
                    'Dim handshakeTesto As String = "{""type"": ""HANDSHAKE_OK""}"
                    'Dim handshakeBuffer() As Byte = Encoding.UTF8.GetBytes(handshakeTesto)
                    'Await ws.SendAsync(New ArraySegment(Of Byte)(handshakeBuffer), WebSocketMessageType.Text, True, CancellationToken.None)

                    ' Avvia il ciclo continuo di ascolto dei messaggi dentro questo specifico tubo
                    Await GestisciMessaggiTablet(ws)

                Else
                    ' Se arriva una richiesta HTTP normale (es. da browser), rispondi picche o gestisci la pagina
                    response.StatusCode = HttpStatusCode.BadRequest
                    response.OutputStream.Close()
                End If

            Catch ex As Exception
                Dim a = 2
                ' Gestisce la chiusura del server o disconnessioni improvvise
            End Try
        End While
    End Sub

    Private Async Sub AscoltaRichieste()
        While _isListening
            Try
                Dim context As HttpListenerContext = Await _listener.GetContextAsync()

                If context.Request.IsWebSocketRequest Then
                    Dim webSocketContext As HttpListenerWebSocketContext = Await context.AcceptWebSocketAsync(subProtocol:=Nothing)
                    Dim ws As WebSocket = webSocketContext.WebSocket

                    Console.WriteLine("🟢 CANALE APERTO: Un tablet si è connesso in WebSocket!")

                    ' 🎯 LA SVOLTA MULTI-THREAD: Lanciamo il ciclo in una Task parallela isolata.
                    ' Questo impedisce all'Async Sub di distruggere l'ascolto quando il metodo cede il controllo.
                    Task.Run(Function() GestisciMessaggiTablet(ws))
                Else
                    context.Response.StatusCode = HttpStatusCode.BadRequest
                    context.Response.OutputStream.Close()
                End If
            Catch ex As Exception
                Console.WriteLine("Errore in AscoltaRichieste: " & ex.Message)
            End Try
        End While
    End Sub

    Private Async Function GestisciMessaggiTablet(ByVal ws As WebSocket) As Task
        Dim buffer(4096) As Byte
        Console.WriteLine("🔄 Thread di ascolto WebSocket avviato per il dispositivo.")

        While ws.State = WebSocketState.Open
            Try
                ' Il PC rimane in attesa bloccante dentro la While finché non arriva un click dal tablet
                Dim result As WebSocketReceiveResult = Await ws.ReceiveAsync(New ArraySegment(Of Byte)(buffer), CancellationToken.None)

                If result.MessageType = WebSocketMessageType.Close Then
                    Await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Chiusura richiesta", CancellationToken.None)
                    Console.WriteLine("🔴 CANALE CHIUSO dal tablet.")
                    Exit While
                End If

                Dim messaggioRicevuto As String = Encoding.UTF8.GetString(buffer, 0, result.Count)
                Console.WriteLine("Messaggio ricevuto dal tablet: " & messaggioRicevuto)

                Dim rispostaJson As String = ""

                ' --- 1. GESTIONE PACCHETTO HANDSHAKE ---
                If messaggioRicevuto.Contains("""type"":""HANDSHAKE""") Then
                    Dim parser As System.Text.Json.JsonDocument = System.Text.Json.JsonDocument.Parse(messaggioRicevuto)
                    Dim root As System.Text.Json.JsonElement = parser.RootElement
                    Dim deviceId As String = root.GetProperty("device_id").GetString()
                    Dim notaUtente As String = root.GetProperty("nota_utente").GetString()
                    Dim nomeAssegnato As String = ""

                    Dim statoHardware As Integer = Class_DEVICE.Dispositivo_Registrato_Stato(deviceId, nomeAssegnato)

                    If statoHardware = 1 Then
                        rispostaJson = "{""type"":""HANDSHAKE_OK"",""nome_dispositivo"":""" & nomeAssegnato & """}"
                    ElseIf statoHardware = 0 Then
                        rispostaJson = "{""type"":""HANDSHAKE_PENDING"",""messaggio"":""In attesa di abilitazione...""}"
                    ElseIf statoHardware = -1 Then
                        Dim inserimentoOK As Boolean = Class_DEVICE.RegistraNuovoDispositivo(deviceId, notaUtente)
                        rispostaJson = "{""type"":""HANDSHAKE_PENDING"",""messaggio"":""Richiesta inviata con successo...""}"
                    End If
                End If

                ' --- 2. GESTIONE PACCHETTO RICHIESTA ELENCO ---
                If messaggioRicevuto.Contains("""type"":""RICHIEDI_ELENCO""") Then
                    Dim datiElenco As String = GeneraJsonElencoFogli()
                    rispostaJson = "{""type"":""ELENCO_FOGLI_RISPOSTA"", ""dati"":" & datiElenco & "}"
                End If

                ' Spedisce la risposta corretta nel tubo aperto del Socket
                If Not String.IsNullOrEmpty(rispostaJson) Then
                    Dim bufferRisposta() As Byte = Encoding.UTF8.GetBytes(rispostaJson)
                    Await ws.SendAsync(New ArraySegment(Of Byte)(bufferRisposta), WebSocketMessageType.Text, True, CancellationToken.None)
                End If

            Catch ex As Exception
                Console.WriteLine("CRASH DENTRO IL LOOP DEL SOCKET VB: " & ex.Message)
                Exit While
            End Try
        End While

        Console.WriteLine("🏁 Thread di ascolto terminato. Il socket non è più monitorato.")
    End Function

    Private Async Function GestisciMessaggiTablet_OLD(ByVal ws As WebSocket) As Task
        Dim buffer(4096) As Byte ' Buffer di lettura di 4KB

        While ws.State = WebSocketState.Open
            Try
                ' Rimane in attesa di un messaggio inviato dal tablet
                Dim result As WebSocketReceiveResult = Await ws.ReceiveAsync(New ArraySegment(Of Byte)(buffer), CancellationToken.None)

                ' Se il tablet chiude la connessione, usciamo dal ciclo
                If result.MessageType = WebSocketMessageType.Close Then
                    Await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Chiusura richiesta", CancellationToken.None)
                    Console.WriteLine("🔴 CANALE CHIUSO: Il tablet si è disconnesso.")
                    Exit While
                End If

                ' Converte i byte ricevuti in testo stringa JSON
                Dim messaggioRicevuto As String = Encoding.UTF8.GetString(buffer, 0, result.Count)
                Console.WriteLine("Messaggio ricevuto dal tablet: " & messaggioRicevuto)

                ' --- ELABORAZIONE DEI COMANDI DEL TABLET ---
                Dim rispostaJson As String = ""

                ' ===================================================================
                ' 1. LOGICA AUTENTICAZIONE E REGISTRAZIONE DISPOSITIVI (SQLite)
                ' ===================================================================
                If messaggioRicevuto.Contains("""type"":""HANDSHAKE""") Then

                    Dim parser As System.Text.Json.JsonDocument = System.Text.Json.JsonDocument.Parse(messaggioRicevuto)
                    Dim root As System.Text.Json.JsonElement = parser.RootElement

                    Dim deviceId As String = root.GetProperty("device_id").GetString()
                    Dim notaUtente As String = root.GetProperty("nota_utente").GetString()

                    Dim nomeAssegnato As String = ""

                    ' 🎯 LA SVOLTA: Chiediamo a SQLite lo stato reale. 
                    ' Modifica la tua funzione in modo che interroghi il campo ENABLED:
                    ' Se non trova nulla -> ritorna -1
                    ' Se trova ENABLED = 0 -> ritorna 0
                    ' Se trova ENABLED = 1 -> ritorna 1
                    Dim statoHardware As Integer = Class_DEVICE.Dispositivo_Registrato_Stato(deviceId, nomeAssegnato)

                    If statoHardware = 1 Then
                        ' Il dispositivo è nel DB ed è stato APPROVATO (ENABLED = 1)
                        rispostaJson = "{""type"":""HANDSHAKE_OK"",""nome_dispositivo"":""" & nomeAssegnato & """}"
                        Console.WriteLine("🟢 DISPOSITIVO AUTORIZZATO: " & nomeAssegnato)

                    ElseIf statoHardware = 0 Then
                        ' Il dispositivo esiste nel DB ma è ancora in attesa (ENABLED = 0)
                        rispostaJson = "{""type"":""HANDSHAKE_PENDING"",""messaggio"":""Il dispositivo è censito nel sistema. In attesa che il gestore attivi la spunta di abilitazione sul PC Windows...""}"
                        Console.WriteLine("⏳ ACCESSO SOSPESO: Il tablet è in lista d'attesa.")

                    ElseIf statoHardware = -1 Then
                        ' Il dispositivo non esiste proprio -> Eseguiamo la INSERT distinta per la prima volta
                        Dim inserimentoOK As Boolean = Class_DEVICE.RegistraNuovoDispositivo(deviceId, notaUtente)

                        If inserimentoOK Then
                            rispostaJson = "{""type"":""HANDSHAKE_PENDING"",""messaggio"":""Richiesta inviata con successo! Postazione inserita nel database SQLite. In attesa di approvazione...""}"
                            Console.WriteLine("🟡 NUOVA RICHIESTA: Dispositivo registrato ora in SQLite.")
                        Else
                            rispostaJson = "{""type"":""HANDSHAKE_ERROR"",""messaggio"":""Errore di scrittura nel file del database SQLite.""}"
                        End If
                    End If

                    ' Spedisce il verdetto reale nel Socket ed esce subito
                    If Not String.IsNullOrEmpty(rispostaJson) Then
                        Dim bufferRisposta() As Byte = Encoding.UTF8.GetBytes(rispostaJson)
                        Await ws.SendAsync(New ArraySegment(Of Byte)(bufferRisposta), WebSocketMessageType.Text, True, CancellationToken.None)
                    End If

                    Exit Function
                End If

                ' ===================================================================
                ' 📄 RICHIESTA DATI REALI (Attiva solo se l'Handshake precedente ha ritornato OK)
                ' ===================================================================


                ' Nota: puoi usare un parser JSON (come JavaScriptSerializer) oppure un semplice Contains per massima velocità
                If messaggioRicevuto.Contains("""type"":""RICHIEDI_ELENCO""") Then
                    ' 🎯 Il tablet ha premuto "Aggiorna Elenco Fogli" -> Inviamo la lista dei bambini
                    Dim datiElenco As String = GeneraJsonElencoFogli()
                    rispostaJson = "{""type"":""ELENCO_FOGLI_RISPOSTA"", ""dati"":" & datiElenco & "}"

                ElseIf messaggioRicevuto.Contains("""type"":""SALVA_FOGLIO""") Then
                    ' 🎯 Il tablet ha inviato il foglio compilato -> Esegui qui la tua scrittura su file/DB
                    ' (Il testo inviato conterrà anche i dati compilati)
                    rispostaJson = "{""type"":""SALVA_FOGLIO_OK""}"
                End If

                ' Se abbiamo una risposta pronta, la rispediamo indietro dentro il tubo aperto
                If Not String.IsNullOrEmpty(rispostaJson) Then
                    Dim bufferRisposta() As Byte = Encoding.UTF8.GetBytes(rispostaJson)
                    Await ws.SendAsync(New ArraySegment(Of Byte)(bufferRisposta), WebSocketMessageType.Text, True, CancellationToken.None)
                End If

            Catch ex As Exception
                Console.WriteLine("Errore durante la comunicazione WebSocket: " & ex.Message)
                Exit While
            End Try
        End While
    End Function



    Public Shared Function GeneraJsonElencoFogli() As String
        ' leggo il contenuto della tabella todolist
        Dim elenco1 As List(Of Class_TODOLIST) = Class_TODOLIST.LoadAll

        If elenco1.Count = 0 Then
            Return "[]"
        End If


        Dim voceJson As New Struct_Elencofogli_Json

        Try
            ' Creiamo la lista vuota basata sulla tua struttura dati
            Dim elenco As New List(Of Struct_Elencofogli_Json)()

            ' Carico la struttura Elenco fogli json da inviar
            For Each voce In elenco1
                voceJson.ID_Questionario = voce.ID
                voceJson.Data_foglio = voce.DATA_FOGLIO
                voceJson.ID_foglio = voce.ID_FOGLIO
                voceJson.ID_Bambino = voce.ID_BAMBINO
                voceJson.Filename = voce.NOMEFILE
                voceJson.Bambino = voce.BAMBINO

                elenco.Add(voceJson)
            Next


            ' 🎯 CHIAMATA ASSOLUTA: Specifichiamo tutto il percorso per evitare conflitti con System.Text
            Dim jsonOttenuto As String = System.Text.Json.JsonSerializer.Serialize(elenco)
            Return jsonOttenuto


        Catch ex As Exception
            ' In caso di errore restituiamo un array vuoto per non bloccare l'app
            Return "[]"
        End Try
    End Function

    Public Shared Function OttieniIPRealiAttivi() As List(Of String)
        Dim listaIPReali As New List(Of String)()

        Try
            ' Recupera l'elenco di tutte le schede di rete presenti sul PC Windows
            Dim schedeDiRete() As NetworkInterface = NetworkInterface.GetAllNetworkInterfaces()

            For Each scheda As NetworkInterface In schedeDiRete
                ' 1. FILTRO HARDWARE: La scheda deve essere accesa, attiva e NON deve essere di Loopback (interna)
                If scheda.OperationalStatus = OperationalStatus.Up AndAlso
                   scheda.NetworkInterfaceType <> NetworkInterfaceType.Loopback Then

                    Dim nomeScheda As String = scheda.Name.ToLower()
                    Dim descrizione As String = scheda.Description.ToLower()

                    ' 2. FILTRO VIRTUALIZZAZIONE: Scarta le schede create da simulatori (VirtualBox, VMware, VPN, ecc.)
                    If Not descrizione.Contains("virtual") AndAlso
                       Not descrizione.Contains("vbox") AndAlso
                       Not descrizione.Contains("vmware") AndAlso
                       Not descrizione.Contains("vpn") Then

                        ' Recupera gli indirizzi IP assegnati a questa specifica scheda hardware
                        Dim proprietaIP As IPInterfaceProperties = scheda.GetIPProperties()

                        For Each unirete As UnicastIPAddressInformation In proprietaIP.UnicastAddresses
                            ' 3. FILTRO PROTOCOLLO: Prende solo gli indirizzi IPv4 standard (es. 192.168.x.x)
                            If unirete.Address.AddressFamily = AddressFamily.InterNetwork Then
                                ' Aggiunge l'IP reale alla lista dei risultati
                                listaIPReali.Add(unirete.Address.ToString())
                            End If
                        Next
                    End If
                End If
            Next

        Catch ex As Exception
            listaIPReali.Add("Errore durante la lettura: " & ex.Message)
        End Try

        ' Se non trova nessuna rete fisica, restituisce l'IP di emergenza localhost
        If listaIPReali.Count = 0 Then
            listaIPReali.Add("127.0.0.1")
        End If

        Return listaIPReali
    End Function

    Public Shared Function OttieniIPAscoltoReale() As String
        Try
            ' Creiamo un socket di test temporaneo (UDP)
            Using socketTest As New Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp)
                ' Bussiamo a un IP esterno fittizio (es. i DNS di Google). 
                ' NOTA: non serve internet attivo, serve solo a costringere Windows a scegliere la scheda di rete attiva.
                socketTest.Connect("8.8.8.8", 65530)

                ' Recuperiamo l'endpoint locale che Windows ha assegnato a questa comunicazione
                Dim endPointLocale As IPEndPoint = TryCast(socketTest.LocalEndPoint, IPEndPoint)

                If endPointLocale IsNot Nothing Then
                    ' Questo è l'unico IP reale accoppiato alla tua scheda Wi-Fi o Ethernet attiva!
                    Return endPointLocale.Address.ToString()
                End If
            End Using
        Catch ex As Exception
            ' Se il PC è totalmente isolato e senza alcuna rete, intercettiamo l'errore
        End Try

        Return "127.0.0.1"
    End Function

End Class

