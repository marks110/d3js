Imports System.Web
Imports System.Web.Services
Imports System
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Drawing.Text
Imports System.IO
Imports System.Data

Public Class getPDF
    Implements System.Web.IHttpHandler

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        CreateImage(context)

    End Sub

    Private Sub CreateImage(ByVal context As HttpContext)
        ' If context.Request.QueryString("pdf") IsNot Nothing Then

        Dim strPdf As String = context.Request.QueryString("path").ToString()
        Dim fileName As String = context.Request.QueryString("filename").ToString()
        'Dim strPdf As String = System.Web.HttpContext.Current.Session("PDF")

        Dim fs As FileStream = Nothing
        Dim br As BinaryReader = Nothing
        Dim data As Byte() = Nothing

        Try
            fs = New FileStream(strPdf, FileMode.Open, FileAccess.Read, FileShare.Read)
            br = New BinaryReader(fs, System.Text.Encoding.Default)
            data = New Byte(Convert.ToInt32(fs.Length) - 1) {}
            br.Read(data, 0, data.Length)
            context.Response.Clear()
            context.Response.ContentType = "application/pdf"
            context.Response.AddHeader("Content-Disposition", "inline; filename=" & fileName & ".pdf")
            context.Response.AddHeader("content-length", data.Length.ToString())
            context.Response.BinaryWrite(data)


            context.Response.End()
        Catch ex As Exception
            context.Response.Write(ex.Message)
        Finally
            fs.Close()
            fs.Dispose()
            br.Close()
            data = Nothing
        End Try

    End Sub


    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class




