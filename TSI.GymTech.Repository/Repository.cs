using System;
using System.Data.Entity;
using System.Linq;
using TSI.GymTech.Data;
using TSI.GymTech.Repository.Interfaces;

namespace TSI.GymTech.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        //Fields
        private readonly Context context;

        //Constructor
        public Repository()
        {
            context = new Context();
        }

        public T GetById(int? id)
        {
            return context.Set<T>().Find(id);
        }

        public T GetByName(string name)
        {
            return context.Set<T>().Find(name);
        }

        public IQueryable<T> GetAll()
        {
            return context.Set<T>();
        }

        public IQueryable<T> query(System.Linq.Expressions.Expression<Func<T, bool>> filter)
        {
            return context.Set<T>().Where(filter);
        }

        public void Add(T entity)
        {
            context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            //Remember to reference System.Data.Entity
            context.Entry<T>(entity).State = EntityState.Modified;
        }

        public void Remove(T entity)
        {
            context.Set<T>().Remove(entity);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}