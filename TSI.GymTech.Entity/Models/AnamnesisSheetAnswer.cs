using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TSI.GymTech.Entity.Models
{
    public class AnamnesisSheetAnswer : SheetAnswerModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessageResourceName = "AnamnesisSheetAnswerIdRequired", ErrorMessageResourceType = typeof(App_LocalResources.AnamnesisSheetAnswer), AllowEmptyStrings = false)]
        [Display(Name = "AnamnesisSheetAnswerId", ResourceType = typeof(App_LocalResources.AnamnesisSheetAnswer))]
        public int AnamnesisSheetAnswerId { get; set; }

        [ForeignKey("EvaluationSheet")]
        [Required(ErrorMessageResourceName = "EvaluationSheetIdRequired", ErrorMessageResourceType = typeof(App_LocalResources.EvaluationSheetAnswer), AllowEmptyStrings = false)]
        [Display(Name = "EvaluationSheetId", ResourceType = typeof(App_LocalResources.EvaluationSheetAnswer))]
        public int EvaluationSheetId { get; set; }

        [Display(Name = "EvaluationSheet", ResourceType = typeof(App_LocalResources.EvaluationSheetAnswer))]
        public virtual EvaluationSheet EvaluationSheet { get; set; }
    }
}
