using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TSI.GymTech.Entity.Models
{
    public class TrainingSheetExercise : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessageResourceName = "TrainingSheetExerciseIdRequired", ErrorMessageResourceType = typeof(App_LocalResources.TrainingSheetExercise), AllowEmptyStrings = false)]
        [Display(Description = "TrainingSheetExerciseId", ResourceType = typeof(App_LocalResources.TrainingSheetExercise))]
        public int TrainingSheetExerciseId { get; set; }

        [ForeignKey("TrainingSheet")]
        [Required(ErrorMessageResourceName = "TrainingSheetRequired", ErrorMessageResourceType = typeof(App_LocalResources.TrainingSheetExercise), AllowEmptyStrings = false)]
        public int TrainingSheetId { get; set; }
        public virtual TrainingSheet TrainingSheet { get; set; }

        [ForeignKey("Exercise")]
        [Required(ErrorMessageResourceName = "ExerciseRequired", ErrorMessageResourceType = typeof(App_LocalResources.TrainingSheetExercise), AllowEmptyStrings = false)]
        public int ExerciseId { get; set; }
        public virtual Exercise Exercise { get; set; }

        [Required(ErrorMessageResourceName = "SerieRequired", ErrorMessageResourceType = typeof(App_LocalResources.TrainingSheetExercise), AllowEmptyStrings = false)]
        [Display(Description = "Serie", ResourceType = typeof(App_LocalResources.TrainingSheetExercise))]
        public int Serie { get; set; }

        [Required(ErrorMessageResourceName = "NumberOfRepetitionsRequired", ErrorMessageResourceType = typeof(App_LocalResources.TrainingSheetExercise), AllowEmptyStrings = false)]
        [Display(Description = "NumberOfRepetitions", ResourceType = typeof(App_LocalResources.TrainingSheetExercise))]
        public int NumberOfRepetitions { get; set; }

        [Display(Description = "Charge", ResourceType = typeof(App_LocalResources.TrainingSheetExercise))]
        public decimal? Charge { get; set; }
    }
}
