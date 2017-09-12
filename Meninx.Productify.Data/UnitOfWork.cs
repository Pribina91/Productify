using System;
using System.Collections.Generic;
using Meninx.Productify.Data.Context;
using Meninx.Productify.Data.Models;

namespace Meninx.Productify.Data
{
    public class UnitOfWork : IDisposable
    {
        private readonly ProductifyContext context;
        private bool disposed;
        private Dictionary<string, object> repositories;


        public UnitOfWork(ProductifyContext context)
        {
            this.context = context;
        }

        public UnitOfWork()
        {
            context = new ProductifyContext();
        }

        public ProductifyContext GetContext()
        {
            return context;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }

        public Repository<T> Repository<T>() where T : BaseEntity
        {
            if (repositories == null)
            {
                repositories = new Dictionary<string, object>();
            }

            var type = typeof(T).Name;

            if (!repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), context);
                repositories.Add(type, repositoryInstance);
            }
            return (Repository<T>)repositories[type];
        }
    }
}