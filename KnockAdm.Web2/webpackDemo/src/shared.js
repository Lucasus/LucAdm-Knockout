define([], function () {
    console.log("shared module loaded");
    return function (msg) { console.log(msg) };
});
