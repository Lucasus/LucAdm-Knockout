define(["knockout", "text!./details.html"], function (ko, template) {

    function ViewModel(id) {
        this.message = ko.observable('This is task details');
        this.id = id;
    }

    ViewModel.prototype.doSomething = function () {
        this.message('You invoked doSomething() on the viewmodel.');
    };


    return ViewModel;
});