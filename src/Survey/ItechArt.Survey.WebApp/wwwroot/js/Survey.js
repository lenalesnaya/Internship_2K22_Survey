let defSurveysArray;
$(document).ready(function () {
    defSurveysArray = $("[id=survey]");
    //$("input[id=SearchBtn]").addEventListener('change', (e)=> searchingByName(e));
    $("input[id=SearchBtn]").change(function (e) {

        const searchString = e.target.value;
        if (searchString === "") return;

        const arraySurv = $("[id=survey]");
        const filteredArraySurv = arraySurv.filter(el => $(el).find(".survey-title").text().startsWith(searchString));

        arraySurv.forEach(el => {
            $(el).addClass('hide');
        });
        filteredArraySurv.forEach(el => {
            $(el).removeClass('hide');
        });

    })

function showPopup(){
    $("[id=MyModal]").modal('show');
}

function hidePopup() {
    $("[id=MyModal]").modal('hide');
}

function saveSurvey() {
    const title = $("[id=InputSurveyTitle]").val();
    const param = $("input[name=radio]:checked").val();
    var request = $.ajax({
        url: `/api/SurveyApi/Create/${title}/${param}`,
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



function searchingByName(e) {
    const searchString = e.target.value;
    if (searchString === "") return;

    const arraySurv = $("[id=survey]");
    const filteredArraySurv = arraySurv.filter(el => $(el).find(".survey-title").text().startsWith(searchString));

    arraySurv.forEach(el => {
        $(el).addClass('hide');
    });
    filteredArraySurv.forEach(el => {
        $(el).removeClass('hide');
    });

}
}
