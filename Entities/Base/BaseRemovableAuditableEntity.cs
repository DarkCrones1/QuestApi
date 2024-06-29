using QuestApi.Entities.Interfaces.Entities;

namespace QuestApi.Entities.Base;

public abstract class BaseRemovableAuditableEntity : BaseAuditableEntity, IRemovableEntity
{
    public bool? IsDeleted { get; set; }
}