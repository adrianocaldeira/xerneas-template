app.settings = {
    root: "",
    getMessageType: function (type) {
        if (type === 0) {
            return "success";
        } else if (type === 1) {
            return "info";
        } else if (type === 2) {
            return "warning";
        } else if (type === 3) {
            return "danger";
        } else {
            return "";
        }
    },
    getViewPort: function() {
        var e = window,
            a = "inner";
        if (!("innerWidth" in window)) {
            a = "client";
            e = document.documentElement || document.body;
        }

        return {
            width: e[a + "Width"],
            height: e[a + "Height"]
        };
    },
    init: function () {
        $("[data-module]").each(function () {
            var $this = $(this);
            var module = $this.data("module");
            var action = $this.data("action");
            var parameters = window[$this.data("parameters") || {}] || $this.data("parameters");
            var handler = null;

            if (app[module] && app[module][action]) {
                handler = app[module][action];
            } else {
                var parts = module.split(".");

                if (parts.length === 2 && app[parts[0]][parts[1]][action]) {
                    handler = app[parts[0]][parts[1]][action];
                }
            }

            if (handler) {
                handler.call({
                    container: $this,
                    module: module,
                    action: action,
                    addEvent: function (event, method) {
                        window[event] = method;
                        return this;
                    }
                }, parameters);
            }
        });

        $("[data-control=\"select-multiple\"]").each(function () {
            var $this = $(this);
            var bind = function() {
                $this.addClass("select2")
                    .select2({
                        allowClear: true,
                        theme: "bootstrap"
                    });
            };
            if ($("[data-window=\"modal\"]").size() > 0) {
                window.setTimeout(function() {
                    bind();
                }, 0);
            } else {
                bind();
            }

        });

        $("[data-control=\"form\"]").each(function() {
            var $form = $(this);
            var options = {
                message: $form.data("message-selector") === undefined ? undefined : $form.data("message-selector"),
                showMessage: $form.data("show-message") === undefined ? true : $form.data("show-message"),
                redirect: $form.data("redirect") === undefined ? false : $form.data("redirect"),
                before: $form.data("before") === undefined ? undefined : $form.data("before"),
                success: $form.data("success") === undefined ? undefined : $form.data("success"),
                warning: $form.data("warning") === undefined ? undefined : $form.data("warning"),
                messageType: $form.data("message-type") === undefined ? "alert" : $form.data("message-type")
            };

            if (options.success !== undefined) {
                options.success = window[options.success];
            }

            if (options.before !== undefined) {
                options.before = window[options.before];
            }

            $.thunder.ajaxForm($form, {
                message: {
                    selector: options.message,
                    plugin: options.messageType
                },
                before: options.before,
                success: function (result) {
                    if (options.success !== undefined) {
                        options.success.call($form, result);
                    }

                    if (options.redirect) {
                        if (options.showMessage) {
                            $.thunder.alert(result.data.message, {
                                type: "success",
                                onOk: function () {
                                    window.location.href = result.data.url;
                                }
                            });
                        } else {
                            window.location.href = result.data.url;
                        }
                    } else {
                        if (options.showMessage) {
                            $.thunder.alert(result.data.message, { type: "success" });
                        }
                    }
                }
            });
        });

        $("[data-control=\"nestable\"]").each(function () {
            var $nestable = $(this);

            $nestable.nestable({
                maxDepth: $nestable.data("max-depth")
            }).on("change", function () {
                var json = JSON.stringify($nestable.nestable("serialize"));

                if ($nestable.data("json") !== json) {
                    $.ajax({
                        url: $nestable.data("organize-url"),
                        type: "post",
                        data: {
                            json: json
                        },
                        success: function (result) {
                            $nestable.data("json", json);

                            if (window[$nestable.data("change")]) {
                                window[$nestable.data("change")].call($nestable, result);
                            }
                        }
                    });
                }
            });;

            $nestable.on("click", ".btn-delete-row", function (e) {
                var $button = $(this);

                e.preventDefault();

                $.thunder.confirm("Deseja realmente excluir esse registro?", {
                    onYes: function () {
                        $.ajax({
                            url: $button.is("a") ? $button.attr("href") : $button.data("url"),
                            type: "delete",
                            headers: {
                                "Url-Parent": window.location.pathname
                            },
                            success: function (result) {
                                if (result.type === 0) {
                                    if (window[$nestable.data("delete")]) {
                                        window[$nestable.data("delete")].call($button, $nestable);
                                    }
                                } else {
                                    $.thunder.alert(result.data || result.messages, {
                                        type: app.settings.getMessageType(result.type)
                                    });
                                }
                            }
                        });
                    }
                });
            });
        });

        $("[data-control=\"grid\"]").each(function () {
            var $grid = $(this);
            var options = {
                form: $grid.data("form") || "#filter",
                load: true,
                fieldPrefix: $grid.data("field-prefix"),
                pageSize: $grid.data("page-size") || 25,
                loadOnDelete: true,
                successOnDelete: $grid.data("success-on-delete") || undefined
            };

            if (options.fieldPrefix === undefined) {
                options.fieldPrefix = "Filter";
            }

            if ($grid.data("load") !== undefined) {
                options.load = $grid.data("load") === "true";
            }

            if ($grid.data("load-on-delete") !== undefined) {
                options.loadOnDelete = $grid.data("load-on-delete") === "true";
            }

            if (options.successOnDelete !== undefined && window[options.successOnDelete] !== undefined) {
                options.successOnDelete = window[options.successOnDelete];
            }

            $.thunder.grid($grid, options);

            $grid.on("click", ".btn-delete-row", function(e) {
                var $button = $(this);

                e.preventDefault();

                $.thunder.confirm("Deseja realmente excluir esse registro?", {
                    onYes: function() {
                        $.ajax({
                            url: $button.is("a") ? $button.attr("href") : $button.data("url"),
                            type: "delete",
                            headers: {
                                "Url-Parent": window.location.pathname
                            },
                            success: function (result) {
                                if (result.type === 0) {
                                    if (options.loadOnDelete) {
                                        $.thunder.grid($grid, "reload");
                                    }

                                    if ($.isFunction(options.successOnDelete)) {
                                        options.successOnDelete.call($button, $grid);
                                    }
                                } else {
                                    $.thunder.alert(result.data || result.messages, {
                                        type: app.settings.getMessageType(result.type)
                                    });
                                }
                            }
                        });
                    }
                });
            });
        });
       
        $.ajaxSetup({
            statusCode: {
                403: function () {
                    $.thunder.alert("Você não possui permissão de acesso à esta funcionalidade!", {
                        type: "warning"
                    });
                }
            }
        });

        $(document).ajaxComplete(function (e, response) {
            if (response.getResponseHeader("Unauthorized-Url")) {
                $.thunder.alert("Sua sessão expirou e você será redirecionado para tela de autenticação do sistema.", {
                    onOk: function () {
                        window.location.href = response.getResponseHeader("Unauthorized-Url");
                    }
                });
            }
        });

        $(".make-icheck").iCheck({
            checkboxClass: "icheckbox_square-blue",
            radioClass: "iradio_square-blue"
        });
        
        $(".make-switch").bootstrapSwitch();

        $("body").on("click", ".panel > .panel-heading .fullscreen", function (e) {
            e.preventDefault();
            var $this = $(this);
            var $panel = $this.closest(".panel");
            var $body = $("body");

            if ($panel.hasClass("panel-fullscreen")) {
                $this.removeClass("on").attr("title", "Expandir");;
                $(".fa", $this).removeClass("fa-compress").addClass("fa-expand");
                $panel.removeClass("panel-fullscreen");
                $body.removeClass("page-panel-fullscreen");
                $panel.children(".panel-body").css("height", "auto");
            } else {
                var height = app.settings.getViewPort().height -
                    $panel.children(".panel-heading").outerHeight() -
                    parseInt($panel.children(".panel-body").css("padding-top")) -
                    parseInt($panel.children(".panel-body").css("padding-bottom"));

                $this.addClass("on").attr("title", "Recolher");
                $panel.addClass("panel-fullscreen");
                $body.addClass("page-panel-fullscreen");
                $panel.children(".panel-body").css("height", height);
                $(".fa", $this).removeClass("fa-expand").addClass("fa-compress");
            }
        });

        $.thunder.alert.defaultOptions.title = "Aviso Importante";
        $.thunder.alert.defaultOptions.type = "warning";
        $.thunder.alert.defaultOptions.button.className = "btn btn-default";

        $.thunder.confirm.defaultOptions.title = "Confirmação";
        $.thunder.confirm.defaultOptions.button.yes.label = "Sim";
        $.thunder.confirm.defaultOptions.button.yes.className = "btn btn-success";
        $.thunder.confirm.defaultOptions.button.no.label = "Não";
        $.thunder.confirm.defaultOptions.button.no.className = "btn btn-danger";
    }
};

$(function() {
    app.settings.init();
});