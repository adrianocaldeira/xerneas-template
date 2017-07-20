@model $rootnamespace$.Models.Views.Users.Index
<div class="row">
    <div class="col-lg-12">
        <h1 class="page-header">
            Usu&aacute;rios
            <span class="pull-right">
                @Html.ButtonNew(Url.Action("New"), new { title = "Novo" })
            </span>
        </h1>
    </div>
</div>
<div class="row">
    <div class="col-lg-12">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="panel-title">
                    <i class="fa fa-filter"></i> Filtro
                </div>
            </div>
            <div class="panel-body">
                <form id="filter">
                    @Html.AntiForgeryToken()
                    <div class="row">
                        <div class="col-lg-3">
                            <div class="form-group">
                                @Html.LabelFor(x => x.Filter.Profile.Id)
                                @Html.DropDownListFor(x => x.Filter.Profile.Id, Model.Profiles, new { @class = "form-control" })
                            </div>
                        </div>
                        <div class="col-lg-7">
                            <div class="form-group">
                                @Html.LabelFor(x => x.Filter.Name)
                                @Html.TextBoxFor(x => x.Filter.Name, new { @class = "form-control" })
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                @Html.LabelFor(x => x.Filter.Active)
                                @Html.StatusFor(x => x.Filter.Active, "Todos", new { @class = "form-control" })
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-2">
                            <button type="submit" class="btn btn-primary btn-block">Filtrar</button>
                        </div>
                    </div>
                </form>
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="panel-title">
                    <div class="caption">
                        <i class="fa fa-table"></i> Resultado
                    </div>
                    <div class="actions">
                        <a href="#" class="fullscreen" title="Expandir">
                            <i class="fa fa-expand"></i>
                        </a>
                    </div>
                </div>
            </div>
            <div class="panel-body">
                @using (Html.Thunder().Grid(Url.Action("List"), UserFilter.DefaultPageSize, new { data_control = "grid" }))
                {
                }
            </div>
        </div>
    </div>
</div>
