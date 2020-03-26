Imports System.Drawing
Imports System.IO
Imports System.Web.Services
Imports Newtonsoft.Json

Public Class Processdata

    Inherits Page
    Public Shared Model As LCMSModel
    Public Shared massSpec As MassSpec
    Public Shared JsonChartData As New JSONData
    Public Shared mintime As Double
    Public Shared maxtime As Double
    Public Shared UpPoint As Point
    Public Shared DownPoint As Point

    'Following variables are used for Rectangle drawing over chart1 with mouse dragging
    Public Shared sDownPoint As Point = New Point(-1.0, -1.0)
    Public Shared sUpPoint As Point

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        'debugtxt.Text = System.Web.HttpContext.Current.Session("TXT")

    End Sub

    <WebMethod()>
    Public Shared Function getChartModelData(data As String)

        Dim rpt As String
        Dim txt As String
        Dim instrument_type As String
        Dim MSFile As String
        Dim UVFile As String



        'rpt = "\\ds\de\Labinst\Analytical_Archive\LCMSData\sysadmin\test-1_190120T1632_LCMS-1\test-1_52652.rpt"
        'txt = "\\ds\de\Labinst\Analytical_Archive\LCMSData\sysadmin\test-1_190120T1632_LCMS-1\test-1_52652.txt"

        instrument_type = System.Web.HttpContext.Current.Session("TYPE")


        If (instrument_type = "waters") Then
            rpt = System.Web.HttpContext.Current.Session("RPT")
            txt = System.Web.HttpContext.Current.Session("TXT")
            Model = WatersLCMSModelFactory.getModel(rpt, txt)
        ElseIf (instrument_type = "agilent") Then
            MSFile = System.Web.HttpContext.Current.Session("MSFILE")
            UVFile = System.Web.HttpContext.Current.Session("UVFILE")
            Model = AgilentLCMSModel.getModel(MSFile, UVFile)
        End If



        rpt = "C:\data\test1.rpt"
        txt = "C:\data\test1.txt"
        instrument_type = "waters"

        Model = WatersLCMSModelFactory.getModel(rpt, txt)


        mintime = (LCMSModel.getMinValueInt(Model.getRetTimes) / 60000)
        maxtime = (LCMSModel.getMaxValueInt(Model.getRetTimes) / 60000)
        massSpec = New MassSpec(Model.getMassSpecs, Model.getRetTimes)


        '==================UV220====================
        Dim PercentIntensity As New List(Of Double)
        Dim maxIntensityUV220 As Double = LCMSModel.getMaxValueDouble(Model.getUV220Int)
        Dim minIntensityUV220 As Double = LCMSModel.getMinValueDouble(Model.getUV220Int)

        For i As Integer = 0 To (Model.getUV220Int.Count - 1)

            PercentIntensity.Add((Model.getUV220Int.Item(i) - minIntensityUV220) / maxIntensityUV220 * 100)
        Next

        JsonChartData.PercentIntensityUV220 = PercentIntensity
        '===========================================

        '==================UV254====================
        Dim PercentIntensityUV254 As New List(Of Double)
        Dim maxIntensityUV254 As Double = LCMSModel.getMaxValueDouble(Model.getUV254Int)
        Dim minIntensityUV254 As Double = LCMSModel.getMinValueDouble(Model.getUV254Int)

        For i As Integer = 0 To (Model.getUV254Int.Count - 1)

            PercentIntensityUV254.Add((Model.getUV254Int.Item(i) - minIntensityUV254) / maxIntensityUV254 * 100)
        Next
        JsonChartData.PercentIntensityUV254 = PercentIntensityUV254
        '===========================================

        '====================TIC====================
        Dim PercentIntensityTIC As New List(Of Double)
        Dim RetTimes As New List(Of Double)
        Dim maxIntensityTIC As Double = LCMSModel.getMaxValueDouble(Model.getMSInt)
        Dim minIntensityTIC As Double = LCMSModel.getMinValueDouble(Model.getMSInt)
        JsonChartData.maxIntensityTIC = maxIntensityTIC

        For i As Integer = 0 To (Model.getMSInt.Count - 1)
            PercentIntensityTIC.Add((Model.getMSInt.Item(i) - minIntensityTIC) / maxIntensityTIC * 100)
            RetTimes.Add(Model.getRetTimes.Item(i) / 60000)
        Next
        JsonChartData.PercentIntensityTIC = PercentIntensityTIC
        JsonChartData.RetTimes = RetTimes
        '===========================================

        JsonChartData.UVretTimes = Model.getUVretTimes()

        Return JsonConvert.SerializeObject(JsonChartData)
    End Function

    Public Shared Function D3NearestX(mouseX As Double, left As Integer, width As Integer) As Integer
        Dim px As Double
        Dim px1 As Double
        Dim myX As Double
        Dim _x As Integer
        Dim i As Integer
        Dim PointCount As Integer


        PointCount = JsonChartData.PercentIntensityTIC.Count - 1

        myX = JsonChartData.UVretTimes(0)

        Dim dx As Double = width / (JsonChartData.RetTimes(PointCount) - JsonChartData.RetTimes(0))
        For i = 1 To PointCount
            px = (JsonChartData.RetTimes(i - 1) - JsonChartData.RetTimes(0)) * dx + left
            px1 = (JsonChartData.RetTimes(i) - JsonChartData.RetTimes(0)) * dx + left

            If (mouseX > px And mouseX < px1) Then
                myX = px
                _x = i - 1
            End If
        Next

        Return _x

    End Function

    <WebMethod()>
    Public Shared Function Chart1MouseUp(
                                        x1 As Integer, y1 As Integer, cx1 As Integer, cy1 As Integer,
                                        x2 As Integer, y2 As Integer, cx2 As Integer, cy2 As Integer,
                                        left As Integer, width As Integer)

        sDownPoint = New Point(cx1, cy1)
        DownPoint = New Point(x1, y1)

        sUpPoint = New Point(cx2, cy2)
        UpPoint = New Point(x2, y2)

        massSpec.setScans(D3NearestX(DownPoint.X, left, width), D3NearestX(UpPoint.X, left, width))
        massSpec.setSubs(D3NearestX(sDownPoint.X, left, width), D3NearestX(sUpPoint.X, left, width))
        Dim msData As List(Of MSPoint) = massSpec.getMS
        'Dim PercentIntensity As Double

        Dim maxInt As Double = -1
        Dim minInt As Double = -1
        Dim lowMass As Double = massSpec.getLowMass
        Dim highMass As Double = massSpec.getHighMass
        'Dim targetPoint As Int32

        maxInt = 0
        For Each p As MSPoint In msData
            If maxInt < p.getIntensity Then
                maxInt = p.getIntensity
            End If

            If minInt > p.getIntensity Then
                minInt = p.getIntensity
            End If
        Next

        Dim JsonTICData As New JSONDataTIC
        JsonTICData.msData = msData
        JsonTICData.highMass = highMass
        JsonTICData.lowMass = lowMass
        JsonTICData.minInt = minInt
        JsonTICData.maxInt = maxInt
        Return JsonConvert.SerializeObject(JsonTICData)
    End Function

    <WebMethod()>
    Public Shared Function Chart1MouseDown(x1 As Integer, y1 As Integer, cx1 As Integer, cy1 As Integer)
        'sDownPoint = New Point(cx1, cy1)
        'DownPoint = New Point(x1, y1)
        Return True
    End Function

    <WebMethod()>
    Public Shared Function drawChart3(mass As Double)
        Dim intens As List(Of Double) = Model.getChromOfMass(mass)
        Dim PercentIntensity As New List(Of Double)
        Dim RetTimes As New List(Of Double)
        Dim maxIntensity As Double = LCMSModel.getMaxValueDouble(intens)
        Dim minIntensity As Double = LCMSModel.getMinValueDouble(intens)

        For i As Integer = 0 To (Model.getMSInt.Count - 1)
            PercentIntensity.Add((intens.Item(i) - minIntensity) / maxIntensity * 100)
            RetTimes.Add(Model.getRetTimes.Item(i) / 60000)
        Next

        Dim JsonLCData As New JSONDataLC
        JsonLCData.PercentIntensity = PercentIntensity
        JsonLCData.RetTimes = RetTimes
        JsonLCData.maxIntensity = maxIntensity
        Return JsonConvert.SerializeObject(JsonLCData)
    End Function

    <WebMethod()>
    Public Shared Function btAddClick(count As Integer)
        Dim CurrentWindow As String
        Dim FeaturesString As String
        Dim left, width As Integer
        left = 70
        width = 650
        CurrentWindow = "Window" & count.ToString
        FeaturesString = CurrentWindow & ";" & D3NearestX(DownPoint.X, left, width) & ";" & D3NearestX(UpPoint.X, left, width) & ";" & D3NearestX(sDownPoint.X, left, width) & ";" & D3NearestX(sUpPoint.X, left, width) & ";100;900"
        Dim lavel As String
        lavel = getLabelText(FeaturesString)

        Return lavel
    End Function

    Public Shared Function getLabelText(ByVal description As String) As String

        Dim token() As String = description.Split(";")
        Dim label As String = ("MS @ " + convScantoMin(token(1)))

        'Range
        If (Not token(2).Equals("-1") AndAlso Not token(2).Equals(token(1))) Then
            label = (label + ("-" + convScantoMin(token(2))))
        End If

        'Baseline subtract
        If Not token(3).Equals("-1") Then
            label = (label + (" minus " + convScantoMin(token(3))))
        End If
        If (Not token(4).Equals("-1") AndAlso Not token(4).Equals(token(3))) Then
            label = (label + ("-" + convScantoMin(token(4))))
        End If
        label = (label + " min")
        Return label

    End Function

    Public Shared Function convScantoMin(ByVal s As String) As String
        If (s Is Nothing) Then
            Return ""
        End If

        Try
            Dim i As Integer = Integer.Parse(s)
            Dim d As Double = (Model.getRetTime(i) / 60000)
            Return FormatNumber(d, 2)
        Catch ex As Exception
            Return ""
        End Try

    End Function
End Class