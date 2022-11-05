Imports System.Data
Imports System.Windows.Media.Animation

Public Class DailyBarcode
    Public TableName As String = "DailyBarcode"

    Dim dt As New DataTable
    Dim bm As New BasicMethods

    Dim m As MainWindow = Application.Current.MainWindow
    Public Flag As Integer = 0
    Public WithImage As Boolean = False

    Public Sub BasicForm_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        If bm.TestIsLoaded(Me) Then Return
        lblSaved.Opacity = 0
        lblNotification.Opacity = 0
        StationId.Text = GetSetting("OMEGA", "DailyBarcode", "StationId")
        StationId_LostFocus(Nothing, Nothing)
        Barcode.Focus()
    End Sub

    Private Sub Barcode_PreviewKeyDown(sender As Object, e As KeyEventArgs) Handles Barcode.PreviewKeyDown
        If e.Key = Key.Enter AndAlso Barcode.Text.Length = 13 Then
            Dim s As String = bm.ExecuteScalar("SaveDailyBarcode", {"StationId", "Barcode", "UserName"}, {Val(StationId.Text), Barcode.Text, Md.UserName})
            If Val(s) = 1 Then
                Animate(lblNotification)
            Else
                Animate(lblSaved)
            End If
            e.Handled = True
            Barcode.Clear()
            Barcode.Focus()
        End If
    End Sub



    Private Sub Animate(txt As Label)
        Dim MyAnimation As DoubleAnimation
        MyAnimation = New DoubleAnimation()
        MyAnimation.From = 0
        MyAnimation.To = 1
        MyAnimation.Duration = New Duration(TimeSpan.FromSeconds(0.5))
        MyAnimation.AutoReverse = True
        txt.BeginAnimation(TextBox.OpacityProperty, MyAnimation)
    End Sub

    Private Sub StationId_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Input.KeyEventArgs) Handles StationId.KeyUp
        bm.ShowHelp("المحطات", StationId, StationName, e, "select Id,Name from Stations")
    End Sub

    Private Sub StationId_LostFocus(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles StationId.LostFocus
        bm.LostFocus(StationId, StationName, "select Name from Stations where Id=" & StationId.Text.Trim())
        SaveSetting("OMEGA", "DailyBarcode", "StationId", StationId.Text)
    End Sub


End Class
