using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using ItechArt.Repositories.Abstractions;
using ItechArt.Survey.DomainModel.SurveyModel.Answers;
using ItechArt.Survey.Foundation.AnswerManagement.Abstrations;
using ItechArt.Survey.Foundation.SurveyManagement.Abstractions;
using ItechArt.Survey.Foundation.UserManagement.Abstractions;
using ItechArt.Survey.WebApp.ViewModels.SurveyViewModels;
using ItechArt.Survey.WebApp.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ItechArt.Survey.WebApp.Controllers;

public class SurveyController : Controller
{
    private readonly ISurveyService _surveyService;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAnswerService _answerService;
    private readonly IUserService _userService;

    public SurveyController(
        ISurveyService surveyService,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        IUnitOfWork unitOfWork,
        IAnswerService answerService,
        IUserService userService)
    {
        _surveyService = surveyService;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor; 
        _unitOfWork = unitOfWork;
        _answerService = answerService;
        _userService = userService;
    }


    [HttpGet]
    [Authorize]
    public async Task<IActionResult> SurveySettings(int id)
    {
        var survey = await _surveyService.GetSurveyByIdAsync(id);
        var surveyViewModel = _mapper.Map<SurveyViewModel>(survey);

        return View(surveyViewModel);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> DisplayAllUserSurveys()
    {
        var userId = Int32.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        var surveysViewModels = (await _surveyService
                .GetAllSurveysByUserIdAsync(userId))
                .Select(dbModel => _mapper.Map<SurveyViewModel>(dbModel))
                .ToList();

        return View(surveysViewModels);
    }

    [HttpGet]
    public async Task<IActionResult> DisplayAllSurveys()
    {
        var surveysViewModel = (await _surveyService.GetAllSurveys())
            .Select(dbModel => _mapper.Map<SurveyViewModel>(dbModel))
            .ToList();

        return View(surveysViewModel);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> TakeASurvey(long id)
    {
        var survey = await _surveyService.GetSurveyByIdAsync(id);
        var surveyViewModel = _mapper.Map<SurveyViewModel>(survey);

        return View(surveyViewModel);
    }

    public async Task<IActionResult> DisplaySurveyStatistics(long id)
    {
        var survey = await _surveyService.GetSurveyByIdAsync(id);
        var answersIds = survey
            .AnswerVariantsQuestions
            .SelectMany(q => q.AnswerVariants.Select(a => a.Id).ToList()).ToList();

        var userAnswers = await _unitOfWork
            .GetRepository<UserAnswer>()
            .GetWhereAsync(ua=> answersIds.Contains(ua.AnswerVariantId));

        var userAnswersIdCount = userAnswers
            .GroupBy(u => u.AnswerVariantId)
        .ToDictionary(
            ua => ua.Key,
            ua => ua.Count());

        return View(userAnswersIdCount);
    }

    [HttpGet]
    public async Task<IActionResult> TheEndTakeASurvey()
    {
        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userService.GetUserByIdAsync(userId);
        var userViewModel = _mapper.Map<UserViewModel>(user);
        return View(userViewModel);
    }

    public async Task<JsonResult> GetSearchingData(string searchValue)
    {
        var rep = _unitOfWork.GetRepository<DomainModel.SurveyModel.Survey>();
        var stuList = (await rep.GetWhereAsync(x => x.Title.StartsWith(searchValue) || searchValue == null))
            .ToList();

        return Json(stuList);
    }
}