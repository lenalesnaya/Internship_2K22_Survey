function showPopup(){
    $("[id=MyModal]").modal('show');
}

function hidePopup() {
    $("[id=MyModal]").modal('hide');
}

function saveSurvey() {
    const title = $("[id=InputSurveyTitle]").val();
    var request = $.ajax({
        url: "/api/SurveyApi/Create/" + title,
        method: 'post'
    });
    request.done((result)=> {
        if (result.isSuccessful){
            location.reload();
        }
    });
}

function deleteSurvey(id){
    var request = $.ajax({
        url: "/api/SurveyApi/Delete/" + id,
        method: 'post'
    });
    request.done((result)=>{
        if (result.isSuccessful){
            location.reload();
        }
    });
}