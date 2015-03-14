zmq = require 'zmq'
pub = zmq.socket 'pub'

pub.bindSync("tcp://127.0.0.1:5555")
console.log "Publisher bound to port 5555"

count = 0

setInterval( ()->
	console.log "Sending TEST #{count++}"
	pub.send(["TEST", "#{count++}"])
, 1000
)