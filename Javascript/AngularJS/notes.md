Angular fundamentals
====================


Introduction
============
What?
-----
* Opinionated MV* framework
* DOM manipulation: preferrably in directives (you can use jQuery there if you want).
* SRP, **Seperation of concerns** via built-in dependency injection
* This also makes it very **testable**: Unit + e2e testing

Architecture
------------
* Two-way data binding
* Dirty checking on POJOs
* Dependency injection (testability!)

Components:

* **Controllers**: responsible for building the $scope object
* **Model**: data in the scope
* **Views**: rendering, templating, directives
* **Services**: bussiness logic + server communication

Scaffolding
------------
* Angular seed
* **Yeoman** as a valid (more heavyweight) alternative
* var app = **angular.module**('appName', [dependecncies]);
 
Controllers & Markup
====================

Expressions
===========
* This is **a subset of javascript**

Directives
==========
* Applied as elements (bad practice, since old IE versions blow up), attributes (just use this approach...) or classes
* As HTML comments (?)
* ngCloack: hide the initial {{flash}}
* ngStyle, ngClass, ngClassEven & ngClassOdd

Custom Directives
=================
* HTTP was meant for static data
* HTML5 web components or angular **directives** more easily support dynamic pages.
* With directives you can: **inject HTML**, respond to **events**, **observe** data.
* **template** & **templateUrl** can be used to generate simple html.
* **link** performs actual directive logic, if you need more logic than a simple template compilatino.
* **restrict** limits the scope where directives can be applied: **A**ttribute = default, **E**lement,...
* Allows you to use a **DSL** inside your HTML!
* Custom directives may result in **invalid html**, but you can instruct angular to replace invalid tags by native html using **replace**
* You can **isolate scope** between different directive instantiations.
* Isolated scope: **&** : evaluate **function** in parent scope, ** = **: evaluate **value** in parent scope, **@**: expect a string, don't evaluate anything, accept as **string**.
* You can create self-contained directives with their own controllers using the **controller** property
* The **link** property allows you to define custom logic inside a directive. It's called for every directive instance
* Directives can communicate through shared controllers using the **require** property.
* You can control the **order** in which directives are executed using the **priority** and **terminal** (stop after this directive) properties. Watch out when using terminal, this also affects **built-in directives**! You can work around this by using **negative** priorities (built-ins have priority 0)
* You can **nest** directives with require: "**^**controller" (to share controllers) and **transclude** (to support inner html/directives)
* Do heavy DOM manipulation (inserts) with the **compile** property, this can return a link function for further scope processing etc. per directive
* You **can use jQuery (and plugins)** inside directives, you can even make jQuery more expressive and encapsulated by using custom directives!

Filters
=======
* Acts as a 'filter' **and** a 'map'
* Formatting, sorting, modifying output (lowercase), filter out elements
* **JSON filter** is handy for debugging!
* The 'filter' by default searches through the entire objects. If you want to narrow your search in specific fields, use the **dot** operator. This will force angular to match the query object with the searched objects.

Two-way binding
===============
* Only works with Input, Select and Textarea elements (i.e. all standard controls that receive user input)
* When referencing a property in **ngModel**, it will **automatically get created** if it does not exist

Validation
==========
* HTML5 **required** attribute: angular also marks the field/form as invalid, this can be used for real-time form styling!
* **ngPattern**: tricky regexes
* Needs **ngModel** to work correctly!
* Nice in combination with **ngDisabled**: visual feedback on when a user can submit a form.
* **CSS** Styling: .ng-dirty .ng-(in)valid .ng-(in)valid-required
* Form needs a **name** if you want to check validity in the controller!

Services
========
* I.e. **worker object** that encapsulates a certain business operation (like DDD services)
* **Built-in** services: navigation, interaction
* Easy to create **custom** services: app.services(...)
* **$scope** is an angular service!
* DI in angular: specify the dependencies you want in the controller/filter/directive function's argument.
* Created using the **app.factory** function
* Both data services and API's

Built-in services
-----------------
* **$-prefix**. Do **not** use dollar prefixes for your own custom services, as you might override angular's built-in services! (Exceptions are when you want to override built-in services, e.g. $exceptionHandler).

###$log###
* Used for debugging: $log.warn('blabla'); outputs to console!
* log, info, warn & error

###$http###
* HTTP calls (AJAX calls with callbacks, very bare-metal)

###$q###
* **promise library**: no more callback hell!
* Elegent way to **structure your 'callbacks'** (promise chaining, etc.)
* Create a promise, set the resolve, reject and notify actions, then return that promise (no callbacks have to be passed in here!)
* When you have a reference to the promise, use the .then() function to inject in functions (see examples to more clearly illustrate this).
* $q is the reason you should **always pass in objects in $scope methods** instead of accessing the $scope directly in the controller: $scope may contain promises instead of the actual values!
* Angular binds to the objects behind promises without extra syntax.

###$resource###
* Neatly expose a RESTful API
* Needs extra .js file + injection!!
* **Synchronous is default**, can be combined with $q
* not only GET's: **saving** data is also nicely supported with $resource
* GET, SAVE, QUERY (array), REMOVE & DELETE
* Custom actions

###$locale###
* Exactly like c-Quilibrium implements it: include a resource file dependend of the user's language.

###Other useful built-in services###
* **$cacheFactory**: has a capacity & put/get for key-value pairs, FIFO cache
* **$compile**: typically used inside directives. Compiles (processes directives) html in a certain scope. Can process bindings, directives,... etc.
* $timeout: js.setTimeout does not play nicely with angular model observers, this is a workaround for that.
* $exceptionHandler: override exception handling (e.g. for server-side logging).
* $parse: parse statements in certain contexts
* $cookieStore
* $window and $document -> exposed as services, provide a seam to easily test DOM manipulation code. Most likely you can do what you want with directives instead of direct DOM manipulation!

DOM manipulation
================
* support for selectors without jQuery: **angular.element** (should only use this from within directives)

Routing
=======
* Helps to get to the **single page app** architecture
* To handle common pains: include .js/.css on every page
* **ng-view** element
* **$routeProvider**: for certain **URL's**, provide a **template (or URL)** and a **controller** for context. This service can be used in the .config block of your application's main module.
* Only a single hit to fetch a template, default behavior of Angular's **template cache**
* Supports **bookmarkability** and **navigation history**, you can copy paste URL's, and angular's **bootstrapping** mechanism will automatically fetch and parse templates.
* default route with $routeProvider.otherwise
* **$routeParams** gives access to arguments encoded in the URL.
* You can use **$route** in controllers to access custom route information
* Support **html5** routing using the **$locationProvider** to avoid using "#" in your routable url's.
* Angular handles **client-side** routing, i.e. if you fresh navigate to a page say localhost/events/1, the server does not know what to do with it! You could just return index.html and let angular handle stuff client-side in this case.
* **resolve** property: fix wonky page loads, e.g. when you have expensive data calls (i.e. not real-time results), the view will get rendered before the data is available. Resolve allows you to inject promises that handle slow-loading pages.
* **$location** service: instead of URL's in a tags, you can navigate using the $location service. You can also find all kinds of relevant information on the $location service.

Testing
=======
* QUnit, Mocha, **Jasmine**
* Test runner: **Karma**
* Mocking: **sinon**
* When setting up a new spec, don't forget to specify the angular module where your artifact resides: **beforeEach(module('myApp'));**
* Testing **directives** is a real PITA (certainly when using templateUrl): changes in karma, checking results in DOM,...

###Karma###
* **Debugging** with karma sucks (breakpoints don't always work flawlessly), instructor advises you to sprinkle your code with console.logs
* to get started, run **karma init**
* If you get **can't find variable: JASMINE** errors, you might want to exclude the karma config files!

###End-to-end testing###
* Karma is being replaced by **protractor**

Impressions
===========
* **DOM manipulation** from controllers/services is a big nono, directives are the place to put this. For example, conditionally disabling buttons is supported out-of-the-box with ngDisable
* cQ javascript framework has a lot of **implicit** conventions (class names, ids that have to match in both JS and HTML). I like how custom angular directives can make these conventions **explicit** and **enforceable**

Troubleshooting
===============

###My app does not seem to do any Angular-related stuff
* include the angular **javascript** file. Make sure you have an **ng-app** directive somewhere on the page.

###Input validation goes wonky###
* **ngModel** is required on the input fields (otherwise the form will always be valid). The form  needs a name for validity checking in the controllers etc. I haven't got JS regexes down, seeing that I can't get **ngPattern** to behave correctly...

###Databinding is not kicking off###
* Databinding with ngModel only works on INPUT elements (textarea, select, input).
* If you simply want to render data, use the **{{property}}** syntax instead!