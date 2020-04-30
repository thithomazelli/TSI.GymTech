using System;
using System.ComponentModel.DataAnnotations;
using TSI.GymTech.Entity.Enumerates;

namespace TSI.GymTech.Entity.Models
{
    public class AccessLogView
    {
        [Key]
        public int AccessLogId { get; set; }

        public int PersonId { get; set; }

        public string PersonName { get; set; }

        public PersonType PersonProfileType { get; set; }

        public GateStatusType AccessType { get; set; }

        public string MessageDisplayed { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
