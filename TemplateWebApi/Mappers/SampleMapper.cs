using TemplateWebApi.Contracts.Responses;
using TemplateWebApi.Entities;

namespace TemplateWebApi.Mappers;

public static class SampleMapper
{
    public static ICollection<GetSampleResponse> MapToSample(this ICollection<Entity> samples)
    {
        return null;
    }
}