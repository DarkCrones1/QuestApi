namespace QuestApi.Dtos.Response;

public class AnswersResponseDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool IsCorrect { get; set; }

    public int QuestId { get; set; }
}