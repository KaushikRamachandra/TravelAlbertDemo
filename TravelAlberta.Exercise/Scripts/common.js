var REGEX_EMAIL = '([a-z0-9!#$%&\'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&\'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)';

/**
 * returns the current context path,
 * ex: http://localhost:8080/MyApp/Controller returns /MyApp/
 * ex: http://localhost:8080/MyApp returns /MyApp/
 * ex: https://www.example.co.za/ returns /
 */
function getContextPath() {
    var ctx = window.location.pathname,
        path = '/' !== ctx ? ctx.substring(0, ctx.indexOf('/', 1) + 1) : ctx;
    return path + (/\/$/.test(path) ? '' : '/');
}

function getRootWebSitePath()
{
    var _location = document.location.toString();
    console.log("Location is " + _location);

    var applicationNameIndex = _location.indexOf('/', _location.indexOf('://') + 3);
    var applicationName = _location.substring(0, applicationNameIndex);
    console.log("App name is " + applicationName);

    var webFolderIndex = _location.indexOf('/', _location.indexOf(applicationName) + applicationName.length + 1);
    var webFolderFullPath = _location.substring(0, webFolderIndex);
    console.log("Web folder path is " + webFolderFullPath);

    if (_location.indexOf("localhost") === -1)
        return applicationName;
    else
        return webFolderFullPath;
}

function validateEmail(email) {
    // http://stackoverflow.com/a/46181/11236

    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(email);
}

function AjaxBegin() {
    $("#wait").css("display", "block");
    $("#waitMobile").css("display", "block");
}

function displayLoading(target) {
    var element = $(target);
    kendo.ui.progress(element, true);
    setTimeout(function () {
        kendo.ui.progress(element, false);
    }, 2000);
}

function AjaxComplete() {
    $("#wait").css("display", "none");
    $("#waitMobile").css("display", "none");
}

function AjaxFailure(ajaxContext) {
    $.messages.error("Error Code [" + ajaxContext.ErrorCode + "] " + ajaxContext.responseText);
}

function pad(n, width, z) {
    z = z || '0';
    n = n + '';
    return n.length >= width ? n : new Array(width - n.length + 1).join(z) + n;
}


function msieversion() {
    var ua = window.navigator.userAgent;
    var msie = ua.indexOf("MSIE ");

    if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./))      // If Internet Explorer, return version number
        return parseInt(ua.substring(msie + 5, ua.indexOf(".", msie)));
    else                 // If another browser, return 0
        return 0;
}

function getTimeZoneOffset() {
    var d = new Date()
    return d.getTimezoneOffset();
}

function getMe(url, onSuccess, context) {
    $.ajax({
        url: url,
        type: 'GET',
        context: context,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        beforeSend: function () { AjaxBegin(); },
        success: onSuccess,
        error: function (request, status, error) {
            $.messages.error('Operation failed.');
        },
        complete: function () {
            AjaxComplete();
        }
    });
}

function postMe(url, data, successMessage, failureMessage) {
    $.ajax({
        url: url,
        data: JSON.stringify(data),
        type: 'POST',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        beforeSend: function () { AjaxBegin(); },
        success: function (data) {
            if (data.length == 0)
                $.messages.success(successMessage);
            else
                $.messages.error(data.StatusMessage);
        },
        error: function (request, status, error) {
            $.messages.error(failureMessage + error);
        },
        complete: function () {
            AjaxComplete();
        }
    });
}

function postMeBack(url, data, failureMessage, onSuccess, context) {
    $.ajax({
        url: url,
        data: JSON.stringify(data),
        context: context,
        type: 'POST',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        beforeSend: function () { AjaxBegin(); },
        success: onSuccess,
        error: function (request, status, error) {
            $.messages.error(failureMessage + error);
        },
        complete: function () {
            AjaxComplete();
        }
    });
}

function postMeBackNoData(url, failureMessage, onSuccess, context) {
    $.ajax({
        url: url,
        context: context,
        type: 'POST',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        beforeSend: function () { AjaxBegin(); },
        success: onSuccess,
        error: function (request, status, error) {
            $.messages.error(failureMessage + error);
        },
        complete: function () {
            AjaxComplete();
        }
    });
}