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
    public sealed class EvaluationSheetAnswerManager
    {
        private readonly Repository<EvaluationSheetAnswer> repository;
        
        public EvaluationSheetAnswerManager()
        {
            repository = new Repository<EvaluationSheetAnswer>();
        }

        /// <summary>
        /// Creates an SheetAnswer object
        /// </summary>
        public ResultEnum Create(EvaluationSheetAnswer evaluationSheetAnswer)
        {
            ResultEnum result = ResultEnum.Success;
            try
            {
                repository.Add(evaluationSheetAnswer);
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
        public Result<EvaluationSheetAnswer> FindById(int? id)
        {
            Result<EvaluationSheetAnswer> result = new Result<EvaluationSheetAnswer>();

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
        public ResultEnum Update(EvaluationSheetAnswer evaluationSheetAnswer)
        {
            ResultEnum result = ResultEnum.Success;
            try
            {
                repository.Update(evaluationSheetAnswer);
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
