var app = window.app || {};

app.modules = [
    "login",
    "userProfiles",
    "users",
    "modules"
];

app.modules.forEach(function (module) {
    if (!app[module]) app[module] = {};
});