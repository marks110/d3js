<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="errorLog.aspx.vb" Inherits="WebApplication5.errorLog" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=10" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <title>Error Logs</title>
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
      <a href="pressure.aspx">Pressure</a>
      <a href="errorLog.aspx" class="active">ErrorLog</a>
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
            &nbsp;&nbsp;&nbsp;
            <br />

            <asp:Button ID="Button1" runat="server" Text="Delete Selected Rows" />

            <asp:Button ID="Button2" runat="server" Text="Delete All" />

            <br />
            <asp:Panel ID="Panel1" runat="server">
                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSource2" ForeColor="#333333" GridLines="None" PageSize="15" Font-Size="Small">
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

        </div>

        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
            ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
            SelectCommand="SELECT ERROR_TEXT, ERROR_DATE FROM INSTRUMENT_ERROR_LOG ORDER BY ERROR_DATE DESC" 
            DeleteCommand="DELETE FROM INSTRUMENT_ERROR_LOG WHERE ERROR_DATE = :ERROR_DATE">
            <DeleteParameters>
                <asp:Parameter Name=":ERROR_DATE" Type="DateTime"/>
           </DeleteParameters>
        </asp:SqlDataSource>
        <p>
            &nbsp;</p>

    </form>
</body>
</html>


