const http = require('http');
const fs = require('fs');
const path = require('path');

let positions = {saikyun: {x: 3, y: 10, z: 0},
                 crab: {x: 4, y: 10, z: 0}};

function responseFunc(request, response) {
  let parts = request.url
    .split("/")
    .filter(x => x !== '');

  console.log(parts);

  switch (parts[0]) {
    case 'position':
      let username = parts[1];
      console.log("username:", username);
      console.log(positions[username]);

      response.writeHead(200, { 'Content-Type': 'application/json' });
      response.end(JSON.stringify(positions[username]), 'utf-8');
      break;
    default:
        response.writeHead(404);
        response.end(`Your request ${request.url} did not match any operations.`, 'utf-8');
  }
}



let server = http.createServer(responseFunc);

server.listen(8125);

console.log('Server running a "server": http://127.0.0.1:8125/');