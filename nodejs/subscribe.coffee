zmq = require 'zmq'
sub = zmq.socket 'sub'

sub.connect("tcp://127.0.0.1:5555");
console.log "Subscriber connect to port 5555"
   
sub.subscribe("TEST")
   .on('message', (topic, msg)->
   		console.log "Received a message related to #{topic}, containing message #{msg}"
   	)