
$(document).ready(function () {
    // Disable the submit button by default
    $("#UserProfileSaveChanges-edit").prop("disabled", true);

    // Validate the email field
    $("#UserEmail-edit").on("input", function () {
        var email = $(this).val();
        if (email.length === 0) {
            $("#UserEmail-edit").addClass("is-invalid");
        } else {
            $("#UserEmail-edit").removeClass("is-invalid");
        }
        validateForm();
    });

    // Validate the password field
    $("#UserPassword-edit").on("input", function () {
        var password = $(this).val();
        if (password.length === 0) {
            $("#UserPassword-edit").addClass("is-invalid");
        } else {
            $("#UserPassword-edit").removeClass("is-invalid");
        }
        validateForm();
    });

    // Validate the first name field
    $("#UserFirstName-edit").on("input", function () {
        var firstName = $(this).val();
        if (firstName.length === 0) {
            $("#UserFirstName-edit").addClass("is-invalid");
        } else {
            $("#UserFirstName-edit").removeClass("is-invalid");
        }
        validateForm();
    });

    // Validate the last name field
    $("#UserLastName-edit").on("input", function () {
        var lastName = $(this).val();
        if (lastName.length === 0) {
            $("#UserLastName-edit").addClass("is-invalid");
        } else {
            $("#UserLastName-edit").removeClass("is-invalid");
        }
        validateForm();
    });

    // Validate the date of birth field
    $("#UserDOB-edit").on("input", function () {
        var dob = $(this).val();
        if (dob.length === 0) {
            $("#UserDOB-edit").addClass("is-invalid");
        } else {
            $("#UserDOB-edit").removeClass("is-invalid");
        }
        validateForm();
    });

    // Validate the checkbox
    $("#UserSaveChangesCheck-edit").on("change", function () {
        validateForm();
    });

    // Function to validate the form and enable/disable the submit button
    function validateForm() {
        var email = $("#UserEmail-edit").val();
        var password = $("#UserPassword-edit").val();
        var firstName = $("#UserFirstName-edit").val();
        var lastName = $("#UserLastName-edit").val();
        var dob = $("#UserDOB-edit").val();
        var checkboxChecked = $("#UserSaveChangesCheck-edit").is(":checked");

        if (email.length > 0 && password.length > 0 && firstName.length > 0 && lastName.length > 0 && dob.length > 0 && checkboxChecked) {
            $("#UserProfileSaveChanges-edit").prop("disabled", false);
        } else {
            $("#UserProfileSaveChanges-edit").prop("disabled", true);
        }
    }
});
