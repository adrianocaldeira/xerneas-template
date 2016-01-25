app.userProfiles.form = function () {
    var $checkboxModules = $("#modules :checkbox");

    $checkboxModules.on("ifChecked", function (e) {
        var $this = $(this);
        var $li = $this.closest("li");

        $(":checkbox:first", $li.closest(".parent")).prop("checked", true);

        if ($li.is(".parent")) {
            $("ul :checkbox", $li).prop("checked", true);
        }

        $checkboxModules.iCheck("update");
    });

    $checkboxModules.on("ifUnchecked", function (e) {
        var $this = $(this);
        var $li = $this.closest("li");

        if ($li.is(".parent")) {
            $("ul :checkbox", $li).prop("checked", false);
        } else {
            var $liParent = $this.closest("li.parent");
            
            if ($("ul :checkbox:checked", $liParent).size() === 0) {
                $(":checkbox:first", $liParent).prop("checked", false);
            }
        }

        $checkboxModules.iCheck("update");
    });
};