using System.Reflection;
using SimpleTools.Mapper.Primitivies;

namespace SimpleTools.Mapper.Helpers;

internal static class FieldFiller
{
    public static TFilled ByCuts<TFilled>(IEnumerable<FieldCut> cuts) where TFilled : new()
    {
        var result = new TFilled();
        var resultType = result.GetType();

        foreach (var cut in cuts)
        {
            if (cut.MemberType == MemberType.Field)
            {
                var requiredField = resultType.GetField(cut.Name);
                if (requiredField == null)
                {
                    continue;
                }
                var fieldType = Nullable.GetUnderlyingType(requiredField.FieldType) ?? requiredField.FieldType;
                if (requiredField.FieldType == fieldType)
                {
                    requiredField.SetValue(result, cut.Value);
                } 
                continue;
            }

            if (cut.MemberType == MemberType.Property)
            {
                var requiredProperty = resultType.GetField(cut.Name);
                if (requiredProperty == null)
                {
                    continue;
                }

                var propertyType = Nullable.GetUnderlyingType(requiredProperty.FieldType) ?? requiredProperty.FieldType;
                if (requiredProperty.FieldType == propertyType)
                {
                    requiredProperty.SetValue(result, cut.Value);
                }
            }
        }
        
        return result;
    }
}