@model $rootnamespace$.Models.Views.Users.Form
<div class="row">
    <div class="col-lg-12">
        <h1 class="page-header">
            Usuários
            <span class="pull-right">
                @Html.ButtonBack(Url.Action("Index"))
            </span>
        </h1>
    </div>
</div>
<div class="row">
    <div class="col-lg-12">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="panel-title">
                    <i class="fa fa-building"></i> Dados Cadastrais
                </div>
            </div>
            <div class="panel-body">
                @using (Html.BeginForm("Save", "Users", FormMethod.Post, new
                {
                    data_control = "form",
                    data_redirect = "true"
                }))
                {
                    @Html.AntiForgeryToken()
                    @Html.HiddenFor(x => x.User.Id)
                    <div class="row">
                        <div class="col-lg-2">
                            <div class="form-group">
                                @Html.LabelFor(x => x.User.Profile.Id)
                                @Html.DropDownListFor(x => x.User.Profile.Id, Model.Profiles, new {@class = "form-control"})
                            </div>
                        </div>
                        <div class="col-lg-5">
                            <div class="form-group">
                                @Html.LabelFor(x => x.User.Name)
                                @Html.Thunder().TextBoxFor(x => x.User.Name, new { @class = "form-control" })
                            </div>
                        </div>
                        <div class="col-lg-5">
                            <div class="form-group">
                                @Html.LabelFor(x => x.User.Email)
                                <div class="input-group">
                                    <span class="input-group-addon">
                                        <i class="fa fa-envelope"></i>
                                    </span>
                                    @Html.Thunder().TextBoxFor(x => x.User.Email, new { @class = "form-control" })
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                @Html.LabelFor(x => x.User.Login)
                                <div class="input-group">
                                    <span class="input-group-addon">
                                        <i class="fa fa-user"></i>
                                    </span>
                                    @Html.Thunder().TextBoxFor(x => x.User.Login, new { @class = "form-control", autocomplete = "off" })
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                @Html.LabelFor(x => x.User.Password)
                                <div class="input-group">
                                    <span class="input-group-addon">
                                        <i class="fa fa-lock"></i>
                                    </span>
                                @Html.Thunder().PasswordFor(x => x.User.Password, new { @class = "form-control", autocomplete = "off" })
                                </div>
                                
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                @Html.LabelFor(x => x.User.Active)
                                <div class="input-group">
                                    @Html.ActiveFor(x => x.User.Active)
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
    </div>
</div>
