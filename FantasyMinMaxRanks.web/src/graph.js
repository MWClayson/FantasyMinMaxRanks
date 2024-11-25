import $ from "jquery";
import * as d3 from "d3";
const url = import.meta.env.VITE_APIURL

var leagueId = (new URL(location.href)).searchParams.get('leagueId');
if(leagueId){
    console.log(leagueId);
    $.ajax({
        url: url+leagueId,
        type: 'GET',
        dataType: 'json',
        crossDomain: true,
    }).done(function (data){
        console.log(data);
        drawGraph(data);
    }).fail(function(ex) {
        console.log(ex)
    })
    
}

// function setLoading()




function drawGraph(data){

    // Declare the chart dimensions and margins.
    const circleSize = 14
    
    const marginTop = 20;
    const marginRight = 20;
    const marginBottom = 30;
    const marginLeft = 150;


    const paddingInner = 1.2
    const paddingOuter =0.7
    const align =0.5
    const width = (data.teamCount*circleSize)*2.1+marginLeft+marginRight;
    const height = (data.teamCount*circleSize)*2.1+marginTop+marginBottom;
    console.log(data.teamCount);
    console.log(data.rankingData);

    const colorFunction = d3.scaleSequential(d3.interpolateRdYlGn).domain([data.teamCount*2+1,0])

    const x = d3.scaleBand()
        .domain(d3.range(1,data.teamCount+1,1).reverse())
        .range([marginLeft, width - marginRight])
        .paddingInner(paddingInner)
        .paddingOuter(paddingOuter)
        .align(align)
        .round(true);

    const y = d3.scaleBand()
        .domain(data.rankingData.map(d => d.teamName))
        .range([height - marginBottom, marginTop])  
        .paddingInner(paddingInner)
        .paddingOuter(paddingOuter)
        .align(align)
        .round(true);


    // Create the SVG container.
    const svg = d3.create("svg")
        .attr("width", width)
        .attr("height", height);



    svg.append("g")
    .selectAll()
    .data(data.rankingData)
    .join("line")
        .attr("stroke",(d) => colorFunction(d.maxPos+d.minPos))
        .attr("x1", (d) => x(d.minPos, 0))
        .attr("x2", (d) => x(d.maxPos, 0))
        .attr("y1", (d) => y(d.teamName))
        .attr("y2", (d) => y(d.teamName))
        .attr("stroke-width", 5);

   const minNode = svg.append("g")
    .selectAll()
    .data(data.rankingData)
    .join("g")
        .attr("transform", d => `translate(${x(d.minPos)},${y(d.teamName)})`)

    minNode.append("circle")
        .attr("fill",(d) => colorFunction(d.maxPos+d.minPos))
        .attr("r", circleSize)

    minNode.append("text")
        .text(d => d.minPos)
            .attr("font-family", "sans-serif")  
            .attr("text-anchor", "middle")  
            .attr("fill","black")
            .attr("dy", ".35em")


    const maxNode = svg.append("g")
        .selectAll()
        .data(data.rankingData)
        .join("g")
            .attr("transform", d => `translate(${x(d.maxPos)},${y(d.teamName)})`);

    // Add the x-axis.
    svg.append("g")
        .attr("transform", `translate(0,${height - marginBottom})`)
        .call(d3.axisBottom(x));

    // Add the y-axis.
    svg.append("g")
        .attr("transform", `translate(${marginLeft},0)`)
        .call(d3.axisLeft(y));


    maxNode.append("circle")
        .attr("fill",(d) => colorFunction(d.maxPos+d.minPos))
        .attr("r", circleSize);

    maxNode.append("text")
        .text(d => d.maxPos)
            .attr("font-family", "sans-serif")  
            .attr("text-anchor", "middle")  
            .attr("fill","black")
            .attr("dy", ".35em");
    

    // Append the SVG element.
    container.append(svg.node());
}
