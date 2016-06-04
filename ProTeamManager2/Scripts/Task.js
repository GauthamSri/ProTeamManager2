
$(document).ready(function () {

    $("#tabs").tabs();


});


function assignToUser(Id,url) {
    var e = document.getElementById(Id);
    var userName = e.options[e.selectedIndex].value;
    $.ajax({
        type: "POST",
        url: url,
        data: { Id: Id, userName: userName },
        // dataType: "html",
        success: function (result) {
            //alert("Inside Success");
            //alert(result);
                window.location.href = result;
        },
        failure: function (response) {
            alert(response.d);
        }
    });
}