using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TSI.GymTech.Entity.Enumerates
{
    public enum TrainingSheetType
    {
        [Display(ResourceType = typeof(App_LocalResources.TrainingSheetType), Name = "PreTraining")]
        PreTraining = 0,
        [Display(ResourceType = typeof(App_LocalResources.TrainingSheetType), Name = "Training")]
        Training = 1,
        [Display(ResourceType = typeof(App_LocalResources.TrainingSheetType), Name = "AfterTraining")]
        AfterTraining = 2
    }
}
