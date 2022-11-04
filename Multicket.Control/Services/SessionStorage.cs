using Multicket.Data.Models;
using System;

namespace Multicket.Module.Services
{
    public class SessionStorage : ISessionStorage
    {
        private const string MULTICKET = "multicket.dat";
        //private const string USER = "user.dat";
        //private const string DATE = "date.dat";
        private ISettings Settings => CrossSettings.Current;

        public string Uid => Settings.GetValueOrDefault(MULTICKET, "");

        //public string Usuario => Settings.GetValueOrDefault(USER, "");

        //public DateTime CreatedAt => Settings.GetValueOrDefault(DATE, DateTime.Now);


        public void Destroy()
        {
            Settings.Remove(MULTICKET);
            //Settings.Remove(USER);
            //Settings.Remove(DATE);
        }

        public void Set(Usuario usuario)
        {
            Settings.AddOrUpdateValue(MULTICKET, usuario.Id);
            Settings.AddOrUpdateValue(MULTICKET, usuario.Nombre);
            Settings.AddOrUpdateValue(MULTICKET, DateTime.Now);
        }

        public bool Get()
        {
            return Settings.Contains(MULTICKET);
        }
    }
}
