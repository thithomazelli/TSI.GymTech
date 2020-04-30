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
    public sealed class EvaluationSheetManager
    {
        private readonly Repository<EvaluationSheet> repository;
        private readonly Repository<EvaluationSheetView> repositoryView;

        public EvaluationSheetManager()
        {
            repository = new Repository<EvaluationSheet>();
            repositoryView = new Repository<EvaluationSheetView>();
        }

        /// <summary>
        /// Creates an EvaluationSheet object
        /// </summary>
        public ResultEnum Create(EvaluationSheet evaluationSheet)
        {
            ResultEnum result = ResultEnum.Success;
            try
            {
                repository.Add(evaluationSheet);
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
        /// Get a EvaluationSheet list 
        /// </summary>
        public Result<IEnumerable<EvaluationSheet>> FindAll()
        {
            Result<IEnumerable<EvaluationSheet>> result = new Result<IEnumerable<EvaluationSheet>>();

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
        /// Get a EvaluationSheet list 
        /// </summary>
        public Result<IEnumerable<EvaluationSheetView>> FindAllByView()
        {
            Result<IEnumerable<EvaluationSheetView>> result = new Result<IEnumerable<EvaluationSheetView>>();

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
        /// Gets an EvaluationSheet object by ID
        /// </summary>
        public Result<EvaluationSheet> FindById(int? id)
        {
            Result<EvaluationSheet> result = new Result<EvaluationSheet>();

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
        /// Gets an EvaluationSheet list by Student ID
        /// </summary>
        public Result<IEnumerable<EvaluationSheet>> FindByStudentId(int? studentId)
        {
            Result<IEnumerable<EvaluationSheet>> result = new Result<IEnumerable<EvaluationSheet>>();

            try
            {
                result.Data = repository.query(evaluationSheet => evaluationSheet.StudentId.Equals(studentId)).AsEnumerable<EvaluationSheet>();
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
        /// Updates an EvaluationSheet object
        /// </summary>
        public ResultEnum Update(EvaluationSheet evaluationSheet)
        {
            ResultEnum result = ResultEnum.Success;
            try
            {
                repository.Update(evaluationSheet);
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
        /// Removes a EvaluationSheet object
        /// </summary>
        public ResultEnum Remove(EvaluationSheet evaluationSheet)
        {
            ResultEnum result = ResultEnum.Success;
            try
            {
                repository.Remove(evaluationSheet);
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