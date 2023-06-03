using SimpleTools.Mapper.Configurations;
using SimpleTools.MapperBenchmark.Objects;

namespace SimpleTools.MapperPlayground.Models;

public class ActorMapConfiguration : MapConfiguration
{
    protected override void Prepare()
    {
        SetOptions<Actor, ActorModel>(options =>
        {
            options.Ignore(_ => _.Name);
        });
    }
}