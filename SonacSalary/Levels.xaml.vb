Imports System.Data

Public Class Levels
    Public TableName As String = "NLevels"
    Public SubId As String = "Id"
    Public SubName As String = "Name"



    Dim dt As New DataTable
    Dim bm As New BasicMethods

    Public Flag As Integer = 0
    Public WithImage As Boolean = False

    Private Sub BasicForm_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        If bm.TestIsLoaded(Me) Then Return

        bm.Fields = New String() {SubId, SubName}
        bm.control = New Control() {txtID, txtName}
        bm.KeyFields = New String() {SubId}
        bm.Table_Name = TableName
        LoadTree()
        btnNew_Click(sender, e)
    End Sub




    Sub LoadTree()
        Dim Main As New MainPage
        bm.TestIsLoaded(Main)
        Main.Lvl = True
        Main.Load()
        TreeView1.Items.Clear()
        TreeView1.Items.Add(New TreeViewItem With {.Header = New CheckBox With {.Content = ("Contents")}})
        AddHandler CType(TreeView1.Items(0).Header, CheckBox).Checked, AddressOf CheckedChanged
        AddHandler CType(TreeView1.Items(0).Header, CheckBox).Unchecked, AddressOf CheckedChanged
        For i As Integer = 0 To Main.tab.Items.Count - 1
            Try
                Dim item As TabItem
                item = Main.tab.Items(i)
                If Not item.Tag Is Nothing And Not item.Tag = "" Then Continue For
                Dim nn As New TreeViewItem
                nn.Name = item.Name
                nn.Header = New CheckBox With {.Content = item.Header}
                TreeView1.Items(0).Items.Add(nn)
                loadNode(item, nn)
                AddHandler CType(nn.Header, CheckBox).Checked, AddressOf CheckedChanged
                AddHandler CType(nn.Header, CheckBox).Unchecked, AddressOf CheckedChanged
            Catch
            End Try
        Next
    End Sub

    Sub loadNode(ByVal item As TabItem, ByVal nn As TreeViewItem)
        For i As Integer = 0 To CType(CType(item.Content, ScrollViewer).Content, WrapPanel).Children.Count - 1
            Try
                Dim item2 As RadioButton
                item2 = CType(CType(item.Content, ScrollViewer).Content, WrapPanel).Children(i)
                'If Not item2.Tag Is Nothing Then Continue For
                Dim nn2 As New TreeViewItem
                nn2.Name = item2.Name
                nn2.Header = New CheckBox With {.Content = CType(item2.Content, TranslateTextAnimationExample).RealText.Text}
                nn.Items.Add(nn2)
                AddHandler CType(nn2.Header, CheckBox).Checked, AddressOf CheckedChanged
                AddHandler CType(nn2.Header, CheckBox).Unchecked, AddressOf CheckedChanged
            Catch
            End Try
        Next
    End Sub

    Sub loadNode(ByVal item As MenuItem, ByVal nn As TreeViewItem)
        For i As Integer = 0 To item.Items.Count - 1
            Try
                Dim item2 As MenuItem
                item2 = item.Items(i)
                If Not item2.Tag Is Nothing And Not item2.Tag = "" Then Continue For
                Dim nn2 As New TreeViewItem
                nn2.Name = item2.Name
                nn2.Header = New CheckBox With {.Content = item2.Header}
                nn.Items.Add(nn2)
                loadNode(item2, nn2)
                AddHandler CType(nn2.Header, CheckBox).Checked, AddressOf CheckedChanged
                AddHandler CType(nn2.Header, CheckBox).Unchecked, AddressOf CheckedChanged
            Catch
            End Try
            Try
                Dim item2 As Separator
                item2 = item.Items(i)
                If Not item2.Tag Is Nothing And Not item2.Tag = "" Then Continue For
                Dim nn2 As New TreeViewItem
                nn2.Name = item2.Name
                nn2.Header = (New CheckBox With {.Content = "ـــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــ"})
                nn.Items.Add(nn2)
            Catch
            End Try
        Next
    End Sub



    Sub FillControls()
        bm.FillControls()
        Dim dt As DataTable = bm.ExcuteAdapter("select * from NLevels where Id='" & txtID.Text & "'")
        Try
            CType(TreeView1.Items(0).Header, CheckBox).IsChecked = True
            For Each nn As TreeViewItem In TreeView1.Items(0).Items
                Try
                    If dt.Rows(0)(nn.Name) Is DBNull.Value Then
                        CType(nn.Header, CheckBox).IsChecked = False
                    Else
                        CType(nn.Header, CheckBox).IsChecked = dt.Rows(0)(nn.Name)
                    End If
                Catch ex As Exception
                    CType(nn.Header, CheckBox).IsChecked = False
                End Try
                FillSubNode(dt, nn)
            Next
        Catch
        End Try
    End Sub

    Sub FillSubNode(ByVal dt As DataTable, ByVal nn As TreeViewItem)
        Try
            For Each nn2 As TreeViewItem In nn.Items
                Try
                    If dt.Rows(0)(nn2.Name) Is DBNull.Value Then
                        CType(nn2.Header, CheckBox).IsChecked = False
                    Else
                        CType(nn2.Header, CheckBox).IsChecked = dt.Rows(0)(nn2.Name)
                    End If
                Catch ex As Exception
                    CType(nn2.Header, CheckBox).IsChecked = False
                End Try
                FillSubNode(dt, nn2)
            Next
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnLast_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLast.Click
        bm.FirstLast(New String() {SubId}, "Max", dt)
        If dt.Rows.Count = 0 Then Return
        FillControls()
    End Sub

    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        bm.NextPrevious(New String() {SubId}, New String() {txtID.Text}, "Next", dt)
        If dt.Rows.Count = 0 Then Return
        FillControls()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtName.Text.Trim = "" Then
            txtName.Focus()
            Return
        End If
        bm.DefineValues()
        If Not bm.Save(New String() {SubId}, New String() {txtID.Text.Trim}) Then Return
        SaveTree()
        btnNew_Click(sender, e)
        AllowClose = True
    End Sub

    Sub SaveTree()
        Dim ss As String = "Update NLevels set "
        Dim Chk As CheckBox
        For Each nn As TreeViewItem In TreeView1.Items(0).Items
            If nn.Name = "" Then Continue For
            Chk = nn.Header
            If Chk.IsChecked Then
                ss &= nn.Name & "=1,"
            Else
                ss &= nn.Name & "=0,"
            End If
            GetSubNode(nn, ss)
        Next
        ss = ss.Substring(0, ss.Length - 1) & " where Id='" & txtID.Text & "'"

        bm.ExecuteNonQuery(ss)
    End Sub

    Sub GetSubNode(ByVal nn As TreeViewItem, ByRef ss As String)
        Dim Chk As CheckBox
        For Each nn2 As TreeViewItem In nn.Items
            Chk = nn2.Header
            If Chk.IsChecked Then
                ss &= nn2.Name & "=1,"
            Else
                ss &= nn2.Name & "=0,"
            End If
            GetSubNode(nn2, ss)
        Next
    End Sub

    Private Sub btnFirst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFirst.Click

        bm.FirstLast(New String() {SubId}, "Min", dt)
        If dt.Rows.Count = 0 Then Return
        FillControls()
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        bm.ClearControls()
        ClearControls()
        txtID.Focus()
        txtID.SelectAll()
    End Sub

    Sub ClearControls()
        Try
            txtName.Clear()
            txtID.Text = bm.ExecuteScalar("select max(" & SubId & ")+1 from " & TableName)
            If txtID.Text = "" Then txtID.Text = "1"
            CType(CType(TreeView1.Items(0), TreeViewItem).Header, CheckBox).IsChecked = True
            CType(CType(TreeView1.Items(0), TreeViewItem).Header, CheckBox).IsChecked = False
            'TreeView1_AfterCheck(Nothing, Nothing)


        Catch ex As Exception
        End Try
    End Sub

    Private Sub ClearSubNode(ByVal nn As TreeViewItem)
        Try
            For Each nn2 As TreeViewItem In nn.Items
                CType(nn2.Header, CheckBox).IsChecked = False
                ClearSubNode(nn2)
            Next
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If bm.ShowDeleteMSG() Then
            bm.ExecuteNonQuery("delete from " & TableName & " where " & SubId & "='" & txtID.Text.Trim & "'")
            btnNew_Click(sender, e)
        End If
    End Sub

    Private Sub btnPrevios_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrevios.Click
        bm.NextPrevious(New String() {SubId}, New String() {txtID.Text}, "Back", dt)
        If dt.Rows.Count = 0 Then Return
        FillControls()
    End Sub
    Dim lv As Boolean = False

    Private Sub txtID_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtID.LostFocus
        If lv Then
            Return
        End If
        lv = True

        bm.DefineValues()
        Dim dt As New DataTable
        bm.RetrieveAll(New String() {SubId}, New String() {txtID.Text.Trim}, dt)
        If dt.Rows.Count = 0 Then
            Dim s As String = txtID.Text
            ClearControls()
            txtID.Text = s

            txtName.Focus()
            lv = False
            Return
        End If
        FillControls()
        lv = False
        txtName.SelectAll()
        txtName.Focus()
        txtName.SelectAll()
        txtName.Focus()
        'txtName.Text = dt(0)("Name")
    End Sub

    Private Sub txtID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Input.KeyEventArgs) Handles txtID.KeyDown
        bm.MyKeyPress(sender, e)
    End Sub


    Dim AllowClose As Boolean = False
    'Private Sub MyBase_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    '    If Not btnSave.Enabled Then Exit Sub
    '    Select Case bm.RequestDelete
    '        Case BasicMethods.CloseState.Yes
    '            AllowClose = False
    '            btnSave_Click(Nothing, Nothing)
    '            If Not AllowClose Then e.Cancel = True
    '        Case BasicMethods.CloseState.No

    '        Case BasicMethods.CloseState.Cancel
    '            e.Cancel = True
    '    End Select
    'End Sub


    'Private Sub TreeView1_AfterCheck(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.ContextMenuClosing
    '    Try
    '        For Each nn As TreeViewItem In e.Node.Nodes
    '            nn.IsSelected = e.Node.Checked
    '        Next
    '    Catch
    '    End Try
    'End Sub

    Dim lop As Boolean = False

    Private Sub CheckedChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim ch As CheckBox = sender
        Dim p As TreeViewItem = ch.Parent

        If Not lop Then
            For Each n As TreeViewItem In p.Items
                CType(n.Header, CheckBox).IsChecked = ch.IsChecked
            Next
        End If

        If p.Parent.GetType.ToString = "System.Windows.Controls.TreeViewItem" Then
            lop = True
            Dim PP As TreeViewItem = p.Parent
            If ch.IsChecked Then CType(PP.Header, CheckBox).IsChecked = True
            lop = False
        End If
    End Sub

End Class
