(function() {
    var viewModel = (new function() {
        this.ToDos = ko.observableArray();
        this.Loading = ko.observable(true);
        this.Errored = ko.observable(false);
        this.SaveErrored = ko.observable(false);
        this.SelectedToDo = ko.observable(new ToDo());

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

    viewModel.EditToDo = function(todo) {
        viewModel.SelectedToDo(todo);
        viewModel.SaveErrored(false);
        $("#editModal").modal("show");
    };

    viewModel.AddToDo = function() {
        viewModel.EditToDo(new ToDo());
    };

    viewModel.SaveToDo = function () {

        viewModel.SaveErrored(false);

        var selectedToDo = viewModel.SelectedToDo();
        if (!selectedToDo.Title() && !selectedToDo.Detail()) {
            $("#editModal").modal("hide");
            return;
        }

        var data = ko.toJSON(viewModel.SelectedToDo());
        var isNew = selectedToDo.Id == null;
        var method = isNew ? Verbs.POST : Verbs.PUT;
        var url = "api/v1/ToDo/" +(!isNew ? selectedToDo.Id : null);

        PostToController({
            url: url,
            method: method,
            data: data,
            success: function (model) {
                var toDo = viewModel.SelectedToDo();
                toDo.Load(model);
                viewModel.ToDos.push(toDo);
                $("#editModal").modal("hide");
            },
            error: function() {
                viewModel.SaveErrored(true);
            }
        });
    };

    PostToController({
        url: "api/v1/ToDo/",
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