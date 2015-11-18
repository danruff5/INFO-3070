QUnit.test("MultiTier Tests", function (assert) {
    assert.async(1); // number of tests.

    // Get all employees:
    $.ajax({
        type: "Get",
        url: "api/employees"
    }).then(function (data) {
        var numOfEmployees = data.length - 1;
        ok(numOfEmployees > 0, "Found " + numOfEmployees + " Employees.");
    });
});