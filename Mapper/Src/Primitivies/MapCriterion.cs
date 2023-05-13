namespace SimpleTools.Mapper.Primitivies;

internal class MapCriterion
{
    public string FieldName { get; init; }
    public Func<object, object> Action { get; init; }
}