function Delete(ID) {
    var ans = confirm("Are you sure you want to delete this Record?");

    
    if (ans) {
        $.ajax({
            url: "/User/Delete",
            data: { ID },
            type: "POST",
            success: function () {
                if (status = "success") {
                    alert("User has Successfully Deleted");
                    window.location.href = '/User/Index';
                }
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}
