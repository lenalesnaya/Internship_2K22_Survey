using System;
using System.Security.Claims;
using System.Threading.Tasks;
using ItechArt.Survey.Foundation.SurveyManagement.Abstractions;
using ItechArt.Survey.WebApp.ViewModels.SurveyEnums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ItechArt.Survey.WebApp.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class SurveyApiController : ControllerBase
{
    private readonly ISurveyService _surveyService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SurveyApiController(
        ISurveyService surveyService,
        IHttpContextAccessor httpContextAccessor)
    {
        _surveyService = surveyService;
        _httpContextAccessor = httpContextAccessor;
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
}