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
    public sealed class ExerciseManager
    {
        private readonly Repository<Exercise> repository;

        public ExerciseManager()
        {
            repository = new Repository<Exercise>();
        }

        /// <summary>
        /// Creates an Exercise object
        /// </summary>
        public ResultEnum Create(Exercise exercise)
        {
            ResultEnum result = ResultEnum.Success;
            try
            {
                repository.Add(exercise);
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
        /// Get a Exercise list 
        /// </summary>
        public Result<IEnumerable<Exercise>> FindAll()
        {
            Result<IEnumerable<Exercise>> result = new Result<IEnumerable<Exercise>>();

            try
            {
                result.Data = repository.GetAll().AsEnumerable<Exercise>();
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
        /// Gets an Exercise object by ID
        /// </summary>
        public Result<Exercise> FindById(int? id)
        {
            Result<Exercise> result = new Result<Exercise>();

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
        /// Gets an Exercises list by muscle worked
        /// </summary>
        public Result<IEnumerable<Exercise>> FindByMuscleWorked(string muscleWorked)
        {
            Result<IEnumerable<Exercise>> result = new Result<IEnumerable<Exercise>>();

            try
            {
                result.Data = repository.query(exercise => exercise.MuscleWorked.Equals(muscleWorked)).AsEnumerable<Exercise>();
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
        /// Gets an Exercises list by muscular group
        /// </summary>
        public Result<IEnumerable<Exercise>> FindByMuscularGroup(string muscularGroup)
        {
            Result<IEnumerable<Exercise>> result = new Result<IEnumerable<Exercise>>();

            try
            {
                result.Data = repository.query(exercise => exercise.MuscularGroup.Equals(muscularGroup)).AsEnumerable<Exercise>();
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
        /// Updates an Exercise object
        /// </summary>
        public ResultEnum Update(Exercise exercise)
        {
            ResultEnum result = ResultEnum.Success;
            try
            {
                repository.Update(exercise);
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
        /// Removes a Exercise object
        /// </summary>
        public ResultEnum Remove(Exercise exercise)
        {
            ResultEnum result = ResultEnum.Success;
            try
            {
                repository.Remove(exercise);
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