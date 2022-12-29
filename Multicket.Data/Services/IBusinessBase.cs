
using Multicket.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Multicket.Data.Services
{
	public interface IBusinessBase
	{
		void Delete<TEntity>(TEntity entity) where TEntity : class, new();
		void Update<TEntity>(TEntity entity) where TEntity : class, new();
		HashSet<TEntity> Find<TEntity>() where TEntity : class, new();
		TEntity Select<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class, new();
		ISet<TEntity> Query<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class, new();
		TEntity Get<TEntity, TKey>(TKey key) where TEntity : class, new();
		void Usuario(Usuario usuario);
		Usuario Verify(string user, string pass);
	}
}
