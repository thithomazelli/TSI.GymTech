using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TSI.GymTech.Entity.Enumerates;

namespace TSI.GymTech.Entity.Models
{
    public class SheetQuestion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessageResourceName = "SheetQuestionRequired", ErrorMessageResourceType = typeof(App_LocalResources.SheetQuestion), AllowEmptyStrings = false)]
        [Display(Description = "SheetQuestionId", ResourceType = typeof(App_LocalResources.SheetQuestion))]
        public int SheetQuestionId { get; set; }

        [Required(ErrorMessageResourceName = "TypeQuestionRequired", ErrorMessageResourceType = typeof(App_LocalResources.SheetQuestion), AllowEmptyStrings = false)]
        [Display(Description = "TypeQuestion", ResourceType = typeof(App_LocalResources.SheetQuestion))]
        public SheetQuestionType TypeQuestion { get; set; }

        [Display(Description = "Order", ResourceType = typeof(App_LocalResources.SheetQuestion))]
        public int? Order { get; set; }

        [Required(ErrorMessageResourceName = "QuestionRequired", ErrorMessageResourceType = typeof(App_LocalResources.SheetQuestion), AllowEmptyStrings = false)]
        [Display(Description = "Question", ResourceType = typeof(App_LocalResources.SheetQuestion))]
        [StringLength(128)]
        public string Question { get; set; }
    }
}
