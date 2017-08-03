function formSubmit() {
    var questions = document.forms[0].elements["myInputs[]"];
    var phone = document.getElementById("phone").value;
    var s_name = document.getElementById("survey-name").value;
    var question_array = [];
    for (var i = 0; i < questions.length; i++) {
        question_array[i] = {"questionText":questions[i].value,"questionIndex":i};
    }
    var myObj = { "surveyName": s_name, "phoneNumber": phone, "questions": question_array };
    var myJSON = JSON.stringify(myObj);
    var request = new XMLHttpRequest();
    request.open('POST', "/api/surveys");
    request.setRequestHeader('Content-Type', 'application/json');
    request.send(myJSON);
 }