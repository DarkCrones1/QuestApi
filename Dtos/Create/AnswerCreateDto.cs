namespace QuestApi.Dtos.Create;

public class AnswerCreateDto
{
    public string Name { get; set; } = null!;

    public bool IsCorrect { get; set; }

    public int QuestId { get; set; }
}