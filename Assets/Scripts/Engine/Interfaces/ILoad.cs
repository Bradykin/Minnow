namespace Game.Util
{
    public interface ILoad<T>
    {
        void LoadFromJson(T jsonData);
    }
}