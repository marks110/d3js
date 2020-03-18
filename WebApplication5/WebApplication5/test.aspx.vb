Imports System.Drawing
Imports System.Web.UI.DataVisualization.Charting
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class test
    Inherits System.Web.UI.Page

    Private model As LCMSModel

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        DrawWaters()
        'DrawAgilent()

    End Sub

    Private Sub DrawWaters()
        Dim rpt As String
        Dim txt As String
        Dim watersfolder As String
        Dim watersjobname As String

        watersjobname = "12379-80_8807"
        watersfolder = "C:\LCMS\temp\"
        rpt = watersfolder & watersjobname & "\" & watersjobname & ".rpt"
        txt = watersfolder & watersjobname & "\" & watersjobname & ".txt"
        model = WatersLCMSModelFactory.getModel(rpt, txt)

        Chart1.ChartAreas(0).AxisX.LabelStyle.Format = "#.#"
        Chart1.ChartAreas(0).AxisY.LabelStyle.Format = "{0}%"
        Chart1.ChartAreas(0).AxisY.Maximum = 100

        drawUV220(Chart1)
        drawUV254(Chart1)
        drawTIC(Chart1)

        Chart1.DataBind()



    End Sub

    Private Sub DrawAgilent()
        Dim UVFile As String
        Dim MSFile As String
        Dim agilentfolder As String
        Dim agilentjobname As String

        agilentjobname = "12747-97-2269868.D"
        agilentfolder = "C:\LCMS\temp\"
        MSFile = agilentfolder & agilentjobname & "\MSD1.MS"
        UVFile = agilentfolder & agilentjobname & "\ExData.txt"
        model = AgilentLCMSModel.getModel(MSFile, UVFile)


        Chart2.ChartAreas(0).AxisX.LabelStyle.Format = "#.#"
        Chart2.ChartAreas(0).AxisY.LabelStyle.Format = "{0}%"
        Chart2.ChartAreas(0).AxisY.Maximum = 100

        drawUV220(Chart2)
        drawUV254(Chart2)
        drawTIC(Chart2)

        Chart2.DataBind()


    End Sub

    Private Sub drawUV220(ByVal myChart As Chart)

        Dim PercentIntensity As New List(Of Double)
        Dim maxIntensity As Double = LCMSModel.getMaxValueDouble(model.getUV220Int)


        For i As Integer = 0 To (Me.model.getUV220Int.Count - 1)
            PercentIntensity.Add(Me.model.getUV220Int.Item(i) / maxIntensity * 100)
        Next

        myChart.Series.Add(New Series("UV220"))
        myChart.Series("UV220").IsValueShownAsLabel = False
        myChart.Series("UV220").BorderWidth = 1
        myChart.Series("UV220").ChartType = SeriesChartType.Line
        myChart.Series("UV220").Points.DataBindXY(Me.model.getUVretTimes(), PercentIntensity)
        myChart.Series("UV220").Color = Color.Blue



    End Sub

    Private Sub drawUV254(ByVal myChart As Chart)

        Dim PercentIntensity As New List(Of Double)
        Dim maxIntensity As Double = LCMSModel.getMaxValueDouble(model.getUV254Int)


        For i As Integer = 0 To (Me.model.getUV254Int.Count - 1)
            PercentIntensity.Add(Me.model.getUV254Int.Item(i) / maxIntensity * 100)
        Next

        myChart.Series.Add(New Series("UV254"))
        myChart.Series("UV254").IsValueShownAsLabel = False
        myChart.Series("UV254").BorderWidth = 1
        myChart.Series("UV254").ChartType = SeriesChartType.Line
        myChart.Series("UV254").Points.DataBindXY(Me.model.getUVretTimes(), PercentIntensity)
        myChart.Series("UV254").Color = Color.Red



    End Sub

    Private Sub drawTIC(ByVal myChart As Chart)

        Dim PercentIntensity As New List(Of Double)
        Dim RetTimes As New List(Of Double)
        Dim maxIntensity As Double = LCMSModel.getMaxValueDouble(model.getMSInt)
        Dim minIntensity As Double = LCMSModel.getMinValueDouble(model.getMSInt)


        For i As Integer = 0 To (Me.model.getMSInt.Count - 1)
            PercentIntensity.Add((Me.model.getMSInt.Item(i) - minIntensity) / maxIntensity * 100)
            RetTimes.Add(Me.model.getRetTimes.Item(i) / 60000)
        Next

        myChart.Series.Add(New Series("TIC"))
        myChart.Series("TIC").IsValueShownAsLabel = False
        myChart.Series("TIC").BorderWidth = 1
        myChart.Series("TIC").ChartType = SeriesChartType.Line
        myChart.Series("TIC").Points.DataBindXY(RetTimes, PercentIntensity)
        myChart.Series("TIC").Color = Color.Green

    End Sub

    Private Shared Function conv_x(ByVal perc As Double, ByVal left As Double, ByVal right As Double) As Double
        Dim l As Double = (right - left)
        Dim d As Double = (perc * l)
        Return (left + d)
    End Function

    Private Overloads Shared Function conv_y(ByVal y As Integer, ByVal top As Double, ByVal bottom As Double, ByVal minMSInt As Double, ByVal maxMSInt As Double) As Double
        Dim l As Double = (bottom - top)
        Dim s As Double = (maxMSInt - minMSInt)
        Dim d As Double = (y - minMSInt) / s * l
        Return (bottom - d)
    End Function

    Private Overloads Shared Function conv_y(ByVal y As Double, ByVal top As Double, ByVal bottom As Double, ByVal minUV As Double, ByVal maxUV As Double) As Double
        Dim l As Double = (bottom - top)
        Dim s As Double = (maxUV - minUV)
        Dim d As Double = (y - minUV) / s * l
        Return (bottom - d)
    End Function

    Protected Sub Chart1_Click(ByVal sender As Object, ByVal e As ImageMapEventArgs) Handles Chart1.Click

        MsgBox(e.PostBackValue.ToString)
        Chart1.ChartAreas(0).AxisX.PixelPositionToValue(e.PostBackValue)

    End Sub

    Protected Sub Button1_Click1(sender As Object, e As EventArgs) Handles Button1.Click

        Dim pdfDoc As Document = New Document(PageSize.LETTER, 10.0F, 10.0F, 10.0F, 0F)
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream)

        Dim msReport As System.IO.MemoryStream = New System.IO.MemoryStream()
        iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDoc, msReport)
        pdfDoc.Open()

        Dim stream As System.IO.MemoryStream = New System.IO.MemoryStream()
        Dim fntTableFontHdr As iTextSharp.text.Font = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK)
        Dim fntTableFont As iTextSharp.text.Font = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)
        Dim strReportName As String = "myPdf" & DateTime.Now.Ticks & ".pdf"


        Dim myTable As New PdfPTable(3)
        myTable.WidthPercentage = 100 ' Table size is set to 100% of the page
        myTable.HorizontalAlignment = 0 'Left aLign
        myTable.SpacingAfter = 10
        Dim sglTblHdWidths(2) As Single
        sglTblHdWidths(0) = 15
        sglTblHdWidths(1) = 200
        sglTblHdWidths(2) = 385
        myTable.SetWidths(sglTblHdWidths) ' Set the column widths on table creation. Unlike HTML cells cannot be sized.

        Dim CellOneHdr As New PdfPCell(New Phrase(" ", fntTableFontHdr))
        myTable.AddCell(CellOneHdr)
        Dim CellTwoHdr As New PdfPCell(New Phrase("cell 2 Hdr", fntTableFontHdr))
        myTable.AddCell(CellTwoHdr)
        Dim CellTreeHdr As New PdfPCell(New Phrase("cell 3 Hdr", fntTableFontHdr))
        myTable.AddCell(CellTreeHdr)

        Dim CellOne As New PdfPCell(New Phrase("R1 C1", fntTableFont))
        CellOne.Rotation = -90
        myTable.AddCell(CellOne)
        Dim CellTwo As New PdfPCell(New Phrase("R1 C2", fntTableFont))
        myTable.AddCell(CellTwo)
        Dim CellTree As New PdfPCell(New Phrase("R1 C3", fntTableFont))
        myTable.AddCell(CellTree)

        Dim CellOneR2 As New PdfPCell(New Phrase("R2 C1", fntTableFont))
        CellOneR2.Rotation = -90
        myTable.AddCell(CellOneR2)
        Dim CellTwoR2 As New PdfPCell(New Phrase("R2 C2", fntTableFont))
        myTable.AddCell(CellTwoR2)
        Dim CellTreeR2 As New PdfPCell(New Phrase("R2 C3", fntTableFont))
        myTable.AddCell(CellTreeR2)


        pdfDoc.Add(myTable)

        Chart1.SaveImage(stream, ChartImageFormat.Png)
        Dim chartImage As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(stream.GetBuffer())
        Dim scaler As Double = PageSize.LETTER.Width / chartImage.Width() * 100
        chartImage.ScalePercent(scaler)
        pdfDoc.Add(chartImage)

        Chart2.SaveImage(stream, ChartImageFormat.Png)
        chartImage = iTextSharp.text.Image.GetInstance(stream.GetBuffer())
        chartImage.ScalePercent(scaler)
        pdfDoc.Add(chartImage)

        pdfDoc.Close()

        Response.Clear()
        Response.AppendHeader("content-disposition", "attachment;filename=Chart.pdf")
        Response.ContentType = "application/pdf"
        Response.BinaryWrite(msReport.ToArray())
        Response.Flush()
        Response.End()



    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim features As New List(Of String)

        features.Add("Window1;100;150;-1;-1;100;200")
        features.Add("Window2;100;500;-1;-1;100;200")
        features.Add("Window3;100;500;-1;-1;100;200")

        Dim x As New PDF_report(Chart1, Chart2, features)
        Dim msReport As System.IO.MemoryStream

        x.sampleid = "23423"
        x.daterun = "02/03/2020"
        x.username = "sysadmin"
        x.datereprocessed = Date.Now.ToString
        x.comment = "blank"
        x.instname = "LCMS-1"

        'Create a PDF report
        msReport = x.CreateReport()

        'Send back the created PDF as a response
        Response.Clear()
        Response.AppendHeader("content-disposition", "attachment;filename=Chart.pdf")
        Response.ContentType = "application/pdf"
        Response.BinaryWrite(msReport.ToArray())
        Response.Flush()
        Response.End()
    End Sub
End Class