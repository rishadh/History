var lst = "";
var usd = "";
var psw = "";
var pswcnfrm = "";
var enrl = "";
var type = "";

    let uploadFile;
    document.getElementById('imgUpload').addEventListener('change', function () {
        uploadFile = this.files[0];
})


// Defining error variables with a default value
var lst_error = true;
var usd_error = true;
var psw_error = true;
var pswcnfrm_error = true;
var enrl_error = true;
var username_error = true;
var password_error = true;

function ajaxSignIn() {
    usd = document.getElementById('usd').value;
    psw = document.getElementById('psw').value;
    let isValid = LoginValidate();
    if (isValid == true) {
        
        let obj = {
            UserName: usd,
            Password: psw,
        }

        $.ajax({
            method: 'POST',  // http method
            url: "/User/Login",
            data: obj,
           
            success: function (status) {
                console.log(status);
                if (status == 'success') {
                    alert("You are welcome", alert("Successfully Logged In "));
                    window.location.href = '/User/Index';
                }
            },
            error: function () {
                alert("Login Unsuccessfull");
            }

        });

    }
}
function ajaxPost() {

    
    //getting all input object
    lst = document.getElementById('lst').value;
    usd = document.getElementById('usd').value;
    psw = document.getElementById('psw').value;
    pswcnfrm = document.getElementById('psw-cnfrm').value;
    enrl = document.getElementById('enrl').value;
    type = document.getElementById('type').value;

    let isValid = Validate();
    if (isValid == true) {

        let formData = new FormData();
        formData.append("LastName", lst);
        formData.append("FirstMidName", usd);
        formData.append("Password", psw);
        formData.append("ConfirmPassword", psw);
        formData.append("EnrollmentDate", enrl);
        formData.append("Type", type);
        formData.append("ImageFile", uploadFile);

        $.ajax({
            method: 'POST',  // http method
            url: "/User/RegisterUser",
            data:formData,       
            contentType: false,
            processData: false,
            success: function (status) {
                console.log(status);
                if (status == 'success') {
                     alert("Successfully registered ");
                    window.location.href = '/User/Login';
                }
            },
            
            error: function () {
                alert("Register Unsuccessful");
            }
        });

    }
}

function printError(elemId, hintMsg) {
    document.getElementById(elemId).innerHTML = hintMsg;
}
function LoginValidate() {
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

    if (psw == "") {
        printError("psw_error", "Password is required");
    }
    else if (psw.length < 4) {
        printError("psw_error", "Password must be at least 4 characters long.");
    } else {
        printError("psw_error", "");
        psw_error = false;
    }
    return true;
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

    if (psw == "") {
        printError("psw_error", "Password is required");
    }
    else if (psw.length < 4) {
        printError("psw_error", "Password must be at least 4 characters long.");
    } else {
        printError("psw_error", "");
        psw_error = false;
    }

    if (psw != pswcnfrm) {
        printError("pswcnfrm_error", "Passwords do not match.");
    } else {
        printError("pswcnfrm_error", "");
        pswcnfrm_error = false;
    }


    if (enrl == "") {
        printError("enrl_error", "enrollment date is required");
    } else {
        printError("enrl_error", "");
        enrl_error = false;
    }

    if ((lst_error || usd_error || psw_error || pswcnfrm_error || enrl_error) == true) {
        return false;
    }

    return true;
}