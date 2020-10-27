namespace Game.Util
{
    public interface ISave<T>
    {
        T SaveToJson();
    }
}