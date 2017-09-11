using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Meninx.Productify.Data.Context;
using Meninx.Productify.Data.Models;

namespace Meninx.Productify.Data
{
    public class Repository<T> where T : BaseEntity
    {
        private readonly ProductifyContext context;
        private IDbSet<T> entities;
        string errorMessage = string.Empty;

        public Repository(ProductifyContext context)
        {
            this.context = context;
        }

        public T GetById(object id)
        {
            return this.Entities.Find(id);
        }

        public void Insert(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }
                this.Entities.Add(entity);
                this.context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorMessage += $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}"
                                        + Environment.NewLine;
                    }
                }
                throw new Exception(errorMessage, dbEx);
            }
        }

        public void Update(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }
                this.context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorMessage += Environment.NewLine
                                        + $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}";
                    }
                }

                throw new Exception(errorMessage, dbEx);
            }
        }

        public void Delete(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }

                this.Entities.Remove(entity);
                this.context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorMessage += Environment.NewLine
                                        + $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}";
                    }
                }
                throw new Exception(errorMessage, dbEx);
            }
        }

        public virtual IQueryable<T> Table => this.Entities;

        private IDbSet<T> Entities => entities ?? (entities = context.Set<T>());
    }
}