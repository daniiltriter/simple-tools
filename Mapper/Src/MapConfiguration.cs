namespace SimpleTools.Mapper.Abstractions;

public abstract class MapConfiguration
{
    private KeyValuePair<Type, Type> _sourceResultTypePair;
    private IMapAct _mapAct;
    

    protected MapConfiguration()
    {
        Prepare();
    }
    
    protected void ApplyOptions<TSource, TResult>(Action<MapAct<TSource, TResult>> modifiers)
    {
        var act = new MapAct<TSource, TResult>();
        _sourceResultTypePair = new KeyValuePair<Type, Type>(typeof(TSource), typeof(TResult));
        modifiers(act);
        _mapAct = act;
    }

    internal IMapAct Act() => _mapAct;

    public (Type, Type) GetSourceResultPair()
    {
        var (key, value) = _sourceResultTypePair;
        return (key, value);
    }

    protected abstract void Prepare();
}
