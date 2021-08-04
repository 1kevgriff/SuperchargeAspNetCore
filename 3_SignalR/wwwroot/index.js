var btnStart = document.getElementById("btnStart");
var btnEnd = document.getElementById("btnEnd");
var newUsers = document.getElementById("newUsers");

var connection = new signalR.HubConnectionBuilder().withUrl("/hubs/sync").build();

/* signalr events */
connection.on("newUser", (user) => {
    console.log("New User");
    
    const node = document.createElement("li");
    const text = document.createTextNode(`Welcome to ${user.name.first} ${user.name.last}`);
    node.appendChild(text);
    newUsers.insertBefore(node, newUsers.firstChild);
});

/* local events */

btnStart.addEventListener("click", (ev) => {
    connection.send("startNotify");

    const node = document.createElement("li");
    const text = document.createTextNode(`Listening...`);
    node.appendChild(text);
    newUsers.insertBefore(node, newUsers.firstChild);
});

btnEnd.addEventListener("click", (ev) => {
    connection.send("endNotify");

    const node = document.createElement("li");
    const text = document.createTextNode(`Stop listening...`);
    node.appendChild(text);
    newUsers.insertBefore(node, newUsers.firstChild);
});

connection.start();