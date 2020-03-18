<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="pressure.aspx.vb" Inherits="WebApplication5.pressure" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=10" />
    <meta http-equiv="refresh" content="600" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <title>LCMS Column Pressures</title>
    <style>
        body {
          margin: 0;
          font-family: Consolas;
         background-color: #596779;
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
        .auto-style2 {
            width: 1745px;
            height: 236px;
        }
        .auto-style3 {
            width: 102%;
        }
        .auto-style4 {
            width: 98%;
        }
        </style>
</head>
<body>

    <div class="topnav" id="myTopnav">
      <a href="status.aspx">Status</a>
      <a href="pressure.aspx" class="active">Pressure</a>
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
        <asp:Panel ID="Panel1" runat="server" Width="1750px" Height="242px" BackColor="#596779">        
            <table class="auto-style4">
                <tr>
                    <td class="auto-style2">
                        <asp:Chart ID="Chart1" runat="server" Height="232px" Width="250px">
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
                        <asp:Chart ID="Chart2a" runat="server" Height="232px" Width="250px">
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
                        <asp:Chart ID="Chart2b" runat="server" Height="232px" Width="250px">
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
                        <asp:Chart ID="Chart3" runat="server" Height="232px" Width="250px">
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
                        <asp:Chart ID="Chart4" runat="server" Height="232px" Width="250px">
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


        <asp:Panel ID="Panel2" runat="server" Width="1500px" BackColor="#596779">
            <table class="auto-style3">
                <tr>
                    <td class="auto-style2">
                        <asp:Chart ID="Chart5a" runat="server" Height="232px" Width="250px">
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
                        <asp:Chart ID="Chart5b" runat="server" Height="232px" Width="250px">
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
                        <asp:Chart ID="Chart6" runat="server" Height="232px" Width="250px">
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
                        <asp:Chart ID="Chart7" runat="server" Height="232px" Width="250px">
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
                        <asp:Chart ID="Chart8" runat="server" Height="232px" Width="250px">
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


        <asp:Panel ID="Panel3" runat="server" Width="1500px" BackColor="#596779">
            <table class="auto-style4">
                <tr>
                    <td class="auto-style2">
                        <asp:Chart ID="Chart9" runat="server" Height="232px" Width="250px">
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
                        <asp:Chart ID="Chart10" runat="server" Height="232px" Width="250px">
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
                        <asp:Chart ID="Chart11" runat="server" Height="232px" Width="250px">
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
                        <asp:Chart ID="Chart12" runat="server" Height="232px" Width="250px">
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
                        <asp:Chart ID="Chart13" runat="server" Height="232px" Width="250px">
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


        <br />
        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
            SelectCommand="SELECT INSTRUMENT_ID,PRESSURE_END,PRESSURE_DATE FROM INSTRUMENT_PRESSURE_LOG WHERE (INSTRUMENT_ID = :ID) ORDER BY PRESSURE_DATE DESC FETCH FIRST 30 ROWS ONLY ">
            <SelectParameters>
                <asp:Parameter Name=":ID" />
           </SelectParameters>
        </asp:SqlDataSource>
    &nbsp;&nbsp;&nbsp;
        <div style="margin-left: 1680px">
        </div>
    </form>
</body>
</html>
