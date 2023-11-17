("use strict");
function deleteRow(e, cnt) {
    var t = $("#purchaseTable > tbody > tr").length;
    if (1 == t) {
        var htmlstr = "<div class='alert text-white bg-warning' role='alert'><div class='rh-alert-icon'> <i class='ri-alert-fill'></i></div><div class='rh-alert-text'> There is only one row so you can't delete. </div><button type='button' class='close' data-dismiss='alert' aria-label='Close'><i class='ri-close-line'></i ></button></div>";
        $('.purmsg').removeAttr('hidden');
        $(".purmsg").html(htmlstr);
        return false;
    }
    else {
        var a = e.parentNode.parentNode;
        a.parentNode.removeChild(a);
        $("#deleterow_" + (parseInt(cnt) - 1)).removeAttr('hidden');
        $("#purchaseTable tbody tr").each(function (i, index) {
            var Cnt = $(index).attr("id");
            purchase_calculation(Cnt);
        });
    }
}
("use strict");
function add_purchaseRow(divName) {

    var row = parseInt($("#purchaseTable tbody").find("tr").last().attr("id")) + 1;
    var count = row;
    var newdiv = ""
    tabindex = count * 7,
        tab1 = tabindex + 1,
        tab2 = tabindex + 2,
        tab3 = tabindex + 3,
        tab6 = tabindex + 6,
        tab8 = tabindex + 8;
    newdiv =
        '<tr id="' + count + '"><td class="span3 manufacturer"><div class="form-group mb-0"> <textarea  name="PurchaseItemList[' + count + '].Name" rows="1" class="form-control no-resize overflow-hidden Item_Name" onkeypress="product_list_purchase(' +
        count + ')" autocomplete = "off" placeholder="Item Name" id="Item_Name_' + count + '" tabindex="' + tab1 + '" required></textarea>' +
        '<input type="hidden" class="autocomplete_hidden_value" name="PurchaseItemList[' + count + '].ItemID" id="ItemId_' + count + '"/>' +
        '<input type="hidden" class="sl" value="' + count + '"> </div></td>' +

        '<td> <input type="text" name="PurchaseItemList[' + count + '].BatchId" id="BatchId_' + count + '" tabindex="' + tab2 +
        '" class="form-control text-right" autocomplete = "off"  tabindex="11" placeholder="Batch No" required /></td>' +

        '<td><input type="text" name="PurchaseItemList[' + count + '].ExpiryDate" autocomplete = "off"  id="ExpiryDate_' + count + '"  class="datetimepicker form-control" tabindex="' +
        tab3 + '" placeholder="Expire Date" autocomplete = "off"/> </td>"' +

        //'<td class="text-right"><input type="text" name="PurchaseItemList[' + count + '].BoxQty"  required  id="BoxQty_' +
        //count + '" autocomplete = "off" class="form-control text-right BoxQty_' + count + '"  placeholder="0.00" value="" min="0" /></td>' +

        '<td class="text-right"><input type="text" name="PurchaseItemList[' + count + '].Qty"  required id="Qty_' +
        count + '" autocomplete = "off" class="form-control text-right Qty_' + count + ' valid_number" onkeyup="purchase_calculation(' + count + ')" placeholder="0" value="" min="0" /></td>' +

        '<td class="test"> <input type="text" name="PurchaseItemList[' + count + '].Vat" onkeyup="purchase_calculation(' + count + ')" id="Vat_' + count + '" class="form-control Price_' +
        count + ' text-right valid_number" autocomplete = "off"  placeholder="0.00" value="" min="0" tabindex="' + tab6 + '"/></td>' +

        '<td class="test"> <input type="text" name="PurchaseItemList[' + count + '].Price" required onkeyup="purchase_calculation(' + count + ')" id="Price_' + count + '" class="form-control Price_' +
        count + ' text-right valid_number" autocomplete = "off" required placeholder="0.00" value="" min="0" tabindex="' + tab6 + '"/></td>' +

        '<td class="text-right"><input class="form-control total_price text-right TotalPurchasePrice_' + count +
        '" type="text" name="PurchaseItemList[' + count + '].TotalPurchasePrice" id="TotalPurchasePrice_' + count +
        '" value="0.00" autocomplete = "off" readonly="readonly" /> </td>' +

        '<td id = "deleterow_' + count + '" > <input id="total_tax_' + count + '" class="total_tax_' + count + ' valid_number" type="hidden">' +
        '<input id = "all_tax_' + count + '" class="total_tax" type = "hidden" name = "tax[]" > <a href="#" class="btn btn-sm btn-icon rh-bg-danger-light" tabindex="' +
        tab8 + '" onclick="deleteRow(this,' + count + ')"><i class="fa fa-trash"></i></a></td></tr>';

    $(".experience-info").append(newdiv);
    $("#deleterow_" + (parseInt(count) - 1)).attr('hidden', true);
    $("#ExpiryDate_" + count).datetimepicker({
        format: 'DD/MM/YYYY',
        icons: {
            up: "fa fa-chevron-up",
            down: "fa fa-chevron-down",
            next: 'fa fa-chevron-right',
            previous: 'fa fa-chevron-left'
        },
        useCurrent: false,
    });
    $(".valid_number").keypress(function (event) {
        var charCode = event.which ? event.which : event.keyCode;
        if (charCode != 46 && charCode != 45 && charCode > 31 && (charCode < 48 || charCode > 57)) return false;
        return true;
    });
    count++;
}
("use strict");
function product_list_purchase(sl) {
    var manufacturer_id = $("#supplier").val();
    if (manufacturer_id == '') {
        $("#supplier").select2("open");
        return false;
    }
    var options = {
        minLength: 2,
        source: function (request, response) {
            var ItemIDs = "";
            $('#purchaseTable > tbody  > tr').each(function () {
                ItemIDs += $(this).find("td").eq(0).find(":hidden").val() + ',';
            });
            var url = "/Stock/GetSearchStockItem?prefix=" + request.term + " &itemids=" + ItemIDs + " &supplierid=" + manufacturer_id + "";
            $.ajax({
                url: url,
                type: "POST",
                dataType: "json",
                async: false,
                success: function (list) {
                    //response(data);
                    if (list.length > 0) {
                        response($.map(list, function (dataitem) {
                            return { label: dataitem.ItemName, ItemId: dataitem.ItemId };
                        }))
                    }
                    else {
                        debugger;
                        var item = "Item_Name_" + sl;
                        $("#" + item).val("");
                        var itm_id = "ItemId_" + sl;
                        $("#" + itm_id).val("");
                        var bat_id = "BatchId_" + sl;
                        $("#" + bat_id).val("");
                        var exp_date = "ExpiryDate_" + sl;
                        $("#" + exp_date).val("");
                        //var boxqty = "BoxQty_" + sl;
                        //$("#" + boxqty).val("");
                        var qty_id = "Qty_" + sl;
                        $("#" + qty_id).val("");
                        var phar_id = "Price_" + sl;
                        $("#" + phar_id).val("");
                        var vat = "Vat_" + sl;
                        $("#" + vat).val("");
                        var total_price = "TotalPurchasePrice_" + sl;
                        $("#" + total_price).val("");
                        purchase_calculation(sl);
                        var list_dt = [];
                        var arr = {};
                        arr.ItemName = "No Record Found";
                        arr.ItemId = "No Record Found";
                        list_dt.push(arr);
                        response($.map(list_dt, function (dataitem) {
                            return { label: dataitem.ItemName, value: dataitem.ItemId };
                        }))
                    }
                },
                error: function (textStatus, errorThrown) {

                    alert(errorThrown);
                }
            });
        },
        select: function (event, ui) {
            if (ui.item.value == "No Record Found") {
                var slid = $(this).parent().parent().find(".sl").val();
                var itemname = "Item_Name_" + slid;
                $("#" + itemname).val("");
                var med_id = "ItemId_" + slid;
                $("#" + med_id).val("");
                var bat_id = "BatchId_" + slid;
                $("#" + bat_id).val("");
                var exp_date = "ExpiryDate_" + slid;
                $("#" + exp_date).val("");
                //var boxqty = "BoxQty_" + slid;
                //$("#" + boxqty).val("");
                var qty_id = "Qty_" + slid;
                $("#" + qty_id).val("");
                var phar_id = "Price_" + slid;
                $("#" + phar_id).val("");
                var vat = "Vat_" + slid;
                $("#" + vat).val("");
                var total_price = "TotalPurchasePrice_" + slid;
                $("#" + total_price).val("");
                return false;
            }
            else {
                $(this).val(ui.item.label);
                $(this).parent().parent().find(".autocomplete_hidden_value").val(ui.item.ItemId);
                $(this).unbind("change");
                return false;
            }
        },
        minLength: 2,
    };
    $("body").on("keypress.autocomplete", ".Item_Name", function () {
        $(this).autocomplete(options).autocomplete("instance")._renderItem = function (ul, item) {
            return $("<li>")
                .append("<div>" + "<span style='color:#15558d;font-size:15px;font-weight:600;'>" + item.label + "</span> </div>")
                .appendTo(ul);
        };
    });
}
("use strict");
function purchase_calculation(sl) {

    var gr_tot = 0;
    var unit_qty = $("#Qty_" + sl).val();
    var vendor_rate = $("#Price_" + sl).val();
    var total_price = unit_qty * vendor_rate;
    var tax = $("#Vat_" + sl).val();
    $("#TotalPurchasePrice_" + sl).val(total_price.toFixed(2));
    tax = (tax != undefined && tax != null) ? parseFloat(tax) : 0;
    var txclass = "total_tax_" + sl;
    $("#" + txclass).val(tax.toFixed(6));
    $("#all_tax_" + sl).val(tax.toFixed(6));

    //var j = 0;
    //$(".total_tax").each(function () {
    //    isNaN(this.value) || 0 == this.value.length || (j += parseFloat(this.value));
    //});

    //$("#TotalVat").val(j.toFixed(2, 2));

    $(".total_price").each(function () {
        isNaN(this.value) || 0 == this.value.length || (gr_tot += parseFloat(this.value));
    });
    $("#sub_total").val(gr_tot.toFixed(2, 2));
    $("#grandTotal").val(gr_tot.toFixed(2, 2));
}
("use strict");
$(".datetimepicker").on("dp.change", function (e) {
    debugger;
    var sl = e.target.id.split("_")[1];
    var purdates = $("#ReceivedDate").val();
    var expiredate = $("#ExpiryDate_" + sl).val();
    var purchasedate = new Date(purdates);
    var expirydate = new Date(expiredate);
    if (expirydate <= purchasedate) {
        var htmlstr = "<div class='alert alert-info alert-dismissible fade show' role='alert'> <strong>Message!</strong>  <a href='#' class='alert-link'></a> <span id='statusmsg'>Expiry Date Should Be Greater than Received Date.</span> <button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button></div > ";
        $('.purmsg').removeAttr('hidden');
        $(".purmsg").html(htmlstr);
        document.getElementById("ExpiryDate_" + sl).value = "";
        return false;
    }
    else {
        $('.purmsg').attr('hidden', true);
    }
    return true;
});
("use strict");
function disoucnt_calculation() {
    debugger;
    var gr_tot = 0;
    var total = parseFloat($("#sub_total").val());
    $(".total_price").each(function () {
        isNaN(this.value) || 0 == this.value.length || (gr_tot += parseFloat(this.value));
    });
    var vat = 0;// $("#TotalVat").val();
    if (vat > 0) {
        var vat = vat;
    } else {
        vat = 0;
    }
    var t = $("#Discount").val();
    if (t > total) {
        var htmlstr = "<div class='alert alert-warning alert-dismissible fade show' role='alert'> <strong>Message!</strong>  <a href='#' class='alert-link'></a> <span id='statusmsg'>Discount Can not Greater than Total Amount.</span> <button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button></div >";
        $('.purmsg').removeAttr('hidden');
        $(".purmsg").html(htmlstr);
        $("#Discount").val(0);
        var net_total = +gr_tot + +vat;
    } else {
        var net_total = +gr_tot + +vat + -t;
    }
    $("#grandTotal").val(net_total);
    paid_calculation();
}
("use strict");
function paid_calculation() {

    var paid_amount = 0;//$("#paid_amount").val();
    var gr_total = $("#grandTotal").val();
    var net_total = parseFloat(gr_total) - paid_amount;
    $("#due_amount").val(net_total);
    //disoucnt_calculation();
}

$(".valid_number").keypress(function (event) {
    var charCode = event.which ? event.which : event.keyCode;
    if (charCode != 46 && charCode != 45 && charCode > 31 && (charCode < 48 || charCode > 57)) return false;
    return true;
});