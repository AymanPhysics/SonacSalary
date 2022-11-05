Imports System.Data

Public Class RPT1
    Dim bm As New BasicMethods

    Dim m As MainWindow = Application.Current.MainWindow
    Public Flag As Integer = 0
    Public GroupsTable As String = "Groups"
    Public ItemsTable As String = "Items"


    Public Sub BasicForm_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        If bm.TestIsLoaded(Me) Then Return
        Select Case Flag
            Case 1
                Button2.Content = "احتساب"
                lblItemId.Visibility = Windows.Visibility.Hidden
                ItemId.Visibility = Windows.Visibility.Hidden
                ItemName.Visibility = Windows.Visibility.Hidden
            Case 2
        End Select

        Dim MyNow As DateTime = bm.MyGetDate()
        FromDate.SelectedDate = New DateTime(MyNow.Year, MyNow.Month, MyNow.Day, 0, 0, 0)
        ToDate.SelectedDate = New DateTime(MyNow.Year, MyNow.Month, MyNow.Day, 0, 0, 0)

    End Sub

    Private Sub Button2_Click(sender As Object, e As RoutedEventArgs) Handles Button2.Click

        Dim rpt As New ReportViewer
        Select Case Flag
            Case 1
                bm.ExcuteNonQuery("CalcDailyBarcodeHistory", {"StationId", "FromDate", "ToDate"}, {Val(StationId.Text), bm.ToStrDate(FromDate.SelectedDate), bm.ToStrDate(ToDate.SelectedDate)})
                bm.ShowMSG("تمت العملية بنجاح")
                Return
            Case 2
                rpt.Rpt = "DailyBarcodeHistory1.rpt"
            Case 3
                rpt.Rpt = "DailyBarcodeHistory2.rpt"
            Case 4
                rpt.Rpt = "DailyBarcodeHistory11.rpt"
            Case 5
                rpt.Rpt = "DailyBarcodeHistory21.rpt"
            Case 6
                rpt.Rpt = "DailyBarcodeHistory0.rpt"
            Case 7
                rpt.Rpt = "DailyBarcodeHistoryStatics1.rpt"
            Case 8
                rpt.Rpt = "DailyBarcodeHistoryStatics2.rpt"
            Case 9
                rpt.Rpt = "DailyBarcodeHistoryStatics3.rpt"
        End Select
        rpt.paraname = New String() {"@StationId", "StationName", "@ItemId", "ItemName", "@FromDate", "@ToDate", "Header"}
        rpt.paravalue = New String() {Val(StationId.Text), StationName.Text, Val(ItemId.Text), ItemName.Text, FromDate.SelectedDate, ToDate.SelectedDate, CType(Parent, Page).Title}
        rpt.ShowDialog()
    End Sub

    Private Sub StationId_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Input.KeyEventArgs) Handles StationId.KeyUp
        bm.ShowHelp("المحطات", StationId, StationName, e, "select Id,Name from Stations")
    End Sub

    Private Sub StationId_LostFocus(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles StationId.LostFocus
        bm.LostFocus(StationId, StationName, "select Name from Stations where Id=" & StationId.Text.Trim())
    End Sub

    Private Sub ItemId_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Input.KeyEventArgs) Handles ItemId.KeyUp
        bm.ShowHelp("الكراتين", ItemId, ItemName, e, "select Id,Name from Items")
    End Sub

    Private Sub ItemId_LostFocus(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles ItemId.LostFocus
        bm.LostFocus(ItemId, ItemName, "select Name from Items where Id=" & ItemId.Text.Trim())
    End Sub

End Class
