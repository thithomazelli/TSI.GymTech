using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TSI.GymTech.Entity.Enumerates
{
    public enum PersonType
    {
        [Display(ResourceType = typeof(App_LocalResources.PersonType), Name = "Visitor")]
        Visitor = 0,
        [Display(ResourceType = typeof(App_LocalResources.PersonType), Name = "Student")]
        Student = 1,
        [Display(ResourceType = typeof(App_LocalResources.PersonType), Name = "Employee")]
        Employee = 2,
        [Display(ResourceType = typeof(App_LocalResources.PersonType), Name = "Personal")]
        Personal = 3,
        [Display(ResourceType = typeof(App_LocalResources.PersonType), Name = "Owner")]
        Owner = 4,
        [Display(ResourceType = typeof(App_LocalResources.PersonType), Name = "Teacher")]
        Teacher = 5,
        [Display(ResourceType = typeof(App_LocalResources.PersonType), Name = "Instructor")]
        Instructor = 6,
        [Display(ResourceType = typeof(App_LocalResources.PersonType), Name = "Cleaner")]
        Cleaner = 7
    }
}
