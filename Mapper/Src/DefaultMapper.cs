using System.Reflection;
using SimpleTools.Mapper.Abstractions;
using SimpleTools.Mapper.Primitivies;

namespace SimpleTools.Mapper;

public class DefaultMapper : IMapper
{
    public TResult Map<TSource, TResult>(TSource source) where TResult : new()
    {
        var sourceCuts = new List<FieldCut>();
        var members = typeof(TSource).GetMembers();
        foreach (var member in members)
        {
            if (member is PropertyInfo property)
            {
                var cut = new FieldCut(property.Name, property.PropertyType, property.GetValue(source));
                sourceCuts.Add(cut);
            }
            
            if (member is FieldInfo field)
            {
                var cut = new FieldCut(field.Name, field.FieldType, field.GetValue(source));
                sourceCuts.Add(cut);
            }
        }

        var result = new TResult();
        
        var resultMembers = result.GetType().GetMembers();
        foreach (var member in resultMembers)
        {
            if (member is PropertyInfo property)
            {
                var requiredCut = sourceCuts.FirstOrDefault(_ => _.Name == property.Name && _.Type == property.PropertyType);
                property.SetValue(result, requiredCut?.Value);
            }

            if (member is FieldInfo field)
            {
                var requiredSlice = sourceCuts.FirstOrDefault(_ => _.Name == field.Name && _.Type == field.FieldType);
                field.SetValue(result, requiredSlice?.Value);
            }
        }

        return result;
    }
}