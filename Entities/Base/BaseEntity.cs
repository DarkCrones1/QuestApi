using QuestApi.Entities.Interfaces.Entities;

namespace QuestApi.Entities.Base;

public abstract class BaseEntity : IBaseQueryable
{
    public int Id { get; set; }
}