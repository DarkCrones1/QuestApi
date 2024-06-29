using QuestApi.Entities.Base;

namespace QuestApi.Entities;

public partial class Quest : BaseEntityPagination
{
    public string Name { get; set; } = null!;

    public virtual ICollection<Answer> Answer { get; set; } = new List<Answer>();
}