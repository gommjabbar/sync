﻿ko.components.register('activity-list', {
    viewModel: function(params) {
        var self = this;
        self.SelectedFolder = params.SelectedFolder || ko.observable(new Folder({}));
        self.SelectedActivity = params.SelectedActivity || ko.observable(new Activity({}));
        self.AllActivities = ko.observableArray();
        // self.Completed = ko.observable(SelectedActivity().Completed || '');
        self.Completed = ko.observable();
        self.ShowActivityList = ko.observable(false);
        self.ShowFolderActivities = ko.observable(new Folder({}));
        self.AllCompletedActivities = ko.observableArray();
       
        //The function gets the list of activities from the selected folder and makes it visible
        self.SelectedFolder.subscribe(function (newSelectedFolder) {
            self.fnShowActivitiesFromFolder();
            

        })

        //The function gets the list of all uncompleted activities from a selected folder
        self.fnShowActivitiesFromFolder = function () {
            self.ShowActivityList(true);
            self.fnGetAllActivities();

        }
        

        //Select an activity
        self.fnSelectActivity = function (activity) {
            //self.SelectedActivity(activity);
            activity.ShowStart(true);
           // self.SelectedFolder().ShowDelete(false);
            self.SelectedActivity(activity);
        }


        //The function get the list of all uncompleted activities
        self.fnGetAllActivities = function () {
            //service.get("api/folders/{folderId}/activities",{folderId:self.SelectedFolder().id}, { completed: false});
            var url = "/api/folders/" + self.SelectedFolder().id + "/activities";
            $.ajax({
                url: url,
                method: "GET",
                async: false,
                data: {completed: false}
            }).done(function (data) {
                var resultArray = $.map(data.result, function (value) {
                    return new Activity(value);
                })
                //alert(resultArray);
                //debugger;
                self.AllActivities(resultArray);
            })
        }
       
        //The function get the list of all completed activities
        self.fnSelectCompletedActivity = function () {
            var url = "/api/folders/" + self.SelectedFolder().id + "/activities/" + self.SelectedActivity().id;
            $.ajax({
                url: url,
                method: "Get",
                async: false,
                data: {completed: true}
            }).done(function (data) {
                var resultArray=$.map(data.result,function(value){
                    return new Activity(value);
                })
                self.AllCompletedActivities(resultArray);
            })
        }

        //The function changes the 'Completed' proprety of an action
        self.fnCompleteActivity = function (activityParam) {
            debugger;
            var url = "api/folders/" + self.SelectedFolder().id + "/activities/" + activityParam.id + "/complete"
            //var url = "/api/activities/" +self.SelectedActivity().id + "/complete";
            $.ajax({
                url: url,
                method: "Put",
                async: false,
            }).done(function (result) {
                if (activityParam.Completed(!activityParam.Completed())) {
                }
            })
        }

        //The function changes the current state of an action 
        self.fnChangeState = function () {
            if (SelectedActivity().ActivityState == true) {
                var url = "/api/activities/" + SelectedActivity().id + "/start";
                $.ajax({
                    url: url,
                    method: "Put",
                    async: false,
                }).done(function (result) { })
            } else {
                var url = "/api/activities/" + SelectedActivity().id + "/stop";
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
