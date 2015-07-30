app.settings = {
    root: "",
    init: function () {
        $("[data-module]").each(function () {
            var $this = $(this);
            var module = $this.data("module");
            var action = $this.data("action");
            var parameters = window[$this.data("parameters") || {}];
            
            if (app[module] && app[module][action]) app[module][action].call($this, parameters);
        });

        $(".data-bind").each(function() {
            var $this = $(this);
            var options = {
                form: $this.data("form") || "#filter",
                load: true,
                fieldPrefix: $this.data("field-prefix") || "",
                pageSize: $this.data("page-size") || 25
            };

            if ($this.data("load") !== undefined) {
                options.load = $this.data("load") === "true";
            }

            $.thunder.grid($this, options);
        });
        
        $.ajaxSetup({
            statusCode: {
                401: function () {
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