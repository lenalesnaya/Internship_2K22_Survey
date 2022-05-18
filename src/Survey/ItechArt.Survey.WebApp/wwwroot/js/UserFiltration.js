let usernameSortOrder = 0;
let regDateSortOrder = 0;
let numOfSurveyOrder = 0;

function sortByUserName(){
    var svg = $("[id=userNameFilterItemSvg]");
    var user = $("[id=user]");
    if (!usernameSortOrder){
        svg.addClass("rotate180deg");
        user.sort((a, b) => $(a).data("username").localeCompare($(b).data("username")));
        usernameSortOrder = 1;
    }
    else{
        svg.removeClass("rotate180deg");
        user.sort((a, b) => $(b).data("username").localeCompare($(a).data("username")));
        usernameSortOrder = 0;
    }

    $(".user-grid").html(user);
}

function sortByRegDate(){
    var svg = $("[id=regDateSvg]");
    var user = $("[id=user]");
    console.log(new Date(user.data("regdate")));
    if (!regDateSortOrder){
        svg.addClass("rotate180deg");
        user.sort((a, b) => new Date($(a).data("regdate")) - new Date($(b).data("regdate")));
        regDateSortOrder = 1;
    }
    else{
        svg.removeClass("rotate180deg");
        user.sort((a, b) => new Date($(b).data("regdate")) - new Date($(a).data("regdate")));
        regDateSortOrder = 0;
    }

    $(".user-grid").html(user);
}

function sortByNumOfSurvey(){
    var svg = $("[id=numOfSurveySvg]");
    var user = $("[id=user]");
    if (!numOfSurveyOrder){
        svg.addClass("rotate180deg");
        user.sort((a, b) => $(a).data("countofsurvey")-$(b).data("countofsurvey"));
        numOfSurveyOrder = 1;
    }
    else{
        svg.removeClass("rotate180deg");
        user.sort((a, b) => $(b).data("countofsurvey")-$(a).data("countofsurvey"));
        numOfSurveyOrder = 0;
    }

    $(".user-grid").html(user);
}