define(["knockout", "text!./users.html"], function(ko, usersTemplate) {

  function HomeViewModel(route) {
    this.message = ko.observable('Welcome to TestKO!');
  }

  HomeViewModel.prototype.doSomething = function() {
    this.message('You invoked doSomething() on the viewmodel.');
  };

  return { viewModel: HomeViewModel, template: usersTemplate };

});
