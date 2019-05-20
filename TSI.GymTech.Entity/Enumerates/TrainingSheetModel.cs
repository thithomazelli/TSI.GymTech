using System.ComponentModel.DataAnnotations;

namespace TSI.GymTech.Entity.Enumerates
{
    public enum TrainingSheetModel
    {
        [Display(ResourceType = typeof(App_LocalResources.TrainingSheetModel), Name = "Standard")]
        Standard = 0,
        [Display(ResourceType = typeof(App_LocalResources.TrainingSheetModel), Name = "Personal")]
        Personal = 1
    }
}
