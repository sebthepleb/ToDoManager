(function() {
    function viewModel() {
        this.ToDos = ko.observableArray();
    };

    viewModel.LoadToDos = function(models) {
        models.forEach(function(model) {
            this.ToDos.push(new ToDo(model));
        });
    };

    ko.applyBindings(viewModel);
})();