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

    $("#empgetbutton").click(function (e) {
        $("#lblstatus").text("");
        var lastname = $("#TextBoxFindLastname").val();
        ajaxCall("Get", "api/employees/" + lastname, "")
        .done(function (data) {
            if (data.Lastname !== "not found") {
                $("#TextBoxEmail").val(data.Email);
                $("#TextBoxTitle").val(data.Title);
                $("#TextBoxFirstname").val(data.Firstname);
                $("#TextBoxLastname").val(data.Lastname);
                $("#TextBoxPhone").val(data.Phoneno);
                $("#ImageHolder").html(
                    '<img id="StaffPicture" height="120" width="110" src="data:image/png;base64,'
                    + data.StaffPicture64
                    + '" />'
                );
                $("#StaffPicture64").val(data.StaffPicture64);
                $("#HiddenEntity").val(data.Entity64);
            } else {
                $("#TextBoxEmail").val("");
                $("#TextBoxTitle").val("");
                $("#TextBoxFirstname").val("");
                $("#TextBoxLastname").val("");
                $("#TextBoxPhone").val("");
                $("#HiddenEntity").val("");
            }
        }).fail(function (jqXHR, textStatus, errorThrown) {
            errorRoutine(jqXHR);
        });

        $("#updateModal").modal("show");
        return false; // Make sure to return false for click or REST calls get cancelled
    });

    $("#empupdbutton").click(function () {
        var reader = new FileReader();
        var file = $("#fileUpload")[0].files[0];
        if (file !== undefined) {
            reader.readAsBinaryString(file);

            reader.onload = function (readerEvt) {
                var binaryString = reader.result;
                var encodedString = btoa(binaryString);

                emp = new Object();
                emp.Title = $("#TextBoxTitle").val();
                emp.Firstname = $("#TextBoxFirstname").val();
                emp.Lastname = $("#TextBoxLastname").val();
                emp.Phoneno = $("#TextBoxPhone").val();
                emp.Email = $("#TextBoxEmail").val();
                emp.Entity64 = $("#HiddenEntity").val();
                emp.StaffPicture64 = encodedString;

                ajaxCall("Put", "api/employees", emp)
                .done(function (data) {
                    $("#lblstatus").text(data);
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    errorRoutine(jqXHR);
                });
            };
        } else {
            emp = new Object();
            emp.Title = $("#TextBoxTitle").val();
            emp.Firstname = $("#TextBoxFirstname").val();
            emp.Lastname = $("#TextBoxLastname").val();
            emp.Phoneno = $("#TextBoxPhone").val();
            emp.Email = $("#TextBoxEmail").val();
            emp.Entity64 = $("#HiddenEntity").val();
            emp.StaffPicture64 = $("#StaffPicture64").val();

            ajaxCall("Put", "api/employees", emp)
            .done(function (data) {
                $("#lblstatus").text(data);
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                errorRoutine(jqXHR);
            });
        }

        
        return false;
    });
});

function ajaxCall(type, url, data) {
    return $.ajax({ // Return the promise that $.ajax returns (deferred object)
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