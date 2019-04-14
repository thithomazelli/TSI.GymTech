using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TSI.GymTech.Entity.Models
{
    [Table("accesscontrol")]
    public class AccessControl : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessageResourceName = "AccessControlIdRequired", ErrorMessageResourceType = typeof(App_LocalResources.AccessControl), AllowEmptyStrings = false)]
        [Display(Name = "AccessControlId", ResourceType = typeof(App_LocalResources.AccessControl))]
        public int AccessControlId { get; set; }

        [Required(ErrorMessageResourceName = "IpAddressRequired", ErrorMessageResourceType = typeof(App_LocalResources.AccessControl), AllowEmptyStrings = false)]
        [Display(Name = "IpAddress", ResourceType = typeof(App_LocalResources.AccessControl))]
        [StringLength(16)]
        public string IpAddress { get; set; }
        
        [Display(Name = "Name", ResourceType = typeof(App_LocalResources.AccessControl))]
        [StringLength(64)]
        public string Name { get; set; }

        [Required(ErrorMessageResourceName = "IsStandardRequired", ErrorMessageResourceType = typeof(App_LocalResources.AccessControl), AllowEmptyStrings = false)]
        [Display(Name = "IsStandard", ResourceType = typeof(App_LocalResources.AccessControl))]
        public bool IsStandard { get; set; }
    }
}
