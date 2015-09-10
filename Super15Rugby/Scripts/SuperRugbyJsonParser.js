var year = { year: 2006 };

var weeks = [];
var count = 0;
$("table").each(function (i, obj) {
    obj.id = 'table' + count;
    var week = { weekNumber: count + 1 };

    var tbl = $('#' + obj.id + ' tr:has(td)').map(function (i, v) {
        var $td = $('td', this);
        return {
            id: ++i,
            TeamOne: $td.eq(0).text(),
            TeamOneScore: $td.eq(1).text(),
            TeamTwo: $td.eq(3).text(),
            TeamTwoScore: $td.eq(4).text(),
            MatchDate: $td.eq(5).text()
        }
    }).get();

    week.results = tbl;
    weeks[count] = week;
    count++;
});

year.weeks = weeks;
$("#jsonstring").text(JSON.stringify(year));