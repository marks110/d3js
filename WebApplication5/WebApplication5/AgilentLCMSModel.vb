Imports System.IO
Public Class AgilentLCMSModel
    Public Shared Function getModel(ByVal msFile As String, ByVal uvFile As String)
        Try
            Dim data() As Byte = getBytesFromFile(msFile)
            Dim summaryOffset As Integer = getAgilentInt(data, 260)
            Dim dataOffset As Integer = getAgilentInt(data, 264)
            Dim numScans As Integer = getAgilentInt(data, 278)

            Dim model As LCMSModel = getMSData(data, (dataOffset - 1) * 2, numScans)
            If Not uvFile Is Nothing Then
                addUVData(model, uvFile)
            End If

            Return model
        Catch e As Exception
            Console.WriteLine(e.ToString())
        End Try

        Return Nothing
    End Function

    Private Shared Function getBytesFromFile(ByVal msFile As String) As Byte()

        Dim bytes() As Byte = File.ReadAllBytes(msFile)

        'Dim is As InputStream = New FileInputStream(File)
        '' Get the size of the file
        'Dim length As Long = File.length
        'If (length > Integer.MAX_VALUE) Then
        '    ' File is too large
        'End If

        '' Create the byte array to hold the data
        'Dim bytes() As Byte = New Byte((CType(length, Integer)) - 1) {}
        '' Read in the bytes
        'Dim offset As Integer = 0
        'Dim numRead As Integer = 0

        'While ((offset < bytes.Length) And (numRead = Is .read(bytes, offset, (bytes.Length - offset)) >= 0))
        '    offset = (offset + numRead)

        'End While

        '' Ensure all the bytes have been read in
        'If (offset < bytes.Length) Then
        '    Throw New IOException(("Could not completely read file " + File.getName))
        'End If

        '' Close the input stream and return bytes
        'Is.close
        Return bytes
    End Function

    Private Shared Function getAgilentInt(ByVal data() As Byte, ByVal offset As Integer) As Integer
        Dim tmp() As Byte = New Byte(3) {}
        Dim i As Integer = 0
        Do While (i < 4)
            tmp(i) = data(offset + i)
            i = i + 1
        Loop

        Return ((tmp(0) And &HFF) << 24) Or ((tmp(1) And &HFF) << 16) Or ((tmp(2) And &HFF) << 8) Or (tmp(3) And &HFF)
    End Function

    Private Shared Function getAgilentShort(ByVal data() As Byte, ByVal offset As Integer) As Integer
        Dim tmp() As Byte = New Byte(1) {}
        Dim i As Integer = 0
        Do While (i < 2)
            tmp(i) = data(offset + i)
            i = i + 1
        Loop

        Return (CInt(tmp(0)) << 8) Or (tmp(1) And &HFF)
    End Function

    Private Shared Function getMSData(ByVal data() As Byte, ByVal offset As Integer, ByVal numScans As Integer) As LCMSModel
        Dim model As LCMSModel = New LCMSModel(numScans)
        Dim i As Integer = 0
        Do While (i < numScans)
            offset = offset + 2
            ' Skip 2 bytes
            Dim retTime As Integer = getAgilentInt(data, offset)
            offset = offset + 10
            ' Skip 6 bytes
            Dim numPeaks As Integer = getAgilentShort(data, offset)
            offset = offset + 6

            Dim a As List(Of MSPoint) = New List(Of MSPoint)
            Dim j As Integer = 0
            Do While (j < numPeaks)
                Dim p As MSPoint = New MSPoint(getAgilentMZ(getAgilentShort(data, offset)), getAgilentAbundance(getAgilentShort(data, (offset + 2))))
                offset = offset + 4
                a.Add(p)
                j = j + 1
            Loop

            offset = offset + 6
            ' Skip 6 bytes
            Dim totalSig As Integer = getAgilentInt(data, offset)
            offset = offset + 4
            model.add(retTime, a, totalSig)
            i = i + 1
        Loop

        Return model
    End Function

    Public Shared Function getAgilentMZ(ByVal mz As Integer) As Single
        ' given int from AgilentShort
        mz <<= 1
        mz = CType((CType(mz, UInteger) >> 1), Integer)
        mz = mz And &HFFFF
        Return CType(mz, Single) / 20.0F
    End Function

    Public Shared Function getAgilentAbundance(ByVal exponent As Integer) As Single
        ' given int from AgilentShort
        Dim base As Integer = exponent
        exponent = CType((CType(exponent, UInteger) >> 14), Integer)
        exponent = exponent And 3
        base = base And &H3FFF
        Return CType((CType(base, Double) * Math.Pow(8D, exponent)), Single)
    End Function

    Private Overloads Shared Sub addUVData(ByVal m As LCMSModel, ByVal uvFile As String)
        Dim uv220 As List(Of Double) = m.getUV220Int
        Dim uv254 As List(Of Double) = m.getUV254Int
        Try
            Using uv As New StreamReader(uvFile, True)

                'Dim in As BufferedReader = New BufferedReader(New FileReader(uv))
                Dim s As String
                Do
                    s = uv.ReadLine()
                    If s Is Nothing Then
                        Exit Do
                    End If

                    If s.StartsWith("1      ") Then
                        Exit Do
                    End If
                Loop

                addUVData(s, uv220, uv254)
                Do
                    s = uv.ReadLine()
                    If s Is Nothing Then
                        Exit Do
                    End If

                    If (s.Length > 40) Then
                        addUVData(s, uv220, uv254)
                    End If
                Loop
            End Using
        Catch ex As Exception
            Console.WriteLine(ex.ToString())
        End Try
    End Sub

    Private Overloads Shared Sub addUVData(ByVal s As String, ByVal uv220 As List(Of Double), ByVal uv254 As List(Of Double))
        Try
            Dim d220 As Double
            Dim d254 As Double
            Double.TryParse(s.Substring(7, 17).Trim, d220)
            Double.TryParse(s.Substring(24, 17).Trim, d254)
            uv220.Add(d220)
            uv254.Add(d254)
        Catch e As Exception
            Console.WriteLine(e.ToString())
        End Try

    End Sub
End Class

