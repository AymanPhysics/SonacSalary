Imports System.Data
Imports System.IO
Imports System.Windows.Forms

Public Class CalcSalary
    Dim bm As New BasicMethods
    Dim dt As New DataTable
    Public Hdr As String = ""
    Public Flag As Integer = 0
    Public Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles Button2.Click
        If Val(txtMonth.Text) = 0 OrElse Val(txtYear.Text) = 0 Then Return

        Dim rpt As New ReportViewer
        Select Case Flag
            Case 1
                If bm.ExcuteNonQuery("OpenNewYear", New String() {"NewYear"}, New String() {Val(Md.UdlName.Substring(Len(Md.UdlName) - 4, 4)) + 1}) Then
                    CopyUdl()
                    bm.ShowMSG("تم فتح سنة مالية جديدة")
                End If
                Return
            Case 2
                CopyUdl()
                bm.ShowMSG("تم بدء مالية جديدة")
                Return
        End Select

        rpt.paraname = New String() {"AccNo", "@Month", "@Period", "@Year", "Header"}
        rpt.paravalue = New String() {TaxAccNo.Text.Trim, Val(txtMonth.Text), Val(txtMonth.Text), Val(txtYear.Text), CType(Parent, Page).Title}
        rpt.Show()

    End Sub

    Private Sub UserControl_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        If bm.TestIsLoaded(Me) Then Return
        LoadResource()
        Dim MyNow As DateTime = bm.MyGetDate()
        txtMonth.Text = MyNow.Month
        txtYear.Text = MyNow.Year
        If Flag = 1 OrElse Flag = 2 Then
            GG.Children.Clear()
            Button2.Content = "ابدأ"
        End If

        
    End Sub
    Private Sub LoadResource()

        lblTaxAcc.SetResourceReference(System.Windows.Controls.Label.ContentProperty, "TaxAcc")
        lblFromDate.SetResourceReference(System.Windows.Controls.Label.ContentProperty, "Month")
        lblFromDate_Copy.SetResourceReference(System.Windows.Controls.Label.ContentProperty, "Year")

        If Flag <> 5 Then
            lblTaxAcc.Visibility = Windows.Visibility.Hidden
            TaxAccNo.Visibility = Windows.Visibility.Hidden
            TaxAccName.Visibility = Windows.Visibility.Hidden
        End If
    End Sub

    Private Sub txtID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Input.KeyEventArgs) Handles txtMonth.KeyDown, txtYear.KeyDown, TaxAccNo.KeyDown
        bm.MyKeyPress(sender, e)
    End Sub


    Private Sub TaxAccNo_LostFocus(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles TaxAccNo.LostFocus
        bm.AccNoLostFocus(TaxAccNo, TaxAccName, , )
    End Sub

    Private Sub TaxAccNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Input.KeyEventArgs) Handles TaxAccNo.KeyUp
        bm.AccNoShowHelp(TaxAccNo, TaxAccName, e, , )
    End Sub

    Private Sub CopyUdl()
        Dim NewConnect As String = System.Windows.Forms.Application.StartupPath & "\" & Md.UdlName.Substring(0, Len(Md.UdlName) - 4) & (Val(Md.UdlName.Substring(Len(Md.UdlName) - 4, 4)) + 1).ToString & ".udl"
        IO.File.Copy(System.Windows.Forms.Application.StartupPath & "\" & Md.UdlName & ".udl", NewConnect, True)

        Dim st As New StreamReader(NewConnect, True)
        Dim s As String = st.ReadToEnd.Replace(Md.con.Database, Md.con.Database.Substring(0, Len(Md.con.Database) - 4) & (Val(Md.con.Database.Substring(Len(Md.con.Database) - 4, 4)) + 1).ToString)
        st.Close()

        File.WriteAllText(NewConnect, s, Text.Encoding.Unicode)
    End Sub



End Class