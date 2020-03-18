
'Agilent stores msInt as integers, but Waters values go above the maximum integer value, so msInt changed to doubles
Public Class JSONData
    Public Property PercentIntensityUV220 As List(Of Double)
    Public Property PercentIntensityUV254 As List(Of Double)
    Public Property PercentIntensityTIC As List(Of Double)
    Public Property UVretTimes As List(Of Double)
    Public Property RetTimes As List(Of Double)
    Public Property maxIntensityTIC As Double

End Class

Public Class JSONDataTIC
    Public Property msData As List(Of MSPoint)
    Public Property lowMass As Double
    Public Property highMass As Double
    Public Property maxInt As Double = -1
    Public Property minInt As Double = -1
End Class

Public Class JSONDataLC
    Public Property PercentIntensity As List(Of Double)
    Public Property RetTimes As List(Of Double)
    Public Property maxIntensity As Double

End Class