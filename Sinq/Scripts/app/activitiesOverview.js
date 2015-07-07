
function Folder(Folder) {
    var self = this;
    self.id = Folder.Id;
    self.Name = ko.observable(Folder.Name || '');
}

function Activity(Activity) {
    var self = this;
    self.id = Activity.Id;
    self.Completed = ko.observable(Activity.Completed || '');
    self.CompletedAt = ko.observable(Activity.CompletedAt || '');
    self.Name = ko.observable(Activity.Name || '');
    self.DueDate = ko.observable(Activity.DueDate || undefined);
    self.ActivityTimes = ko.observableArray(Activity.ActivityTimes || []);
    self.placeholderText = Activity.placeholderText || 'Type the name of the activity';
    self.ElaspedTime = ko.observable(Activity.ElaspedTime || '');
    self.ActivityState = ko.computed(function () { return Activity.IsStarted });
}




function ActivityOverviewVM() {
    var self = this;
    self.NewActivity = ko.observable(new Activity({}));
    self.AllActivities = ko.observableArray();
    self.ShownActivity = ko.observable();
    self.DisplayActivity = ko.observable(false);
    self.AllFolders = ko.observableArray();
    self.ShowActivitiesFromFolder = ko.observable();
    self.ShowFolderActivities = ko.observable();
    self.DisplayFolderActivities = ko.observable(false);
    self.NewFolder  = ko.observable(new Folder({}));

    // The function adds a new activity
    self.fnAddNewActivity = function () {
        $.ajax({
            url: "/api/activities",
            method: "POST",
            async: false,
            data: {
                Id: -1,
                Name: self.NewActivity().name(),
                DueDate: undefined
            }
        }).done(function (result) {
            alert(result)
        })
    }


    //The function get the list of all activities
    self.fnGetAllActivities = function () {
        $.getJSON("/api/activities", function (data) {
            var resultArray = $.map(data.result, function (value) {

                return new Activity(value);
            })
            debugger;

            self.AllActivities(resultArray);
        })
    }
    //    self.fnGetAllActivities();


    //The function removes an activity
    self.fnRemoveActivity = function (id) {
        $.ajax({
            url: "/api/activities/" + id,
            method: "Delete",
            async: false,
        }).done(function (result) {
        })
    }


    //The function changes the 'Completed' proprety of an action
    self.fnCompleteActivity = function (activity) {
        var url = "/api/activities/" + activity.id() + "/complete";
        $.ajax({
            url: url,
            method: "Put",
            async: false,
        }).done(function (result) {
            if (sefl.complet(!self.complet())) {
            }
        })
    }


    //The function changes the current state of an action 
    self.fnChangeState = function (activity) {
        /* var url = "/api/activities/" + activity.id + "/start";
         $.ajax({
             url: url,
             method:"Put",
             async: false,
         }).done(function (result) {
             if (result != true) {
                 ("ActivityState").val("Start");
                 var url = "/api/activities/" + activity.id + "/stop";
                 $.ajax({
                     url: url,
                     method: "Put",
                     async: false,
                 }).done(function (result) { })

             } else {
                 ("ActivityState").val("Stop");
             }
         })*/
        if (activity.ActivityState == true) {
            var url = "/api/activities/" + activity.id + "/start";
            $.ajax({
                url: url,
                method: "Put",
                async: false,
            }).done(function (result) { })
        } else {
            var url = "/api/activities/" + activity.id + "/stop";
            $.ajax({
                url: url,
                method: "Put",
                async: false,
            }).done(function (result) { })
        }
    }


    //The function makes visible the details of a selected activity
    self.fnShowActivity = function (activity) {
        self.ShownActivity() = activity;
        self.DisplayActivity(true);

    }

    //The function gets the list of all folders
    self.fnGetAllFolders = function () {
        $.getJSON("api/folders", function (data) {
            var resultArray = $.map(data.result, function (value) {

                return new Folder(value);
            })
            self.AllFolders(resultArray);
        })
    }
    self.fnGetAllFolders();

    //The function gets the list of all activities from a selected folder
    self.fnShowActivitiesFromFolder = function (folder) {
        self.ShowFolderActivities() = folder;
        self.DisplayFolderActivities(true);
    }

    //The function adds a new folder
    self.fnAddNewFolder = function () {
        //debugger;
        $.ajax({
            url: "api/folders",
            method: "Post",
            async: false,
            data: {
                Id: -1,
                Name: self.NewFolder().Name
            }
        }).done(function (result) {
            self.fnGetAllFolders();
        })
    }

    //The function deletes a folder
    self.fnDeleteFolder = function (folder) {
        $.ajax({
            url: "/api/folders/" + folder.idF,
            method: "FolderDelete",
            async: false,
        }).done(function (result) {
        })
    }

}
ko.applyBindings(new ActivityOverviewVM());

$('#datetimepicker').datetimepicker();
$('#Name').attr("placeholder", "Type activity name here");


//Used for the click envent
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


// weird javascript code
//function test() { return { prop: "asd" }};
//test['a'] = function () { alert(3) } ;
//test['a']()
//test.a();
//alert(test().prop);


