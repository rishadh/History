$(document).ready(function () {
    $.ajax({
        url: '/Course/IndexCourse',
        dataType: "JSON",
        method: 'post',
        success: function (ModelList) {
            var CourseTable = $('#tblCourse tbody');
            CourseTable.empty();
            ModelList.forEach((item) => {
                CourseTable.append('<tr><td>' + item.Title + '</td><td>'
                    + item.Credit + '</td><td>' + item.Lecture
                    + "</td><td><a href='/Course/Edit?id=" + item.CourseID + "'>Edit</a> | <a href='#' onclick='Delete(" + item.CourseID + ")'>Delete</a></td></tr>");
            });
        },
        error: function (err) {
            alert(err);
        }
    });
});