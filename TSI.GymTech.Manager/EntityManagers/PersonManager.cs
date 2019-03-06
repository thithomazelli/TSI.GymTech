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
    public sealed class PersonManager
    {
        private readonly Repository<Person> repository;

        public PersonManager()
        {
            repository = new Repository<Person>();
        }

        /// <summary>
        /// Creates a Person object
        /// </summary>
        public ResultEnum Create(Person person)
        {
            ResultEnum result = ResultEnum.Success;
            try
            {
                repository.Add(person);
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
        /// Get a Person list 
        /// </summary>
        public Result<IEnumerable<Person>> FindAll()
        {
            Result<IEnumerable<Person>> result = new Result<IEnumerable<Person>>();

            try
            {
                result.Data = repository.GetAll().AsEnumerable<Person>();
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
        /// Get a Person object by ID
        /// </summary>
        public Result<Person> FindById(int? id)
        {
            Result<Person> result = new Result<Person>();

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
        /// Gets a Person list by SocialSecurityCard
        /// </summary>
        public Result<IEnumerable<Person>> FindBySocialSecurityCard(string socialSecurityCard)
        {
            Result<IEnumerable<Person>> result = new Result<IEnumerable<Person>>();

            try
            {
                result.Data = repository.query(person => person.SocialSecurityCard.Equals(socialSecurityCard)).AsEnumerable<Person>();
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
        /// Gets a Person list by first name
        /// </summary>
        public Result<IEnumerable<Person>> FindByFirstName(string firstName)
        {
            Result<IEnumerable<Person>> result = new Result<IEnumerable<Person>>();

            try
            {
                result.Data = repository.query(person => person.SocialSecurityCard.Contains(firstName)).AsEnumerable<Person>();
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
        /// Gets a Person list by last name
        /// </summary>
        public Result<IEnumerable<Person>> FindByLastName(string lastName)
        {
            Result<IEnumerable<Person>> result = new Result<IEnumerable<Person>>();

            try
            {
                result.Data = repository.query(person => person.SocialSecurityCard.Contains(lastName)).AsEnumerable<Person>();
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
        /// Gets a Person list by email
        /// </summary>
        public Result<IEnumerable<Person>> FindByEmail(string email)
        {
            Result<IEnumerable<Person>> result = new Result<IEnumerable<Person>>();

            try
            {
                result.Data = repository.query(person => person.Email.Contains(email)).AsEnumerable<Person>();
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
        /// Gets a Person list by ProfileType
        /// </summary>
        public Result<IEnumerable<Person>> FindByProfileType(PersonType profileType, bool searchingEqual)
        {
            Result<IEnumerable<Person>> result = new Result<IEnumerable<Person>>();

            try
            {
                result.Data = searchingEqual == true ? repository.query(person => person.ProfileType == profileType).AsEnumerable<Person>() :
                                                       repository.query(person => person.ProfileType != profileType).AsEnumerable<Person>(); ;
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
        /// Updates a Person object
        /// </summary>
        public ResultEnum Update(Person person)
        {
            ResultEnum result = ResultEnum.Success;
            try
            {
                repository.Update(person);
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
        /// Removes a Person object
        /// </summary>
        public ResultEnum Remove(Person person)
        {
            ResultEnum result = ResultEnum.Success;
            try
            {
                repository.Remove(person);
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
