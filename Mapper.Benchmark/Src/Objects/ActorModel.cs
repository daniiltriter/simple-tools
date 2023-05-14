namespace SimpleTools.MapperBenchmark.Objects;

public class ActorModel
{
    public string Name { get; set; }
    public ActorRole Role { get; set; }
    public DateTime Updated { get; set; }
    public DateTime Created { get; set; }
    public ulong? GroupId { get; set; }
}