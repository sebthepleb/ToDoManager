ko.bindingHandlers.date = {
    update: function (element, valueAccessor, allBindings) {
        var date = ko.unwrap(valueAccessor());
        var momentDate = moment(date);

        var format = allBindings.get("format") || "DD/MM/YYYY HH:mm";

        if (!momentDate.isValid())
            $(element).text(null);
        else
            $(element).text(momentDate.format(format));
    }
};