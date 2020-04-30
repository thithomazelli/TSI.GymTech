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

        [Display(Name = "Description", ResourceType = typeof(App_LocalResources.Payment))]
        [Required(ErrorMessageResourceName = "DescriptionRequired", ErrorMessageResourceType = typeof(App_LocalResources.Payment), AllowEmptyStrings = false)]
        public string Description { get; set; }

        [Display(Name = "PaymentType", ResourceType = typeof(App_LocalResources.Payment))]
        public PaymentType? PaymentType { get; set; }

        [Required(ErrorMessageResourceName = "StatusRequired", ErrorMessageResourceType = typeof(App_LocalResources.Payment), AllowEmptyStrings = false)]
        [Display(Name = "Status", ResourceType = typeof(App_LocalResources.Payment))]
        public PaymentStatus Status { get; set; }

        [Display(Name = "DatePaymentEstimated", ResourceType = typeof(App_LocalResources.Payment))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DatePaymentEstimated { get; set; }

        [Display(Name = "DatePaymentCompleted", ResourceType = typeof(App_LocalResources.Payment))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DatePaymentCompleted { get; set; }

        [Display(Name = "Comments", ResourceType = typeof(App_LocalResources.Payment))]
        [DataType(DataType.MultilineText)]
        [StringLength(1024)]
        public string Comments { get; set; }

        [Display(Name = "Discount", ResourceType = typeof(App_LocalResources.Payment))]
        public decimal Discount { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "TotalPrice", ResourceType = typeof(App_LocalResources.Payment))]
        public decimal TotalPrice { get; set; }

        [ForeignKey("Student")]
        [Required(ErrorMessageResourceName = "StudentRequired", ErrorMessageResourceType = typeof(App_LocalResources.Payment), AllowEmptyStrings = false)]
        public int StudentId { get; set; }

        [Display(Name = "Student", ResourceType = typeof(App_LocalResources.Payment))]
        public virtual Person Student { get; set; }

        public virtual ICollection<PaymentProduct> PaymentProducts { get; set; }
    }
}
