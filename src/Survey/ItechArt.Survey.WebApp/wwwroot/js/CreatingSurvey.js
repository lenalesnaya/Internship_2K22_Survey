function showPopup(){
    $("[id=MyModal]").modal('show');
}

function hidePopup() {
    $("[id=MyModal]").modal('hide');
}

function saveSurvey() {
    const title = $("[id=InputSurveyTitle]").val();
    var request = $.ajax({
        url: `https://localhost:5001/api/SurveyApi/Create?title=${title}`,
        method: 'post'
    });
    request.done((result)=> {
        if (result.isSuccessful){
            location.reload();
        }
    });
}