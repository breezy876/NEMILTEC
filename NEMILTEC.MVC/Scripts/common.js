

function getValues(obj) {
    var vals = Object.keys(obj).map(function (key) {
        return obj[key];
    });
    return vals;
}

Array.prototype.contains = function (sub) {
    var self = this;
    var result = sub.filter(function (item) {
        return self.indexOf(item) > -1;
    });
    return sub.length === result.length;
}



toDictionaryBy = function(itemCol, key, val) {
    var outCol = {};
    for (var i = 0; i < itemCol.length; i++) {
        var item = itemCol[i];
        var k = item[key];
        if (val == null || val === '') {
            outCol[k] = item;
        } else {
            var value = item[val];
            outCol[k] = value == null ? '' : value;
        }
    }
    return outCol;
}



clearValues = function (arr) {
    var keys = Object.keys(arr);
    for (var i = 0; i < keys.length; i++) {
        var key = keys[i];
        arr[key] = null;
    }
}

removeAll = function (arr, items) {
    var i = arr.length;
    while(i--) {
        var val = arr[i];
        var index = items.indexOf(val);
        if (index > -1) {
            arr.splice(i, 1);
        }
    }
    return arr;
}

removeItems = function (obj, keys) {
    var i = keys.length;
    while (i--) {
        var key = keys[i];
        if (obj[key] != null) {
            delete obj[key];
        }
    }
    return obj;
}


removeNullOrEmpty = function (obj) {
    var keys = Object.keys(obj);
    var i = keys.length;
    while (i--) {
        var key = keys[i];
        var val = obj[key];
        if (val === null || val === '') {
            delete obj[key];
        }
    }
}

copyArr = function(arr) {
    return arr.slice(0);
}

clearArr = function (col) {
    while (col.length > 0) col.pop();
}


append = function(itemCol, arr) {
    for (var i = 0; i < arr.length; i++) {
        itemCol.push(arr[i]);
    }
}

appendTop = function (itemCol, arr) {
    for (var i = 0; i < arr.length; i++) {
        itemCol.unshift(arr[i]);
    }
}


replaceArr = function (itemCol, newArr) {
    clearArr(itemCol);
    append(itemCol, newArr);
}



fixNulls = function (arr) {
    var keys = Object.keys(arr);
    for (var i = 0; i < keys.length; i++) {
        var key = keys[i];
        var val = arr[key];
        if (val === null || val === undefined) {
            arr[key] = '';
        }
    }
}




getPropertyBy = function (itemCol, propName, val) {
    for (var i = 0; i < itemCol.length; i++) {
        var ref = itemCol[i][propName];
        if (ref === val)
            return itemCol[i];
    }
    return null;
}

countBy = function (itemCol, propName, val) {
    var total = 0;
    for (var i = 0; i < itemCol.length; i++) {
        var ref = itemCol[i][propName];;
        if (ref === val)
            total++;
    }
    return total;
}

selectBy = function (items, propName) {
    var out = [];
    for (var i = 0; i < items.length; i++) {
        var item = items[i];
        var val = item[propName];
        out.push(val);
    }
    return out;
}
