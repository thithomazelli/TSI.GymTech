using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TSI.GymTech.Entity.Models;
using TSI.GymTech.Manager.EntityManagers;

namespace TSI.GymTech.WebAPI.WebAPI
{
    public class PersonController : ApiController
    {
        PersonManager _personManager;
        
        public PersonController()
        {
            Person person = new Person();

            _personManager = new PersonManager();
        }

        public Person GetPersonById(int id)
        {
            return _personManager.FindById(id).Data;
        }
    }
}
