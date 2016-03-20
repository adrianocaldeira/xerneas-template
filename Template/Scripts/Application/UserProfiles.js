app.userProfiles.form = function () {
    var $checkboxModules = $("#modules :checkbox");
    var getIds = function(selector) {
        return $(selector).map(function () {
            return parseInt($(this).val(), 10);
        }).get();
    };
    var functionalities = function(modules, checked) {
        $.each(modules, function() {
            $(":checkbox", $("#modal-" + this))
                .prop("checked", checked)
                .iCheck("update");
        });
    };

    $checkboxModules.on("ifChecked", function (e) {
        var $this = $(this);
        var $li = $this.closest("li");
        var modules = getIds($this);

        $(":checkbox:first", $li.closest(".parent")).prop("checked", true);

        if ($li.is(".parent")) {
            $("ul :checkbox", $li).prop("checked", true);
            modules = $.merge(modules, getIds($("ul :checkbox:checked", $li)));
        }

        functionalities(modules, true);

        $checkboxModules.iCheck("update");
    });

    $checkboxModules.on("ifUnchecked", function (e) {
        var $this = $(this);
        var $li = $this.closest("li");
        var modules = getIds($this);

        if ($li.is(".parent")) {
            $("ul :checkbox", $li).prop("checked", false);
            modules = $.merge(modules, getIds($("ul :checkbox", $li)));
        } else {
            var $liParent = $this.closest("li.parent");
            
            if ($("ul :checkbox:checked", $liParent).size() === 0) {
                $(":checkbox:first", $liParent).prop("checked", false);
                modules = $.merge(modules, getIds($(":checkbox:first", $liParent)));
            }
        }

        functionalities(modules, false);

        $checkboxModules.iCheck("update");
    });

    $(".treeview a").on("click", function(e) {
        var $this = $(this);

        e.preventDefault();

        if ($(":checkbox:first", $this.closest("li")).is(":checked")) {
            $($this.data("target")).modal("show");
        } else {
            $.thunder.alert("Para aplicar as permissões por funcionalidade é necessário primeiro selecionar o módulo.");
        }
    });

    this.addEvent("beforeSave", function() {
        var data = [];

        $(":checkbox:checked", ".modal").each(function (i) {
            var item = {};
            item["UserProfile.Functionalities[" + i + "].Id"] = $(this).val();
            data.push(item);
        });

        return data;
    });
};