Imports System.Data

Public Class HelpCustomers
    Dim bm As New BasicMethods
    Public FirstColumn As String = "الكود", SecondColumn As String = "الاسم عربى", ThirdColumn As String = "الاسم إنجليزى", FourthColumn As String = "الرقم القومى", FifthColumn As String = "تليفون", SixthColumn As String = "موبيل", SeventhColumn As String = "العنوان"

    Dim dt As New DataTable
    Dim dv As New DataView
    Public Header As String = ""
    Private Sub Help_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Loaded
        If bm.TestIsLoaded(Me) Then Return

        Banner1.StopTimer = True
        Banner1.Header = Header
        Try
            dt = bm.ExcuteAdapter("CasesSearch")
            dt.TableName = "tbl"
            dt.Columns(0).ColumnName = FirstColumn
            dt.Columns(1).ColumnName = SecondColumn
            dt.Columns(2).ColumnName = ThirdColumn
            dt.Columns(3).ColumnName = FourthColumn
            dt.Columns(4).ColumnName = FifthColumn
            dt.Columns(5).ColumnName = SixthColumn
            dt.Columns(6).ColumnName = SeventhColumn

            dv.Table = dt
            DataGridView1.ItemsSource = dv
            DataGridView1.Columns(0).Width = 85
            DataGridView1.Columns(1).Width = 165
            DataGridView1.Columns(2).Width = 165
            DataGridView1.Columns(3).Width = 90
            DataGridView1.Columns(4).Width = 80
            DataGridView1.Columns(5).Width = 85
            DataGridView1.Columns(6).Width = 120

            DataGridView1.SelectedIndex = 0
        Catch
        End Try
        txtArName.Focus()
    End Sub

    Private Sub txtId_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtID.GotFocus
        Try
            dv.Sort = FirstColumn
        Catch
        End Try
    End Sub

    Private Sub txtArName_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtArName.GotFocus
        Try
            dv.Sort = SecondColumn
        Catch
        End Try
    End Sub

    Private Sub txtEnName_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEnName.GotFocus
        Try
            dv.Sort = ThirdColumn
        Catch
        End Try
    End Sub

    Private Sub NationalId_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NationalId.GotFocus
        Try
            dv.Sort = FourthColumn
        Catch
        End Try
    End Sub

    Private Sub txtTel_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTel.GotFocus
        Try
            dv.Sort = FifthColumn
        Catch
        End Try
    End Sub

    Private Sub txtMob_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMob.GotFocus
        Try
            dv.Sort = SixthColumn
        Catch
        End Try
    End Sub

    Private Sub txtAddress_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAddress.GotFocus
        Try
            dv.Sort = SeventhColumn
        Catch
        End Try
    End Sub


    Private Sub txtId_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtID.TextChanged, txtArName.TextChanged, txtEnName.TextChanged, NationalId.TextChanged, txtTel.TextChanged, txtMob.TextChanged, txtAddress.TextChanged
        Try
            dv.RowFilter = " [" & FirstColumn & "] like '%" & txtID.Text & "%' and [" & SecondColumn & "] like '%" & txtArName.Text & "%' and [" & ThirdColumn & "] like '%" & txtEnName.Text & "%' and [" & FourthColumn & "] like '%" & NationalId.Text & "%' and ([" & FifthColumn & "] like '%" & txtTel.Text & "%' or [" & SixthColumn & "] like '%" & txtTel.Text & "%') and ([" & FifthColumn & "] like '%" & txtMob.Text & "%' or [" & SixthColumn & "] like '%" & txtMob.Text & "%') and [" & SeventhColumn & "] like '%" & txtAddress.Text & "%'"
        Catch ex As Exception
        End Try
    End Sub

    Public SelectedId As Integer = 0
    Public SelectedName As String = ""

    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Input.KeyEventArgs) Handles Me.PreviewKeyDown
        Try
            If e.Key = System.Windows.Input.Key.Enter Then
                SelectedId = DataGridView1.Items(DataGridView1.SelectedIndex)(0)
                SelectedName = DataGridView1.Items(DataGridView1.SelectedIndex)(1)
                Close()
            ElseIf e.Key = Input.Key.Escape Then
                Close()
            ElseIf e.Key = Input.Key.Up Then
                DataGridView1.SelectedIndex = DataGridView1.SelectedIndex - 1
            ElseIf e.Key = Input.Key.Down Then
                DataGridView1.SelectedIndex = DataGridView1.SelectedIndex + 1
            End If
        Catch ex As Exception
        End Try
    End Sub


    Private Sub DataGridView1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles DataGridView1.MouseDoubleClick
        Try
            SelectedId = DataGridView1.Items(DataGridView1.SelectedIndex)(0)
            SelectedName = DataGridView1.Items(DataGridView1.SelectedIndex)(1)
            Close()
        Catch ex As Exception
        End Try
    End Sub



    Private Sub btnSave_Click(sender As Object, e As RoutedEventArgs) Handles btnSave.Click
        If txtArName.Text.Trim = "" Then Return
        txtID.Clear()

        For i As Integer = 0 To dv.Table.Rows.Count - 1
            If txtArName.Text.Trim = dv.Table.Rows(i).Item(1).ToString Then Return
        Next

        txtArName.Text = txtArName.Text.Trim
        txtEnName.Text = txtEnName.Text.Trim
        NationalId.Text = NationalId.Text.Trim
        txtTel.Text = txtTel.Text.Trim
        txtMob.Text = txtMob.Text.Trim
        txtAddress.Text = txtAddress.Text.Trim

        txtEnName.Text = bm.GetEnName(txtArName.Text.Trim)
        If Not bm.AddItemToTable("cases", {"ArName", "EnName", "NationalId", "HomePhone", "Mobile", "Address"}, {txtArName.Text, txtEnName.Text, NationalId.Text, txtTel.Text, txtMob.Text, txtAddress.Text}) Then Return
        Help_Load(Nothing, Nothing)
        DataGridView1.SelectedIndex = dv.Table.Rows.Count - 1

        txtId_TextChanged(Nothing, Nothing)
    End Sub

    Dim IsLoaded As Boolean = False

End Class