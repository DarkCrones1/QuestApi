using System.ComponentModel.DataAnnotations.Schema;

using QuestApi.Entities.Interfaces.Entities;

namespace QuestApi.Entities.Base;

public abstract class BaseRemovableAuditablePaginationEntity : BaseRemovableAuditableEntity, IPaginationQueryable
{
    [NotMapped]
    public int PageSize { get; set; }

    [NotMapped]
    public int PageNumber { get; set; }
}