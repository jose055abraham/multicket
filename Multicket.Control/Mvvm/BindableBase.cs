﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Multicket.Control.Mvvm
{
	public abstract class BindableBase : INotifyPropertyChanged
	{
		private Dictionary<string, object> _properties = new Dictionary<string, object>();

		/// <summary>
		/// Gets the value of a property
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="name"></param>
		/// <returns></returns>
		protected T Get<T>([CallerMemberName] string name = null)
		{
			Debug.Assert(name != null, "name != null");

			if (_properties.TryGetValue(name, out object value))
				return value == null ? default(T) : (T)value;
			return default(T);
		}

		/// <summary>
		/// Sets the value of a property
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value"></param>
		/// <param name="name"></param>
		/// <remarks>Use this overload when implicitly naming the property</remarks>
		protected void Set<T>(T value, [CallerMemberName] string name = null)
		{
			Debug.Assert(name != null, "name != null");

			if (Equals(value, Get<T>(name)))
				return;
			_properties[name] = value;
			OnPropertyChanged(name);
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
