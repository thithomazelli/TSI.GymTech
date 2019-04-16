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
    public sealed class AddressManager
    {
        private readonly Repository<Address> repository;

        public AddressManager()
        {
            repository = new Repository<Address>();
        }

        /// <summary>
        /// Creates an Address object
        /// </summary>
        public ResultEnum Create(Address address)
        {
            ResultEnum result = ResultEnum.Success;
            try
            {
                repository.Add(address);
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
        /// Gets an Address object by ID
        /// </summary>
        public Result<Address> FindById(int? id)
        {
            Result<Address> result = new Result<Address>();

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
        /// Gets an Address list by PersonId
        /// </summary>
        public Result<IEnumerable<Address>> FindByPersonId(int? personId)
        {
            Result<IEnumerable<Address>> result = new Result<IEnumerable<Address>>();

            try
            {
                result.Data = repository.query(address => address.PersonId == personId).AsEnumerable<Address>();
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
        /// Updates a Address object
        /// </summary>
        public ResultEnum Update(Address address)
        {
            ResultEnum result = ResultEnum.Success;
            try
            {
                repository.Update(address);
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
        /// Removes a Address object
        /// </summary>
        public ResultEnum Remove(Address address)
        {
            ResultEnum result = ResultEnum.Success;
            try
            {
                repository.Remove(address);
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
