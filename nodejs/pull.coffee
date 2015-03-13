zmq = require 'zmq'
pull = zmq.socket 'pull'

pull.connect("tcp://127.0.0.1:5555");
console.log "Worker connect to port 5555"

pull.on('message', (msg) ->
	console.log "Working on #{msg}"
)