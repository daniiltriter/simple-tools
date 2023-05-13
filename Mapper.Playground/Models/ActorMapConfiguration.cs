namespace Mapper.Playground.Models;

public class ActorMapConfiguration : MapConfiguration
{
    protected override void Prepare()
    {
        ApplyOptions<Actor, ActorModel>(options => options
                .Ignore(_ => _.Name)
                //.ChangeValueFor(r => r.Name, s => s.Name + "-lol")
        );
    }
}