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
    public sealed class PaymentManager
    {
        private readonly Repository<Payment> repository;

        public PaymentManager()
        {
            repository = new Repository<Payment>();
        }

        /// <summary>
        /// Creates an Payment object
        /// </summary>
        public ResultEnum Create(Payment payment)
        {
            ResultEnum result = ResultEnum.Success;
            try
            {
                repository.Add(payment);
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
        /// Get a Payment list 
        /// </summary>
        public Result<IEnumerable<Payment>> FindAll()
        {
            Result<IEnumerable<Payment>> result = new Result<IEnumerable<Payment>>();

            try
            {
                result.Data = repository.GetAll().AsEnumerable<Payment>();
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
        /// Gets an Payment object by ID
        /// </summary>
        public Result<Payment> FindById(int? id)
        {
            Result<Payment> result = new Result<Payment>();

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
        /// Gets an Payment list by Person ID
        /// </summary>
        public Result<IEnumerable<Payment>> FindByPersonId(int? personId)
        {
            Result<IEnumerable<Payment>> result = new Result<IEnumerable<Payment>>();

            try
            {
                result.Data = repository.query(payment => payment.PersonId.Equals(personId)).AsEnumerable<Payment>();
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
        /// Gets an Payment list by Status
        /// </summary>
        public Result<IEnumerable<Payment>> FindByStatus(PaymentStatus? status)
        {
            Result<IEnumerable<Payment>> result = new Result<IEnumerable<Payment>>();

            try
            {
                result.Data = repository.query(payment => payment.Status == status).AsEnumerable<Payment>();
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
        /// Gets an Payment list by Type
        /// </summary>
        public Result<IEnumerable<Payment>> FindByType(PaymentType? type)
        {
            Result<IEnumerable<Payment>> result = new Result<IEnumerable<Payment>>();

            try
            {
                result.Data = repository.query(payment => payment.PaymentType == type).AsEnumerable<Payment>();
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
        /// Updates an Payment object
        /// </summary>
        public ResultEnum Update(Payment payment)
        {
            ResultEnum result = ResultEnum.Success;
            try
            {
                repository.Update(payment);
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
        /// Removes a Payment object
        /// </summary>
        public ResultEnum Remove(Payment payment)
        {
            ResultEnum result = ResultEnum.Success;
            try
            {
                repository.Remove(payment);
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