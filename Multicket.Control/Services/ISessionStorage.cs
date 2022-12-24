using Multicket.Data.Models;
using System;

namespace Multicket.Module.Services
{
    public interface ISessionStorage
    {

        bool Get();
        void Set(Usuario usuario);
        void Destroy();
        string Uid { get; }
        //string SelectedUsuarioItem { get; }
        //DateTime CreatedAt { get; }
    }
}