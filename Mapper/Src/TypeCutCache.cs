using SimpleTools.Mapper.Helpers;
using SimpleTools.Mapper.Primitivies;

namespace Mapper;

public class TypeCutCache
{
    private readonly Dictionary<Type, ICollection<FieldCut>> _data = new Dictionary<Type, ICollection<FieldCut>>();

    internal ICollection<FieldCut> GetOrAdd<T>()
    {
        var type = typeof(T);
        if (_data.TryGetValue(type, out var result))
        {
            return result;
        }
        
        var cuts = FieldSlicer.ByType<T>();
        _data[type] = cuts;

        return cuts;
    }
}