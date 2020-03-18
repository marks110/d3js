
Public Class PDFReport

    Private hm As Dictionary(Of String, String)
    Private bfont As BaseFont = Nothing
    Private logo As Image = Nothing
    Private context As ServletContext
    Private model As LCMSModel
    Private features As List(Of String)
    Private mintime As Double
    Private maxtime As Double
    Private massSpec As MassSpec


    'final static Color UV220_COLOR = new Color(0, 170, 0);
    'final static Color UV254_COLOR = new Color(255, 140, 0);
    Private Shared UV220_COLOR As Color = New Color(0, 85, 0)
    Private Shared UV254_COLOR As Color = New Color(192, 105, 0)

    Public Sub New(ByVal mappings As Dictionary(Of String, String), ByVal features As List(Of String), ByVal context As ServletContext)
        MyBase.New
        Me.hm = mappings
        Me.features = Me.features
        Me.context = Me.context
        Try
            Me.logo = Image.getInstance(Me.context.getResource("/resources/corp_logo.png"))
            Me.logo.scalePercent(11)
        Catch e As Exception
            e.printStackTrace
        End Try

        Try
            Me.bfont = BaseFont.createFont(BaseFont.HELVETICA, BaseFont.WINANSI, True)
        Catch e As Exception
            e.printStackTrace
        End Try

        Dim path As String = Me.hm.get("filename1")
        If path.toLowerCase.endsWith(".pdf") Then
            Dim f As File = New File(path)
            path = f.getParent
        End If

        Dim reprocessor As String = Me.hm.get("reprocessor")
        If reprocessor.Equals("1") Then
            ' Agilent
            Dim msFile As File = New File((path + "/MSD1.MS"))
            If Not msFile.exists Then
                msFile = New File((path + "/DATA.MS"))
            End If

            Dim uvFile As File = New File((path + "/ExData.txt"))
            If Not uvFile.exists Then
                uvFile = Nothing
            End If

            If Not msFile.exists Then
                ' try backup location (filename2)
                path = Me.hm.get("filename2")
                If path.toLowerCase.endsWith(".pdf") Then
                    Dim f As File = New File(path)
                    path = f.getParent
                End If

                msFile = New File((path + "/MSD1.MS"))
                If Not msFile.exists Then
                    msFile = New File((path + "/DATA.MS"))
                End If

                uvFile = New File((path + "/ExData.txt"))
                If Not uvFile.exists Then
                    uvFile = Nothing
                End If

            End If

            Me.model = AgilentLCMSModelFactory.getModel(msFile, uvFile)
        Else
            ' Waters
            Dim rpt As File = Nothing
            Dim txt As File = Nothing
            Dim parent As File = New File(path)
            If ((Not (parent) Is Nothing) AndAlso parent.exists) Then
                For Each child As String In parent.list
                    If child.toLowerCase.endsWith(".rpt") Then
                        rpt = New File((parent.getAbsolutePath + ("/" + child)))
                        txt = New File((path + ("/" + (parent.getName + ".txt"))))
                    End If

                Next
            End If

            If (rpt Is Nothing) Then
                ' try backup location (filename2)
                path = Me.hm.get("filename2")
                If path.toLowerCase.endsWith(".pdf") Then
                    Dim f As File = New File(path)
                    path = f.getParent
                End If

                parent = New File(path)
                If ((Not (parent) Is Nothing) _
                            AndAlso parent.exists) Then
                    For Each child As String In parent.list
                        If child.toLowerCase.endsWith(".rpt") Then
                            rpt = New File((parent.getAbsolutePath + ("/" + child)))
                            txt = New File((path + ("/" _
                                            + (parent.getName + ".txt"))))
                        End If

                    Next
                End If

            End If

            Me.model = WatersLCMSModelFactory.getModel(rpt, txt)
        End If

        Me.mintime = (LCMSModel.getMinValueInt(Me.model.getRetTimes) / 60000)
        Me.maxtime = (LCMSModel.getMaxValueInt(Me.model.getRetTimes) / 60000)
        Me.massSpec = New MassSpec(Me.model.getMassSpecs, Me.model.getRetTimes)
    End Sub

    Public Sub create(ByVal out As OutputStream)
        Try
            Dim document As Document = New Document(PageSize.LETTER, 30, 30, 90, 55)
            ' left, right, top, bottom - margins
            Dim writer As PdfWriter = PdfWriter.getInstance(document, Unknown)
            writer.setPageEvent(New EndPage)
            document.addCreator("Incyte Corp.")
            document.addTitle("Reprocessed LCMS Data")
            document.open
            Dim top As PdfPTable = Me.setupTopTable
            top.setTableEvent(New ThickTableBorder)
            top.setSpacingAfter(12)
            document.add(top)
            document.add(Me.getLC(writer, "TIC"))
            If (Me.model.getUV220Int.size > 0) Then
                document.add(Me.getLC(writer, "UV220"))
            End If

            If (Me.model.getUV254Int.size > 0) Then
                document.add(Me.getLC(writer, "UV254"))
            End If

            For Each s As String In Me.features
                ' chromatograms first
                If s.StartsWith("CH;") Then
                    document.add(Me.getLC(writer, s))
                End If

            Next
            For Each s As String In Me.features
                ' spectra second
                If s.StartsWith("MS;") Then
                    document.add(Me.getMS(writer, s))
                End If

            Next
            document.close
        Catch e As Exception
            e.printStackTrace
        End Try

    End Sub

    Private Function setupTopTable() As PdfPTable
        Dim table As PdfPTable = New PdfPTable(2)
        Try
            table.setWidths(New Integer() {12, 10})
        Catch e As Exception

        End Try

        table.setWidthPercentage(95)
        table.addCell(Me.myCell("Sample ID: " + Me.sampleid), 4, 5)
        table.addCell(Me.myCell("Date Acquired: " + Me.daterun), 4, 5)
        table.addCell(Me.myCell("Username: " + Me.username), 4, 5)
        table.addCell(Me.myCell("Date Reprocessed: " + Me.datereprocessed), 4, 5)
        table.addCell(Me.myCell("Comment: " + Me.comment), 4, 5)
        table.addCell(Me.myCell("Instrument: " + Me.instname), 4, 5)
        Return table
    End Function

    Private Function getLC(ByVal writer As PdfWriter, ByVal description As String) As Image
        Dim width As Integer = 527
        Dim height As Integer = 145
        Dim cb As PdfContentByte = writer.getDirectContent
        Dim tp As PdfTemplate = cb.createTemplate(width, height)
        Dim g2d As Graphics2D = tp.createGraphics(width, height, New DefaultFontMapper)
        Dim r2d As Rectangle2D.Double = New Rectangle2D.Double(30, 20, (width - 30), (height - 54))
        Me.drawLCOutline(g2d, r2d, Me.mintime, Me.maxtime)
        g2d.setFont(New java.awt.Font("Arial Unicode MS", java.awt.Font.BOLD, 11))
        Dim label As String = description
        If description.StartsWith("CH;") Then
            label = ("m/z = " _
                        + (description.Substring(3) + " � 0.5"))
        End If

        g2d.drawString(label, 0!, 10.0!)
        If description.Equals("TIC") Then
            Me.drawTIC(g2d, r2d)
        End If

        If description.Equals("UV220") Then
            Me.drawUV220(g2d, r2d)
        End If

        If description.Equals("UV254") Then
            Me.drawUV254(g2d, r2d)
        End If

        If description.StartsWith("CH;") Then
            Dim tokens() As String = description.Split(";")
            Try
                Dim f As Single = Float.parseFloat(tokens(1))
                Me.drawLC(g2d, r2d, f)
            Catch e As Exception
                e.printStackTrace
            End Try

        End If

        g2d.dispose
        Dim i As Image = Nothing
        Try
            i = Image.getInstance(tp)
        Catch e As Exception

        End Try

        'i.setBorder(Image.BOX);
        'i.setBorderColor(new Color(0xFF, 0x00, 0x00));
        'i.setBorderWidth(2);
        Return i
    End Function

    Private Sub drawTIC(ByVal g2d As Graphics2D, ByVal r As Rectangle2D.Double)
        Dim left As Double = r.getX
        Dim top As Double = r.getY
        Dim right As Double = (left + r.getWidth)
        Dim bottom As Double = (top + r.getHeight)
        Dim minMSInt As Double = LCMSModel.getMinValueDouble(Me.model.getMSInt)
        Dim maxMSInt As Double = LCMSModel.getMaxValueDouble(Me.model.getMSInt)
        Dim y2 As Double
        Dim y1 As Double
        Dim intens As ArrayList(Of Double) = Me.model.getMSInt
        y1 = intens.get(0)
        Dim i As Integer = 1
        Do While (i < intens.size)
            y2 = intens.get(i)
            g2d.draw(New Line2D.Double(PDFReport.conv_x((CType((i - 1), Double) / intens.size), left, right), PDFReport.conv_y(y1, top, bottom, minMSInt, maxMSInt), PDFReport.conv_x((CType(i, Double) / intens.size), left, right), PDFReport.conv_y(y2, top, bottom, minMSInt, maxMSInt)))
            y1 = y2
            i = (i + 1)
        Loop

        PDFReport.labelMaxIntensity(g2d, r, maxMSInt, "Max: ")
    End Sub

    Private Sub drawUV220(ByVal g2d As Graphics2D, ByVal r As Rectangle2D.Double)
        Dim left As Double = r.getX
        Dim top As Double = r.getY
        Dim right As Double = (left + r.getWidth)
        Dim bottom As Double = (top + r.getHeight)
        Dim uv220 As ArrayList(Of Double) = Me.model.getUV220Int
        Dim minUV As Double = LCMSModel.getMinValueDouble(uv220)
        Dim maxUV As Double = LCMSModel.getMaxValueDouble(uv220)
        Dim y2 As Double
        Dim y1 As Double
        g2d.setColor(UV220_COLOR)
        y1 = uv220.get(0)
        Dim i As Integer = 1
        Do While (i < uv220.size)
            y2 = uv220.get(i)
            g2d.draw(New Line2D.Double(PDFReport.conv_x((CType((i - 1), Double) / uv220.size), left, right), PDFReport.conv_y(y1, top, bottom, minUV, maxUV), PDFReport.conv_x((CType(i, Double) / uv220.size), left, right), PDFReport.conv_y(y2, top, bottom, minUV, maxUV)))
            y1 = y2
            i = (i + 1)
        Loop

        PDFReport.labelMaxIntensity(g2d, r, maxUV, "UV220: ")
    End Sub

    Private Sub drawUV254(ByVal g2d As Graphics2D, ByVal r As Rectangle2D.Double)
        Dim left As Double = r.getX
        Dim top As Double = r.getY
        Dim right As Double = (left + r.getWidth)
        Dim bottom As Double = (top + r.getHeight)
        Dim uv254 As ArrayList(Of Double) = Me.model.getUV254Int
        Dim minUV As Double = LCMSModel.getMinValueDouble(uv254)
        Dim maxUV As Double = LCMSModel.getMaxValueDouble(uv254)
        Dim y2 As Double
        Dim y1 As Double
        g2d.setColor(UV254_COLOR)
        y1 = uv254.get(0)
        Dim i As Integer = 1
        Do While (i < uv254.size)
            y2 = uv254.get(i)
            g2d.draw(New Line2D.Double(PDFReport.conv_x((CType((i - 1), Double) / uv254.size), left, right), PDFReport.conv_y(y1, top, bottom, minUV, maxUV), PDFReport.conv_x((CType(i, Double) / uv254.size), left, right), PDFReport.conv_y(y2, top, bottom, minUV, maxUV)))
            y1 = y2
            i = (i + 1)
        Loop

        PDFReport.labelMaxIntensity(g2d, r, maxUV, "UV254: ")
    End Sub

    Private Sub drawLC(ByVal g2d As Graphics2D, ByVal r As Rectangle2D.Double, ByVal mass As Single)
        Dim left As Double = r.getX
        Dim top As Double = r.getY
        Dim right As Double = (left + r.getWidth)
        Dim bottom As Double = (top + r.getHeight)
        Dim intens As ArrayList(Of Double) = Me.model.getChromOfMass(mass)
        Dim minMSInt As Double = LCMSModel.getMinValueDouble(intens)
        Dim maxMSInt As Double = LCMSModel.getMaxValueDouble(intens)
        Dim y2 As Double
        Dim y1 As Double
        y1 = intens.get(0)
        Dim i As Integer = 1
        Do While (i < intens.size)
            y2 = intens.get(i)
            g2d.draw(New Line2D.Double(PDFReport.conv_x((CType((i - 1), Double) / intens.size), left, right), PDFReport.conv_y(y1, top, bottom, minMSInt, maxMSInt), PDFReport.conv_x((CType(i, Double) / intens.size), left, right), PDFReport.conv_y(y2, top, bottom, minMSInt, maxMSInt)))
            y1 = y2
            i = (i + 1)
        Loop

        Dim max As Double = LCMSModel.getMaxValueDouble(intens)
        PDFReport.labelMaxIntensity(g2d, r, max, "Max: ")
    End Sub

    Private Function getMS(ByVal writer As PdfWriter, ByVal description As String) As Image
        Dim width As Integer = 527
        Dim height As Integer = 145
        Dim cb As PdfContentByte = writer.getDirectContent
        Dim tp As PdfTemplate = cb.createTemplate(width, height)
        Dim g2d As Graphics2D = tp.createGraphics(width, height, New DefaultFontMapper)
        Dim r2d As Rectangle2D.Double = New Rectangle2D.Double(30, 20, (width - 30), (height - 54))
        Try
            Dim token() As String = description.Split(";")
            Dim label As String = ("MS @ " + Me.convScantoMin(token(1)))
            If (Not token(2).Equals("-1") _
                        AndAlso Not token(2).Equals(token(1))) Then
                label = (label + ("-" + Me.convScantoMin(token(2))))
            End If

            If Not token(3).Equals("-1") Then
                label = (label + (" minus " + Me.convScantoMin(token(3))))
            End If

            If (Not token(4).Equals("-1") _
                        AndAlso Not token(4).Equals(token(3))) Then
                label = (label + ("-" + Me.convScantoMin(token(4))))
            End If

            label = (label + " min")
            Me.drawMSOutline(g2d, r2d, Double.parseDouble(token(5)), Double.parseDouble(token(6)))
            g2d.setFont(New java.awt.Font("Arial Unicode MS", java.awt.Font.BOLD, 11))
            g2d.drawString(label, 0!, 10.0!)

            'setScans sums from token1 to token2
            'setSubs  sums from token3 to token4
            'setZooms zooms to token5 to token6
            'Dim msData As ArrayList(Of MSPoint) = Me.massSpec.getMS
            Me.massSpec.setScans(Integer.parseInt(token(1)), Integer.parseInt(token(2)))
            Me.massSpec.setSubs(Integer.parseInt(token(3)), Integer.parseInt(token(4)))
            Me.massSpec.setZooms(Double.parseDouble(token(5)), Double.parseDouble(token(6)))
            Me.drawMS(g2d, r2d)
        Catch e As Exception
            e.printStackTrace
        End Try

        g2d.dispose
        Dim i As Image = Nothing
        Try
            i = Image.getInstance(tp)
        Catch e As Exception

        End Try

        'i.setBorder(Image.BOX);
        'i.setBorderColor(new Color(0xFF, 0x00, 0x00));
        'i.setBorderWidth(2);
        Return i
    End Function

    Private Sub drawMS(ByVal g2d As Graphics2D, ByVal r As Rectangle2D.Double)
        Dim left As Double = r.getX
        Dim top As Double = r.getY
        Dim right As Double = (left + r.getWidth)
        Dim bottom As Double = (top + r.getHeight)
        Dim height As Double = r.getHeight
        Dim lowMass As Double = Me.massSpec.getLowMass
        Dim highMass As Double = Me.massSpec.getHighMass
        Dim msData As ArrayList(Of MSPoint) = Me.massSpec.getMS
        Dim maxInt As Single = LCMSModel.getMaxInt(msData)
        Dim minInt As Single = LCMSModel.getMinInt(msData)
        ' Draw MS lines
        Dim oldStroke As Stroke = g2d.getStroke
        g2d.setStroke(New BasicStroke(0.55!))
        For Each p As MSPoint In msData
            If ((p.getMass <= highMass) _
                        AndAlso (p.getMass >= lowMass)) Then
                Dim percY As Double = ((p.getIntensity - minInt) _
                            / (maxInt - minInt))
                ' calculate percent height of intensity line (and normalize intensity)
                g2d.draw(New Line2D.Double(PDFReport.position(((p.getMass - lowMass) _
                                        / (highMass - lowMass)), left, right), bottom, PDFReport.position(((p.getMass - lowMass) _
                                        / (highMass - lowMass)), left, right), (bottom _
                                    - (height * percY))))
            End If

        Next
        g2d.setStroke(oldStroke)
        ' Sort MS lines by intensity
        Collections.sort(msData, New Comparator)
        ' Try to label all masses above 10% normalized max intensity
        g2d.setFont(New java.awt.Font("Arial Unicode MS", java.awt.Font.BOLD, 9))
        Dim collisions As ArrayList(Of Rectangle2D.Double) = New ArrayList(Of Rectangle2D.Double)
        For Each p As MSPoint In msData
            If ((p.getMass <= highMass) _
                        AndAlso (p.getMass >= lowMass)) Then
                If (((p.getIntensity - minInt) _
                            > ((maxInt - minInt) _
                            * 0.1)) _
                            AndAlso (collisions.size < 20)) Then
                    ' Limit number of peak labels to 20
                    ' Try to draw if label doesn't overlap with previously drawn label or line
                    Dim x As Double = PDFReport.position(((p.getMass - lowMass) _
                                    / (highMass - lowMass)), left, right)
                    Dim percY As Double = ((p.getIntensity - minInt) _
                                / (maxInt - minInt))
                    ' calculate percent height of intensity line (and normalize intensity)
                    Dim y As Double = (bottom _
                                - (height * percY))
                    drawLabelIfNoCollisions(g2d, p.getMass, x, y, collisions)
                Else
                    Exit For
                End If

            End If

        Next
        PDFReport.labelMaxIntensity(g2d, r, maxInt, "Max: ")
    End Sub

    Private Shared Sub drawLabelIfNoCollisions(ByVal g2d As Graphics2D, ByVal mass As Single, ByVal x As Double, ByVal y As Double, ByVal collisions As ArrayList(Of Rectangle2D.Double))
        Dim s As String = ("" + mass)
        Dim fm As FontMetrics = g2d.getFontMetrics
        Dim textBounds As Rectangle2D = fm.getStringBounds(s, g2d)
        Dim r As Rectangle2D.Double = New Rectangle2D.Double((x + 2), (y + 2), textBounds.getWidth, textBounds.getHeight)
        For Each test As Rectangle2D.Double In collisions
            If test.intersects(r) Then
                Return
            End If

        Next
        g2d.drawString(s, (CType(x, Single) + 2.0!), (CType(y, Single) + 2.0!))
        collisions.add(r)
    End Sub

    'Public Overloads Function myCell(ByVal s As String, ByVal padding As Integer, ByVal leftpad As Integer) As PdfPCell
    '    Dim font As Font = New Font(Me.bfont)
    '    font.setSize(10)
    '    Dim cell As PdfPCell = New PdfPCell(New Phrase(s, font))
    '    cell.setPadding(padding)
    '    cell.setPaddingLeft(leftpad)
    '    cell.setLeading(0!, 1.2!)
    '    cell.setUseAscender(True)
    '    cell.setUseDescender(False)
    '    Return cell
    'End Function

    'Public Function myHeaderCell(ByVal s As String, ByVal padding As Integer) As PdfPCell
    '    Dim font As Font = New Font(Me.bfont)
    '    font.setSize(14)
    '    Dim cell As PdfPCell = New PdfPCell(New Phrase(s, font))
    '    cell.setPadding(padding)
    '    cell.setHorizontalAlignment(Element.ALIGN_RIGHT)
    '    cell.setBorder(0)
    '    Return cell
    'End Function

    'Public Overloads Function myCell(ByVal img As Image) As PdfPCell
    '    Dim cell As PdfPCell = New PdfPCell(img)
    '    cell.setBorder(0)
    '    Return cell
    'End Function

    'Public Function myText(ByVal s As String, ByVal fontsize As Integer) As Paragraph
    '    Dim font As Font = New Font(Me.bfont, fontsize)
    '    Dim p As Paragraph = New Paragraph(s, font)
    '    Return p
    'End Function

    Private Function convertDate(ByVal dateString As String) As String
        ' Convert '2010-12-12' to '12 Dec 2010'
        Dim date As java.util.Date
        Dim sdfOut As SimpleDateFormat = New SimpleDateFormat("dd MMM yyyy")
        Dim sdfIn As SimpleDateFormat = New SimpleDateFormat("yyyy-MM-dd")
        Try
            Date = sdfIn.parse(dateString)
            Return sdfOut.format(Of Date)
        Catch e As Exception
            Return dateString
        End Try

    End Function

    'Public Class ThickTableBorder
    '    Implements PdfPTableEvent

    '    Public Sub tableLayout(ByVal table As PdfPTable, ByVal width(,) As Single, ByVal height() As Single, ByVal headerRows As Integer, ByVal rowStart As Integer, ByVal canvas() As PdfContentByte)
    '        Dim widths() As Single = width(0)
    '        Dim x1 As Single = widths(0)
    '        Dim x2 As Single = widths((widths.Length - 1))
    '        Dim y1 As Single = height(0)
    '        Dim y2 As Single = height((height.Length - 1))
    '        Dim cb As PdfContentByte = canvas(PdfPTable.LINECANVAS)
    '        cb.rectangle(x1, y1, (x2 - x1), (y2 - y1))
    '        cb.setLineWidth(1.5!)
    '        cb.stroke
    '        cb.resetRGBColorStroke
    '    End Sub
    'End Class

    Public Class EndPage
        Inherits PdfPageEventHelper

        Private tpl As PdfTemplate

        Public Sub onOpenDocument(ByVal writer As PdfWriter, ByVal document As Document)
            Try
                ' initialization of the template
                Me.tpl = writer.getDirectContent.createTemplate(100, 100)
            Catch e As Exception
                Throw New ExceptionConverter(e)
            End Try

        End Sub

        Public Sub onEndPage(ByVal writer As PdfWriter, ByVal document As Document)
            Dim cb As PdfContentByte = writer.getDirectContent
            cb.saveState
            'compose the header
            Try
                Dim table As PdfPTable = New PdfPTable(2)
                table.setWidths(New Integer() {1, 1})
                table.setTotalWidth(527)
                table.addCell(Me.myCell(logo))
                table.addCell(Me.myHeaderCell(hm.get("sampleid"), 3))
                'table.addCell(myCell(getProperty("Header_Center"), false, 11, 1, 0, BaseColor.BLUE));
                'table.addCell(myCell(getProperty("Header_Right"), false, 8, 0, 0, BaseColor.BLUE));
                'table.writeSelectedRows(0, -1, 34, 827, cb);
                table.writeSelectedRows(0, -1, 34, 760, cb)
            Catch e As Exception
                e.printStackTrace
            End Try

            ' compose the footer
            Dim textBase As Single = (document.bottom - 20)
            Dim text As String = ("Page " _
                        + (writer.getPageNumber + " of "))
            Dim textSize As Single = bfont.getWidthPoint(text, 9)
            Dim adjust As Single = bfont.getWidthPoint("0", 9)
            cb.beginText
            cb.setFontAndSize(bfont, 9)
            cb.setTextMatrix((document.right _
                            - (textSize - adjust)), textBase)
            cb.showText(text)
            cb.endText
            cb.addTemplate(Me.tpl, (document.right - adjust), textBase)
            cb.restoreState
        End Sub

        Public Sub onCloseDocument(ByVal writer As PdfWriter, ByVal document As Document)
            Me.tpl.beginText
            Me.tpl.setFontAndSize(bfont, 9)
            Me.tpl.setTextMatrix(0, 0)
            Me.tpl.showText(("" _
                            + (writer.getPageNumber - 1)))
            Me.tpl.endText
        End Sub
    End Class

    Private Sub drawLCOutline(ByVal g2d As Graphics2D, ByVal r As Rectangle2D.Double, ByVal mintime As Double, ByVal maxtime As Double)
        Dim HASH_LG As Double = 8
        Dim HASH_SM As Double = 4
        Dim left As Double = r.getX
        Dim top As Double = r.getY
        Dim right As Double = (left + r.getWidth)
        Dim bottom As Double = (top + r.getHeight)
        Dim hlines As Double = (CType((bottom - top), Double) / 10)
        Dim vlines As Double = (CType((right - left), Double) / 10)
        ' Draw axis lines
        g2d.draw(New Line2D.Double(left, bottom, right, bottom))
        g2d.draw(New Line2D.Double(left, top, left, bottom))
        ' Draw y hash lines
        Dim i As Integer = 1
        Do While (i <= 11)
            Dim hash As Double = (((i - 1) _
                        Mod 5) _
                        = 0)
            'TODO: Warning!!!, inline IF is not supported ?
            g2d.draw(New Line2D.Double((left - hash), (top + CType(((i - 1) _
                                * hlines), Integer)), left, (top + CType(((i - 1) _
                                * hlines), Integer))))
            i = (i + 1)
        Loop

        ' Draw x hash lines - variable depending on scale
        Dim diff As Double = (Me.maxtime - Me.mintime)
        Dim inc As Double = 0.1

        While ((diff / inc) _
                    > 100)
            inc = (inc * 10)

        End While

        ' less than 100 hash marks

        While ((diff / inc) _
                    < 20)
            inc = (inc / 10)

        End While

        ' greater than 20 hash marks
        Dim hashinc As Double = inc
        If ((diff / inc) _
                    < 50) Then
            hashinc = (hashinc / 2)
        End If

        Dim d As Double = ((Math.Floor((Me.mintime / inc)) * inc) _
                    + inc)
        Do While (d _
                    <= (Me.maxtime + 0.00001))
            Dim hash As Double = (((d + 0.0001) _
                        Mod (hashinc * 10)) _
                        < 0.001)
            'TODO: Warning!!!, inline IF is not supported ?
            g2d.draw(New Line2D.Double((left + CType(((d - Me.mintime) _
                                / ((Me.maxtime - Me.mintime) _
                                * (right - left))), Integer)), bottom, (left + CType(((d - Me.mintime) _
                                / ((Me.maxtime - Me.mintime) _
                                * (right - left))), Integer)), (bottom + hash)))
            d = (d + inc)
        Loop

        'Draw y labels
        g2d.setFont(New java.awt.Font("Arial Unicode MS", java.awt.Font.PLAIN, 10))
        g2d.drawString("100", (CType(left, Single) - 30), (CType(top, Single) + 5))
        g2d.drawString("%", (CType(left, Single) - 20), (CType(top, Single) _
                        + (CType((5 * hlines), Integer) + 5)))
        g2d.drawString("0", (CType(left, Single) - 20), (CType(bottom, Single) + 5))
        'Draw x labels
        inc = (hashinc * 10)
        Dim d As Double = ((Math.Floor((Me.mintime / inc)) * inc) _
                    + inc)
        Do While (d < Me.maxtime)
            g2d.drawString(Double.ToString(d), (CType(left, Single) _
                            + (CType(((d - Me.mintime) _
                            / ((Me.maxtime - Me.mintime) _
                            * (right - left))), Integer) - 7)), (CType(bottom, Single) + 17))
            d = (d + inc)
        Loop

        g2d.drawString("Time (min)", CType(258, Single), (CType(bottom, Single) + 29))
    End Sub

    Private Sub drawMSOutline(ByVal g2d As Graphics2D, ByVal r As Rectangle2D.Double, ByVal lowMass As Double, ByVal highMass As Double)
        Dim HASH_LG As Double = 8
        Dim HASH_SM As Double = 4
        Dim left As Double = r.getX
        Dim top As Double = r.getY
        Dim right As Double = (left + r.getWidth)
        Dim bottom As Double = (top + r.getHeight)
        Dim hlines As Double = (CType((bottom - top), Double) / 10)
        Dim vlines As Double = (CType((right - left), Double) / 10)
        ' Draw axis lines
        g2d.setColor(Color.black)
        g2d.draw(New Line2D.Double(left, bottom, right, bottom))
        g2d.draw(New Line2D.Double(left, top, left, bottom))
        ' Draw y hash lines
        Dim i As Integer = 1
        Do While (i <= 11)
            Dim hash As Double = (((i - 1) _
                        Mod 5) _
                        = 0)
            'TODO: Warning!!!, inline IF is not supported ?
            g2d.draw(New Line2D.Double((left - hash), (top + CType(((i - 1) _
                                * hlines), Integer)), left, (top + CType(((i - 1) _
                                * hlines), Integer))))
            i = (i + 1)
        Loop

        ' Draw x hash lines - variable depending on scale
        Dim diff As Double = (highMass - lowMass)
        Dim inc As Double = 10

        While ((diff / inc) _
                    > 80)
            inc = (inc * 10)

        End While

        ' less than 80 hash marks

        While ((diff / inc) _
                    < 10)
            inc = (inc / 10)

        End While

        ' greater than 10 hash marks
        Dim hashinc As Double = inc
        If ((diff / inc) _
                    < 50) Then
            hashinc = (hashinc / 2)
        End If

        Dim d As Double = ((Math.Floor((lowMass / inc)) * inc) _
                    + inc)
        Do While (d <= highMass)
            Dim hash As Double = (((d + 0.0001) _
                        Mod (hashinc * 10)) _
                        < 0.001)
            'TODO: Warning!!!, inline IF is not supported ?
            g2d.draw(New Line2D.Double((left _
                                + ((d - lowMass) _
                                / ((highMass - lowMass) _
                                * (right - left)))), bottom, (left _
                                + ((d - lowMass) _
                                / ((highMass - lowMass) _
                                * (right - left)))), (bottom + hash)))
            d = (d + inc)
        Loop

        'Draw y labels
        g2d.setFont(New java.awt.Font("Arial Unicode MS", java.awt.Font.PLAIN, 10))
        g2d.drawString("100", (CType(left, Single) - 30), (CType(top, Single) + 5))
        g2d.drawString("%", (CType(left, Single) - 20), (CType(top, Single) _
                        + (CType((5 * hlines), Integer) + 5)))
        g2d.drawString("0", (CType(left, Single) - 20), (CType(bottom, Single) + 5))
        'Draw x labels
        inc = (hashinc * 10)
        Dim d As Double = ((Math.Floor((lowMass / inc)) * inc) _
                    + inc)
        Do While (d < highMass)
            g2d.drawString(("" + CType(d, Integer)), (CType(left, Single) _
                            + (CType(((d - lowMass) _
                            / ((highMass - lowMass) _
                            * (right - left))), Integer) - 8)), (CType(bottom, Single) + 17))
            d = (d + inc)
        Loop

        g2d.drawString("m/z", CType(270, Single), (CType(bottom, Single) + 29))
    End Sub

    Private Shared Function conv_x(ByVal perc As Double, ByVal left As Double, ByVal right As Double) As Double
        Dim l As Double = (right - left)
        Dim d As Double = (perc * l)
        Return (left + d)
    End Function

    Private Overloads Shared Function conv_y(ByVal y As Integer, ByVal top As Double, ByVal bottom As Double, ByVal minMSInt As Double, ByVal maxMSInt As Double) As Double
        Dim l As Double = (bottom - top)
        Dim s As Double = (maxMSInt - minMSInt)
        Dim d As Double = ((CType((y - minMSInt), Double) / s) _
                    * l)
        Return (bottom - d)
    End Function

    Private Overloads Shared Function conv_y(ByVal y As Double, ByVal top As Double, ByVal bottom As Double, ByVal minUV As Double, ByVal maxUV As Double) As Double
        Dim l As Double = (bottom - top)
        Dim s As Double = (maxUV - minUV)
        Dim d As Double = ((CType((y - minUV), Double) / s) _
                    * l)
        Return (bottom - d)
    End Function

    ' RETURN: value between bounds of left and right, given percentage
    Private Shared Function position(ByVal perc As Double, ByVal left As Double, ByVal right As Double) As Double
        Dim l As Double = (right - left)
        Dim d As Double = (perc * l)
        Return (left + d)
    End Function

    Private Shared Sub labelMaxIntensity(ByVal g2d As Graphics2D, ByVal r As Rectangle2D.Double, ByVal max As Double, ByVal type As String)
        g2d.setFont(New java.awt.Font("Arial Unicode MS", java.awt.Font.BOLD, 9))
        Dim formatter As NumberFormat = New DecimalFormat("0.00E0")
        Dim s As String = (type + formatter.format(max))
        Dim fm As FontMetrics = g2d.getFontMetrics
        Dim textBounds As Rectangle2D = fm.getStringBounds(s, g2d)
        Dim right As Double = (r.getX _
                    + (r.getWidth - textBounds.getWidth))
        Dim top As Double = r.getY
        If type.Equals("UV254: ") Then
            top = (top + textBounds.getHeight)
        End If

        g2d.drawString(s, CType(right, Single), CType(top, Single))
    End Sub

    Private Function convScantoMin(ByVal s As String) As String
        If (s Is Nothing) Then
            Return ""
        End If

        Try
            Dim i As Integer = Integer.parseInt(s)
            Dim d As Double = (Me.model.getRetTime(i) / 60000)
            Dim formatter As NumberFormat = New DecimalFormat("0.00")
            Return formatter.format(d)
        Catch e As Exception
            e.printStackTrace
        End Try

        Return ""
    End Function
End Class

