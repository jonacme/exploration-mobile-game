const http = require('http');
const fs = require('fs');
const path = require('path');

let positions = {saikyun: {x: 0, y: 0, z: 0},
                 crab: {x: 4, y: 10, z: 0}};

function v3add(v1, v2) {
  return {x: v1.x + v2.x,
          y: v1.y + v2.y,
          z: v1.z + v2.z};
}

// turns a vector into something like {x: 1, y: 0, z: 0}
function v3_4way(v) {
  if (v.x !== 0) {
    return {x: v.x / Math.abs(v.x),
            y: 0,
            z: 0};
  } else if (v.y !== 0) {
    return {x: 0,
            y: v.y / Math.abs(v.y),
            z: 0};
  } else {
    return {x: 0,
            y: 0,
            z: 0};
  }
}

function v3eq(v1, v2) {
  return v1.x == v2.x && v1.y == v2.y && v1.z == v2.z;
}

function responseFunc(request, response) {
  console.log("got request:", request.url);
  let parts = request.url
    .split("/")
    .filter(x => x !== '');

  console.log(parts);

  switch (parts[0]) {
    case 'position':
      let username = parts[1];
      console.log("username:", username);
      console.log(positions[username]);


      setTimeout(() => {
        response.writeHead(200, { 'Content-Type': 'application/json' });
        response.end(JSON.stringify(positions[username]), 'utf-8');
      }, 300);
      break;
    case 'set-position':
      let data = '';

      request.on('data', chunk => {
        data += chunk;
      });

      request.on('end', () => {
        let username = parts[1];
        let movement = JSON.parse(decodeURIComponent(data));
        console.log(movement);

        movement.direction = v3_4way(movement.direction);

        setTimeout(() => {
          if (v3eq(movement.current_pos, positions[username])) {
            positions[username] = v3add(positions[username], movement.direction);
          }

          console.log("user", username, "got new pos", positions[username]);
          response.writeHead(200);
          response.end("nice");
        }, 300);
      });
      break;
    default:
        response.writeHead(404);
        response.end(`Your request ${request.url} did not match any operations.`, 'utf-8');
  }
}



let server = http.createServer(responseFunc);

server.listen(8125);

console.log('Server running a "server": http://127.0.0.1:8125/');