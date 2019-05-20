using System.ComponentModel.DataAnnotations;

namespace TSI.GymTech.Entity.Enumerates
{
    public enum PaymentStatus
    {
        [Display(ResourceType = typeof(App_LocalResources.PaymentStatus), Name = "Completed")]
        Completed = 0,
        [Display(ResourceType = typeof(App_LocalResources.PaymentStatus), Name = "Pending")]
        Pending = 1
    }
}
