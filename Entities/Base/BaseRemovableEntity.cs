using QuestApi.Entities.Interfaces.Entities;

namespace QuestApi.Entities.Base;
public abstract class BaseRemovableEntity : BaseEntity, IRemovableEntity
{
    public bool? IsDeleted { get; set; }
}