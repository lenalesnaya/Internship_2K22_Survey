﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using ItechArt.Survey.DomainModel.SurveyModel.Answers;
using ItechArt.Survey.DomainModel.SurveyModel.Questions;
using ItechArt.Survey.Foundation.QuestionManagement.Abstractions;
using ItechArt.Survey.Foundation.SurveyManagement.Abstractions;
using ItechArt.Survey.Foundation.UserAnswerManagement.Abstractions;
using ItechArt.Survey.WebApp.ViewModels.SurveyEnums;
using ItechArt.Survey.WebApp.ViewModels.SurveyViewModels.Answers;
using ItechArt.Survey.WebApp.ViewModels.SurveyViewModels.Questions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ItechArt.Survey.WebApp.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class SurveyApiController : ControllerBase
{
    private ISurveyService _surveyService;
    private IQuestionService _questionService;
    private IUserAnswerService _userAnswerService;
    private IHttpContextAccessor _httpContextAccessor;
    private IMapper _mapper;

    public SurveyApiController(
        ISurveyService surveyService,
        IHttpContextAccessor httpContextAccessor,
        IQuestionService questionService,
        IMapper mapper,
        IUserAnswerService userAnswerService
        )
    {
        _surveyService = surveyService;
        _httpContextAccessor = httpContextAccessor;
        _questionService = questionService;
        _mapper = mapper;
        _userAnswerService = userAnswerService;
    }
    [Route("{title}/{param}")]
    [HttpPost]
    public async Task<IActionResult> Create(string title, int param)
    {
        var userId = Int32.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        var survey = new DomainModel.SurveyModel.Survey
        {
            Title = title,
            CreationDate = DateTime.Now,
            LastUpdateDate = DateTime.Now,
            CreatorId = userId,
            IsAnonymous = param == (int)SurveyTypeEnum.IsAnonymous
        };

        var result = await _surveyService.CreateSurveyAsync(survey);
        if (!result.IsSuccessful)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [Route("{id:long}")]
    [HttpPost]
    public async Task<IActionResult> Delete(long id)
    {
        var result = await _surveyService.DeleteSurveyByIdAsync(id);
        if (!result.IsSuccessful)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddQuestionWithAnswerVariants([FromBody] AnswerVariantsQuestionViewModel questionViewModel)
    {
        var question = _mapper.Map<AnswerVariantsQuestion>(questionViewModel);

        var result = await _questionService.CreateQuestion(question);
        if (!result.IsSuccessful)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> AddUserAnswer([FromBody] UserAnswerViewModel userAnswerViewModel)
    {
        var userId = Int32.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        var answers = _mapper.Map<IList<UserAnswer>>(userAnswerViewModel.UserAnswers);
        foreach (var answer in answers)
        {
            answer.UserId = userId;
        }
        var result = await _userAnswerService.AddAnswers(answers);
        if (!result.IsSuccessful)
        {
            return BadRequest();
        }

        return Ok();
    }

}