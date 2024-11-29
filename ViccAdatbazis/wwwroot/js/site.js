// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function lajkolas(id) {
    var xhr = new XMLHttpRequest();
    xhr.withCredentials = true;

    xhr.addEventListener("readystatechange", function () {
        if (this.readyState === 4) {
            console.log(this.responseText)
            document.getElementById("likeDb").innerHTML = this.responseText;
        }
    });

    xhr.open("PATCH", "http://localhost:5271/api/Vicc/" + id + "/like");

    xhr.send();
}