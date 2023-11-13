"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/asucher").build();

//Disable the send button until connection is established.
document.getElementById("suchb").disabled = true;

connection.on("GefundeneArtikel", function (jsonArray) {
    if (jsonArray.length > 0) {
        var listelemente = "";
        for (var i = 0; i < jsonArray.length; i++) {
            listelemente += "<form method=\"post\"><input name=\"suchb\" id=\"suchb\" type=\"hidden\" value=\"" + jsonArray[i] + "\"/><button type=\"submit\" class=\"btn\">" + jsonArray[i] + "</button></form></li>";
        }
        document.getElementById("artikel").innerHTML = listelemente;
        document.getElementById("artikel").style.display = "block"
    }
    else {
        document.getElementById("artikel").innerHTML = "";
        document.getElementById("artikel").style.display = "none"
    }
});

connection.start().then(function () {
    document.getElementById("suchb").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("suchb").addEventListener("input", function (event) {
    var suchb = document.getElementById("suchb").value;
    connection.invoke("ArtikelSuche", suchb).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});