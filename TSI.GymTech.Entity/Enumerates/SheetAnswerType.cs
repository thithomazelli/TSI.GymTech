using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSI.GymTech.Entity.Enumerates
{
    public enum SheetAnswerType
    {
        [Display(ResourceType = typeof(App_LocalResources.SheetAnswerType), Name = "String")]
        String = 0,
        [Display(ResourceType = typeof(App_LocalResources.SheetAnswerType), Name = "Boolean")]
        Boolean = 1,
        [Display(ResourceType = typeof(App_LocalResources.SheetAnswerType), Name = "YesOrNo")]
        YesOrNo = 2
    }
}
