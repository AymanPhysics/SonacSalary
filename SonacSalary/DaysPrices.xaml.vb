Imports System.Data

Public Class DaysPrices
    Public TableName As String = "DaysPrices"
    Public SubId As String = "DayDate"
    Public SubName As String = "DayPrice"



    Dim dt As New DataTable
    Dim bm As New BasicMethods

    Dim m As MainWindow = Application.Current.MainWindow
    Public Flag As Integer = 0
    Public WithImage As Boolean = False

    Public Sub BasicForm_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        If bm.TestIsLoaded(Me) Then Return

        If WithImage Then
            btnSetImage.Visibility = Visibility.Visible
            btnSetNoImage.Visibility = Visibility.Visible
            Image1.Visibility = Visibility.Visible
        End If
        bm.Fields = New String() {SubId, SubName}
        bm.control = New Control() {DayDate, DayPrice}
        bm.KeyFields = New String() {SubId} 
        bm.Table_Name = TableName
        btnNew_Click(sender, e)
    End Sub

    Sub FillControls()
        bm.FillControls()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CType(Application.Current.MainWindow, MainWindow).TabControl1.Items.Remove(Me.Parent)
    End Sub

    Private Sub btnLast_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLast.Click
        bm.FirstLast(New String() {SubId}, "Max", dt)
        If dt.Rows.Count = 0 Then Return
        FillControls()
    End Sub

    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        bm.NextPrevious(New String() {SubId}, New String() {bm.ToStrDate(DayDate.SelectedDate)}, "Next", dt)
        If dt.Rows.Count = 0 Then Return
        FillControls()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If DayPrice.Text.Trim = "" Then
            DayPrice.Focus()
            Return
        End If
        bm.DefineValues()
        If Not bm.Save(New String() {SubId}, New String() {bm.ToStrDate(DayDate.SelectedDate)}) Then Return
        btnNew_Click(sender, e) 
    End Sub

    Private Sub btnFirst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFirst.Click

        bm.FirstLast(New String() {SubId}, "Min", dt)
        If dt.Rows.Count = 0 Then Return
        FillControls()
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        bm.ClearControls()
        ClearControls()
    End Sub

    Sub ClearControls()
        bm.SetNoImage(Image1)
        DayPrice.Clear()
        DayDate.SelectedDate = bm.MyGetDate
        DayDate.Focus()
        DayPrice.Focus()
        DayDate.Focus()

    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If bm.ShowDeleteMSG() Then
            bm.ExecuteNonQuery("delete from " & TableName & " where " & SubId & "='" & bm.ToStrDate(DayDate.SelectedDate) & "'")
            btnNew_Click(sender, e)
        End If
    End Sub

    Private Sub btnPrevios_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrevios.Click
        bm.NextPrevious(New String() {SubId}, New String() {bm.ToStrDate(DayDate.SelectedDate)}, "Back", dt)
        If dt.Rows.Count = 0 Then Return
        FillControls()
    End Sub
    Dim lv As Boolean = False

    Private Sub txtID_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Input.KeyEventArgs) Handles DayDate.KeyUp
        'bm.ShowHelp(CType(Parent, Page).Title, DayDate, DayPrice, e, "select Id,Name from " & TableName, TableName)
    End Sub

    Private Sub txtID_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DayDate.LostFocus
        If lv OrElse DayDate.SelectedDate Is Nothing Then
            Return
        End If
        lv = True

        bm.DefineValues()
        Dim dt As New DataTable
        bm.RetrieveAll(New String() {SubId}, New String() {bm.ToStrDate(DayDate.SelectedDate)}, dt)
        If dt.Rows.Count = 0 Then
            Dim s = DayDate.SelectedDate
            ClearControls()
            DayDate.SelectedDate = s

            DayPrice.Focus()
            lv = False
            Return
        End If
        FillControls()
        lv = False
        DayPrice.Focus()
    End Sub

    Private Sub txtID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Input.KeyEventArgs) Handles DayDate.KeyDown
        bm.MyKeyPress(sender, e)
    End Sub

     


End Class
