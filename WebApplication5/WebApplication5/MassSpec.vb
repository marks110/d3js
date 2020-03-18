Public Class MassSpec

    Public subtLow As Integer
    Public subtHigh As Integer

    Public scanLow As Integer
    Public scanHigh As Integer

    Public zoomLow As Double
    Public zoomHigh As Double

    Private massSpec As List(Of List(Of MSPoint))
    Private retTimes As List(Of Integer)

    Private minTime As Double
    Private maxTime As Double
    Private minMass As Double
    Private maxMass As Double

    Public Sub New(ByVal massSpec As List(Of List(Of MSPoint)), ByVal retTimes As List(Of Integer))
        MyBase.New
        Me.massSpec = massSpec
        Me.retTimes = retTimes
        Me.minTime = LCMSModel.getMinValueInt(Me.retTimes)
        Me.maxTime = LCMSModel.getMaxValueInt(Me.retTimes)
        Me.minMass = LCMSModel.getMinMass(Me.massSpec)
        Me.maxMass = LCMSModel.getMaxMass(Me.massSpec)
        Me.resetScans()
        Me.resetZoom()
    End Sub

    Public Sub setScan(ByVal scan As Integer)
        Me.scanLow = scan
        Me.scanHigh = scan
        Me.resetZoom()
    End Sub

    Public Sub setScans(ByVal scanLow As Integer, ByVal scanHigh As Integer)
        Me.scanLow = scanLow
        Me.scanHigh = scanHigh
        Me.resetZoom()
    End Sub

    Public Sub setSub(ByVal scan As Integer)
        Me.subtLow = scan
        Me.subtHigh = scan
        Me.resetZoom()
    End Sub

    Public Sub setSubs(ByVal subtLow As Integer, ByVal subtHigh As Integer)
        Me.subtLow = subtLow
        Me.subtHigh = subtHigh
        Me.resetZoom()
    End Sub

    Public Sub setZooms(ByVal zoomLow As Double, ByVal zoomHigh As Double)
        Me.zoomLow = Me.zoomLow
        Me.zoomHigh = Me.zoomHigh
    End Sub

    Public Function getMS() As List(Of MSPoint)
        Dim map As Dictionary(Of Double, Double) = New Dictionary(Of Double, Double)
        Dim i As Integer

        If (Me.scanLow <> -1) Then
            ' add the ms to the map
            i = Me.scanLow
            Do While (i <= Me.scanHigh)
                Me.add(map, Me.massSpec.Item(i), (1 / ((Me.scanHigh - Me.scanLow) + 1)))
                i = (i + 1)
            Loop

        End If

        If (Me.subtLow <> -1) Then
            ' subtract the background
            i = Me.subtLow
            Do While (i <= Me.subtHigh)
                Me.subtract(map, Me.massSpec.Item(i), (1 / ((Me.subtHigh - Me.subtLow) + 1)))
                i = (i + 1)
            Loop

        End If

        Dim globalmax As Double = 0.0
        Dim a As List(Of MSPoint) = New List(Of MSPoint)

        For Each kvp As KeyValuePair(Of Double, Double) In map
            Dim mass As Double = kvp.Key
            Dim intensity As Double = kvp.Value
            If (intensity > globalmax) Then
                globalmax = intensity
            End If

            a.Add(New MSPoint(mass, intensity))

        Next


        'Dim mass As Double = CType(iterator.next, Double)

        ''convert map to arraylist
        'Dim a As List(Of MSPoint) = New List(Of MSPoint)
        'Dim iterator As Iterator(Of Double) = map.keySet.iterator
        'Dim globalmax As Double = 0!
        '' find global max intensity to get rid of "grass"

        'While iterator.hasNext
        '    Dim mass As Double = CType(iterator.next, Double)
        '    Dim intensity As Double = map.Item(mass)
        '    If (intensity > globalmax) Then
        '        globalmax = intensity
        '    End If

        '    a.Add(New MSPoint(mass, intensity))

        'End While

        'Collections.sort(a)

        a = a.OrderBy(Function(x) x.getMass).ToList()
        ' ensure arraylist is sorted by mass

        ' Bin results within 0.2 mass units
        i = 2
        Do While (i < (a.Count - 2))
            Dim f As Double = a.Item(i).getMass
            Dim maxInt As Double = a.Item(i).getIntensity
            Dim pIndex As Integer = 0
            If ((f - a.Item((i - 1)).getMass) <= 0.2) Then
                If (a.Item((i - 1)).getIntensity > maxInt) Then
                    maxInt = a.Item((i - 1)).getIntensity
                    pIndex = (i - 1)
                End If

            End If

            If ((f - a.Item((i - 2)).getMass) <= 0.2) Then
                If (a.Item((i - 2)).getIntensity > maxInt) Then
                    maxInt = a.Item((i - 2)).getIntensity
                    pIndex = (i - 2)
                End If

            End If

            If ((a.Item((i + 1)).getMass - f) <= 0.2) Then
                If (a.Item((i + 1)).getIntensity > maxInt) Then
                    maxInt = a.Item((i + 1)).getIntensity
                    pIndex = (i + 1)
                End If

            End If

            If ((a.Item((i + 2)).getMass - f) <= 0.2) Then
                If (a.Item((i + 2)).getIntensity > maxInt) Then
                    maxInt = a.Item((i + 2)).getIntensity
                    pIndex = (i + 2)
                End If

            End If

            If (pIndex > 0) Then
                a.Item(pIndex).addIntensity(a.Item(i).getIntensity)
                a.Item(i).setIntensity(0!)
            End If

            i = (i + 1)
        Loop

        '*/
        ' Remove negative or zero intensities
        Dim b As List(Of MSPoint) = New List(Of MSPoint)
        For Each p As MSPoint In a
            If (p.getIntensity > (globalmax * 0.002)) Then
                b.Add(p)
            End If

            ' value can be changed
        Next
        Return b
    End Function

    Public Function getLowMass() As Double
        Return Me.zoomLow
    End Function

    Public Function getHighMass() As Double
        Return Me.zoomHigh
    End Function

    ' returns percentage along lcms of this scan
    Public Function getBlueX1() As Double
        If (Me.scanLow = -1) Then
            Return -1
        End If

        Dim time As Double = Me.retTimes.Item(Me.scanLow)
        Return ((time - Me.minTime) / (Me.maxTime - Me.minTime))
    End Function

    Public Function getBlueX2() As Double
        If (Me.scanHigh = -1) Then
            Return -1
        End If

        Dim time As Double = Me.retTimes.Item(Me.scanHigh)
        Return ((time - Me.minTime) / (Me.maxTime - Me.minTime))
    End Function

    Public Function getRedX1() As Double
        If (Me.subtLow = -1) Then
            Return -1
        End If

        Dim time As Double = Me.retTimes.Item(Me.subtLow)
        Return ((time - Me.minTime) / (Me.maxTime - Me.minTime))
    End Function

    Public Function getRedX2() As Double
        If (Me.subtHigh = -1) Then
            Return -1
        End If

        Dim time As Double = Me.retTimes.Item(Me.subtHigh)
        Return ((time - Me.minTime) / (Me.maxTime - Me.minTime))
    End Function

    Private Sub resetScans()
        Me.subtLow = -1
        Me.subtHigh = -1
        Me.scanLow = -1
        Me.scanHigh = -1
    End Sub

    Public Sub resetZoom()
        Me.zoomLow = Me.minMass
        Me.zoomHigh = Me.maxMass
    End Sub

    Public Sub zoomMS(ByVal left As Double, ByVal right As Double)
        Dim size As Double = (Me.zoomHigh - Me.zoomLow)
        Dim low As Double = Me.zoomLow
        If (left < right) Then
            Me.zoomLow = (low + (size * left))
            Me.zoomHigh = (low + (size * right))
        ElseIf (right > left) Then
            Me.zoomLow = (low + (size * right))
            Me.zoomHigh = (low + (size * left))
        End If

    End Sub

    Private Sub add(ByVal map As Dictionary(Of Double, Double), ByVal a As List(Of MSPoint), ByVal fract As Double)
        Dim f As Double

        For Each p As MSPoint In a
            If map.ContainsKey(p.getMass) Then
                f = map.Item(p.getMass)
                f = f + (p.getIntensity * fract)
                map.Item(p.getMass) = f
            Else
                f = 0.0
                f = f + (p.getIntensity * fract)
                map.Add(p.getMass, f)
            End If
        Next
    End Sub

    Private Sub subtract_new(ByVal map As Dictionary(Of Double, Double), ByVal a As List(Of MSPoint), ByVal fract As Double)
        Dim f As Double

        For Each p As MSPoint In a
            If map.ContainsKey(p.getMass) Then
                f = map.Item(p.getMass)
            Else
                f = 0.0
            End If

            f = f - (p.getIntensity * fract)
            map(p.getMass) = f
        Next
    End Sub

    Private Sub subtract(ByVal map As Dictionary(Of Double, Double), ByVal a As List(Of MSPoint), ByVal fract As Double)
        Dim f As Double

        For Each p As MSPoint In a
            If map.ContainsKey(p.getMass) Then
                f = map.Item(p.getMass)
                f = f - (p.getIntensity * fract)
                If (f < 0) Then
                    f = 0!
                End If
                map(p.getMass) = f
            End If
        Next
    End Sub


End Class





