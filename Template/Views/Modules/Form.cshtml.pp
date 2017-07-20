@using Newtonsoft.Json
@using Thunder.Web
@using $rootnamespace$.Properties
@model $rootnamespace$.Models.Module
@{
    var functionalities = Model.Functionalities.Select(x => new
    {
        x.Name,
        x.Action,
        x.Controller,
        x.Default,
        x.Description,
        x.HttpMethod,
        x.Id
    });
}
@section Scripts
{
    <script type="text/javascript">
        window.moduleParameters = {
            functionalities: @Html.Raw(functionalities.Json(Formatting.None))
            };
    </script>
}
<div class="row">
    <div class="col-lg-12">
        <h1 class="page-header">
            M&oacute;dulos
            <span class="pull-right">
                @Html.ButtonBack(Url.Action("Index"))
            </span>
        </h1>
    </div>
</div>
<div class="row" data-module="modules" data-action="form" data-parameters="moduleParameters">
    <div class="col-lg-12">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="panel-title">
                    <i class="fa fa-info"></i> Dados Cadastrais
                </div>
            </div>
            <div class="panel-body">
                @using (Html.BeginForm("Save", "Modules", FormMethod.Post, new
                {
                    data_control = "form",
                    data_before = "onBeforeSave",
                    data_redirect = "true"                 
                }))
                {
                    @Html.AntiForgeryToken()
                    @Html.HiddenFor(x => x.Id)
                    @Html.HiddenFor(x => x.Parent.Id)
                    <div class="row">
                        <div class="col-lg-5">
                            <div class="form-group">
                                @Html.LabelFor(x => x.Parent.Id)
                                <div class="form-control disabled">
                                    @(Model.Parent == null ? "Raiz" : Model.Parent.Name)
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-7">
                            <div class="form-group">
                                @Html.LabelFor(x => x.Name)
                                @Html.Thunder().TextBoxFor(x => x.Name, new { @class = "form-control" })
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                @Html.LabelFor(x => x.CssClass)
                                @Html.Thunder().TextBoxFor(x => x.CssClass, new { @class = "form-control" })
                            </div>
                        </div>
                        <div class="col-lg-8">
                            <div class="form-group">
                                @Html.LabelFor(x => x.Description)
                                @Html.Thunder().TextBoxFor(x => x.Description, new { @class = "form-control" })
                            </div>
                        </div>
                    </div>
                    <div class="panel panel-default" id="panel-functionalities">
                        <div class="panel-heading">
                            <div class="panel-title">
                                <i class="fa fa-table"></i> Funcionalidades
                                <div class="pull-right">
                                    @Html.ButtonNew(Url.Action("Form", "Functionalities"), new { id = "add-functionality", data_index = -1 })
                                </div>
                            </div>
                        </div>
                        <div class="panel-body">
                            <table id="panel-functionalities-data" class="table table-striped table-bordered table-hover clear-margin-bottom" style="display: none">
                                <thead>
                                    <tr>
                                        <th class="align-middle">Nome</th>
                                        <th class="align-middle">Descri&ccedil;&atilde;o</th>
                                        <th class="align-middle">Controller</th>
                                        <th class="align-middle">Action</th>
                                        <th class="align-middle align-center">M&eacute;todo Http</th>
                                        <th class="align-middle align-center">Principal</th>
                                        <th class="align-middle align-center col-edit">Editar</th>
                                        <th class="align-middle align-center col-delete">Excluir</th>
                                    </tr>
                                </thead>
                                <tbody></tbody>
                            </table>
                            <script id="panel-functionalities-template" type="text/x-handlebars-template">
                                {{#each data}}
                                <tr>
                                    <td class="align-middle">{{name}}</td>
                                    <td class="align-middle">{{description}}</td>
                                    <td class="align-middle">{{controller}}</td>
                                    <td class="align-middle">{{action}}</td>
                                    <td class="align-middle align-center">{{httpMethod}}</td>
                                    <td class="align-middle align-center">
                                        {{#if default}}Sim{{else}}Não{{/if}}
                                    </td>
                                    <td class="align-middle align-center">
                                        @Html.ButtonEdit(Url.Action("Form", "Functionalities"), new { data_index = "{{@index}}", @class = "edit-functionality" })
                                    </td>
                                    <td class="align-middle align-center">
                                        @Html.ButtonDelete("#", new { data_index = "{{@index}}", @class = "delete-functionality" })
                                    </td>
                                </tr>
                                {{/each}}
                            </script>
                            @Html.Thunder().Notify(NotifyType.Information, Resources.EmptyResult, false, new { id = "panel-functionalities-empty", style = "margin-bottom: 0px;" })
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-2">
                            <button type="submit" class="btn btn-primary">Salvar</button>
                        </div>
                    </div>
                }
            </div>
        </div>
    </div>
</div>
