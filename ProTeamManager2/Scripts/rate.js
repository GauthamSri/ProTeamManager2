function addComment(taskId, url) {
    var e = document.getElementById("rating");
    var rating = e.options[e.selectedIndex].value;
    var comment = $.trim($("#comment").val());

    $.ajax({
        type: "POST",
        url: url,
        data: { taskId: taskId, rating: rating, comment: comment },
        // dataType: "html",
        success: function (result) {
            window.location.href = result;
        },
        failure: function (response) {
            alert(response.d);
        }
    });
}