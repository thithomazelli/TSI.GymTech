using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSI.GymTech.Entity.Models
{
    public class ValidationError
    {
        public string PropertyName { get; set; }

        public string ErrorMessage { get; set; }
    }
}
