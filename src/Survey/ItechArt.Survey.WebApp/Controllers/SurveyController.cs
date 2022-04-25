using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using ItechArt.Survey.Foundation.SurveyManagement.Abstractions;
using ItechArt.Survey.WebApp.ViewModels.SurveyViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ItechArt.Survey.WebApp.Controllers;

public class SurveyController : Controller
{
    private readonly ISurveyService _surveyService;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SurveyController(
        ISurveyService surveyService,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor)
    {
        _surveyService = surveyService;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }


    [HttpGet]
    public async Task<IActionResult> SurveySettings(int id)
    {
        var survey = await _surveyService.GetSurveyById(id);
        var surveyViewModel = _mapper.Map<SurveyViewModel>(survey);

        return View(surveyViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> DisplayAllSurveys()
    {
        var userId = Int32.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        var surveysViewModels = (await _surveyService
            .GetAllSurveysByUserId(userId))
                .Select(dbModel => _mapper.Map<SurveyViewModel>(dbModel))
                .ToList();

        return View(surveysViewModels);
    }
}