using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TSI.GymTech.Entity.Models
{
    public class AccessControl : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessageResourceName = "AccessControlIdRequired", ErrorMessageResourceType = typeof(App_LocalResources.AccessControl), AllowEmptyStrings = false)]
        [Display(Description = "AccessControlId", ResourceType = typeof(App_LocalResources.AccessControl))]
        public int AccessControlId { get; set; }

        [Required(ErrorMessageResourceName = "IpAddressRequired", ErrorMessageResourceType = typeof(App_LocalResources.AccessControl), AllowEmptyStrings = false)]
        [Display(Description = "IpAddress", ResourceType = typeof(App_LocalResources.AccessControl))]
        [StringLength(16)]
        public string IpAddress { get; set; }
        
        [Display(Description = "Name", ResourceType = typeof(App_LocalResources.AccessControl))]
        [StringLength(64)]
        public string Name { get; set; }

        [Required(ErrorMessageResourceName = "IsStandardRequired", ErrorMessageResourceType = typeof(App_LocalResources.AccessControl), AllowEmptyStrings = false)]
        [Display(Description = "IsStandard", ResourceType = typeof(App_LocalResources.AccessControl))]
        public bool IsStandard { get; set; }
    }
}
