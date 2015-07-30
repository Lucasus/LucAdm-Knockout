var gulp = require('gulp');
var gulpWebpack = require('gulp-webpack');
var webpack = require('webpack');
var path = require('path');


gulp.task('default', function () {
    return gulp.src(['src/pageA.js', 'src/pageB.js', 'src/shared.js', 'src/common.js'])
      .pipe(gulpWebpack({
          entry: {
              pageA: "./src/pageA",
              pageB: "./src/pageB"
          },
          output: {
              filename: '[name].js',
              publicPath: "./assets/",
          },
          plugins: [
              new webpack.optimize.CommonsChunkPlugin("commons.js"),
              new webpack.optimize.DedupePlugin()
          ]

      }))
      .pipe(gulp.dest('assets/'));
});