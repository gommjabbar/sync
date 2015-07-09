ko.components.register('activity-list', {
    viewModel: function(params) {
        var self = this;
        self.SelectedFolder = params.SelectedFolder || ko.observable(new Folder({}));
        self.SelectedActivity = params.SelectedActivity || ko.observable(new Activity({}));
        self.AllActivities = ko.observableArray();
        self.Completed = ko.observable(Activity.Completed || '');
        self.ShowActivityList = ko.observable();
       

        //The function gets the list of all activities from a selected folder
        self.fnShowActivitiesFromFolder = function (folder) {
          //  self.ShowFolderActivities() = folder;
            alert('test')
            self.DisplayFolderActivities(true);
            self.fnGetAllActivities();
        }
        self.fnShowActivitiesFromFolder();

        self.fnSelectActivity = function (activity) {
            self.SelectedActivity(activity);
        }

        //The function get the list of all activities
        self.fnGetAllActivities = function (folder) {
            $.getJSON("/api/folders/" + folder.id+"/activities", function (data) {
                var resultArray = $.map(data.result, function (value) {
    
                    return new Activity(value);
                })
                debugger;
    
                self.AllActivities(resultArray);
            })
        }
           // self.fnGetAllActivities();

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
