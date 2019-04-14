using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TSI.GymTech.Entity.Enumerates;

namespace TSI.GymTech.Entity.Models
{
    [Table("product")]
    public class Product : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessageResourceName = "ProductIdRequired", ErrorMessageResourceType = typeof(App_LocalResources.Product), AllowEmptyStrings = false)]
        [Display(Name = "ProductId", ResourceType = typeof(App_LocalResources.Product))]
        public int ProductId { get; set; }

        [Required(ErrorMessageResourceName = "NameRequired", ErrorMessageResourceType = typeof(App_LocalResources.Product), AllowEmptyStrings = false)]
        [Display(Name = "Name", ResourceType = typeof(App_LocalResources.Product))]
        [StringLength(128)]
        public string Name { get; set; }

        [Display(Name = "Description", ResourceType = typeof(App_LocalResources.Product))]
        [StringLength(128)]
        public string Description { get; set; }

        [Required(ErrorMessageResourceName = "TypeRequired", ErrorMessageResourceType = typeof(App_LocalResources.Product), AllowEmptyStrings = false)]
        [Display(Name = "Type", ResourceType = typeof(App_LocalResources.Product))]
        public ProductType? Type { get; set; }

        [Display(Name = "Status", ResourceType = typeof(App_LocalResources.Product))]
        public ProductStatus? Status { get; set; }

        [Required(ErrorMessageResourceName = "SuggestedPriceRequired", ErrorMessageResourceType = typeof(App_LocalResources.Product), AllowEmptyStrings = false)]
        [Display(Name = "SuggestedPrice", ResourceType = typeof(App_LocalResources.Product))]
        public decimal SuggestedPrice { get; set; }

        [Display(Name = "QuantityStock", ResourceType = typeof(App_LocalResources.Product))]
        public int? QuantityStock { get; set; }

        [Display(Name = "Duplication", ResourceType = typeof(App_LocalResources.Product))]
        public int? Duplication { get; set; }

        [Display(Name = "Photo", ResourceType = typeof(App_LocalResources.Product))]
        [StringLength(64)]
        public string Photo { get; set; }

        [Display(Name = "Comments", ResourceType = typeof(App_LocalResources.Product))]
        [DataType(DataType.MultilineText)]
        [StringLength(1024)]
        public string Comments { get; set; }
    }
}
