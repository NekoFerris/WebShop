"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/asucher").build();

//Disable the send button until connection is established.
document.getElementById("suchb").disabled = true;

connection.on("GefundeneArtikel", function (jsonArray) {
    if (jsonArray.length > 0) {
        var listelemente = "";
        for (var i = 0; i < jsonArray.length; i++) {
            listelemente += "<li><a class=\"dropdown-item\" href=\"asuche\">" + jsonArray[i] + "</a></li>";
        }
        document.getElementById("artikel").innerHTML = listelemente;
    }
    else {
        document.getElementById("artikel").innerHTML = "";
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