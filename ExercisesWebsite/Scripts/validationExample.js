$(function () {

    $("#empgetbutton").click(function (e) {  // click event handler

        //   reset validation
        var validator = $('#EmployeeModalForm').validate();
        validator.resetForm();

        var lastname = $("#TextBoxFindLastname").val();
        $("#lblstatus").text("loading....");
        $("#lblstatus").css({"color":"black"});

        $("#empupdbutton").attr("disabled", true);
        ajaxCall("Get", "api/employees/" + lastname, "")
        .done(function (data) {
            if (data.Lastname !== "not found") {
                $("#lblstatus").text("employee found!");
                $("#lblstatus").css({ "color": "green" });
                $("#empupdbutton").attr("disabled", false);
                $("#TextBoxEmail").val(data.Email);
                $("#TextBoxTitle").val(data.Title);
                $("#TextBoxFirstname").val(data.Firstname);
                $("#TextBoxLastname").val(data.Lastname);
                $("#TextBoxPhone").val(data.Phoneno);
            }
            else {
                $("#lblstatus").text("employee not found!");
                $("#lblstatus").css({ "color": "red" });
                $("#TextBoxEmail").val("");
                $("#TextBoxTitle").val("");
                $("#TextBoxFirstname").val("Not Found");
                $("#TextBoxLastname").val("");
                $("#TextBoxPhone").val("");
            }
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            errorRoutine(jqXHR);
        }); // ajaxCall


        $("#updateModal").modal("show");

    }); // empgetbutton click 

    $("#empupdbutton").click(function () {
        if ($("#EmployeeModalForm").valid()) {
            $("#lblstatus").text("Data Validated by jQuery!");
            $("#lblstatus").css({ "color": "green" });
        }
        else {
            $("#lblstatus").text("fix existing problems");
            $("#lblstatus").css({ "color": "red" });
        }
        return false; // or page will return to server
    }); // empupdbutton click

    $.validator.addMethod("validTitle", function (value, element) { // custom rule
        return this.optional(element) || (value == "Mr." || value == "Ms." || value == "Mrs." || value == "Dr.");
    }, "");

}); // main jQuery function

$("#EmployeeModalForm").validate({
    rules: {
        TextBoxTitle: { maxlength: 4, required: true, validTitle: true },
        TextBoxFirstname: { maxlength: 25, required: true },
        TextBoxLastname: { maxlength: 25, required: true },
        TextBoxEmail: { maxlength: 40, required: true, email: true },
        TextBoxPhone: { maxlength: 15, required: true }
    },
    ignore: ".ignore, :hidden",
    errorElement: "div",
    wrapper: "div", // A wrapper around the error message
    messages: {
        TextBoxTitle: {
            required: "Required field.", maxlength: "Must be 1-4 characters.", validTitle: "Mr. Ms. Mrs. or Dr."
        },
        TextBoxFirstname: {
            required: "Required field.", maxlength: "Must be 1-25 characters."
        },
        TextBoxLastname: {
            required: "Required field.", maxlength: "Must be 1-25 characters."
        },
        TextBoxPhone: {
            required: "Required field.", maxlength: "Must be 1-15 characters."
        },
        TextBoxEmail: {
            required: "Required field.", maxlength: "Must be 1-40 characters.", email: "Needs to be a valid email format"
        }
    }
});

function ajaxCall(type, url, data) {
    return $.ajax({ // return the promise that `$.ajax` returns
        type: type,
        url: url,
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        processData: true
    });
}

function errorRoutine(jqXHR) { // commmon error
    if (jqXHR.responseJSON == null) {
        $("#lblstatus").text(jqXHR.responseText);
    }
    else {
        $("#lblstatus").text(jqXHR.responseJSON.Message);
    }
}
