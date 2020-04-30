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
    public sealed class SheetQuestionManager
    {
        private readonly Repository<SheetQuestion> repository;

        public SheetQuestionManager()
        {
            repository = new Repository<SheetQuestion>();
        }

        /// <summary>
        /// Creates an SheetQuestion object
        /// </summary>
        public ResultEnum Create(SheetQuestion sheetQuestion)
        {
            ResultEnum result = ResultEnum.Success;
            try
            {
                repository.Add(sheetQuestion);
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
        /// Get a SheetQuestion list 
        /// </summary>
        public Result<IEnumerable<SheetQuestion>> FindAll()
        {
            Result<IEnumerable<SheetQuestion>> result = new Result<IEnumerable<SheetQuestion>>();

            try
            {
                result.Data = repository.GetAll().AsEnumerable<SheetQuestion>();
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
        /// Gets an SheetQuestion object by ID
        /// </summary>
        public Result<SheetQuestion> FindById(int? id)
        {
            Result<SheetQuestion> result = new Result<SheetQuestion>();

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
        /// Gets an SheetQuestion list by Question
        /// </summary>
        public Result<IEnumerable<SheetQuestion>> FindByQuestion(string question)
        {
            Result<IEnumerable<SheetQuestion>> result = new Result<IEnumerable<SheetQuestion>>();

            try
            {
                result.Data = repository.query(sheetQuestion => sheetQuestion.Question.Equals(question)).AsEnumerable<SheetQuestion>();
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
        /// Gets an SheetQuestion list by Sheet Question Type
        /// </summary>
        public Result<IEnumerable<SheetQuestion>> FindBySheetQuestionType(SheetQuestionType? sheetTypeQuestion)
        {
            Result<IEnumerable<SheetQuestion>> result = new Result<IEnumerable<SheetQuestion>>();

            try
            {
                result.Data = repository.query(sheetQuestion => sheetQuestion.QuestionType == sheetTypeQuestion).AsEnumerable<SheetQuestion>().ToList();
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
        /// Updates an SheetQuestion object
        /// </summary>
        public ResultEnum Update(SheetQuestion sheetQuestion)
        {
            ResultEnum result = ResultEnum.Success;
            try
            {
                repository.Update(sheetQuestion);
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
        /// Removes a SheetQuestion object
        /// </summary>
        public ResultEnum Remove(SheetQuestion sheetQuestion)
        {
            ResultEnum result = ResultEnum.Success;
            try
            {
                repository.Remove(sheetQuestion);
                repository.Save();
            }
            catch (Exception ex)
            {
                result = ResultEnum.Error;
                //Pending: error to the log file
            }
            return result;
        }
        
        public bool IsDuplicated(SheetQuestion sheetQuestion)
        {
            return repository.query(_ => _.SheetQuestionId != sheetQuestion.SheetQuestionId
                && _.Question == sheetQuestion.Question
                && _.QuestionType == sheetQuestion.QuestionType).Any();
        }
    }
}