$(document).ready(function () {
    $("#country").select2();
    $('#DateOfBirth').datetimepicker({
        dateFormat: "dd/mm/yy",

    }).on('dp.change', function (e) {
        debugger;
        if (parseInt(new Date(e.date).getFullYear()) > parseInt(1850)) {
            var today = new Date();
            var birthDate = new Date(e.date);
            var age = today.getFullYear() - birthDate.getFullYear();
            var m = today.getMonth() - birthDate.getMonth();
            if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
                age--;
            }
            $('#Patientage').val(age);
        }
        else {
            var month = new Date().getMonth().toLocaleString().length > 1 ? (new Date().getMonth() + 1) : ("0" + (new Date().getMonth() + 1))
            var curdate = new Date().getDate() + "/" + month + "/" + new Date().getFullYear();
            $('#DateOfBirth').data('DateTimePicker').date(curdate);
        }
    });
    $('#DateOfBirth').data("DateTimePicker").maxDate(new Date());
});
function GetDropdownData(Type) {
    $('.loadingimg').css('display', 'block');
    var url = "";
    if (Type == "state")
        url = "/HMSPatient/GetDatabyCode?code=" + $("#country").val() + "&Type=" + Type + "";
    else if (Type == "district")
        url = "/HMSPatient/GetDatabyCode?code=" + $("#state").val() + "&Type=" + Type + "";
    else if (Type == "city")
        url = "/HMSPatient/GetDatabyCode?code=" + $("#district").val() + "&Type=" + Type + "";
    $.ajax({
        url: url,
        datatype: 'json',
        ContentType: 'application/json;utf-8'
    }).done(function (datahtml) {
        $('.loadingimg').css('display', 'none');
        if (Type == "state") {
            $(".statebnd").empty();
            $(".statebnd").html($(datahtml).find(".statebnd").html());
            $("#state").select2();
        }
        else if (Type == "district") {
            $(".districtbnd").empty();
            $(".districtbnd").html($(datahtml).find(".districtbnd").html());
            $("#district").select2();
        }
        else if (Type == "city") {
            $(".citybnd").empty();
            $(".citybnd").html($(datahtml).find(".citybnd").html());
            $("#city").select2();
        }
        $('#loadingmessage').hide();
    });
}
function ajaxbegin() {
    debugger;
    var file = $("#imageUploadForm").get(0).files;
    var data = new FormData;
    data.append("ImageFile", file[0]);
}
function SuccessMethod(data) {
    debugger;
    var htmlstr = "";
    if (data == "Record Update Sucessfully!!") {
        $('#cardbox').removeAttr('hidden');
        htmlstr = "<div class='alert alert-success alert-dismissible fade show' role='alert'> <strong> Success!</strong> Your <a href='#' class='alert-link'></a> <span id='statusmsg'>" + data + "</span> <button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button></div > ";
    }
    else if (data == "Record Does Not Update Sucessfully!!") {
        $('#cardbox').removeAttr('hidden');
        htmlstr = "<div class='alert alert-danger alert-dismissible fade show' role='alert'> <strong>Error!</strong> problem <a href='#' class='alert-link'>Data</a>  has been occurred while submitting your data :<span id='statusmsg'>" + data + "</span> <button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button></div > ";
    }
    else {
        $('#cardbox').removeAttr('hidden');
        htmlstr = "<div class='alert alert-info alert-dismissible fade show' role='alert'> <strongPlease read the</strong> comments <a href='#' class='alert-link'>Data</a> <span id='statusmsg'>" + data + "</span> <button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button></div > ";
    }
    $("#cardbox").html(htmlstr);
}
function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#blah')
                .attr('src', e.target.result)
                .width(50)
                .height(50);
        };
        reader.readAsDataURL(input.files[0]);
    }
}