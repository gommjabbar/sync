ko.bindingHandlers.singleOrDoubleClick = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        var singleHandler = valueAccessor().click,
            doubleHandler = valueAccessor().dblclick,
            delay = valueAccessor().delay || 200,
            clicks = 0;

        $(element).click(function (event) {
            clicks++;
            if (clicks === 1) {
                setTimeout(function () {
                    if (clicks === 1) {
                        // Call the single click handler - passing viewModel as this 'this' object
                        // you may want to pass 'this' explicitly
                        if (singleHandler !== undefined) {
                            singleHandler.call(viewModel, bindingContext.$data, event);
                        }
                    } else {
                        // Call the double click handler - passing viewModel as this 'this' object
                        // you may want to pass 'this' explicitly
                        if (doubleHandler !== undefined) {
                            doubleHandler.call(viewModel, bindingContext.$data, event);
                        }
                    }
                    clicks = 0;
                }, delay);
            }
        });
    }
};


var fromHtmlTemplateLoader = {
    loadTemplate: function (name, templateConfig, callback) {
        if (templateConfig.fromFileType) {

            var dashedComponentName = name.replace(/([a-z])([A-Z])/g, '$1-$2').toLowerCase();
            var templateFolder = '/templates/' + dashedComponentName.split('-')[0] + '/';

            // Uses jQuery's ajax facility to load the markup from a file
            var fullUrl = templateFolder + dashedComponentName + '-template.' + templateConfig.fromFileType;
            $.get(fullUrl, function (markupString) {
                // We need an array of DOM nodes, not a string.
                // We can use the default loader to convert to the
                // required format.
                ko.components.defaultLoader.loadTemplate(name, markupString, callback);
            });
        } else {
            // Unrecognized config format. Let another loader handle it.
            callback(null);
        }
    }
};

// Register it
ko.components.loaders.unshift(fromHtmlTemplateLoader);
