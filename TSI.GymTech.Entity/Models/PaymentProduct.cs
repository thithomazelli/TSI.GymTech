using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TSI.GymTech.Entity.Models
{
    public class PaymentProduct : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessageResourceName = "PaymentProductIdRequired", ErrorMessageResourceType = typeof(App_LocalResources.PaymentProduct), AllowEmptyStrings = false)]
        [Display(Description = "PaymentProductId", ResourceType = typeof(App_LocalResources.PaymentProduct))]
        public int PaymentProductId { get; set; }

        [ForeignKey("Payment")]
        [Required(ErrorMessageResourceName = "PaymentRequired", ErrorMessageResourceType = typeof(App_LocalResources.PaymentProduct), AllowEmptyStrings = false)]
        public int PaymentId { get; set; }
        public virtual Payment Payment { get; set; }

        [ForeignKey("Product")]
        [Required(ErrorMessageResourceName = "ProductRequired", ErrorMessageResourceType = typeof(App_LocalResources.PaymentProduct), AllowEmptyStrings = false)]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        [Display(Description = "Quantity", ResourceType = typeof(App_LocalResources.PaymentProduct))]
        public int? Quantity { get; set; }

        [Display(Description = "Discount", ResourceType = typeof(App_LocalResources.PaymentProduct))]
        public decimal? Discount { get; set; }

        [Display(Description = "UnitPrice", ResourceType = typeof(App_LocalResources.PaymentProduct))]
        public decimal? UnitPrice { get; set; }

        [Display(Description = "TotalPrice", ResourceType = typeof(App_LocalResources.PaymentProduct))]
        public decimal? TotalPrice { get; set; }
    }
}
