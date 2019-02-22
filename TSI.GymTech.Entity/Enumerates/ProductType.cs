using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TSI.GymTech.Entity.Enumerates
{
    public enum ProductType
    {
        [Display(ResourceType = typeof(App_LocalResources.ProductType), Name = "Once")]
        Once = 0,
        [Display(ResourceType = typeof(App_LocalResources.ProductType), Name = "Monthly")]
        Monthly = 1,
        [Display(ResourceType = typeof(App_LocalResources.ProductType), Name = "Quarterly")]
        Quarterly = 2,
        [Display(ResourceType = typeof(App_LocalResources.ProductType), Name = "Semester")]
        Semester = 3,
        [Display(ResourceType = typeof(App_LocalResources.ProductType), Name = "Yearly")]
        Yearly = 4,
        [Display(ResourceType = typeof(App_LocalResources.ProductType), Name = "Registration")]
        Registration = 5,
        [Display(ResourceType = typeof(App_LocalResources.ProductType), Name = "Weekly")]
        Weekly = 6,
        [Display(ResourceType = typeof(App_LocalResources.ProductType), Name = "Supplementation")]
        Supplementation = 7,
        [Display(ResourceType = typeof(App_LocalResources.ProductType), Name = "Drinks")]
        Drinks = 8,
        [Display(ResourceType = typeof(App_LocalResources.ProductType), Name = "Clothing")]
        Clothing = 9,
        [Display(ResourceType = typeof(App_LocalResources.ProductType), Name = "DanceOnceWeek")]
        DanceOnceWeek = 10,
        [Display(ResourceType = typeof(App_LocalResources.ProductType), Name = "ThreeTimesWeek")]
        ThreeTimesWeek = 11
    }
}
