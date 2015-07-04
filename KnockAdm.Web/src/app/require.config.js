// require.js looks for the following global when initializing
var require = {
    baseUrl: ".",
    paths: {
        "bootstrap":            "bower_modules/bootstrap/dist/js/bootstrap",
        "crossroads":           "bower_modules/crossroads/dist/crossroads",
        "hasher":               "bower_modules/hasher/dist/js/hasher",
        "jquery":               "bower_modules/jquery/dist/jquery",
        "knockout":             "bower_modules/knockout/dist/knockout.debug",
        "signals":              "bower_modules/js-signals/dist/signals",
        "text":                 "bower_modules/requirejs-text/text"
    },
    shim: {
        "bootstrap": { deps: ["jquery"] }
    }
};
