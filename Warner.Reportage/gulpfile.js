var gulp = require("gulp"),
    fs = require("fs"),
    less = require("gulp-less");

gulp.task("default", function () {
    return gulp.src("Styles/main.less")
        .pipe(less())
        .pipe(gulp.dest("wwwroot/css"));
});