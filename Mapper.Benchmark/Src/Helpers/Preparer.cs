using AutoMapper;
using Bogus;
using SimpleTools.Mapper;
using SimpleTools.Mapper.DI;
using SimpleTools.MapperBenchmark.Objects;
using SimpleTools.MapperPlayground.Models;
using IMapper = SimpleTools.Mapper.Abstractions.IMapper;

namespace Mapper.Benchmark.Helpers;

public static class Preparer
{
    // public static IMapper NewSimpleMapper()
    // {
    //     var rawOptions = new MapperOptions();
    //     var completedOptions = rawOptions.With<ActorMapConfiguration>();
    //     var cuts = new TypeCutCache();
    //     
    //     return new DefaultMapper(completedOptions, cuts);
    // }
    
    public static AutoMapper.IMapper NewAutoMapper()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<MapProfile>());
        return config.CreateMapper();
    }

    public static Actor NewActor() => new Actor()
    {
        Name = new Faker().Random.String(15),
        Updated = new Faker().Date.Soon(),
        Created = new Faker().Date.Past(),
        Role = new Faker().PickRandom<ActorRole>(),
        GroupId = new Faker().Random.ULong()
    };
}