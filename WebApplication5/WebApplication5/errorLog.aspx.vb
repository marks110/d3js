Public Class errorLog
    Inherits System.Web.UI.Page

    Dim machines As Dictionary(Of String, String)

    Protected Sub loadInstruments()

        machines = New Dictionary(Of String, String)

        machines.Add("74", "LCMS-0")
        machines.Add("20", "LCMS-1")
        machines.Add("42", "LCMS-2")
        machines.Add("43", "LCMS-3")
        machines.Add("44", "LCMS-4")
        machines.Add("45", "LCMS-5")
        machines.Add("51", "LCMS-6")
        machines.Add("61", "LCMS-7")
        machines.Add("67", "LCMS-8")
        machines.Add("71", "LCMS-9")
        machines.Add("72", "LCMS-10")
        machines.Add("82", "LCMS-11")
        machines.Add("83", "LCMS-12")
        machines.Add("88", "LCMS-13")

        machines.Add("53", "Prep-1")
        machines.Add("52", "Prep-2")
        machines.Add("56", "Prep-3")
        machines.Add("55", "Prep-4")
        machines.Add("62", "Prep-5")
        machines.Add("66", "Prep-6")
        machines.Add("80", "Prep-7")
        machines.Add("79", "Prep-8")
        machines.Add("81", "Prep-9")

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        loadInstruments()

    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)

        'If the instrument id is a number than get the instrument name from the dictionary
        If e.Row.Cells.Count > 3 Then
            If Regex.IsMatch(e.Row.Cells(3).Text, "^[0-9 ]+$") Then
                e.Row.Cells(3).Text = machines.Item(e.Row.Cells(3).Text)
            End If
        End If


    End Sub


    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        For Each item As GridViewRow In GridView1.Rows

            If (TryCast(item.Cells(0).FindControl("cbSelect"), CheckBox)).Checked Then
                Dim dt As DateTime = DateTime.Parse(item.Cells(2).Text)
                SqlDataSource2.DeleteParameters(":ERROR_DATE").DefaultValue = dt
                SqlDataSource2.Delete()
            End If
        Next
        GridView1.DataBind()

    End Sub

    Protected Sub deleteRow(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)
        SqlDataSource2.DeleteCommand = "DELETE FROM INSTRUMENT_ERROR_LOG WHERE ERROR_DATE=:ERROR_DATE"
        SqlDataSource2.DeleteParameters.Add("ERROR_DATE", GridView1.DataKeys(e.RowIndex).Values("ERROR_DATE"))
        SqlDataSource2.Delete()
        'GridView1.DataBind()
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        SqlDataSource2.DeleteCommand = "DELETE FROM INSTRUMENT_ERROR_LOG"
        SqlDataSource2.Delete()
        GridView1.DataBind()
    End Sub

End Class