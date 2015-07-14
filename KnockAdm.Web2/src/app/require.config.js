// require.js looks for the following global when initializing
var require = {
    baseUrl: ".",
    out: 'scripts.js',
    paths: {
        "jquery": "Scripts/jquery-1.10.2",
        "underscore": "Scripts/underscore",
        "backbone": "Scripts/backbone",
        "knockout": "Scripts/knockout-3.3.0",
        'text': 'Scripts/text',
    },
    insertRequire: ['app/startup'],
    shim: {
        backbone: {
            deps: ['jquery', 'underscore'],
            exports: 'Backbone'
        },
        underscore: {
            'exports': '_'
        }
    }
};
