define(["knockout", "text!./details.html"], function (ko, template) {

    function ViewModel() {
        this.message = ko.observable('This is task details');
    }

    ViewModel.prototype.doSomething = function () {
        this.message('You invoked doSomething() on the viewmodel.');
    };


    return ViewModel;
});