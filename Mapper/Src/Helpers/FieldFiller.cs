using System.Reflection;
using SimpleTools.Mapper.Primitivies;

namespace SimpleTools.Mapper.Helpers;

internal static class FieldFiller
{
    public static TFilled ByCuts<TFilled>(IEnumerable<FieldCut> cuts) where TFilled : new()
    {
        var result = new TFilled();
        
        var resultMembers = result.GetType().GetMembers();
        foreach (var member in resultMembers)
        {
            if (member is PropertyInfo property)
            {
                var receivedType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                var requiredCut = cuts.FirstOrDefault(_ => _.Name == property.Name && _.Type == receivedType);
                property.SetValue(result, requiredCut?.Value);
            }

            if (member is FieldInfo field)
            {
                var receivedType = Nullable.GetUnderlyingType(field.FieldType) ?? field.FieldType;
                var requiredSlice = cuts.FirstOrDefault(_ => _.Name == field.Name && _.Type == receivedType);
                field.SetValue(result, requiredSlice?.Value);
            }
        }

        return result;
    }
}