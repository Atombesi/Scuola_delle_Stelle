Imports System.IO
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading
Imports System.Net.WebSockets
Imports System.Text.Json
Imports System.Windows
Imports System.Windows.Input

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
        ' Buffer di transito per i singoli segmenti in arrivo dalla rete
        Dim buffer(4096) As Byte
        Console.WriteLine("🔄 Thread di ascolto WebSocket avviato per il dispositivo.")
        Dim handshakeCompletato As Boolean = False
        Dim deviceIdAutenticato As String = ""

        While ws.State = WebSocketState.Open
            Try
                Dim messaggioRicevuto As String = ""

                ' 🎯 SOLUZIONE OVERFLOW: Usiamo un MemoryStream per accumulare pacchetti di qualsiasi dimensione (anche > 4KB)
                Using ms As New System.IO.MemoryStream()
                    Dim result As WebSocketReceiveResult

                    Do
                        result = Await ws.ReceiveAsync(New ArraySegment(Of Byte)(buffer), CancellationToken.None)

                        ' Se il socket si sta chiudendo, usciamo immediatamente
                        If result.MessageType = WebSocketMessageType.Close Then
                            Await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Chiusura richiesta", CancellationToken.None)
                            Console.WriteLine("🔴 CANALE CHIUSO dal tablet.")
                            Debug.Print("CANALE CHIUSO dal tablet.")
                            Exit Function
                        End If

                        ' Accumula i byte letti nel flusso di memoria
                        ms.Write(buffer, 0, result.Count)

                    Loop While Not result.EndOfMessage ' Continua a pescare finché il tablet non ha finito di inviare l'intero file

                    ' Trasformiamo l'intera sequenza di byte accumulata nella stringa JSON grezza finale
                    messaggioRicevuto = Encoding.UTF8.GetString(ms.ToArray())
                    Debug.Print(messaggioRicevuto)
                End Using

                Console.WriteLine("Messaggio completo ricevuto dal tablet. Lunghezza caratteri: " & messaggioRicevuto.Length)

                ' Spacchettamento preventivo immediato per estrarre tipo comando e device_id
                Dim msgDecodificato As Struct_Decode_Android_message = Decode_Android_message(messaggioRicevuto)
                Dim rispostaJson As String = ""


                ' TABLET-REG-03 - INIZIO: controllo autorizzazione tablet

                Dim comando As String = msgDecodificato.type

                If comando <> "HANDSHAKE" AndAlso comando <> "PING_VERIFICA" Then

                    Dim nomeTablet As String = ""
                    Dim statoTablet As Integer = -2

                    ' Il dispositivo deve avere completato almeno un handshake
                    ' valido su questa connessione WebSocket.
                    If handshakeCompletato Then
                        statoTablet = Class_DEVICE.Dispositivo_Registrato_Stato(
            deviceIdAutenticato, nomeTablet)
                    End If

                    If statoTablet <> 1 Then

                        rispostaJson = JsonSerializer.Serialize(New With {
            .type = "ACCESSO_NEGATO",
            .messaggio = "Tablet non autorizzato"
        })

                        Dim bytesRisposta() As Byte = Encoding.UTF8.GetBytes(rispostaJson)

                        Await ws.SendAsync(
            New ArraySegment(Of Byte)(bytesRisposta),
            WebSocketMessageType.Text,
            True,
            CancellationToken.None)

                        Continue While
                    End If

                End If

                ' TABLET-REG-03 - FINE


                ' Switch dei comandi basato sulla struttura
                Select Case msgDecodificato.type
                                        ' --- 1. GESTIONE PACCHETTO HANDSHAKE ---

' TABLET-REG-02B - INIZIO: gestione handshake e registrazione tablet
                    Case "HANDSHAKE"

                        Dim nomeAssegnato As String = ""
                        Dim notaUtente As String = ""

                        Using parser As JsonDocument = JsonDocument.Parse(messaggioRicevuto)
                            Dim root As JsonElement = parser.RootElement
                            Dim elementoNota As JsonElement

                            If root.TryGetProperty("nota_utente", elementoNota) AndAlso
                               elementoNota.ValueKind = JsonValueKind.String Then
                                notaUtente = elementoNota.GetString()
                            End If
                        End Using

                        Dim deviceId As String = msgDecodificato.deviceId
                        Dim stato As Integer = Class_DEVICE.Dispositivo_Registrato_Stato(deviceId, nomeAssegnato)

                        Select Case stato

                            Case 1
                                handshakeCompletato = True
                                deviceIdAutenticato = deviceId
                                rispostaJson = JsonSerializer.Serialize(New With {.type = "HANDSHAKE_OK", .nome_dispositivo = nomeAssegnato})
                            Case 0
                                AttivaSegnalazioneSulForm(deviceId)
                                rispostaJson = "{""type"":""HANDSHAKE_PENDING"",""messaggio"":""In attesa di abilitazione...""}"

                            Case -1
                                If String.IsNullOrWhiteSpace(notaUtente) Then
                                    rispostaJson = "{""type"":""HANDSHAKE_REGISTRATION_REQUIRED""}"
                                Else
                                    Dim registrato As Boolean =
                                        Class_DEVICE.RegistraNuovoDispositivo(deviceId, notaUtente)

                                    If registrato Then
                                        AttivaSegnalazioneSulForm(deviceId)
                                        rispostaJson = "{""type"":""HANDSHAKE_PENDING"",""messaggio"":""In attesa di abilitazione...""}"
                                    Else
                                        rispostaJson = "{""type"":""HANDSHAKE_ERROR"",""messaggio"":""Registrazione non riuscita""}"
                                    End If
                                End If

                            Case Else
                                rispostaJson = "{""type"":""HANDSHAKE_ERROR"",""messaggio"":""Errore verifica dispositivo""}"

                        End Select

' TABLET-REG-02B - FINE



                    ' --- 2. GESTIONE PACCHETTO RICHIESTA ELENCO ---
                    Case "RICHIEDI_ELENCO"
                        Dim datiElenco As String = GeneraJsonElencoFogli(msgDecodificato.deviceId)
                        rispostaJson = "{""type"":""ELENCO_FOGLI_RISPOSTA"", ""dati"":" & datiElenco & "}"
                        Debug.Print(rispostaJson)

                    ' --- 3. GESTIONE INVIO FOGLIO RICHIESTO ---
                    Case "RICHIEDI_FOGLIO"
                        Dim parser As System.Text.Json.JsonDocument = System.Text.Json.JsonDocument.Parse(messaggioRicevuto)
                        Dim root As System.Text.Json.JsonElement = parser.RootElement
                        'Debug.Print(rispostaJson)

                        Dim idRichiesto As Integer = root.GetProperty("id_questionario").GetInt32()
                        Dim jsonFoglioBambino As String = Carica_Foglio_da_inviare_al_Tablet(idRichiesto)
                        rispostaJson = "{""type"":""FOGLIO_RICEVUTO"", ""dati"":" & jsonFoglioBambino & "}"
                        Debug.Print(rispostaJson)

                        If Not String.IsNullOrEmpty(rispostaJson) Then
                            Dim bufferRisposta() As Byte = Encoding.UTF8.GetBytes(rispostaJson)
                            Await ws.SendAsync(New ArraySegment(Of Byte)(bufferRisposta), WebSocketMessageType.Text, True, CancellationToken.None)
                            Console.WriteLine("🟢 SUCCESS: File JSON del foglio spedito al tablet con successo.")
                        End If
                        rispostaJson = ""

                    ' --- 4. CONFERMA RICEZIONE FOGLIO ---
                    Case "CONFERMA_RICEZIONE_FOGLIO"
                        Call TODOLIST_Aggiorna_Foglio_In_Compilazione(messaggioRicevuto)
                        Debug.Print(rispostaJson)

                    ' --- 5. PING_VERIFICA ---
                    Case "PING_VERIFICA"
                        rispostaJson = "{""type"":""PONG_ATTIVO""}"

                        Debug.Print(rispostaJson)
                    ' --- 6. SALVA FOGLIO COMPILATO ---
                    Case "SALVA_FOGLIO_COMPILATO"
                        ' 1. Esegui la sotto-routine che elabora il JSON per scrivere le risposte finali nel DB SQLite di Windows
                        Call SalvaFoglioCompilato_Json(messaggioRicevuto)
                        Debug.Print(rispostaJson)

                        ' 2. 🎯 RISPOSTA DI CONFERMA: Spariamo la parola chiave obbligatoria concordata con il tablet
                        ' Questo messaggio sbloccherà il tablet, azzerando lo schermo e richiedendo il rinfresco
                        rispostaJson = "{""type"":""FOGLIO_RICEVUTO"",""dati"":{""stato"":""OK_SALVATO""}}"
                        Debug.Print(rispostaJson)

                    ' --- 7. COMPILAZIONE ANNULLATA ---
                    Case "COMPILAZIONE_ANNULLATA"
                        ' 1. Esegui la sotto-routine che elabora il JSON per scrivere le risposte finali nel DB SQLite di Windows
                        Dim parser As System.Text.Json.JsonDocument = System.Text.Json.JsonDocument.Parse(messaggioRicevuto)
                        Dim root As System.Text.Json.JsonElement = parser.RootElement
                        Dim idRichiesto As Integer = root.GetProperty("id_questionario").GetInt32()

                        Call Comunica_Compilazione_annullata(idRichiesto)
                        Debug.Print(rispostaJson)

                    Case Else
                        Console.WriteLine("⚠️ Messaggio non riconosciuto dal motore di switch: " & msgDecodificato.type)
                        Debug.Print(rispostaJson)
                End Select

                ' Spedisce la risposta automatica cumulativa nel socket verso il tablet
                If Not String.IsNullOrEmpty(rispostaJson) Then
                    Dim bufferRisposta() As Byte = Encoding.UTF8.GetBytes(rispostaJson)
                    Await ws.SendAsync(New ArraySegment(Of Byte)(bufferRisposta), WebSocketMessageType.Text, True, CancellationToken.None)
                End If

            Catch ex As Exception
                Console.WriteLine("CRASH DENTRO IL LOOP DEL SOCKET VB: " & ex.Message)
                Debug.Print("CRASH DENTRO IL LOOP DEL SOCKET VB")
                Exit While
            End Try
        End While
        ' codice per forzare il cambio nome del device 
        'Dim comandoCambioNome As String = "{""type"":""AGGIORNA_NOME_TABLET"",""nome_dispositivo"":""NUOVO_NOME_ASSEGNATO""}"
        'Dim bufferNome() As Byte = Encoding.UTF8.GetBytes(comandoCambioNome)
        'Await ws.SendAsync(New ArraySegment(Of Byte)(bufferNome), WebSocketMessageType.Text, True, CancellationToken.None)
        Console.WriteLine("🏁 Thread di ascolto terminator. Il socket non è più monitorato.")
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



    Public Shared Function GeneraJsonElencoFogli(deviceid As String) As String

        ' Aggiorna la todolist 
        Call Todolist_Azzera_e_Ricrea(False)

        ' leggo il contenuto della tabella todolist
        Dim elenco1 As List(Of Class_TODOLIST) = Class_TODOLIST.LoadAll
        Dim Tablet_id = 0
        Dim tablet_name = ""

        Dim result = Class_DEVICE.Trova_dispositivo(deviceid)
        If result.Esito Then
            Tablet_id = result.Devices.ID
            tablet_name = result.Devices.NOME
        End If

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
                If voce.CLIENT_ID = Tablet_id Then
                    voceJson.note = "DISPONIBILE per " + tablet_name.ToString
                    voceJson.disponibile = 1  ' 1=disponibile
                ElseIf voce.CLIENT_ID = 0 Then
                    voceJson.note = "DISPONIBILE"
                    voceJson.disponibile = 1  ' 1=disponibile
                Else
                    voceJson.note = voce.CLIENT_DATARICONSEGNA
                    voceJson.note = "IN USO DA: " + voce.CLIENT_NOME
                    voceJson.disponibile = 0  ' 0=NON disponibile
                End If
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

