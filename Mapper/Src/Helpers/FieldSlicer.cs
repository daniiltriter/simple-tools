using System.Reflection;
using SimpleTools.Mapper.Extensions;
using SimpleTools.Mapper.Primitivies;

namespace SimpleTools.Mapper.Helpers;

internal static class FieldSlicer
{
    public static IEnumerable<FieldCut> Cuts<TSource>(TSource source)
    {
        var cuts = new List<FieldCut>();
        var members = typeof(TSource).GetMembers();
        members.ForEach(m =>
        {
            if (m is PropertyInfo property)
            {
                var cut = new FieldCut(property.Name, property.PropertyType, property.GetValue(source));
                cuts.Add(cut);
            }
            
            if (m is FieldInfo field)
            {
                var cut = new FieldCut(field.Name, field.FieldType, field.GetValue(source));
                cuts.Add(cut);
            }
        });
        return cuts;
    }
    
    public static bool TryCut<TSource>(MemberInfo member, out FieldCut result)
    {
        if (member is PropertyInfo property)
        {
            result = new FieldCut(property.Name, property.PropertyType, property.GetValue(member));
            return true;
        }
        
        if (member is FieldInfo field)
        {
            result = new FieldCut(field.Name, field.FieldType, field.GetValue(member));
            return true;
        }

        result = null;
        return false;
    }
    
    public static FieldCut Cut<TSource>(MemberInfo member)
    {
        if (member is PropertyInfo property)
        {
            return new FieldCut(property.Name, property.PropertyType, property.GetValue(member));
        }
        
        if (member is FieldInfo field)
        {
            return new FieldCut(field.Name, field.FieldType, field.GetValue(member));
        }

        return null;
    }
}