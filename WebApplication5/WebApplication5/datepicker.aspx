<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="datepicker.aspx.vb" Inherits="WebApplication5.datepicker" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">


<head runat="server">  
    <title></title>  
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css">
    <script src="http://code.jquery.com/jquery-1.9.1.js"></script>  
    <script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>  
  
    <!-- Load SCRIPT.JS which will create datepicker for input field  -->  
    <script>  
        $(document).ready(  
  
  /* This is the function that will get executed after the DOM is fully loaded */  
  function () {  
      $("#txtDate1").datepicker({  
          changeMonth: true,//this option for allowing user to select month  
          changeYear: true //this option for allowing user to select from year range  
      });  
      $("#txtDate2").datepicker({  
          changeMonth: true,//this option for allowing user to select month  
          changeYear: true //this option for allowing user to select from year range  
      });  
  }  
  
);  
    </script>  

    <style type="text/css">  
        body {  
            color: #333;  
            text-align: center;  
        }  
    </style>  
</head>  

<body>
    <form id="form1" runat="server">
        <div>
            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True">
                <asp:ListItem>Equal</asp:ListItem>
                <asp:ListItem>Before</asp:ListItem>
                <asp:ListItem>After</asp:ListItem>
                <asp:ListItem>Between</asp:ListItem>
            </asp:DropDownList>
                                        
            <asp:TextBox ID="txtDate1" runat="server"  AutoPostBack="True" /> 
            <asp:TextBox ID="txtDate2" runat="server" Visible="False" /> 
        </div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString2 %>" ProviderName="<%$ ConnectionStrings:ConnectionString2.ProviderName %>" SelectCommand="SELECT UPPER(USERNAME) AS UNAME, SAMPLEID, DATERUN, INSTNAME, CUSTOM1, CUSTOM2, CUSTOM6, CUSTOM7 FROM ANALYTICAL_CC.ENTRIES WHERE (DATERUN &gt;= SYSDATE - 1) ORDER BY ID DESC"></asp:SqlDataSource>
    </form>
</body>
</html>
