using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TSI.GymTech.Entity.Models
{
    public class EvaluationSheet : BaseSheet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessageResourceName = "EvaluationSheetIdRequired", ErrorMessageResourceType = typeof(App_LocalResources.EvaluationSheet), AllowEmptyStrings = false)]
        [Display(Name = "EvaluationSheetId", ResourceType = typeof(App_LocalResources.EvaluationSheet))]
        public int EvaluationSheetId { get; set; }
        
        [ForeignKey("Student")]
        [Required(ErrorMessageResourceName = "StudentRequired", ErrorMessageResourceType = typeof(App_LocalResources.TrainingSheet), AllowEmptyStrings = false)]
        [Display(Name = "StudentId", ResourceType = typeof(App_LocalResources.TrainingSheet))]
        public int? StudentId { get; set; }
        public virtual Person Student { get; set; }

        public virtual IEnumerable<SheetAnswer> EvaluationSheetAnswers { get; set; }
    }
}
