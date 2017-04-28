(function() {
    var viewModel = (new function() {
        this.ToDos = ko.observableArray();
    });

    viewModel.LoadToDos = function(models) {
        models.forEach(function(model) {
            viewModel.ToDos.push(new ToDo(model));
        });
    };

    PostToController({
        url: "api/v1/ToDo/",
        method: Verbs.GET,
        success: function(models) {
            viewModel.LoadToDos(models);
        }
    });

    ko.applyBindings(viewModel);
})();