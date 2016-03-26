@using $rootnamespace$.Properties
@model IPaging<$rootnamespace$.Models.UserProfile>
@if (Model.Any())
{
    <div class="row">
        <div class="col-lg-12">
            <table class="table table-striped table-bordered table-hover">
                <thead>
                    <tr>
                        <th class="align-middle align-center" style="width: 60px;">#</th>
                        <th class="align-middle">Nome</th>
                        <th class="align-middle align-center" style="width: 80px;">Status</th>
                        <th class="align-middle align-center col-edit">Editar</th>
                        <th class="align-middle align-center col-edit">Excluir</th>
                    </tr>
                </thead>
                <tbody>
                    @foreach (var item in Model)
                    {
                        <tr>
                            <td class="align-middle align-center">@item.Id</td>
                            <td class="align-middle">@item.Name</td>
                            <td class="align-middle align-center">@(item.Active ? "Ativo" : "Inativo")</td>
                            <td class="align-middle align-center">
                                @Html.ButtonEdit(Url.Action("Edit", new {item.Id}))
                            </td>
                            <td class="align-middle align-center">
                                @Html.ButtonDelete(Url.Action("Delete", new { item.Id }))
                            </td>
                        </tr>
                    }
                </tbody>
            </table>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-6">
            @Model.Records registro(s) encontrado(s)
        </div>
        <div class="col-lg-6">
            <div class="pull-right">
                @Html.Thunder().Pagination(Model)
            </div>
        </div>
    </div>
}
else
{
    @Html.Thunder().Notify(NotifyType.Warning, "Não existe nenhum registro cadastrado.")
}