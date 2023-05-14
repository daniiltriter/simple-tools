using BenchmarkDotNet.Attributes;
using Bogus;
using Mapper;
using Mapper.Benchmark.Helpers;
using SimpleTools.Mapper;
using SimpleTools.Mapper.Abstractions;
using SimpleTools.Mapper.DI;
using SimpleTools.MapperBenchmark.Objects;
using SimpleTools.MapperPlayground.Models;

namespace SimpleTools.MapperBenchmark;

public class Watcher
{
    private Actor _actor = Preparer.NewActor();
    private readonly IMapper _simpleMapper = Preparer.NewSimpleMapper();
    private readonly AutoMapper.IMapper _autoMapper = Preparer.NewAutoMapper();
    

    [Benchmark]
    public ActorModel SimpleMapper()
    {
        return _simpleMapper.Map<Actor, ActorModel>(_actor);
    }
    
    [Benchmark]
    public ActorModel AutoMapper()
    {
        return _autoMapper.Map<Actor, ActorModel>(_actor);
    }
}