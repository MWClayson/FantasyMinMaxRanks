

const url = "http://localhost:7155/api/GetLeagueRankStats?LeagueId=1052318984504311808"

$.ajax({
    url: "http://localhost:7155/api/GetLeagueRankStats?LeagueId=1052318984504311808",
    type: 'GET',
    dataType: 'json',
    crossDomain: true,
}).done(function (data){
    console.log(data);
    drawGraph(data);
}).fail(function(ex) {
    console.log(ex)
})



function drawGraph(data){

    // Declare the chart dimensions and margins.
    const width = 1000;
    const height = 1000;
    const marginTop = 20;
    const marginRight = 20;
    const marginBottom = 30;
    const marginLeft = 150;
    console.log(data.teamCount);
    console.log(data.rankingData);

    // Declare the x (horizontal position) scale.
    const x = d3.scaleLinear()
        .domain([data.teamCount+0.5,1]).nice()
        .range([marginLeft, width - marginRight]);


    const y = d3.scaleBand()
        .domain(data.rankingData.map(d => d.teamName))
        .range([height - marginBottom, marginTop]);

    

    // Create the SVG container.
    const svg = d3.create("svg")
        .attr("width", width)
        .attr("height", height);



    svg.append("g")
    .selectAll()
    .data(data.rankingData)
    .join("line")
        .attr("stroke","green")
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
        .attr("fill","blue")
        .attr("r", 12)

    minNode.append("text")
        .text(d => d.minPos)
            .attr("text-anchor", "middle")  
            .attr("fill","black")
            .attr("dy", ".35em")


    const maxNode = svg.append("g")
        .selectAll()
        .data(data.rankingData)
        .join("g")
            .attr("transform", d => `translate(${x(d.maxPos)},${y(d.teamName)})`);



    maxNode.append("circle")
        .attr("fill","blue")
        .attr("r", 12);

    maxNode.append("text")
        .text(d => d.maxPos)
            .attr("text-anchor", "middle")  
            .attr("fill","black")
            .attr("dy", ".35em");
    
    // Add the x-axis.
    svg.append("g")
        .attr("transform", `translate(0,${height - marginBottom})`)
        .call(d3.axisBottom(x));

    // Add the y-axis.
    svg.append("g")
        .attr("transform", `translate(${marginLeft},0)`)
        .call(d3.axisLeft(y));

    // Append the SVG element.
    container.append(svg.node());
}
