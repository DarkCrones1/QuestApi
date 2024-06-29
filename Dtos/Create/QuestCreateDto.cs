namespace QuestApi.Dtos.Create;

public class QuestCreateDto
{
    private IEnumerable<AnswerQuestCreateDto> _answer;

    public QuestCreateDto()
    {
        _answer = new List<AnswerQuestCreateDto>();
    }

    public string Name { get; set; } = null!;

    public IEnumerable<AnswerQuestCreateDto> Answers { get => _answer; set => _answer = value ?? new List<AnswerQuestCreateDto>(); }
}