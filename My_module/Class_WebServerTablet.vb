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
    Public Shared FormPrincipaleRiferimento As F_main
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
                        ' Il dispositivo è già abilitato, mandiamo l'OK
                        rispostaJson = "{""type"":""HANDSHAKE_OK"",""nome_dispositivo"":""" & nomeAssegnato & """}"

                        'ElseIf statoHardware = 0 Then
                        '    ' È già nel DB ma ancora in attesa. 
                        '    ' Mostriamo comunque la maschera nel caso l'amministratore voglia abilitarlo ora
                        '    MostraMascheraAbilitazioneInThreadSicuro(deviceId, notaUtente)

                        '    rispostaJson = "{""type"":""HANDSHAKE_PENDING"",""messaggio"":""In attesa di abilitazione...""}"

                        'ElseIf statoHardware = -1 Then
                        '    ' Nuovo dispositivo: lo registriamo nel DB come pending
                        '    Dim inserimentoOK As Boolean = Class_DEVICE.RegistraNuovoDispositivo(deviceId, notaUtente)

                        '    ' INTERCETTAZIONE: Apriamo la maschera di approvazione a schermo sul Server!
                        '    MostraMascheraAbilitazioneInThreadSicuro(deviceId, notaUtente)

                        '    rispostaJson = "{""type"":""HANDSHAKE_PENDING"",""messaggio"":""Richiesta inviata con successo...""}"

                    ElseIf statoHardware = 0 OrElse statoHardware = -1 Then
                        ' Se è un nuovo dispositivo (-1), lo registriamo nel DB come pending
                        If statoHardware = -1 Then
                            Dim inserimentoOK As Boolean = Class_DEVICE.RegistraNuovoDispositivo(deviceId, notaUtente)
                        End If

                        ' INTERCETTAZIONE SILENZIOSA: Aggiungiamo alla coda ed accendiamo l'icona sul Form principale
                        AttivaSegnalazioneSulForm(deviceId)

                        rispostaJson = "{""type"":""HANDSHAKE_PENDING"",""messaggio"":""In attesa di abilitazione...""}"
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

                ' --- 2. GESTIONE INVIO ELENCO FOGLI ---
                If messaggioRicevuto.Contains("""type"":""RICHIEDI_ELENCO""") Then
                    Dim datiElenco As String = GeneraJsonElencoFogli()
                    rispostaJson = "{""type"":""ELENCO_FOGLI_RISPOSTA"", ""dati"":" & datiElenco & "}"
                End If

                ' --- 3. GESTIONE INVIO FOGLIO RICHIESTO ---
                If messaggioRicevuto.Contains("""type"":""RICHIEDI_FOGLIO""") Then
                    ' Eseguiamo il parsing nativo del JSON inviato dal tablet
                    Dim parser As System.Text.Json.JsonDocument = System.Text.Json.JsonDocument.Parse(messaggioRicevuto)
                    Dim root As System.Text.Json.JsonElement = parser.RootElement

                    ' Estraiamo in modo matematico l'ID inviato dinamicamente dal tablet (es. 101, 102, ecc.)
                    Dim idRichiesto As Integer = root.GetProperty("id_questionario").GetInt32()
                    Dim jsonFoglioBambino As String = Carica_Foglio_da_inviare_al_Tablet(idRichiesto)
                    rispostaJson = "{""type"":""FOGLIO_RICEVUTO"", ""dati"":" & jsonFoglioBambino & "}"

                    ' 🎯 LA CORREZIONE: Spingiamo fisicamente i byte nel Socket verso il tablet!
                    If Not String.IsNullOrEmpty(rispostaJson) Then
                        Dim bufferRisposta() As Byte = Encoding.UTF8.GetBytes(rispostaJson)
                        Await ws.SendAsync(New ArraySegment(Of Byte)(bufferRisposta), WebSocketMessageType.Text, True, CancellationToken.None)
                        Console.WriteLine("🟢 SUCCESS: File JSON del foglio spedito al tablet con successo.")
                    End If

                    ' 🎯 IMPORTANTE: Interrompiamo questo ciclo di messaggi per rimettere la While in ascolto del click dopo
                    Exit Function
                End If


            Catch ex As Exception
                Console.WriteLine("CRASH DENTRO IL LOOP DEL SOCKET VB: " & ex.Message)
                Exit While
            End Try
        End While

        Console.WriteLine("🏁 Thread di ascolto terminato. Il socket non è più monitorato.")
    End Function

    Private Sub AttivaSegnalazioneSulForm(ByVal deviceId As String)

        ' Controlliamo che sia stata accoppiata
        If Class_WebServerTablet.FormPrincipaleRiferimento Is Nothing Then Return
        Dim frmReal As F_main = Class_WebServerTablet.FormPrincipaleRiferimento
        If frmReal.InvokeRequired Then
            frmReal.Invoke(Sub() AttivaSegnalazioneSulForm(deviceId))
        Else
            ' Evita di aggiungere duplicati se il tablet invia più richieste
            If Not DispositiviInAttesa.Contains(deviceId) Then
                DispositiviInAttesa.Add(deviceId)
            End If

            ' Accendi la tua icona di segnalazione (es. una PictureBox o un pulsante)
            ' Immaginiamo si chiami PctNotifica
            frmReal.PctNotifica.Visible = True
            frmReal.PctNotifica.Refresh()
            ' Opzionale: puoi cambiare il colore di sfondo o mostrare un contatore, es:
            ' Form1.BtnNotifiche.Text = "Richieste (" & Form1.DispositiviInAttesa.Count & ")"
        End If
    End Sub
    Private Async Function GestisciMessaggiTablet_OLD(ByVal ws As WebSocket) As Task
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

                ' --- 2. GESTIONE INVIO ELENCO FOGLI ---
                If messaggioRicevuto.Contains("""type"":""RICHIEDI_ELENCO""") Then
                    Dim datiElenco As String = GeneraJsonElencoFogli()
                    rispostaJson = "{""type"":""ELENCO_FOGLI_RISPOSTA"", ""dati"":" & datiElenco & "}"
                End If

                ' --- 3. GESTIONE INVIO FOGLIO RICHIESTO ---
                If messaggioRicevuto.Contains("""type"":""RICHIEDI_FOGLIO""") Then
                    ' Eseguiamo il parsing nativo del JSON inviato dal tablet
                    Dim parser As System.Text.Json.JsonDocument = System.Text.Json.JsonDocument.Parse(messaggioRicevuto)
                    Dim root As System.Text.Json.JsonElement = parser.RootElement

                    ' Estraiamo in modo matematico l'ID inviato dinamicamente dal tablet (es. 101, 102, ecc.)
                    Dim idRichiesto As Integer = root.GetProperty("id_questionario").GetInt32()
                    Dim jsonFoglioBambino As String = Carica_Foglio_da_inviare_al_Tablet(idRichiesto)
                    rispostaJson = "{""type"":""FOGLIO_RICEVUTO"", ""dati"":" & jsonFoglioBambino & "}"

                    ' 🎯 LA CORREZIONE: Spingiamo fisicamente i byte nel Socket verso il tablet!
                    If Not String.IsNullOrEmpty(rispostaJson) Then
                        Dim bufferRisposta() As Byte = Encoding.UTF8.GetBytes(rispostaJson)
                        Await ws.SendAsync(New ArraySegment(Of Byte)(bufferRisposta), WebSocketMessageType.Text, True, CancellationToken.None)
                        Console.WriteLine("🟢 SUCCESS: File JSON del foglio spedito al tablet con successo.")
                    End If

                    ' 🎯 IMPORTANTE: Interrompiamo questo ciclo di messaggi per rimettere la While in ascolto del click dopo
                    Exit Function
                End If


            Catch ex As Exception
                Console.WriteLine("CRASH DENTRO IL LOOP DEL SOCKET VB: " & ex.Message)
                Exit While
            End Try
        End While

        Console.WriteLine("🏁 Thread di ascolto terminato. Il socket non è più monitorato.")
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

    Private Sub MostraMascheraAbilitazioneInThreadSicuro(ByVal deviceId As String, ByVal notaUtente As String)
        ' Verifica qual è il Form principale attivo sul tuo server per usare il suo contesto grafico
        ' Esempio: se il tuo form principale si chiama FormMainServer
        If F_main.InvokeRequired Then
            ' Se il socket è in un thread secondario, invoca la funzione sul thread principale
            F_main.Invoke(Sub() MostraMascheraAbilitazioneInThreadSicuro(deviceId, notaUtente))
        Else
            ' Siamo sul thread principale: possiamo aprire la maschera in sicurezza!
            Dim frm As New F_Client()

            ' Passiamo i dati del tablet al form di approvazione per mostrarli a schermo
            frm.LblDeviceID.Text = deviceId
            frm.T_nomeclient.Text = notaUtente

            ' Mostra il form a schermo (ShowDialog impedisce di cliccare altrove finché non si decide)
            frm.ShowDialog()
        End If
    End Sub
End Class

