@model $rootnamespace$.Models.Views.Login.Index
@{
    Layout = null;
}
<!DOCTYPE html>
<html lang="pt-BR">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">
    <title>Xerneas Template</title>
    @Styles.Render("~/content/bundle")
    @Styles.Render("~/content/sb-admin-2/dist/metisMenu/css/bundle")
    @Styles.Render("~/content/sb-admin-2/css/bundle")
    @Styles.Render("~/content/sb-admin-2/dist/font-awesome/css/bundle")
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
</head>
<body>
    <div class="container" data-module="login" data-action="index">
        <div class="row">
            <div class="col-md-6 col-md-offset-3">
                <div class="login-panel panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">Autenticação</h3>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-4">
                                <img src="~/Content/images/logo.png" class="img-responsive" />
                            </div>
                            <div class="col-md-8">
                                @using (Html.BeginForm("Authentication", "Login", FormMethod.Post, new
                                    {
                                        role = "form",
                                        data_control = "form",
                                        data_redirect = "true",
                                        data_show_message = "false"                                        
                                    }))
                                {
                                    @Html.AntiForgeryToken()
                                    @Html.HiddenFor(x => x.ReturnUrl)
                                    <div class="well well-sm">
                                        Informe o seu login ou e-mail e senha de acesso.
                                    </div>
                                    <fieldset>
                                        <div class="form-group">
                                            <div class="input-group">
                                                <span class="input-group-addon"><i class="fa fa-user" style="width: 14px;"></i></span>
                                                @Html.TextBoxFor(x => x.UserName, new { @class = "form-control", placeholder = "Login ou e-mail de cadastro", autofocus = "autofocus" })
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="input-group">
                                                <span class="input-group-addon"><i class="fa fa-key"></i></span>
                                                @Html.PasswordFor(x => x.Password, new { @class = "form-control", placeholder = "Senha de acesso" })
                                            </div>
                                        </div>
                                        <button type="submit" class="btn btn-default btn-block">Acessar</button>
                                    </fieldset>
                                }
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
    @Scripts.Render("~/scripts/bundle")
</body>

</html>