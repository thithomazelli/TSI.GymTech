using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TSI.GymTech.Entity.Models
{
    public class AnamnesisSheet : BaseSheet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessageResourceName = "AnamnesisSheetIdRequired", ErrorMessageResourceType = typeof(App_LocalResources.AnamnesisSheet), AllowEmptyStrings = false)]
        [Display(Name = "AnamnesisSheetId", ResourceType = typeof(App_LocalResources.AnamnesisSheet))]
        public int AnamnesisSheetId { get; set; }
        
        [ForeignKey("Student")]
        [Required(ErrorMessageResourceName = "StudentRequired", ErrorMessageResourceType = typeof(App_LocalResources.TrainingSheet), AllowEmptyStrings = false)]
        [Display(Name = "StudentId", ResourceType = typeof(App_LocalResources.TrainingSheet))]
        public int? StudentId { get; set; }
        public virtual Person Student { get; set; }

        public virtual IEnumerable<SheetAnswer> AnamnesisSheetAnswers { get; set; }
    }
}
