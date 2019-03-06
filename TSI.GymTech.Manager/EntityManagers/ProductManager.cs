using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSI.GymTech.Entity.Enumerates;
using TSI.GymTech.Entity.Models;
using TSI.GymTech.Manager.Result;
using TSI.GymTech.Repository;

namespace TSI.GymTech.Manager.EntityManagers
{
    public sealed class ProductManager
    {
        private readonly Repository<Product> repository;

        public ProductManager()
        {
            repository = new Repository<Product>();
        }

        /// <summary>
        /// Creates an Product object
        /// </summary>
        public ResultEnum Create(Product product)
        {
            ResultEnum result = ResultEnum.Success;
            try
            {
                repository.Add(product);
                repository.Save();
            }
            catch (Exception ex)
            {
                result = ResultEnum.Error;
                //Pending: error to the log file
            }
            return result;
        }

        /// <summary>
        /// Get a Product list 
        /// </summary>
        public Result<IEnumerable<Product>> FindAll()
        {
            Result<IEnumerable<Product>> result = new Result<IEnumerable<Product>>();

            try
            {
                result.Data = repository.GetAll().AsEnumerable<Product>();
                result.Status = ResultEnum.Success;
            }
            catch (Exception ex)
            {
                result.Status = ResultEnum.Error;
                //Pending: error to the log file
            }
            return result;
        }

        /// <summary>
        /// Gets an Product object by ID
        /// </summary>
        public Result<Product> FindById(int? id)
        {
            Result<Product> result = new Result<Product>();

            try
            {
                result.Data = repository.GetById(id);
                result.Status = ResultEnum.Success;
            }
            catch (Exception ex)
            {
                result.Status = ResultEnum.Error;
                //Pending: error to the log file
            }
            return result;
        }

        /// <summary>
        /// Gets an Product list by Product Name
        /// </summary>
        public Result<IEnumerable<Product>> FindByProductName(string productName)
        {
            Result<IEnumerable<Product>> result = new Result<IEnumerable<Product>>();

            try
            {
                result.Data = repository.query(product => product.Name.Equals(productName)).AsEnumerable<Product>();
                result.Status = ResultEnum.Success;
            }
            catch (Exception)
            {
                result.Status = ResultEnum.Error;
                //Pending: error to the log file
            }
            return result;
        }

        /// <summary>
        /// Gets an Product list by Status
        /// </summary>
        public Result<IEnumerable<Product>> FindByStatus(ProductStatus? status)
        {
            Result<IEnumerable<Product>> result = new Result<IEnumerable<Product>>();

            try
            {
                result.Data = repository.query(product => product.Status == status).AsEnumerable<Product>();
                result.Status = ResultEnum.Success;
            }
            catch (Exception)
            {
                result.Status = ResultEnum.Error;
                //Pending: error to the log file
            }
            return result;
        }

        /// <summary>
        /// Gets an Product list by Type
        /// </summary>
        public Result<IEnumerable<Product>> FindByType(ProductType? type)
        {
            Result<IEnumerable<Product>> result = new Result<IEnumerable<Product>>();

            try
            {
                result.Data = repository.query(product => product.Type == type).AsEnumerable<Product>();
                result.Status = ResultEnum.Success;
            }
            catch (Exception)
            {
                result.Status = ResultEnum.Error;
                //Pending: error to the log file
            }
            return result;
        }

        /// <summary>
        /// Updates an Product object
        /// </summary>
        public ResultEnum Update(Product product)
        {
            ResultEnum result = ResultEnum.Success;
            try
            {
                repository.Update(product);
                repository.Save();
            }
            catch (Exception ex)
            {
                result = ResultEnum.Error;
                //Pending: error to the log file
            }
            return result;
        }

        /// <summary>
        /// Removes a Product object
        /// </summary>
        public ResultEnum Remove(Product product)
        {
            ResultEnum result = ResultEnum.Success;
            try
            {
                repository.Remove(product);
                repository.Save();
            }
            catch (Exception ex)
            {
                result = ResultEnum.Error;
                //Pending: error to the log file
            }
            return result;
        }
    }
}