$(document).ready(function () {
    $('#authenticated').hide();
    $('#sign-up-authentication-error').hide();
    $('#login-authentication-error').hide();
    $('#authenticated').hide();
    $('#btn-generate').hide();
    $('#btn-logout').css('visibility', 'hidden');
    $('#btn-logout-3').hide();
    $('#result-error-container').hide();

    ////////////////////////////////////////////// DatePicker //////////////////////////////////////////////

    /*
    const input = document.getElementById('age');
    const datepicker = new TheDatepicker.Datepicker(input);
    datepicker.render();
    */

    ////////////////////////////////////////////// DatePicker //////////////////////////////////////////////

    if ($('#weight').val().trim().length != 0 && $('#targetWeight').val().trim().length != 0) {
        $('#btn-next-step-2').prop('disabled', false);
    }

    if ($('#weight').val().trim().length != 0 && $('#height').val().trim().length != 0) {
        updateBMI();
        if ($('#BMI').val().trim().length != 0) {
            $('#btn-next-step-3').prop('disabled', false);
        }
    }

    /////////////////////////////////// Login and SignUp Validations ///////////////////////////////////
    function validateEmail(email) {
        var emailRegex = /^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$/;
        return emailRegex.test(email);
    }

    function validatePassword(password) {
        var passwordRegex = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,}$/;
        return password.length >= 6; //passwordRegex.test(password);
    }

    function validateNotEmpty(value) {
        var passwordRegex = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,}$/;
        return value.trim().length > 0; //passwordRegex.test(value);
    }

    function validateInput(input, showError) {
        var isValid = true;
        var errorElement = $('#' + input.attr('id') + '-validation');

        if (!validateNotEmpty(input.val())) {
            isValid = false;
            if (showError) {
                errorElement.text('This field is required.');
            }
        } else if (input.attr('type') === 'email' && !validateEmail(input.val())) {
            isValid = false;
            if (showError) {
                errorElement.text('Please enter a valid email address.');
            }
        } else if (input.attr('type') === 'password' && !validatePassword(input.val())) {
            isValid = false;
            if (showError) {
                errorElement.text('Password must be at least 6 characters.');
            }
        } else {
            errorElement.text('');
        }

        return isValid;
    }

    function validateForm(formSelector, buttonSelector) {
        var isValid = true;
        $(formSelector + ' input[required]').each(function () {
            if (!validateInput($(this), false)) {
                isValid = false;
            }
        });

        if (formSelector === '#noAccount') {
            var password = $('#password');
            var confirmPassword = $('#confirmPassword');
            var termsAndConditionsCheckbox = $('#TermsAndConditionsCheckbox');

            if (password.val() !== confirmPassword.val()) {
                isValid = false;
                if (password.val().length > 0 && confirmPassword.val().length > 0) {
                    $('#confirmPassword-validation').text('Passwords do not match.');
                }
            } else {
                $('#confirmPassword-validation').text('');
            }

            // Check if the terms and conditions checkbox is checked
            if (!termsAndConditionsCheckbox.is(':checked')) {
                isValid = false;
                $('#terms-validation').text('You must agree to the terms and conditions.');
            } else {
                $('#terms-validation').text('');
            }
        }

        $(buttonSelector).prop('disabled', !isValid);
    }


    $('#btn-sign-up').hide();

    $('input[type=radio][name=HasAccount]').change(function () {
        if (this.value == 'true') {
            $('#hasAccount').show();
            $('#noAccount').hide();

            $('#btn-login').show();
            $('#btn-sign-up').hide();
        }
        else if (this.value == 'false') {
            $('#hasAccount').hide();
            $('#noAccount').show();

            $('#btn-login').hide();
            $('#btn-sign-up').show();
        }

        validateForm('#hasAccount', '#btn-login');
        validateForm('#noAccount', '#btn-sign-up');
    });

    $('#hasAccount input[required], #noAccount input[required], #TermsAndConditionsCheckbox').on('input change', function () {
        validateForm('#hasAccount', '#btn-login');
        validateForm('#noAccount', '#btn-sign-up');
    });

    validateForm('#hasAccount', '#btn-login');
    validateForm('#noAccount', '#btn-sign-up');

    /////////////////////////////////// Login and SignUp Validations ///////////////////////////////////

    ///////////////////////////// Calculate BMI ////////////////////////////////////////////////////////

    function updateBMI() {
        const weight = parseFloat($('#weight').val());
        const height = parseFloat($('#height').val()) / 100; // convert cm to m

        if (!isNaN(weight) && !isNaN(height) && height > 0) {
            const bmi = weight / (height * height);
            $('#BMI').val(bmi.toFixed(2));
        } else {
            $('#BMI').val('');
        }
    }

    $('#weight, #height').on('input', updateBMI);

    ///////////////////////////// Calculate BMI /////////////////////////////////////////////////////////

    ////////////////////////////////////////// Form validations /////////////////////////////////////////
    function dietPlanValidations() {
        var weight = parseFloat($('#weight').val());
        var targetWeight = parseFloat($('#targetWeight').val());
        const height = parseFloat($('#height').val());

        $('#btn-next-step-2').prop('disabled', true);
        $('#btn-next-step-3').prop('disabled', true);
        $('#btn-next-step-4').prop('disabled', true);

        //step 2
        if (isNaN(weight) || isNaN(targetWeight) || weight <= 0 || targetWeight <= 0) {
            $('#btn-next-step-2').prop('disabled', true);
        }
        else {
            $('#btn-next-step-2').prop('disabled', false);
        }

        //step 3
        if (isNaN(height) || height <= 0) {
            $('#btn-next-step-3').prop('disabled', true);
        }
        else {
            $('#btn-next-step-3').prop('disabled', false);
        }

        //step 4
        if ($('#ocupation').val() == '' || $('#ocupation').val() == null) {
            $('#btn-next-step-4').prop('disabled', true);
        }
        else {
            $('#btn-next-step-4').prop('disabled', false);
        }
    }

    dietPlanValidations();

    $('#weight, #targetWeight').on('input', dietPlanValidations);
    $('#height').on('input', dietPlanValidations);
    $('#ocupation').on('input', dietPlanValidations);

    $('#DietaryRestrictions, #foodPreferences, #ocupation').keyup(function () {
        var maxLength = 150;
        var length = $(this).val().length;
        if (length > maxLength) {
            $(this).val($(this).val().substring(0, maxLength));
        }
    });

    ////////////////////////////////////////// Form validations /////////////////////////////////////////

    $("#show-password").click(function () {
        var x = document.getElementById("password");
        if (x.type === "password") {
            x.type = "text";
        } else {
            x.type = "password";
        }
    });

    $("#download-btn").click(function () {
        var HTML_Width = $("#result-container").find("#pdf-content").width();
        var HTML_Height = $("#result-container").find("#pdf-content").height();
        var top_left_margin = 15;
        var PDF_Width = HTML_Width + (top_left_margin * 2);
        var PDF_Height = (PDF_Width * 1.5) + (top_left_margin * 2);
        var canvas_image_width = HTML_Width;
        var canvas_image_height = HTML_Height;

        var totalPDFPages = Math.ceil(HTML_Height / PDF_Height) - 1;

        html2canvas($("#result-container").find("#pdf-content")[0]).then(function (canvas) {
            var imgData = canvas.toDataURL("image/jpeg", 1.0);
            var pdf = new jsPDF('p', 'pt', [PDF_Width, PDF_Height]);
            pdf.addImage(imgData, 'JPG', top_left_margin, top_left_margin, canvas_image_width, canvas_image_height);
            for (var i = 1; i <= totalPDFPages; i++) {
                pdf.addPage(PDF_Width, PDF_Height);
                pdf.addImage(imgData, 'JPG', top_left_margin, -(PDF_Height * i) + (top_left_margin * 4), canvas_image_width, canvas_image_height);
            }
            pdf.save("Your_Diet_Plan.pdf");
        });
    });

    /*
    $("#download-btn").click(function () {
        // clone the element
        var clone = $("#result-container").find("#pdf-content").clone();

        // apply rotation and append to body (make sure it's not visible to the user)
        clone.css({
            'transform': 'rotate(90deg)',
            'position': 'absolute',
            'left': '-1000px', // off screen
            'font-size': '1em', // reduce font size
            'margin-left': '20%', // add left margin
        }).attr('id', 'pdf-content-clone').appendTo('body');

        var HTML_Width = clone.width();
        var HTML_Height = clone.height();
        var top_left_margin = 15;
        var PDF_Width = HTML_Width + (top_left_margin * 3.5);
        var PDF_Height = (PDF_Width * 2.5) + (top_left_margin * 2.5);
        var canvas_image_width = HTML_Width;
        var canvas_image_height = HTML_Height;

        var totalPDFPages = Math.ceil(HTML_Height / PDF_Height) - 1;

        clone.css('transform', 'rotate(360deg)'); // rotate back to original orientation

        html2canvas(clone[0]).then(function (canvas) {
            var imgData = canvas.toDataURL("image/jpeg", 1.0);
            var pdf = new jsPDF('p', 'pt', [PDF_Height, PDF_Width]); // Initiate as portrait

            pdf.addImage(imgData, 'JPG', top_left_margin, top_left_margin, canvas_image_height, canvas_image_width); // flip width and height

            for (var i = 1; i <= totalPDFPages; i++) {
                pdf.addPage(PDF_Height, PDF_Width);
                pdf.addImage(imgData, 'JPG', top_left_margin, -(PDF_Width * i) + (top_left_margin * 4), canvas_image_height, canvas_image_width);
            }

            pdf.save("Your_Diet_Plan.pdf");

            // remove the clone after we're done with it
            clone.remove();
        });
    });
    */

    //--------------------------------------------- Download the PDF file when the button is clicked - end -----------------------------------------

    //---------------------------------------------------------- Print diet plan - tart ------------------------------------------------------------
    $('#print-btn').click(function (e) {
        e.preventDefault();

        // Create a copy of the target div
        var $printContent = $('#result-container').clone();

        // Remove the download button from the copied content
        $printContent.find('#download-btn').remove();

        // Remove the print button from the copied content
        $printContent.find('#print-btn').remove();

        // Create a new window
        var printWindow = window.open('', '', 'width=800,height=800');

        // Write the modified HTML to the new window
        printWindow.document.write('<html><head><title>Diet Plan</title></head><body>');
        printWindow.document.write($printContent.html());
        printWindow.document.write('</body></html>');
        printWindow.document.close();
        printWindow.print();
    });
    //---------------------------------------------------------- Print diet plan - end ------------------------------------------------------------
});

window.onload = function () {
    ////////////////////////////////////////// Terms and Conditions Modal ///////////////////////////////

    // Get the modal
    var modal = document.getElementById("termsAndConditionsModal");

    // Get the <span> element that closes the modal
    var span = document.getElementsByClassName("close")[0];

    // When the user clicks on <span> (x), close the modal
    span.onclick = function () {
        modal.style.display = "none";
    }

    // Alternatively, you can use a function to show the modal
    function showModal() {
        modal.style.display = "block";
    }

    ////////////////////////////////////////// Terms and Conditions Modal ///////////////////////////////

    ////////////////////////////////////////// Terms and conditions link ////////////////////////////////
    // Get the link
    var link = $("#terms_and_conditions_link");

    // When the user clicks the link, open the modal 
    link.on('click', function (e) {e.preventDefault(); showModal(); })

    ////////////////////////////////////////// Terms and conditions link ////////////////////////////////
};
