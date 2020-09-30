$(document).ready(function () {
    $.ajax({
        url: '/User/IndexUser',
        dataType: "JSON",
        method: 'post',
        success: function (ModelList) {
            console.log(ModelList);
            var userTable = $('#tblUser tbody');
            userTable.empty();
            ModelList.forEach((item) => {
                userTable.append('<tr><td>' + item.LastName + '</td><td>'
                    + item.FirstMidName + '</td><td>' + dateFormat(new Date(parseInt((item.EnrollmentDate).match(/\d+/)[0]))) + '</td><td>' + item.Type
                    + "</td><td> <img class='img-circle' width='70px' height='70px' src='" + item.ImagePath.replace('~', '') + "' /></td><td><a href='/User/Edit?id=" + item.ID + "'>Edit</a> | <a href='#' onclick='Delete(" + item.ID + ")'>Delete</a></td></tr>" );
            });
    
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