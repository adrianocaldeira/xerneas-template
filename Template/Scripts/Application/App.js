var app = window.app || {};

app.modules = [
    "login",
    "modules"
];

app.modules.forEach(function (module) {
    if (!app[module]) app[module] = {};
});