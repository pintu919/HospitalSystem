(function ($) {
    "use strict";

    //// Pricing Options Show

    //$('#pricing_select input[name="rating_option"]').on('click', function () {
    //    if ($(this).val() == 'price_free') {
    //        $('#custom_price_cont').hide();
    //    }
    //    if ($(this).val() == 'custom_price') {
    //        $('#custom_price_cont').show();
    //    }
    //    else {
    //    }
    //});

   

   
    // Experience Add More

    $(".experience-info").on('click', '.trash', function () {
        $(this).closest('.experience-cont').remove();
        return false;
    });

    $(".add-experience").on('click', function () {
        debugger;
       
        var optionList = $("#regent")[0].innerHTML;
        var count = $(".experience-info").find(".experience-cont").length;
        var experiencecontent = '<div class="row form-row experience-cont">' +
            '<div class="col-12 col-md-10 col-lg-12">' +
            '<div class="row form-row">' +
            '<div class="col-12 col-md-6 col-lg-2">' +
            '<div class="form-group">' +
            //'<label>Quantity</label>' +
            '<select class="form-control  requiredSelect" data-val="true"  id="regent" name="[0].purchaseItem[' + count + '].ReagentId">' + optionList +
            '</select > ' +
            '</div>' +
            '</div>' +
            '<div class="col-12 col-md-6 col-lg-1">' +
            '<div class="form-group">' +
            //'<label>Quantity</label>' +
            '<input type="number" data-val="true"  min="1" data-val-required="" name="[0].purchaseItem[' + count + '].Qty" class="form-control" >' +
            '</div>' +
            '</div>' +
            '<div class="col-12 col-md-6 col-lg-2">' +
            '<div class="form-group">' +
            //'<label>Product Expiry</label>' +
            '<input type="text" data-val="true" data-val-required="" name="[0].purchaseItem[' + count + '].ProductExpiry" class="form-control datetimepicker required" autocomplete="off">' +
            '</div>' +
            '</div>' +
            '<div class="col-12 col-md-6 col-lg-2">' +
            '<div class="form-group">' +
            //'<label>Quantity Per Unit</label>' +
            '<input type="number" data-val="true" min="1"  data-val-required="" name="[0].purchaseItem[' + count + '].QtyPerUnit" class="form-control " autocomplete="off">' +
            '</div>' +
            '</div>' +
            '<div class="col-12 col-md-6 col-lg-2">' +
            '<div class="form-group">' +
            //'<label>Quantity Per Unit</label>' +
            '<input type="number" data-val="true" min="1"  data-val-required="" name="[0].purchaseItem[' + count + '].Amount" class="form-control " autocomplete="off">' +
            '</div>' +
            '</div>' +
            '<div class="col-12 col-md-6 col-lg-2">' +
            '<div class="form-group">' +
            //'<label>Quantity Per Unit</label>' +
            '<input type="number" data-val="true" min="1"  data-val-required="" name="[0].purchaseItem[' + count + '].ValidityDays" class="form-control " autocomplete="off">' +
            '</div>' +
            '</div>' +
            //'<div class="col-12 col-md-6 col-lg-4">' +
            //'<div class="form-group">' +
            //'<label>Designation</label>' +
            //'<input type="text" data-val="true" data-val-required="" name="[' + count + '].Designation" class="form-control">' +
            //'</div>' +
            //'</div>' +
            '<div class="col-12 col-md-2 col-lg-1"><label class="d-sm-none d-none">&nbsp;</label><a href="#" class="btn btn-danger trash"><i class="fa fa-trash"></i></a></div>' +
            '</div>' +
            '</div>' +
           
            '</div>';
        $(".experience-info").append(experiencecontent);
        $('.datetimepicker').datetimepicker({
            format: 'DD/MM/YYYY',
            icons: {
                up: "fa fa-chevron-up",
                down: "fa fa-chevron-down",
                next: 'fa fa-chevron-right',
                previous: 'fa fa-chevron-left'
            },
            useCurrent: false,

        });
        var form = $("form")
            .removeData("validator")
            .removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse(form);
        return false;
    });

    //// Awards Add More

    //$(".awards-info").on('click', '.trash', function () {
    //    $(this).closest('.awards-cont').remove();
    //    return false;
    //});

    //$(".add-award").on('click', function () {

    //    var regcontent = '<div class="row form-row awards-cont">' +
    //        '<div class="col-12 col-md-5">' +
    //        '<div class="form-group">' +
    //        '<label>Awards</label>' +
    //        '<input type="text" class="form-control">' +
    //        '</div>' +
    //        '</div>' +
    //        '<div class="col-12 col-md-5">' +
    //        '<div class="form-group">' +
    //        '<label>Year</label>' +
    //        '<input type="text" class="form-control">' +
    //        '</div>' +
    //        '</div>' +
    //        '<div class="col-12 col-md-2">' +
    //        '<label class="d-md-block d-sm-none d-none">&nbsp;</label>' +
    //        '<a href="#" class="btn btn-danger trash"><i class="far fa-trash-alt"></i></a>' +
    //        '</div>' +
    //        '</div>';

    //    $(".awards-info").append(regcontent);
    //    return false;
    //});

    //// Membership Add More

    //$(".membership-info").on('click', '.trash', function () {
    //    $(this).closest('.membership-cont').remove();
    //    return false;
    //});

    //$(".add-membership").on('click', function () {

    //    var membershipcontent = '<div class="row form-row membership-cont">' +
    //        '<div class="col-12 col-md-10 col-lg-5">' +
    //        '<div class="form-group">' +
    //        '<label>Memberships</label>' +
    //        '<input type="text" class="form-control">' +
    //        '</div>' +
    //        '</div>' +
    //        '<div class="col-12 col-md-2 col-lg-2">' +
    //        '<label class="d-md-block d-sm-none d-none">&nbsp;</label>' +
    //        '<a href="#" class="btn btn-danger trash"><i class="far fa-trash-alt"></i></a>' +
    //        '</div>' +
    //        '</div>';

    //    $(".membership-info").append(membershipcontent);
    //    return false;
    //});

    //// Registration Add More

    //$(".registrations-info").on('click', '.trash', function () {
    //    $(this).closest('.reg-cont').remove();
    //    return false;
    //});

    //$(".add-reg").on('click', function () {

    //    var regcontent = '<div class="row form-row reg-cont">' +
    //        '<div class="col-12 col-md-5">' +
    //        '<div class="form-group">' +
    //        '<label>Registrations</label>' +
    //        '<input type="text" class="form-control">' +
    //        '</div>' +
    //        '</div>' +
    //        '<div class="col-12 col-md-5">' +
    //        '<div class="form-group">' +
    //        '<label>Year</label>' +
    //        '<input type="text" class="form-control">' +
    //        '</div>' +
    //        '</div>' +
    //        '<div class="col-12 col-md-2">' +
    //        '<label class="d-md-block d-sm-none d-none">&nbsp;</label>' +
    //        '<a href="#" class="btn btn-danger trash"><i class="far fa-trash-alt"></i></a>' +
    //        '</div>' +
    //        '</div>';

    //    $(".registrations-info").append(regcontent);
    //    return false;
    //});

})(jQuery);