var tl;
var cr;
var op1;

var tl_error = true;
var cr_error = true;

$(document).ready(function () {
    $.ajax({
        url: "/Course/GetLecture",
        type: "Get",
        dataType: "JSON",
        success: function (ModelList) {
            var L_Drop = $('#op1');
            L_Drop.empty();
            ModelList.forEach((item) => {
                L_Drop.append("<option value='" + item.ID + "'>" + item.FirstMidName + "</option>");
            });
        }

    });
});

function ajaxCreate() {

    //getting all input object
    tl = document.getElementById('tl').value;
    cr = document.getElementById('cr').value;
    op1 = document.getElementById('op1').value;

    let isValid = Validate();
    if (isValid == true) {
        let formData = new FormData();
        formData.append("Title", tl);
        formData.append("Credit", cr);
        formData.append("Lecture", op1);

        $.ajax({
            method: 'POST',  // http method
            url: "/Course/CreateCourse",
            data: formData,
            contentType: false,
            processData: false,
            success: function (status) {
                console.log(status);
                if (status == 'success') {
                    alert("Successfully Course Created");
                    window.location.href = '/Course/Index';
                }
            },

            error: function () {
                alert("Course create Unsuccessful");
            }
        });

    }
}

function ajaxEdit() {


    //getting all input object
    tl = document.getElementById('tl').value;
    cr = document.getElementById('cr').value;
    op1 = document.getElementById('op1').value;

    let isValid = Validate();
    if (isValid == true) {

        const urlParams = new URLSearchParams(window.location.search);
        const courseId = urlParams.get('id');
        debugger;
        let obj = {
            CourseId: courseId,
            Title: tl,
            Credit: cr,
            Lecture: op1
        };

        $.ajax({
            method: 'POST',  // http method
            url: "/Course/EditCourse",
            data: obj,
            success: function (status) {
                console.log(status);
                if (status == 'success') {
                    alert("Successfully Edited");
                    window.location.href = '/Course/Index';
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
    if (tl == "") {
        printError("tl_error", "Course Name is required");
    } else {
        var regex = /^[a-zA-Z\s]+$/;
        if (regex.test(tl) === false) {
            printError("tl_error", "Please enter a valid name, name sholud be characters");
        } else {
            printError("tl_error", "");
            tl_error = false;
        }
    }

    if (cr == "") {
        printError("cr_error", "Credit is required");
    }
    else {
            printError("cr_error", "");
            cr_error = false;
        }
    
    if ((tl_error || cr_error) == true) {
        return false;
    }
    return true;
}