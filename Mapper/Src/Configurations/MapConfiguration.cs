using SimpleTools.Mapper.Abstractions;
using SimpleTools.Mapper.Primitivies;

namespace SimpleTools.Mapper.Configurations;

public abstract class MapConfiguration
{
    private KeyValuePair<Type, Type> _sourceResultTypePair;
    private readonly ICollection<MapCriterion> _criteria = new List<MapCriterion>();

    protected MapConfiguration()
    {
        Prepare();
    }
    
    protected void SetOptions<TSource, TResult>(Action<IMapAct<TSource, TResult>> modifiers)
    {
        var act = new MapAct<TSource, TResult>();
        _sourceResultTypePair = new KeyValuePair<Type, Type>(typeof(TSource), typeof(TResult));
        modifiers(act);
        var criteria = act.TransitCriteria();
        foreach (var criterion in criteria)
        {
            _criteria.Add(criterion);
        }
    }

    internal void Apply<TSource, TResult, TMember>(TSource source, FieldCut cut)
    {
        var required = _criteria.FirstOrDefault(c => c.FieldName == cut.Name);
        cut.Value = ((Func<TSource, TMember>)(object)required.Action)(source);
    }

    internal (Type, Type) GetTypePair()
    {
        var (key, value) = _sourceResultTypePair;
        return (key, value);
    }

    protected abstract void Prepare();
}
