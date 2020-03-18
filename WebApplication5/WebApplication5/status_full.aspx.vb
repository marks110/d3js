Imports System.Drawing
Imports System.IO

Public Class Status_full
    Inherits System.Web.UI.Page

    Public icon_cog As String = "<i class=""fa fa-cog fa-spin"" style=""color:lime""></i>"
    Public icon_pause As String = "<i class=""fa fa-pause-circle fa-lg"" style=""color:black""></i>"
    Public icon_NA As String = "<i class=""fa fa-ban fa-lg"" style=""color:black""></i>"
    Public icon_NoConnection As String = "<i class=""fas fa-eye-slash fa-lg"" style=""color:black""></i>"
    Public icon_idle As String = "<i class=""fas fa-play"" style=""color:lime""></i>"
    Public icon_service As String = "<i class=""fas fa-wrench fa-lg"" style=""color:black""></i>"
    Public icon_available As String = "<i class=""fa fa-check-circle fa-lg"" style=""color:lime""></i>"

    Dim machines As Dictionary(Of String, String)
    Dim instruments As Dictionary(Of String, Integer)

    Protected Sub loadInstruments()

        machines = New Dictionary(Of String, String)

        machines.Add("74", "LCMS-0")
        machines.Add("20", "LCMS-1")
        machines.Add("42", "LCMS-2")
        machines.Add("43", "LCMS-3")
        machines.Add("44", "LCMS-4")
        machines.Add("45", "LCMS-5")
        machines.Add("51", "LCMS-6")
        machines.Add("61", "LCMS-7")
        machines.Add("67", "LCMS-8")
        machines.Add("71", "LCMS-9")
        machines.Add("72", "LCMS-10")
        machines.Add("82", "LCMS-11")
        machines.Add("83", "LCMS-12")
        machines.Add("88", "LCMS-13")

        machines.Add("53", "Prep-1")
        machines.Add("52", "Prep-2")
        machines.Add("56", "Prep-3")
        machines.Add("55", "Prep-4")
        machines.Add("62", "Prep-5")
        machines.Add("66", "Prep-6")
        machines.Add("80", "Prep-7")
        machines.Add("79", "Prep-8")
        machines.Add("81", "Prep-9")
        machines.Add("84", "Prep-10")
        machines.Add("85", "Prep-11")
        machines.Add("86", "Prep-12")
        machines.Add("87", "SFCMS-1")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim args As New DataSourceSelectArguments()
        Dim AnalyticalView As DataView = SqlDataSource2.Select(args)
        Dim dt_anal As DataTable = AnalyticalView.ToTable()

        Dim PrepView As DataView = SqlDataSource3.Select(args)
        Dim dt_prep As DataTable = PrepView.ToTable()

        Load_LCMS_Row(TableRow1, "LCMS-1", dt_anal)
        Load_LCMS_Row(TableRow2, "LCMS-2", dt_anal)
        Load_LCMS_Row(TableRow3, "LCMS-3", dt_anal)
        Load_LCMS_Row(TableRow4, "LCMS-4", dt_anal)
        Load_LCMS_Row(TableRow5, "LCMS-5", dt_anal)
        Load_LCMS_Row(TableRow6, "LCMS-6", dt_anal)
        Load_LCMS_Row(TableRow7, "LCMS-7", dt_anal)
        Load_LCMS_Row(TableRow8, "LCMS-8", dt_anal)
        Load_LCMS_Row(TableRow9, "LCMS-11", dt_anal)
        Load_LCMS_Row(TableRow10, "LCMS-12", dt_anal)
        Load_LCMS_Row(TableRow11, "LCMS-13", dt_anal)

        Load_Prep_Row(TableRow12, "PrepLCMS-1", dt_prep)
        Load_Prep_Row(TableRow13, "PrepLCMS-2", dt_prep)
        Load_Prep_Row(TableRow14, "PrepLCMS-3", dt_prep)
        Load_Prep_Row(TableRow15, "PrepLCMS-4", dt_prep)
        Load_Prep_Row(TableRow16, "PrepLCMS-5", dt_prep)
        Load_Prep_Row(TableRow17, "PrepLCMS-6", dt_prep)
        Load_Prep_Row(TableRow18, "PrepLCMS-7", dt_prep)
        Load_Prep_Row(TableRow19, "PrepLCMS-8", dt_prep)
        Load_Prep_Row(TableRow20, "PrepLCMS-9", dt_prep)

        Label2.Text = Date.Now.ToString

        loadInstruments()
        loadGraphs()

    End Sub

    Private Sub ChangeRowFontColor(ByVal myRow As TableRow)

        For Each mycell As TableCell In myRow.Cells
            mycell.ForeColor = System.Drawing.Color.Black
            mycell.Font.Bold = True
        Next
    End Sub

    Private Sub Load_LCMS_Row(ByVal myRow As TableRow, ByVal InstName As String, ByVal dt As DataTable)
        Dim myrows() As DataRow = dt.Select("INSTRUMENT_NAME = '" & InstName & "'")
        Dim stat_text As String

        'Put font awesome icons according to availability
        'If Column or queue states report error convert availability to No - This is a second measure to detect errors
        If (myrows(0).Item(4).ToString.Contains("Error") Or myrows(0).Item(9).ToString.Contains("Error")) Then
            myRow.Cells.Item(1).Text = "No"
        Else
            myRow.Cells.Item(1).Text = myrows(0).Item(1).ToString
        End If


        If (myRow.Cells.Item(1).Text.Contains("Yes")) Then
            myRow.Cells.Item(1).Text = icon_available
        ElseIf (myRow.Cells.Item(1).Text.Contains("No")) Then
            myRow.Cells.Item(1).Text = icon_NA
            myRow.BackColor = System.Drawing.Color.Red
            ChangeRowFontColor(myRow)
        ElseIf (myRow.Cells.Item(1).Text.Contains("NC")) Then
            myRow.Cells.Item(1).Text = icon_NoConnection
            myRow.BackColor = System.Drawing.Color.Red
            ChangeRowFontColor(myRow)
        ElseIf (myRow.Cells.Item(1).Text.Contains("Svc")) Then
            myRow.Cells.Item(1).Text = icon_service
            myRow.BackColor = System.Drawing.Color.Yellow
            ChangeRowFontColor(myRow)
        End If

        'Put Queue
        myRow.Cells.Item(2).Text = myrows(0).Item(2).ToString

        'Put Estimated Time
        myRow.Cells.Item(3).Text = myrows(0).Item(3).ToString

        'Put Status if it is available
        stat_text = ""
        If (myRow.Cells.Item(1).Text = icon_available) Then
            Select Case myrows(0).Item(4).ToString
                Case "Waiting"
                    stat_text = icon_idle
                Case "WAITING"
                    stat_text = icon_idle
                Case "IN RUN"
                    stat_text = icon_cog
                Case "Running"
                    stat_text = icon_cog
                Case Else
                    stat_text = icon_idle
            End Select
        End If

        myRow.Cells.Item(4).Text = stat_text

        'username and job id
        myRow.Cells.Item(5).Text = myrows(0).Item(5).ToString
        myRow.Cells.Item(6).Text = myrows(0).Item(6).ToString

        myRow.Cells.Item(7).Text = myrows(0).Item(7).ToString
        myRow.Cells.Item(8).Text = myrows(0).Item(8).ToString
        myRow.Cells.Item(9).Text = myrows(0).Item(9).ToString
    End Sub

    Private Sub Load_Prep_Row(ByVal myRow As TableRow, ByVal InstName As String, ByVal dt As DataTable)
        Dim myrows() As DataRow = dt.Select("INSTRUMENT_NAME = '" & InstName & "'")
        Dim temp() As String
        Dim stat_text As String

        'Put font awesome icons according to availability
        If (myrows(0).Item(1).ToString.Contains("Yes")) Then
            myRow.Cells.Item(1).Text = icon_available
        ElseIf (myrows(0).Item(1).ToString.Contains("Svc")) Then
            myRow.Cells.Item(1).Text = icon_service
            myRow.BackColor = System.Drawing.Color.Orange
            ChangeRowFontColor(myRow)
        ElseIf (myrows(0).Item(1).ToString.Contains("No")) Then
            myRow.Cells.Item(1).Text = icon_NA
            myRow.BackColor = System.Drawing.Color.Red
            ChangeRowFontColor(myRow)
        End If

        If (myrows(0).Item(1).ToString.Contains("PAUSED")) Then
            myRow.Cells.Item(1).Text = icon_pause
            myRow.BackColor = System.Drawing.Color.Yellow
            ChangeRowFontColor(myRow)
        End If

        'Put Queue
        myRow.Cells.Item(2).Text = myrows(0).Item(2).ToString
        myRow.Cells.Item(3).Text = myrows(0).Item(3).ToString

        'Divide the Status from "-" signs to get rid of Yes
        temp = myrows(0).Item(1).ToString.Split(New Char() {"-"c}, 2)
        'Remove OK- to leave Running/Idle type description
        If (temp.Count > 1) Then
            stat_text = temp(1).Replace("OK-", "")
        Else
            stat_text = myrows(0).Item(4).ToString
        End If

        'If the machine is not idle put the jobname
        If (stat_text = "Idle") Then
            myRow.Cells.Item(4).Text = icon_idle
        ElseIf (stat_text = "Running") Then
            myRow.Cells.Item(4).Text = icon_cog
        End If

        myRow.Cells.Item(5).Text = myrows(0).Item(5).ToString
            myRow.Cells.Item(6).Text = myrows(0).Item(6).ToString



        'Method
        myRow.Cells.Item(7).Text = myrows(0).Item(7).ToString

        'Flowrate
        myRow.Cells.Item(8).Text = myrows(0).Item(8).ToString

        'PercentB
        myRow.Cells.Item(9).Text = myrows(0).Item(9).ToString

        'Process options
        myRow.Cells.Item(10).Text = myrows(0).Item(10).ToString

        'Runs
        myRow.Cells.Item(11).Text = myrows(0).Item(11).ToString

        'Detect State
        myRow.Cells.Item(12).Text = myrows(0).Item(12).ToString

        'Last Updated
        myRow.Cells.Item(13).Text = myrows(0).Item(13).ToString

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Response.Redirect("status.aspx")
    End Sub


    'This part is the errorlog

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)

        'If the instrument id is a number than get the instrument name from the dictionary
        If e.Row.Cells.Count > 3 Then
            If Regex.IsMatch(e.Row.Cells(3).Text, "^[0-9 ]+$") Then
                e.Row.Cells(3).Text = machines.Item(e.Row.Cells(3).Text)
            End If
        End If


    End Sub


    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        For Each item As GridViewRow In GridView1.Rows

            If (TryCast(item.Cells(0).FindControl("cbSelect"), CheckBox)).Checked Then
                Dim dt As DateTime = DateTime.Parse(item.Cells(2).Text)
                SqlDataSource1.DeleteParameters(":ERROR_DATE").DefaultValue = dt
                SqlDataSource1.Delete()
            End If
        Next
        GridView1.DataBind()


    End Sub

    Protected Sub deleteRow(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)
        SqlDataSource1.DeleteCommand = "DELETE FROM INSTRUMENT_ERROR_LOG WHERE ERROR_DATE=:ERROR_DATE"
        SqlDataSource1.DeleteParameters.Add("ERROR_DATE", GridView1.DataKeys(e.RowIndex).Values("ERROR_DATE"))
        SqlDataSource1.Delete()
        'GridView1.DataBind()
    End Sub

    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        SqlDataSource1.DeleteCommand = "DELETE FROM INSTRUMENT_ERROR_LOG"
        SqlDataSource1.Delete()
        GridView1.DataBind()
    End Sub


    'This part is pressure graphs

    Dim darkcolor As System.Drawing.Color = System.Drawing.Color.FromArgb(255, 11, 41, 81)
    Dim lightcolor As System.Drawing.Color = System.Drawing.Color.FromArgb(255, 255, 255, 255)
    Dim fontcolor As System.Drawing.Color = System.Drawing.Color.FromArgb(255, 255, 255, 255)
    Dim legendfontcolor As System.Drawing.Color = System.Drawing.Color.FromArgb(255, 0, 0, 0)
    Dim gridcolor As System.Drawing.Color = System.Drawing.Color.FromArgb(255, 150, 150, 150)
    Dim linecolor As System.Drawing.Color = System.Drawing.Color.FromArgb(255, 0, 255, 0)

    Protected Function GetInstrumentTable() As DataTable

        SqlDataSource4.SelectCommand = "SELECT INSTRUMENT_ID,PRESSURE_END,PRESSURE_DATE,SAMPLEID FROM INSTRUMENT_PRESSURE_LOG ORDER BY PRESSURE_DATE DESC FETCH FIRST 3000 ROWS ONLY"
        Dim args As New DataSourceSelectArguments()
        Dim View As DataView = SqlDataSource4.Select(args)
        Dim dt As DataTable = View.ToTable()

        Return dt
    End Function


    Protected Sub loadGraphs()

        instruments = New Dictionary(Of String, Integer)

        instruments.Add("LCMS-0", 74)
        instruments.Add("LCMS-1", 20)
        instruments.Add("LCMS-2", 42)
        instruments.Add("LCMS-3", 43)
        instruments.Add("LCMS-4", 44)
        instruments.Add("LCMS-5", 45)
        instruments.Add("LCMS-6", 51)
        instruments.Add("LCMS-7", 61)
        instruments.Add("LCMS-8", 67)
        instruments.Add("LCMS-9", 71)
        instruments.Add("LCMS-10", 72)
        instruments.Add("LCMS-11", 82)
        instruments.Add("LCMS-12", 83)
        instruments.Add("LCMS-13", 88)


        drawWatersChart(Chart1, "LCMS-1")
        drawAgilentChart(Chart2a, Chart2b, "LCMS-2")
        drawWatersChart(Chart3, "LCMS-3")
        drawWatersChart(Chart4, "LCMS-4")
        drawAgilentChart(Chart5a, Chart5b, "LCMS-5")
        drawWatersChart(Chart6, "LCMS-6")
        drawWatersChart(Chart7, "LCMS-7")
        drawWatersChart(Chart8, "LCMS-8")
        drawWatersChart(Chart9, "LCMS-9")
        drawWatersChart(Chart10, "LCMS-10")
        drawWatersChart(Chart11, "LCMS-11")
        drawWatersChart(Chart12, "LCMS-12")
        drawWatersChart(Chart13, "LCMS-13")
    End Sub

    Protected Sub drawWatersChart(ByVal myChart As DataVisualization.Charting.Chart, ByVal instrumentName As String)
        Dim myRow As DataRow

        'Retrive the instrument id corresponding to the LCMS name
        Dim id As Integer = instruments.Item(instrumentName)

        'Create a list of rows corresponding to the the instrument id
        Dim rows() As DataRow = GetInstrumentTable().Select("INSTRUMENT_ID = " & id)


        myChart.Series.Clear()


        'Add the chart
        myChart.Series.Add(instrumentName)
        myChart.Series(instrumentName).ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Column
        myChart.BackColor = darkcolor
        myChart.ChartAreas(0).AxisX.LineColor = fontcolor
        myChart.ChartAreas(0).AxisY.LineColor = fontcolor
        myChart.ChartAreas(0).BackColor = lightcolor
        myChart.ChartAreas(0).AxisY.LabelStyle.ForeColor = fontcolor

        'Get the last 30 pressure points
        Dim max = 30
        If (rows.Count < 30) Then
            max = rows.Count - 1
        End If

        For c As Integer = 0 To max
            Try
                myRow = rows(c)
                myChart.Series(instrumentName).Points.AddXY(myRow(2), myRow(1))
            Catch ex As Exception
                'MsgBox(c & instrumentName)
            End Try
        Next

        'Format Chart
        myChart.Series(instrumentName)("PixelPointWidth") = "2"
        myChart.ChartAreas(0).AxisY.Minimum = 300
        myChart.ChartAreas(0).AxisY.Maximum = 1000
        myChart.ChartAreas(0).AxisX.LabelStyle.Enabled = False
        myChart.ChartAreas(0).AxisX.MajorGrid.Enabled = False
        myChart.ChartAreas(0).AxisX.MinorGrid.Enabled = False
        myChart.Legends(0).DockedToChartArea = myChart.ChartAreas(0).Name
        myChart.Legends(0).Docking = DataVisualization.Charting.Docking.Top
        myChart.ChartAreas(0).AxisY.MajorGrid.LineColor = gridcolor
        myChart.Legends(0).ForeColor = legendfontcolor

        'Add vertical Line
        Dim vl As DataVisualization.Charting.HorizontalLineAnnotation = New DataVisualization.Charting.HorizontalLineAnnotation()
        vl.AxisX = myChart.ChartAreas(0).AxisX
        vl.AxisY = myChart.ChartAreas(0).AxisY
        vl.LineWidth = 2
        vl.LineDashStyle = DataVisualization.Charting.ChartDashStyle.DashDot
        vl.AnchorY = 900
        vl.ClipToChartArea = "ChartArea1"
        vl.IsInfinitive = True
        vl.LineColor = linecolor
        myChart.Annotations.Add(vl)

    End Sub

    Protected Sub drawAgilentChart(ByVal myChartA As DataVisualization.Charting.Chart, ByVal myChartB As DataVisualization.Charting.Chart, ByVal instrumentName As String)
        Dim myRow As DataRow
        Dim NH4 = instrumentName & "_NH4"
        Dim TFA = instrumentName & "_TFA"
        Dim max As Integer = 30

        'Retrive the instrument id corresponding to the LCMS name
        Dim id As Integer = instruments.Item(instrumentName)

        'Create a list of rows corresponding to the the instrument id
        Dim rows() As DataRow = GetInstrumentTable().Select("INSTRUMENT_ID = " & id)


        myChartA.Series.Clear()
        myChartB.Series.Clear()

        'Add the chart
        myChartA.Series.Add(NH4)
        myChartA.Series(NH4).ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Column
        myChartB.Series.Add(TFA)
        myChartB.Series(TFA).ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Column
        myChartA.BackColor = darkcolor
        myChartA.ChartAreas(0).AxisX.LineColor = fontcolor
        myChartA.ChartAreas(0).AxisY.LineColor = fontcolor
        myChartA.ChartAreas(0).BackColor = lightcolor
        myChartA.ChartAreas(0).AxisY.LabelStyle.ForeColor = fontcolor
        myChartB.BackColor = darkcolor
        myChartB.ChartAreas(0).AxisX.LineColor = fontcolor
        myChartB.ChartAreas(0).AxisY.LineColor = fontcolor
        myChartB.ChartAreas(0).BackColor = lightcolor
        myChartB.ChartAreas(0).AxisY.LabelStyle.ForeColor = fontcolor

        'Get the last 30 pressure points
        If max >= rows.Count Then
            max = rows.Count - 1
        End If

        For c As Integer = 0 To max
            myRow = rows(c)
            If (myRow(3).ToString.Contains("NH4")) Then
                If (myChartA.Series(NH4).Points.Count < 30) Then
                    myChartA.Series(NH4).Points.AddXY(myRow(2), myRow(1))
                End If
            Else
                If (myChartB.Series(TFA).Points.Count < 30) Then
                    myChartB.Series(TFA).Points.AddXY(myRow(2), myRow(1))
                End If
            End If
        Next

        myChartA.Series(NH4)("PixelPointWidth") = "2"
        myChartA.ChartAreas(0).AxisY.Minimum = 150
        myChartA.ChartAreas(0).AxisY.Maximum = 650
        myChartA.ChartAreas(0).AxisX.LabelStyle.Enabled = False
        myChartA.ChartAreas(0).AxisX.MajorGrid.Enabled = False
        myChartA.ChartAreas(0).AxisX.MinorGrid.Enabled = False
        myChartA.Legends(0).DockedToChartArea = myChartA.ChartAreas(0).Name
        myChartA.Legends(0).Docking = DataVisualization.Charting.Docking.Top
        myChartA.Series(NH4).Color = Drawing.Color.Red
        myChartA.ChartAreas(0).AxisY.MajorGrid.LineColor = gridcolor
        myChartA.Legends(0).ForeColor = legendfontcolor

        myChartB.Series(TFA)("PixelPointWidth") = "2"
        myChartB.ChartAreas(0).AxisY.Minimum = 150
        myChartB.ChartAreas(0).AxisY.Maximum = 650
        myChartB.ChartAreas(0).AxisX.LabelStyle.Enabled = False
        myChartB.ChartAreas(0).AxisX.MajorGrid.Enabled = False
        myChartB.ChartAreas(0).AxisX.MinorGrid.Enabled = False
        myChartB.Legends(0).DockedToChartArea = myChartB.ChartAreas(0).Name
        myChartB.Legends(0).Docking = DataVisualization.Charting.Docking.Top
        myChartB.Series(TFA).Color = System.Drawing.Color.FromArgb(255, 0, 255, 0)
        myChartB.ChartAreas(0).AxisY.MajorGrid.LineColor = gridcolor
        myChartB.Legends(0).ForeColor = legendfontcolor

        'Add vertical Line
        Dim vlA As DataVisualization.Charting.HorizontalLineAnnotation = New DataVisualization.Charting.HorizontalLineAnnotation()
        vlA.AxisX = myChartA.ChartAreas(0).AxisX
        vlA.AxisY = myChartA.ChartAreas(0).AxisY
        vlA.LineColor = System.Drawing.Color.FromArgb(255, 255, 0, 0)
        vlA.LineWidth = 2
        vlA.LineDashStyle = DataVisualization.Charting.ChartDashStyle.DashDot
        vlA.AnchorY = 550
        vlA.ClipToChartArea = "ChartArea1"
        vlA.IsInfinitive = True
        vlA.LineColor = linecolor
        myChartA.Annotations.Add(vlA)


        'Add vertical Line
        Dim vlB As DataVisualization.Charting.HorizontalLineAnnotation = New DataVisualization.Charting.HorizontalLineAnnotation()
        vlB.AxisX = myChartB.ChartAreas(0).AxisX
        vlB.AxisY = myChartB.ChartAreas(0).AxisY
        vlB.LineColor = Drawing.Color.Red
        vlB.LineWidth = 2
        vlB.LineDashStyle = DataVisualization.Charting.ChartDashStyle.DashDot
        vlB.AnchorY = 550
        vlB.ClipToChartArea = "ChartArea1"
        vlB.IsInfinitive = True
        vlB.LineColor = linecolor
        myChartB.Annotations.Add(vlB)
    End Sub
End Class