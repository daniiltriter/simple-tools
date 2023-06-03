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
            else if (m is FieldInfo field)
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
    
    public static ICollection<FieldCut> Cut<TSource>(TSource source)
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
                    Value = property.GetValue(source),
                    MemberType = MemberType.Property
                };
                cuts.Add(cut);
            }
            else if (m is FieldInfo field)
            {
                var fieldType = Nullable.GetUnderlyingType(field.FieldType) ?? field.FieldType;
                var cut = new FieldCut
                {
                    Name = field.Name,
                    Type = fieldType, 
                    Value = field.GetValue(source),
                    MemberType = MemberType.Field
                };
                cuts.Add(cut);
            }
        });
        return cuts;
    }
    
    public static Dictionary<string, object> ByTypeDict<TSource>(TSource source)
    {
        var cuts = new Dictionary<string, object>();
        var members = typeof(TSource).GetMembers();
        members.ForEach(m =>
        {
            if (m is PropertyInfo property)
            {
                cuts.TryAdd(property.Name, property.GetValue(source));
            }
            else if (m is FieldInfo field)
            {
                cuts.TryAdd(field.Name, field.GetValue(source));
            }
        });
        return cuts;
    }
}