using SimpleTools.Mapper.Primitivies;

namespace SimpleTools.Mapper.Extensions;

internal static class FieldCutExtensions
{
    public static void FillValue<TSource>(this FieldCut cut, TSource source)
    {
        var sourceType = source.GetType();
        
        cut.Value = cut.MemberType switch
        {
            MemberType.Field => sourceType.GetField(cut.Name).GetValue(source),
            MemberType.Property => sourceType.GetProperty(cut.Name).GetValue(source)
        };
    }

    public static Dictionary<string, object> ToDictionary(this ICollection<FieldCut> cuts)
    {
        var result = new Dictionary<string, object>();
        cuts.ForEach(c =>
        {
            result.TryAdd(c.Name, c.Value);
        });

        return result;
    }
}