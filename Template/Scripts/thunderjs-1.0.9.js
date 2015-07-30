if (typeof jQuery === "undefined") {
    throw new Error("thunderJs requires jQuery");
}

if (typeof jQuery.ui === "undefined") {
    throw new Error("thunderJs requires jQuery.ui");
}

(function ($, window) {
    if (!$.thunder) {
        $.thunder = {};
    };

    $.thunder.version = "1.0.9";

    $.thunder.statusCode = {
        400: "Bad request",
        401: "Unauthorized",
        403: "Forbidden",
        404: "Page not found",
        405: "Method not allowed",
        407: "Proxy authentication required",
        408: "Request timeout",
        500: "Internal server error",
        501: "Not implemented",
        502: "Bad gateway",
        503: "Service unavailable"
    };

    $.thunder.activeModal = $({});

    $.thunder.utility = function () {
        return {
            queryString: function (name) {
                return this.querStringFormUrl(window.location.search, name);
            },
            querStringFormUrl: function (url, name) {
                name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
                var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"), results = regex.exec(url);
                return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
            },
            statusCode: function (options) {
                var defaults = $.extend(true, {}, {
                    message: {
                        plugin: "message", //message, alert
                        selector: null,
                        showClose: false
                    }
                }, options);

                var show = function (code) {
                    var message = $.thunder.statusCode[code];

                    if (defaults.message.plugin === "message") {
                        if ($(defaults.message.selector).length > 0) {
                            $.thunder.message(defaults.message.selector, message,
                                $.extend(true, { type: "danger" }, defaults.message)
                            );
                        } else {
                            $.thunder.alert(message, { type: "danger" });
                        }
                    } else {
                        $.thunder.alert(message, { type: "danger" });
                    }
                };

                return {
                    400: function () {
                        show(400);
                    },
                    401: function () {
                        show(401);
                    },
                    403: function () {
                        show(403);
                    },
                    404: function () {
                        show(404);
                    },
                    405: function () {
                        show(405);
                    },
                    407: function () {
                        show(407);
                    },
                    408: function () {
                        show(408);
                    },
                    500: function () {
                        show(500);
                    },
                    501: function () {
                        show(501);
                    },
                    502: function () {
                        show(502);
                    },
                    503: function () {
                        show(503);
                    }
                };
            }
        };
    };

    $.thunder.closeModal = function () {
        if ($.thunder.activeModal !== "undefined" && $.thunder.activeModal.size() > 0) {
            $.thunder.activeModal.dialog("close");
        }
    };

    $.thunder.appendMessage = function(selector, message) {
        return $(selector).each(function () {
            var getMessages = function (m) {
                var array = [];

                if ($.isArray(m) && m.length > 0) {
                    if (m[0].key !== undefined && m[0].value !== undefined) {
                        $.each(m, function () {
                            if ($.isArray(this.value)) {
                                $.each(this.value, function () {
                                    array.push(this);
                                });
                            } else {
                                array.push(this.value);
                            }
                        });
                    } else {
                        $.each(m, function () {
                            array.push(this);
                        });
                    }
                } else {
                    if ($.isPlainObject(m) && m.key !== undefined && m.value !== undefined) {
                        if ($.isArray(m.value)) {
                            $.each(m.value, function () {
                                array.push(this);
                            });
                        } else {
                            array.push(m.value);
                        }
                    } else {
                        array.push(m);
                    }
                }

                return array;
            };

            var messages = getMessages(message);

            if (messages.length > 1) {
                var ul = $('<ul></ul>');
                $.each(messages, function () {
                    ul.append($("<li></li>").html(this));
                });
                $(this).append(ul);
            } else {
                $(this).append(messages[0]);
            }
        });
    };

    $.thunder.disableElement = function (selector) {
        return $(selector).each(function () {
            $(this).addClass("disabled").attr("disabled", "disabled");
        });
    };

    $.thunder.enableElement = function (selector) {
        return $(selector).each(function () {
            $(this).removeClass("disabled").removeAttr("disabled");
        });
    };
    
    $.thunder.alert = function (message, options) {
        var defaults = $.extend(true, {
            onOk: function () {
            }
        }, $.thunder.alert.defaultOptions, options);

        var $this = $("." + defaults.className);
        var $dialog = $("<div class=\"modal-dialog\"></div>");
        var $content = $("<div class=\"modal-content\"></div>");
        var $header = $("<div class=\"modal-header\"></div>");
        var $body = $("<div class=\"modal-body\"></div>");
        var $footer = $("<div class=\"modal-footer\"></div>");
        var $ok = $("<button type=\"button\"></button>");

        if ($this.size() === 0) {
            $this = $("<div></div>").addClass(defaults.className);
            $("body").append($this);
        }
        
        if (defaults.type !== "none") {
            $this.removeClass(defaults.className + "-none")
                .removeClass(defaults.className + "-success")
                .removeClass(defaults.className + "-info")
                .removeClass(defaults.className + "-warning")
                .removeClass(defaults.className + "-danger");
            $this.addClass(defaults.className + "-" + defaults.type);
        }
        
        if ($.trim(defaults.title) !== "") {
            $content.append($header.append($("<h4 class=\"modal-title\"></h4>").html(defaults.title)));
        }

        $ok.addClass(defaults.button.className).html(defaults.button.label);

        $ok.on("click", function () {
            if ($.isFunction(defaults.onOk)) {
                defaults.onOk.call($this);
            }
            $this.modal("hide");
        });
        
        $.thunder.appendMessage($body, message);

        $content.append($body)
            .append($footer.append($ok));

        $dialog.css("width", ($(window).width() > defaults.width ? defaults.width : $(window).width()-20))
            .append($content);

        $this.empty().addClass("modal")
            .addClass(defaults.effect)
            .append($dialog);

        $this.on("shown.bs.modal", function () {
            $ok.focus();
        });

        $this.modal("show");
    };

    $.thunder.alert.defaultOptions = {
        title: "Alert",
        className: "thunder-alert",
        effect: "fade",
        type: "none", //none, success, info, warning, danger
        button: {
            label: "OK",
            className: "btn btn-default btn-sm"
        },
        width: 600
    };

    $.thunder.confirm = function(message, options) {
        var defaults = $.extend(true, {
            onYes: function() {
            },
            onNo: function () {
            }
        }, $.thunder.confirm.defaultOptions, options);
        
        var $this = $("." + defaults.className);
        var $dialog = $("<div class=\"modal-dialog\"></div>");
        var $content = $("<div class=\"modal-content\"></div>");
        var $header = $("<div class=\"modal-header\"></div>");
        var $body = $("<div class=\"modal-body\"></div>").html(message);
        var $footer = $("<div class=\"modal-footer\"></div>");
        var $yes = $("<button type=\"button\"></button>");
        var $no = $("<button type=\"button\"></button>");
        
        if ($this.size() === 0) {
            $this = $("<div></div>").addClass(defaults.className);
            $("body").append($this);
        }

        if($.trim(defaults.title) !== "") {
            $content.append($header.append($("<h4 class=\"modal-title\"></h4>").html(defaults.title)));
        }

        $yes.addClass(defaults.button.yes.className)
            .html(defaults.button.yes.label)
            .on("click", function() {
                if ($.isFunction(defaults.onYes)) {
                    defaults.onYes.call($this);
                }
                $this.modal("hide");
            });

        $no.addClass(defaults.button.no.className)
            .html(defaults.button.no.label)
            .on("click", function() {
                if ($.isFunction(defaults.onNo)) {
                    defaults.onNo.call($this);
                }
                $this.modal("hide");
            });
        
        $content.append($body)
            .append($footer.append($yes).append($no));

        $dialog.css("width", ($(window).width() > defaults.width ? defaults.width : $(window).width() - 20))
            .append($content);

        $this.empty().addClass("modal")
            .addClass(defaults.effect)
            .append($dialog);

        $this.modal("show");
    };

    $.thunder.confirm.defaultOptions = {
        title: "Confirm",
        className: "thunder-confirm",
        effect: "fade",
        button: {
            yes: {
                label: "Yes",
                className: "btn btn-success btn-sm"
            },
            no: {
                label: "No",
                className: "btn btn-danger btn-sm"
            }
        },
        width: 600
    };

    $.thunder.modal = function (url, options) {
        var defaults = $.extend(true, {
            data: {},
            open: function () { },
            beforeClose: function () { },
            close: function () { }
        }, $.thunder.modal.defaultOptions, options);

        if (url === "undefined" || $.trim(url) === "") {
            throw new Error("Unknown url");
        }

        var getUrl = function () {
            var delimiter = function () {
                return (url.indexOf("?") === -1 ? "?" : "&");
            };

            if (jQuery.isPlainObject(defaults.data) && !jQuery.isEmptyObject(defaults.data)) {
                url += delimiter() + $.param(defaults.data);
            } else {
                if (typeof defaults.data === "string") {
                    url += delimiter() + defaults.data;
                }
            }

            if (defaults.forceReload) {
                url += delimiter() + "forceReload=" + parseInt(Math.random() * 9999999999);
            }

            url += delimiter() + "forceFocusOnLoadInModal=" + defaults.forceFocusOnLoadInModal;

            return url;
        };

        var loading = function () {
            var $loading = $("<div></div>").addClass("thunder-modal-loading");

            for (var i = 1; i <= 8; i++) {
                $loading.append($("<div></div>")
                    .addClass("thunder-modal-loading-item")
                    .addClass("thunder-modal-loading-item-" + i)
                );
            }

            return $loading;
        };

        var $this = $("." + defaults.className + "-iframe");
        var $iframe = $("<iframe></iframe>")
            .attr("frameborder", defaults.iframe.frameBorder)
            .attr("marginheight", defaults.iframe.marginHeight)
            .attr("marginwidth", defaults.iframe.marginWidth)
            .attr("scrolling", defaults.iframe.scrolling)
            .attr("src", getUrl())
            .hide();

        if ($this.length === 0) {
            $this = $("<div></div>").addClass(defaults.className + "-iframe");
            $("body").append($this);
        }

        $.thunder.activeModal = $this;

        $this.empty().show().dialog({
            autoOpen: true,
            modal: true,
            resizable: false,
            draggable: false,
            closeOnEscape: defaults.closeOnEscape,
            dialogClass: defaults.className,
            width: ($(window).width() > defaults.width ? defaults.width : $(window).width() - 20),
            height: defaults.height,
            open: function () {
                var $loading = loading();

                $this.prev(".ui-dialog-titlebar").remove();

                $iframe.attr("width", $this.width()).attr("height", $this.height());

                $this.append($loading).append($iframe);

                $iframe.load(function () {
                    $iframe.show();
                    $loading.remove();

                    window.setTimeout(function () {
                        var $contents = $iframe.contents();

                        if ($.isFunction(defaults.open)) {
                            defaults.open.call($contents);
                        }
                    }, 100);
                });
            },
            beforeClose: function () {
                $this.empty();
                if ($.isFunction(defaults.beforeClose)) {
                    defaults.beforeClose.call();
                }
            },
            close: function () {
                $.thunder.activeModal = $({});
                $this.remove();

                if ($.isFunction(defaults.close)) {
                    window.setTimeout(function () {
                        defaults.close.call();
                    }, 0);
                }
            }
        });
    };

    $.thunder.modal.defaultOptions = {
        className: "thunder-modal",
        iframe: {
            frameBorder: 0,
            marginHeight: 0,
            marginWidth: 0,
            scrolling: "auto"
        },
        width: 600,
        height: 400,
        forceReload: true,
        forceFocusOnLoadInModal: true,
        closeOnEscape: false
    };

    $.thunder.openModal = function (selector, options) {
        return $(selector).each(function () {
            var $this = $(this);
            var getUrl = function (element) {
                var url = "about:blank";

                if (element.is("a")) {
                    if (element.attr("href") !== "undefined" && element.attr("href") !== "#" && element.attr("href") !== "") {
                        url = element.attr("href");
                    }
                }

                if (url === "about:blank" && element.data("url") !== "undefined") {
                    url = element.data("url");
                }

                return url;
            };

            $this.on("click", function (e) {
                e.preventDefault();

                $.thunder.modal(getUrl($(this)), $.extend(true, {
                    width: $(this).data("width"),
                    height: $(this).data("height")
                }, options));
            });

            return $this;
        });
    };

    $.thunder.message = function(selector, message, options) {
        var defaults = $.extend(true, {
                show: function(){}
            },
            $.thunder.message.defaultOptions,
            options);
        
        return $(selector).each(function () {
            var $this = $(this)
                .removeClass("alert")
                .removeClass("alert-success")
                .removeClass("alert-info")
                .removeClass("alert-warning")
                .removeClass("alert-danger")
                .addClass("alert")
                .addClass("alert-" + defaults.type)
                .empty()
                .hide()
                .css(defaults.css);

            var closeAction = function () {
                if (defaults.removeAfterClose) {
                    $this.remove();
                } else {
                    $this.hide();
                }
            };

            if (defaults.showClose) {
                var $close = $("<button></button>")
                    .attr("type", "button")
                    .addClass("close")
                    .on("click", closeAction)
                    .html("&times;");
                $this.append($close);
            }

            $.thunder.appendMessage($this, message);

            $this.show(defaults.animate.duration, defaults.animate.easing, function () {
                if ($.isFunction(defaults.show)) {
                    defaults.show.call($this);
                }
            });

            if (defaults.autoClose.enable) {
                window.setTimeout(closeAction, defaults.autoClose.timer);
            }

            return $this;
        });
    };

    $.thunder.message.defaultOptions = {
        showClose: true,
        type: "success", //success, info, warning, danger
        removeAfterClose: false,
        autoClose: {
            enable: false,
            timer: 5000
        },
        css: {
            marginTop: 0
        },
        animate: {
            duration: 400,
            easing: "linear"
        }
    };

    $.thunder.ajaxForm = function (selector, options) {
        var defaults = $.extend(true, {
            message: {
                selector: null
            },
            async: true,
            autoResolveResult: true,
            before: function () { },
            success: function () { },
            complete: function () { },
            validate: function () { return true; }
        }, $.thunder.ajaxForm.defaultOptions, options);
        
        return $(selector).each(function() {
            var $this = $(this).addClass(defaults.className);
            var $message = $(defaults.message.selector);
            var $loading = $(defaults.loading);

            if ($this.is("form") && ($this.attr("action") === "undefined" || $.trim($this.attr("action")) === "")) {
                throw new Error("Action attribute is empty in form");
            }

            if (!$this.is("form") && ($this.data("action") === "undefined" || $.trim($this.data("action")) === "")) {
                throw new Error("Action attribute is empty in element");
            }

            if ($message.length === 0 && !$this.prev().is("." + defaults.className + "-message")) {
                $this.before($("<div></div>").addClass(defaults.className + "-message"));
                $message = $this.prev();
            }

            if ($loading.length === 0 && $("." + defaults.className + "-loading", $this).length === 0) {
                $loading = $("<div></div>").addClass(defaults.className + "-loading");

                for (var i = 1; i <= 8; i++) {
                    $loading.append($("<div></div>")
                        .addClass(defaults.className + "-loading-item")
                        .addClass(defaults.className + "-loading-item-" + i)
                    );
                }

                $this.prepend($loading);

                $loading = $("." + defaults.className + "-loading", $this);
            }
            
            var messages = function (m, o) {
                var settings = $.extend({
                    type: 3
                }, o);

                var getMessageType = function () {
                    if (settings.type === 0) {
                        return "success";
                    } else if (settings.type === 1) {
                        return "info";
                    } else if (settings.type === 2) {
                        return "warning";
                    } else if (settings.type === 3) {
                        return "danger";
                    } else {
                        return "";
                    }
                };

                if (defaults.message.plugin === "message") {
                    $.thunder.message($message, m,
                        $.extend(true,
                        {
                            type: getMessageType(),
                            show: function() {
                                if ($.isFunction(defaults.message.show)) {
                                    defaults.message.show.call();
                                }
                            }
                        }, defaults.message)
                    );
                } else {
                    $.thunder.alert(m, { type: getMessageType() });
                }
            };
            var getFields = function() {
                var fields = $("input,select,textarea", $this);

                if (defaults.ignore !== "undefined" && defaults.ignore !== "") {
                    fields = $("input,select,textarea", $this).filter(defaults.ignore);
                }

                return fields;
            };
            var serialize = function (form, extraData) {
                var $form = $(form);
                var fields = getFields();
                var parameters = fields.serializeArray();

                if ($.isArray(extraData)) {
                    $.each(extraData, function () {
                        $.each(this, function (k, v) {
                            parameters.push({ name: k, value: v });
                        });
                    });
                } else if ($.isPlainObject(extraData) && !$.isEmptyObject(extraData)) {
                    $.each(extraData, function (k, v) {
                        parameters.push({ name: k, value: v });
                    });
                }

                return $.param(parameters);
            };
            var submit = function(event) {
                var $form = $this;
                var extraData = {};
                var $fields = getFields();

                event.preventDefault();

                $loading.hide();
                $message.hide();

                if ($.isFunction(defaults.before)) {
                    extraData = defaults.before.call($form);
                }

                var dataSerialize = serialize($form, extraData);

                if ($.isFunction(defaults.validate) && !defaults.validate.call($form, dataSerialize)) {
                    return;
                }

                var statusCode = $.thunder.utility().statusCode(
                    $.extend(true, {
                        message: {
                            selector: $message
                        }
                    }, defaults.statusCode)
                );

                $.ajax({
                    statusCode: statusCode,
                    async: defaults.async,
                    url: ($form.is("form") ? $form.attr("action") : $form.data("action")),
                    type: ($form.is("form") ? $form.attr("method") : ($form.data("method") || "post")),
                    data: dataSerialize,
                    headers: {
                        "Url-Parent": window.location.pathname
                    },
                    beforeSend: function () {
                        $loading.show();
                        $.thunder.disableElement($("input,select,textarea,button", $form));
                        $fields.removeClass(defaults.className + "-error");
                    },
                    complete: function () {
                        $.thunder.enableElement($("input,select,textarea,button", $form));
                        $loading.hide();
                        if ($.isFunction(defaults.complete)) {
                            defaults.complete.call($form);
                        }
                    },
                    success: function (result) {
                        window.setTimeout(function () {
                            if (defaults.autoResolveResult) {
                                if ($.isPlainObject(result) && !$.isEmptyObject(result) && result.type !== "undefined") {
                                    if (result.type === 0) {
                                        if ($.isFunction(defaults.success)) {
                                            defaults.success.call($form, result);
                                        }
                                    } else {
                                        var $firstField = null;
                                        $.each(result.messages, function () {
                                            if (this.key !== undefined) {
                                                var $field = $("[name='" + this.key + "'],#" + this.key, $form);

                                                if ($firstField === null) {
                                                    $firstField = $field;
                                                }

                                                $field.addClass(defaults.className + "-error");
                                            }
                                        });
                                        messages(result.messages, { type: result.type });
                                    }
                                } else {
                                    defaults.success.call($form, result);
                                }
                            } else {
                                defaults.success.call($form, result);
                            }
                        }, 0);
                    }
                });
            };
            var $fields = getFields();

            if ($this.is("form")) {
                $this.on("submit", function (e) {
                    submit(e);
                });
            } else {
                var $submitButton = $(defaults.submitButton);
                
                $fields.each(function () {
                    $(this).on("keypress", function(e) {
                        if (e.which === 13) {
                            e.preventDefault();
                            $submitButton.trigger("click");
                        }
                    });
                });

                $submitButton.on("click", function (e) {
                    submit(e);
                });
            }

            $fields.each(function () {
                $(this).on("blur", function () {
                    if (($(this).is("select") && $(this).val() !== "0") || (!$(this).is("select") && $(this).val() !== "")) {
                        $(this).removeClass(defaults.className + "-error");
                    }
                });
            });

            return $this;
        });
    };

    $.thunder.ajaxForm.defaultOptions = {
        className: "thunder-ajax-form",
        ignore: "",
        loading: null,
        submitButton: "button.submit-button, a.submit-button",
        statusCode: {
            message: {
                plugin: "message", //message, alert
                showClose: false
            }
        },
        message: {
            plugin: "message", //message, alert
            showClose: true
        }
    };

    $.thunder.grid = function (selector, options) {
        var defaults = $.extend(true, {
            message: {
                selector: null
            },
            fieldPrefix: "",
            async: true,
            load: true,
            currentPage: 0,
            form: null,
            before: function () { },
            success: function () { },
            complete: function () { },
            validate: function () { return true; }
        }, $.thunder.grid.defaultOptions, options);

        var messages = function (m, o) {
            var settings = $.extend({
                type: 3
            }, o);

            var getMessageType = function () {
                if (settings.type === 0) {
                    return "success";
                } else if (settings.type === 1) {
                    return "info";
                } else if (settings.type === 2) {
                    return "warning";
                } else if (settings.type === 3) {
                    return "danger";
                } else {
                    return "";
                }
            };

            if (defaults.message.plugin === "message") {
                $.thunder.message(this, m,
                    $.extend(true,
                    {
                        type: getMessageType(),
                        show: function () {
                            if ($.isFunction(defaults.message.show)) {
                                defaults.message.show.call();
                            }
                        }
                    }, defaults.message)
                );
            } else {
                $.thunder.alert(m, { type: getMessageType() });
            }
        };
        var serialize = function (extraData) {
            var $form = $(this);
            var fields = $("input,select,textarea", $form);

            if (defaults.ignore !== "undefined" && defaults.ignore !== "") {
                fields = $("input,select,textarea", $form).filter(defaults.ignore);
            }

            var parameters = fields.serializeArray();

            if ($.isArray(extraData)) {
                $.each(extraData, function () {
                    $.each(this, function (k, v) {
                        parameters.push({ name: k, value: v });
                    });
                });
            } else if ($.isPlainObject(extraData) && !$.isEmptyObject(extraData)) {
                $.each(extraData, function (k, v) {
                    parameters.push({ name: k, value: v });
                });
            }

            return $.param(parameters);
        };
        var makeFieldName = function (name) {
            if (defaults.fieldPrefix !== null && defaults.fieldPrefix !== "") {
                return defaults.fieldPrefix + "." + name;
            }
            return name;
        };
        var setCurrentPage = function (currentPage) {
            $("." + defaults.className + "-current-page", this).val(currentPage);
        };
        var load = function () {
            var $grid = $(this);
            var $form = $grid.data("form");
            var $loading = $grid.data("loading");
            var $message = $grid.data("message");
            var $content = $grid.data("content");
            var extraData = {};

            $loading.hide();
            $message.hide();

            if ($.isFunction(defaults.before)) {
                extraData = defaults.before.call($form);
            }

            var dataSerialize = serialize.call($form, extraData);

            if ($.isFunction(defaults.validate) && !defaults.validate.call($form, dataSerialize)) {
                return;
            }

            var statusCode = $.thunder.utility().statusCode(
                $.extend(true, {
                    message: {
                        selector: $message
                    }
                }, defaults.statusCode)
            );
            
            $.ajax({
                statusCode: statusCode,
                async: defaults.async,
                url: $form.attr("action"),
                type: $form.attr("method"),
                data: dataSerialize,
                headers: {
                    "Url-Parent": window.location.pathname
                },
                beforeSend: function() {
                    $loading.show();

                    $.thunder.disableElement($("input,select,textarea,button", $form));
                    $.thunder.disableElement($("a,input,select,textarea,button", $grid));

                    $("input,select,textarea", $form).removeClass(defaults.className + "-error");
                },
                complete: function() {
                    $.thunder.enableElement($("input,select,textarea,button", $form));
                    $.thunder.enableElement($("a,input,select,textarea,button", $grid));

                    $loading.hide();

                    if ($.isFunction(defaults.complete)) {
                        defaults.complete.call($form);
                    }
                },
                success: function(result) {
                    window.setTimeout(function() {
                        if ($.isPlainObject(result) && !$.isEmptyObject(result) && result.type !== "undefined") {
                            if (result.type === 0) {
                                if ($.isFunction(defaults.success)) {
                                    defaults.success.call($form, result);
                                }
                            } else {
                                var $firstField = null;
                                $.each(result.messages, function () {
                                    if (this.key !== undefined) {
                                        var $field = $("[name='" + this.key + "'],#" + this.key, $form);

                                        if ($firstField === null) {
                                            $firstField = $field;
                                        }

                                        $field.addClass(defaults.className + "-error");
                                    }
                                });
                                messages(result.messages, { type: result.type });
                            }
                        } else {
                            $content.html(result);
                            defaults.success.call($form, result);
                        }
                    }, 0);
                }
            });
        };

        if (options === "reload") {
            $(selector).trigger("reload");
            return $(selector);
        }

        return $(selector).each(function() {
            var $grid = $(this);
            var $message = $(defaults.message.selector);
            var $loading = $(defaults.loading);
            var $form = $(defaults.form);
            var $content = $("<div></div>");

            $grid.addClass(defaults.className).css(defaults.css);

            if ($("." + defaults.className + "-content", $grid).length === 0) {
                $content.addClass(defaults.className + "-content").css(defaults.content.css);
            } else {
                $content = $("." + defaults.className + "-content", $grid);
            }

            if ($grid.data("url") === undefined) {
                throw new Error("Data url attribute is empty in grid");
            }

            if ($message.length === 0 && !$grid.prev().is("." + defaults.className + "-message")) {
                $grid.before($("<div></div>").addClass(defaults.className + "-message"));
                $message = $grid.prev();
            }

            if ($form.length === 0 && $("." + defaults.className + "-form", $grid).length === 0) {
                $grid.prepend($("<form></form>").addClass(defaults.className + "-form"));
                $form = $("." + defaults.className + "-form", $grid);
            }

            if ($loading.length === 0 && $("." + defaults.className + "-loading", $grid).length === 0) {
                $loading = $("<div></div>").addClass(defaults.className + "-loading");

                for (var i = 1; i <= 8; i++) {
                    $loading.append($("<div></div>")
                        .addClass(defaults.className + "-loading-item")
                        .addClass(defaults.className + "-loading-item-" + i)
                    );
                }

                $grid.prepend($loading);

                $loading = $("." + defaults.className + "-loading", $grid);
            }

            if ($("." + defaults.className + "-current-page", $form).length === 0) {
                $form.prepend($("<input/>").attr("type", "hidden").attr("name", makeFieldName("CurrentPage"))
                    .val(defaults.currentPage).addClass(defaults.className + "-current-page"));
            }

            if ($("." + defaults.className + "-page-size", $form).length === 0) {
                $form.prepend($("<input/>").attr("type", "hidden").attr("name", makeFieldName("PageSize"))
                    .val(defaults.pageSize).addClass(defaults.className + "-page-size"));
            }
            
            if ($("." + defaults.className + "-content", $grid).length === 0) {
                $grid.append($content);
            }

            $form.attr("action", $grid.data("url"))
                .attr("method", defaults.httpMethod)
                .on("submit", function(event) {
                    event.preventDefault();
                    setCurrentPage.call(this, 0);
                    load.call($grid);
                });

            $grid.bind("reload", function() {
                $form.submit();
            });

            $grid.data("loading", $loading)
                .data("message", $message)
                .data("content", $content)
                .data("form", $form);

            $grid.on("click", "ul.pagination a", function(event) {
                var $link = $(this);

                event.preventDefault();
                
                if (!$link.is(".disabled") && !$link.parents("li:first").is(".disabled")) {
                    setCurrentPage.call($form, $link.data("page"));
                    load.call($grid);
                }
            });

            if (defaults.load) {
                load.call($grid);
            }

            return $grid;
        });
    };

    $.thunder.grid.defaultOptions = {
        className: "thunder-grid",
        ignore: "",
        loading: null,
        httpMethod: "POST",
        statusCode: {
            message: {
                plugin: "message", //message, alert
                showClose: false
            }
        },
        message: {
            plugin: "message", //message, alert
            showClose: true
        },
        pageSize: 20,
        css: {},
        content: {
            css: {}
        }
    }
    
    $.thunder.openModal("[data-thunder-plugin=modal]");
    
    if($("[data-window=modal]").size() > 0) {
        if ($.thunder.utility().queryString("forceFocusOnLoadInModal") === "true") {
            window.setTimeout(function() {
                $("input:visible:not(input[type='hidden'],:disabled),select:visible:not(:disabled),textarea:visible:not(:disabled)").filter(":first").focus();
            }, 500);
        }

        $("button.close").on("click", function () {
            window.parent.$.thunder.closeModal();
        });
    }
    
}(window.jQuery, window));