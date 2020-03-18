Imports Oracle.ManagedDataAccess.Client


Public Class pressure1
    Inherits System.Web.UI.Page

    Protected Function GetInstrumentTable() As DataTable

        SqlDataSource1.SelectCommand = "SELECT ID, INSTTYPE, INSTNAME FROM INSTRUMENTS WHERE ((INSTTYPE = 'LCMS') OR (INSTTYPE = 'PrepLCMS'))"
        Dim args As New DataSourceSelectArguments()
        Dim View As DataView = SqlDataSource1.Select(args)
        Dim dt As DataTable = View.ToTable()
        Return dt
    End Function


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim i As Integer
        Dim InstrumentList As New List(Of String)

        For i = 0 To ListBox1.Items.Count - 1
            If ListBox1.Items(i).Selected = True Then
                InstrumentList.Add(ListBox1.Items(i).Text)
            End If
        Next

        If (InstrumentList.Count = 0) Then
            If (TextBox1.Text <> Calendar1.SelectedDate.ToShortDateString()) Then
                Calendar1.SelectedDates.Add(Now.AddDays(-1))
                Calendar2.SelectedDates.Add(Date.Today)
                TextBox1.Text = Calendar1.SelectedDate.ToShortDateString()
                TextBox2.Text = Calendar2.SelectedDate.ToShortDateString()
                Reset()
            Else
                Reset()
            End If

        Else
            PlotSelected(InstrumentList)
        End If




    End Sub

    Protected Sub Reset()
        Dim dt As DataTable = GetInstrumentTable()
        Dim myRow As DataRow
        Dim mySeries As String
        Dim myInstID As String
        Dim myReader As DataView

        Chart1.Series.Dispose()
        Chart1.Series.Clear()

        For Each myRow In dt.Rows
            If myRow(1).Equals("LCMS") Then

                mySeries = myRow(2).ToString
                myInstID = myRow(0).ToString

                Chart1.Series.Add(mySeries)
                SqlDataSource3.SelectParameters(":ID").DefaultValue = myInstID
                SqlDataSource3.SelectParameters(":StartDate").DefaultValue = Calendar1.SelectedDate
                SqlDataSource3.SelectParameters(":EndDate").DefaultValue = Calendar2.SelectedDate


                If (SqlDataSource3.Select(DataSourceSelectArguments.Empty)) IsNot Nothing Then
                    myReader = SqlDataSource3.Select(DataSourceSelectArguments.Empty)
                    Chart1.Series(mySeries).Points.DataBind(SqlDataSource3.Select(DataSourceSelectArguments.Empty), "PRESSURE_DATE", "PRESSURE_END", "")
                    Chart1.Series(mySeries).ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.FastPoint
                End If
            End If
        Next
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Calendar1.SelectedDates.Clear()
        Calendar2.SelectedDates.Clear()

        Calendar1.SelectedDates.Add(Now.AddDays(-7))
        Calendar2.SelectedDates.Add(Date.Today)
        ListBox1.ClearSelection()
        Reset()

    End Sub

    Protected Sub PlotSelected(ByVal InstrumentList As List(Of String))

        Dim dt As DataTable = GetInstrumentTable()
        Dim myRow As DataRow
        Dim mySeries As String
        Dim myInstID As String
        Dim myReader As DataView

        Chart1.Series.Dispose()
        Chart1.Series.Clear()

        For Each myRow In dt.Rows
            If myRow(1).Equals("LCMS") Then
                If InstrumentList.Contains(myRow(2).ToString) Then

                    mySeries = myRow(2).ToString
                    myInstID = myRow(0).ToString

                    Chart1.Series.Add(mySeries)
                    SqlDataSource3.SelectParameters(":ID").DefaultValue = myInstID
                    SqlDataSource3.SelectParameters(":StartDate").DefaultValue = Calendar1.SelectedDate
                    SqlDataSource3.SelectParameters(":EndDate").DefaultValue = Calendar2.SelectedDate

                    If (SqlDataSource3.Select(DataSourceSelectArguments.Empty)) IsNot Nothing Then
                        myReader = SqlDataSource3.Select(DataSourceSelectArguments.Empty)
                        Chart1.Series(mySeries).Points.DataBind(SqlDataSource3.Select(DataSourceSelectArguments.Empty), "PRESSURE_DATE", "PRESSURE_END", "")
                        Chart1.Series(mySeries).ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.FastPoint
                    End If
                End If
            End If

        Next
    End Sub

    Protected Sub DateChange1(sender As Object, e As EventArgs) Handles Calendar1.SelectionChanged
        TextBox1.Text = Calendar1.SelectedDate.ToShortDateString()
    End Sub

    Protected Sub DateChange2(sender As Object, e As EventArgs) Handles Calendar2.SelectionChanged
        TextBox2.Text = Calendar2.SelectedDate.ToShortDateString()
    End Sub

End Class