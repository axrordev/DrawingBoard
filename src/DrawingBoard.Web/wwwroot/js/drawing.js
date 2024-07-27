const connection = new signalR.HubConnectionBuilder()
    .withUrl("/drawingHub")
    .build();

connection.on("ReceiveDrawing", function (user, drawingData) {
    const canvas = document.getElementById("drawingCanvas");
    const context = canvas.getContext("2d");
    const image = new Image();
    image.onload = function () {
        context.drawImage(image, 0, 0);
    };
    image.src = drawingData;
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});

const canvas = document.getElementById("drawingCanvas");
const context = canvas.getContext("2d");
canvas.width = window.innerWidth;
canvas.height = window.innerHeight;

let drawing = false;

canvas.addEventListener("mousedown", function (e) {
    drawing = true;
    context.beginPath();
    context.moveTo(e.clientX - canvas.offsetLeft, e.clientY - canvas.offsetTop);
});

canvas.addEventListener("mouseup", function () {
    drawing = false;
    context.closePath();
});

canvas.addEventListener("mousemove", function (e) {
    if (drawing) {
        context.lineTo(e.clientX - canvas.offsetLeft, e.clientY - canvas.offsetTop);
        context.stroke();
        connection.invoke("SendDrawing", "Anonymous", canvas.toDataURL()).catch(function (err) {
            return console.error(err.toString());
        });
    }
});

document.getElementById("clearButton").addEventListener("click", function () {
    context.clearRect(0, 0, canvas.width, canvas.height);
    connection.invoke("SendDrawing", "Anonymous", canvas.toDataURL()).catch(function (err) {
        return console.error(err.toString());
    });
});

//const canvas = document.getElementById("drawingCanvas");
//const context = canvas.getContext("2d");
//let drawing = false;

//canvas.addEventListener("mousedown", function (e) {
//    drawing = true;
//    context.moveTo(e.clientX - canvas.offsetLeft, e.clientY - canvas.offsetTop);
//});

//canvas.addEventListener("mouseup", function () {
//    drawing = false;
//});

//canvas.addEventListener("mousemove", function (e) {
//    if (drawing) {
//        context.lineTo(e.clientX - canvas.offsetLeft, e.clientY - canvas.offsetTop);
//        context.stroke();
//        connection.invoke("SendDrawing", drawingId, canvas.toDataURL()).catch(function (err) {
//            return console.error(err.toString());
//        });
//    }
//});
