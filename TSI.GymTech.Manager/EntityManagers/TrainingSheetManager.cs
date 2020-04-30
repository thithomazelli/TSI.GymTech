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
    public sealed class TrainingSheetManager
    {
        private readonly Repository<TrainingSheet> repository;
        private readonly Repository<TrainingSheetView> repositoryView;

        public TrainingSheetManager()
        {
            repository = new Repository<TrainingSheet>();
            repositoryView = new Repository<TrainingSheetView>();
        }

        /// <summary>
        /// Creates an TrainingSheet object
        /// </summary>
        public ResultEnum Create(TrainingSheet trainingSheet)
        {
            ResultEnum result = ResultEnum.Success;
            try
            {
                repository.Add(trainingSheet);
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
        /// Get a TrainingSheet list 
        /// </summary>
        public Result<IEnumerable<TrainingSheet>> FindAll()
        {
            Result<IEnumerable<TrainingSheet>> result = new Result<IEnumerable<TrainingSheet>>();

            try
            {
                result.Data = repository.GetAll().AsEnumerable().ToList();
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
        /// Get a TrainingSheetView list 
        /// </summary>
        public Result<IEnumerable<TrainingSheetView>> FindAllByView()
        {
            Result<IEnumerable<TrainingSheetView>> result = new Result<IEnumerable<TrainingSheetView>>();

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
        /// Gets an TrainingSheet object by ID
        /// </summary>
        public Result<TrainingSheet> FindById(int? id)
        {
            Result<TrainingSheet> result = new Result<TrainingSheet>();

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
        /// Gets an TrainingSheet list by Student ID
        /// </summary>
        //public Result<IEnumerable<TrainingSheet>> FindByStudentId(int? studentId)
        //{
        //    Result<IEnumerable<TrainingSheet>> result = new Result<IEnumerable<TrainingSheet>>();

        //    try
        //    {
        //        result.Data = repository.query(trainingSheet => trainingSheet.StudentId.Equals(studentId)).AsEnumerable<TrainingSheet>();
        //        result.Status = ResultEnum.Success;
        //    }
        //    catch (Exception)
        //    {
        //        result.Status = ResultEnum.Error;
        //        //Pending: error to the log file
        //    }
        //    return result;
        //}

        /// <summary>
        /// Gets an TrainingSheet list by Trainer ID
        /// </summary>
        //public Result<IEnumerable<TrainingSheet>> FindByTrainerId(int? trainerId)
        //{
        //    Result<IEnumerable<TrainingSheet>> result = new Result<IEnumerable<TrainingSheet>>();

        //    try
        //    {
        //        result.Data = repository.query(trainingSheet => trainingSheet.TrainerId.Equals(trainerId)).AsEnumerable<TrainingSheet>();
        //        result.Status = ResultEnum.Success;
        //    }
        //    catch (Exception)
        //    {
        //        result.Status = ResultEnum.Error;
        //        //Pending: error to the log file
        //    }
        //    return result;
        //}

        /// <summary>
        /// Gets an TrainingSheet list by Status
        /// </summary>
        public Result<IEnumerable<TrainingSheet>> FindByStatus(TrainingSheetStatus? status)
        {
            Result<IEnumerable<TrainingSheet>> result = new Result<IEnumerable<TrainingSheet>>();

            try
            {
                result.Data = repository.query(trainingSheet => trainingSheet.Status == status).AsEnumerable<TrainingSheet>();
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
        /// Gets an TrainingSheet list by Type
        /// </summary>
        public Result<IEnumerable<TrainingSheet>> FindByType(TrainingSheetType? type)
        {
            Result<IEnumerable<TrainingSheet>> result = new Result<IEnumerable<TrainingSheet>>();

            try
            {
                result.Data = repository.query(trainingSheet => trainingSheet.Type == type).AsEnumerable<TrainingSheet>();
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
        /// Updates an TrainingSheet object
        /// </summary>
        public ResultEnum Update(TrainingSheet trainingSheet)
        {
            ResultEnum result = ResultEnum.Success;
            try
            {
                repository.Update(trainingSheet);
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
        /// Removes a TrainingSheet object
        /// </summary>
        public ResultEnum Remove(TrainingSheet trainingSheet)
        {
            ResultEnum result = ResultEnum.Success;
            try
            {
                repository.Remove(trainingSheet);
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