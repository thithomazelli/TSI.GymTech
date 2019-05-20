using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TSI.GymTech.Entity.Enumerates;

namespace TSI.GymTech.Entity.Models
{
    public class Payment : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessageResourceName = "PaymentIdRequired", ErrorMessageResourceType = typeof(App_LocalResources.Payment), AllowEmptyStrings = false)]
        [Display(Name = "PaymentId", ResourceType = typeof(App_LocalResources.Payment))]
        public int PaymentId { get; set; }
        
        [ForeignKey("Person")]
        [Required(ErrorMessageResourceName = "PersonRequired", ErrorMessageResourceType = typeof(App_LocalResources.Payment), AllowEmptyStrings = false)]
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }

        [Display(Name = "Discount", ResourceType = typeof(App_LocalResources.Payment))]
        public decimal? Discount { get; set; }

        [Display(Name = "TotalPrice", ResourceType = typeof(App_LocalResources.Payment))]
        public decimal? TotalPrice { get; set; }

        [Display(Name = "PaymentType", ResourceType = typeof(App_LocalResources.Payment))]
        public PaymentType? PaymentType { get; set; }

        [Required(ErrorMessageResourceName = "StatusRequired", ErrorMessageResourceType = typeof(App_LocalResources.Payment), AllowEmptyStrings = false)]
        [Display(Name = "Status", ResourceType = typeof(App_LocalResources.Payment))]
        public PaymentStatus? Status { get; set; }

        [Display(Name = "DatePaymentEstimated", ResourceType = typeof(App_LocalResources.Payment))]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? DatePaymentEstimated { get; set; }

        [Display(Name = "DatePaymentCompleted", ResourceType = typeof(App_LocalResources.Payment))]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? DatePaymentCompleted { get; set; }

        [Display(Name = "Comments", ResourceType = typeof(App_LocalResources.Payment))]
        [DataType(DataType.MultilineText)]
        [StringLength(1024)]
        public string Comments { get; set; }

        public virtual IEnumerable<PaymentProduct> PaymentProducts { get; set; }
    }
}
