ko.components.register('folder-list', {
    viewModel: function(params) {
        var self = this;
        self.SelectedFolder = params.SelectedFolder || ko.observable(new Folder({}));
        self.UnSelectedFolder = params.UnSelectedFolder || ko.observable(new Folder({}));
        self.NewFolder = ko.observable(new Folder({}));
        self.AllFolders = ko.observableArray();

        //The function gets the list of all folders
        self.fnGetAllFolders = function () {
            $.getJSON("/api/folders", function (data) {
                var resultArray = $.map(data.result, function (value) {
                    return new Folder(value);
                })
                if (resultArray.length > 0){
                    self.SelectedFolder(resultArray[0]);
                }
                self.AllFolders(resultArray);
            })
        }

        self.fnSelectFolder = function (folder) {
            folder.ShowDelete(true);
            self.SelectedFolder(folder);
        }

        self.fnUnSelectFolder = function (folder) {
            folder.ShowDelete(false);
            self.SelectedFolder(folder);
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
                self.NewFolder().Name('');
                self.fnGetAllFolders();
            })
        }




        //The function that clear from textbox - Add button for folders

        //The function deletes a folder
        self.fnDeleteFolder = function (folder) {
            $.ajax({
                url: "/api/folders/" + folder.id,
                method: "Delete",
                async: true,
            }).done(function (result) {
                self.fnGetAllFolders();
            })
        }
        self.fnGetAllFolders();
    },
    template: { fromFileType: 'html' }
});
