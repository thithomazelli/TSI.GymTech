using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TSI.GymTech.Entity.Enumerates
{
    public enum MuscularGroup
    {
        [Display(ResourceType = typeof(App_LocalResources.MuscularGroup), Name = "Undefined")]
        Undefined = 0,
        [Display(ResourceType = typeof(App_LocalResources.MuscularGroup), Name = "Biceps")]
        Biceps = 1,
        [Display(ResourceType = typeof(App_LocalResources.MuscularGroup), Name = "Back")]
        Back = 2,
        [Display(ResourceType = typeof(App_LocalResources.MuscularGroup), Name = "Shoulders")]
        Shoulders = 3,
        [Display(ResourceType = typeof(App_LocalResources.MuscularGroup), Name = "Breastplate")]
        Breastplate = 4,
        [Display(ResourceType = typeof(App_LocalResources.MuscularGroup), Name = "Legs")]
        Legs = 5,
        [Display(ResourceType = typeof(App_LocalResources.MuscularGroup), Name = "Triceps")]
        Triceps = 6
    }
}
