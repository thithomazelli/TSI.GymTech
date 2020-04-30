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
    public sealed class AccessControlManager
    {
        private readonly Repository<AccessControl> repository;

        public AccessControlManager()
        {
            repository = new Repository<AccessControl>();
        }

        /// <summary>
        /// Creates an AccessControl object
        /// </summary>
        public ResultEnum Create(AccessControl accessControl)
        {
            ResultEnum result = ResultEnum.Success;
            try
            {
                repository.Add(accessControl);
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
        /// Get a AccessControl list 
        /// </summary>
        public Result<IEnumerable<AccessControl>> FindAll()
        {
            Result<IEnumerable<AccessControl>> result = new Result<IEnumerable<AccessControl>>();

            try
            {
                result.Data = repository.GetAll().AsEnumerable<AccessControl>();
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
        /// Gets an AccessControl object by ID
        /// </summary>
        public Result<AccessControl> FindById(int? id)
        {
            Result<AccessControl> result = new Result<AccessControl>();

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
        /// Gets an AccessControl list by IP Address
        /// </summary>
        public Result<IEnumerable<AccessControl>> FindByIpAddress(string ipAddress)
        {
            Result<IEnumerable<AccessControl>> result = new Result<IEnumerable<AccessControl>>();

            try
            {
                result.Data = repository.query(accessControl => accessControl.IpAddress.Equals(ipAddress)).AsEnumerable<AccessControl>();
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
        /// Gets an AccessControl list by Name
        /// </summary>
        public Result<IEnumerable<AccessControl>> FindByName(string name)
        {
            Result<IEnumerable<AccessControl>> result = new Result<IEnumerable<AccessControl>>();

            try
            {
                result.Data = repository.query(accessControl => accessControl.Name.Equals(name)).AsEnumerable<AccessControl>();
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
        /// Updates an AccessControl object
        /// </summary>
        public ResultEnum Update(AccessControl accessControl)
        {
            ResultEnum result = ResultEnum.Success;
            try
            {
                repository.Update(accessControl);
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
        /// Removes a AccessControl object
        /// </summary>
        public ResultEnum Remove(AccessControl accessControl)
        {
            ResultEnum result = ResultEnum.Success;
            try
            {
                repository.Remove(accessControl);
                repository.Save();
            }
            catch (Exception ex)
            {
                result = ResultEnum.Error;
                //Pending: error to the log file
            }
            return result;
        }

        public bool IsIpAddressDuplicated(AccessControl accessControl)
        {
            return repository.query(_ => _.AccessControlId != accessControl.AccessControlId && _.IpAddress == accessControl.IpAddress).Any();
        }
    }
}