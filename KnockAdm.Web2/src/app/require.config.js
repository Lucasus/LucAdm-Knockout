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
    include: [
        'jquery',
        'underscore',
        'backbone',
        'knockout'
    ],
    bundles: {
        // If you want parts of the site to load on demand, remove them from the 'include' list
        // above, and group them into bundles here.
        // 'bundle-name': [ 'some/module', 'another/module' ],
        // 'another-bundle-name': [ 'yet-another-module' ]
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
