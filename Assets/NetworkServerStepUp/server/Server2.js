const http = require('http');
const fs = require('fs');
const path = require('path');

let position = {juma: {x:3, y:10, z: 0},                     // set position for the unity character movement.
                monk: {x:4, y:9, z: 0}};  

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
                let newPos = JSON.parse(decodeURIComponent(data));
                console.log('user', username, "Got new Pos", newPos);
                position[username] = newPos;
                
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