using System;
using System.ComponentModel.DataAnnotations;
using TSI.GymTech.Entity.Enumerates;

namespace TSI.GymTech.Entity.Models
{
    public class PaymentView
    {
        [Key]
        public int PaymentId { get; set; }

        public string Description { get; set; }
        
        public int? StudentId { get; set; }

        public string StudentName { get; set; }

        public PaymentType? PaymentType { get; set; }

        public PaymentStatus? Status { get; set; }

        public DateTime? DatePaymentEstimated { get; set; }

        public DateTime? DatePaymentCompleted { get; set; }

        public decimal? Discount { get; set; }

        public decimal? TotalPrice { get; set; }
    }
}
