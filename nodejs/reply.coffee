zmq = require 'zmq'

reply = zmq.socket 'rep'

reply.bind('tcp://127.0.0.1:5555', (e) -> 
	console.error e
)


reply.on('message', (msg) ->
	console.log "Reply #{msg}"
	reply.send 'Pong!'

)

