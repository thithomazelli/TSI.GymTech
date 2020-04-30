using System;
using System.ComponentModel.DataAnnotations;
using TSI.GymTech.Entity.Enumerates;

namespace TSI.GymTech.Entity.Models
{
    public abstract class BaseSheet : BaseModel
    {
        [Display(Name = "Description", ResourceType = typeof(App_LocalResources.BaseSheet))]
        [Required(ErrorMessageResourceName = "DescriptionRequired", ErrorMessageResourceType = typeof(App_LocalResources.BaseSheet), AllowEmptyStrings = false)]
        public string Description { get; set; }

        [Display(Name = "Cycle", ResourceType = typeof(App_LocalResources.BaseSheet))]
        [StringLength(64)]
        public string Cycle { get; set; }

        [Display(Name = "Status", ResourceType = typeof(App_LocalResources.BaseSheet))]
        public TrainingSheetStatus? Status { get; set; }

        [Display(Name = "Revaluation", ResourceType = typeof(App_LocalResources.BaseSheet))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Revaluation { get; set; }

        [Display(Name = "Comments", ResourceType = typeof(App_LocalResources.BaseSheet))]
        [DataType(DataType.MultilineText)]
        [StringLength(1024)]
        public string Comments { get; set; }
    }
}
