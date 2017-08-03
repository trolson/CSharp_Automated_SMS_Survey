var req = new XMLHttpRequest();
var href = location.href;
var table = document.getElementById("question-table");
var info = document.getElementById("survey-info");
console.log(href.match(/([^\/]*)\/*$/)[1]);
console.log("TEST");
var url = '/api/survey/' + href.match(/([^\/]*)\/*$/)[1];
req.open('GET', url);
req.onload = function() {
    var data = JSON.parse(req.responseText);
    addHTMLText(data);
    addHTMLTable(data);
};
req.send();

function addHTMLTable(data){
    var htmlString = "";
    data = data.questions;
    for(i = 0; i < data.length; i++) {
        htmlString += '<tr class="details"><td>' + data[i].questionId + '</td><td>' + data[i].questionText + '</td><td>' + data[i].questionIndex + '<td><a href="#" class="details">Show details</a>' + '</td>';
        htmlString += addHTMLAnswers(data[i].answers);
        htmlString += '</tr>';
    }
    table.insertAdjacentHTML('beforeend', htmlString);
}

function addHTMLText(data){
    info.insertAdjacentHTML('beforeend', '<h5>Survey ID: ' + data.surveyId + '</h5>');
    info.insertAdjacentHTML('beforeend', '<h5>Survey Name: ' + data.surveyName + '</h5>');
}

function addHTMLAnswers(data) {
    var htmlAnswersString = '<tr class="details"><td colspan="3"><div class="slide"><p>';
    htmlAnswersString += data;
    htmlAnswersString += '</p></div></td></tr>';
    return htmlAnswersString;
}


