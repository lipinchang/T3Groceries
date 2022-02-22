namespace GatewayAPI.Services
{
    public interface IManageUser<T>
    {
        Task<T> Add(T user);
        Task<T> Login(T user);
    }
}
