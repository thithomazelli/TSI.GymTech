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
    public sealed class AnamnesisSheetAnswerManager
    {
        private readonly Repository<AnamnesisSheetAnswer> repository;

        public AnamnesisSheetAnswerManager()
        {
            repository = new Repository<AnamnesisSheetAnswer>();
        }

        /// <summary>
        /// Creates an SheetAnswer object
        /// </summary>
        public ResultEnum Create(AnamnesisSheetAnswer anamnesisSheetAnswer)
        {
            ResultEnum result = ResultEnum.Success;
            try
            {
                repository.Add(anamnesisSheetAnswer);
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
        /// Gets an SheetQuestion object by ID
        /// </summary>
        public Result<AnamnesisSheetAnswer> FindById(int? id)
        {
            Result<AnamnesisSheetAnswer> result = new Result<AnamnesisSheetAnswer>();

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
        /// Updates an SheetQuestion object
        /// </summary>
        public ResultEnum Update(AnamnesisSheetAnswer anamnesisSheetAnswer)
        {
            ResultEnum result = ResultEnum.Success;
            try
            {
                repository.Update(anamnesisSheetAnswer);
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
