
function initScope(scope, model, $http, $timeout, Upload) {

    scope.parent = scope;

    scope.model = model;

    scope.DataType = {
        String: 1,
        Number: 2,
        Decimal: 3,
        Boolean: 4,
        Object: 5,
        List: 6,
        Entity: 7
    };

    scope.ContainerType = {
        Table: 1,
        Tabs: 2,
        List: 3,
        Segment: 4,
        Data: 5
    };


    angular.module('infinite-scroll').value('THROTTLE_MILLISECONDS', 1000);

    $('.dropdown').dropdown();

    $('.sticky').sticky({ topSpacing: 0 });

    //misc functions

    scope.toJSON = function (obj) {
        var json = JSON.stringify(obj, function (key, val) {
            if (key === '$$hashKey' || key === '$id' || key === '$ref') {
                return undefined;
            }
            return val;
        });
        return json;
    }

    scope.clean = function (arr) {
        return removeAll(arr, ['$id', '$$hashkey', '$ref', '$type']);
    }


    scope.getKeys = function (obj) {
        var keys = Object.keys(obj);
        return scope.clean(keys);
    }

    scope.uploadFiles = function (container, file, errFiles) {

        scope.file = file;
        scope.errFiles = errFiles;

        if (file) {

            file.upload = Upload.upload({
                url: scope.getUrl(model, 'UploadFile'),
                data: { type: container.Type, file: file }
            });

            file.upload.then(function (response) {
                $timeout(function () {
                    if (response.data != null && response.data.length > 0) {

                        var newItems = response.data;

                        newItems.map(function (item) {
                            var newItem = scope.addEmptyItem(container, true);
                            newItem.Properties = item.Properties;

                        });

                        container.childPropDic = scope.createChildPropDic(container.Children);

                        scope.updateIndexes(container);
                    }
                });
            }, function (response) {
                if (response.status > 0)
                    scope.errorMsg = response.status + ': ' + response.data;
            }, function (evt) {
                file.progress = Math.min(100, parseInt(100.0 *
                                         evt.loaded / evt.total));
            });
        }
    }

    //end 


    //container functions
    scope.createPropDic = function (item) {
        var itemProps = item['Properties'];
        var propKeys = scope.getKeys(itemProps);

        var props = {};

        for (var k = 0; k < propKeys.length; k++) {
            var key = propKeys[k];
            props[key] = itemProps[key]['Value'];
        }

        return props;
    }

    scope.createChildPropDic = function (items) {

        var propVals = [];

        for (var i = 0; i < items.length; i++) {
            var item = items[i];
            var props = scope.createPropDic(item);
            propVals.push(props);
        }

        return propVals;
    }

    scope.initContainer = function (container) {

        container.paginationOptions = { startIndex: 0, total: container.MaxItems };

        container.filterDic = toDictionaryBy(getValues(scope.clean(container.Filters)), 'Name', 'Value');

        container.childPropDic = scope.createChildPropDic(container.Children);

        if (container.filterApplied == null) {
            container.filterApplied = false;
        }

        container.allowLoading = false;

        container.filterMatches = [];

        container.totalSelected = 0;
        container.totalInEdit = 0;
        container.totalNew = 0;
        container.totalChanged = 0;

        container.editSelected = false;
        container.selectAll = false;


    }


    scope.confirmModal = function (title, content, callback) {

        var dvModal = $('.ui.confirm.modal');

        dvModal.find('.header').text(title);
        dvModal.find('.content p').text(content);

        var callbackFunc = function () {
            callback();
        }

        dvModal.modal({
            closable: false,
            duration: 200,
            dimmerSettings: {
                opacity: 0.01,
                closable: false,
                useCSS: true
            },
            onDeny: function () {
            },
            onApprove: function () {
                callbackFunc();
            }
        })
        .modal('show');
    }

    scope.getUrl = function (model, actionName) {
        return '/' + model.CategoryName + '/' + actionName;
    }
    //end


    //filter functions
    filterItem = function (itemPropDic, filterDic) {

        var result = true;

        var keys = Object.keys(filterDic);

        for (var i = 0; i < keys.length; i++) {
            var key = keys[i];
            var filterVal = filterDic[key];
            var itemVal = itemPropDic[key];
            result &= ((filterVal === null || filterVal === '' || filterVal == undefined) ||
                (itemVal != null && itemVal != undefined && itemVal !== '' && itemVal.toString().includes(filterVal)));
        }

        return result;

    }

    filter = function (itemCol, childPropDic, filterDic) {
        var outCol = [];
        for (var i = 0; i < childPropDic.length; i++) {
            var itemPropDic = childPropDic[i];
            var item = itemCol[i];
            if (!item.IsNew && filterItem(itemPropDic, filterDic)) {
                outCol.push(item);
            }
        }
        return outCol;
    }

    scope.getFilterToggleTitle = function (container) {
        return (container.ShowFilters ? 'Hide Filters' : 'Show Filters');
    }

    scope.clearFilters = function (container) {

        var keys = scope.getKeys(container.filterDic);

        for (var i = 0; i < keys.length; i++) {
            var key = keys[i];
            container.filterDic[key] = null;
        }

        $('.filter.dropdown').dropdown('set selected', '0');

        $('.filterInput').val(null);

        container.filterApplied = false;
    }

    scope.toggleFilters = function (container) {
        if (container.ShowFilters) {
            container.ShowFilters = false;
        } else {
            container.ShowFilters = true;
        }
    }

    scope.filtersEmpty = function (filterDic) {
        var allEmpty = true;
        var keys = Object.keys(filterDic);
        for (var i = 0; i < keys.length; i++) {
            var key = keys[i];
            var filterVal = filterDic[key];
            allEmpty &= filterVal === null || filterVal === '';
        }
        return allEmpty;
    }

    scope.changeFilter = function (container, filterName, val) {

        console.log('change');
        container.filterDic[filterName] = val;

        container.filterApplied = !scope.filtersEmpty(container.filterDic);

        if (container.filterApplied) {
            container.filterMatches = filter(container.Children, container.childPropDic, container.filterDic);
        }

        scope.togglePrevSelected(container);


    }
    //end


    //begin item functions

    scope.exportItems = function (container) {

        var dataTable = {
            Rows: [],
            Columns: []
        };

        var items = scope.containerItems(container);
        var selectedItems = scope.getSelectedItems(items);

        var props = getValues(container.Properties).filter(function (p) { return p.IsVisible });

        var propNames = props.map(function (prop) {
            if (prop.Type === scope.DataType.List) {
                return prop.Title;
            } else {
                return prop.Name;
            }
        });

        propNames = scope.clean(propNames);

        for (var colIndex = 0; colIndex < propNames.length; colIndex++) {
            var colName = propNames[colIndex];
            dataTable.Columns.push(colName);
        }

        for (var rowIndex = 0; rowIndex < selectedItems.length; rowIndex++) {

            var item = selectedItems[rowIndex];

            if (!item.IsNew) {

                var newRow = { Values: []};
                var values = [];

                for (var colIndex = 0; colIndex < propNames.length; colIndex++) {
                    var colName = propNames[colIndex];
                    var prop = item.Properties[colName];
                    var propVal = prop.Value;
                    values.push(propVal);
                }

                newRow.Values = values;

                dataTable.Rows.push(newRow);
            }
        }

        //var jsonData = scope.toJSON(dataTable);

        $http.post(
           '/Data/Export',
           { name: container.Title, data: dataTable }
        ).then(function successCallback(resp) {
            if (resp.data != null) {
                window.location = '/Data/DownloadFile?fileGuid=' + resp.data.FileGUID
                          + '&fileName=' + resp.data.FileName;
            }
        }, function errorCallback(data) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
        });
    }

    scope.countItemsBy = function (container, propName, val) {
        var items = scope.containerItems(container);
        var total = 0;
        for (var i = 0; i < items.length; i++) {
            var ref = items[i][propName];;
            if (ref === val)
                total++;
        }
        return total;
    }

    scope.getItemTotal = function (container) {
        var items = scope.containerItems(container);
        return items.length;
    }

    scope.getItemPropClass = function (item) {
        return item.HasChanged ? 'changed' : '';
    }

    scope.itemChanged = function (container, item) {
        if (!item.HasChanged) {
            item.HasChanged = true;
            container.totalChanged++;
        }

    }

    scope.loadMoreItems = function (container) {
        if (container.allowLoading && !container.filterApplied) { //dont load more items if filter previously applied
            var startIndex = container.paginationOptions.startIndex;
            startIndex += container.paginationOptions.total;
            container.paginationOptions.startIndex = startIndex;

            scope.loadItems(container, startIndex, container.paginationOptions.total, false);
        }
    }

    scope.containerItems = function (container) {
        return container.filterApplied ? container.filterMatches :
            container.Children;
    }

    scope.clearItem = function (item) {
        item.Children = null;
        item.Properties = null;
        item.PropertyMappings = null;
        item.Commands = null;
    }

    scope.clearItems = function (items) {
        for (var i = 0; i < items.length; i++) {
            scope.clearItem(items[i]);
        }
    }


    scope.getItems = function(container, startIndex, total, addBlank, callbackFunc)
    {
        $http.post(
           scope.getUrl(model, '/GetAll'),
           { type: container.Type, startIndex: startIndex, total: total, addBlank: addBlank }
       ).then(function successCallback(resp) {
           if (resp.data != null && resp.data.length > 0) {
               callbackFunc(resp.data);
           }
       }, function errorCallback(data) {
           // called asynchronously if an error occurs
           // or server returns response with an error status.
       });
    }


    scope.getDataItems = function (container, startIndex, total, addBlank, callbackFunc) {
        $http.post(
           '/Data/GetAll',
           { type: container.Type, startIndex: startIndex, total: total, addBlank: addBlank }
       ).then(function successCallback(resp) {
           if (resp.data != null && resp.data.length > 0) {
               callbackFunc(resp.data);
           }
       }, function errorCallback(data) {
           // called asynchronously if an error occurs
           // or server returns response with an error status.
       });
    }

    scope.loadItems = function (container, startIndex, total, addBlank, doLoad) {

        if (doLoad) {

            container.allowLoading = false;

            var callbackFunc = function (data) {

                var itemCol = container.Children;

                var itemsCopy = copyArr(itemCol);
                append(itemsCopy, data);

                container.childPropDic = scope.createChildPropDic(itemsCopy); //rebuild property dictionary for filtering

                replaceArr(itemCol, itemsCopy);

                if (container.selectAll) {
                    scope.selectAll(container);
                }

                container.allowLoading = true;
            }

            scope.getItems(container, startIndex, total, addBlank, callbackFunc);

        }
    }

    scope.getChildren = function (parent) {
        $http.post(
           scope.getUrl(model, '/GetChildren'),
            { type: parent.Type, parentId: parent.Id }
        ).then(function successCallback(resp) {
            if (resp.data != null) {
                if (parent.length > 0)
                {
                    replaceArr(parent.Children, resp.data);
                }
                else {
                    parent.Children = resp.data;
                }
            
            }
        }, function errorCallback(data) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
        });
    }



    scope.saveItem = function (container, item, actionName) {

        var items = scope.containerItems(container);
        var selectedCol = scope.getSelectedItems(items);

        var itemCopy = angular.copy(item);

        scope.clearItem(itemCopy);

        var itemJSON = scope.toJSON(itemCopy);

        $http.post(
               scope.getUrl(model, actionName),
            { type: container.Type, itemJSON: itemJSON }
        ).then(function successCallback(resp) {
            if (resp.data != null && resp.data.length > 0) {

                var newItems = resp.data;
                newItems = scope.revertItems(newItems);

                replaceArr(container.Children, newItems);

                scope.initContainer(container);

                if (container.filterApplied) {
                    container.filterMatches = filter(container.Children, container.childPropDic, container.filterDic);
                }

                if (selectedCol != null && selectedCol.length > 0) {
                    scope.selectItemsFrom(container, newItems, selectedCol);
                }


                //scope.$apply();
            }
        }, function errorCallback(data) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
        });

    }


    scope.removeItem = function (container, item, actionName) {

        var itemCopy = angular.copy(item);

        scope.clearItem(itemCopy);

        if (item.Id === 0 && itemCopy.IsNew) {
            container.Children = container.Children.filter(function(i) {
                return item.IsNew && i.Index !== itemCopy.Index;
            });
            scope.totalNew--;
        }

        else {

            var title = 'Delete ' + itemCopy.Title;
            var content = 'Are you sure you want to delete ' + (itemCopy.Name === '' ? 'this ' + itemCopy.Title : itemCopy.Name) + '?';

            var callback = function () {
                scope.saveItem(container, itemCopy, actionName);
            };

            scope.confirmModal(title, content, callback);

        }

        container.childPropDic = scope.createChildPropDic(container.Children);

        scope.updateIndexes(container);

    }



    scope.saveSelected = function (container, actionName) {

        var items = angular.copy(scope.containerItems(container));

        scope.clearItems(items);

        var selectedCol = scope.getSelectedItems(items);
        var itemsForSave = scope.getChangedItems(selectedCol);

        if (itemsForSave != null && itemsForSave.length > 0) {

            var title = 'Save Selected';
            var content = 'Are you sure you want to save the selected rows?';

            var callback = function () {

                $http.post(
                        scope.getUrl(model, actionName),
                        { type: container.Type, itemJSONCol: scope.containerItemsJSON(itemsForSave) }
                    )
                    .then(function successCallback(resp) {
                        if (resp.data != null && resp.data.length > 0) {

                            var newItems = resp.data;
                            newItems = scope.revertItems(newItems);

                            replaceArr(container.Children, newItems);

                            scope.initContainer(container);

                            if (container.filterApplied) {
                                container.filterMatches = filter(container.Children, container.childPropDic, container.filterDic);
                            }


                            if (selectedCol != null && selectedCol.length > 0) {
                                scope.selectItemsFrom(container, newItems, selectedCol);
                            }
                        }
                    },
                        function errorCallback(data) {
                            // called asynchronously if an error occurs
                            // or server returns response with an error status.
                        });

            }

            scope.confirmModal(title, content, callback);

        }
    }


    scope.removeSelected = function (container, actionName) {

        var items = angular.copy(scope.containerItems(container));

        scope.clearItems(items);

        scope.removeNewSelected(container);

        var selectedCol = scope.getSelectedItems(items);

        if (selectedCol != null && selectedCol.length > 0 && scope.hasSavedItems(selectedCol)) {

            var title = 'Delete Selected';
            var content = 'Are you sure you want to delete the selected rows?';

            var callback = function () {
                $http.post(
                     scope.getUrl(model, actionName),
                       { type: container.Type, itemJSONCol: scope.containerItemsJSON(selectedCol) }
                   )
                   .then(function successCallback(resp) {
                       if (resp.data != null && resp.data.length > 0) {

                           var newItems = resp.data;

                           replaceArr(container.Children, newItems);

                           scope.initContainer(container);

                           if (container.filterApplied) {
                               container.filterMatches = filter(container.Children, container.childPropDic, container.filterDic);
                           }
                       }
                   },
                       function errorCallback(data) {
                           // called asynchronously if an error occurs
                           // or server returns response with an error status.
                       });
            };

            scope.confirmModal(title, content, callback);

        }


    }

    scope.hasSavedItems = function (items) {
        var total = 0;
        for (var i = 0; i < items.length; i++) {
            var item = items[i];
            if (!item.IsNew) {
                total++;
            }
        }
        return total > 0;
    }

    scope.editSelectedItems = function (container, flag) {

        var itemCol = scope.containerItems(container);
        var selectedCol = scope.getSelectedItems(itemCol);

        if (selectedCol != null && selectedCol.length > 0) {
            for (var i = 0; i < selectedCol.length; i++) {
                var item = selectedCol[i];
                if (!item.IsNew) {
                    item.IsEdit = flag;
                }
            }
        }
    }



    scope.containerItemsJSON = function (itemCol) {
        var jsonCol = [];
        for (var i = 0; i < itemCol.length; i++) {
            var item = itemCol[i];
            var itemJSON = scope.toJSON(item);
            jsonCol.push(itemJSON);
        }
        return jsonCol;
    }

    scope.getNewEditItems = function (itemCol) {
        var outCol = [];
        for (var i = 0; i < itemCol.length; i++) {
            var item = itemCol[i];
            if (item.IsNew || item.IsEdit) {
                outCol.push(item);
            }
        }
        return outCol;
    }

    scope.getNewItems = function (itemCol) {
        var outCol = [];
        for (var i = 0; i < itemCol.length; i++) {
            var item = itemCol[i];
            if (item.IsNew) {
                outCol.push(item);
            }
        }
        return outCol;
    }



    scope.isLastNewItem = function (container) {
        var items = scope.containerItems(container);
        var total = 0;
        for (var i = 0; i < items.length; i++) {
            var item = items[i];
            if (!item.IsDeleted && item.IsNew) {
                total++;
            }
        }
        return total === 1;
    }

    scope.getChangedItems = function (itemCol) {
        var outCol = [];
        for (var i = 0; i < itemCol.length; i++) {
            var item = itemCol[i];
            if (item.HasChanged) {
                outCol.push(item);
            }
        }
        return outCol;
    }

    scope.getSelectedItems = function (itemCol) {
        var outCol = [];
        for (var i = 0; i < itemCol.length; i++) {
            var item = itemCol[i];
            if (item.IsSelected) {
                outCol.push(item);
            }
        }
        return outCol;
    }

    scope.getNewItems = function (itemCol) {
        var outCol = [];
        for (var i = 0; i < itemCol.length; i++) {
            var item = itemCol[i];
            if (item.IsNew) {
                outCol.push(item);
            }
        }
        return outCol;
    }

    scope.getNotNewItems = function (itemCol) {
        var outCol = [];
        for (var i = 0; i < itemCol.length; i++) {
            var item = itemCol[i];
            if (!item.IsNew) {
                outCol.push(item);
            }
        }
        return outCol;
    }

    scope.getSortedItems = function (itemCol) {
        var first = [];
        var second = [];
        for (var i = 0; i < itemCol.length; i++) {
            var item = itemCol[i];
            if (item.IsNew) {
                first.push(item);
            }
            else {
                second.push(item);
            }
        }
        var sortedItems = first.concat(second);
        return sortedItems;
    }



    scope.toggleSelectItem = function (container, item) {
        if (!item.IsSelected) {
            scope.selectItem(container, item);
        } else {
            scope.deselectItem(container, item);
        }
    }

    scope.togglePrevSelected = function (container) {

        var itemCol = scope.containerItems(container);
        var prevSelected = scope.getSelectedItems(itemCol);

        container.totalSelected = prevSelected.length;

    }


    scope.selectItem = function (container, item) {
        item.IsSelected = true;
        container.totalSelected++;
    }

    scope.deselectItem = function (container, item) {
        item.IsSelected = false;
        container.totalSelected--;
    }

    scope.selectItemsFrom = function (container, itemCol, select) {
        var itemDic = toDictionaryBy(itemCol, 'Id');
        var selectDic = toDictionaryBy(select, 'Id');
        var keys = scope.getKeys(selectDic);
        for (var i = 0; i < keys.length; i++) {
            var key = keys[i];
            if (itemDic[key] != null) {
                var item = itemDic[key];
                scope.selectItem(container, item);
            }
        }
    }

    scope.selectItems = function (container, itemCol) {
        for (var i = 0; i < itemCol.length; i++) {
            var item = itemCol[i];
            scope.selectItem(container, item);
        }
    }

    scope.selectAll = function (container) {

        var itemCol = scope.containerItems(container);

        container.totalSelected = 0;

        for (var i = 0; i < itemCol.length; i++) {
            var item = itemCol[i];
            scope.selectItem(container, item);
        }

    }

    scope.deselectAll = function (container) {

        var itemCol = scope.containerItems(container);

        for (var i = 0; i < itemCol.length; i++) {
            var item = itemCol[i];
            scope.deselectItem(container, item);
        }
    }

    scope.toggleSelectAll = function (container) {

        var itemCol = scope.containerItems(container);

        if (!container.selectAll) {
            container.selectAll = true;
        } else {
            container.selectAll = false;
        }
                        
        if (container.selectAll) {
            scope.selectAll(container);
        }
        else {
            scope.deselectAll(container);
        }

    }



    scope.getSelectItemTitle = function (itemRef) {
        return itemRef.IsSelected ? "Deselect" : "Select";
    }

    scope.getSelectAllTitle = function (container) {
        return container.selectAll ? "Deselect All" : "Select All";
    }


    scope.getChangedText = function (container) {
        if (container.totalChanged > 0) {
            var outText = 'Total changed: ' + container.totalChanged;
            return outText;
        }
    }

    scope.getStatusText = function (container) {
        var outText = 'Total rows: ' + scope.containerItems(container).length;
        return outText;
    }

    scope.getFilterText = function (container) {
        var outText = '';
        var totalFiltered = 0;
        if (container.filterApplied) {
            totalFiltered = container.filterMatches.length;
            if (totalFiltered === 0) {
                outText = ' Filter did not match any rows.';
            } else {
                outText = ' Filter matches  ' + totalFiltered + ((totalFiltered === 1) ? ' row.' : ' rows.');
            }
        }
        return outText;
    }

    scope.getSelectedText = function (container) {
        var outText = '';
        if (container.totalSelected > 0) {
            outText = '\nSelected ' + container.totalSelected + ((container.totalSelected === 1) ? ' row.' : ' rows.');
        }
        return outText;
    }

    scope.updateIndexes = function(container) {
        for (var i = 0; i < container.Children.length; i++) {
            container.Children[i].Index = i;
        }
    }


    scope.addEmptyItem = function (container, isImport) {

        //var expandedItems = container.Children.map(function (i) { i.IsExpanded });

        var item = angular.copy(container.Children[0]);

        if (container.selectAll) {
            scope.selectItem(container, item);
        }

        var props = scope.createPropDic(item);

        container.childPropDic.unshift(props);

        container.Children.unshift(item);

            scope.updateIndexes(container);

        container.totalNew++;

        //expandedItems.map(function(i) { sortTableRows('table.content', i); });

        return(item);
    }


    scope.revertItem = function (item) {
        if (item.ID > 0) {
            item.IsNew = false;
        }

        item.IsEdit = false;
        item.HasChanged = false;
    }

    scope.revertItems = function (itemCol) {
        for (var i = 0; i < itemCol.length; i++) {
            var item = itemCol[i];
            scope.revertItem(item);
        }
        return itemCol;
    }

    scope.removeNewSelected = function (container) {
        var items = container.Children;
        i = items.length;
        while (i--) {
            var item = items[i];
            if (item.IsSelected && item.IsNew && i > 0) {
                container.Children.splice(i, 1);
                container.totalNew--;
            }
        }
    }


    scope.getItemClass = function (item) {
        var outStr = 'item';
        if (item.HasChanged) {
            outStr += ' changed';
            return outStr;
        }
        else if (item.IsEdit) {
            outStr += ' active';
            return outStr;
        }
        else if (item.IsNew) {
            outStr += ' new';
            return outStr;
        }
    }

    scope.showEdit = function (container, item) {
        //scope.initDropDowns("table.content tr.item[data-id='" + item.Id + "']");
        item.IsEdit = true;
        container.totalInEdit++;
    }

    scope.hideEdit = function (container, item) {
        item.IsEdit = false;
        container.totalInEdit--;
    }

    //begin toggles

    scope.toggleEditSelectedItems = function (container) {
        if (!container.editSelected) {
            container.editSelected = true;
        } else {
            container.editSelected = false;
        }

        scope.editSelectedItems(container, container.editSelected);
    }

    scope.getEditSelectedItemClass = function (container) {
        return !container.editSelected ? 'fa fa-edit' : 'fa fa-minus-circle';
    }

    scope.getEditSelectedItemTitle = function (container) {
        return container.editSelected ? 'Cancel Edit' : 'Edit Selected';
    }



    scope.getItemToggleClass = function (item) {
        return item.IsExpanded ? 'fa fa-chevron-up' : 'fa fa-chevron-down';
    }

    scope.getItemToggleTitle = function (item) {
        return item.IsExpanded ? 'Collapse ' : 'Expand';
    }

    scope.toggleItemContent = function (item) {

        if (!item.IsExpanded) {

            sortTableRows('table.content', item);

            item.IsExpanded = true;

            scope.getChildren(item);

        } else {
            item.IsExpanded = false;
        }

    }

    scope.getItemPropVal = function (prop) {
        return prop.Type === scope.DataType.Object && prop.Value != null ? prop.Name : prop.Value;
    }

    scope.getItemPropTitle = function (prop) {
        return prop.Type === scope.PropertyType.List ? prop.Title : prop.Name;
    }

    scope.setItemPropVal = function (container, item, propName, val) {
        item[propName] = val;
        scope.itemChanged(container, item);
    }


    //end item functions

    //begin tabs
    scope.getTabClass = function (item, index) {
        var outStr = 'contentTab item blue-grey lighten-5';
        if (index === 0) {
            outStr += ' active';
        }
        return outStr;
    }

    scope.getTabItemClass = function (item, index) {
        var outStr = 'ui mini bottom attached tab segment blue-grey lighten-5';
        if (index === 0) {
            outStr += ' active';
        }
        return outStr;
    }
    //end


    //begin drop down lists
    scope.initDropDownLists = function (parentSelector) {
        var parent = $(parentSelector);
        parent.find('.dropdown').dropdown();
    }


    scope.dropDownListItemSelected = function(container, prop, listItem, item, isFilter)
    {
        if (isFilter)
        {
            scope.changeFilter(container, prop.Title, listItem.Name);
        }
        else {
            scope.setItemPropVal(container, item, prop.Name, listItem.Id);
        }
    }

    scope.showDropDownMenu = function (container) {

        var childCol = container.Children;

        var callbackFunc = function (data) {
            if (childCol.length == 0) {
                append(childCol, data);
            }
        }

        scope.getDataItems(container, 0, 20, false, callbackFunc);
    }

    scope.getDropDownMenuSelectedItemClass = function(propName, item) {
        var prop = item.Properties[propName];
        return (prop.Value !== null && prop.Value !== '') ? 'text' : 'default text';
    }

    scope.getDropDownMenuSelectedItem = function(propName, item) {
        var prop = item.Properties[propName];
        return (prop.Value !== null && prop.Value !== '') ? prop.Value : propName;
    }
    //end


    //entity editor

    scope.EntityEditor = {

        initContainer: function(container) {
            container.totalSelected = 0;
            container.totalInEdit = 0;
            container.totalNew = 0;
            container.totalChanged = 0;

            container.editSelected = false;
            container.selectAll = false;

            this.updateIndexes(container);
        },

        updateIndexes: function(container) {
            var index = 0;
            container.Data.Properties.map(function(prop) {
                prop.Index = index;
                index++;
            });
        },

        removeProperty: function (container, prop) {
            container.Data.Properties = container.Data.Properties.filter(function (p) { return p.Index !== prop.Index; });
            this.updateIndexes(container);
        },

        removeProperties: function(container, props) {
            var propIndexes = props.map(function(prop) { return prop.Index });
            container.Data.Properties = container.Data.Properties.filter(function (prop) { return propIndexes.indexOf(prop.Index) === -1; });
            this.updateIndexes(container);
        },

        getSelectedProperties: function(container) {
            var props = container.Data.Properties;
            var selected = props.filter(function (prop) { return prop.IsSelected; });
            return selected;
        },

        removeSelectedProperties: function(container) {
            var selected = this.getSelectedProperties(container);
            this.removeProperties(container, selected);
        },

        toggleSelectProperty: function (container, prop) {
            if (!prop.IsSelected) {
                this.selectProperty(container, prop);
            } else {
                this.deselectProperty(container, prop);
            }
        },

        selectProperty: function (container, prop) {
            prop.IsSelected = true;
            container.totalSelected++;
        },

        deselectProperty: function (container, prop) {
            prop.IsSelected = false;
            container.totalSelected--;
        },


       selectAll: function (container) {

           var itemCol = container.Data.Properties;

            container.totalSelected = 0;

            for (var i = 0; i < itemCol.length; i++) {
                var item = itemCol[i];
                this.selectProperty(container, item);
            }
        },

        deselectAll: function (container) {

            var itemCol = container.Data.Properties;

            for (var i = 0; i < itemCol.length; i++) {
                var item = itemCol[i];
                this.deselectProperty(container, item);
            }
        },

        toggleSelectAll: function (container) {

            var itemCol = container.Data.Properties;

            if (!container.selectAll) {
                container.selectAll = true;
            } else {
                container.selectAll = false;
            }
                        
            if (container.selectAll) {
                this.selectAll(container);
            }
            else {
                this.deselectAll(container);
            }

        },

       addNewProperty: function(container) {

           var props = container.Data.Properties;

           var item = angular.copy(props[0]);

            if (container.selectAll) {
                this.selectProperty(container, item);
            }

            props.unshift(item);

            this.updateIndexes(container);

            container.totalNew++;
       },

       getPropertyClass: function (prop) {
            var outStr = 'item';
            if (prop.HasChanged) {
                outStr += ' changed';
                return outStr;
            }
            else if (prop.IsEdit) {
                outStr += ' active';
                return outStr;
            }
            else if (prop.IsNew) {
                outStr += ' new';
                return outStr;
            }
           return outStr;
       },

      showEdit: function (container, item) {
            item.IsEdit = true;
            container.totalInEdit++;
        },

         hideEdit: function (container, item) {
                item.IsEdit = false;
                container.totalInEdit--;
         },

         isLastNewProperty: function (container) {

             var props = container.Data.Properties;
             var total = 0;

            props.map(function (prop) {
                if (prop.IsNew) {
                    total++;
                }
            });

            return total === 1;
         },

        propertyValueChanged: function(container, prop, propVal) {
            prop.Value = propVal;
            prop.valueChanged = true;
            if (prop.valueChanged && prop.nameChanged) {
                prop.IsNew = false;
            }
        },

        propertyNameChanged: function (container, prop, propName) {
            prop.Name = propName;
            prop.nameChanged = true;
            if (prop.valueChanged && prop.nameChanged) {
                prop.IsNew = false;
            }
        }
    };

    scope.Data = {
        save: function (container) {

            var dataJSON = scope.toJSON(container.Data);

            $http.post(
                '/Data/SaveData',
                { parentId: container.ParentId, type: container.Type, dataType: container.DataType, dataJSON: dataJSON }
            ).then(function successCallback(resp) {
                if (resp.data != null && resp.data.length > 0) {

                }
            });
        }
    };

    scope.ExpressionFactory = {
        ExpressionType: {
            Binary: 1,
            Function: 2,
            Data: 3
        },

        add: function (expr, child) {
            expr.Expressions.push(child);
        }
    }

    //scope.EntityEditor.setModelDataPropVal = function (model, propName, val) {
    //    model.Data[propName] = val;
    //}

    //scope.saveModelData = function(model) {

    //    var dataJSON = scope.toJSON(model.Data);

    //    $http.post(
    //        'Data/SaveData',
    //        { parentId: model.Id, type: model.Type, dataType: model.DataType, data: dataJSON }
    //    ).then(function successCallback(resp) {
    //        if (resp.data != null && resp.data.length > 0) {

    //        }
    //    });
    //}
    //end


}

function sortTableRows(tableSelector, item) {

    var $table = angular.element(tableSelector);

    var itemRow = $table.children('tbody' ).first().children("tr.item[data-id='"+item.Id+"']");
    var contentRow = $table.children('tbody').first().children("tr.content[data-id='" + item.Id + "']");

    contentRow.insertAfter(itemRow);

}

