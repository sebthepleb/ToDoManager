function ToDo(model) {

    this.Id = null;
    this.Title = ko.observable(null);
    this.Detail = ko.observable(null);
    this.DateCreated = ko.observable(new Date());
    this.DateUpdated = ko.observable(null);

    var toDo = this;

    this.Load = function (model) {
        if (model == null)
            model = {};

        toDo.Id = model.Id;
        toDo.Title(model.Title);
        toDo.Detail(model.Detail);
        toDo.DateCreated(new Date(model.DateCreated));
        toDo.DateUpdated(model.DateUpdated ? new Date(model.DateUpdated) : null);
    };

    this.Load(model);
};