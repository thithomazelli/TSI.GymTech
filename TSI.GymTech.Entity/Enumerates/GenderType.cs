using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TSI.GymTech.Entity.Enumerates
{
    public enum GenderType
    {
        [Display(ResourceType = typeof(App_LocalResources.GenderType), Name = "Female")]
        Female = 0,
        [Display(ResourceType = typeof(App_LocalResources.GenderType), Name = "Male")]
        Male = 1
    }
}
