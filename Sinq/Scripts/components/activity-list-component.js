ko.components.register('activity-list', {
    viewModel: function(params) {
        var self = this;
        self.SelectedFolder = params.SelectedFolder || ko.observable(new Folder({}));
        self.SelectedActivity = params.SelectedActivity || ko.observable(new Activity({}));
        self.AllActivities = ko.observableArray();
        self.Completed = ko.observable(Activity.Completed || '');
        self.ShowActivityList = ko.observable();
        self.ShowFolderActivities = ko.observable(new Folder({}));
        self.DisplayFolderActivities = ko.observable(false);
       
        self.SelectedFolder.subscribe(function (newSelectedFolder) {
            self.fnShowActivitiesFromFolder();
        })
        //The function gets the list of all activities from a selected folder
        self.fnShowActivitiesFromFolder = function () {
            self.DisplayFolderActivities(true);
            self.fnGetAllActivities();
        }
        

        self.fnSelectActivity = function (activity) {
            self.SelectedActivity(activity);
        }

        //The function get the list of all activities
        self.fnGetAllActivities = function () {
            var url = "/api/folders/" + self.SelectedFolder().id + "/activities";//"/api/f/" + self.SelectedFolder().id + "/act"
            $.ajax({
                url: url,
                method:"GET",
                data: {completed: false}
            }).done(function (data) {
                var resultArray = $.map(data.result, function (value) {
    
                    return new Activity(value);
                })
               debugger;
    
                self.AllActivities(resultArray);
            })
        }
        //self.fnGetAllActivities();

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
    },
    template: { fromFileType: 'html' }
});
