namespace T3PersonalWkSpcApp.Services
{
    public interface IRepo<K, T>
    {
        //ICollection<T> GetAll();
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(K k);
        Task<IEnumerable<T>> GetSpecific(K k);
        Task<IEnumerable<T>> GetSpecificUsingObj(T t);
        Task<T> Add(T item);
        Task<T> Remove(K id);
        Task<T> Update(T item);
        void GetToken(string token);
    }
}
