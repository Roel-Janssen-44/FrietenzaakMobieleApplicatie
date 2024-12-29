

namespace A2D2KrokanteHap.Abstractions
{
    public interface IBaseRepository<T> : IDisposable where T : TableData, new()
    {
        void SaveEntity(T entity);

        T? GetEntity(int id);
        List<T> GetEntities();

        void DeleteEntity(T entity);
    }
}
