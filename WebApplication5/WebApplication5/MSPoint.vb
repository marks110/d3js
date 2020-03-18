
Public Class MSPoint

    Public Property mass As Double
    Public Property intensity As Double

    Public Sub New()
        MyBase.New

    End Sub

    Public Sub New(ByVal mass As Double, ByVal intensity As Double)
        MyBase.New
        Me.mass = mass
        Me.intensity = intensity
    End Sub

    Public Function getMass() As Double
        Return Me.mass
    End Function

    Public Function getIntensity() As Double
        Return Me.intensity
    End Function

    Public Sub setIntensity(ByVal f As Double)
        Me.intensity = f
    End Sub

    Public Sub addIntensity(ByVal f As Double)
        Me.intensity = (Me.intensity + f)
    End Sub

    Public Function compareTo(ByVal p2 As Object) As Integer
        Dim m As Double = CType(p2, MSPoint).getMass
        'if (mass == m) return 0;
        If (Me.mass < m) Then
            Return -1
        End If

        Return 1
    End Function
End Class


