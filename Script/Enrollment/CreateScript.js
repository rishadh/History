var op1;
var op2;
var ac;
var ac_error = true;

//course
$(document).ready(function () {
    $.ajax({
        url: "/Enrollment/GetCourse",
        type: "Get",
        success: function (ModelList) {
            var L_Drop = $('#op1');
            L_Drop.empty();
            ModelList.forEach((item) => {
                L_Drop.append("<option value='" + item.CourseID + "'>" + item.CourseName + "</option>");
            });
        }

    });
});

//user
$(document).ready(function () {
    $.ajax({
        url: "/Enrollment/GetUser",
        type: "Get",
        dataType: "JSON",
        success: function (ModelList) {
            var L_Drop = $('#op2');
            L_Drop.empty();
            ModelList.forEach((item) => {
                L_Drop.append("<option value='" + item.ID + "'>" + item.FirstMidName + "</option>");
            });
        }

    });
});

function ajaxCreate() {


    //getting all input object
    op1 = document.getElementById('op1').value;
    op2 = document.getElementById('op2').value;
    ac = document.getElementById('ac').value;

    let isValid = Validate();
    if (isValid == true) {
        const urlParams = new URLSearchParams(window.location.search);
        const EnrollmentId = urlParams.get('id');

        let formData = new FormData();
        formData.append("CourseID", op1);
        formData.append("UserID", op2);
        formData.append("Grade", ac);
      
        $.ajax({
            method: 'POST',  // http method
            url: "/Enrollment/EnrollmentCreate",
            data: formData,
            contentType: false,
            processData: false,
            success: function (status) {
                console.log(status);
                if (status == 'success') {
                    alert("Successfully Enrolled");
                    window.location.href = '/Enrollment/Index';
                }
            },

            error: function () {
                alert("Enrollment Unsuccessful");
            }
        });

    }
}

function ajaxEdit() {


    //getting all input object
    op1 = document.getElementById('op1').value;
    op2 = document.getElementById('op2').value;
    ac = document.getElementById('ac').value;

    let isValid = Validate();
    if (isValid == true) {

        let formData = new FormData();
        formData.append("CourseID", op1);
        formData.append("UserID", op2);
        formData.append("Grade", ac);

        $.ajax({
            method: 'POST',  // http method
            url: "/Enrollment/EnrollmentEdit",
            data: formData,
            contentType: false,
            processData: false,
            success: function (status) {
                console.log(status);
                if (status == 'success') {
                    alert("Successfully Edited");
                    window.location.href = '/Enrollment/Index';
                }
            },

            error: function () {
                alert("Edit Unsuccessful");
            }
        });

    }
}
function printError(elemId, hintMsg) {
    document.getElementById(elemId).innerHTML = hintMsg;
}
function Validate() {
    if (ac == "") {
        printError("ac_error", "Accademic Year is required");
        ac_error = false;
    }
    if (ac_error == true) {
        return false;
    }

    return true;
}