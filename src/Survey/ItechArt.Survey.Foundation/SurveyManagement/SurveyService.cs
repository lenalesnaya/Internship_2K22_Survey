// using System.Threading.Tasks;
// using ItechArt.Common;
// using ItechArt.Survey.Foundation.SurveyManagement.Abstractions;
// using ItechArt.Survey.Foundation.SurveyManagement.Stores;
//
// namespace ItechArt.Survey.Foundation.SurveyManagement;
//
// public class SurveyService : ISurveyService
// {
//     private readonly SurveyStore _surveyStore;
//
//
//     public SurveyService(SurveyStore surveyStore)
//     {
//         _surveyStore = surveyStore;
//     }
//
//     public async Task<OperationResult<SurveyManagementErrors>> CreateSurvey()
//     {
//         var creatingResult = await _surveyStore.CreateAsync();
//     }
// }