using System.ComponentModel.DataAnnotations;

namespace TSI.GymTech.Entity.Enumerates
{
    public enum GateStatusType
    {
        [Display(ResourceType = typeof(App_LocalResources.GateStatusType), Name = "Denied")]
        Denied = 0,
        [Display(ResourceType = typeof(App_LocalResources.GateStatusType), Name = "AllowedEntry")]
        AllowedEntry = 1,
        [Display(ResourceType = typeof(App_LocalResources.GateStatusType), Name = "AllowedExit")]
        AllowedExit = 2,
        [Display(ResourceType = typeof(App_LocalResources.GateStatusType), Name = "AllowedBothSides")]
        AllowedBothSides = 4
    }
}
