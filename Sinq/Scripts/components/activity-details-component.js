ko.components.register('activity-details', {
    viewModel: function (params) {
        var self = this;
        self.SelectedActivity = params.SelectedActivity || ko.observable(new Activity({}));
        self.ShowDetails = ko.observable();
        self.DisplayActivityDetails = ko.observable(false);

        self.SelectedActivity.subscribe(function (newSelectedActivity) {
            self.fnShowActivityDetails();
        })

        self.fnShowActivityDetails = function () {
            self.DisplayActivityDetails(true);
        }

        //The function removes an activity
        self.fnRemoveActivity = function () {
            $.ajax({
                url: "/api/activities/" + self.SelectedActivity().id,
                method: "Delete",
                async: false,
            }).done(function (result) {
            })
        }
    },
    template: { fromFileType: 'html' }
});