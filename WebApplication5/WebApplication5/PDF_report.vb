
Imports System.Drawing
Imports System.Web.UI.DataVisualization.Charting
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class PDF_report

    Inherits System.Web.UI.Page

    Private Model As LCMSModel
    Private massSpec As MassSpec
    Private features As List(Of String)
    Private mintime As Double
    Private maxtime As Double

    'Report table variables
    Public sampleid As String = "23423"
    Public daterun As String = "02/03/2020"
    Public username As String = "sysadmin"
    Public datereprocessed As String = Date.Now.ToString
    Public comment As String = "blank"
    Public instname As String = "LCMS-1"

    Private logo As Bitmap = My.Resources.corp_logo
    Private Chart1 As Global.System.Web.UI.DataVisualization.Charting.Chart
    Private Chart2 As Global.System.Web.UI.DataVisualization.Charting.Chart


    Public Sub New(ByVal Chart1 As Global.System.Web.UI.DataVisualization.Charting.Chart, ByVal Chart2 As Global.System.Web.UI.DataVisualization.Charting.Chart, ByVal features As List(Of String))

        Me.Chart1 = Chart1
        Me.Chart2 = Chart2
        Me.features = features

        DrawWaters()

    End Sub


    Public Sub DrawWaters()
        Dim rpt As String
        Dim txt As String
        Dim watersfolder As String
        Dim watersjobname As String

        'watersjobname = "12379-80_8807"
        'watersfolder = "C:\LCMS\temp\"
        'rpt = watersfolder & watersjobname & "\" & watersjobname & ".rpt"
        'txt = watersfolder & watersjobname & "\" & watersjobname & ".txt"


        rpt = "C:\data\test1.rpt"
        txt = "C:\data\test1.txt"

        Me.Model = WatersLCMSModelFactory.getModel(rpt, txt)
        Me.mintime = (LCMSModel.getMinValueInt(Me.Model.getRetTimes) / 60000)
        Me.maxtime = (LCMSModel.getMaxValueInt(Me.Model.getRetTimes) / 60000)

        Dim x As List(Of Integer) = Me.Model.getRetTimes
        Dim y As List(Of List(Of MSPoint)) = Me.Model.getMassSpecs

        Me.massSpec = New MassSpec(Me.Model.getMassSpecs, Me.Model.getRetTimes)

        Chart1.Series.Clear()

        DrawUV220(Chart1)
        DrawUV254(Chart1)
        DrawTIC(Chart1)

        Chart1.ChartAreas(0).AxisX.LabelStyle.Format = "#.#"
        Chart1.ChartAreas(0).AxisY.LabelStyle.Format = "{0}%"
        Chart1.ChartAreas(0).AxisY.Maximum = 100

        Chart1.ChartAreas(0).AxisX.MajorGrid.Enabled = False
        Chart1.ChartAreas(0).AxisX.MinorGrid.Enabled = False
        Chart1.ChartAreas(0).AxisY.MajorGrid.Enabled = False
        Chart1.ChartAreas(0).AxisY.MinorGrid.Enabled = False

        Chart1.ChartAreas(0).AxisY.LabelStyle.Enabled = False
        Chart1.ChartAreas(0).AxisX.Interval = 0.2
        Chart1.ChartAreas(0).AxisX.IntervalOffset = 0
        Chart1.ChartAreas(0).AxisX.MinorTickMark.Enabled = True

        Chart1.DataBind()

    End Sub



    Private Sub DrawUV220(ByVal myChart As Chart)

        Dim PercentIntensity As New List(Of Double)
        Dim maxIntensity As Double = LCMSModel.getMaxValueDouble(Model.getUV220Int)


        For i As Integer = 0 To (Me.Model.getUV220Int.Count - 1)
            PercentIntensity.Add(Me.Model.getUV220Int.Item(i) / maxIntensity * 100)
        Next

        myChart.Series.Add(New Series("UV220"))
        myChart.Series("UV220").IsValueShownAsLabel = False
        myChart.Series("UV220").BorderWidth = 1
        myChart.Series("UV220").ChartType = SeriesChartType.Line
        myChart.Series("UV220").Points.DataBindXY(Me.Model.getUVretTimes(), PercentIntensity)
        myChart.Series("UV220").Color = Color.Green



    End Sub

    Private Sub DrawUV254(ByVal myChart As Chart)

        Dim PercentIntensity As New List(Of Double)
        Dim maxIntensity As Double = LCMSModel.getMaxValueDouble(Model.getUV254Int)


        For i As Integer = 0 To (Me.Model.getUV254Int.Count - 1)
            PercentIntensity.Add(Me.Model.getUV254Int.Item(i) / maxIntensity * 100)
        Next

        myChart.Series.Add(New Series("UV254"))
        myChart.Series("UV254").IsValueShownAsLabel = False
        myChart.Series("UV254").BorderWidth = 1
        myChart.Series("UV254").ChartType = SeriesChartType.Line
        myChart.Series("UV254").Points.DataBindXY(Me.Model.getUVretTimes(), PercentIntensity)
        myChart.Series("UV254").Color = Color.Orange



    End Sub

    Private Sub DrawTIC(ByVal myChart As Chart)

        Dim PercentIntensity As New List(Of Double)
        Dim RetTimes As New List(Of Double)
        Dim maxIntensity As Double = LCMSModel.getMaxValueDouble(Model.getMSInt)
        Dim minIntensity As Double = LCMSModel.getMinValueDouble(Model.getMSInt)


        For i As Integer = 0 To (Me.Model.getMSInt.Count - 1)
            PercentIntensity.Add((Me.Model.getMSInt.Item(i) - minIntensity) / maxIntensity * 100)
            RetTimes.Add(Me.Model.getRetTimes.Item(i) / 60000)
        Next

        myChart.Series.Add(New Series("TIC"))
        myChart.Series("TIC").IsValueShownAsLabel = False
        myChart.Series("TIC").BorderWidth = 1
        myChart.Series("TIC").ChartType = SeriesChartType.Line
        myChart.Series("TIC").Points.DataBindXY(RetTimes, PercentIntensity)
        myChart.Series("TIC").Color = Color.Black

    End Sub


    'Private Sub Chart1_Paint(sender As Object, e As PaintEventArgs) Handles Chart1.Paint
    '    Dim blueBrush As New System.Drawing.SolidBrush(Color.FromArgb(50, System.Drawing.Color.Blue))
    '    Dim silverBrush As New System.Drawing.SolidBrush(Color.FromArgb(50, System.Drawing.Color.Silver))
    '    Dim redBrush As New System.Drawing.SolidBrush(Color.FromArgb(50, System.Drawing.Color.Red))

    '    Dim y0 As Single
    '    Dim y1 As Single
    '    Dim myRectangle As Rectangle

    '    y1 = Chart1.ChartAreas(0).AxisY.ValueToPixelPosition(-20.0)
    '    y0 = Chart1.ChartAreas(0).AxisY.ValueToPixelPosition(100.0)

    '    If IsMouseMoving = True Then
    '        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
    '        myRectangle = New Rectangle(DownPoint.X, y0, MovingPoint.X - DownPoint.X, y1)
    '        e.Graphics.FillRectangle(silverBrush, myRectangle)
    '    ElseIf IsMouseUp = True And DownPoint.X > -1 Then
    '        'If it is a single click draw a line
    '        If DownPoint.X = UpPoint.X Then
    '            e.Graphics.DrawLine(New Pen(Brushes.Blue, 3), UpPoint.X, y0, UpPoint.X, y1)
    '        End If

    '        'Draw the rectangle
    '        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
    '        myRectangle = New Rectangle(DownPoint.X, y0, UpPoint.X - DownPoint.X, y1)
    '        e.Graphics.FillRectangle(blueBrush, myRectangle)

    '        'Set scans for integration and display the mass spec plot
    '        Me.massSpec.setScans(NearestX(UpPoint.X), NearestX(UpPoint.X))
    '        'Me.massSpec.setSubs(Integer.parseInt(token(3)), Integer.parseInt(token(4)))
    '        'Me.massSpec.setZooms(Double.parseDouble(token(5)), Double.parseDouble(token(6)))
    '        DrawMS()
    '    End If
    'End Sub

    Private Function NearestX(mouseX As Double) As Integer
        Dim px As Double
        Dim px1 As Double
        Dim myX As Double
        Dim _x As Integer
        Dim i As Integer
        Dim PointCount As Integer

        PointCount = Chart1.Series("TIC").Points.Count - 1

        myX = Chart1.ChartAreas(0).AxisX.ValueToPixelPosition(Chart1.Series(0).Points.Item(0).XValue)

        For i = 1 To PointCount
            px = Chart1.ChartAreas(0).AxisX.ValueToPixelPosition(Chart1.Series("TIC").Points.Item(i - 1).XValue)
            px1 = Chart1.ChartAreas(0).AxisX.ValueToPixelPosition(Chart1.Series("TIC").Points.Item(i).XValue)

            If (mouseX > px And mouseX < px1) Then
                myX = px
                _x = i - 1
            End If
        Next

        Return _x

    End Function


    'Draw the MS plot at the bottom
    Private Function DrawMS(ByVal description As String) As String

        Dim PercentIntensity As Double

        Dim maxInt As Double = -1
        Dim minInt As Double = -1
        Dim lowMass As Double = Me.massSpec.getLowMass
        Dim highMass As Double = Me.massSpec.getHighMass
        Dim targetPoint As Int32
        Dim token() As String = description.Split(";")
        Dim label As String = ("MS @ " + Me.convScantoMin(token(1)))

        'Range
        If (Not token(2).Equals("-1") AndAlso Not token(2).Equals(token(1))) Then
            label = (label + ("-" + Me.convScantoMin(token(2))))
        End If

        'Baseline subtract
        If Not token(3).Equals("-1") Then
            label = (label + (" minus " + Me.convScantoMin(token(3))))
        End If
        If (Not token(4).Equals("-1") AndAlso Not token(4).Equals(token(3))) Then
            label = (label + ("-" + Me.convScantoMin(token(4))))
        End If
        label = (label + " min")

        'Set Zoom
        If Not token(5).Equals("-1") AndAlso Not token(6).Equals("-1") Then
            lowMass = Double.Parse(token(5))
            highMass = Double.Parse(token(6))
        End If

        'Set time window for the MS
        Dim scan1 As Integer = Integer.Parse(token(1))
        Dim scan2 As Integer = Integer.Parse(token(2))
        Me.massSpec.setScans(scan1, scan2)
        Dim msData As List(Of MSPoint) = Me.massSpec.getMS


        'Clear earlier graph
        Chart2.Series.Clear()

        Chart2.Series.Add(New Series("TIC"))
        Chart2.Series("TIC").IsValueShownAsLabel = False
        Chart2.Series("TIC").BorderWidth = 1
        Chart2.Series("TIC").ChartType = SeriesChartType.Column
        Chart2.Series("TIC").Color = Color.Green
        Chart2.Series("TIC").CustomProperties = "PixelPointWidth = 0.5"
        Chart2.ChartAreas(0).AxisX.LabelStyle.Enabled = False

        Chart2.ChartAreas(0).AxisX.MajorGrid.Enabled = False
        Chart2.ChartAreas(0).AxisX.MinorGrid.Enabled = False
        Chart2.ChartAreas(0).AxisY.MajorGrid.Enabled = False
        Chart2.ChartAreas(0).AxisY.MinorGrid.Enabled = False

        Chart2.Height = 250

        maxInt = 0
        For Each p As MSPoint In msData
            If ((p.getMass <= highMass) AndAlso (p.getMass >= lowMass)) Then
                If maxInt < p.getIntensity Then
                    maxInt = p.getIntensity
                End If

                If minInt > p.getIntensity Then
                    minInt = p.getIntensity
                End If
            End If
        Next


        For Each p As MSPoint In msData
            If ((p.getMass <= highMass) AndAlso (p.getMass >= lowMass)) Then

                targetPoint = Chart2.Series("TIC").Points.AddXY(p.getMass.ToString, p.getIntensity)
                PercentIntensity = (p.getIntensity - minInt) / (maxInt - minInt) * 100

                If (PercentIntensity > 20) Then
                    Chart2.Series("TIC").Points.Item(targetPoint).Label = p.getMass.ToString
                End If

            End If
        Next

        Chart2.DataBind()
        Return label

    End Function

    'UV toggle for the top graph
    Private Sub Button1_Click(sender As Object, e As EventArgs)


        If (Chart1.Series("UV254").Enabled = True) Then
            Chart1.Series("UV254").Enabled = False
            Chart1.Series("UV220").Enabled = False
            Chart2.Series("TIC").Points.Clear()
        Else
            Chart1.Series("UV254").Enabled = True
            Chart1.Series("UV220").Enabled = True
            Chart2.Series("TIC").Points.Clear()
        End If

    End Sub

    Private Function setupTopTable() As PdfPTable
        Dim table As PdfPTable = New PdfPTable(2)
        Try
            table.SetWidths(New Integer() {12, 10})
        Catch e As Exception

        End Try

        table.WidthPercentage = 95
        table.AddCell(Me.myCell(("Sample ID: " + Me.sampleid), 4, 5))
        table.AddCell(Me.myCell(("Date Acquired: " + Me.daterun), 4, 5))
        table.AddCell(Me.myCell(("Username: " + Me.username), 4, 5))
        table.AddCell(Me.myCell(("Date Reprocessed: " + Me.datereprocessed), 4, 5))
        table.AddCell(Me.myCell(("Comment: " + Me.comment), 4, 5))
        table.AddCell(Me.myCell(("Instrument: " + Me.instname), 4, 5))
        Return table
    End Function


    Public Overloads Function myCell(ByVal s As String, ByVal padding As Integer, ByVal leftpad As Integer) As PdfPCell

        Dim font As iTextSharp.text.Font = FontFactory.GetFont("Helvetica", 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)
        Dim cell As PdfPCell = New PdfPCell(New Phrase(s, font))
        cell.Padding = padding
        cell.PaddingLeft = leftpad
        cell.SetLeading(0, 1.2)
        cell.UseAscender = True
        cell.UseDescender = False
        Return cell
    End Function

    Public Function myHeaderCell(ByVal s As String, ByVal padding As Integer) As PdfPCell
        Dim font As iTextSharp.text.Font = FontFactory.GetFont("Helvetica", 14, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)
        Dim cell As PdfPCell = New PdfPCell(New Phrase(s, font))
        cell.Padding = padding
        cell.HorizontalAlignment = Element.ALIGN_RIGHT
        cell.Border = 0
        Return cell
    End Function

    Public Overloads Function myCell(ByVal img As iTextSharp.text.Image) As PdfPCell
        Dim cell As PdfPCell = New PdfPCell(img)
        cell.Border = 0
        Return cell
    End Function

    Public Function myText(ByVal s As String, ByVal fontsize As Integer) As Paragraph
        Dim font As iTextSharp.text.Font = FontFactory.GetFont("Helvetica", 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)
        Dim p As Paragraph = New Paragraph(s, font)
        Return p
    End Function

    Public Function CreateReport() As System.IO.MemoryStream
        Dim msReport As System.IO.MemoryStream = New System.IO.MemoryStream()
        Dim temp_stream As System.IO.MemoryStream = New System.IO.MemoryStream()
        Dim labeltext As String

        Dim pdfDoc As Document = New Document(PageSize.LETTER, 10.0F, 10.0F, 10.0F, 0F)
        pdfDoc.AddCreator("Incyte Corp.")
        pdfDoc.AddTitle("Reprocessed LCMS Data")
        iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDoc, msReport)

        pdfDoc.Open()
        pdfDoc.AddCreator("Incyte Corp.")
        pdfDoc.AddTitle("Reprocessed LCMS Data")

        'Convert the logo image into a cell
        Dim ms As New System.IO.MemoryStream
        Me.logo.Save(ms, Imaging.ImageFormat.Png)
        Dim bitmapBytes As Byte() = ms.ToArray
        Dim mypng As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(bitmapBytes)
        mypng.ScalePercent(11)

        'Create the logo table
        Dim table As PdfPTable = New PdfPTable(2)
        table.SetWidths(New Integer() {1, 1})
        table.WidthPercentage = 95

        'Add logo table to the top
        table.AddCell(Me.myCell(mypng))
        table.AddCell(Me.myHeaderCell(Me.sampleid, 1))
        pdfDoc.Add(table)

        'Add description table to the top
        Dim top As PdfPTable = Me.setupTopTable
        top.TableEvent = New ThickTableBorder
        top.SpacingAfter = 12
        pdfDoc.Add(top)
        'pdfDoc.Add(Me.getLC(writer, "TIC"))

        'Add Chart1 table 95% of the pagewidth
        Chart1.SaveImage(temp_stream, ChartImageFormat.Png)
        Dim chartImage As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(temp_stream.GetBuffer())
        Dim scaler As Double = PageSize.LETTER.Width / chartImage.Width() * 95
        chartImage.ScalePercent(scaler)
        pdfDoc.Add(chartImage)


        For Each description As String In Me.features

            temp_stream = New System.IO.MemoryStream()
            'Draw Chart2
            labeltext = DrawMS(description)

            'Add Chart2 label
            pdfDoc.Add(myText(labeltext, 10))

            'Add Chart2 table
            Chart2.SaveImage(temp_stream, ChartImageFormat.Png)
            chartImage = iTextSharp.text.Image.GetInstance(temp_stream.GetBuffer())
            chartImage.ScalePercent(scaler)

            pdfDoc.Add(chartImage)

        Next

        pdfDoc.Close()

        Return msReport

    End Function

    Private Function convScantoMin(ByVal s As String) As String
        If (s Is Nothing) Then
            Return ""
        End If

        Try
            Dim i As Integer = Integer.Parse(s)
            Dim d As Double = (Me.Model.getRetTime(i) / 60000)
            Return FormatNumber(d, 2)
        Catch ex As Exception
            Return ""
        End Try

    End Function

End Class


Public Class ThickTableBorder
    Implements IPdfPTableEvent

    Public Sub TableLayout(ByVal table As PdfPTable, ByVal widths As Single()(), ByVal heights As Single(), ByVal headerRows As Integer, ByVal rowStart As Integer, ByVal canvases As PdfContentByte()) Implements IPdfPTableEvent.TableLayout

        Dim width() As Single = widths(0)
        Dim x1 As Single = width(0)
        Dim x2 As Single = width(width.Length - 1)
        Dim y1 As Single = heights(0)
        Dim y2 As Single = heights(heights.Length - 1)
        Dim cb As PdfContentByte = canvases(PdfPTable.LINECANVAS)
        cb.Rectangle(x1, y1, (x2 - x1), (y2 - y1))
        cb.SetLineWidth(1.5!)
        cb.Stroke()
        cb.ResetRGBColorStroke()

    End Sub

End Class

