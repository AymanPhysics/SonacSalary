Imports System.Data
Imports System.Windows
Imports System.Windows.Media
Imports System.Management

Public Class PrintBarcode


    Dim dv As New DataView
    Dim HelpDt As New DataTable
    Dim dt As New DataTable
    Dim bm As New BasicMethods

    Public ItemsTable As String = ""

    Dim StaticsDt As New DataTable
    WithEvents G As New MyGrid
    WithEvents MyTimer As New Threading.DispatcherTimer
    Public Flag As Integer
    Public FirstColumn As String = "الكـــــود", SecondColumn As String = "الاســــــــــــم", Statement As String = ""


    Private Sub Sales_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        If bm.TestIsLoaded(Me) Then Return


        LoadAllItems()

        LoadWFH()
    End Sub


    Structure GC
        Shared Id As String = "Id"
        Shared Name As String = "Name"
    End Structure


    Private Sub LoadWFH()
        'WFH.Background = New SolidColorBrush(Colors.LightSalmon)
        'WFH.Foreground = New SolidColorBrush(Colors.Red)
        WFH.Child = G

        G.Columns.Clear()
        G.ForeColor = System.Drawing.Color.DarkBlue
        G.Columns.Add(GC.Id, "كود الموظف")
        G.Columns.Add(GC.Name, "اسم الموظف")


        G.Columns(GC.Id).FillWeight = 110
        G.Columns(GC.Name).FillWeight = 300

        G.Columns(GC.Id).ReadOnly = True
        G.Columns(GC.Name).ReadOnly = True

        G.AllowUserToDeleteRows = True
        G.AllowUserToAddRows = False
        G.SelectionMode = Forms.DataGridViewSelectionMode.FullRowSelect

    End Sub

    Sub LoadAllItems()
        Try
            HelpDt = bm.ExcuteAdapter("Select Id,ArName Name From Employees where manager=0")
            HelpDt.TableName = "tbl"
            HelpDt.Columns(0).ColumnName = FirstColumn
            HelpDt.Columns(1).ColumnName = SecondColumn

            dv.Table = HelpDt
            HelpGD.ItemsSource = dv
            HelpGD.Columns(0).Width = 75
            HelpGD.Columns(1).Width = 298

            HelpGD.SelectedIndex = 0
        Catch
        End Try

    End Sub

    Private Sub txtId_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtID.GotFocus
        Try
            dv.Sort = FirstColumn
        Catch
        End Try
    End Sub

    Private Sub txtName_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtName.GotFocus
        Try
            dv.Sort = SecondColumn
        Catch
        End Try
    End Sub

    Private Sub txtId_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtID.TextChanged, txtName.TextChanged
        Try
            dv.RowFilter = " [" & FirstColumn & "] >" & IIf(txtID.Text.Trim = "", 0, txtID.Text) & " and [" & SecondColumn & "] like '" & txtName.Text & "%'"
        Catch
        End Try
    End Sub


    Private Sub HelpGD_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Input.KeyEventArgs) Handles txtID.PreviewKeyDown, txtName.PreviewKeyDown
        Try
            If e.Key = Input.Key.Up Then
                HelpGD.SelectedIndex = HelpGD.SelectedIndex - 1
            ElseIf e.Key = Input.Key.Down Then
                HelpGD.SelectedIndex = HelpGD.SelectedIndex + 1
            End If
        Catch ex As Exception
        End Try
    End Sub


    Private Sub HelpGD_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles HelpGD.MouseDoubleClick
        Try
            AddItem(HelpGD.Items(HelpGD.SelectedIndex)(0), HelpGD.Items(HelpGD.SelectedIndex)(1))
        Catch ex As Exception
        End Try
    End Sub

    Sub AddItem(Id As String, Name As String)
        Try
            If Not TabControl1.SelectedIndex = 0 Then TabControl1.SelectedIndex = 0
            Dim Exists As Boolean = False
            Dim Move As Boolean = False
            G.AutoSizeColumnsMode = Forms.DataGridViewAutoSizeColumnsMode.Fill

            For x As Integer = 0 To G.Rows.Count - 1
                If G.Rows(x).Cells(GC.Id).Value = Id Then
                    G.CurrentCell = G.Rows(x).Cells(GC.Id)
                    Exit Sub
                End If
            Next

            Dim i As Integer = G.Rows.Add()
            G.Rows(i).Cells(GC.Id).Value = Id
            G.Rows(i).Cells(GC.Name).Value = Name
        Catch
        End Try
    End Sub

    Dim lop As Boolean = False


    Sub ClearRow(ByVal i As Integer)
        G.Rows(i).Cells(GC.Id).Value = Nothing
        G.Rows(i).Cells(GC.Name).Value = Nothing
    End Sub

    Sub ClearControls()
        Try
            G.Rows.Clear()
        Catch
        End Try
    End Sub
     


    Private Sub ItemId_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Input.KeyEventArgs) Handles ItemId.KeyUp
        bm.ShowHelp("الأصناف", ItemId, ItemName, e, "select Id,Name from Items")
    End Sub

    Private Sub ItemId_LostFocus(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles ItemId.LostFocus
        bm.LostFocus(ItemId, ItemName, "select Name from Items where Id=" & ItemId.Text.Trim())
    End Sub


    Private Sub btnNew_Click(sender As Object, e As RoutedEventArgs) Handles btnNew.Click
        G.Rows.Clear()
        ItemId.Clear()
        ItemName.Clear()
        Count.Clear()
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As RoutedEventArgs) Handles btnPrint.Click
        If Val(ItemId.Text) < 1 Then
            bm.ShowMSG("برجاء تحديد الصنف")
            ItemId.Focus()
            Return
        End If
        If Val(Count.Text) < 1 Then
            bm.ShowMSG("برجاء تحديد العدد")
            Count.Focus()
            Return
        End If



        
        'For i As Integer = 0 To G.Rows.Count - 1
        '    Dim rpt0 As New ReportViewer
        '    rpt0.Rpt = "PrintBarcode0.rpt"
        '    rpt0.paraname = New String() {"EmpName", "ItemName"}
        '    rpt0.paravalue = New String() {G.Rows(i).Cells(GC.Name).Value, ItemName.Text}
        '    rpt0.Print()

        '    For i2 As Integer = 0 To Val(Count.Text) - 1
        '        Dim rpt As New ReportViewer
        '        rpt.Rpt = "PrintBarcode.rpt"
        '        rpt.paraname = New String() {"@ItemId", "@EmpId", "@Count", "Header"}
        '        rpt.paravalue = New String() {Val(ItemId.Text), Val(G.Rows(i).Cells(GC.Id).Value), 1, CType(Parent, Page).Title}
        '        rpt.Print()
        '    Next
        'Next



        For i As Integer = 0 To G.Rows.Count - 1
            Dim rpt0 As New ReportViewer
            rpt0.Rpt = "PrintBarcode0.rpt"
            rpt0.paraname = New String() {"EmpName", "ItemName"}
            rpt0.paravalue = New String() {G.Rows(i).Cells(GC.Name).Value, ItemName.Text}
            rpt0.Print()
            'rpt0.ShowDialog()

            Dim rpt As New ReportViewer
            rpt.Rpt = "PrintBarcode.rpt"
            rpt.paraname = New String() {"@ItemId", "@EmpId", "@Count", "Header"}
            rpt.paravalue = New String() {Val(ItemId.Text), Val(G.Rows(i).Cells(GC.Id).Value), Val(Count.Text), CType(Parent, Page).Title}
            rpt.Print()
            'rpt.ShowDialog()
        Next

    End Sub
End Class
