function showPopup(){
    $("[id=MyModal]").modal('show');
}

function hidePopup() {
    $("[id=MyModal]").modal('hide');
}

function hideQuestion(){
    $('.question-body-jq').remove();
}

let checkRadioButton = 0;
function checkRadioButtons() {
    $('#questionForm').click(function () {
        var response = $("input[type=radio]:checked").val();
        switch (response) {
            case 'oneAnswerVariant':
                hideQuestion()
                questionOneAnswersVariantGeneration();
                break;
            case 'someAnswersVariant':
                hideQuestion();
                questionWithSomeAnswersVariantGeneration();
                break;
            case  'text':
                hideQuestion();
                questionWithText();
                break;
            case 'file':
                hideQuestion();
                questionWithFile();
                break;
            case 'starRating':
                hideQuestion();
                questionWithStar();
                break;
            case 'scale':
                hideQuestion();
                questionWithScale();
                break;
        }
    })
}

function questionOneAnswersVariantGeneration() {
    var response = $("input[type=radio]:checked").val();
    if (response === 'oneAnswerVariant') {
        $('.question-body').append($('<div class="question-body-jq">\n' +
            '                      <input type="text" class="inp form-control w-50" placeholder="Enter answer" id="InputAnswer"/>\n' +
            '                      <input type="text" class="inp form-control w-50" placeholder="Enter answer" id="InputAnswer"/>\n' +
            '                      <input type="text" class="inp form-control w-50" placeholder="Enter answer" id="InputAnswer"/>\n' +
            '                      <input type="text" class="inp form-control w-50" placeholder="Enter answer" id="InputAnswer"/>\n' +
            '                    </div>'))
    }
}

function questionWithSomeAnswersVariantGeneration(){
    var response = $("input[type=radio]:checked").val();
    if (response === 'someAnswersVariant'){
       $('.question-body').append($('<div class="question-body-jq">\n' +
           '                      <input type="text" class="inp form-control w-50" placeholder="Enter answer" id="InputAnswer"/>\n' +
           '                      <input type="text" class="inp form-control w-50" placeholder="Enter answer" id="InputAnswer"/>\n' +
           '                      <input type="text" class="inp form-control w-50" placeholder="Enter answer" id="InputAnswer"/>\n' +
           '                    </div>'))
    }
}

function questionWithText() {
    var response = $("input[type=radio]:checked").val();
    if (response === 'text'){
        console.log('some')
    }
}

function questionWithFile() {
    var response = $("input[type=radio]:checked").val();
    if (response === 'file'){
        console.log('some')
    }
}


function questionWithStar() {
    var response = $("input[type=radio]:checked").val();
    if (response === 'starRating'){
        console.log('some')
    }
}

function questionWithScale() {
    var response = $("input[type=radio]:checked").val();
    if (response === 'scale'){
        console.log('some')
    }
}

function saveQuestion(id){
    const text = $("[id=InputQuestionTitle]").val();
    var answer = $("input[id=InputAnswer]").map( (i,el) => $(el).val()).get();
    const data = {
        title: text,
        surveyId: id,
        answer,
    };
    var request = $.ajax({
        contentType: 'application/json',
        url: `/api/SurveyApi/AddQuestionWithAnswerVariants`,
        method: 'post',
        data: JSON.stringify(data),
    })
    request.done((result)=> {
        if (result.isSuccessful){
            location.reload();
        }
    });
}
