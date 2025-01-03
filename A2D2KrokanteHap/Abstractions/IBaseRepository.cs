

namespace A2D2KrokanteHap.Abstractions
{
    public interface IBaseRepository<T> : IDisposable where T : TableData, new()
    {
        int SaveEntity(T entity);
        int SaveEntityWithChildren(T entity, bool recursive = false);


        T? GetEntity(int id);
        List<T> GetEntities();
        List<T> GetEntitiesWithChildren();


        void DeleteEntity(T entity);
        void DeleteEntityWithChildren(T entity);
    }
}
