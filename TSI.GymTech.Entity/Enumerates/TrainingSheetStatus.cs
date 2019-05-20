using System.ComponentModel.DataAnnotations;

namespace TSI.GymTech.Entity.Enumerates
{
    public enum TrainingSheetStatus
    {
        [Display(ResourceType = typeof(App_LocalResources.TrainingSheetStatus), Name = "Available")]
        Available = 0,
        [Display(ResourceType = typeof(App_LocalResources.TrainingSheetStatus), Name = "Expired")]
        Expired = 1
    }
}
