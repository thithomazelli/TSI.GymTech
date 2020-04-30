using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TSI.GymTech.Entity.Enumerates;

namespace TSI.GymTech.Entity.Models
{
    public class SheetQuestion : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessageResourceName = "SheetQuestionIdRequired", ErrorMessageResourceType = typeof(App_LocalResources.SheetQuestion), AllowEmptyStrings = false)]
        [Display(Name = "SheetQuestionId", ResourceType = typeof(App_LocalResources.SheetQuestion))]
        public int SheetQuestionId { get; set; }

        [Required(ErrorMessageResourceName = "AnswerTypeRequired", ErrorMessageResourceType = typeof(App_LocalResources.SheetQuestion), AllowEmptyStrings = false)]
        [Display(Name = "AnswerType", ResourceType = typeof(App_LocalResources.SheetQuestion))]
        public SheetAnswerType AnswerType { get; set; }

        [Required(ErrorMessageResourceName = "QuestionTypeRequired", ErrorMessageResourceType = typeof(App_LocalResources.SheetQuestion), AllowEmptyStrings = false)]
        [Display(Name = "QuestionType", ResourceType = typeof(App_LocalResources.SheetQuestion))]
        public SheetQuestionType? QuestionType { get; set; }

        [Display(Name = "Order", ResourceType = typeof(App_LocalResources.SheetQuestion))]
        public int? Order { get; set; }

        [Required(ErrorMessageResourceName = "QuestionRequired", ErrorMessageResourceType = typeof(App_LocalResources.SheetQuestion), AllowEmptyStrings = false)]
        [Display(Name = "Question", ResourceType = typeof(App_LocalResources.SheetQuestion))]
        [StringLength(128)]
        public string Question { get; set; }
    }
}
