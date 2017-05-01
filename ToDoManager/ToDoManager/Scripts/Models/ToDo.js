function ToDo(model) {

    if (model == null)
        model = {};

    this.Id = model.Id;
    this.Title = ko.observable(model.Title);
    this.Detail = ko.observable(model.Detail);
    this.DateCreated = new Date(model.DateCreated);
    this.DateUpdated = model.DateUpdated ? new Date(model.DateUpdated) : null;
}