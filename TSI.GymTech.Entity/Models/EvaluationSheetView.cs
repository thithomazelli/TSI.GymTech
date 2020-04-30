using System;
using System.ComponentModel.DataAnnotations;
using TSI.GymTech.Entity.Enumerates;

namespace TSI.GymTech.Entity.Models
{
    public class EvaluationSheetView
    {
        [Key]
        public int EvaluationSheetId { get; set; }

        public string Description { get; set; }

        public int StudentId { get; set; }

        public string StudentName { get; set; }

        public DateTime? Revaluation { get; set; }

        public TrainingSheetStatus? Status { get; set; }

        public string Comments { get; set; }
    }
}
