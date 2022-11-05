Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Common
Imports System.Drawing

Module Md
    Public LastVersion As Integer = 12
    Public MyProjectType As ProjectType = ProjectType.Full

    Public cmd As New SqlCommand
    Public con As New SqlConnection
    Public s As New SqlClient.SqlConnectionStringBuilder
    Public FourceExit As Boolean = False
    Public HasLeft As Boolean = False
    Public UdlName As String = "Connect"

    Public UserName, ArName, LevelId, Password, CompanyName, CompanyTel, Manager As String
    Public EnName As String = "Please, Login", Currentpage As String = ""

    Public StoreId As String = ""
    Public Cashier As String = "0"

    Enum ProjectType
        Full
        PCs
    End Enum

End Module
