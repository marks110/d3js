<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="status.aspx.vb" Inherits="WebApplication5.status" %>

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
            Width: 600px; 
            background-color: #073971;
            background-color: #0b2c51;
        }
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
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT INSTRUMENT_NAME, STATUS, QUEUE, ESTIMATED_TIME, CS_STATUS, USERNAME, CURRENT_SAMPLEID, EZX_STATUS, LAST_UPDATED, DETECT_STATE FROM INSTRUMENT_STATUS WHERE (INSTRUMENT_NAME LIKE '%LCMS%') OR (INSTRUMENT_NAME LIKE '%PREP%') ORDER BY INSTRUMENT_NAME"></asp:SqlDataSource>
    <div class="pagediv">
        <div id="lcms" style="width: 100%;  margin: auto;">
            <div>
                <asp:Label ID="Label3" runat="server" Text="ANALYTICAL LC-MS" style="color:lightgrey; font-size:11pt; font-family:Consolas; font-weight:bold;"></asp:Label>
                <asp:Button ID="Button1" runat="server" BackColor="lime" Font-Names="Consolas" ForeColor="Black" Text="Fullview" style="position:relative; float:right; margin-right:5px;" />
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
                Font-Bold="true"
                Font-UnderLine="true"
                padding-top= "100px" 
                Color= "lightgrey" 
                HorizontalAlign="Center">
                <asp:TableHeaderCell style="width: 50px;height: 25px;">Instr</asp:TableHeaderCell>
                <asp:TableHeaderCell style="width: 30px;">Status</asp:TableHeaderCell>
                <asp:TableHeaderCell style="width: 30px;">#</asp:TableHeaderCell>
                <asp:TableHeaderCell style="width: 50px;">Queue</asp:TableHeaderCell>
                <asp:TableHeaderCell style="width: 50px;">User</asp:TableHeaderCell>
                <asp:TableHeaderCell style="width: 50px;">Sample</asp:TableHeaderCell>
            </asp:TableHeaderRow>
            <asp:TableRow ID="TableRow1" runat="server" VerticalAlign="Middle" HorizontalAlign="Left" style="height: 20px; color:white; font-size:10pt; font-family:Consolas;">
                <asp:TableCell>1  (653)</asp:TableCell>
                <asp:TableCell>Yes</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">0</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">0</asp:TableCell>
                <asp:TableCell>Admin</asp:TableCell>
                <asp:TableCell>No queue</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow2" runat="server" VerticalAlign="Middle" HorizontalAlign="Left" style="height: 20px; color:white; font-size:10pt; font-family:Consolas;">
                <asp:TableCell>2  (653)</asp:TableCell>
                <asp:TableCell>Yes</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">0</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">0</asp:TableCell>
                <asp:TableCell>Admin</asp:TableCell>
                <asp:TableCell>No queue</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow3" runat="server" VerticalAlign="Middle" HorizontalAlign="Left" style="height: 20px; color:white; font-size:10pt; font-family:Consolas;">
                <asp:TableCell>3  (641)</asp:TableCell>
                <asp:TableCell>Yes</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">0</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">0</asp:TableCell>
                <asp:TableCell>Admin</asp:TableCell>
                <asp:TableCell>No queue</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow4" runat="server" VerticalAlign="Middle" HorizontalAlign="Left" style="height: 20px; color:white; font-size:10pt; font-family:Consolas;">
                <asp:TableCell>4  (653)</asp:TableCell>
                <asp:TableCell>Yes</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">0</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">0</asp:TableCell>
                <asp:TableCell>Admin</asp:TableCell>
                <asp:TableCell>No queue</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow5" runat="server" VerticalAlign="Middle" HorizontalAlign="Left" style="height: 20px; color:white; font-size:10pt; font-family:Consolas;">
                <asp:TableCell>5  (653)</asp:TableCell>
                <asp:TableCell>Yes</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">0</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">0</asp:TableCell>
                <asp:TableCell>Admin</asp:TableCell>
                <asp:TableCell>No queue</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow6" runat="server" VerticalAlign="Middle" HorizontalAlign="Left" style="height: 20px; color:white; font-size:10pt; font-family:Consolas;">
                <asp:TableCell>6  (612)</asp:TableCell>
                <asp:TableCell>Yes</asp:TableCell>
                 <asp:TableCell HorizontalAlign="Center">0</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">0</asp:TableCell>
                <asp:TableCell>Admin</asp:TableCell>
                <asp:TableCell>No queue</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow7" runat="server" VerticalAlign="Middle" HorizontalAlign="Left" style="height: 20px; color:white; font-size:10pt; font-family:Consolas;">
                <asp:TableCell>7  (641)</asp:TableCell>
                <asp:TableCell>Yes</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">0</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">0</asp:TableCell>
                <asp:TableCell>Admin</asp:TableCell>
                <asp:TableCell>No queue</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow8" runat="server" VerticalAlign="Middle" HorizontalAlign="Left" style="height: 20px; color:white; font-size:10pt; font-family:Consolas;">
                <asp:TableCell>8  (612)</asp:TableCell>
                <asp:TableCell>Yes</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">0</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">0</asp:TableCell>
                <asp:TableCell>Admin</asp:TableCell>
                <asp:TableCell>No queue</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow9" runat="server" VerticalAlign="Middle" HorizontalAlign="Left" style="height: 20px; color:white; font-size:10pt; font-family:Consolas;">
                <asp:TableCell>11 (612)</asp:TableCell>
                <asp:TableCell>Yes</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">0</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">0</asp:TableCell>
                <asp:TableCell>Admin</asp:TableCell>
                <asp:TableCell>No queue</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow10" runat="server" VerticalAlign="Middle" HorizontalAlign="Left" style="height: 20px; color:white; font-size:10pt; font-family:Consolas;">
                <asp:TableCell>12 (641)</asp:TableCell>
                <asp:TableCell>Yes</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">0</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">0</asp:TableCell>
                <asp:TableCell>Admin</asp:TableCell>
                <asp:TableCell>No queue</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow11" runat="server" VerticalAlign="Middle" HorizontalAlign="Left" style="height: 20px; color:white; font-size:10pt; font-family:Consolas;">
                <asp:TableCell>13 (612)</asp:TableCell>
                <asp:TableCell>Yes</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">0</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">0</asp:TableCell>
                <asp:TableCell>Admin</asp:TableCell>
                <asp:TableCell>No queue</asp:TableCell>
            </asp:TableRow>
             <asp:TableRow ID="blank" runat="server" VerticalAlign="Middle" HorizontalAlign="Left" style="height: 20px; color:white; font-size:10pt; font-family:Consolas;">
                <asp:TableCell style="color:lightgrey; font-size:11pt; font-family:Consolas; font-weight:bold;">PREP</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow12" runat="server" VerticalAlign="Middle" HorizontalAlign="Left" style="height: 20px; color:white; font-size:10pt; font-family:Consolas;">
                <asp:TableCell>Prep-1</asp:TableCell>
                <asp:TableCell>Yes</asp:TableCell>
                 <asp:TableCell HorizontalAlign="Center">0</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">0</asp:TableCell>
                <asp:TableCell>Admin</asp:TableCell>
                <asp:TableCell>No queue</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow13" runat="server" VerticalAlign="Middle" HorizontalAlign="Left" style="height: 20px; color:white; font-size:10pt; font-family:Consolas;">
                <asp:TableCell>Prep-2</asp:TableCell>
                <asp:TableCell>Yes</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">0</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">0</asp:TableCell>
                <asp:TableCell>Admin</asp:TableCell>
                <asp:TableCell>No queue</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow14" runat="server" VerticalAlign="Middle" HorizontalAlign="Left" style="height: 20px; color:white; font-size:10pt; font-family:Consolas;">
                <asp:TableCell>Prep-3</asp:TableCell>
                <asp:TableCell>Yes</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">0</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">0</asp:TableCell>
                <asp:TableCell>Admin</asp:TableCell>
                <asp:TableCell>No queue</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow15" runat="server" VerticalAlign="Middle" HorizontalAlign="Left" style="height: 20px; color:white; font-size:10pt; font-family:Consolas;">
                <asp:TableCell>Prep-4</asp:TableCell>
                <asp:TableCell>Yes</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">0</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">0</asp:TableCell>
                <asp:TableCell>Admin</asp:TableCell>
                <asp:TableCell>No queue</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow16" runat="server" VerticalAlign="Middle" HorizontalAlign="Left" style="height: 20px; color:white; font-size:10pt; font-family:Consolas;">
                <asp:TableCell>Prep-5</asp:TableCell>
                <asp:TableCell>Yes</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">0</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">0</asp:TableCell>
                <asp:TableCell>Admin</asp:TableCell>
                <asp:TableCell>No queue</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow17" runat="server" VerticalAlign="Middle" HorizontalAlign="Left" style="height: 20px; color:white; font-size:10pt; font-family:Consolas;">
                <asp:TableCell>Prep-6</asp:TableCell>
                <asp:TableCell>Yes</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">0</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">0</asp:TableCell>
                <asp:TableCell>Admin</asp:TableCell>
                <asp:TableCell>No queue</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow18" runat="server" VerticalAlign="Middle" HorizontalAlign="Left" style="height: 20px; color:white; font-size:10pt; font-family:Consolas;">
                <asp:TableCell>Prep-7</asp:TableCell>
                <asp:TableCell>Yes</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">0</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">0</asp:TableCell>
                <asp:TableCell>Admin</asp:TableCell>
                <asp:TableCell>No queue</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow19" runat="server" VerticalAlign="Middle" HorizontalAlign="Left" style="height: 20px; color:white; font-size:10pt; font-family:Consolas;">
                <asp:TableCell>Prep-8</asp:TableCell>
                <asp:TableCell>Yes</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">0</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">0</asp:TableCell>
                <asp:TableCell>Admin</asp:TableCell>
                <asp:TableCell>No queue</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow20" runat="server" VerticalAlign="Middle" HorizontalAlign="Left" style="height: 20px; color:white; font-size:10pt; font-family:Consolas;">
                <asp:TableCell>Prep-9</asp:TableCell>
                <asp:TableCell>Yes</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">0</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">0</asp:TableCell>
                <asp:TableCell>Admin</asp:TableCell>
                <asp:TableCell>No queue</asp:TableCell>
            </asp:TableRow>
        </asp:Table>
         </div>
            <asp:Label ID="Label2" runat="server" Text="Last Updated: " style="float:left; color:white; font-size:10pt; font-family:Consolas;"></asp:Label>
        </div>
  
    </form>


</body>
</html>

