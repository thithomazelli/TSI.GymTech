using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TSI.GymTech.Entity.Enumerates;

namespace TSI.GymTech.Entity.Models
{
    public class PaymentProduct : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessageResourceName = "PaymentProductIdRequired", ErrorMessageResourceType = typeof(App_LocalResources.PaymentProduct), AllowEmptyStrings = false)]
        [Display(Name = "PaymentProductId", ResourceType = typeof(App_LocalResources.PaymentProduct))]
        public int PaymentProductId { get; set; }

        [Display(Name = "Description", ResourceType = typeof(App_LocalResources.PaymentProduct))]
        public string Description { get; set; }

        [Display(Name = "PaymentType", ResourceType = typeof(App_LocalResources.PaymentProduct))]
        public PaymentType? PaymentType { get; set; }

        [Required(ErrorMessageResourceName = "StatusRequired", ErrorMessageResourceType = typeof(App_LocalResources.PaymentProduct), AllowEmptyStrings = false)]
        [Display(Name = "Status", ResourceType = typeof(App_LocalResources.PaymentProduct))]
        public PaymentStatus Status { get; set; }

        [Display(Name = "Quantity", ResourceType = typeof(App_LocalResources.PaymentProduct))]
        public int Quantity { get; set; }

        [Display(Name = "Discount", ResourceType = typeof(App_LocalResources.PaymentProduct))]
        public decimal Discount { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "UnitPrice", ResourceType = typeof(App_LocalResources.PaymentProduct))]
        public decimal UnitPrice { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "TotalPrice", ResourceType = typeof(App_LocalResources.PaymentProduct))]
        public decimal TotalPrice { get; set; }

        [Display(Name = "Quota", ResourceType = typeof(App_LocalResources.PaymentProduct))]
        public int? Quota { get; set; }

        [ForeignKey("Payment")]
        [Required(ErrorMessageResourceName = "PaymentRequired", ErrorMessageResourceType = typeof(App_LocalResources.PaymentProduct), AllowEmptyStrings = false)]
        public int PaymentId { get; set; }
        public virtual Payment Payment { get; set; }

        [ForeignKey("Product")]
        [Required(ErrorMessageResourceName = "ProductRequired", ErrorMessageResourceType = typeof(App_LocalResources.PaymentProduct), AllowEmptyStrings = false)]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }


    }
}
