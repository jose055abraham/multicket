using Multicket.Data.Config;
using NHibernate;
using NHibernate.Context;
using Serilog;
using System;

namespace Multicket.Data
{
	public class SessionManager
	{
		private static ISessionFactory sessionFactory;

		public static ISessionFactory SetSessionFactory()
		{

			if (sessionFactory == null)
			{
				sessionFactory = ConfigManager.SetConfiguration().BuildSessionFactory();
			}
			return sessionFactory;

		}

		public static ISession OpenSession()
		{
			ISession session;

			if (CurrentSessionContext.HasBind(SetSessionFactory()))
			{
				session = SetSessionFactory().GetCurrentSession();
			}
			else
			{
				session = SetSessionFactory().OpenSession();
				CurrentSessionContext.Bind(session);
			}
			return session;
		}

		public static void EndContextSession()
		{
			var factory = SetSessionFactory();
			var session = CurrentSessionContext.Unbind(factory);
			if (session != null && session.IsOpen)
			{
				try
				{
					if (session.Transaction != null && session.Transaction.IsActive)
					{
						session.Transaction.Rollback();
						throw new Exception("Rolling back uncommited NHibernate transaction.");
					}
					session.Flush();
				}
				catch (Exception ex)
				{
					Log.Error("SessionKey.EndContextSession", ex);
					throw;
				}
				finally
				{
					session.Close();
					session.Dispose();
				}
			}
		}
	}
}
