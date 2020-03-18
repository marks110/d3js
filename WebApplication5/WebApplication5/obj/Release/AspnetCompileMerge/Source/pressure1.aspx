<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="pressure1.aspx.vb" Inherits="WebApplication5.pressure1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="refresh" content="600"></meta>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <title>LCMS Column Pressures</title>
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
      <a href="status.aspx">Status</a>
      <a href="pressure1.aspx" class="active">Pressure</a>
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
        <div>
        </div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT &quot;ID&quot;, &quot;INSTTYPE&quot;, &quot;INSTNAME&quot; FROM &quot;INSTRUMENTS&quot; WHERE ((&quot;INSTTYPE&quot; = ?) AND (&quot;INSTTYPE&quot; = ?))">
            <SelectParameters>
                <asp:Parameter DefaultValue="LCMS" Name="INSTTYPE" Type="String" />
                <asp:Parameter DefaultValue="PrepLCMS" Name="INSTTYPE2" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
            SelectCommand="SELECT INSTNAME FROM ANALYTICAL_CC.INSTRUMENTS WHERE (INSTTYPE = 'LCMS') ORDER BY INSTNAME"></asp:SqlDataSource>
        <asp:Panel ID="Panel1" runat="server" Width="1764px">
            <table style="width:100%;">
                <tr>
                    <td class="auto-style2">
                        <asp:Chart ID="Chart1" runat="server" Height="740px" Width="1417px" CssClass="auto-style2">
                            <Legends>
                                <asp:Legend BackColor="Transparent" Enabled="True" Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="False" Name="Default" TitleFont="Microsoft Sans Serif, 8pt, style=Bold">
                                </asp:Legend>
                            </Legends>
                            <Series>
                                <asp:Series ChartType="Line">
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </td>
                    <td class="auto-style3">
                        <asp:ListBox ID="ListBox1" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource2" DataTextField="INSTNAME" DataValueField="INSTNAME" SelectionMode="Multiple" Width="232px" Rows="14"></asp:ListBox>
                        <br />
                        <br />
                        <asp:Calendar ID="Calendar1" runat="server" OnSelectionChanged="DateChange1">
                        </asp:Calendar>
                        <asp:Label ID="Label1" runat="server" Text="Start Date"></asp:Label>
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        <br />
                        <asp:Calendar ID="Calendar2" runat="server" OnSelectionChanged="DateChange2" Visible="True">
                        </asp:Calendar>
                        <asp:Label ID="Label2" runat="server" Text="End Date"></asp:Label>
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                        <br />
                        <br />
                        <br />
                        <asp:Button ID="Button1" runat="server" Text="Reset Graph" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </asp:Panel>
        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
            SelectCommand="SELECT INSTRUMENT_ID,PRESSURE_END,PRESSURE_DATE FROM INSTRUMENT_PRESSURE_LOG WHERE (INSTRUMENT_ID = :ID) AND  PRESSURE_DATE >= :StartDate AND PRESSURE_DATE <= :EndDate ">
            <SelectParameters>
                <asp:Parameter Name=":ID" />
                <asp:Parameter Name=":StartDate"  Type="DateTime"/>
                <asp:Parameter Name=":EndDate"  Type="DateTime"/>
           </SelectParameters>
        </asp:SqlDataSource>
    &nbsp;&nbsp;&nbsp;
        <div style="margin-left: 1680px">
        </div>
    </form>
</body>
</html>
