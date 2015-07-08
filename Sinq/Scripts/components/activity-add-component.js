ko.components.register('activity-add', {
    viewModel: function(params) {
        var self = this;
        self.SelectedFolder = params.SelectedFolder || ko.observable(new Folder({}));
        self.NewActivity = ko.observable(new Activity({}));

        // The function adds a new activity
        self.fnAddNewActivity=self.SelectedFolder (function (folder) {
            $.ajax({
                url: "/api/folders/" + folder.id + "/activities",
                method: "Post",
                async: false,
                data: {
                    Id: -1,
                    Name: self.NewActivity().name(),
                    DueDate: undefined
                }
            }).done(function (result) {
                alert(result)
            })
        })

    },
    template: { fromFileType: 'html' }
});
