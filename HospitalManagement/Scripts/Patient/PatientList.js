


var url = "";

//$(document).on('click', 'a[data-async="true"]', function (e) {
    
//    e.preventDefault();
    $("#PatientStatusData_ChronicMedicineData_ChronicMedicine_Name").autocomplete({
        source: function (request, response) {
            debugger;
            $("#PatientStatusData_ChronicMedicineData_ChronicMedicine_Name").text("")
            var MedicineIds = ""; var BrandCodess = "";
            $('#chronic_table > tbody  > tr').each(function () {
                MedicineIds += $(this).find("td").eq(0).find(":hidden").val() + ',';
                BrandCodess += $(this).find("td").eq(1).find(":hidden").val() + ',';
            });
            $.ajax({
                url: '/HMSPatient/GetSearchMedicine',
                type: "POST",
                dataType: "json",
                async: false,
                data: { Prefix: request.term, formulation_code: $("#PatientStatusData_ChronicMedicineData_formulation_code").val(), MedIds: MedicineIds, BrandCodess: BrandCodess },
                success: function (list) {
                    if (list.length > 0) {
                        response($.map(list, function (dataitem) {
                            return { label: dataitem.drug_generic_name, value: dataitem.drug_generic_name, med_id: dataitem.medicine_id, Brand_Name: dataitem.Brand_Name, brand_code: dataitem.brand_code };
                        }))
                    }
                    else {
                        var list_dt = [];
                        var arr = {};
                        arr.drug_generic_name = "No Record Found";
                        arr.medicine_id = "No Record Found";
                        list_dt.push(arr);
                        response($.map(list_dt, function (dataitem) {
                            return { label: dataitem.drug_generic_name, value: dataitem.medicine_id, Brand_Name: dataitem.Brand_Name };
                        }))
                        $("#PatientStatusData_ChronicMedicineData_ChronicMedicine_Name").val("");
                    }
                }
            })
        },
        select: function (e, i) {
            debugger;
            if (i.item.value == "No Record Found") { return false; }
            else {
                //$("#PatientStatusData_ChronicMedicineData_ChronicMedicine_Name").text(i.item.med_id);
                $("#MappingId").val(i.item.med_id);
                $("#brand_code").val(i.item.brand_code);
            }
        },
        minLength: 3,
    }).autocomplete("instance")._renderItem = function (ul, item) {
        return $("<li>")
            .append("<div>" + "<span style='color:#15558d;font-size:15px;font-weight:600;'>" + item.Brand_Name + "</span>" + "<br>" + "<span style='font-size:13px;'>" + item.label + "</span>" + "</div>")
            .appendTo(ul);
    };

//});


function GetDropdownDatafilter() {
    debugger;
    $('.loadingimg').css('display', 'block');
    var day = $("#Days").val() == 'Select Day' ? 'Select Day' : $("#Days").val();
    var firstDay = new Date();
    var lastDay = new Date();
    if ($('#Days').val() == 'Today') {
        firstDay = new Date().toDateString();
        lastDay = new Date().toDateString();
    }
    if ($('#Days').val() == 'Weekly') {
        var curr = new Date; // get current date
        var first = curr.getDate() - curr.getDay(); // First day is the day of the month - the day of the week
        var last = first + 6; // last day is the first day + 6
        firstDay = new Date(curr.setDate(first)).toUTCString();
        lastDay = new Date(curr.setDate(last)).toUTCString();
    }
    if ($('#Days').val() == 'Custom') {
        $('#colpfrom').removeClass('collapse');
        $('#colpto').removeClass('collapse');
        $('#colpbtn').removeClass('collapse');

        if ($('#datetimepicker11').val() != null && $('#datetimepicker11').val() != "") {
            var from = $("#datetimepicker11").val().split("/")
            var f = new Date(from[2], from[1] - 1, from[0])
            firstDay = f.toDateString();
        }
        if ($('#datetimepicker12').val() != null && $('#datetimepicker12').val() != "") {
            var to = $("#datetimepicker12").val().split("/")
            var t = new Date(to[2], to[1] - 1, to[0])
            lastDay = t.toDateString();
        }
    }
    else {
        $('#colpfrom').addClass('collapse');
        $('#colpto').addClass('collapse');
        $('#colpbtn').addClass('collapse');

    }
    url = "/HMSPatient/HMSPatientList?fromdate=" + firstDay + " &todate=" + lastDay + "";
    $.ajax({
        url: url,
        datatype: 'json',
        ContentType: 'application/json;utf-8'
    }).done(function (datahtml) {
        $('.loadingimg').css('display', 'none');
        $(".bindtable").empty();
        $(".bindtable").html($(datahtml).find(".bindtable").html());
        $(".datatable").dataTable({
            "order": [[2, "desc"]]
        });
       
       
        //SetDataTableFillter();
    });
}
var search = function () {
    debugger;
    $('.loadingimg').css('display', 'block');
    if ($('#datetimepicker11').val() != null && $('#datetimepicker11').val() != "") {
        var from = $("#datetimepicker11").val().split("/")
        var f = new Date(from[2], from[1] - 1, from[0])
        firstDay = f.toDateString();
    }
    else {
        $('#datetimepicker11').focus();
        return false;
    }
    if ($('#datetimepicker12').val() != null && $('#datetimepicker12').val() != "") {
        var to = $("#datetimepicker12").val().split("/")
        var t = new Date(to[2], to[1] - 1, to[0])
        lastDay = t.toDateString();
    }
    else {
        $('#datetimepicker12').focus();
        return false;
    }

    url = "/HMSPatient/HMSPatientList?fromdate=" + firstDay + " &todate=" + lastDay + "";
    $.ajax({
        url: url,
        datatype: 'json',
        ContentType: 'application/json;utf-8'
    }).done(function (datahtml) {
        $('.loadingimg').css('display', 'none');
        $(".bindtable").empty();
        $(".bindtable").html($(datahtml).find(".bindtable").html());
        $(".datatable").dataTable({
            "order": [[2, "desc"]]
        });
      
        //SetDataTableFillter();
    });

}
var ConfirmDelete = function (PATCode) {
    debugger;
    $("#hiddenpatcode").val(PATCode);
    $("#delete_patient").modal('show');
}
var DeleteHosPatient = function () {
    debugger;
    var PATCode = $("#hiddenpatcode").val();
    $.ajax({
        type: "POST",
        url: "/HMSPatient/HosPatientDelete",
        data: { PATCode: PATCode },
        success: function (result) {
            if (result == true) {
                $("#delete_patient").modal("hide");
                $("#row_" + PATCode).remove();
                location.reload();
            }
            else {
                alert("Something went to wrorng !!!")
            }
        }
    })
}
function GetDropdownData(Type) {
    debugger;
    $('.loadingimg').css('display', 'block');
    var url = "";
    if (Type == "surgery")
        url = "/HMSPatient/GetPatientOrgans?StatusNameId=" + $("#surgery").val() + "&Type=" + Type + "";
    else if (Type == "disease")
        url = "/HMSPatient/GetPatientOrgans?StatusNameId=" + $("#disease").val() + "&Type=" + Type + "";
    $.ajax({
        url: url,
        datatype: 'json',
        ContentType: 'application/json;utf-8'
    }).done(function (datahtml) {
        $('.loadingimg').css('display', 'none');
        if (Type == "surgery") {
            $(".surgerybnd").empty();
            $(".surgerybnd").html($(datahtml).find(".surgerybnd").html());
        }
        else if (Type == "disease") {
            $(".diseasebnd").empty();
            $(".diseasebnd").html($(datahtml).find(".diseasebnd").html());
        }

        $('#loadingmessage').hide();
    });
}
$('.collapsed').on('click', function () {
    //$("#Vital_model").find('input:text,input:file,select,textarea').val('');
    $("#collapseOne").find('input:text,input:file,select,textarea').val('');
    $("#collapseThree").find('input:text,input:file,select,textarea').val('');
    $('.collapse').collapse('hide');
})
function ajaxbegin() {
}
function SuccessMethod(data) {
    debugger;
    var Type = $(data).find(".vital_type").val();
    $("#collapseOne").find('input:text,input:file,select,textarea').val('');
    $("#collapseThree").find('input:text,input:file,select,textarea').val('');
    if (Type == "surgery") {
        var msg = $(data).find(".surgery_msg").val();
        $(".bindvitalsurgery").empty();
        $(".bindvitalsurgery").html($(data).find(".bindvitalsurgery").html());
        if (msg == "ins") {
            $('.ins').removeAttr('hidden');
        }
        else if (msg == "ext") {
            $('.ext').removeAttr('hidden');
        }
    }
    if (Type == "disease") {
        var msg = $(data).find(".deases_msg").val();
        $(".bindvitaldisease").empty();
        $(".bindvitaldisease").html($(data).find(".bindvitaldisease").html());
        if (msg == "ins") {
            $('.insdes').removeAttr('hidden');
        }
        else if (msg == "ext") {
            $('.extdes').removeAttr('hidden');
        }
    }
    if (Type == "other") {
        var msg = $(data).find(".other_msg").val();
        $(".bindothervital").empty();
        $(".bindothervital").html($(data).find(".bindothervital").html());
        if (msg == "ins") {
            $('.insother').removeAttr('hidden');
        }
        else if (msg == "ext") {
            $('.extother').removeAttr('hidden');
        }
    }
    if (Type == "chronic") {
        $(".bindchronicvital").empty();
        $(".bindchronicvital").html($(data).find(".bindchronicvital").html());
    }

    if (Type == "familyhistory") {
        $(".bindfamilyhistory").empty();
        $(".bindfamilyhistory").html($(data).find(".bindfamilyhistory").html());
        $("#collapseFive").find('input:text,input:file,select,textarea').val('');
    }
}
var OpenVitalModal = function (patcode) {
    debugger;
    $("#Vital_model").find('input:text,input:file,select, textarea').val('');
    $("#PatientCode").val(patcode);
    url = "/HMSPatient/GetVitalPara?PatientCode=" + patcode + "";
    $.ajax({
        url: url,
        datatype: 'json',
        ContentType: 'application/json;utf-8'
    }).done(function (datahtml) {
        $(".bindvitalsurgery").empty();
        $(".bindvitalsurgery").html($(datahtml).find(".bindvitalsurgery").html());
        $(".bindvitaldisease").empty();
        $(".bindvitaldisease").html($(datahtml).find(".bindvitaldisease").html());
        $(".bindothervital").empty();
        $(".bindothervital").html($(datahtml).find(".bindothervital").html());
        $(".bindchronicvital").empty();
        $(".bindchronicvital").html($(datahtml).find(".bindchronicvital").html());
        $(".bindfamilyhistory").empty();
        $(".bindfamilyhistory").html($(datahtml).find(".bindfamilyhistory").html());
        $("#Vital_model").modal('show');
        $("#collapseOne").addClass('show');
        $("#collapseThree").removeClass('show');
        $("#collapseFour").removeClass('show');

    });
}
var AddotherVitalPara = function () {
    var PatientCode = $("#PatientCode").val();
    var vitalCode = $('#Vitalcode').find('option:selected').val()
    var fromdate = $("#fromDate").val();
    var remarks = $("#Remarks").val();
    var Value = $("#vitalvalue").val();
    if (vitalCode == '') {
        $('#cardboxvital').removeAttr('hidden');
        var htmlstr = "<div class='alert alert-warning alert-dismissible fade show' role='alert'> <strong>Warning!</strong><a href='#' class='alert-link'></a> <span id='statusmsg'>Please Select Vital</span> <button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button></div > ";
        $("#cardboxvital").html(htmlstr);
        return false;
    }
    if (Value == '') {
        $('#cardboxvital').removeAttr('hidden');
        var htmlstr = "<div class='alert alert-warning alert-dismissible fade show' role='alert'> <strong>Warning!</strong> <a href='#' class='alert-link'></a> <span id='statusmsg'>Please Add Vital value</span> <button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button></div > ";
        $("#cardboxvital").html(htmlstr);
        return false;
    }
    var ischeck = true;
    if (ischeck) {
        $.ajax({
            type: "POST",
            url: "/HMSPatient/InsertVitalPara",
            data: '{vitalcode:"' + vitalCode + '", fromdate: "' + fromdate + '", remarks: "' + remarks + '", patientCode:"' + PatientCode + '", Value:"' + Value + '" }',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (datahtml) {
                $(".bindvitalpara").empty();
                $(".bindvitalpara").html($(datahtml).find(".bindvitalpara").html());
            }
        });
    }
};
$("#PatientStatusData_Other_Med_IsPregnent").change(function () {
    var ischecked = $(this).is(':checked');
    if (ischecked) {
        $("#PatientStatusData_Other_Med_DeleviryDate").attr('readonly', false);
        $("#PatientStatusData_Other_Med_DeleviryDate").val("");
    }
    else {
        $("#PatientStatusData_Other_Med_DeleviryDate").attr('readonly', true);
    }
});
var ConfirmDelete = function (vitalid) {
    debugger;
    $.ajax({
        type: "POST",
        url: "/HMSPatient/DeleteVitalSurgery",
        data: { VitalId: vitalid },
        success: function (result) {
            $("#row_" + vitalid).remove();
        }
    })
}
var ConfirmDeletechronic = function (chronicid) {
    debugger;
    $.ajax({
        type: "POST",
        url: "/HMSPatient/DeleteVitalChronic",
        data: { chronicid: chronicid },
        success: function (result) {
            $("#row_" + chronicid).remove();
        }
    })
}

var ConfirmDeletefamilyhistory = function (id) {
    debugger;
    $.ajax({
        type: "POST",
        url: "/HMSPatient/DeleteVitalChronic",
        data: { Id: id },
        success: function (result) {
            $("#row_" + chronicid).remove();
        }
    })
}
//Assign Click event to Plus Image.
$("body").on("click", "img[src*='plus.png']", function () {
    debugger;
    $(this).closest("tr").after("<tr class='del'><td colspan = '999'>" + $(this).next().html() + "</td></tr>");
    $(this).attr("src", "/Image/minus.png");
    var index = $(".active").attr("data_id");//get current active tab

    $('.nav-tabs a[data_id="' + index + '"]').tab('show');
    $('#myTab li a').click(function () {
        debugger;
        var index = $(this).attr('data_id');
        var contant = $(this).attr('id').split('-')[0];

        var index1 = $(".active").attr("data_id");//get current active tab
        var contant1 = $(".active").attr('id').split('-')[0];
        $('.nav-tabs a[data_id="' + index1 + '"]').removeClass('active');
        $('.nav-tabs a[data_id="' + index + '"]').addClass('active');
        $('.nav-tabs a[data_id="' + index + '"]').tab('show');
        $('.tab-content div[id="' + contant1 + '"]').removeClass('show');
        $('.tab-content div[id="' + contant1 + '"]').removeClass('active');
        $('.tab-content div[id="' + contant + '"]').addClass('show');
        $('.tab-content div[id="' + contant + '"]').addClass('active');
    });

});
//Assign Click event to Minus Image.
$("body").on("click", "img[src*='minus.png']", function () {
    debugger;
    $(this).attr("src", "/Image/plus.png");
    //$(this).closest("tr").next().remove();
    $(this).closest("tr").next('.del').remove();
});
$('th').on('click', function () {
    $("#DataTables_Table_0").find('.plusicon').find('.plusimg').attr("src", "/Image/plus.png");
    // DataTables has done a sort
});
function submit(Uniqrow) {
    debugger;
    $.post("/Dashboard/SetParaValue", { Para: Uniqrow }, function () { window.open('/HMSPatient/EditHMSPatient', '_self') });
}
//Added By Dhaval for Other Patient Search
function SuccessMethod_PatientFilter(datahtml) {
    debugger;
    $(".bindtable").empty();
    $(".bindtable").html($(datahtml).find(".bindtable").html());
    $(".datatable").dataTable({
        "order": [[2, "desc"]]
    });
   

    //$("#PatientSearch_MobileNo").val('');
    //$("#PatientSearch_Email").val('');
    //$("#PatientSearch_PatientName").val('');
    //$("#PatientSearch_PatientCode").val('');
    //$("#PatientSearch_PatientBirthYear").val('');
    //$("#PatientSearch_PatientFatherName").val('');
}
//Added By Dhaval End