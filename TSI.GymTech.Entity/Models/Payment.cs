using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TSI.GymTech.Entity.Enumerates;

namespace TSI.GymTech.Entity.Models
{
    public class Payment : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessageResourceName = "PaymentIdRequired", ErrorMessageResourceType = typeof(App_LocalResources.Payment), AllowEmptyStrings = false)]
        [Display(Description = "PaymentId", ResourceType = typeof(App_LocalResources.Payment))]
        public int PaymentId { get; set; }

        [Display(Description = "ProductName", ResourceType = typeof(App_LocalResources.Payment))]
        [StringLength(128)]
        public string ProductName { get; set; }

        [ForeignKey("Person")]
        [Required(ErrorMessageResourceName = "PersonRequired", ErrorMessageResourceType = typeof(App_LocalResources.Payment), AllowEmptyStrings = false)]
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }

        [Display(Description = "Discount", ResourceType = typeof(App_LocalResources.Payment))]
        public decimal? Discount { get; set; }

        [Display(Description = "TotalPrice", ResourceType = typeof(App_LocalResources.Payment))]
        public decimal? TotalPrice { get; set; }

        [Display(Description = "PaymentType", ResourceType = typeof(App_LocalResources.Payment))]
        public PaymentType? PaymentType { get; set; }

        [Required(ErrorMessageResourceName = "StatusRequired", ErrorMessageResourceType = typeof(App_LocalResources.Payment), AllowEmptyStrings = false)]
        [Display(Description = "Status", ResourceType = typeof(App_LocalResources.Payment))]
        public PaymentStatus? Status { get; set; }

        [Display(Description = "DatePaymentEstimated", ResourceType = typeof(App_LocalResources.Payment))]
        [DataType(DataType.DateTime)]
        public DateTime? DatePaymentEstimated { get; set; }

        [Display(Description = "DatePaymentCompleted", ResourceType = typeof(App_LocalResources.Payment))]
        [DataType(DataType.DateTime)]
        public DateTime? DatePaymentCompleted { get; set; }

        [Display(Description = "Comments", ResourceType = typeof(App_LocalResources.Payment))]
        [DataType(DataType.MultilineText)]
        [StringLength(1024)]
        public string Comments { get; set; }

        public virtual IEnumerable<PaymentProduct> PaymentProducts { get; set; }
    }
}
