using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TSI.GymTech.Entity.Enumerates
{
    public enum PersonType
    {
        [Display(ResourceType = typeof(App_LocalResources.PersonType), Name = "Administrator")]
        Administrator = 0,
        [Display(ResourceType = typeof(App_LocalResources.PersonType), Name = "Visitor")]
        Visitor = 1,
        [Display(ResourceType = typeof(App_LocalResources.PersonType), Name = "Student")]
        Student = 2,
        [Display(ResourceType = typeof(App_LocalResources.PersonType), Name = "Employee")]
        Employee = 3,
        [Display(ResourceType = typeof(App_LocalResources.PersonType), Name = "Personal")]
        Personal = 4,
        [Display(ResourceType = typeof(App_LocalResources.PersonType), Name = "Owner")]
        Owner = 5,
        [Display(ResourceType = typeof(App_LocalResources.PersonType), Name = "Teacher")]
        Teacher = 6,
        [Display(ResourceType = typeof(App_LocalResources.PersonType), Name = "Instructor")]
        Instructor = 7,
        [Display(ResourceType = typeof(App_LocalResources.PersonType), Name = "Cleaner")]
        Cleaner = 8
    }
}
