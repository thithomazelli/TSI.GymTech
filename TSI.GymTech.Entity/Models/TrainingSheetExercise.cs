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
        [Display(Name = "TrainingSheetExerciseId", ResourceType = typeof(App_LocalResources.TrainingSheetExercise))]
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
        [Display(Name = "Serie", ResourceType = typeof(App_LocalResources.TrainingSheetExercise))]
        public int Serie { get; set; }

        [Required(ErrorMessageResourceName = "NumberOfRepetitionsRequired", ErrorMessageResourceType = typeof(App_LocalResources.TrainingSheetExercise), AllowEmptyStrings = false)]
        [Display(Name = "NumberOfRepetitions", ResourceType = typeof(App_LocalResources.TrainingSheetExercise))]
        public int NumberOfRepetitions { get; set; }

        [Display(Name = "Charge", ResourceType = typeof(App_LocalResources.TrainingSheetExercise))]
        public string Charge { get; set; }
    }
}
