using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using System.Web.Http;
using TSI.GymTech.Entity.Enumerates;
using TSI.GymTech.Entity.Models;
using TSI.GymTech.Manager.EntityManagers;

namespace TSI.GymTech.WebAPI.WebAPI
{
    public class PersonController : ApiController
    {
        private readonly PersonManager _personManager;
        private ValidationErrorManager _validationErrorManager;

        private IEnumerable<Person> _listOfStudents;

        public PersonController()
        {
            _personManager = new PersonManager();
            _listOfStudents = _personManager.FindByProfileType(PersonType.Student, false, true).Data;
        }

        private string GetResourceName(ResourceManager resourceManager, string enumName)
        {
            CultureInfo _cultureInfo = new CultureInfo("pt");
            return resourceManager.GetString(enumName, _cultureInfo);
        }

        public Person GetPersonById(int? id)
        {
            return _personManager.FindById(id).Data;
        }

        public IHttpActionResult GetTotalOfStudents()
        {
            return Json(new
            {
                Success = true,
                TotalStudents = _listOfStudents?.Count(),
                TotalActiveStudents = _listOfStudents?.Where(_ => _.Status == PersonStatus.Active)?.Count(),
                TotalActiveFrequentStudents = _personManager.FindAllByStudentFrequentView().Data.Count(),
                TotalActiveNotFrequentStudents = _personManager.FindAllByStudentNotFrequentView().Data.Count(),
                TotalInactiveStudents = _listOfStudents?.Where(_ => _.Status != PersonStatus.Active)?.Count()
            }); 
        }

        [HttpGet]
        public IHttpActionResult GetBirthdaysOfTheDay()
        {
            var _cultureInfo = new CultureInfo("pt");
            var currentDate = DateTime.Now;

            return Json(new
            {
                Success = true,
                BirthdaysOfTheDayCount = _listOfStudents?
                    .Where(_ => _.Status == PersonStatus.Active
                            && _.BirthDate?.Day == currentDate.Day
                            && _.BirthDate?.Month == currentDate.Month)?.Count(),
                BirthdaysOfTheDayList = _listOfStudents?
                    .Where(_ => _.Status == PersonStatus.Active && _.BirthDate != null
                                && DateTime.Parse(_.BirthDate?.Day + "." + _.BirthDate?.Month + "." + currentDate.Year, _cultureInfo).Date >= currentDate.Date
                                && DateTime.Parse(_.BirthDate?.Day + "." + _.BirthDate?.Month + "." + currentDate.Year, _cultureInfo).Date <= currentDate.Date.AddDays(4))
                    .Select(_ => new
                    {
                        Id = _.PersonId,
                        _.Name,
                        _.ProfileType,
                        BirthDay = _.BirthDate != null && _.BirthDate.Value.Day == currentDate.Day
                            ? "Hoje"
                            : _.BirthDate != null && _.BirthDate.Value.Day == currentDate.AddDays(1).Day
                            ? "Amanhã"
                            : String.Format("{0:dddd}", DateTime.Parse(_.BirthDate?.Day + "." + _.BirthDate?.Month + "." + currentDate.Year, _cultureInfo).Date),
                        OriginalBirthDate = _.BirthDate,
                        BirthDate = _.BirthDate != null
                            ? String.Format("{0:, d}" + " de " + "{0: MMMM}", _.BirthDate)
                            : string.Empty,
                        Photo = !string.IsNullOrEmpty(_.Photo)
                            ? _.Photo
                            : "default-user-profile.svg"
                    })
                    .OrderBy(_ => DateTime.Parse(_.OriginalBirthDate?.Day + "." + _.OriginalBirthDate?.Month + "." + currentDate.Year, _cultureInfo).Date)
            });
        }

        [HttpGet]
        public IHttpActionResult GetStudents(string filter)
        {
            var currentDate = DateTime.Now;
            
            switch(filter)
            {
                case "Birthday":
                    {
                        var studentList = _personManager.FindByProfileType(PersonType.Student, false, true).Data
                            .Where(_ => _.Status == PersonStatus.Active && _.BirthDate != null
                                        && DateTime.Parse(_.BirthDate?.Day + "." + _.BirthDate?.Month + "." + currentDate.Year).Date >= currentDate.Date
                                        && DateTime.Parse(_.BirthDate?.Day + "." + _.BirthDate?.Month + "." + currentDate.Year).Date <= currentDate.Date.AddDays(4))
                            .Select(_ => new
                            {
                                Id = _.PersonId,
                                Status = GetResourceName(new ResourceManager(typeof(Entity.App_LocalResources.PersonStatus)),
                                        Enum.GetName(typeof(PersonStatus), _.Status)),
                                _.Name,
                                _.SocialSecurityCard,
                                _.Email,
                                _.BirthDate
                            })
                            .OrderBy(_ => DateTime.Parse(_.BirthDate?.Day + "." + _.BirthDate?.Month + "." + currentDate.Year).Date);

                        return Json(new { data = studentList });
                    }
                case "Frequent":
                    {
                        var studentList = _personManager.FindAllByStudentFrequentView().Data
                            .Select(_ => new
                            {
                                Id = _.PersonId,
                                Status = GetResourceName(new ResourceManager(typeof(Entity.App_LocalResources.PersonStatus)),
                                        Enum.GetName(typeof(PersonStatus), _.Status)),
                                _.Name,
                                _.SocialSecurityCard,
                                _.Email
                            });

                        return Json(new { data = studentList });
                    }
                case "NotFrequent":
                    {
                        var studentList = _personManager.FindAllByStudentNotFrequentView().Data
                            .Select(_ => new
                            {
                                Id = _.PersonId,
                                Status = GetResourceName(new ResourceManager(typeof(Entity.App_LocalResources.PersonStatus)),
                                        Enum.GetName(typeof(PersonStatus), _.Status)),
                                _.Name,
                                _.SocialSecurityCard,
                                _.Email
                            });

                        return Json(new { data = studentList });
                    }
                case "Inactive":
                    {
                        var studentList = _personManager.FindByProfileType(PersonType.Student, false, true).Data
                            .Where(_ => _.Status != PersonStatus.Active)
                            .Select(_ => new
                            {
                                Id = _.PersonId,
                                Status = GetResourceName(new ResourceManager(typeof(Entity.App_LocalResources.PersonStatus)),
                                        Enum.GetName(typeof(PersonStatus), _.Status)),
                                _.Name,
                                _.SocialSecurityCard,
                                _.Email
                            }); ;

                        return Json(new { data = studentList });
                    }
                default:
                    {
                        var studentList = _personManager.FindByProfileType(PersonType.Student, false, true).Data
                            .Select(_ => new
                            {
                                Id = _.PersonId,
                                Status = GetResourceName(new ResourceManager(typeof(Entity.App_LocalResources.PersonStatus)),
                                        Enum.GetName(typeof(PersonStatus), _.Status)),
                                _.Name,
                                _.SocialSecurityCard,
                                _.Email
                            }); ;

                        return Json(new { data = studentList });
                    }
            }
        }
    }
}
