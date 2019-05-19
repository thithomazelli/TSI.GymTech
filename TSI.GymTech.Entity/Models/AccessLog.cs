using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Web.Script.Serialization;
using TSI.GymTech.Entity.Enumerates;

namespace TSI.GymTech.Entity.Models
{
    [Table("accesslog")]
    public class AccessLog : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessageResourceName = "AccessLogIdRequired", ErrorMessageResourceType = typeof(App_LocalResources.AccessLog), AllowEmptyStrings = false)]
        [Display(Name = "AccessLogId", ResourceType = typeof(App_LocalResources.AccessLog))]
        public int AccessLogId { get; set; }

        [Display(Name = "AccessType", ResourceType = typeof(App_LocalResources.AccessLog))]
        public GateStatusType AccessType { get; set; }

        [Display(Name = "MessageDisplayed", ResourceType = typeof(App_LocalResources.AccessLog))]
        public string MessageDisplayed { get; set; }

        [ForeignKey("Person")]
        [Required(ErrorMessageResourceName = "PersonRequired", ErrorMessageResourceType = typeof(App_LocalResources.Payment), AllowEmptyStrings = false)]
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }

        [ForeignKey("AccessControl")]
        public int AccessControlId { get; set; }
        public virtual AccessControl AccessControl { get; set; }
    }
}
