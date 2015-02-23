function HomeViewModel(app, dataModel) {
    var self = this;

    // Private operations
    function initialize() {
        dataModel.getNotes().success(function (data) {
            self.notes(data);
        });
    }

    // Data
    self.notes = ko.observableArray();

    initialize();
}

app.addViewModel({
    name: "Home",
    bindingMemberName: "home",
    factory: HomeViewModel
});
