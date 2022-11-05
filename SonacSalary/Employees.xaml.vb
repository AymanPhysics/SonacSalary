Imports System.Data
Public Class Employees
    Public TableName As String = "Employees"
    Public SubId As String = "Id"



    Dim dt As New DataTable
    Dim bm As New BasicMethods

    Public Flag As Integer = 0

    Private Sub BasicForm_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        If bm.TestIsLoaded(Me) Then Return

        TabControl1.Visibility = Windows.Visibility.Hidden
        Doctor.Visibility = Windows.Visibility.Hidden
        Nurse.Visibility = Windows.Visibility.Hidden
        Receptionist.Visibility = Windows.Visibility.Hidden
        lblTax.Visibility = Windows.Visibility.Hidden
        Tax.Visibility = Windows.Visibility.Hidden

        lblWorkDays.Visibility = Windows.Visibility.Hidden
        Saturday.Visibility = Windows.Visibility.Hidden
        Sunday.Visibility = Windows.Visibility.Hidden
        Monday.Visibility = Windows.Visibility.Hidden
        Tuesday.Visibility = Windows.Visibility.Hidden
        Wednesday.Visibility = Windows.Visibility.Hidden
        Thursday.Visibility = Windows.Visibility.Hidden
        Friday.Visibility = Windows.Visibility.Hidden

        btnSetImage.Visibility = Windows.Visibility.Hidden
        btnSetNoImage.Visibility = Windows.Visibility.Hidden
        Image1.Visibility = Windows.Visibility.Hidden

        bm.Fields = New String() {SubId, "ArName", "Address", "DateOfBirth", "DepartmentId", "Notes", "Nurse", "Receptionist", "Manager", "SystemUser", "NationalId", "HomePhone", "Mobile", "Email", "Password", "EnName", "LevelId", "Doctor", "Stopped", "HiringDate", "Duration", "Cnt", "hh", "mm", "Saturday", "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "IsSalary", "IsFreelancer", "LateAllowance", "Allowance", "SpAllowance", "FromHH", "ToHH", "FromMM", "ToMM", "Salary", "ConsultationPrice", "DetectionPrice", "SalaryOnly", "ShiftsOnly", "SalaryOrShifts", "ShiftCount", "ShiftValue", "Tax", "Bonus", "Insurance", "Annual", "NoofDaysOff", "NoofMonthlyExecuses", "HolidayWorkValue", "DelayValue", "OvertimeValue", "IsFixedHours", "TotalHours", "TotalMinutes", "HasAttendance", "MainJobId", "SubJobId", "TaxAccNo"}
        bm.control = New Control() {txtID, ArName, Address, DateOfBirth, DepartmentId, Notes, Nurse, Receptionist, Manager, SystemUser, NationalId, HomePhone, Mobile, Email, Password, EnName, LevelId, Doctor, Stopped, HiringDate, Duration, Cnt, hh, mm, Saturday, Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, IsSalary, IsFreelancer, LateAllowance, Allowance, SpAllowance, FromHH, ToHH, FromMM, ToMM, Salary, ConsultationPrice, DetectionPrice, SalaryOnly, ShiftsOnly, SalaryOrShifts, ShiftCount, ShiftValue, Tax, Bonus, Insurance, Annual, NoofDaysOff, NoofMonthlyExecuses, HolidayWorkValue, DelayValue, OvertimeValue, IsFixedHours, TotalHours, TotalMinutes, HasAttendance, MainJobId, SubJobId, TaxAccNo}
        bm.KeyFields = New String() {SubId}
        bm.Table_Name = TableName
        btnNew_Click(sender, e)
        Doctor_UnChecked(Nothing, Nothing)
        IsSalary_Checked(Nothing, Nothing)
        IsFixedHours_Unchecked(Nothing, Nothing)
    End Sub

    Private Sub btnLast_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLast.Click
        bm.FirstLast(New String() {SubId}, "Max", dt)
        If dt.Rows.Count = 0 Then Return
        FillControls()
    End Sub

    Sub FillControls()
        bm.FillControls()
        'bm.GetImage(TableName, New String() {SubId}, New String() {txtID.Text.Trim}, "Image", Image1)
        LevelId_LostFocus(Nothing, Nothing)
        DepartmentId_LostFocus(Nothing, Nothing)

        MainJobId_LostFocus(Nothing, Nothing)
        SubJobId_LostFocus(Nothing, Nothing)
        TaxAccNo_LostFocus(Nothing, Nothing)
    End Sub
    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        bm.NextPrevious(New String() {SubId}, New String() {txtID.Text}, "Next", dt)
        If dt.Rows.Count = 0 Then Return
        FillControls()
    End Sub

    Dim AllowSave As Boolean = False
    Dim DontClear As Boolean = False
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        AllowSave = False
        If ArName.Text.Trim = "" OrElse Not bm.TestNames(ArName, EnName) Then
            ArName.Focus()
            Return
        End If
        Salary.Text = Val(Salary.Text)
        ConsultationPrice.Text = Val(ConsultationPrice.Text)
        DetectionPrice.Text = Val(DetectionPrice.Text)
        ShiftCount.Text = Val(ShiftCount.Text)
        ShiftValue.Text = Val(ShiftValue.Text)
        Tax.Text = Val(Tax.Text)

        If Val(Tax.Text) <> 0 AndAlso Val(TaxAccNo.Text) = 0 Then
            bm.ShowMSG("برجاء تحديد حساب الضريبة")
            TaxAccNo.Focus()
            Return
        End If

        Bonus.Text = Val(Bonus.Text)
        Allowance.Text = Val(Allowance.Text)
        SpAllowance.Text = Val(SpAllowance.Text)
        LateAllowance.Text = Val(LateAllowance.Text)
        Insurance.Text = Val(Insurance.Text)
        Annual.Text = Val(Annual.Text)
        NoofDaysOff.Text = Val(NoofDaysOff.Text)
        NoofMonthlyExecuses.Text = Val(NoofMonthlyExecuses.Text)
        HolidayWorkValue.Text = Val(HolidayWorkValue.Text)

        TotalHours.Text = Val(TotalHours.Text)
        TotalMinutes.Text = Val(TotalMinutes.Text)
        FromHH.Text = Val(FromHH.Text)
        ToHH.Text = Val(ToHH.Text)
        FromMM.Text = Val(FromMM.Text)
        ToMM.Text = Val(ToMM.Text)
        DelayValue.Text = Val(DelayValue.Text)
        OvertimeValue.Text = Val(OvertimeValue.Text)

        bm.DefineValues()
        If Not bm.Save(New String() {SubId}, New String() {txtID.Text.Trim}) Then Return
        'bm.SaveImage(TableName, New String() {SubId}, New String() {txtID.Text.Trim}, "Image", Image1)
        If Not DontClear Then btnNew_Click(sender, e)
        AllowSave = True
        AllowClose = True
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

    Private Sub MainJobId_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Input.KeyEventArgs) Handles MainJobId.KeyUp
        bm.ShowHelp("MainJobs", MainJobId, MainJobName, e, "select Id,Name from MainJobs", "MainJobs")
    End Sub

    Private Sub SubJobId_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Input.KeyEventArgs) Handles SubJobId.KeyUp
        bm.ShowHelp("SubJobs", SubJobId, SubJobName, e, "select Id,Name from SubJobs where MainJobId=" & MainJobId.Text.Trim, "SubJobs", {"MainJobId"}, {Val(MainJobId.Text)})
    End Sub

    Private Sub MainJobId_LostFocus(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MainJobId.LostFocus
        bm.LostFocus(MainJobId, MainJobName, "select Name from MainJobs where Id=" & MainJobId.Text.Trim())
        SubJobId_LostFocus(Nothing, Nothing)
    End Sub

    Private Sub SubJobId_LostFocus(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles SubJobId.LostFocus
        bm.LostFocus(SubJobId, SubJobName, "select Name from SubJobs where MainJobId=" & MainJobId.Text.Trim & " and Id=" & SubJobId.Text.Trim())
    End Sub


    Sub ClearControls()
        bm.ClearControls()
        'bm.SetNoImage(Image1, True)
        IsSalary.IsChecked = True
        SalaryOnly.IsChecked = True

        Saturday.IsChecked = True
        Sunday.IsChecked = True
        Monday.IsChecked = True
        Tuesday.IsChecked = True
        Wednesday.IsChecked = True
        Thursday.IsChecked = True
        Friday.IsChecked = True

        LevelName.Clear()
        DepartmentName.Clear()
        MainJobName.Clear()
        SubJobName.Clear()
        TaxAccName.Clear()

        ArName.Clear()
        txtID.Text = bm.ExecuteScalar("select max(" & SubId & ")+1 from " & TableName)
        If txtID.Text = "" Then txtID.Text = "1"

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


    Private Sub txtID_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Input.KeyEventArgs) Handles txtID.KeyUp
        If bm.ShowHelp("Employees", txtID, ArName, e, "Select cast(Id as varchar(10))Id," & "ArName" & " Name from Employees") Then
            txtID_LostFocus(sender, Nothing)
        End If
    End Sub


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

            ArName.Focus()
            lv = False
            Return
        End If
        FillControls()
        lv = False
        ArName.SelectAll()
        ArName.Focus()
        ArName.SelectAll()
        ArName.Focus()
        'arName.Text = dt(0)("Name")
    End Sub

    Private Sub LevelId_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Input.KeyEventArgs) Handles LevelId.KeyUp
        bm.ShowHelp("Security Levels", LevelId, LevelName, e, "select Id,Name from NLevels")
    End Sub

    Private Sub DepartmentId_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Input.KeyEventArgs) Handles DepartmentId.KeyUp
        bm.ShowHelp("Departments", DepartmentId, DepartmentName, e, "select Id,Name from Departments", "Departments")
    End Sub


    Private Sub txtID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Input.KeyEventArgs) Handles txtID.KeyDown, LevelId.KeyDown, DepartmentId.KeyDown, Duration.KeyDown, Cnt.KeyDown, hh.KeyDown, mm.KeyDown, TotalHours.KeyDown, TotalMinutes.KeyDown
        bm.MyKeyPress(sender, e)
    End Sub

    Private Sub txtID_KeyPress2(ByVal sender As Object, ByVal e As System.Windows.Input.KeyEventArgs)
        bm.MyKeyPress(sender, e, True)
    End Sub


    Dim AllowClose As Boolean = False

    Private Sub LevelId_LostFocus(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles LevelId.LostFocus
        bm.LostFocus(LevelId, LevelName, "select Name from NLevels where Id=" & LevelId.Text.Trim())
    End Sub

    Private Sub DepartmentId_LostFocus(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles DepartmentId.LostFocus
        bm.LostFocus(DepartmentId, DepartmentName, "select Name from Depa   rtments where Id=" & DepartmentId.Text.Trim())
    End Sub

    Private Sub btnSetImage_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnSetImage.Click
        DontClear = True
        btnSave_Click(btnSave, Nothing)
        DontClear = False
        If Not AllowSave Then Return
        bm.SetImage(Image1)
    End Sub

    Private Sub btnSetNoImage_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnSetNoImage.Click
        bm.SetNoImage(Image1, True, True)
    End Sub

    Private Sub ArName_LostFocus(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles ArName.LostFocus
        ArName.Text = ArName.Text.Trim
        EnName.Text = bm.GetEnName(ArName.Text.Trim)
    End Sub



    Private Sub Doctor_Checked(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles Doctor.Checked
        lblDuration.Visibility = Visibility.Visible
        lblMinutes.Visibility = Visibility.Visible
        'lblWorkDays.Visibility = Visibility.Visible
        Duration.Visibility = Visibility.Visible

        lblCnt.Visibility = Visibility.Visible
        Cnt.Visibility = Visibility.Visible

        lblStartingAt.Visibility = Visibility.Visible
        lblmm.Visibility = Visibility.Visible
        lblhh.Visibility = Visibility.Visible
        hh.Visibility = Visibility.Visible
        mm.Visibility = Visibility.Visible

        'Saturday.Visibility = Visibility.Visible
        'Sunday.Visibility = Visibility.Visible
        'Monday.Visibility = Visibility.Visible
        'Tuesday.Visibility = Visibility.Visible
        'Wednesday.Visibility = Visibility.Visible
        'Thursday.Visibility = Visibility.Visible
        'Friday.Visibility = Visibility.Visible

        lblConsultationPrice.Visibility = Visibility.Visible
        ConsultationPrice.Visibility = Visibility.Visible
        lblLE2.Visibility = Visibility.Visible
        lblDetectionPrice.Visibility = Visibility.Visible
        DetectionPrice.Visibility = Visibility.Visible
        lblLE1.Visibility = Visibility.Visible

        'Saturday.IsChecked = True
        'Sunday.IsChecked = True
        'Monday.IsChecked = True
        'Tuesday.IsChecked = True
        'Wednesday.IsChecked = True
        'Thursday.IsChecked = True
        'Friday.IsChecked = True

    End Sub

    Private Sub Doctor_UnChecked(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles Doctor.Unchecked
        lblDuration.Visibility = Visibility.Hidden
        lblMinutes.Visibility = Visibility.Hidden
        'lblWorkDays.Visibility = Visibility.Hidden
        Duration.Visibility = Visibility.Hidden
        Duration.Clear()

        lblCnt.Visibility = Visibility.Hidden
        Cnt.Visibility = Visibility.Hidden
        Cnt.Clear()

        lblStartingAt.Visibility = Visibility.Hidden
        lblmm.Visibility = Visibility.Hidden
        lblhh.Visibility = Visibility.Hidden
        hh.Visibility = Visibility.Hidden
        mm.Visibility = Visibility.Hidden
        hh.Clear()
        mm.Clear()

        'Saturday.Visibility = Visibility.Hidden
        'Sunday.Visibility = Visibility.Hidden
        'Monday.Visibility = Visibility.Hidden
        'Tuesday.Visibility = Visibility.Hidden
        'Wednesday.Visibility = Visibility.Hidden
        'Thursday.Visibility = Visibility.Hidden
        'Friday.Visibility = Visibility.Hidden

        lblConsultationPrice.Visibility = Visibility.Hidden
        ConsultationPrice.Visibility = Visibility.Hidden
        lblLE2.Visibility = Visibility.Hidden
        lblDetectionPrice.Visibility = Visibility.Hidden
        DetectionPrice.Visibility = Visibility.Hidden
        lblLE1.Visibility = Visibility.Hidden
        ConsultationPrice.Clear()
        DetectionPrice.Clear()

        'Saturday.IsChecked = False
        'Sunday.IsChecked = False
        'Monday.IsChecked = False
        'Tuesday.IsChecked = False
        'Wednesday.IsChecked = False
        'Thursday.IsChecked = False
        'Friday.IsChecked = False

    End Sub


    Private Sub IsSalary_Checked(sender As Object, e As RoutedEventArgs) Handles IsSalary.Checked, IsSalary.Unchecked, IsFreelancer.Checked, IsFreelancer.Unchecked
        If sender Is IsSalary Then IsFreelancer.IsChecked = Not IsSalary.IsChecked Else IsSalary.IsChecked = Not IsFreelancer.IsChecked
        If IsSalary.IsChecked Then
            LblSalaryType.Content = ("Salary")
            lblLE3.Content = ("L.E.")
            LateAllowance.Visibility = Windows.Visibility.Visible
            FromHH.Visibility = Windows.Visibility.Visible
            ToHH.Visibility = Windows.Visibility.Visible
            FromMM.Visibility = Windows.Visibility.Visible
            ToMM.Visibility = Windows.Visibility.Visible
            lblLateAllowance.Visibility = Windows.Visibility.Visible
            lblMinutes2.Visibility = Windows.Visibility.Visible

            IsFixedHours.Visibility = Windows.Visibility.Visible
            lblFromHH.Visibility = Windows.Visibility.Visible
            lblToHH.Visibility = Windows.Visibility.Visible
            lblFromMM.Visibility = Windows.Visibility.Visible
            lblToMM.Visibility = Windows.Visibility.Visible
            lblFrom.Visibility = Windows.Visibility.Visible
            lblTo.Visibility = Windows.Visibility.Visible

        Else
            LblSalaryType.Content = ("Perc")
            lblLE3.Content = "%"
            LateAllowance.Visibility = Windows.Visibility.Hidden
            FromHH.Visibility = Windows.Visibility.Hidden
            ToHH.Visibility = Windows.Visibility.Hidden
            FromMM.Visibility = Windows.Visibility.Hidden
            ToMM.Visibility = Windows.Visibility.Hidden
            lblLateAllowance.Visibility = Windows.Visibility.Hidden
            lblMinutes2.Visibility = Windows.Visibility.Hidden

            IsFixedHours.Visibility = Windows.Visibility.Hidden
            IsFixedHours.IsChecked = False
            lblFromHH.Visibility = Windows.Visibility.Hidden
            lblToHH.Visibility = Windows.Visibility.Hidden
            lblFromMM.Visibility = Windows.Visibility.Hidden
            lblToMM.Visibility = Windows.Visibility.Hidden
            lblFrom.Visibility = Windows.Visibility.Hidden
            lblTo.Visibility = Windows.Visibility.Hidden

        End If

        Salary.Clear()
        LateAllowance.Clear()
        FromHH.Clear()
        ToHH.Clear()
        FromMM.Clear()
        ToMM.Clear()

    End Sub



    Dim lop As Boolean = False
    Private Sub SalaryOnly_Checked(sender As Object, e As RoutedEventArgs) Handles SalaryOnly.Checked, ShiftsOnly.Checked, SalaryOrShifts.Checked
        If lop Then Return
        lop = True
        If sender Is SalaryOnly Then
            ShiftsOnly.IsChecked = False
            SalaryOrShifts.IsChecked = False
        ElseIf sender Is ShiftsOnly Then
            SalaryOnly.IsChecked = False
            SalaryOrShifts.IsChecked = False
        ElseIf sender Is SalaryOrShifts Then
            ShiftsOnly.IsChecked = False
            SalaryOnly.IsChecked = False
        End If
        lop = False
    End Sub

    Private Sub SalaryOnly_Unchecked(sender As Object, e As RoutedEventArgs) Handles SalaryOnly.Unchecked, ShiftsOnly.Unchecked, SalaryOrShifts.Unchecked
        If lop Then Return
        lop = True
        If sender Is SalaryOnly Then
            ShiftsOnly.IsChecked = Not SalaryOnly.IsChecked
        ElseIf sender Is ShiftsOnly Then
            SalaryOrShifts.IsChecked = Not ShiftsOnly.IsChecked
        ElseIf sender Is SalaryOrShifts Then
            SalaryOnly.IsChecked = Not SalaryOrShifts.IsChecked
        End If
        lop = False
    End Sub

    Private Sub IsFixedHours_Checked(sender As Object, e As RoutedEventArgs) Handles IsFixedHours.Checked
        lblFromHH2.IsEnabled = True
        lblFromMM2.IsEnabled = True
        TotalHours.IsEnabled = True
        TotalMinutes.IsEnabled = True

        lblFrom.Visibility = Windows.Visibility.Hidden
        lblTo.Visibility = Windows.Visibility.Hidden
        lblFromHH.Visibility = Windows.Visibility.Hidden
        lblToHH.Visibility = Windows.Visibility.Hidden
        lblFromMM.Visibility = Windows.Visibility.Hidden
        lblToMM.Visibility = Windows.Visibility.Hidden
        FromHH.Visibility = Windows.Visibility.Hidden
        ToHH.Visibility = Windows.Visibility.Hidden
        FromMM.Visibility = Windows.Visibility.Hidden
        ToMM.Visibility = Windows.Visibility.Hidden

    End Sub

    Private Sub IsFixedHours_Unchecked(sender As Object, e As RoutedEventArgs) Handles IsFixedHours.Unchecked
        lblFromHH2.IsEnabled = False
        lblFromMM2.IsEnabled = False
        TotalHours.IsEnabled = False
        TotalMinutes.IsEnabled = False

        lblFrom.Visibility = Windows.Visibility.Visible
        lblTo.Visibility = Windows.Visibility.Visible
        lblFromHH.Visibility = Windows.Visibility.Visible
        lblToHH.Visibility = Windows.Visibility.Visible
        lblFromMM.Visibility = Windows.Visibility.Visible
        lblToMM.Visibility = Windows.Visibility.Visible
        FromHH.Visibility = Windows.Visibility.Visible
        ToHH.Visibility = Windows.Visibility.Visible
        FromMM.Visibility = Windows.Visibility.Visible
        ToMM.Visibility = Windows.Visibility.Visible

    End Sub

    Sub CalcTotalHoursMinutes() Handles FromHH.LostFocus, ToHH.LostFocus, FromMM.LostFocus, ToMM.LostFocus, IsFixedHours.Checked, IsFixedHours.Unchecked
        If TotalHours.IsEnabled Then Return
        Dim i As Integer = (Val(ToHH.Text) * 60 + Val(ToMM.Text)) - (Val(FromHH.Text) * 60 + Val(FromMM.Text))
        If i < 0 Then
            TotalHours.Text = 0
            TotalMinutes.Text = 0
            ToHH.Text = FromHH.Text
            ToMM.Text = FromMM.Text
            Return
        End If
        TotalMinutes.Text = i Mod 60
        TotalHours.Text = (i - Val(TotalMinutes.Text)) / 60
    End Sub

    Private Sub TaxAccNo_LostFocus(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles TaxAccNo.LostFocus
        bm.AccNoLostFocus(TaxAccNo, TaxAccName, , )
    End Sub

    Private Sub TaxAccNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Input.KeyEventArgs) Handles TaxAccNo.KeyUp
        bm.AccNoShowHelp(TaxAccNo, TaxAccName, e, , )
    End Sub


End Class
