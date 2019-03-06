using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TSI.GymTech.Entity.Models
{
    public class BaseSheet : BaseModel
    {
        [Display(Name = "Revaluation", ResourceType = typeof(App_LocalResources.BaseSheet))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? Revaluation { get; set; }

        [Display(Name = "Comments", ResourceType = typeof(App_LocalResources.BaseSheet))]
        [DataType(DataType.MultilineText)]
        [StringLength(1024)]
        public string Comments { get; set; }

        [ForeignKey("Student")]
        [Required(ErrorMessageResourceName = "StudentRequired", ErrorMessageResourceType = typeof(App_LocalResources.BaseSheet), AllowEmptyStrings = false)]
        [Display(Name = "StudentId", ResourceType = typeof(App_LocalResources.BaseSheet))]
        public int StudentId { get; set; }
        public virtual Person Student { get; set; }

        [ForeignKey("Trainer")]
        [Required(ErrorMessageResourceName = "TrainerRequired", ErrorMessageResourceType = typeof(App_LocalResources.BaseSheet), AllowEmptyStrings = false)]
        [Display(Name = "TrainerId", ResourceType = typeof(App_LocalResources.BaseSheet))]
        public int TrainerId { get; set; }
        public virtual Person Trainer { get; set; }
    }
}
