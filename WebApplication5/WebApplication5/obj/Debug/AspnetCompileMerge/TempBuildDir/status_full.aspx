<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="status_full.aspx.vb" Inherits="WebApplication5.status_full" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=10" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta http-equiv="refresh" content="600" />

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <title>Instrument Status</title>
    <style>
        body {
          margin: 0;
          font-family: Consolas;
          color:lime; font-size: 12pt;
        }
        .mypanel {
         background-image:url('Resources/Leaves Bkgrd.jpg');
         background-image:url('');        
         background-size:cover;
         background-color: #6D7B8D;
         background-color: #4F5D6F;
         background-color: #596779;
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
        </style>
</head>
<body>
    <div class="topnav" id="myTopnav">
      <a href="status.aspx" class="active">Status</a>
      <a href="pressure.aspx">Pressure</a>
      <a href="errorLog.aspx">ErrorLog</a>
      <a href="entries.aspx">Entries</a>
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

        <asp:Panel ID="Panel1" runat="server" Height="600px" Width="100%"  CssClass="mypanel">
        <div id="title" >
            <asp:Label ID="Label1" runat="server" Text="Incyte 1801 Open Access Monitor" style="color:lightgrey; font-size:12pt; font-family:Consolas;"></asp:Label>
            <br />
            <asp:Label ID="Label2" runat="server" Text="Last Updated: " style="float:right; color:lightgrey; font-size:10pt; font-family:Consolas;"></asp:Label>
            <br />
          <hr style="border-top: 3px solid lightgrey; height:10px" />      
        </div>
        <div id="lcms" >
        <asp:Label ID="Label3" runat="server" Text="Analytical LC-MS" style="color:lightgrey; font-size:11pt; font-family:Consolas; font-weight:bold;"></asp:Label>
        <asp:Table ID="Table1" 
            runat="server" 
            Font-Size="10"          
            Width="100%" 
            Font-Names="Consolas" 
            VerticalAlign="Middle" 
            HorizontalAlign="Center"
            Gridlines="Both"
            >
            <asp:TableHeaderRow 
                runat="server" 
                Font-Bold="true"
                Font-UnderLine="true"
                Style="padding-top:100px; Color:lightgrey;">
                <asp:TableHeaderCell>Instr</asp:TableHeaderCell>
                <asp:TableHeaderCell>Avail</asp:TableHeaderCell>
                <asp:TableHeaderCell>Queue</asp:TableHeaderCell>
                <asp:TableHeaderCell>Est Time</asp:TableHeaderCell>
                <asp:TableHeaderCell>Status</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Sample</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">User</asp:TableHeaderCell>
                <asp:TableHeaderCell >Method</asp:TableHeaderCell>
                <asp:TableHeaderCell >Run Status</asp:TableHeaderCell>
                <asp:TableHeaderCell >Last Update</asp:TableHeaderCell>
            </asp:TableHeaderRow>
            <asp:TableRow ID="TableRow1" runat="server" VerticalAlign="Middle" HorizontalAlign="Center" style="color:lime; font-size:10pt; font-family:Consolas;">
                <asp:TableCell >LCMS-1  (653)</asp:TableCell>
                <asp:TableCell >Yes</asp:TableCell>
                <asp:TableCell >0</asp:TableCell>
                <asp:TableCell >0 min</asp:TableCell>
                <asp:TableCell >WAITING</asp:TableCell>
                <asp:TableCell HorizontalAlign="Left">No queue</asp:TableCell>
                <asp:TableCell HorizontalAlign="Left">Admin</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow2" runat="server" VerticalAlign="Middle" HorizontalAlign="Center" style="color:lime; font-size:10pt; font-family:Consolas;">
                <asp:TableCell >LCMS-2  (653)</asp:TableCell>
                <asp:TableCell >Yes</asp:TableCell>
                <asp:TableCell >0</asp:TableCell>
                <asp:TableCell >0 min</asp:TableCell>
                <asp:TableCell >WAITING</asp:TableCell>
                <asp:TableCell HorizontalAlign="Left">No queue</asp:TableCell>
                <asp:TableCell HorizontalAlign="Left">Admin</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow3" runat="server" VerticalAlign="Middle" HorizontalAlign="Center" style="color:lime; font-size:10pt; font-family:Consolas;">
                <asp:TableCell >LCMS-3  (641)</asp:TableCell>
                <asp:TableCell >Yes</asp:TableCell>
                <asp:TableCell >0</asp:TableCell>
                <asp:TableCell >0 min</asp:TableCell>
                <asp:TableCell >WAITING</asp:TableCell>
                <asp:TableCell HorizontalAlign="Left">No queue</asp:TableCell>
                <asp:TableCell HorizontalAlign="Left">Admin</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow4" runat="server" VerticalAlign="Middle" HorizontalAlign="Center" style="color:lime; font-size:10pt; font-family:Consolas;">
                <asp:TableCell >LCMS-4  (653)</asp:TableCell>
                <asp:TableCell >Yes</asp:TableCell>
                <asp:TableCell >0</asp:TableCell>
                <asp:TableCell >0 min</asp:TableCell>
                <asp:TableCell >WAITING</asp:TableCell>
                <asp:TableCell HorizontalAlign="Left">No queue</asp:TableCell>
                <asp:TableCell HorizontalAlign="Left">Admin</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow5" runat="server" VerticalAlign="Middle" HorizontalAlign="Center" style="color:lime; font-size:10pt; font-family:Consolas;">
                <asp:TableCell >LCMS-5  (653)</asp:TableCell>
                <asp:TableCell >Yes</asp:TableCell>
                <asp:TableCell >0</asp:TableCell>
                <asp:TableCell >0 min</asp:TableCell>
                <asp:TableCell >WAITING</asp:TableCell>
                <asp:TableCell HorizontalAlign="Left">No queue</asp:TableCell>
                <asp:TableCell HorizontalAlign="Left">Admin</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow6" runat="server" VerticalAlign="Middle" HorizontalAlign="Center" style="color:lime; font-size:10pt; font-family:Consolas;">
                <asp:TableCell >LCMS-6  (612)</asp:TableCell>
                <asp:TableCell >Yes</asp:TableCell>
                <asp:TableCell >0</asp:TableCell>
                <asp:TableCell >0 min</asp:TableCell>
                <asp:TableCell >WAITING</asp:TableCell>
                <asp:TableCell HorizontalAlign="Left">No queue</asp:TableCell>
                <asp:TableCell HorizontalAlign="Left">Admin</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow7" runat="server" VerticalAlign="Middle" HorizontalAlign="Center" style="color:lime; font-size:10pt; font-family:Consolas;">
                <asp:TableCell >LCMS-7  (641)</asp:TableCell>
                <asp:TableCell >Yes</asp:TableCell>
                <asp:TableCell >0</asp:TableCell>
                <asp:TableCell >0 min</asp:TableCell>
                <asp:TableCell >WAITING</asp:TableCell>
                <asp:TableCell HorizontalAlign="Left">No queue</asp:TableCell>
                <asp:TableCell HorizontalAlign="Left">Admin</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow8" runat="server" VerticalAlign="Middle" HorizontalAlign="Center" style="color:lime; font-size:10pt; font-family:Consolas;">
                <asp:TableCell >LCMS-8  (612)</asp:TableCell>
                <asp:TableCell >Yes</asp:TableCell>
                <asp:TableCell >0</asp:TableCell>
                <asp:TableCell >0 min</asp:TableCell>
                <asp:TableCell >WAITING</asp:TableCell>
                <asp:TableCell HorizontalAlign="Left">No queue</asp:TableCell>
                <asp:TableCell HorizontalAlign="Left">Admin</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow9" runat="server" VerticalAlign="Middle" HorizontalAlign="Center" style="color:lime; font-size:10pt; font-family:Consolas;">
                <asp:TableCell >LCMS-11 (612)</asp:TableCell>
                <asp:TableCell >Yes</asp:TableCell>
                <asp:TableCell >0</asp:TableCell>
                <asp:TableCell >0 min</asp:TableCell>
                <asp:TableCell >WAITING</asp:TableCell>
                <asp:TableCell HorizontalAlign="Left">No queue</asp:TableCell>
                <asp:TableCell HorizontalAlign="Left">Admin</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow10" runat="server" VerticalAlign="Middle" HorizontalAlign="Center" style="color:lime; font-size:10pt; font-family:Consolas;">
                <asp:TableCell >LCMS-12 (641)</asp:TableCell>
                <asp:TableCell >Yes</asp:TableCell>
                <asp:TableCell >0</asp:TableCell>
                <asp:TableCell >0 min</asp:TableCell>
                <asp:TableCell >WAITING</asp:TableCell>
                <asp:TableCell HorizontalAlign="Left">No queue</asp:TableCell>
                <asp:TableCell HorizontalAlign="Left">Admin</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow11" runat="server" VerticalAlign="Middle" HorizontalAlign="Center" style="color:lime; font-size:10pt; font-family:Consolas;">
                <asp:TableCell >LCMS-13 (612)</asp:TableCell>
                <asp:TableCell >Yes</asp:TableCell>
                <asp:TableCell >0</asp:TableCell>
                <asp:TableCell >0 min</asp:TableCell>
                <asp:TableCell >WAITING</asp:TableCell>
                <asp:TableCell HorizontalAlign="Left">No queue</asp:TableCell>
                <asp:TableCell HorizontalAlign="Left">Admin</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
            </asp:TableRow>
        </asp:Table>
          <hr style="border-top: 3px solid lightgrey; height:10px" />  
         </div>
         <div id="prep" >
        <asp:Label ID="Label4" runat="server" Text="PREP LC-MS" style="color:lightgrey; font-size:11pt; font-family:Consolas; font-weight:bold;"></asp:Label>
        <asp:Table ID="Table2" 
            runat="server" 
            Font-Size="10"          
            Width="100%" 
            Font-Names="Consolas" 
            VerticalAlign="Middle" 
            HorizontalAlign="Center"
            Gridlines="Both"
            >
            <asp:TableHeaderRow 
                runat="server" 
                Font-Bold="true"
                Font-UnderLine="true"
                Style="padding-top:100px;color:lightgrey;">
                <asp:TableHeaderCell>Instr</asp:TableHeaderCell>
                <asp:TableHeaderCell>Avail</asp:TableHeaderCell>
                <asp:TableHeaderCell>Queue</asp:TableHeaderCell>
                <asp:TableHeaderCell>Est Time</asp:TableHeaderCell>
                <asp:TableHeaderCell>Status</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Sample</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">User</asp:TableHeaderCell>   
                <asp:TableHeaderCell >Method</asp:TableHeaderCell>
                <asp:TableHeaderCell >Flowrate</asp:TableHeaderCell>
                <asp:TableHeaderCell >%B</asp:TableHeaderCell>
                <asp:TableHeaderCell >ProcessOpts</asp:TableHeaderCell>
                <asp:TableHeaderCell >Runs</asp:TableHeaderCell>
                <asp:TableHeaderCell >DetectState</asp:TableHeaderCell>
                <asp:TableHeaderCell >Last Update</asp:TableHeaderCell>
            </asp:TableHeaderRow>
            <asp:TableRow ID="TableRow12" runat="server" VerticalAlign="Middle" HorizontalAlign="Center" style="color:lime; font-size:10pt; font-family:Consolas;">
                <asp:TableCell >Prep-1</asp:TableCell>
                <asp:TableCell >Yes</asp:TableCell>
                <asp:TableCell >0</asp:TableCell>
                <asp:TableCell >0 min</asp:TableCell>
                <asp:TableCell >WAITING</asp:TableCell>
                <asp:TableCell HorizontalAlign="Left">No queue</asp:TableCell>
                <asp:TableCell HorizontalAlign="Left">Admin</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow13" runat="server" VerticalAlign="Middle" HorizontalAlign="Center" style="color:lime; font-size:10pt; font-family:Consolas;">
                <asp:TableCell >Prep-2</asp:TableCell>
                <asp:TableCell >Yes</asp:TableCell>
                <asp:TableCell >0</asp:TableCell>
                <asp:TableCell >0 min</asp:TableCell>
                <asp:TableCell >WAITING</asp:TableCell>
                <asp:TableCell HorizontalAlign="Left">No queue</asp:TableCell>
                <asp:TableCell HorizontalAlign="Left">Admin</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow14" runat="server" VerticalAlign="Middle" HorizontalAlign="Center" style="color:lime; font-size:10pt; font-family:Consolas;">
                <asp:TableCell >Prep-3</asp:TableCell>
                <asp:TableCell >Yes</asp:TableCell>
                <asp:TableCell >0</asp:TableCell>
                <asp:TableCell >0 min</asp:TableCell>
                <asp:TableCell >WAITING</asp:TableCell>
                <asp:TableCell HorizontalAlign="Left">No queue</asp:TableCell>
                <asp:TableCell HorizontalAlign="Left">Admin</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow15" runat="server" VerticalAlign="Middle" HorizontalAlign="Center" style="color:lime; font-size:10pt; font-family:Consolas;">
                <asp:TableCell >Prep-4</asp:TableCell>
                <asp:TableCell >Yes</asp:TableCell>
                <asp:TableCell >0</asp:TableCell>
                <asp:TableCell >0 min</asp:TableCell>
                <asp:TableCell >WAITING</asp:TableCell>
                <asp:TableCell HorizontalAlign="Left">No queue</asp:TableCell>
                <asp:TableCell HorizontalAlign="Left">Admin</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow16" runat="server" VerticalAlign="Middle" HorizontalAlign="Center" style="color:lime; font-size:10pt; font-family:Consolas;">
                <asp:TableCell >Prep-5</asp:TableCell>
                <asp:TableCell >Yes</asp:TableCell>
                <asp:TableCell >0</asp:TableCell>
                <asp:TableCell >0 min</asp:TableCell>
                <asp:TableCell >WAITING</asp:TableCell>
                <asp:TableCell HorizontalAlign="Left">No queue</asp:TableCell>
                <asp:TableCell HorizontalAlign="Left">Admin</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow17" runat="server" VerticalAlign="Middle" HorizontalAlign="Center" style="color:lime; font-size:10pt; font-family:Consolas;">
                <asp:TableCell >Prep-6</asp:TableCell>
                <asp:TableCell >Yes</asp:TableCell>
                <asp:TableCell >0</asp:TableCell>
                <asp:TableCell >0 min</asp:TableCell>
                <asp:TableCell >WAITING</asp:TableCell>
                <asp:TableCell HorizontalAlign="Left">No queue</asp:TableCell>
                <asp:TableCell HorizontalAlign="Left">Admin</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow18" runat="server" VerticalAlign="Middle" HorizontalAlign="Center" style="color:lime; font-size:10pt; font-family:Consolas;">
                <asp:TableCell >Prep-7</asp:TableCell>
                <asp:TableCell >Yes</asp:TableCell>
                <asp:TableCell >0</asp:TableCell>
                <asp:TableCell >0 min</asp:TableCell>
                <asp:TableCell >WAITING</asp:TableCell>
                <asp:TableCell HorizontalAlign="Left">No queue</asp:TableCell>
                <asp:TableCell HorizontalAlign="Left">Admin</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow19" runat="server" VerticalAlign="Middle" HorizontalAlign="Center" style="color:lime; font-size:10pt; font-family:Consolas;">
                <asp:TableCell >Prep-8</asp:TableCell>
                <asp:TableCell >Yes</asp:TableCell>
                <asp:TableCell >0</asp:TableCell>
                <asp:TableCell >0 min</asp:TableCell>
                <asp:TableCell >WAITING</asp:TableCell>
                <asp:TableCell HorizontalAlign="Left">No queue</asp:TableCell>
                <asp:TableCell HorizontalAlign="Left">Admin</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow20" runat="server" VerticalAlign="Middle" HorizontalAlign="Center" style="color:lime; font-size:10pt; font-family:Consolas;">
                <asp:TableCell >Prep-9</asp:TableCell>
                <asp:TableCell >Yes</asp:TableCell>
                <asp:TableCell >0</asp:TableCell>
                <asp:TableCell >0 min</asp:TableCell>
                <asp:TableCell >WAITING</asp:TableCell>
                <asp:TableCell HorizontalAlign="Left">No queue</asp:TableCell>
                <asp:TableCell HorizontalAlign="Left">Admin</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
                <asp:TableCell >N/A</asp:TableCell>
            </asp:TableRow>
        </asp:Table>
          <hr style="border-top: 3px solid lightgrey; height:10px" />  
         </div>
        </asp:Panel>
    </form>


</body>
</html>

