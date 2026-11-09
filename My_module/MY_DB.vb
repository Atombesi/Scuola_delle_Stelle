
Imports System.IO
Imports Microsoft.Data.Sqlite
Public Class MY_DB
    ' Stringa di connessione

    Public Shared ReadOnly _ConnectionString_Scuoladellestelle As String =
    "Data Source=" & ini.Database

    Public Shared Property ConnectionString_Bambini As String =
        "Server=.\SQLEXPRESS;Database=gbl_db_bambini;Trusted_Connection=True;"
    '"Server=.\SQLEXPRESS;Database=DefaultVariabile;Trusted_Connection=True;"
    '"Data Source=" & Path.Combine("G:\My\My_Sorgenti\Visualstudio\ScuoladelleStelle", "Scuoladellestelle.db")

    ' Restituisce unanuova connessione
    Public Shared Function GetConnection_Scuoladellestelle() As SqliteConnection
        Dim cn As New SqliteConnection(_ConnectionString_Scuoladellestelle)
        cn.Open()
        Return cn
    End Function

    Public Shared Function GetConnection_Bambino(Optional Db_bambino As String = "") As SqliteConnection
        Dim percorsoFileBambino = ""
        If Db_bambino = "" Then
            Gbl_DB_BAMBINO = Gbl_DB_BAMBINO.ToUpper
            If InStr(Gbl_DB_BAMBINO, ".DB") = 0 Then Gbl_DB_BAMBINO = Gbl_DB_BAMBINO + ".DB"
            percorsoFileBambino = IO.Path.Combine(ini.DB_Bambini_path, Gbl_DB_BAMBINO).ToUpper
        Else
            Db_bambino = Db_bambino.ToUpper
            If InStr(Db_bambino, ".DB") = 0 Then Db_bambino = Db_bambino + ".DB"
            percorsoFileBambino = IO.Path.Combine(ini.DB_Bambini_path, Db_bambino).ToUpper
        End If
        ' Compone la stringa nel formato corretto per SQLite ("Data Source=...")
        Dim connectionStringDinamica As String = "Data Source=" & percorsoFileBambino

        ' Crea, apre e restituisce la connessione SQLite
        Dim cn As New SqliteConnection(connectionStringDinamica)
        cn.Open()
        Return cn

    End Function

    Public Shared Function Foglio_Esistente_Aggiorna_solo_Totali(foglio As Struct_foglio, dbbambino As String) As (OK As Boolean, erroreSu As String)
        '-- SALVO SOLO I TOTALI --
        '-- Se sono qui devo essere sicuro che il foglio esista gia --
        ' return (0) Salvataggio completo
        ' return (-1) Nessun record salvato
        ' return >0  Salvataggio parziale record

        Dim CPcnt = 0, RTcnt = 0, ELCARcnt = 0, m = "", Err = ""
        Dim salvare_cp = True, salvare_rt = True, salvato = 0
        Try
            CPcnt = foglio.CP.Count
            RTcnt = foglio.RT.Count
            ELCARcnt = foglio.VociDiUnFoglio.Count
        Catch ex As Exception
            Return (False, "NESSUN RECORD SALVATO")
        End Try
        Dim ok_voci = True, ok_rt = True, Ok_CP = True, ok = True

        Using cn = MY_DB.GetConnection_Bambino(dbbambino)
            Dim trans As SqliteTransaction = Nothing

            Try
                cn.Open()
                ' 2. Avviamo la transazione globale
                trans = cn.BeginTransaction()

                ' -- Registro nel GIORNALIERO -- 
                For j = 0 To ELCARcnt - 1
                    ' Passiamo la connessione e la transazione correnti
                    If Not foglio.VociDiUnFoglio(j).Salva_Totali(cn, trans) Then
                        ok = False : Err += " [VOCI ELCAR]"
                        Exit For ' Inutile continuare se uno fallisce
                    End If
                Next

                ' -- Registro nel GIORNALIERO_CP -- 
                If ok Then
                    For j = 0 To CPcnt - 1
                        If Not foglio.CP(j).Salva_Totali(cn, trans) Then
                            ok = False : Err += " [VOCI CP]"
                            Exit For
                        End If
                    Next
                End If

                ' -- Registro nel GIORNALIERO_ROUTINE -- 
                If ok Then
                    For j = 0 To RTcnt - 1
                        If Not foglio.RT(j).Salva_Totali(cn, trans) Then
                            ok = False : Err += " [VOCI RT]"
                            Exit For
                        End If
                    Next
                End If

                ' 3. Controllo finale
                If ok Then
                    ' Se tutto è andato bene, salviamo DEFINITIVAMENTE i dati nel DB
                    trans.Commit()
                    Return (True, "SALVATAGGIO OK")
                Else
                    ' Se qualcosa è fallito nei cicli, annulliamo TUTTO quello che era stato fatto finora
                    trans.Rollback()
                    Return (False, "Problemi salvataggio di:" & Err)
                End If

            Catch ex As Exception
                ' Se crasha il database o scatta un blocco/timeout imprevisto
                If trans IsNot Nothing Then trans.Rollback()
                Return (False, "Errore critico di sistema durante il salvataggio: " & ex.Message)
            End Try
        End Using
    End Function

    Public Shared Function Foglio_salva(idff, dbbambino, giornaliero) As (OK As Boolean, erroreSu As String)
        '-- Preparo messagio di conferma salvataggio
        Dim CPcnt = 0, RTcnt = 0, ELCARcnt = 0
        Dim salvare_cp = True, salvare_rt = True
        Dim t = "Salvataggio del foglio" + vbCrLf
        Try
            CPcnt = Gbl_foglio.CP.Count
        Catch ex As Exception
        End Try
        Try
            RTcnt = Gbl_foglio.RT.Count
        Catch ex As Exception
        End Try
        Try
            ELCARcnt = Gbl_foglio.VociDiUnFoglio.Count
        Catch ex As Exception
        End Try

        Dim testo = "Foglio n." + Gbl_foglio.ID_foglio.ToString + " del:" + Gbl_foglio.Data + vbCrLf
        testo = testo + "N. Esercizi:     " + ELCARcnt.ToString + vbCrLf
        testo = testo + "N. voci CP:      " + CPcnt.ToString + vbCrLf
        testo = testo + "N. voci ROUTINE: " + RTcnt.ToString + vbCrLf
        Dim m = testo + vbCrLf + "Vuoi salvare il nuovo foglio di esercizi?"
        If MsgBox(testo, vbYesNo + vbQuestion, t) <> vbYes Then
            Return (False, "")
        End If

        'Procedo con il salvataggio nelle 3 tabelle
        If CPcnt = 0 Then salvare_cp = False
        If RTcnt = 0 Then salvare_rt = False

        'Controllo se esiste gia nel database un foglio con lo stesso titolo e Data
        Dim elenco_fogli As New Class_Elenco_fogli()
        Dim trovato = elenco_fogli.Foglio_esistente(Gbl_foglio.Data)
        If trovato Then
            m = "Non è stato possibile salvare il nuovo foglio " + vbCrLf
            m = m + "perchè è stato trovato un altro foglio con la stesso stessa Data"
            m = m + "foglio id: " + Gbl_foglio.ID_foglio.ToString + ") del: " + Gbl_foglio.Data + ""
            Call MsgBox(m, vbCritical + vbOKOnly)
            m = "[001] Salvataggio foglio " + Gbl_foglio.ID_foglio.ToString + ") del: " + Gbl_foglio.Data + " " + Gbl_foglio.DB_Bambino
            APPEND_LOG(m, ini.File_Log, True, "E")
            Dim st As New StackTrace()
            Dim frame As StackFrame = st.GetFrame(1)
            Return (False, "Data esistente")
        End If

        Dim salvato = True, err = ""

        Using cn = MY_DB.GetConnection_Bambino(dbbambino)
            Dim trans As SqliteTransaction = Nothing
            Try
                cn.Open()
                trans = cn.BeginTransaction()

                '-- Registro nel GIORNALIERO -- 
                For j = 0 To ELCARcnt - 1
                    If Gbl_foglio.VociDiUnFoglio(j).Codice = "" Then Continue For

                    If Not Gbl_foglio.VociDiUnFoglio(j).Salva_nuovo(cn, trans) Then
                        ' Nota: avevi scritto "giornaliero.Codice", ma l'oggetto nel ciclo è Gbl_foglio.VociDiUnFoglio(j)
                        Dim v = Gbl_foglio.VociDiUnFoglio(j)
                        m = "Problemi salvataggio codice (" + v.Codice + ") id_foglio:" + v.ID_FOGLIO.ToString + " DB:" + Gbl_DB_BAMBINO
                        APPEND_LOG(m, GbL_Logfile, True, "E")
                        err += " [VOCI ELCAR]"
                        salvato = False
                        Exit For ' Inutile continuare a fare tentativi se uno è fallito
                    End If
                Next

                '-- Registro nel GIORNALIERO_CP -- 
                If salvato AndAlso salvare_cp Then ' Continua solo se "salvato" è ancora True
                    Gbl_CP_selezionate = Gbl_foglio.CP
                    For j = 0 To Gbl_CP_selezionate.Count - 1
                        If Not Gbl_foglio.CP(j).Salva_nuovo(cn, trans) Then
                            m = "[002] Problemi salvataggio CP (" + Gbl_foglio.CP(j).TESTO + ") id_foglio:" + Gbl_foglio.CP(j).ID_FOGLIO.ToString + " DB:" + Gbl_foglio.DB_Bambino
                            APPEND_LOG(m, GbL_Logfile, True, "E")
                            err += " [VOCI CP]"
                            salvato = False
                            Exit For
                        End If
                    Next
                End If

                '-- Registro nel GIORNALIERO_ROUTINE -- 
                If salvato AndAlso salvare_rt Then ' Continua solo se "salvato" è ancora True
                    Gbl_RT_selezionate = Gbl_foglio.RT
                    For j = 0 To Gbl_RT_selezionate.Count - 1
                        If Not Gbl_foglio.RT(j).Salva_nuovo(cn, trans) Then
                            m = "[003] Problemi salvataggio RT (" + Gbl_foglio.RT(j).TESTO + ") id_foglio:" + Gbl_foglio.RT(j).ID_FOGLIO.ToString + " DB:" + Gbl_foglio.DB_Bambino
                            APPEND_LOG(m, GbL_Logfile, True, "E")
                            err += " [VOCI RT]"
                            salvato = False
                            Exit For
                        End If
                    Next
                End If

                ' 3. Controllo finale
                If salvato Then
                    trans.Commit()
                    Return (True, "SALVATAGGIO OK")
                Else
                    trans.Rollback()
                    Return (False, "Problemi salvataggio di:" & err)
                End If

            Catch ex As Exception
                If trans IsNot Nothing Then
                    Try
                        trans.Rollback()
                    Catch rollEx As Exception
                        ' Evita che un fallimento del rollback nasconda l'eccezione principale
                    End Try
                End If
                Return (False, "Errore critico di sistema durante il salvataggio: " & ex.Message)
            End Try
        End Using
        Return (salvato, err)



    End Function


End Class

