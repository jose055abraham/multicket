using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Validator.Cfg;
using NHibernate.Validator.Event;
using System.Reflection;
using Environment = NHibernate.Validator.Cfg.Environment;


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
			Configuration cfg = new Configuration();
			cfg.AddAssembly(Assembly.GetExecutingAssembly());

			cfg.DataBaseIntegration(dataBaseIntegration: (c) =>
			{
				c.ConnectionString = url;
				c.Driver<MySqlDataDriver>();
				c.Dialect<MySQLDialect>();
			});
			new SchemaExport(cfg).Execute(true, true, false);
			ConfigureNhibernateValidator(cfg);
			return cfg;

			#region Configuración de NHibernate con Fluent NHibernate 

			//return Fluently.Configure()
			//        .Database(MySQLConfiguration.Standard.ConnectionString(url).ShowSql())
			//        .ExposeConfiguration((config) =>
			//        {
			//            config.SetProperty("current_session_context_class", "web");
			//            new SchemaExport(config).Create(true, false);
			//            ConfigureNhibernateValidator(config);
			//        })
			//        .Mappings((e) =>
			//        {
			//            e.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly());
			//            e.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()).ExportTo(@"D:\\Sistema de Ventas\\Multicket-3.0.0\\Mappings");
			//        }).BuildConfiguration();

			#endregion
		}

		private static void ConfigureNhibernateValidator(Configuration config)
		{
			INHVConfiguration nhvc = new XmlConfiguration();

			Environment.SharedEngineProvider = new NHibernateSharedEngineProvider();
			nhvc.Properties[Environment.ApplyToDDL] = "true";
			nhvc.Properties[Environment.AutoregisterListeners] = "true";
			nhvc.Properties[Environment.ValidatorMode] = "UseAttribute";
			nhvc.Mappings.Add(new MappingConfiguration(Assembly.GetExecutingAssembly().ToString(), null));
			ValidatorInitializer.Initialize(config);

			#region Configuración de NHibernate Validador con Fluent NHibernate 

			//Environment.SharedEngineProvider = new NHibernateSharedEngineProvider();
			//FluentConfiguration nhvConfiguration = new FluentConfiguration();
			//ValidatorEngine validatorEngine = Environment.SharedEngineProvider.GetEngine();

			//nhvConfiguration.SetDefaultValidatorMode(ValidatorMode.OverrideAttributeWithExternal)
			//				.Register(Assembly.Load(Assembly.GetExecutingAssembly().GetName())
			//				.ValidationDefinitions()).IntegrateWithNHibernate
			//				.ApplyingDDLConstraints().RegisteringListeners();

			//validatorEngine.Configure(nhvConfiguration);
			//ValidatorInitializer.Initialize(config, validatorEngine);

			#endregion
		}
	}
}
