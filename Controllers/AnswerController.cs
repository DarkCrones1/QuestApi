using System.Linq.Expressions;
using System.Net;

using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using QuestApi.Data;
using QuestApi.Dtos.Response;
using QuestApi.Dtos.Create;
using QuestApi.Entities;

namespace QuestApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AnswerController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly QuestDbContext _dbContext;

    public AnswerController(IMapper mapper, QuestDbContext dbContext)
    {
        this._mapper = mapper;
        this._dbContext = dbContext;
    }

    [HttpPost]
    [Route("")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<AnswersResponseDto>))]
    public async Task<IActionResult> CreateAnswer([FromBody] AnswerCreateDto requestDto)
    {
        var entity = _mapper.Map<Answer>(requestDto);
        _dbContext.Add(entity);
        await _dbContext.SaveChangesAsync();

        var result = _mapper.Map<AnswersResponseDto>(entity);
        var response = new ApiResponse<AnswersResponseDto>(result);
        return Ok(response);
    }
}