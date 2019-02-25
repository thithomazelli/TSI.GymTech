using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TSI.GymTech.Entity.Models
{
    public class BaseModel
    {
        [Display(Description = "CreateDate", ResourceType = typeof(App_LocalResources.BaseModel))]
        public DateTime? CreateDate { get; set; }

        [Display(Description = "CreateUser", ResourceType = typeof(App_LocalResources.BaseModel))]
        public int CreateUserId { get; set; }
        
        [Display(Description = "ModifyDate", ResourceType = typeof(App_LocalResources.BaseModel))]
        public DateTime? ModifyDate { get; set; }
        
        [Display(Description = "ModifyUser", ResourceType = typeof(App_LocalResources.BaseModel))]
        public int ModifyUserId { get; set; }
    }
}
