<%@ Page Language="vb" AutoEventWireup="true"  MasterPageFile="~/Site.Master" CodeBehind="processdata.aspx.vb" Inherits="WebApplication5.processdata" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>       
        .loading-spinner {
            z-index: 2000 !important;
            position: fixed;
            left: 0;
            top: 0;
            height: 100vh;
            width: 100%;
            background-color: rgba(0,0,0,0.3);
            transition: visibility 0.15s linear, opacity 0.15s linear;
            visibility: hidden;
        }
        .loading-spinner.show {
           display: flex !important;
           justify-content: center;
            visibility: visible;
            opacity:1;
        }
        .loading-spinner > .KR-spinner {
            display: table !important;
            margin: auto;
        }

        .diplayhide{
           display:none;
        }

        .printWidth {
            width:1141px;
        }       

        .icons {
            font-size: 25px;            
            margin-right: 15px;
            margin-left: 15px;
        }

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
<%--    <link rel="stylesheet" href="./fontawesome-5.8.1/css/all.min.css"/>--%>
    <link href="Content/fontawesome-all.css" rel="stylesheet" />
    <script src="https://d3js.org/d3.v3.min.js"></script>    
    <script src="https://ajax.aspnetcdn.com/ajax/jQuery/jquery-3.4.1.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css"/>       

    <script src="scripts/moment.min.js"></script>  
    <script src="scripts/canvas-toBlob.js"></script>  
    <script src="scripts/FileSaver.min.js"></script> 
    <script src="scripts/rasterizeHTML.allinone.js"></script> 
    <script src="scripts/jspdf.min.js"></script> 



    <script src="Graph.js"></script>    
    

    <div class="d-flex justify-content-center" style="margin-top:10px;user-select: none;" draggable="false">
        <div style="position:relative" draggable="false" id="firstGraph">            
            <svg width="540" height="230" id="d3Chart1">
                <defs id="svg_style">
                    <style>
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

                        .axis path, .axis line {
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
		                    stroke-width: 2px;
	                    } 

                        path.orange {
		                    fill: none;
		                    stroke: orange;
		                    stroke-width: 2px;
	                    } 

                        path.grey {
		                    fill: none;
		                    stroke: grey;
		                    stroke-width: 2px;
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

                        .bar {
                            fill: steelblue;
                            clip-path: url(#clip);
                        }
                    </style>
                </defs>
                <rect id="RecFillByMouseChart1" width="0" height="200" style="fill:rgb(0,0,255);" />
                <rect id="RecFillByMouseChart2" width="0" height="200" style="fill:rgb(0,0,255);" />
            </svg>               
        </div>

        <div class="mt-3" id="secondGraph" draggable="false">
           <svg width="540" height="270" id="d3Chart2"></svg>
           <svg width="540" height="270" id="d3Chart3" style="display:none"></svg>       
        </div>

        <div class="d-flex" style="margin-top:10px; margin-left:20px; display: none" id="divMass">                        
            <label class="input-group-text" id="lbMass" style="color: #337ab7;">Mass</label>            
            <input class="mx-5" type="text" id="tbMass" style="border-color: bisque;height: 31px; vertical-align: middle; text-align: center;  width: 90px;">
            <button type="button" id="btOnOff" class="btn btn-info" style="font-size: 15px;font-weight: bold;">UV on/off</button>                        
            <label class="input-group-text" id="lbCount" style="margin-left:30px; color: #ee0b0b;">0</label>            
            <p id="btAdd" class="fa fa-plus icons" title="Add"></p>
            <p id="btClear" class="fa fa-trash icons" title="Clear"></p>		    
            <p id="btDragout" class="fa fa-hand-point-up icons" draggable="true" title="Drag out" style="z-index:1000"></p>
            <p id="btPDF" class="fa fa-download icons" title="Download"></p>
        </div>                  
    </div>

    <div class="mt-3 diplayhide printWidth" id="printPDF" style ="margin-top:50px;">          
        <div id="topTable" style ="margin-bottom:100px; font-size: 20px;">
           <div id="lbID" style="margin-right:60px; font-size: 40px; text-align: right;">23423</div>
           <table style="margin-left:60px; margin-right:60px; width: 90%; border-color: black; border-style: solid;">              
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
        <div id="temp" class="diplayhide printWidth">
            <img id="imageAdd" src="" style="width:100%">
        </div>
        <div id="addGraph" style="margin-left:25px"></div>
    </div>

    <!-- loading spinner -->
    <div class="loading-spinner fade" style="">
        <div class="KR-spinner" style="font-size:20px"><i class="fas fa-sync fa-spin" style="font-size:30px"></i> Waiting ..</div>       
    </div>    
</asp:Content>
