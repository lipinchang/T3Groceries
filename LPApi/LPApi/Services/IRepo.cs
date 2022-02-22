namespace LPApi.Services
{
    public interface IRepo<K, T>
    {
        ICollection<T> GetAll();
        T GetT(K k);
        ICollection<T> GetSpecific(K id);
        ICollection<T> GetSpecificUsingObj(T t);
        T Add(T item);
        T Remove(K id);
        T Update(T item);
    }
}
