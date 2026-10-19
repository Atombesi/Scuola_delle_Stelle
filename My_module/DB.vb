
Imports System.IO
Imports Microsoft.Data.Sqlite
Public Class DB
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
End Class

