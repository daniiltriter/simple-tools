using SimpleTools.Mapper.Configurations;

namespace SimpleTools.MapperPlayground.Models;

public class ActorMapConfiguration : MapConfiguration
{
    protected override void Prepare()
    {
        SetOptions<Actor, ActorModel>(options =>
        {
            options.Ignore(_ => _.Role);
            //options.Alternate(_ => _.Name, _ => "lol");
        });
    }
}