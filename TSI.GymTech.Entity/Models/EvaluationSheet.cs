using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TSI.GymTech.Entity.Models
{
    public class EvaluationSheet : BaseSheet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessageResourceName = "EvaluationSheetIdRequired", ErrorMessageResourceType = typeof(App_LocalResources.EvaluationSheet), AllowEmptyStrings = false)]
        [Display(Description = "EvaluationSheetId", ResourceType = typeof(App_LocalResources.EvaluationSheet))]
        public int EvaluationSheetId { get; set; }

        [ForeignKey("SheetId")]
        public IEnumerable<SheetAnswer> EvaluationSheetAnswers { get; set; }
    }
}
