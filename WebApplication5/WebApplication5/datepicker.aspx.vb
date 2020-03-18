Imports System.Globalization

Public Class datepicker
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged

        If DropDownList1.SelectedIndex = 3 Then
            txtDate2.Visible = True
        Else
            txtDate2.Visible = False
        End If

    End Sub

    Protected Sub txtDate1_TextChanged(sender As Object, e As EventArgs) Handles txtDate1.TextChanged
        Dim dt As DateTime = DateTime.ParseExact(txtDate1.Text, "mm/dd/yyyy", CultureInfo.InvariantCulture)
        mySearch()

    End Sub

    Protected Sub mySearch()


        SqlDataSource1.SelectCommand = "SELECT UPPER(USERNAME) AS UNAME, SAMPLEID, DATERUN, INSTNAME, CUSTOM1, CUSTOM2, CUSTOM6, CUSTOM7 FROM ANALYTICAL_CC.ENTRIES "
        Dim WhereClause As New List(Of String)


        If txtDate1.Text.Count > 0 Then
            If DropDownList1.SelectedIndex = 0 Then
                WhereClause.Add(" TRUNC(DATERUN) = TO_DATE('" & txtDate1.Text & "', 'MM/DD/YYYY')")
            ElseIf DropDownList1.SelectedIndex = 1 Then
                WhereClause.Add(" TRUNC(DATERUN) >= TO_DATE('" & txtDate1.Text & "', 'MM/DD/YYYY')")
            ElseIf DropDownList1.SelectedIndex = 2 Then
                WhereClause.Add(" TRUNC(DATERUN) <= TO_DATE('" & txtDate1.Text & "', 'MM/DD/YYYY')")
            End If
        End If

        If txtDate1.Text.Count > 0 And txtDate2.Text.Count > 0 Then
            WhereClause.Add(" TRUNC(DATERUN) >= TO_DATE('" & txtDate1.Text & "', 'MM/DD/YYYY')")
            WhereClause.Add(" TRUNC(DATERUN) <= TO_DATE('" & txtDate2.Text & "', 'MM/DD/YYYY')")
        End If

        'If txtDate1.Text.Count > 0 Then
        '    WhereClause.Add(" DATERUN >= TO_DATE('" & txtDate1.Text & "', 'MM/DD/YYYY')")
        'End If

        'If txtDate2.Text.Count > 0 Then
        '    WhereClause.Add(" DATERUN <= TO_DATE('" & txtDate2.Text & "', 'MM/DD/YYYY')")
        'End If



        If (WhereClause.Count > 0) Then
            SqlDataSource1.SelectCommand = SqlDataSource1.SelectCommand & " WHERE "
            SqlDataSource1.SelectCommand = SqlDataSource1.SelectCommand & WhereClause.Item(0)

            If (WhereClause.Count > 1) Then
                For i As Integer = 1 To (WhereClause.Count - 1)
                    SqlDataSource1.SelectCommand = SqlDataSource1.SelectCommand & " AND " & WhereClause.Item(i)
                Next
            End If
        End If

        SqlDataSource1.SelectCommand = SqlDataSource1.SelectCommand & " ORDER BY DATERUN DESC FETCH FIRST 500 ROWS ONLY"
        SqlDataSource1.Select(DataSourceSelectArguments.Empty)
        SqlDataSource1.DataBind()

        Dim dt As DataTable = CType(SqlDataSource1.Select(DataSourceSelectArguments.Empty), DataView).Table
        dt.AsDataView

    End Sub
End Class