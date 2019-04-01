function CustomerValidation() {

    var errorMessages = [];

    //Validation for Employee Id
    var employeeId = $("#id").val().trim();

    //in a boolean context, following are considered as false:
    //0, empty string, false, null, undefined.
    if (!employeeId) {
        $("#id").addClass("err")
        errorMessages.push("Employee ID is **Required");
    }
    else if (employeeId.isNaN) {
        $("#Id").addClass("err")
        errorMessages.push("It must be numeric")
    }
    else if (employeeId <= 0) {
        $("#Id").addClass("err")
        errorMessages.push("Id must be greater than 0");
    }

    /////Validation for Employee Name
    var employeeName = $("#name").val().trim();

    if (!employeeName) {
        $("#name").addClass("err")
        errorMessages.push("Employee Name is **Required");
    }
    else if (employeeName.length < 3 || employeeName.length > 25) {
        $("#name").addClass("err")
        errorMessages.push("Name cannot be to long");
    }
    else if (!employeeName.length > 0) {
        $("#name").addClass("err")
        errorMessages.push("Name field cannot be empty");
    }


    /////Validation for Employee City
    var employeeCity = $("#City").val().trim();

    if (!employeeCity) {
        $("#City").addClass("err")
        errorMessages.push("Employee City is **Required");
    }
    else if (employeeCity.length < 3 || employeeName.length > 15) {
        $("#City").addClass("err")
        errorMessages.push("Enter a proper city Name");
    }
    else if (!employeeCity.length > 0) {
        $("#City").addClass("err")
        errorMessages.push("Employee City field cannot be empty");
    }


    //////validation for Email
    var employeeEmail = $("#Email").val().trim();

    if (!validateEmail(employeeEmail)) {
        $("#Email").addClass("err")
        errorMessages.push("enter a proper email id");
    }
    else if (!employeeEmail.length > 0) {
        $("#Email").addClass("err")
        errorMessages.push("Email is **Required");
    }


    /////Validation for Employee Salary
    var employeeSalary = $("#Salary").val().trim();

    if (!employeeSalary) {
        $("#Salary").addClass("err")
        errorMessages.push("Salary is **Required");
    }
    else if (employeeSalary.isNaN) {
        $("#Salary").addClass("err")
        errorMessages.push("Salary must be numeric")
    }
    else if (employeeSalary <= 20000) {
        $("#Salary").addClass("err")
        errorMessages.push("Salary must be greater than 20000");
    }

    //////Validation for Gender ---> radio
    var employeeGender = $("#gender").val();

    if (!$("input[type=radio]:checked").length > 0) {
        $("#gender").addClass("err")
        errorMessages.push("Select atleast one Gender");
    }
    if (errorMessages.length > 0) {
        //some validations errors exits
        //$("ul")--> quries for all ul elements
        //$("<ul>")--> creates a new DOM elements called "ul"
        var errList = $("<ul>");
        for (var i = 0; i < errorMessages.length; i++) {
            $("<li>").html(errorMessages[i]).appendTo(errList);
        }
        $("#errmsg").html(errList);
        return false;
    }

    return true;
}


function validateEmail($email) {
    var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
    return emailReg.test($email);
}

function RemoveItemsInCart(id, index) {
    if (!confirm("Are sure you want to delete this permanently")) {
        return;
    }
    $.ajax({
        url: "/Cart/RemoveItemFromCart/" + id,
        success: function (resp) {
            var id = '#' + index;
            $(id).remove();
        }
    });

}

function AddProductCart(id) {
    $.ajax({
        url: "/Cart/AddToCart/" + id,
        success: function (resp) {
            var out = "";
            if (resp && resp > 0) {
                out = '<a href="/Cart/ViewCart/" class="btn btn-primary btn-block">' + resp + ' items in your cart</a>'
            } else {
                out = '<h5 class="text-center"> your cart is empty!!</h5>'
            }
            $("div#CartDetails").html(out);
        }
    });
}


function AfterLogin() {
    $.ajax({
        method :"get",
        url: "/Customer/Login/",
        success: function (resp) {
            $("#Login").remove();
            $("#Register").remove();
        }
    });
}