// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function displayBusyIndicator() {
  document.getElementById("loading").style.display = "block";
}


$(document).ready(function () {
    // Example with grater loading time - loads longer
    $('.pie_progress_temperature,.pie_progress_cpu, .pie_progress_mem, .pie_progress_disk').asPieProgress({});
getTemp();
    getCpu();
    getMem();
    getDisk();
});

function getTemp() {
    $.ajax({
        url: 'temperature.json.php',
        success: function (response) {
            update('temperature', response);
            setTimeout(function () {
                getTemp();
            }, 1000);
        }
    });
}


function getCpu() {
    $.ajax({
        url: 'cpu.json.php',
        success: function (response) {
            update('cpu', response);
            setTimeout(function () {
                getCpu();
            }, 1000);
        }
    });
}

function getMem() {
    $.ajax({
        url: 'https://sistemas.thiagoferreira.net/inc/cpuinfo/memory.json.php',
        success: function (response) {
            update('mem', response);

            setTimeout(function () {
                getMem();
            }, 1000);
        }
    });
}

function getDisk() {
    $.ajax({
        url: 'disk.json.php',
        success: function (response) {
            update('disk', response);
            setTimeout(function () {
                getDisk();
            }, 1000);
        }
    });
}

function update(name, response) {
    $('.pie_progress_' + name).asPieProgress('go', response.percent);
    $("#" + name + "Div div.title").text(response.title);
    $("#" + name + "Div pre").text(response.output.join('\n'));
}
