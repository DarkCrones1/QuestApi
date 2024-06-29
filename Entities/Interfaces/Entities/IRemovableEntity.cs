namespace QuestApi.Entities.Interfaces.Entities;

public interface IRemovableEntity : IBaseQueryable
{
    public bool? IsDeleted { get; set; }
}