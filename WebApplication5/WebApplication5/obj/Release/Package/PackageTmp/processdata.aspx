<%@ Page Language="vb" AutoEventWireup="true"  MasterPageFile="~/Site.Master" CodeBehind="processdata.aspx.vb" Inherits="WebApplication5.processdata" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .btn:focus {
          outline: none;
        }

        .area {
          fill: steelblue;
          clip-path: url(#clip);
        }

        .zoom {
          cursor: move;
          fill: none;
          pointer-events: all;
        }  

        svg {
          font: 10px sans-serif;
        }


        .bar {
          fill: steelblue;
          clip-path: url(#clip);
        }
  
        .subBar { 
          fill: gray;
          opacity: 0.5;
        }

        .axis path,
        .axis line {
          fill: none;
          stroke: #000;
          shape-rendering: crispEdges;
        }

        .brush .extent {
          stroke: #fff;
          fill: steelblue;
          fill-opacity: .25;
          shape-rendering: crispEdges;
        }
        
	    text {
		    font-family: arial;
		    font-size: 11px;
	    }
        
        .label {
		    font-size: 11px !important;
            font-weight: normal !important;
	    }
	     
	    path.green {
		    fill: none;
		    stroke: green;
		    stroke-width: 1px;
	    } 

        path.orange {
		    fill: none;
		    stroke: orange;
		    stroke-width: 1px;
	    } 

        path.grey {
		    fill: none;
		    stroke: grey;
		    stroke-width: 1px;
	    } 

        path.blue {
		    fill: none;
		    stroke: blue;
		    stroke-width: 1px;
	    } 
	    
		.axis path,
		.axis line {
		  fill: none;
		  stroke: slategray;
		  shape-rendering: crispEdges;
		}
        @media print {
          @page { margin: 0; }
          body { margin: 20px; }
        }

        td {
            width: 50%;
            padding: 6px !important;                     
            border-color: darkgray;
            border-style: solid;
            border-width: 2px;
        }
    </style>
    
    <script src="https://d3js.org/d3.v3.min.js"></script>    
    <script src="https://ajax.aspnetcdn.com/ajax/jQuery/jquery-3.4.1.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css"/>       
    <script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.24.0/moment.min.js"></script>
    <script src="Graph.js"></script>    
    <div class="d-flex justify-content-center" style="margin-top:20px;user-select: none;" draggable="false">
        <div id="topTable" style ="margin-bottom:30px; display:none">
           <div id="lbID" style="margin-left: 630px; font-size: 23px;">23423</div>
           <table style=" margin-left: 60px; width: 640px; border-color: black; border-style: solid;">              
              <tr>
                <td id="sampleID">Sample ID : 23423</td>
                <td id="dateAcquired">Date Acquired : 02/03/2020</td>                
              </tr>
              <tr>
                <td id="username">Username : sysadmin</td>
                <td id="dateReprocessed">Date Reprocessed : 1/27/2020 7:21:20 AM</td> 
              </tr>
               <tr>
                <td id="commnet">Commnet : blank</td>
                <td id="instrument">Instrument : LCMS-1</td>                
              </tr>              
            </table>
        </div>
        <label class="input-group-text" style="margin-left:50px; color: #ADD8E6;">Mouse/Drag to integrate</label>            
        <label class="input-group-text" style="margin-left:50px; color: #ee0b0b;">Ctrl Mouse/Drag to Subtract</label>      
        <div style="position:relative" draggable="false">            
            <svg width="700" height="300" id="d3Chart1"> 
                <rect id="RecFillByMouseChart1" width="0" height="260" style="fill:rgb(0,0,255);" />
                <rect id="RecFillByMouseChart2" width="0" height="260" style="fill:rgb(0,0,255);" />                
            </svg>               
            <%--<div id="RecFillByMouseChart1" 
                style="position:absolute;background-color: rgba(128,128,255,0.5); width: 10px; height: 260px; top:0px;left:0px;">
            </div>--%>
        </div>

        <div class="mt-3" id="secondGraph" draggable="false">
           <svg width="700" height="350" id="d3Chart2"></svg>
           <svg width="700" height="350" id="d3Chart3" style="display:none"></svg>       
        </div>

        <div class="d-flex" style="margin-left:20px; display: none" id="divMass">                        
            <label class="input-group-text" id="lbMass" style="color: #337ab7;">Mass</label>            
            <input class="mx-5" type="text" id="tbMass" style="border-color: bisque;height: 31px; vertical-align: middle; text-align: center;  width: 150px;">
            <button type="button" id="btOnOff" class="btn btn-info" style="font-size: 15px;font-weight: bold;">UV on/off</button>            
            
            <label class="input-group-text" id="lbCount" style="margin-left:50px; color: #ee0b0b;">0</label>            
            <button type="button" id="btAdd" class="btn btn-info" style="width:100px; font-size: 15px;font-weight: bold;">Add</button>
            <button type="button" id="btClear" class="btn btn-info" style="width:100px; font-size: 15px;font-weight: bold;">Clear</button>            
            <button type="button" id="btPDF" class="btn btn-info" style="width:100px; font-size: 15px;font-weight: bold;">SavePDF</button>            
        </div>          
        <div class="mt-3" id="secondGraphAdd" draggable="false" style="display:none">           
        </div>        
    </div>   
</asp:Content>