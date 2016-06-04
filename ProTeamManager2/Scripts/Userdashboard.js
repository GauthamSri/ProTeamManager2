
$(document).ready(function () {

    $("#tabs").tabs();


});

function changeStatus(taskId, url) {

    var e = document.getElementById(taskId);
    var status = e.options[e.selectedIndex].value;
    $.ajax({
        type: "POST",
        url: url,
        data: { taskId: taskId, status: status },
        // dataType: "html",
        success: function (result) {
            if (result == 1)
                location.reload();
            else
                alert("Status not updated!");
        },
        failure: function (response) {
            alert(response.d);
        }
    });
}

function changePriority(taskId, url) {

    $.ajax({
        type: "POST",
        url: url,
        data: { taskId: taskId},
        // dataType: "html",
        success: function (result) {
            if (result == 1)
                location.reload();
            else
                alert("Prioity Not Flipped!");
        },
        failure: function (response) {
            alert(response.d);
        }
    });
}

function rate(taskId, url) {
    $.ajax({
        type: "POST",
        url: url,
        data: { taskId: taskId },
        // dataType: "html",
        success: function (result) {
                window.location.href = result;
        },
        failure: function (response) {
            alert(response.d);
        }
    });
}

function generateUserGraph(url) {

    var e = document.getElementById("users");
    var userName = e.options[e.selectedIndex].value;
    var graph = document.getElementById("graph");
    graph.src = url + "?" + "userName=" + userName;
}