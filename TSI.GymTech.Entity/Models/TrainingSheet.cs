using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
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
        
        [Display(Name = "Cycle", ResourceType = typeof(App_LocalResources.TrainingSheet))]
        [StringLength(64)]
        public string Cycle { get; set; }

        [Display(Name = "Status", ResourceType = typeof(App_LocalResources.TrainingSheet))]
        public TrainingSheetStatus? Status { get; set; }

        [Required(ErrorMessageResourceName = "TypeRequired", ErrorMessageResourceType = typeof(App_LocalResources.TrainingSheet), AllowEmptyStrings = false)]
        [Display(Name = "Type", ResourceType = typeof(App_LocalResources.TrainingSheet))]
        public TrainingSheetType Type { get; set; }

        public virtual IEnumerable<TrainingSheetExercise> TrainingSheetExercises { get; set; }
    }
}
