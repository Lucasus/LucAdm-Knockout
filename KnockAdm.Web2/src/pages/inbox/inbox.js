define(["knockout", "text!./inbox.html"], function (ko, template) {

    function ViewModel() {
        this.message = ko.observable('This is inbox');
        this.tasks = ko.observableArray([
            { id: "1", name: "task 1"},
            { id: "2", name: "task 2" },
            { id: "3", name: "task 3" },
        ]);
    }

    ViewModel.prototype.doSomething = function () {
        this.message('You invoked doSomething() on the viewmodel.');
    };


    return ViewModel;
});