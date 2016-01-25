var app = window.app || {};

app.modules = [
    "login"
    , "modules"
    , "userProfiles"
];

app.modules.forEach(function(module) {
    if (!app[module]) app[module] = {};
});