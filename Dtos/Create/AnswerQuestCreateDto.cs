namespace QuestApi.Dtos.Create;

public class AnswerQuestCreateDto
{
    public string Name { get; set; } = null!;

    public bool IsCorrect { get; set; }
}