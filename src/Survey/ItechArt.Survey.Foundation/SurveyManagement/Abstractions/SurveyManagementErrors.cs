namespace ItechArt.Survey.Foundation.SurveyManagement.Abstractions;

public enum SurveyManagementErrors
{
    SurveyCreationIsFailed,
    SurveyUpdatingIsFailed,
    SurveyDeletingIsFailed,
    QuestionCreationIsFailed,
    QuestionUpdatingIsFailed,
    QuestionDeletingIsFailed,
    AnswerVariantCreationIsFailed,
    AnswerVariantUpdatingIsFailed,
    AnswerVariantDeletingIsFailed,
    TitleIsRequired,
    InvalidTitleLength,
    QuestionTextIsRequired,
    InvalidQuestionTextLength,
    AnswerVariantTextIsRequired,
    InvalidAnswerVariantTextLength,
    InvalidScaleMaxValue,
    InvalidAmountOfStars
}