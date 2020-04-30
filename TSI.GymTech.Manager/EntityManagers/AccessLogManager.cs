using System;
using System.Collections.Generic;
using System.Globalization;
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
        private readonly Repository<AccessLogView> repositoryView;

        public AccessLogManager()
        {
            repository = new Repository<AccessLog>();
            repositoryView = new Repository<AccessLogView>();
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
                result.Data = repository.GetAll().AsEnumerable<AccessLog>().OrderBy(_ => _.CreateDate).ToList();
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
        /// Get a AccessLogView list 
        /// </summary>
        public Result<IEnumerable<AccessLogView>> FindAllByView()
        {
            Result<IEnumerable<AccessLogView>> result = new Result<IEnumerable<AccessLogView>>();

            try
            {
                result.Data = repositoryView.GetAll().AsEnumerable().ToList();
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
        /// Get a AnamnesisSheetView list 
        /// </summary>
        public Result<IEnumerable<AccessLogView>> FindByDateView(int lastDays)
        {
            DateTime currentDate = DateTime.Now.Date.AddDays(-lastDays);
            Result<IEnumerable<AccessLogView>> result = new Result<IEnumerable<AccessLogView>>();

            try
            {
                result.Data = repositoryView.GetAll().AsEnumerable().ToList();
                result.Data = repositoryView
                    .query(_ => _.CreateDate >= currentDate)
                    .OrderByDescending(_ => _.CreateDate)
                    .AsEnumerable<AccessLogView>();
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
        public Result<IEnumerable<AccessLog>> FindByPersonId(int? personId)
        {
            Result<IEnumerable<AccessLog>> result = new Result<IEnumerable<AccessLog>>();

            try
            {
                result.Data = repository.query(accessLog => accessLog.PersonId == personId).AsEnumerable<AccessLog>();
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
