$(document).ready(function (e) {
    var t = e(".main-wrapper"),
        s = e(".page-wrapper"),
        i = e(".slimscroll"),
        l = e(".sidebar-overlay");

    function o(t) {
        t.length && (t.toggleClass("opened"), l.toggleClass("opened"), e("html").toggleClass("menu-opened"), l.attr("data-reff", "#" + t[0].id))
    }
    if (e("#sidebar-menu a").on("click", function (t) {
        e(this).parent().hasClass("submenu") && t.preventDefault(), e(this).hasClass("subdrop") ? e(this).hasClass("subdrop") && (e(this).removeClass("subdrop"), e(this).next("ul").slideUp(350)) : (e("ul", e(this).parents("ul:first")).slideUp(350), e("a", e(this).parents("ul:first")).removeClass("subdrop"), e(this).next("ul").slideDown(350), e(this).addClass("subdrop"))
    }), e("#sidebar-menu ul li.submenu a.active").parents("li:last").children("a:first").addClass("active").trigger("click"), e(document).on("click", "#mobile_btn", function () {
        return o(e(e(this).attr("href"))), t.toggleClass("slide-nav"), e("#chat_sidebar").removeClass("opened"), !1
    }), e(document).on("click", "#task_chat", function () {
        var t = e(e(this).attr("href"));
        return console.log(t), o(t), !1
    }), l.on("click", function () {
        var s = e(e(this).attr("data-reff"));
        return s.length && (s.removeClass("opened"), e("html").removeClass("menu-opened"), e(this).removeClass("opened"), t.removeClass("slide-nav")), !1
    }), e(".select").length > 0 && e(".select").select2({
        minimumResultsForSearch: -1,
        width: "100%"
    }), e(".floating").length > 0 && e(".floating").on("focus blur", function (t) {
        e(this).parents(".form-focus").toggleClass("focused", "focus" === t.type || this.value.length > 0)
    }).trigger("blur"), e("#msg_list").length > 0) {
        e("#msg_list").slimscroll({
            height: "100%",
            color: "#878787",
            disableFadeOut: !0,
            borderRadius: 0,
            size: "4px",
            alwaysVisible: !1,
            touchScrollStep: 100
        });
        var n = e(window).height() - 124;
        e("#msg_list").height(n), e(".msg-sidebar .slimScrollDiv").height(n), e(window).resize(function () {
            var t = e(window).height() - 124;
            e("#msg_list").height(t), e(".msg-sidebar .slimScrollDiv").height(t)
        })
    }
    if (i.length > 0) {
        i.slimScroll({
            height: "auto",
            width: "100%",
            position: "right",
            size: "7px",
            color: "#ccc",
            wheelStep: 10,
            touchScrollStep: 100
        });
        var a = e(window).height() - 60;
        i.height(a), e(".sidebar .slimScrollDiv").height(a), e(window).resize(function () {
            var t = e(window).height() - 60;
            i.height(t), e(".sidebar .slimScrollDiv").height(t)
        })
    }
    var r = e(window).height();
    if (s.css("min-height", r), e(window).resize(function () {
        var t = e(window).height();
        s.css("min-height", t)
    }), e(".datetimepicker").length > 0 && e(".datetimepicker").datetimepicker({
        format: "DD/MM/YYYY"
    }), e('[data-toggle="tooltip"]').length > 0 && e('[data-toggle="tooltip"]').tooltip(), e(document).on("click", "#open_msg_box", function () {
        return t.toggleClass("open-msg-box"), !1
    }), e("#lightgallery").length > 0 && e("#lightgallery").lightGallery({
        thumbnail: !0,
        selector: "a"
    }), e("#incoming_call").length > 0 && e("#incoming_call").modal("show"), e(".summernote").length > 0 && e(".summernote").summernote({
        height: 200,
        minHeight: null,
        maxHeight: null,
        focus: !1
    }), e(document).on("click", "#check_all", function () {
        return e(".checkmail").click(), !1
    }), e(".checkmail").length > 0 && e(".checkmail").each(function () {
        e(this).on("click", function () {
            e(this).closest("tr").hasClass("checked") ? e(this).closest("tr").removeClass("checked") : e(this).closest("tr").addClass("checked")
        })
    }), e(document).on("click", ".mail-important", function () {
        e(this).find("i.fa").toggleClass("fa-star").toggleClass("fa-star-o")
    }), e("#drop-zone").length > 0) {
        var c = document.getElementById("drop-zone"),
            d = document.getElementById("js-upload-form"),
            h = function (e) {
                console.log(e)
            };
        d.addEventListener("submit", function (e) {
            var t = document.getElementById("js-upload-files").files;
            e.preventDefault(), h(t)
        }), c.ondrop = function (e) {
            e.preventDefault(), this.className = "upload-drop-zone", h(e.dataTransfer.files)
        }, c.ondragover = function () {
            return this.className = "upload-drop-zone drop", !1
        }, c.ondragleave = function () {
            return this.className = "upload-drop-zone", !1
        }
    }
    screen.width >= 992 && (e(document).on("click", "#toggle_btn", function () {
        return e("body").hasClass("mini-sidebar") ? (e("body").removeClass("mini-sidebar"), e(".subdrop + ul").slideDown()) : (e("body").addClass("mini-sidebar"), e(".subdrop + ul").slideUp()), !1
    }), e(document).on("mouseover", function (t) {
        if (t.stopPropagation(), e("body").hasClass("mini-sidebar") && e("#toggle_btn").is(":visible")) return e(t.target).closest(".sidebar").length ? (e("body").addClass("expand-menu"), e(".subdrop + ul").slideDown()) : (e("body").removeClass("expand-menu"), e(".subdrop + ul").slideUp()), !1
    }))
});