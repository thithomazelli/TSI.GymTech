using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TSI.GymTech.Entity.Enumerates;

namespace TSI.GymTech.Entity.Models
{
    public class Address : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessageResourceName = "AddressIdRequired", ErrorMessageResourceType = typeof(App_LocalResources.Address), AllowEmptyStrings = false)]
        [Display(Description = "AddressId", ResourceType = typeof(App_LocalResources.Address))]
        public int AddressId { get; set; }

        [Display(Description = "AddressType", ResourceType = typeof(App_LocalResources.Address))]
        public AddressType? AddressType { get; set; }

        [Display(Description = "PostalCode", ResourceType = typeof(App_LocalResources.Address))]
        [StringLength(16)]
        public string PostalCode { get; set; }

        [Display(Description = "Street", ResourceType = typeof(App_LocalResources.Address))]
        [StringLength(64)]
        public string Street { get; set; }

        [Display(Description = "Number", ResourceType = typeof(App_LocalResources.Address))]
        public int? Number { get; set; }

        [Display(Description = "Comments", ResourceType = typeof(App_LocalResources.Address))]
        [StringLength(128)]
        public string Comments { get; set; }

        [Display(Description = "District", ResourceType = typeof(App_LocalResources.Address))]
        [StringLength(32)]
        public string District { get; set; }

        [Display(Description = "State", ResourceType = typeof(App_LocalResources.Address))]
        [StringLength(32)]
        public string State { get; set; }

        [Display(Description = "City", ResourceType = typeof(App_LocalResources.Address))]
        [StringLength(64)]
        public string City { get; set; }

        [Display(Description = "Country", ResourceType = typeof(App_LocalResources.Address))]
        [StringLength(32)]
        public string Country { get; set; }

    }
}
