using SimpleTools.Mapper.Helpers;
using SimpleTools.Mapper.Primitivies;

namespace Mapper;

public class TypeCutCache
{
    private readonly Dictionary<Type, ICollection<FieldCut>> _data = new();
    private readonly Dictionary<Type, Dictionary<string, object>> _dict = new();

    internal ICollection<FieldCut> GetOrAdd<T>(T source)
    {
        var type = typeof(T);
        if (_data.TryGetValue(type, out var result))
        {
            return result;
        }
        
        var cuts = FieldSlicer.Cut(source);
        _data[type] = cuts;

        return cuts;
    }
    
    internal Dictionary<string, object> GetOrAddDict<T>(T source)
    {
        var type = typeof(T);
        if (_dict.TryGetValue(type, out var result))
        {
            return result;
        }
        
        var cuts = FieldSlicer.ByTypeDict(source);
        _dict[type] = cuts;

        return cuts;
    }
}