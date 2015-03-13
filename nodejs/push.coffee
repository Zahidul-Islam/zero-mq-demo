zmq = require 'zmq'
push = zmq.socket 'push'
count = 0

push.bindSync("tcp://127.0.0.1:5555");
console.log "Producer bound to port 5555"

setInterval(() ->
	console.log "Sending Job #{count++}"
	push.send("Job #{count++}")
, 1000
)