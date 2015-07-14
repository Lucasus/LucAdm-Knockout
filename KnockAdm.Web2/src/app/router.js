define(["jquery", "backbone", "knockout", "Scripts/custom/templateEngine"],

    function ($, Backbone, ko) {

        function AppViewModel() {
            this.page = ko.observable({ name: 'default-page', viewModel: '' });
        }

        var vm = new AppViewModel();

        ko.applyBindings(vm);

        var Router = Backbone.Router.extend({

            initialize: function () {
                Backbone.history.start();
            },

            routes: {
                "": "inbox",
                "details/:id" : "details"
            },

            inbox: function () {
                require(["src/pages/inbox/inbox", "text!src/pages/inbox/inbox.html"], function (Page, template) {
                    ko.templates["inbox"] || (ko.templates["inbox"] = template);
                    vm.page({ name: "inbox", viewModel: new Page() });
                });
            },

            details: function (id) {
                require(["src/pages/details/details", "text!src/pages/details/details.html"], function (Page, template) {
                    ko.templates["details"] = template;
                    vm.page({ name: "details", viewModel: new Page(id) });
                });
            }
        });

        return Router;
    }

);