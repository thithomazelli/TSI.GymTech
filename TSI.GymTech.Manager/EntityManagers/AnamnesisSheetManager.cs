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
    public sealed class AnamnesisSheetManager
    {
        private readonly Repository<AnamnesisSheet> repository;

        public AnamnesisSheetManager()
        {
            repository = new Repository<AnamnesisSheet>();
        }

        /// <summary>
        /// Creates an AnamnesisSheet object
        /// </summary>
        public ResultEnum Create(AnamnesisSheet anamnesisSheet)
        {
            ResultEnum result = ResultEnum.Success;
            try
            {
                repository.Add(anamnesisSheet);
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
        /// Get a AnamnesisSheet list 
        /// </summary>
        public Result<IEnumerable<AnamnesisSheet>> FindAll()
        {
            Result<IEnumerable<AnamnesisSheet>> result = new Result<IEnumerable<AnamnesisSheet>>();

            try
            {
                result.Data = repository.GetAll().AsEnumerable<AnamnesisSheet>();
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
        /// Gets an AnamnesisSheet object by ID
        /// </summary>
        public Result<AnamnesisSheet> FindById(int? id)
        {
            Result<AnamnesisSheet> result = new Result<AnamnesisSheet>();

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
        /// Gets an AnamnesisSheet list by Student ID
        /// </summary>
        public Result<IEnumerable<AnamnesisSheet>> FindByStudentId(int? studentId)
        {
            Result<IEnumerable<AnamnesisSheet>> result = new Result<IEnumerable<AnamnesisSheet>>();

            try
            {
                result.Data = repository.query(anamnesisSheet => anamnesisSheet.StudentId.Equals(studentId)).AsEnumerable<AnamnesisSheet>();
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
        /// Updates an AnamnesisSheet object
        /// </summary>
        public ResultEnum Update(AnamnesisSheet anamnesisSheet)
        {
            ResultEnum result = ResultEnum.Success;
            try
            {
                repository.Update(anamnesisSheet);
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
        /// Removes a AnamnesisSheet object
        /// </summary>
        public ResultEnum Remove(AnamnesisSheet anamnesisSheet)
        {
            ResultEnum result = ResultEnum.Success;
            try
            {
                repository.Remove(anamnesisSheet);
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