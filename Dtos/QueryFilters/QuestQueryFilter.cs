using QuestApi.Entities.Interfaces.Entities;
using QuestApi.Filters;

namespace QuestApi.Dtos.QueryFilters;

public class QuestQueryFilter :PaginationControlRequestFilter, IBaseQueryFilter
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
}