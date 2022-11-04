using Multicket.Data.Models;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Multicket.Data.Services
{
    public class BusinessBase : IBusinessBase
    {

        public BusinessBase()
        {
        }

        public TEntity Get<TEntity, TKey>(TKey key) where TEntity : class, new()
        {
            using (ISession session = SessionManager.SetSessionFactory().OpenSession())
            {
                try
                {
                    return session.Get<TEntity>(key);
                }
                catch (Exception)
                {
                    session.Close();
                    throw;
                }
                finally
                {
                    session.Close();
                }
            }
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class, new()
        {
            using (ISession session = SessionManager.SetSessionFactory().OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                try
                {
                    session.Delete(entity);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
                finally
                {
                    session.Close();
                }
            }
        }

        public void Save<TEntity>(TEntity entity) where TEntity : class, new()
        {
            using (ISession session = SessionManager.SetSessionFactory().OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                try
                {
                    session.SaveOrUpdate(entity);
                    session.Flush();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
                finally
                {
                    session.Close();
                }

            }

        }

        public void Update<TEntity>(TEntity entity) where TEntity : class, new()
        {
            using (ISession session = SessionManager.SetSessionFactory().OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                try
                {
                    session.Update(entity);
                    session.Flush();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
                finally
                {
                    session.Close();
                }
            }

        }

        public HashSet<TEntity> Find<TEntity>() where TEntity : class, new()
        {
            using (ISession session = SessionManager.SetSessionFactory().OpenSession())
            {
                try
                {
                    return session.Query<TEntity>().ToHashSet();
                }
                catch (Exception)
                {
                    session.Close();
                    throw;
                }
                finally
                {
                    session.Close();
                }
            }
        }

        public Usuario Verify(string user, string pass)
        {
            using (ISession session = SessionManager.SetSessionFactory().OpenSession())
            {
                try
                {
                    return session.CreateCriteria<Usuario>()
                                  .Add(expression: Restrictions.Eq("Nombre", user))
                                  .Add(expression: Restrictions.Eq("Password", pass))
                                  .UniqueResult<Usuario>();
                }
                catch (Exception)
                {
                    session.Close();
                    throw;
                }
                finally
                {
                    session.Close();
                }
            }
        }

        public IQueryable<TEntity> Query<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class, new()
        {
            using (ISession session = SessionManager.SetSessionFactory().OpenSession())
            {
                try
                {
                    return session.Query<TEntity>().Where(expression);
                }
                catch (Exception)
                {
                    session.Close();
                    throw;
                }
                finally
                {
                    session.Close();
                }
            }
        }
    }
}
