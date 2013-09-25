Twitter bootstrap
=================

Introduction
------------

###Core concepts
* Semantic class names: "warning" instead of "redtext"
* Convention: magic happens when HTML is structured in a certain way (e.g: pagination)

Layout
------
* CSS **box model**: everything is a rectangle with padding (inner), border and margin (outer)

### Grid system
* every "row" has 12 "columns". You can give a column a certain relative width by using the **col-xs-X** classes. If you do not want your columns to spill to the next line, wrap your rows in a **container**

Components
----------

* Expect a certain HTML structure + classes + data-attributes


Impressions
-----------

* Very low-level, you're basically inlining styling into your HTML again? You can combat this using **LESS**, which allows you to re-use classnames in css
* Responsive design! Perhaps way to go for desktop/mobile combinations?
* Bootstrap 3 is very mobile minded, I don't want to see col-xs-3 in my HTML...
* Okay for proof-of-concepts (rapid results), perhaps less so for long-term projects?