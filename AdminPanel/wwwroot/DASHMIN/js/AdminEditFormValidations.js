
$(document).ready(function () {
    // Disable the submit button by default
    $("#AdminProfileSaveChanges-edit").prop("disabled", true);

    // Validate the email field
    $("#AdminEmail-edit").on("input", function () {
        var email = $(this).val();
        if (email.length === 0) {
            $("#AdminEmail-edit").addClass("is-invalid");
        } else {
            $("#AdminEmail-edit").removeClass("is-invalid");
        }
        validateForm();
    });

    // Validate the password field
    $("#AdminPassword-edit").on("input", function () {
        var password = $(this).val();
        if (password.length === 0) {
            $("#AdminPassword-edit").addClass("is-invalid");
        } else {
            $("#AdminPassword-edit").removeClass("is-invalid");
        }
        validateForm();
    });

    // Validate the first name field
    $("#AdminFirstName-edit").on("input", function () {
        var firstName = $(this).val();
        if (firstName.length === 0) {
            $("#AdminFirstName-edit").addClass("is-invalid");
        } else {
            $("#AdminFirstName-edit").removeClass("is-invalid");
        }
        validateForm();
    });

    // Validate the last name field
    $("#AdminLastName-edit").on("input", function () {
        var lastName = $(this).val();
        if (lastName.length === 0) {
            $("#AdminLastName-edit").addClass("is-invalid");
        } else {
            $("#AdminLastName-edit").removeClass("is-invalid");
        }
        validateForm();
    });

    // Validate the date of birth field
    $("#AdminDOB-edit").on("input", function () {
        var dob = $(this).val();
        if (dob.length === 0) {
            $("#AdminDOB-edit").addClass("is-invalid");
        } else {
            $("#AdminDOB-edit").removeClass("is-invalid");
        }
        validateForm();
    });

    // Validate the checkbox
    $("#SaveChangesCheck-edit").on("change", function () {
        validateForm();
    });

    // Function to validate the form and enable/disable the submit button
    function validateForm() {
        var email = $("#AdminEmail-edit").val();
        var password = $("#AdminPassword-edit").val();
        var firstName = $("#AdminFirstName-edit").val();
        var lastName = $("#AdminLastName-edit").val();
        var dob = $("#AdminDOB-edit").val();
        var checkboxChecked = $("#SaveChangesCheck-edit").is(":checked");

        if (email.length > 0 && password.length > 0 && firstName.length > 0 && lastName.length > 0 && dob.length > 0 && checkboxChecked) {
            $("#AdminProfileSaveChanges-edit").prop("disabled", false);
        } else {
            $("#AdminProfileSaveChanges-edit").prop("disabled", true);
        }
    }
});
