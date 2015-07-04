define(['knockout', 'text!./users-list.html'], function (ko, template) {

    function UsersListViewModel(params) {
        var self = this;
        self.searchTerm = ko.observable('');
        self.sortColumn = ko.observable('');
        self.sortType = ko.observable('');
        self.page = ko.observable(1);
        self.pageSize = ko.observable(10);
        self.users = ko.observableArray([]);
        self.total = ko.observable(null);

        self.loadUsers();
    }

    UsersListViewModel.prototype.loadUsers = function () {
        var self = this;
        $.getJSON("/api/users/", {
            searchTerm: self.searchTerm(),
            sortColumn: self.sortColumn(),
            sortType: self.sortType(),
            page: self.page(),
            pageSize: self.pageSize()
        }).done(function (result) {
            self.users(result.list);
            self.total(result.total);
        })
    };

    UsersListViewModel.prototype.search = function () {
        this.loadUsers();
    };

    return { viewModel: UsersListViewModel, template: template };
});

