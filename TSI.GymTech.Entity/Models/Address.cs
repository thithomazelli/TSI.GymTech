using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TSI.GymTech.Entity.Enumerates;

namespace TSI.GymTech.Entity.Models
{
    public class Address : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessageResourceName = "AddressIdRequired", ErrorMessageResourceType = typeof(App_LocalResources.Address), AllowEmptyStrings = false)]
        [Display(Name = "AddressId", ResourceType = typeof(App_LocalResources.Address))]
        public int AddressId { get; set; }
        
        [Display(Name = "AddressType", ResourceType = typeof(App_LocalResources.Address))]
        public AddressType? AddressType { get; set; }

        [Display(Name = "PostalCode", ResourceType = typeof(App_LocalResources.Address))]
        [StringLength(16)]
        public string PostalCode { get; set; }

        [Display(Name = "Street", ResourceType = typeof(App_LocalResources.Address))]
        [StringLength(64)]
        public string Street { get; set; }

        [Display(Name = "Number", ResourceType = typeof(App_LocalResources.Address))]
        public int? Number { get; set; }

        [Display(Name = "Comments", ResourceType = typeof(App_LocalResources.Address))]
        [StringLength(128)]
        public string Comments { get; set; }

        [Display(Name = "District", ResourceType = typeof(App_LocalResources.Address))]
        [StringLength(32)]
        public string District { get; set; }

        [Display(Name = "State", ResourceType = typeof(App_LocalResources.Address))]
        [StringLength(32)]
        public string State { get; set; }

        [Display(Name = "City", ResourceType = typeof(App_LocalResources.Address))]
        [StringLength(64)]
        public string City { get; set; }

        [Display(Name = "Country", ResourceType = typeof(App_LocalResources.Address))]
        [StringLength(32)]
        public string Country { get; set; }

        [ForeignKey("Person")]
        public int PersonId { get; set; }

        public virtual Person Person { get; set; }
    }
}
