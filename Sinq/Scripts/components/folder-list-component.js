ko.components.register('folder-list', {
    viewModel: function(params) {
        var self = this;

        self.NewFolder = ko.observable(new Folder({}));
        self.AllFolders = ko.observableArray();

        self.fnGetAllFolders = function () {
            $.getJSON("api/folders", function (data) {
                var resultArray = $.map(data.result, function (value) {

                    return new Folder(value);
                })
                self.AllFolders(resultArray);
            })
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
                url: "/api/folders/" + folder.id,
                method: "Delete",
                async: true,
            }).done(function (result) {
                alert(result)
                self.fnGetAllFolders();
            })
        }
        self.fnGetAllFolders();
    },
    template: { fromFileType: 'html' }
});
