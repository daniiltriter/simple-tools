using AutoMapper;
using BenchmarkDotNet.Attributes;
using Mapper.Benchmark;
using Mapper.Benchmark.Helpers;
using Microsoft.Extensions.DependencyInjection;
using SimpleTools.Mapper.DI;
using SimpleTools.MapperBenchmark.Objects;
using SimpleTools.MapperPlayground.Models;
using IMapper = SimpleTools.Mapper.Abstractions.IMapper;

namespace SimpleTools.MapperBenchmark;

public class Watcher
{
    private readonly Actor _actor = Preparer.NewActor();
    private IMapper _simpleMapper;
    private AutoMapper.IMapper _autoMapper;
    private IServiceProvider _services;

    [GlobalSetup]
    public void Setup()
    {
        var builder = new ServiceCollection();
        builder.AddMapper(options =>
        {
            options.With<ActorMapConfiguration>();
        });
        var config = new MapperConfiguration(cfg => cfg.AddProfile<MapProfile>());
        
        _services = builder.BuildServiceProvider();
        _simpleMapper = _services.GetService<IMapper>();
        _autoMapper = config.CreateMapper();
    }

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
