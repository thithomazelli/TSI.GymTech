using System.ComponentModel.DataAnnotations;
using TSI.GymTech.Entity.Enumerates;

namespace TSI.GymTech.Entity.Models
{
    public class StudentFrequentView
    {
        [Key]
        public int PersonId { get; set; }

        public string Name { get; set; }

        public string SocialSecurityCard { get; set; }

        public PersonStatus Status { get; set; }

        public string Email { get; set; }
    }
}
