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

    var rX = 400;
    var rY = 20;
    var dx = 70;
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
function DrawTIC(data, width, height, svg) {

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
}
function DrawMS(data, maxInt, minInt) {

    $("#d3Chart2 > g").remove();
    $("#d3Chart2 > .label").remove();

    var margin = { top: 10, right: 0, bottom: 60, left: 60 },
        margin2 = { top: 280, right: 0, bottom: 20, left: 60 },
        width = 700 - margin.left - margin.right,
        height = 330 - margin.top - margin.bottom,
        height2 = 330 - margin2.top - margin2.bottom;

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

    console.log(x(data[0].mass))
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

    svg.selectAll(".text")
        .data(data)
        .enter()
        .append("text")
        .attr("class", "label")
        .attr("x", (function (d) { return x(d.mass) + 60 }))
        .attr("y", function (d) { return y(d.intensity) + 1; })
        .attr("dy", ".75em")
        .text(function (d) {
            var PercentIntensity = (d.intensity - minInt) / (maxInt - minInt) * 100;
            if (PercentIntensity > 20)
                return d.mass;
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
        var filteredTickValues = data.filter(function (d, i) { return i % tickValueMultiplier === 0 }).map(function (d) { return d.mass })

        focus.select(".x.axis").call(xAxis.tickValues(filteredTickValues));
        focus.select(".y.axis").call(yAxis);

        $("#d3Chart2 .x.axis text").remove();
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
            .attr("x", (function (d) { return x(d.mass) + 60 }))
            .attr("y", function (d) { return y(d.intensity) + 1; })
            .attr("dy", ".75em")
            .text(function (d) {
                var PercentIntensity = (d.intensity - minInt) / (maxInt - minInt) * 100;
                if (PercentIntensity > 20)
                    return d.mass;
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
function DrawLC(data) {
    var margin = { top: 10, right: 30, bottom: 30, left: 60 },
        width = 460 - margin.left - margin.right,
        height = 400 - margin.top - margin.bottom;

    width = 650;
    height = 250;
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
}

$(document).ready(function () {
    var bOn = true;
    var d3Chart1Draw = function () {
        // set the dimensions and margins of the graph
        var margin = { top: 10, right: 30, bottom: 30, left: 60 },
            width = 460 - margin.left - margin.right,
            height = 400 - margin.top - margin.bottom;

        width = 650;
        height = 250;
        // append the svg object to the body of the page
        var svg = d3.select("#d3Chart1")
            .append("svg")
            .attr("width", width + margin.left + margin.right)
            .attr("height", height + margin.top + margin.bottom)
            .append("g")
            .attr("transform",
                "translate(" + margin.left + "," + margin.top + ")");

       //data: JSON.stringify({ rpt: rpt, txt: txt }),
        $.ajax({
            type: "POST",
            url: "processdata.aspx/getChartModelData",
            data: JSON.stringify({data: 'mydata'}),
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

                    DrawUV220(jsonDataUV220, width, height, svg);
                    DrawUV254(jsonDataUV254, width, height, svg);
                    DrawTIC(jsonDataTIC, width, height, svg);
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
        $("#RecFillByMouseChart1").width(0);

        var x1, x2;
        $("#d3Chart1").mousedown(function (e) {
            console.log("Handler for .mousedown() called.");
            var posX = $(this).offset().left,
                posY = $(this).offset().top;

            console.log((e.pageX - posX) + ' , ' + (e.pageY - posY));
            mouse_down = true; mouse_up = false; mouse_move = false;

            x1 = e.pageX - posX;
            $("#RecFillByMouseChart1").width(0);
            $("#RecFillByMouseChart1").css("left", x1);
            $("#RecFillByMouseChart1").css("fill", "rgba(0, 0, 0, 0.1)");
            $.ajax({
                type: "POST",
                url: "processdata.aspx/Chart1MouseDown",
                data: JSON.stringify({ x: parseInt(e.pageX - posX, 10), y: parseInt(e.pageY - posY, 10) }),
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
            $("#d3Chart2").show();
            $("#d3Chart3").hide();

            console.log("Handler for .mouseup() called.");
            var posX = $(this).offset().left,
                posY = $(this).offset().top;

            console.log((e.pageX - posX) + ' , ' + (e.pageY - posY));

            mouse_down = false; mouse_up = true; mouse_move = false;
            $("#RecFillByMouseChart1").css("background-color", "rgba(128,128,255,0.5)");

            if (x1 == x2) {
                $("#RecFillByMouseChart1").width(3);
                $("#RecFillByMouseChart1").css("background-color", "rgba(0, 0, 255)");
            }

            $.ajax({
                type: "POST",
                url: "processdata.aspx/Chart1MouseUp",
                data: JSON.stringify({ x: parseInt(e.pageX - posX, 10), y: parseInt(e.pageY - posY, 10), left: margin.left, width: width }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response != null && response.d != null) {
                        var pData = response.d;

                        //we need to parse it to JSON 
                        pData = $.parseJSON(pData);

                        msData = []
                        //msData = pData['msData'] 

                        for (var i = 0; i < pData['msData'].length; i++) {
                            var intensity = pData['msData'][i].intensity;
                            var mass = +pData['msData'][i].mass.toFixed(2);
                            //var mass = Math.round(pData['msData'][i].mass);                            
                            msData[i] = { 'mass': mass, 'intensity': intensity };
                        }

                        DrawMS(msData, pData['maxInt'], pData['minInt']);
                    }
                },
                failure: function (response) {

                },
                error: function (response) {

                }
            });
        });

        $("#d3Chart1").mousemove(function (e) {

            var posX = $(this).offset().left,
                posY = $(this).offset().top;

            mouse_move = true;
            x2 = e.pageX - posX;
            if (mouse_down && mouse_move) {
                $("#RecFillByMouseChart1").css("background-color", "rgba(195,195,195,0.5)");
                $("#RecFillByMouseChart1").width(x2 - x1 - 5);
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

                            DrawLC(jsonData);
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
});


