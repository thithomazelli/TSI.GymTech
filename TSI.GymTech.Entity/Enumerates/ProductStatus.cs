using System.ComponentModel.DataAnnotations;

namespace TSI.GymTech.Entity.Enumerates
{
    public enum ProductStatus
    {
        [Display(ResourceType = typeof(App_LocalResources.ProductStatus), Name = "Inactive")]
        Inactive = 0,
        [Display(ResourceType = typeof(App_LocalResources.ProductStatus), Name = "Active")]
        Active = 1,
    }
}
