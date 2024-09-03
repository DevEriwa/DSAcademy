function ChangeColorToValid(selectedClass) {

    for (var i = 0; i < selectedClass.length; i++) {
        selectedClass[i].style.color = "green";
    }
}
function ChangeColorToInValid(selectedClass) {

    for (var i = 0; i < selectedClass.length; i++) {
        selectedClass[i].style.color = "gray";
    }
}

function HideFormPage11Next() {
    //FORM SECTION INDICATORS
    var registrationFormValidation = document.getElementsByClassName("tickcheck1");
    var personFormValidation = document.getElementsByClassName("tickcheck2");
    var skillFormValidation = document.getElementsByClassName("tickcheck3");

    // FORM INPUTS FOR VALIDATING SCRIPT
    var fName = document.getElementById("FirstName").value;
    var lName = document.getElementById("LastName").value;
    var noba = document.getElementById("PhoneNo").value;
    var userNam = document.getElementById("UserName").value;
    var email = document.getElementById("Email").value;
    var passwrd = document.getElementById("Passwrd").value;
    var cPasswrd = document.getElementById("Cpasswrd").value;
    var address = document.getElementById("Address").value;


    if (fName != "" && lName != "" && noba != "" && userNam != "" && email != "" && passwrd != "" && cPasswrd != "" && passwrd == cPasswrd && address != "") {

        document.getElementById("form11").style.display = "none";
        document.getElementById("form22").style.display = "block";
        document.getElementById("form33").style.display = "none";
        ChangeColorToValid(registrationFormValidation);
        ChangeColorToInValid(personFormValidation);
        ChangeColorToInValid(skillFormValidation);
    }
    else {
        document.getElementById("form11").style.display = "block";
        document.getElementById("form22").style.display = "none";
        document.getElementById("form33").style.display = "none";
        ChangeColorToInValid(registrationFormValidation);
        ChangeColorToInValid(personFormValidation);
        ChangeColorToInValid(skillFormValidation);
        if (fName == "") {
            document.getElementById("FNameValidation").style.display = "block";
        } else {
            document.getElementById("FNameValidation").style.display = "none";
        }
        if (lName == "") {
            document.getElementById("LNameValidation").style.display = "block";
        } else {
            document.getElementById("LNameValidation").style.display = "none";
        }
        if (noba == "") {
            document.getElementById("TelValidation").style.display = "block";
        } else {
            document.getElementById("TelValidation").style.display = "none";
        }
        if (userNam == "") {
            document.getElementById("UserNameValidation").style.display = "block";
        } else {
            document.getElementById("UserNameValidation").style.display = "none";
        }
        if (email == "") {
            document.getElementById("EmailValidation").style.display = "block";
        } else {
            document.getElementById("EmailValidation").style.display = "none";
        }
        if (passwrd == "") {
            document.getElementById("PasswordValidation").style.display = "block";
        } else {
            document.getElementById("PasswordValidation").style.display = "none";
        }
        if (cPasswrd != passwrd) {
            document.getElementById("ConfirmValidation").style.display = "block";
        } else {
            document.getElementById("ConfirmValidation").style.display = "none";
        }
        if (address == "") {
            document.getElementById("AddressValidation").style.display = "block";
        } else {
            document.getElementById("AddressValidation").style.display = "none";
        }
    }

}

// Registration for page 2 Button Next
function HideFormPage22Next() {
    //FORM SECTION INDICATORS
    var registrationFormValidation = document.getElementsByClassName("tickcheck1");
    var personFormValidation = document.getElementsByClassName("tickcheck2");
    var skillFormValidation = document.getElementsByClassName("tickcheck3");

    // FORM INPUTS FOR VALIDATING SCRIPT
    var jNysc = document.getElementById("nysc").value;
    var jLaptop = document.getElementById("laptop").value;
    var jProgrammingExp = document.getElementById("programmingExp").value;
    var jProgrammingExpList = document.getElementById("programmingExpList").value;
    var jStateOfResident = document.getElementById("stateOfResident").value;
    var jReasons = document.getElementById("reasons").value;

    if (jNysc != 0 && jLaptop != 0 && jProgrammingExp != 0 && jStateOfResident != 0 && jReasons != "") {
        if (jProgrammingExp == 1 && jProgrammingExpList == "") {
            document.getElementById("form11").style.display = "none";
            document.getElementById("form22").style.display = "block";
            document.getElementById("form33").style.display = "none";
            ChangeColorToValid(registrationFormValidation);
            ChangeColorToInValid(personFormValidation);
            ChangeColorToInValid(skillFormValidation);
            document.getElementById("ProgrammingExpListValidation").style.display = "block";
        } else {
            document.getElementById("form11").style.display = "none";
            document.getElementById("form22").style.display = "none";
            document.getElementById("form33").style.display = "block";
            ChangeColorToValid(registrationFormValidation);
            ChangeColorToValid(personFormValidation);
            ChangeColorToInValid(skillFormValidation);
        }
    }
    else {
        document.getElementById("form11").style.display = "none";
        document.getElementById("form22").style.display = "block";
        document.getElementById("form33").style.display = "none";
        ChangeColorToValid(registrationFormValidation);
        ChangeColorToInValid(personFormValidation);
        ChangeColorToInValid(skillFormValidation);
        if (jNysc == 0) {
            document.getElementById("NyscValidation").style.display = "block";
        } else {
            document.getElementById("NyscValidation").style.display = "none";
        }
        if (jLaptop == 0) {
            document.getElementById("LaptopValidation").style.display = "block";
        } else {
            document.getElementById("LaptopValidation").style.display = "none";
        }
        if (jProgrammingExp == 0) {
            document.getElementById("programmingExpValidation").style.display = "block";
        } else {
            document.getElementById("programmingExpValidation").style.display = "none";
        }
        if (jStateOfResident == 0) {
            document.getElementById("StateOfResidentValidation").style.display = "block";
        } else {
            document.getElementById("StateOfResidentValidation").style.display = "none";
        }
        if (jReasons == "") {
            document.getElementById("ReasonValidation").style.display = "block";
        } else {
            document.getElementById("ReasonValidation").style.display = "none";
        }
    }

}

// Registration for page 2 Button Previous
function HideFormPage22Previous() {
    document.getElementById("form11").style.display = "block";
    document.getElementById("form22").style.display = "none";
    document.getElementById("form33").style.display = "none";
    var registrationFormValidation = document.getElementsByClassName("tickcheck1");
    var personFormValidation = document.getElementsByClassName("tickcheck2");
    var skillFormValidation = document.getElementsByClassName("tickcheck3");

    ChangeColorToInValid(registrationFormValidation);
    ChangeColorToInValid(personFormValidation);
    ChangeColorToInValid(skillFormValidation);
}

// Registration for page 3 Button Previous
function HideFormPage33Previous() {

    document.getElementById("form11").style.display = "none";
    document.getElementById("form22").style.display = "block";
    document.getElementById("form33").style.display = "none";
    var registrationFormValidation = document.getElementsByClassName("tickcheck1");
    var personFormValidation = document.getElementsByClassName("tickcheck2");
    var skillFormValidation = document.getElementsByClassName("tickcheck3");

    ChangeColorToValid(registrationFormValidation);
    ChangeColorToInValid(personFormValidation);
    ChangeColorToInValid(skillFormValidation);
}

// Registration for page 3 Button Submt
function ApplicationRequest() {

    var registrationFormValidation = document.getElementsByClassName("tickcheck1");
    var personFormValidation = document.getElementsByClassName("tickcheck2");
    var skillFormValidation = document.getElementsByClassName("tickcheck3");
    var skll = document.getElementById("skills").value;
    var surv = document.getElementById("survive").value;
    var cv = document.getElementById("cv").value;


    if (skll != "" && surv != "" && cv != "") {
        ChangeColorToValid(registrationFormValidation);
        ChangeColorToValid(personFormValidation);
        ChangeColorToValid(skillFormValidation);

        ApplicationRequestMain();
    }
    else {
        ChangeColorToValid(registrationFormValidation);
        ChangeColorToValid(personFormValidation);
        ChangeColorToInValid(skillFormValidation);

        if (skll == "") {
            document.getElementById("SkillsValidation").style.display = "block";
        } else {
            document.getElementById("SkillsValidation").style.display = "none";
        }
        if (surv == "") {
            document.getElementById("SurviveValidation").style.display = "block";
        } else {
            document.getElementById("SurviveValidation").style.display = "none";
        }
        if (cv == "") {
            document.getElementById("CvValidation").style.display = "block";
        } else {
            document.getElementById("CvValidation").style.display = "none";
        }
    }

}

function EnableOrDisableInput() {
    var inputControlla = document.getElementById("programmingExp").value;
    var inputToChange = document.getElementById("programmingExpList");

    if (inputControlla == 1) {
        inputToChange.disabled = false;
    } else {
        inputToChange.disabled = true;
    }
}

// APPLICATION REQUEST 
function ApplicationRequestMain() {

    $('#loader').show();
    $('#loader-wrapper').show();

    var file = document.getElementById("cv").files;

    if (file[0] != null) {
        const reader = new FileReader();
        reader.readAsDataURL(file[0]);

        reader.onload = function () {
            var data = {};
            // section 1
            data.FirstName = $('#FirstName').val();
            data.LastName = $('#LastName').val();
            data.PhoneNumber = $('#PhoneNo').val();
            data.UserName = $('#UserName').val();
            data.Email = $('#Email').val();
            data.Password = $('#Passwrd').val();
            data.ConfirmPassword = $('#Cpasswrd').val();
            data.Address = $('#Address').val();

            // section 2
            data.HasCompletedNysc = $('#nysc').val();
            data.HasLaptop = $('#laptop').val();
            data.HasAnyProgrammingExp = $('#programmingExp').val();
            data.ApplicantResideInEnugu = $('#stateOfResident').val();
            data.ProgrammingLanguagesExps = $('#programmingExpList').val();
            data.ReasonForProgramming = $('#reasons').val();

            // section 3
            data.AboutYourSkills = $('#skills').val();
            data.HowDoYouIntendToCopeStatement = $('#survive').val();
            data.CV = reader.result;
            data.fileExtensionChecker = data.CV.includes("application/pdf");

            if (data.fileExtensionChecker) {
                let applicationViewModel = JSON.stringify(data);
                $.ajax({
                    type: 'Post',
                    dataType: 'json',
                    url: '/Accounts/Application',
                    data:
                    {
                        applicationUserViewModel: applicationViewModel,
                    },
                    success: function (result) {

                        if (!result.isError) {
                            $("#loader").fadeOut(3000);

                            var url = '/Accounts/Login';
                            successAlertWithRedirect(result.msg, url)
                        }
                        else {
                            $("#loader").fadeOut(3000);
                            errorAlert(result.msg)
                        }
                    },
                    error: function (ex) {
                        $("#loader").fadeOut(3000);
                        errorAlert("Error occured try again");
                    }
                });
            }
            else {
                $("#loader").fadeOut(3000);
                errorAlert("Only PDF file is allowed");
            }
        }
    }
}

// LOGIN POST ACTION
function loginPost() {

    $('#loader').show();
    $('#loader-wrapper').show();

    var email = document.getElementById("Email").value;
    var Passwrd = document.getElementById("Passwrd").value;
    if (email != null && Passwrd != null) {

        var data = {};
        data.Email = $("#Email").val();
        data.Password = $("#Passwrd").val();
        let loginViewModel = JSON.stringify(data);
        $.ajax({
            type: 'POST',
            dataType: 'Json',
            url: '/Accounts/Login',
            data:
            {
                loginViewModel: loginViewModel
            },
            success: function (result) {


                if (result.isNotVerified) {
                    $("#loader").fadeOut(3000);
                    errorAlert(result.msg)
                }
                else if (!result.isError) {
                    $("#loader").fadeOut(3000);
                    successAlertWithRedirect(result.msg, result.dashboard)
                }
                else {
                    $("#loader").fadeOut(3000);
                    errorAlert(result.msg)
                }
            },
            Error: function (ex) {
                $("#loader").fadeOut(3000);
                errorAlert(ex);
            }
        });
    }
}

// Admin Registration POST ACTION
function RegisterAdmin() {
    debugger;
    var companyLogo = document.getElementById("companyLogo").files;
    var data = {};
    data.CompanyName = $('#companyName').val();
    data.CompanyAddress = $('#companyAddress').val();
    data.CompanyPhone = $('#companyPhone').val();
    data.CompanyMobile = $('#companyMobile').val();
    data.FirstName = $('#FirstName').val();
    data.LastName = $('#LastName').val();
    data.CompanyEmail = $('#companyEmail').val();
    data.Password = $('#Password').val();
    data.ConfirmPassword = $('#ConfirmPassword').val();
    data.Address = $('#Address').val();
    data.CheckBox = $('#termsCondition').is(":checked");
    const reader = new FileReader();
    var base64;
    if (companyLogo.length > 0) {
        reader.readAsDataURL(companyLogo[0]);
        reader.onload = function () {
            base64 = reader.result;
            if (data.FirstName != ""
                && data.LastName != ""
                && data.CompanyEmail != ""
                && data.Password != ""
                && data.Password == data.ConfirmPassword
                && data.CompanyName != ""
                && data.CompanyAddress != ""
                && data.CompanyPhone != "") {

                let applicationViewModel = JSON.stringify(data);
                $.ajax({
                    type: 'POST',
                    dataType: 'Json',
                    url: '/Accounts/AdminRegisteration',
                    data:
                    {
                        applicationUserViewModel: applicationViewModel,
                        base64: base64
                    },
                    success: function (result) {

                        if (!result.isError) {
                            $("#loader").fadeOut(3000);
                            var url = '/Accounts/Login'
                            successAlertWithRedirect(result.msg, url)
                        }
                        else {
                            $("#loader").fadeOut(3000);
                            errorAlert(result.msg)
                        }
                    },
                    Error: function (ex) {
                        $("#loader").fadeOut(3000);
                        errorAlert(ex);
                    }
                });
            }
            else {
                if (data.FirstName == "") {
                    document.querySelector("#fNameVDT").style.display = "block";
                } else {
                    document.querySelector("#fNameVDT").style.display = "none";
                }
                if (data.LastName == "") {
                    document.querySelector("#lNameVDT").style.display = "block";
                } else {
                    document.querySelector("#lNameVDT").style.display = "none";
                }
                if (data.Email == "") {
                    document.querySelector("#emailVDT").style.display = "block";
                } else {
                    document.querySelector("#emailVDT").style.display = "none";
                }
                if (data.Password == "") {
                    document.querySelector("#passwrdVDT").style.display = "block";
                } else {
                    document.querySelector("#passwrdVDT").style.display = "none";
                    if (data.Password != data.ConfirmPassword) {
                        document.querySelector("#cpasswrdVDT").style.display = "block";
                    } else {
                        document.querySelector("#cpasswrdVDT").style.display = "none";
                    }
                }
            }
        }
    }
}

function viewApplcantsData(HasCompletedNysc, HasLaptop, HasAnyProgrammingExp, ApplicantResideInEnugu, ProgrammingLanguagesExps, ReasonForProgramming) {

    var nyscCheck = HasCompletedNysc;
    var laptopCheck = HasLaptop;
    var programmingExpCheck = HasAnyProgrammingExp;
    var ResideInEnuguCheck = ApplicantResideInEnugu;
    if (nyscCheck == "Yes") {
        document.querySelector("#nyscChecker1").style.display = "block";
        document.querySelector("#nyscChecker2").style.display = "none";
    } else {
        document.querySelector("#nyscChecker1").style.display = "none";
        document.querySelector("#nyscChecker2").style.display = "block";
    }
    if (laptopCheck == "Yes") {
        document.querySelector("#laptopChecker1").style.display = "block";
        document.querySelector("#laptopChecker2").style.display = "none";

    } else {
        document.querySelector("#laptopChecker1").style.display = "none";
        document.querySelector("#laptopChecker2").style.display = "block";
    }
    if (programmingExpCheck == "Yes") {
        document.querySelector("#programmingExpChecker1").style.display = "block";
        document.querySelector("#programmingExpChecker2").style.display = "none";
    } else {
        document.querySelector("#programmingExpChecker1").style.display = "none";
        document.querySelector("#programmingExpChecker2").style.display = "block";
    }
    if (ResideInEnuguCheck == "Yes") {
        document.querySelector("#resideInEnuguChecker1").style.display = "block";
        document.querySelector("#resideInEnuguChecker2").style.display = "none";
    } else {
        document.querySelector("#resideInEnuguChecker1").style.display = "none";
        document.querySelector("#resideInEnuguChecker2").style.display = "block";
    }

    $("#nysc").val(nyscCheck);
    $("#laptop").val(laptopCheck);
    $("#programmingExp").val(programmingExpCheck);
    $("#ApplicantResideInEnugu").val(ResideInEnuguCheck);
    $("#programmingExpList").val(ProgrammingLanguagesExps);
    $("#reasons").val(ReasonForProgramming);
}
// ACCEPT APPLICATION  POST ACTION
function AcceptApplicationPost() {
    $('#loader').show();
    $('#loader-wrapper').show();

    var actionType = "Accepted";
    var data = {};
    data.Id = $("#qualifyId").val();
    data.Status = actionType;
    let applicationToBeAccepted = JSON.stringify(data);
    $.ajax({
        type: 'POST',
        dataType: 'Json',
        url: '/Admin/ApplicationStatusPost',
        data:
        {
            applicantDetails: applicationToBeAccepted
        },
        success: function (result) {

            if (!result.isError) {
                $("#loader").fadeOut(3000);
                var url = '/Admin/InterviewResult'
                successAlertWithRedirect(result.msg, url)
            }
            else {
                $("#loader").fadeOut(3000);
                errorAlert(result.msg)
            }
        },
        Error: function (ex) {
            $("#loader").fadeOut(3000);
            errorAlert(ex);
        }
    });
}

// REJECT APPLICATION POST ACTION
function RejectApplicationPost() {
    $('#loader').show();
    $('#loader-wrapper').show();

    var actionType = "Rejected";
    var data = {};
    data.Id = $("#disqualifyId").val();
    data.Status = actionType;
    let applicationToBeRejected = JSON.stringify(data);
    $.ajax({
        type: 'POST',
        dataType: 'Json',
        url: '/Admin/ApplicationStatusPost',
        data:
        {
            applicantDetails: applicationToBeRejected
        },
        success: function (result) {

            if (!result.isError) {
                $("#loader").fadeOut(3000);
                var url = '/Admin/InterviewResult'
                successAlertWithRedirect(result.msg, url)
            }
            else {
                $("#loader").fadeOut(3000);
                errorAlert(result.msg)
            }
        },
        Error: function (ex) {
            $("#loader").fadeOut(3000);
            errorAlert(ex);
        }
    });
}

// REJECT APPLICATION POST ACTION
function RejectNewApplicationPost() {
    $('#loader').show();
    $('#loader-wrapper').show();

    var actionType = "Rejected";
    var data = {};
    data.Id = $("#rejectApplicantId").val();
    data.Status = actionType;
    let applicationToBeRejected = JSON.stringify(data);
    $.ajax({
        type: 'POST',
        dataType: 'Json',
        url: '/Admin/ApplicationStatusPost',
        data:
        {
            applicantDetails: applicationToBeRejected
        },
        success: function (result) {

            if (!result.isError) {
                $("#loader").fadeOut(3000);
                var url = '/Admin/OnboardingStudent'
                successAlertWithRedirect(result.msg, url)
            }
            else {
                $("#loader").fadeOut(3000);
                errorAlert(result.msg)
            }
        },
        Error: function (ex) {
            $("#loader").fadeOut(3000);
            errorAlert(ex);
        }
    });
}
// QUALIFY APPLICANT FOR INTERVIEW  POST ACTION
function QualifyApplicantForInterview() {
    $('#loader').show();
    $('#loader-wrapper').show();

    var data = $("#acceptApplicantId").val();
    let applicationData = data;
    $.ajax({
        type: 'POST',
        dataType: 'Json',
        url: '/Admin/QualifyApllicantFoeWrittenInterview',
        data:
        {
            userId: applicationData
        },
        success: function (result) {

            if (!result.isError) {
                $("#loader").fadeOut(3000);
                var url = '/Admin/OnboardingStudent'
                successAlertWithRedirect(result.msg, url)
            }
            else {
                $("#loader").fadeOut(3000);
                errorAlert(result.msg)
            }
        },
        Error: function (ex) {
            $("#loader").fadeOut(3000);
            errorAlert(ex);
        }
    });
}
// DOCUMENTATION SUBMISSION 
function ApplicantDocumentation() {

    var file = {};
    file.FirstGuarantor = document.getElementById("FirstGuarantor").files;
    file.SecondGuarantor = document.getElementById("SecondGuarantor").files;
    file.NepaBill = document.getElementById("NepaBill").files;
    file.Contractforms = document.getElementById("Contractforms").files;
    var BVN = $('#BVN').val();
    if (file.FirstGuarantor[0] != null && file.SecondGuarantor[0] != null && file.NepaBill[0] != null && file.Contractforms[0] != null && BVN != null) {

        $('#loader').show();
        $('#loader-wrapper').show();
        if (file.FirstGuarantor[0] != null) {
            const reader = new FileReader();
            reader.readAsDataURL(file.FirstGuarantor[0]);
            var base64FirstGuarantor;
            reader.onload = function () {
                base64FirstGuarantor = reader.result;


                if (file.SecondGuarantor[0] != null) {
                    const reader = new FileReader();
                    reader.readAsDataURL(file.SecondGuarantor[0]);
                    var base64SecondGuarantor;
                    reader.onload = function () {
                        base64SecondGuarantor = reader.result;

                        if (file.NepaBill[0] != null) {
                            const reader = new FileReader();
                            reader.readAsDataURL(file.NepaBill[0]);
                            var base64NepaBill;
                            reader.onload = function () {
                                base64NepaBill = reader.result;

                                if (file.Contractforms[0] != null) {
                                    const reader = new FileReader();
                                    reader.readAsDataURL(file.Contractforms[0]);
                                    var base64Contractforms;
                                    reader.onload = function () {
                                        base64Contractforms = reader.result;
                                        var allDocument = {};
                                        allDocument.BVN = BVN;
                                        allDocument.FirstGuarantor = base64FirstGuarantor;
                                        allDocument.SecondGuarantor = base64SecondGuarantor;
                                        allDocument.NepaBill = base64NepaBill;
                                        allDocument.SignedContract = base64Contractforms;

                                        if (BVN != "" || BVN != 0) {
                                            let rawData = JSON.stringify(allDocument);
                                            $.ajax({
                                                type: 'Post',
                                                dataType: 'Json',
                                                url: '/Student/SubmitApplicantDocument',
                                                data:
                                                {
                                                    applicantDetailedDocuments: rawData,
                                                },
                                                success: function (result) {

                                                    if (result.isError) {
                                                        $("#loader").fadeOut(3000);
                                                        errorAlert(result.msg)
                                                    }
                                                    else {
                                                        $("#loader").fadeOut(3000);
                                                        var url = '/Student/ViewApplicantsDocumentation';
                                                        newSuccessAlert(result.msg, url);
                                                    }
                                                },
                                                error: function (ex) {
                                                    $("#loader").fadeOut(3000);
                                                    errorAlert("Error occured try again");
                                                }
                                            })
                                        }
                                        else {
                                            $("#loader").fadeOut(3000);
                                            errorAlert("Incorrect Details");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

            }
        }
    }
    else {

        if (BVN == "") {
            document.querySelector("#BVNVDT").style.display = "block";
        } else {
            document.querySelector("#BVNVDT").style.display = "none";
        }
        if (file.FirstGuarantor[0] == null) {
            document.querySelector("#FirstGuarantorVDT").style.display = "block";
        } else {
            document.querySelector("#FirstGuarantorVDT").style.display = "none";
        }
        if (file.SecondGuarantor[0] == null) {
            document.querySelector("#SecondGuarantorVDT").style.display = "block";
        } else {
            document.querySelector("#SecondGuarantorVDT").style.display = "none";
        }
        if (file.NepaBill[0] == null) {
            document.querySelector("#NepaBillVDT").style.display = "block";
        } else {
            document.querySelector("#NepaBillVDT").style.display = "none";
        }
        if (file.Contractforms[0] == null) {
            document.querySelector("#ContractformsVDT").style.display = "block";
        } else {
            document.querySelector("#ContractformsVDT").style.display = "none";
        }
    }
}

// CHANGE PASSWORD POST ACTION
function ChangePasswordPost() {

    $('#loader').show();
    $('#loader-wrapper').show();
    var data = {};
    data.OldPassword = $("#OldPasswrd").val();
    data.NewPassword = $("#NewPasswrd").val();
    data.ConfirmPassword = $("#Cpasswrd").val();
    let changePasswordDetails = JSON.stringify(data);
    $.ajax({
        type: 'POST',
        dataType: 'Json',
        url: '/Accounts/ChangePasswordPost',
        data:
        {
            userPasswordDetails: changePasswordDetails
        },
        success: function (result) {

            if (!result.isError) {
                $("#loader").fadeOut(3000);
                successAlertWithRedirect(result.msg, result.url)
            }
            else {
                $("#loader").fadeOut(3000);
                errorAlert(result.msg)
            }
        },
        Error: function (ex) {
            $("#loader").fadeOut(3000);
            errorAlert(ex);
        }
    });
}

// FORGOT PASSWORD POST ACTION
function ForgotPasswordReset() {
    var email = $("#Email").val();
    if (email != "") {
        let emailAccount = email;
        $.ajax({
            type: 'POST',
            url: '/Accounts/ForgotPassword', // we are calling json method
            dataType: 'json',
            data:
            {
                email: emailAccount,
            },
            success: function (result) {

                if (!result.isNotVerified) {
                    var url = '/Accounts/Login';
                    successAlertWithRedirect(result.msg, url)
                }
                else {
                    errorAlert(result.msg);
                }
            },
            Error: function (ex) {
                errorAlert(ex);
            }
        });
    }
    else {
        errorAlert("Enter your email");
    }
}

// SET NEW PASSWORD POST ACTION
function resetPassword() {

    $('#loader').show();
    $('#loader-wrapper').show();
    var data = {};
    data.Password = $("#newPasswordId").val();
    data.ConfirmPassword = $("#confirmPasswordId").val();
    data.Token = $("#Token").val();
    let resetPasswordViewModel = JSON.stringify(data);
    if (data.Password != "" && data.ConfirmPassword != "" && data.Password == data.ConfirmPassword) {
        $.ajax({
            type: 'POST',
            url: '/Accounts/ResetUserPassword', // we are calling json method
            dataType: 'json',
            data:
            {
                viewmodel: resetPasswordViewModel,
            },
            success: function (result) {

                if (!result.isError) {
                    $("#loader").fadeOut(3000);
                    successAlert(result.msg)
                    window.location.reload;
                }
                else {
                    $("#loader").fadeOut(3000);
                    errorAlert(result.msg);
                }
            },
            error: function (ex) {
                $("#loader").fadeOut(3000);
                errorAlert(ex);
            }
        });
    } else {
        if (data.Password == "") {
            errorAlert("Enter password");
        }
        if (data.Password != data.ConfirmPassword) {
            errorAlert("Password and password confirm not matched");
        }
    }
}

function CreateTrainingCourse(action) {
    var defaultBtnValue = $('#submit_btn').html();
    $('#submit_btn').html("Please wait...");
    $('#submit_btn').attr("disabled", true);
    var logo = document.getElementById("logoId").files;
    var data = {};
    data.Title = $('#Title').val();
    data.Description = $('#Description').val();
    data.Amount = $('#Cost').val();
    data.Duration = $('#Duration').val();
    data.TestDuration = $('#TestDuration').val();
    data.ProgramStatus = $('#programId').val();
    data.ActionType = action;
    const reader = new FileReader();

    var base64;
    if (logo.length > 0) {
        reader.readAsDataURL(logo[0]);
        reader.onload = function ()
        {
            base64 = reader.result;
            if (data.Title == "" || data.Description == "" || data.Amount == "" || data.Duration == "" || data.TestDuration == "") {
                if (data.Title == "") {
                    document.querySelector("#titleVDT").style.display = "block";
                } else {
                    document.querySelector("#titleVDT").style.display = "none";
                }
                if (data.Description == "") {
                    document.querySelector("#descVDT").style.display = "block";
                } else {
                    document.querySelector("#descVDT").style.display = "none";
                }
                if (data.Amount == "") {
                    document.querySelector("#amtcVDT").style.display = "block";
                } else {
                    document.querySelector("#amtcVDT").style.display = "none";
                }
                if (data.Duration == "") {
                    document.querySelector("#durationVDT").style.display = "block";
                } else {
                    document.querySelector("#durationVDT").style.display = "none";
                }
                if (data.TestDuration == "") {
                    document.querySelector("#testDurationVDT").style.display = "block";
                } else {
                    document.querySelector("#testDurationVDT").style.display = "none";
                }
                if (data.Logo == "") {
                    document.querySelector("#testLogoVDT").style.display = "block";
                } else {
                    document.querySelector("#testLogoVDT").style.display = "none";
                }
            }
            else {
                document.querySelector("#titleVDT").style.display = "none";
                document.querySelector("#descVDT").style.display = "none";
                document.querySelector("#amtcVDT").style.display = "none";
                document.querySelector("#durationVDT").style.display = "none";
                document.querySelector("#testDurationVDT").style.display = "none";
                document.querySelector("#testLogoVDT").style.display = "none";

                $('#submit_btn').html(defaultBtnValue);
                SendTrainingCourseToController(data, base64);
            }
        }
    }
   
}

function getCourseById(id) {
    $.ajax({
        type: 'GET',
        url: '/Admin/GetTrainingCourseById',
        dataType: 'json',
        data:
        {
            id: id
        },
        success: function (data) {
            if (!data.isError) {
                $('#edit_Id').val(data.id);
                $('#Delete_Id').val(data.id);
                $('#Description_Id').val(data.id);
                $('#Description').val(data.description);
                $('#editTitle').val(data.title);
                $('#editDescription').val(data.description);
                $('#editCost').val(data.amount);
                $('#editTestDuration').val(data.duration);
                $('#editDuration').val(data.testDuration);
                $('#editProgramId').val(data.programStatus).trigger('change');
               // $('#upgrade_Course').modal('show');
            }
        },
        error: function (ex) {
            "Something went wrong, contact support - " + errorAlert(ex);
        }
    });
};

function getCourseDescriptionById(id) {
    $.ajax({
        type: 'GET',
        url: '/Admin/GetTrainingCourseById',
        dataType: 'json',
        data:
        {
            id: id
        },
        success: function (data) {
            if (!data.isError) {
                $('#description_Id').val(data.id);
                $('#description').val(data.description);
                $('#description_Modal').modal('show');
            }
        },
        error: function (ex) {
            "Something went wrong, contact support - " + errorAlert(ex);
        }
    });
};

function EditTrainingCourse(action) {
    var defaultBtnValue = $('#submit_btn').html();
    $('#submit_btn').html("Please wait...");
    $('#submit_btn').attr("disabled", true);
    debugger
    var data = {};
    var logo = document.getElementById("editlogoId").files;
    data.Id = $('#edit_Id').val();
    data.Title = $('#editTitle').val();
    data.Description = $('#editDescription').val();
    data.Amount = $('#editCost').val();
    data.Duration = $('#editDuration').val();
    data.TestDuration = $('#editTestDuration').val();
    data.ProgramStatus = $('#editProgramId').val();
    data.ActionType = action;
    const reader = new FileReader();

    var base64;
    if (logo.length > 0) {
        reader.readAsDataURL(logo[0]);
        reader.onload = function () {
            base64 = reader.result;
        }
    }
    $('#submit_btn').html(defaultBtnValue);
    SendTrainingCourseToController(data, base64);
}

function SendTrainingCourseToController(data, base64) {
    var defaultBtnValue = $('#submit_btn').html();
    $('#submit_btn').html("Please wait...");
    $('#submit_btn').attr("disabled", true);
    let collectedData = JSON.stringify(data);
    $.ajax({
        type: 'Post',
        dataType: 'json',
        url: '/Admin/TrainingCoursePost',
        data:
        {
            collectedTrainingData: collectedData,
            base64: base64
        },
        success: function (result) {
            if (!result.isError) {
                var url = '/Admin/TrainingCourse';
                successAlertWithRedirect(result.msg, url)
                $('#submit_btn').html(defaultBtnValue);
            }
            else {
                $('#submit_btn').html(defaultBtnValue);
                $('#submit_btn').attr("disabled", false);
                errorAlert(result.msg)
            }
        },
        error: function (ex) {
            $('#submit_btn').html(defaultBtnValue);
            $('#submit_btn').attr("disabled", false);
            errorAlert("Error occured try again");
        }
    });
}
function getCourseByIdToDelete(id) {
    $.ajax({
        type: 'GET',
        url: '/Admin/GetTrainingCourseById',
        dataType: 'json',
        data:
        {
            id: id
        },
        success: function (data) {
            if (!data.isError) {
                $('#Delete_Id').val(data.id);
                $('#delete_Course').modal('show');
            }
        },
        error: function (ex) {
            "Something went wrong, contact support - " + errorAlert(ex);
        }
    });
};
function TrainingCoursePost(action, id) {
    if (action == 'DeactivateTrainingCourse' || action == 'ActivateTrainingCourse' && id != "") {
        var data = {};
        data.Id = id;
        data.ActionType = action;
        if (data.Id != "") {
            SendTrainingCourseToController(data);
        } else {
            errorAlert("Error occured try again");
        }
    }
    else if (action == 'DeleteTrainingCourse') {
        var data = {};
        data.Id = $('#Delete_Id').val();
        data.ActionType = action;
        if (data.Id != "") {
            SendTrainingCourseToController(data);
        } else {
            errorAlert("Error occured try again");
        }
    }
}

// GET TRAINING COST FOR EDIT
function GetTrainingCourseById(Id) {

    $.ajax({
        type: 'GET',
        url: '/Admin/GetTrainingCourseById', // we are calling json method
        data: { Id: Id },
        dataType: 'json',
        success: function (data) {

            if (!data.isError) {
                $("#Delete_Id").val(data.id);
                $("#Edit_Id").val(data.id);
                $("#Edit_Title").val(data.title);
                $("#Edit_Description").val(data.description);
                $("#Edit_Cost").val(data.amount);
                $("#Edit_Duration").val(data.duration);
                $("#Edit_TestDuration").val(data.testDuration);
                $("#Edit_Logo").val(data.logo);
            }
        }
    });
}

// GET PAYMENT BY ID
function GetPaymentById(Id) {

    $.ajax({
        type: 'GET',
        url: '/Admin/GetPaymentById', // we are calling json method
        data: { Id: Id },
        dataType: 'json',
        success: function (data) {

            if (!data.isError) {
                $("#approvedId").val(data.id);
                $("#declineId").val(data.id);
            }
        }
    });
}

// APPROVE PAYMENT  POST ACTION
function ApprovePaymentPost() {

    $('#loader').show();
    $('#loader-wrapper').show();
    var actionType = "Approved";
    var data = {};
    data.Id = $("#approvedId").val();
    data.Status = actionType;
    if (data.Id > 0 && data.Id != '') {
        let paymentdata = JSON.stringify(data);
        $.ajax({
            type: 'POST',
            dataType: 'Json',
            url: '/Admin/PaymentPostAction',
            data:
            {
                collectedPaymentID: paymentdata
            },
            success: function (result) {

                if (!result.isError) {
                    $("#loader").fadeOut(3000);
                    var url = '/Admin/Payments'
                    successAlertWithRedirect(result.msg, url)
                }
                else {
                    $("#loader").fadeOut(3000);
                    errorAlert(result.msg)
                }
            },
            Error: function (ex) {
                $("#loader").fadeOut(3000);
                errorAlert(ex);
            }
        });
    }
    else {
        $("#loader").fadeOut(3000);
        errorAlert("Error Occurred, Please Try Again");
    }

}

// DECLINE PAYMEN POST ACTION
function DeclinePaymentPost() {

    $('#loader').show();
    $('#loader-wrapper').show();
    var actionType = "Declined";
    var data = {};
    data.Id = $("#declineId").val();
    data.Status = actionType;
    if (data.Id > 0 && data.Id != '') {
        let paymentdata = JSON.stringify(data);
        $.ajax({
            type: 'POST',
            dataType: 'Json',
            url: '/Admin/PaymentPostAction',
            data:
            {
                collectedPaymentID: paymentdata
            },
            success: function (result) {

                if (!result.isError) {
                    $("#loader").fadeOut(3000);
                    var url = '/Admin/Payments'
                    successAlertWithRedirect(result.msg, url)
                }
                else {
                    $("#loader").fadeOut(3000);
                    errorAlert(result.msg)
                }
            },
            Error: function (ex) {
                $("#loader").fadeOut(3000);
                errorAlert(ex);
            }
        });
    }
    else {
        $("#loader").fadeOut(3000);
        errorAlert("Error Occurred, Please Try Again");
    }
}

function manualSubmitTest(action, Id) {

    $('#loader').show();
    $('#loader-wrapper').show();
    var data = [];
    var allCheckedFeatures = document.getElementsByClassName("chkd");

    for (var i = 0; i < allCheckedFeatures.length; i++) {
        var ckd = { questionId: '', selectedAnswer: '' };

        if (allCheckedFeatures[i].checked) {
            ckd.questionId = allCheckedFeatures[i].name;
            ckd.selectedAnswer = allCheckedFeatures[i].value;
            data.push(ckd);
        }
    }
    var resultCount = data.length;
    if (resultCount == 10) {
        //Check if answers has been submitted automatically
        var stopper = $("#manualStopper").val();
        if (stopper == "Off") {
            $("#autoStopper").val("On");

            let dataa = {};
            dataa.AnsweredQuestions = data;
            dataa.ActionType = action;
            dataa.Id = Id;
            let QandA = JSON.stringify(dataa);
            $.ajax({
                type: 'Post',
                dataType: 'json',
                url: '/Student/SubmitTestQuestions',
                data:
                {
                    collectedData: QandA,
                },
                success: function (result) {

                    if (!result.isError) {
                        $("#loader").fadeOut(3000);
                        var url = '/Student/TakeTest/' + result.courseID;
                        successAlertWithRedirect(result.msg, url)
                    }
                    else {
                        $("#loader").fadeOut(3000);
                        errorAlert(result.msg)
                    }
                },
                error: function (ex) {
                    $("#loader").fadeOut(3000);
                    errorAlert("Error occured try again");
                }
            });
        }
    }
    else {
        $("#loader").fadeOut(3000);
        errorAlert("Answer All the questions");
    }

}

function autoSubmitTest(action, Id) {

    //Check if answers has been submitted manually
    var stopper = $("#autoStopper").val();
    if (stopper == "Off") {
        $("#manualStopper").val("On");

        $('#loader').show();
        $('#loader-wrapper').show();
        var data = [];
        var allCheckedFeatures = document.getElementsByClassName("chkd");

        for (var i = 0; i < allCheckedFeatures.length; i++) {
            var ckd = { questionId: '', selectedAnswer: '' };

            if (allCheckedFeatures[i].checked) {
                ckd.questionId = allCheckedFeatures[i].name;
                ckd.selectedAnswer = allCheckedFeatures[i].value;
                data.push(ckd);
            }
        }
        var resultCount = data.length;

        let dataa = {};
        dataa.AnsweredQuestions = data;
        dataa.ActionType = action;
        dataa.Id = Id;
        let QandA = JSON.stringify(dataa);
        $.ajax({
            type: 'Post',
            dataType: 'json',
            url: '/Student/SubmitTestQuestions',
            data:
            {
                collectedData: QandA,
            },
            success: function (result) {

                if (!result.isError) {
                    $("#loader").fadeOut(3000);
                    var url = '/Student/TakeTest/' + result.courseID;
                    successAlertWithRedirect(result.msg, url)
                }
                else {
                    $("#loader").fadeOut(3000);
                    errorAlert(result.msg)
                }
            },
            error: function (ex) {
                $("#loader").fadeOut(3000);
                errorAlert("Error occured try again");
            }
        });
    }
}

// SUBMIT PROJECT TOPICS
function SubmitProjectTopic() {

    $('#loader').show();
    $('#loader-wrapper').show();
    var data = {};
    data.CourseId = $("#name").val();
    data.Title = $("#title").val();
    data.Description = $("#description").val();
    if (data.CourseId != 0 && data.Title != "" && data.Description != "") {
        let topicsData = JSON.stringify(data);
        $.ajax({
            type: 'POST',
            dataType: 'Json',
            url: '/Student/UploadProjectTopics',
            data:
            {
                topics: topicsData
            },
            success: function (result) {

                if (!result.isError) {
                    $("#loader").fadeOut(3000);
                    var url = '/Student/ProjectTopics'
                    successAlertWithRedirect(result.msg, url)
                }
                else {
                    $("#loader").fadeOut(3000);
                    errorAlert(result.msg)
                }
            },
            Error: function (ex) {
                $("#loader").fadeOut(3000);
                errorAlert(ex);
            }
        });
    }
    else {
        $("#loader").fadeOut(3000);
        if (data.CourseId == 0) {
            document.querySelector("#nameVDT").style.display = "block"
        } else {
            document.querySelector("#nameVDT").style.display = "none"
        }
        if (data.Title == "") {
            document.querySelector("#titleVDT").style.display = "block"
        } else {
            document.querySelector("#titleVDT").style.display = "none"
        }
        if (data.Description == "") {
            document.querySelector("#descriptionVDT").style.display = "block"
        } else {
            document.querySelector("#descriptionVDT").style.display = "none"
        }
    }

}

// SUBMIT PROJECT GIT LINK
function AddProjectLink() {

    $('#loader').show();
    $('#loader-wrapper').show();
    var projectData = {};
    projectData.Id = $("#update_Id").val();
    projectData.GitLink = $("#git_link").val();
    projectData.RedmineLink = $("#redmine_link").val();
    if (projectData.GitLink != "" && projectData.RedmineLink != "") {
        let topicData = JSON.stringify(projectData);
        $.ajax({
            type: 'POST',
            dataType: 'Json',
            url: '/Student/ProjectLinkUpdatePost',
            data:
            {
                topicData: topicData
            },
            success: function (result) {

                if (!result.isError) {
                    $("#loader").fadeOut(3000);
                    var url = '/Student/AprovedTopics'
                    successAlertWithRedirect(result.msg, url)
                }
                else {
                    $("#loader").fadeOut(3000);
                    errorAlert(result.msg)
                }
            },
            Error: function (ex) {
                $("#loader").fadeOut(3000);
                errorAlert(ex);
            }
        });
    }
    else {
        $("#loader").fadeOut(3000);
        if (projectData.GitLink == "") {
            document.querySelector("#git_linkVDT").style.display = "block"
        } else {
            document.querySelector("#git_linkVDT").style.display = "none"
        }
        if (projectData.RedmineLink == "") {
            document.querySelector("#redmine_linkVDT").style.display = "block"
        } else {
            document.querySelector("#redmine_linkVDT").style.display = "none"
        }
    }

}

// EDIT PROJECT GIT LINK
function EditProjectLink(action) {

    $('#loader').show();
    $('#loader-wrapper').show();
    var projectData = {};
    projectData.Id = $("#edit_Id").val();
    projectData.GitLink = $("#edit_Git_link").val();
    projectData.RedmineLink = $("#edit_Redmine_link").val();
    if (projectData.GitLink != "" && projectData.RedmineLink != "") {
        let topicData = JSON.stringify(projectData);
        $.ajax({
            type: 'POST',
            dataType: 'Json',
            url: '/Student/ProjectLinkUpdatePost',
            data:
            {
                topicData: topicData
            },
            success: function (result) {

                if (!result.isError) {
                    $("#loader").fadeOut(3000);
                    var url = '/Student/AprovedTopics'
                    successAlertWithRedirect(result.msg, url)
                }
                else {
                    $("#loader").fadeOut(3000);
                    errorAlert(result.msg)
                }
            },
            Error: function (ex) {
                $("#loader").fadeOut(3000);
                errorAlert(ex);
            }
        });
    }
    else {
        $("#loader").fadeOut(3000);
        if (projectData.GitLink == "") {
            document.querySelector("#edit_Git_linkVDT").style.display = "block"
        } else {
            document.querySelector("#edit_Git_linkVDT").style.display = "none"
        }
        if (projectData.RedmineLink == "") {
            document.querySelector("#edit_Redmine_linkVDT").style.display = "block"
        } else {
            document.querySelector("#edit_Redmine_linkVDT").style.display = "none"
        }
    }

}

// GET Project Links
function GetLinksById(Id) {

    $.ajax({
        type: 'GET',
        url: '/Student/GetProjectLinksById',
        data: { Id: Id },
        dataType: 'json',
        success: function (data) {

            if (!data.isError) {
                document.getElementById("gitLinkView").innerHTML = data.gitLink;
                document.getElementById("redmineLinkView").innerHTML = data.redmineLink;
                $("#copyGit").val(data.gitLink);
                $("#copyRedmine").val(data.redmineLink);
            }
        }
    });
}

//SEND COURSEID FOR PAYMENT 
function SendCourseIdForPayment() {

    $('#loader').show();
    $('#loader-wrapper').show();
    var id = $('#courseId').val();
    let Id = id;
    $.ajax({
        type: 'Post',
        dataType: 'json',
        url: '/Student/GetPaymentDetials',
        data:
        {
            Id: Id,
        },
        success: function (result) {

            if (!result.isError) {
                $("#loader").fadeOut(3000);
                Swal.fire({
                    title: "Success",
                    text: "You're good to go,Let's make the payment now",
                    icon: "success",
                    timer: "30000",
                    confirmButtonColor: "##0253cc",

                })
                    .then(function () {
                        location.href = result.paystackUrl;
                    });
            }
        },
        error: function (ex) {
            $("#loader").fadeOut(3000);
            errorAlert("Error occured try again");
        }
    });
}
function payUpdate(courseId, amount, programStatus) {
    $('#courseId').val(courseId);
    $('#amount').val(amount);
    $("#programStatus").val(programStatus);
}

function sendPaymentDetailss() {
    debugger;
    $('#loader').show();
    $('#loader-wrapper').show();
    var data = {};
    data.SalesLogsId = $('#salesLogsId').val();
    // Check if amount and status are not undefined before proceeding
    if (amount === undefined || status === undefined) {
        $("#loader").fadeOut(3000);
        errorAlert("Payment details are missing, please refresh the page and try again.");
        return;
    }
    $.ajax({
        type: 'Post',
        dataType: 'json',
        url: '/Student/GetPaymentDetials',
        data: {
            Id: courseId,
            Amount: amount,
            Status: status // Ensure this matches the expected parameter name on the server side
        },
        success: function (result) {
            if (!result.isError) {
                $("#loader").fadeOut(3000);
                Swal.fire({
                    title: "Success",
                    text: "You're good to go, let's make the payment now",
                    icon: "success",
                    timer: 30000,
                    confirmButtonColor: "#0253cc",
                }).then(function () {
                    location.href = result.paystackUrl;
                });
            }
        },
        error: function (ex) {
            $("#loader").fadeOut(3000);
            errorAlert("Error occurred, try again");
        }
    });
}

function sendPaymentDetails() {
    debugger
    var defaultBtnValue = $('#submit_btn').html();
    $('#submit_btn').html("Please wait...");
    $('#submit_btn').attr("disabled", true);

    var data = {
        CourseId: $('#courseId').val(),
        Amount: $('#amount').val(),
        ProgramStatus: $('#programStatus').val()
    };

    var paymentDetails = JSON.stringify(data);  // Serialize object to JSON string

    console.log("Serialized data sent to server:", paymentDetails);

    $.ajax({
        type: 'POST',
        url: '/Student/GetPaymentDetails',
        dataType: 'json',
        data: { paymentDetails: paymentDetails },  // Send serialized JSON string

        success: function (result) {
            $('#submit_btn').html(defaultBtnValue);
            $('#submit_btn').attr("disabled", false);

            if (!result.isError) {
                console.log('Success:', result);
                var url = 'Student/Payments?SalesLogsId=' + result;
                newSuccessAlert(result.msg, url);
            } else {
                console.log('Error from server:', result.msg);
                errorAlert(result.msg);
            }
        },

        error: function (ex) {
            $('#submit_btn').html(defaultBtnValue);
            $('#submit_btn').attr("disabled", false);
            console.error("AJAX error:", ex);
            errorAlert("Network failure, please try again");
        }
    });
}

function ManualPaymentAUpload() {
    debugger;
    var fileInput = document.getElementById('bankTransferUploadProof');
    var file = fileInput.files[0];
    var courseId = document.getElementById('courseId').value;

    if (file) {
        var reader = new FileReader();
        reader.readAsDataURL(file);

        reader.onload = function () {
            var data = {
                id: courseId,
                proveOfPayment: reader.result 
            };

            fetch('/Student/ManualPaymenUpload', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(data)
            })
                .then(response => response.json())
                .then(result => {
                    debugger
                    if (!result.isError) {
                        successAlertWithRedirect(result.msg, '/Student/StudentCourses');
                    } else {
                        errorAlert(result.msg);
                    }
                })
                .catch(error => {
                    errorAlert("Error occurred, please try again");
                });
        };
    } else {
        errorAlert("Upload proof to proceed");
    }
}

// APPLICATION REQUEST 
function ManualPaymentAUploads() {

    $('#loader').show();
    $('#loader-wrapper').show();

    var file = document.getElementById("prove").files;

    if (file[0] != null) {
        const reader = new FileReader();
        reader.readAsDataURL(file[0]);

        reader.onload = function () {
            var data = {};
            data.Id = $('#courseId').val();
            data.ProveOfPayment = reader.result;

            if (data.ProveOfPayment != "" && data.Id > 0) {
                let paymentData = JSON.stringify(data);
                $.ajax({
                    type: 'Post',
                    dataType: 'json',
                    url: '/Student/ManualPaymenUpload',
                    data:
                    {
                        paymentData: paymentData,
                    },
                    success: function (result) {

                        if (!result.isError) {
                            $("#loader").fadeOut(3000);

                            var url = '/Student/StudentCourses';
                            successAlertWithRedirect(result.msg, url)
                        }
                        else {
                            $("#loader").fadeOut(3000);
                            errorAlert(result.msg)
                        }
                    },
                    error: function (ex) {
                        $("#loader").fadeOut(3000);
                        errorAlert("Error occured try again");
                    }
                });
            }
            else {
                $("#loader").fadeOut(3000);
                errorAlert("Only PDF file is allowed");
            }
        }
    }
    else {
        $("#loader").fadeOut(3000);
        errorAlert("Upload Prove to proceed");
    }
}
function ApplicantsGetJobById(id) {

    $.ajax({
        type: 'GET',
        url: '/Student/FindJob',
        data: { jobId: id },
        dataType: 'json',
        success: function (data) {

            if (!data.isError) {
                var desc = data.description.split("\n");
                var resp = data.responsibilities.split("\n");
                var requ = data.requirements.split("\n");
                document.getElementById('jobDescriptionView').innerHTML = "<ul><li>" + desc.join("</li><br><li>") + "</li></ul>";
                document.getElementById('jobResponsibilitiesView').innerHTML = "<ul><li>" + resp.join("</li><br><li>") + "</li></ul>";
                document.getElementById('jobRequirementsView').innerHTML = "<ul><li>" + requ.join("</li><br><li>") + "</li></ul>";
                document.getElementById('companyName').innerHTML = "<b>Company Name : <b>" + data.companyName + "</b>";
                document.getElementById('title').innerHTML = "<b>Job Title : " + data.title + "</b>";
                document.getElementById('salary').innerHTML = "<b>Salary : ₦" + data.salary.toLocaleString('en-US') + " a month" + "</b>";
                var typee = data.type;
                if (typee == 1) {
                    document.getElementById('type').innerHTML = "<b>Full-time (On Premise)</b>";
                } else if (typee == 2) {
                    document.getElementById('type').innerHTML = "<b>Part-time (On Premise)</b>";
                }
                else if (typee == 3) {
                    document.getElementById('type').innerHTML = "<b>Full-time (On Home)</b>";
                }
                else {
                    document.getElementById('type').innerHTML = "<b>Part-time (On Home)</b>";
                }
            }
        }
    });
}

// SEARCH MAPPING FOR JOBS
function MapJoyTypeIdForSearch() {
    var searchId = $("#jobSearch").val();
    var address = "/Student/Jobs/" + searchId;
    $("a[href]").attr("href", address)
}

function manualSubmitInterviewTest() {
    //Check if answers has been submitted automatically
    var stopper = $("#manualStopper").val();
    if (stopper == "Off") {

        $("#autoStopper").val("On");

        $('#loader').show();
        $('#loader-wrapper').show();
        var data = [];
        var allCheckedFeatures = document.getElementsByClassName("chkd");

        for (var i = 0; i < allCheckedFeatures.length; i++) {
            var CheckedAnswers = { questionId: '', selectedAnswer: '' };

            if (allCheckedFeatures[i].checked) {
                CheckedAnswers.questionId = allCheckedFeatures[i].name;
                CheckedAnswers.selectedAnswer = allCheckedFeatures[i].value;
                data.push(CheckedAnswers);
            }
        }
        var resultCount = data.length;
        if (resultCount == 20) {
            let dataa = {};
            dataa.InterviewAnsweredQuestions = data;
            let questionAndAnswer = JSON.stringify(dataa);
            $.ajax({
                type: 'Post',
                dataType: 'json',
                url: '/Student/SubmitInterviewAnwser',
                data:
                {
                    collectedData: questionAndAnswer,
                },
                success: function (result) {

                    if (!result.isError) {
                        $("#loader").fadeOut(3000);
                        var url = '/Student/Index';
                        successAlertWithRedirect(result.msg, url)
                    }
                    else {
                        $("#loader").fadeOut(3000);
                        errorAlert(result.msg)
                    }
                },
                error: function (ex) {
                    $("#loader").fadeOut(3000);
                    errorAlert("Error occured try again");
                }
            });
        }
        else {
            $("#loader").fadeOut(3000);
            errorAlert("Answer All the questions");
        }
    }
    else {
        $("#loader").fadeOut(3000);
    }
}

function autoSubmitInterview() {

    //Check if answers has been submitted manually
    var stopper = $("#autoStopper").val();
    if (stopper == "Off") {
        $("#manualStopper").val("On");

        $('#loader').show();
        $('#loader-wrapper').show();
        var data = [];
        var allCheckedFeatures = document.getElementsByClassName("chkd");

        for (var i = 0; i < allCheckedFeatures.length; i++) {
            var CheckedAnswers = { questionId: '', selectedAnswer: '' };

            if (allCheckedFeatures[i].checked) {
                CheckedAnswers.questionId = allCheckedFeatures[i].name;
                CheckedAnswers.selectedAnswer = allCheckedFeatures[i].value;
                data.push(CheckedAnswers);
            }
        }

        let dataa = {};
        dataa.InterviewAnsweredQuestions = data;
        let questionAndAnswer = JSON.stringify(dataa);
        $.ajax({
            type: 'Post',
            dataType: 'json',
            url: '/Student/SubmitInterviewAnwser',
            data:
            {
                collectedData: questionAndAnswer,
            },
            success: function (result) {

                if (!result.isError) {
                    $("#loader").fadeOut(3000);
                    var url = '/Student/Index';
                    successAlertWithRedirect(result.msg, url)
                }
                else {
                    $("#loader").fadeOut(3000);
                    errorAlert(result.msg)
                }
            },
            error: function (ex) {
                $("#loader").fadeOut(3000);
                errorAlert("Error occured try again");
            }
        });
    }
}

//COPY TEXT
// <!......................... ADMIN SCRIPTS ............................>
function CourseVideoPost(action) {

    $('#loader').show();
    $('#loader-wrapper').show();
    var data = {};
    if (action == "CREATE") {
        data.ActionType = action;
        data.CourseId = $("#name").val();
        var nameVDT = document.querySelector("#nameValidation");
        data.Outline = $("#outline").val();
        var outlineVDT = document.querySelector("#outlineValidation");
        data.VideoLink = $("#videoLink").val();
        var videoLinkVDT = document.querySelector("#videoLinkValidation");
    }
    if (action == "EDIT") {
        data.ActionType = action;
        data.Id = $("#edit_Id").val();
        data.CourseId = $("#edit_Name").val();
        var nameVDT = document.querySelector("#edit_NameValidation");
        data.Outline = $("#edit_Outline").val();
        var outlineVDT = document.querySelector("#edit_OutlineValidation");
        data.VideoLink = $("#edit_videoLink").val();
        var videoLinkVDT = document.querySelector("#edit_videoLinkValidation");
    }
    if (action == "DELETE") {
        data.ActionType = action;
        data.Id = $("#Delete_Id").val();
    }

    if (data.ActionType == "CREATE") {
        if (data.CourseId != "" && data.Outline != "" && data.VideoLink != "") {

            SendVideoDataToController(data);
        }
        else {
            $("#loader").fadeOut(3000);
            if (data.CourseId == "") {
                nameVDT.style.display = "block";
            } else {
                nameVDT.style.display = "none";
            }
            if (data.Outline == "") {
                outlineVDT.style.display = "block";
            } else {
                outlineVDT.style.display = "none";
            }
            if (data.VideoLink == "") {
                videoLinkVDT.style.display = "block";
            } else {
                videoLinkVDT.style.display = "none";
            }
        }
    }
    else if (data.ActionType == "EDIT") {
        if (data.Id != "" && data.CourseId != 0 && data.Outline != "" && data.VideoLink) {

            SendVideoDataToController(data);
        }
        else {
            $("#loader").fadeOut(3000);
            if (data.CourseId == "") {
                nameVDT.style.display = "block";
            } else {
                nameVDT.style.display = "none";
            }
            if (data.Outline == "") {
                outlineVDT.style.display = "block";
            } else {
                outlineVDT.style.display = "none";
            }
            if (data.VideoLink == "") {
                videoLinkVDT.style.display = "block";
            } else {
                videoLinkVDT.style.display = "none";
            }
        }
    }
    else if (data.ActionType == "DELETE") {
        if (data.Id != "") {

            SendVideoDataToController(data);
        }
    }
    else {

        $("#loader").fadeOut(3000);
        errorAlert("Failed");
    }
}

function SendVideoDataToController(data) {
    let collectedData = JSON.stringify(data);

    $.ajax({
        type: 'Post',
        dataType: 'json',
        url: '/Admin/TrainingVideoUpload',
        data:
        {
            collectedVideoData: collectedData,
        },
        success: function (result) {

            if (!result.isError) {
                $("#loader").fadeOut(3000);
                var url = '/Admin/TrainingVideos';
                successAlertWithRedirect(result.msg, url)
            }
            else {
                $("#loader").fadeOut(3000);
                errorAlert(result.msg)
            }
        },
        error: function (ex) {
            $("#loader").fadeOut(3000);
            errorAlert("Error occured try again");
        }
    });
}

function FullScreenVideo(videoLink) {

    $('#full_Screen').attr('src', videoLink);
}

function ViewOutline2(id) {

    $.ajax({
        type: 'GET',
        url: '/Student/GetCourseOutLineById',
        data: { id: id },
        dataType: 'json',
        success: function (data) {

            if (!data.isError) {
                var outlin = data.split("\n");
                document.getElementById('view_Outline').innerHTML = "<ul><li>" + outlin.join("</li><br><li>") + "</li></ul>";
            }
        }
    });

}

function GetVideoById(data) {
    let Id = JSON.stringify(data);

    $.ajax({
        type: 'GET',
        url: '/Admin/GetVideoById',
        data: { videoId: Id },
        dataType: 'json',
        success: function (data) {

            if (!data.isError) {
                $("#Delete_Id").val(data.id);
                $("#edit_Id").val(data.id);
                $("#edit_Name").val(data.courseId);
                $("#edit_Outline").val(data.outline);
                $("#edit_videoLink").val(data.videoLink);
            }
        }
    });
}

function TrainingQuestionPost(action, Id) {

    $('#loader').show();
    $('#loader-wrapper').show();
    var data = {};
    if (action == "CREATE") {
        data.ActionType = action;
        data.CourseId = $("#title").val();
        data.Question = $("#question").val();
        data.Answer = $("#answer").val();
        var titleVDT = document.querySelector("#titleValidation");
        var questionVDT = document.querySelector("#questionValidation");
        var awserVDT = document.querySelector("#answerValidation");
    }
    if (action == "EDIT") {
        data.ActionType = action;
        data.Id = $("#edit_Id").val();
        data.CourseId = $("#edit_Title").val();
        data.Question = $("#edit_Question").val();
        data.Answer = $("#edit_Aanswer").val();
        var titleVDT = document.querySelector("#edit_titleValidation");
        var questionVDT = document.querySelector("#edit_questionValidation");
        var awserVDT = document.querySelector("#edit_answerValidation");
    }
    if (action == "DELETE") {
        data.ActionType = action;
        data.Id = $("#Delete_Id").val();
    }
    if (action == "ACTIVATE" || action == "DEACTIVATE") {
        data.ActionType = action;
        data.Id = Id;
    }

    if (data.ActionType == "CREATE") {
        if (data.CourseId != "" && data.Question != "" && data.Answer != "") {

            SendQuestionDataToController(data);
        }
        else {
            $("#loader").fadeOut(3000);
            if (data.CourseId == "") {
                titleVDT.style.display = "block";
            } else {
                titleVDT.style.display = "none";
            }
            if (data.Outline == "") {
                questionVDT.style.display = "block";
            } else {
                questionVDT.style.display = "none";
            }
            if (data.Answer == "") {
                awserVDT.style.display = "block";
            } else {
                awserVDT.style.display = "none";
            }
        }
    }
    else if (data.ActionType == "EDIT") {
        if (data.Id != "" && data.CourseId != "" && data.Question != "" && data.Answer != "") {

            SendQuestionDataToController(data);
        }
        else {
            $("#loader").fadeOut(3000);
            if (data.CourseId == "") {
                titleVDT.style.display = "block";
            } else {
                titleVDT.style.display = "none";
            }
            if (data.Outline == "") {
                questionVDT.style.display = "block";
            } else {
                questionVDT.style.display = "none";
            }
            if (data.Answer == "") {
                awserVDT.style.display = "block";
            } else {
                awserVDT.style.display = "none";
            }
        }
    }
    else if (data.ActionType == "DELETE" || action == "ACTIVATE" || action == "DEACTIVATE") {
        if (data.Id != "") {

            SendQuestionDataToController(data);
        }
    }
    else {
        $("#loader").fadeOut(3000);
        errorAlert("Failed");
    }
}

function SendQuestionDataToController(data) {
    let collectedData = JSON.stringify(data);

    $.ajax({
        type: 'Post',
        dataType: 'json',
        url: '/Admin/AdminPostActionsForTestQuestions',
        data:
        {
            collectedTestQuestionsData: collectedData,
        },
        success: function (result) {

            if (!result.isError) {
                $("#loader").fadeOut(3000);
                var url = '/Admin/TestQuestions';
                successAlertWithRedirect(result.msg, url)
            }
            else {
                $("#loader").fadeOut(3000);
                errorAlert(result.msg)
            }
        },
        error: function (ex) {
            $("#loader").fadeOut(3000);
            errorAlert("Error occured try again");
        }
    });
}

function GetQuestionById(Id) {

    $('#loader').show();
    $('#loader-wrapper').show();
    $.ajax({
        type: 'GET',
        url: '/Admin/GetQuestionById',
        data: { Id: Id },
        dataType: 'json',
        success: function (data) {

            if (!data.isError) {
                $("#loader").fadeOut(3000);
                $("#Delete_Id").val(data.id);
                $("#edit_Id").val(data.id);
                $("#edit_Title").val(data.courseId);
                $("#edit_Question").val(data.question);
                $("#edit_Aanswer").val(data.answer);
            }
        }
    });
}

function GetOptionsById(id) {

    $.ajax({
        type: 'GET',
        url: '/Admin/GetOptionsByQuestionId',
        data: { id: id },
        dataType: 'json',
        success: function (data) {

            if (!data.isError) {
                $("#optQuestionId").val(id);
                if (data.isNull) {

                    document.getElementById("optList").innerHTML = data.val;
                }
                else {
                    var dataCount = data.length;
                    $("#optCount").val(dataCount);
                    var list = "<ul><li>" + data.join("</li><li>") + "</li></ul >";
                    document.getElementById("optList").innerHTML = list;

                    $("#option1Edit").val(data[3]);
                    $("#option2Edit").val(data[2]);
                    $("#option3Edit").val(data[1]);
                    $("#option4Edit").val(data[0]);

                }
            }
        }
    });
}

function CreateOption() {

    var data = {};
    data.QuestionId = $("#optQuestionId").val();
    data.OptionOne = $("#option1").val();
    data.OptionTwo = $("#option2").val();
    data.OptionThree = $("#option3").val();
    data.OptionFour = $("#option4").val();
    var counter = $("#optCount").val();
    if (counter >= 4) {
        errorAlert("Fialed!!! Maximum of 4 Option per question")
    }
    else {
        if (data.QuestionId > 0 && data.OptionOne != "" && data.OptionTwo != "" && data.OptionThree != "" && data.OptionFour != "") {
            let optdata = JSON.stringify(data);

            $('#loader').show();
            $('#loader-wrapper').show();
            $.ajax({
                type: 'POST',
                dataType: 'Json',
                url: '/Admin/AddAnwserOptions',
                data:
                {
                    dataCollected: optdata
                },
                success: function (result) {

                    if (!result.isError) {
                        $("#loader").fadeOut(3000);
                        var url = '/Admin/TestQuestions'
                        successAlertWithRedirect(result.msg, url)
                    }
                    else {
                        $("#loader").fadeOut(3000);
                        errorAlert(result.msg)
                    }
                },
                Error: function (ex) {
                    $("#loader").fadeOut(3000);
                    errorAlert(ex);
                }
            });
        }
        else {
            errorAlert("Fill all option field")
        }

    }
}

function EditOption() {

    var data = {};
    data.QuestionId = $("#optQuestionId").val();
    data.OptionOne = $("#option1Edit").val();
    data.OptionTwo = $("#option2Edit").val();
    data.OptionThree = $("#option3Edit").val();
    data.OptionFour = $("#option4Edit").val();
    if (data.QuestionId > 0 && data.OptionOne != "" && data.OptionTwo != "" && data.OptionThree != "" && data.OptionFour != "") {
        let optdata = JSON.stringify(data);

        $('#loader').show();
        $('#loader-wrapper').show();
        $.ajax({
            type: 'POST',
            dataType: 'Json',
            url: '/Admin/AddAnwserOptions',
            data:
            {
                dataCollected: optdata
            },
            success: function (result) {

                if (!result.isError) {
                    $("#loader").fadeOut(3000);
                    var url = '/Admin/TestQuestions'
                    successAlertWithRedirect(result.msg, url)
                }
                else {
                    $("#loader").fadeOut(3000);
                    errorAlert(result.msg)
                }
            },
            Error: function (ex) {
                $("#loader").fadeOut(3000);
                errorAlert(ex);
            }
        });
    }
    else {
        errorAlert("Fill all option field")
    }
}

function InterviewQuestionPost(action, Id) {

    $('#loader').show();
    $('#loader-wrapper').show();
    var data = {};
    if (action == "CREATE") {
        data.ActionType = action;
        data.Question = $("#question").val();
        data.Answer = $("#answer").val();
        var questionVDT = document.querySelector("#questionValidation");
        var awserVDT = document.querySelector("#answerValidation");
    }
    if (action == "EDIT") {
        data.ActionType = action;
        data.Id = $("#edit_Id").val();
        data.Question = $("#edit_Question").val();
        data.Answer = $("#edit_Aanswer").val();
        var questionVDT = document.querySelector("#edit_questionValidation");
        var awserVDT = document.querySelector("#edit_answerValidation");
    }
    if (action == "DELETE") {
        data.ActionType = action;
        data.Id = $("#Delete_Id").val();
    }
    if (action == "ACTIVATE" || action == "DEACTIVATE") {
        data.ActionType = action;
        data.Id = Id;
    }

    if (data.ActionType == "CREATE") {
        if (data.Question != "" && data.Answer != "") {

            SendInterviewQuestionDataToController(data);
        }
        else {
            $("#loader").fadeOut(3000);
            if (data.Question == "") {
                questionVDT.style.display = "block";
            } else {
                questionVDT.style.display = "none";
            }
            if (data.Answer == "") {
                awserVDT.style.display = "block";
            } else {
                awserVDT.style.display = "none";
            }
        }
    }
    else if (data.ActionType == "EDIT") {
        if (data.Id != "" && data.Question != "" && data.Answer != "") {

            SendInterviewQuestionDataToController(data);
        }
        else {
            $("#loader").fadeOut(3000);
            if (data.Question == "") {
                questionVDT.style.display = "block";
            } else {
                questionVDT.style.display = "none";
            }
            if (data.Answer == "") {
                awserVDT.style.display = "block";
            } else {
                awserVDT.style.display = "none";
            }
        }
    }
    else if (data.ActionType == "DELETE" || action == "ACTIVATE" || action == "DEACTIVATE") {
        if (data.Id != "") {

            SendInterviewQuestionDataToController(data);
        }
    }
    else {
        $("#loader").fadeOut(3000);
        errorAlert("Failed");
    }
}

function SendInterviewQuestionDataToController(data) {
    let collectedData = JSON.stringify(data);

    $.ajax({
        type: 'Post',
        dataType: 'json',
        url: '/Admin/PostActionsForInterveiwQuestions',
        data:
        {
            collectedTestQuestionsData: collectedData,
        },
        success: function (result) {

            if (!result.isError) {
                $("#loader").fadeOut(3000);
                var url = '/Admin/InterviewQuestions';
                successAlertWithRedirect(result.msg, url)
            }
            else {
                $("#loader").fadeOut(3000);
                errorAlert(result.msg)
            }
        },
        error: function (ex) {
            $("#loader").fadeOut(3000);
            errorAlert("Error occured try again");
        }
    });
}

function MapInterviewData(id, question, answer) {
    $("#Delete_Id").val(id);
    $("#edit_Id").val(id);
    $("#edit_Question").val(question);
    $("#edit_Aanswer").val(answer);
}

function GetInterviewOptionsById(id) {
    $.ajax({
        type: 'GET',
        url: '/Admin/GetInterviewOptionsByQuestionId',
        data: { id: id },
        dataType: 'json',
        success: function (data) {

            if (!data.isError) {
                $("#optQuestionId").val(id);
                if (data.isNull) {

                    document.getElementById("optList").innerHTML = data.val;
                }
                else {
                    var dataCount = data.length;
                    $("#optCount").val(dataCount);
                    var list = "<ul><li>" + data.join("</li><li>") + "</li></ul >";
                    document.getElementById("optList").innerHTML = list;

                    $("#option1Edit").val(data[3]);
                    $("#option2Edit").val(data[2]);
                    $("#option3Edit").val(data[1]);
                    $("#option4Edit").val(data[0]);
                }
            }
        }
    });
}

function CreateInterviewAnsOption() {

    var data = {};
    data.QuestionId = $("#optQuestionId").val();
    data.OptionOne = $("#option1").val();
    data.OptionTwo = $("#option2").val();
    data.OptionThree = $("#option3").val();
    data.OptionFour = $("#option4").val();
    var counter = $("#optCount").val();
    if (counter >= 4) {
        errorAlert("Fialed!!! Maximum of 4 Option per question")
    }
    else {
        if (data.QuestionId > 0 && data.OptionOne != "" && data.OptionTwo != "" && data.OptionThree != "" && data.OptionFour != "") {
            let optdata = JSON.stringify(data);

            $('#loader').show();
            $('#loader-wrapper').show();
            $.ajax({
                type: 'POST',
                dataType: 'Json',
                url: '/Admin/AddInterViewAnwserOptions',
                data:
                {
                    dataCollected: optdata
                },
                success: function (result) {

                    if (!result.isError) {
                        $("#loader").fadeOut(3000);
                        var url = '/Admin/InterviewQuestions'
                        successAlertWithRedirect(result.msg, url)
                    }
                    else {
                        $("#loader").fadeOut(3000);
                        errorAlert(result.msg)
                    }
                },
                Error: function (ex) {
                    $("#loader").fadeOut(3000);
                    errorAlert(ex);
                }
            });
        }
        else {
            errorAlert("Fill all option field")
        }

    }
}

function EditInterviewAnsOption() {

    var data = {};
    data.QuestionId = $("#optQuestionId").val();
    data.OptionOne = $("#option1Edit").val();
    data.OptionTwo = $("#option2Edit").val();
    data.OptionThree = $("#option3Edit").val();
    data.OptionFour = $("#option4Edit").val();
    if (data.QuestionId > 0 && data.OptionOne != "" && data.OptionTwo != "" && data.OptionThree != "" && data.OptionFour != "") {
        let optdata = JSON.stringify(data);

        $('#loader').show();
        $('#loader-wrapper').show();
        $.ajax({
            type: 'POST',
            dataType: 'Json',
            url: '/Admin/AddInterViewAnwserOptions',
            data:
            {
                dataCollected: optdata
            },
            success: function (result) {

                if (!result.isError) {
                    $("#loader").fadeOut(3000);
                    var url = '/Admin/InterviewQuestions'
                    successAlertWithRedirect(result.msg, url)
                }
                else {
                    $("#loader").fadeOut(3000);
                    errorAlert(result.msg)
                }
            },
            Error: function (ex) {
                $("#loader").fadeOut(3000);
                errorAlert(ex);
            }
        });
    }
    else {
        errorAlert("Fill all option field")
    }
}

function AppoveSelectedProjectTopic(Id) {

    $('#loader').show();
    $('#loader-wrapper').show();
    if (Id != "") {
        let id = Id;
        $.ajax({
            type: 'POST',
            dataType: 'Json',
            url: '/Admin/AppoveProjectTopic',
            data:
            {
                id: id
            },
            success: function (result) {

                if (!result.isError) {
                    $("#loader").fadeOut(3000);
                    var url = '/Admin/ViewProjectTopics'
                    successAlertWithRedirect(result.msg, url)
                }
                else {
                    $("#loader").fadeOut(3000);
                    errorAlert(result.msg)
                }
            },
            Error: function (ex) {
                $("#loader").fadeOut(3000);
                errorAlert(ex);
            }
        });
    }
    else {
        $("#loader").fadeOut(3000);
        errorAlert("Error Occured")
    }
}

function MarkProjectAsCompleted() {

    $('#loader').show();
    $('#loader-wrapper').show();
    var id = $('#accept_Id').val();
    if (id != "") {
        let projectId = id;
        $.ajax({
            type: 'POST',
            dataType: 'Json',
            url: '/Admin/MarkProjectAsCompleted',
            data:
            {
                projectId: projectId
            },
            success: function (result) {

                if (!result.isError) {
                    $("#loader").fadeOut(3000);
                    var url = '/Admin/AprovedTopics'
                    successAlertWithRedirect(result.msg, url)
                }
                else {
                    $("#loader").fadeOut(3000);
                    errorAlert(result.msg)
                }
            },
            Error: function (ex) {
                $("#loader").fadeOut(3000);
                errorAlert(ex);
            }
        });
    }
    else {
        $("#loader").fadeOut(3000);
        errorAlert("Error Occured")
    }
}

function JobCreatePost(action) {

    $('#loader').show();
    $('#loader-wrapper').show();
    var salaryInt = $('#salary').val();
    var jobData = {};
    jobData.ActionType = action;
    jobData.CompanyName = $("#companyName").val();
    jobData.Title = $("#title").val();
    jobData.Salary = parseFloat(salaryInt).toFixed(2);
    jobData.Type = $("#type").val();
    jobData.Description = $("#description").val();
    jobData.Responsibilities = $("#responsibilities").val();
    jobData.Requirements = $("#requirements").val();
    if (jobData.CompanyName != "" && jobData.Title != "" && jobData.Salary != "NaN" && jobData.Type != 0 && jobData.Description != "" && jobData.Responsibilities != "" && jobData.Requirements != "") {

        SendJobDataToController(jobData);
    }
    else {
        $("#loader").fadeOut(3000);
        if (jobData.CompanyName == "") {
            document.querySelector("#companyNameVDT").style.display = "block";
        } else {
            document.querySelector("#companyNameVDT").style.display = "none";
        }
        if (jobData.Title == "") {
            document.querySelector("#titleVDT").style.display = "block";
        } else {
            document.querySelector("#titleVDT").style.display = "none";
        }
        if (jobData.Salary == "NaN") {
            document.querySelector("#salaryVDT").style.display = "block";
        } else {
            document.querySelector("#salaryVDT").style.display = "none";
        }
        if (jobData.Type == 0) {
            document.querySelector("#typeVDT").style.display = "block";
        } else {
            document.querySelector("#typeVDT").style.display = "none";
        }
        if (jobData.Description == "") {
            document.querySelector("#descriptionVDT").style.display = "block";
        } else {
            document.querySelector("#descriptionVDT").style.display = "none";
        }
        if (jobData.Responsibilities == "") {
            document.querySelector("#responsibilitiesVDT").style.display = "block";
        } else {
            document.querySelector("#responsibilitiesVDT").style.display = "none";
        }
        if (jobData.Requirements == "") {
            document.querySelector("#requirementsVDT").style.display = "block";
        } else {
            document.querySelector("#requirementsVDT").style.display = "none";
        }
    }
}

function JobEditPost(action) {

    $('#loader').show();
    $('#loader-wrapper').show();
    var salaryInt = $('#edit_salary').val();
    var jobData = {};
    jobData.ActionType = action;
    jobData.Id = $("#edit_Id").val();
    jobData.CompanyName = $("#edit_companyName").val();
    jobData.Title = $("#edit_title").val();
    jobData.Salary = parseFloat(salaryInt).toFixed(4);
    jobData.Type = $("#edit_type").val();
    jobData.Description = $("#edit_description").val();
    jobData.Responsibilities = $("#edit_responsibilities").val();
    jobData.Requirements = $("#edit_requirements").val();
    if (jobData.CompanyName != "" && jobData.Title != "" && jobData.Salary != "" &&
        jobData.Type != "" && jobData.Description != "" && jobData.Responsibilities != "" && jobData.Requirements != "") {

        SendJobDataToController(jobData);
    }
    else {
        $("#loader").fadeOut(3000);
        if (jobData.Id <= 0) {
            errorAlert("Error occured try again");
        }
        if (jobData.CompanyName == "") {
            document.querySelector("#edit_companyNameVDT").style.display = "block";
        } else {
            document.querySelector("#edit_companyNameVDT").style.display = "none";
        }
        if (jobData.Title == "") {
            document.querySelector("#edit_titleVDT").style.display = "block";
        } else {
            document.querySelector("#edit_titleVDT").style.display = "none";
        }
        if (jobData.Salary == "") {
            document.querySelector("#edit_salaryVDT").style.display = "block";
        } else {
            document.querySelector("#edit_salaryVDT").style.display = "none";
        }
        if (jobData.Type == "") {
            document.querySelector("#edit_typeVDT").style.display = "block";
        } else {
            document.querySelector("#edit_typeVDT").style.display = "none";
        }
        if (jobData.Description == "") {
            document.querySelector("#edit_descriptionVDT").style.display = "block";
        } else {
            document.querySelector("#edit_descriptionVDT").style.display = "none";
        }
        if (jobData.Responsibilities == "") {
            document.querySelector("#edit_responsibilitiesVDT").style.display = "block";
        } else {
            document.querySelector("#edit_responsibilitiesVDT").style.display = "none";
        }
        if (jobData.Requirements == "") {
            document.querySelector("#edit_requirementsVDT").style.display = "block";
        } else {
            document.querySelector("#edit_requirementsVDT").style.display = "none";
        }
    }
}

function GetJobById(id) {

    $.ajax({
        type: 'GET',
        url: '/Admin/FindJob',
        data: { jobId: id },
        dataType: 'json',
        success: function (data) {

            if (!data.isError) {

                document.getElementById('jobDescriptionView').innerHTML = data.description;
                document.getElementById('jobResponsibilitiesView').innerHTML = data.responsibilities;
                document.getElementById('jobRequirementsView').innerHTML = data.requirements;
                $("#edit_Id").val(data.id);
                $("#edit_companyName").val(data.companyName);
                $("#edit_title").val(data.title);
                $("#edit_salary").val(data.salary);
                $("#edit_type").val(data.type);
                $('#edit_description').summernote('code', data.description);
                $('#edit_responsibilities').summernote('code', data.responsibilities);
                $('#edit_requirements').summernote('code', data.requirements);
            }
        }
    });
}

function ActivateAndDeactiveJob(action, id) {

    $('#loader').show();
    $('#loader-wrapper').show();
    var jobData = {};
    jobData.ActionType = action;
    jobData.Id = id;
    if (jobData.CompanyName != "") {

        SendJobDataToController(jobData);
    }
    else {
        $("#loader").fadeOut(3000);
        errorAlert("Error occured try again");
    }
}

function SendJobDataToController(jobData) {
    let collectedData = JSON.stringify(jobData);

    $.ajax({
        type: 'Post',
        dataType: 'json',
        url: '/Admin/JopMGTPostAction',
        data:
        {
            collectedJobData: collectedData,
        },
        success: function (result) {

            if (!result.isError) {
                $("#loader").fadeOut(3000);
                var url = '/Admin/Jobs';
                successAlertWithRedirect(result.msg, url)
            }
            else {
                $("#loader").fadeOut(3000);
                errorAlert(result.msg)
            }
        },
        error: function (ex) {
            $("#loader").fadeOut(3000);
            errorAlert("Error occured try again");
        }
    });
}

function CreateExamDuration() {
    var data = {};
    data.Type = $("#examType").val();
    data.Duration = $("#duration").val();
    if (data.Type == "" || data.Duration == "") {
        if (data.Type == "") {
            document.querySelector("#examTypeVDT").style.display = "block";
        } else {
            document.querySelector("#examTypeVDT").style.display = "none";
        }
        if (data.Duration == "") {
            document.querySelector("#durationVDT").style.display = "block";
        } else {
            document.querySelector("#durationVDT").style.display = "none";
        }
    }
    else {
        document.querySelector("#examTypeVDT").style.display = "none";
        document.querySelector("#durationVDT").style.display = "none";

        let durationData = JSON.stringify(data);
        $.ajax({
            type: 'POST',
            dataType: 'Json',
            url: '/Admin/CreateExamDuration',
            data:
            {
                dataCollected: durationData
            },
            success: function (result) {

                if (!result.isError) {
                    var url = '/Admin/ExamDuration'
                    successAlertWithRedirect(result.msg, url)
                }
                else {
                    errorAlert(result.msg)
                }
            },
            Error: function (ex) {
                errorAlert(ex);
            }
        });
    }
}

function EditExamDuration() {
    var data = {};
    data.Id = $("#edit_Id").val();
    data.Duration = $("#duration_Edit").val();
    if (data.Duration == "" || data.Id == "") {
        if (data.Duration == "") {
            document.querySelector("#duration_EditVTD").style.display = "block";
        } else {
            document.querySelector("#duration_EditVTD").style.display = "none";
        }
    }
    else {
        document.querySelector("#duration_EditVTD").style.display = "none";

        let durationData = JSON.stringify(data);
        $.ajax({
            type: 'POST',
            dataType: 'Json',
            url: '/Admin/EditExamDuration',
            data:
            {
                dataCollected: durationData
            },
            success: function (result) {

                if (!result.isError) {
                    var url = '/Admin/ExamDuration'
                    successAlertWithRedirect(result.msg, url)
                }
                else {
                    errorAlert(result.msg)
                }
            },
            Error: function (ex) {
                errorAlert(ex);
            }
        });
    }
}

function DeleteExamDuration() {

    var id = $("#delete_Id").val();
    if (id == "") {
        errorAlert("Error occured, please try again")
    }
    else {
        let durationId = id;
        $.ajax({
            type: 'POST',
            dataType: 'Json',
            url: '/Admin/DeleteExamDuration',
            data:
            {
                id: durationId
            },
            success: function (result) {

                if (!result.isError) {
                    var url = '/Admin/ExamDuration'
                    successAlertWithRedirect(result.msg, url)
                }
                else {
                    errorAlert(result.msg)
                }
            },
            Error: function (ex) {
                errorAlert(ex);
            }
        });
    }
}

function DeleteDocument(Id) {

    var id = Id;
    if (id == "") {
        errorAlert("Error occured, please try again")
    }
    else {
        let docId = id;
        $.ajax({
            type: 'POST',
            dataType: 'Json',
            url: '/Admin/DeleteDocuments',
            data:
            {
                docId: docId
            },
            success: function (result) {

                if (!result.isError) {
                    var url = '/Admin/StudentsDocumentations'
                    successAlertWithRedirect(result.msg, url)
                }
                else {
                    errorAlert(result.msg)
                }
            },
            Error: function (ex) {
                errorAlert(ex);
            }
        });
    }
}
function ApproveApplicantDocuments(Id) {

    if (Id == "") {
        errorAlert("Error occured, please try again")
    }
    else {
        let id = Id;
        $.ajax({
            type: 'POST',
            dataType: 'Json',
            url: '/Admin/ApproveApplicantDocuments',
            data:
            {
                docId: id
            },
            success: function (result) {

                if (!result.isError) {
                    var url = '/Admin/StudentsDocumentations'
                    successAlertWithRedirect(result.msg, url)
                }
                else {
                    errorAlert(result.msg)
                }
            },
            Error: function (ex) {
                errorAlert(ex);
            }
        });
    }
}
// Accept Graduation form submission POST ACTION
function AcceptGraduationPost() {

    var data = {};
    data.Id = $("#approveID").val();
    let graduationFormData = data;
    $.ajax({
        type: 'POST',
        dataType: 'Json',
        url: '/Admin/ManageGraduationForms',
        data:
        {
            employementDataViewModel: graduationFormData
        },
        success: function (result) {

            if (!result.isError) {
                var url = '/Admin/employementDataViewModel'
                successAlertWithRedirect(result.msg, url)
            }
            else {
                errorAlert(result.msg)
            }
        },
        Error: function (ex) {
            errorAlert(ex);
        }
    });
}

//SEND JobId to controller for applications 
function JobAplicationSubmittion(action, id) {

    $('#loader').show();
    $('#loader-wrapper').show();
    if (action == "APPLIED") {
        $("#loader").fadeOut(3000);
        errorAlert("Oops! you have already applied for this job");
    }
    else {
        let Id = id;
        $.ajax({
            type: 'Post',
            dataType: 'json',
            url: '/Student/JobApplicationPost',
            data:
            {
                Id: Id,
            },
            success: function (result) {

                if (!result.isError) {
                    $("#loader").fadeOut(3000);
                    var url = '/Student/Jobs';

                    successAlertWithRedirect(result.msg, url)
                }
                else {
                    $("#loader").fadeOut(3000);
                    errorAlert(result.msg)
                }
            },
            error: function (ex) {
                $("#loader").fadeOut(3000);
                errorAlert("Error occured try again");
            }
        });
    }
}
// <!......................... ADMIN SCRIPTS ............................>

// <!......................... SUPER ADMIN SCRIPTS ............................>
// APPLICATION REQUEST 
function ActivateAndDeactivateAdminPost(action, id) {

    $('#loader').show();
    $('#loader-wrapper').show();
    var data = {};
    data.Id = id;
    data.ActionType = action;
    let collectedData = JSON.stringify(data);
    $.ajax({
        type: 'Post',
        dataType: 'json',
        url: '/SuperAdmin/AdminTurnOnAndOff',
        data:
        {
            collectedAdminData: collectedData,
        },
        success: function (result) {

            if (!result.isError) {
                $("#loader").fadeOut(3000);
                var url = '/SuperAdmin/ManageAdmin';
                successAlertWithRedirect(result.msg, url)
            }
            else {
                $("#loader").fadeOut(3000);
                errorAlert(result.msg)
            }
        },
        error: function (ex) {
            $("#loader").fadeOut(3000);
            errorAlert("Error occured try again");
        }
    });
}

// <!......................... SUPER ADMIN SCRIPTS ............................>
// <!======EDIT APPLICANT DOCUMENTS==========>
function editApplicantDocumentation(Id) {

    var file = {};
    file.FirstGuarantor = document.getElementById("editFirstGuarantor").files;
    file.SecondGuarantor = document.getElementById("editSecondGuarantor").files;
    file.NepaBill = document.getElementById("editNepaBill").files;
    file.SignedContract = document.getElementById("editContractforms").files;
    var BVN = $('#editBVN').val();
    if (file.FirstGuarantor[0] != null && file.SecondGuarantor[0] != null && file.NepaBill[0] != null && file.SignedContract[0] != null && BVN != null) {
        $('#loader').show();
        $('#loader-wrapper').show();
        if (file.FirstGuarantor[0] != null) {
            const reader = new FileReader();
            reader.readAsDataURL(file.FirstGuarantor[0]);
            var base64FirstGuarantor;
            reader.onload = function () {
                base64FirstGuarantor = reader.result;

                if (file.SecondGuarantor[0] != null) {
                    const reader = new FileReader();
                    reader.readAsDataURL(file.SecondGuarantor[0]);
                    var base64SecondGuarantor;
                    reader.onload = function () {
                        base64SecondGuarantor = reader.result;

                        if (file.NepaBill[0] != null) {
                            const reader = new FileReader();
                            reader.readAsDataURL(file.NepaBill[0]);
                            var base64NepaBill;
                            reader.onload = function () {
                                base64NepaBill = reader.result;

                                if (file.SignedContract[0] != null) {
                                    const reader = new FileReader();
                                    reader.readAsDataURL(file.SignedContract[0]);
                                    var base64Contractforms;
                                    reader.onload = function () {
                                        base64Contractforms = reader.result;
                                        var allDocument = {};
                                        allDocument.Id = Id;
                                        allDocument.BVN = BVN;
                                        allDocument.FirstGuarantor = base64FirstGuarantor;
                                        allDocument.SecondGuarantor = base64SecondGuarantor;
                                        allDocument.NepaBill = base64NepaBill;
                                        allDocument.SignedContract = base64Contractforms;

                                        if (BVN != "" || BVN != 0) {
                                            let rawData = JSON.stringify(allDocument);
                                            $.ajax({
                                                type: 'Post',
                                                dataType: 'Json',
                                                url: '/Student/EditApplicantDocuments',
                                                data:
                                                {
                                                    applicantDocuments: rawData,
                                                },
                                                success: function (result) {

                                                    if (!result.isError) {
                                                        $("#loader").fadeOut(3000);
                                                        successAlert(result.msg)
                                                        window.location.reload;
                                                    }
                                                    else {
                                                        $("#loader").fadeOut(3000);
                                                        errorAlert(result.msg)
                                                    }
                                                },
                                                error: function (ex) {
                                                    $("#loader").fadeOut(3000);
                                                    errorAlert("Error occured try again");
                                                }
                                            })
                                        }
                                        else {
                                            $("#loader").fadeOut(3000);
                                            errorAlert("Incorrect Details");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

            }
        }

    }
    else {

        if (BVN == "") {
            document.querySelector("#editBVNVDT").style.display = "block";
        } else {
            document.querySelector("#editBVNVDT").style.display = "none";
        }
        if (file.FirstGuarantor[0] == null) {
            document.querySelector("#editFirstGuarantorVDT").style.display = "block";
        } else {
            document.querySelector("#editFirstGuarantorVDT").style.display = "none";
        }
        if (file.SecondGuarantor[0] == null) {
            document.querySelector("#editSecondGuarantorVDT").style.display = "block";
        } else {
            document.querySelector("#editSecondGuarantorVDT").style.display = "none";
        }
        if (file.NepaBill[0] == null) {
            document.querySelector("#editNepaBillVDT").style.display = "block";
        } else {
            document.querySelector("#editNepaBillVDT").style.display = "none";
        }
        if (file.SignedContract[0] == null) {
            document.querySelector("#editContractformsVDT").style.display = "block";
        } else {
            document.querySelector("#editContractformsVDT").style.display = "none";
        }
    }
}
// <!======/EDIT APPLICANT DOCUMENTS==========>

// APPLICATION REQUEST 
function registerStudent() {
    debugger
    var defaultBtnValue = $('#submit_btn').html();
    $('#submit_btn').html("Please wait...");
    $('#submit_btn').attr("disabled", true);
    var urlParams = new URLSearchParams(window.location.search);
    var companyId = urlParams.get('id');
    var data = {};
    data.FirstName = $('#studentFirstName').val();
    data.LastName = $('#studentLastName').val();
    data.PhoneNumber = $('#studentPhoneNumber').val();
    data.Email = $('#studentEmail').val();
    data.PassWord = $('#studentPassword').val();
    data.ConfirmPassword = $('#studentConfirmPassword').val();
    data.ProgrammingLanguagesExps = $('#studentProgrammingLanguagesExps').val();
    data.HasAnyProgrammingExp = $('#studentHasAnyProgrammingExp').val();
    data.ApplicantResideInEnugu = $('#studentApplicantResideInEnugu').val();
    data.HasLaptop = $('#studentHasLaptop').val();
    data.ReasonForProgramming = $('#studentReasonForProgramming').val();
    data.Address = $('#studentAddress').val();
    data.CompanyId = $('#companyId').val();
    data.IsAdmin = false;
    data.CheckBox = $('#termsCondition').is(":checked");
    let userDetails = JSON.stringify(data);
    $.ajax({
        type: 'Post',
        url: '/Accounts/StudentRegisteration',
        dataType: 'json',
        data:
        {
            userDetails: userDetails,
            companyId: companyId
        },
        success: function (result) {
            if (!result.isError) {
                var url = '/Accounts/Login';
                successAlertWithRedirect(result.msg, url);
                $('#submit_btn').html(defaultBtnValue);
            }
            else {
                $('#submit_btn').html(defaultBtnValue);
                $('#submit_btn').attr("disabled", false);
                errorAlert(result.msg);
            }
        },
        error: function (ex) {
            $('#submit_btn').html(defaultBtnValue);
            $('#submit_btn').attr("disabled", false);
            errorAlert("An error has occurred, try again. Please contact support if the error persists");
        }
    });
}


///For LogIn
function login() {
    $('#loader').show();
    $('#loader-wrapper').show();
    var defaultBtnValue = $('#submit_btn').html();
    $('#submit_btn').html("Please wait...");
    $('#submit_btn').attr("disabled", true);
    var email = $('#email').val();
    var password = $('#password').val();
    $.ajax({
        type: 'Post',
        url: '/Accounts/Login',
        dataType: 'json',
        data:
        {
            email: email,
            password: password
        },
        success: function (result) {
            if (result.isNotVerified) {
                $("#loader").fadeOut(3000);
                errorAlert(result.msg)
            }
            else if (!result.isError) {
                $("#loader").fadeOut(3000);
                successAlertWithRedirect(result.msg, result.dashboard)
            }
            else {
                $("#loader").fadeOut(3000);
                errorAlert(result.msg)
            }
        },
        error: function (ex) {
            $('#submit_btn').html(defaultBtnValue);
            $('#submit_btn').attr("disabled", false);
            errorAlert("An error has occurred, try again. Please contact support if the error persists");
        }
    });
}

function payBeforeComingn() {
    debugger
    var defaultBtnValue = $('#submit_btn_paid_before').html();
    $('#submit_btn_paid_before').html("Please wait...");
    $('#submit_btn_paid_before').attr("disabled", true);
    var paymentPhoto = document.getElementById("paidBeforeUploadProof").files;
    var payData = {};
    payData.CourseId = $("#courseId").val();
    payData.Name = $("#name").val();
    const reader = new FileReader();
    var base64;
    var data = JSON.stringify(payData);
    if (paymentPhoto.length > 0) {
        reader.readAsDataURL(paymentPhoto[0]);
        reader.onload = function ()
        {
            base64 = reader.result;
            if (payData.CourseId != 0 || payData.CourseId != "") {

                $.ajax({
                    type: 'Post',
                    url: '/Accounts/SaveProveOfPayment',
                    dataType: 'json',
                    data:
                    {
                        collectedData: data,
                        base64: base64
                    },
                    success: function (result) {
                        if (!result.isError) {
                            var url = '/Student/Index';
                            successAlertWithRedirect(result.msg, url);
                            $('#submit_btn_paid_before').html(defaultBtnValue);
                        }
                        else {
                            $('#submit_btn_paid_before').html(defaultBtnValue);
                            $('#submit_btn_paid_before').attr("disabled", false);
                            errorAlert(result.msg);
                        }
                    },
                    error: function (ex) {
                        $('#submit_btn_paid_before').html(defaultBtnValue);
                        $('#submit_btn_paid_before').attr("disabled", false);
                        errorAlert("An error has occurred, try again. Please contact support if the error persists");
                    }
                });

            }
        }

    }

}

function payBeforeComing() {
    debugger
    const submitButton = document.getElementById('submit_btn_paid_before');
    const defaultBtnValue = submitButton.innerHTML;

    submitButton.innerHTML = "Please wait...";
    submitButton.setAttribute("disabled", true);

    const paymentPhoto = document.getElementById("paidBeforeUploadProof").files;
    const courseId = document.getElementById('courseId').value;
    const name = document.getElementById('paidBeforeName').value;

    if (paymentPhoto.length > 0) {
        const reader = new FileReader();

        reader.onload = function () {
            const base64 = reader.result;
            const payData = {
                CourseId: courseId,
                Name: name
            };

            if (payData.CourseId && payData.Name) {
                // Prepare form data
                const formData = new FormData();
                formData.append("collectedData", JSON.stringify(payData));
                formData.append("base64", base64);

                fetch('/Accounts/SaveProveOfPayment', {
                    method: 'POST',
                    body: formData
                })
                    .then(response => response.json())
                    .then(result => {
                        submitButton.innerHTML = defaultBtnValue;
                        submitButton.removeAttribute("disabled");

                        if (!result.isError) {
                            const url = '/Student/Index';
                            successAlertWithRedirect(result.msg, url);
                        } else {
                            errorAlert(result.msg);
                        }
                    })
                    .catch(error => {
                        submitButton.innerHTML = defaultBtnValue;
                        submitButton.removeAttribute("disabled");
                        errorAlert("An error has occurred, try again. Please contact support if the error persists");
                    });
            } else {
                submitButton.innerHTML = defaultBtnValue;
                submitButton.removeAttribute("disabled");
                errorAlert("Please provide all required details.");
            }
        };

        reader.readAsDataURL(paymentPhoto[0]);
    } else {
        submitButton.innerHTML = defaultBtnValue;
        submitButton.removeAttribute("disabled");
        errorAlert("Please upload a proof of payment.");
    }
}

    
function studemtLink(companyId) {
    $.ajax({
        type: 'GET',
        url: '/Admin/GetStudentLink',
        data: {
            companyId: companyId,
        },
        success: function (data) {
            if (data.isError) {
                errorAlert(data.msg);
            } else {
                if (typeof data !== 'string') {
                    data = JSON.stringify(data);
                }
                copyLinkToClipboard(data);
            }
        },
        error: function (xhr, status, error) {
            errorAlert("Error fetching student link");
        }
    });
}

function copyLinkToClipboard(link) {
    if (navigator.clipboard && window.isSecureContext) {
        // Use modern Clipboard API
        navigator.clipboard.writeText(link).then(function () {
            successAlert("Student link copied successfully");
        }, function (err) {
            errorAlert("Failed to copy link to clipboard: " + err);
        });
    } else {
        // Fallback for older browsers
        var $temp = $("<input>");
        $("body").append($temp);
        $temp.val(link).select();
        document.execCommand("copy");
        $temp.remove();
        successAlert("Student link copied successfully");
    }
}




// Function to handle "Paid Before" payment method
function payBeforeComingnn() {
    var senderName = document.getElementById('paidBeforeName').value;
    var courseId = document.getElementById('paidBeforeCourseId').value;
    var uploadProof = document.getElementById('paidBeforeUploadProof').files[0];

    if (!senderName || !uploadProof) {
        alert("Please fill in all fields and upload proof of payment.");
        return;
    }

    var formData = new FormData();
    formData.append("SenderName", senderName);
    formData.append("CourseId", courseId);
    formData.append("UploadProof", uploadProof);

    fetch('/Accounts/PayBeforeComing', {
        method: 'POST',
        body: formData
    })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                alert("Payment proof submitted successfully.");
            } else {
                alert("There was an error submitting your payment proof: " + data.message);
            }
        })
        .catch(error => console.error('Error:', error));
}

// Function to handle "Bank Transfer" payment method
function handleBankTransfer() {
    var senderName = document.getElementById('bankTransferName').value;
    var courseId = document.getElementById('bankTransferCourseId').value;
    var uploadProof = document.getElementById('bankTransferUploadProof').files[0];

    if (!senderName || !uploadProof) {
        alert("Please fill in all fields and upload proof of payment.");
        return;
    }

    var formData = new FormData();
    formData.append("SenderName", senderName);
    formData.append("CourseId", courseId);
    formData.append("UploadProof", uploadProof);

    fetch('/Accounts/BankTransfer', {
        method: 'POST',
        body: formData
    })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                alert("Bank transfer proof submitted successfully.");
            } else {
                alert("There was an error submitting your bank transfer proof: " + data.message);
            }
        })
        .catch(error => console.error('Error:', error));
}


function copyToClipboard(elementId) {
    debugger
    // Get the input field
    const copyText = document.getElementById(elementId);

    // Set the input field to be readonly and then focus on it
    copyText.readOnly = false;
    copyText.focus();
    copyText.select();
    copyText.setSelectionRange(0, 99999); // For mobile devices

    // Try using the modern clipboard API
    navigator.clipboard.writeText(copyText.value)
        .then(() => {
            alert("Account number copied to clipboard!");
        })
        .catch(err => {
            console.warn('Clipboard API failed. Falling back to execCommand. Error:', err);
            // Fallback for browsers not supporting clipboard API
            document.execCommand('copy');
            alert("Account number copied to clipboard!");
        })
        .finally(() => {
            // Reset the input field to readonly after copy attempt
            copyText.readOnly = true;
        });
}

