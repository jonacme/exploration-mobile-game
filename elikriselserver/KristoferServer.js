const http = require('http');
const fs = require('fs');
const path = require('path');

const port = 8125
const hostname = '127.0.0.1'

let positions = {elikrisel: {x: 0, y: 0, z: 0},
		balley: {x: 4, y: 10, z: 0}};

function v3add(v1, v2) {

	//console.log("defining v3add: ", v3add);
	return {x: v1.x + v2.x,
		y: v1.y + v2.y,
		z: v1.z + v2.z};
}

function v3_4way(v){
	
	//console.log("defining 4way: ", v);
	if(v.x !== 0) {
	return {x: v.x / Math.abs(v.x),
	y: 0,
	z: 0};
	}
	else if(v.y !== 0) {
	return {x: 0,
	y: v.y / Math.abs(v.y),
	z: 0};
	}
	else {
		return {x: 0,
			y: 0,
			z: 0
			};
	}
		
	

}

function v3eq(v1,v2){

	
	return v1.x == v2.x && v1.y == v2.y && v1.z == v2.z;
	
	
}





function move(request, response, username){
	console.log("Inside move ");
	let data = '';

	request.on('data', chunk => {
		data += chunk;
		console.log("Got data:", request.on);
	});
	
	request.on('end', () => {
		
		let movement = JSON.parse(decodeURIComponent(data));
		console.log("JSON Parse data: ", data);

		movement.direction = v3_4way(movement.direction);
		
		console.log("moving in direction...: ", movement);
	setTimeout(() => {
		if(v3eq(movement.currentPos,positions[username]))
		{
			positions[username] = v3add(positions[username],movement.direction);
		}
	
		console.log(username, "  got new position: ", positions[username]);
		response.writeHead(200);
	
		response.end("nice");
	
		}, 300);
	
		});
}



function responseFunc(request, response) {
	
	//console.log("Got request:", request.url);
	console.log("Got response: ", response.end);
	let parts = request.url
	.split("/")
	.filter(x => x !== '');
	
	console.log(parts[0]);
	
	switch(parts[0]) {
	
	case 'position':
	let username = parts[1];
	//console.log("username: ", username);
	setTimeout(() => {
		response.writeHead(200, { 'Content-Type': 'application/json' });
		response.end(JSON.stringify(positions[username]), 'utf-8');

		
		},300);
	break;
	
	case 'move':
		move(request, response, parts[1]);
		break;
	default:
		//console.log("Response end:", response.end);
		//console.log("response writehead: ", response.writeHead(200));
		response.writeHead(404);
		response.end(`Your request ${request.url} did not match any operations.`, 'utf8');
	}

	
			
	
}
	


console.log("Initating Server....");

let server = http.createServer(responseFunc);
console.log("server has started:", server);

server.listen(8125);

console.log("listening to server:", server.listen);

