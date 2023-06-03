using SimpleTools.Mapper.Primitivies;

namespace Mapper;

internal class MapApplicator<TSource, TResult, TMember> : IMapApplicator<TSource, TResult, TMember>
{
    private readonly ICollection<MapCriterion> _criteria = new List<MapCriterion>();
    public void Apply(TSource source, ICollection<FieldCut> cuts)
    {
        foreach (var cut in cuts)
        {
            if (!_criteria.Any())
            {
                break;
            }
            var requiredCriteria = _criteria.FirstOrDefault(c => c.FieldName == cut.Name);
            if (requiredCriteria != null)
            {
                var actionType = requiredCriteria.Action.GetType();
                var result = requiredCriteria.Action;
                cut.Value = result;
            }
        }
    }
}