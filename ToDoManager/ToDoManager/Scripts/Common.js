var Verbs = {
    GET: "GET",
    PUT: "PUT",
    POST: "POST",
    DELETE: "DELETE"
};

var baseUrl = "http://localhost/ToDoManager/";

function PostToController(options) {
    if (options == null || typeof(options) != "object")
        return;

    $.ajax({
        url: baseUrl + options.url,
        method: options.method,
        dataType: "json",
        data: options.data,
        success: options.success,
        error: options.error
    });
}