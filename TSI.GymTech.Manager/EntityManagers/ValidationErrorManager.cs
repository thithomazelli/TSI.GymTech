using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TSI.GymTech.Entity.Models;

namespace TSI.GymTech.Manager.EntityManagers
{
    public class ValidationErrorManager
    {
        public IEnumerable<ValidationError> GetModelStateErrors(ModelStateDictionary ModelState)
        {
            return (from m in ModelState where m.Value.Errors.Count() > 0
                    select new ValidationError
                    {
                        PropertyName = m.Key,
                        ErrorMessage = (from msg in m.Value.Errors select msg.ErrorMessage).FirstOrDefault()
                    }).AsEnumerable();
        }

    }
}
