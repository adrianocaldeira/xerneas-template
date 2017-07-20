﻿@using $rootnamespace$.Models
@model $rootnamespace$.Models.Views.UserProfiles.Form
@helper TreeView(UserProfile profile, IList<Module> modules)
{
    if (modules.Count > 0)
    {
        <ul class="treeview">
            @foreach (var module in modules)
            {
                <li class="@(module.Childs.Any() ? "parent" : "")">
                    @if (profile.HasAccess(module))
                    {
                        <input type="checkbox" class="make-icheck" value="@module.Id" checked="checked" />
                    }
                    else
                    {
                        <input type="checkbox" class="make-icheck" value="@module.Id" />
                    }
                    @if (module.Functionalities.Any())
                    {
                        <a href="#" data-target="#modal-@module.Id">
                            @module.Name
                        </a>
                    }
                    else
                    {
                        @module.Name
                    }
                    @TreeView(profile, module.Childs)
                </li>
            }
        </ul>
    }
}
@helper Functionalities(UserProfile profile, IList<Module> modules)
{
    if (modules.Count > 0)
    {
        foreach (var module in modules)
        {
            <div class="modal fade" id="modal-@module.Id" data-module-id="@module.Id" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title theme-font bold uppercase">Permissões do módulo @module.Name</h4>
                        </div>
                        <div class="modal-body functionalities" style="overflow: auto">
                            <ul>
                                @foreach (var functionality in module.Functionalities.OrderBy(x => x.Name))
                                {
                                    <li>
                                        <label>
                                            @if (profile.HasAccess(functionality))
                                            {
                                                <input type="checkbox" class="make-icheck" value="@functionality.Id" checked="checked" />
                                            }
                                            else
                                            {
                                                <input type="checkbox" class="make-icheck" value="@functionality.Id" />
                                            }
                                            @functionality.Name
                                        </label>
                                    </li>
                                }
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
            @Functionalities(profile, module.Childs)
        }
    }
}
@section Styles
{
    <style>
        .functionalities ul {
            overflow:hidden;
            padding: 0;
        }

        .functionalities ul li {
            float:left;
            display:inline;
            width: 50%;
        }
    </style>
}
<div class="row" data-module="userProfiles" data-action="form">
    <div class="col-lg-12">
        <h1 class="page-header">
            Perfis de Usu&aacute;rio
            <span class="pull-right">
                @Html.ButtonBack(Url.Action("Index"))
            </span>
        </h1>
    </div>
</div>
<div class="row">
    <div class="col-lg-12">
        @using (Html.BeginForm("Save", "UserProfiles", FormMethod.Post, new
                {
                    data_control = "form",
                    data_redirect = "true",
                    data_before = "beforeSave"
                }))
        {
            @Html.AntiForgeryToken()
            @Html.HiddenFor(x => x.UserProfile.Id)
            <div class="panel with-nav-tabs panel-default">
                <div class="panel-heading">
                    <ul class="nav nav-tabs">
                        <li class="active"><a href="#tab_0" data-toggle="tab">Dados do Perfil</a></li>
                        <li><a href="#tab_1" data-toggle="tab">Funcionalidades</a></li>
                    </ul>
                </div>
                <div class="panel-body">
                    <div class="tab-content">
                        <div class="tab-pane in active" id="tab_0">
                            <div class="row">
                                <div class="col-lg-10">
                                    <div class="form-group">
                                        @Html.LabelFor(x => x.UserProfile.Name)
                                        @Html.Thunder().TextBoxFor(x => x.UserProfile.Name, new { @class = "form-control" })
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        @Html.LabelFor(x => x.UserProfile.Active)
                                        <div class="input-group">
                                            @Html.ActiveFor(x => x.UserProfile.Active)
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane" id="tab_1">
                            <div class="row">
                                <div id="modules" class="col-md-12">
                                    @TreeView(Model.UserProfile, Model.Modules)
                                </div>
                                @Functionalities(Model.UserProfile, Model.Modules)
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <button type="submit" class="pull-right btn btn-primary"><i class="fa fa-save"></i> Salvar</button>
                </div>
            </div>
        }
    </div>
</div>
