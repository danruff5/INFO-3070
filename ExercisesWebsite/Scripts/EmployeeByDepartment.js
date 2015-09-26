$(function () {
    $("#empbutton").click(function (e) {
        var department = $("#TextBoxDepartment").val();
        $("#lblstatus").text("Please be patient...");

        ajaxCall("Get", "api/employees/department/" + department, "")
        .done(function (data) {
            $("#employees").text("");
            if (data.employees !== null) {
                for (employee in data.employees) {
                    $("#employees").append("<div>"
                        + data.employees[employee].Firstname
                        + " "
                        + data.employees[employee].Lastname
                        + "</div>");
                }
            } else {
                $("#employees").append("not found");
            }
        }).fail(function (jqXHR, textStatus, errorThrown) {
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