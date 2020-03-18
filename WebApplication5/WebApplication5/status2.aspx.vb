Public Class Status2
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub


    Protected Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        'GridView1.DataSource = SqlDataSource2
        GridView1.DataBind()
        Label1.Text = "Last Updated: " & Date.Now

        'For all datagrid rows 
        'if the update date is more than 5 mints probably network or machine is down
        'color the row red
    End Sub


End Class