$(function () {
    $("#empbutton").click(function (e) {
        var lastname = $("#TextBoxLastname").val();
        ajaxCall("Get", "api/employees/" + lastname, "")
        .done(function (data) {
            if (data.Lastname !== "not found") {
                $("#email").text(data.Email);
                $("#title").text(data.Title);
                $("#firstname").text(data.Firstname);
                $("#phone").text(data.Phoneno);
            } else {
                $("#firstname").text("Not Found");
                $("#email").text("");
                $("#title").text("");
                $("#phone").text("");
            }
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            errorRoutine(jqXHR);
        });
    });
});

function ajaxCall(type, url, data) {
    return $.ajax({ // Returnt he promise that $.ajax returns
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