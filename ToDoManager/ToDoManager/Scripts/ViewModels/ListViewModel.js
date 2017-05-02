(function() {
    var viewModel = (new function() {
        this.ToDos = ko.observableArray();
        this.Loading = ko.observable(true);
        this.Errored = ko.observable(false);

        var vm = this;

        this.Empty = ko.computed(function() {
            return vm.ToDos().length === 0;
        });
    });

    viewModel.LoadToDos = function(models) {
        models.forEach(function(model) {
            viewModel.ToDos.push(new ToDo(model));
        });
    };

    PostToController({
        url: "/api/v1/ToDo/",
        method: Verbs.GET,
        success: function(models) {
            viewModel.LoadToDos(models);
            viewModel.Loading(false);
        },
        error: function() {
            viewModel.Loading(false);
            viewModel.Errored(true);
        }
    });

    $(function() {
        ko.applyBindings(viewModel);
    });
})();