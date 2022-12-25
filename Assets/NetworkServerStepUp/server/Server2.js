const http = require('http');
const fs = require('fs');
const path = require('path');

let position = {juma: {x:3, y:10, z: 0},                     // set position for the unity character movement.
                monk: {x:4, y:9, z: 0}};  

function v3add(v1, v2)
{
    return{x: v1.x + v2.x,
           y: v1.y + v2.y, 
           z: v1.z + v2.z};
}   

// turns a vector into something like {x:1, y:0, z:0}
function v3_4way(v)
{
    if(v.x !== 0)
    {
        return{
            x: v.x / Math.abs(v.x),
            y:0,
            z:0};
    }
    else if(v.y !== 0)
    {
        return{
            x:0,
            y: v.y / Math.abs(v.y),
            z:0};
    }
    else if(v.z !== 0)
    {
        return{
            x:0,
            y:0,
            z:0};
    }
}

//console.log("position/juma");
//console.log("/positions / juma".split("/").filter(x=> x !== ''));

function responsFunc(request, response)                                    // request lets user requests and respone gives answer or something in webside or http server instead
{
    let parts = request.url.split("/").filter(x => x !== '');
    //console.log(parts);

    //console.log(request);

    switch (parts[0])
    {
        case 'positions':
            let username =parts[1];
            //console.log("username:", username);
            //console.log(position[username]);

            response.writeHead(200, {'Content-type': 'application /json'});
            response.end(JSON.stringify(position[username]), 'utf-8');
            break;
        case 'set-positions':                                          // its either positions or position
            let data ='';
            request.on('data', chunk => {
            data +=chunk;    
            console.log("got chunk:", decodeURIComponent(data));
            });

            request.on('end', () =>
            {                
                //console.log("oh i got all the data!", data);

                let username = parts[1];
                console.log("jason data:", "\ndecoded:", decodeURIComponent(data));
                let direction = JSON.parse(decodeURIComponent(data));
                direction = v3_4way(direction);

                position[username] = v3add(position[username], direction);


                console.log('user', username, "Got new Pos", position[username]);
                response.writeHead(200);
                response.end("nice");
            });
            
            break;
        default:
            response.writeHead(404);
            response.end(`Your request  ${request.url} didnt match any operations.`, 'utf-8');
    }

    console.log(response);


    //console.log("Hello");
    //console.log(request.url); 
    //console.log(position[request.url]);
}


let server = http.createServer(responsFunc);

//console.log(server);
//console.log(Object.getPrototypeOf(Object.getPrototypeOf(server)));

server.listen(8125);

console.log('Server running a "Server": http://127.0.0.1:8125/');