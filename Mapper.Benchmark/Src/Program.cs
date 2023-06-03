using AutoMapper;
using BenchmarkDotNet.Running;
using Mapper.Benchmark;
using Microsoft.Extensions.DependencyInjection;
using SimpleTools.Mapper.DI;
using SimpleTools.MapperBenchmark;
using SimpleTools.MapperBenchmark.Objects;
using SimpleTools.MapperPlayground.Models;
using IMapper = SimpleTools.Mapper.Abstractions.IMapper;

// var builder = new ServiceCollection();
// builder.AddMapper(options =>
// {
//     options.With<ActorMapConfiguration>();
// });
// var config = new MapperConfiguration(cfg => cfg.AddProfile<MapProfile>());
// builder.AddSingleton(config.CreateMapper());

//var services = builder.BuildServiceProvider();

// var autoMapper = services.GetService<AutoMapper.Mapper>();
// var simpleMapper = services.GetService<IMapper>();
// Watcher.SetSimpleMapper(simpleMapper);
// Watcher.SetAutoMapper(autoMapper);

// var actor = new Actor();
// simpleMapper.Map<Actor, ActorModel>(actor);

BenchmarkRunner.Run<Watcher>();