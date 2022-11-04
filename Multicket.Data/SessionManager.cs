using Multicket.Data.Config;
using NHibernate;

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
            return SetSessionFactory().OpenSession();
        }
    }
}
