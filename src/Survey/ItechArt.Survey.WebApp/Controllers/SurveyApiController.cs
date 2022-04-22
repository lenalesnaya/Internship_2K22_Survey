using System.Threading.Tasks;
using ItechArt.Survey.Foundation.SurveyManagement.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace ItechArt.Survey.WebApp.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class SurveyApiController : ControllerBase
{
    private ISurveyService _surveyService;


    public SurveyApiController(ISurveyService surveyService)
    {
        _surveyService = surveyService;
    }


    [HttpPost]
    public async Task<IActionResult> Create(string title)
    {
        var result = await _surveyService.CreateSurvey(title);
        if (!result.IsSuccessful)
        {
            return BadRequest();
        }

        return Ok(result);
    }
}