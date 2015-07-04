define(['jquery', 'knockout', './router', 'bootstrap'], function ($, ko, router) {

    // Components can be packaged as AMD modules, such as the following:
    ko.components.register('users-page', { require: 'pages/users-page/users' });
    ko.components.register('about-page', { template: { require: 'text!pages/about-page/about.html' } });

    ko.components.register('nav-bar', { require: 'components/nav-bar/nav-bar' });
    ko.components.register('users-list', { require: 'components/users-list/users-list' });

    // [Scaffolded component registrations will be inserted here. To retain this feature, don't remove this comment.]

    // Start the application
    ko.applyBindings({ route: router.currentRoute });
});
