using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TSI.GymTech.Entity.Enumerates;

namespace TSI.GymTech.Entity.Models
{
    public class Exercise : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessageResourceName = "ExerciseIdRequired", ErrorMessageResourceType = typeof(App_LocalResources.Exercise), AllowEmptyStrings = false)]
        [Display(Description = "ExerciseId", ResourceType = typeof(App_LocalResources.Exercise))]
        public int ExerciseId { get; set; }

        [Required(ErrorMessageResourceName = "NameRequired", ErrorMessageResourceType = typeof(App_LocalResources.Exercise), AllowEmptyStrings = false)]
        [Display(Description = "Name", ResourceType = typeof(App_LocalResources.Exercise))]
        [StringLength(128)]
        public string Name { get; set; }

        [Display(Description = "Description", ResourceType = typeof(App_LocalResources.Exercise))]
        [StringLength(128)]
        public string Description { get; set; }

        [Display(Description = "Photo", ResourceType = typeof(App_LocalResources.Exercise))]
        [StringLength(64)]
        public string Photo { get; set; }

        [Display(Description = "Comments", ResourceType = typeof(App_LocalResources.Exercise))]
        [DataType(DataType.MultilineText)]
        [StringLength(1024)]
        public string Comments { get; set; }

        [Display(Description = "MuscleWorked", ResourceType = typeof(App_LocalResources.Exercise))]
        [StringLength(64)]
        public string MuscleWorked { get; set; }

        [Display(Description = "MuscularGroup", ResourceType = typeof(App_LocalResources.Exercise))]
        public MuscularGroup? MuscularGroup { get; set; }
    }
}
