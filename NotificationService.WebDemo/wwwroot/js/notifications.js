"use strict";

const urlParams = new URLSearchParams(window.location.search);
const userAlias = urlParams.get('user');

var connection = new signalR.HubConnectionBuilder().withUrl("https://nginx.example.com:5001/notification").build();

connection.on("NotificationEvent", function (message) {
    console.log(message); 
});

connection.start().then(function () {
    console.log("Conexão realizada.");
}).catch(function (err) {
    return console.error(err.toString());
});
