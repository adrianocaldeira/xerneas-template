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
    init: function () {
        $("[data-module]").each(function () {
            var $this = $(this);
            var module = $this.data("module");
            var action = $this.data("action");
            var parameters = window[$this.data("parameters") || {}];
            
            if (app[module] && app[module][action]) app[module][action].call($this, parameters);
        });

        $("[data-control=\"form\"]").each(function() {
            var $form = $(this);
            var $message = $form.data("message-selector") === undefined ? null : $form.data("message-selector");

            $.thunder.ajaxForm($form, {
                message: {
                    selector: $message
                },
                success: function (result) {
                    $.thunder.alert(result.data, {
                        type: "success",
                        onOk: function () {
                            window.location.href = $form.data("redirect-on-success");
                        }
                    });
                }
            });
        });

        $(".grid-bind").each(function () {
            var $grid = $(this);
            var options = {
                form: $grid.data("form") || "#filter",
                load: true,
                fieldPrefix: $grid.data("field-prefix") || "Filter",
                pageSize: $grid.data("page-size") || 25,
                loadOnDelete: true,
                successOnDelete: $grid.data("success-on-delete") || undefined
            };

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
            if (response.getResponseHeader('Unauthorized-Url')) {
                $.thunder.alert("Sua sessão expirou e você será redirecionado para tela de autenticação do sistema.", {
                    onOk: function () {
                        window.location.href = response.getResponseHeader("Unauthorized-Url");
                    }
                });
            }
        });

        $.thunder.alert.defaultOptions.title = "Aviso";

        $.thunder.confirm.defaultOptions.title = "Confirmação";
        $.thunder.confirm.defaultOptions.button.yes.label = "Sim";
        $.thunder.confirm.defaultOptions.button.yes.className = "btn btn-sm green";
        $.thunder.confirm.defaultOptions.button.no.label = "Não";
        $.thunder.confirm.defaultOptions.button.no.className = "btn btn-sm red";
    }
};

$(function() {
    app.settings.init();
});