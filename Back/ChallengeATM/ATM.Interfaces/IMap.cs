namespace ATM.Interfaces
{
    public interface IMap<T, U> where T : class
    {
        T? Map(U source);

        U? Map(T source);
    }
}
