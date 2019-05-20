using System.ComponentModel.DataAnnotations;

namespace TSI.GymTech.Entity.Enumerates
{
    public enum PaymentType
    {
        [Display(ResourceType = typeof(App_LocalResources.PaymentType), Name = "CreditCard")]
        CreditCard = 0,
        [Display(ResourceType = typeof(App_LocalResources.PaymentType), Name = "DebitCard")]
        DebitCard = 1,
        [Display(ResourceType = typeof(App_LocalResources.PaymentType), Name = "Cash")]
        Cash = 2
    }
}
