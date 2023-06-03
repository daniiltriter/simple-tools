using System.Reflection;
using SimpleTools.Mapper.Primitivies;

namespace SimpleTools.Mapper.Helpers;

internal static class FieldFiller
{
    public static TFilled FromCuts<TFilled>(IEnumerable<FieldCut> cuts) where TFilled : new()
    {
        var result = new TFilled();
        var resultType = result.GetType();

        foreach (var cut in cuts)
        {
            switch (cut.MemberType)
            {
                case MemberType.Field:
                {
                    var requiredField = resultType.GetField(cut.Name);
                    if (requiredField == null)
                    {
                        continue;
                    }
                    var fieldType = Nullable.GetUnderlyingType(requiredField.FieldType) ?? requiredField.FieldType;
                    if (cut.Type == fieldType)
                    {
                        requiredField.SetValue(result, cut.Value);
                    }

                    break;
                }
                case MemberType.Property:
                {
                    var requiredProperty = resultType.GetProperty(cut.Name);
                    if (requiredProperty == null)
                    {
                        continue;
                    }

                    var propertyType = Nullable.GetUnderlyingType(requiredProperty.PropertyType) ?? requiredProperty.PropertyType;
                    if (cut.Type == propertyType)
                    {
                        requiredProperty.SetValue(result, cut.Value);
                    }

                    break;
                }
            }
        }
        
        return result;
    }
}