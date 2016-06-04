$(document).ready(function () {
    $("#tabs").tabs();
    alert("avg");
    computeAverage();

});



function addUserToTeam(teamId) {

    var e = document.getElementById(teamId);
    var userName = e.options[e.selectedIndex].value;
    
        $.ajax({
            type: "POST",
            url: "Home/AddMemberToTeam",
            data: { teamId: teamId, userName: userName },
           // dataType: "html",
            success: function (result) {
                if (result == 1)
                    location.reload();
                else
                    alert("Team Member not added!");
            },
            failure: function (response) {
                alert(response.d);
            }
        });
}

function assignTeam(projectId) {

    var e = document.getElementById(projectId);
    
    var teamName = e.options[e.selectedIndex].value;
    alert(teamName);
    $.ajax({
        type: "POST",
        url: "Home/AddTeam",
        data: { projectId: projectId, teamName: teamName },

        success: function (result) {
            if (result == 1)
                location.reload();
            else
                alert("Team not assigned!");
        },
        failure: function (response) {
            alert(response.d);
        }
    });
}

function computeAverage() {

    var myValues = new Array();

   // myValues[0] = 1;
    //myValues[1] = 2;
    //myValues[2] = 3;

    if (myValues.length != 0) {
        var sum = 0;

        for (i = 0; i < myValues.length; i++) {
            sum += myValues[i];
        }

        var avg = sum / myValues.length;
        alert(avg);
    }
    else {
        alert("array is empty");
    }
}
