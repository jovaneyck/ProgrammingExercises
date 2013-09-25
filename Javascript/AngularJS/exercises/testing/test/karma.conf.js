// Karma configuration
// Generated on Wed Sep 18 2013 09:36:53 GMT+0200 (Romance Daylight Time)


// base path, that will be used to resolve files and exclude
basePath = '../src/';


// list of files / patterns to load in the browser
files = [
  JASMINE,
  JASMINE_ADAPTER,
  '../../lib/jquery-2.0.3.min.js', //when testing directives, jQuery is useful for selection/event triggering
  '../../lib/angular.min.js', //angular source
  '../../lib/angular-*.js',
  '../lib/angular-mocks.js', //make angular testable
  '**/*.js', //code under test
  '../test/*.js',  //test code
  'templates/*.html' //include external templates in Karma!!!
];

//Relative path must match the relative path defined in the directive in both the files and the preprocessors config!
/*Add a preprocessor for external templates*/
preprocessors = {
	'templates/*.html' : 'html2js'
};

// list of files to exclude
exclude = [
  '../test/karma.conf.js' //exclude karma file itself
];


// test results reporter to use
// possible values: 'dots', 'progress', 'junit'
reporters = ['progress'];


// web server port
port = 9876;


// cli runner port
runnerPort = 9100;


// enable / disable colors in the output (reporters and logs)
colors = true;


// level of logging
// possible values: LOG_DISABLE || LOG_ERROR || LOG_WARN || LOG_INFO || LOG_DEBUG
logLevel = LOG_INFO;


// enable / disable watching file and executing tests whenever any file changes
autoWatch = true;


// Start these browsers, currently available:
// - Chrome
// - ChromeCanary
// - Firefox
// - Opera
// - Safari (only Mac)
// - PhantomJS
// - IE (only Windows)
browsers = ['PhantomJS'];


// If browser does not capture in given timeout [ms], kill it
captureTimeout = 60000;


// Continuous Integration mode
// if true, it capture browsers, run tests and exit
singleRun = false;
