var gLeft = 70;
var gRight = 10;

function DrawUV220(data, width, height, svg) {

    var max = d3.max(data, function (d) { return d.PercentIntensity; });
    var minX = d3.min(data, function (d) { return d.UVretTimes; });
    var maxX = d3.max(data, function (d) { return d.UVretTimes; });

    var y = d3.scale.linear()
        .domain([0, max])
        .range([height, 0]);

    var x = d3.scale.linear()
        .domain([minX, maxX])
        .range([0, width]);

    var yAxis = d3.svg.axis()
        .orient("left")
        .scale(y);

    var xAxis = d3.svg.axis()
        .orient("bottom")
        .scale(x);

    var line = d3.svg.line()
        .x(function (d) { return x(d.UVretTimes); })
        .y(function (d) { return y(d.PercentIntensity); })
        .interpolate("cardinal");

    var chartGroup = svg.append("g");

    chartGroup.append("path")
        .attr("class", "green")
        .attr("d", function (d) { return line(data); })


    chartGroup.append("g")
        .attr("class", "axis x")
        .attr("transform", "translate(0," + height + ")")
        .call(xAxis);

    chartGroup.append("g")
        .attr("class", "axis y")
        .call(yAxis);

    var rX = 300;
    var rY = 20;
    var dx = 60;
    var radius = 5;

    svg.append("circle").attr("cx", rX).attr("cy", rY).attr("r", radius).style("fill", "green").attr("class", "green")
    svg.append("circle").attr("cx", rX + dx).attr("cy", rY).attr("r", radius).style("fill", "orange").attr("class", "orange")
    svg.append("circle").attr("cx", rX + 2 * dx).attr("cy", rY).attr("r", radius).style("fill", "grey")
    svg.append("text").attr("x", rX + 10).attr("y", rY).text("UV220").style("font-size", "12px").attr("alignment-baseline", "middle").attr("class", "green")
    svg.append("text").attr("x", rX + dx + 10).attr("y", rY).text("UV254").style("font-size", "12px").attr("alignment-baseline", "middle").attr("class", "orange")
    svg.append("text").attr("x", rX + 2 * dx + 10).attr("y", rY).text("TIC").style("font-size", "12px").attr("alignment-baseline", "middle")
}
function DrawUV254(data, width, height, svg) {

    var max = d3.max(data, function (d) { return d.PercentIntensity; });
    var minX = d3.min(data, function (d) { return d.UVretTimes; });
    var maxX = d3.max(data, function (d) { return d.UVretTimes; });

    var y = d3.scale.linear()
        .domain([0, max])
        .range([height, 0]);

    var x = d3.scale.linear()
        .domain([minX, maxX])
        .range([0, width]);

    var line = d3.svg.line()
        .x(function (d) { return x(d.UVretTimes); })
        .y(function (d) { return y(d.PercentIntensity); })
        .interpolate("cardinal");

    var chartGroup = svg.append("g");

    chartGroup.append("path")
        .attr("class", "orange")
        .attr("d", function (d) { return line(data); })
}
function DrawTIC(data, width, height, svg, maxIntensityTIC) {

    var max = d3.max(data, function (d) { return d.PercentIntensity; });
    var minX = d3.min(data, function (d) { return d.RetTimes; });
    var maxX = d3.max(data, function (d) { return d.RetTimes; });

    var y = d3.scale.linear()
        .domain([0, max])
        .range([height, 0]);

    var x = d3.scale.linear()
        .domain([minX, maxX])
        .range([0, width]);

    var line = d3.svg.line()
        .x(function (d) { return x(d.RetTimes); })
        .y(function (d) { return y(d.PercentIntensity); })
        .interpolate("cardinal");

    var chartGroup = svg.append("g");

    chartGroup.append("path")
        .attr("class", "grey")
        .attr("d", function (d) { return line(data); })

    var rX = 280;
    var rY = 0;
    svg.append("text").attr("x", rX + 20).attr("y", rY).text("TIC MAX:").style("font-size", "12px").style("fill", "red").attr("alignment-baseline", "middle")
    svg.append("text").attr("id", "graph1Max").attr("x", rX + 80).attr("y", rY).text(maxIntensityTIC.toExponential(1)).style("font-size", "12px;").attr("alignment-baseline", "middle")

}
function DrawMS(data, maxInt, minInt) {

    $("#d3Chart2 > g").remove();
    $("#d3Chart2 > .label").remove();

    var margin = { top: 10, right: gRight, bottom: 60, left: gLeft },
        margin2 = { top: 220, right: gRight, bottom: 20, left: gLeft },
        width = 540 - margin.left - margin.right,
        height = 270 - margin.top - margin.bottom,
        height2 = 270 - margin2.top - margin2.bottom;

    var x = d3.scale.ordinal().rangeBands([0, width], .1),
        x2 = d3.scale.ordinal().rangeBands([0, width], .1),
        y = d3.scale.linear().range([height, 0]),
        y2 = d3.scale.linear().range([height2, 0]);

    var xAxis = d3.svg.axis().scale(x).orient("bottom"),
        xAxis2 = d3.svg.axis().scale(x2).orient("bottom").tickValues([]),
        yAxis = d3.svg.axis().scale(y).orient("left");

    x.domain(data.map(function (d) { return d.mass }));
    y.domain([0, d3.max(data, function (d) { return d.intensity; })]);
    x2.domain(x.domain());
    y2.domain(y.domain());

    var brush = d3.svg.brush()
        .x(x2)
        .on("brush", brushed);

    var svg = d3.select("#d3Chart2")
        .attr("width", width + margin.left + margin.right)
        .attr("height", height + margin.top + margin.bottom);

    var focus = svg.append("g")
        .attr("class", "focus")
        .attr("transform", "translate(" + margin.left + "," + margin.top + ")");

    var context = svg.append("g")
        .attr("class", "context")
        .attr("transform", "translate(" + margin2.left + "," + margin2.top + ")");

    focus.append("g")
        .attr("class", "x axis")
        .attr("transform", "translate(0," + height + ")")
        .call(xAxis);

    focus.append("g")
        .attr("class", "y axis")
        .call(yAxis);

    //console.log(x(data[0].mass))
    enter(data)
    updateScale(data)

    var subBars = context.selectAll('.subBar')
        .data(data)

    subBars.enter().append("rect")
        .classed('subBar', true)
        .attr(
            {
                height: function (d) {
                    return height2 - y2(d.intensity);
                },
                width: function (d) { return x.rangeBand() },
                x: function (d) {

                    return x2(d.mass);
                },
                y: function (d) {
                    return y2(d.intensity)
                }
            })

    context.append("g")
        .attr("class", "x axis")
        .attr("transform", "translate(0," + height2 + ")")
        .call(xAxis2);

    context.append("g")
        .attr("class", "x brush")
        .call(brush)
        .selectAll("rect")
        .attr("y", -6)
        .attr("height", height2 + 7);

    var values = [100, 200, 300, 400, 500, 600, 700, 800, 900];
    context.select(".x.axis").call(xAxis.tickValues(values));

    svg.selectAll(".text")
        .data(data)
        .enter()
        .append("text")
        .attr("class", "label")
        .attr("x", (function (d) { return x(d.mass) + gLeft }))
        .attr("y", function (d) { return y(d.intensity) + 1; })
        .attr("dy", ".75em")
        .text(function (d) {
            var PercentIntensity = (d.intensity - minInt) / (maxInt - minInt) * 100;
            if (PercentIntensity > 20)
                //return d.mass;
                return d.massV;
            else
                return "";
        });

    var minX = d3.min(data, function (d) { return d.mass; });
    var maxX = d3.max(data, function (d) { return d.mass; });

    function brushed() {
        var selected = null;
        selected = x2.domain()
            .filter(function (d) {
                return (brush.extent()[0] <= x2(d)) && (x2(d) <= brush.extent()[1]);
            });

        var start;
        var end;


        if (brush.extent()[0] != brush.extent()[1]) {
            start = selected[0];
            end = selected[selected.length - 1] + 1;
        } else {
            start = minX;
            end = maxX;
        }

        var updatedData = data.filter(function (d) {
            return d.mass >= start && d.mass <= end
        });

        update(updatedData);
        enter(updatedData);
        exit(updatedData);
        updateScale(updatedData)
    }

    function updateScale(data) {

        var tickScale = d3.scale.pow().range([data.length / 5, 0]).domain([data.length, 0]).exponent(.5)

        var brushValue = brush.extent()[1] - brush.extent()[0];
        if (brushValue === 0) {
            brushValue = width;
        }

        var tickValueMultiplier = Math.ceil(Math.abs(tickScale(brushValue)));
        tickValueMultiplier = tickValueMultiplier;

        var filteredTickValues = data.filter(function (d, i) {
            return i % tickValueMultiplier === 0
        }).map(function (d) {
            return d.mass
        })

        focus.select(".x.axis").call(xAxis.tickValues(filteredTickValues));
        focus.select(".y.axis").call(yAxis);

        $('#d3Chart2 .x.axis text').each(function () {
            var eachVal = $(this).text();
            eachVal = Number(eachVal).toFixed();
            $(this).text(eachVal);
        });

        var rX = 400;
        var rY = 20;
        svg.selectAll("#graph2MaxLavel").remove();
        svg.selectAll("#graph2MaxValue").remove();
        var maxY = d3.max(data, function (d) { return d.intensity; });
        maxY = +maxY.toFixed(3);
        svg.append("text").attr("id", "graph2MaxLavel").attr("x", rX + 20).attr("y", rY).text("MAX:").style("font-size", "12px").style("fill", "red").attr("alignment-baseline", "middle")
        svg.append("text").attr("id", "graph2MaxValue").attr("x", rX + 55).attr("y", rY).text(maxY.toExponential(1)).style("font-size", "12px;").attr("alignment-baseline", "middle")
       
    }

    function update(data) {
        x.domain(data.map(function (d) { return d.mass }));
        y.domain([0, d3.max(data, function (d) { return d.intensity; })]);

        var bars = focus.selectAll('.bar')
            .data(data)
        bars
            .attr(
                {
                    height: function (d, i) {
                        return height - y(d.intensity);
                    },
                    width: function (d) {
                        return "1"
                    },
                    x: function (d) {

                        return x(d.mass);
                    },
                    y: function (d) {
                        return y(d.intensity)
                    }
                })

    }

    function exit(data) {
        var bars = focus.selectAll('.bar').data(data)
        bars.exit().remove()
        svg.selectAll(".label").remove();

        svg.selectAll(".text")
            .data(data)
            .enter()
            .append("text")
            .attr("class", "label")
            .attr("x", (function (d) { return x(d.mass) + gLeft }))
            .attr("y", function (d) { return y(d.intensity) + 1; })
            .attr("dy", ".75em")
            .text(function (d) {
                var PercentIntensity = (d.intensity - minInt) / (maxInt - minInt) * 100;
                if (PercentIntensity > 20)
                    return d.massV;
                else
                    return "";
            });

    }

    function enter(data) {
        x.domain(data.map(function (d) { return d.mass }));
        y.domain([0, d3.max(data, function (d) { return d.intensity; })]);

        var bars = focus.selectAll('.bar')
            .data(data)
        bars.enter().append("rect")
            .classed('bar', true)
            .attr(
                {
                    height: function (d, i) {
                        return height - y(d.intensity);
                    },
                    width: function (d) {
                        return "1"
                    },
                    x: function (d) {

                        return x(d.mass);
                    },
                    y: function (d) {
                        return y(d.intensity)
                    }
                })
    }

}
function DrawLC(data, maxIntensity) {
    var margin = { top: 10, right: gRight, bottom: 30, left: gLeft },
        width = 460 - margin.left - margin.right,
        height = 270 - margin.top - margin.bottom;

    width = 460;
    height = 200;
    // append the svg object to the body of the page
    var svg = d3.select("#d3Chart3")
        .append("svg")
        .attr("width", width + margin.left + margin.right)
        .attr("height", height + margin.top + margin.bottom)
        .append("g")
        .attr("transform",
            "translate(" + margin.left + "," + margin.top + ")");

    var max = d3.max(data, function (d) { return d.PercentIntensity; });
    var minX = d3.min(data, function (d) { return d.RetTimes; });
    var maxX = d3.max(data, function (d) { return d.RetTimes; });

    var y = d3.scale.linear()
        .domain([0, max])
        .range([height, 0]);

    var x = d3.scale.linear()
        .domain([minX, maxX])
        .range([0, width]);

    var yAxis = d3.svg.axis()
        .orient("left")
        .scale(y);

    var xAxis = d3.svg.axis()
        .orient("bottom")
        .scale(x);

    var line = d3.svg.line()
        .x(function (d) { return x(d.RetTimes); })
        .y(function (d) { return y(d.PercentIntensity); })
        .interpolate("cardinal");

    var chartGroup = svg.append("g");

    chartGroup.append("path")
        .attr("class", "blue")
        .attr("d", function (d) { return line(data); })


    chartGroup.append("g")
        .attr("class", "axis x")
        .attr("transform", "translate(0," + height + ")")
        .call(xAxis);

    chartGroup.append("g")
        .attr("class", "axis y")
        .call(yAxis);

    var rX = 280;
    var rY = 0;
    svg.append("text").attr("x", rX + 20).attr("y", rY).text("MAX:").style("font-size", "12px").style("fill", "red").attr("alignment-baseline", "middle")
    svg.append("text").attr("id", "graph1Max").attr("x", rX + 80).attr("y", rY).text(maxIntensity.toExponential(1)).style("font-size", "12px;").attr("alignment-baseline", "middle")
}

function getSVGString(svgNode) {
    svgNode.setAttribute('xlink', 'http://www.w3.org/1999/xlink');
    var cssStyleText = getCSSStyles(svgNode);
    appendCSS(cssStyleText, svgNode);

    var serializer = new XMLSerializer();
    var svgString = serializer.serializeToString(svgNode);
    svgString = svgString.replace(/(\w+)?:?xlink=/g, 'xmlns:xlink='); // Fix root xlink without namespace
    svgString = svgString.replace(/NS\d+:href/g, 'xlink:href'); // Safari NS namespace fix

    return svgString;

    function getCSSStyles(parentElement) {
        var selectorTextArr = [];

        // Add Parent element Id and Classes to the list
        selectorTextArr.push('#' + parentElement.id);

        // Add Children element Ids and Classes to the list
        var nodes = parentElement.getElementsByTagName("*");
        for (var i = 0; i < nodes.length; i++) {
            var id = nodes[i].id;
            if (!contains('#' + id, selectorTextArr))
                selectorTextArr.push('#' + id);
            var classes = nodes[i].classList;
        }

        // Extract CSS Rules
        var extractedCSSText = "";
        for (var i = 0; i < document.styleSheets.length; i++) {
            var s = document.styleSheets[i];

            try {
                if (!s.cssRules) continue;
            } catch (e) {
                if (e.name !== 'SecurityError') throw e; // for Firefox
                continue;
            }

            var cssRules = s.cssRules;
            for (var r = 0; r < cssRules.length; r++) {
                if (contains(cssRules[r].selectorText, selectorTextArr))
                    extractedCSSText += cssRules[r].cssText;
            }
        }


        return extractedCSSText;

        function contains(str, arr) {
            return arr.indexOf(str) === -1 ? false : true;
        }

    }

    function appendCSS(cssText, element) {
        var styleElement = document.createElement("style");
        styleElement.setAttribute("type", "text/css");
        styleElement.innerHTML = cssText;
    }
}
function svgString2Image(svgString, width, height, format, callback) {
    var format = format ? format : 'png';

    var imgsrc = 'data:image/svg+xml;base64,' + btoa(unescape(encodeURIComponent(svgString))); // Convert SVG string to data URL

    var canvas = document.createElement("canvas");
    var context = canvas.getContext("2d");

    canvas.width = width;
    canvas.height = height;

    $("#imageAdd").attr("src", imgsrc);

    //image.src = imgsrc;
}
function D3GraphToImage(svg) {
    width = 650;
    height = 250;
    var svgString = getSVGString(svg.node());
    svgString2Image(svgString, 2 * width, 2 * height, 'png', save); // passes Blob and filesize String to the callback

    function save(dataBlob, filesize) {
        //saveAs(dataBlob, 'D3 vis exported to PNG.png'); // FileSaver.js function
    }
}

$(document).ready(function () {
    var bOn = true;

    var d3Chart1Draw = function () {
        // set the dimensions and margins of the graph
        var margin = { top: 10, right: 30, bottom: 30, left: gLeft },
            width = 460 - margin.left - margin.right,
            height = 400 - margin.top - margin.bottom;

        width = 460;
        height = 190;
        // append the svg object to the body of the page
        var svg = d3.select("#d3Chart1")
            .append("svg")
            .attr("width", width + margin.left + margin.right)
            .attr("height", height + margin.top + margin.bottom)
            .append("g")
            .attr("transform",
                "translate(" + margin.left + "," + margin.top + ")");

        $.ajax({
            type: "POST",
            url: "processdata.aspx/getChartModelData",
            data: JSON.stringify({ data: 'mydata' }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response != null && response.d != null) {

                    if (response.d === false) {
                        window.location.href = 'processdata.aspx';
                        return;
                    }

                    $("#divMass").show();
                    var pData = response.d;
                    //we need to parse it to JSON 
                    pData = $.parseJSON(pData);
                    jsonDataUV220 = [];
                    jsonDataUV254 = [];

                    for (var i = 0; i < pData['PercentIntensityUV220'].length; i++) {
                        var PercentIntensityUV220 = pData['PercentIntensityUV220'][i];
                        var PercentIntensityUV254 = pData['PercentIntensityUV254'][i];
                        var UVretTimes = pData['UVretTimes'][i];
                        jsonDataUV220[i] = { 'PercentIntensity': PercentIntensityUV220, 'UVretTimes': UVretTimes };
                        jsonDataUV254[i] = { 'PercentIntensity': PercentIntensityUV254, 'UVretTimes': UVretTimes };
                    }

                    jsonDataTIC = [];
                    for (var i = 0; i < pData['PercentIntensityTIC'].length; i++) {
                        var PercentIntensityTIC = pData['PercentIntensityTIC'][i];
                        var RetTimes = pData['RetTimes'][i];
                        jsonDataTIC[i] = { 'PercentIntensity': PercentIntensityTIC, 'RetTimes': RetTimes };
                    }

                    var maxIntensityTIC = pData['maxIntensityTIC'];
                    DrawUV220(jsonDataUV220, width, height, svg);
                    DrawUV254(jsonDataUV254, width, height, svg);
                    DrawTIC(jsonDataTIC, width, height, svg, maxIntensityTIC);
                }

            },
            failure: function (response) {

            },
            error: function (response) {

            }
        });

        var mouse_down = false;
        var mouse_up = false;
        var mouse_move = false;
        var ctrlKey = false;

        var x1 = -1, x2 = -1, cx1 = -1, cx2 = -1;
        var y1 = -1, y2 = -1, cy1 = -1, cy2 = -1;

        $("#d3Chart1").mousedown(function (e) {

            //console.log("Handler for .mousedown() called.");
            var posX = $(this).offset().left,
                posY = $(this).offset().top;

            //console.log((e.pageX - posX) + ' , ' + (e.pageY - posY));
            mouse_down = true; mouse_up = false; mouse_move = false;

            if (window.event.ctrlKey) {
                ctrlKey = true;
                cx1 = e.pageX - posX;
                cy1 = e.pageY - posY;

                $('#RecFillByMouseChart2').attr('x', cx1);
                $("#RecFillByMouseChart2").css("fill", "rgba(0, 0, 0, 0.1)");
            }
            else {
                ctrlKey = false;
                x1 = e.pageX - posX;
                y1 = e.pageY - posY;

                $('#RecFillByMouseChart1').attr('x', x1);
                $("#RecFillByMouseChart1").css("fill", "rgba(0, 0, 0, 0.1)");
            }

            $.ajax({
                type: "POST",
                url: "processdata.aspx/Chart1MouseDown",
                data: JSON.stringify({ x1: parseInt(x1, 10), y1: parseInt(y1, 10), cx1: parseInt(cx1, 10), cy1: parseInt(cy1, 10) }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {

                },
                failure: function (response) {

                },
                error: function (response) {

                }
            });

        });

        $("#d3Chart1").mouseup(function (e) {
            $('#secondGraph').show();
            $('#divMass').show();

            $("#d3Chart2").show();
            $("#d3Chart3").hide();

            //console.log("Handler for .mouseup() called.");
            var posX = $(this).offset().left,
                posY = $(this).offset().top;

            //console.log((e.pageX - posX) + ' , ' + (e.pageY - posY));

            if (ctrlKey && mouse_down) {

                $("#RecFillByMouseChart2").css("fill", "rgba(255,205,205,0.5)");
                cx2 = e.pageX - posX;
                cy2 = e.pageY - posY;

                if (cx1 > cx2) {
                    var tempX = cx2;
                    cx2 = cx1;
                    cx1 = tempX;

                    var tempY = cy2;
                    cy2 = cy1;
                    cy1 = tempY;

                }

                if (Math.abs(cx1 - cx2) < 3) {
                    $("#RecFillByMouseChart2").css("fill", "rgba(255, 0, 0)");
                    $('#RecFillByMouseChart2').attr('width', 3);
                }
            }
            else {
                $("#RecFillByMouseChart1").css("fill", "rgba(128,128,255,0.5)");
                x2 = e.pageX - posX;
                y2 = e.pageY - posY;

                if (x1 > x2) {
                    var tempX2 = x2;
                    x2 = x1;
                    x1 = tempX2;

                    var tempY2 = y2;
                    y2 = y1;
                    y1 = tempY2;
                }

                if (Math.abs(x1 - x2) < 3) {
                    $("#RecFillByMouseChart1").css("fill", "rgba(0, 0, 255)");
                    $('#RecFillByMouseChart1').attr('width', 3);
                }
            }

            $.ajax({
                type: "POST",
                url: "processdata.aspx/Chart1MouseUp",
                data: JSON.stringify({
                    x1: parseInt(x1, 10), y1: parseInt(y1, 10), cx1: parseInt(cx1, 10), cy1: parseInt(cy1, 10),
                    x2: parseInt(x2, 10), y2: parseInt(y2, 10), cx2: parseInt(cx2, 10), cy2: parseInt(cy2, 10),
                    left: margin.left, width: width
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response != null && response.d != null) {
                        var pData = response.d;

                        //we need to parse it to JSON 
                        pData = $.parseJSON(pData);
                        var msData = [];
                        var nFixed = 0;
                        var delta = 1;
                        var seek = 0;
                        var length = pData['msData'].length;
                        if (length <= 0)
                            return;
                        var min = +pData['msData'][0].mass.toFixed(nFixed);
                        var max = +pData['msData'][length - 1].mass.toFixed(nFixed);

                        var intensity = pData['msData'][seek].intensity;
                        var mass = +pData['msData'][seek].mass.toFixed(nFixed);
                        var massV = +pData['msData'][seek].mass.toFixed(2);
                        var i = 100.00;

                        while (true) {
                            if (i > 900) break;

                            if (i < min || i > max || seek >= length) {
                                msData.push({ 'mass': i, 'intensity': 0, 'massV': massV });
                                i = i + delta;
                                i = +i.toFixed(nFixed);
                                continue;
                            }

                            if (i == mass) {
                                msData.push({ 'mass': i, 'intensity': intensity, 'massV': massV });
                                seek++;

                                if (seek < length) {
                                    intensity = pData['msData'][seek].intensity;
                                    mass = +pData['msData'][seek].mass.toFixed(nFixed);
                                    massV = +pData['msData'][seek].mass.toFixed(2);
                                }
                            }
                            else {
                                msData.push({ 'mass': i, 'intensity': 0, 'massV': massV });
                                i = i + delta;
                                i = +i.toFixed(nFixed);
                            }
                        }

                        DrawMS(msData, pData['maxInt'], pData['minInt']);
                    }
                },
                failure: function (response) {

                },
                error: function (response) {

                }
            });
            mouse_down = false; mouse_up = true; mouse_move = false; ctrlKey = false;
        });

        $("#d3Chart1").mousemove(function (e) {

            var posX = $(this).offset().left,
                posY = $(this).offset().top;

            mouse_move = true;

            if (mouse_down && mouse_move) {
                if (window.event.ctrlKey) {
                    ctrlKey = true;
                    cx2 = e.pageX - posX;
                    if (cx1 > cx2) {
                        $('#RecFillByMouseChart2').attr('x', cx2);
                    }
                    $("#RecFillByMouseChart2").css("fill", "rgba(195,195,195,0.5)");
                    $('#RecFillByMouseChart2').attr('width', Math.abs(cx2 - cx1));
                }
                else {
                    ctrlKey = false;
                    x2 = e.pageX - posX;
                    x2 = e.pageX - posX;
                    if (x1 > x2) {
                        $('#RecFillByMouseChart1').attr('x', x2);
                    }
                    $("#RecFillByMouseChart1").css("fill", "rgba(195,195,195,0.5)");
                    $('#RecFillByMouseChart1').attr('width', Math.abs(x2 - x1));
                }
            }
        });

        $('form').submit(function (e) {
            e.preventDefault();
        });

        $('#tbMass').keypress(function (event) {
            var keycode = (event.keyCode ? event.keyCode : event.which);
            if (keycode == '13') {
                var mass = event.target.value;

                if (mass == "")
                    return;

                var float = /^\s*(\+|-)?((\d+(\.\d+)?)|(\.\d+))\s*$/;
                if (!float.test(mass)) {
                    alert('Value must be float or int');
                    return;
                }

                if (!$.isNumeric(mass))
                    return;

                if (mass >= 900 || mass <= 100) {
                    alert("Please enter a value between 100-900 m/z");
                    return;
                }

                $("#d3Chart3").show();
                $("#d3Chart2").hide();
                $("#d3Chart3 > svg").remove();


                $.ajax({
                    type: "POST",
                    url: "processdata.aspx/drawChart3",
                    data: JSON.stringify({ mass: mass }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if (response != null && response.d != null) {
                            pData = response.d;
                            pData = $.parseJSON(pData);
                            jsonData = [];

                            for (var i = 0; i < pData['PercentIntensity'].length; i++) {
                                var PercentIntensity = pData['PercentIntensity'][i];
                                var RetTimes = pData['RetTimes'][i];
                                jsonData[i] = { 'PercentIntensity': PercentIntensity, 'RetTimes': RetTimes };
                            }

                            DrawLC(jsonData, pData['maxIntensity']);
                        }
                    },
                    failure: function (response) {
                        //alert(response.responseText);
                    },
                    error: function (response) {
                        //alert(response.responseText);
                    }
                });


            }
        });

        $('#btOnOff').on('click', function () {
            if (bOn) {
                $('.green').css('opacity', '0');
                $('.orange').css('opacity', '0');
            }
            else {
                $('.green').css('opacity', '1');
                $('.orange').css('opacity', '1');
            }

            bOn = !bOn
        });
    }
    d3Chart1Draw();

    $('#btAdd').on('click', function () {
        var count = $('#lbCount').text();
        count = Number(count) + 1;
        if (count == 1) {
            var svg = d3.select("#d3Chart1 svg");
            D3GraphToImage(svg);
            $("#addGraph").append($("#imageAdd").clone());

            $objNew = $('<div id="pa" style="height:570px;"></div>');
            $objNew.appendTo("#addGraph");
        }

        $.ajax({
            type: "POST",
            url: "processdata.aspx/btAddClick",
            data: JSON.stringify({ count: count }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response != null && response.d != null) {
                    $('#lbCount').text(count.toString());
                    $objLabel = $('<div style ="margin: 15px;font-size: 20px;">' + response.d + '</div>');
                    $objLabel.appendTo("#addGraph");
                    $obj = $("#d3Chart2").clone().removeAttr('id');
                    $obj.find('.context').remove();
                    $("#temp svg").remove();
                    $obj.appendTo("#temp");
                    var svg = d3.select("#temp svg");
                    D3GraphToImage(svg);
                    $("#addGraph").append($("#imageAdd").clone());

                    if (count % 2 == 0) {
                        $objNext = $('<div id="pa" style="height: 96px;"></div>');
                        $objNext.appendTo("#addGraph");
                    }
                }
            },
            failure: function (response) {

            },
            error: function (response) {

            }
        });

    });

    $('#btClear').on('click', function () {
        $('#lbCount').text("0");
        $("#addGraph").empty();
    });

    var setTopTable = function () {

        var id = 23423;
        $("#lbID").text(id);
        $("#sampleID").text("Sample ID : " + id);

        var dateAcq = "02/03/2020";
        $("#dateAcquired").text("Date Acquired : " + dateAcq);

        var username = "sysadmin";
        $("#username").text("Username : " + username);

        var dateReprocessed = moment().format("MM/DD/YYYY HH:mm:ss A");
        $("#dateReprocessed").text("Date Reprocessed : " + dateReprocessed);

        var commnet = "blank";
        $("#commnet").text("Commnet : " + commnet);

        var instrument = "LCMS-1";
        $("#instrument").text("Instrument : " + instrument);

    }

    $('#btPDF').on('click', function () {

        $('#firstGraph').hide();
        $('#secondGraph').hide();
        $('#divMass').hide();
        $("#RecFillByMouseChart1").hide();
        $('#RecFillByMouseChart2').hide();

        setTopTable();
        $('#topTable').show();
        $("#printPDF").show();
        document.title = '';
        document.pageX = '';
        document.pageY = '';
        //window.print();
        //debugger;        

        var options = {
            background: "rgb(255,255,255)",
            pagesplit: true,
            //useOverflow: true
        };

        var pdf = new jsPDF('p', 'pt', [600, 600]);
        pdf.internal.scaleFactor = 2;
        pdf.addHTML($("#printPDF"), 25, 25, options, function () {
            pdf.save('output.pdf');
        });

        $('#firstGraph').show();
        $('#secondGraph').show();
        $('#divMass').show();
        $("#RecFillByMouseChart1").show();
        $('#RecFillByMouseChart2').show();

        $('#topTable').hide();
        $("#printPDF").hide();
    });

});


