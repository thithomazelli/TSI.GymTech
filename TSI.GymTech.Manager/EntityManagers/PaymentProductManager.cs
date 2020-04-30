using System;
using System.Collections.Generic;
using System.Linq;
using TSI.GymTech.Entity.Models;
using TSI.GymTech.Manager.Result;
using TSI.GymTech.Repository;

namespace TSI.GymTech.Manager.EntityManagers
{
    public sealed class PaymentProductManager
    {
        private readonly Repository<PaymentProduct> repository;
        private readonly PaymentManager paymentManager;

        public PaymentProductManager()
        {
            repository = new Repository<PaymentProduct>();
            paymentManager = new PaymentManager();
        }

        /// <summary>
        /// Creates a PaymentProduct object
        /// </summary>
        public ResultEnum Create(PaymentProduct paymentProduct)
        {
            ResultEnum result = ResultEnum.Success;
            try
            {
                repository.Add(paymentProduct);
                repository.Save();

                paymentManager.UpdatePaymentTotalPrice(paymentProduct.PaymentId);
            }
            catch (Exception ex)
            {
                result = ResultEnum.Error;
                //Pending: error to the log file
            }
            return result;
        }

        /// <summary>
        /// Gets a PaymentProduct object by ID
        /// </summary>
        public Result<PaymentProduct> FindById(int? id)
        {
            Result<PaymentProduct> result = new Result<PaymentProduct>();

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
        /// Gets a PaymentProduct list by Payment
        /// </summary>
        public Result<IEnumerable<PaymentProduct>> FindByPaymentId(int? paymentId)
        {
            Result<IEnumerable<PaymentProduct>> result = new Result<IEnumerable<PaymentProduct>>();

            try
            {
                result.Data = repository.query(_ => _.PaymentId == paymentId).AsEnumerable();
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
        /// Updates a PaymentProduct object
        /// </summary>
        public ResultEnum Update(PaymentProduct paymentProduct)
        {
            ResultEnum result = ResultEnum.Success;
            try
            {
                repository.Update(paymentProduct);
                repository.Save();

                paymentManager.UpdatePaymentTotalPrice(paymentProduct.PaymentId);
            }
            catch (Exception ex)
            {
                result = ResultEnum.Error;
                //Pending: error to the log file
            }
            return result;
        }

        /// <summary>
        /// Removes a PaymentProduct object
        /// </summary>
        public ResultEnum Remove(PaymentProduct paymentProduct)
        {
            ResultEnum result = ResultEnum.Success;
            try
            {
                repository.Remove(paymentProduct);
                repository.Save();

                paymentManager.UpdatePaymentTotalPrice(paymentProduct.PaymentId);
            }
            catch (Exception ex)
            {
                result = ResultEnum.Error;
                //Pending: error to the log file
            }
            return result;
        }

        /// <summary>
        /// Calculate PaymentProduct price 
        /// </summary>
        public decimal CalcuatePaymentProductPrice(PaymentProduct paymentProduct)
        {
            var totalPrice = paymentProduct.UnitPrice * paymentProduct.Quantity;

            return paymentProduct.Discount > 0
                ? totalPrice - totalPrice * (paymentProduct.Discount / 100)
                : totalPrice;
        }
    }
}