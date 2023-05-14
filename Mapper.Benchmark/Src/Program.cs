using AutoMapper;
using BenchmarkDotNet.Running;
using Microsoft.Extensions.DependencyInjection;
using SimpleTools.MapperBenchmark;
using SimpleTools.MapperBenchmark.Objects;
using Mapper = AutoMapper.Mapper;

BenchmarkRunner.Run<Watcher>();

// var mapper = Preparer.NewMapper();
// var actor = Preparer.NewActor();
//
// var model = mapper.Map<Actor, ActorModel>(actor);