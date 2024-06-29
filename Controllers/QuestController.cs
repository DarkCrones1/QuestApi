using System.Linq.Expressions;
using System.Net;

using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using QuestApi.Data;
using QuestApi.Dtos.Response;
using QuestApi.Dtos.Create;
using QuestApi.Entities;
using QuestApi.Dtos.QueryFilters;
using QuestApi.Filters.Exceptions;
using Microsoft.EntityFrameworkCore;
using QuestApi.Entities.Base;

namespace QuestApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class QuestController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly QuestDbContext _dbContext;

    public QuestController(IMapper mapper, QuestDbContext dbContext)
    {
        this._mapper = mapper;
        this._dbContext = dbContext;
    }

    [HttpGet]
    [Route("")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<QuestResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<QuestResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<QuestResponseDto>>))]
    public async Task<IActionResult> Get([FromQuery] QuestQueryFilter filter)
    {
        try
        {
            var entities = await GetPageds(filter);
            var dtos = _mapper.Map<IEnumerable<QuestResponseDto>>(entities);
            var metaDataResponse = new MetaDataResponse(
                entities.TotalCount,
                entities.CurrentPage,
                entities.PageSize
            );
            var response = new ApiResponse<IEnumerable<QuestResponseDto>>(data: dtos, meta: metaDataResponse);
            return Ok(response);
        }
        catch (Exception ex)
        {
            throw new LogicBusinessException(ex);
        }
    }

    [HttpGet]
    [Route("{id:int}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<QuestDetailResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<QuestDetailResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<QuestDetailResponseDto>>))]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        try
        {
            var entity = await _dbContext.Quest.FirstOrDefaultAsync(x => x.Id == id);

            var dto = _mapper.Map<QuestDetailResponseDto>(entity);
            var response = new ApiResponse<QuestDetailResponseDto>(data: dto);
            return Ok(response);
        }
        catch (Exception ex)
        {
            throw new LogicBusinessException(ex);
        }
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<QuestResponseDto>))]
    public async Task<IActionResult> Create([FromBody] QuestCreateDto requestDto)
    {
        try
        {
            var entity = _mapper.Map<Quest>(requestDto);
            await _dbContext.Quest.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            var result = _mapper.Map<QuestResponseDto>(entity);
            var response = new ApiResponse<QuestResponseDto>(result);
            return Ok(response);
        }
        catch (Exception ex)
        {
            throw new LogicBusinessException(ex);
        }
    }

    private async Task<PagedList<Quest>> GetPageds(QuestQueryFilter filter)
    {
        var result = await GetPaged(filter);
        var pagedItems = PagedList<Quest>.Create(result, filter.PageNumber, filter.PageSize);
        return pagedItems;
    }

    private async Task<IEnumerable<Quest>> GetPaged(QuestQueryFilter entity)
    {
        var query = _dbContext.Quest.AsQueryable();

        if (entity.Id > 0)
            query = query.Where(x => x.Id == entity.Id);

        if (!string.IsNullOrEmpty(entity.Name) && !string.IsNullOrWhiteSpace(entity.Name))
            query = query.Where(x => x.Name.Contains(entity.Name));

        return await query.ToListAsync();
    }

}