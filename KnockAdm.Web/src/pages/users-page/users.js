define(["knockout", "text!./users.html"], function (ko, template) {

    function UsersPageViewModel(route) {
        this.route = route;
        this.message = ko.observable('Welcome to TestKO!');
    }

    UsersPageViewModel.prototype.doSomething = function () {
        this.message('You invoked doSomething() on the viewmodel.');
    };


    return { viewModel: UsersPageViewModel, template: template };
});