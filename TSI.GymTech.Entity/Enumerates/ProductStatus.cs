using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSI.GymTech.Entity.Enumerates
{
    public enum ProductStatus
    {
        [Display(ResourceType = typeof(App_LocalResources.ProductStatus), Name = "Inactive")]
        Inactive = 0,
        [Display(ResourceType = typeof(App_LocalResources.ProductStatus), Name = "Active")]
        Active = 1,
    }
}
