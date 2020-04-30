using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TSI.GymTech.Entity.Models
{
    public abstract class SheetAnswerModel : BaseModel
    {
        [Display(Name = "Answer", ResourceType = typeof(App_LocalResources.SheetAnswer))]
        [StringLength(128)]
        public string Answer { get; set; }

        [ForeignKey("SheetQuestion")]
        [Required(ErrorMessageResourceName = "SheetQuestionRequired", ErrorMessageResourceType = typeof(App_LocalResources.SheetAnswer), AllowEmptyStrings = false)]
        [Display(Name = "SheetQuestionId", ResourceType = typeof(App_LocalResources.SheetAnswer))]
        public int SheetQuestionId { get; set; }
        public virtual SheetQuestion SheetQuestion { get; set; }
    }
}
