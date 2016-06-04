
$(document).ready(function () {

    $("#tabs").tabs();

    $(".fixwidth").datepicker({ dateFormat: 'mm/dd/yy' });
});

function setReleaseDate(Id, url) {
    var currentDate = $("#" + Id).datepicker("getDate");
    alert(currentDate);
    $.ajax({
        type: "POST",
        url: url,
        data: { Id: Id, currentDate: currentDate },
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