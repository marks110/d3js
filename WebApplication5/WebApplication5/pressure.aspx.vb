Imports Oracle.ManagedDataAccess.Client


Public Class pressure
    Inherits System.Web.UI.Page

    Dim machines As Dictionary(Of String, Integer)
    Dim darkcolor As System.Drawing.Color = System.Drawing.Color.FromArgb(255, 11, 41, 81)
    Dim lightcolor As System.Drawing.Color = System.Drawing.Color.FromArgb(255, 255, 255, 255)
    Dim fontcolor As System.Drawing.Color = System.Drawing.Color.FromArgb(255, 255, 255, 255)
    Dim legendfontcolor As System.Drawing.Color = System.Drawing.Color.FromArgb(255, 0, 0, 0)
    Dim gridcolor As System.Drawing.Color = System.Drawing.Color.FromArgb(255, 150, 150, 150)
    Dim linecolor As System.Drawing.Color = System.Drawing.Color.FromArgb(255, 0, 255, 0)

    Protected Function GetInstrumentTable() As DataTable

        SqlDataSource1.SelectCommand = "SELECT INSTRUMENT_ID,PRESSURE_END,PRESSURE_DATE,SAMPLEID FROM INSTRUMENT_PRESSURE_LOG ORDER BY PRESSURE_DATE DESC FETCH FIRST 3000 ROWS ONLY"
        Dim args As New DataSourceSelectArguments()
        Dim View As DataView = SqlDataSource1.Select(args)
        Dim dt As DataTable = View.ToTable()

        Return dt
    End Function


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        loadInstruments()
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

    Protected Sub loadInstruments()

        machines = New Dictionary(Of String, Integer)

        machines.Add("LCMS-0", 74)
        machines.Add("LCMS-1", 20)
        machines.Add("LCMS-2", 42)
        machines.Add("LCMS-3", 43)
        machines.Add("LCMS-4", 44)
        machines.Add("LCMS-5", 45)
        machines.Add("LCMS-6", 51)
        machines.Add("LCMS-7", 61)
        machines.Add("LCMS-8", 67)
        machines.Add("LCMS-9", 71)
        machines.Add("LCMS-10", 72)
        machines.Add("LCMS-11", 82)
        machines.Add("LCMS-12", 83)
        machines.Add("LCMS-13", 88)

    End Sub

    Protected Sub drawWatersChart(ByVal myChart As DataVisualization.Charting.Chart, ByVal instrumentName As String)
        Dim myRow As DataRow

        'Retrive the instrument id corresponding to the LCMS name
        Dim id As Integer = machines.Item(instrumentName)

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
                MsgBox(c & instrumentName)
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
        Dim id As Integer = machines.Item(instrumentName)

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