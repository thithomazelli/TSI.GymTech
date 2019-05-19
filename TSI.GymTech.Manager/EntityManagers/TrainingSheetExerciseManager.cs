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
    public class TrainingSheetExerciseManager
    {
        private readonly Repository<TrainingSheetExercise> repository;

        public TrainingSheetExerciseManager()
        {
            repository = new Repository<TrainingSheetExercise>();
        }

        /// <summary>
        /// Creates a TrainingSheetExercise object
        /// </summary>
        public ResultEnum Create(TrainingSheetExercise trainingSheetExercise)
        {
            ResultEnum result = ResultEnum.Success;
            try
            {
                repository.Add(trainingSheetExercise);
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
        /// Gets a TrainingSheetExercise object by ID
        /// </summary>
        public Result<TrainingSheetExercise> FindById(int? id)
        {
            Result<TrainingSheetExercise> result = new Result<TrainingSheetExercise>();

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
        /// Gets a TrainingSheetExercise list by Training Sheet
        /// </summary>
        public Result<IEnumerable<TrainingSheetExercise>> FindByTrainingSheetId(int? traininghSheetId)
        {
            Result<IEnumerable<TrainingSheetExercise>> result = new Result<IEnumerable<TrainingSheetExercise>>();

            try
            {
                result.Data = repository.query(_ => _.TrainingSheetId == traininghSheetId).AsEnumerable();
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
        /// Updates a TrainingSheetExercise object
        /// </summary>
        public ResultEnum Update(TrainingSheetExercise trainingSheetExercise)
        {
            ResultEnum result = ResultEnum.Success;
            try
            {
                repository.Update(trainingSheetExercise);
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
        /// Removes a TrainingSheetExercise object
        /// </summary>
        public ResultEnum Remove(TrainingSheetExercise trainingSheetExercise)
        {
            ResultEnum result = ResultEnum.Success;
            try
            {
                repository.Remove(trainingSheetExercise);
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
