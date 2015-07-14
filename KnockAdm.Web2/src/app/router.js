define(["jquery", "backbone", "knockout", "Scripts/custom/templateEngine"],

    function ($, Backbone, ko) {

        function getPage (pageName, params, callback) {
            require(["src/pages/" + pageName + "/" + pageName, "text!src/pages/" + pageName + "/" + pageName + ".html"], function (Page, template) {
                ko.templates[pageName] || (ko.templates[pageName] = template);
                var component = { name: pageName, viewModel: new Page(params) };
                callback(component);
            });
        }

        function AppViewModel() {
            this.navBar = ko.observable({ text: "this is navbar" });
            this.page = ko.observable({ name: 'default-page', viewModel: '' });
        }

        var app = new AppViewModel();

        ko.applyBindings(app);

        var Router = Backbone.Router.extend({

            initialize: function () {
                Backbone.history.start();
            },

            routes: {
                "": "inbox",
                "details/:id" : "details"
            },

            inbox: function () {
                getPage("inbox", {}, function (page) { app.page(page) });
            },

            details: function (id) {
                getPage("details", id, function (page) { app.page(page) });
            },
        });

        return Router;
    }

);