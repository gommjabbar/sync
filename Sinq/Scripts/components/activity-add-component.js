ko.components.register('activity-add', {
    viewModel: function(params) {
        var self = this;
        self.SelectedFolder = params.SelectedFolder || ko.observable(new Folder({}));
        self.NewActivity = ko.observable(new Activity({}));

        // The function adds a new activity
        self.fnAddNewActivity = function () {
            var route = "/api/folders/" + self.SelectedFolder().id + "/activities";
            var data = {
                Id: -1,
                Name: self.NewActivity().Name,
                DueDate: undefined
            }
            $.ajax({url: route,method: "Post",async: false,data: data
            }).done(function (result) {
                //alert(result)
                self.NewActivity().Name('');


            })
        }

    },
    template: { fromFileType: 'html' }
});
