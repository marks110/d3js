Imports System.Drawing
Imports System.IO

Public Class Status
    Inherits System.Web.UI.Page

    Public icon_cog As String = "<i class=""fa fa-cog fa-spin"" style=""color:lime""></i>"
    Public icon_pause As String = "<i class=""fa fa-pause-circle fa-lg"" style=""color:black""></i>"
    Public icon_NA As String = "<i class=""fa fa-ban fa-lg"" style=""color:black""></i>"
    Public icon_NoConnection As String = "<i class=""fas fa-eye-slash fa-lg"" style=""color:black""></i>"
    Public icon_idle As String = "<i class=""fas fa-play"" style=""color:lime""></i>"
    Public icon_service As String = "<i class=""fas fa-wrench fa-lg"" style=""color:black""></i>"
    Public icon_available As String = "<i class=""fa fa-check-circle fa-lg"" style=""color:lime""></i>"



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim args As New DataSourceSelectArguments()
        Dim View As DataView = SqlDataSource2.Select(args)
        Dim dt As DataTable = View.ToTable()

        Load_LCMS_Row(TableRow1, "LCMS-1", dt)
        Load_LCMS_Row(TableRow2, "LCMS-2", dt)
        Load_LCMS_Row(TableRow3, "LCMS-3", dt)
        Load_LCMS_Row(TableRow4, "LCMS-4", dt)
        Load_LCMS_Row(TableRow5, "LCMS-5", dt)
        Load_LCMS_Row(TableRow6, "LCMS-6", dt)
        Load_LCMS_Row(TableRow7, "LCMS-7", dt)
        Load_LCMS_Row(TableRow8, "LCMS-8", dt)
        Load_LCMS_Row(TableRow9, "LCMS-11", dt)
        Load_LCMS_Row(TableRow10, "LCMS-12", dt)
        Load_LCMS_Row(TableRow11, "LCMS-13", dt)

        Load_Prep_Row(TableRow12, "PrepLCMS-1", dt)
        Load_Prep_Row(TableRow13, "PrepLCMS-2", dt)
        Load_Prep_Row(TableRow14, "PrepLCMS-3", dt)
        Load_Prep_Row(TableRow15, "PrepLCMS-4", dt)
        Load_Prep_Row(TableRow16, "PrepLCMS-5", dt)
        Load_Prep_Row(TableRow17, "PrepLCMS-6", dt)
        Load_Prep_Row(TableRow18, "PrepLCMS-7", dt)
        Load_Prep_Row(TableRow19, "PrepLCMS-8", dt)
        Load_Prep_Row(TableRow20, "PrepLCMS-9", dt)

        Label2.Text = Date.Now.ToString

    End Sub


    Private Sub ChangeRowFontColor(ByVal myRow As TableRow)

        For Each mycell As TableCell In myRow.Cells
            mycell.ForeColor = System.Drawing.Color.Black
            mycell.Font.Bold = True
        Next
    End Sub

    Private Sub Load_LCMS_Row(ByVal myRow As TableRow, ByVal InstName As String, ByVal dt As DataTable)
        Dim myrows() As DataRow = dt.Select("INSTRUMENT_NAME = '" & InstName & "'")
        Dim stat_text As String

        'Put font awesome icons according to availability
        'If Column or queue states report error convert availability to No - This is a second measure to detect errors
        If (myrows(0).Item(4).ToString.Contains("Error") Or myrows(0).Item(9).ToString.Contains("Error")) Then
            myRow.Cells.Item(1).Text = "No"
        Else
            myRow.Cells.Item(1).Text = myrows(0).Item(1).ToString
        End If

        If (myRow.Cells.Item(1).Text.Contains("Yes")) Then
            myRow.Cells.Item(1).Text = icon_available
        ElseIf (myRow.Cells.Item(1).Text.Contains("No")) Then
            myRow.Cells.Item(1).Text = icon_NA
            myRow.BackColor = System.Drawing.Color.Red
            ChangeRowFontColor(myRow)
        ElseIf (myRow.Cells.Item(1).Text.Contains("NC")) Then
            myRow.Cells.Item(1).Text = icon_NoConnection
            myRow.BackColor = System.Drawing.Color.Red
            ChangeRowFontColor(myRow)
        ElseIf (myRow.Cells.Item(1).Text.Contains("Svc")) Then
            myRow.Cells.Item(1).Text = icon_service
            myRow.BackColor = System.Drawing.Color.Yellow
            ChangeRowFontColor(myRow)
        End If

        'Put Estimated Time
        myRow.Cells.Item(2).Text = myrows(0).Item(2).ToString
        myRow.Cells.Item(3).Text = myrows(0).Item(3).ToString

        'Put Status if it is available
        stat_text = ""
        If (myRow.Cells.Item(1).Text = icon_available) Then
            Select Case myrows(0).Item(4).ToString
                Case "Waiting"
                    stat_text = icon_idle
                Case "WAITING"
                    stat_text = icon_idle
                Case "IN RUN"
                    stat_text = icon_cog
                Case "Running"
                    stat_text = icon_cog
                Case Else
                    stat_text = icon_idle
            End Select
        End If

        'If the machine is running put the jobname
        If stat_text.Contains("cog") Then
            myRow.Cells.Item(4).Text = myrows(0).Item(5).ToString
            myRow.Cells.Item(5).Text = myTruncate(myrows(0).Item(6).ToString)
        Else
            myRow.Cells.Item(4).Text = ""
            myRow.Cells.Item(5).Text = ""
        End If

        'Put two tabs between availability and status
        If (myRow.Cells.Item(1).Text = icon_NA) Then
            myRow.Cells.Item(1).Text = myRow.Cells.Item(1).Text
        Else
            myRow.Cells.Item(1).Text = myRow.Cells.Item(1).Text & "&emsp;&emsp;" & stat_text
        End If


    End Sub


    Private Sub Load_Prep_Row(ByVal myRow As TableRow, ByVal InstName As String, ByVal dt As DataTable)
        Dim myrows() As DataRow = dt.Select("INSTRUMENT_NAME = '" & InstName & "'")
        Dim temp() As String
        Dim stat_text As String

        'Put font awesome icons according to availability
        If (myrows(0).Item(1).ToString.Contains("Yes")) Then
            myRow.Cells.Item(1).Text = icon_available
        ElseIf (myrows(0).Item(1).ToString.Contains("Svc")) Then
            myRow.Cells.Item(1).Text = icon_service
            myRow.BackColor = System.Drawing.Color.Orange
            ChangeRowFontColor(myRow)
        ElseIf (myrows(0).Item(1).ToString.Contains("No")) Then
            myRow.Cells.Item(1).Text = icon_NA
            myRow.BackColor = System.Drawing.Color.Red
            ChangeRowFontColor(myRow)
        End If

        If (myrows(0).Item(1).ToString.Contains("PAUSED")) Then
            myRow.Cells.Item(1).Text = icon_pause
            myRow.BackColor = System.Drawing.Color.Yellow
            ChangeRowFontColor(myRow)
        End If


        'Put Estimated Time
        myRow.Cells.Item(2).Text = myrows(0).Item(2).ToString
        myRow.Cells.Item(3).Text = myrows(0).Item(3).ToString

        'Divide the Status from "-" signs to get rid of Yes
        temp = myrows(0).Item(1).ToString.Split(New Char() {"-"c}, 2)
        'Remove OK- to leave Running/Idle type description
        If (temp.Count > 1) Then
            stat_text = temp(1).Replace("OK-", "")
        Else
            stat_text = myrows(0).Item(4).ToString
        End If

        'If the machine is idle remove last jobname
        If (stat_text = "Idle") Then
            stat_text = icon_idle
            myRow.Cells.Item(4).Text = ""
            myRow.Cells.Item(5).Text = ""
        ElseIf (stat_text = "Running") Then
            stat_text = icon_cog
            myRow.Cells.Item(4).Text = myrows(0).Item(5).ToString
            myRow.Cells.Item(5).Text = myTruncate(myrows(0).Item(6).ToString)
        Else
            myRow.Cells.Item(4).Text = myrows(0).Item(5).ToString
            myRow.Cells.Item(5).Text = myTruncate(myrows(0).Item(6).ToString)
        End If

        'If it is in service mode Turn Text Service
        'If (myrows(0).Item(1).ToString.Contains("Svc")) Then
        '    stat_text = icon_service
        'End If

        'Put two tabs between availability and status
        If (myRow.Cells.Item(1).Text = icon_NA) Then
            myRow.Cells.Item(1).Text = myRow.Cells.Item(1).Text
        Else
            myRow.Cells.Item(1).Text = myRow.Cells.Item(1).Text & "&emsp;&emsp;" & stat_text
        End If


    End Sub


    Private Function myTruncate(ByVal input As String) As String
        Dim shortString As String

        If (input.Length > 8) Then
            shortString = input.Substring(0, 8)
        Else
            shortString = input
        End If

        Return shortString

    End Function

    Protected Sub Button1_Click(sender As Object, e As EventArgs)

    End Sub

    Protected Sub Button1_Click1(sender As Object, e As EventArgs) Handles Button1.Click
        Response.Redirect("status_full.aspx")
    End Sub
End Class