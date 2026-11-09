Imports System.Drawing.Drawing2D
Imports QRCoder

Module Module_QRCODE
    Public Function GeneraQRCode_Senzabordo(testo As String, Optional pixelPerModulo As Integer = 10, Optional Spessorebordo As Integer = 1) As Bitmap

        Using qrGenerator As New QRCodeGenerator()
            Using qrCodeData As QRCodeData = qrGenerator.CreateQrCode(testo, QRCodeGenerator.ECCLevel.Q)
                Using qrCode As New QRCode(qrCodeData)

                    ' 1. Genera il QR senza il bordo della libreria (False)
                    Using qrSenzaBordo As Bitmap = qrCode.GetGraphic(pixelPerModulo, MyColor.Black, MyColor.White, False)

                        ' 2. Scegli lo spessore del tuo bordo personalizzato (es. 6 pixel)

                        ' 3. Crea una nuova bitmap più grande per ospitare il bordo
                        Dim qrConBordo As New Bitmap(qrSenzaBordo.Width + (Spessorebordo * 2), qrSenzaBordo.Height + (Spessorebordo * 2))

                        ' 4. Disegna lo sfondo bianco e incolla il QR al centro
                        Using g As Graphics = Graphics.FromImage(qrConBordo)
                            g.Clear(MyColor.White) ' Colore del bordo
                            g.DrawImage(qrSenzaBordo, Spessorebordo, Spessorebordo)
                        End Using

                        Return qrConBordo
                    End Using

                End Using
            End Using
        End Using
    End Function

    Function ROTATE(BMP As Image, Optional angolo As Integer = 90) As Image
        'BMP = img
        If angolo = 90 Then
            BMP.RotateFlip(RotateFlipType.Rotate270FlipNone)
        ElseIf angolo = 180 Then
            BMP.RotateFlip(RotateFlipType.Rotate180FlipNone)
        End If
        Return BMP
    End Function


    Public Function RuotaImmagine(ByVal imgOrigine As Bitmap, ByVal angoloGradi As Single) As Bitmap
        ' Calcola le nuove dimensioni per evitare che gli angoli vengano tagliati durante la rotazione
        ' (Facoltativo: se l'angolo è minimo, es. < 2°, puoi mantenere le stesse dimensioni di imgOrigine)
        Dim nuovaLarghezza As Integer = imgOrigine.Width
        Dim nuovaAltezza As Integer = imgOrigine.Height

        ' Crea una nuova bitmap vuota che conterrà l'immagine ruotata
        Dim imgRuotata As New Bitmap(nuovaLarghezza, nuovaAltezza, imgOrigine.PixelFormat)

        ' Imposta la stessa risoluzione DPI (fondamentale per le scansioni A4)
        imgRuotata.SetResolution(imgOrigine.HorizontalResolution, imgOrigine.VerticalResolution)

        ' Inizializza l'oggetto Graphics per disegnare sulla nuova bitmap
        Using g As Graphics = Graphics.FromImage(imgRuotata)
            ' Massima qualità di rendering (ideale per non perdere dettagli sui testi/QR code)
            g.InterpolationMode = InterpolationMode.HighQualityBicubic
            g.SmoothingMode = SmoothingMode.HighQuality
            g.PixelOffsetMode = PixelOffsetMode.HighQuality

            ' Sposta l'origine degli assi (0,0) esattamente al centro dell'immagine
            g.TranslateTransform(nuovaLarghezza / 2, nuovaAltezza / 2)

            ' Applica la rotazione dell'angolo desiderato (valori positivi = orario, negativi = antiorario)
            g.RotateTransform(angoloGradi)

            ' Riposiziona l'asse per disegnare l'immagine centrata rispetto al nuovo centro
            g.TranslateTransform(-imgOrigine.Width / 2, -imgOrigine.Height / 2)

            ' Disegna l'immagine originale applicando la trasformazione geometrica
            g.DrawImage(imgOrigine, New Point(0, 0))
        End Using

        Return imgRuotata
    End Function
End Module
