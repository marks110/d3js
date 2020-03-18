
Public Class Entries
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            If (System.Web.HttpContext.Current.Session("Layout") = "horizontal") Then
                Response.Redirect("entriesH.aspx")
            Else
                SqlDataSource1.SelectCommand = "SELECT UPPER(USERNAME) AS UNAME, SAMPLEID, DATERUN, INSTNAME, CUSTOM1, CUSTOM2, CUSTOM6, CUSTOM7 FROM ANALYTICAL_CC.ENTRIES WHERE (DATERUN >= SYSDATE - 1) ORDER BY ID DESC"
                GridView1.DataSource = SqlDataSource1
                GridView1.DataBind()
                iframePDFViewer.Visible = False
                LoadSessionOptions() ' Retrieve the search options from session

                'Dim testView As DataView = SqlDataSource1.Select(DataSourceSelectArguments.Empty)
                'testView.Table.DataSet.WriteXml("C:\TEMP\x.xml")
            End If

        End If
    End Sub

    Protected Sub LoadSessionOptions()

        'Populate the dropdown list items
        DropDownList1.DataBind()
        DropDownList2.DataBind()

        'select the indexes from session history
        DropDownList1.SelectedIndex = System.Web.HttpContext.Current.Session("DropDownList1")
        DropDownList2.SelectedIndex = System.Web.HttpContext.Current.Session("DropDownList2")
        TextBox1.Text = System.Web.HttpContext.Current.Session("TextBox1")

        'redo the searches
        If (DropDownList1.SelectedIndex > 0 Or DropDownList2.SelectedIndex > 0 Or TextBox1.Text.Length > 0) Then
            mySearch()
        End If

    End Sub
    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged

        Dim myPDFPath As String = GridView1.SelectedRow.Cells(8).Text
        Dim dataPath As String = GridView1.SelectedRow.Cells(7).Text

        If GridView1.SelectedIndex > -1 Then

            'Send the notebook name in case the user saves PDF from pdf save option
            iframePDFViewer.Src = "getPDF.ashx?path=" & myPDFPath & "&zoom=FitH" & "&filename=" & GridView1.SelectedRow.Cells(2).Text
            iframePDFViewer.Visible = True

            'Process.Start("file://C:\Program Files\Mestrelab Research S.L\MestReNova\MestReNova.exe", dataPath)
            'dataPath = dataPath.Replace("\", "\\")
            'Label1.Text = "<script type='text/javascript'>CopyToClipboard('" & dataPath & "');</script>"
            'dataPath = "file:" & myPDFPath.Replace("\", "\\")
            'Dim localPath As String = New Uri(dataPath).LocalPath
            'Dim data As String = System.IO.File.ReadAllText(localPath)
            'dataPath = "file:" & dataPath.Replace("\", "\\")

        Else
            iframePDFViewer.Visible = False
        End If

    End Sub

    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs)
        'Click a row to select
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."

        End If

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If GridView1.SelectedIndex > -1 Then

            Dim myPDFPath As String = GridView1.SelectedRow.Cells(8).Text
            Response.ContentType = "Application/pdf"
            Response.AppendHeader("Content-Disposition", "attachment; filename=" & GridView1.SelectedRow.Cells(2).Text & ".pdf")
            Response.TransmitFile(myPDFPath)

        End If

    End Sub

    Private Function GetInstType(ByVal inst_name As String) As String
        Dim mytype As String = "NA"

        If (inst_name = "LCMS-1" Or inst_name = "LCMS-4" Or inst_name = "LCMS-6" Or inst_name = "LCMS-7" Or inst_name = "LCMS-8" Or inst_name = "LCMS-12" Or inst_name = "SFCMS-1") Then
            mytype = "waters"
        ElseIf (inst_name = "LCMS-2" Or inst_name = "LCMS-3" Or inst_name = "LCMS-5" Or inst_name = "LCMS-9" Or inst_name = "LCMS-10" Or inst_name = "LCMS-11" Or inst_name = "LCMS-13") Then
            mytype = "agilent"
        ElseIf inst_name.Contains("Prep") Then
            mytype = "prep"
        End If

        Return mytype

    End Function
    Protected Sub Process_click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim dataPath As String = GridView1.SelectedRow.Cells(7).Text
        Dim instName As String = GridView1.SelectedRow.Cells(4).Text
        Dim queryString As String = "processdata.aspx"
        Dim instType As String = GetInstType(instName)

        System.Web.HttpContext.Current.Session("TYPE") = instType
        System.Web.HttpContext.Current.Session("RPT") = dataPath & "\data.rpt"
        System.Web.HttpContext.Current.Session("TXT") = dataPath & "\data.txt"
        System.Web.HttpContext.Current.Session("MSFILE") = dataPath & "\MSD1.MS"
        System.Web.HttpContext.Current.Session("UVFILE") = dataPath & "\ExData.txt"


        If instType = "prep" Or instType = "NA" Then
            Dim myscript As String = "alert('PrepLCMS Processing has not been implemented yet!');"
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", myscript, True)
        Else
            'Dim newWin As String = "window.open('" + queryString + "','_blank', 'toolbar=0, scrollbars=0, resizable=1, width=760, height=760, channelmode=0, left=100,top=100, menubar=0, titlebar=0, status=0').focus();"
            Dim newWin As String = "window.open('" + queryString + "','_blank', 'width=760, height=760, left=100,top=100').focus();"
            ClientScript.RegisterStartupScript(Me.GetType(), "pop", newWin, True)
        End If

    End Sub

    Protected Sub TextBox1_TextChanged(sender As Object, e As EventArgs) 'Handles TextBox1.TextChanged

        System.Web.HttpContext.Current.Session("TextBox1") = TextBox1.Text
        mySearch()

    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged
        System.Web.HttpContext.Current.Session("DropDownList1") = DropDownList1.SelectedIndex
        mySearch()
    End Sub

    Protected Sub DropDownList2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList2.SelectedIndexChanged
        System.Web.HttpContext.Current.Session("DropDownList2") = DropDownList2.SelectedIndex
        mySearch()
    End Sub

    Protected Sub txtDate1_TextChanged(sender As Object, e As EventArgs) Handles txtDate1.TextChanged
        mySearch()
    End Sub

    Protected Sub mySearch()

        Dim tempdate As DateTime

        GridView1.SelectedIndex = -1
        iframePDFViewer.Visible = False

        SqlDataSource1.SelectCommand = "SELECT UPPER(USERNAME) AS UNAME, SAMPLEID, DATERUN, INSTNAME, CUSTOM1, CUSTOM2, CUSTOM6, CUSTOM7 FROM ANALYTICAL_CC.ENTRIES "
        Dim WhereClause As New List(Of String)

        If DropDownList1.SelectedIndex > 0 Then
            WhereClause.Add(" UPPER(USERNAME) = '" & DropDownList1.SelectedValue & "'")
        End If

        If DropDownList2.SelectedIndex > 0 Then
            WhereClause.Add(" INSTNAME = '" & DropDownList2.SelectedValue & "'")
        End If

        If TextBox1.Text <> "" Then
            WhereClause.Add(" SAMPLEID LIKE '%" & TextBox1.Text & "%'")
        End If

        If DateTime.TryParse(txtDate1.Text, tempdate) Then
            WhereClause.Add(" TRUNC(DATERUN) = TO_DATE('" & txtDate1.Text & "', 'MM/DD/YYYY')")
        End If

        If (WhereClause.Count > 0) Then
            SqlDataSource1.SelectCommand = SqlDataSource1.SelectCommand & " WHERE "
            SqlDataSource1.SelectCommand = SqlDataSource1.SelectCommand & WhereClause.Item(0)

            If (WhereClause.Count > 1) Then
                For i As Integer = 1 To (WhereClause.Count - 1)
                    SqlDataSource1.SelectCommand = SqlDataSource1.SelectCommand & " AND " & WhereClause.Item(i)
                Next
            End If
        End If

        SqlDataSource1.SelectCommand = SqlDataSource1.SelectCommand & " ORDER BY DATERUN DESC FETCH FIRST 1000 ROWS ONLY"
        SqlDataSource1.Select(DataSourceSelectArguments.Empty)
        SqlDataSource1.DataBind()
        GridView1.DataSource = SqlDataSource1
        GridView1.DataBind()

    End Sub


    Protected Sub ChangeLayout_Click(sender As Object, e As EventArgs) Handles Button3.Click
        System.Web.HttpContext.Current.Session("Layout") = "horizontal"
        Response.Redirect("entriesH.aspx")
    End Sub

    Protected Sub Refresh_Click(sender As Object, e As EventArgs) Handles Button4.Click
        mySearch()
    End Sub

    Protected Sub Clear_Click(sender As Object, e As EventArgs) Handles Button5.Click
        System.Web.HttpContext.Current.Session("DropDownList1") = 0
        System.Web.HttpContext.Current.Session("DropDownList2") = 0
        System.Web.HttpContext.Current.Session("ListBox1") = 0
        System.Web.HttpContext.Current.Session("TextBox1") = ""

        DropDownList1.SelectedIndex = 0
        DropDownList2.SelectedIndex = 0
        txtDate1.Text = ""
        TextBox1.Text = ""
        mySearch()

    End Sub
End Class