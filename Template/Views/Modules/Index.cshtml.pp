@using $rootnamespace$.Models
@model IList<Module>
@helper TreeView(IOrderedEnumerable<Module> modules, int level)
{
    foreach (var module in modules)
    {
        <li class="dd-item dd3-item" data-id="@module.Id">
            <div class="dd-handle dd3-handle">Drag</div>
            <div class="dd3-content">
                <i class="fa @module.CssClass"></i> @module.Name
                <div class="pull-right">
                    @Html.ButtonNew(Url.Action("New", new RouteValueDictionary { { "Parent.Id", module.Id } }), new { @class = "add-sub-module", style = (level >= 2 ? "display:none" : "") })
                    @Html.ButtonEdit(Url.Action("Edit", new { module.Id }))
                    @Html.ButtonDelete(Url.Action("Delete", new { module.Id }))
                </div>
            </div>
            @if (module.Childs.Any())
            {
                <ol class="dd-list">
                    @TreeView(module.Childs.OrderBy(x => x.Order), level+1)
                </ol>
            }
        </li>
    }
}
<div class="row" data-module="modules" data-action="index">
    <div class="col-lg-12">
        <h1 class="page-header">
            Módulos
            <span class="pull-right">
                @Html.ButtonNew(Url.Action("New"), new { title = "Novo" })
            </span>
        </h1>
    </div>
</div>
<div class="row">
    <div class="col-lg-12">
        <div class="dd" data-control="nestable" data-max-depth="3" data-organize-url="@Url.Action("Organazer")" data-change="onChangeModule" data-delete="onDeleteModule">
            <ol class="dd-list">
                @TreeView(Model.OrderBy(x => x.Order), 0)
            </ol>
        </div>
    </div>
</div>