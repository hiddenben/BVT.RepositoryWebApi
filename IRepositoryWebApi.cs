using System.Collections.Generic;

namespace BVT.RepositoryWebApi
{
    public interface IRepositoryWebApi<T> where T : class
    {         
        void Delete(T item);
        void Delete<TProperty>(TProperty id);
        
        List<T> Get();
        List<T> Get(string actionName);
        List<T> Get<TProperty>(string actionName, TProperty id);
        List<T> Get<TProperty>(string actionName, object args);
        T GetById<TProperty>(TProperty id);
        T GetById<TProperty>(string actionName, TProperty id);
        T GetByParams(string actionName, object args);
        
        void Update<TProperty>(TProperty id, T item);

        T Create(string actionName, T item);
        
        string SerializeEntity(T item);
        
    }
}