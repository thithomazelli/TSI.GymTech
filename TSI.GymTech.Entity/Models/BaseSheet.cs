using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TSI.GymTech.Entity.Models
{
    public class BaseSheet : BaseModel
    {
        [Display(Description = "Revaluation", ResourceType = typeof(App_LocalResources.BaseSheet))]
        [DataType(DataType.Date)]
        public DateTime? Revaluation { get; set; }

        [Display(Description = "Comments", ResourceType = typeof(App_LocalResources.BaseSheet))]
        [DataType(DataType.MultilineText)]
        [StringLength(1024)]
        public string Comments { get; set; }

        [ForeignKey("PersonId")]
        [Required(ErrorMessageResourceName = "StudentRequired", ErrorMessageResourceType = typeof(App_LocalResources.BaseSheet), AllowEmptyStrings = false)]
        [Display(Description = "StudentId", ResourceType = typeof(App_LocalResources.BaseSheet))]
        public int StudentId { get; set; }
        public virtual Person Student { get; set; }

        [ForeignKey("PersonId")]
        [Required(ErrorMessageResourceName = "TrainerRequired", ErrorMessageResourceType = typeof(App_LocalResources.BaseSheet), AllowEmptyStrings = false)]
        [Display(Description = "TrainerId", ResourceType = typeof(App_LocalResources.BaseSheet))]
        public int TrainerId { get; set; }
        public virtual Person Trainer { get; set; }
    }
}
