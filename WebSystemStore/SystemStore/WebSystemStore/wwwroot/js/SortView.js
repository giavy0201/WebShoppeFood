$('.th-sort').click(function () {
    var table = $(this).parents('table').eq(0)
    var type = $(this).attr("type-sort")
    var rows = table.find('tr:gt(0)').toArray().sort(comparer($(this).index(), type))
    this.asc = !this.asc
    if (!this.asc) { rows = rows.reverse() }
    for (var i = 0; i < rows.length; i++) { table.append(rows[i]) }
})
function comparer(index, type) {
    return function (a, b) {
        var valA = getCellValue(a, index, type), valB = getCellValue(b, index, type)
        return compareValues(valA, valB);
    }
}
function compareValues(valA, valB) {
    if ($.isNumeric(valA) && $.isNumeric(valB)) {
        return valA - valB;
    } else {
        const dateA = stringToDate(valA);
        const dateB = stringToDate(valB);
        if (!isNaN(dateA) && !isNaN(dateB)) {
            return dateA - dateB;
        } else {
            return valA.toString().localeCompare(valB);
        }
    }
}
function stringToDate(dateString) {
    let dateParts = dateString.split(/\/|\s|:/);
    let year = parseInt(dateParts[2], 10);
    let month = parseInt(dateParts[1], 10) - 1;
    let day = parseInt(dateParts[0], 10);
    let hours = parseInt(dateParts[3], 10);
    let minutes = parseInt(dateParts[4], 10);
    let second = parseInt(dateParts[5], 10);

    let dateObject = new Date(year, month, day, hours, minutes, second);
    return dateObject;
}

function getCellValue(row, index,type) {
    var $row_selectd = $(row).children('td').eq(index).find(".value-td");
    var value = $row_selectd.text().trim();
    if (type == "number")
        value = parseInt(value.replace(/[^\d]/g, ''), 10);
    return value
}

$('.dropdown-menu').click(function (e) {
    if ($(e.target).is(':checkbox')) {
        return true;
    }

    e.preventDefault();
    e.stopImmediatePropagation();
    return false;
});