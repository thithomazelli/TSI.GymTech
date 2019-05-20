using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TSI.GymTech.Entity.Enumerates;

namespace TSI.GymTech.Entity.Models
{
    public class TrainingSheet : BaseSheet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessageResourceName = "TrainingSheetIdRequired", ErrorMessageResourceType = typeof(App_LocalResources.TrainingSheet), AllowEmptyStrings = false)]
        [Display(Name = "TrainingSheetId", ResourceType = typeof(App_LocalResources.TrainingSheet))]
        public int TrainingSheetId { get; set; }

        [Required(ErrorMessageResourceName = "TypeRequired", ErrorMessageResourceType = typeof(App_LocalResources.TrainingSheet), AllowEmptyStrings = false)]
        [Display(Name = "Type", ResourceType = typeof(App_LocalResources.TrainingSheet))]
        public TrainingSheetType Type { get; set; }

        [Required(ErrorMessageResourceName = "ModelRequired", ErrorMessageResourceType = typeof(App_LocalResources.TrainingSheet), AllowEmptyStrings = false)]
        [Display(Name = "Model", ResourceType = typeof(App_LocalResources.TrainingSheet))]
        public TrainingSheetModel Model { get; set; }
        
        [ForeignKey("Student")]
        [Display(Name = "StudentId", ResourceType = typeof(App_LocalResources.TrainingSheet))]
        public int? StudentId { get; set; }

        [Display(Name = "Student", ResourceType = typeof(App_LocalResources.TrainingSheet))]
        public virtual Person Student { get; set; }

        public virtual ICollection<TrainingSheetExercise> TrainingSheetExercises { get; set; }
    }
}
