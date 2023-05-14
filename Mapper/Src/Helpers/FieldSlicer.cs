using System.Reflection;
using SimpleTools.Mapper.Extensions;
using SimpleTools.Mapper.Primitivies;

namespace SimpleTools.Mapper.Helpers;

internal static class FieldSlicer
{
    public static IEnumerable<FieldCut> ByObject<TSource>(TSource source)
    {
        var cuts = new List<FieldCut>();
        var members = typeof(TSource).GetMembers();
        members.ForEach(m =>
        {
            if (m is PropertyInfo property)
            {
                var propertyType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                var cut = new FieldCut
                {
                    Name = property.Name,
                    Type = propertyType, 
                    Value = property.GetValue(source)
                };
                cuts.Add(cut);
            }
            
            if (m is FieldInfo field)
            {
                var fieldType = Nullable.GetUnderlyingType(field.FieldType) ?? field.FieldType;
                var cut = new FieldCut
                {
                    Name = field.Name,
                    Type = fieldType, 
                    Value = field.GetValue(source)
                };
                cuts.Add(cut);
            }
        });
        return cuts;
    }
    
    public static ICollection<FieldCut> ByType<TSource>()
    {
        var cuts = new List<FieldCut>();
        var members = typeof(TSource).GetMembers();
        members.ForEach(m =>
        {
            if (m is PropertyInfo property)
            {
                var propertyType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                var cut = new FieldCut
                {
                    Name = property.Name,
                    Type = propertyType, 
                    Value = default,
                    MemberType = MemberType.Property
                };
                cuts.Add(cut);
            }
            
            if (m is FieldInfo field)
            {
                var fieldType = Nullable.GetUnderlyingType(field.FieldType) ?? field.FieldType;
                var cut = new FieldCut
                {
                    Name = field.Name,
                    Type = fieldType, 
                    Value = default,
                    MemberType = MemberType.Field
                };
                cuts.Add(cut);
            }
        });
        return cuts;
    }
}