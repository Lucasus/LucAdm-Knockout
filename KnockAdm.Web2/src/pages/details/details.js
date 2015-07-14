define(["knockout", "text!./details.html", "./comments/comments", "text!./comments/comments.html"], function (ko, template, CommentsViewModel, commentsTemplate) {

    function ViewModel(id) {
        ko.templates["comments"] || (ko.templates["comments"] = commentsTemplate);

        this.message = ko.observable('This is task details');
        this.comments = ko.observable({ name: "comments", viewModel: new CommentsViewModel(id) });
        this.id = id;
    }

    return ViewModel;
});