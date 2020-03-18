
Imports System.IO

Public Class WatersLCMSModelFactory

    Public Shared Function getModel(ByVal rpt As String, ByVal txt As String) As LCMSModel
        Dim lcms As LCMSModel = New LCMSModel
        Try
            WatersLCMSModelFactory.readRPT(rpt, lcms)
            WatersLCMSModelFactory.readTXT(txt, lcms)
        Catch e As Exception
            Debug.Print(e.ToString)
            Return Nothing
        End Try

        Return lcms
    End Function

    Private Shared Sub readRPT(ByVal rpt As String, ByVal lcms As LCMSModel)

        Dim br As StreamReader = New StreamReader(rpt)

        Try
            Dim line As String = br.readLine

            While (Not (line) Is Nothing)
                If (line.Equals("Description" & vbTab & "MS ES+ :TIC") OrElse line.Equals("Description" & vbTab & "MS ES- :TIC")) Then

                    While ((Not (line) Is Nothing) AndAlso Not line.StartsWith("MaxIntensity"))
                        line = br.readLine

                    End While

                    Dim max As Double
                    Double.TryParse(line.Substring(13), max)

                    While ((Not (line) Is Nothing) AndAlso Not line.Equals("{"))
                        line = br.readLine

                    End While

                    line = br.ReadLine

                    While ((Not (line) Is Nothing) AndAlso Not line.Equals("}"))
                        Dim tokens() As String = line.Split(vbTab)
                        If (tokens.Length = 2) Then
                            Dim time As Double
                            Double.TryParse(tokens(0), time)
                            Dim percent As Double
                            Double.TryParse(tokens(1), percent)
                            lcms.add(Int(time * 60000.0), max * percent)

                        End If

                        line = br.readLine

                    End While

                End If

                If (line.Equals("Description" & vbTab & "DAD: 220") OrElse line.Equals("Description" & vbTab & "DAD: 254")) Then
                    Dim uv As New List(Of Double)

                    If line.EndsWith("220") Then
                        uv = lcms.getUV220Int
                    Else
                        uv = lcms.getUV254Int
                    End If


                    While ((Not (line) Is Nothing) AndAlso Not line.StartsWith("MaxIntensity"))
                        line = br.ReadLine

                    End While

                    Dim max As Double
                    Double.TryParse(line.Substring(13), max)

                    While ((Not (line) Is Nothing) AndAlso Not line.Equals("{"))
                        line = br.ReadLine

                    End While

                    line = br.ReadLine

                    While ((Not (line) Is Nothing) AndAlso Not line.Equals("}"))
                        Dim tokens() As String = line.Split(vbTab)
                        If (tokens.Length = 2) Then
                            Dim percent As Double
                            Double.TryParse(tokens(1), percent)
                            uv.Add((max * percent))

                        End If

                        line = br.ReadLine

                    End While

                End If

                line = br.readLine

            End While

        Finally
            br.close
        End Try

    End Sub

    Private Shared Sub readTXT(ByVal txt As String, ByVal lcms As LCMSModel)
        Dim br As StreamReader = New StreamReader(txt)
        Try
            Dim line As String = br.ReadLine

            While (Not (line) Is Nothing)
                If line.Equals("FUNCTION 1") Then
                    line = br.ReadLine
                    line = br.ReadLine

                    While line.StartsWith("Scan")
                        Dim a As New List(Of MSPoint)
                        line = br.ReadLine
                        line = br.ReadLine
                        line = br.ReadLine

                        While Not line.Equals("")
                            Dim tokens() As String = line.Split(vbTab)
                            If (tokens.Length = 2) Then
                                Dim mass As Double
                                Double.TryParse(tokens(0), mass)
                                Dim intensity As Double
                                Double.TryParse(tokens(1), intensity)
                                a.Add(New MSPoint(mass, intensity))
                            End If

                            line = br.ReadLine

                        End While

                        lcms.addMS(a)
                        line = br.ReadLine

                    End While

                End If

                line = br.ReadLine

            End While

        Finally
            br.Close()
        End Try

    End Sub
End Class


