ko.components.register('activity-add', {
    viewModel: function(params) {
        var self = this;
        self.SelectedFolder = params.SelectedFolder || ko.observable(new Folder({}));
        self.NewActivity = ko.observable(new Activity({}));

        self.SelectedFolder.subscribe(function (newFolder) {
            //alert(newFolder.Name());
        })

        // The function adds a new activity
        self.SelectedFolder.fnAddNewActivity (function (folder) {
            $.ajax({
                url: "/api/folder" + folder.id + "/activities",
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
        })

    },
    template: { fromFileType: 'html' }
});
