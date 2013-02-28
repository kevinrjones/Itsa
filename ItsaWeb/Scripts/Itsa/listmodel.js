

function ListModel(params) {

    var listmodel = this;

    listmodel.cardinalityFormatter = params.cardinalityFormatter;


    listmodel.itemsloaded = ko.observable(false);

    listmodel._items = ko.observableArray();
    listmodel.items = ko.computed(
        {
            read: function () { return listmodel._items(); },
            write: function (value) {
                listmodel.itemsloaded(true);
                listmodel._items(value);
            }
        });

    listmodel.any = ko.computed(function () {
        return listmodel.items().length > 0;
    });

    listmodel.empty = ko.computed(function () {
        return listmodel.items().length == 0;
    });

    listmodel.checked = ko.computed(function () {

        var result = [];
        ko.utils.arrayForEach(listmodel.items(), function (item) {
            if (item.isChecked())
                result.push(item);
        });
        return result;

    });

    listmodel.checked.subscribe(function () {
        var checkedItems = listmodel.checked();

        if (listmodel.itemSelecting) {
            listmodel.lastCheckedItem = null;
        }
        else {
            if (checkedItems.length == 1) {
                listmodel.lastCheckedItem = checkedItems[0];
            } else if (checkedItems.length == 0) {
                if (listmodel.lastCheckedItem) {
                    var indexToSelect = listmodel._items.indexOf(listmodel.lastCheckedItem);
                    if (indexToSelect != null) {
                        listmodel._selected(listmodel.lastCheckedItem);
                    }
                }
            }
        }
    });

    listmodel.selectedOrChecked = ko.computed(function () {

        var result = [];
        ko.utils.arrayForEach(listmodel.items(), function (item) {
            if (item.isSelectedOrChecked())
                result.push(item);
        });
        return result;

    });

    listmodel.anyChecked = ko.computed(function () {
        return listmodel.checked().length > 0;
    });

    listmodel.anySelectedOrChecked = ko.computed(function () {
        return listmodel.selectedOrChecked().length > 0;
    });

    listmodel.selectedOrCheckedCardinality = ko.computed(function () {
        return listmodel.cardinalityFormatter(listmodel.selectedOrChecked().length);
    });
    
    listmodel.dontShowLoadingMessage = ko.observable(false);

    listmodel.selectedOrCheckedCardinalityWithLabel = ko.computed(function () {
        if (listmodel.dontShowLoadingMessage()) {
            return;
        }
        if (!listmodel.itemsloaded())
            return resources.res('Resources.LoadingByCalendar') + listmodel.cardinalityFormatter('')+'...';

        if (listmodel.items().length == 0)
            return resources.res('Resources.ThereAre') + listmodel.cardinalityFormatter(resources.res('Resources.no'));
        return listmodel.cardinalityFormatter(listmodel.selectedOrChecked().length) + resources.res('Resources.selected');
    });


    listmodel.allChecked = ko.computed({
        read: function () {
            var items = listmodel.items();
            for (var i = 0; i < items.length; i++) {
                var item = items[i];
                if (!item.isChecked()) {
                    return false;
                }
            }
            if (items.length == 0) return false;
            return true;
        },
        write: function (value) {
            var items = listmodel.items();
            for (var i = 0; i < items.length; i++) {
                var item = items[i];
                item.isChecked(value);
            }
        }
    });

    listmodel.itemSelecting = false;

    listmodel._selected = ko.observable(null);

    listmodel.selected = ko.computed(
        {
            read: function () {
                return listmodel._selected();
            },

            write: function (value) {
                listmodel.itemSelecting = true;
                listmodel.selectedOpacity(0.2);
                listmodel._selected(value);
                
                ko.utils.arrayForEach(listmodel.items(), function (item) {
                    item._isChecked(false);
                });
                listmodel.increaseSelectedOpacity();
                listmodel.itemSelecting = false;

            }
        }
    );

    listmodel.selectedIndex = function() {
        var index = 0;
        var isselected = false;
        $(listmodel.items()).each(function (ndx, item) {
            if (listmodel.selected() == item) {
                return false;
            }
            index++;
        });
        if (index < listmodel.items().length) {
            return index;
        } else {
            return -1;
        }
    };

    listmodel.selectByIndex = function(index) {
        $(listmodel.items()).each(function (ndx, item) {
            if (ndx == index) {
                listmodel.selected(item);
                return false;
            }
        });
    };

    // the "bulk" item that represents to common state of all "checked" list items
    listmodel.bulk = ko.observable(null);

    // set up monitoring of a specific set of fields to keep the "bulk" item in sync
    listmodel.monitorBulk = function (bulkItem, fieldNames) {

        listmodel.bulk(bulkItem);

        // whenever the list of "checked" itmes changes...
        listmodel.checked.subscribe(function () {

            // for each field we're monitoring:
            for (var fieldNameIndex in fieldNames) {
                // IE8 fix

                var fieldName = fieldNames[fieldNameIndex];

                if (typeof (fieldName) != "function") {
                    // get all the checked items
                    var checkedItems = listmodel.checked();

                    // are all values identical so far? (set to false if there are no items)
                    var valuesIdentical = checkedItems.length > 0;

                    // track the common value, if any
                    var bulkValue = null;

                    // for each item in the checked list
                    for (var i = 0; i < checkedItems.length; i++) {
                        var checkedItem = checkedItems[i];

                        // get the value of the given field for this checked item
                        var itemValue = checkedItem[fieldName]();

                        // if a) haven't yet set a value or b) the value is the same as the bulk value, all is well
                        if (bulkValue == null || listmodel.deepCompare(itemValue, bulkValue)) {

                            // if we've not yet set the bulk value, create it (as potentially deep copy of the item value)
                            if (bulkValue == null)
                                bulkValue = listmodel.deepCopy(itemValue);
                        } else {

                            // otherwise the values aren't common, so abort!
                            valuesIdentical = false;
                        }
                    }

                    // now we'll set the value in our bulk object
                    var bulkItemField = bulkItem[fieldName];

                    // suspend update notifications, so we don't cascade changes back to the checked items!
                    bulkItemField.SuspendUpdates = true;

                    // if the values are identical, set the common value, and mark it as "not undefined" (so controls aren't greyed out)
                    if (valuesIdentical) {
                        bulkItemField(bulkValue);
                        bulkItemField.IsUndefined(false);

                    }
                        // otherwise
                    else {
                        // if it's an array, set it to the empty array
                        if (listmodel.isObservableArray(bulkItemField)) {
                            bulkItemField([]);
                        }
                            // else set it to null
                        else {
                            bulkItemField(null);
                        }

                        // in either case mark the field as "undefined"
                        bulkItemField.IsUndefined(true);

                    }

                    // now re-enable updates, so any changes made by the user are cascaded
                    bulkItemField.SuspendUpdates = null;

                }
            }
        });


        for (var outerfieldNameIndex in fieldNames) {

            var outerFieldName = fieldNames[outerfieldNameIndex];

            var ignore = function (innerFieldName) {

                try {
                    // fix for IE8
                    if (typeof (innerFieldName) != "function") {
                        var bulkItemField = bulkItem[innerFieldName];

                        bulkItemField.IsUndefined = ko.observable(true);

                        bulkItemField.subscribe(function () {

                            if (bulkItemField.SuspendUpdates)
                                return;

                            console.log('Bulk item field changed: ' + innerFieldName);
                            bulkItemField.IsUndefined(false);

                            var newValue = bulkItemField();
                            if (newValue != null) {
                                var checkedItems = listmodel.checked();
                                if (checkedItems != null)
                                    for (var i = 0; i < checkedItems.length; i++) {
                                        var checkedItem = checkedItems[i];
                                        var checkedField = checkedItem[innerFieldName];
                                        var itemValue = checkedField();
                                        if (!listmodel.deepCompare(itemValue, newValue)) {
                                            var deepCopy = listmodel.deepCopy(newValue);
                                            checkedField(deepCopy);
                                        }
                                    }
                            }
                        });
                    }
                } catch (e) {
                    console.log('Trying to subscribe to "' + innerFieldName + '" from ');
                    console.log(fieldNames);
                    console.log(bulkItem);
                    throw (e);
                }

            }(outerFieldName);
        }
    };

    listmodel.selectedOrBulk = ko.computed(function () {

        var result = listmodel.selected();
        if (result != null)
            return result;

        if (listmodel.checked().length == 0)
            return null;

        return listmodel.bulk();
    }
    );

    listmodel.selectedOpacity = ko.observable(1);
    listmodel.increaseSelectedOpacity = function () {
        var currentValue = listmodel.selectedOpacity();
        if (currentValue >= 1) return;

        listmodel.selectedOpacity(currentValue + 0.25);
        listmodel.selectedOpacityTimeout = setTimeout(listmodel.increaseSelectedOpacity, 100);
    };

    listmodel.noneSelected = ko.computed(function () {
        return listmodel._selected() == null;
    });

    listmodel.push = function (item) {
        listmodel.itemsloaded(true);
        listmodel._items.push(item);
    };

    listmodel.remove = function (item, forceSelection) {
        var indexToSelect = null;
        if (listmodel._selected() == item) {
            indexToSelect = listmodel._items.indexOf(item);
            listmodel._selected(null);
        }
        if (listmodel.lastCheckedItem == item) {
            listmodel.lastCheckedItem = null;
        }
        listmodel._items.remove(item);
        var newLength = listmodel._items().length;
        if (newLength > 0) {
            if (indexToSelect == null && forceSelection) {
                indexToSelect = 0;
            }
            if (indexToSelect != null) {
                var found = listmodel._items()[Math.min(indexToSelect, newLength - 1)];
                if (found && found.onClick)
                    found.onClick();
            }
        } else {
            listmodel._selected(null);
        }
    };

    listmodel.removeAll = function() {
        listmodel._items([]);
        listmodel._selected(null);
        listmodel.lastCheckedItem = null;
    };
    listmodel.isMenuShowing = ko.observable(false);
    listmodel.showHideMenu = function () {
        toggleMenu(listmodel.isMenuShowing);
    };

    listmodel.isViewMenuShowing = ko.observable(false);
    listmodel.showHideViewMenu = function () {
        toggleMenu(listmodel.isViewMenuShowing);
    };

    listmodel.deepCompare = function (object1, object2) {
        var json1 = JSON.stringify(object1);
        var json2 = JSON.stringify(object2);
        if (json1 != json2) {
            console.log('JSON unequal:');
            console.log(json1);
            console.log(json2);
        }
        return json1 == json2;
    };

    listmodel.isObservableArray = function (object) {
        return ko.isObservable(object) && object.hasOwnProperty('remove');
    };

    listmodel.deepCopy = function (object) {
        if (object == null) return null;

        if (listmodel.isObservableArray(object)) {
            console.log('Cloning array:');
            console.log(object);
            var result = ko.observableArray();
            for (var i = 0; i < object.length; i++) {
                var elem = object[i];
                console.log(elem);
                if (elem instanceof AccessControlUserViewModel) {
                    console.log('Creating new AccessControlUserViewModel');
                    elem = new AccessControlUserViewModel(elem.Name(), elem.Reference());
                }
                result.push(elem);
            }
        }
        else
            return object;

    };

}

function ListItemModel(itemmodel, listmodel) {

    itemmodel.list = listmodel;

    itemmodel.isSelected = ko.computed(
        {
            read: function () { return itemmodel == listmodel.selected(); },
            write: function (value) { listmodel.selected(itemmodel); }
        });

    itemmodel._isChecked = ko.observable(false);

    itemmodel.isChecked = ko.computed(
        {
            read: function () { return itemmodel._isChecked(); },
            write: function (value) {
                itemmodel._isChecked(value);
                if (value) {
                    listmodel._selected(null);
                }
            }
        });

    itemmodel.isSelectedOrChecked = ko.computed(function () {
        return (itemmodel.isSelected() || itemmodel.isChecked());
    });

    itemmodel.isHovered = ko.observable(false);

}

