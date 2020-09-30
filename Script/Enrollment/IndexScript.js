$(document).ready(function () {
    $.ajax({
        url: '/Enrollment/IndexEnrollment',
        dataType: "JSON",
        method: 'post',
        success: function (ModelList) {
            var EnrollmentTable = $('#tblEnrollment tbody');
            EnrollmentTable.empty();
            ModelList.forEach((item) => {
                EnrollmentTable.append('<tr><td>' + item.CourseID + '</td><td>'
                    + item.UserID + '</td><td>' + item.Grade
                    + "</td><td><a href='/Enrollment/Edit?id=" + item.EnrollmentID + "'>Edit</a> | <a href='#' onclick='Delete(" + item.EnrollmentID + ")'>Delete</a></td></tr>");
            });

        },
        error: function (err) {
            alert(err);
        }
    });
});