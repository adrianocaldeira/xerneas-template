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
    form: function (parameters) {
        var $form = $("#form-functionality");

        //if (parameters.index !== -1) {
        //    var functionality = window.parent.app.modules.functionalities.data[parameters.index];
        //    $("#Id").val(functionality.id);
        //    $("#Name").val(functionality.name);
        //    $("#Action").val(functionality.action);
        //    $("#Controller").val(functionality.controller);
        //    $("#default").val(functionality.default);
        //    $("#Participant_Gender_Id").val(functionality.gender.id);
        //    $("#Participant_Position").val(functionality.position);
        //    $("#Participant_BirthDate").val(functionality.birthDate);
        //    $("#Participant_MobilePhone").val(functionality.mobilePhone);
        //}

        //$.each(window.parent.app.tickets.participants.data, function (i) {
        //    $form.append($("<input />").attr({
        //        "type": "hidden",
        //        "name": "Cpfs[" + i + "].Cpf",
        //        "value": this.cpf
        //    })).append($("<input />").attr({
        //        "type": "hidden",
        //        "name": "Cpfs[" + i + "].Index",
        //        "value": i
        //    }));
        //});

        //$("#same-informations").on("click", function () {
        //    if ($(this).is(":checked")) {
        //        var parent = window.parent;
        //        $("#Participant_Name").val(parent.$("#Ticket_ResponsibleName").val());
        //        $("#Participant_Email").val(parent.$("#Ticket_ResponsibleEmail").val());
        //        $("#Participant_MobilePhone").val(parent.$("#Ticket_Client_MobilePhone").val());
        //    }

        //    $("#Participant_Name").focus();
        //});

        //$.thunder.ajaxForm("#form-ticket-participant", {
        //    success: function (result) {
        //        window.parent.app.tickets.participants.save(result.data);
        //    },
        //    message: {
        //        plugin: "alert",
        //        alert: {
        //            title: "Aviso",
        //            width: 300
        //        }
        //    }
        //});

        //$("Participant_#Name").focus();
    }
};
