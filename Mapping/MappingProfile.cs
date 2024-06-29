using AutoMapper;
using QuestApi.Dtos.Create;
using QuestApi.Dtos.QueryFilters;
using QuestApi.Dtos.Response;
using QuestApi.Entities;

namespace QuestApi.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AnswerCreateDto, Answer>()
        .ForMember(
            dest => dest.IsCorrect,
            opt => opt.MapFrom(src => src.IsCorrect)
        ).ForMember(
            dest => dest.QuestId,
            opt => opt.MapFrom(src => src.QuestId)
        ).ForMember(
            dest => dest.Name,
            opt => opt.MapFrom(src => src.Name)
        );

        CreateMap<Answer, AnswersResponseDto>();

        CreateMap<Quest, QuestResponseDto>();

        CreateMap<Quest, QuestDetailResponseDto>();

        CreateMap<QuestCreateDto, Quest>()
        .AfterMap(
            (src, dest) =>
            {
                foreach (var item in src.Answers)
                {
                    var answer = new Answer
                    {
                        Name = item.Name,
                        IsCorrect = item.IsCorrect,
                    };
                    dest.Answer.Add(answer);
                }
            }
        );

        CreateMap<Quest, QuestQueryFilter>();
    }
}