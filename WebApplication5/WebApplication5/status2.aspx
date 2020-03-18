<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="status2.aspx.vb" Inherits="WebApplication5.status2" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta http-equiv="refresh" content="600" />

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <title>Instrument Status</title>
    <style>
        body {
          margin: 0;
          font-family: Arial, Helvetica, sans-serif;
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
      <a href="status2.aspx" class="active">Status</a>
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
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div>
            &nbsp;&nbsp;&nbsp;
            <br />
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
                SelectCommand="SELECT INSTRUMENT_NAME FROM INSTRUMENT_STATUS WHERE (INSTRUMENT_NAME LIKE '%LCMS%') OR (INSTRUMENT_NAME LIKE '%PREP%') ORDER BY INSTRUMENT_NAME">
            </asp:SqlDataSource>

            <br />
            <asp:Panel ID="Panel1" runat="server">
                <asp:Timer ID="Timer1" runat="server" Interval="10000" ></asp:Timer>
                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellPadding="6" DataSourceID="SqlDataSource2" ForeColor="#333333" GridLines="None" PageSize="15">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="INSTRUMENT_ID" HeaderText="INSTRUMENT_ID" SortExpression="INSTRUMENT_ID" />
                        <asp:BoundField DataField="INSTRUMENT_NAME" HeaderText="INSTRUMENT_NAME" SortExpression="INSTRUMENT_NAME" />
                        <asp:BoundField DataField="STATUS" HeaderText="STATUS" SortExpression="STATUS" />
                        <asp:BoundField DataField="QUEUE" HeaderText="QUEUE" SortExpression="QUEUE" />
                        <asp:BoundField DataField="ESTIMATED_TIME" HeaderText="ESTIMATED_TIME" SortExpression="ESTIMATED_TIME" />
                        <asp:BoundField DataField="EZX_STATUS" HeaderText="EZX_STATUS" SortExpression="EZX_STATUS" />
                        <asp:BoundField DataField="CS_STATUS" HeaderText="CS_STATUS" SortExpression="CS_STATUS" />
                        <asp:BoundField DataField="LAST_UPDATED" HeaderText="LAST_UPDATED" SortExpression="LAST_UPDATED" />
                        <asp:BoundField DataField="NO_RUNS" HeaderText="NO_RUNS" SortExpression="NO_RUNS" />
                        <asp:BoundField DataField="CURRENT_SAMPLEID" HeaderText="CURRENT_SAMPLEID" SortExpression="CURRENT_SAMPLEID" />
                        <asp:BoundField DataField="DETECT_STATE" HeaderText="DETECT_STATE" SortExpression="DETECT_STATE" />
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
            </asp:Panel>

        </div>

        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
            ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
            SelectCommand="SELECT INSTRUMENT_ID, INSTRUMENT_NAME, STATUS, QUEUE, ESTIMATED_TIME, EZX_STATUS, CS_STATUS, LAST_UPDATED, NO_RUNS, CURRENT_SAMPLEID, DETECT_STATE FROM INSTRUMENT_STATUS WHERE (INSTRUMENT_NAME LIKE '%LCMS%') OR (INSTRUMENT_NAME LIKE '%PREP%') ORDER BY INSTRUMENT_NAME"></asp:SqlDataSource>
        <asp:Label ID="Label1" runat="server"></asp:Label>
&nbsp;<br />
    </form>
</body>
</html>

