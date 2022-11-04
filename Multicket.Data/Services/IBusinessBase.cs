
using Multicket.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Multicket.Data.Services
{
    public interface IBusinessBase
    {
        void Delete<TEntity>(TEntity entity) where TEntity : class, new();
        void Save<TEntity>(TEntity entity) where TEntity : class, new();
        void Update<TEntity>(TEntity entity) where TEntity : class, new();
        HashSet<TEntity> Find<TEntity>() where TEntity : class, new();
        IQueryable<TEntity> Query<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class, new();
        TEntity Get<TEntity, TKey>(TKey key) where TEntity : class, new();
        Usuario Verify(string user, string pass);
    }
}
