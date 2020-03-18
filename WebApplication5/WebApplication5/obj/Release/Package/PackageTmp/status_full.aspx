<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="status_full.aspx.vb" Inherits="WebApplication5.status_full" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=10" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="google" content="notranslate" />
    <meta http-equiv="Content-Language" content="en" />
    <meta http-equiv="refresh" content="20" />      
    <meta http-equiv="cache-control" content="no-cache, must-revalidate, post-check=0, pre-check=0" />
    <meta http-equiv="cache-control" content="max-age=0" />
    <meta http-equiv="expires" content="0" />
    <meta http-equiv="expires" content="Tue, 01 Jan 1980 1:00:00 GMT" />
    <meta http-equiv="pragma" content="no-cache" />

    <link href="Content/fontawesome-all.css" rel="stylesheet" />
    <title>Instrument Status</title>
    <style>
        body {
            margin: 0;
            font-family: Consolas;
            color:  white; 
            background-color: #0b2c51;
            font-size: 14pt;
        }
        .mypanel {
            background-image:url('Resources/Leaves Bkgrd.jpg');
            background-image:url('');
            background-size:cover;
            background-color: black;
            background-color: #6D7B8D;
            background-color: #596779;
            overflow: hidden;
        }
        .pagediv {
            Height:500px; 
            Width: 100%; 
            background-color: #073971;
            background-color: #0b2c51;
        }
        .topnav {
          overflow: hidden;
          background-color: #333;
        }

        .topnav a {
          float: left;
          display: block;
          color: #f2f2f2;
          text-align: center;
          padding: 14px 16px;
          text-decoration: none;
          font-size: 17px;
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

        @media screen and (max-width: 600px) {
          .topnav a:not(:first-child) {display: none;}
          .topnav a.icon {
            float: right;
            display: block;
          }
        }

        @media screen and (max-width: 600px) {
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
        </style>
</head>
<body>
    <div class="topnav" id="myTopnav">
      <a href="status.aspx" class="active">Status</a>
<%--      <a href="pressure.aspx">Pressure</a>
      <a href="errorLog.aspx">ErrorLog</a>--%>
      <a href="entriesH.aspx">Entries</a>
      <a href="javascript:void(0);" class="icon" onclick="myFunction()">
        <i class="fa fa-bars"></i>
      </a>
    </div>

    <script>
    function myFunction() {
      var x = document.getElementById("myTopnav");
      if (x.className === "topnav") {
        x.className += " responsive";
      } else {
        x.className = "topnav";
      }
    }
    </script>
    <form id="form1" runat="server">
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT INSTRUMENT_NAME, STATUS, QUEUE, ESTIMATED_TIME, CS_STATUS, CURRENT_SAMPLEID, USERNAME, METHOD, DETECT_STATE, LAST_UPDATED  FROM INSTRUMENT_STATUS WHERE (INSTRUMENT_NAME LIKE '%LCMS%') ORDER BY INSTRUMENT_NAME"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT INSTRUMENT_NAME, STATUS, QUEUE, ESTIMATED_TIME, CS_STATUS, CURRENT_SAMPLEID, USERNAME, METHOD, FLOWRATE, PERCENTB, PROCESS_OPTS, RUNS, DETECT_STATE, LAST_UPDATED  FROM INSTRUMENT_STATUS WHERE (INSTRUMENT_NAME LIKE '%PrepLCMS%') ORDER BY INSTRUMENT_NAME"></asp:SqlDataSource>

    <div class="pagediv">
        <div id="lcms" >
            <div>
                <asp:Label ID="Label3" runat="server" Text="ANALYTICAL LC-MS" style="color:white; font-size:11pt; font-family:Consolas; font-weight:bold;"></asp:Label>
                <asp:Button ID="Button1" runat="server" BackColor="lime" Font-Names="Consolas" ForeColor="Black" Text="Summary" style="position:relative; float:right; margin-right:5px;" />
            </div>
         <asp:Table ID="Table1" 
            runat="server" 
            Font-Size="10pt"          
            Width= "100%"
            Font-Names="Consolas" 
            VerticalAlign="Middle" 
            Gridlines="Both"
            table-layout="Fixed"
            >
            <asp:TableHeaderRow 
                runat="server" 
                height="25px"
                Font-Bold="true"
                Font-UnderLine="true"
                padding-top= "100px" 
                Color= "white" 
                HorizontalAlign="Left">
                <asp:TableHeaderCell>Instr</asp:TableHeaderCell>
                <asp:TableHeaderCell>Avail</asp:TableHeaderCell>
                <asp:TableHeaderCell>Queue</asp:TableHeaderCell>
                <asp:TableHeaderCell>Est Time</asp:TableHeaderCell>
                <asp:TableHeaderCell>Status</asp:TableHeaderCell>
                <asp:TableHeaderCell>Sample</asp:TableHeaderCell>
                <asp:TableHeaderCell>Last User</asp:TableHeaderCell>
                <asp:TableHeaderCell>Method</asp:TableHeaderCell>
                <asp:TableHeaderCell>Run Status</asp:TableHeaderCell>
                <asp:TableHeaderCell>Last Update</asp:TableHeaderCell>
            </asp:TableHeaderRow>
            <asp:TableRow ID="TableRow1" runat="server" VerticalAlign="Middle" HorizontalAlign="Left" style="color:white; font-size:10pt; font-family:Consolas;">
                <asp:TableCell >LCMS-1  (653)</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">Yes</asp:TableCell>
                <asp:TableCell >0</asp:TableCell>
                <asp:TableCell >0 min</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">WAITING</asp:TableCell>
                <asp:TableCell>No queue</asp:TableCell>
                <asp:TableCell>Admin</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow2" runat="server" VerticalAlign="Middle" HorizontalAlign="Left" style="color:white; font-size:10pt; font-family:Consolas;">
                <asp:TableCell >LCMS-2  (653)</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">Yes</asp:TableCell>
                <asp:TableCell >0</asp:TableCell>
                <asp:TableCell >0 min</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">WAITING</asp:TableCell>
                <asp:TableCell>No queue</asp:TableCell>
                <asp:TableCell>Admin</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow3" runat="server" VerticalAlign="Middle" HorizontalAlign="Left" style="color:white; font-size:10pt; font-family:Consolas;">
                <asp:TableCell >LCMS-3  (641)</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">Yes</asp:TableCell>
                <asp:TableCell >0</asp:TableCell>
                <asp:TableCell >0 min</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">WAITING</asp:TableCell>
                <asp:TableCell>No queue</asp:TableCell>
                <asp:TableCell>Admin</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow4" runat="server" VerticalAlign="Middle" HorizontalAlign="Left" style="color:white; font-size:10pt; font-family:Consolas;">
                <asp:TableCell >LCMS-4  (653)</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">Yes</asp:TableCell>
                <asp:TableCell >0</asp:TableCell>
                <asp:TableCell >0 min</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">WAITING</asp:TableCell>
                <asp:TableCell>No queue</asp:TableCell>
                <asp:TableCell>Admin</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow5" runat="server" VerticalAlign="Middle" HorizontalAlign="Left" style="color:white; font-size:10pt; font-family:Consolas;">
                <asp:TableCell >LCMS-5  (653)</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">Yes</asp:TableCell>
                <asp:TableCell >0</asp:TableCell>
                <asp:TableCell >0 min</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">WAITING</asp:TableCell>
                <asp:TableCell>No queue</asp:TableCell>
                <asp:TableCell>Admin</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow6" runat="server" VerticalAlign="Middle" HorizontalAlign="Left" style="color:white; font-size:10pt; font-family:Consolas;">
                <asp:TableCell >LCMS-6  (612)</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">Yes</asp:TableCell>
                <asp:TableCell >0</asp:TableCell>
                <asp:TableCell >0 min</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">WAITING</asp:TableCell>
                <asp:TableCell>No queue</asp:TableCell>
                <asp:TableCell>Admin</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow7" runat="server" VerticalAlign="Middle" HorizontalAlign="Left" style="color:white; font-size:10pt; font-family:Consolas;">
                <asp:TableCell >LCMS-7  (641)</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">Yes</asp:TableCell>
                <asp:TableCell >0</asp:TableCell>
                <asp:TableCell >0 min</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">WAITING</asp:TableCell>
                <asp:TableCell>No queue</asp:TableCell>
                <asp:TableCell>Admin</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow8" runat="server" VerticalAlign="Middle" HorizontalAlign="Left" style="color:white; font-size:10pt; font-family:Consolas;">
                <asp:TableCell >LCMS-8  (612)</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">Yes</asp:TableCell>
                <asp:TableCell >0</asp:TableCell>
                <asp:TableCell >0 min</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">WAITING</asp:TableCell>
                <asp:TableCell>No queue</asp:TableCell>
                <asp:TableCell>Admin</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow9" runat="server" VerticalAlign="Middle" HorizontalAlign="Left" style="color:white; font-size:10pt; font-family:Consolas;">
                <asp:TableCell >LCMS-11 (612)</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">Yes</asp:TableCell>
                <asp:TableCell >0</asp:TableCell>
                <asp:TableCell >0 min</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">WAITING</asp:TableCell>
                <asp:TableCell>No queue</asp:TableCell>
                <asp:TableCell>Admin</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow10" runat="server" VerticalAlign="Middle" HorizontalAlign="Left" style="color:white; font-size:10pt; font-family:Consolas;">
                <asp:TableCell >LCMS-12 (641)</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">Yes</asp:TableCell>
                <asp:TableCell >0</asp:TableCell>
                <asp:TableCell >0 min</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">WAITING</asp:TableCell>
                <asp:TableCell>No queue</asp:TableCell>
                <asp:TableCell>Admin</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow11" runat="server" VerticalAlign="Middle" HorizontalAlign="Left" style="color:white; font-size:10pt; font-family:Consolas;">
                <asp:TableCell >LCMS-13 (612)</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">Yes</asp:TableCell>
                <asp:TableCell >0</asp:TableCell>
                <asp:TableCell >0 min</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">WAITING</asp:TableCell>
                <asp:TableCell>No queue</asp:TableCell>
                <asp:TableCell>Admin</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
            </asp:TableRow>
        </asp:Table>
         </div>
         <div id="prep" >
        <asp:Label ID="Label4" runat="server" Text="PREP LC-MS" style="color:white; font-size:11pt; font-family:Consolas; font-weight:bold;"></asp:Label>
         <asp:Table ID="Table2" 
            runat="server" 
            Font-Size="10pt"          
            Width= "100%"
            Font-Names="Consolas" 
            VerticalAlign="Middle" 
            Gridlines="Both"
            table-layout="Fixed"
            >
            <asp:TableHeaderRow 
                runat="server"
                height="25px"
                Font-Bold="true"
                Font-UnderLine="true"
                padding-top= "100px" 
                Color= "white" 
                HorizontalAlign="Left">
                <asp:TableHeaderCell>Instr</asp:TableHeaderCell>
                <asp:TableHeaderCell>Avail</asp:TableHeaderCell>
                <asp:TableHeaderCell>Queue</asp:TableHeaderCell>
                <asp:TableHeaderCell>Est Time</asp:TableHeaderCell>
                <asp:TableHeaderCell>Status</asp:TableHeaderCell>
                <asp:TableHeaderCell>Sample</asp:TableHeaderCell>
                <asp:TableHeaderCell>Last User</asp:TableHeaderCell>   
                <asp:TableHeaderCell>Method</asp:TableHeaderCell>
                <asp:TableHeaderCell>Flowrate</asp:TableHeaderCell>
                <asp:TableHeaderCell>%B</asp:TableHeaderCell>
                <asp:TableHeaderCell>ProcessOpts</asp:TableHeaderCell>
                <asp:TableHeaderCell>Runs</asp:TableHeaderCell>
                <asp:TableHeaderCell>DetectState</asp:TableHeaderCell>
                <asp:TableHeaderCell>Last Update</asp:TableHeaderCell>
            </asp:TableHeaderRow>
            <asp:TableRow ID="TableRow12" runat="server" VerticalAlign="Middle" HorizontalAlign="Left" style="color:white; font-size:10pt; font-family:Consolas;">
                <asp:TableCell >Prep-1</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">Yes</asp:TableCell>
                <asp:TableCell >0</asp:TableCell>
                <asp:TableCell >0 min</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">WAITING</asp:TableCell>
                <asp:TableCell>No queue</asp:TableCell>
                <asp:TableCell>Admin</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow13" runat="server" VerticalAlign="Middle" HorizontalAlign="Left" style="color:white; font-size:10pt; font-family:Consolas;">
                <asp:TableCell >Prep-2</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">Yes</asp:TableCell>
                <asp:TableCell >0</asp:TableCell>
                <asp:TableCell >0 min</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">WAITING</asp:TableCell>
                <asp:TableCell>No queue</asp:TableCell>
                <asp:TableCell>Admin</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow14" runat="server" VerticalAlign="Middle" HorizontalAlign="Left" style="color:white; font-size:10pt; font-family:Consolas;">
                <asp:TableCell >Prep-3</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">Yes</asp:TableCell>
                <asp:TableCell >0</asp:TableCell>
                <asp:TableCell >0 min</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">WAITING</asp:TableCell>
                <asp:TableCell>No queue</asp:TableCell>
                <asp:TableCell>Admin</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow15" runat="server" VerticalAlign="Middle" HorizontalAlign="Left" style="color:white; font-size:10pt; font-family:Consolas;">
                <asp:TableCell >Prep-4</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">Yes</asp:TableCell>
                <asp:TableCell >0</asp:TableCell>
                <asp:TableCell >0 min</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">WAITING</asp:TableCell>
                <asp:TableCell>No queue</asp:TableCell>
                <asp:TableCell>Admin</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow16" runat="server" VerticalAlign="Middle" HorizontalAlign="Left" style="color:white; font-size:10pt; font-family:Consolas;">
                <asp:TableCell >Prep-5</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">Yes</asp:TableCell>
                <asp:TableCell >0</asp:TableCell>
                <asp:TableCell >0 min</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">WAITING</asp:TableCell>
                <asp:TableCell>No queue</asp:TableCell>
                <asp:TableCell>Admin</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow17" runat="server" VerticalAlign="Middle" HorizontalAlign="Left" style="color:white; font-size:10pt; font-family:Consolas;">
                <asp:TableCell >Prep-6</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">Yes</asp:TableCell>
                <asp:TableCell >0</asp:TableCell>
                <asp:TableCell >0 min</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">WAITING</asp:TableCell>
                <asp:TableCell>No queue</asp:TableCell>
                <asp:TableCell>Admin</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow18" runat="server" VerticalAlign="Middle" HorizontalAlign="Left" style="color:white; font-size:10pt; font-family:Consolas;">
                <asp:TableCell >Prep-7</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">Yes</asp:TableCell>
                <asp:TableCell >0</asp:TableCell>
                <asp:TableCell >0 min</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">WAITING</asp:TableCell>
                <asp:TableCell>No queue</asp:TableCell>
                <asp:TableCell>Admin</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow19" runat="server" VerticalAlign="Middle" HorizontalAlign="Left" style="color:white; font-size:10pt; font-family:Consolas;">
                <asp:TableCell >Prep-8</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">Yes</asp:TableCell>
                <asp:TableCell >0</asp:TableCell>
                <asp:TableCell >0 min</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">WAITING</asp:TableCell>
                <asp:TableCell>No queue</asp:TableCell>
                <asp:TableCell>Admin</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow20" runat="server" VerticalAlign="Middle" HorizontalAlign="Left" style="color:white; font-size:10pt; font-family:Consolas;">
                <asp:TableCell >Prep-9</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">Yes</asp:TableCell>
                <asp:TableCell >0</asp:TableCell>
                <asp:TableCell >0 min</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">WAITING</asp:TableCell>
                <asp:TableCell>No queue</asp:TableCell>
                <asp:TableCell>Admin</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
            </asp:TableRow>
        </asp:Table>
         </div>
         <div>
            <asp:Label ID="Label2" runat="server" Text="Last Updated: " style="float:left; color:white; font-size:10pt; font-family:Consolas;"></asp:Label>
        </div>

      </div>

       <div style="float: left; width:40%;">
            &nbsp;&nbsp;&nbsp;
            <br />
            <asp:Panel ID="Panel1" runat="server">
                <asp:GridView OnRowDataBound="GridView1_RowDataBound" ID="GridView1" runat="server" AllowPaging="True"  AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None" PageSize="15" Font-Size="Small">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:templatefield HeaderText="Select">
                            <itemtemplate>
                                <asp:checkbox ID="cbSelect"
                                CssClass="gridCB" runat="server"></asp:checkbox>
                            </itemtemplate>
                        </asp:templatefield>
                        <asp:BoundField DataField="ERROR_TEXT" HeaderText="ERROR_TEXT" SortExpression="ERROR_TEXT" />
                        <asp:BoundField DataField="ERROR_DATE" HeaderText="ERROR_DATE" SortExpression="ERROR_DATE" />
                        <asp:BoundField DataField="INSTRUMENT_ID" HeaderText="INSTRUMENT_ID" SortExpression="INSTRUMENT_ID" />
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
            </asp:Panel>
            &nbsp;&nbsp;&nbsp;
            <br />

            <asp:Button ID="Button2" runat="server" Text="Delete Selected Rows" />

            <asp:Button ID="Button3" runat="server" Text="Delete All" />

            <br />
       </div>
       <div style="float: left; width: 60%;">

        <asp:Panel ID="Panel2" runat="server"  Width="100%" BackColor="#0b2c51">
            <table class="auto-style4">
                <tr>
                    <td class="auto-style2">
                        <asp:Chart ID="Chart1" runat="server" Height="150px" Width="225px">
                            <Legends>
                                <asp:Legend BackColor="Transparent" Enabled="True" Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="False" Name="Default" TitleFont="Microsoft Sans Serif, 8pt, style=Bold">
                                </asp:Legend>
                            </Legends>
                            <Series>
                                <asp:Series ChartType="Bubble" YValuesPerPoint="2">
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                        <asp:Chart ID="Chart2a" runat="server" Height="150px" Width="225px">
                            <Legends>
                                <asp:Legend BackColor="Transparent" Enabled="True" Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="False" Name="Default" TitleFont="Microsoft Sans Serif, 8pt, style=Bold">
                                </asp:Legend>
                            </Legends>
                            <Series>
                                <asp:Series ChartType="Bubble" YValuesPerPoint="2">
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                        <asp:Chart ID="Chart2b" runat="server" Height="150px" Width="225px">
                            <Legends>
                                <asp:Legend BackColor="Transparent" Enabled="True" Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="False" Name="Default" TitleFont="Microsoft Sans Serif, 8pt, style=Bold">
                                </asp:Legend>
                            </Legends>
                            <Series>
                                <asp:Series ChartType="Bubble" YValuesPerPoint="2">
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                        <asp:Chart ID="Chart3" runat="server" Height="150px" Width="225px">
                            <Legends>
                                <asp:Legend BackColor="Transparent" Enabled="True" Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="False" Name="Default" TitleFont="Microsoft Sans Serif, 8pt, style=Bold">
                                </asp:Legend>
                            </Legends>
                            <Series>
                                <asp:Series ChartType="Bubble" YValuesPerPoint="2">
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                        <asp:Chart ID="Chart4" runat="server" Height="150px" Width="225px">
                            <Legends>
                                <asp:Legend BackColor="Transparent" Enabled="True" Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="False" Name="Default" TitleFont="Microsoft Sans Serif, 8pt, style=Bold">
                                </asp:Legend>
                            </Legends>
                            <Series>
                                <asp:Series ChartType="Bubble" YValuesPerPoint="2">
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </td>
                </tr>
            </table>
        </asp:Panel>


        <asp:Panel ID="Panel3" runat="server" Width="100%" BackColor="#0b2c51">
            <table class="auto-style3">
                <tr>
                    <td class="auto-style2">
                        <asp:Chart ID="Chart5a" runat="server" Height="150px" Width="225px">
                            <Legends>
                                <asp:Legend BackColor="Transparent" Enabled="True" Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="False" Name="Default" TitleFont="Microsoft Sans Serif, 8pt, style=Bold">
                                </asp:Legend>
                            </Legends>
                            <Series>
                                <asp:Series ChartType="Bubble" YValuesPerPoint="2">
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                        <asp:Chart ID="Chart5b" runat="server" Height="150px" Width="225px">
                            <Legends>
                                <asp:Legend BackColor="Transparent" Enabled="True" Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="False" Name="Default" TitleFont="Microsoft Sans Serif, 8pt, style=Bold">
                                </asp:Legend>
                            </Legends>
                            <Series>
                                <asp:Series ChartType="Bubble" YValuesPerPoint="2">
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                        <asp:Chart ID="Chart6" runat="server" Height="150px" Width="225px">
                            <Legends>
                                <asp:Legend BackColor="Transparent" Enabled="True" Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="False" Name="Default" TitleFont="Microsoft Sans Serif, 8pt, style=Bold">
                                </asp:Legend>
                            </Legends>
                            <Series>
                                <asp:Series ChartType="Bubble" YValuesPerPoint="2">
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                        <asp:Chart ID="Chart7" runat="server" Height="150px" Width="225px">
                            <Legends>
                                <asp:Legend BackColor="Transparent" Enabled="True" Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="False" Name="Default" TitleFont="Microsoft Sans Serif, 8pt, style=Bold">
                                </asp:Legend>
                            </Legends>
                            <Series>
                                <asp:Series ChartType="Bubble" YValuesPerPoint="2">
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                        <asp:Chart ID="Chart8" runat="server" Height="150px" Width="225px">
                            <Legends>
                                <asp:Legend BackColor="Transparent" Enabled="True" Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="False" Name="Default" TitleFont="Microsoft Sans Serif, 8pt, style=Bold">
                                </asp:Legend>
                            </Legends>
                            <Series>
                                <asp:Series ChartType="Bubble" YValuesPerPoint="2">
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </td>
                </tr>
            </table>
        </asp:Panel>


        <asp:Panel ID="Panel4" runat="server"  Width="100%" BackColor="#0b2c51">
            <table class="auto-style4">
                <tr>
                    <td class="auto-style2">
                        <asp:Chart ID="Chart9" runat="server" Height="150px" Width="225px">
                            <Legends>
                                <asp:Legend BackColor="Transparent" Enabled="True" Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="False" Name="Default" TitleFont="Microsoft Sans Serif, 8pt, style=Bold">
                                </asp:Legend>
                            </Legends>
                            <Series>
                                <asp:Series ChartType="Bubble" YValuesPerPoint="2">
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                        <asp:Chart ID="Chart10" runat="server" Height="150px" Width="225px">
                            <Legends>
                                <asp:Legend BackColor="Transparent" Enabled="True" Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="False" Name="Default" TitleFont="Microsoft Sans Serif, 8pt, style=Bold">
                                </asp:Legend>
                            </Legends>
                            <Series>
                                <asp:Series ChartType="Bubble" YValuesPerPoint="2">
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                        <asp:Chart ID="Chart11" runat="server" Height="150px" Width="225px">
                            <Legends>
                                <asp:Legend BackColor="Transparent" Enabled="True" Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="False" Name="Default" TitleFont="Microsoft Sans Serif, 8pt, style=Bold">
                                </asp:Legend>
                            </Legends>
                            <Series>
                                <asp:Series ChartType="Bubble" YValuesPerPoint="2">
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                        <asp:Chart ID="Chart12" runat="server" Height="150px" Width="225px">
                            <Legends>
                                <asp:Legend BackColor="Transparent" Enabled="True" Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="False" Name="Default" TitleFont="Microsoft Sans Serif, 8pt, style=Bold">
                                </asp:Legend>
                            </Legends>
                            <Series>
                                <asp:Series ChartType="Bubble" YValuesPerPoint="2">
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                        <asp:Chart ID="Chart13" runat="server" Height="150px" Width="225px">
                            <Legends>
                                <asp:Legend BackColor="Transparent" Enabled="True" Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="False" Name="Default" TitleFont="Microsoft Sans Serif, 8pt, style=Bold">
                                </asp:Legend>
                            </Legends>
                            <Series>
                                <asp:Series ChartType="Bubble" YValuesPerPoint="2">
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </td>
                </tr>
            </table>
        </asp:Panel>


        </div>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
            ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
            SelectCommand="SELECT ERROR_TEXT, ERROR_DATE, INSTRUMENT_ID FROM INSTRUMENT_ERROR_LOG ORDER BY ERROR_DATE DESC" 
            DeleteCommand="DELETE FROM INSTRUMENT_ERROR_LOG WHERE ERROR_DATE = :ERROR_DATE">
            <DeleteParameters>
                <asp:Parameter Name=":ERROR_DATE" Type="DateTime"/>
           </DeleteParameters>
        </asp:SqlDataSource>
        
            <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT &quot;ID&quot;, &quot;INSTTYPE&quot;, &quot;INSTNAME&quot; FROM &quot;INSTRUMENTS&quot; WHERE ((&quot;INSTTYPE&quot; = ?) AND (&quot;INSTTYPE&quot; = ?))">
            <SelectParameters>
                <asp:Parameter DefaultValue="LCMS" Name="INSTTYPE" Type="String" />
                <asp:Parameter DefaultValue="PrepLCMS" Name="INSTTYPE2" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>

    </form>


</body>
</html>

