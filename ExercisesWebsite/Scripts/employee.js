$(function () {
    $("#empbutton").click(function (e) {
        var lastname = $("#TextBoxLastname").val();
        $("#lblstatus").text("Please be patient...");
        ajaxCall("Get", "api/employees/" + lastname, "")
        .done(function (data) {
            if (data.Lastname !== "not found") {
                $("#email").text(data.Email);
                $("#title").text(data.Title);
                $("#firstname").text(data.Firstname);
                $("#phone").text(data.Phoneno);

                ajaxCall("Get", "api/departments/" + data.DepartmentId, "")
                .done(function (data) {
                    if (data.DepartmentId !== "not found") {
                        $("#departmentname").text(data.DepartmentName);
                    } else {
                        $("#departmentname").text("Department not found");
                    }
                }).fail(function (jqXHR, textStatus, errorThrown) {
                    errorRoutine(jqXHR);
                });
                $("#lblstatus").text("Employee " + data.Lastname + " Found!");
            } else {
                $("#firstname").text("");
                $("#email").text("");
                $("#title").text("");
                $("#phone").text("");
                $("#lblstatus").text("Employee "+ data.Lastname + " not found");
            }
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            errorRoutine(jqXHR);
        });
    });
});

function ajaxCall(type, url, data) {
    return $.ajax({ // Return the promise that $.ajax returns
        type: type,
        url, url,
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        processData: true
    });
}

function errorRoutine(jqXHR) { // common error
    if (jqXHR.responseJSON == null) {
        $("#lblstatus").text(jqXHR.responseText);
    } else {
        $("#lblstatus").text(jqXHR.responseJSON.Message);
    }
}