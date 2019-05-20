using System.ComponentModel.DataAnnotations;

namespace TSI.GymTech.Entity.Enumerates
{
    public enum TrainingSheetType
    {
        [Display(ResourceType = typeof(App_LocalResources.TrainingSheetType), Name = "Training")]
        Training = 0,
        [Display(ResourceType = typeof(App_LocalResources.TrainingSheetType), Name = "PreTraining")]
        PreTraining = 1,
        [Display(ResourceType = typeof(App_LocalResources.TrainingSheetType), Name = "AfterTraining")]
        AfterTraining = 2
    }
}
