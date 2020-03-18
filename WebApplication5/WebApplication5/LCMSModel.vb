
'Agilent stores msInt as integers, but Waters values go above the maximum integer value, so msInt changed to doubles
Public Class LCMSModel

    Public Property numScans As Integer = 0

    Public Property retTimes As New List(Of Integer)

    Public Property massSpec As New List(Of List(Of MSPoint))

    Public Property msInt As New List(Of Double)

    Public Property uv220Int As New List(Of Double)

    Public Property uv254Int As New List(Of Double)

    Public Sub New()
        MyBase.New
        Me.numScans = Me.numScans
    End Sub

    Public Sub New(ByVal numScans As Integer)
        MyBase.New
        Me.numScans = Me.numScans
    End Sub

    Public Overloads Sub add(ByVal retTime As Integer, ByVal ms As List(Of MSPoint), ByVal ms_int As Integer)
        ' Used by agilent
        Me.retTimes.Add(retTime)
        Me.massSpec.Add(ms)
        Me.msInt.Add(CType(ms_int, Double))
    End Sub

    Public Overloads Sub add(ByVal retTime As Integer, ByVal ms_int As Double)
        ' Used by waters
        Me.retTimes.Add(retTime)
        Me.msInt.Add(ms_int)
        Me.numScans = (Me.numScans + 1)
    End Sub

    Public Sub addMS(ByVal ms As List(Of MSPoint))
        ' Used by waters
        Me.massSpec.Add(ms)
    End Sub

    Public Function getNumScans() As Integer
        Return Me.numScans
    End Function

    Public Function getRetTimes() As List(Of Integer)
        Return Me.retTimes
    End Function

    Public Function getUVretTimes() As List(Of Double)

        Dim UVretTimes As New List(Of Double)
        Dim interval As Double = (Me.getRetTimes.Max - Me.getRetTimes.Min) / Me.getRetTimes.Count / 2
        Dim x As Double

        Dim intervalx As Double

        If Me.retTimes.Count > 0 Then
            If Me.uv220Int.Count > 0 Then

                intervalx = Me.getRetTimes.Max / Me.uv220Int.Count / 60000

                For i As Integer = 0 To (Me.uv220Int.Count - 1)
                    UVretTimes.Add(intervalx * i)
                Next

            End If
        End If

        Return UVretTimes

    End Function

    Public Function getMSInt() As List(Of Double)
        Return Me.msInt
    End Function

    Public Function getUV220Int() As List(Of Double)
        Return Me.uv220Int
    End Function

    Public Function getUV254Int() As List(Of Double)
        Return Me.uv254Int
    End Function

    Public Function getMassSpec(ByVal scan As Integer) As List(Of MSPoint)
        Return Me.massSpec.Item(scan)
    End Function

    Public Function getMassSpecs() As List(Of List(Of MSPoint))
        Return Me.massSpec
    End Function

    Public Function getRetTime(ByVal scan As Integer) As Integer
        Return Me.retTimes.Item(scan)
    End Function

    Public Shared Function getMinValueInt(ByVal a As List(Of Integer)) As Integer
        If a Is Nothing OrElse a.Count = 0 Then
            Return 0
        End If

        Dim min As Integer = a.Item(0)
        For Each i As Integer In a
            If (i < min) Then
                min = i
            End If

        Next
        Return min
    End Function

    Public Shared Function getMinValueDouble(ByVal a As List(Of Double)) As Double
        If a Is Nothing OrElse a.Count = 0 Then
            Return 0
        End If

        Dim min As Double = a.Item(0)
        For Each d As Double In a
            If (d < min) Then
                min = d
            End If

        Next
        Return min
    End Function

    Public Shared Function getMaxValueInt(ByVal a As List(Of Integer)) As Integer
        If a Is Nothing OrElse a.Count = 0 Then
            Return 0
        End If

        Dim max As Integer = a.Item(0)
        For Each i As Integer In a
            If (i > max) Then
                max = i
            End If

        Next
        Return max
    End Function

    Public Shared Function getMaxValueDouble(ByVal a As List(Of Double)) As Double
        If a Is Nothing OrElse a.Count = 0 Then
            Return 0
        End If

        Dim max As Double = a.Item(0)
        For Each d As Double In a
            If (d > max) Then
                max = d
            End If

        Next
        Return max
    End Function

    Public Shared Function getMinInt(ByVal a As List(Of MSPoint)) As Double
        If a Is Nothing OrElse a.Count = 0 Then
            Return 0
        End If

        Dim min As Double = a.Item(0).getIntensity
        For Each p As MSPoint In a
            If (p.getIntensity < min) Then
                min = p.getIntensity
            End If

        Next
        Return min
    End Function

    Public Shared Function getMaxInt(ByVal a As List(Of MSPoint)) As Double
        If a Is Nothing OrElse a.Count = 0 Then
            Return 0
        End If

        Dim max As Double = a.Item(0).getIntensity
        For Each p As MSPoint In a
            If (p.getIntensity > max) Then
                max = p.getIntensity
            End If

        Next
        Return max
    End Function

    Public Shared Function getMinMass(ByVal ms As List(Of List(Of MSPoint))) As Double
        Dim min As Double = 1000

        If ms Is Nothing OrElse ms.Count = 0 Then
            Return 0
        End If

        For Each a As List(Of MSPoint) In ms
            For Each p As MSPoint In a
                If (p.getMass < min) Then
                    min = p.getMass
                End If

            Next
        Next
        min = (min - (min Mod 50))
        ' round down to the nearest 50
        If (min > 999) Then
            min = 100
        End If

        Return min
    End Function

    Public Shared Function getMaxMass(ByVal ms As List(Of List(Of MSPoint))) As Double
        Dim max As Double = 0

        If ms Is Nothing OrElse ms.Count = 0 Then
            Return 0
        End If

        For Each a As List(Of MSPoint) In ms
            For Each p As MSPoint In a
                If (p.getMass > max) Then
                    max = p.getMass
                End If

            Next
        Next
        If ((max Mod 50) > 0.1) Then
            max = (max + (50 - (max Mod 50)))
        End If

        ' round up to the nearest 50
        If (max = 0) Then
            max = 900
        End If

        Return max
    End Function

    ' Build chromatogram pulling out mass from LCMS - allows for +/- 0.5m/z
    Public Function getChromOfMass(ByVal mass As Double) As List(Of Double)
        Dim a As New List(Of Double)
        Dim i As Integer = 0
        Do While (i < Me.massSpec.Count)
            Dim temp As List(Of MSPoint) = Me.massSpec.Item(i)
            Dim f As Double = 0
            For Each p As MSPoint In temp
                If ((p.getMass > (mass - 0.51)) And (p.getMass < (mass + 0.51))) Then
                    f = (f + p.getIntensity)
                End If

            Next
            a.Add(CType(f, Double))
            i = (i + 1)
        Loop

        Return a
    End Function
End Class