using System.Linq.Expressions;

namespace SimpleTools.Mapper.Primitivies;

internal class MapCriterion
{
    public string FieldName { get; init; }
    public object Action { get; init; }
    public Type MemberType { get; init; }
}