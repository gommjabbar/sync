
function Folder(Folder) {
    var self = this;
    self.id = Folder.Id;
    self.Name = ko.observable(Folder.Name || '');
    self.FolderActivities = ko.observable(Folder.Activities);
}

function Activity(Activity) {
    var self = this;
    self.id = Activity.Id;
    self.Completed = ko.observable(Activity.Completed || '');
    self.CompletedAt = ko.observable(Activity.CompletedAt || '');
    self.Name = ko.observable(Activity.Name || '');
    self.DueDate = ko.observable(Activity.DueDate || undefined);
    self.ActivityTimes = ko.observableArray(Activity.ActivityTimes || []);
    self.placeholderText = Activity.placeholderText || 'Type the name of the activity';
    self.ElaspedTime = ko.observable(Activity.ElaspedTime || '');
    self.ActivityState = ko.computed(function () { return Activity.IsStarted });
}




function ActivityOverviewVM() {
    var self = this;
    self.NewActivity = ko.observable(new Activity({}));
    self.AllActivities = ko.observableArray();
    self.ShownActivity = ko.observable();
    self.DisplayActivity = ko.observable(false);
    self.ShowActivitiesFromFolder = ko.observable();
    self.ShowFolderActivities = ko.observable();
    self.DisplayFolderActivities = ko.observable(false);
    self.SelectedFolder = ko.observable();
    self.SelectedActivity = ko.observable();

    self.SelectedFolder.subscribe(function (newFolder) {
        //alert(newFolder.Name());
    })
   


   

    


    


    


    //The function makes visible the details of a selected activity
    self.fnShowActivity = function (activity) {
        self.ShownActivity() = activity;
        self.DisplayActivity(true);

    }

   
    
    

    //The function gets the list of all activities from a selected folder
    self.fnShowActivitiesFromFolder = function (folder) {        
        //  self.ShowFolderActivities() = folder;
        alert('test')
        self.DisplayFolderActivities(true);
        self.fnGetAllActivities();
    }

    
    

    

}
ko.applyBindings(new ActivityOverviewVM());

$('#datetimepicker').datetimepicker();
$('#Name').attr("placeholder", "Type activity name here");


//Used for the click envent

// weird javascript code
//function test() { return { prop: "asd" }};
//test['a'] = function () { alert(3) } ;
//test['a']()
//test.a();
//alert(test().prop);


