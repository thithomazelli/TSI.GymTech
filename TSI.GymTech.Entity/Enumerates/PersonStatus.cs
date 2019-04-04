using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TSI.GymTech.Entity.Enumerates
{
    public enum PersonStatus
    {
        [Display(ResourceType = typeof(App_LocalResources.PersonStatus), Name = "Inactive")]
        Inactive = 0,
        [Display(ResourceType = typeof(App_LocalResources.PersonStatus), Name = "Active")]
        Active = 1,
        [Display(ResourceType = typeof(App_LocalResources.PersonStatus), Name = "Blocked")]
        Blocked = 2
    }
}
