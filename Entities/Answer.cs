using QuestApi.Entities.Base;

namespace QuestApi.Entities;

public partial class Answer : BaseEntityPagination
{
    public string Name { get; set; } = null!;

    public bool IsCorrect { get; set; }

    public int QuestId { get; set; }

    public virtual Quest Quest { get; set; } = null!;
}