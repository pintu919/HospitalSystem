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
          var month =  new Date().getMonth().toLocaleString().length > 1 ? (new Date().getMonth() + 1) : ("0" + (new Date().getMonth() + 1))
            var curdate = new Date().getDate() + "/" + month + "/"+ new Date().getFullYear();
            $('#DateOfBirth').data('DateTimePicker').date(curdate);
        }
    });
    $('#DateOfBirth').data("DateTimePicker").maxDate(new Date());
    //$("#Male").attr("checked", true);
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
    var file = $("#imageUploadForm").get(0).files;
    var data = new FormData;
    data.append("ImageFile", file[0]);
}
function SuccessMethod(data) {
    debugger;
    var htmlstr = "";
    if (data[0].status == "1") {
        $("#Regpatient").modal('hide');
        $('#cardbox').removeAttr('hidden');
        htmlstr = "<div class='alert alert-success alert-dismissible fade show' role='alert'> <strong>Success!</strong> Record Added Successfully! </span> <button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button></div > ";

    }
    else if (data[0].status == "0") {
        $('#cardbox').removeAttr('hidden');
        htmlstr = "<div class='alert alert-danger alert-dismissible fade show' role='alert'> Record Not Saved!</span> <button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button></div > ";
    }

    else if (data[0].status == "old") {
        $('#example2 tbody tr').remove();
        $.each(data, function (i, dt) {
            tr = $('<tr />');
            tr.append("<td>" + dt.name + "</td>");
            $('#example2').append(tr);
        });

        $("#Regpatient").modal('show');
    }

    else if (data[0].status == "3") {
        $('#cardbox').removeAttr('hidden');
        htmlstr = "<div class='alert alert-warning alert-dismissible fade show' role='alert'> Record Allrady Exists </span> <button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button></div > ";
    }

    $("#cardbox").html(htmlstr);

}
function closepopup() {
    debugger;
    $('#Relation').val('');
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