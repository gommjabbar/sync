ko.components.register('activity-list', {
    viewModel: function(params) {
        var self = this;
        self.SelectedFolder = params.SelectedFolder || ko.observable(new Folder({}));
        self.AllActivities = ko.observableArray();
        self.Completed = ko.observable(Activity.Completed || '');
        self.ShowActivityList = ko.observable();
        

       // self.SelectedFolder.subscribe(function (newFolder) {
       //     alert(newFolder.Name());
       // })

        //The function get the list of all activities
        self.fnGetAllActivities=self.SelectedFolder(function (folder) {
            $.getJSON("/api/folder/" + folder.id+"/activities", function (data) {
                var resultArray = $.map(data.result, function (value) {
    
                    return new Activity(value);
                })
                debugger;
    
                self.AllActivities(resultArray);
            })
        })
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
