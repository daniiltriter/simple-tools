namespace SimpleTools.MapperBenchmark.Objects;

public class Actor
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ActorRole Role { get; set; }
    public DateTime Updated { get; set; }
    public DateTime Created { get; set; }
    public ulong? GroupId { get; set; }
}