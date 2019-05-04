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
    public class AccessControlController : ApiController
    {
        AccessControlManager _accessControleManager;

        public AccessControlController()
        {
            _accessControleManager = new AccessControlManager();
        }

        public IEnumerable<AccessControl> GetAllAccessControl ()
        {
            return _accessControleManager.FindAll().Data;
        }
    }
}
