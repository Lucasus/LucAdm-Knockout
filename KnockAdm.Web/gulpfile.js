/// <vs />
// include plug-ins
var gulp = require("gulp");
var concat = require("gulp-concat");
var uglify = require("gulp-uglify");
var del = require("del");
var inject = require("gulp-inject");
var series = require("stream-series");
var debug = require("gulp-debug");
var less = require("gulp-less");
var path = require("path");
var newer = require("gulp-newer");

var config = {
    lib: [
        "bower_components/jquery/dist/jquery.js",
        "bower_components/knockout/dist/knockout.js"],
    src: ["app/app.js",
        "app/**/*.js"],
    css: ["bower_components/bootstrap/dist/css/bootstrap.css",
          "bower_components/bootstrap/dist/css/bootstrap.css.map"],
    fonts: ["bower_components/bootstrap/dist/fonts/*.*"],
    less: ["bower_components/bootstrap/less/bootstrap.less"]
};
// Common tasks:
gulp.task("clean", function(){
  del.sync(["dist/*.*"]);
});
 
gulp.task("fonts", function () {
    return gulp.src(config.fonts)
    .pipe(newer("./fonts"))
    .pipe(gulp.dest("./fonts"));
});

gulp.task("less", function () {
    return gulp.src(config.less)
      .pipe(newer("./css"))
      .pipe(less({
          paths: [path.join(__dirname, "less", "includes")]
      }))
      .pipe(gulp.dest("./css"));
});
// Debug tasks:
gulp.task("scripts-debug", ["clean"], function () {
    return gulp.src(config.lib)
      .pipe(newer("./lib"))
      .pipe(gulp.dest("./lib"));
});

gulp.task("index-debug", ["less", "scripts-debug"], function () {
    return gulp.src("index.html")
        .pipe(inject(series(gulp.src(["css/*.css"]), gulp.src(config.lib).pipe(gulp.dest("./lib")), gulp.src(config.src)).pipe(debug())))
        .pipe(gulp.dest("."));
});

// Release tasks
gulp.task("scripts-release", ["clean"], function () {
    return gulp.src(config.lib.concat(config.src))
      .pipe(debug())
      .pipe(concat("all.js"))
      .pipe(gulp.dest("./dist"));
});


gulp.task("css-release", ["less"], function () {
    return gulp.src(["css/*.css"])
    .pipe(debug({title: "css-release"}))
    .pipe(concat("all.css"))
    .pipe(gulp.dest("./dist"));
});


gulp.task("index-release", ["css-release", "scripts-release"], function () {
    return gulp.src("index.html")
        .pipe(inject(gulp.src(["dist/all.css", "dist/all.js"])))
        .pipe(gulp.dest("."));
});

//Set a default tasks
gulp.task("Debug", ["fonts", "scripts-debug", "less", "index-debug"], function () { });
gulp.task("Test", ["fonts", "scripts-release", "css-release", "index-release"], function () { });
gulp.task("Release", ["fonts", "scripts-release", "css-release", "index-release"], function () { });

