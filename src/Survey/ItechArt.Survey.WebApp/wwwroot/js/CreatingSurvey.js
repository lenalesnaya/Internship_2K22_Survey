function showPopup(){
    $("[id=MyModal]").modal('show');
}

function hidePopup() {
    $("[id=MyModal]").modal('hide');
}

function saveSurvey() {
    const title = $("[id=InputSurveyTitle]").val();
    $.ajax({
        url: `https://localhost:5001/api/SurveyApi/Create?title=${title}`
    }).done((response)=> {
        if (response.isSuccessful){
            location.reload();
        }
    }).fail((data) => alert(data))
}