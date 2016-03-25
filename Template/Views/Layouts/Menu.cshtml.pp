﻿@model IList<$rootnamespace$.Models.Module>
<div class="navbar-default sidebar" role="navigation">
    <div class="sidebar-nav navbar-collapse">
        <ul class="nav" id="side-menu">
            @if (Model.Any())
            {
                var controllerName = ViewContext.ParentActionViewContext.RouteData.GetRequiredString("controller");
                var actionName = ViewContext.ParentActionViewContext.RouteData.GetRequiredString("action");
                
                foreach (var module in Model.OrderBy(x => x.Order))
                {
                    <li class="@(module.Contains(controllerName, actionName) ? "active" : "")">
                        <a class="@(!module.Childs.Any() && module.Contains(controllerName, actionName) ? "active" : "")" href="@(module.DefaultFunctionality == null ? "javascript:;" : Url.Action(module.DefaultFunctionality.Action, module.DefaultFunctionality.Controller))">
                            @if (!string.IsNullOrWhiteSpace(module.CssClass))
                            {
                                <i class="fa @module.CssClass fa-fw"></i>
                            } @module.Name
                            @if (module.Childs.Any())
                            {
                                <span class="fa arrow"></span>
                            }
                        </a>
                        @if (module.Childs.Any())
                        {
                            <ul class="nav nav-second-level @(module.Contains(controllerName, actionName) ? "in" : "")">
                                @foreach (var secondLevel in module.Childs.OrderBy(x => x.Order))
                                {
                                    var secondLevelUrl = secondLevel.DefaultFunctionality != null ?
                                        Url.Action(secondLevel.DefaultFunctionality.Action, secondLevel.DefaultFunctionality.Controller) : "#";
                                    <li>
                                        <a class="@(!secondLevel.Childs.Any() && secondLevel.Contains(controllerName, actionName) ? "active" : "")" href="@secondLevelUrl">
                                            @secondLevel.Name
                                            @if (secondLevel.Childs.Any())
                                            {
                                                <span class="fa arrow"></span>
                                            }
                                        </a>
                                        @if (secondLevel.Childs.Any())
                                        {
                                            <ul class="nav nav-third-level @(secondLevel.Contains(controllerName, actionName) ? "in" : "")">
                                                @foreach (var thirdLevel in secondLevel.Childs.OrderBy(x => x.Order))
                                                {
                                                    var thirdLevelUrl = thirdLevel.DefaultFunctionality != null ?
                                                        Url.Action(thirdLevel.DefaultFunctionality.Action, thirdLevel.DefaultFunctionality.Controller) : "#";
                                                    <li>
                                                        <a class="@(thirdLevel.Contains(controllerName, actionName) ? "active" : "")" href="@thirdLevelUrl">@thirdLevel.Name</a>
                                                    </li>
                                                }
                                            </ul>
                                        }
                                    </li>
                                }
                            </ul>
                        }
                    </li>
                }
            }
            <li>
                <a href="@Url.Action("Index", "Logoff")"><i class="fa fa-sign-out fa-fw"></i> Sair</a>
            </li>
        </ul>
    </div>
    <!-- /.sidebar-collapse -->
</div>