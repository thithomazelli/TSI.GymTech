using System.ComponentModel.DataAnnotations;
using TSI.GymTech.Entity.Enumerates;

namespace TSI.GymTech.Entity.Models
{
    public class TrainingSheetView
    {
        [Key]
        public int TrainingSheetId { get; set; }

        public string Name { get; set; }

        public string Cycle { get; set; }

        public int? StudentId { get; set; }

        public string StudentName { get; set; }

        public TrainingSheetModel Model { get; set; }

        public TrainingSheetStatus? Status { get; set; }

        public TrainingSheetType Type { get; set; }
    }
}
