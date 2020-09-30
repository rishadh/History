$(document).ready(function () {
    $.ajax({
        url: '/User/ProfileUser',
        dataType: "JSON",
        method: 'get',
        success: function (modelList) {
            console.log(modelList);
            var userTable = $('#tblUser tbody');
            userTable.empty();
            userTable.append('<tr><td>' + modelList.LastName + '</td><td>'
                + modelList.FirstMidName + '</td><td>' + dateFormat(new Date(parseInt((modelList.EnrollmentDate).match(/\d+/)[0]))) + '</td><td>' + modelList.Type
                + "</td><td> <img class='img-circle' width='70px' height='70px' src='" + modelList.ImagePath.replace('~', '') + "' /></td><td><a href='/User/Edit?id=" + modelList.ID + "'>Edit</a> </td></tr>");
        

        },
        error: function (err) {
            alert(err);
        }
    });
    function dateFormat(d) {
        console.log(d);
        return ((d.getMonth() + 1) + "").padStart(2, "0")
            + "/" + (d.getDate() + "").padStart(2, "0")
            + "/" + d.getFullYear();
    }
});  