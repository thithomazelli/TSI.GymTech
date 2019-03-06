using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TSI.GymTech.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// This function will be receive the "ID" as parameter and should be returns the object found.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns the object found</returns>
        T GetById(int? id);

        /// <summary>
        /// This function will be receive the "Name" as parameter and should be returns the object found.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Returns the object found</returns>
        T GetByName(string name);

        /// <summary>
        /// This function will be not receive parameter and should be returns all registers found in this entity.
        /// </summary>
        /// <returns>Returns all registers found in this entity.</returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// This function will be receive na expression as parameter and should be returns results from the execute.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>Returns results from the execute.</returns>
        IQueryable<T> query(Expression<Func<T, bool>> filter);

        /// <summary>
        /// This function will be receive an object as parameter and should be add it to the database.
        /// </summary>
        /// <param name="entity"></param>
        void Add(T entity);

        /// <summary>
        /// This function will be receive an object as parameter and should be update it to the database.
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);

        /// <summary>
        /// This function will be receive an object as parameter and should be remove it to the database.
        /// </summary>
        /// <param name="entity"></param>
        void Remove(T entity);

        /// <summary>
        /// This function should be commit the changes to the database.
        /// </summary>
        /// <param name="entity"></param>
        void Save();
    }
}