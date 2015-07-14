define(["knockout", "text!./comments.html"], function (ko, template) {

    function ViewModel(id) {
        this.comment = ko.observable("example comment for task id = " + id);
    }

    return ViewModel;
});