
var connection =
    new signalR.HubConnectionBuilder().withUrl("/gameHub").build();

connection.start()
    .then(() => {
        console.log('Clinete Conectado');
    })

connection.on("ReceiveData", (clients) => {

    console.table(clients);

    var container = document.getElementById("gameContainer")
    container.innerHTML = ` `;
    clients.forEach((item) => {
        var div = document.createElement("div");
        //https://file.io/fEektd5DwwJz
        div.innerHTML = ` 
            <div class="glass-container" onclick="play()">
            <img src="/img/glass.png"/>
            <div class="glass" style="height:${item.gameNumber}%"></div >
            </div>
            `;
        container.appendChild(div);
    })

})

function play() {
    connection.invoke("play")
}
//connection.on // Funciona para recibir mensajes
//connection.invoke() //Enviar Mensajes
