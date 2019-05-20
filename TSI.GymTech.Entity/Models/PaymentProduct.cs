using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TSI.GymTech.Entity.Models
{
    public class PaymentProduct : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessageResourceName = "PaymentProductIdRequired", ErrorMessageResourceType = typeof(App_LocalResources.PaymentProduct), AllowEmptyStrings = false)]
        [Display(Name = "PaymentProductId", ResourceType = typeof(App_LocalResources.PaymentProduct))]
        public int PaymentProductId { get; set; }

        [ForeignKey("Payment")]
        [Required(ErrorMessageResourceName = "PaymentRequired", ErrorMessageResourceType = typeof(App_LocalResources.PaymentProduct), AllowEmptyStrings = false)]
        public int PaymentId { get; set; }
        public virtual Payment Payment { get; set; }

        [ForeignKey("Product")]
        [Required(ErrorMessageResourceName = "ProductRequired", ErrorMessageResourceType = typeof(App_LocalResources.PaymentProduct), AllowEmptyStrings = false)]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        [Display(Name = "Quantity", ResourceType = typeof(App_LocalResources.PaymentProduct))]
        public int? Quantity { get; set; }

        [Display(Name = "Discount", ResourceType = typeof(App_LocalResources.PaymentProduct))]
        public decimal? Discount { get; set; }

        [Display(Name = "UnitPrice", ResourceType = typeof(App_LocalResources.PaymentProduct))]
        public decimal? UnitPrice { get; set; }

        [Display(Name = "TotalPrice", ResourceType = typeof(App_LocalResources.PaymentProduct))]
        public decimal? TotalPrice { get; set; }
    }
}
