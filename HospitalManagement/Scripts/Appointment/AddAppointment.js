
$(document).ready(function () {
    $(".select2").select2();
    $("#country").select2();
    $("#NonRegPatAppointment_HosDepCode").select2();
    $("#MaritalStatus").val('Single');
    $('#DateOfBirth').datetimepicker({
        format: "dd/mm/yyyy",
        minView: 2
    }).on('change', function (e) {
        debugger;
        var today = new Date();
        var birthDate = new Date(e.target.value.split("/").reverse().join("/"));
        var age = today.getFullYear() - birthDate.getFullYear();
        var m = today.getMonth() - birthDate.getMonth();
        if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
            age--;
        }
        $('#Patientage').val(age);
    });
    $('#datetimepicker11').datetimepicker({
        format: "dd/mm/yyyy",
        autoclose: true,
        minView: 2,
        startDate: new Date()
    });
    $('#datetimepicker14').datetimepicker({
        format: "dd/mm/yyyy",
        autoclose: true,
        minView: 2,
        startDate: new Date()
    });
    $('#datetimepicker12').datetimepicker({
        format: "dd/mm/yyyy",
        autoclose: true,
        minView: 2,
        startDate: new Date()
    });
    $('#datetimepicker13').datetimepicker({
        format: "dd/mm/yyyy",
        autoclose: true,
        minView: 2,
        startDate: new Date()
    });
    $('#DateWise').change(function () {
        debugger;
        $('#datwise').removeAttr('hidden');
        $("#Appointdate").attr("hidden", true);
        $("form").find('input:text,select').val('');
        // $("#form2").find('input:text,select').val('');
    });
    $('#DepartmentWise').change(function () {
        $('#Appointdate').removeAttr('hidden');
        $("#datwise").attr("hidden", true);
        $("form").find('input:text,select').val('');
        //  $("#form2").find('input:text,select').val('');
    });
    $('#DateWise_Non_Reg').change(function () {
        debugger;
        $('#datwise_non_reg').removeAttr('hidden');
        $("#Appointdate_Non_Reg").attr("hidden", true);
        $("form").find('input:text,select').val('');

    });
    $('#DepartmentWise_Non_Reg').change(function () {
        $('#Appointdate_Non_Reg').removeAttr('hidden');
        $("#datwise_non_reg").attr("hidden", true);
        $("form").find('input:text,select').val('');

    });
});
//$("#RegPatAppointment_patient_code").autocomplete({
//    source: function (request, response) {
//        $.ajax({
//            url: '/HMSDoctorAppointment/GatePatientCode',
//            type: "POST",
//            dataType: "json",
//            async: false,
//            data: { Prefix: request.term },
//            success: function (data) {
//                if (data.length > 0) {
//                    response($.map(data, function (item) {
//                        return { label: item.patient_firstname, value: item.patient_code };
//                    }))
//                }
//                else {
//                    var data = [];
//                    var arr = {};
//                    arr.patient_firstname = "No Record Found";
//                    arr.patient_code = "";
//                    data.push(arr);
//                    response($.map(data, function (item) {
//                        return { label: item.patient_firstname, value: item.patient_code };
//                    }))
//                    $("#patient_code").val("");
//                }
//            }
//        })
//    },
//    select: function (e, i) {
//        if (i.item.value == "No Record Found") { return false; }
//        else
//            $("#patient_code").val(i.item.value);

//    },
//    minLength: 3,
//});

$("#RegPatAppointment_PatientName").autocomplete({
    source: function (request, response) {
        $.ajax({
            url: '/HMSDoctorAppointment/GatePatientCode',
            type: "POST",
            dataType: "json",
            async: false,
            data: { Prefix: request.term },
            success: function (data) {
                debugger;
                if (data.length > 0) {
                    response($.map(data, function (item) {

                        return { label: item.patient_firstname, value: item.patient_firstname, patient_code: item.patient_code };
                    }))
                }
                else {
                    var data = [];
                    var arr = {};
                    arr.patient_firstname = "No Record Found";
                    arr.patient_code = "";
                    data.push(arr);
                    response($.map(data, function (item) {

                        return { label: item.patient_firstname, value: item.patient_firstname, patient_code: item.patient_code };
                    }))
                    $("#RegPatAppointment_patient_code").val("");
                    $("#RegPatAppointment_PatientName").val("");
                    $('#patdanger').removeAttr('hidden');
                    $('#patsucess').attr('hidden', true);

                }
            }
        })
    },
    select: function (e, i) {
        if (i.item.value == "No Record Found") { return false; }
        else {

            $.ajax({
                url: '/HMSDoctorAppointment/Checkpatient',
                type: "POST",
                dataType: "json",
                async: false,
                data: { patient_code: i.item.patient_code },
                success: function (data) {
                    debugger;
                    if (data == true) {
                        $('#cardbox').removeAttr('hidden');
                        var htmlstr1 = "<div class='alert alert-warning alert-dismissible fade show' role='alert'>  <span id='statusmsg'> Your selected patient is admit in this Hospital, So you can not take an appointment for this patient !!!!</span> <button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button></div > ";
                        $("#cardbox").html(htmlstr1);
                        i.item.patient_code = "";
                        i.item.value = "";
                        $("#RegPatAppointment_patient_code").val("");
                        $("#RegPatAppointment_PatientName").val("");
                        
                    }
                    else {
                        $('#patmsg').attr('hidden', true);
                        $('#cardbox').attr('hidden', true);
                        $("#RegPatAppointment_patient_code").val(i.item.patient_code);
                        $("#RegPatAppointment_PatientName").val(i.item.value);
                        $('#patsucess').removeAttr('hidden');
                        $('#patdanger').attr('hidden', true);
                    }
                    
                }
            })

           

          
        }
    },
    minLength: 3,
}).bind("keyup.autocomplete", function (event) {
    if (event.keyCode == $.ui.keyCode.BACKSPACE || event.keyCode == $.ui.keyCode.DELETE) {
        $("#RegPatAppointment_patient_code").val("");
        $("#RegPatAppointment_PatientName").val("");
        $('#patdanger').removeAttr('hidden');
        $('#patsucess').attr('hidden', true);
    };
});


function ajaxbegin() {
    debugger;
    if ($("#RegPatAppointment_patient_code").val() == "" || $("#RegPatAppointment_patient_code").val() == null) {
        $('#patmsg').removeAttr('hidden');
        return false;
    }
    else {
        $('#patmsg').attr('hidden', true);
    }

    var file = $("#imageUploadForm").get(0).files;
    var data = new FormData;
    data.append("ImageFile", file[0]);
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

function SuccessMethodNonreg(data) {
    debugger;
    var htmlstr = "";
    if (data[0].status == "1") {
        $("#Regpatient").modal('hide');
        $('#cardbox1').removeAttr('hidden');
        htmlstr = "<div class='alert alert-success alert-dismissible fade show' role='alert'> <strong>Success!</strong> Record Added Successfully! </span> <button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button></div > ";

    }
    else if (data[0].status == "0") {
        $('#cardbox1').removeAttr('hidden');
        htmlstr = "<div class='alert alert-danger alert-dismissible fade show' role='alert'> Record Not Saved!</span> <button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button></div > ";
    }

    else if (data[0].status == "old") {
        $('#example2 tbody tr').remove();
        $.each(data, function (i, dt) {
            tr = $('<tr />');
            tr.append("<td>" + dt.name + "</td>");
            $('#example2').append(tr);
        });


        //$('#cardbox1').removeAttr('hidden');
        //htmlstr = "<div class='alert alert-warning alert-dismissible fade show' role='alert'> " + data[1] + "</span> <button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button></div > ";
        $("#Regpatient").modal('show');
    }

    else if (data[0].status == "3") {
        $('#cardbox1').removeAttr('hidden');
        htmlstr = "<div class='alert alert-warning alert-dismissible fade show' role='alert'> Record Allrady Exists </span> <button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button></div > ";
    }

    //else {
    //    $('#cardbox1').removeAttr('hidden');
    //    htmlstr = "<div class='alert alert-info alert-dismissible fade show' role='alert'> " + data[1] + "</span> <button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button></div > ";
    //}
    $("#cardbox1").html(htmlstr);

}

function closepopup() {
    debugger;
    $('#NonRegPatAppointment_Relation').val('');
}

function SuccessMethod(data) {
    $("#clear_data").find('input:text,input:file,select, textarea').val('');
    var htmlstr = "";
    if (data == "Record Added Successfully!") {
        $('#cardbox').removeAttr('hidden');
        htmlstr = "<div class='alert alert-success alert-dismissible fade show' role='alert'> <strong>Success!</strong> Your <a href='#' class='alert-link'></a> <span id='statusmsg'>" + data + "</span> <button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button></div>";
        /*$('.select2').val(null).trigger('change');*/
        $('#RegPatAppointment_HosDepCode').val('').trigger('change.select2');
        $('#hosDoctor').val('').trigger('change.select2');
        $('#DoctorTiming').val('').trigger('change.select2');
        $("#cardbox").html(htmlstr);
        return false;
       
        
    }
    else if (data == "Record Not Saved!") {
        $('#cardbox').removeAttr('hidden');
        htmlstr = "<div class='alert alert-danger alert-dismissible fade show' role='alert'> <strong>Error!</strong> problem <a href='#' class='alert-link'></a>  has been occurred while submitting your data :<span id='statusmsg'>" + data + "</span> <button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button></div>";
        $("#cardbox").html(htmlstr);
        return false;
    }
    else if (data == "Please Enter Valid Patient") {
        $('#cardbox').removeAttr('hidden');
        htmlstr = "<div class='alert alert-warning alert-dismissible fade show' role='alert'> <strong>Comments!</strong> problem <a href='#' class='alert-link'></a> <span id='statusmsg'>" + data + "</span> <button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button></div>";
        $("#cardbox").html(htmlstr);
        return false;
    }
    else {
        $('#cardbox').removeAttr('hidden');
        htmlstr = "<div class='alert alert-info alert-dismissible fade show' role='alert'> <strongPlease read the</strong> comments <a href='#' class='alert-link'>Data</a> <span id='statusmsg'>" + data + "</span> <button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button></div>";
        $("#cardbox").html(htmlstr);
        return false;
    }
 
   
}
function GetDropdownData(Type) {
    debugger;
    var radioValue = "";
    if (Type == "Register_Pat")
        radioValue = $("[name='RegPatAppointment.AppointmentType']:checked").val();
    else if (Type == "Non_Reg_Patient")
        radioValue = $("[name='NonRegPatAppointment.AppointmentType']:checked").val();

    var Depart_Code = "";
    if (Type == "Register_Pat")
        Depart_Code = $("#RegPatAppointment_HosDepCode").val();
    else if (Type == "Non_Reg_Patient")
        Depart_Code = $("#NonRegPatAppointment_HosDepCode").val();
    if (radioValue == 'Department Wise') {
        $('.loadingimg').css('display', 'block');
        var url = "";
        url = "/HMSDoctorAppointment/GetDatabyCode?code=" + Depart_Code + "";
        $.ajax({
            url: url,
            datatype: 'json',
            ContentType: 'application/json;utf-8'
        }).done(function (datahtml) {
            $('.loadingimg').css('display', 'none');
            if (Type == "Register_Pat") {
                $(".binddoc").empty();
                $(".binddoc").html($(datahtml).find(".binddoc").html());
                $(".select2").select2();
            }
            else if (Type == "Non_Reg_Patient") {
                $(".non_regbinddoc").empty();
                $(".non_regbinddoc").html($(datahtml).find(".non_regbinddoc").html());
                $(".select2").select2();
            }
        });

       
    }
    else {
        var date = "";
        if (Type == "Register_Pat")
            date = $("#datetimepicker12").val();
        else if (Type == "Non_Reg_Patient")
            date = $("#datetimepicker13").val();
        var newdate = date.split("/").reverse().join("-");
        var d = new Date(newdate);
        var day = d.getDay();
        $('.loadingimg').css('display', 'block');
        var url = "";
        url = "/HMSDoctorAppointment/GetDocDatabyCode?code=" + Depart_Code + " &day=" + day + "";
        $.ajax({
            url: url,
            datatype: 'json',
            ContentType: 'application/json;utf-8'
        }).done(function (datahtml) {
            $('.loadingimg').css('display', 'none');
            if (Type == "Register_Pat") {
                $(".binddoc").empty();
                $(".binddoc").html($(datahtml).find(".binddoc").html());
                $(".select2").select2();
                
            }
            else if (Type == "Non_Reg_Patient") {
                $(".non_regbinddoc").empty();
                $(".non_regbinddoc").html($(datahtml).find(".non_regbinddoc").html());
                $(".select2").select2();
            }
        });
    }
}
function GetDoctorDays(Type) {
    debugger;
    var radioValue = "";
    var doc_code = "";
    if (Type == "Reg_Pat") {
        doc_code = $("#hosDoctor").val();
        radioValue = $("[name='RegPatAppointment.AppointmentType']:checked").val();
    }
    else if (Type == "Non_Reg_Pat") {
        doc_code = $("#hosDoctor_nonreg").val();
        radioValue = $("[name='NonRegPatAppointment.AppointmentType']:checked").val();
    }
    if (radioValue == 'Department Wise') {
        $('.loadingimg').css('display', 'block');
        $.ajax({
            url: "/HMSDoctorAppointment/GateDoctorDays",
            dataType: "json",
            ContentType: 'application/json;utf-8',
            data: { code: doc_code },
            success: function (data) {
                $('.loadingimg').css('display', 'none');
                if (data.days.length > 0) {
                    var sub_array = [];
                    for (var i = 0; i < data.days.length; i++) {
                        sub_array.push(data.days[i].daynumber);
                    }
                    if (Type == "Reg_Pat") {
                        $('#datetimepicker11').datetimepicker('setDaysOfWeekDisabled', sub_array);
                    }
                    else if (Type == "Non_Reg_Pat") {
                        $('#datetimepicker14').datetimepicker('setDaysOfWeekDisabled', sub_array);
                    }
                    var arr = [];
                    if (data.date.length > 0) {
                        var startDate = new Date(data.date[0].fromdate);
                        var endDate = new Date(data.date[0].todate);
                        var dt = new Date(startDate);
                        while (dt <= endDate) {
                            var d = new Date(dt);
                            month = '' + (d.getMonth() + 1),
                                day = '' + d.getDate(),
                                year = d.getFullYear();
                            if (month.length < 2) month = '0' + month;
                            if (day.length < 2) day = '0' + day;
                            var date = [year, month, day].join('-');
                            arr.push(date);
                            dt.setDate(dt.getDate() + 1);
                        }
                    }
                    if (Type == "Reg_Pat") {
                        $('#datetimepicker11').datetimepicker('setDatesDisabled', arr);
                    }
                    else if (Type == "Non_Reg_Pat") {
                        $('#datetimepicker14').datetimepicker('setDatesDisabled', arr);
                    }
                }
                else {
                    if (Type == "Reg_Pat") {
                        $('#datetimepicker11').datetimepicker('setDaysOfWeekDisabled', []);
                    }
                    else if (Type == "Non_Reg_Pat") {
                        $('#datetimepicker14').datetimepicker('setDaysOfWeekDisabled', []);
                    }
                }
                if (Type == "Reg_Pat") {
                    $('#datetimepicker11').datetimepicker('show');
                }
                else if (Type == "Non_Reg_Pat") {
                    $('#datetimepicker14').datetimepicker('show');
                }
            }
        })
    }
    else {
        var date = "";
        var doc_code = "";
        if (Type == "Reg_Pat") {
            date = $("#datetimepicker12").val();
            doc_code = $("#hosDoctor").val();
        }
        else if (Type == "Non_Reg_Pat") {
            doc_code = $("#hosDoctor_nonreg").val();
            date = $("#datetimepicker13").val();
        }
        var newdate = date.split("/").reverse().join("-");
        var d = new Date(newdate);
        var day = d.getDay();
        var date1 = new Date();
        var hours = date1.getHours() < 10 ? "0" + date1.getHours() : date1.getHours();
        var minutes = date1.getMinutes() < 10 ? "0" + date1.getMinutes() : date1.getMinutes();
        var time = hours + ":" + minutes + ":" + '00';
        $('.loadingimg').css('display', 'block');
        var url = "";
        url = "/HMSDoctorAppointment/GetDoctorTimeSlot?Doccode=" + doc_code + " &day=" + day + " &AppointDate=" + date + " &time=" + time + " ";
        $.ajax({
            url: url,
            datatype: 'json',
            ContentType: 'application/json;utf-8'
        }).done(function (datahtml) {
            $('.loadingimg').css('display', 'none');
            if (Type == "Reg_Pat") {
                $(".bindTimeslot").empty();
                $(".bindTimeslot").html($(datahtml).find(".bindTimeslot").html());
                $(".select2").select2();
            }
            else if (Type == "Non_Reg_Pat") {
                $(".non_regbindTimeslot").empty();
                $(".non_regbindTimeslot").html($(datahtml).find(".non_regbindTimeslot").html());
                $(".select2").select2();
            }
        });
    }
}
$("#datetimepicker11").change(function () {
    var dat = $("#datetimepicker11").val();
    $("#datetimepicker12").val(dat);
    var date = $("#datetimepicker11").val();
    var newdate = date.split("/").reverse().join("-");
    var d = new Date(newdate);
    var day = d.getDay();
    var date1 = new Date();
    var hours = date1.getHours() < 10 ? "0" + date1.getHours() : date1.getHours();
    var minutes = date1.getMinutes() < 10 ? "0" + date1.getMinutes() : date1.getMinutes();
    var time = hours + ":" + minutes + ":" + '00';
    $('.loadingimg').css('display', 'block');
    var url = "";
    url = "/HMSDoctorAppointment/GetDoctorTimeSlot?Doccode=" + $("#hosDoctor").val() + " &day=" + day + " &AppointDate=" + $("#datetimepicker11").val() + " &time=" + time + " ";
    $.ajax({
        url: url,
        datatype: 'json',
        ContentType: 'application/json;utf-8'
    }).done(function (datahtml) {
        $('.loadingimg').css('display', 'none');
        $(".bindTimeslot").empty();
        $(".bindTimeslot").html($(datahtml).find(".bindTimeslot").html());
        $(".select2").select2();
    });
});
$("#datetimepicker14").change(function () {
    var dat = $("#datetimepicker14").val();
    $("#datetimepicker13").val(dat);
    var date = $("#datetimepicker14").val();
    var newdate = date.split("/").reverse().join("-");
    var d = new Date(newdate);
    var day = d.getDay();
    var date1 = new Date();
    var hours = date1.getHours() < 10 ? "0" + date1.getHours() : date1.getHours();
    var minutes = date1.getMinutes() < 10 ? "0" + date1.getMinutes() : date1.getMinutes();
    var time = hours + ":" + minutes + ":" + '00';
    $('.loadingimg').css('display', 'block');
    var url = "";
    url = "/HMSDoctorAppointment/GetDoctorTimeSlot?Doccode=" + $("#hosDoctor_nonreg").val() + " &day=" + day + " &AppointDate=" + $("#datetimepicker14").val() + " &time=" + time + " ";
    $.ajax({
        url: url,
        datatype: 'json',
        ContentType: 'application/json;utf-8'
    }).done(function (datahtml) {
        $('.loadingimg').css('display', 'none');
        $(".non_regbindTimeslot").empty();
        $(".non_regbindTimeslot").html($(datahtml).find(".non_regbindTimeslot").html());
        $(".select2").select2();
    });
});
$("#datetimepicker12").change(function () {
    $("#RegPatAppointment_HosDepCode").val('');
    $("#hosDoctor").val('');
});
$("#datetimepicker13").change(function () {
    $("#NonRegPatAppointment_HosDepCode").val('');
    $("#hosDoctor_nonreg").val('');
});

function GetDropdownData_Country(Type) {
    debugger;
    $('.loadingimg').css('display', 'block');
    var url = "";
    if (Type == "state")
        url = "/HMSDoctorAppointment/GetDatabyCode_data?code=" + $("#country").val() + "&Type=" + Type + "";
    else if (Type == "district")
        url = "/HMSDoctorAppointment/GetDatabyCode_data?code=" + $("#state").val() + "&Type=" + Type + "";
    else if (Type == "city")
        url = "/HMSDoctorAppointment/GetDatabyCode_data?code=" + $("#district").val() + "&Type=" + Type + "";
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