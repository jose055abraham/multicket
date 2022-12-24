using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Validator.Cfg;
using NHibernate.Validator.Cfg.Loquacious;
using NHibernate.Validator.Engine;
using NHibernate.Validator.Event;
using System.Reflection;
using Environment = NHibernate.Validator.Cfg.Environment;
using FluentConfiguration = NHibernate.Validator.Cfg.Loquacious.FluentConfiguration;


namespace Multicket.Data.Config
{
    public sealed class ConfigManager
    {
        private const string host = "localhost";
        private const int port = 3306;
        private const string data = "nhibernate";
        private const string uid = "root";
        private const string pwd = "";
        private const string sslmode = "none";
        private static string url = $"host={host};port={port};database={data};uid={uid};pwd={pwd};sslmode={sslmode};";

        public static Configuration SetConfiguration()
        {
            return Fluently.Configure()
                    .Database(MySQLConfiguration.Standard.ConnectionString(url).ShowSql())
                    .ExposeConfiguration((config) =>
                    {
                        new SchemaExport(config).Create(true, false);
                        ConfigureNhibernateValidator(config);
                    })
                    .Mappings((e) => e.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly())
                    ).BuildConfiguration();
            //var config = new Configuration();
            //mapper = new ModelMapper();
            //config = new Configuration();
            //mapper.AddMappings(Assembly.GetExecutingAssembly().GetExportedTypes());

            //config.DataBaseIntegration(dataBaseIntegration: (c) =>
            //{
            //    c.ConnectionString = url;
            //    c.Driver<MySqlDataDriver>();
            //    c.Dialect<MySQLDialect>();

            //});
            //config.AddAssembly(Assembly.GetExecutingAssembly());
            //config.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());
            //return config;
        }

        private static void ConfigureNhibernateValidator(Configuration config)
        {
            Environment.SharedEngineProvider = new NHibernateSharedEngineProvider();
            FluentConfiguration nhvConfiguration = new FluentConfiguration();
            ValidatorEngine validatorEngine = Environment.SharedEngineProvider.GetEngine();

            nhvConfiguration.SetDefaultValidatorMode(ValidatorMode.OverrideAttributeWithExternal)
                            .Register(Assembly.Load(Assembly.GetExecutingAssembly().GetName())
                            .ValidationDefinitions()).IntegrateWithNHibernate
                            .ApplyingDDLConstraints().RegisteringListeners();

            validatorEngine.Configure(nhvConfiguration);
            ValidatorInitializer.Initialize(config, validatorEngine);
        }
    }
}
