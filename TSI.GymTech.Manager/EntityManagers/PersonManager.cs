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
        private readonly Repository<StudentFrequentView> repositoryStudentFrequent;
        private readonly Repository<StudentNotFrequentView> repositoryStudentNotFrequent;

        public PersonManager()
        {
            repository = new Repository<Person>();
            repositoryStudentFrequent = new Repository<StudentFrequentView>();
            repositoryStudentNotFrequent = new Repository<StudentNotFrequentView>();
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
        /// Gets a Person list 
        /// </summary>
        public Result<IEnumerable<Person>> FindAll()
        {
            Result<IEnumerable<Person>> result = new Result<IEnumerable<Person>>();

            try
            {
                result.Data = repository.GetAll().AsEnumerable();
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
        /// Get a StudentFrequentView list 
        /// </summary>
        public Result<IEnumerable<StudentFrequentView>> FindAllByStudentFrequentView()
        {
            Result<IEnumerable<StudentFrequentView>> result = new Result<IEnumerable<StudentFrequentView>>();

            try
            {
                result.Data = repositoryStudentFrequent.GetAll().AsEnumerable().ToList();
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
        /// Get a StudentFrequentView list 
        /// </summary>
        public Result<IEnumerable<StudentNotFrequentView>> FindAllByStudentNotFrequentView()
        {
            Result<IEnumerable<StudentNotFrequentView>> result = new Result<IEnumerable<StudentNotFrequentView>>();

            try
            {
                result.Data = repositoryStudentNotFrequent.GetAll().AsEnumerable().ToList();
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
        /// Gets a Person object by ID
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
        public Result<IEnumerable<Person>> FindByProfileType(PersonType profileType, bool onlyActive = true, bool searchingEqual = true)
        {
            Result<IEnumerable<Person>> result = new Result<IEnumerable<Person>>();

            try
            {
                if (onlyActive && searchingEqual)
                {
                    result.Data = repository.query(person => person.ProfileType == profileType && person.Status == PersonStatus.Active)
                                            .OrderBy(person => person.Name)
                                            .AsEnumerable().ToList();
                }
                else if (onlyActive && !searchingEqual)
                {
                    result.Data = repository.query(person => person.ProfileType != profileType && person.Status == PersonStatus.Active)
                                            .OrderBy(person => person.Name)
                                            .AsEnumerable().ToList();
                }
                else if (!onlyActive && searchingEqual)
                {
                    result.Data = repository.query(person => person.ProfileType == profileType)
                                            .OrderBy(person => person.Name)
                                            .AsEnumerable().ToList();
                }
                else if (!onlyActive && !searchingEqual)
                {
                    result.Data = repository.query(person => person.ProfileType != profileType)
                                            .OrderBy(person => person.Name)
                                            .AsEnumerable().ToList();
                }

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

        public bool IsNameDuplicated(Person person)
        {
            return repository.query(_ => _.PersonId != person.PersonId && _.Name == person.Name).Any();
        }

        public bool IsEmailDuplicated(Person person)
        {
            return repository.query(_ => _.PersonId != person.PersonId && !string.IsNullOrEmpty(_.Email) 
            && _.Email == person.Email).Any();
        }
        
        public bool IsSocialSecurityCardDuplicated(Person person)
        {
            return repository.query(_ => _.PersonId != person.PersonId && !string.IsNullOrEmpty(_.SocialSecurityCard)
            && _.SocialSecurityCard == person.SocialSecurityCard).Any();
        }

        public bool IsNationalIDCardDuplicated(Person person)
        {
            return repository.query(_ => _.PersonId != person.PersonId && !string.IsNullOrEmpty(_.NationalIDCard) 
            && _.NationalIDCard == person.NationalIDCard).Any();
        }
    }
}
