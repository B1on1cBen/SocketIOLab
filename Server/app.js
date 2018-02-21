var io = require('socket.io')(process.envPort||3000);
var shortid = require('shortid');

console.log("Server started");

io.on('connection', function(socket){
    var thisPlayerId = shortid.generate();
    var players = [];
    players.push(thisPlayerId);

    console.log('Client connected. Spawning player with id ' + thisPlayerId);

    socket.broadcast.emit('spawn player', {id:thisPlayerId});

    players.forEach(function(playerId){
        if(playerId == thisPlayerId) return;

        socket.emit('spawn player', {id:playerId});
        console.log('Adding a new player' + playerId);
    });

    socket.on('playerhere', function(data){
        console.log("Player has logged in.");
    });

    socket.on('disconnect', function(){
        console.log("Player disconnected.");
        players.splice(players.indexOf(thisPlayerId), 1);
        socket.broadcast.emit('disconnected', {id:thisPlayerId});
    });
});