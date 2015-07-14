define(["knockout", "text!./inbox.html"], function (ko, template) {

    function ViewModel() {
        this.message = ko.observable('This is inbox');
    }

    ViewModel.prototype.doSomething = function () {
        this.message('You invoked doSomething() on the viewmodel.');
    };


    return ViewModel;
});