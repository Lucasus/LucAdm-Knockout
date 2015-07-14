define(["jquery", "backbone", "knockout", "Scripts/custom/templateEngine"],

    function ($, Backbone, ko) {

        function AppViewModel() {
            this.templateName = ko.observable('default-page');
            this.pageViewModel = ko.observable('');
        }

        var vm = new AppViewModel();

        ko.applyBindings(vm);

        var Router = Backbone.Router.extend({

            initialize: function () {
                Backbone.history.start();
            },

            routes: {
                "": "inbox",
                "details" : "details"
            },

            inbox: function () {
                require(["src/pages/inbox/inbox", "text!src/pages/inbox/inbox.html"], function (Page, template) {
                    ko.templates["inbox"] = template;
                    vm.pageViewModel(new Page());
                    vm.templateName("inbox");
                });
            },

            details: function () {
                require(["src/pages/details/details", "text!src/pages/details/details.html"], function (Page, template) {
                    ko.templates["details"] = template;
                    vm.pageViewModel(new Page());
                    vm.templateName("details");
                });
            }
        });

        return Router;
    }

);