function saveNewTeam(newTeam) {
    alert(newTeam);
    $.ajax({
        type: "POST",
        url: "Home/AddNewTeam",
        data: newTeam,
        dataType: "text",
        success: function (result) {
            //if (result == "true") {
            //    location.reload();
            //}
            //else {
            //    alert("Error in creating new team");
            //}
            alert(result);
        },
        failure: function (response) {
            alert(response.d);
        }
    });
}