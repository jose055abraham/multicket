using NHibernate.Validator.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Multicket.Control.Mvvm
{
	public abstract class Bind : BindableBase, IDataErrorInfo
	{
		private IList<InvalidValue> rules;
		private readonly ValidatorEngine validator;


		public Bind()
		{
			validator = NHibernate.Validator.Cfg.Environment.SharedEngineProvider.GetEngine();

		}

		public string this[string columnName]
		{
			get
			{
				rules = GetInvalidRules(columnName);
				if (rules != null && rules.Count > 0)
				{
					Error = rules[0].Message;
					return string.Format("{0} {1}", rules[0].Value, rules[0].Message);
				}
				return null;
			}
		}

		private IList<InvalidValue> GetInvalidRules(string propertyName)
		{
			Type type = GetType();

			return validator.ValidatePropertyValue(type, propertyName, GetPropertyValue(type, propertyName));

		}

		private object GetPropertyValue(Type type, string propertyName)
		{
			return type.GetProperty(propertyName).GetValue(this, null);
		}

		public IList<InvalidValue> GetAllInvalidRules()
		{
			return validator.Validate(this);
		}

		public string Error { get; set; }
	}
}
