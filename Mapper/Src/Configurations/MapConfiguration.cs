using SimpleTools.Mapper.Abstractions;
using SimpleTools.Mapper.Primitivies;

namespace SimpleTools.Mapper.Configurations;

public abstract class MapConfiguration
{
    private KeyValuePair<Type, Type> _sourceResultTypePair;
    private readonly ICollection<MapCriterion> _сriteria = new List<MapCriterion>();

    protected MapConfiguration()
    {
        Prepare();
    }
    
    protected void SetOptions<TSource, TResult>(Action<IMapAct<TSource, TResult>> modifiers)
    {
        var act = new MapAct<TSource, TResult>();
        _sourceResultTypePair = new KeyValuePair<Type, Type>(typeof(TSource), typeof(TResult));
        modifiers(act);
        foreach (var criterion in act.TransitCriteria())
        {
            _сriteria.Add(criterion);
        }
    }

    internal void Apply<TResult>(ref TResult result)
    {
        foreach (var сriterion in _сriteria)
        {
            сriterion.Action(result);
        }
    }

    internal (Type, Type) GetTypePair()
    {
        var (key, value) = _sourceResultTypePair;
        return (key, value);
    }

    protected abstract void Prepare();
}
