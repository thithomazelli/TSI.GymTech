using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSI.GymTech.Entity.Models;
using TSI.GymTech.Manager.Result;
using TSI.GymTech.Repository;

namespace TSI.GymTech.Manager.EntityManagers
{
    public sealed class AccessLogManager
    {
        private readonly Repository<AccessLog> repository;

        public AccessLogManager()
        {
            repository = new Repository<AccessLog>();
        }

        /// <summary>
        /// Creates an AccessLog object
        /// </summary>
        public ResultEnum Create(AccessLog accessLog)
        {
            ResultEnum result = ResultEnum.Success;
            try
            {
                repository.Add(accessLog);
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
        /// Get a AccessLog list 
        /// </summary>
        public Result<IEnumerable<AccessLog>> FindAll()
        {
            Result<IEnumerable<AccessLog>> result = new Result<IEnumerable<AccessLog>>();

            try
            {
                result.Data = repository.GetAll().AsEnumerable<AccessLog>();
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
        /// Gets an AccessLog object by ID
        /// </summary>
        public Result<AccessLog> FindById(int? id)
        {
            Result<AccessLog> result = new Result<AccessLog>();

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
        /// Gets an AccessLog list by Person ID
        /// </summary>
        public Result<IEnumerable<AccessLog>> FindByPersonId(string personId)
        {
            Result<IEnumerable<AccessLog>> result = new Result<IEnumerable<AccessLog>>();

            try
            {
                result.Data = repository.query(accessLog => accessLog.PersonId.Equals(personId)).AsEnumerable<AccessLog>();
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
        /// Updates an AccessLog object
        /// </summary>
        public ResultEnum Update(AccessLog accessLog)
        {
            ResultEnum result = ResultEnum.Success;
            try
            {
                repository.Update(accessLog);
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
        /// Removes a AccessLog object
        /// </summary>
        public ResultEnum Remove(AccessLog accessLog)
        {
            ResultEnum result = ResultEnum.Success;
            try
            {
                repository.Remove(accessLog);
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
