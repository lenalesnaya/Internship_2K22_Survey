function takeASurvey(){
    let userAnswers = $('input[data-answer="answer"]:checked').map( (e, el) =>({
        id: Number($(el).val()),
    })).get();
    const data ={
        userAnswers
    }
    let request = $.ajax({
        contentType: 'application/json',
        url: `/api/SurveyApi/AddUserAnswer`,
        method: 'post',
        data: JSON.stringify(data),
    });
    request.done((result)=> {
        if (result.isSuccessful){
            location.reload();
        }
    });
}