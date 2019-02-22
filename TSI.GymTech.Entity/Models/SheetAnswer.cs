using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TSI.GymTech.Entity.Models
{
    public class SheetAnswer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessageResourceName = "SheetAnswerIdRequired", ErrorMessageResourceType = typeof(App_LocalResources.SheetAnswer), AllowEmptyStrings = false)]
        [Display(Description = "SheetAnswerId", ResourceType = typeof(App_LocalResources.SheetAnswer))]
        public int SheetAnswerId { get; set; }

        [Display(Description = "Answer", ResourceType = typeof(App_LocalResources.SheetAnswer))]
        [StringLength(128)]
        public string Answer { get; set; }

        [Required(ErrorMessageResourceName = "SheetIdRequired", ErrorMessageResourceType = typeof(App_LocalResources.SheetAnswer), AllowEmptyStrings = false)]
        [Display(Description = "SheetId", ResourceType = typeof(App_LocalResources.SheetAnswer))]
        public int SheetId { get; set; }

        [ForeignKey("SheetQuestion")]
        [Required(ErrorMessageResourceName = "SheetQuestionRequired", ErrorMessageResourceType = typeof(App_LocalResources.SheetAnswer), AllowEmptyStrings = false)]
        public int SheetQuestionId { get; set; }
        public SheetQuestion SheetQuestion { get; set; }
    }
}
