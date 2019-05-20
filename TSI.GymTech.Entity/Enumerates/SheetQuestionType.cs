using System.ComponentModel.DataAnnotations;

namespace TSI.GymTech.Entity.Enumerates
{
    public enum SheetQuestionType
    {
        [Display(ResourceType = typeof(App_LocalResources.SheetQuestionType), Name = "Anamnesis")]
        Anamnesis = 0,
        [Display(ResourceType = typeof(App_LocalResources.SheetQuestionType), Name = "Evaluation")]
        Evaluation = 1
    }
}
