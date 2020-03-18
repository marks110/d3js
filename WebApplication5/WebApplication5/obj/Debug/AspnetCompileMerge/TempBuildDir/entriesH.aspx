﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="entriesH.aspx.vb" Inherits="WebApplication5.entriesH" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>Entries</title>
    <meta http-equiv="X-UA-Compatible" content="IE=10" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
<%--    <meta http-equiv="refresh" content="600" />--%>
    <meta charset="utf-8" />
<%--    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    <script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/split.js/1.3.5/split.min.js"></script> 
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js"></script>--%>
     <link href="Content/bootstrap.min.css" rel="stylesheet"/>
    <link href="Content/font-awesome.min.css" rel="stylesheet" />
    <script src="Scripts/jquery-3.4.1.min.js"></script>
    <script src="Scripts/jquery-ui-1.12.1.min.js"></script>
    <script src="Scripts/split.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>

    <style>
        .topnav {
          overflow: hidden;
          background-color: #333;
          height: 48px;
        }

        .topnav a {
          float: left;
          display: block;
          color: #f2f2f2;
          text-align: center;
          padding: 14px 16px;
          text-decoration: none;
          font-size: 17px;
          font-family: Consolas;
        }

        .topnav a:hover {
          background-color: #ddd;
          color: black;
        }

        .topnav a.active {
          background-color: #4CAF50;
          color: white;
        }

        .topnav .icon {
          display: none;
        }

        @media screen and (max-width: 100vw) {
          .topnav a:not(:first-child) {display: none;}
          .topnav a.icon {
            float: right;
            display: block;
          }
        }

        @media screen and (max-width: 100vw) {
          .topnav.responsive {position: relative;}
          .topnav.responsive .icon {
            position: absolute;
            right: 0;
            top: 0;
          }
          .topnav.responsive a {
            float: none;
            display: block;
            text-align: left;
          }
        }
        .hiddencol
        {
           display: none;
        }
        .txtbx {
            border: 2px solid;
            padding: 3px 3px 3px 3px;
            margin: 3px 3px 3px 3px;
        }    
    
        .container-fluid {
          background-color: white;
          border: 2px white;
        }
        .row {
          background-color: white;
          height: 35px;
          margin: 2px;
        }
        .col {
          margin: 15px;
        }
        /* CSS Goes Here */
        .split {-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;overflow-y: auto;overflow-x: hidden;}
        .gutter {background-color: transparent;background-repeat: no-repeat;background-position: 50%;}
        .gutter.gutter-horizontal {cursor: col-resize;background-image:  url('data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAUAAAAeCAYAAADkftS9AAAAIklEQVQoU2M4c+bMfxAGAgYYmwGrIIiDjrELjpo5aiZeMwF+yNnOs5KSvgAAAABJRU5ErkJggg=='); }
        .gutter.gutter-vertical {cursor: row-resize;background-image: url('data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAB4AAAAFAQMAAABo7865AAAABlBMVEVHcEzMzMzyAv2sAAAAAXRSTlMAQObYZgAAABBJREFUeF5jOAMEEAIEEFwAn3kMwcB6I2AAAAAASUVORK5CYII='); }
        .split.split-horizontal, .gutter.gutter-horizontal { height: 100%;float: left;}
        
        #one{
            background:white;
            overflow: auto;
        }
        #two{
            background:green;
            overflow: auto;
        }   
        </style>
</head>
<body>
    <script>
    function myFunction() {
      var x = document.getElementById("myTopnav");
      if (x.className === "topnav") {
        x.className += " responsive";
      } else {
        x.className = "topnav";
      }
    }

    function CopyToClipboard(myPath) 
    {
             var controlValue  = myPath;
             //alert(controlValue);
             window.clipboardData.setData('Text', controlValue);
             //alert('Copied text to clipboard : ' +controlValue); 
    }   

 </script>

    <form id="form1" runat="server">
        <div>
                <asp:Panel ID="pnlNav" runat="server" Width="100%">
                    <div class="topnav" id="myTopnav">
                        <a href="status.aspx">Status</a>
                        <a href="pressure.aspx">Pressure</a>
                        <a href="errorLog.aspx">ErrorLog</a>
                        <a href="entriesH.aspx" class="active">Entries</a>&nbsp;<a href="javascript:void(0);" class="icon" onclick="myFunction()"><i class="fa fa-bars"></i></a>
                    </div>
                </asp:Panel>
        </div>

         <div class="container-fluid ">
          <div class="row">
                    <div class="d-flex mr-auto flex-fill" >
                           <asp:TextBox ID="TextBox1" placeholder="Sample..." runat="server" CssClass="txtbx" Width="184px" Height="30px"></asp:TextBox>

                            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource2" DataTextField="UNAME" DataValueField="UNAME" Selected="False" AppendDataBoundItems="True">
                                <asp:ListItem Text="All Users" Value="0" />
                            </asp:DropDownList>

                            <asp:DropDownList ID="DropDownList2" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="SqlDataSource3" DataTextField="INSTNAME" DataValueField="INSTNAME" Selected="False">
                                <asp:ListItem Text="Instruments" Value="0" />
                            </asp:DropDownList>

                            <asp:DropDownList ID="ListBox1" runat="server" AutoPostBack="True" onselectedindexchanged="ListBox1_SelectedIndexChanged">
                                <asp:ListItem Text="Date" Value="0" />
                                <asp:ListItem Text="Today" Value="1" />
                                <asp:ListItem Text="Past Week" Value="2" />
                                <asp:ListItem Text="Past Month" Value="3" />
                            </asp:DropDownList>
                            
                            <asp:Button ID="Button5" runat="server" Text="Clear" />

                    </div>
                    <div class="d-flex justify-content-end flex-fill">       
                            <asp:Button ID="Button1" runat="server" Text="Download" />       
                            <asp:Label ID="Label1" runat="server"></asp:Label>
                            <asp:Button ID="Button2" runat="server" Text="Process" />
                            <asp:Button ID="Button4" runat="server" Text="Refresh"  />
                            <asp:Button ID="Button3" runat="server" Text="Layout" />
                    </div>
            </div>
         </div>

        <div class="row no-gutters" style="height: 100vh; width: 100vw;">
            <div id="one">         
                <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False" enablepagingandcallback="true" CellPadding="4" ForeColor="#333333" GridLines="None" Font-Size="Small" OnRowDataBound="OnRowDataBound">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:CommandField SelectText="PDF" ShowSelectButton="True" />
                        <asp:BoundField DataField="UNAME" HeaderText="USERNAME" SortExpression="USERNAME" />
                        <asp:BoundField DataField="SAMPLEID" HeaderText="SAMPLEID" SortExpression="SAMPLEID" />
                        <asp:BoundField DataField="DATERUN" HeaderText="DATERUN" SortExpression="DATERUN" />
                        <asp:BoundField DataField="INSTNAME" HeaderText="INSTNAME" SortExpression="INSTNAME" />
                        <asp:BoundField DataField="CUSTOM1" HeaderText="CUSTOM1" SortExpression="CUSTOM1" Visible="False" />
                        <asp:BoundField DataField="CUSTOM2" HeaderText="METHOD" SortExpression="CUSTOM2" />
                        <asp:BoundField DataField="CUSTOM6" HeaderText="CUSTOM6" SortExpression="CUSTOM6" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                        <asp:BoundField DataField="CUSTOM7" HeaderText="CUSTOM7" SortExpression="CUSTOM7" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
            </div>
            <div id="two">
               <iframe src="#" id="iframePDFViewer"  runat="server" class="auto-style1"></iframe>
            </div>

         <asp:SqlDataSource 
            ID="SqlDataSource1" 
            runat="server" 
            ConnectionString="<%$ ConnectionStrings:ConnectionString2 %>" 
            ProviderName="<%$ ConnectionStrings:ConnectionString2.ProviderName %>" 
            SelectCommand="SELECT UPPER(USERNAME) AS UNAME, SAMPLEID, DATERUN, INSTNAME, CUSTOM1, CUSTOM2, CUSTOM6, CUSTOM7 FROM ANALYTICAL_CC.ENTRIES WHERE (DATERUN &gt;= SYSDATE - 1) ORDER BY ID DESC">
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString2 %>" ProviderName="<%$ ConnectionStrings:ConnectionString2.ProviderName %>" SelectCommand="SELECT DISTINCT UPPER(USERNAME) AS UNAME FROM ANALYTICAL_CC.ADB_USERS ORDER BY UNAME"></asp:SqlDataSource>       
        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString2 %>" ProviderName="<%$ ConnectionStrings:ConnectionString2.ProviderName %>" SelectCommand="SELECT DISTINCT INSTNAME FROM ANALYTICAL_CC.ENTRIES WHERE INSTNAME LIKE 'LCMS%' OR INSTNAME LIKE 'PrepLCMS%' ORDER BY INSTNAME ASC"></asp:SqlDataSource>             

    </div>
    <script>
        var sizes = localStorage.getItem('split-sizes1');

        if ( sizes === 'undefined') {
            sizes = [50, 50]; // default sizes
        } else {
            sizes = JSON.parse(sizes);
        }

        var mysplit = Split(["#one","#two"], {
            sizes: sizes,
            onDragEnd: function (sizes) { localStorage.setItem('split-sizes1', JSON.stringify(mysplit.getSizes())); },
            minSize: 200,
            gutterSize: 6,
            cursor: 'col-resize'
        });

                    //elementStyle: function (dimension, size, gutterSize) { 
            //    $(window).trigger('resize'); // Optional
            //    return {'flex-basis': 'calc(' + size + '% - ' + gutterSize + 'px)'}
            //},
            //gutterStyle: function (dimension, gutterSize) { return {'flex-basis':  gutterSize + 'px'} },
    </script>
    </form>
        
</body>
</html>
