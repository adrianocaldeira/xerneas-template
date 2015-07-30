app.login.index = function () {
    $.thunder.ajaxForm("#form-login", {
        success: function (result) {
            window.location.href = result.data;
        },
        message: {
            selector: "#message",
            show: function () {
                $("html, body").animate({
                    scrollTop: this.offset().top - 80
                }, 400);
            }
        }
    });
};