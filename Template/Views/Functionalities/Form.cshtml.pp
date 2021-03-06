﻿@model $rootnamespace$.Models.Views.Functionalities.Form
@{
    Layout = "~/Views/Shared/_Modal.cshtml";
}
<div class="modal-header modal-thunder-header">
    <button type="button" class="close">&times;</button>
    Funcionalidade
</div>
@using (Html.BeginForm("Save", "Functionalities", FormMethod.Post, new
{
    role = "form",
    data_module = "modules.functionalities",
    data_action = "form",
    data_control = "form",
    data_success = "successOnSave",
    data_before = "beforeOnSave"
}))
{
    @Html.AntiForgeryToken()
    @Html.HiddenFor(m => m.Functionality.Id)
    @Html.HiddenFor(m => m.Functionality.HttpMethod)
    @Html.HiddenFor(m => m.Index)
    <div class="modal-body modal-thunder-body container">
        <div class="row">
            <div class="col-xs-6">
                <div class="form-group">
                    @Html.LabelFor(m => m.Functionality.Name)
                    @Html.Thunder().TextBoxFor(m => m.Functionality.Name, new { @class = "form-control" })
                </div>
            </div>
            <div class="col-xs-6">
                <div class="form-group">
                    @Html.LabelFor(m => m.Functionality.Description)
                    @Html.Thunder().TextBoxFor(m => m.Functionality.Description, new { @class = "form-control" })
                </div>
            </div>
            <div class="col-xs-6">
                <div class="form-group">
                    @Html.LabelFor(m => m.Functionality.Controller)
                    @Html.Thunder().TextBoxFor(m => m.Functionality.Controller, new { @class = "form-control" })
                </div>
            </div>
            <div class="col-xs-6">
                <div class="form-group">
                    @Html.LabelFor(m => m.Functionality.Action)
                    @Html.Thunder().TextBoxFor(m => m.Functionality.Action, new { @class = "form-control" })
                </div>
            </div>
            <div class="col-xs-8">
                <div class="form-group">
                    @Html.LabelFor(m => m.Functionality.HttpMethod)
                    <select class="form-control" multiple="multiple" data-control="select-multiple" data-placeholder="Selecione" name="@Html.NameFor(m => m.SelectedHttpMethod)" id="@Html.IdFor(m => m.SelectedHttpMethod)">
                        @foreach (var item in Model.HttpMethods)
                        {
                            <option value="@item.Value" @((Model.Functionality.HttpMethod ?? string.Empty).Contains(item.Value) ? Html.Raw("selected=\"selected\"") : Html.Raw("")) >@item.Text</option>
                        }
                    </select>
                </div>
            </div>
            <div class="col-xs-4">
                <div class="form-group">
                    @Html.LabelFor(x => x.Functionality.Default)
                    <div class="input-group">
                        @Html.YesOrNoFor(x => x.Functionality.Default)
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal-footer modal-thunder-footer  action-btn">
        <button type="submit" class="btn btn-primary">Salvar</button>
    </div>
}