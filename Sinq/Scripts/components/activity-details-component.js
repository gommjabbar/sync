﻿ko.components.register('activity-details', {
    viewModel: function (params) {
        var self = this;
        self.SelectedActivity = params.SelectedActivity || ko.observable(new Activity({}));

        //The function removes an activity
        self.fnRemoveActivity = function (activity) {
            $.ajax({
                url: "/api/activities/" + activity.id,
                method: "Delete",
                async: false,
            }).done(function (result) {
            })
        }
    },
    template: { fromFileType: 'html' }
});