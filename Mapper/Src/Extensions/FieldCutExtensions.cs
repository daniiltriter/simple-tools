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
}