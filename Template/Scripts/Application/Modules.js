app.modules.form = function (options) {
    app.modules.functionalities.data = options.functionalities;
    app.modules.functionalities.init();
};

app.modules.functionalities = {
    data: [],
    init: function () {
        $("#panel-functionalities").on("click", "a.edit-functionality, #add-functionality", function (e) {
            e.preventDefault();

            var $this = $(this);
            var url = $this.attr("href") + "?Index=" + $this.data("index");
            
            $.thunder.modal(url, {
                width: 800,
                height: 400
            });
        });

        $("#panel-functionalities").on("click", "a.delete-functionality", function (e) {
            var $this = $(this);
            e.preventDefault();
            $.thunder.confirm("Deseja realmente excluir esse registro.", {
                onYes: function () {
                    app.modules.functionalities.remove($this.data("index"));
                }
            });
        });

        this.list();
    },
    reset: function () {
        this.data = [];
        this.list();
    },
    remove: function (index) {
        this.data.splice(index, 1);
        this.list();
    },
    save: function (functionality) {
        $.thunder.closeModal();

        if (functionality.index === -1) {
            functionality.index = this.data.length;
            this.data.push(functionality);
        } else {
            this.data[functionality.index] = functionality;
        }

        this.list();
    },
    list: function () {
        var $empty = $("#panel-functionalities-empty");
        var $data = $("#panel-functionalities-data");

        if (this.data.length > 0) {
            var template = Handlebars.compile($("#panel-functionalities-template").html());

            $("tbody", $data).html(template(app.modules.functionalities));

            $data.show();
            $empty.hide();
        } else {
            $data.hide();
            $empty.show();
        }
    },
    toParameters: function () {
        var functionalities = [];

        $.each(this.data, function (i) {
            var functionalities = {};

            functionalities["Functionalities[" + i + "].Name"] = this.name;
            functionalities["Functionalities[" + i + "].Action"] = this.action;
            functionalities["Functionalities[" + i + "].Controller"] = this.controller;
            functionalities["Functionalities[" + i + "].Default"] = this.default;
            functionalities["Functionalities[" + i + "].Description"] = this.description;
            functionalities["Functionalities[" + i + "].HttpMethod"] = this.httpMethod;
            functionalities["Functionalities[" + i + "].Id"] = this.id;

            functionalities.push(functionalities);
        });

        return functionalities;
    },
    form: function (index) {
        if (index !== -1) {
            var functionality = window.parent.app.modules.functionalities.data[index];

            $("#Functionality_Id").val(functionality.id);
            $("#Functionality_Name").val(functionality.name);
            $("#Functionality_Description").val(functionality.description);
            $("#Functionality_Controller").val(functionality.controller);
            $("#Functionality_Action").val(functionality.action);
            $("#Functionality_HttpMethod").val(functionality.httpMethod);

            $("#SelectedHttpMethod").val($("#Functionality_HttpMethod").val().split(","));

            if (functionality.default) $("#Functionality_Default").attr("checked", "checked");
        }

        window.beforeOnSave = function() {
            var data = [];
            var $selectedHttpMethod = $("#SelectedHttpMethod");

            if ($selectedHttpMethod.val()) {
                $("#Functionality_HttpMethod").val($selectedHttpMethod.val().join(","));
            }

            return data;
        };

        window.successOnSave = function(result) {
            window.parent.app.modules.functionalities.save(result.data);
        };
    }
};
