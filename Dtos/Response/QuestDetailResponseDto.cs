namespace QuestApi.Dtos.Response;

public class QuestDetailResponseDto
{
    private IEnumerable<AnswersResponseDto> _answer;

    public QuestDetailResponseDto()
    {
        _answer = new List<AnswersResponseDto>();
    }

    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public IEnumerable<AnswersResponseDto> Answer { get => _answer; set => _answer = value; }
}