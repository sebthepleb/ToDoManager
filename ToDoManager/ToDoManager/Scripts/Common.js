// If there isn't a base API URL, default to the root of the site.
window.baseApiUrl = window.baseApiUrl || "/";

var Verbs = {
    GET: "GET",
    PUT: "PUT",
    POST: "POST",
    DELETE: "DELETE"
};

var errorModalHtml = "<div id=\"uncaughtErrorModal\" class=\"modal fade\" role=\"dialog\">" +
    "<div class=\"modal-dialog\">" +
        "<div class=\"modal-content\">" +
            "<div class=\"modal-body danger\">" +
                "<i class=\"pull-right fa fa-times\" data-dismiss=\"modal\"></i>" +
                "<h4>Oops, something went wrong.</h4>" +
                "<p>There was an unexpected error whilst dealing with your request.</p>" +
                "<p>Please try again, or contact us if the problem persists.</p>" +
            "</div>" +
        "</div>" +
    "</div>" +
"</div>";

var genericErrorHandler = function() {
    if (!$("#uncaughtErrorModal").length)
        $("body").append(errorModalHtml);

    $("#uncaughtErrorModal").modal("show");
}

function PostToController(options) {
    if (options == null || typeof(options) != "object")
        return;

    $.ajax({
        url: window.baseApiUrl + options.url,
        method: options.method,
        dataType: "json",
        data: options.data,
        success: options.success,
        error: options.error || genericErrorHandler
    });
}