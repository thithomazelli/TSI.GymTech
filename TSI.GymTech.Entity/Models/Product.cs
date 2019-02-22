using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TSI.GymTech.Entity.Enumerates;

namespace TSI.GymTech.Entity.Models
{
    public class Product : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessageResourceName = "ProductIdRequired", ErrorMessageResourceType = typeof(App_LocalResources.Product), AllowEmptyStrings = false)]
        [Display(Description = "ProductId", ResourceType = typeof(App_LocalResources.Product))]
        public int ProductId { get; set; }

        [Display(Description = "ProductName", ResourceType = typeof(App_LocalResources.Product))]
        [StringLength(128)]
        public string ProductName { get; set; }

        [Display(Description = "Description", ResourceType = typeof(App_LocalResources.Product))]
        [StringLength(128)]
        public string Description { get; set; }

        [Required(ErrorMessageResourceName = "ProductTypeRequired", ErrorMessageResourceType = typeof(App_LocalResources.Product), AllowEmptyStrings = false)]
        [Display(Description = "ProductType", ResourceType = typeof(App_LocalResources.Product))]
        public ProductType? ProductType { get; set; }

        [Required(ErrorMessageResourceName = "SuggestedPriceRequired", ErrorMessageResourceType = typeof(App_LocalResources.Product), AllowEmptyStrings = false)]
        [Display(Description = "SuggestedPrice", ResourceType = typeof(App_LocalResources.Product))]
        public decimal SuggestedPrice { get; set; }

        [Display(Description = "QuantityStock", ResourceType = typeof(App_LocalResources.Product))]
        public int? QuantityStock { get; set; }

        [Display(Description = "Duplication", ResourceType = typeof(App_LocalResources.Product))]
        public int? Duplication { get; set; }

        [Display(Description = "Photo", ResourceType = typeof(App_LocalResources.Product))]
        [StringLength(64)]
        public string Photo { get; set; }

        [Display(Description = "Comments", ResourceType = typeof(App_LocalResources.Product))]
        [DataType(DataType.MultilineText)]
        [StringLength(1024)]
        public string Comments { get; set; }
    }
}
