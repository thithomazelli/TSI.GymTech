using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TSI.GymTech.Entity.Models
{
    [Table("anamnesissheet")]
    public class AnamnesisSheet : BaseSheet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessageResourceName = "AnamnesisSheetIdRequired", ErrorMessageResourceType = typeof(App_LocalResources.AnamnesisSheet), AllowEmptyStrings = false)]
        [Display(Name = "AnamnesisSheetId", ResourceType = typeof(App_LocalResources.AnamnesisSheet))]
        public int AnamnesisSheetId { get; set; }

        public virtual IEnumerable<SheetAnswer> AnamnesisSheetAnswers { get; set; }
    }
}
