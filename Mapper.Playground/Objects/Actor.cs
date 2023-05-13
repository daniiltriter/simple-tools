namespace SimpleTools.MapperPlayground.Objects;

public class Actor
{
    public ulong Id { get; set; }
    public string Name { get; set; }
    public ActorRole Role { get; set; }
    public ActorSettings Settings { get; set; }

    public static Actor Make(string name, ActorRole role) => new Actor
    {
        Name = name,
        Role = role
    };
}