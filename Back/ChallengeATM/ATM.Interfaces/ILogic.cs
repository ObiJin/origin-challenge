namespace ATM.Interfaces
{
    public interface ILogic<T>
    {
        T? Find(T param);

        T? Find(int id);
    }
}
