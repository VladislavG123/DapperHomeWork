using DL.Models;
using System;
using System.Collections.Generic;

namespace DL.DataAccess.Abstract
{
    // Позволяет уничтожить объект, чтобы юзать его в using
    public interface IRepository<T> : IDisposable where T : Entity
    {
        void Add(T item);

        void Update(T item);

        void Delete(Guid id);

        ICollection<T> GetAll();
    }
}
