

namespace A2D2KrokanteHap.Abstractions
{
    public interface IBaseRepository<T> : IDisposable where T : TableData, new()
    {
        void SaveEntity(T entity);
        void SaveEntityWithChildren(T entity, bool recursive = false);


        T? GetEntity(int id);
        List<T> GetEntities();
        List<T> GetEntitiesWithChildren();


        void DeleteEntity(T entity);
        void DeleteEntityWithChildren(T entity);
    }
}
