using AutoMapper;
using SimpleTools.MapperBenchmark.Objects;

namespace Mapper.Benchmark;

public class MapProfile : Profile
{
    public MapProfile()
    {
        CreateMap<Actor, ActorModel>().ForMember(_ => _.Name, _ => _.MapFrom(_ => _.Name + "dasda"));
    }
}