namespace ATM.Interfaces
{
    public interface IAuthentication<T>
    {
        T? Authenticate(string username, string password);
    }
}
