namespace SimpleTools.Mapper.Abstractions;

public interface IMapAct
{
    public void Do<TResult>(ref TResult result);
}