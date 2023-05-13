namespace Mapper.Playground.Models;

public class ActorMapConfiguration : MapConfiguration
{
    protected override void Prepare()
    {
        SetOptions<Actor, ActorModel>(options => options
                .Ignore(_ => _.Name)
        );
    }
}