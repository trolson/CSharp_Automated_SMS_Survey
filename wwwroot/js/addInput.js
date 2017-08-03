var counter = 1;
var limit = 30;
function addInput(divName){
     var newdiv = document.createElement('div');
     newdiv.innerHTML = "Question " + (counter + 1) + " <br><input type='text' name='myInputs[]'>";
     document.getElementById(divName).appendChild(newdiv);
     counter++;
}