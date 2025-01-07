

using SQLite;
using A2D2KrokanteHap.Abstractions;
using System.Linq.Expressions;
using SQLiteNetExtensions.Extensions;

namespace A2D2KrokanteHap.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : TableData, new()
    {

        SQLiteConnection connection;

        public string? statusMessage { get; set; }

        public BaseRepository()
        {
            connection = new SQLiteConnection(
                Constants.DatabasePath,
                Constants.flags);
            connection.CreateTable<T>();
        }


        public void DeleteEntity(T entity)
        {
            try
            {
                connection.Delete(entity);

            }
            catch (Exception ex)
            {
                statusMessage = $"Error: {ex.Message}";

            }
        }

        public void Dispose()
        {
            connection.Dispose();
        }

        public List<T>? GetEntities()
        {
            try
            {
                return connection.Table<T>().ToList();
            }
            catch (Exception ex)
            {
                statusMessage = $"Error: {ex.Message}";
            }
            return null;
        }

        public T? GetEntity(int id)
        {
            try
            {
                return connection.Table<T>().FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                statusMessage = $"Error: {ex.Message}";
            }
            return null;
        }
        
        public T? GetEntityWithChildren(int id)
        {
            try
            {
                 return connection.GetWithChildren<T>(id, recursive: true);
            }
            catch (Exception ex)
            {
                statusMessage = $"Error: {ex.Message}";
            }
            return null;
        }

        public int SaveEntity(T entity)
        {
            int result = 0;
            if (entity != null)
            {
                try
                {
                    if (entity.Id != 0)
                    {
                        result = connection.Update(entity);
                        statusMessage = $"{result} row(s) updated";
                    }
                    else
                    {
                        result = connection.Insert(entity);
                        statusMessage = $"{result} row(s) added";

                    }
                }
                catch (Exception ex)
                {
                    statusMessage = $"Error {ex.Message}";
                }
            }
            return entity.Id;
        }

        // **New Method: Get entities by condition**
        public T? GetByCondition(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return connection.Table<T>().FirstOrDefault(predicate);
            }
            catch (Exception ex)
            {
                statusMessage = $"Error: {ex.Message}";
            }
            return null;
        }

        public int SaveEntityWithChildren(T entity, bool recursive = false)
        {
            connection.InsertWithChildren(entity, recursive);
            return entity.Id;

        }

        public List<T> GetEntitiesWithChildren()
        {
            try
            {
                return connection.GetAllWithChildren<T>(recursive: true).ToList();
            }
            catch (Exception ex)
            {
                statusMessage = $"Error: {ex.Message}";
            }
            return null;
        }

        public void DeleteEntityWithChildren(T entity)
        {
            try
            {
                connection.Delete(entity, true);
            }
            catch (Exception ex)
            {
                statusMessage = $"Error: {ex.Message}";
            }
        }

        public void DeleteAllEntities()
        {
            try
            {
                connection.DeleteAll<T>(); // Deletes all rows in the table
                statusMessage = "All entities deleted successfully.";
            }
            catch (Exception ex)
            {
                statusMessage = $"Error: {ex.Message}";
            }
        }

    }
}
