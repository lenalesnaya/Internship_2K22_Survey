using AutoMapper;
using ItechArt.Survey.Foundation.SurveyManagement.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace ItechArt.Survey.WebApp.Controllers;

public class SurveyController : Controller
{
    private ISurveyService _surveyService;
    private IMapper _mapper;

    public SurveyController(ISurveyService surveyService, IMapper mapper)
    {
        _surveyService = surveyService;
        _mapper = mapper;
    }


    [HttpGet]
    public IActionResult CreatingASurvey()
    {
        return View();
    }

    // public IActionResult AllSurvey()
    // {
    //     
    // }
    // [HttpPost]
    // public async Task<IActionResult> CreatingASurvey(SurveyViewModel surveyViewModel)
    // {
    //     var survey = _mapper.Map<DomainModel.SurveyModel.Survey>(surveyViewModel); 
    //     var creatingResult = await _surveyService.CreateSurvey();
    //     if (!creatingResult.IsSuccessful)
    //     {
    //         //tut error
    //         return View();
    //     }
    //
    //     return View();
    // }
}