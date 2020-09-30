var lst = "";
var usd = "";

let uploadFile;
document.getElementById('imgUpload').addEventListener('change', function () {
    uploadFile = this.files[0];
})

// Defining error variables with a default value
var lst_error = true;
var usd_error = true;

function ajaxEdit() {
    lst = document.getElementById('lst').value;
    usd = document.getElementById('usd').value;

    let isValid = Validate();
    if (isValid == true) {

        const queryString = window.location.search;
        const urlParams = new URLSearchParams(queryString);
        const user_id = parseInt(urlParams.get('id'), 10);
        console.log(user_id);

        let formData = new FormData();
        formData.append("ID", user_id); 
        formData.append("LastName", lst);
        formData.append("FirstMidName", usd);
        formData.append("ImageFile", uploadFile);
        $.ajax({
            method: 'POST',  // http method
            url: "/User/EditUser",
            data: formData,
            contentType: false,
            processData: false,
            success: function (status) {
                console.log(status);
                if (status == 'success') {
                     alert("Profile has Successfully Edited");
                    window.location.href = '/User/Index';
                }
            },
            error: function () {
                alert("Profile Edit Unsuccessfully");
            }

        });

    }
}
function printError(elemId, hintMsg) {
    document.getElementById(elemId).innerHTML = hintMsg;
}

function Validate() {


    if (lst == "") {
        printError("lst_error", "Last Name is required");
    } else {
        var regex = /^[a-zA-Z\s]+$/;
        if (regex.test(lst) === false) {
            printError("lst_error", "Please enter a valid name, name should characters");
        } else {
            printError("lst_error", "");
            lst_error = false;
        }
    }
    if (usd == "") {
        printError("usd_error", "User Name is required");
    } else {
        var regex = /^[a-zA-Z\s]+$/;
        if (regex.test(usd) === false) {
            printError("usd_error", "Please enter a valid name, name sholud be characters");
        } else {
            printError("usd_error", "");
            usd_error = false;
        }
    }

    if ((lst_error || usd_error) == true) {
        return false;
    }

    return true;
}