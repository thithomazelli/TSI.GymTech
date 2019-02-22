using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TSI.GymTech.Entity.Models
{
    public class AnamnesisSheet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessageResourceName = "AnamnesisSheetIdRequired", ErrorMessageResourceType = typeof(App_LocalResources.AnamnesisSheet), AllowEmptyStrings = false)]
        [Display(Description = "AnamnesisSheetId", ResourceType = typeof(App_LocalResources.AnamnesisSheet))]
        public int AnamnesisSheetId { get; set; }

        [ForeignKey("SheetId")]
        public IEnumerable<SheetAnswer> AnamnesisSheetAnswers { get; set; }
    }
}
